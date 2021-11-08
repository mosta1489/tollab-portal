using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace TolabPortal.Controllers.Utils
{
    public static class VimeoConnector
    {
        private const string VimeoToken = "3251e9876001f9b94a96b446bacdb6c2";
        public static async Task<string> GenerateEmbed(string videoUrl, string height, string width)
        {

            try
            {
                var client = new HttpClient();
                string uri = $"https://vimeo.com/api/oembed.json?url={videoUrl}&width={width}&height={height}&byline=0&background=0&portrait=0";
                client.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", VimeoToken);
                client.DefaultRequestHeaders.Add("Accept", "application/vnd.vimeo.*+json;version=3.4");
                var response = await client.GetAsync(uri).ConfigureAwait(false);
                var responseString = await response.Content.ReadAsStringAsync();
                var serialized = JsonConvert.DeserializeObject<VimeoResponse>(responseString);
                return serialized.html ?? "";

            }
            catch (Exception ex)
            {
                return "";
            }
        }

     
    }
}
