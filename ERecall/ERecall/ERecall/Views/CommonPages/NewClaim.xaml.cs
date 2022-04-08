using Acr.UserDialogs;
using ERecall.CommonClasses;
using ERecall.Controls;
using ERecall.ServiceLayer;
using ERecall.ServiceLayer.ServiceModel;
using ERecall.Views.Menu;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ERecall.Views.CommonPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewClaim : ContentPage
    {
        Dictionary<int, string> dicSuppliers;
        int RecallId;
        string RecallNo = string.Empty;
        string ProductName = string.Empty;
        string CategoryId = string.Empty;

        public NewClaim(GeteRecallSearch_MyERecall.Response ItemFromMyFavourite, GetListOfImportedProductsByCategory.Response ItemFromProductCenter)
        {
            InitializeComponent();
            if(ItemFromMyFavourite==null && ItemFromProductCenter!=null)
            {
                this.RecallId = ItemFromProductCenter.recallId;
                lblRecallID.Text = this.RecallNo = ItemFromProductCenter.recallNo;
                this.ProductName = ItemFromProductCenter.ProductName;
                this.CategoryId = ItemFromProductCenter.CategoryId;
            }
            else if(ItemFromMyFavourite != null && ItemFromProductCenter == null)
            {
                this.RecallId = ItemFromMyFavourite.RecallId;
                lblRecallID.Text = this.RecallNo = ItemFromMyFavourite.RecallNo;
                this.ProductName = ItemFromMyFavourite.ProductName;
                this.CategoryId = Convert.ToString(ItemFromMyFavourite.CategoryId);
            }
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (CheckInternetConnection.IsInternetConnected())
            {
                Loading(true);
                await GetService_GetSupplierList();
                Loading(false);
            }
            else
            {
                new ShowUserDialog(UserDialogs.Instance, "Internet Connection Required", System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }

        private void Loading(bool flag)
        {
            LoadingIndicator.IsVisible = flag;
            txtClaimTitle.IsEnabled = !flag;
            txtClaimDesc.IsEnabled = !flag;
            btnSave.IsEnabled = !flag;
        }

        private async Task GetService_GetSupplierList()
        {
            try
            {
                var Service_response = await GetResponseFromWebService.GetResponse<GetSupplierList.RootObject>(ServiceURLs.GetSupplierList + "recallId="+ RecallId);
                if (Service_response != null)
                {
                    if (Service_response.status)
                    {
                        dicSuppliers = new Dictionary<int, string>();
                        for (int i = 0; i < Service_response.response.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(Service_response.response[i].ManufacturerName))
                            {
                                dicSuppliers.Add(Service_response.response[i].ManufacturerId, Service_response.response[i].ManufacturerName);
                            }
                        }
                        if (dicSuppliers.Count > 0)
                        {
                            pkrSupplierName.IsEnabled = true;
                            pkrSupplierName.ItemsSource = dicSuppliers.Values.ToList();
                        }
                        else
                        {
                            pkrSupplierName.IsEnabled = false;
                        }
                    }
                    else
                    {
                        pkrSupplierName.IsEnabled = false;
                        if (Service_response.statusCode == 417)
                        {
                            new ShowUserDialog(UserDialogs.Instance, "WebServer Exception", System.Drawing.Color.Red, System.Drawing.Color.White);
                        }
                    }
                }
                else
                {
                    pkrSupplierName.IsEnabled = false;
                    new ShowUserDialog(UserDialogs.Instance, "WebServer not Responding", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
            }
            catch (WebException ex)
            {
                pkrSupplierName.IsEnabled = false;
                new ShowUserDialog(UserDialogs.Instance, ex.Message, System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }

        private async Task PostService_PostUserClaim()
        {
            try
            {
                PostUserClaim.UserClaim objPostUserClaim = new PostUserClaim.UserClaim();
                objPostUserClaim.UserId = Convert.ToInt32(App.User_UserId);
                objPostUserClaim.recallId = RecallId;
                if (dicSuppliers != null && pkrSupplierName.Items.Count>0)
                {
                    objPostUserClaim.ManufacturerId = dicSuppliers.FirstOrDefault(s => s.Value == pkrSupplierName.Items[pkrSupplierName.SelectedIndex]).Key;
                    objPostUserClaim.ManufacturerName = dicSuppliers.FirstOrDefault(s => s.Value == pkrSupplierName.Items[pkrSupplierName.SelectedIndex]).Value;
                    objPostUserClaim.SupplierAccountId = dicSuppliers.FirstOrDefault(s => s.Value == pkrSupplierName.Items[pkrSupplierName.SelectedIndex]).Key;
                }
                objPostUserClaim.ClaimTitle = txtClaimTitle.Text.Trim();
                objPostUserClaim.ClaimDetail = txtClaimDesc.Text.Trim();
                objPostUserClaim.ClaimStatus = 1;
                objPostUserClaim.recallNo = RecallNo;
                objPostUserClaim.CategoryId = Convert.ToInt32(CategoryId);
                objPostUserClaim.ProductName = ProductName;
                string jsonContents = JsonConvert.SerializeObject(objPostUserClaim);
                var Service_response = await GetResponseFromWebService.GetResponsePostData<PostUserClaim.RootObject>(ServiceURLs.PostUserClaim, jsonContents);
                if (Service_response != null)
                {
                    if (Service_response.status && Service_response.statusCode == 200)
                    {
                        HelperToast.LongMessage("Claim is submitted sucessfully. Kindly see the resolution center.");
                        Application.Current.MainPage = new SideMenu();
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

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            if (CheckInternetConnection.IsInternetConnected())
            {
                if (string.IsNullOrEmpty(txtClaimTitle.Text))
                {
                    new ShowUserDialog(UserDialogs.Instance, "Claim Title is required", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
                else
                {
                    Loading(true);
                    await PostService_PostUserClaim();
                    Loading(false);
                }
            }
            else
            {
                new ShowUserDialog(UserDialogs.Instance, "Internet Connection Required", System.Drawing.Color.Red, System.Drawing.Color.White);
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