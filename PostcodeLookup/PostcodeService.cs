using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MKS.PostcodeLookupService.Interfaces;
using MKS.PostcodeLookupService.Models;
using System.Threading.Tasks;

namespace MKS.PostcodeLookupService
{
    public class PostcodeService
    {
        private readonly IPostcodeLookup _lookupService;

        public PostcodeService(IPostcodeLookup lookupService)
        {
            _lookupService = lookupService;
        }

        public async Task<IList<PostCodeLookupAddress>> LookupPostcode(string postCode)
        {
            if (string.IsNullOrWhiteSpace(postCode) || postCode.Replace(" ", "").Length > 7 || postCode.Length < 5)
            {
                throw new ArgumentException("invalid postCode provided");
            }
            
            //if the postcode regex doesn't match error
            if (!Regex.IsMatch(postCode, @"[A-Z]{1,2}[0-9]{1,2}[A-Z]{0,1}\s{0,1}[0-9]{1,2}[A-Z]{1,2}",RegexOptions.IgnoreCase))
            {
                throw new ArgumentException("postCode has invalid format");
            }

            return await _lookupService.LookupAsync(postCode);
        }
    }
}
