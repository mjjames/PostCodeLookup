using System;

namespace MKS.PostcodeLookup.Core.Models
{
    public class PostCodeLookupAddress : IEquatable<PostCodeLookupAddress>
    {
        public string Address { get; set; }
        public string Town { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string PostCode { get; set; }

        public bool Equals(PostCodeLookupAddress other)
        {
            return String.Equals(Address, other.Address)
            && String.Equals(Town, other.Town)
            && String.Equals(City, other.City)
            && String.Equals(County, other.County)
            && String.Equals(PostCode, other.PostCode);
        }


        public override bool Equals(object obj)
        {
            var otherAddress = obj as PostCodeLookupAddress;
            return otherAddress != null && Equals(otherAddress);
        }

        public override int GetHashCode()
        {
            var hash = 13;
            hash = (hash * 7) + Address.GetHashCode();
            hash = (hash * 7) + Town.GetHashCode();
            hash = (hash * 7) + County.GetHashCode();
            hash = (hash * 7) + City.GetHashCode();
            hash = (hash * 7) + PostCode.GetHashCode();
            return hash;
        }


    }
}
