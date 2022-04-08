using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERecall.ServiceLayer.ServiceModel
{
    public class PostUserClaim
    {
        public class UserClaim
        {
            public int UserId { get; set; }
            public int recallId { get; set; }
            public int ManufacturerId { get; set; }
            public string ManufacturerName { get; set; }
            public int SupplierAccountId { get; set; }
            public string ClaimTitle { get; set; }
            public string ClaimDetail { get; set; }
            public int ClaimStatus { get; set; }
            public string recallNo { get; set; }
            public int CategoryId { get; set; }
            public int ClaimCount { get; set; }
            public string ProductName { get; set; }
        }
        public class Response
        {
            public int UserClaimId { get; set; }
            public int ProductRecallId { get; set; }
            public int UserId { get; set; }
            public int ManufacturerId { get; set; }
            public int AccountId { get; set; }
            public int ClaimStatusId { get; set; }
            public int MessagesCount { get; set; }
            public bool IsDeleted { get; set; }
            public int CreatedBy { get; set; }
            public DateTime CreatedDate { get; set; }
            public int ModifedBy { get; set; }
            public DateTime ModifiedDate { get; set; }
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
