using Acr.UserDialogs;
using ERecall.CommonClasses;
using ERecall.Controls;
using ERecall.ServiceLayer;
using ERecall.ServiceLayer.ServiceModel;
using ERecall.Views.InviteFriendsModule;
using ERecall.Views.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ERecall.Views.SignUp_Onboarding
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OnboardingScreens : ContentPage
	{
        List<OnBardingImagesList> LstOnboardingImages;
        List<GetAccountProfileByUserId.Response> objResponse;
        string userEmail = string.Empty;
        string userPassword = string.Empty;

        public OnboardingScreens(string userEmail, string userPassword)
		{
			InitializeComponent ();
            this.userEmail = userEmail;
            this.userPassword = userPassword;
            LoadingIndicator.IsVisible = false;
            LstOnboardingImages = new List<OnBardingImagesList>
            {
                new OnBardingImagesList
                {
                    ImageUrl = "onboarding_1.png"
                },
                new OnBardingImagesList
                {
                    ImageUrl = "onboarding_2.png"
                },
                new OnBardingImagesList
                {
                    ImageUrl = "onboarding_3.png"
                },
                new OnBardingImagesList
                {
                    ImageUrl = "onboarding_4.png"
                }
            };
            CarouselView.ItemsSource = LstOnboardingImages;
            SkipButtonVisibility();
        }

        private void SkipButtonVisibility()
        {
            if (CarouselView.Position == 0)
            {
                btnSkip.IsVisible = true;
                btnBack.IsVisible = false;
            }
            else if(CarouselView.Position > 0 && CarouselView.Position < 4)
            {
                btnSkip.IsVisible = false;
                btnBack.IsVisible = true;
            }
        }

        private async void btnSkip_Clicked(object sender, EventArgs e)
        {
            if (CheckInternetConnection.IsInternetConnected())
            {
                LoadingIndicator.IsVisible = true;
                await GetService_SignIn();
                LoadingIndicator.IsVisible = false;
            }
            else
            {
                new ShowUserDialog(UserDialogs.Instance, "Internet Connection Required", System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }

        private async void btnNext_Clicked(object sender, EventArgs e)
        {
            if (CarouselView.Position < 3)
            {
                CarouselView.Position++;
            }
            else
            {
                if (CheckInternetConnection.IsInternetConnected())
                {
                    LoadingIndicator.IsVisible = true;
                    await GetService_SignIn();
                    LoadingIndicator.IsVisible = false;
                }
                else
                {
                    new ShowUserDialog(UserDialogs.Instance, "Internet Connection Required", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
            }
            SkipButtonVisibility();
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            if (CarouselView.Position >= 0)
            {
                CarouselView.Position--;
            }
            SkipButtonVisibility();
        }

        private void CarouselView_PositionSelected(object sender, SelectedPositionChangedEventArgs e)
        {
            SkipButtonVisibility();
        }

        private async Task GetService_SignIn()
        {
            try
            {
                string url = ServiceURLs.SignIn + "email=" + userEmail + "&password=" + userPassword;
                var Service_response = await GetResponseFromWebService.GetResponse<SignIn.RootObject>(url);
                if (Service_response != null)
                {
                    if (Service_response.status)
                    {
                        Application.Current.Properties["User_Email"] = App.User_Email = string.IsNullOrEmpty(Service_response.response.Email) ? string.Empty : Service_response.response.Email;
                        Application.Current.Properties["User_Name"] = App.User_Name = string.IsNullOrEmpty(Service_response.response.Name) ? string.Empty : Service_response.response.Name;
                        Application.Current.Properties["User_UserName"] = App.User_UserName = string.IsNullOrEmpty(Service_response.response.UserName) ? string.Empty : Service_response.response.UserName;
                        Application.Current.Properties["User_UserId"] = App.User_UserId = Service_response.response.UserId == 0 ? 0 : Service_response.response.UserId;
                        Application.Current.Properties["User_Id"] = App.User_Id = string.IsNullOrEmpty(Service_response.response.Id) ? string.Empty : Service_response.response.Id;
                        await GetService_GetAccountProfileByUserId();
                        await GetService_Notifications();
                        Application.Current.MainPage = new NavigationPage(new InviteFriendsAfterOnboarding());
                    }
                    else
                    {
                        if (Service_response.statusCode == 417)
                        {
                            new ShowUserDialog(UserDialogs.Instance, "WebServer Exception", System.Drawing.Color.Red, System.Drawing.Color.White);
                        }
                        else if (Service_response.statusCode == 203)
                        {
                            new ShowUserDialog(UserDialogs.Instance, "Invalid Email or Password", System.Drawing.Color.Red, System.Drawing.Color.White);
                        }
                    }
                }
                else
                {
                    new ShowUserDialog(UserDialogs.Instance, "WebServer not Responding", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
            }
            catch (WebException ex)
            {
                new ShowUserDialog(UserDialogs.Instance, ex.Message, System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }

        private async Task GetService_GetAccountProfileByUserId()
        {
            try
            {
                var url = ServiceURLs.GetAccountProfileByUserId + App.User_UserId;
                var Service_response = await GetResponseFromWebService.GetResponse<GetAccountProfileByUserId.RootObject>(url);
                if (Service_response != null)
                {
                    if (Service_response.status)
                    {
                        if (Service_response.response != null && Service_response.response.Count > 0)
                        {
                            objResponse = new List<GetAccountProfileByUserId.Response>();
                            for (int i = 0; i < Service_response.response.Count; i++)
                            {
                                GetAccountProfileByUserId.Response objResponse = new GetAccountProfileByUserId.Response();
                                Application.Current.Properties["User_ImporedProductsCount"] = objResponse.ImporedProductsCount = App.User_ImporedProductsCount = Service_response.response[i].ImporedProductsCount;
                                Application.Current.Properties["User_OpenTickets"] = objResponse.OpenTickets = App.User_OpenTickets = Service_response.response[i].OpenTickets;
                                Application.Current.Properties["User_AvailableReports"] = objResponse.AvailableReports = App.User_AvailableReports = Service_response.response[i].AvailableReports;
                                Application.Current.Properties["User_Credits"] = objResponse.Credits = App.User_Credits = Service_response.response[i].Credits;
                                Application.Current.Properties["User_AccountTypeId"] = App.User_AccountTypeId = Service_response.response[i].AccountTypeId;
                                Application.Current.Properties["User_AccountTypeName"] = App.User_AccountTypeName = Service_response.response[i].AccountTypeName;
                                Application.Current.Properties["User_AccountId"] = App.User_AccountId = Service_response.response[i].AccountId;
                            }
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                new ShowUserDialog(UserDialogs.Instance, ex.Message, System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }

        private async Task GetService_Notifications()
        {
            try
            {
                string url = ServiceURLs.Notifications + "userName=" + App.User_Email;
                var Service_response = await GetResponseFromWebService.GetResponse<ServiceLayer.ServiceModel.Notifications.RootObject>(url);
                if (Service_response != null)
                {
                    if (Service_response.status)
                    {
                        if (Service_response.response != null && Service_response.response.Count > 0)
                        {
                            var objLstNewProducts = Service_response.response.Where(d => d.IsRead == false).ToList();
                            if (objLstNewProducts != null && objLstNewProducts.Count > 0)
                            {
                                Application.Current.Properties["User_NotificationCount"] = App.User_NotificationCount = objLstNewProducts.Count;
                            }
                            else
                            {
                                Application.Current.Properties["User_NotificationCount"] = App.User_NotificationCount = 0;
                            }
                        }
                    }
                    else
                    {
                        Application.Current.Properties["User_NotificationCount"] = App.User_NotificationCount = 0;
                    }
                }
                else
                {
                    Application.Current.Properties["User_NotificationCount"] = App.User_NotificationCount = 0;
                }
            }
            catch (WebException ex)
            {
                Application.Current.Properties["User_NotificationCount"] = App.User_NotificationCount = 0;
            }
        }

        private int _position;
        public int Position { get { return _position; } set { _position = value; OnPropertyChanged(); } }

    }
    public class OnBardingImagesList
    {
        public string ImageUrl { get; set; }
    }
}