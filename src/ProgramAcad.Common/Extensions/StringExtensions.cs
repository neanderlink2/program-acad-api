using System;
using System.Text.RegularExpressions;

namespace ProgramAcad.Common.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveHtmlTags(this string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }

        public static bool SearchIsInThisText(this string text, string searchText)
        {
            return text.Contains(searchText);
        }

        public static bool HasValue(this string str)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str) || str.Trim().Length <= 0)
            {
                return false;
            }

            return true;
        }

        public static string HideCharacters(this string str, int visibleLength = 3)
        {
            string firstPart = str.Substring(0, visibleLength);
            int len = str.Length;
            string lastPart = str.Substring(len - 4, 4);
            int middlePartLenght = len - (firstPart.Length + lastPart.Length);
            string middlePart = new string('*', middlePartLenght);
            return firstPart + middlePart + lastPart;
        }
    }
}
