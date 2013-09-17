using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MKS.PostcodeLookup.Core;
using MKS.PostcodeLookupService.Interfaces;
using MKS.PostcodeLookupService.Models;
using Newtonsoft.Json;

namespace MKS.PostcodeLookupService
{
    public class PostcodeAnywhereUKPostcodeLookup : IPostcodeLookup
    {
        private readonly Uri _serviceUri = new Uri("http://services.postcodeanywhere.co.uk/PostcodeAnywhere/Interactive/RetrieveByPostcodeAndBuilding/v1.30/json3.ws");

        private readonly string _apiKey;

        public PostcodeAnywhereUKPostcodeLookup(string apiKey)
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
            if (String.IsNullOrWhiteSpace(postCode))
            {
                throw new ArgumentException("No Post Code Provided", "postCode");
            }
            if (!PostcodeValidator.IsValid(postCode))
            {
                throw new ArgumentException("Invalid PostCode Format", "postCode");
            }
            using (var client = new HttpClient())
            {
                var queryString = string.Format("?key={0}&postcode={1}", _apiKey, Uri.EscapeDataString(postCode));
                var response = await client.GetStringAsync(new Uri(_serviceUri, queryString));
                var result = await JsonConvert.DeserializeObjectAsync<PostcodeAnywhereUKResult>(response);
                return result.Items.Select(a => new PostcodeLookup.Core.Models.PostCodeLookupAddress
                    {
                        Address = a.Line1,
                        City = a.PostTown,
                        County = a.County,
                        Town = a.Line2,
                        PostCode = a.Postcode
                    }).ToList();
            }
        }
    }
}