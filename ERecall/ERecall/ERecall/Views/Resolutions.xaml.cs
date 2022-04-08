using Acr.UserDialogs;
using ERecall.CommonClasses;
using ERecall.Controls;
using ERecall.ServiceLayer;
using ERecall.ServiceLayer.ServiceModel;
using ERecall.Views.FeedsModule;
using ERecall.Views.ResolutionModule;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ERecall.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Resolutions : ContentPage
    {
        ObservableCollection<GetProductClaimList.Response> objLstClaimList_Business;
        ObservableCollection<GetClaimsLists.Response> objLstClaimList_Free;

        string recallId = null;

        public Resolutions()
        {
            InitializeComponent();
            lstClaimsForFreeUsers.ItemSelected += (sender, e) =>
            {
                ((ListView)sender).SelectedItem = null;
            };
            lstClaimsForBusinessUsers.ItemSelected += (sender, e) =>
            {
                ((ListView)sender).SelectedItem = null;
            };
        }

        public Resolutions(string recallId = null)
        {
            InitializeComponent();
            this.recallId = recallId;
            lstClaimsForFreeUsers.ItemSelected += (sender, e) =>
            {
                ((ListView)sender).SelectedItem = null;
            };
            lstClaimsForBusinessUsers.ItemSelected += (sender, e) =>
            {
                ((ListView)sender).SelectedItem = null;
            };
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (CheckInternetConnection.IsInternetConnected())
            {
                LoadingIndicator.IsVisible = true;
                if (App.User_AccountTypeId == 1 || App.User_AccountTypeName == "Free")
                {
                    await GetService_GetClaimsLists_Free();
                }
                else
                {
                    if (recallId!=null)
                    {
                        await GetService_GetClaimListForBusinessUser(recallId);
                    }
                    else
                    {
                        await GetService_ProductClaimList_Business();
                    }
                }
                LoadingIndicator.IsVisible = false;
            }
            else
            {
                new ShowUserDialog(UserDialogs.Instance, "Internet Connection Required", System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            recallId = null;
        }

        private void ChangeVisibility(bool IsFreeUser, bool IsBusinessUser, bool IsNoData)
        {
            if (IsNoData && !IsFreeUser && !IsBusinessUser)
            {
                lstClaimsForFreeUsers.IsVisible = false;
                lstClaimsForBusinessUsers.IsVisible = false;
                lblNoRecordFound.IsVisible = true;
            }
            else
            {
                if (IsFreeUser)
                {
                    lstClaimsForFreeUsers.IsVisible = true;
                    lstClaimsForBusinessUsers.IsVisible = false;
                    lblNoRecordFound.IsVisible = false;
                }
                else if (IsBusinessUser)
                {
                    lstClaimsForFreeUsers.IsVisible = false;
                    lstClaimsForBusinessUsers.IsVisible = true;
                    lblNoRecordFound.IsVisible = false;
                }
            }
        }

        private async Task GetService_GetClaimsLists_Free()
        {
            try
            {
                var Service_response = await GetResponseFromWebService.GetResponse<GetClaimsLists.RootObject>(ServiceURLs.GetClaimsLists + "userId=" + App.User_UserId);
                if (Service_response != null)
                {
                    if (Service_response.status)
                    {
                        if (Service_response.response != null && Service_response.response.Count > 0)
                        {
                            ChangeVisibility(true, false, false);
                            objLstClaimList_Free = new ObservableCollection<GetClaimsLists.Response>();
                            for (int i = 0; i < Service_response.response.Count; i++)
                            {
                                GetClaimsLists.Response objGetClaimList = new GetClaimsLists.Response();
                                objGetClaimList.ProductName = Service_response.response[i].ProductName;
                                objGetClaimList.Manufacturer = Service_response.response[i].Manufacturer;
                                objGetClaimList.RecallNumber = Service_response.response[i].RecallNumber;
                                var timespan = DateTime.Now.Subtract(Service_response.response[i].ClaimDate);
                                if (timespan.Days > 0)
                                {
                                    objGetClaimList.FromLastUpdateInterval = timespan.Days + " days ago";
                                }
                                else if (timespan.Hours > 0)
                                {
                                    objGetClaimList.FromLastUpdateInterval = timespan.Hours + " hours ago";
                                }
                                else if (timespan.Minutes > 0)
                                {
                                    objGetClaimList.FromLastUpdateInterval = timespan.Minutes + " minutes ago";
                                }
                                else if (timespan.Seconds > 0)
                                {
                                    objGetClaimList.FromLastUpdateInterval = timespan.Seconds + " seconds ago";
                                }
                                else
                                {
                                    objGetClaimList.FromLastUpdateInterval = "0 seconds ago";
                                }
                                objGetClaimList.RecallDatestring = Service_response.response[i].ClaimDate.ToString("MM/dd/yyyy");
                                objGetClaimList.MessageCount = Service_response.response[i].MessageCount;
                                objGetClaimList.StatusId = Convert.ToInt32(Service_response.response[i].StatusId);
                                objGetClaimList.ClaimStatusName = Service_response.response[i].Status;
                                objGetClaimList.ClaimId = Service_response.response[i].ClaimId;
                                objLstClaimList_Free.Add(objGetClaimList);
                            }
                            lstClaimsForFreeUsers.ItemsSource = objLstClaimList_Free;
                        }
                    }
                    else
                    {
                        ChangeVisibility(false, false, true);
                        if (Service_response.statusCode == 417)
                        {
                            new ShowUserDialog(UserDialogs.Instance, "WebServer Exception", System.Drawing.Color.Red, System.Drawing.Color.White);
                        }
                    }
                }
                else
                {
                    ChangeVisibility(false, false, true);
                    new ShowUserDialog(UserDialogs.Instance, "WebServer not Responding", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
            }
            catch (WebException ex)
            {
                ChangeVisibility(false, false, true);
                new ShowUserDialog(UserDialogs.Instance, ex.Message, System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }

        private async Task GetService_ProductClaimList_Business()
        {
            try
            {
                var Service_response = await GetResponseFromWebService.GetResponse<GetProductClaimList.RootObject>(ServiceURLs.ProductClaimList + "AccountId=" + App.User_AccountId);
                if (Service_response != null)
                {
                    if (Service_response.status)
                    {
                        if (Service_response.response != null && Service_response.response.Count > 0)
                        {
                            ChangeVisibility(false, true, false);
                            objLstClaimList_Business = new ObservableCollection<GetProductClaimList.Response>();
                            for (int i = 0; i < Service_response.response.Count; i++)
                            {
                                GetProductClaimList.Response objGetProductClaimList = new GetProductClaimList.Response();
                                objGetProductClaimList.ProductName = Service_response.response[i].ProductName;
                                objGetProductClaimList.ManufacturerName = Convert.ToString(Service_response.response[i].ManufacturerName);
                                objGetProductClaimList.recallNo = Service_response.response[i].recallNo;
                                objGetProductClaimList.recallId = Service_response.response[i].recallId;
                                var timespan = DateTime.Now.Subtract(Service_response.response[i].LatestUpdate);
                                if (timespan.Days > 0)
                                {
                                    objGetProductClaimList.FromLastUpdateInterval = timespan.Days + " days ago";
                                }
                                else if (timespan.Hours > 0)
                                {
                                    objGetProductClaimList.FromLastUpdateInterval = timespan.Hours + " hours ago";
                                }
                                else if (timespan.Minutes > 0)
                                {
                                    objGetProductClaimList.FromLastUpdateInterval = timespan.Minutes + " minutes ago";
                                }
                                else if (timespan.Seconds > 0)
                                {
                                    objGetProductClaimList.FromLastUpdateInterval = timespan.Seconds + " seconds ago";
                                }
                                else
                                {
                                    objGetProductClaimList.FromLastUpdateInterval = "0 seconds ago";
                                }
                                objGetProductClaimList.RecallDatestring = Service_response.response[i].RecallDate.ToString("MM/dd/yyyy");
                                objGetProductClaimList.ClaimCount = Service_response.response[i].ClaimCount;
                                objGetProductClaimList.ClaimStatus = Service_response.response[i].ClaimStatus;
                                #region Commented code
                                //switch(Service_response.response[i].ClaimStatus)
                                //{
                                //    case 1:
                                //        objGetProductClaimList.ClaimStatusName = "Sent";
                                //        break;
                                //    case 2:
                                //        objGetProductClaimList.ClaimStatusName = "Received";
                                //        break;
                                //    case 3:
                                //        objGetProductClaimList.ClaimStatusName = "Replied";
                                //        break;
                                //    case 4:
                                //        objGetProductClaimList.ClaimStatusName = "Resolved";
                                //        break;
                                //    case 5:
                                //        objGetProductClaimList.ClaimStatusName = "Closed";
                                //        break;
                                //    default:
                                //        objGetProductClaimList.ClaimStatusName = "No Status";
                                //        break;
                                //} 
                                #endregion
                                objLstClaimList_Business.Add(objGetProductClaimList);
                            }
                            lstClaimsForBusinessUsers.ItemsSource = objLstClaimList_Business;
                        }
                    }
                    else
                    {
                        ChangeVisibility(false, false, true);
                        if (Service_response.statusCode == 417)
                        {
                            new ShowUserDialog(UserDialogs.Instance, "WebServer Exception", System.Drawing.Color.Red, System.Drawing.Color.White);
                        }
                    }
                }
                else
                {
                    ChangeVisibility(false, false, true);
                    new ShowUserDialog(UserDialogs.Instance, "WebServer not Responding", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
            }
            catch (WebException ex)
            {
                ChangeVisibility(false, false, true);
                new ShowUserDialog(UserDialogs.Instance, ex.Message, System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }

        private async Task GetService_GetClaimListForBusinessUser(string recallId)
        {
            try
            {
                var Service_response = await GetResponseFromWebService.GetResponse<GetClaimsLists.RootObject>(ServiceURLs.GetClaimListForBusinessUser + "recallId=" + recallId + "&accountId=" + App.User_AccountId);
                if (Service_response != null)
                {
                    if (Service_response.status)
                    {
                        if (Service_response.response != null && Service_response.response.Count > 0)
                        {
                            ChangeVisibility(true, false, false);
                            objLstClaimList_Free = new ObservableCollection<GetClaimsLists.Response>();
                            for (int i = 0; i < Service_response.response.Count; i++)
                            {
                                GetClaimsLists.Response objGetClaimList = new GetClaimsLists.Response();
                                objGetClaimList.ProductName = Service_response.response[i].ProductName;
                                objGetClaimList.Manufacturer = Service_response.response[i].Manufacturer;
                                objGetClaimList.RecallNumber = Service_response.response[i].RecallNumber;
                                var timespan = DateTime.Now.Subtract(Service_response.response[i].ClaimDate);
                                if (timespan.Days > 0)
                                {
                                    objGetClaimList.FromLastUpdateInterval = timespan.Days + " days ago";
                                }
                                else if (timespan.Hours > 0)
                                {
                                    objGetClaimList.FromLastUpdateInterval = timespan.Hours + " hours ago";
                                }
                                else if (timespan.Minutes > 0)
                                {
                                    objGetClaimList.FromLastUpdateInterval = timespan.Minutes + " minutes ago";
                                }
                                else if (timespan.Seconds > 0)
                                {
                                    objGetClaimList.FromLastUpdateInterval = timespan.Seconds + " seconds ago";
                                }
                                else
                                {
                                    objGetClaimList.FromLastUpdateInterval = "0 seconds ago";
                                }
                                objGetClaimList.RecallDatestring = Service_response.response[i].RecallDate.ToString("MM/dd/yyyy");
                                objGetClaimList.MessageCount = Service_response.response[i].MessageCount;
                                objGetClaimList.StatusId = Convert.ToInt32(Service_response.response[i].StatusId);
                                objGetClaimList.ClaimStatusName = Service_response.response[i].Status;
                                objGetClaimList.ClaimId = Service_response.response[i].ClaimId;
                                objLstClaimList_Free.Add(objGetClaimList);
                            }
                            lstClaimsForFreeUsers.ItemsSource = objLstClaimList_Free;
                        }
                    }
                    else
                    {
                        ChangeVisibility(false, false, true);
                        if (Service_response.statusCode == 417)
                        {
                            new ShowUserDialog(UserDialogs.Instance, "WebServer Exception", System.Drawing.Color.Red, System.Drawing.Color.White);
                        }
                    }
                }
                else
                {
                    ChangeVisibility(false, false, true);
                    new ShowUserDialog(UserDialogs.Instance, "WebServer not Responding", System.Drawing.Color.Red, System.Drawing.Color.White);
                }
            }
            catch (WebException ex)
            {
                ChangeVisibility(false, false, true);
                new ShowUserDialog(UserDialogs.Instance, ex.Message, System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }

        private async void LstPulled(object sender, EventArgs e)
        {
            lstClaimsForBusinessUsers.IsPullToRefreshEnabled = false;
            lstClaimsForFreeUsers.IsPullToRefreshEnabled = false;
            if (CheckInternetConnection.IsInternetConnected())
            {
                LoadingIndicator.IsVisible = true;
                if (App.User_AccountTypeId == 1 || App.User_AccountTypeName == "Free")
                {
                    await GetService_GetClaimsLists_Free();
                }
                else
                {
                    if (recallId != null)
                    {
                        await GetService_GetClaimListForBusinessUser(recallId);
                    }
                    else
                    {
                        await GetService_ProductClaimList_Business();
                    }
                }
                LoadingIndicator.IsVisible = false;
            }
            else
            {
                new ShowUserDialog(UserDialogs.Instance, "Internet Connection Required", System.Drawing.Color.Red, System.Drawing.Color.White);
            }
            lstClaimsForBusinessUsers.IsRefreshing = false;
            lstClaimsForFreeUsers.IsRefreshing = false;
            lstClaimsForBusinessUsers.IsPullToRefreshEnabled = true;
            lstClaimsForFreeUsers.IsPullToRefreshEnabled = true;
        }

        private void ListItemForBusinessUser_Tapped(object sender, EventArgs e)
        {
            var obj = sender as Grid;
            var recallId = obj.ClassId;
            if (!string.IsNullOrEmpty(recallId))
            {
                var stack = Navigation.NavigationStack;
                if (stack[stack.Count - 1].GetType() != typeof(Resolutions))
                {
                    Navigation.PushAsync(new Resolutions(recallId));
                }
            }
        }

        private void ListItemForFreeUser_Tapped(object sender, EventArgs e)
        {
            try
            {
                var obj = sender as Grid;
                var ClaimId = obj.ClassId;
                if (!string.IsNullOrEmpty(ClaimId))
                {
                    var stack = Navigation.NavigationStack;
                    if (objLstClaimList_Free != null && objLstClaimList_Free.Count > 0)
                    {
                        var objClaim = objLstClaimList_Free.FirstOrDefault(d => d.ClaimId == Convert.ToInt32(ClaimId));
                        if (stack[stack.Count - 1].GetType() != typeof(ResolutionDetail))
                        {
                            Navigation.PushAsync(new ResolutionDetail(objClaim));
                        }
                    }
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