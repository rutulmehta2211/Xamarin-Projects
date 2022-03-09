
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

    public partial class PhotosListDetail : ContentPage
    {
        PopupLayout popupLayout;
        ObservableCollection<PhotoDisplay> PhotoDisplayList = new ObservableCollection<PhotoDisplay>();
        ObservableCollection<CMO.ServicesClasses.Photo> PopupPhotoList = new ObservableCollection<ServicesClasses.Photo>();
        int widthdevice;
        public PhotosListDetail(string pageid)
        {
            InitializeComponent();
			NavigationPage.SetBackButtonTitle(this, "");
            this.Title = AppResources.LPhotos.ToUpper();
            widthdevice = App.DeviceWidth;
            PhotoDetailList.ItemTapped += PhotoDetailList_ItemTapped;
            CallWebServiceForPhotoGalleryList(pageid);
        }
        protected override void OnAppearing()
        {
           var x = Navigation.NavigationStack.Count;
        }
        private void PhotoDetailList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var x = sender as ListView;
            x.SelectedItem = null;
        }
        public PhotosListDetail(ObservableCollection<CMO.ServicesClasses.Photo> _popupPhotoList)
        {
            InitializeComponent();
			NavigationPage.SetBackButtonTitle(this, "");
            PopupPhotoList = _popupPhotoList;
            this.Title = AppResources.LPhotos.ToUpper();
            widthdevice = App.DeviceWidth;
            int a = 0;
            PhotoDetailList.ItemTapped += PhotoDetailList_ItemTapped;
            /*loadingIndicator.IsVisible = true;
            loading.IsRunning = true;
            loading.IsVisible = true;*/
            BindingListInPhotosList();

        }
		private async Task BindingListInPhotosList()
        {
            try
            {

                //if (PopupPhotoList.Count > 10)
                //{
                //    for (int i = 0; i < a; i++)
                for (int i = 0; i < PopupPhotoList.Count; i++)
                {
                    var photoitems = new PhotoDisplay();
					photoitems.SetFontSize = App.GetFontSizeMedium();
                    photoitems.leftId = (i + 1).ToString();
                    //photoitems.leftImageUrl = PopupPhotoList[i].photo_path;
                    //Bitmap bitmap = DecodeSmallFile(PopupPhotoList[i].photo_path, 500, 500);
                    photoitems.leftImageUrl = PopupPhotoList[i].thumb_path;
                    if (Device.Idiom == TargetIdiom.Tablet)
                    {
                        photoitems.leftImageUrl = PopupPhotoList[i].ipad_thumb_path;
                    }
                    //photoitems.leftImageUrl = bitmap;
                    photoitems.leftTitle = PopupPhotoList[i].photo_title;
                    photoitems.ColorStackBorderright = Xamarin.Forms.Color.White;
                    photoitems.background = Xamarin.Forms.Color.FromHex("#132561");
                    i++;
                    if (i < PopupPhotoList.Count)
                    {
                        photoitems.rightId = (i + 1).ToString();
                        //photoitems.rightImageUrl = PopupPhotoList[i].photo_path;
                        photoitems.rightImageUrl = PopupPhotoList[i].thumb_path;
                        if (Device.Idiom == TargetIdiom.Tablet)
                        {
                            photoitems.rightImageUrl = PopupPhotoList[i].ipad_thumb_path;
                        }
                        photoitems.PlaceholderRight = "photogalleryplaceholder.png";
                        photoitems.rightTitle = PopupPhotoList[i].photo_title.ToUpper();
                        photoitems.ColorStackBorderright = Xamarin.Forms.Color.White;
                        photoitems.background = Xamarin.Forms.Color.FromHex("#132561");
                    }
                    else
                    {
                        photoitems.rightId = null;
                        photoitems.rightImageUrl = null;
                        photoitems.PlaceholderRight = null;
                        photoitems.rightTitle = null;
                        photoitems.background = Xamarin.Forms.Color.FromHex("#000000");
                        photoitems.ColorStackBorderright = Xamarin.Forms.Color.FromHex("#09091a");
                        PhotoDisplayList.Add(photoitems);
                        break;
                    }
                    photoitems.ColorStackBorderleft = Xamarin.Forms.Color.White;
                    PhotoDisplayList.Add(photoitems);
                }
                PhotoDetailList.ItemsSource = PhotoDisplayList;
                //   PhotoDetailList.RowHeight = widthdevice / 2;
                if (App.DeviceHeight > 2000)
                {
                    PhotoDetailList.RowHeight = 2 * 153;
                }
                else
                {
                    PhotoDetailList.RowHeight = 153;
                }
                if (Device.Idiom == TargetIdiom.Tablet)
                { PhotoDetailList.RowHeight = 2 * 153; }
                loading.IsVisible = false;
                loading.IsEnabled = false;
                PhotoDetailList.IsVisible = true;
           //     if (PhotoDisplayList.Count <= 2)
            //    {
           //         PhotoDetailList.HeightRequest = PhotoDetailList.RowHeight * PhotoDisplayList.Count;
           //     }
            }
            catch (Exception ex)
            {
				if (App.CurrentPage() == "photogallerylist")
               await DisplayAlert(AppResources.LError, AppResources.LSomethingWentWrong, AppResources.LOk);
				//DisplayAlert("photos", "Error1", AppResources.LOk);
                loading.IsVisible = false;
                loading.IsEnabled = false;
            }
            //loadingIndicator.IsVisible = false;
            //loading.IsRunning = false;
            //loading.IsVisible = false;
        }
        //#region compress decompress image
        //private Bitmap DecodeSmallFile(String filename, int width, int height)
        //{
        //    var options = new BitmapFactory.Options { InJustDecodeBounds = true };
        //    BitmapFactory.DecodeFile(filename, options);
        //    options.InSampleSize = CalculateInSampleSize(options, width, height);
        //    options.InJustDecodeBounds = false;
        //    return BitmapFactory.DecodeFile(filename, options);
        //}
        //public static int CalculateInSampleSize(BitmapFactory.Options options, int reqWidth, int reqHeight)
        //{
        //    // Raw height and width of image
        //    float height = options.OutHeight;
        //    float width = options.OutWidth;
        //    double inSampleSize = 1D;

        //    if (height > reqHeight || width > reqWidth)
        //    {
        //        int halfHeight = (int)(height / 2);
        //        int halfWidth = (int)(width / 2);

        //        // Calculate a inSampleSize that is a power of 2 - the decoder will use a value that is a power of two anyway.
        //        while ((halfHeight / inSampleSize) > reqHeight && (halfWidth / inSampleSize) > reqWidth)
        //        {
        //            inSampleSize *= 2;
        //        }
        //    }

        //    return (int)inSampleSize;
        //}
        ////async Task<BitmapFactory.Options> GetBitmapOptionsOfImageAsync()
        ////{
        ////    BitmapFactory.Options options = new BitmapFactory.Options
        ////    {
        ////        InJustDecodeBounds = true
        ////    };

        ////    // The result will be null because InJustDecodeBounds == true.
        ////    Bitmap result = await BitmapFactory.DecodeResourceAsync(Resources, Resource.Drawable.samoyed, options);

        ////    int imageHeight = options.OutHeight;
        ////    int imageWidth = options.OutWidth;

        ////    _originalDimensions.Text = string.Format("Original Size= {0}x{1}", imageWidth, imageHeight);

        ////    return options;
        ////}
        ////public async void CallWebService()

        //#endregion
        public void popupviewIos(string id)
        {
            popupLayout = this.Content as PopupLayout;

            if (popupLayout.IsPopupActive)
            {
                // PhotoDetailList.GestureRecognizers.Remove
                popupLayout.DismissPopup();
                PhotoDetailMainStack.Opacity = 1;
                //   MainStack.BackgroundColor = Color.Transparent;
            }
            else
            {
                PhotoDetailMainStack.Opacity = 0.2;
                //MainStack.BackgroundColor = Color.Black;
                #region xaml design popup
                StackLayout MainStack = new StackLayout()
                {
                    BackgroundColor = Xamarin.Forms.Color.White,
                    HeightRequest = this.Height * .5,
                    WidthRequest = this.Width * .8,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    Padding = new Thickness(5, 5, 5, 5)
                };
                StackLayout ImageStack = new StackLayout() { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center };
                Grid ImageGridLayout = new Grid()
                {
                    BackgroundColor = Xamarin.Forms.Color.White,
                    HeightRequest = this.Height * .45,
                    WidthRequest = this.Width * .8,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                };
                Image ImageContent = new Image()
                {
                    Aspect = Aspect.Fill,
                    HeightRequest = this.Height * .45,
                    WidthRequest = this.Width * .8
                };
                Image ImageContentPlaceholder = new Image()
                {
                    Aspect = Aspect.Fill,
                    HeightRequest = this.Height * .45,
                    WidthRequest = this.Width * .8,
                    Source = "photogalleryplaceholder.png"
                };
                #endregion

                #region bottom stack

                StackLayout BottomStack = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                    BackgroundColor = Xamarin.Forms.Color.White,
                    HeightRequest = this.Height * .05,
                    WidthRequest = this.Width * .8,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.CenterAndExpand
                };
                Label ImageNumber = new Label() { BackgroundColor = Xamarin.Forms.Color.White, TextColor = Xamarin.Forms.Color.FromHex("#09091a"), VerticalOptions = LayoutOptions.Center, FontSize = 20, HorizontalOptions = LayoutOptions.Start };
                Image LeftArrow = new Image() { Source = "prev_btn.png", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start };
                Image RightArrow = new Image() { Source = "next_btn.png", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start };
                Image CloseButton = new Image() { Source = "btn_close.png", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.EndAndExpand };
                int MinTapValue = 1;
                int PhotoCountValue = Convert.ToInt32(id);
                int Maxtap = PopupPhotoList.Count;
                //ImageContent.Source = PopupPhotoList[MinTapValue - 1].photo_path;
                ImageContent.Source = PopupPhotoList[PhotoCountValue - 1].photo_path;
                ImageNumber.Text = PhotoCountValue + " " + AppResources.LOf + " " + PopupPhotoList.Count;
                var LeftArrowTap = new TapGestureRecognizer(); LeftArrowTap.Tapped += (object sender, EventArgs e) =>
                {
                    //if (PhotoCountValue <= 0)
                    //{
                    //}
                    //else
                    //{
                    if (PhotoCountValue > 1)
                    {
                        PhotoCountValue = PhotoCountValue - 1;
                        //ImageContent.Source = PopupPhotoList[PhotoCountValue - 1].photo_path;
                        ImageContent.Source = PopupPhotoList[PhotoCountValue - 1].photo_path;
                        ImageNumber.Text = PhotoCountValue + " " + AppResources.LOf + " " + PopupPhotoList.Count;
                    }
                    //}

                };
                var RightArrowTap = new TapGestureRecognizer(); RightArrowTap.Tapped += (object sender, EventArgs e) =>
                {
                    //if (PhotoCountValue >= PopupPhotoList.Count)
                    //{
                    //}
                    //else
                    //{
                    if (PhotoCountValue < PopupPhotoList.Count)
                    {
                        PhotoCountValue = PhotoCountValue + 1;
                        //ImageContent.Source = PopupPhotoList[PhotoCountValue - 1].photo_path;
                        ImageContent.Source = PopupPhotoList[PhotoCountValue - 1].photo_path;
                        ImageNumber.Text = PhotoCountValue + " " + AppResources.LOf + " " + PopupPhotoList.Count;
                    }

                    //}
                };
                var CloseButtonTap = new TapGestureRecognizer(); CloseButtonTap.Tapped += (object sender, EventArgs e) =>
                {
                    //if (popupLayout.IsPopupActive)
                    //{
                    //    PhotoDetailMainStack.Opacity = 1;
                    //    popupLayout.DismissPopup();
                    //}
                    popupviewIos(PhotoCountValue.ToString());
                };
                LeftArrow.GestureRecognizers.Add(LeftArrowTap);
                RightArrow.GestureRecognizers.Add(RightArrowTap);
                CloseButton.GestureRecognizers.Add(CloseButtonTap);
                BottomStack.Children.Add(LeftArrow);
                BottomStack.Children.Add(ImageNumber);
                BottomStack.Children.Add(RightArrow);
                BottomStack.Children.Add(CloseButton);
                #endregion
                ImageGridLayout.Children.Add(ImageContentPlaceholder);
                ImageGridLayout.Children.Add(ImageContent);
                ImageStack.Children.Add(ImageGridLayout);
                //  ImageStack.Children.Add(ImageContent);
                ImageStack.Children.Add(BottomStack);
                MainStack.Children.Add(ImageStack);
                popupLayout.ShowPopup(MainStack);
            }
        }
        public void popupviewAndroid(string id)
        {
            //popupLayout = this.Content as PopupLayout;

            //if (popupLayout.IsPopupActive)
            //{
            //   // PhotoDetailList.GestureRecognizers.Remove
            //    popupLayout.DismissPopup();
            //    PhotoDetailMainStack.Opacity = 1;
            // //   MainStack.BackgroundColor = Color.Transparent;
            //}
            if (Popupviewimage.ClassId == "ON")
            {
                Popupviewimage.IsVisible = false;
                Popupviewimage.ClassId = "OFF";
                Popupviewimage.Children.Clear();
                PhotoDetailMainStack.Opacity = 1;
                // popupLayout.DismissPopup();
            }
            else
            {
                PhotoDetailMainStack.Opacity = 0.2;
                //MainStack.BackgroundColor = Color.Black;
                #region xaml design popup
                Popupviewimage.HeightRequest = this.Height * .5;
                Popupviewimage.WidthRequest = this.WidthRequest * .8;
                var MainStack = new StackLayout()
                {
                    BackgroundColor = Xamarin.Forms.Color.White,
                    HeightRequest = this.Height * .5,
                    WidthRequest = this.Width * .8,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    Padding = new Thickness(5, 5, 5, 5)
                };
                var ImageStack = new StackLayout() { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center };
                var ImageGridLayout = new Grid()
                {
                    BackgroundColor = Xamarin.Forms.Color.White,
                    HeightRequest = this.Height * .45,
                    WidthRequest = this.Width * .8,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                };
                var ImageContent = new Image()
                {
                    Aspect = Aspect.Fill,
                    HeightRequest = this.Height * .45,
                    WidthRequest = this.Width * .8
                };
                var ImageContentPlaceholder = new Image()
                {
                    Aspect = Aspect.Fill,
                    HeightRequest = this.Height * .45,
                    WidthRequest = this.Width * .8,
                    Source = "photogalleryplaceholder.png"
                };
                #endregion

                #region bottom stack

                StackLayout BottomStack = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                    BackgroundColor = Xamarin.Forms.Color.White,
                    HeightRequest = this.Height * .05,
                    WidthRequest = this.Width * .8,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions= LayoutOptions.CenterAndExpand
                };
                Label ImageNumber = new Label() { BackgroundColor = Xamarin.Forms.Color.White, TextColor = Xamarin.Forms.Color.FromHex("#09091a"), VerticalOptions = LayoutOptions.Center, FontSize=20,HorizontalOptions= LayoutOptions.Start };
                Image LeftArrow = new Image() { Source = "prev_btn.png", VerticalOptions = LayoutOptions.Center ,HorizontalOptions= LayoutOptions.Start };
                Image RightArrow = new Image() { Source = "next_btn.png", VerticalOptions = LayoutOptions.Center , HorizontalOptions= LayoutOptions.Start };
                Image CloseButton = new Image() { Source = "btn_close.png", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.EndAndExpand };
                int MinTapValue = 1;
                int PhotoCountValue = Convert.ToInt32(id);
                int Maxtap = PopupPhotoList.Count;
                //ImageContent.Source = PopupPhotoList[MinTapValue - 1].photo_path;
                ImageContent.Source = PopupPhotoList[PhotoCountValue - 1].photo_path;
                ImageNumber.Text =PhotoCountValue + " " + AppResources.LOf + " " + PopupPhotoList.Count;
                var LeftArrowTap = new TapGestureRecognizer(); LeftArrowTap.Tapped += (object sender, EventArgs e) =>
                {
                    //if (PhotoCountValue <= 0)
                    //{
                    //}
                    //else
                    //{
                    if (PhotoCountValue > 1)
                    {
                        PhotoCountValue = PhotoCountValue - 1;
                        //ImageContent.Source = PopupPhotoList[PhotoCountValue - 1].photo_path;
                        ImageContent.Source = PopupPhotoList[PhotoCountValue - 1].photo_path;
                        ImageNumber.Text = PhotoCountValue + " " + AppResources.LOf + " " + PopupPhotoList.Count;
                    }
                    //}

                };
                var RightArrowTap = new TapGestureRecognizer(); RightArrowTap.Tapped += (object sender, EventArgs e) =>
                {
                    //if (PhotoCountValue >= PopupPhotoList.Count)
                    //{
                    //}
                    //else
                    //{
                    if (PhotoCountValue < PopupPhotoList.Count)
                    {
                        PhotoCountValue = PhotoCountValue + 1;
                        //ImageContent.Source = PopupPhotoList[PhotoCountValue - 1].photo_path;
                        ImageContent.Source = PopupPhotoList[PhotoCountValue - 1].photo_path;
                        ImageNumber.Text = PhotoCountValue + " "+AppResources.LOf+" " + PopupPhotoList.Count;
                    }

                    //}
                };
                var CloseButtonTap = new TapGestureRecognizer();CloseButtonTap.Tapped += (object sender, EventArgs e) =>
                {
                    //if (popupLayout.IsPopupActive)
                    //{
                    //    PhotoDetailMainStack.Opacity = 1;
                    //    popupLayout.DismissPopup();
                    //}
                    popupviewAndroid(PhotoCountValue.ToString());
                };
                LeftArrow.GestureRecognizers.Add(LeftArrowTap);
                RightArrow.GestureRecognizers.Add(RightArrowTap);
                CloseButton.GestureRecognizers.Add(CloseButtonTap);
                BottomStack.Children.Add(LeftArrow);
                BottomStack.Children.Add(ImageNumber);
                BottomStack.Children.Add(RightArrow);
                BottomStack.Children.Add(CloseButton);
                #endregion
                ImageGridLayout.Children.Add(ImageContentPlaceholder);
                ImageGridLayout.Children.Add(ImageContent);
                ImageStack.Children.Add(ImageGridLayout);
              //  ImageStack.Children.Add(ImageContent);
                ImageStack.Children.Add(BottomStack);
                MainStack.Children.Add(ImageStack);
                Popupviewimage.Children.Add(MainStack);
                Popupviewimage.IsVisible = true;
                Popupviewimage.ClassId = "ON";
              //  popupLayout.ShowPopup(MainStack);
            }
        }
        private void LeftPhotoTapped(object sender, EventArgs e)
        {
            var StackID = sender as Grid;
            var obj = StackID.ClassId;
            if (obj != null && obj != "")
            {
                if (TargetPlatform.iOS == Device.OS)
                {
                    popupviewIos(obj);
                }
                if (TargetPlatform.Android == Device.OS)
                {
                    popupviewAndroid(obj);
                }
            }
        }
        private void RightPhotoTapped(object sender, EventArgs e)
        {
            var StackID = sender as Grid;
            var obj = StackID.ClassId;
            if (obj != null && obj != "")
            {
                if (TargetPlatform.iOS == Device.OS)
                {
                    popupviewIos(obj);
                }
                if (TargetPlatform.Android == Device.OS)
                {
                    popupviewAndroid(obj);
                }
            }
        }
        public async void CallWebServiceForPhotoGalleryList(string pageid)
        {
            if (!App.CheckInternetConnection())
            {
               // Loading(false);
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
                   // values.Add(new KeyValuePair<string, string>("page_id", "456"));
                    values.Add(new KeyValuePair<string, string>("page_id", pageid));
                    var response = await GeneralClass.GetResponse<CMO.ServicesClasses.RootObjectPhotoDetailResponse>("http://14.141.36.212/maharastracmo/api/getphotogallerylistbyid", values);
                    if (response != null)
                    {
                        if (response._resultflag != 0)
                        {
                            foreach (var item in response.gallery_list.photo)
                                PopupPhotoList.Add(item);
                            //  PopupPhotoList=  response.gallery_list.photo;
                           await BindingListInPhotosList();
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
            }
        }
    }
    public class PhotoDisplay
    {
        public string leftId { get; set; }
        //public Bitmap leftImageUrl { get; set; }
        public string leftImageUrl { get; set; }
        public string leftTitle { get; set; }
        public Xamarin.Forms.Color ColorStackBorderleft { get; set; }

        public Xamarin.Forms.Color background { get; set; }

        public string rightId { get; set; }
        public string rightImageUrl { get; set; }
        public string rightTitle { get; set; }
        public Xamarin.Forms.Color ColorStackBorderright { get; set; }
        public ImageSource PlaceholderRight { get; set; }
		public int SetFontSize { get; set; }
    }
}
