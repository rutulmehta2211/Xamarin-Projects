using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERecall.ServiceLayer.ServiceModel
{
    public class GetListOfImportedProductsByCategory
    {
        public class Response
        {
            public string ProductId { get; set; }
            public string ProductName { get; set; }
            public object ProductDesc { get; set; }
            public object Title { get; set; }
            public string CategoryId { get; set; }
            public object SubscriptionCategory { get; set; }
            public bool Recalled { get; set; }
            public string CreatedDate { get; set; }
            public string CompanyName { get; set; }
            public int Price { get; set; }
            public object Vehicle { get; set; }
            public object VINNo { get; set; }
            public object Year { get; set; }
            public object Engine { get; set; }
            public string Type { get; set; }
            public object ManufacturerId { get; set; }
            public object Manufacturer { get; set; }
            public object UPCS { get; set; }
            public object UserId { get; set; }
            public object SearchCount { get; set; }
            public string ProductImg { get; set; }
            public object CompanyImg { get; set; }
            public object RecallDate { get; set; }
            public int recallId { get; set; }
            public string recallNo { get; set; }

            public string Price_string { get; set; }
            public string PlaceholderImg { get; set; }
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
