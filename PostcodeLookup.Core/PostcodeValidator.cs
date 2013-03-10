using System.Text.RegularExpressions;

namespace MKS.PostcodeLookup.Core
{
    public class PostcodeValidator
    {
        public static bool IsValid(string postCode)
        {
            if (string.IsNullOrWhiteSpace(postCode) || postCode.Replace(" ", "").Length > 7 || postCode.Length < 5)
            {
                return false;
            }

            //if the postcode regex doesn't match error
            return Regex.IsMatch(postCode, @"[A-Z]{1,2}[0-9]{1,2}[A-Z]{0,1}\s{0,1}[0-9]{1,2}[A-Z]{1,2}",
                                 RegexOptions.IgnoreCase); 
        }
    }
}
