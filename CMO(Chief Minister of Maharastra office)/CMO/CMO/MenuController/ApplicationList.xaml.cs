using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CMO.MenuController
{
    public partial class ApplicationList : ContentPage
    {
        public ApplicationList()
        {
            InitializeComponent();
            this.Title = AppResources.LApplicationList;
            NavigationPage.SetBackButtonTitle(this, "");
          
           
        }
        public void ApplicationListBinding()
        {
            List<ApplicationData> ApplicattionList = new List<ApplicationData>();
            #region Binding List For Android
            if (TargetPlatform.Android == Device.OS)
            {
				ApplicattionList.Add(new ApplicationData() { SetFontSize=App.GetFontSizeMedium(),Name = "Mahavitaran", PackageName = "com.msedcl.app", Icon = "AppMahaVitran", URL = "", AppStatus = "" });
				ApplicattionList.Add(new ApplicationData() { SetFontSize=App.GetFontSizeMedium(), Name = "Maha Explorer", PackageName = "com.mtdc.mtdcapp", Icon = "AppMahaExplorer", URL = "", AppStatus = "" });
                ApplicattionList.Add(new ApplicationData() { SetFontSize=App.GetFontSizeMedium(),Name = "MSRTC", PackageName = "com.expscs.msrtc", Icon = "AppMSRTC_Mobile_Reservation_App", URL = "", AppStatus = "" });
                ApplicattionList.Add(new ApplicationData() { SetFontSize = App.GetFontSizeMedium(), Name = "MahaNews", PackageName = "in.gov.mahanews", Icon = "AppMahaNews", URL = "", AppStatus = "" });
                ApplicattionList.Add(new ApplicationData() { SetFontSize = App.GetFontSizeMedium(), Name = "GoM GR, The Official App", PackageName = "in.gov.maharashtra.govtresolutions", Icon = "AppMaharashtra_Govt_Resolutions", URL = "", AppStatus = "" });
                ApplicattionList.Add(new ApplicationData() { SetFontSize = App.GetFontSizeMedium(), Name = "Lokrajya", PackageName = "com.news.lokrajya", Icon = "AppLokrajya_Magazine", URL = "", AppStatus = "" });
                ApplicattionList.Add(new ApplicationData() { SetFontSize = App.GetFontSizeMedium(), Name = "MOLBarCode", PackageName = "in.mol.barcodeScanner", Icon = "AppMahaOnline_Barcode_Scanner", URL = "", AppStatus = "" });
                ApplicattionList.Add(new ApplicationData() { SetFontSize = App.GetFontSizeMedium(), Name = "FAQ", PackageName = "com.crystalhitech.faq", Icon = "AppFAQS", URL = "", AppStatus = "" });
                ApplicattionList.Add(new ApplicationData() { SetFontSize = App.GetFontSizeMedium(), Name = "Aaple Sarkar", PackageName = "com.aaplesarkar", Icon = "AppAaple_Sarkar", URL = "", AppStatus = "" });
                ApplicattionList.Add(new ApplicationData() { SetFontSize = App.GetFontSizeMedium(), Name = "Accessible Places", PackageName = "com.celerapp.redpanda.accessibleplaces", Icon = "AppAccessible_Places", URL = "", AppStatus = "" });
                ApplicattionList.Add(new ApplicationData() { SetFontSize = App.GetFontSizeMedium(), Name = "MCGM 24X7", PackageName = "in.cdac.gov.mgov.mcgm", Icon = "AppMCGM", URL = "", AppStatus = "" });
                ApplicattionList.Add(new ApplicationData() { SetFontSize = App.GetFontSizeMedium(), Name = "PuneConnect", PackageName = "io.cordova.pmcpunefirst", Icon = "AppPuneConnect", URL = "", AppStatus = "" });
                ApplicattionList.Add(new ApplicationData() { SetFontSize = App.GetFontSizeMedium(), Name = "Smart Nashik", PackageName = "com.swt.nmc.smartnashik", Icon = "AppSmart_Nashik", URL = "", AppStatus = "" });
                ApplicattionList.Add(new ApplicationData() { SetFontSize = App.GetFontSizeMedium(), Name = "NMC Complaints", PackageName = "com.SWT.NMCComplaints", Icon = "AppNMC_Complaint_App", URL = "", AppStatus = "" });
                ApplicattionList.Add(new ApplicationData() { SetFontSize = App.GetFontSizeMedium(), Name = "GeoTrack", PackageName = "thane.android.com.thaneapp", Icon = "AppGeotrack", URL = "", AppStatus = "" });
                ApplicattionList.Add(new ApplicationData() { SetFontSize = App.GetFontSizeMedium(), Name = "PollutionControl", PackageName = "com.pollutioncontrol", Icon = "AppPollutionControl", URL = "", AppStatus = "" });
                ApplicattionList.Add(new ApplicationData() { SetFontSize = App.GetFontSizeMedium(), Name = "NMMC Citizen App", PackageName = "com.mars.nmmc_citizen", Icon = "AppNMMC_Citizen_App", URL = "", AppStatus = "" });
                ApplicattionList.Add(new ApplicationData() { SetFontSize = App.GetFontSizeMedium(), Name = "SmartKDMC", PackageName = "com.abm.smartkdmc", Icon = "AppSmartKDMC", URL = "", AppStatus = "" });
                ApplicattionList.Add(new ApplicationData() { SetFontSize = App.GetFontSizeMedium(), Name = "CIDCO", PackageName = "in.gov.maharashtra.cidco.cidco2", Icon = "AppMyCIDCO", URL = "", AppStatus = "" });
                ApplicattionList.Add(new ApplicationData() { SetFontSize = App.GetFontSizeMedium(), Name = "WCD", PackageName = "wcd.crystalhitech.com.wcd", Icon = "AppWCD_Anganwadi", URL = "", AppStatus = "" });
            }
            #endregion
            #region Binding List For iOS
            if (TargetPlatform.iOS == Device.OS)
            {
             //   ApplicattionList.Add(new ApplicationData() { Name = "Mahavitaran", PackageName = "com.msedcl.app", Icon = "http://a3.mzstatic.com/us/r30/Purple18/v4/60/92/b8/6092b8a9-d125-9a09-9b9e-facb0297cf4e/icon175x175.jpeg",
         //           URL = "https://itunes.apple.com/in/app/mahavitaran-consumer-app/id1128838189?mt=8", AppStatus = "" });
             //   ApplicattionList.Add(new ApplicationData() { Name = "Maha Explorer", PackageName = "com.mtdc.mtdcapp", Icon = "", URL = "", AppStatus = "" });
          //      ApplicattionList.Add(new ApplicationData() { Name = "MSRTC", PackageName = "com.expscs.msrtc", Icon = "http://a5.mzstatic.com/us/r30/Purple/v4/a8/6d/5f/a86d5f4a-81f4-fa98-074a-88ff8c93f66e/icon175x175.jpeg",
           //         URL = "https://itunes.apple.com/in/app/msrtc/id788426884?mt=8", AppStatus = "" });
           //     ApplicattionList.Add(new ApplicationData() { Name = "MahaNews", PackageName = "in.gov.mahanews", Icon = "http://a5.mzstatic.com/us/r30/Purple/v4/f5/48/66/f5486642-1b1b-7258-887b-08d37e2fd8d8/icon175x175.png",
            //        URL = "https://itunes.apple.com/in/app/mahanews/id871107735?mt=8", AppStatus = "" });
                ApplicattionList.Add(new ApplicationData() {SetFontSize = App.GetFontSizeMedium(), Name = "GoM GR, The Official App", PackageName = "in.gov.maharashtra.govtresolutions", Icon = "http://a1.mzstatic.com/us/r30/Purple1/v4/a2/cd/65/a2cd6530-2881-2ef3-2f0d-73b182128922/icon175x175.png",
                    URL = "https://itunes.apple.com/in/app/maharashtra-govt.-resolutions/id1015385492?mt=8http://a1.mzstatic.com/us/r30/Purple1/v4/a2/cd/65/a2cd6530-2881-2ef3-2f0d-73b182128922/icon175x175.png", AppStatus = "" });
           //     ApplicattionList.Add(new ApplicationData() { Name = "Lokrajya", PackageName = "com.news.lokrajya", Icon = "", URL = "", AppStatus = "" });
             //   ApplicattionList.Add(new ApplicationData() { Name = "MOLBarCode", PackageName = "in.mol.barcodeScanner", Icon = "", URL = "", AppStatus = "" });
                ApplicattionList.Add(new ApplicationData() { SetFontSize = App.GetFontSizeMedium(),
					Name = "FAQ", PackageName = "com.crystalhitech.faq", Icon = "http://a2.mzstatic.com/us/r30/Purple49/v4/2b/14/fc/2b14fc70-4759-e850-8fc8-a9d62b332d2d/icon175x175.png",
                    URL = "https://itunes.apple.com/us/app/faqs-on-local-body-elections/id1071809672?mt=8", AppStatus = "" });
                ApplicattionList.Add(new ApplicationData() {SetFontSize = App.GetFontSizeMedium(), Name = "Aaple Sarkar", PackageName = "com.aaplesarkar", Icon = "http://a4.mzstatic.com/us/r30/Purple69/v4/0c/8a/0f/0c8a0fe2-0c38-206e-cbd8-84a42f90f0a1/icon175x175.png",
                    URL = "https://itunes.apple.com/in/app/aaple-sarkar/id964411876?mt=8", AppStatus = "" });
					//    ApplicattionList.Add(new ApplicationData() { Name = "Accessible Places", PackageName = "com.celerapp.redpanda.accessibleplaces", Icon = "", URL = "", AppStatus = "" });
                ApplicattionList.Add(new ApplicationData() { SetFontSize = App.GetFontSizeMedium(),
					Name = "MCGM 24X7", PackageName = "in.cdac.gov.mgov.mcgm", Icon = "http://a1.mzstatic.com/us/r30/Purple20/v4/18/68/ab/1868abc8-d58f-3f96-0766-c441de77d953/icon175x175.png",
                    URL = "https://itunes.apple.com/in/app/mcgm-24x7/id982888250?mt=8", AppStatus = "" });
                ApplicattionList.Add(new ApplicationData() {SetFontSize = App.GetFontSizeMedium(), Name = "PuneConnect", PackageName = "io.cordova.pmcpunefirst", Icon = "http://a3.mzstatic.com/us/r30/Purple71/v4/18/50/08/18500871-4c19-8f8f-fb7f-64f7083a3caa/icon175x175.jpeg",
                    URL = "https://itunes.apple.com/in/app/puneconnect/id1073608394?mt=8", AppStatus = "" });
                ApplicattionList.Add(new ApplicationData() {SetFontSize = App.GetFontSizeMedium(), Name = "Smart Nashik", PackageName = "com.swt.nmc.smartnashik", Icon = "http://a4.mzstatic.com/us/r30/Purple49/v4/50/a3/96/50a396f8-f076-fcf9-7987-97d8fa61f944/icon175x175.jpeg",
                    URL = "https://itunes.apple.com/in/app/smart-nashik/id1102398897?mt=8", AppStatus = "" });
             //   ApplicattionList.Add(new ApplicationData() { Name = "NMC Complaints", PackageName = "com.SWT.NMCComplaints", Icon = "", URL = "", AppStatus = "" });
            //    ApplicattionList.Add(new ApplicationData() { Name = "GeoTrack", PackageName = "thane.android.com.thaneapp", Icon = "http://a3.mzstatic.com/us/r30/Purple62/v4/8f/00/a2/8f00a2d3-a39d-55b4-f301-ebee88488045/icon175x175.png",
             //       URL = "https://itunes.apple.com/in/app/geotrack-tmc/id1159341578?mt=8", AppStatus = "" });
             //   ApplicattionList.Add(new ApplicationData() { Name = "PollutionControl", PackageName = "com.pollutioncontrol", Icon = "", URL = "", AppStatus = "" });
            //    ApplicattionList.Add(new ApplicationData() { Name = "NMMC Citizen App", PackageName = "com.mars.nmmc_citizen", Icon = "NMMC_Citizen_App.png", URL = "", AppStatus = "" });
             //   ApplicattionList.Add(new ApplicationData() { Name = "SmartKDMC", PackageName = "com.abm.smartkdmc", Icon = "", URL = "", AppStatus = "" });
             //   ApplicattionList.Add(new ApplicationData() { Name = "CIDCO", PackageName = "in.gov.maharashtra.cidco.cidco2", Icon = "", URL = "", AppStatus = "" });
             //   ApplicattionList.Add(new ApplicationData() { Name = "WCD", PackageName = "wcd.crystalhitech.com.wcd", Icon = "http://a4.mzstatic.com/us/r30/Purple62/v4/82/f7/db/82f7dbed-2f1b-62ae-c27e-08edfa87634f/icon175x175.jpeg",
                 //   URL = "https://itunes.apple.com/in/app/wcd-anganwadi-mobile-app/id1154408213?mt=8", AppStatus = "" });
            }
            #endregion
            //src="http://a4.mzstatic.com/us/r30/Purple69/v4/0c/8a/0f/0c8a0fe2-0c38-206e-cbd8-84a42f90f0a1/icon175x175.png"
            AppList.ItemsSource = ApplicattionList;
            for (int i = 0; i < ApplicattionList.Count; i++)
            {
                if (TargetPlatform.Android == Device.OS)
                {
                    if (DependencyService.Get<IIsAppInstalled>().IsAppInstalled(ApplicattionList[i].PackageName))
                    {
                        ApplicattionList[i].AppStatus = "open";
                    }
                    else
                    {
                        ApplicattionList[i].AppStatus = "install";
                    }
                }

                if (TargetPlatform.iOS == Device.OS)
                {
                    if (DependencyService.Get<IIsAppInstalled>().IsAppInstalled(ApplicattionList[i].URL))
                    {
                        ApplicattionList[i].AppStatus = "open";
                    }
                    else
                    {
                        ApplicattionList[i].AppStatus = "install";
                    }
                }
            }
            loading.IsVisible = false;
            loading.IsRunning = false;
            loadingIndicator.IsVisible = false;
            AppList.HeightRequest = CMO.App.DeviceHeight;
            AppList.IsVisible = true;
            AppList.ItemTapped += AppList_ItemTapped;
        }
        private void AppList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
           var x= sender as ListView;
            x.SelectedItem = null;
        }
        public void AppStatusButtonCLicked(object sender, EventArgs args)
        {
            var x = sender as Button;
            string packgname= x.ClassId;
            string buttontext = x.Text;
            DependencyService.Get<IIsAppInstalled>().NavigatePage(packgname);
            if (buttontext == "open")
            {
                DependencyService.Get<IIsAppInstalled>().NavigatePage(packgname);
            }
            else if (buttontext == "install")
            {
                DependencyService.Get<IIsAppInstalled>().NavigatePage(packgname);
            }
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            Application.Current.Properties["CurrentPage"] = "application";
            if (!App.CheckInternetConnection())
            {
                loadingIndicator.IsVisible = false;
                loading.IsRunning = false;
                loading.IsVisible = false;
                await DisplayAlert(AppResources.LNetworkError, AppResources.LNoInternetConnection, AppResources.LOk);
            }
            else
            {
                loadingIndicator.IsVisible = true;
                loading.IsRunning = true;
                loading.IsVisible = true;
                ApplicationListBinding();
            }
        }
        protected override bool OnBackButtonPressed()
        {
            Application.Current.MainPage = new SideMenu();
            return true;
        }
    }
    public interface IIsAppInstalled
    {
        bool IsAppInstalled(string Package_Name);
        void NavigatePage(string Package_Name);
        string AppVersion { get; set; }
       
    }

	public class ApplicationData
	{
		public string Name { get; set; }
		public string PackageName { get; set; }
		public string URL { get; set; }
		public string Icon { get; set; }
		public string AppStatus { get; set; }
		public string ButtonTap { get; set; }
		public int SetFontSize {get; set;}
		public string buttonColor { get; set;}
    }
}
