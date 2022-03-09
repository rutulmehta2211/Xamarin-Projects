using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMO.Gallery;

using Xamarin.Forms;
//using Plugin.Vibrate;
using Refractored.Xam.Vibrate;
using SlideOverKit;
using SlideOverKit.MoreSample;

namespace CMO.MenuController
{
    public partial class HomePage :  MenuContainerPage
    {


		public static string PageTitleForComingSoonPage;
        public HomePage()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
            TextOpacity();
			setfontsize();
            HomeGrid.IsVisible = true;
			BindingContext = this;
			filterbutton.Clicked += Filterbutton_Clicked;
			this.SlideMenu = new RightSideMasterPage();

          //  ImgChiefMinister.Source = new UriImageSource { CachingEnabled = true, Uri =new Uri( "http://server.com/image" )};
        }

	    public void Filterbutton_Clicked(object sender, EventArgs e)
		{
			//HomeGrid.Opacity = 0.8;
			this.ShowMenu();
		}

		public async void TapChiefMinisterImage(object sender, EventArgs args)
        {
           // vibratephone();
            await Navigation.PushAsync(new CMO.ChiefMinister.ChiefMinisterContentPage("CM"));
        }
        public async void TapCMVisitImage(object sender, EventArgs args)
        {
           // vibratephone();
            await Navigation.PushAsync(new CMO.ChiefMinister.ChiefMinisterContentPage("CMV"));
        }
        public async void TapTeamMaharashtraImage(object sender, EventArgs args)
        {
          //  vibratephone();
            await Navigation.PushAsync(new CMO.ChiefMinister.ChiefMinisterContentPage("TM"));
        }
        public async void TapPhotoGalleryImage(object sender, EventArgs args)
        {
          //  vibratephone();
            Application.Current.Properties["navigationflag"] = "0";
            await Navigation.PushAsync(new Gallery.PhotoGalleryListPage());
        }
        public async void TapVideoGalleryImage(object sender, EventArgs args)
        {
           // vibratephone();
            Application.Current.Properties["navigationflag"] = "0";
            await Navigation.PushAsync(new Gallery.VideoGallery());
        }
        public async void TapNewsUpdatesImage(object sender, EventArgs args)
        {
          //  vibratephone();
            // PageTitleForComingSoonPage = AppResources.LPressRelease;
            Application.Current.Properties["navigationflag"] = "0";
            await Navigation.PushAsync(new CMO.Gallery.NewsGalleryListPage());
        }
		public async void TapInitiativesImage(object sender, EventArgs args)
		{
		//	vibratephone();
			// PageTitleForComingSoonPage = AppResources.LPressRelease;
			Application.Current.Properties["navigationflag"] = "0";
			await Navigation.PushAsync(new CMO.Initiatives.InitiativesMain());
		}
		public async void TapJoinUsImage(object sender, EventArgs args)
		{
		//	vibratephone();
			// PageTitleForComingSoonPage = AppResources.LPressRelease;
			Application.Current.Properties["navigationflag"] = "0";
			await Navigation.PushAsync(new CMO.ChiefMinister.ChiefMinisterContentPage("JU"));
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();
            titlechange();
            HomeGrid.IsVisible = true;
            Application.Current.Properties["CurrentPage"] = "home";
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
         //   HomeGrid.IsVisible = false;
        }
        public void titlechange()
        {
            if (Application.Current.Properties.ContainsKey("Language"))
            {
                var lang = Application.Current.Properties["Language"] as string;
                
                if (lang == "mr")
                {
                    AppResources.Culture = new System.Globalization.CultureInfo("mr");
                }
                else
                {
                    AppResources.Culture = new System.Globalization.CultureInfo("en");
                }
            }


			//string k = "<div class='modal modal-box' id='theChiefMinister'> <div class='modalContent'> <div class='imgOuter'><img width='437' height='440' alt='The Chief Minister - Shri Devendra Fadnavis' src='sites / all / themes / maharastracmonew / images / cm - pic - big.jpg' title='The Chief Minister - Shri Devendra Fadnavis' /></div> <div class='contentOuter'> <div class='title'> <h2>The Chief Minister</h2> <h3>Shri Devendra Fadnavis</h3> </div> <div class='contentPara'> <p>Devendra Gangadharrao Fadnavis is the current Chief Minister of Maharashtra and President of the ‘Bharatiya Janata Party (BJP) Maharashtra Pradesh’ is a rare brand of politician who is both, erudite and popular with the masses.</p> </div> <ul class='bulletListing'><li><a href='http://www.devendrafadnavis.in/the-man/bio/' target='_blank' title='Biography'><span class='biography circle'> </span>Biography</a></li> <li><a href='/maharastracmo/mr/vision-mission-oath' title='Vision, Mission and Oath'><span class='mission circle'> </span>Vision, Mission and Oath</a></li> <li><a href='http://www.devendrafadnavis.in/index.html' target='_blank' title='Personal Website'><span class='external circle'> </span>Personal Website</a></li> </ul></div> </div> </div>";
			//string re = @"<a [^>]+>(.*?)<\/a>";
			//string ans = System.Text.RegularExpressions.Regex.Replace(k, re, "$1");

			ChiefMinisterLabel.Text = AppResources.LTheChiefMinisterTitle;
			CMsVisitsLabel.Text = AppResources.LCmVisitsTitle;
            TeamMaharashtraLabel.Text = AppResources.LTeamMaharashtraTitle;
            PhotoGalleryLabel.Text = AppResources.LPhotoGalleryTitle;
			VideoGalleryLabel.Text = AppResources.LVideoGalleryTitle;
			NewsUpdatesLabel.Text = AppResources.LNewsUpdatesTitle;
			JoinUsLabel.Text = AppResources.LJoinUsTitle;
			InitiativesLabel.Text = AppResources.LInitiativesTitle;
            this.Title = AppResources.LHome.ToUpper();
        }
        public void vibratephone()
        {
            var v = Refractored.Xam.Vibrate.CrossVibrate.Current;
            v.Vibration(100);
        }
        private void TextOpacity()
        {
            ChiefMinisterLabel.Opacity = 0.88;
            CMsVisitsLabel.Opacity = 0.88;
            TeamMaharashtraLabel.Opacity = 0.88;
            PhotoGalleryLabel.Opacity = 0.88;
            VideoGalleryLabel.Opacity = 0.88;
			NewsUpdatesLabel.Opacity = 0.88;
			InitiativesLabel.Opacity = 0.88;
			JoinUsLabel.Opacity = 0.88;
        }
		 private void setfontsize()
		{
			ChiefMinisterLabel.FontSize = App.GetFontSizeTitle();
			CMsVisitsLabel.FontSize = App.GetFontSizeTitle();
			TeamMaharashtraLabel.FontSize = App.GetFontSizeTitle();
			PhotoGalleryLabel.FontSize = App.GetFontSizeTitle();
			VideoGalleryLabel.FontSize = App.GetFontSizeTitle();
			NewsUpdatesLabel.FontSize = App.GetFontSizeTitle();
			InitiativesLabel.FontSize = App.GetFontSizeTitle();
			JoinUsLabel.FontSize = App.GetFontSizeTitle();
		}

        #region Exit Application
        private bool _canClose = true;

        protected override bool OnBackButtonPressed()
        {
            if (_canClose)
            {
                ShowExitDialog();
            }
            return _canClose;
        }

        private async void ShowExitDialog()
        {
            var answer = await DisplayAlert(AppResources.LExit, AppResources.LExitApp, AppResources.LYes, AppResources.LNo);
            if (answer)
            {
                if (Device.OS == TargetPlatform.Android)
                {
                    DependencyService.Get<IAndroidMethods>().CloseApp();
                }
                _canClose = false;
                OnBackButtonPressed();
            }
        }
        #endregion
    }
}
