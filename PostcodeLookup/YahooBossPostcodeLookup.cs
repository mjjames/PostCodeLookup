using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MKS.PostcodeLookupService.Interfaces;
using MKS.PostcodeLookupService.Models;

namespace MKS.PostcodeLookupService
{
    internal class YahooBossPostcodeLookup : IPostcodeLookup
    {
        public IList<Address> Lookup(string postCode)
        {
            throw new NotImplementedException();
        }
    }
}
