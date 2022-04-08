using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERecall.ServiceLayer.ServiceModel
{
    public class GetAccountProfileByUserId
    {
        public class Response
        {
            public object Id { get; set; }
            public string Email { get; set; }
            public bool EmailConfirmed { get; set; }
            public object PasswordHash { get; set; }
            public object SecurityStamp { get; set; }
            public object PhoneNumber { get; set; }
            public object PhoneNumberConfirmed { get; set; }
            public object TwoFactorEnabled { get; set; }
            public object LockoutEndDateUtc { get; set; }
            public object LockoutEnabled { get; set; }
            public object AccessFailedCount { get; set; }
            public object UserName { get; set; }
            public int UserId { get; set; }
            public string Name { get; set; }
            public object ZipCode { get; set; }
            public int CountryId { get; set; }
            public string Country { get; set; }
            public object IsDeleted { get; set; }
            public object CreatedDate { get; set; }
            public int AccountId { get; set; }
            public object AccountNo { get; set; }
            public object AccountName { get; set; }
            public int AccountTypeId { get; set; }
            public object ParentAccountId { get; set; }
            public object ValidFrom { get; set; }
            public object ValidTo { get; set; }
            public object CompanyName { get; set; }
            public object CompanyAddress { get; set; }
            public object CompanyWebsite { get; set; }
            public object PrimaryContactName { get; set; }
            public object PrimaryContactEmail { get; set; }
            public object SecondaryContactName { get; set; }
            public object SecondaryContactEmail { get; set; }
            public object Notes { get; set; }
            public object ProfileImage { get; set; }
            public object AccountUserId { get; set; }
            public string AccountTypeName { get; set; }
            public object UserSubscriptionId { get; set; }
            public object UserSubscription { get; set; }
            public object SubscriptionFrequencyId { get; set; }
            public object SubscriptionFrequency { get; set; }
            public object SubscriptionCategoryId { get; set; }
            public object SubscriptionCategory { get; set; }
            public object UserImportedProductId { get; set; }
            public int ImporedProductsCount { get; set; }
            public int OpenTickets { get; set; }
            public int AvailableReports { get; set; }
            public int Credits { get; set; }
            public int SavedAmount { get; set; }
            public string AccountNumber { get; set; }
            public object MWS_SellerId { get; set; }
            public object MWS_MarketPlaceId { get; set; }
            public string ManufacturerName { get; set; }
            public string ManufacturerCode { get; set; }
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
