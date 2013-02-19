using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MKS.PostcodeLookupService.Models;

namespace MKS.PostcodeLookupService.Interfaces
{
    public interface IPostcodeLookup
    {
        IList<PostCodeLookupAddress> Lookup(string postCode);
    }
}
