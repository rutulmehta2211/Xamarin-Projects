using CMO.MenuController;
using CMO.ServicesClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CMO.Gallery
{
    public partial class MagazineGallery : ContentPage
    {
        string pdfurl = null;
        public MagazineGallery()
        {
            InitializeComponent();
			LMaharashtraAhead.FontSize = App.GetFontSizeMedium();
			LMagazineGalleryJune2015.FontSize = App.GetFontSizeMedium();
            this.Title = AppResources.LMagazineGallery.ToUpper();
           
        }
        protected async override void OnAppearing()
        {
            Application.Current.Properties["CurrentPage"] = "magazinegallery";
            if (Application.Current.Properties.ContainsKey("navigationflag"))
            {
                var _currentPage = Application.Current.Properties["navigationflag"] as string;
                if (_currentPage == "0")
                {
                    if (!App.CheckInternetConnection())
                    {
                        Indicatior_A.IsRunning = false;
                        Indicatior_A.IsVisible = false;
                        await DisplayAlert(AppResources.LNetworkError, AppResources.LNoInternetConnection, AppResources.LOk);
                    }
                    else
                    {
                        await CallWebService();
                    }
                }
            }
           
        }
        private async Task CallWebService()
        {
            if (!App.CheckInternetConnection())
            {
                await DisplayAlert(AppResources.LNetworkError, AppResources.LNoInternetConnection,AppResources.LOk);
            }
            else
            {
                try
                {
                    List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>();
                    string lang = "";
                    if (Application.Current.Properties.ContainsKey("Language"))
                    { lang = Application.Current.Properties["Language"] as string; }
                    values.Add(new KeyValuePair<string, string>("lang", lang));

                    var response = await GeneralClass.GetResponse<RootObjectMagazineGallery>("http://14.141.36.212/maharastracmo/api/getmagazinegallerylist", values);
                    if (response != null)
                    {
                        // Title = response.page_title;
                        if (response._resultflag == 1)
                        {
                            imgSource.Source = response.src;
                            LMaharashtraAhead.Text = AppResources.LMaharashtraAhead;
                            LMagazineGalleryJune2015.Text = AppResources.LMagazineGalleryJune2015;
                            pdfurl = response.href;
                            MainStack.IsVisible = true;
                        }
                        else
                        {
                            if (App.CurrentPage() == "magazinegallery")
								await DisplayAlert(AppResources.LError, AppResources.LSomethingWentWrong, AppResources.LOk);
						}
                    }
                    else
                    {
						if (App.CurrentPage() == "magazinegallery")
							await DisplayAlert(AppResources.LError, AppResources.LSomethingWentWrong, AppResources.LOk);
                    }
                }

                catch (WebException ex)
                {
                   if (App.CurrentPage() == "magazinegallery")
					{
						if (ex.Message.Contains("Network"))
							await DisplayAlert(AppResources.LError, AppResources.LWebserverNotResponding, AppResources.LOk);
					}
                }
            }
            Indicatior_A.IsRunning = false;
            Indicatior_A.IsVisible = false;
        }
        public async void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            var answer = await DisplayAlert(AppResources.LNotification, AppResources.LDownloadFile, AppResources.LYes,AppResources.LNo);
            if (answer == true)
            {
                if (pdfurl != null)
                {
                    var uri = new Uri(pdfurl);
                    Device.OpenUri(uri);
                }
            }
        }
        #region Exit Application
        private bool _canClose = true;

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

        private async void ShowExitDialog()
        {
            var answer = await DisplayAlert(AppResources.LExit, AppResources.LExitApp, AppResources.LYes,AppResources.LNo);
            if (answer)
            {
                if (Device.OS == TargetPlatform.Android)
                {
                    DependencyService.Get<IAndroidMethods>().CloseApp();
                }
                _canClose = false;
                OnBackButtonPressed();
            }
        }
        #endregion
    }
}
