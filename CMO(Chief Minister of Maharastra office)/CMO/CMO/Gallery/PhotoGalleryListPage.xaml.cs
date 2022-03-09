using CMO.MenuController;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CMO.Gallery
{
    public partial class PhotoGalleryListPage : ContentPage
    {
        PopupLayout popupLayout;
        ObservableCollection<PhotoGalleryItemClass> PHOTOSLIST = new ObservableCollection<PhotoGalleryItemClass>();
        ObservableCollection<CMO.ServicesClasses.Photo> PopupPhotoList = new ObservableCollection<ServicesClasses.Photo>();
        int widthdevice;
        int deviceheight;
        public string SearchFilterText = "";
        int index = 0;
        int totalItems = 0;
        int totalListItems = 0;
        decimal MaxIndex = 0;
        bool isLoading = false;
        string flagvalue = null;
        ObservableCollection<PhotoGalleryItemClass> PhotoList;
		public PhotoGalleryListPage()
		{
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
			lblNoRecord.FontSize = App.GetFontSizeMedium();
			FilterPhotoGallerytList.FontSize = App.GetFontSizeMedium();
           // index = 0;
            widthdevice = App.DeviceWidth;
            deviceheight = App.DeviceHeight;
            if (TargetPlatform.Android == Device.OS)
            {
           //     EntryOuterStack.Padding = new Thickness(5, 7, 0, 0);
            }
            FilterPhotoGallerytList.Placeholder = AppResources.LSearch;
            this.Title = AppResources.LMenuPhotoGallery.ToUpper();
            //if (!isLoading)
            //{
            //    index = 0;
            //    Loading(true);
            //    CallWebServiceForPhotoGalleryList();
            //}
            PhotoGalleryList.ItemTapped += PhotoGalleryList_ItemTapped;
            PhotoGalleryList.ItemAppearing += PhotoGalleryList_ItemAppering1;
            //  firstrow.Height = (deviceheight * 6.5) / 100;
            //CallWebServiceForPhotoGalleryList(index);
            PhotoGalleryList.Refreshing += PhotoGalleryList_Refreshing;
        }

        private void PhotoGalleryList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var x = sender as ListView;
            x.SelectedItem = null;
        }
        private async void PhotoGalleryList_Refreshing(object sender, EventArgs e)
        {
            index = 0;
            loadingIndicator.IsVisible = false;
            loading.IsVisible = false;
            loading.IsRunning = false;
            if (lblNoRecord.IsVisible == true)
            {
                SearchFilterText = "";
                FilterPhotoGallerytList.Text = "";
            }
           await CallWebServiceForPhotoGalleryList();
        }
        protected async override void OnAppearing()
        {
            
            Application.Current.Properties["CurrentPage"] = "photogallerylist";
            if (Application.Current.Properties.ContainsKey("navigationflag"))
            {
                flagvalue = Application.Current.Properties["navigationflag"] as string;
                if (flagvalue == "0")
                {
                    if (!App.CheckInternetConnection())
                    {
                        Loading(false);
                        await DisplayAlert(AppResources.LNetworkError, AppResources.LNoInternetConnection, AppResources.LOk);
                    }
                    else
                    {
                        index = 0;
                        loadingIndicator.IsVisible = true;
                        loading.IsRunning = true;
                        loading.IsVisible = true;
                        if (!isLoading)
                        {
                            index = 0;
                            Loading(true);
                           await CallWebServiceForPhotoGalleryList();
                        }
                    }
                }
            }
        }
        private void Loading(bool load)
        {
            loadingIndicator.IsVisible = load;
            loading.IsRunning = load;
            loading.IsVisible = load;
            isLoading = load;
            if (load == false)
            {
                PhotoGalleryList.IsRefreshing = false;
            }
        }
        private async void PhotoGalleryList_ItemAppering1(object sender, ItemVisibilityEventArgs e)
        {
            try
            {
                    MaxIndex = Math.Ceiling(((decimal)totalListItems) / 6);
                    if (index < MaxIndex && !isLoading)
                    {
                        if (PhotoList != null && e.Item != null && e.Item == PhotoList[PhotoList.Count - 1])
                        {
                            Loading(true);
                           await CallWebServiceForPhotoGalleryList();
                        }
                    }
         
                //if (totalItems != 0)
                //{
                //    MaxIndex = Math.Ceiling(((decimal)totalListItems) / 10);
                //    if (index < MaxIndex)
                //    {
                //        if (PhotoList != null && e.Item != null && e.Item == PhotoList[PhotoList.Count - 1])
                //        {
                //            index++;
                //            if (index != MaxIndex)
                //            {
                //                loadingIndicator.IsVisible = true;
                //                loading.IsVisible = true;
                //                loading.IsRunning = true;
                //                CallWebServiceForPhotoGalleryList(index);
                //            }
                //        }
                //    }
                //}

            }
            catch (Exception ex)
            {
				if (App.CurrentPage() == "photogallerylist")
				{
					if (ex.Message.Contains("Network"))
						await DisplayAlert(AppResources.LError, AppResources.LWebserverNotResponding, AppResources.LOk);
				}
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
                    if (FilterPhotoGallerytList.Text == "" || FilterPhotoGallerytList.Text == null)
                    {
                        SearchFilterText = "";
                    }
                    else
                    {
                        SearchFilterText = FilterPhotoGallerytList.Text;
                    }
                    index = 0;
                    Loading(true);
                    PhotoList.Clear();
                  await  CallWebServiceForPhotoGalleryList();
                }
            }
        }
        private void LeftPhotoTapped(object sender, EventArgs e)
        {
            var StackID = sender as Grid;
            var obj = StackID.ClassId;
            if (obj != null)
            {
                var stack = Navigation.NavigationStack;
                if (stack[stack.Count - 1].GetType() != typeof(PhotosListDetail))
                {
                    if (PHOTOSLIST != null)
                    {
                        for (int i = 0; i < PHOTOSLIST.Count; i++)
                        {
                            if (PHOTOSLIST[i].leftId == obj)
                            {
                                PopupPhotoList = new ObservableCollection<ServicesClasses.Photo>();
                                PopupPhotoList = PHOTOSLIST[i].photosLeft;
                                break;
                            }
                        }
                    }
                    //  Navigation.PushAsync(new PhotosListDetail(PopupPhotoList));
                    Application.Current.Properties["navigationflag"] = "1";
                    Navigation.PushAsync(new PhotosListDetail(obj));
                }
            }

        }
        private void RightPhotoTapped(object sender, EventArgs e)
        {
            var StackID = sender as Grid;
            var obj = StackID.ClassId;
            if (obj != null)
            {
                var stack = Navigation.NavigationStack;
                if (stack[stack.Count - 1].GetType() != typeof(PhotosListDetail))
                {
                    if (PHOTOSLIST != null)
                    {
                        for (int i = 0; i < PHOTOSLIST.Count; i++)
                        {
                            if (PHOTOSLIST[i].rightId != null)
                            {
                                if (PHOTOSLIST[i].rightId == obj)
                                {
                                    PopupPhotoList = new ObservableCollection<ServicesClasses.Photo>();
                                    PopupPhotoList = PHOTOSLIST[i].photosRight;
                                    break;
                                }
                            }
                        }
                    }
                    Application.Current.Properties["navigationflag"] = "1";
                    Navigation.PushAsync(new PhotosListDetail(obj));
                }
            }
            //popupview(obj);
        }
        public void popupview(string id)
        {
            popupLayout = this.Content as PopupLayout;

            if (popupLayout.IsPopupActive)
            {
                popupLayout.DismissPopup();
            }
            else
            {
                StackLayout MainStack = new StackLayout()
                {
                    BackgroundColor = Color.White,
                    HeightRequest = this.Height * .5,
                    WidthRequest = this.Width * .8,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    Padding = new Thickness(5, 5, 5, 5)
                };
                StackLayout ImageStack = new StackLayout() { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center };

                if (PHOTOSLIST != null)
                {
                    for (int i = 0; i < PHOTOSLIST.Count; i++)
                    {
                        if (PHOTOSLIST[i].leftId == id)
                        {
                            PopupPhotoList = new ObservableCollection<ServicesClasses.Photo>();
                            PopupPhotoList = PHOTOSLIST[i].photosLeft;
                        }
                        if (PHOTOSLIST[i].rightId != null)
                        {
                            if (PHOTOSLIST[i].rightId == id)
                            {
                                PopupPhotoList = new ObservableCollection<ServicesClasses.Photo>();
                                PopupPhotoList = PHOTOSLIST[i].photosLeft;
                            }
                        }
                    }
                }

                Image ImageContent = new Image()
                {
                    Aspect = Aspect.Fill,
                    HeightRequest = this.Height * .4,
                    WidthRequest = this.Width * .8
                };

                #region bottom stack

                StackLayout BottomStack = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                    BackgroundColor = Color.White,
                    HeightRequest = this.Height * .1,
                    WidthRequest = this.Width * .8,
                    VerticalOptions = LayoutOptions.Center
                };
                Label ImageNumber = new Label() { BackgroundColor = Color.White, TextColor = Color.FromHex("#09091a"), VerticalOptions = LayoutOptions.Center };

                Image LeftArrow = new Image() { Source = "ico_down_arrow2_right.png", VerticalOptions = LayoutOptions.Center };
                Image RightArrow = new Image() { Source = "ico_down_arrow2_right.png", VerticalOptions = LayoutOptions.Center };
                int PhotoCountValue = 0;
                ImageContent.Source = PopupPhotoList[PhotoCountValue].photo_path;
                ImageNumber.Text = PhotoCountValue + " of " + PopupPhotoList.Count;
                var LeftArrowTap = new TapGestureRecognizer(); LeftArrowTap.Tapped += (object sender, EventArgs e) =>
                {
                    if (PhotoCountValue <= 0) { }
                    else
                    {
                        PhotoCountValue = PhotoCountValue - 1;
                        ImageContent.Source = PopupPhotoList[PhotoCountValue].photo_path;
                        ImageNumber.Text = PhotoCountValue + " of " + PopupPhotoList.Count;
                    }
                };
                var RightArrowTap = new TapGestureRecognizer(); RightArrowTap.Tapped += (object sender, EventArgs e) =>
                {
                    if (PhotoCountValue >= PopupPhotoList.Count) { }
                    else
                    {
                        PhotoCountValue = PhotoCountValue + 1;
                        ImageContent.Source = PopupPhotoList[PhotoCountValue].photo_path;
                        ImageNumber.Text = PhotoCountValue + " of " + PopupPhotoList.Count;
                    }
                };
                LeftArrow.GestureRecognizers.Add(LeftArrowTap);
                RightArrow.GestureRecognizers.Add(RightArrowTap);
                BottomStack.Children.Add(LeftArrow);
                BottomStack.Children.Add(RightArrow);
                BottomStack.Children.Add(ImageNumber);

                #endregion

                ImageStack.Children.Add(ImageContent);
                ImageStack.Children.Add(BottomStack);

                MainStack.Children.Add(ImageStack);

                popupLayout.ShowPopup(MainStack);
            }
        }
        public async Task CallWebServiceForPhotoGalleryList()
        {
            if (!App.CheckInternetConnection())
            {
                Loading(false);
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
                    values.Add(new KeyValuePair<string, string>("index", Convert.ToString(index)));
                    values.Add(new KeyValuePair<string, string>("limit", "8"));

                  //  var response = await GeneralClass.GetResponse<CMO.ServicesClasses.RootObjectPhotoGalleryList>("http://14.141.36.212/maharastracmo/api/getphotogallerylist", values);
                  //  var response = await GeneralClass.GetPlacesAsync<CMO.ServicesClasses.RootObjectPhotoGalleryList>("http://14.141.36.212/maharastracmo/api/getphotogallerylist", values);
                    var response = await GeneralClass.GetResponse<CMO.ServicesClasses.RootObjectPhotoGalleryList>("http://14.141.36.212/maharastracmo/api/getphotogallerylist", values);
                    if (response != null)
                    {
                        if (response._resultflag != 0)
                        {
                            if (PhotoList == null || index == 0)
                            {
                                PhotoList = new ObservableCollection<PhotoGalleryItemClass>();
                            }

                            index++;

                            totalItems = response.total_results;
                            totalListItems = response.search_results;
                            for (int i = 0; i < response.gallery_list.Count; i++)
                            {
                                var photoitems = new PhotoGalleryItemClass();
								photoitems.SetFontSize = App.GetFontSizeMedium();
                                photoitems.photosLeft = response.gallery_list[i].photo;
                                photoitems.leftId = response.gallery_list[i].page_id;
                                //photoitems.leftImageUrl = response.gallery_list[i].photo[0].photo_path;
                                photoitems.leftImageUrl = new UriImageSource { CachingEnabled = true, Uri = new Uri(response.gallery_list[i].photo[0].thumb_path) };
                                if (Device.Idiom == TargetIdiom.Tablet)
                                {
                                    photoitems.leftImageUrl = new UriImageSource { CachingEnabled = true, Uri = new Uri(response.gallery_list[i].photo[0].ipad_thumb_path) };
                                }
                                photoitems.leftTitle = response.gallery_list[i].title.ToUpper();
                                photoitems.ColorStackBorderright = Color.White;
                                photoitems.background = Xamarin.Forms.Color.FromHex("#132561");
                                //   countryitems.ColorStackBorderleft = Color.White;
                                i++;
                                if (i < response.gallery_list.Count)
                                {
                                    photoitems.photosRight = response.gallery_list[i].photo;
                                    photoitems.rightId = response.gallery_list[i].page_id;
                                    //photoitems.rightImageUrl = response.gallery_list[i].photo[0].photo_path;
                                    photoitems.rightImageUrl = new UriImageSource { CachingEnabled = true, Uri = new Uri(response.gallery_list[i].photo[0].thumb_path) };
                                    if (Device.Idiom == TargetIdiom.Tablet)
                                    {
                                        photoitems.rightImageUrl = new UriImageSource { CachingEnabled = true, Uri = new Uri(response.gallery_list[i].photo[0].ipad_thumb_path) };
                                    }
                                    photoitems.PlaceholderRight = "photogalleryplaceholder.png";
                                    photoitems.rightTitle = response.gallery_list[i].title.ToUpper();
                                    photoitems.ColorStackBorderright = Color.White;
                                    photoitems.background = Xamarin.Forms.Color.FromHex("#132561");
                                }
                                else
                                {
                                    photoitems.photosRight = null;
                                    photoitems.rightId = null;
                                    photoitems.rightImageUrl = null;
                                    photoitems.rightTitle = null;
                                    photoitems.PlaceholderRight = null;
                                    photoitems.ColorStackBorderright = Color.FromHex("#09091a");
                                    photoitems.background = Xamarin.Forms.Color.FromHex("#000000");
                                 //   photoitems.backgroundleft= Xamarin.Forms.Color.FromHex("#132561");
                                    PhotoList.Add(photoitems);
                                    break;
                                }
                                photoitems.ColorStackBorderleft = Color.White;
                               // photoitems.backgroundleft = Xamarin.Forms.Color.FromHex("#132561");
                                PhotoList.Add(photoitems);
                            }
                            //if (PhotoGalleryList.IsVisible == false)
                            //{
                            //    PhotoGalleryList.IsVisible = true;
                            //}
                            PHOTOSLIST = PhotoList;
                            PhotoGalleryList.ItemsSource = PhotoList;
                            //  PhotoGalleryList.RowHeight = widthdevice / 2;
                            if (App.DeviceHeight > 2000)
                            {
                                PhotoGalleryList.RowHeight = 2*153;
                            }
                            else
                            {
                                PhotoGalleryList.RowHeight = 153;
                            }
                            if (Device.Idiom == TargetIdiom.Tablet)
                            { PhotoGalleryList.RowHeight = 2 * 153; }
                                PhotoGalleryList.IsRefreshing = false;
                           // PhotoGalleryList.IsVisible = true;
                        }
                        else
                        {
                            if (index == 0)
                            {
                                lblNoRecord.IsVisible = true;
                                PhotoList.Clear();
                                //PhotoGalleryList.IsVisible = false;
                                PhotoGalleryList.IsRefreshing = false;
                            }
                        }
                    }
                    else
                    {
						if (App.CurrentPage() == "photogallerylist")
                        await DisplayAlert(AppResources.LError, AppResources.LSomethingWentWrong, AppResources.LOk);
					}
                }

                catch (WebException exception)
                {
					if (App.CurrentPage() == "photogallerylist")
					{
						if (exception.Message.Contains("Network"))
							await DisplayAlert(AppResources.LError, AppResources.LWebserverNotResponding, AppResources.LOk);
					}
                }
              //  Loading(false);
            }
            Loading(false);
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
    public class WrappedTruncatedLabel : Label
    {
    }
    public class PhotoGalleryItemClass
    {
        public string leftId { get; set; }
        public UriImageSource leftImageUrl { get; set; }
        public string leftTitle { get; set; }
        public ObservableCollection<CMO.ServicesClasses.Photo> photosLeft { get; set; }
        public Color ColorStackBorderleft { get; set; }

        public string rightId { get; set; }
        public UriImageSource rightImageUrl { get; set; }
        public string rightTitle { get; set; }
        public ObservableCollection<CMO.ServicesClasses.Photo> photosRight { get; set; }
        public Color ColorStackBorderright { get; set; }

        public Xamarin.Forms.Color background { get; set; }
        public ImageSource PlaceholderRight { get; set; } 
     //   public Color backgroundleft { get; set; }
		public int SetFontSize { get; set; }
    }
}
