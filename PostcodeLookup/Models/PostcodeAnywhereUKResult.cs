using System.Collections.Generic;

namespace MKS.PostcodeLookupService.Models
{
    public class PostcodeAnywhereUKResult
    {
        public IEnumerable<PostcodeAnywhereUKAddress> Items { get; set; }
    }
}
