using CMO.ChiefMinister;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CMO.JoinUs
{
    public partial class JoinUsContentPage : ContentPage
    {
        public JoinUsContentPage()
        {
            InitializeComponent();
			lblName.FontSize = App.GetFontSizeTitle();
			lblDescription.FontSize = App.GetFontSizeSmall();
            NavigationPage.SetBackButtonTitle(this, "");
            this.Title = AppResources.LJoinUs.ToUpper(); 
            ListModule.ItemTapped += ListModule_ItemTapped;
			if (Device.Idiom == TargetIdiom.Tablet)
			{
					ListModule.RowHeight = 80;
			}

            List<ContentPagesCMO> _joinus = new List<ContentPagesCMO>();
            _joinus.Add(new ContentPagesCMO() {SetFontSize = App.GetFontSizeSmall(), ListTitle = AppResources.LAapleSarkar, photoicon_path = ImageSource.FromFile("ico_aaple_sarkar.png"), navigateTo = "https://aaplesarkar.maharashtra.gov.in/" });
            _joinus.Add(new ContentPagesCMO() {SetFontSize = App.GetFontSizeSmall(), ListTitle = AppResources.LSocialResponsibilityCell, photoicon_path = ImageSource.FromFile("ico_soc_respon_cell.png"), navigateTo = "http://14.141.36.213/srcportal/" });
            _joinus.Add(new ContentPagesCMO() {SetFontSize = App.GetFontSizeSmall(), ListTitle = AppResources.LCmInternshipProgram, photoicon_path = "ico_cms_intern_prog.png", FlagValue = "PW", navigateTo = "https://mahades.maharashtra.gov.in/FELLOWSHIP/english/about.html" });
            ListModule.ItemsSource = _joinus;
            lblDescription.Text = AppResources.LJoinUsDescription;
			if (Device.Idiom == TargetIdiom.Tablet)
			{
				imgHeader.HeightRequest = 350;
			}
			if (Device.OS == TargetPlatform.iOS)
			{
				if (Device.Idiom == TargetIdiom.Phone)
				{
					imgHeader.HeightRequest = 170;
				}
			}
            imgHeader.Source = "join_us_next.png";
		
        }

        private void ListModule_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedListItem = sender as Xamarin.Forms.ListView;
            var Item = selectedListItem.SelectedItem as ContentPagesCMO;
            if (Item != null)
            {
                var uri = new Uri(Item.navigateTo);
                Device.OpenUri(uri);
            }
           ((ListView)sender).SelectedItem = null;
        }
        protected override void OnAppearing()
        {
            Application.Current.Properties["CurrentPage"] = "joinus";
        }
    }
}
