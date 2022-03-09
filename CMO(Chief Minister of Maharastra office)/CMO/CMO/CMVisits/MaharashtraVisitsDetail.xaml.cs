using CMO.Gallery;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CMO.CMVisits
{
    public partial class MaharashtraVisitsDetail : ContentPage
    {
        CMO.ServicesClasses.CmVisit _data = new CMO.ServicesClasses.CmVisit();
        ObservableCollection<CMO.ServicesClasses.Photo> SelectedItemPhotoList = new ObservableCollection<CMO.ServicesClasses.Photo>();

        public MaharashtraVisitsDetail()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
            //this.Title = "Maharashtra Visits Detail";
			setfontsize();
        }
		public void setfontsize()
		{
			MaharashtraVisittitle.FontSize = App.GetFontSizeMedium();
			MaharashtraVisitDate.FontSize = App.GetFontSizeSmall();
			MaharashtraVisitDetail.FontSize = App.GetFontSizeSmall();
			MaharashtraVisitPhoto.FontSize = App.GetFontSizeMedium();
			MaharashtraVisitVideo.FontSize = App.GetFontSizeMedium();
			                     
		}
        public MaharashtraVisitsDetail(CMO.ServicesClasses.CmVisit Data)
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
            MaharashtraVisitVideo.Text = AppResources.LVideos;
            MaharashtraDetailImage.HeightRequest = App.DeviceHeight * 0.40;
            MaharashtraDetailImageBackgrnd.HeightRequest = App.DeviceHeight * 0.40;
            Title = Data.title.ToUpper();
            MaharashtraDetailImage.Source = Data.image;
            MaharashtraVisittitle.Text = Data.title;
            MaharashtraVisitDate.Text = Data.date;
            MaharashtraVisitDetail.Text = Data.description.Replace("<p>", "").Replace("</p>", "");
            if (Data.photo_gallery_id != "")
            {
                MaharashtraVisitPhoto.Text = AppResources.LPhotos;
                imgIco.IsVisible = true;
                imgIco.IsEnabled = true;
                MaharashtraVisitPhoto.IsVisible = true;
                MaharashtraVisitPhoto.IsEnabled = true;
            }
            else
            {
                imgIco.IsVisible = false;
                imgIco.IsEnabled = false;
                MaharashtraVisitPhoto.IsEnabled = false;
                MaharashtraVisitPhoto.IsVisible = false;
            }
            if(Data.video_gallery_id!="" && Data.video_gallery_id!=null)
            {
                VideoGrid.IsVisible = true;
                VideoGrid.IsEnabled = true;
            }
            else
            {
                VideoGrid.IsVisible = false;
                VideoGrid.IsEnabled = false;
            }
            _data = Data;
			setfontsize();
            CallWebServiceForPhotoGalleryList(_data.title);
           
        }
        public async Task CallWebServiceForPhotoGalleryList(string MaharashtraVisitPhotoTitle)
        {
            if (!App.CheckInternetConnection())
            {
                await DisplayAlert(AppResources.LNetworkError, AppResources.LNoInternetConnection, AppResources.LOk);
            }
            else
            {
                try
                {
                    string lang = "";
                    if (Application.Current.Properties.ContainsKey("Language"))
                    { lang = Application.Current.Properties["Language"] as string; }

                    List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>();
                    values.Add(new KeyValuePair<string, string>("lang", lang));
                    values.Add(new KeyValuePair<string, string>("title", AppResources.LMaharashtraVisitPhotos));
                    values.Add(new KeyValuePair<string, string>("index", "0"));
                    values.Add(new KeyValuePair<string, string>("limit", "10"));

                    var response = await GeneralClass.GetResponse<CMO.ServicesClasses.RootObjectPhotoGalleryList>("http://14.141.36.212/maharastracmo/api/getphotogallerylist", values);
                    if (response != null)
                    {
                        if (response._resultflag == 1)
                        {
                            if (response.gallery_list[0].photo != null)
                            {
                                SelectedItemPhotoList = new ObservableCollection<CMO.ServicesClasses.Photo>();
                                SelectedItemPhotoList = response.gallery_list[0].photo;
                                imgIco.IsVisible = true;
                                imgIco.IsEnabled = true;
                                MaharashtraVisitPhoto.IsVisible = true;
                                MaharashtraVisitPhoto.IsEnabled = true;
                                MaharashtraVisitPhoto.Text = AppResources.LPhotos;
                                loading.IsRunning = false;
                                MainStack1.IsVisible = true;
								MainStack2.IsVisible = true;
                                //    await Navigation.PushAsync(new PhotosListDetail(response.gallery_list[0].photo));
                            }
                            else
                            {
                                MainStack1.IsVisible = true;
								MainStack2.IsVisible = true;
                                loading.IsRunning = false;
                            }
                        }
                        else
                        {
                            MainStack1.IsVisible = true;
							MainStack2.IsVisible = true;
                            loading.IsRunning = false;
                            //   await DisplayAlert("Error", App.responseflagMessage, "Ok");
                        }
                    }
                }

                catch (WebException exception)
                {
					if (App.CurrentPage() == "maharashtravisits")
					{
						if (exception.Message.Contains("Network"))
							await DisplayAlert(AppResources.LError, AppResources.LWebserverNotResponding, AppResources.LOk);
					}
                }
            }

        }
        public async void TapPhotoImage(object sender, EventArgs args)
        {
            if (SelectedItemPhotoList != null)
            {
                await Navigation.PushAsync(new PhotosListDetail(SelectedItemPhotoList));
            }
        }
        public void TapVideoImage(object sender, EventArgs args)
        {
            var uri = new Uri(_data.video_gallery_id);
            Device.OpenUri(uri);
        }
    }
}
