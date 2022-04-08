using Acr.UserDialogs;
using ERecall.CommonClasses;
using ERecall.Controls;
using ERecall.ServiceLayer;
using ERecall.ServiceLayer.ServiceModel;
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

namespace ERecall.Views.ResolutionModule
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResolutionDetail : ContentPage
    {
        ObservableCollection<GetMessagesList.Response> objLstMessages;
        int ClaimId = 0;
        public ResolutionDetail(GetClaimsLists.Response objClaimDetail)
        {
            InitializeComponent();
            this.ClaimId = objClaimDetail.ClaimId;
            this.Title = objClaimDetail.ProductName;
            lblRecallNo.Text = objClaimDetail.RecallNumber;
            lblRecallDate.Text = objClaimDetail.RecallDatestring;
            lblUserName.Text = App.User_Name;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (CheckInternetConnection.IsInternetConnected())
            {
                loading(true);
                await GetService_GetMessagesList();
                loading(false);
            }
            else
            {
                new ShowUserDialog(UserDialogs.Instance, "Internet Connection Required", System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }

        private void loading(bool flag)
        {
            LoadingIndicator.IsVisible = flag;
            lstMessageList.IsEnabled = !flag;
            txtMessage.IsEnabled = !flag;
            stkForReplyMessage.IsEnabled = !flag;
        }

        private async Task GetService_GetMessagesList()
        {
            try
            {
                var Service_response = await GetResponseFromWebService.GetResponse<GetMessagesList.RootObject>(ServiceURLs.GetMessagesList + "userId="+App.User_UserId+ "&claimId="+ClaimId);
                if (Service_response != null)
                {
                    if (Service_response.status)
                    {
                        if (Service_response.response != null && Service_response.response.Count > 0)
                        {
                            lstMessageList.IsVisible = true;
                            objLstMessages = new ObservableCollection<GetMessagesList.Response>();
                            for (int i = 0; i < Service_response.response.Count; i++)
                            {
                                GetMessagesList.Response objMessageList = new GetMessagesList.Response();
                                objMessageList.ClaimId = Service_response.response[i].ClaimId;
                                objMessageList.ReplyMessageId = Service_response.response[i].ReplyMessageId;
                                objMessageList.UserName = Service_response.response[i].UserName;
                                objMessageList.MessagesTitle = Service_response.response[i].MessagesTitle;
                                objMessageList.MessagesDescription = Service_response.response[i].MessagesDescription;
                                objLstMessages.Add(objMessageList);
                            }
                            lstMessageList.ItemsSource = objLstMessages;
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

        private async Task PostService_SubmitClaimMessages()
        {
            try
            {
                SubmitClaimMessages.ReplyMessages objPostClaimMessages = new SubmitClaimMessages.ReplyMessages();
                objPostClaimMessages.ClaimId = ClaimId;
                objPostClaimMessages.StatusId = 3;
                objPostClaimMessages.UserId = App.User_UserId;
                objPostClaimMessages.MessagesDescription = txtMessage.Text.Trim();
                string jsonContents = JsonConvert.SerializeObject(objPostClaimMessages);
                var Service_response = await GetResponseFromWebService.GetResponsePostData<SubmitClaimMessages.RootObject>(ServiceURLs.SubmitClaimMessages, jsonContents);
                if (Service_response != null)
                {
                    if (Service_response.status)
                    {
                        HelperToast.LongMessage("Successfully Saved");
                        await GetService_GetMessagesList();
                        txtMessage.Text = string.Empty;
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

        private async void ReplyMessage_Tapped(object sender, EventArgs e)
        {
            if (CheckInternetConnection.IsInternetConnected())
            {
                loading(true);
                await PostService_SubmitClaimMessages();
                loading(false);
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
            MainGridForms.IsVisible = true;
        }
    }
}