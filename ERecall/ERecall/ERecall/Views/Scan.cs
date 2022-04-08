using Acr.UserDialogs;
using ERecall.CommonClasses;
using ERecall.Controls;
using ERecall.ServiceLayer;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;
using static ERecall.ServiceLayer.ServiceModel.GetRecallStatusByUPCForMobile;

namespace ERecall.Views
{
    public class Scan : ContentPage
    {
        ZXingScannerView zxing;
        ZXingDefaultOverlay overlay;
        Response objresponse;

        public Scan(): base ()
        {
            Title = "Scan";
            zxing = new ZXingScannerView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                AutomationId = "zxingScannerView",
            };
            
            //zxing.OnScanResult += (result) =>
            //    Device.BeginInvokeOnMainThread(async () => {

            //        // Stop analysis until we navigate away so we don't keep reading barcodes
            //        zxing.IsAnalyzing = false;

            //        if (CheckInternetConnection.IsInternetConnected())
            //        {
            //            //call service
            //            await GetService_GetRecallStatusByUPCForMobile(result.Text);
            //        }
            //        else
            //        {
            //            new ShowUserDialog(UserDialogs.Instance, "Internet Connection Required", System.Drawing.Color.Red, System.Drawing.Color.White);
            //        }
                    
            //        // Navigate away
            //        //await Navigation.PopAsync();
            //    });

            overlay = new ZXingDefaultOverlay
            {
                TopText = "Hold your phone up to the barcode",
                BottomText = "Scanning will happen automatically",
                ShowFlashButton = zxing.HasTorch,
                AutomationId = "zxingDefaultOverlay",
            };
            overlay.FlashButtonClicked += (sender, e) => {
                zxing.IsTorchOn = !zxing.IsTorchOn;
            };
            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            grid.Children.Add(zxing);
            grid.Children.Add(overlay);

            // The root page of your application
            Content = grid;
        }

        private async void Zxing_OnScanResult(ZXing.Result result)
        {
            if (CheckInternetConnection.IsInternetConnected())
            {
                //call service
                zxing.IsAnalyzing = false;
                await GetService_GetRecallStatusByUPCForMobile(result.Text);
                zxing.IsAnalyzing = true;
            }
            else
            {
                new ShowUserDialog(UserDialogs.Instance, "Internet Connection Required", System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }

        private async Task GetService_GetRecallStatusByUPCForMobile(string upc)
        {
            try
            {
                string url = ServiceURLs.GetRecallStatusByUPCForMobile + "upc="+upc+"&userId="+App.User_UserId+"&src=Amazon";
                var Service_response = await GetResponseFromWebService.GetResponse<RootObject>(url);
                if (Service_response != null)
                {
                    if (Service_response.status)
                    {
                        if (Service_response.response != null && Service_response.statusCode == 200)
                        {
                            objresponse = new Response();
                            objresponse.isRecall = Service_response.response.isRecall;
                            objresponse.manufacturerId = Service_response.response.manufacturerId;
                            objresponse.productName = Service_response.response.productName;
                            objresponse.productRecallId = Service_response.response.productRecallId;
                            objresponse.upcCode = Service_response.response.upcCode;
                            var message = objresponse.isRecall ? "Product is not safe, Recalled" : "Safe product, Not a recalled";
                            await DisplayAlert(upc, message, "OK");
                        }
                    } 
                    else
                    {
                        if (Service_response.statusCode == 404)
                        {
                            new ShowUserDialog(UserDialogs.Instance, "Could not lookup a product, Not a recalled product", System.Drawing.Color.Red, System.Drawing.Color.White);
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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            zxing.IsScanning = true;
            zxing.OnScanResult += Zxing_OnScanResult;
        }
        protected override void OnDisappearing()
        {
            zxing.IsScanning = false;
            base.OnDisappearing();
            zxing.OnScanResult -= Zxing_OnScanResult;
        }
    }
}