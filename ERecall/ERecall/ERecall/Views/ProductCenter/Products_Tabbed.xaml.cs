using ERecall.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ERecall.Views.ProductCenter
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Products_Tabbed : ScrollableTabbed
    {
        public Products_Tabbed(int pageIndex)
        {
            InitializeComponent();
            this.CurrentPage = Children[pageIndex];
        }
    }
}