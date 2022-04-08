using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERecall.ServiceLayer.ServiceModel
{
    public class PostAddUsermobileSetting
    {
        public class PostUserSettings
        {
            public int UserId { get; set; }
            public bool NotifyNewProductInProductCenter { get; set; }
            public bool NotifyProductRecalls { get; set; }
            public bool NotifyResolutionCenter { get; set; }
            public string NotifyAppUse { get; set; }
        }
        public class RootObject
        {
            public bool status { get; set; }
            public int statusCode { get; set; }
            public string response { get; set; }
        }
    }
}
