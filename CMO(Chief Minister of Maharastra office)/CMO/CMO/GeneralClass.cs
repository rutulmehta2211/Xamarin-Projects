using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.NetworkInformation;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Net;

namespace CMO
{
    public class GeneralClass
    {
        public static async Task<T> GetResponse<T>(string URI, List<KeyValuePair<string, string>> values) where T : class
        {
            if (NetworkInterface.GetIsNetworkAvailable() == true)
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.IfModifiedSince = DateTimeOffset.Now;
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("token", "rh5p5jdrqklpo8b1igl3zsu39gvoq3f7");

                var request = new HttpRequestMessage(HttpMethod.Post, URI);

                if (request.Headers.CacheControl == null)
                {
                    request.Headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue();
                }

                request.Headers.CacheControl.NoCache = true;

                request.Content = new FormUrlEncodedContent(values);

                try
                {
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
                catch (Exception ex)
                {

                }
            }
            else
            {

            }

            return null;
        }
        public static async Task<T> GetPlacesAsync<T>(string URI, List<KeyValuePair<string, string>> values)
        {

            var client = new System.Net.Http.HttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("token", "rh5p5jdrqklpo8b1igl3zsu39gvoq3f7");
            client.BaseAddress = new Uri("http://14.141.36.212/");
            StringContent str = new StringContent(values.ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await client.PostAsync(new Uri(URI), str);
            var placesJson = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(placesJson);
        }
        public static async Task<T> FillCalendarData<T>(string URI, List<KeyValuePair<string, string>> values)
        {
            try
            {
                if (NetworkInterface.GetIsNetworkAvailable() == true)
                {
                    HttpClient httpClient = new HttpClient();
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("token", "rh5p5jdrqklpo8b1igl3zsu39gvoq3f7");

                    string url1 = string.Format(URI);
                    HttpRequestMessage requestLoc = new HttpRequestMessage(HttpMethod.Post, new Uri(url1, UriKind.Absolute));
                    string jsonContents = null;
                    //var values = new List<KeyValuePair<string, string>>
                    //{
                    //   new KeyValuePair<string, string>("params",jsonContents),
                    //};

                    requestLoc.Content = new FormUrlEncodedContent(values);

                    if (httpClient.DefaultRequestHeaders != null)
                    {
                        httpClient.DefaultRequestHeaders.IfModifiedSince = DateTimeOffset.Now;
                    }

                    try
                    {
                        var response11 = await httpClient.SendAsync(requestLoc);
                        if (response11 != null && response11.IsSuccessStatusCode)
                        {
                            var responseString = await response11.Content.ReadAsStringAsync();
                            if (!string.IsNullOrWhiteSpace(responseString))
                            {
                                return JsonConvert.DeserializeObject<T>(responseString);

                            }
                        }
                    }
                    catch (Exception e)
                    { return JsonConvert.DeserializeObject<T>(""); }
                }
                return JsonConvert.DeserializeObject<T>(""); 
            }
            catch (Exception e)
            { return JsonConvert.DeserializeObject<T>(""); }
        }
    }
}



   
