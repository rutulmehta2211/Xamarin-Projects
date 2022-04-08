using Acr.UserDialogs;
using ERecall.CommonClasses;
using ERecall.Controls;
using ERecall.ServiceLayer;
using ERecall.ServiceLayer.ServiceModel;
using ERecall.Views.CommonPages;
using ERecall.Views.FeedsModule;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static ERecall.ServiceLayer.ServiceModel.GeteRecallSearch_MyERecall;

namespace ERecall.Views.MyERecallsModule
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class All : ContentPage
    {
        string url = string.Empty;
        int CatNo;
        ObservableCollection<Response> objLstResponse;
        public All()
        {
            InitializeComponent();
            lstMyERecall.ItemSelected += (sender, e) =>
            {
                ((ListView)sender).SelectedItem = null;
            };
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            switch (this.Title)
            {
                case "ALL":
                    CatNo = 0;
                    break;
                case "FOODS":
                    CatNo = 1;
                    break;
                case "PRODUCTS":
                    CatNo = 5;
                    break;
                case "VEHICLES":
                    CatNo = 4;
                    break;
                case "DRUGS":
                    CatNo = 2;
                    break;
                case "MEDICAL DEVICES":
                    CatNo = 3;
                    break;
            }
            url = ServiceURLs.GeteRecallSearch + "userId=" + App.User_UserId + "&catId=" + CatNo;
            lblTotalCount.Text = "0";
            lblNoRecordFound.IsVisible = false;
            objLstResponse = null;
            if (CheckInternetConnection.IsInternetConnected())
            {
                LoadingIndicator.IsVisible = true;
                await GetService_GeteRecallSearch();
                LoadingIndicator.IsVisible = false;
            }
            else
            {
                new ShowUserDialog(UserDialogs.Instance, "Internet Connection Required", System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }

        private void NoRecordFoundVisibility(bool IsVisible)
        {
            lstMyERecall.IsVisible = !IsVisible;
            lblNoRecordFound.IsVisible = IsVisible;
            if (IsVisible)
            {
                lblTotalCount.Text = "0";
            }
        }

        private async Task GetService_GeteRecallSearch()
        {
            try
            {
                var Service_response = await GetResponseFromWebService.GetResponse<GeteRecallSearch_MyERecall.RootObject>(url);
                if (Service_response != null)
                {
                    if (Service_response.status)
                    {
                        if (Service_response.response != null && Service_response.response.Count > 0)
                        {
                            NoRecordFoundVisibility(false);
                            lblTotalCount.Text = Convert.ToString(Service_response.response.Count);
                            objLstResponse = new ObservableCollection<Response>();
                            for (int i = 0; i < Service_response.response.Count; i++)
                            {
                                Response objResponse = new Response();
                                objResponse.CategoryId = Service_response.response[i].CategoryId;
                                objResponse.Date = Service_response.response[i].Date;
                                objResponse.Manufacturer = string.IsNullOrEmpty(Service_response.response[i].Manufacturer) ? "-" : Service_response.response[i].Manufacturer;
                                objResponse.IsBrandAvailable = string.IsNullOrEmpty(Service_response.response[i].Manufacturer) ? false : true;
                                objResponse.ManufacturerId = Service_response.response[i].ManufacturerId;
                                objResponse.Price = Service_response.response[i].Price;
                                objResponse.ProductName = Service_response.response[i].ProductName;
                                objResponse.RecallDate = Service_response.response[i].RecallDate;
                                objResponse.RecallId = Service_response.response[i].RecallId;
                                objResponse.RecallNo = Service_response.response[i].RecallNo;
                                objResponse.TotalCount = Service_response.response[i].TotalCount;
                                objResponse.UserId = Service_response.response[i].UserId;
                                objLstResponse.Add(objResponse);
                            }
                            lstMyERecall.ItemsSource = objLstResponse;
                        }
                        else
                        {
                            NoRecordFoundVisibility(true);
                        }
                    }
                    else
                    {
                        NoRecordFoundVisibility(true);
                        if (Service_response.statusCode == 417)
                        {
                            new ShowUserDialog(UserDialogs.Instance, "WebServer Exception", System.Drawing.Color.Red, System.Drawing.Color.White);
                        }
                    }
                }
                else
                {
                    new ShowUserDialog(UserDialogs.Instance, "WebServer not Responding", System.Drawing.Color.Red, System.Drawing.Color.White);
                    NoRecordFoundVisibility(true);
                }
            }
            catch (WebException ex)
            {
                new ShowUserDialog(UserDialogs.Instance, ex.Message, System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }

        private async void LstPulled(object sender, EventArgs e)
        {
            lstMyERecall.IsPullToRefreshEnabled = false;
            objLstResponse = null;
            if (CheckInternetConnection.IsInternetConnected())
            {
                LoadingIndicator.IsVisible = true;
                await GetService_GeteRecallSearch();
                LoadingIndicator.IsVisible = false;
            }
            else
            {
                new ShowUserDialog(UserDialogs.Instance, "Internet Connection Required", System.Drawing.Color.Red, System.Drawing.Color.White);
            }
            lstMyERecall.IsRefreshing = false;
            lstMyERecall.IsPullToRefreshEnabled = true;
        }

        private async Task PostService_IOwnThisProduct(int recallId, string price)
        {
            try
            {
                PostIOwnThisProduct.PostIOwnIt objPostIOwnIt = new PostIOwnThisProduct.PostIOwnIt();
                objPostIOwnIt.UserId = Convert.ToString(App.User_Id);
                objPostIOwnIt.RecallId = recallId;
                objPostIOwnIt.Price = price;
                string jsonContents = JsonConvert.SerializeObject(objPostIOwnIt);
                var Service_response = await GetResponseFromWebService.GetResponsePostData<PostIOwnThisProduct.RootObject>(ServiceURLs.PostIOwnThisProduct, jsonContents);
                if (Service_response != null)
                {
                    if (Service_response.status && Service_response.statusCode == 202)
                    {
                        HelperToast.LongMessage("This product has been added sucessfully to the products list");
                    }
                    else
                    {
                        if (Service_response.statusCode == 409)
                        {
                            new ShowUserDialog(UserDialogs.Instance, "This product already exists in Product list", System.Drawing.Color.Red, System.Drawing.Color.White);
                        }
                        else if (Service_response.statusCode == 400)
                        {
                            new ShowUserDialog(UserDialogs.Instance, "Invalid Data", System.Drawing.Color.Red, System.Drawing.Color.White);
                        }
                        else if (Service_response.statusCode == 417)
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

        private void stkItem_Tapped(object sender, EventArgs e)
        {
            var obj = sender as StackLayout;
            var RecallId = obj.ClassId;
            if (!string.IsNullOrEmpty(RecallId))
            {
                var stack = Navigation.NavigationStack;
                if (stack[stack.Count - 1].GetType() != typeof(RecalledItemDetails))
                {
                    Navigation.PushAsync(new RecalledItemDetails(RecallId));
                }
            }
        }

        private void stkShare_Tapped(object sender, EventArgs e)
        {
            try
            {
                var obj = sender as StackLayout;
                var RecallId = obj.ClassId;
                if (!string.IsNullOrEmpty(RecallId) && objLstResponse != null && objLstResponse.Count > 0)
                {
                    var item = objLstResponse.FirstOrDefault(s => s.RecallId == Convert.ToInt32(RecallId));
                    if (item != null)
                    {
                        Sharing.Share("Share", item.ProductName, null);
                    }
                }
            }
            catch (Exception ex)
            {
                new ShowUserDialog(UserDialogs.Instance, ex.Message, System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }

        private void stkContactSupplier_Tapped(object sender, EventArgs e)
        {
            try
            {
                var obj = sender as StackLayout;
                var RecallId = obj.ClassId;
                if (!string.IsNullOrEmpty(RecallId) && objLstResponse != null && objLstResponse.Count > 0)
                {
                    var item = objLstResponse.FirstOrDefault(s => s.RecallId == Convert.ToInt32(RecallId));
                    var stack = Navigation.NavigationStack;
                    if (stack[stack.Count - 1].GetType() != typeof(NewClaim))
                    {
                        Navigation.PushAsync(new NewClaim(item, null));
                    }
                }
            }
            catch (Exception ex)
            {
                new ShowUserDialog(UserDialogs.Instance, ex.Message, System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }

        private async void stkIOwnThis_Tapped(object sender, EventArgs e)
        {
            var obj = sender as StackLayout;
            var RecallId = obj.ClassId;
            if (!string.IsNullOrEmpty(RecallId))
            {
                if (CheckInternetConnection.IsInternetConnected())
                {
                    if (objLstResponse != null && objLstResponse.Count > 0)
                    {
                        LoadingIndicator.IsVisible = true;
                        var item = objLstResponse.FirstOrDefault(s => s.RecallId == Convert.ToInt32(RecallId));
                        await PostService_IOwnThisProduct(Convert.ToInt32(RecallId), Convert.ToString(item.Price));
                        LoadingIndicator.IsVisible = false;
                    }
                }
                else
                {
                    new ShowUserDialog(UserDialogs.Instance, "Internet Connection Required", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
            }
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            var stack = Navigation.NavigationStack;
            if (stack[stack.Count - 1].GetType() != typeof(SearchMyeRecall))
            {
                Navigation.PushAsync(new SearchMyeRecall(CatNo));
            }
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