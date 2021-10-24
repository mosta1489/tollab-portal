using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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
    }
}
