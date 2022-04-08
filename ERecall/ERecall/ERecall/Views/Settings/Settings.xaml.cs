using Acr.UserDialogs;
using ERecall.CommonClasses;
using ERecall.Controls;
using ERecall.ServiceLayer;
using ERecall.ServiceLayer.ServiceModel;
using ERecall.Views.Menu;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ERecall.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        Dictionary<int, string> dicNotifyMe;
        Dictionary<int, string> dicNotifyInterval;
        public Settings()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            FillNotifyMeAndInterval();
            if (CheckInternetConnection.IsInternetConnected())
            {
                LoadingIndicator.IsVisible = true;
                await GetService_GetUserMobileSetting();
                LoadingIndicator.IsVisible = false;
            }
            else
            {
                new ShowUserDialog(UserDialogs.Instance, "Internet Connection Required", System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }

        private void FillNotifyMeAndInterval()
        {
            dicNotifyMe = new Dictionary<int, string>();
            dicNotifyInterval = new Dictionary<int, string>();
            dicNotifyMe.Add(0, "Daily");
            dicNotifyMe.Add(1, "weekly");
            dicNotifyMe.Add(2, "Monthly");
            for (int i = 1; i <= 30; i++)
            {
                dicNotifyInterval.Add(i, i + " day");
            }
            pkrNotifyMe.ItemsSource = dicNotifyMe.Values.ToList();
            pkrNotifyInterval.ItemsSource = dicNotifyInterval.Values.ToList();
            pkrNotifyMe.SelectedIndex = 0;
            pkrNotifyInterval.SelectedIndex = 4;
        }

        private async void Save_Activated(object sender, EventArgs e)
        {
            if (CheckInternetConnection.IsInternetConnected())
            {
                LoadingIndicator.IsVisible = true;
                await PostService_AddUsermobileSetting();
                LoadingIndicator.IsVisible = false;
            }
            else
            {
                new ShowUserDialog(UserDialogs.Instance, "Internet Connection Required", System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }

        private async Task GetService_GetUserMobileSetting()
        {
            try
            {
                var Service_response = await GetResponseFromWebService.GetResponse<GetUserMobileSetting.RootObject>(ServiceURLs.GetUserMobileSetting + App.User_UserId);
                if (Service_response != null)
                {
                    if (Service_response.status)
                    {
                        if (Service_response.response != null && Service_response.response.Count > 0)
                        {
                            SwitchNewProduct.IsToggled = Service_response.response[0].NotifyNewProductInProductCenter;
                            SwitchExistingProduct.IsToggled = Service_response.response[0].NotifyProductRecalls;
                            SwitchResolutionCenter.IsToggled = Service_response.response[0].NotifyResolutionCenter;
                            MatchCollection matchCollection = Regex.Matches(Service_response.response[0].NotifyAppUse, "'(.*?)'");//@"'(.*?)'"  //"\"(.*?)\""
                            if (matchCollection.Count > 0)
                            {
                                string[] stringCollections = new string[matchCollection.Count];
                                for (int i = 0; i < stringCollections.Length; i++)
                                {
                                    stringCollections[i] = matchCollection[i].Groups[1].Value;
                                }
                                pkrNotifyMe.SelectedItem = dicNotifyMe.FirstOrDefault(d => d.Value.ToUpper() == stringCollections[3].ToUpper()).Value;
                                pkrNotifyInterval.SelectedItem = dicNotifyInterval.FirstOrDefault(d => d.Key == Convert.ToInt32(stringCollections[1])).Value;
                            }
                            MatchCollection matchCollection2 = Regex.Matches(Service_response.response[0].NotifyAppUse, "\"(.*?)\"");//@"'(.*?)'"  //"\"(.*?)\""
                            if (matchCollection2.Count > 0)
                            {
                                string[] stringCollections = new string[matchCollection2.Count];
                                for (int i = 0; i < stringCollections.Length; i++)
                                {
                                    stringCollections[i] = matchCollection2[i].Groups[1].Value;
                                }
                                pkrNotifyMe.SelectedItem = dicNotifyMe.FirstOrDefault(d => d.Value.ToUpper() == stringCollections[3].ToUpper()).Value;
                                pkrNotifyInterval.SelectedItem = dicNotifyInterval.FirstOrDefault(d => d.Key == Convert.ToInt32(stringCollections[1])).Value;
                            }
                        }
                    }
                    else
                    {
                        if (Service_response.statusCode == 417)
                        {
                            new ShowUserDialog(UserDialogs.Instance, "WebServer Exception", System.Drawing.Color.Red, System.Drawing.Color.White);
                        }
                        else if(Service_response.statusCode == 404)
                        {
                            new ShowUserDialog(UserDialogs.Instance, "Data not found", System.Drawing.Color.Red, System.Drawing.Color.White);
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

        private async Task PostService_AddUsermobileSetting()
        {
            try
            {
                PostAddUsermobileSetting.PostUserSettings objPostUserSettings = new PostAddUsermobileSetting.PostUserSettings();
                objPostUserSettings.UserId = App.User_UserId;
                objPostUserSettings.NotifyNewProductInProductCenter = SwitchNewProduct.IsToggled;
                objPostUserSettings.NotifyProductRecalls = SwitchExistingProduct.IsToggled;
                objPostUserSettings.NotifyResolutionCenter = SwitchResolutionCenter.IsToggled;
                var NotifyMe = dicNotifyMe.FirstOrDefault(s => s.Value == pkrNotifyMe.Items[pkrNotifyMe.SelectedIndex]).Value;
                var NotifyInterval= Convert.ToString(dicNotifyInterval.FirstOrDefault(s => s.Value == pkrNotifyInterval.Items[pkrNotifyInterval.SelectedIndex]).Key);
                objPostUserSettings.NotifyAppUse = "{'Threshold': '" + NotifyInterval + "', 'Interval' : '" + NotifyMe + "'}";
                string jsonContents = JsonConvert.SerializeObject(objPostUserSettings);
                var Service_response = await GetResponseFromWebService.GetResponsePostData<PostAddUsermobileSetting.RootObject>(ServiceURLs.AddUsermobileSetting, jsonContents);
                if (Service_response != null)
                {
                    if (Service_response.status)
                    {
                        HelperToast.LongMessage("User's settings are saved.");
                    }
                    else
                    {
                        if (Service_response.statusCode == 417)
                        {
                            new ShowUserDialog(UserDialogs.Instance, "WebServer Exception", System.Drawing.Color.Red, System.Drawing.Color.White);
                        }
                    }
                }
                else
                {
                    new ShowUserDialog(UserDialogs.Instance, "WebServer not Responding", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
            }
            catch (Exception ex)
            {
                new ShowUserDialog(UserDialogs.Instance, ex.Message, System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }

        protected override bool OnBackButtonPressed()
        {
            Application.Current.MainPage = new SideMenu();
            return true;
        }

        /// <summary>
        /// Function to generate Alert
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="image"></param>
        /// <param name="bgcolor"></param>
        private void Alert(string title, string message, string image, Color bgcolor)
        {
            MaingrdAlert.IsVisible = true;
            CustAlertView.AlertTitle = title;
            CustAlertView.AlertMessage = message;
            CustAlertView.AlertImage = image;
            CustAlertView.AlertBackground = bgcolor;
            CustAlertView.HeightRequest = App.DeviceHeight * 0.13;
            CustAlertView.WidthRequest = App.DeviceHeight * 0.13;
        }
        /// <summary>
        /// Tapped Event of Validation alert Grid to close validation alert
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MaingrdAlert_Tapped(object sender, EventArgs e)
        {
            MaingrdAlert.IsVisible = false;
            MaingrdForms.IsVisible = true;
        }
    }
}