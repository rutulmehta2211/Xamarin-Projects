using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERecall.ServiceLayer.ServiceModel
{
    public class GetProductClaimList
    {
        public class Response
        {
            public int UserId { get; set; }
            public int recallId { get; set; }
            public object ManufacturerId { get; set; }
            public object ManufacturerName { get; set; }
            public object SupplierAccountId { get; set; }
            public object ClaimTitle { get; set; }
            public object ClaimDetail { get; set; }
            public int ClaimStatus { get; set; }
            public string recallNo { get; set; }
            public DateTime LatestUpdate { get; set; }
            public DateTime RecallDate { get; set; }
            public object CategoryId { get; set; }
            public int ClaimCount { get; set; }
            public string ProductName { get; set; }
            public DateTime CreateDate { get; set; }

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
