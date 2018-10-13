using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App2;


namespace App2
{
    public class Common
    {
        public static string API_LINK = "http://api.openweathermap.org/data/2.5/weather";
        public static string API_KEY = "aaced87160b57faab29eef49bed9f4e1";

        public static string APIRequest(string lat, string lon)
        {
            StringBuilder strBuilder = new StringBuilder(API_LINK);
            //units= metric to convert temp to Celsius
            strBuilder.AppendFormat("?lat={0}&lon={1}&APPID={2}&units=metric", lat, lon, API_KEY);
            return strBuilder.ToString();
        }

        public static DateTime ConvertUnixTimeToDateTime(double unix)
        {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dt = dt.AddSeconds(unix).ToLocalTime();
            return dt;
        }

    }
}
