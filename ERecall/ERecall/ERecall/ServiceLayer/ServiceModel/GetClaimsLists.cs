using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERecall.ServiceLayer.ServiceModel
{
    public class GetClaimsLists
    {
        public class Response
        {
            public int ClaimId { get; set; }
            public int StatusId { get; set; }
            public int UserId { get; set; }
            public int CategoryId { get; set; }
            public string ProductName { get; set; }
            public object UserName { get; set; }
            public string Manufacturer { get; set; }
            public string RecallNumber { get; set; }
            public int recallId { get; set; }
            public int TotalCount { get; set; }
            public int MessageCount { get; set; }
            public string Status { get; set; }
            public DateTime ClaimDate { get; set; }
            public DateTime RecallDate { get; set; }
            public object MessageDate { get; set; }
            public object ClaimTitle { get; set; }
            public object Description { get; set; }
            public DateTime CreateDate { get; set; }
            public object ClosedDate { get; set; }
            public object ClosedBy { get; set; }
            public object TotalTime { get; set; }

            public string ClaimStatusName { get; set; }
            public string FromLastUpdateInterval { get; set; }
            public string RecallDatestring { get; set; }
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
