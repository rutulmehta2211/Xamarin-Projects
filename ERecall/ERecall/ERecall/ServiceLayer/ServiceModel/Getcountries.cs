using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERecall.ServiceLayer.ServiceModel
{
    public class Getcountries
    {
        public class Response
        {
            public int CountryId { get; set; }
            public string CountryName { get; set; }
            public string CountryCode { get; set; }
            public object ImageUrl { get; set; }
        }

        public class RootObject
        {
            public bool status { get; set; }
            public int statusCode { get; set; }
            public List<Response> response { get; set; }
        }
    }
}
