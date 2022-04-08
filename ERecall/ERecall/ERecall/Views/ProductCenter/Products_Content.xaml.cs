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
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static ERecall.ServiceLayer.ServiceModel.GetListOfImportedProductsByCategory;

namespace ERecall.Views.ProductCenter
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Products_Content : ContentPage
    {
        string url = string.Empty;
        ObservableCollection<Response> objLstResponse;
        int CatNo;
        public Products_Content()
        {
            InitializeComponent();
            lstProducts.ItemSelected += (sender, e) =>
            {
                ((ListView)sender).SelectedItem = null;
            };
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            switch(this.Title)
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
            url = ServiceURLs.GetListOfImportedProductsByCategory + "userId="+App.User_UserId+"&catId="+ CatNo;
            lblNoRecordFound.IsVisible = false;
            objLstResponse = null;

            if (CheckInternetConnection.IsInternetConnected())
            {
                LoadingIndicator.IsVisible = true;
                await GetService_GetListOfImportedProductsByCategory();
                LoadingIndicator.IsVisible = false;
            }
            else
            {
                new ShowUserDialog(UserDialogs.Instance, "Internet Connection Required", System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }

        private void NoRecordFoundVisibility(bool IsVisible)
        {
            lstProducts.IsVisible = !IsVisible;
            lblNoRecordFound.IsVisible = IsVisible;
        }

        private async Task GetService_GetListOfImportedProductsByCategory()
        {
            try
            {
                var Service_response = await GetResponseFromWebService.GetResponse<GetListOfImportedProductsByCategory.RootObject>(url);
                if (Service_response != null)
                {
                    if (Service_response.status)
                    {
                        if (Service_response.response != null && Service_response.response.Count > 0)
                        {
                            NoRecordFoundVisibility(false);
                            objLstResponse = new ObservableCollection<GetListOfImportedProductsByCategory.Response>();
                            for (int i = 0; i < Service_response.response.Count; i++)
                            {
                                Response objResponse = new Response();
                                objResponse.ProductName = Service_response.response[i].ProductName;
                                objResponse.ProductImg = Service_response.response[i].ProductImg;
                                switch(CatNo)
                                {
                                    case 0:
                                        objResponse.PlaceholderImg = "product.png";
                                        break;
                                    case 1:
                                        objResponse.PlaceholderImg = "product_foods.png";
                                        break;
                                    case 2:
                                        objResponse.PlaceholderImg = "product_drugs.png";
                                        break;
                                    case 3:
                                        objResponse.PlaceholderImg = "product_medicaldevice.png";
                                        break;
                                    case 4:
                                        objResponse.PlaceholderImg = "product_vehicle.png";
                                        break;
                                    case 5:
                                        objResponse.PlaceholderImg = "product.png";
                                        break;
                                }
                                objResponse.ProductId = Service_response.response[i].ProductId;
                                objResponse.Recalled = Service_response.response[i].Recalled;
                                objResponse.Price = Service_response.response[i].Price;
                                objResponse.Price_string = "$ " + Service_response.response[i].Price;
                                objResponse.recallId = Service_response.response[i].recallId;

                                objResponse.CategoryId= Service_response.response[i].CategoryId;
                                objResponse.CompanyImg = Service_response.response[i].CompanyImg;
                                objResponse.CompanyName = Service_response.response[i].CompanyName;
                                objResponse.CreatedDate = Service_response.response[i].CreatedDate;
                                objResponse.Engine = Service_response.response[i].Engine;
                                objResponse.Manufacturer = Service_response.response[i].Manufacturer;
                                objResponse.ManufacturerId = Service_response.response[i].ManufacturerId;
                                objResponse.ProductDesc = Service_response.response[i].ProductDesc;
                                objResponse.RecallDate = Service_response.response[i].RecallDate;
                                objResponse.recallNo = Service_response.response[i].recallNo;
                                objResponse.SearchCount = Service_response.response[i].SearchCount;
                                objResponse.SubscriptionCategory = Service_response.response[i].SubscriptionCategory;
                                objResponse.Title = Service_response.response[i].Title;
                                objResponse.Type = Service_response.response[i].Type;
                                objResponse.UPCS = Service_response.response[i].UPCS;
                                objResponse.UserId = Service_response.response[i].UserId;
                                objResponse.Vehicle = Service_response.response[i].Vehicle;
                                objResponse.VINNo = Service_response.response[i].VINNo;
                                objResponse.Year = Service_response.response[i].Year;
                                objLstResponse.Add(objResponse);
                            }
                            lstProducts.ItemsSource = objLstResponse;
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
                NoRecordFoundVisibility(true);
            }
        }

        private async Task PostService_deleteUserProducts(Response item)
        {
            try
            {
                List<GetdeleteUserProducts> objLstPostdata = new List<GetdeleteUserProducts>();
                GetdeleteUserProducts objPostdata = new GetdeleteUserProducts();
                objPostdata.CategoryId = Convert.ToInt32(item.CategoryId);
                objPostdata.CompanyImg = Conversion.NullToString(item.CompanyImg);
                objPostdata.CompanyName = Conversion.NullToString(item.CompanyName);
                objPostdata.CreatedDate = Conversion.NullToString(item.CreatedDate);
                objPostdata.Engine = Convert.ToString(item.Engine);
                objPostdata.Manufacturer = Convert.ToString(item.Manufacturer);
                objPostdata.ManufacturerId = Convert.ToInt32(item.ManufacturerId);
                objPostdata.Price = Conversion.NullToZero(item.Price);
                objPostdata.ProductDesc = Conversion.NullToString(item.ProductDesc);
                objPostdata.ProductId = Convert.ToInt32(item.ProductId);
                objPostdata.ProductImg = Conversion.NullToString(item.ProductImg);
                objPostdata.ProductName = Conversion.NullToString(item.ProductName);
                objPostdata.RecallDate = Conversion.NullToString(item.RecallDate);
                objPostdata.Recalled = item.Recalled;
                objPostdata.recallId = Conversion.NullToZero(item.recallId);
                objPostdata.recallNo = Conversion.NullToString(item.recallNo);
                objPostdata.SearchCount = Conversion.NullToString(item.SearchCount);
                objPostdata.SubscriptionCategory = Conversion.NullToString(item.SubscriptionCategory);
                objPostdata.Title = Conversion.NullToString(item.Title);
                objPostdata.Type = Conversion.NullToString(item.Type);
                objPostdata.UPCS = Conversion.NullToString(item.UPCS);
                objPostdata.UserId = Conversion.NullToZero(item.UserId);
                objPostdata.Vehicle = Conversion.NullToString(item.Vehicle);
                objPostdata.VINNo = Conversion.NullToString(item.VINNo);
                objPostdata.Year = Conversion.NullToString(item.Year);
                objLstPostdata.Add(objPostdata);
                string jsonContents = JsonConvert.SerializeObject(objLstPostdata);
                var Service_response = await GetResponseFromWebService.GetResponsePostData<ServiceLayer.ServiceModel.RootObject>(ServiceURLs.deleteUserProducts, jsonContents);
                if (Service_response != null)
                {
                    if (Service_response.status && Service_response.statusCode==200)
                    {
                        HelperToast.LongMessage("Product deleted successfully.");
                        await GetService_GetListOfImportedProductsByCategory();
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

        private async void LstPulled(object sender, EventArgs e)
        {
            lstProducts.IsPullToRefreshEnabled = false;
            objLstResponse = null;
            if (CheckInternetConnection.IsInternetConnected())
            {
                LoadingIndicator.IsVisible = true;
                await GetService_GetListOfImportedProductsByCategory();
                LoadingIndicator.IsVisible = false;
            }
            else
            {
                new ShowUserDialog(UserDialogs.Instance, "Internet Connection Required", System.Drawing.Color.Red, System.Drawing.Color.White);
            }
            lstProducts.IsRefreshing = false;
            lstProducts.IsPullToRefreshEnabled = true;
        }

        private void stkContactSupplier_Tapped(object sender, EventArgs e)
        {
            var obj = sender as StackLayout;
            var recallId = obj.ClassId;
            if (!string.IsNullOrEmpty(recallId))
            {
                var item = objLstResponse.FirstOrDefault(s => s.recallId == Convert.ToInt32(recallId));
                var stack = Navigation.NavigationStack;
                if (stack[stack.Count - 1].GetType() != typeof(NewClaim))
                {
                    Navigation.PushAsync(new NewClaim(null,item));
                }
            }
        }

        private async void stkDelete_Tapped(object sender, EventArgs e)
        {
            try
            {
                var obj = sender as StackLayout;
                var recallId = obj.ClassId;
                if (!string.IsNullOrEmpty(recallId) && objLstResponse != null && objLstResponse.Count > 0)
                {
                    var item = objLstResponse.FirstOrDefault(s => s.recallId == Convert.ToInt32(recallId));
                    
                    if (CheckInternetConnection.IsInternetConnected())
                    {
                        if (item != null)
                        {
                            var answer = await DisplayAlert("Delete", "Are you sure?", "Yes", "No");
                            if (answer)
                            {
                                LoadingIndicator.IsVisible = true;
                                await PostService_deleteUserProducts(item);
                                LoadingIndicator.IsVisible = false;
                            }
                        }
                    }
                    else
                    {
                        new ShowUserDialog(UserDialogs.Instance, "Internet Connection Required", System.Drawing.Color.Red, System.Drawing.Color.White);
                    }
                }
            }
            catch (Exception ex)
            {
                new ShowUserDialog(UserDialogs.Instance, ex.Message, System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }

        private void grdItem_Tapped(object sender, EventArgs e)
        {
            var obj = sender as Grid;
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