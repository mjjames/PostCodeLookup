using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MKS.PostcodeLookup.Core.Models;

namespace MKS.PostcodeLookupService.Interfaces
{
    public interface IPostcodeLookup
    {
        IList<PostCodeLookupAddress> Lookup(string postCode);
        Task<IList<PostCodeLookupAddress>> LookupAsync(string postCode);
    }
}
