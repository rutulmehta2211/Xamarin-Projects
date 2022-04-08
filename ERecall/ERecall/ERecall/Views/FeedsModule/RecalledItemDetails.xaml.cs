using ERecall.ServiceLayer;
using ERecall.ServiceLayer.ServiceModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading.Tasks;
using FFImageLoading.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ERecall.CommonClasses;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using ERecall.Controls;
using Acr.UserDialogs;

namespace ERecall.Views.FeedsModule
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecalledItemDetails : ContentPage
    {
        List<GetRecalledItemDetails_Feeds.RecalledItemImage> objLstRecalledItemImage;
        GetRecalledItemDetails_Feeds.RecalledItemImage objRecalledItemImage;

        string productId;
        string price = string.Empty;
        string productName = string.Empty;
        string recallNotice = string.Empty;

        public RecalledItemDetails(string productId)
        {
            InitializeComponent();
            this.productId = productId;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (CheckInternetConnection.IsInternetConnected())
            {
                Loading(true);
                await GetService_GetRecalledItemDetails(productId);
                await GetService_GetReleventRecall(productId);
                Loading(false);
                BindingContext = this;

                if (objLstRecalledItemImage != null && objLstRecalledItemImage.Count > 1)
                {
                    Device.StartTimer(TimeSpan.FromSeconds(4), () =>
                    {
                        if (Position == objLstRecalledItemImage.Count - 1)
                        {
                            Position = 0;
                        }
                        else
                        {
                            Position++;
                        }
                        return true;
                    });
                }
            }
            else
            {
                new ShowUserDialog(UserDialogs.Instance, "Internet Connection Required", System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            stkReleventRecalls.Children.Clear();
        }

        private void Loading(bool flag)
        {
            MainScrlForms.IsVisible = !flag;
            LoadingIndicator.IsVisible = flag;
        }

        private async Task GetService_GetRecalledItemDetails(string ProductId)
        {
            try
            {
                string url = ServiceURLs.GetRecalledItemDetails + "recallId=" + ProductId;
                var Service_response = await GetResponseFromWebService.GetResponse<GetRecalledItemDetails_Feeds.RootObject>(url);
                if (Service_response != null)
                {
                    if (Service_response.status)
                    {
                        if (Service_response.response != null && Service_response.response.Count > 0)
                        {
                            lblManufecturer.Text = string.IsNullOrEmpty(Service_response.response[0].ManufacturerName) ? " - " : Service_response.response[0].ManufacturerName;
                            lblProductName.Text = string.IsNullOrEmpty(Service_response.response[0].ProductName) ? " - " : Service_response.response[0].ProductName;
                            productName = string.IsNullOrEmpty(Service_response.response[0].ProductName) ? string.Empty : Service_response.response[0].ProductName;
                            lblProductPrice.Text = string.IsNullOrEmpty(Convert.ToString(Service_response.response[0].ProductPrice)) ? "0" : Convert.ToString(Service_response.response[0].ProductPrice);
                            price = Convert.ToString(Service_response.response[0].ProductPrice);
                            lblCompany.Text = string.IsNullOrEmpty(Service_response.response[0].Company) ? " - " : Service_response.response[0].Company;
                            lblRecallDate.Text = string.IsNullOrEmpty(Service_response.response[0].RecallDate) ? " - " : Conversion.ConvertedDate2(Service_response.response[0].RecallDate);
                            lblAffectedUnits.Text = string.IsNullOrEmpty(Convert.ToString(Service_response.response[0].AffectedUnits)) ? " - " : Convert.ToString(Service_response.response[0].AffectedUnits);
                            lblMadeIn.Text = string.IsNullOrEmpty(Service_response.response[0].MadeIn) ? " - " : Service_response.response[0].MadeIn;
                            lblOrganization.Text = string.IsNullOrEmpty(Service_response.response[0].Organization) ? " - " : Service_response.response[0].Organization;
                            lblRecallNo.Text = string.IsNullOrEmpty(Service_response.response[0].RecallNo) ? " - " : Service_response.response[0].RecallNo;
                            lblCategory.Text = string.IsNullOrEmpty(Service_response.response[0].Category) ? " - " : Service_response.response[0].Category;
                            lblCountry.Text = string.IsNullOrEmpty(Service_response.response[0].Country) ? " - " : Service_response.response[0].Country;
                            lblRecallClass.Text = string.IsNullOrEmpty(Convert.ToString(Service_response.response[0].RecallClass)) ? " - " : Convert.ToString(Service_response.response[0].RecallClass);
                            lblHealthRisk.Text = string.IsNullOrEmpty(Convert.ToString(Service_response.response[0].HealthRisk)) ? " - " : Convert.ToString(Service_response.response[0].HealthRisk);
                            lblProductDesc.Text = string.IsNullOrEmpty(Service_response.response[0].ProductDesc) ? " - " : Service_response.response[0].ProductDesc;
                            lblDefectSummary.Text = string.IsNullOrEmpty(Service_response.response[0].DefectSummary) ? " - " : Service_response.response[0].DefectSummary;
                            lblProductSource.Text = string.IsNullOrEmpty(Service_response.response[0].ProductSource) ? " - " : Service_response.response[0].ProductSource;
                            lblRemedyType.Text = string.IsNullOrEmpty(Service_response.response[0].RemedyType) ? " - " : Service_response.response[0].RemedyType;
                            recallNotice = Service_response.response[0].Content;

                            GrdOrgRecallNo.IsVisible = string.IsNullOrEmpty(Service_response.response[0].Organization) || string.IsNullOrEmpty(Service_response.response[0].RecallNo) ? false : true;
                            GrdCatCountry.IsVisible = string.IsNullOrEmpty(Service_response.response[0].Category) || string.IsNullOrEmpty(Service_response.response[0].Country) ? false : true;
                            GrdClassesRisk.IsVisible = string.IsNullOrEmpty(Convert.ToString(Service_response.response[0].RecallClass)) || string.IsNullOrEmpty(Convert.ToString(Service_response.response[0].RecallClass)) ? false : true;
                            GrdSourceRemedy.IsVisible = string.IsNullOrEmpty(Service_response.response[0].ProductSource) || string.IsNullOrEmpty(Service_response.response[0].RemedyType) ? false : true;
                            grdDefectSummary.IsVisible = string.IsNullOrEmpty(Service_response.response[0].DefectSummary) || string.IsNullOrEmpty(Service_response.response[0].DefectSummary) ? false : true;
                            lblReadMore.IsVisible = string.IsNullOrEmpty(Service_response.response[0].Content) ? false : true;

                            if (Service_response.response[0].RecalledItemImages != null && Service_response.response[0].RecalledItemImages.Count > 0)
                            {
                                lblForNoImages.IsVisible = false;
                                objLstRecalledItemImage = new List<GetRecalledItemDetails_Feeds.RecalledItemImage>();
                                for (int i = 0; i < Service_response.response[0].RecalledItemImages.Count; i++)
                                {
                                    objRecalledItemImage = new GetRecalledItemDetails_Feeds.RecalledItemImage();
                                    objRecalledItemImage.ImageNo = Service_response.response[0].RecalledItemImages[i].ImageNo;
                                    objRecalledItemImage.ImageUrl = Service_response.response[0].RecalledItemImages[i].ImageUrl;
                                    objLstRecalledItemImage.Add(objRecalledItemImage);
                                }
                            }
                            else
                            {
                                lblForNoImages.IsVisible = true;
                            }
                            CarouselView.ItemsSource = objLstRecalledItemImage;
                            CarouselIndicators.ItemsSource = objLstRecalledItemImage;
                        }
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
            catch (WebException ex)
            {
                new ShowUserDialog(UserDialogs.Instance, ex.Message, System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }

        private async Task GetService_GetReleventRecall(string recallId)
        {
            try
            {
                string url = ServiceURLs.GetReleventRecall + "recallId=" + recallId;
                var Service_response = await GetResponseFromWebService.GetResponse<GetReleventRecall.RootObject>(url);
                if (Service_response != null)
                {
                    if (Service_response.status)
                    {
                        if (Service_response.response != null && Service_response.response.Count > 0)
                        {
                            for (int i = 0; i < Service_response.response.Count; i++)
                            {
                                var lblStyle = new Style(typeof(Label))
                                {
                                    BaseResourceKey = "LabelDescStyle"
                                };
                                var lbldesh = new Label()
                                {
                                    Text = "-",
                                    FontAttributes = FontAttributes.Bold
                                };
                                var lblReleventRecall = new Label()
                                {
                                    Text = Service_response.response[i].ProductName,
                                    Style = lblStyle
                                };
                                var grid = new Grid();
                                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                grid.Children.Add(lbldesh, 0, 0);
                                grid.Children.Add(lblReleventRecall, 1, 0);
                                stkReleventRecalls.Children.Add(grid);
                            }
                        }
                    }
                    else
                    {
                        boxBeforReleventRecall.IsVisible = false;
                        stkMainReleventRecalls.IsVisible = false;
                    }
                }
                else
                {
                    boxBeforReleventRecall.IsVisible = false;
                    stkMainReleventRecalls.IsVisible = false;
                }
            }
            catch (WebException ex)
            {
                boxBeforReleventRecall.IsVisible = false;
                stkMainReleventRecalls.IsVisible = false;
            }
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
                        HelperToast.LongMessage("This product has been added sucessfully to the products center");
                    }
                    else
                    {
                        if (Service_response.statusCode == 409)
                        {
                            new ShowUserDialog(UserDialogs.Instance, "This product already exists in product center", System.Drawing.Color.Red, System.Drawing.Color.White);
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
                    Alert("Alert", "WebServer not Responding", "error.png", Color.Red);
                }
            }
            catch (Exception ex)
            {
                Alert("Exception", ex.Message, "error.png", Color.Red);
            }
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
                            new ShowUserDialog(UserDialogs.Instance, "This product already exists Favourite", System.Drawing.Color.Red, System.Drawing.Color.White);
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
                new ShowUserDialog(UserDialogs.Instance, "This product already exists in Favorites", System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }
        
        private async void btnIOwnIt_Clicked(object sender, EventArgs e)
        {
            if (CheckInternetConnection.IsInternetConnected())
            {
                LoadingIndicator.IsVisible = true;
                await PostService_IOwnThisProduct(Convert.ToInt32(productId), price);
                LoadingIndicator.IsVisible = false;
            }
            else
            {
                new ShowUserDialog(UserDialogs.Instance, "Internet Connection Required", System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }

        private async void btnAddToMyeRecall_Clicked(object sender, EventArgs e)
        {
            if (CheckInternetConnection.IsInternetConnected())
            {
                LoadingIndicator.IsVisible = true;
                await PostService_AddToMyeRecall(Convert.ToInt32(productId), price);
                LoadingIndicator.IsVisible = false;
            }
            else
            {
                new ShowUserDialog(UserDialogs.Instance, "Internet Connection Required", System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }
        
        private int _position;
        public int Position { get { return _position; } set { _position = value; OnPropertyChanged(); } }

        private void ProductDecription_Tapped(object sender, EventArgs e)
        {
            var img = sender as CachedImage;
            var IsOpen = img.ClassId;
            if (IsOpen == "false")
            {
                img.ClassId = "true";
                lblProductDesc.IsVisible = true;
                imgForProduceDesc.Source = "icon_DownArrow.png";
            }
            else if (IsOpen == "true")
            {
                img.ClassId = "false";
                lblProductDesc.IsVisible = false;
                imgForProduceDesc.Source = "icon_UpArrow.png";
            }
        }

        private void imgForShare_Tapped(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(productName))
            //{
            //    Sharing.Share("eRecall", productName, null);
            //}
            Sharing.Share("eRecall", "I am sharing this" +productName+ "with you. So, Be careful", null);
        }

        private void lblReadMore_Tapped(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(recallNotice))
            {
                var stack = Navigation.NavigationStack;
                if (stack[stack.Count - 1].GetType() != typeof(ERecallNotice))
                {
                    Navigation.PushAsync(new ERecallNotice(recallNotice));
                }
            }
        }

        private void DefectSummary_Tapped(object sender, EventArgs e)
        {
            var img = sender as CachedImage;
            var IsOpen = img.ClassId;
            if (IsOpen == "false")
            {
                img.ClassId = "true";
                lblDefectSummary.IsVisible = true;
                imgForDefectSummary.Source = "icon_DownArrow.png";
            }
            else if (IsOpen == "true")
            {
                img.ClassId = "false";
                lblDefectSummary.IsVisible = false;
                imgForDefectSummary.Source = "icon_UpArrow.png";
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
        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            MaingrdAlert.IsVisible = false;
            MainScrlForms.IsVisible = true;
        }

    }
}
