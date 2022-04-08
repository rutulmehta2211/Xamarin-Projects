using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ERecall.ServiceLayer.ServiceModel
{
    public class Notifications
    {
        public class Response
        {
            public int NotificationId { get; set; }
            public string NotificationName { get; set; }
            public object ProductCount { get; set; }
            public string ProductName { get; set; }
            public string ProductDesc { get; set; }
            public string ProductImg { get; set; }
            public object NotificationDate { get; set; }
            public int? CategoryId { get; set; }
            public int ProductRecallId { get; set; }
            public object RecallNumber { get; set; }
            public object UserId { get; set; }
            public object CreatedBy { get; set; }
            public bool IsRead { get; set; }

            public Color bgcolor { get; set; }
        }

        public class RootObject
        {
            public bool status { get; set; }
            public int statusCode { get; set; }
            [JsonConverter(typeof(StringToResponseConverter))]
            public List<Response> response { get; set; }
        }

        public class StringToResponseConverter : JsonConverter
        {
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                object retVal = new Object();
                if (reader.TokenType == JsonToken.StartArray)
                {
                    var instance = (List<Response>)serializer.Deserialize(reader, typeof(List<Response>));
                    retVal = instance;
                }
                else if (reader.TokenType == JsonToken.String)
                {
                    string message = (string)serializer.Deserialize(reader);
                    retVal = new List<Response>();
                }

                return retVal;
            }

            public override bool CanConvert(Type objectType)
            {
                return true;
            }
        }
    }
}
