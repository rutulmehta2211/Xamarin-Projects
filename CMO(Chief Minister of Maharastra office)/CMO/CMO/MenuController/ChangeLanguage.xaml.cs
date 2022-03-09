using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CMO.MenuController
{
    public partial class ChangeLanguage : ContentPage
    {
        public ChangeLanguage()
        {
            InitializeComponent();
			setfontsize();
            //EnglishChangeLanguageSwitch.BackgroundColor = Color.FromHex("#f47421");
            //MarathiChangeLanguageSwitch.BackgroundColor = Color.FromHex("#f47421");
            
            string lang = "";
            if (Application.Current.Properties.ContainsKey("Language"))
            { lang = Application.Current.Properties["Language"] as string; }
            if (lang == "en")
            {
                EnglishChangeLanguageSwitch.IsToggled = true;
                MarathiChangeLanguageSwitch.IsToggled = false;
            }
            else
            {
                EnglishChangeLanguageSwitch.IsToggled = false;
                MarathiChangeLanguageSwitch.IsToggled = true;
            }
            ChangeLanguageTapEvents();
            this.Title = AppResources.LChangeLanguage.ToUpper();
        }

		public void setfontsize()
		{
			English.FontSize = App.GetFontSizeTitle();
			Marathi.FontSize = App.GetFontSizeTitle();
			if (Device.OS == TargetPlatform.iOS)
			{
				if (Device.Idiom == TargetIdiom.Phone)
				{
				}
			}
		}

		private void ChangeLanguageTapEvents()
        {
            EnglishChangeLanguageSwitch.Toggled += EnglishChangeLanguageSWITCH_Toggled;
            MarathiChangeLanguageSwitch.Toggled += MarathiChangeLanguageSWITCH_Toggled;
        }
        protected override void OnAppearing()
        {
            Application.Current.Properties["CurrentPage"] = "changelang";
        }
        private void MarathiChangeLanguageSWITCH_Toggled(object sender, ToggledEventArgs e)
        {
            #region change logic toggle logic
            var obj = sender as Switch;
            if (obj.IsToggled)
            {
                Application.Current.Properties["Language"] = "mr";
                AppResources.Culture = new System.Globalization.CultureInfo("mr");
                EnglishChangeLanguageSwitch.IsToggled = false;
             //   OnBackButtonPressed();
            }
            else
            {
                Application.Current.Properties["Language"] = "en";
                AppResources.Culture = new System.Globalization.CultureInfo("en");
                EnglishChangeLanguageSwitch.IsToggled = true;
              //  OnBackButtonPressed();
            }
            #endregion
            this.Title = AppResources.LChangeLanguage.ToUpper();
        }
        private void EnglishChangeLanguageSWITCH_Toggled(object sender, ToggledEventArgs e)
        {
            #region change logic toggle logic
            var obj = sender as Switch;
            if (obj.IsToggled)
            {
                Application.Current.Properties["Language"] = "en";
                AppResources.Culture = new System.Globalization.CultureInfo("en");
                MarathiChangeLanguageSwitch.IsToggled = false;
            //    OnBackButtonPressed();
            }
            else
            {
                Application.Current.Properties["Language"] = "mr";
                AppResources.Culture = new System.Globalization.CultureInfo("mr");
                MarathiChangeLanguageSwitch.IsToggled = true;
             //   OnBackButtonPressed();
            }
            #endregion
            this.Title = AppResources.LChangeLanguage.ToUpper();
        }
        public void EnglishLanguageTap(object sender, EventArgs args)
        {
            #region change logic toggle logic
            if (EnglishChangeLanguageSwitch.IsToggled == true)
            {
                MarathiChangeLanguageSwitch.IsToggled = true;
                EnglishChangeLanguageSwitch.IsToggled = false;
                Application.Current.Properties["Language"] = "mr";
                AppResources.Culture = new System.Globalization.CultureInfo("mr");
              //  OnBackButtonPressed();
            }
            else
            {
                MarathiChangeLanguageSwitch.IsToggled = false;
                EnglishChangeLanguageSwitch.IsToggled = true;
                Application.Current.Properties["Language"] = "en";
                AppResources.Culture = new System.Globalization.CultureInfo("en");
              //  OnBackButtonPressed();
            }
            #endregion
            this.Title = AppResources.LChangeLanguage.ToUpper();
        }
        public void MarathiLanguageTap(object sender, EventArgs args)
        {
            #region change logic toggle logic
            var obj = sender as Switch;
            if (MarathiChangeLanguageSwitch.IsToggled == true)
            {
                MarathiChangeLanguageSwitch.IsToggled = false;
                EnglishChangeLanguageSwitch.IsToggled = true;
                Application.Current.Properties["Language"] = "en";
                AppResources.Culture = new System.Globalization.CultureInfo("en");
               // OnBackButtonPressed();
            }
            else
            {
                MarathiChangeLanguageSwitch.IsToggled = true;
                EnglishChangeLanguageSwitch.IsToggled = false;
                Application.Current.Properties["Language"] = "mr";
                AppResources.Culture = new System.Globalization.CultureInfo("mr");
              //  OnBackButtonPressed();
            }
            #endregion
            this.Title = AppResources.LChangeLanguage.ToUpper();
        }
        protected override bool OnBackButtonPressed()
        {
            //if (_canClose)
            //{
            //    ShowExitDialog();
            //}
            //return _canClose;
            Application.Current.MainPage = new SideMenu();
            return true;
        }
    }
}
