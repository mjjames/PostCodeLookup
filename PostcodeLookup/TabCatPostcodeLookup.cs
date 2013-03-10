using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MKS.PostcodeLookup.Core.Models;
using MKS.PostcodeLookupService.Interfaces;

namespace MKS.PostcodeLookupService
{
    public class TabCatPostcodeLookup : IPostcodeLookup
    {
        private readonly Uri _serviceUrl;

        public TabCatPostcodeLookup(Uri serviceUrl)
        {
            _serviceUrl = serviceUrl;
        }

        public IList<PostCodeLookupAddress> Lookup(string postCode)
        {
            var task = LookupPostcodeFromService(postCode);
            task.Wait(TimeSpan.FromSeconds(5));
            return DeserialiseAddresses(task.Result).Result;
        }

        public async Task<IList<PostCodeLookupAddress>> LookupAsync(string postCode)
        {
            var data = await LookupPostcodeFromService(postCode);
            return await DeserialiseAddresses(data);
        }

        private async Task<string> LookupPostcodeFromService(string postCode)
        {
            using (var httpClient = new HttpClient())
            {
                var response =
                    await
                    httpClient.GetAsync("http://www.tabcat.co.uk/postcode_lookup_json_v2.php?postcode=" + postCode);
                // Check that response was successful or throw exception 
                response.EnsureSuccessStatusCode();
                //sadly this postcode api is a bit crap and doesn't return the correct mime type so we can't auto convert
                //so read the string
                return await response.Content.ReadAsStringAsync();
            }
        }

        private async Task<IList<PostCodeLookupAddress>> DeserialiseAddresses(string jsonString)
        {
            jsonString = jsonString.Substring(1, jsonString.Length - 2);
            //then json deserialise using JSON.NET
            var results = await Newtonsoft.Json.JsonConvert.DeserializeObjectAsync<IEnumerable<PostCodeLookupAddress>>(jsonString);

            //now we have results we need to properly order these as we get alot of rubbish returned



            //filter to items which have all fields populated
            var filteredResults = results.Where(r =>
                                                    !String.IsNullOrWhiteSpace(r.Address)
                                                    && !String.IsNullOrWhiteSpace(r.County)
                                                    && !String.IsNullOrWhiteSpace(r.Town)
                                                ).ToList();

            //if we dont have any see we have any with just a county and town
            if (!filteredResults.Any())
            {
                filteredResults = results.Where(r =>
                                                    !String.IsNullOrWhiteSpace(r.County)
                                                    && !String.IsNullOrWhiteSpace(r.Town)
                                                ).ToList();
            }
            return filteredResults.Where(p => p.PostCode != "tabcat.co.uk").ToList();
        }
    }
}
