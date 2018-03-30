using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class CommonUtils
    {
        public static string ConvertToVietnamese(string text)
        {
            string result = text;
            string s = "[àáạảãâầấậẩẫăằắặẳẵèéẹẻẽêềếệểễìíịỉĩòóọỏõôồốộổỗơờớợởỡùúụủũưừứựửữỳýỵỷỹđ]";
            if (!string.IsNullOrWhiteSpace(text))
            {
                // Tìm và thay thế chữ a
                result = new Regex("[àáạảãâầấậẩẫăằắặẳẵ]").Replace(result, "a");
                result = new Regex("[ÀÁẠẢÃÂẦẤẬẨẪĂẰẮẶẲẴ]").Replace(result, "A");

                // Tìm và thay thế chữ e
                result = new Regex("[èéẹẻẽêềếệểễ]").Replace(result, "e");
                result = new Regex("[ÈÉẸẺẼÊỀẾỆỂỄ]").Replace(result, "E");

                // Tìm và thay thế chữ i
                result = new Regex("[ìíịỉĩ]").Replace(result, "i");
                result = new Regex("[ÌÍỊỈĨ]").Replace(result, "I");

                // Tìm và thay thế chữ o
                result = new Regex("[òóọỏõôồốộổỗơờớợởỡ]").Replace(result, "o");
                result = new Regex("[ÒÓỌỎÕÔỒỐỘỔỖƠỜỚỢỞỠ]").Replace(result, "O");

                // Tìm và thay thế chữ u
                result = new Regex("[ùúụủũưừứựửữ]").Replace(result, "u");
                result = new Regex("[ÙÚỤỦŨƯỪỨỰỬỮ]").Replace(result, "U");

                // Tìm và thay thế chữ y
                result = new Regex("[ỳýỵỷỹ]").Replace(result, "y");
                result = new Regex("[ỲÝỴỶỸ]").Replace(result, "Y");

                // Tìm và thay thế chữ y
                result = new Regex("[đ]").Replace(result, "d");
                result = new Regex("[Đ]").Replace(result, "D");
            }

            return result;
        }

        public static string RemoveSpace(string text)
        {
            string result = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text.ToLower());

            var strs = result.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries );

            result = string.Join("", strs);

            return result;
        }
    }
}
