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
            Console.WriteLine("'{0}'", s.GenerateKey("key"));
            Console.WriteLine("'{0}'", s.ConvertToUnsignVietnamese().RemoveSpace());
            Console.WriteLine("'{0}'", s.RemoveTagHTML().RemoveSpace().ConvertToUnsignVietnamese());
            Console.WriteLine("'{0}'", s1.RemoveSpace().RemoveTagHTML());


            Console.ReadKey();
        }
    }
}
