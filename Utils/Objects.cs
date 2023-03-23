using System;

namespace DatabaseUtils.Utils
{
    public static class Objects
    {
        public static bool IsEmpty(this string target)
        {
            return string.IsNullOrEmpty(target) || string.IsNullOrEmpty(target.Trim());
        }

        public static bool IsNotEmpty(this string target)
        {
            return !string.IsNullOrEmpty(target) && !string.IsNullOrEmpty(target.Trim());
        }

        public static bool InsensitiveEqual(this string source, string target)
        {
            return target.Equals(source, System.StringComparison.OrdinalIgnoreCase);
        }


        public static string ReplaceBetweenTwoStrings(this string originalString, string startString, string endString, string replacementString)
        { 

            int startIndex = originalString.IndexOf(startString, StringComparison.OrdinalIgnoreCase) + startString.Length;
            int endIndex = originalString.IndexOf(endString, startIndex);

            if (startIndex >= 0 && endIndex >= 0)
            {
                string beforeTarget = originalString.Substring(0, startIndex);
                string afterTarget = originalString.Substring(endIndex);
                string newString = beforeTarget + replacementString + afterTarget;
                return newString;
            }

            return originalString;
        }
    }
}
