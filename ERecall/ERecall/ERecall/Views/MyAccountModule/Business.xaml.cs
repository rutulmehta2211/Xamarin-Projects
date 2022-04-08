using Acr.UserDialogs;
using ERecall.CommonClasses;
using ERecall.Controls;
using ERecall.ServiceLayer;
using ERecall.ServiceLayer.ServiceModel;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ERecall.Views.MyAccountModule
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Business : ContentPage
    {
        PostUpdateProfile.UpdateProfile objForUpdateProfile;
        public Business()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (CheckInternetConnection.IsInternetConnected())
            {
                MaingrdAlert.IsVisible = false;
                Loading(true);
                await GetService_GetAccountProfileByUserId();
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
            txtCompanyName.IsEnabled = !flag;
            txtCompanyAddress.IsEnabled = !flag;
            txtPrimaryName.IsEnabled = !flag;
            txtPrimaryEmail.IsEnabled = !flag;
            txtSecondaryName.IsEnabled = !flag;
            txtSecondaryEmail.IsEnabled = !flag;
            btnUpdate.IsEnabled = !flag;
        }

        private async Task GetService_GetAccountProfileByUserId()
        {
            try
            {
                var Service_response = await GetResponseFromWebService.GetResponse<GetAccountProfileByUserId.RootObject>(ServiceURLs.GetAccountProfileByUserId + App.User_UserId);
                if (Service_response != null)
                {
                    if (Service_response.status)
                    {
                        if (Service_response.response != null && Service_response.response.Count > 0)
                        {
                            objForUpdateProfile = new PostUpdateProfile.UpdateProfile();
                            objForUpdateProfile.UserId = Service_response.response[0].UserId;
                            objForUpdateProfile.Name = Service_response.response[0].Name;
                            objForUpdateProfile.Email = Service_response.response[0].Email;
                            objForUpdateProfile.ZipCode = Service_response.response[0].ZipCode;
                            objForUpdateProfile.CountryId = Service_response.response[0].CountryId;
                            objForUpdateProfile.CompanyName = txtCompanyName.Text = Convert.ToString(Service_response.response[0].CompanyName);
                            objForUpdateProfile.CompanyAddress = txtCompanyAddress.Text = Convert.ToString(Service_response.response[0].CompanyAddress);
                            objForUpdateProfile.PrimaryContactName = txtPrimaryName.Text = Convert.ToString(Service_response.response[0].PrimaryContactName);
                            objForUpdateProfile.PrimaryContactEmail = txtPrimaryEmail.Text = Convert.ToString(Service_response.response[0].PrimaryContactEmail);
                            objForUpdateProfile.SecondaryContactName = txtSecondaryName.Text = Convert.ToString(Service_response.response[0].SecondaryContactName);
                            objForUpdateProfile.SecondaryContactEmail = txtSecondaryEmail.Text = Convert.ToString(Service_response.response[0].SecondaryContactEmail);
                            objForUpdateProfile.Notes = Service_response.response[0].Notes;
                            objForUpdateProfile.ProfileImage = Service_response.response[0].ProfileImage;
                        }
                    }
                    else
                    {
                        new ShowUserDialog(UserDialogs.Instance, "WebServer Exception", System.Drawing.Color.Red, System.Drawing.Color.White);
                    }
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
                if (objForUpdateProfile != null)
                {
                    objForUpdateProfile.CompanyName = txtCompanyName.Text;
                    objForUpdateProfile.CompanyAddress = txtCompanyAddress.Text;
                    objForUpdateProfile.PrimaryContactName = txtPrimaryName.Text;
                    objForUpdateProfile.PrimaryContactEmail = txtPrimaryEmail.Text;
                    objForUpdateProfile.SecondaryContactName = txtSecondaryName.Text;
                    objForUpdateProfile.SecondaryContactEmail = txtSecondaryEmail.Text;
                    string jsonContents = JsonConvert.SerializeObject(objForUpdateProfile);
                    var Service_response = await GetResponseFromWebService.GetResponsePostData<PostUpdateProfile.RootObject>(ServiceURLs.PostUpdateProfile, jsonContents);
                    if (Service_response != null)
                    {
                        HelperToast.LongMessage("Business profile updated successfully");
                    }
                    else
                    {
                        new ShowUserDialog(UserDialogs.Instance, "WebServer not Responding", System.Drawing.Color.Red, System.Drawing.Color.White);
                    }
                }
            }
            catch (Exception ex)
            {
                new ShowUserDialog(UserDialogs.Instance, ex.Message, System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }

        private async void btnUpdate_Clicked(object sender, EventArgs e)
        {
            if (CheckInternetConnection.IsInternetConnected())
            {
                if (!string.IsNullOrEmpty(txtPrimaryEmail.Text) && !Validation.IsEmail(txtPrimaryEmail.Text))
                {
                    new ShowUserDialog(UserDialogs.Instance, "Please Enter Valid Primary Email", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
                else if (!string.IsNullOrEmpty(txtSecondaryEmail.Text) && !Validation.IsEmail(txtSecondaryEmail.Text))
                {
                    new ShowUserDialog(UserDialogs.Instance, "Please Enter Valid Secondary Email", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
                else
                {
                    Loading(true);
                    await PostService_AddUsermobileSetting();
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