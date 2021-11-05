using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

namespace Tolab.Common
{
    public class CommonUtilities
    {
        public static async Task<T> GetResponseModelFromJson<T>(HttpResponseMessage httResponseMessage)
        {
            var responseString = await httResponseMessage.Content.ReadAsStringAsync();

            var responseResult = JsonConvert.DeserializeObject<T>(responseString);

            return responseResult;
        }

        public static string DateFromEnglishToArabic(DateTime dateTime)
        {
            return dateTime.ToString("dd, MMMM, yyyy", new CultureInfo("ar-AE")).Replace(",", "");
        }
    }
}
