using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CommunityWeb.Models;
using Newtonsoft.Json;

namespace CommunityWeb.Util
{
    public static class JsonParser<T>
    {
        public static async Task<IEnumerable<T>> RetrieveJsonDataFromUrlAsync(string url)
        {
            var jsonData = Enumerable.Empty<T>();

            using (var client = new HttpClient())
            {
                try
                {
                    var meetupResponse = await client.GetAsync(url);
                    meetupResponse.EnsureSuccessStatusCode();

                    string stringResponse = await meetupResponse.Content.ReadAsStringAsync();
                    jsonData = JsonConvert.DeserializeObject<IEnumerable<T>>(stringResponse);
                }
                catch (HttpRequestException ex)
                {

                }
            }

            return jsonData;
        }
    }
}