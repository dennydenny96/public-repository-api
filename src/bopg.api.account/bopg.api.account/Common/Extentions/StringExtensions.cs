using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bopg.api.account.Common
{
    public static class StringExtensions
    {
        public static string ToDecodeBase64(this string text)
        {
            string Str = text.Trim().Replace(" ", "+");
            if (Str.Length % 4 > 0)
            {
                Str = Str.PadRight(Str.Length + 4 - Str.Length % 4, '=');
            }
            return Encoding.UTF8.GetString(Convert.FromBase64String(Str));
        }

        public static string ToDateString(this object date)
        {
            return String.Format("{0:dd MMM yyyy}", date);
        }

        public static string ToDateTimeString(this object date)
        {
            return String.Format("{0:dd MMM yyyy HH.mm}", date);
        }

        public static string ToDateRawString(this object date)
        {
            return String.Format("{0:yyyy-MM-dd}", date);
        }

        public static int ToInt(this object o)
        {
            return Convert.ToInt32(o);
        }

        public static string ToString(this object o)
        {
            return Convert.ToString(o);
        }

        public static string ToTimeString(this object time)
        {
            return String.Format(@"{0:hh\:mm}", time);
        }

        public static int ToTimeInt(this object time)
        {
            return Convert.ToInt32(String.Format(@"{0:hhmm}", time));
        }
    }
}
