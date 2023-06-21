using System;
using System.Globalization;
using System.Text;

/**
 * Author: ICSC余士鵬
 * Date: 2019/10/09
 * Description: Calandar Help 
 * Reference: 
 * Modified: 
 */
namespace Core.Util
{
    public static class CalanderUtil
    {
       
        public static string ConvertDateFormatStr(this string dateStr, string inFormat, string outFormat)
        {
            return DateTime.ParseExact(dateStr, inFormat, null, DateTimeStyles.AllowWhiteSpaces).ToString(outFormat);
        }
        public static string ConvertTimeFormatStr(this string timeStr, string inFormat, string outFormat)
        {
            return DateTime.ParseExact(timeStr, inFormat, CultureInfo.InvariantCulture).ToString(outFormat);
        }
        public static DateTime ConverDateTime(this byte[] datebyte)
        {
            var dateStr = Encoding.UTF8.GetString(datebyte).Trim();
            return DateTime.Parse(dateStr);
        }

    }
}
