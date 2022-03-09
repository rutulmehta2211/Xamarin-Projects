using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CMO.MenuController
{
    public partial class SideMenuContent : ContentPage
    {
        public StackLayout SIDEMENUMainStack { get {return SideMenuMainStack;} }
        #region 0 HOME
        public Label HOMEHeader { get { return HomeHeader; } }
        public StackLayout HeaderStackHOME { get { return HeaderStackHome; } }
        #endregion
        #region 1 for  CMOffice module 
        public Image CMOfficeIMAGE { get { return CMOfficeImage; } }
        public Label CMOfficeHEADER { get { return CMOfficeHeader; } }
        public StackLayout STACKCMOffice { get { return StackCMOffice; } }
        public StackLayout HEADERSTACKCMOffice { get { return HeaderStackCMOffice; } }
        public Label ITEMTeamCMO { get { return ItemTeamCMO; } }
        public Label ITEMFormerCMs { get { return ItemFormerCMs; } }
        public Label ITEMCMsReliefFund { get { return ItemCMsReliefFund; } }
        public Label ITEMContactCMO { get { return ItemContactCMO; } }
        #endregion
        #region 2 for  The Chief Minister module 
        public Image ChiefMinisterIMAGE { get { return ChiefMinisterImage; } }
        public Label ChiefMinisterHEADER { get { return ChiefMinisterHeader; } }
        public StackLayout STACKChiefMinister { get { return StackCheifMinister; } }
        public StackLayout HEADERSTACKChiefMinister { get { return HeaderStackChiefMinister; } }
        public Label ITEMBiography { get { return ItemBiography; } }
        public Label ITEMVissionMissionOath { get { return ItemVissionMissionOath; } }
        public Label ITEMPersonalWebsite { get { return ItemPersonalWebsite; } }

        #endregion
        #region 3 for  Initiatives module 
        //public Image InitiativesIMAGE { get { return InititiativesImage; } }
        public Label InitiativesHEADER { get { return InititiativesHeader; } }
        public StackLayout STACKInitiatives { get { return StackInitiatives; } }
        public StackLayout HEADERSTACKInitiatives { get { return HeaderStackInitiatives; } }
        public Label ITEMDroughtFreeMaharashtra { get { return ItemDroughtFreeMaharashtra; } }
        public Label ITEMSwatchMaharashtra { get { return ItemSwatchMaharashtra; } }
        public Label ITEMMakeInMaharashtra { get { return ItemMakeInMaharashtra; } }
        public Label ITEMSkillDevelopment { get { return ItemSkillDevelopment; } }
        public Label ITEMRightToService { get { return ItemRightToService; } }
        public Label ITEMDigitalMaharashtra { get { return ItemDigitalMaharashtra; } }

        #endregion
        #region 4 for  TeamMaharashtra module 
        public Image TeamMaharashtrasIMAGE { get { return TeamMaharashtraImage; } }
        public Label TeamMaharashtrasHEADER { get { return TeamMaharashtraHeader; } }
        public StackLayout STACKTeamMaharashtra { get { return StackTeamMaharashtra; } }
        public StackLayout HEADERSTACKTeamMaharashtra { get { return HeaderStackTeamMaharashtra; } }
        public Label ITEMGoverner { get { return ItemGoverner; } }
        public Label ITEMCabinetMinister { get { return ItemCabinetMinister; } }
        public Label ITEMStateMinister { get { return ItemStateMinister; } }
        public Label ITEMChiefJustice { get { return ItemChiefJustice; } }
        public Label ITEMSecretaries { get { return ItemSecretaries; } }
        public Label ITEMCOllectors { get { return ItemCollectors; } }
        public Label ITEMGovtDepartment { get { return ItemGovtDepartment; } }
        #endregion
        #region 5 for  Join Us module 
        public Image JoinUsIMAGE { get { return JoinUsImage; } }
        public Label JoinUsHEADER { get { return JoinUsHeader; } }
        public StackLayout STACKJoinUs { get { return StackJoinUs; } }
        public StackLayout HEADERSTACKJoinUs { get { return HeaderStackJoinUs; } }
        public Label ITEMApleSarkar { get { return ItemApleSarkar; } }
        public Label ITEMSocialResponsibilityCell { get { return ItemSocialResponsibilityCell; } }
        public Label ITEMCMsInternshipProgram { get { return ItemCMsInternshipProgram; } }

        #endregion
        #region 6  for CMVisit module 
        public Image CMVisitIMAGE { get { return CMVisitImage; } }
        public Label CMVisitHEADER { get { return CMVisitHeader; } }
        public StackLayout STACKCMVisit { get { return StackCMVisit; } }
        public StackLayout HEADERSTACKCMVisit { get { return HeaderStackCMVisit; } }
        public Label ITEMMakeInMaharashtraGoesInternational { get { return ItemMakeInMaharashtraGoesInternational; } }
        public Label ITEMMaharashtraVisits { get { return ItemMaharashtraVisits; } }
        public Label ITEMJalyuktaVisit { get { return ItemJalyuktaVisit; } }
        public Label ITEMEvents { get { return ItemEvents; } }

        #endregion
        #region 7 for  Gallery module 
        public Image GalleryModuleIMAGE { get { return MediaGalleryImage; } }
        public Label GalleryModuleHEADER { get { return MediaGalleryHeader; } }
        public StackLayout STACKGalleryModule { get { return StackMediaGallery; } }
        public StackLayout HEADERSTACKGalleryModule { get { return HeaderStackMediaGallery; } }
        public Label ITEMNewsGallery { get { return ItemNewsGallery; } }
        public Label ITEMMagazineGallery { get { return ItemMagazineGallery; } }
        public Label ITEMPhotoGallery { get { return ItemPhotoGallery; } }
        public Label ITEMVideoGallery { get { return ItemVideoGallery; } }
        public Label ITEMPressReleases { get { return ItemPressReleases; } }

        #endregion
        #region 8 Change Language 
        public StackLayout HEADERSTACKChangeLanguage { get { return ChangeLanguageStackHeader; } }
        public Label ChangeLanguageHEADERLABEL { get { return ChangeLanguageHeaderLabel; } }
        public StackLayout STACKChangeLanguage { get { return StackChangeLanguage; } }
        public Image ChangeLanguageIMAGE { get { return ChangeLanguageImage; } }
        public Switch EnglishChangeLanguageSWITCH { get {return EnglishChangeLanguageSwitch; } }
        public Switch MarathiChangeLanguageSWITCH { get { return MarathiChangeLanguageSwitch; } }
        #endregion
        #region Menu
        public Label MENUHeader { get { return MenuHeader; } }
        #endregion
        #region 9 Application List
        public ListView ITEMApplicationList { get { return AppList; } }
        public Image ApplicationListIMAGE { get { return ApplicationListImage; } }
        public Label ApplicationListHEADER { get { return ApplicationListHeader; } }
        public StackLayout STACKApplicationList { get { return StackApplicationList; } }
        public StackLayout HEADERSTACKApplicationList { get { return HeaderStackApplicationList; } }
        #endregion
        public SideMenuContent()
        {
            InitializeComponent();
			//  this.Icon = "ico_nav_menu.png";
			setFontSize();
            // ApplicationListBinding();
        }

		public void setFontSize()
		{
			#region 0 for Headers
			HOMEHeader.FontSize = App.GetFontSizeTitle();
			CMOfficeHEADER.FontSize = App.GetFontSizeTitle();
			ChiefMinisterHEADER.FontSize = App.GetFontSizeTitle();
			InitiativesHEADER.FontSize = App.GetFontSizeTitle();
			TeamMaharashtrasHEADER.FontSize = App.GetFontSizeTitle();
			JoinUsHEADER.FontSize = App.GetFontSizeTitle();
			CMVisitHEADER.FontSize = App.GetFontSizeTitle();
			GalleryModuleHEADER.FontSize = App.GetFontSizeTitle();
			ChangeLanguageHEADERLABEL.FontSize = App.GetFontSizeTitle();
			ApplicationListHEADER.FontSize = App.GetFontSizeTitle();
			#endregion

			#region 1 for  CMOffice module 
			ITEMTeamCMO.FontSize = App.GetFontSizeSmall();
			ITEMFormerCMs.FontSize = App.GetFontSizeSmall();
			ITEMCMsReliefFund.FontSize = App.GetFontSizeSmall();
			ITEMContactCMO.FontSize = App.GetFontSizeSmall();
			#endregion

			#region 2 for  The Chief Minister module
			ITEMBiography.FontSize = App.GetFontSizeSmall();
			ITEMVissionMissionOath.FontSize = App.GetFontSizeSmall();
			ITEMPersonalWebsite.FontSize = App.GetFontSizeSmall();
			#endregion

			#region 3 for  Initiatives module 
			#endregion

        	#region 4 for  TeamMaharashtra module 
			ITEMGoverner.FontSize = App.GetFontSizeSmall();
			ITEMCabinetMinister.FontSize = App.GetFontSizeSmall();
			ITEMStateMinister.FontSize = App.GetFontSizeSmall();
			ITEMChiefJustice.FontSize = App.GetFontSizeSmall();
			ITEMSecretaries.FontSize = App.GetFontSizeSmall();
			ITEMCOllectors.FontSize = App.GetFontSizeSmall();
			ITEMGovtDepartment.FontSize = App.GetFontSizeSmall();
			#endregion

			#region 5 for  Join Us module 
			#endregion

   		    #region 6  for CMVisit module 
			ITEMMakeInMaharashtraGoesInternational.FontSize = App.GetFontSizeSmall();
			ITEMMaharashtraVisits.FontSize = App.GetFontSizeSmall();
			ITEMJalyuktaVisit.FontSize = App.GetFontSizeSmall();
			ITEMEvents.FontSize = App.GetFontSizeSmall();
			#endregion

			#region 7 for  Gallery module 
			ITEMNewsGallery.FontSize = App.GetFontSizeSmall();
			ITEMMagazineGallery.FontSize = App.GetFontSizeSmall();
			ITEMPhotoGallery.FontSize = App.GetFontSizeSmall();
			ITEMVideoGallery.FontSize = App.GetFontSizeSmall();
			ITEMPressReleases.FontSize = App.GetFontSizeSmall();
			#endregion
		}

		public void ApplicationListBinding()
        {
            List<ApplicationData> ApplicattionList = new List<ApplicationData>();
            ApplicattionList.Add(new ApplicationData() { Name = "Facebook", PackageName = "com.facebook.katana", Icon = "https://lh3.googleusercontent.com/BoQUq1FnM3HcgRFyfl8vj45aof-waeGdcX9rgZOGkD4ToGXUIG1KnTQOGNurrSlUpx0=w300-rw", URL = "", AppStatus = "", ButtonTap = "" });
            ApplicattionList.Add(new ApplicationData() { Name = "Mahavitaran", PackageName = "com.msedcl.app", Icon = "https://lh3.googleusercontent.com/BoQUq1FnM3HcgRFyfl8vj45aof-waeGdcX9rgZOGkD4ToGXUIG1KnTQOGNurrSlUpx0=w300-rw", URL = "", AppStatus = "", ButtonTap = "" });
            ApplicattionList.Add(new ApplicationData() { Name = "Maha Explorer (Official Guide)", PackageName = "com.mtdc.mtdcapp", Icon = "https://lh3.googleusercontent.com/RT_-IqURSnSX6LS7LTpBLt6dKtpisxwa2SntnRJ8yXCQn3vz1wb85yyuGPlyv9Z-gIBb=w300-rw", URL = "", AppStatus = "", ButtonTap = "" });
            ApplicattionList.Add(new ApplicationData() { Name = "MSRTC Mobile Reservation App", PackageName = "com.expscs.msrtc", Icon = "https://lh3.googleusercontent.com/EbxiRGj7iyRFNByCyE8W1BP_Na_n8BY8uzTigPbuL-GePUD3QugsWvGtxRu-7-rcBkRh=w300-rw", URL = "", AppStatus = "", ButtonTap = "" });
            ApplicattionList.Add(new ApplicationData() { Name = "MahaNews", PackageName = "in.gov.mahanews", Icon = "https://lh6.ggpht.com/EXpWrRQGqGgy2DXRcqM8ViAlSxwU2HfsBFJApAkPRXx4Jctliz3l9usNj4ZP5DAAuwR9=w300-rw", URL = "", AppStatus = "", ButtonTap = "" });
            ApplicattionList.Add(new ApplicationData() { Name = "GR Mobile App", PackageName = "in.gov.maharashtra.govtresolutions", Icon = "https://lh4.ggpht.com/r0nz3jLVT3z5RSgNauoJpGE6OPeDZXFipidhqmxS-QIuqjWjHyn8s3Is0SQBDN28xA=w300-rw", URL = "", AppStatus = "" });
            ApplicattionList.Add(new ApplicationData() { Name = "Lokrajya Magazine", PackageName = "com.news.lokrajya", Icon = "https://lh6.ggpht.com/9TJp2qrfgfey4Yq_x8gWzYvkjPSCSHyyuHtf6VgmGlEsR1wqgZ6Jr0RcGymeyV-8WGo=w300-rw", URL = "", AppStatus = "" });
            ApplicattionList.Add(new ApplicationData() { Name = "MahaOnline Barcode Scanner", PackageName = "in.mol.barcodeScanner", Icon = "https://lh5.ggpht.com/Ax4kYYaph5WPpryEqlxpQCZFnBtxa6kZPAql38k22k48DI_5tvq1C04DdOSBSr467LQ=w300-rw", URL = "", AppStatus = "" });
            ApplicattionList.Add(new ApplicationData() { Name = "FAQs on Local Body Elections", PackageName = "com.crystalhitech.faq", Icon = "https://lh3.googleusercontent.com/P0Qe_VaBIgKHerCB1RBBdY3GvfnQDdEdhJubU_Jl1wWEhdC5g0FSY_5zLXEuJ9AL8MA=w300-rw", URL = "", AppStatus = "" });
            ApplicattionList.Add(new ApplicationData() { Name = "AapleSarkar AapleSarkar", PackageName = "com.aaplesarkar", Icon = "https://lh3.ggpht.com/k9mviLxtKAsLDFggkpn_il4o_IVyUKPc5p8VIv2oJ73QSOg0iZ3dk6t2yITZONeKdtMZ=w300-rw", URL = "", AppStatus = "" });
            ApplicattionList.Add(new ApplicationData() { Name = "Accessible Places by DIT", PackageName = "com.celerapp.redpanda.accessibleplaces", Icon = "https://lh4.ggpht.com/-UOGNTJmz_H-vevY7Lbm4Y1lTi2g2SciXmgwoc7jIV09z9gd2h527pabIMGazghex4e-=w300-rw", URL = "", AppStatus = "" });
            ApplicattionList.Add(new ApplicationData() { Name = "MCGM 24X7", PackageName = "in.cdac.gov.mgov.mcgm", Icon = "https://lh6.ggpht.com/txMe8bpEZ7YnlmX8FVUGpamGaOF5kU0JNoLITMOW9Wce9wSCeYE-nJg-OAamHLZNsSju=w300-rw", URL = "", AppStatus = "" });
            ApplicattionList.Add(new ApplicationData() { Name = "PuneConnect", PackageName = "io.cordova.pmcpunefirst", Icon = "https://lh3.googleusercontent.com/9isJFPZJtHA8nLYY21rzPUkqNktWIdP3g8vpzrbQXuYacw8fMloXUwP4Jv81BUm4uZA=w300-rw", URL = "", AppStatus = "" });
            ApplicattionList.Add(new ApplicationData() { Name = "Smart Nashik", PackageName = "com.swt.nmc.smartnashik", Icon = "https://lh3.googleusercontent.com/ADG4AuZq2aNFLxFLycgREPbtmiVLn-aMKXaJ1vTAHvkbE9L9fC4wUklrj3JDMDyee5iM=w300-rw", URL = "", AppStatus = "" });
            ApplicattionList.Add(new ApplicationData() { Name = "NMC Complaint App", PackageName = "com.SWT.NMCComplaints", Icon = "https://lh3.googleusercontent.com/MLlcv09QnpkvWovG7zCMA2edNx_wQQsICM9SpdsJjnEeRhVdKh8daI1FE0cStmZNcw=w300-rw", URL = "", AppStatus = "" });
            ApplicattionList.Add(new ApplicationData() { Name = "GeoTrack", PackageName = "thane.android.com.thaneapp", Icon = "https://lh3.googleusercontent.com/F3Eryz7Bsm9XnOI6RGNVordrgtfepy4b63-hmMZatjMBgMHPgoijz6IGY6lFAmKnZKw=w300-rw", URL = "", AppStatus = "" });
            ApplicattionList.Add(new ApplicationData() { Name = "Pollution Control", PackageName = "com.pollutioncontrol", Icon = "https://lh3.googleusercontent.com/aDiPyNKGs3rBtzicIUjRX1PlSQCh-j8nl6Hw8L8aGfdsJ9msskDxaK4s8mS1NGihDg=w300-rw", URL = "", AppStatus = "" });
            ApplicattionList.Add(new ApplicationData() { Name = "NMMC Citizen App", PackageName = "com.mars.nmmc_citizen", Icon = "https://lh4.ggpht.com/b5YZ4kgjW-hfRMrRf2_tkdZyLKygszpmuekC76zzZGG21uT776Uz7lYAcqEFg9UIqE4=w300-rw", URL = "", AppStatus = "" });
            ApplicattionList.Add(new ApplicationData() { Name = "SmartKDMC", PackageName = "com.abm.smartkdmc", Icon = "https://lh3.googleusercontent.com/uJBYs1n-gWNBzeI4hJGAA6cb_aH5tJCf5WLelMoSNhcJs_ioiZ3Q18EIaJ5MKNKXtut1=w300-rw", URL = "", AppStatus = "" });
            ApplicattionList.Add(new ApplicationData() { Name = "My CIDCO", PackageName = "in.gov.maharashtra.cidco.cidco2", Icon = "https://lh3.googleusercontent.com/i8K3swjcuQ7hEzdazVE1gOCluhpViLd4jM5ZJkBmT-3VIiGBjAbQnh2OhJYruw-rcg=w300-rw", URL = "", AppStatus = "" });
            ApplicattionList.Add(new ApplicationData() { Name = "WCD Anganwadi", PackageName = "wcd.crystalhitech.com.wcd", Icon = "https://lh3.googleusercontent.com/HS9ngTkUzYZk7ZqxegWJpZRXfwFAzT0_jfTPirBVINxIdmsNRAb9ic6VBwFgJOvkDJE=w300-rw", URL = "", AppStatus = "" });
            AppList.ItemsSource = ApplicattionList;
            for (int i = 0; i < ApplicattionList.Count; i++)
            {
                if (DependencyService.Get<IIsAppInstalled>().IsAppInstalled(ApplicattionList[i].Name))
                {
                    ApplicattionList[i].ButtonTap = ApplicattionList[i].PackageName;
                    ApplicattionList[i].AppStatus = "open";
                }
                else
                {
                    ApplicattionList[i].ButtonTap = ApplicattionList[i].PackageName;
                    ApplicattionList[i].AppStatus = "install";
                }
            }
        }
    }
    

    #region Interface to get action bar height
    public interface IActionBarHeight
    {
        string GetActionBarHeight();
    }
    #endregion

}
