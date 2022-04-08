using Acr.UserDialogs;
using ERecall.CommonClasses;
using ERecall.Controls;
using ERecall.Interfaces;
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

namespace ERecall.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignIn_SignUp : ContentPage
    {
        Dictionary<int, string> dicCountries;
        bool IsimgTermAndCond = false;
        string zipcodeValue = string.Empty;
        List<GetAccountProfileByUserId.Response> objResponse;
        public SignIn_SignUp()
        {
            InitializeComponent();
            ChangeGrdVisibility(true, false);
            MaingrdAlert.IsVisible = false;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (CheckInternetConnection.IsInternetConnected())
            {
                MaingrdAlert.IsVisible = false;
                Loading(true);
                await GetService_Getcountries();
                Loading(false);
            }
            else
            {
                new ShowUserDialog(UserDialogs.Instance, "Internet Connection Required", System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }

        #region Common Functions
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

        private void ChangeGrdVisibility(bool IsSignIn, bool IsSignUp)
        {
            grdSignIn.IsVisible = IsSignIn;
            grdSignUp.IsVisible = IsSignUp;
            stkSignInYellowBorder.IsVisible = IsSignIn;
            stkSignUpYellowBorder.IsVisible = IsSignUp;
            if (IsSignIn == true && IsSignUp == false)
            {
                lblSignIn.TextColor = Color.FromHex("#FCFCFC");
                lblSignup.TextColor = Color.FromHex("#9EB7EA");
            }
            else
            {
                lblSignIn.TextColor = Color.FromHex("#9EB7EA");
                lblSignup.TextColor = Color.FromHex("#FCFCFC");
            }
        }

        private void Loading(bool flag)
        {
            LoadingIndicator.IsVisible = flag;
            LoadingIndicator.LoadingImage = "icon_loading_white.png";
            txtEmail.IsEnabled = !flag;
            txtPassword.IsEnabled = !flag;
            txtAddname.IsEnabled = !flag;
            txtAddemail.IsEnabled = !flag;
            txtAddpassword.IsEnabled = !flag;
            txtAddConfPass.IsEnabled = !flag;
            txtAddZipCode.IsEnabled = !flag;
            pkrAddCountry.IsEnabled = !flag;
            btnSignIn.IsEnabled = !flag;
            btnSignUp.IsEnabled = !flag;
        }
        #endregion

        #region Clicked Events
        private void MaingrdAlert_Tapped(object sender, EventArgs e)
        {
            MaingrdAlert.IsVisible = false;
            MaingrdForms.IsVisible = true;
        }

        private void stksignin_Tapped(object sender, EventArgs e)
        {
            ChangeGrdVisibility(true, false);
        }

        private void stksignup_Tapped(object sender, EventArgs e)
        {
            ChangeGrdVisibility(false, true);
        }

        private void imgTermAndCond_Tapped(object sender, EventArgs e)
        {
            if (!IsimgTermAndCond)
            {
                imgTermAndCond.Source = "icon_checked.png";
                IsimgTermAndCond = true;
            }
            else
            {
                imgTermAndCond.Source = "icon_unchecked.png";
                IsimgTermAndCond = false;
            }
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<IHideKeyboard>().HideKeyboard();
            if (CheckInternetConnection.IsInternetConnected())
            {
                if (string.IsNullOrEmpty(txtEmail.Text))
                {
                    new ShowUserDialog(UserDialogs.Instance, "Please Enter Email", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
                else if (!Validation.IsEmail(txtEmail.Text))
                {
                    new ShowUserDialog(UserDialogs.Instance, "Invalid Email Address", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
                else if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    new ShowUserDialog(UserDialogs.Instance, "Please Enter Password", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
                else
                {
                    Loading(true);
                    await GetService_SignIn();
                    Loading(false);
                }
            }
            else
            {
                new ShowUserDialog(UserDialogs.Instance, "Internet Connection Required", System.Drawing.Color.Red, System.Drawing.Color.White);
            }

        }

        private async void SignUp_Clicked(object sender, EventArgs e)
        {
            if (CheckInternetConnection.IsInternetConnected())
            {
                if (string.IsNullOrEmpty(txtAddname.Text))
                {
                    new ShowUserDialog(UserDialogs.Instance, "Please Enter Name", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
                else if (string.IsNullOrEmpty(txtAddemail.Text))
                {
                    new ShowUserDialog(UserDialogs.Instance, "Please Enter Email", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
                else if (!Validation.IsEmail(txtAddemail.Text))
                {
                    new ShowUserDialog(UserDialogs.Instance, "Please Enter Valid Email", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
                else if (string.IsNullOrEmpty(txtAddpassword.Text))
                {
                    new ShowUserDialog(UserDialogs.Instance, "Please Enter Password", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
                else if (txtAddpassword.Text.Length < 7)
                { 
                    new ShowUserDialog(UserDialogs.Instance, "Please Enter at least 7 alphanumeric character", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
                else if (!Validation.IsPassAlphaNumaric(txtAddpassword.Text))
                {
                    new ShowUserDialog(UserDialogs.Instance, "Please Enter alphanumeric Password", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
                else if(string.IsNullOrEmpty(txtAddConfPass.Text))
                {
                    new ShowUserDialog(UserDialogs.Instance, "Please Enter Confirm Password Field", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
                else if (!string.Equals(txtAddpassword.Text, Conversion.NullToString(txtAddConfPass.Text)))
                {
                    new ShowUserDialog(UserDialogs.Instance, "Confirm password doesn't match", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
                else if (pkrAddCountry.SelectedIndex == 0)
                {
                    new ShowUserDialog(UserDialogs.Instance, "Please Select Country name", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
                else if (!IsimgTermAndCond)
                {
                    new ShowUserDialog(UserDialogs.Instance, "please accept terms of use and privacy policy", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
                else
                {
                    Loading(true);
                    await PostService_SignUp();
                    Loading(false);
                }
            }
            else
            {
                new ShowUserDialog(UserDialogs.Instance, "Internet Connection Required", System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }

        private void txtAddZipCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender != null)
            {
                Entry entry = sender as Entry;
                String val = entry.Text; //Get Current Text

                if (val.Contains("."))
                {
                    val = val.Remove(val.Length - 1);// Remove Last character
                    entry.Text = val;
                }
                if (val.Length >= 8)
                {
                    val = val.Remove(val.Length - 1);// Remove Last character
                    entry.Text = val; //Set the Old value
                }
                zipcodeValue = val;
            }
        }
        #endregion

        #region API Call
        private async Task GetService_Getcountries()
        {
            try
            {
                var Service_response = await GetResponseFromWebService.GetResponse<Getcountries.RootObject>(ServiceURLs.Getcountries);
                if (Service_response != null)
                {
                    if (Service_response.status)
                    {
                        dicCountries = new Dictionary<int, string>();
                        for (int i = 0; i < Service_response.response.Count; i++)
                        {
                            dicCountries.Add(Service_response.response[i].CountryId, Service_response.response[i].CountryName);
                        }
                        pkrAddCountry.ItemsSource = dicCountries.Values.ToList();
                        if (pkrAddCountry.Items != null && pkrAddCountry.Items.Count > 0)
                        {
                            var selectedindex = pkrAddCountry.Items.IndexOf(dicCountries.FirstOrDefault(d => d.Value == "United States").Value);
                            pkrAddCountry.SelectedIndex = selectedindex;
                        }
                    }
                    else
                    {
                        pkrAddCountry.IsEnabled = false;
                        if (Service_response.statusCode == 417)
                        {
                            new ShowUserDialog(UserDialogs.Instance, "WebServer Exception", System.Drawing.Color.Red, System.Drawing.Color.White);
                        }
                    }
                }
                else
                {
                    pkrAddCountry.IsEnabled = false;
                    new ShowUserDialog(UserDialogs.Instance, "WebServer not Responding", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
            }
            catch (WebException ex)
            {
                new ShowUserDialog(UserDialogs.Instance, ex.Message, System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }

        private async Task GetService_SignIn()
        {
            try
            {
                string url = ServiceURLs.SignIn + "email=" + txtEmail.Text + "&password=" + txtPassword.Text;
                var Service_response = await GetResponseFromWebService.GetResponse<SignIn.RootObject>(url);
                if (Service_response != null)
                {
                    if (Service_response.status)
                    {
                        Application.Current.Properties["User_Email"] = App.User_Email = string.IsNullOrEmpty(Service_response.response.Email) ? string.Empty : Service_response.response.Email;
                        Application.Current.Properties["User_Name"] = App.User_Name = string.IsNullOrEmpty(Service_response.response.Name) ? string.Empty : Service_response.response.Name;
                        Application.Current.Properties["User_UserName"] = App.User_UserName = string.IsNullOrEmpty(Service_response.response.UserName) ? string.Empty : Service_response.response.UserName;
                        Application.Current.Properties["User_UserId"] = App.User_UserId = Service_response.response.UserId == 0 ? 0 : Service_response.response.UserId;
                        Application.Current.Properties["User_Id"] = App.User_Id= string.IsNullOrEmpty(Service_response.response.Id) ? string.Empty : Service_response.response.Id;
                        await GetService_GetAccountProfileByUserId();
                        await GetService_Notifications();
                        Application.Current.MainPage = new SideMenu();
                        HelperToast.ShortMessage("Login Successful");
                    }
                    else
                    {
                        if (Service_response.statusCode == 417)
                        {
                            new ShowUserDialog(UserDialogs.Instance, "WebServer Exception", System.Drawing.Color.Red, System.Drawing.Color.White);
                        }
                        else if (Service_response.statusCode == 203)
                        {
                            new ShowUserDialog(UserDialogs.Instance, "Invalid Email or Password", System.Drawing.Color.Red, System.Drawing.Color.White);
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
                            objResponse = new List<GetAccountProfileByUserId.Response>();
                            for (int i = 0; i < Service_response.response.Count; i++)
                            {
                                GetAccountProfileByUserId.Response objResponse = new GetAccountProfileByUserId.Response();
                                Application.Current.Properties["User_ImporedProductsCount"] = objResponse.ImporedProductsCount = App.User_ImporedProductsCount = Service_response.response[i].ImporedProductsCount;
                                Application.Current.Properties["User_OpenTickets"] = objResponse.OpenTickets = App.User_OpenTickets = Service_response.response[i].OpenTickets;
                                Application.Current.Properties["User_AvailableReports"] = objResponse.AvailableReports = App.User_AvailableReports = Service_response.response[i].AvailableReports;
                                Application.Current.Properties["User_Credits"] = objResponse.Credits = App.User_Credits = Service_response.response[i].Credits;
                                Application.Current.Properties["User_AccountTypeId"] = App.User_AccountTypeId = Service_response.response[i].AccountTypeId;
                                Application.Current.Properties["User_AccountTypeName"] = App.User_AccountTypeName = Service_response.response[i].AccountTypeName;
                                Application.Current.Properties["User_AccountId"] = App.User_AccountId = Service_response.response[i].AccountId;
                            }
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                new ShowUserDialog(UserDialogs.Instance, ex.Message, System.Drawing.Color.Red, System.Drawing.Color.White);
            }
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
                            var objLstNewProducts = Service_response.response.Where(d => d.IsRead == false).ToList();
                            if (objLstNewProducts != null && objLstNewProducts.Count > 0)
                            {
                                Application.Current.Properties["User_NotificationCount"] = App.User_NotificationCount = objLstNewProducts.Count;
                            }
                            else
                            {
                                Application.Current.Properties["User_NotificationCount"] = App.User_NotificationCount = 0;
                            }
                        }
                    }
                    else
                    {
                        Application.Current.Properties["User_NotificationCount"] = App.User_NotificationCount = 0;
                    }
                }
                else
                {
                    Application.Current.Properties["User_NotificationCount"] = App.User_NotificationCount = 0;
                }
            }
            catch (WebException ex)
            {
                Application.Current.Properties["User_NotificationCount"] = App.User_NotificationCount = 0;
            }
        }

        private async Task PostService_SignUp()
        {
            try
            {
                SignUp.PostSignUp objPostSignUp = new SignUp.PostSignUp();
                objPostSignUp.name = txtAddname.Text;
                objPostSignUp.email = txtAddemail.Text;
                objPostSignUp.password = txtAddpassword.Text;
                objPostSignUp.ZipCode = zipcodeValue;
                objPostSignUp.CountryId = dicCountries.FirstOrDefault(s => s.Value == pkrAddCountry.Items[pkrAddCountry.SelectedIndex]).Key;
                string jsonContents = JsonConvert.SerializeObject(objPostSignUp);
                var Service_response = await GetResponseFromWebService.GetResponsePostData<SignUp.RootObject>(ServiceURLs.SignUp, jsonContents);
                if (Service_response != null)
                {
                    if (Service_response.status)
                    {
                        //HelperToast.ShortMessage("Add User Successfully");
                        Application.Current.MainPage = new SignUp_Onboarding.OnboardingScreens(txtAddemail.Text, txtAddpassword.Text);
                    }
                    else
                    {
                        if (Service_response.statusCode == 300)
                        {
                            new ShowUserDialog(UserDialogs.Instance, "EmailId already Exist", System.Drawing.Color.Red, System.Drawing.Color.White);
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
                new ShowUserDialog(UserDialogs.Instance, "EmailId already Exist", System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }
        #endregion
    }
}