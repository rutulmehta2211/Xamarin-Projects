using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERecall.ServiceLayer.ServiceModel
{
    public class PostMyRecall
    {
        public class RootObject
        {
            public bool status { get; set; }
            public int statusCode { get; set; }
            public string response { get; set; }
        }
        public class PostMyFavRecall
        {
            public string UserId { get; set; }
            public int RecallId { get; set; }
            public object Price { get; set; }
        }
    }
}
