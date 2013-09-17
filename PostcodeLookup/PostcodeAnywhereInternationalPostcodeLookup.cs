using MKS.PostcodeLookup.Core;
using MKS.PostcodeLookupService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MKS.PostcodeLookupService.Models;
using Newtonsoft.Json;

namespace MKS.PostcodeLookupService
{
    public class PostcodeAnywhereInternationalPostcodeLookup : IPostcodeLookup
    {
        private readonly Uri _serviceUri = new Uri("https://services.postcodeanywhere.co.uk/PostcodeAnywhereInternational/Interactive/RetrieveByPostalCode/v2.20/json3.ws");
        private readonly string _apiKey;
        private const string IsoCode = "GBR";

        public PostcodeAnywhereInternationalPostcodeLookup(string apiKey)
        {
            _apiKey = Uri.EscapeDataString(apiKey);
        }

        public IList<PostcodeLookup.Core.Models.PostCodeLookupAddress> Lookup(string postCode)
        {
            var task = LookupAsync(postCode);
            task.Wait(TimeSpan.FromSeconds(10));
            return task.Result;
        }

        public async Task<IList<PostcodeLookup.Core.Models.PostCodeLookupAddress>> LookupAsync(string postCode)
        {
            if(String.IsNullOrWhiteSpace(postCode)){
                throw new ArgumentException("No Post Code Provided", "postCode");
            }
            if(!PostcodeValidator.IsValid(postCode)){
                throw new ArgumentException("Invalid PostCode Format", "postCode");
            }
            using (var client = new HttpClient())
            {
                var queryString = string.Format("?key={0}&country={1}&postalcode={2}", _apiKey, IsoCode, Uri.EscapeDataString(postCode));
                var response = await client.GetStringAsync(new Uri(_serviceUri, queryString));
                var result = await JsonConvert.DeserializeObjectAsync<PostCodeAnywhereInternationalLookupResult>(response);
                return result.Items.Select(a => new PostcodeLookup.Core.Models.PostCodeLookupAddress
                    {
                        Address = a.Street,
                        City = a.City,
                        County = a.State,
                        Town = a.District,
                        PostCode = a.PostalCode
                    }).ToList();
            }
        }
    }
}
