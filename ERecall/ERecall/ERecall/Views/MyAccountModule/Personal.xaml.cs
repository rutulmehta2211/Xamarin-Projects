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
    public partial class Personal : ContentPage
    {
        PostUpdateProfile.UpdateProfile objForUpdateProfile;

        Dictionary<int, string> dicCountries;
        public Personal()
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
                await GetService_Getcountries();
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
            txtName.IsEnabled = !flag;
            txtEmail.IsEnabled = !flag;
            txtZipCode.IsEnabled = !flag;
            lblAccountType.IsEnabled = !flag;
            btnUpdate.IsEnabled = !flag;
            btnResetPassword.IsEnabled = !flag;
            if(flag)
            {
                pkrCountry.IsEnabled = false;
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
                            objForUpdateProfile = new PostUpdateProfile.UpdateProfile();
                            objForUpdateProfile.Id = Service_response.response[0].Id;
                            objForUpdateProfile.UserId = Service_response.response[0].UserId;
                            objForUpdateProfile.Name = txtName.Text = Service_response.response[0].Name;
                            objForUpdateProfile.Email = txtEmail.Text = Service_response.response[0].Email;
                            txtZipCode.Text = Convert.ToString(Service_response.response[0].ZipCode);
                            objForUpdateProfile.ZipCode = Service_response.response[0].ZipCode;
                            if(dicCountries != null && dicCountries.Count > 0)
                            {
                                pkrCountry.SelectedItem = dicCountries.FirstOrDefault(d => d.Key == Service_response.response[0].CountryId).Value;
                            }
                            else
                            {
                                pkrCountry.SelectedIndex = 0;
                            }
                            objForUpdateProfile.CountryId = Service_response.response[0].CountryId;
                            lblAccountType.Text = Service_response.response[0].AccountTypeName;
                            objForUpdateProfile.CompanyName = Service_response.response[0].CompanyName;
                            objForUpdateProfile.CompanyAddress = Service_response.response[0].CompanyAddress;
                            objForUpdateProfile.CompanyWebsite = Service_response.response[0].CompanyWebsite;
                            objForUpdateProfile.PrimaryContactName = Service_response.response[0].PrimaryContactName;
                            objForUpdateProfile.PrimaryContactEmail = Service_response.response[0].PrimaryContactEmail;
                            objForUpdateProfile.SecondaryContactName = Service_response.response[0].SecondaryContactName;
                            objForUpdateProfile.SecondaryContactEmail = Service_response.response[0].SecondaryContactEmail;
                            objForUpdateProfile.Notes = Service_response.response[0].Notes;
                            objForUpdateProfile.ProfileImage = Service_response.response[0].ProfileImage;
                        }
                    }
                    else
                    {
                        new ShowUserDialog(UserDialogs.Instance, "WebServer not Responding", System.Drawing.Color.Red, System.Drawing.Color.White);
                    }
                }
            }
            catch (WebException ex)
            {
                new ShowUserDialog(UserDialogs.Instance, ex.Message, System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }

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
                        pkrCountry.IsEnabled = true;
                        pkrCountry.ItemsSource = dicCountries.Values.ToList();
                    }
                    else
                    {
                        pkrCountry.IsEnabled = false;
                        if (Service_response.statusCode == 417)
                        {
                            new ShowUserDialog(UserDialogs.Instance, "WebServer Exception", System.Drawing.Color.Red, System.Drawing.Color.White);
                        }
                    }
                }
                else
                {
                    pkrCountry.IsEnabled = false;
                    new ShowUserDialog(UserDialogs.Instance, "WebServer not Responding", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
            }
            catch (WebException ex)
            {
                pkrCountry.IsEnabled = false;
                new ShowUserDialog(UserDialogs.Instance, ex.Message, System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }

        private async Task PostService_PostUpdateProfile()
        {
            try
            {
                if (objForUpdateProfile!=null)
                {
                    objForUpdateProfile.Name = txtName.Text;
                    objForUpdateProfile.Email = txtEmail.Text;
                    objForUpdateProfile.ZipCode = Convert.ToInt32(txtZipCode.Text);
                    objForUpdateProfile.CountryId = dicCountries.FirstOrDefault(s => s.Value == pkrCountry.Items[pkrCountry.SelectedIndex]).Key;
                    string jsonContents = JsonConvert.SerializeObject(objForUpdateProfile);
                    var Service_response = await GetResponseFromWebService.GetResponsePostData<PostUpdateProfile.RootObject>(ServiceURLs.PostUpdateProfile, jsonContents);
                    if (Service_response != null)
                    {
                        HelperToast.LongMessage("Personal profile updated successfully");
                        Application.Current.Properties["User_Email"] = App.User_Email = string.IsNullOrEmpty(objForUpdateProfile.Email) ? string.Empty : objForUpdateProfile.Email;
                        Application.Current.Properties["User_Name"] = App.User_Name = string.IsNullOrEmpty(objForUpdateProfile.Name) ? string.Empty : objForUpdateProfile.Name;
                        Application.Current.Properties["User_UserId"] = App.User_UserId = objForUpdateProfile.UserId == 0 ? 0 : objForUpdateProfile.UserId;
                        Application.Current.Properties["User_Id"] = App.User_Id = string.IsNullOrEmpty(Convert.ToString(objForUpdateProfile.Id)) ? string.Empty : Convert.ToString(objForUpdateProfile.Id);
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

        private void txtZipCode_TextChanged(object sender, TextChangedEventArgs e)
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
                    entry.Text = val; //Set the Old valu
                }
            }
        }       

        private async void btnUpdate_Clicked(object sender, EventArgs e)
        {
            if (CheckInternetConnection.IsInternetConnected())
            {
                if (string.IsNullOrEmpty(txtName.Text))
                {
                    new ShowUserDialog(UserDialogs.Instance, "Please Enter Name", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
                else if (string.IsNullOrEmpty(txtEmail.Text))
                {
                    new ShowUserDialog(UserDialogs.Instance, "Please Enter Email", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
                else if (!Validation.IsEmail(txtEmail.Text))
                {
                    new ShowUserDialog(UserDialogs.Instance, "Please Enter Valid Email", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
                else if (string.IsNullOrEmpty(txtZipCode.Text))
                {
                    new ShowUserDialog(UserDialogs.Instance, "Please enter valid zip code", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
                else
                {
                    Loading(true);
                    await PostService_PostUpdateProfile();
                    Loading(false);
                }
            }
            else
            {
                new ShowUserDialog(UserDialogs.Instance, "Internet Connection Required", System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }

        private void btnResetPassword_Clicked(object sender, EventArgs e)
        {
            var stack = Navigation.NavigationStack;
            if (stack[stack.Count - 1].GetType() != typeof(ResetPassword))
            {
                Navigation.PushAsync(new ResetPassword());
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