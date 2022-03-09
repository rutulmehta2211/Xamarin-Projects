using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CMO.Gallery
{
    public partial class NewsGalleryDetail : ContentPage
    {
        CMO.ServicesClasses.Newslist _data = new CMO.ServicesClasses.Newslist();
        public NewsGalleryDetail()
        {
            InitializeComponent();
			setfontsize();
            NavigationPage.SetBackButtonTitle(this, "");
        }

		public void setfontsize()
		{
			NewsGallerytitle.FontSize = App.GetFontSizeMedium();
			NewsGalleryDate.FontSize = App.GetFontSizeSmall();
			NewsGalleryDetailLabel.FontSize = App.GetFontSizeSmall();
		}

		public NewsGalleryDetail(CMO.ServicesClasses.Newslist Data)
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
            this.Title = Data.title.ToUpper();
            NewsGalleryImage.HeightRequest = App.DeviceHeight * 0.40;
            NewsGalleryImageBackgrnd.HeightRequest = App.DeviceHeight * 0.40;
            NewsGalleryImage.Source = Data.large_news_photo;
            NewsGallerytitle.Text = Data.title;
            NewsGalleryDate.Text = Data.date;
            string _detailData= Data.content.Replace("<p>", "").Replace("</p>", "");
            NewsGalleryDetailLabel.Text = _detailData.Replace("&amp;", "&");
            //No need to put photoes and videos buttons
            //NewsGalleryPhoto.Text = AppResources.LPhotos;
            //NewsGalleryVideo.Text = AppResources.LPhotos;
			setfontsize();
        }
        //No need to put photoes and videos buttons
        //public void TapPhotoImage(object sender, EventArgs args)
        //{
        //    CallWebServiceForPhotoGalleryList(_data.title);
        //}

        //No need to put photoes and videos buttons
        //public async void CallWebServiceForPhotoGalleryList(string MaharashtraVisitPhotoTitle)
        //{
        //    try
        //    {
        //        string lang = "en";

        //        if (Application.Current.Properties.ContainsKey("Language"))
        //        {
        //            lang = Application.Current.Properties["Language"] as string;
        //            // do something with i
        //        }
        //        List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>();
        //        values.Add(new KeyValuePair<string, string>("lang", lang));
        //        values.Add(new KeyValuePair<string, string>("title", MaharashtraVisitPhotoTitle));
        //        values.Add(new KeyValuePair<string, string>("index", "0"));
        //        values.Add(new KeyValuePair<string, string>("limit", "10"));
        //        var response = await GeneralClass.GetResponse<CMO.ServicesClasses.RootObjectPhotoGalleryList>("http://14.141.36.212/maharastracmo/api/getphotogallerylist", values);
        //        if (response != null)
        //        {
        //            await Navigation.PushAsync(new PhotosListDetail(response.gallery_list[0].photo));
        //        }
        //        else
        //        {
        //            await DisplayAlert("Error", App.responseflagMessage, "Ok");
        //        }
        //    }

        //    catch (WebException exception)
        //    {
        //        await DisplayAlert("Error", App.CatchMessage, "OK");
        //    }
        //}

    }
}
