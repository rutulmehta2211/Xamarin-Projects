using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERecall.ServiceLayer.ServiceModel
{
    public class SubmitClaimMessages
    {
        public class ReplyMessages
        {
            public int UserId { get; set; }
            public int ClaimId { get; set; }
            public string MessagesDescription { get; set; }
            public int StatusId { get; set; }
        }

        public class Response
        {
            public int ClaimMessageId { get; set; }
            public int ClaimId { get; set; }
            public string MessageTitle { get; set; }
            public string MessageDescription { get; set; }
            public int ReplyToMessageId { get; set; }
            public bool IsDeleted { get; set; }
            public int CreatedBy { get; set; }
            public DateTime CreatedDate { get; set; }
            public object ModifiedBy { get; set; }
            public object ModifiedDate { get; set; }
        }

        public class RootObject
        {
            public bool status { get; set; }
            public int statusCode { get; set; }
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
