using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DotNetOpenAuth.OAuth;
using MKS.PostcodeLookupService.Interfaces;
using MKS.PostcodeLookupService.Models;

namespace MKS.PostcodeLookupService
{
    internal class YahooBossPostcodeLookup : IPostcodeLookup
    {
        public IList<PostCodeLookupAddress> Lookup(string postCode)
        {
            var bossResultData = BossQuery(postCode);
            bossResultData.Wait(TimeSpan.FromSeconds(5));
            throw new NotImplementedException();
        }
        
        public async Task<IList<PostCodeLookupAddress>> LookupAsync(string postCode)
        {
            var bossResultData = await BossQuery(postCode);
            throw new NotImplementedException();
        }

        private Task<string> BossQuery(string postCode)
        {
            throw new NotImplementedException();
           //var request = new DotNetOpenAuth.OAuth.WebConsumer(new ServiceProviderDescription(), new  )
        }
    }
}
