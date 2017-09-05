using System;
using System.Text.RegularExpressions;

namespace BDSA2017.Lecture02
{
    public static class PostalCodeValidator
    {
        public static bool IsValid(string postalCode)
        {
            var pattern = @"^\d{3,4}$";

            return Regex.IsMatch(postalCode, pattern);
        }

        public static bool TryParse(string postalCodeAndLocality, 
            out string postalCode, 
            out string locality)
        {
            var pattern = @"(?<postalCode>\d{4}) (?<locality>\w+)";

            postalCode = default(string);
            locality = default(string);

            var match = Regex.Match(postalCodeAndLocality, pattern);

            if (match.Success)
            {
                postalCode = match.Groups["postalCode"].Value;
                locality = match.Groups["locality"].Value;
            }

            return match.Success;
        }
    }
}
