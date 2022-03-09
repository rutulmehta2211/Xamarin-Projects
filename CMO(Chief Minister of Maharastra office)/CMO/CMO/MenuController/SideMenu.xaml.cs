using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CMO.MenuController
{
    public partial class SideMenu : MasterDetailPage
    {
        public static string TeamMaharashtraPageID;
        public static string PageTitleForComingSoonPage;
        
        #region tap Flags
        bool HomeFlag = false;
        bool CMOfficeFlag = false;
        bool ChiefMinisterFlag = false;
        bool JoinUsFlag = false;
        bool CMVisitFlag = false;
        bool InitiativesFlag = false;
        bool TeamMaharashtraFlag = false;
        bool GalleryModuleFlag = false;
        bool ChangeLanguageFLag = false;
        bool ApplicationListFlag = false;
        #endregion
        public SideMenu()
        {
            InitializeComponent();
           // masterPage.Icon = "ico_nav_menu.png";
            SideMenuTapevents();
            SideMenuModuleHeaderTitleBinding();
            string lang = "";
            if (Application.Current.Properties.ContainsKey("language"))
            {
                lang = Application.Current.Properties["language"] as string;
            }
           ChangedTitle();
           IsPresentedChanged += SideMenu_IsPresentedChanged;
			//this.Detail = new NavigationPage(new CMO.mainpage());
          this.Detail = new NavigationPage(new CMO.MenuController.HomePage());
		//this.Detail = new NavigationPage(new SlideOverKit.MoreSample.RightSideDetailPage());
        }

		public void setfontsize()
		{
			#region 0 for Headers
			masterPage.HOMEHeader.FontSize = App.GetFontSizeTitle();
			masterPage.CMOfficeHEADER.FontSize = App.GetFontSizeTitle();
			masterPage.ChiefMinisterHEADER.FontSize = App.GetFontSizeTitle();
			masterPage.InitiativesHEADER.FontSize = App.GetFontSizeTitle();
			masterPage.TeamMaharashtrasHEADER.FontSize = App.GetFontSizeTitle();
			masterPage.JoinUsHEADER.FontSize = App.GetFontSizeTitle();
			masterPage.CMVisitHEADER.FontSize = App.GetFontSizeTitle();
			masterPage.GalleryModuleHEADER.FontSize = App.GetFontSizeTitle();
			masterPage.ChangeLanguageHEADERLABEL.FontSize = App.GetFontSizeTitle();
			masterPage.ApplicationListHEADER.FontSize = App.GetFontSizeTitle();
			#endregion

			#region 1 for  CMOffice module 
			masterPage.ITEMTeamCMO.FontSize = App.GetFontSizeSmall();
			masterPage.ITEMFormerCMs.FontSize = App.GetFontSizeSmall();
			masterPage.ITEMCMsReliefFund.FontSize = App.GetFontSizeSmall();
			masterPage.ITEMContactCMO.FontSize = App.GetFontSizeSmall();
			#endregion

			#region 2 for  The Chief Minister module
			masterPage.ITEMBiography.FontSize = App.GetFontSizeSmall();
			masterPage.ITEMVissionMissionOath.FontSize = App.GetFontSizeSmall();
			masterPage.ITEMPersonalWebsite.FontSize = App.GetFontSizeSmall();
			#endregion

			#region 3 for  Initiatives module 
			#endregion

			#region 4 for  TeamMaharashtra module 
			masterPage.ITEMGoverner.FontSize = App.GetFontSizeSmall();
			masterPage.ITEMCabinetMinister.FontSize = App.GetFontSizeSmall();
			masterPage.ITEMStateMinister.FontSize = App.GetFontSizeSmall();
			masterPage.ITEMChiefJustice.FontSize = App.GetFontSizeSmall();
			masterPage.ITEMSecretaries.FontSize = App.GetFontSizeSmall();
			masterPage.ITEMCOllectors.FontSize = App.GetFontSizeSmall();
			masterPage.ITEMGovtDepartment.FontSize = App.GetFontSizeSmall();
			#endregion

			#region 5 for  Join Us module 
			#endregion

			#region 6  for CMVisit module 
			masterPage.ITEMMakeInMaharashtraGoesInternational.FontSize = App.GetFontSizeSmall();
			masterPage.ITEMMaharashtraVisits.FontSize = App.GetFontSizeSmall();
			masterPage.ITEMJalyuktaVisit.FontSize = App.GetFontSizeSmall();
			masterPage.ITEMEvents.FontSize = App.GetFontSizeSmall();
			#endregion

			#region 7 for  Gallery module 
			masterPage.ITEMNewsGallery.FontSize = App.GetFontSizeSmall();
			masterPage.ITEMMagazineGallery.FontSize = App.GetFontSizeSmall();
			masterPage.ITEMPhotoGallery.FontSize = App.GetFontSizeSmall();
			masterPage.ITEMVideoGallery.FontSize = App.GetFontSizeSmall();
			masterPage.ITEMPressReleases.FontSize = App.GetFontSizeSmall();
			#endregion
		}
        private void SideMenu_IsPresentedChanged(object sender, EventArgs e)
        {
            MasterBehavior = Xamarin.Forms.MasterBehavior.Popover;
            if (App.CurrentPage() == "changelang")
            {
                if (IsPresented == true)
                {
                    ChangedTitle();
                }
            }
          /*  if (App.CurrentPage() != "pressrelease")
            {
            }
            if (App.CurrentPage() != "videogallery")
            {
            }
            if (App.CurrentPage() != "newsgallerylist")
            { }
            if (App.CurrentPage() != "photogallerylist")
            {
            }
            if (App.CurrentPage() != "magazinegallery")
            {
            }*/
        }
        private void SideMenuModuleHeaderTitleBinding()
        {
            masterPage.HOMEHeader.Text = AppResources.LHome;
            //var tapGestureRecognizer = new TapGestureRecognizer();
            //tapGestureRecognizer.Tapped += (s, e) => {
                
            //};
            //masterPage.HOMEHeader.GestureRecognizers.Add(tapGestureRecognizer);
        }
        private void SideMenuTapevents()
        {
            #region Module Tap events
            var HomeTap = new TapGestureRecognizer();
            HomeTap.Tapped += HomeHeaderGestureRecognizer_Tapped;
            var CMofficeHeaderTap = new TapGestureRecognizer();
            CMofficeHeaderTap.Tapped += CMofficeHeaderGestureRecognizer_Tapped;
            var TheChiefMinisterHeaderTap = new TapGestureRecognizer();
            TheChiefMinisterHeaderTap.Tapped += TheChiefMinisterGestureRecognizer_Tapped;
            var JoinUsHeaderTap = new TapGestureRecognizer();
            JoinUsHeaderTap.Tapped += JoinUsHeaderGestureRecognizer_Tapped;
            var CMsVisitHeaderTap = new TapGestureRecognizer();
            CMsVisitHeaderTap.Tapped += CMsVisitHeaderGestureRecognizer_Tapped;
            var InitiativeHeaderTap = new TapGestureRecognizer();
            InitiativeHeaderTap.Tapped += InitiativeHeaderGestureRecognizer_Tapped;
            var TeamMaharashtraHeaderTap = new TapGestureRecognizer();
            TeamMaharashtraHeaderTap.Tapped += TeamMaharashtraHeaderGestureRecognizer_Tapped;
            var GalleryHeaderTap = new TapGestureRecognizer();
            GalleryHeaderTap.Tapped += GalleryHeaderGestureRecognizer_Tapped;
            var ChangeLanguageHeaderTap = new TapGestureRecognizer();
            ChangeLanguageHeaderTap.Tapped += ChangeLanguageHeaderTap_Tapped;
            var ApplicationListHeaderTap = new TapGestureRecognizer();
            ApplicationListHeaderTap.Tapped += ApplicationListHeaderTap_Tapped;
            masterPage.HEADERSTACKCMOffice.GestureRecognizers.Add(CMofficeHeaderTap);
            masterPage.HEADERSTACKChiefMinister.GestureRecognizers.Add(TheChiefMinisterHeaderTap);
            masterPage.HEADERSTACKCMVisit.GestureRecognizers.Add(CMsVisitHeaderTap);
            masterPage.HEADERSTACKJoinUs.GestureRecognizers.Add(JoinUsHeaderTap);
            masterPage.HEADERSTACKInitiatives.GestureRecognizers.Add(InitiativeHeaderTap);
            masterPage.HEADERSTACKTeamMaharashtra.GestureRecognizers.Add(TeamMaharashtraHeaderTap);
            masterPage.HEADERSTACKGalleryModule.GestureRecognizers.Add(GalleryHeaderTap);
            masterPage.HeaderStackHOME.GestureRecognizers.Add(HomeTap);
            masterPage.HEADERSTACKChangeLanguage.GestureRecognizers.Add(ChangeLanguageHeaderTap);
            masterPage.HEADERSTACKApplicationList.GestureRecognizers.Add(ApplicationListHeaderTap);
            #endregion
            #region module items browser navigation links
            
          
            var ApleSarkarTap = new TapGestureRecognizer();
            ApleSarkarTap.Tapped += ApleSarkarTap_Tapped;
            masterPage.ITEMApleSarkar.GestureRecognizers.Add(ApleSarkarTap);

            var SocialRespomsibilityCellTap = new TapGestureRecognizer();
            SocialRespomsibilityCellTap.Tapped += SocialRespomsibilityCellTap_Tapped;
            masterPage.ITEMSocialResponsibilityCell.GestureRecognizers.Add(SocialRespomsibilityCellTap);

            var CMInternshipProgramTap = new TapGestureRecognizer();
            CMInternshipProgramTap.Tapped += CMInternshipProgramTap_Tapped;
            masterPage.ITEMCMsInternshipProgram.GestureRecognizers.Add(CMInternshipProgramTap);
            #endregion

            CMOfficeTapEvents();
            CMVisitTapEvents();
            TeamMaharashtraTapEvents();
            InitiativesTapEvents();
            ChiefMinisterTapEvents();
            GalleryTapEvents();
            ChangeLanguageTapEvents();
        }
        public void ChangedTitle()
        {
            #region Menu & Home
            masterPage.MENUHeader.Text = AppResources.LMenu;
            masterPage.HOMEHeader.Text = AppResources.LHome;
            #endregion
            #region CMOffice
            masterPage.CMOfficeHEADER.Text = AppResources.LCmOffice;
            masterPage.ITEMTeamCMO.Text = AppResources.LTeamcmo;
            masterPage.ITEMFormerCMs.Text = AppResources.LFormercmo;
            masterPage.ITEMCMsReliefFund.Text = AppResources.LCmReliefsFund;
            masterPage.ITEMContactCMO.Text = AppResources.LContactcmo;
            #endregion
            #region ChiefMinister
            masterPage.ChiefMinisterHEADER.Text = AppResources.LTheChiefMinister;
            masterPage.ITEMBiography.Text = AppResources.LBiography;
            masterPage.ITEMVissionMissionOath.Text = AppResources.LVisionMissionOath;
            masterPage.ITEMPersonalWebsite.Text = AppResources.LPersonalWebsite;
            #endregion
            #region Initiatives
            masterPage.InitiativesHEADER.Text = AppResources.LInitiatives;
            masterPage.ITEMDroughtFreeMaharashtra.Text = AppResources.LDroughtFreeMaharashtra;
            masterPage.ITEMSwatchMaharashtra.Text = AppResources.LSwachhMaharashtra;
            masterPage.ITEMMakeInMaharashtra.Text = AppResources.LMakeInMaharashtra;
            masterPage.ITEMSkillDevelopment.Text = AppResources.LSkillDevelopment;
            masterPage.ITEMRightToService.Text = AppResources.LRightToService;
            masterPage.ITEMDigitalMaharashtra.Text = AppResources.LDigitalMaharashtra;
            #endregion
            #region TeamMaharashtras
            masterPage.TeamMaharashtrasHEADER.Text = AppResources.LTeamMaharashtra;
            masterPage.ITEMGoverner.Text = AppResources.LGovernor;
            masterPage.ITEMCabinetMinister.Text = AppResources.LCabinetMinister;
            masterPage.ITEMStateMinister.Text = AppResources.LStateMinisters;
            masterPage.ITEMChiefJustice.Text = AppResources.LMenuChiefMinister;
            masterPage.ITEMSecretaries.Text = AppResources.LSecretaries;
            masterPage.ITEMCOllectors.Text = AppResources.LCollectors;
            masterPage.ITEMGovtDepartment.Text = AppResources.LGovtDepartment;
            #endregion
            #region CMVisit
            masterPage.CMVisitHEADER.Text = AppResources.LCmVisits;
            masterPage.ITEMMakeInMaharashtraGoesInternational.Text = AppResources.LForeignVisits;
            masterPage.ITEMMaharashtraVisits.Text = AppResources.LMaharashtraVisits;
            masterPage.ITEMJalyuktaVisit.Text = AppResources.LJalyuktaVisits;
            masterPage.ITEMEvents.Text = AppResources.LEvents;
            #endregion
            #region Gallery
            masterPage.GalleryModuleHEADER.Text = AppResources.LGallery;
            masterPage.ITEMNewsGallery.Text = AppResources.LNewsGallery;
            masterPage.ITEMMagazineGallery.Text = AppResources.LMagazineGallery;
            masterPage.ITEMPhotoGallery.Text = AppResources.LMenuPhotoGallery;
            masterPage.ITEMVideoGallery.Text = AppResources.LMenuVideoGallery;
            masterPage.ITEMPressReleases.Text = AppResources.LMenuPressRelease;
            #endregion
            #region JoinUs
            masterPage.JoinUsHEADER.Text = AppResources.LJoinUs;
            masterPage.ITEMApleSarkar.Text = AppResources.LAapleSarkar;
            masterPage.ITEMSocialResponsibilityCell.Text = AppResources.LSocialResponsibilityCell;
            masterPage.ITEMCMsInternshipProgram.Text = AppResources.LCmInternshipProgram;
            #endregion
            #region Change Language
            masterPage.ChangeLanguageHEADERLABEL.Text = AppResources.LChangeLanguage;
            #endregion
            #region Application List
            masterPage.ApplicationListHEADER.Text = AppResources.LApplicationList;
            #endregion
        }
        #region Change Language Methods
        private void ChangeLanguageTapEvents()
        {
            masterPage.EnglishChangeLanguageSWITCH.Toggled += EnglishChangeLanguageSWITCH_Toggled;
            masterPage.MarathiChangeLanguageSWITCH.Toggled += MarathiChangeLanguageSWITCH_Toggled;
        }
        private void MarathiChangeLanguageSWITCH_Toggled(object sender, ToggledEventArgs e)
        {
            #region change logic toggle logic
            var obj = sender as Switch;
            if (obj.IsToggled)
            {
                Application.Current.Properties["Language"] = "mr";
                AppResources.Culture = new System.Globalization.CultureInfo("mr");
                masterPage.EnglishChangeLanguageSWITCH.IsToggled = false;
                
               // InitializeComponent();
            }
          
            else
            {
                Application.Current.Properties["Language"] = "en";
                AppResources.Culture = new System.Globalization.CultureInfo("en");
                masterPage.EnglishChangeLanguageSWITCH.IsToggled = true;
            }
            ChangedTitle();
            IsPresented = false;
         //   Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.MenuController.HomePage)));
            ChangeLanguageFLag = false;

            masterPage.STACKChangeLanguage.IsVisible = false;
            masterPage.ChangeLanguageIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.ChangeLanguageHEADERLABEL.TextColor = Color.White;
            #endregion
            ChangeLanguagePageRefresh();
        }
        private void EnglishChangeLanguageSWITCH_Toggled(object sender, ToggledEventArgs e)
        {
            #region change logic toggle logic
            var obj = sender as Switch;
            if (obj.IsToggled)
            {
                Application.Current.Properties["Language"] = "en";
                AppResources.Culture = new System.Globalization.CultureInfo("en");
                masterPage.MarathiChangeLanguageSWITCH.IsToggled = false;
            }
            else
            {
                Application.Current.Properties["Language"] = "mr";
                AppResources.Culture = new System.Globalization.CultureInfo("mr");
                masterPage.MarathiChangeLanguageSWITCH.IsToggled = true;
            }
            IsPresented = false;

            ChangedTitle();
         //   Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.MenuController.HomePage)));

            ChangeLanguageFLag = false;
            masterPage.STACKChangeLanguage.IsVisible = false;
            masterPage.ChangeLanguageIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.ChangeLanguageHEADERLABEL.TextColor = Color.White;
            #endregion
            ChangeLanguagePageRefresh();

        }
        public void ChangeLanguagePageRefresh()
        {
            #region Change Page Refresh
            if (App.CurrentPage() == "home")
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.MenuController.HomePage)));
            }
            #region CM office
            if (App.CurrentPage() == "teamcmo")
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.CMOffice.TeamCMO)));
            }
            if (App.CurrentPage() == "formercm")
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.CMOffice.FormerCMs)));
            }
            if (App.CurrentPage() == "contactcmo")
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.CMOffice.Contact_CMO)));
            }
            #endregion
            #region The Chief Minister
            if (App.CurrentPage() == "vision")
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.ChiefMinister.VisionMissionOath)));
            }
            #endregion
            #region Initiatives
            if (App.CurrentPage() == "initiative")
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.Initiatives.InitiativesMain)));
            }
            #endregion
            #region Team Maharashta
            if (App.CurrentPage() == TeamMaharashtraPageID)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.TeamMaharashtra.Comman_WebView_TeamMaharashtra)));
            }
            #endregion
            #region CM Visit
            if (App.CurrentPage() == "events")
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.CMVisits.Eventslists)));
            }
            if (App.CurrentPage() == "jalyuktavisits")
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.CMVisits.JalyuktaVisits)));
            }
            if (App.CurrentPage() == "maharashtravisits")
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.CMVisits.MaharashtraVisitList)));
            }
            if (App.CurrentPage() == "makeinmaharashtrainternational")
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.CMVisits.MakeInMaharashtraInternationalMain)));
            }
            #endregion
            #region Gallery
            if (App.CurrentPage() == "pressrelease")
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.ComingSoonPage)));
            }
            if (App.CurrentPage() == "videogallery")
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.Gallery.VideoGallery)));
            }
            if (App.CurrentPage() == "newsgallerylist")
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.Gallery.NewsGalleryListPage)));
            }
            if (App.CurrentPage() == "photogallerylist")
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.Gallery.PhotoGalleryListPage)));
            }
            if (App.CurrentPage() == "magazinegallery")
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.Gallery.MagazineGallery)));
            }
            #endregion
            #region Join Us
            if (App.CurrentPage() == "joinus")
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.JoinUs.JoinUsContentPage)));
            }
            #endregion
            #endregion
        }
        #endregion
        #region Gallery methods
        private void GalleryTapEvents()
        {
            
            var MagazineTapped = new TapGestureRecognizer();
            MagazineTapped.Tapped += MagazineTapped_Tapped;
            masterPage.ITEMMagazineGallery.GestureRecognizers.Add(MagazineTapped);

            var PhotoGalleryTapped = new TapGestureRecognizer();
            PhotoGalleryTapped.Tapped += PhotoGalleryTapped_Tapped;
            masterPage.ITEMPhotoGallery.GestureRecognizers.Add(PhotoGalleryTapped);

            var NewsGalleryTapped = new TapGestureRecognizer();
            NewsGalleryTapped.Tapped += NewsGalleryTapped_Tapped;
            masterPage.ITEMNewsGallery.GestureRecognizers.Add(NewsGalleryTapped);

            var VideoGalleryTapped = new TapGestureRecognizer();
            VideoGalleryTapped.Tapped += VideoGalleryTapped_Tapped;
            masterPage.ITEMVideoGallery.GestureRecognizers.Add(VideoGalleryTapped);

            var PressReleaseTapped = new TapGestureRecognizer();
            PressReleaseTapped.Tapped += PressReleaseTapped_Tapped;
            masterPage.ITEMPressReleases.GestureRecognizers.Add(PressReleaseTapped);
        }
        private void PressReleaseTapped_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
            PageTitleForComingSoonPage = AppResources.LPressRelease;

            if (App.CurrentPage() != "pressrelease")
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.ComingSoonPage)));
            }
           
        }
        private void VideoGalleryTapped_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
            if (App.CurrentPage() != "videogallery")
            {
                Application.Current.Properties["navigationflag"] = "0";
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.Gallery.VideoGallery)));
            }
        }
        private void NewsGalleryTapped_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
            if (App.CurrentPage() != "newsgallerylist")
            {
                Application.Current.Properties["navigationflag"] = "0";
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.Gallery.NewsGalleryListPage)));
            }
        }
        private void PhotoGalleryTapped_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
            if (App.CurrentPage() != "photogallerylist")
            {
                Application.Current.Properties["navigationflag"] = "0";
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.Gallery.PhotoGalleryListPage)));
            }
        }
        private void MagazineTapped_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
            if (App.CurrentPage() != "magazinegallery")
            {
                Application.Current.Properties["navigationflag"] = "0";
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.Gallery.MagazineGallery)));
            }
        }
        #endregion
        #region Chief Minister Methods
        private void ChiefMinisterTapEvents()
        {
            var Biographytap = new TapGestureRecognizer();
            Biographytap.Tapped += Biographytap_Tapped;
            masterPage.ITEMBiography.GestureRecognizers.Add(Biographytap);
            var PersonalWebsiteTap = new TapGestureRecognizer();
            PersonalWebsiteTap.Tapped += PersonalWebsiteTap_Tapped;
            masterPage.ITEMPersonalWebsite.GestureRecognizers.Add(PersonalWebsiteTap);

            var VisionMissionOathTap = new TapGestureRecognizer();
            VisionMissionOathTap.Tapped += VisionMissionOathTap_Tapped;
            masterPage.ITEMVissionMissionOath.GestureRecognizers.Add(VisionMissionOathTap);
        }

        private void VisionMissionOathTap_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
            if (App.CurrentPage() != "vision")
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.ChiefMinister.VisionMissionOath)));
            }
        }
        private void PersonalWebsiteTap_Tapped(object sender, EventArgs e)
        {
            var uri = new Uri("http://www.devendrafadnavis.in/index.html");
        //    Device.OpenUri(new Uri("mailto:ryan.hatfield@test.com"));
            Device.OpenUri(uri);
        }
        private void Biographytap_Tapped(object sender, EventArgs e)
        {
            var uri = new Uri("http://www.devendrafadnavis.in/the-man/bio/");
            Device.OpenUri(uri);
        }
        #endregion
        #region Initiatives Methods
        private void InitiativesTapEvents()
        {
            //var ItemInitiativesTap = new TapGestureRecognizer();
            //ItemInitiativesTap.Tapped += ItemDroughFreeMahrashtraTap_Tapped;
            //masterPage.ITEMDroughtFreeMaharashtra.GestureRecognizers.Add(ItemInitiativesTap);
            //masterPage.ITEMSwatchMaharashtra.GestureRecognizers.Add(ItemInitiativesTap);
            //masterPage.ITEMMakeInMaharashtra.GestureRecognizers.Add(ItemInitiativesTap);
            //masterPage.ITEMSkillDevelopment.GestureRecognizers.Add(ItemInitiativesTap);
            //masterPage.ITEMRightToService.GestureRecognizers.Add(ItemInitiativesTap);
            //masterPage.ITEMDigitalMaharashtra.GestureRecognizers.Add(ItemInitiativesTap);
        }

        private void ItemDroughFreeMahrashtraTap_Tapped(object sender, EventArgs e)
        {
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.Initiatives.InitiativesMain)));
            IsPresented = false;
        }
        #endregion
        #region TeamMaharashra methods
        private void TeamMaharashtraTapEvents()
        {
            var ItemGovernorTap = new TapGestureRecognizer();
            ItemGovernorTap.Tapped += ItemGovernorTap_Tapped;
            masterPage.ITEMGoverner.GestureRecognizers.Add(ItemGovernorTap);

            var ItemCabinetMinisterTap = new TapGestureRecognizer();
            ItemCabinetMinisterTap.Tapped += ItemCabinetMinisterTap_Tapped;
            masterPage.ITEMCabinetMinister.GestureRecognizers.Add(ItemCabinetMinisterTap);

            var ItemStateMinisterTap = new TapGestureRecognizer();
            ItemStateMinisterTap.Tapped += ItemStateMinisterTap_Tapped;
            masterPage.ITEMStateMinister.GestureRecognizers.Add(ItemStateMinisterTap);

            var ItemChiefJusticeTap = new TapGestureRecognizer();
            ItemChiefJusticeTap.Tapped += ItemChiefJusticeTap_Tapped;
            masterPage.ITEMChiefJustice.GestureRecognizers.Add(ItemChiefJusticeTap);

            var ItemSecretariesTap = new TapGestureRecognizer();
            ItemSecretariesTap.Tapped += ItemSecretariesTap_Tapped;
            masterPage.ITEMSecretaries.GestureRecognizers.Add(ItemSecretariesTap);

            var ItemCollectorsTap = new TapGestureRecognizer();
            ItemCollectorsTap.Tapped += ItemCollectorsTap_Tapped;
            masterPage.ITEMCOllectors.GestureRecognizers.Add(ItemCollectorsTap);

            var ItemGovtDeptTap = new TapGestureRecognizer();
            ItemGovtDeptTap.Tapped += ItemGovtDeptTap_Tapped;
            masterPage.ITEMGovtDepartment.GestureRecognizers.Add(ItemGovtDeptTap);
        }

        private void ItemGovtDeptTap_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
            TeamMaharashtraPageID = "47";
            if (App.CurrentPage() != TeamMaharashtraPageID)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.TeamMaharashtra.Comman_WebView_TeamMaharashtra)));
            }
        }
        private void ItemCollectorsTap_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
            TeamMaharashtraPageID = "45";
            if (App.CurrentPage() != TeamMaharashtraPageID)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.TeamMaharashtra.Comman_WebView_TeamMaharashtra)));
            }
        }
        private void ItemSecretariesTap_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
            TeamMaharashtraPageID = "44";
            if (App.CurrentPage() != TeamMaharashtraPageID)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.TeamMaharashtra.Comman_WebView_TeamMaharashtra)));
            }
        }
        private void ItemChiefJusticeTap_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
            TeamMaharashtraPageID = "43";
            if (App.CurrentPage() != TeamMaharashtraPageID)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.TeamMaharashtra.Comman_WebView_TeamMaharashtra)));
            }
        }
        private void ItemStateMinisterTap_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
            TeamMaharashtraPageID = "42";
            if (App.CurrentPage() != TeamMaharashtraPageID)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.TeamMaharashtra.Comman_WebView_TeamMaharashtra)));
            }
        }
        private void ItemCabinetMinisterTap_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
            TeamMaharashtraPageID = "41";
            if (App.CurrentPage() != TeamMaharashtraPageID)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.TeamMaharashtra.Comman_WebView_TeamMaharashtra)));
            }
        }
        private void ItemGovernorTap_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
            TeamMaharashtraPageID = "40";
            if (App.CurrentPage() != TeamMaharashtraPageID)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.TeamMaharashtra.Comman_WebView_TeamMaharashtra)));
            }
        }
        #endregion
        #region CMVisits methods
        private void CMVisitTapEvents()
        {
            var ItemMakeInMaharashtraGoesInternationalTap = new TapGestureRecognizer();
            ItemMakeInMaharashtraGoesInternationalTap.Tapped += ItemMakeInMaharashtraGoesInternationalTap_Tapped;
            masterPage.ITEMMakeInMaharashtraGoesInternational.GestureRecognizers.Add(ItemMakeInMaharashtraGoesInternationalTap);

            var ItemMaharashtraVisitsTap = new TapGestureRecognizer();
            ItemMaharashtraVisitsTap.Tapped += ItemMaharashtraVisitsTap_Tapped;
            masterPage.ITEMMaharashtraVisits.GestureRecognizers.Add(ItemMaharashtraVisitsTap);

            var ItemJalyuktaVisitsTap = new TapGestureRecognizer();
            ItemJalyuktaVisitsTap.Tapped += ItemJalyuktaVisitsTap_Tapped;
            masterPage.ITEMJalyuktaVisit.GestureRecognizers.Add(ItemJalyuktaVisitsTap);

            var ItemEventsTap = new TapGestureRecognizer();
            ItemEventsTap.Tapped += ItemEventsTap_Tapped;
            masterPage.ITEMEvents.GestureRecognizers.Add(ItemEventsTap);
        }

        private void ItemEventsTap_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
            if (App.CurrentPage() != "events")
            {
                Application.Current.Properties["navigationflag"] = "0";
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.CMVisits.Eventslists)));
            }
        }

        private void ItemJalyuktaVisitsTap_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
            if (App.CurrentPage() != "jalyuktavisits")
            {
                Application.Current.Properties["navigationflag"] = "0";
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.CMVisits.JalyuktaVisits)));
            }
        }

        private void ItemMaharashtraVisitsTap_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
            if (App.CurrentPage() != "maharashtravisits")
            {
                Application.Current.Properties["navigationflag"] = "0";
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.CMVisits.MaharashtraVisitList)));
            }
        }

        private void ItemMakeInMaharashtraGoesInternationalTap_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
            if (App.CurrentPage() != "makeinmaharashtrainternational")
            {
                Application.Current.Properties["navigationflag"] = "0";
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.CMVisits.MakeInMaharashtraInternationalMain)));
            }
        }
        #endregion
        #region CMOFFICE methods
        private void CMOfficeTapEvents()
        {
            var ItemContactCMOTap = new TapGestureRecognizer();
            ItemContactCMOTap.Tapped += ItemContactCMOTap_Tapped;
            masterPage.ITEMContactCMO.GestureRecognizers.Add(ItemContactCMOTap);

            var ItemFormerCMTap = new TapGestureRecognizer();
            ItemFormerCMTap.Tapped += ItemFormerCMTap_Tapped;
            masterPage.ITEMFormerCMs.GestureRecognizers.Add(ItemFormerCMTap);

            var ItemTeamCMO = new TapGestureRecognizer();
            ItemTeamCMO.Tapped += ItemTeamCMO_Tapped;
            masterPage.ITEMTeamCMO.GestureRecognizers.Add(ItemTeamCMO);

            var CMsReliefFundTap = new TapGestureRecognizer();
            CMsReliefFundTap.Tapped += CMsReliefFund_Tapped;
            masterPage.ITEMCMsReliefFund.GestureRecognizers.Add(CMsReliefFundTap);
        }
        private void ItemTeamCMO_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
            if (App.CurrentPage() != "teamcmo")
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.CMOffice.TeamCMO)));
            }
           
        }
        private void ItemFormerCMTap_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
            if (App.CurrentPage() != "formercm")
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.CMOffice.FormerCMs)));
            }
          
        }
        private void ItemContactCMOTap_Tapped(object sender, EventArgs e)
        {
            //Page displayPage = (Page)Activator.CreateInstance(typeof(CMO.CMOffice.Contact_CMO));
            //Detail.Navigation.PushAsync(displayPage);
            IsPresented = false;
            if (App.CurrentPage() != "contactcmo")
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.CMOffice.Contact_CMO)));
            }
          
        }
        private void CMsReliefFund_Tapped(object sender, EventArgs e)
        {
            var uri = new Uri("https://cmrf.maharashtra.gov.in/CMRFCitizen/index.action");
            Device.OpenUri(uri);
        }
        #endregion
        #region browser redirection methods
      
        private void ApleSarkarTap_Tapped(object sender, EventArgs e)
        {
            var uri = new Uri("https://aaplesarkar.maharashtra.gov.in/");
            Device.OpenUri(uri);
        }
        private void SocialRespomsibilityCellTap_Tapped(object sender, EventArgs e)
        {
            var uri = new Uri("http://14.141.36.213/srcportal/");
            Device.OpenUri(uri);
        }
        private void CMInternshipProgramTap_Tapped(object sender, EventArgs e)
        {
            var uri = new Uri("https://mahades.maharashtra.gov.in/FELLOWSHIP/english/about.html");
            Device.OpenUri(uri);
        }
        #endregion
        #region Module Tap MEthods
        private void HomeHeaderGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            masterPage.HeaderStackHOME.BackgroundColor = Color.FromHex("#0c1337");
            IsPresented = false;
            if (App.CurrentPage() != "home")
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.MenuController.HomePage)));
            }
            #region Visibility Module Items
            CMOfficeFlag = false;
            masterPage.STACKCMOffice.IsVisible = false;
            ChiefMinisterFlag = false;
            masterPage.STACKChiefMinister.IsVisible = false;
            CMVisitFlag = false;
            masterPage.STACKCMVisit.IsVisible = false;
            JoinUsFlag = false;
            masterPage.STACKJoinUs.IsVisible = false;
            InitiativesFlag = false;
            masterPage.STACKInitiatives.IsVisible = false;
            TeamMaharashtraFlag = false;
            masterPage.STACKTeamMaharashtra.IsVisible = false;
            GalleryModuleFlag = false;
            masterPage.STACKGalleryModule.IsVisible = false;
            ChangeLanguageFLag = false;
            masterPage.STACKChangeLanguage.IsVisible = false;
            ApplicationListFlag = false;
            masterPage.STACKApplicationList.IsVisible = false;
            #endregion
            #region Set Down Arrow Image Defualt
            masterPage.ChiefMinisterIMAGE.Source = "ico_down_arrow2_right.png";
            //masterPage.InitiativesIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.TeamMaharashtrasIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.GalleryModuleIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.JoinUsIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.CMOfficeIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.CMVisitIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.ChangeLanguageIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.ApplicationListIMAGE.Source = "ico_down_arrow2_right.png";
            #endregion
            #region Set Default Color of all module names
            masterPage.CMOfficeHEADER.TextColor = Color.White;
            masterPage.ChiefMinisterHEADER.TextColor = Color.White;
            masterPage.InitiativesHEADER.TextColor = Color.White;
            masterPage.TeamMaharashtrasHEADER.TextColor = Color.White;
            masterPage.JoinUsHEADER.TextColor = Color.White;
            masterPage.GalleryModuleHEADER.TextColor = Color.White;
            masterPage.CMVisitHEADER.TextColor = Color.White;
            masterPage.ChangeLanguageHEADERLABEL.TextColor = Color.White;
            masterPage.ApplicationListHEADER.TextColor = Color.White;
            #endregion
            #region set default color of module background on tap
            masterPage.HeaderStackHOME.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKCMOffice.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKChiefMinister.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKInitiatives.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKTeamMaharashtra.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKCMVisit.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKGalleryModule.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKJoinUs.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKChangeLanguage.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKApplicationList.BackgroundColor = Color.FromHex("#141b3d");
            #endregion
        }
        private void CMofficeHeaderGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            #region Visibility Module Items
          
            ChiefMinisterFlag = false;
            masterPage.STACKChiefMinister.IsVisible = false;
            CMVisitFlag = false;
            masterPage.STACKCMVisit.IsVisible = false;
            JoinUsFlag = false;
            masterPage.STACKJoinUs.IsVisible = false;
            InitiativesFlag = false;
            masterPage.STACKInitiatives.IsVisible = false;
            TeamMaharashtraFlag = false;
            masterPage.STACKTeamMaharashtra.IsVisible = false;
            GalleryModuleFlag = false;
            masterPage.STACKGalleryModule.IsVisible = false;
            ChangeLanguageFLag = false;
            masterPage.STACKChangeLanguage.IsVisible = false;
            ApplicationListFlag = false;
            masterPage.STACKApplicationList.IsVisible = false;
            if (CMOfficeFlag == false)
            {
                CMOfficeFlag = true;
                masterPage.STACKCMOffice.IsVisible = true;
                masterPage.CMOfficeIMAGE.Source = "ico_down_arrow2.png";
                masterPage.CMOfficeHEADER.TextColor = Color.FromHex("#f47421");
                masterPage.HEADERSTACKCMOffice.BackgroundColor = Color.FromHex("#0c1337");
            }
            else if (CMOfficeFlag == true)
            {
                CMOfficeFlag = false;
                masterPage.STACKCMOffice.IsVisible = false;
                masterPage.CMOfficeIMAGE.Source = "ico_down_arrow2_right.png";
                masterPage.CMOfficeHEADER.TextColor = Color.White;
                masterPage.HEADERSTACKCMOffice.BackgroundColor = Color.FromHex("#141b3d");
            }
            #endregion
            #region Set Down Arrow Image Defualt
            masterPage.ChiefMinisterIMAGE.Source = "ico_down_arrow2_right.png";
            //masterPage.InitiativesIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.TeamMaharashtrasIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.GalleryModuleIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.JoinUsIMAGE.Source = "ico_down_arrow2_right.png";
            //  masterPage.CMOfficeIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.CMVisitIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.ChangeLanguageIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.ApplicationListIMAGE.Source = "ico_down_arrow2_right.png";
            #endregion
            #region Set Default Color of all module names
            //masterPage.CMOfficeHEADER.TextColor = Color.White;
            masterPage.ChiefMinisterHEADER.TextColor = Color.White;
            masterPage.InitiativesHEADER.TextColor = Color.White;
            masterPage.TeamMaharashtrasHEADER.TextColor = Color.White;
            masterPage.JoinUsHEADER.TextColor = Color.White;
            masterPage.GalleryModuleHEADER.TextColor = Color.White;
            masterPage.CMVisitHEADER.TextColor = Color.White;
            masterPage.ChangeLanguageHEADERLABEL.TextColor = Color.White;
            masterPage.ApplicationListHEADER.TextColor = Color.White;
            #endregion
            #region set default color of module background on tap
            masterPage.HeaderStackHOME.BackgroundColor = Color.FromHex("#141b3d");
            //masterPage.HEADERSTACKCMOffice.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKChiefMinister.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKInitiatives.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKTeamMaharashtra.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKCMVisit.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKGalleryModule.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKJoinUs.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKChangeLanguage.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKApplicationList.BackgroundColor = Color.FromHex("#141b3d");
            #endregion
        }
        private void TheChiefMinisterGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            #region Visibility Module Items
          
            CMOfficeFlag = false;
            masterPage.STACKCMOffice.IsVisible = false;
            CMVisitFlag = false;
            masterPage.STACKCMVisit.IsVisible = false;
            JoinUsFlag = false;
            masterPage.STACKJoinUs.IsVisible = false;
            InitiativesFlag = false;
            masterPage.STACKInitiatives.IsVisible = false;
            TeamMaharashtraFlag = false;
            masterPage.STACKTeamMaharashtra.IsVisible = false;
            GalleryModuleFlag = false;
            masterPage.STACKGalleryModule.IsVisible = false;
            ChangeLanguageFLag = false;
            masterPage.STACKChangeLanguage.IsVisible = false;
            ApplicationListFlag = false;
            masterPage.STACKApplicationList.IsVisible = false;
            if (ChiefMinisterFlag == false)
            {
                ChiefMinisterFlag = true;
                masterPage.STACKChiefMinister.IsVisible = true;
                masterPage.ChiefMinisterIMAGE.Source = "ico_down_arrow2.png";
                masterPage.ChiefMinisterHEADER.TextColor = Color.FromHex("#f47421");
                masterPage.HEADERSTACKChiefMinister.BackgroundColor = Color.FromHex("#0c1337");
            }
            else if (ChiefMinisterFlag == true)
            {
                ChiefMinisterFlag = false;
                masterPage.STACKChiefMinister.IsVisible = false;
                masterPage.ChiefMinisterIMAGE.Source = "ico_down_arrow2_right.png";
                masterPage.ChiefMinisterHEADER.TextColor = Color.White;
                masterPage.HEADERSTACKChiefMinister.BackgroundColor = Color.FromHex("#141b3d");
            }
            #endregion
            #region Set Down Arrow Image Defualt
            //  masterPage.ChiefMinisterIMAGE.Source = "ico_down_arrow2_right.png";
            //masterPage.InitiativesIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.TeamMaharashtrasIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.GalleryModuleIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.JoinUsIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.CMOfficeIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.CMVisitIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.ChangeLanguageIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.ApplicationListIMAGE.Source = "ico_down_arrow2_right.png";
            #endregion
            #region Set Default Color of all module names
            masterPage.CMOfficeHEADER.TextColor = Color.White;
          //  masterPage.ChiefMinisterHEADER.TextColor = Color.White;
            masterPage.InitiativesHEADER.TextColor = Color.White;
            masterPage.TeamMaharashtrasHEADER.TextColor = Color.White;
            masterPage.JoinUsHEADER.TextColor = Color.White;
            masterPage.GalleryModuleHEADER.TextColor = Color.White;
            masterPage.CMVisitHEADER.TextColor = Color.White;
            masterPage.ChangeLanguageHEADERLABEL.TextColor = Color.White;
            masterPage.ApplicationListHEADER.TextColor = Color.White;
            #endregion
            #region set default color of module background on tap
            masterPage.HeaderStackHOME.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKCMOffice.BackgroundColor = Color.FromHex("#141b3d");
          //  masterPage.HEADERSTACKChiefMinister.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKInitiatives.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKTeamMaharashtra.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKCMVisit.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKGalleryModule.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKJoinUs.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKChangeLanguage.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKApplicationList.BackgroundColor = Color.FromHex("#141b3d");
            #endregion
        }
        private void CMsVisitHeaderGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            #region Visibility Module Items 
           
            CMOfficeFlag = false;
            masterPage.STACKCMOffice.IsVisible = false;
            ChiefMinisterFlag = false;
            masterPage.STACKChiefMinister.IsVisible = false;
            JoinUsFlag = false;
            masterPage.STACKJoinUs.IsVisible = false;
            InitiativesFlag = false;
            masterPage.STACKInitiatives.IsVisible = false;
            TeamMaharashtraFlag = false;
            masterPage.STACKTeamMaharashtra.IsVisible = false;
            GalleryModuleFlag = false;
            masterPage.STACKGalleryModule.IsVisible = false;
            ChangeLanguageFLag = false;
            masterPage.STACKChangeLanguage.IsVisible = false;
            ApplicationListFlag = false;
            masterPage.STACKApplicationList.IsVisible = false;
            if (CMVisitFlag == false)
            {
                CMVisitFlag = true;
                masterPage.STACKCMVisit.IsVisible = true;
                masterPage.CMVisitIMAGE.Source = "ico_down_arrow2.png";
                masterPage.CMVisitHEADER.TextColor = Color.FromHex("#f47421");
                masterPage.HEADERSTACKCMVisit.BackgroundColor = Color.FromHex("#0c1337");
            }
            else if (CMVisitFlag == true)
            {
                CMVisitFlag = false;
                masterPage.STACKCMVisit.IsVisible = false;
                masterPage.CMVisitIMAGE.Source = "ico_down_arrow2_right.png";
                masterPage.CMVisitHEADER.TextColor = Color.White;
                masterPage.HEADERSTACKCMVisit.BackgroundColor = Color.FromHex("#141b3d");
            }
            #endregion
            #region Set Down Arrow Image Defualt
            masterPage.ChiefMinisterIMAGE.Source = "ico_down_arrow2_right.png";
            //masterPage.InitiativesIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.TeamMaharashtrasIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.GalleryModuleIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.JoinUsIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.CMOfficeIMAGE.Source = "ico_down_arrow2_right.png";
            //    masterPage.CMVisitIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.ChangeLanguageIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.ApplicationListIMAGE.Source = "ico_down_arrow2_right.png";
            #endregion
            #region  Set Default Color of all module names
            masterPage.CMOfficeHEADER.TextColor = Color.White;
            masterPage.ChiefMinisterHEADER.TextColor = Color.White;
            masterPage.InitiativesHEADER.TextColor = Color.White;
            masterPage.TeamMaharashtrasHEADER.TextColor = Color.White;
            masterPage.JoinUsHEADER.TextColor = Color.White;
            masterPage.GalleryModuleHEADER.TextColor = Color.White;
            //  masterPage.CMVisitHEADER.TextColor = Color.White;
            masterPage.ChangeLanguageHEADERLABEL.TextColor = Color.White;
            masterPage.ApplicationListHEADER.TextColor = Color.White;
            #endregion
            #region set default color of module background on tap
            masterPage.HeaderStackHOME.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKCMOffice.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKChiefMinister.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKInitiatives.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKTeamMaharashtra.BackgroundColor = Color.FromHex("#141b3d");
          //  masterPage.HEADERSTACKCMVisit.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKGalleryModule.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKJoinUs.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKChangeLanguage.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKApplicationList.BackgroundColor = Color.FromHex("#141b3d");
            #endregion
        }
        private void JoinUsHeaderGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            masterPage.HEADERSTACKJoinUs.BackgroundColor = Color.FromHex("#0c1337");
            IsPresented = false;
            if (App.CurrentPage() != "joinus")
            {
				Detail = new NavigationPage(new CMO.ChiefMinister.ChiefMinisterContentPage("JU"));
            }
            #region Visibility Module Items

            CMOfficeFlag = false;
            masterPage.STACKCMOffice.IsVisible = false;
            ChiefMinisterFlag = false;
            masterPage.STACKChiefMinister.IsVisible = false;
            CMVisitFlag = false;
            masterPage.STACKCMVisit.IsVisible = false;
            InitiativesFlag = false;
            masterPage.STACKInitiatives.IsVisible = false;
            TeamMaharashtraFlag = false;
            masterPage.STACKTeamMaharashtra.IsVisible = false;
            GalleryModuleFlag = false;
            masterPage.STACKGalleryModule.IsVisible = false;
            ChangeLanguageFLag = false;
            masterPage.STACKChangeLanguage.IsVisible = false;
            ApplicationListFlag = false;
            masterPage.STACKApplicationList.IsVisible = false;
            //if (JoinUsFlag == false)
            //{
            //  ////  JoinUsFlag = true;
            //  ////  masterPage.STACKJoinUs.IsVisible = true;
            //  //  masterPage.JoinUsIMAGE.Source = "ico_down_arrow2.png";
            //    //masterPage.JoinUsHEADER.TextColor = Color.FromHex("#f47421");
            //    //masterPage.HEADERSTACKJoinUs.BackgroundColor = Color.FromHex("#0c1337");
            //}
            //else if (JoinUsFlag == true)
            //{
            //    JoinUsFlag = false;
            //    masterPage.STACKJoinUs.IsVisible = false;
            //    masterPage.JoinUsIMAGE.Source = "ico_down_arrow2_right.png";
            //    masterPage.JoinUsHEADER.TextColor = Color.White;
            //    masterPage.HEADERSTACKJoinUs.BackgroundColor = Color.FromHex("#141b3d");
            //}
            #endregion
            #region Set Down Arrow Image Defualt
            masterPage.ChiefMinisterIMAGE.Source = "ico_down_arrow2_right.png";
            //masterPage.InitiativesIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.TeamMaharashtrasIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.GalleryModuleIMAGE.Source = "ico_down_arrow2_right.png";
            // masterPage.JoinUsIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.CMOfficeIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.CMVisitIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.ChangeLanguageIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.ApplicationListIMAGE.Source = "ico_down_arrow2_right.png";
            #endregion
            #region  Set Default Color of all module names
            masterPage.CMOfficeHEADER.TextColor = Color.White;
            masterPage.ChiefMinisterHEADER.TextColor = Color.White;
            masterPage.InitiativesHEADER.TextColor = Color.White;
            masterPage.TeamMaharashtrasHEADER.TextColor = Color.White;
            //    masterPage.JoinUsHEADER.TextColor = Color.White;
            masterPage.GalleryModuleHEADER.TextColor = Color.White;
            masterPage.CMVisitHEADER.TextColor = Color.White;
            masterPage.ChangeLanguageHEADERLABEL.TextColor = Color.White;
            masterPage.ApplicationListHEADER.TextColor = Color.White;
            #endregion
            #region set default color of module background on tap
            masterPage.HeaderStackHOME.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKCMOffice.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKChiefMinister.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKInitiatives.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKTeamMaharashtra.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKCMVisit.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKGalleryModule.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKJoinUs.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKChangeLanguage.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKApplicationList.BackgroundColor = Color.FromHex("#141b3d");
            #endregion
        }
        private void InitiativeHeaderGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            masterPage.HEADERSTACKInitiatives.BackgroundColor = Color.FromHex("#0c1337");
            IsPresented = false;
            if (App.CurrentPage() != "initiative")
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.Initiatives.InitiativesMain)));
            }
            #region Visibility Module Items
            CMOfficeFlag = false;
            masterPage.STACKCMOffice.IsVisible = false;
            ChiefMinisterFlag = false;
            masterPage.STACKChiefMinister.IsVisible = false;
            CMVisitFlag = false;
            masterPage.STACKCMVisit.IsVisible = false;
            JoinUsFlag = false;
            masterPage.STACKJoinUs.IsVisible = false;
         //   InitiativesFlag = false;
         //   masterPage.STACKInitiatives.IsVisible = false;
            TeamMaharashtraFlag = false;
            masterPage.STACKTeamMaharashtra.IsVisible = false;
            GalleryModuleFlag = false;
            masterPage.STACKGalleryModule.IsVisible = false;
            ChangeLanguageFLag = false;
            masterPage.STACKChangeLanguage.IsVisible = false;
            ApplicationListFlag = false;
            masterPage.STACKApplicationList.IsVisible = false;
            #endregion
            #region Set Down Arrow Image Defualt
            masterPage.ChiefMinisterIMAGE.Source = "ico_down_arrow2_right.png";
            //masterPage.InitiativesIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.TeamMaharashtrasIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.GalleryModuleIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.JoinUsIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.CMOfficeIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.CMVisitIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.ChangeLanguageIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.ApplicationListIMAGE.Source = "ico_down_arrow2_right.png";
            #endregion
            #region Set Default Color of all module names
            masterPage.CMOfficeHEADER.TextColor = Color.White;
            masterPage.ChiefMinisterHEADER.TextColor = Color.White;
            masterPage.InitiativesHEADER.TextColor = Color.White;
            masterPage.TeamMaharashtrasHEADER.TextColor = Color.White;
            masterPage.JoinUsHEADER.TextColor = Color.White;
            masterPage.GalleryModuleHEADER.TextColor = Color.White;
            masterPage.CMVisitHEADER.TextColor = Color.White;
            masterPage.ChangeLanguageHEADERLABEL.TextColor = Color.White;
            masterPage.ApplicationListHEADER.TextColor = Color.White;
            #endregion
            #region set default color of module background on tap
            masterPage.HeaderStackHOME.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKCMOffice.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKChiefMinister.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKInitiatives.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKTeamMaharashtra.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKCMVisit.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKGalleryModule.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKJoinUs.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKChangeLanguage.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKApplicationList.BackgroundColor = Color.FromHex("#141b3d");
            #endregion
        }
        private void TeamMaharashtraHeaderGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            #region Visibility Module Items
            
            CMOfficeFlag = false;
            masterPage.STACKCMOffice.IsVisible = false;
            ChiefMinisterFlag = false;
            masterPage.STACKChiefMinister.IsVisible = false;
            CMVisitFlag = false;
            masterPage.STACKCMVisit.IsVisible = false;
            JoinUsFlag = false;
            masterPage.STACKJoinUs.IsVisible = false;
            InitiativesFlag = false;
            masterPage.STACKInitiatives.IsVisible = false;
            GalleryModuleFlag = false;
            masterPage.STACKGalleryModule.IsVisible = false;
            ChangeLanguageFLag = false;
            masterPage.STACKChangeLanguage.IsVisible = false;
            ApplicationListFlag = false;
            masterPage.STACKApplicationList.IsVisible = false;
            if (TeamMaharashtraFlag == false)
            {
                TeamMaharashtraFlag = true;
                masterPage.STACKTeamMaharashtra.IsVisible = true;
                masterPage.TeamMaharashtrasIMAGE.Source = "ico_down_arrow2.png";
                masterPage.TeamMaharashtrasHEADER.TextColor = Color.FromHex("#f47421");
                masterPage.HEADERSTACKTeamMaharashtra.BackgroundColor = Color.FromHex("#0c1337");
            }
            else if (TeamMaharashtraFlag == true)
            {
                TeamMaharashtraFlag = false;
                masterPage.STACKTeamMaharashtra.IsVisible = false;
                masterPage.TeamMaharashtrasIMAGE.Source = "ico_down_arrow2_right.png";
                masterPage.TeamMaharashtrasHEADER.TextColor = Color.White;
                masterPage.HEADERSTACKTeamMaharashtra.BackgroundColor = Color.FromHex("#141b3d");
            }
            #endregion
            #region Set Down Arrow Image Defualt
            masterPage.ChiefMinisterIMAGE.Source = "ico_down_arrow2_right.png";
            //masterPage.InitiativesIMAGE.Source = "ico_down_arrow2_right.png";
            //  masterPage.TeamMaharashtrasIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.GalleryModuleIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.JoinUsIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.CMOfficeIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.CMVisitIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.ChangeLanguageIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.ApplicationListIMAGE.Source = "ico_down_arrow2_right.png";
            #endregion
            #region  Set Default Color of all module names
            masterPage.CMOfficeHEADER.TextColor = Color.White;
            masterPage.ChiefMinisterHEADER.TextColor = Color.White;
            masterPage.InitiativesHEADER.TextColor = Color.White;
            //      masterPage.TeamMaharashtrasHEADER.TextColor = Color.White;
            masterPage.JoinUsHEADER.TextColor = Color.White;
            masterPage.GalleryModuleHEADER.TextColor = Color.White;
            masterPage.CMVisitHEADER.TextColor = Color.White;
            masterPage.ChangeLanguageHEADERLABEL.TextColor = Color.White;
            masterPage.ApplicationListHEADER.TextColor = Color.White;
            #endregion
            #region set default color of module background on tap
            masterPage.HeaderStackHOME.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKCMOffice.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKChiefMinister.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKInitiatives.BackgroundColor = Color.FromHex("#141b3d");
         //   masterPage.HEADERSTACKTeamMaharashtra.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKCMVisit.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKGalleryModule.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKJoinUs.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKChangeLanguage.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKApplicationList.BackgroundColor = Color.FromHex("#141b3d");
            #endregion
        }
        private void GalleryHeaderGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            #region Visibility Module Items
            
            CMOfficeFlag = false;
            masterPage.STACKCMOffice.IsVisible = false;
            ChiefMinisterFlag = false;
            masterPage.STACKChiefMinister.IsVisible = false;
            CMVisitFlag = false;
            masterPage.STACKCMVisit.IsVisible = false;
            JoinUsFlag = false;
            masterPage.STACKJoinUs.IsVisible = false;
            InitiativesFlag = false;
            masterPage.STACKInitiatives.IsVisible = false;
            TeamMaharashtraFlag = false;
            masterPage.STACKTeamMaharashtra.IsVisible = false;
            ChangeLanguageFLag = false;
            masterPage.STACKChangeLanguage.IsVisible = false;
            ApplicationListFlag = false;
            masterPage.STACKApplicationList.IsVisible = false;
            if (GalleryModuleFlag == false)
            {
                GalleryModuleFlag = true;
                masterPage.STACKGalleryModule.IsVisible = true;
                masterPage.GalleryModuleIMAGE.Source = "ico_down_arrow2.png";
                masterPage.GalleryModuleHEADER.TextColor = Color.FromHex("#f47421");
                masterPage.HEADERSTACKGalleryModule.BackgroundColor = Color.FromHex("#0c1337");
            }
            else if (GalleryModuleFlag == true)
            {
                GalleryModuleFlag = false;
                masterPage.STACKGalleryModule.IsVisible = false;
                masterPage.GalleryModuleIMAGE.Source = "ico_down_arrow2_right.png";
                masterPage.GalleryModuleHEADER.TextColor = Color.White;
                masterPage.HEADERSTACKGalleryModule.BackgroundColor = Color.FromHex("#141b3d");
            }
            #endregion
            #region Set Down Arrow Image Defualt
            masterPage.ChiefMinisterIMAGE.Source = "ico_down_arrow2_right.png";
            //masterPage.InitiativesIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.TeamMaharashtrasIMAGE.Source = "ico_down_arrow2_right.png";
            //    masterPage.GalleryModuleIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.JoinUsIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.CMOfficeIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.CMVisitIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.ChangeLanguageIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.ApplicationListIMAGE.Source = "ico_down_arrow2_right.png";
            #endregion
            #region  Set Default Color of all module names
            masterPage.CMOfficeHEADER.TextColor = Color.White;
            masterPage.ChiefMinisterHEADER.TextColor = Color.White;
            masterPage.InitiativesHEADER.TextColor = Color.White;
            masterPage.TeamMaharashtrasHEADER.TextColor = Color.White;
            masterPage.JoinUsHEADER.TextColor = Color.White;
            //  masterPage.GalleryModuleHEADER.TextColor = Color.White;
            masterPage.CMVisitHEADER.TextColor = Color.White;
            masterPage.ChangeLanguageHEADERLABEL.TextColor = Color.White;
            masterPage.ApplicationListHEADER.TextColor = Color.White;
            #endregion
            #region set default color of module background on tap
            masterPage.HeaderStackHOME.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKCMOffice.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKChiefMinister.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKInitiatives.BackgroundColor = Color.FromHex("#141b3d");
             masterPage.HEADERSTACKTeamMaharashtra.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKCMVisit.BackgroundColor = Color.FromHex("#141b3d");
            //masterPage.HEADERSTACKGalleryModule.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKJoinUs.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKChangeLanguage.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKApplicationList.BackgroundColor = Color.FromHex("#141b3d");
            #endregion
        }
        private void ChangeLanguageHeaderTap_Tapped(object sender, EventArgs e)
        {
            masterPage.HEADERSTACKChangeLanguage.BackgroundColor = Color.FromHex("#0c1337");
            IsPresented = false;
            if (App.CurrentPage() != "changelang")
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.MenuController.ChangeLanguage)));
            }
            #region Visibility Module Items
            CMOfficeFlag = false;
            masterPage.STACKCMOffice.IsVisible = false;
            ChiefMinisterFlag = false;
            masterPage.STACKChiefMinister.IsVisible = false;
            CMVisitFlag = false;
            masterPage.STACKCMVisit.IsVisible = false;
            JoinUsFlag = false;
            masterPage.STACKJoinUs.IsVisible = false;
            InitiativesFlag = false;
            masterPage.STACKInitiatives.IsVisible = false;
            TeamMaharashtraFlag = false;
            masterPage.STACKTeamMaharashtra.IsVisible = false;
            GalleryModuleFlag = false;
            masterPage.STACKGalleryModule.IsVisible = false;
            ApplicationListFlag = false;
            masterPage.STACKApplicationList.IsVisible = false;
            #endregion
            #region Set Down Arrow Image Defualt
            masterPage.ChiefMinisterIMAGE.Source = "ico_down_arrow2_right.png";
            //masterPage.InitiativesIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.TeamMaharashtrasIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.GalleryModuleIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.JoinUsIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.CMOfficeIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.CMVisitIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.ApplicationListIMAGE.Source = "ico_down_arrow2_right.png";
            #endregion
            #region Set Default Color of all module names
            masterPage.CMOfficeHEADER.TextColor = Color.White;
            masterPage.ChiefMinisterHEADER.TextColor = Color.White;
            masterPage.InitiativesHEADER.TextColor = Color.White;
            masterPage.TeamMaharashtrasHEADER.TextColor = Color.White;
            masterPage.JoinUsHEADER.TextColor = Color.White;
            masterPage.GalleryModuleHEADER.TextColor = Color.White;
            masterPage.CMVisitHEADER.TextColor = Color.White;
            masterPage.ApplicationListHEADER.TextColor = Color.White;
            #endregion
            #region set default color of module background on tap
            masterPage.HeaderStackHOME.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKCMOffice.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKChiefMinister.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKInitiatives.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKTeamMaharashtra.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKCMVisit.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKGalleryModule.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKJoinUs.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKChangeLanguage.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKApplicationList.BackgroundColor = Color.FromHex("#141b3d");
            #endregion
        }
        private void ApplicationListHeaderTap_Tapped(object sender, EventArgs e)
        {
            masterPage.HEADERSTACKApplicationList.BackgroundColor = Color.FromHex("#0c1337");
            IsPresented = false;
            if (App.CurrentPage() != "application")
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(CMO.MenuController.ApplicationList)));
            }
            #region Visibility Module Items
            CMOfficeFlag = false;
            masterPage.STACKCMOffice.IsVisible = false;
            ChiefMinisterFlag = false;
            masterPage.STACKChiefMinister.IsVisible = false;
            CMVisitFlag = false;
            masterPage.STACKCMVisit.IsVisible = false;
            JoinUsFlag = false;
            masterPage.STACKJoinUs.IsVisible = false;
            InitiativesFlag = false;
            masterPage.STACKInitiatives.IsVisible = false;
            TeamMaharashtraFlag = false;
            masterPage.STACKTeamMaharashtra.IsVisible = false;
            GalleryModuleFlag = false;
            masterPage.STACKGalleryModule.IsVisible = false;
            ChangeLanguageFLag = false;
            masterPage.STACKChangeLanguage.IsVisible = false;
            ApplicationListFlag = false;
            masterPage.STACKApplicationList.IsVisible = false;
            #endregion
            #region Set Down Arrow Image Defualt
            masterPage.ChiefMinisterIMAGE.Source = "ico_down_arrow2_right.png";
            //masterPage.InitiativesIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.TeamMaharashtrasIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.GalleryModuleIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.JoinUsIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.CMOfficeIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.CMVisitIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.ChangeLanguageIMAGE.Source = "ico_down_arrow2_right.png";
            masterPage.ApplicationListIMAGE.Source = "ico_down_arrow2_right.png";
            #endregion
            #region Set Default Color of all module names
            masterPage.CMOfficeHEADER.TextColor = Color.White;
            masterPage.ChiefMinisterHEADER.TextColor = Color.White;
            masterPage.InitiativesHEADER.TextColor = Color.White;
            masterPage.TeamMaharashtrasHEADER.TextColor = Color.White;
            masterPage.JoinUsHEADER.TextColor = Color.White;
            masterPage.GalleryModuleHEADER.TextColor = Color.White;
            masterPage.CMVisitHEADER.TextColor = Color.White;
            masterPage.ChangeLanguageHEADERLABEL.TextColor = Color.White;
            masterPage.ApplicationListHEADER.TextColor = Color.White;
            #endregion
            #region set default color of module background on tap
            masterPage.HeaderStackHOME.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKCMOffice.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKChiefMinister.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKInitiatives.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKTeamMaharashtra.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKCMVisit.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKGalleryModule.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKJoinUs.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKChangeLanguage.BackgroundColor = Color.FromHex("#141b3d");
            masterPage.HEADERSTACKApplicationList.BackgroundColor = Color.FromHex("#141b3d");
            #endregion
        }

        #endregion

    }
    public partial class MyMasterDetailPage : MasterDetailPage
    {
        public static readonly BindableProperty DrawerWidthProperty = BindableProperty.Create<MyMasterDetailPage, int>(p => p.DrawerWidth, default(int));

        public int DrawerWidth
        {
            get { return (int)GetValue(DrawerWidthProperty); }
            set { SetValue(DrawerWidthProperty, value); }
        }
    }
}
