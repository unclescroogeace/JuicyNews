using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JuicyNews.Utility
{
    public class Helper
    {
        public static string stringShorter(string str, int maxLength)
        {
            return str.Substring(0, Math.Min(str.Length, maxLength));
        }
    }
}