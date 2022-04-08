using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERecall.ServiceLayer.ServiceModel
{
    public class PostUserVehicle
    {
        public class AddUserVehicle
        {
            public int VehicleMakeId { get; set; }
            public string MakeName { get; set; }
            public int VehicleModelId { get; set; }
            public string ModelName { get; set; }
            public int YearId { get; set; }
            public int Year { get; set; }
            public int UserId { get; set; }
        }

        public class RootObject
        {
            public bool status { get; set; }
            public int statusCode { get; set; }
            public string response { get; set; }
        }
    }
}
