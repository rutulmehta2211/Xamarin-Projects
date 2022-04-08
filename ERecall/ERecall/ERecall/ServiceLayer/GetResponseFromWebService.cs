using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ERecall.ServiceLayer
{
    public class GetResponseFromWebService
    {
        public static async Task<T> GetResponse<T>(string URI) where T : class
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                HttpClient httpClient = new HttpClient();

                var request = new HttpRequestMessage(HttpMethod.Get, URI);

                if (request.Headers.CacheControl == null)
                {
                    request.Headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue();
                }

                request.Headers.CacheControl.NoCache = true;

                var response1 = await httpClient.SendAsync(request);
                if (response1 != null)//&& response1.IsSuccessStatusCode
                {
                    var responseString = await response1.Content.ReadAsStringAsync();

                    if (!string.IsNullOrWhiteSpace(responseString))
                    {
                        return JsonConvert.DeserializeObject<T>(responseString);
                    }
                }
            }
            return null;
        }

        public static async Task<T> GetResponsePostDataUsingKeyValue<T>(string URI, List<KeyValuePair<string, string>> values) where T : class
        {
            if (NetworkInterface.GetIsNetworkAvailable() == true)
            {
                HttpClient httpClient = new HttpClient();

                var request = new HttpRequestMessage(HttpMethod.Post, URI);

                if (request.Headers.CacheControl == null)
                {
                    request.Headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue();
                }

                request.Headers.CacheControl.NoCache = true;

                request.Content = new FormUrlEncodedContent(values);

                var response1 = await httpClient.SendAsync(request);
                if (response1 != null && response1.IsSuccessStatusCode)
                {
                    var responseString = await response1.Content.ReadAsStringAsync();

                    if (!string.IsNullOrWhiteSpace(responseString))
                    {
                        return JsonConvert.DeserializeObject<T>(responseString);
                    }
                }
            }
            return null;
        }

        public static async Task<T> GetResponsePostData<T>(string URI, string values) where T : class
        {
            if (NetworkInterface.GetIsNetworkAvailable() == true)
            {
                HttpClient httpClient = new HttpClient();

                var request = new HttpRequestMessage(HttpMethod.Post, URI);
                if (request.Headers.CacheControl == null)
                {
                    request.Headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue();
                }

                request.Headers.CacheControl.NoCache = true;
                if (values != null)
                {
                    request.Content = new StringContent(values, UTF8Encoding.UTF8, "application/json");
                }
                var response1 = await httpClient.SendAsync(request);
                if (response1 != null)//response1 != null && response1.IsSuccessStatusCode
                {
                    var responseString = await response1.Content.ReadAsStringAsync();

                    if (!string.IsNullOrWhiteSpace(responseString))
                    {
                        return JsonConvert.DeserializeObject<T>(responseString);
                    }
                }
                #region Comment Code Option-2
                //var content = new StringContent(values, UTF8Encoding.UTF8, "application/json");
                //var response = await httpClient.PostAsync(URI, content);
                //var result = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                //return result; 
                #endregion
            }
            return null;
        }
    }
}
