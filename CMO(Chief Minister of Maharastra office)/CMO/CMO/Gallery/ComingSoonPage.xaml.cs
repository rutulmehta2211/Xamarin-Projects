using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CMO
{
    public partial class ComingSoonPage : ContentPage
    {
        public ComingSoonPage()
        {
            InitializeComponent();
            this.Title = AppResources.LPressRelease.ToUpper();
            lbltext.Text = AppResources.LComingSoon;
			lbltext.FontSize = App.GetFontSizeMedium();
            NavigationPage.SetBackButtonTitle(this, "");
            if (CMO.MenuController.SideMenu.PageTitleForComingSoonPage != null)
            {
                this.Title = CMO.MenuController.SideMenu.PageTitleForComingSoonPage.ToUpper();
            }
            if(CMO.MenuController.HomePage.PageTitleForComingSoonPage!=null)
            {
                this.Title=CMO.MenuController.HomePage.PageTitleForComingSoonPage.ToUpper();
            }
        }
        protected override void OnAppearing()
        {
            Application.Current.Properties["CurrentPage"] = "pressrelease";
        }
    }
}
