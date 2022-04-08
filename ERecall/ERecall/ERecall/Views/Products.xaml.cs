using Acr.UserDialogs;
using ERecall.CommonClasses;
using ERecall.Controls;
using ERecall.ServiceLayer;
using ERecall.ServiceLayer.ServiceModel;
using ERecall.Views.ProductCenter;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static ERecall.ServiceLayer.ServiceModel.GetListOfImportedProductsByCategory;

namespace ERecall.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Products : ContentPage
    {
        public Products()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (CheckInternetConnection.IsInternetConnected())
            {
                LoadingIndicator.IsVisible = true;
                await GetService_GetListOfImportedProductsByCategory("1");
                await GetService_GetListOfImportedProductsByCategory("5");
                await GetService_GetListOfImportedProductsByCategory("4");
                await GetService_GetListOfImportedProductsByCategory("2");
                await GetService_GetListOfImportedProductsByCategory("3");
                LoadingIndicator.IsVisible = false;
            }
            else
            {
                new ShowUserDialog(UserDialogs.Instance, "Internet Connection Required", System.Drawing.Color.Red, System.Drawing.Color.White);
            }

        }
        private async Task GetService_GetListOfImportedProductsByCategory(string CatNo)
        {
            try
            {
                string url = ServiceURLs.GetListOfImportedProductsByCategory + "userId=" + App.User_UserId +"&catId=" + CatNo;
                var Service_response = await GetResponseFromWebService.GetResponse<GetListOfImportedProductsByCategory.RootObject>(url);
                if (Service_response != null)
                {
                    if (Service_response.status)
                    {
                        if (Service_response.response != null && Service_response.response.Count > 0)
                        {
                            switch(CatNo)
                            {
                                case "1":
                                    lblFoodItemCount.Text = Convert.ToString(Service_response.response.Count) + " Items";
                                    lblFoodLastUpdateData.Text = Service_response.response[0].ProductName;
                                    break;
                                case "5":
                                    lblProductsItemCount.Text = Convert.ToString(Service_response.response.Count) + " Items";
                                    lblProductsLastUpdateData.Text = Service_response.response[0].ProductName;
                                    break;
                                case "4":
                                    lblVehicleItemCount.Text = Convert.ToString(Service_response.response.Count) + " Items";
                                    lblVehicleLastUpdateData.Text = Service_response.response[0].ProductName;
                                    break;
                                case "2":
                                    lblDrugsItemCount.Text = Convert.ToString(Service_response.response.Count) + " Items";
                                    lblDrugsLastUpdateData.Text = Service_response.response[0].ProductName;
                                    break;
                                case "3":
                                    lblMedicalDeviceItemCount.Text = Convert.ToString(Service_response.response.Count) + " Items";
                                    lblMedicalDeviceLastUpdateData.Text = Service_response.response[0].ProductName;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        if (Service_response.statusCode == 417)
                        {
                            new ShowUserDialog(UserDialogs.Instance, "WebServer Exception", System.Drawing.Color.Red, System.Drawing.Color.White);
                        }
                        MakeEmptyLabels(CatNo);
                    }
                }
                else
                {
                    new ShowUserDialog(UserDialogs.Instance, "WebServer not Responding", System.Drawing.Color.Red, System.Drawing.Color.White);
                    MakeEmptyLabels(CatNo);
                }
            }
            catch (WebException ex)
            {
                new ShowUserDialog(UserDialogs.Instance, ex.Message, System.Drawing.Color.Red, System.Drawing.Color.White);
                MakeEmptyLabels(CatNo);
            }
        }

        private void MakeEmptyLabels(string CatNo)
        {
            switch (CatNo)
            {
                case "1":
                    lblFoodItemCount.Text = "0 Items";
                    lblFoodLastUpdateData.Text = string .Empty;
                    break;
                case "5":
                    lblProductsItemCount.Text = "0 Items";
                    lblProductsLastUpdateData.Text = string.Empty;
                    break;
                case "4":
                    lblVehicleItemCount.Text = "0 Items";
                    lblVehicleLastUpdateData.Text = string.Empty;
                    break;
                case "2":
                    lblDrugsItemCount.Text = "0 Items";
                    lblDrugsLastUpdateData.Text = string.Empty;
                    break;
                case "3":
                    lblMedicalDeviceItemCount.Text = "0 Items";
                    lblMedicalDeviceLastUpdateData.Text = string.Empty;
                    break;
            }
        }

        private void NavigateToDetailPage(int pageindex)
        {
            var stack = Navigation.NavigationStack;
            if (stack[stack.Count - 1].GetType() != typeof(Products_Tabbed))
            {
                Navigation.PushAsync(new Products_Tabbed(pageindex));
            }
        }

        private void lblSeeAllVehicles_Tapped(object sender, EventArgs e)
        {
            var obj = sender as Label;
            var pageindex = obj.ClassId;
            NavigateToDetailPage(Convert.ToInt32(pageindex));
        }

        private void lblSeeAllProducts_Tapped(object sender, EventArgs e)
        {
            var obj = sender as Label;
            var pageindex = obj.ClassId;
            NavigateToDetailPage(Convert.ToInt32(pageindex));
        }

        private void lblSeeAllFoods_Tapped(object sender, EventArgs e)
        {
            var obj = sender as Label;
            var pageindex = obj.ClassId;
            NavigateToDetailPage(Convert.ToInt32(pageindex));
        }

        private void lblSeeAllDrugs_Tapped(object sender, EventArgs e)
        {
            var obj = sender as Label;
            var pageindex = obj.ClassId;
            NavigateToDetailPage(Convert.ToInt32(pageindex));
        }

        private void lblSeeAllMedicalDevices_Tapped(object sender, EventArgs e)
        {
            var obj = sender as Label;
            var pageindex = obj.ClassId;
            NavigateToDetailPage(Convert.ToInt32(pageindex));
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