using Acr.UserDialogs;
using ERecall.CommonClasses;
using ERecall.Controls;
using ERecall.ServiceLayer;
using ERecall.ServiceLayer.ServiceModel;

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
    public partial class ResetPassword : ContentPage
    {
        public ResetPassword()
        {
            InitializeComponent();
            LoadingIndicator.IsVisible = false;
        }

        private async Task GetService_ResetPassword()
        {
            try
            {
                var url = ServiceURLs.ResetPassword + "email=" + App.User_UserName + "&password=" + txtNewPassword.Text + "";
                var Service_response = await GetResponseFromWebService.GetResponse<RootObject>(url);
                if (Service_response != null)
                {
                    if (Service_response.status)
                    {
                        if(Service_response.statusCode == 200)
                        {
                            HelperToast.LongMessage("Password reset successfully");
                            Application.Current.Properties.Clear();
                            Application.Current.MainPage = new SignIn_SignUp();
                        }
                    }
                    else
                    {
                        if (Service_response.statusCode == 203)
                        {
                            new ShowUserDialog(UserDialogs.Instance, "Email address doesn't exist.", System.Drawing.Color.Red, System.Drawing.Color.White);
                        }
                        else if(Service_response.statusCode == 417)
                        {
                            new ShowUserDialog(UserDialogs.Instance, "WebServer Exception", System.Drawing.Color.Red, System.Drawing.Color.White);
                        }
                        else
                        {
                            new ShowUserDialog(UserDialogs.Instance, Service_response.response, System.Drawing.Color.Red, System.Drawing.Color.White);
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

        private void Loading(bool flag)
        {
            LoadingIndicator.IsVisible = flag;
            txtOldPassword.IsEnabled = !flag;
            txtNewPassword.IsEnabled = !flag;
            txtConfirmPassword.IsEnabled = !flag;
            btnResetPassword.IsEnabled = !flag;
        }

        private async void btnResetPassword_Clicked(object sender, EventArgs e)
        {
            if (CheckInternetConnection.IsInternetConnected())
            {
                if (string.IsNullOrEmpty(txtOldPassword.Text))
                {
                    new ShowUserDialog(UserDialogs.Instance, "Old Password is required", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
                else if (string.IsNullOrEmpty(txtNewPassword.Text))
                {
                    new ShowUserDialog(UserDialogs.Instance, "Please Enter New Password", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
                else if (!Validation.IsPassAlphaNumaric(txtNewPassword.Text))
                {
                    new ShowUserDialog(UserDialogs.Instance, "Password must be alphanumeric", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
                else if (!string.Equals(txtNewPassword.Text, Conversion.NullToString(txtConfirmPassword.Text)))
                {
                    new ShowUserDialog(UserDialogs.Instance, "Confirm password doesn't match", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
                else
                {
                    Loading(true);
                    await GetService_ResetPassword();
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