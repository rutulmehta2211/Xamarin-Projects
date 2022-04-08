using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using ERecall.Controls;
using ERecall.ServiceLayer;
using Firebase.Iid;
using Newtonsoft.Json;

namespace ERecall.Droid.Services
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class MyFirebaseIIDService : FirebaseInstanceIdService
    {
        const string TAG = "MyFirebaseIIDService";
        public async override void OnTokenRefresh()
        {
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            Log.Debug(TAG, "Refreshed token: " + refreshedToken);
            await SendRegistrationToServer(refreshedToken);
        }
        private async Task SendRegistrationToServer(string refreshedToken)
        {
            try
            {
                var Service_response = await GetResponseFromWebService.GetResponsePostData<PostServiceToken.RootObject>(ServiceURLs.RegisterDeviceGCM + "&deviceToken="+ refreshedToken, null);
                if (Service_response != null)
                {
                    if (Service_response.status && Service_response.statusCode == 200)
                    {
                        Log.Debug(TAG, "Token stored Successfully");
                    }
                    else
                    {
                        Log.Debug(TAG, "Token not stored with status code :" + Service_response.statusCode);
                    }
                }
                else
                {
                    Log.Debug(TAG, "Webserver not responding");
                }
            }
            catch (Exception ex)
            {
                Log.Debug(TAG, ex.Message);
            }
        }
    }

    public class PostServiceToken
    {
        public class Response
        {
            public string ETag { get; set; }
            public DateTime ExpirationTime { get; set; }
            public string RegistrationId { get; set; }
            public string Tags { get; set; }
            public string GcmRegistrationId { get; set; }
        }

        public class RootObject
        {
            public bool status { get; set; }
            public int statusCode { get; set; }
            public Response response { get; set; }
        }
    }

    //public class StringToResponseConverter : JsonConverter
    //{
    //    public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, JsonSerializer serializer)
    //    {
    //        throw new NotImplementedException();
    //    }
    //    public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    //    {
    //        object retVal = new Object();
    //        if (reader.TokenType == Newtonsoft.Json.JsonToken.StartArray)
    //        {
    //            var instance = (PostServiceToken.Response)serializer.Deserialize(reader, typeof(PostServiceToken.Response));
    //            retVal = instance;
    //        }
    //        else if (reader.TokenType == Newtonsoft.Json.JsonToken.String)
    //        {
    //            string message = (string)serializer.Deserialize(reader);
    //            retVal = new PostServiceToken.Response();
    //        }

    //        return retVal;
    //    }
    //    public override bool CanConvert(Type objectType)
    //    {
    //        return true;
    //    }  
    //}
}