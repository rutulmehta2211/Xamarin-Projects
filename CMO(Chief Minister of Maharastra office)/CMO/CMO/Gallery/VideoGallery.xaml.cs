using CMO.MenuController;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CMO.Gallery
{
    public partial class VideoGallery : ContentPage
    {
        int widthdevice;
        int deviceheight;
        public string SearchFilterText = "";
        int index = 0;
        int ItemListcount = 0;
        int totalItems = 0;
        int totalListItems = 0;
        decimal MaxIndex = 0;

        ObservableCollection<VideoGalleryItemClass> VideoList;
        public VideoGallery()
        {
            InitializeComponent();
			lblNoRecord.FontSize = App.GetFontSizeMedium();
			FilterVideoGallerytList.FontSize = App.GetFontSizeMedium();
            NavigationPage.SetBackButtonTitle(this, "");
            widthdevice = App.DeviceWidth;
            deviceheight = App.DeviceHeight;
            if (TargetPlatform.Android == Device.OS)
            {
              //  EntryOuterStack.Padding = new Thickness(5, 7, 0, 0);
            }
            FilterVideoGallerytList.Placeholder = AppResources.LSearch;
            this.Title = AppResources.LMenuVideoGallery.ToUpper();
          //  firstrow.Height = (deviceheight * 6.5) / 100;
            VideoGalleryList.ItemAppearing += VideoGalleryList_ItemAppearing1;
            //loadingIndicator.IsVisible = false;
           // CallWebServiceForVideoGalleryList(index);
            VideoGalleryList.Refreshing += VideoGalleryList_Refreshing;
        }

        private async void VideoGalleryList_Refreshing(object sender, EventArgs e)
        {
            index = 0;
            loadingIndicator.IsVisible = false;
            loading.IsVisible = false;
            loading.IsRunning = false;
            if (lblNoRecord.IsVisible == true)
            {
                SearchFilterText = "";
                FilterVideoGallerytList.Text = "";
            }
           await CallWebServiceForVideoGalleryList(index);
        }
        private async void VideoGalleryList_ItemAppearing1(object sender, ItemVisibilityEventArgs e)
        {
            try
            {
                if (totalItems != 0)
                {
                    MaxIndex = Math.Ceiling(((decimal)totalListItems) / 10);
                    if (index < MaxIndex)
                    {
                        if (VideoList != null && e.Item != null && e.Item == VideoList[VideoList.Count - 1])
                        {
                            index++;
                            if (index != MaxIndex)
                            {
                                loadingIndicator.IsVisible = true;
                                loading.IsVisible = true;
                                loading.IsRunning = true;
                                await CallWebServiceForVideoGalleryList(index);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
				if (App.CurrentPage() == "videogallery")
                await DisplayAlert(AppResources.LError,AppResources.LWebserverNotResponding, AppResources.LOk);
            }
        }

        public async void TapSearchImage(object sender, EventArgs args)
        {
            lblNoRecord.IsVisible = false;
            if (loading.IsRunning == false)
            {
                if (!App.CheckInternetConnection())
                {
                    await DisplayAlert(AppResources.LNetworkError, AppResources.LNoInternetConnection, AppResources.LOk);
                }
                else
                {
                    if (FilterVideoGallerytList.Text == "" || FilterVideoGallerytList.Text == null)
                    {
                        SearchFilterText = "";
                    }
                    else
                    {
                        SearchFilterText = FilterVideoGallerytList.Text;
                    }
                    index = 0;
                    loadingIndicator.IsVisible = true;
                    loading.IsVisible = true;
                    loading.IsRunning = true;
                    VideoList.Clear();
                   await CallWebServiceForVideoGalleryList(index);
                }
            }
        }
        protected async override void OnAppearing()
        {
            Application.Current.Properties["CurrentPage"] = "videogallery";
            if (Application.Current.Properties.ContainsKey("navigationflag"))
            {

                var  flagvalue = Application.Current.Properties["navigationflag"] as string;
                if (flagvalue == "0")
                {
                    if (!App.CheckInternetConnection())
                    {
                        loadingIndicator.IsVisible = false;
                        loading.IsRunning = false;
                        loading.IsVisible = false;
                        await DisplayAlert(AppResources.LNetworkError, AppResources.LNoInternetConnection, AppResources.LOk);
                    }
                    else
                    {
                        index = 0;
                        loadingIndicator.IsVisible = true;
                        loading.IsRunning = true;
                        loading.IsVisible = true;
                       await CallWebServiceForVideoGalleryList(index);
                    }
                }
            }
        }
        private void LeftVideoTapped(object sender, EventArgs e)
        {
            var VideoID = sender as Grid;
            if (VideoID.ClassId != null)
            {
                var uri = new Uri(VideoID.ClassId);
                Device.OpenUri(uri);
            }
        }
        private void RightVideoTapped(object sender, EventArgs e)
        {
            var VideoID = sender as Grid;
            if (VideoID.ClassId != null)
            {
                var uri = new Uri(VideoID.ClassId);
                Device.OpenUri(uri);
            }
        }
        public async Task CallWebServiceForVideoGalleryList(int index)
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
                    values.Add(new KeyValuePair<string, string>("title", SearchFilterText));
                    //values.Add(new KeyValuePair<string, string>("title", ""));
                    values.Add(new KeyValuePair<string, string>("index", Convert.ToString(index)));
                    values.Add(new KeyValuePair<string, string>("limit", "10"));

                    var response = await GeneralClass.GetResponse<CMO.ServicesClasses.RootObjectVideoGalleryList>("http://14.141.36.212/maharastracmo/api/getvideoslist", values);
                    if (response != null)
                    {
                        if (response._resultflag != 0)
                        {
                            if (VideoList == null || index == 0)
                            {
                                VideoList = new ObservableCollection<VideoGalleryItemClass>();
                            }
                            totalItems = response.total_results;
                            totalListItems = response.search_results;
                            for (int i = 0; i < response.videolist.Count; i++)
                            {
                                var Videoitems = new VideoGalleryItemClass();
								Videoitems.SetFontSize = App.GetFontSizeMedium();
                                Videoitems.left_page_id = response.videolist[i].page_id;
                                Videoitems.left_title = response.videolist[i].title.ToUpper();
                                Videoitems.left_video_thumb_path = response.videolist[i].video_thumb_path;
                                if (Device.Idiom == TargetIdiom.Tablet)
                                {
                                    Videoitems.left_video_thumb_path = response.videolist[i].ipad_thumb_path;
                                }
                                Videoitems.left_video_url = response.videolist[i].video_url;
                                Videoitems.left_content = response.videolist[i].content;
                                Videoitems.ColorStackBorderright = Color.White;
                                Videoitems.background = Xamarin.Forms.Color.FromHex("#132561");

                                i++;
                                if (i < response.videolist.Count)
                                {
                                    Videoitems.right_page_id = response.videolist[i].page_id;
                                    Videoitems.right_title = response.videolist[i].title.ToUpper();
                                    Videoitems.right_video_thumb_path = response.videolist[i].video_thumb_path;
                                    Videoitems.right_video_url = response.videolist[i].video_url;
                                    if (Device.Idiom == TargetIdiom.Tablet)
                                    {
                                        Videoitems.right_video_thumb_path = response.videolist[i].ipad_thumb_path;
                                    }
                                    Videoitems.PlaceholderRight = "photogalleryplaceholder.png";
									Videoitems.playright = "play";
                                    Videoitems.right_content = response.videolist[i].content;
                                    Videoitems.ColorStackBorderright = Color.White;
                                    Videoitems.background = Xamarin.Forms.Color.FromHex("#132561");
                                }
                                else
                                {
                                    Videoitems.right_page_id = 0;
                                    Videoitems.right_title = null;
                                    Videoitems.right_video_thumb_path = null;
                                    Videoitems.right_video_url = null;
                                    Videoitems.PlaceholderRight = null;
									Videoitems.playright = null;
                                    Videoitems.right_content = null;
                                    Videoitems.ColorStackBorderright = Color.FromHex("#09091a");
                                    Videoitems.background = Xamarin.Forms.Color.FromHex("#000000");
                                    VideoList.Add(Videoitems);
                                    break;
                                }
                                //Videoitems.title = response.videolist[i].title;
                                Videoitems.ColorStackBorderleft = Color.White;
                                VideoList.Add(Videoitems);
                            }
                            //if (VideoGalleryList.IsVisible == false)
                            //{
                            //    VideoGalleryList.IsVisible = true;
                            //}
                            //List<VideoGalleryItemClass> item = new List<VideoGalleryItemClass>();
                            //if (!string.IsNullOrEmpty(SearchFilterText))
                            //{
                            //    item = VideoList.Where(a => a.title.Contains(SearchFilterText)).ToList();

                            //}
                            //else
                            //{
                            //    item = VideoList.ToList();
                            //}
                            VideoGalleryList.ItemsSource = VideoList;
                            VideoGalleryList.IsRefreshing = false;
                            //  VideoGalleryList.IsVisible = true;
                            //   VideoGalleryList.RowHeight = widthdevice / 2;
                            if (App.DeviceHeight > 2000)
                            {
                                VideoGalleryList.RowHeight = 2 * 153;
                            }
                            else
                            {
                                VideoGalleryList.RowHeight = 153;
                            }
                            if (Device.Idiom == TargetIdiom.Tablet)
                            { VideoGalleryList.RowHeight = 2 * 153; }
                            ItemListcount = VideoList.Count;
                        }
                        else
                        {
                            lblNoRecord.IsVisible = true;
                            VideoList.Clear();
                           // VideoGalleryList.IsVisible = false;
                        }
                    }
                    else
                    {
						if (App.CurrentPage() == "videogallery")
                        await DisplayAlert(AppResources.LError, AppResources.LSomethingWentWrong, AppResources.LOk);
                    }
                }

                catch (WebException exception)
                {
					if (App.CurrentPage() == "videogallery")
					{
						if (exception.Message.Contains("Network"))
							await DisplayAlert(AppResources.LError, AppResources.LWebserverNotResponding, AppResources.LOk);
					}
                }
            }
                loading.IsVisible = false;
                loading.IsRunning = false;
                loadingIndicator.IsVisible = false;
                VideoGalleryList.IsRefreshing = false;
        }

        #region Exit Application
        private bool _canClose = true;

        protected override bool OnBackButtonPressed()
        {
            //if (_canClose)
            //{
            //    ShowExitDialog();
            //}
            //return _canClose;
            Application.Current.MainPage = new SideMenu();
            return true;
        }

        private async void ShowExitDialog()
        {
            var answer = await DisplayAlert(AppResources.LExit, AppResources.LExitApp,AppResources.LYes,AppResources.LNo);
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

    public class VideoGalleryItemClass
    {
        public int left_page_id { get; set; }
        public string left_title { get; set; }
        public string left_video_thumb_path { get; set; }
        public string left_video_url { get; set; }
        public string left_content { get; set; }
        public Color ColorStackBorderleft { get; set; }

        public int right_page_id { get; set; }
        public string right_title { get; set; }
        public string right_video_thumb_path { get; set; }
        public string right_video_url { get; set; }
        public string right_content { get; set; }
        public Color ColorStackBorderright { get; set; }

        public string title { get; set; }

        public Xamarin.Forms.Color background { get; set; }
        public ImageSource PlaceholderRight { get; set; }
		public ImageSource playright { get; set;}
		public int SetFontSize { get; set; }
    }
}
