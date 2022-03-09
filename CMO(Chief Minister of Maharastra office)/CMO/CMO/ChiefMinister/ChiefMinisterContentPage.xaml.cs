using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CMO.ChiefMinister
{
    public partial class ChiefMinisterContentPage : ContentPage
    {
        public static string TeamMaharashtraPageID;
        public ChiefMinisterContentPage(string modulename)
        {
            InitializeComponent();
			setfontsize();
			NavigationPage.SetBackButtonTitle(this, "");
			DataBindingList(modulename);
        }

		public async void TapContentItem(object sender, EventArgs args)
		{
			var tappeditem = sender as StackLayout;
		 	CMO.MenuController.SideMenu.TeamMaharashtraPageID = tappeditem.ClassId;
			await Navigation.PushAsync(new CMO.TeamMaharashtra.Comman_WebView_TeamMaharashtra());
		}
		public async void CMTapContentItem(object sender, EventArgs args)
		{
			var tappeditem = sender as StackLayout;
			if (tappeditem.ClassId == "ChiefMinister1")
			{
				openurl("http://www.devendrafadnavis.in/the-man/bio/");
			}
			else if (tappeditem.ClassId == "ChiefMinister2")
			{ 
				await Navigation.PushAsync(new CMO.ChiefMinister.VisionMissionOath());
			}
			else if (tappeditem.ClassId == "ChiefMinister3")
			{
				openurl("http://www.devendrafadnavis.in/index.html");
			}
				
		}
		public async void CMVisitTapContentItem(object sender, EventArgs args)
		{
			var tappeditem = sender as StackLayout;
	         Application.Current.Properties["navigationflag"] = "0";
			if (tappeditem.ClassId == "CMVisit1")
			{ 
			    await Navigation.PushAsync(new CMO.CMVisits.MakeInMaharashtraInternationalMain());
			}
			else if (tappeditem.ClassId == "CMVisit2")
			{
				await Navigation.PushAsync(new CMO.CMVisits.MaharashtraVisitList());
			}
			else if (tappeditem.ClassId == "CMVisit3")
			{
				await Navigation.PushAsync(new CMO.CMVisits.JalyuktaVisits());;
			}
			else if (tappeditem.ClassId == "CMVisit4")
			{ 
				await Navigation.PushAsync(new CMO.CMVisits.Eventslists());
			}

		}
		public void JoinUsTapContentItem(object sender, EventArgs args)
		{
			var tappeditem = sender as StackLayout;
			openurl(tappeditem.ClassId);
		}
        public void openurl(string url)
        {Device.OpenUri(new Uri(url));}
        private void DataBindingList(string modulename)
        {
            if (Device.Idiom == TargetIdiom.Tablet)
            {
                imgHeader.HeightRequest =350;
            }
			if (Device.OS == TargetPlatform.iOS)
			{
				if (Device.Idiom == TargetIdiom.Phone)
				{
					imgHeader.HeightRequest = 170;
				}
			}
            if (modulename == "CM")
            {
                this.Title = AppResources.LTheChiefMinister;
	            lblName.Text = AppResources.LChiefMinisterName;
				CheifMinisterStack.IsVisible = true;
				TeamMaharashtraStack.IsVisible = false;
				CMVisitStack.IsVisible = false;
				DataStack.Padding = new Thickness(10, 10, 0, 0);
                lblDescription.Text = AppResources.LChiefMinisterDescription;
                imgHeader.Source = "the_chief_minister_next.png";
            }
            else if (modulename == "TM")
            {
                this.Title = AppResources.LTeamMaharashtra;
    			CheifMinisterStack.IsVisible = false;
				TeamMaharashtraStack.IsVisible = true;
				CMVisitStack.IsVisible = false;
				JoinUsStack.IsVisible = false;
                lblDescription.Text = AppResources.LTeamMaharashtraDescription;
				DataStack.Padding = new Thickness(0, 0, 0, 0);
                lblName.IsVisible = false;
                imgHeader.Source = "team_maha_next.png";
            }
            else if (modulename == "CMV")
            {
                this.Title = AppResources.LCmVisits;
                CheifMinisterStack.IsVisible = false;
				TeamMaharashtraStack.IsVisible = false;
				CMVisitStack.IsVisible = true;
				JoinUsStack.IsVisible = false;
                lblDescription.Text = AppResources.LCMVisitDescription;
				DataStack.Padding = new Thickness(0, 0, 0, 0);
                lblName.IsVisible = false;
                imgHeader.Source = "cm_visit_next.png";
            }
			else if (modulename == "JU")
			{
				this.Title = AppResources.LJoinUs.ToUpper();
			    JoinUsStack.IsVisible = true;;
				CheifMinisterStack.IsVisible = false;
				TeamMaharashtraStack.IsVisible = false;
				CMVisitStack.IsVisible = false;
				lblDescription.Text = AppResources.LJoinUsDescription;
				DataStack.Padding = new Thickness(0, 0, 0, 0);
				lblName.IsVisible = false;
				imgHeader.Source = "join_us_next.png";
				Application.Current.Properties["CurrentPage"] = "joinus";
			}
        }

		public void setfontsize()
		{
			lblName.FontSize = App.GetFontSizeTitle();
			lblDescription.FontSize = App.GetFontSizeSmall();

			CMVisitContentLabel1.FontSize = App.GetFontSizeSmall();
			CMVisitContentLabel2.FontSize = App.GetFontSizeSmall();
			CMVisitContentLabel3.FontSize = App.GetFontSizeSmall();
			CMVisitContentLabel4.FontSize = App.GetFontSizeSmall();

			CMContentLabel1.FontSize = App.GetFontSizeSmall();
			CMContentLabel2.FontSize = App.GetFontSizeSmall();
			CMContentLabel3.FontSize = App.GetFontSizeSmall();

			JoinUsContentLabel1.FontSize = App.GetFontSizeSmall();
			JoinUsContentLabel2.FontSize = App.GetFontSizeSmall();
			JoinUsContentLabel3.FontSize = App.GetFontSizeSmall();

			ContentLabel1.FontSize = App.GetFontSizeSmall();
			ContentLabel2.FontSize = App.GetFontSizeSmall();
			ContentLabel3.FontSize = App.GetFontSizeSmall();
			ContentLabel4.FontSize = App.GetFontSizeSmall();
			ContentLabel5.FontSize = App.GetFontSizeSmall();
			ContentLabel6.FontSize = App.GetFontSizeSmall();
		    ContentLabel7.FontSize = App.GetFontSizeSmall();
		}

    }
    public class ContentPagesCMO
    {
        public string ListTitle { get; set; }
        public ImageSource photoicon_path { get; set; }
        public string FlagValue { get; set; }
        public string navigateTo { get; set; } 
		public int SetFontSize { get; set; }
    }

    public class TextColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
				return Color.White;
            }
            else
            {
				return Color.White;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // You probably don't need this, this is used to convert the other way around
            // so from color to yes no or maybe
            throw new NotImplementedException();
        }
    }
}
