using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERecall.ServiceLayer.ServiceModel
{
    public class GetRecallStatusByUPCForMobile
    {
        public class Response
        {
            public string upcCode { get; set; }
            public string productName { get; set; }
            public bool isRecall { get; set; }
            public int productRecallId { get; set; }
            public int manufacturerId { get; set; }
        }

        public class RootObject
        {
            public bool status { get; set; }
            public int statusCode { get; set; }
            
            public bool lookUp { get; set; }
            [JsonConverter(typeof(StringToResponseConverter))]
            public Response response { get; set; }
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
                if (reader.TokenType == JsonToken.StartObject)
                {
                    var instance = (Response)serializer.Deserialize(reader, typeof(Response));
                    retVal = instance;
                }
                else if (reader.TokenType == JsonToken.String)
                {
                    string message = (string)serializer.Deserialize(reader);
                    retVal = new Response();
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
