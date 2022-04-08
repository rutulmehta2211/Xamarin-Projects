using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERecall.ServiceLayer.ServiceModel
{
    public class RootObject
    {
        public bool status { get; set; }
        public int statusCode { get; set; }
        public string response { get; set; }
    }
}
