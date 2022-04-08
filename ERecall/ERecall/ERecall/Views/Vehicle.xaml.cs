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

namespace ERecall.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Vehicle : ContentPage
    {
        Dictionary<int, string> dicVehicleMakes;
        Dictionary<int, string> dicVehicleModels;
        Dictionary<int, string> dicYearsforvehiclesModels;

        public Vehicle()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (CheckInternetConnection.IsInternetConnected())
            {
                Loading(true);
                await GetService_GetVehicleMakes();
                await GetService_GetYearsforvehiclesModels();
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
            pkrMake.IsEnabled = !flag;
            pkrYear.IsEnabled = !flag;
            txtVIN.IsEnabled = !flag;
            btnAdd.IsEnabled = !flag;
        }

        private async void pkrMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CheckInternetConnection.IsInternetConnected())
            {
                if (dicVehicleMakes != null && dicVehicleMakes.Count > 1 && pkrMake.SelectedIndex != 0)
                {
                    Loading(true);
                    pkrModel.IsEnabled = false;
                    var MakeId = dicVehicleMakes.FirstOrDefault(s => s.Value == pkrMake.Items[pkrMake.SelectedIndex]).Key;
                    await GetService_GetVehicleModels(MakeId);
                    Loading(false);
                }
                else
                {
                    new ShowUserDialog(UserDialogs.Instance, "Please select Manufacturer of Vehicle", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
            }
            else
            {
                new ShowUserDialog(UserDialogs.Instance, "Internet Connection Required", System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            if (CheckInternetConnection.IsInternetConnected())
            {
                if (dicVehicleMakes == null && dicVehicleMakes.Count <= 0 && pkrMake.IsEnabled == false || pkrMake.SelectedIndex <= 0)
                {
                    new ShowUserDialog(UserDialogs.Instance, "Please select Manufacturer of Vehicle", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
                else if (dicVehicleModels == null && dicVehicleModels.Count <= 0 && pkrModel.IsEnabled == false || pkrModel.SelectedIndex <= 0)
                {
                    new ShowUserDialog(UserDialogs.Instance, "Please select Vehicle Model", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
                else if (dicYearsforvehiclesModels == null && dicYearsforvehiclesModels.Count <= 0 && pkrYear.IsEnabled == false || pkrYear.SelectedIndex <= 0)
                {
                    new ShowUserDialog(UserDialogs.Instance, "Please select years of Vehicle", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
                else
                {
                    Loading(true);
                    await PostService_PostUserVehicle();
                    Loading(false);
                }
            }
            else
            {
                new ShowUserDialog(UserDialogs.Instance, "Internet Connection Required", System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }

        private async Task GetService_GetVehicleMakes()
        {
            try
            {
                var Service_response = await GetResponseFromWebService.GetResponse<GetVehicleMakes.RootObject>(ServiceURLs.GetVehicleMakes);
                if (Service_response != null)
                {
                    if (Service_response.status)
                    {
                        dicVehicleMakes = new Dictionary<int, string>();
                        dicVehicleMakes.Add(0, "--Select--");
                        for (int i = 0; i < Service_response.response.Count; i++)
                        {
                            dicVehicleMakes.Add(Convert.ToInt32(Service_response.response[i].ManufacturerId), Convert.ToString(Service_response.response[i].ManufacturerName));
                        }
                        pkrMake.IsEnabled = true;
                        pkrMake.ItemsSource = dicVehicleMakes.Values.ToList();
                    }
                    else
                    {
                        pkrMake.IsEnabled = false;
                        if (Service_response.statusCode == 417)
                        {
                            new ShowUserDialog(UserDialogs.Instance, "WebServer Exception", System.Drawing.Color.Red, System.Drawing.Color.White);
                        }
                    }
                }
                else
                {
                    pkrMake.IsEnabled = false;
                    new ShowUserDialog(UserDialogs.Instance, "WebServer not Responding", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
            }
            catch (WebException ex)
            {
                new ShowUserDialog(UserDialogs.Instance, ex.Message, System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }

        private async Task GetService_GetVehicleModels(int MakeId)
        {
            try
            {
                var Service_response = await GetResponseFromWebService.GetResponse<GetVehicleModels.RootObject>(ServiceURLs.GetVehicleModels + "makeId=" + MakeId);
                if (Service_response != null)
                {
                    if (Service_response.status)
                    {
                        pkrModel.IsEnabled = true;
                        dicVehicleModels = new Dictionary<int, string>();
                        dicVehicleModels.Add(0, "--Select--");
                        for (int i = 0; i < Service_response.response.Count; i++)
                        {
                            dicVehicleModels.Add(Convert.ToInt32(Service_response.response[i].ProductModelId), Convert.ToString(Service_response.response[i].ProductModel));
                        }
                        pkrModel.ItemsSource = null;
                        pkrModel.ItemsSource = dicVehicleModels.Values.ToList();
                    }
                    else
                    {
                        pkrModel.IsEnabled = false;
                        if (Service_response.statusCode == 417)
                        {
                            new ShowUserDialog(UserDialogs.Instance, "WebServer Exception", System.Drawing.Color.Red, System.Drawing.Color.White);
                        }
                    }
                }
                else
                {
                    pkrModel.IsEnabled = false;
                    new ShowUserDialog(UserDialogs.Instance, "WebServer not Responding", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
            }
            catch (WebException ex)
            {
                new ShowUserDialog(UserDialogs.Instance, ex.Message, System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }

        private async Task GetService_GetYearsforvehiclesModels()
        {
            try
            {
                var Service_response = await GetResponseFromWebService.GetResponse<GetYearsforvehiclesModels.RootObject>(ServiceURLs.GetYearsforvehiclesModels);
                if (Service_response != null)
                {
                    if (Service_response.status)
                    {
                        dicYearsforvehiclesModels = new Dictionary<int, string>();
                        dicYearsforvehiclesModels.Add(0, "--Select--");
                        for (int i = 0; i < Service_response.response.Count; i++)
                        {
                            dicYearsforvehiclesModels.Add(Service_response.response[i].YearId, Convert.ToString(Service_response.response[i].YearNo));
                        }
                        pkrYear.ItemsSource = dicYearsforvehiclesModels.Values.ToList();
                    }
                    else
                    {
                        pkrYear.IsEnabled = false;
                        if (Service_response.statusCode == 417)
                        {
                            new ShowUserDialog(UserDialogs.Instance, "WebServer Exception", System.Drawing.Color.Red, System.Drawing.Color.White);
                        }
                    }
                }
                else
                {
                    pkrYear.IsEnabled = false;
                    new ShowUserDialog(UserDialogs.Instance, "WebServer not Responding", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
            }
            catch (WebException ex)
            {
                new ShowUserDialog(UserDialogs.Instance, ex.Message, System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }

        private async Task PostService_PostUserVehicle()
        {
            try
            {
                PostUserVehicle.AddUserVehicle objPostUserVehicle = new PostUserVehicle.AddUserVehicle();
                objPostUserVehicle.VehicleMakeId = dicVehicleMakes.FirstOrDefault(s => s.Value == pkrMake.Items[pkrMake.SelectedIndex]).Key;
                objPostUserVehicle.MakeName = dicVehicleMakes.FirstOrDefault(s => s.Value == pkrMake.Items[pkrMake.SelectedIndex]).Value;
                objPostUserVehicle.VehicleModelId = dicVehicleModels.FirstOrDefault(s => s.Value == pkrModel.Items[pkrModel.SelectedIndex]).Key;
                objPostUserVehicle.ModelName = dicVehicleModels.FirstOrDefault(s => s.Value == pkrModel.Items[pkrModel.SelectedIndex]).Value;
                objPostUserVehicle.YearId = dicYearsforvehiclesModels.FirstOrDefault(s => s.Value == pkrYear.Items[pkrYear.SelectedIndex]).Key;
                objPostUserVehicle.Year = Convert.ToInt32(dicYearsforvehiclesModels.FirstOrDefault(s => s.Value == pkrYear.Items[pkrYear.SelectedIndex]).Value);
                objPostUserVehicle.UserId = App.User_UserId;

                string jsonContents = JsonConvert.SerializeObject(objPostUserVehicle);
                var Service_response = await GetResponseFromWebService.GetResponsePostData<PostUserVehicle.RootObject>(ServiceURLs.PostUserVehicle, jsonContents);
                if (Service_response != null)
                {
                    if (Service_response.status && Service_response.statusCode == 200)
                    {
                        HelperToast.LongMessage("Vehicle Added sucessfully to your Vehicles list");
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