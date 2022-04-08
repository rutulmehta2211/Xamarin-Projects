using Acr.UserDialogs;
using ERecall.CommonClasses;
using ERecall.Controls;
using ERecall.ServiceLayer;
using ERecall.ServiceLayer.ServiceModel;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static ERecall.ServiceLayer.ServiceModel.GetPopularProducts_Feeds;

namespace ERecall.Views.FeedsModule
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopularProducts : ContentPage
    {
        int index = 0;
        ObservableCollection<Response> objLstResponse;

        public PopularProducts()
        {
            InitializeComponent();
            lstPopularProducts.ItemSelected += (sender, e) =>
            {
                ((ListView)sender).SelectedItem = null;
            };
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            LatestProducts.IsLoadBefore = false;
            index = 0;
            lblNoRecordFound.IsVisible = false;
            objLstResponse = null;
            if (CheckInternetConnection.IsInternetConnected())
            {
                LoadingIndicator.IsVisible = true;
                await GetService_GetPopularProducts();
                LoadingIndicator.IsVisible = false;
            }
            else
            {
                new ShowUserDialog(UserDialogs.Instance, "Internet Connection Required", System.Drawing.Color.Red, System.Drawing.Color.White);
                NoRecordFoundVisibility(true);
            }

        }

        private void NoRecordFoundVisibility(bool IsVisible)
        {
            lstPopularProducts.IsVisible = !IsVisible;
            lblNoRecordFound.IsVisible = IsVisible;
        }

        private async Task GetService_GetPopularProducts()
        {
            try
            {
                string url = ServiceURLs.GetPopularProducts + "PageIndex=" + index + "&PageSize=5";
                var Service_response = await GetResponseFromWebService.GetResponse<GetPopularProducts_Feeds.RootObject>(url);
                if (Service_response != null)
                {
                    if (Service_response.status)
                    {
                        if (Service_response.response != null && Service_response.response.Count > 0)
                        {
                            NoRecordFoundVisibility(false);
                            if (objLstResponse == null)
                            {
                                objLstResponse = new ObservableCollection<Response>();
                            }
                            for (int i = 0; i < Service_response.response.Count; i++)
                            {
                                Response objResponse = new Response();
                                objResponse.AffectedUnits = Service_response.response[i].AffectedUnits;
                                objResponse.CategoryIconImg = Service_response.response[i].CategoryIconImg;
                                objResponse.CategoryId = Service_response.response[i].CategoryId;
                                objResponse.CategoryImg = Service_response.response[i].CategoryImg;
                                objResponse.CompanyImg = Service_response.response[i].CompanyImg;
                                objResponse.CompanyName = Service_response.response[i].CompanyName;
                                objResponse.CreatedDate = Conversion.ConvertedDate(Service_response.response[i].CreatedDate);
                                objResponse.Engine = Service_response.response[i].Engine;
                                objResponse.IsAdd = Service_response.response[i].IsAdd;
                                objResponse.MakeId = Service_response.response[i].MakeId;
                                objResponse.MessageCount = Service_response.response[i].MessageCount;
                                objResponse.ModelId = Service_response.response[i].ModelId;
                                objResponse.ModelNo = Service_response.response[i].ModelNo;
                                objResponse.OrganizationId = Service_response.response[i].OrganizationId;
                                objResponse.Price = Service_response.response[i].Price;
                                objResponse.PriceTo = Service_response.response[i].PriceTo;
                                objResponse.ProductBrand = Service_response.response[i].ProductBrand;
                                objResponse.ProductDesc = Service_response.response[i].ProductDesc;
                                objResponse.ProductId = Service_response.response[i].ProductId;
                                objResponse.ProductImg = Service_response.response[i].ProductImg;
                                objResponse.ProductName = Service_response.response[i].ProductName;
                                objResponse.ProductStatus = Service_response.response[i].ProductStatus;
                                objResponse.ProductTotal = Service_response.response[i].ProductTotal;
                                objResponse.RecallDate = Service_response.response[i].RecallDate;
                                objResponse.Recalled = Service_response.response[i].Recalled;
                                objResponse.RemedyId = Service_response.response[i].RemedyId;
                                objResponse.SampIconImg = Service_response.response[i].SampIconImg;
                                objResponse.SearchCount = Service_response.response[i].SearchCount;
                                objResponse.Source = Service_response.response[i].Source;
                                objResponse.SubscriptionCategory = Service_response.response[i].SubscriptionCategory;
                                objResponse.SubscriptionCategoryId = Service_response.response[i].SubscriptionCategoryId;
                                objResponse.UPCS = Service_response.response[i].UPCS;
                                objResponse.Vehicle = Service_response.response[i].Vehicle;
                                objResponse.VINNo = Service_response.response[i].VINNo;
                                objResponse.Year = Service_response.response[i].Year;
                                objResponse.YearId = Service_response.response[i].YearId;
                                objResponse.eRecallIconImage = Service_response.response[i].IsFavorite != null ? Convert.ToBoolean(Service_response.response[i].IsFavorite) ? "erecall_yellow.png" : "erecall.png" : "erecall.png";
                                switch (objResponse.CategoryId)
                                {
                                    case "1":
                                        objResponse.imgCategory = "icon_foods.png";
                                        objResponse.PlaceholderImg = "placeholder_foods.png";
                                        break;
                                    case "2":
                                        objResponse.imgCategory = "icon_drugs.png";
                                        objResponse.PlaceholderImg = "placeholder_drugs.png";
                                        break;
                                    case "3":
                                        objResponse.imgCategory = "icon_medicaldevices.png";
                                        objResponse.PlaceholderImg = "placeholder_drugs.png";
                                        break;
                                    case "4":
                                        objResponse.imgCategory = "icon_vehicle.png";
                                        objResponse.PlaceholderImg = "placeholder_vehicles.png";
                                        break;
                                    case "5":
                                        objResponse.imgCategory = "icon_products.png";
                                        objResponse.PlaceholderImg = "placeholder_products.png";
                                        break;
                                    default:
                                        objResponse.imgCategory = "icon_products.png";
                                        objResponse.PlaceholderImg = "placeholder_products.png";
                                        break;
                                }

                                objLstResponse.Add(objResponse);
                            }
                            lstPopularProducts.ItemsSource = objLstResponse;
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

        private async void LstLoadmore(object sender, ItemVisibilityEventArgs e)
        {
            if (objLstResponse != null && e.Item != null && e.Item == objLstResponse[objLstResponse.Count - 1])
            {
                index++;
                if (CheckInternetConnection.IsInternetConnected())
                {
                    LoadingIndicator.IsVisible = true;
                    await GetService_GetPopularProducts();
                    LoadingIndicator.IsVisible = false;
                }
                else
                {
                    new ShowUserDialog(UserDialogs.Instance, "Internet Connection Required", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
            }
        }

        private async void LstPulled(object sender, EventArgs e)
        {
            lstPopularProducts.IsPullToRefreshEnabled = false;
            index = 0;
            objLstResponse = null;
            if (CheckInternetConnection.IsInternetConnected())
            {
                LoadingIndicator.IsVisible = true;
                await GetService_GetPopularProducts();
                LoadingIndicator.IsVisible = false;
            }
            else
            {
                new ShowUserDialog(UserDialogs.Instance, "Internet Connection Required", System.Drawing.Color.Red, System.Drawing.Color.White);
            }
            lstPopularProducts.IsRefreshing = false;
            lstPopularProducts.IsPullToRefreshEnabled = true;
        }

        private async Task PostService_AddToMyeRecall(int recallId, string price)
        {
            try
            {
                PostMyRecall.PostMyFavRecall objPostAddToMyeRecall = new PostMyRecall.PostMyFavRecall();
                objPostAddToMyeRecall.UserId = Convert.ToString(App.User_Id);
                objPostAddToMyeRecall.RecallId = recallId;
                objPostAddToMyeRecall.Price = price;
                string jsonContents = JsonConvert.SerializeObject(objPostAddToMyeRecall);
                var Service_response = await GetResponseFromWebService.GetResponsePostData<PostMyRecall.RootObject>(ServiceURLs.PostMyRecall, jsonContents);
                if (Service_response != null)
                {
                    if (Service_response.status && Service_response.statusCode == 200)
                    {
                        HelperToast.LongMessage("This product has been added sucessfully to Favorites");
                    }
                    else
                    {
                        if (Service_response.statusCode == 409)
                        {
                            new ShowUserDialog(UserDialogs.Instance, "This product already exists in Favorites", System.Drawing.Color.Red, System.Drawing.Color.White);
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
            catch (HttpRequestException ex)
            {
                new ShowUserDialog(UserDialogs.Instance, "This product already exists in Favorites", System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }

        private async void eRecallIconImage_Tapped(object sender, EventArgs e)
        {
            try
            {
                var selectedData = sender as FFImageLoading.Forms.CachedImage;
                var recallId = selectedData.ClassId;
                string price = null;
                if (objLstResponse != null && objLstResponse.Count > 0)
                {
                    if (!string.IsNullOrEmpty(recallId))
                    {
                        if (CheckInternetConnection.IsInternetConnected())
                        {
                            LoadingIndicator.IsVisible = true;
                            price = Convert.ToString(objLstResponse.FirstOrDefault(d => d.ProductId == recallId).Price);
                            await PostService_AddToMyeRecall(Convert.ToInt32(recallId), price);
                            LoadingIndicator.IsVisible = false;
                        }
                        else
                        {
                            new ShowUserDialog(UserDialogs.Instance, "Internet Connection Required", System.Drawing.Color.Red, System.Drawing.Color.White);
                        }
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
            var selectedData = sender as Grid;
            var ProductId = selectedData.ClassId;
            if (!string.IsNullOrEmpty(ProductId))
            {
                var stack = Navigation.NavigationStack;
                if (stack[stack.Count - 1].GetType() != typeof(RecalledItemDetails))
                {
                    Navigation.PushAsync(new RecalledItemDetails(ProductId));
                }
            }
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            var stack = Navigation.NavigationStack;
            if (stack[stack.Count - 1].GetType() != typeof(SearchingFeedModule))
            {
                Navigation.PushAsync(new SearchingFeedModule());
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