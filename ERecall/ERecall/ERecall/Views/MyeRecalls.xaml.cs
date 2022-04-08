using ERecall.Controls;
using ERecall.Views.Menu;
using ERecall.Views.MyERecallsModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ERecall.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyeRecalls : ScrollableTabbed
    {
        public MyeRecalls()
        {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {
            Application.Current.MainPage = new SideMenu();
            return true;
        }
    }
}