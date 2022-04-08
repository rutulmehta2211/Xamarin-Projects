using ERecall.Controls;
using ERecall.Views.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ERecall.Views.Notifications
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Notification_Tabbed : ScrollableTabbed
    {
        public Notification_Tabbed()
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