using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERecall.ServiceLayer.ServiceModel
{
    public class GetdeleteUserProducts
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public string SubscriptionCategory { get; set; }
        public bool Recalled { get; set; }
        public string CreatedDate { get; set; }
        public string CompanyName { get; set; }
        public int Price { get; set; }
        public string Vehicle { get; set; }
        public string VINNo { get; set; }
        public string Year { get; set; }
        public string Engine { get; set; }
        public string Type { get; set; }
        public int ManufacturerId { get; set; }
        public string Manufacturer { get; set; }
        public string UPCS { get; set; }
        public int UserId { get; set; }
        public string SearchCount { get; set; }
        public string ProductImg { get; set; }
        public string CompanyImg { get; set; }
        public string RecallDate { get; set; }
        public int recallId { get; set; }
        public string recallNo { get; set; }
    }
}
