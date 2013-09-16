using System.Collections.Generic;

namespace MKS.PostcodeLookupService.Models
{
    internal class PostCodeAnywhereLookupResult
    {
        public IEnumerable<PostcodeAnywhereAddress> Items { get; set; }
    }
}
