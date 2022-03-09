using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CMO.CMVisits
{
    public partial class JalyuktaVisitsDetail : ContentPage
    {
        public JalyuktaVisitsDetail()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
			setfontsize();
        }
		public void setfontsize()
		{
			JalyuktaVisittitle.FontSize = App.GetFontSizeMedium();
			JalyuktaVisitDate.FontSize = App.GetFontSizeSmall();
			JalyuktaVisitDetail.FontSize = App.GetFontSizeSmall();
		}
        public JalyuktaVisitsDetail(CMO.ServicesClasses.CmVisit Data)
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
            Title = Data.title.ToUpper();
            JalyuktaDetailImage.Source = Data.image;
            JalyuktaVisittitle.Text = Data.title;
            JalyuktaVisitDate.Text = Data.date;
            JalyuktaVisitDetail.Text = Data.description.Replace("<p>", "").Replace("</p>", ""); ;
            JalyuktaDetailImage.HeightRequest = App.DeviceHeight * 0.40;
            JalyuktaDetailImageBackgrnd.HeightRequest = App.DeviceHeight * 0.40;
			setfontsize();
        }
    }
}
