using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommonUtils
{
    public static class StringExtension
    {
        public static string RemoveSpace(this string text)
        {
            string result = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text.ToLower());

            result = Regex.Replace(result, @"[\t\f\n\r]", " ");

            var strs = result.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            result = string.Join("", strs);

            return result;
        }

        public static string RemoveTagHTML(this string text)
        {
            string result = text;

            result = Regex.Replace(result, @"(<[^<>]*>)|(</[^<>]*>)|(<[^<>]*/>)", "");


            return result;
        }

        public static string ConvertToUnsignVietnamese(this string text)
        {
            string result = text;
            if (!string.IsNullOrWhiteSpace(text))
            {
                // Tìm và thay thế chữ a
                result = Regex.Replace(result, @"[àáạảãâầấậẩẫăằắặẳẵ]", "a");
                result = Regex.Replace(result, @"[ÀÁẠẢÃÂẦẤẬẨẪĂẰẮẶẲẴ]", "A");

                // Tìm và thay thế chữ e
                result = Regex.Replace(result, @"[èéẹẻẽêềếệểễ]", "e");
                result = Regex.Replace(result, @"[ÈÉẸẺẼÊỀẾỆỂỄ]", "E");

                // Tìm và thay thế chữ i
                result = Regex.Replace(result, @"[ìíịỉĩ]", "i");
                result = Regex.Replace(result, @"[ÌÍỊỈĨ]", "I");

                // Tìm và thay thế chữ o
                result = Regex.Replace(result, @"[òóọỏõôồốộổỗơờớợởỡ]", "o");
                result = Regex.Replace(result, @"[ÒÓỌỎÕÔỒỐỘỔỖƠỜỚỢỞỠ]", "O");

                // Tìm và thay thế chữ u
                result = Regex.Replace(result, @"[ùúụủũưừứựửữ]", "u");
                result = Regex.Replace(result, @"[ÙÚỤỦŨƯỪỨỰỬỮ]", "U");

                // Tìm và thay thế chữ y
                result = Regex.Replace(result, @"[ỳýỵỷỹ]", "y");
                result = Regex.Replace(result, @"[ỲÝỴỶỸ]", "Y");

                // Tìm và thay thế chữ y
                result = Regex.Replace(result, @"[đ]", "d");
                result = Regex.Replace(result, @"[Đ]", "D");
            }

            return result;
        }

        public static string GenerateKeyName(this string text, string prefix)
        {
            string result = "";

            text = text.ConvertToUnsignVietnamese().RemoveSpace().RemoveTagHTML();
            text = text.First().ToString().ToLower() + text.Substring(1)?? "";

            result = prefix + "." + text;
            return result;
        }

        public static string GenerateKey(this string text, string prefix)
        {
            string result = "";
            string key = text;
            key = key.Trim(' ', '"').ConvertToUnsignVietnamese().RemoveSpace().RemoveTagHTML();
            key = key.First().ToString().ToLower() + key.Substring(1) ?? "";
            key = prefix + "." + key;

            result = key.Trim() + " = " + text.Trim(' ', '"');

            return result;
        }
    }
}
