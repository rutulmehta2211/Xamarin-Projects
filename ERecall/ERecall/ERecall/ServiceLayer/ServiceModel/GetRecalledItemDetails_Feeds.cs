using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERecall.ServiceLayer.ServiceModel
{
    public class GetRecalledItemDetails_Feeds
    {
        public class RecalledItemImage
        {
            public string ImageNo { get; set; }
            public string ImageUrl { get; set; }
        }

        public class Response
        {
            public int RecallId { get; set; }
            public int CategoryId { get; set; }
            public string ReplaceRecallName { get; set; }
            public string Corportation { get; set; }
            public string ProductName { get; set; }
            public string Company { get; set; }
            public string CompanyImg { get; set; }
            public string ProductDesc { get; set; }
            public object ProductPrice { get; set; }
            public string ProductSource { get; set; }
            public string RemedyType { get; set; }
            public string Organization { get; set; }
            public string RecallDate { get; set; }
            public string RecallNo { get; set; }
            public string Category { get; set; }
            public string UPCScode { get; set; }
            public string Country { get; set; }
            public object AffectedUnits { get; set; }
            public object ContactForRecall { get; set; }
            public object ContactEmail { get; set; }
            public object ContactFromTime { get; set; }
            public object FromToDate { get; set; }
            public string ContactPhones { get; set; }
            public object LotFromToNo { get; set; }
            public string MadeIn { get; set; }
            public string RecallUrl { get; set; }
            public string Hazard { get; set; }
            public string Content { get; set; }
            public object Notes { get; set; }
            public string DefectSummary { get; set; }
            public object CorrectiveSummary { get; set; }
            public object ConsequenceSummary { get; set; }
            public object ComponentDescription { get; set; }
            public object Model { get; set; }
            public object Make { get; set; }
            public object Year { get; set; }
            public object RecallClass { get; set; }
            public object HealthRisk { get; set; }
            public string States { get; set; }
            public List<RecalledItemImage> RecalledItemImages { get; set; }
            public string ManufacturerName { get; set; }
        }

        public class RootObject
        {
            public bool status { get; set; }
            public int statusCode { get; set; }
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
