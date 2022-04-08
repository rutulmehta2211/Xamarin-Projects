using Acr.UserDialogs;
using ERecall.CommonClasses;
using ERecall.Controls;
using ERecall.ServiceLayer;
using ERecall.ServiceLayer.ServiceModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ERecall.Views.Notifications
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Notification_Content : ContentPage
    {
        ObservableCollection<ServiceLayer.ServiceModel.Notifications.Response> objLstResponse;
        public Notification_Content()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            lblNoRecordFound.IsVisible = false;
            if (CheckInternetConnection.IsInternetConnected())
            {
                LoadingIndicator.IsVisible = true;
                await GetService_Notifications();
                LoadingIndicator.IsVisible = false;
            }
            else
            {
                new ShowUserDialog(UserDialogs.Instance, "Internet Connection Required", System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }

        private void NoRecordFoundVisibility(bool IsVisible)
        {
            lstNotifications.IsVisible = !IsVisible;
            lblNoRecordFound.IsVisible = IsVisible;
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
                            NoRecordFoundVisibility(false);
                            objLstResponse = new ObservableCollection<ServiceLayer.ServiceModel.Notifications.Response>();
                            for (int i = 0; i < Service_response.response.Count; i++)
                            {
                                ServiceLayer.ServiceModel.Notifications.Response objResponse = new ServiceLayer.ServiceModel.Notifications.Response();
                                objResponse.NotificationId = Service_response.response[i].NotificationId;
                                objResponse.NotificationName = Service_response.response[i].NotificationName;
                                objResponse.ProductCount = Service_response.response[i].ProductCount;
                                objResponse.ProductName = Service_response.response[i].ProductName;
                                objResponse.ProductDesc = Service_response.response[i].ProductDesc;
                                objResponse.ProductImg = Service_response.response[i].ProductImg;
                                objResponse.NotificationDate = Service_response.response[i].NotificationDate;
                                objResponse.CategoryId = Service_response.response[i].CategoryId;
                                objResponse.ProductRecallId = Service_response.response[i].ProductRecallId;
                                objResponse.RecallNumber = Service_response.response[i].RecallNumber;
                                objResponse.UserId = Service_response.response[i].UserId;
                                objResponse.CreatedBy = Service_response.response[i].CategoryId;
                                if(Service_response.response[i].IsRead)
                                {
                                    objResponse.bgcolor = Color.White; //ffde00
                                }
                                else
                                {
                                    objResponse.bgcolor = Color.FromHex("#f1f1f1");
                                }
                                objResponse.IsRead = Service_response.response[i].IsRead;
                                objLstResponse.Add(objResponse);
                            }
                            switch (this.Title)
                            {
                                case "NEW PRODUCTS":
                                    var objLstNewProducts = objLstResponse.Where(d => d.NotificationName == "IMPORTED_PRODUCTS").ToList(); //d.IsRead == false
                                    ObservableCollection<ServiceLayer.ServiceModel.Notifications.Response> LstNewProductsFiltered = new ObservableCollection<ServiceLayer.ServiceModel.Notifications.Response>(objLstNewProducts);
                                    lstNotifications.ItemsSource = LstNewProductsFiltered;
                                    break;
                                case "PRODUCT CENTER":
                                    var objLstProductCenter = objLstResponse.Where(d => d.NotificationName == "RECALLED_PRODUCTS_CENTER").ToList(); //d.IsRead == false
                                    ObservableCollection<ServiceLayer.ServiceModel.Notifications.Response> LstProductCenterFiltered = new ObservableCollection<ServiceLayer.ServiceModel.Notifications.Response>(objLstProductCenter);
                                    lstNotifications.ItemsSource = LstProductCenterFiltered;
                                    break;
                                case "RESOLUTION CENTER":
                                    var objLstResolutionCenter= objLstResponse.Where(d => d.NotificationName == "RESOLUTION_CENTER").ToList(); //d.IsRead == false
                                    ObservableCollection<ServiceLayer.ServiceModel.Notifications.Response> LstResolutionCenterFiltered = new ObservableCollection<ServiceLayer.ServiceModel.Notifications.Response>(objLstResolutionCenter);
                                    lstNotifications.ItemsSource = LstResolutionCenterFiltered;
                                    break;
                            }
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

        private async Task GetService_NotificationRead(string NotificationId)
        {
            try
            {
                string url = ServiceURLs.NotificationRead + "NotificationId=" + NotificationId;
                var Service_response = await GetResponseFromWebService.GetResponsePostData<NotificationRead.RootObject>(url,null);
                if (Service_response != null)
                {
                    if (Service_response.status)
                    {
                        if (Service_response.response != null && Service_response.statusCode == 200)
                        {
                            HelperToast.LongMessage("Thank you to read Notification");
                            objLstResponse = null;
                            await GetService_Notifications();
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
                            new ShowUserDialog(UserDialogs.Instance, "Data Not Available", System.Drawing.Color.Red, System.Drawing.Color.White);
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

        private async void LstPulled(object sender, EventArgs e)
        {
            objLstResponse = null;
            if (CheckInternetConnection.IsInternetConnected())
            {
                LoadingIndicator.IsVisible = true;
                await GetService_Notifications();
                LoadingIndicator.IsVisible = false;
            }
            else
            {
                new ShowUserDialog(UserDialogs.Instance, "Internet Connection Required", System.Drawing.Color.Red, System.Drawing.Color.White);
            }
            lstNotifications.IsRefreshing = false;
            lstNotifications.IsPullToRefreshEnabled = true;
        }

        private async void NotificationItem_Tapped(object sender, EventArgs e)
        {
            var selectedData = sender as Grid;
            var NotificationId = selectedData.ClassId;
            if (!string.IsNullOrEmpty(NotificationId))
            {
                if (CheckInternetConnection.IsInternetConnected())
                {
                    if (objLstResponse!=null && objLstResponse.Count > 0)
                    {
                        var IsRead = objLstResponse.FirstOrDefault(d => d.NotificationId == Convert.ToInt32(NotificationId)).IsRead;
                        if (!IsRead)
                        {
                            LoadingIndicator.IsVisible = true;
                            await GetService_NotificationRead(NotificationId);
                            LoadingIndicator.IsVisible = false;
                        }
                        else
                        {
                            new ShowUserDialog(UserDialogs.Instance, "This item is already read", System.Drawing.Color.Red, System.Drawing.Color.White);
                        } 
                    }
                }
                else
                {
                    new ShowUserDialog(UserDialogs.Instance, "Internet Connection Required", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
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