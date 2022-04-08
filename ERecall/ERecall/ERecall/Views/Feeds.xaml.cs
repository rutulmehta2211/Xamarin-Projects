using ERecall.Controls;
using ERecall.ServiceLayer;
using ERecall.Views.FeedsModule;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ERecall.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Feeds : TabbedPage
    {
        public Feeds()
        {
            InitializeComponent();
            LatestProducts.IsLoadBefore = false;
        }
    }
}