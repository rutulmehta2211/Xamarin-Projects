using ERecall.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERecall.ServiceLayer.ServiceModel
{
    public class GetLatestProducts_Feeds
    {
        public class Response
        {
            public string ProductId { get; set; }
            public string ProductName { get; set; }
            public string ProductDesc { get; set; }
            public string ProductImg { get; set; }
            public object ProductTotal { get; set; }
            public string CategoryId { get; set; }
            public int SubscriptionCategoryId { get; set; }
            public string SubscriptionCategory { get; set; }
            public object CategoryImg { get; set; }
            public string CompanyImg { get; set; }
            public bool Recalled { get; set; }
            public string CreatedDate { get; set; }
            public string SearchCount { get; set; }
            public object ProductBrand { get; set; }
            public string RecallDate { get; set; }
            public object MessageCount { get; set; }
            public object ProductStatus { get; set; }
            public string CompanyName { get; set; }
            public object ModelNo { get; set; }
            public object Price { get; set; }
            public object PriceTo { get; set; }
            public object IsAdd { get; set; }
            public object Vehicle { get; set; }
            public object VINNo { get; set; }
            public object Year { get; set; }
            public object Engine { get; set; }
            public string CategoryIconImg { get; set; }
            public string SampIconImg { get; set; }
            public string RemedyId { get; set; }
            public object Source { get; set; }
            public int OrganizationId { get; set; }
            public object AffectedUnits { get; set; }
            public object ModelId { get; set; }
            public object YearId { get; set; }
            public object MakeId { get; set; }
            public object IsFavorite { get; set; }
            public object UPCS { get; set; }

            public string PlaceholderImg { get; set; }
            public string eRecallIconImage { get; set; }
            public string imgCategory { get; set; }
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
