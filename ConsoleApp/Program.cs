using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommonUtils;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = @"""<b>Mã nhân viên</b> không tồn tại""";

            string s1 = @"<div class=""panel - heading clearfix vt-like - tab vt - relative"">
                      < strong >
      
                      < span class=""glyphicon glyphicon-th""></span>
                        <b> Hoang tri                   dung </b>
                	<bean:message key = ""robrIndex.reportUnit "" />
                </ strong >
            </ div >                       ";

            Regex regex = new Regex(@"[.,?\/~!@#$%^&*()\[\]{}:;""']");

            var x1 = "\" phút\"";
            var x2 = "\" ngày\"";
            var x3 = "\" giờ, \"";

            var x4 = "Giá trị cột \"Cấp ủy viên phụ trách BVAN\" chỉ có thể là X";

            var z = x4.Replace(@"\""", "\"");

            var y = x3.RemoveSpecialCharacter();

            Console.WriteLine("'{0}'", s.GenerateKey("key"));
            Console.WriteLine("'{0}'", s.ConvertToUnsignVietnamese().RemoveSpace());
            Console.WriteLine("'{0}'", s.RemoveTagHTML().RemoveSpace().ConvertToUnsignVietnamese());
            Console.WriteLine("'{0}'", s1.RemoveSpace().RemoveTagHTML());
            Console.WriteLine("'{0}'", y);
            Console.WriteLine("'{0}'", z);

            Console.ReadKey();
        }
    }
}
