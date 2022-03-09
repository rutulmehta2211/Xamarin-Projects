using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CMO.CMVisits
{
    public partial class EventsDetail : ContentPage
    {
        public EventsDetail()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
			setfontsize();

        }
		public void setfontsize()
		{
			EventsVisittitle.FontSize = App.GetFontSizeMedium();
			EventsVisitDate.FontSize = App.GetFontSizeSmall();
			EventsVisitDetail.FontSize = App.GetFontSizeSmall();
		}
        protected override void OnAppearing()
        {
            var x = Navigation.NavigationStack.Count;
        }
        public EventsDetail(CMO.ServicesClasses.Event Data)
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
            Title = Data.title.ToUpper();
            EventsDetailImage.Source = Data.image;
            EventsVisittitle.Text = Data.title;
            EventsVisitDate.Text = Data.event_start_date;
            EventsVisitDetail.Text = Data.description.Replace("<p>", "").Replace("</p>", "");
            EventsDetailImage.HeightRequest = App.DeviceHeight * 0.40;
            EventsDetailImageBackgrnd.HeightRequest = App.DeviceHeight * 0.40;
			setfontsize();
        }
    }
}
