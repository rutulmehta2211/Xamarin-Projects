using CMO.MenuController;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace CMO.CMVisits
{
    public partial class MakeInMaharashtraGoesInternational : ContentPage
    {
        public string SearchFilterText = "";
        int widthdevice;
        int deviceheight;
        int index = 0;
        int totalItems = 0;
        int totalListItems = 0;
        decimal MaxIndex = 0;
        ObservableCollection<DatalData> CountryList;
        public MakeInMaharashtraGoesInternational()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
           // index = 0;
            widthdevice = App.DeviceWidth;
            deviceheight = App.DeviceHeight;
			FilterMahrashtraVisitGoesInternationalList.FontSize = App.GetFontSizeMedium();
            if (TargetPlatform.Android == Device.OS)
            {
            //    EntryOuterStack.Padding = new Thickness(5, 7, 0, 0);
            }
            // firstrow.Height = (deviceheight * 6.5) / 100;
            FilterMahrashtraVisitGoesInternationalList.Placeholder = AppResources.LSearch;
            this.Title = AppResources.LForeignVisits.ToUpper();
            GalleryList.ItemAppearing += GalleryList_ItemAppearing1;
            loadingIndicator.IsVisible = true;
            loading.IsRunning = true;
            loading.IsVisible = true;
            //GalleryList.IsVisible = false;
            GalleryList.ItemTapped += GalleryList_ItemTapped;
            GalleryList.Refreshing += GalleryList_Refreshing;
           // CallWebServiceForForiegnVisits(index);
        }
        private void GalleryList_Refreshing(object sender, EventArgs e)
        {
            index = 0;
            loadingIndicator.IsVisible = false;
            loading.IsVisible = false;
            loading.IsRunning = false;
            if (lblNoRecord.IsVisible == true)
            {
                SearchFilterText = "";
                FilterMahrashtraVisitGoesInternationalList.Text = "";
            }
            CallWebServiceForForiegnVisits(index);
        }
        private void GalleryList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var x = sender as ListView;
            x.SelectedItem = null;
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
                    if (FilterMahrashtraVisitGoesInternationalList.Text == "" || FilterMahrashtraVisitGoesInternationalList.Text == null)
                    { SearchFilterText = ""; }
                    else
                    {
                        SearchFilterText = FilterMahrashtraVisitGoesInternationalList.Text;
                    }
                    index = 0;
                    loadingIndicator.IsVisible = true;
                    loading.IsRunning = true;
                    loading.IsVisible = true;
                   // GalleryList.IsVisible = false; u
                    CountryList.Clear();
                   await CallWebServiceForForiegnVisits(index);
                }
            }
        }
        private async void GalleryList_ItemAppearing1(object sender, ItemVisibilityEventArgs e)
        {
            try
            {
                if (totalItems != 0)
                {
                    MaxIndex = Math.Ceiling(((decimal)totalListItems) / 5);

                    if (index < MaxIndex)
                    {
                        if (CountryList != null && e.Item != null && e.Item == CountryList[CountryList.Count - 1])
                        {
                            index++;
                            if (index != MaxIndex)
                            {
                                loadingIndicator.IsVisible = true;
                                loading.IsVisible = true;
                                loading.IsRunning = true;
                               await CallWebServiceForForiegnVisits(index);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
				if (App.CurrentPage() == "makeinmaharashtrainternational")
				{
					if (ex.Message.Contains("Network"))
						await DisplayAlert(AppResources.LError, AppResources.LWebserverNotResponding, AppResources.LOk);
				}
            }
        }
        protected override async void OnAppearing()
        {

            //    index = 0;
            //if (Application.Current.Properties.ContainsKey("navigationflag"))
            //{
            //    var _currentPage = Application.Current.Properties["navigationflag"] as string;
            //    if (_currentPage == "1")
            //    {
            //        index = 0;
            //        loadingIndicator.IsVisible = true;
            //        loading.IsRunning = true;
            //        loading.IsVisible = true;
            //       // GalleryList.IsVisible = false; u
            //        CallWebServiceForForiegnVisits(index);
            //    }
            //}
            if (loading.IsRunning == true)
            {
               await CallWebServiceForForiegnVisits(index);
            }
            //    Application.Current.Properties["CurrentPage"] = "makeinmaharashtrainternational";
			lblNoRecord.FontSize = App.GetFontSizeMedium();

        }
        private async void LeftStackTaped(object sender, EventArgs e)
        {
            var stack = Navigation.NavigationStack;
            if (stack[stack.Count - 1].GetType() != typeof(MakeInMaharashtraGoesInternationalDetail))
            {
                var StackID = sender as Grid;
                if (StackID.ClassId != null)
                {
                  //  Application.Current.Properties["navigationflag"] = "";
                   await Navigation.PushAsync(new CMO.MakeInMaharashtraGoesInternationalDetail(widthdevice, StackID.ClassId));
                }
            }
        }
		private async void RightStackTaped(object sender, EventArgs e)
        {
            var stack = Navigation.NavigationStack;
            if (stack[stack.Count - 1].GetType() != typeof(MakeInMaharashtraGoesInternationalDetail))
            {
                var StackID = sender as Grid;
                if (StackID.ClassId != null)
                {
                //    Application.Current.Properties["navigationflag"] = "2";
                   await Navigation.PushAsync(new CMO.MakeInMaharashtraGoesInternationalDetail(widthdevice, StackID.ClassId));
                }
            }
        }
        public async Task CallWebServiceForForiegnVisits(int index)
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
                    values.Add(new KeyValuePair<string, string>("country", ""));
                    values.Add(new KeyValuePair<string, string>("index", Convert.ToString(index)));
                    values.Add(new KeyValuePair<string, string>("limit", "8"));


                    var response = await GeneralClass.GetResponse<CMO.ServicesClasses.RootObjectCMVisitList>("http://14.141.36.212/maharastracmo/api/cmforeignvisits", values);
                    if (response != null)
                    {
                        if (response._resultflag == 1)
                        {
                            totalItems = response.total_results;
                            totalListItems = response.search_results;
                            if (CountryList == null || index == 0)
                            {
                                CountryList = new ObservableCollection<DatalData>();
                            }

                            for (int i = 0; i < response.cm_visit.Count; i++)
                            {
                                DateTime oDate = Convert.ToDateTime(response.cm_visit[i].date);
                                response.cm_visit[i].date = oDate.ToString("MMM. dd, yyyy");
                            }

                            for (int i = 0; i < response.cm_visit.Count; i++)
                            {
                                var countryitems = new DatalData();
								countryitems.SetFontSize = App.GetFontSizeMedium();
                                countryitems.leftimage = response.cm_visit[i].image;
                                countryitems.lefttitle = response.cm_visit[i].title.ToUpper();
                                countryitems.leftdate = response.cm_visit[i].date;
                                countryitems.CalenderIcon = "ico_calendar.png";
                                countryitems.leftId = response.cm_visit[i].title;
                                countryitems.ColorStackBorderright = Color.White;
                                countryitems.ImageBackgroundright = Color.FromHex("#132561");
                                //countryitems.ColorStackBorderleft = Color.White;
                                i++;
                                if (i < response.cm_visit.Count)
                                {
                                    countryitems.ColorStackBorderright = Color.White;
                                    countryitems.ImageBackgroundright = Color.FromHex("#132561");
                                    //countryitems.ColorStackBorderleft = Color.White;
                                    countryitems.rightimage = response.cm_visit[i].image;
                                    countryitems.PlaceholderRight = "photogalleryplaceholder.png";
                                    countryitems.righttitle = response.cm_visit[i].title.ToUpper();
                                    countryitems.rightdate = response.cm_visit[i].date;
                                    countryitems.rightId = response.cm_visit[i].title;
                                    countryitems.CalenderIcon = "ico_calendar.png";
                                }
                                else
                                {
                                    countryitems.CalenderIcon = null;
                                    countryitems.rightimage = null;
                                    countryitems.PlaceholderRight = null;
                                    countryitems.righttitle = null;
                                    countryitems.rightdate = null;
                                    countryitems.ColorStackBorderright = Color.FromHex("#09091a");
                                    countryitems.ImageBackgroundright = Color.FromHex("#09091a");
                                    countryitems.ColorStackBorderleft = Color.White;

                                    CountryList.Add(countryitems);
                                    break;
                                }
                                countryitems.ColorStackBorderleft = Color.White;
                                CountryList.Add(countryitems);
                            }
                            //if (GalleryList.IsVisible == false)
                            //{
                            //    GalleryList.IsVisible = true;
                            //} u
                            GalleryList.ItemsSource = CountryList;
                            lblNoRecord.IsVisible = false;
                            GalleryList.IsRefreshing = false;
                           // GalleryList.IsVisible = true; u
                            if (Device.Idiom == TargetIdiom.Tablet)
                            {
                                GalleryList.RowHeight = 300;
                            }
                            if (Device.Idiom == TargetIdiom.Phone)
                            {
                               if (Device.OS == TargetPlatform.iOS)
								{
									GalleryList.RowHeight = 160;
								}
								else {

									GalleryList.RowHeight = 200;
								}
                            }
                        }
                        else
                        {
                            lblNoRecord.IsVisible = true;
                          //  GalleryList.IsVisible = false; u
                            GalleryList.IsRefreshing = false;
                            CountryList.Clear();
                        }


                    }
                    else
                    {
                        await DisplayAlert(AppResources.LError,AppResources.LSomethingWentWrong, AppResources.LOk);
                    }
                }
                catch (WebException exception)
                {
					if (App.CurrentPage() == "makeinmaharashtrainternational")
                    await DisplayAlert(AppResources.LError, AppResources.LWebserverNotResponding,AppResources.LOk);
                }
              //  GalleryList.IsVisible = true; u
                GalleryList.IsRefreshing = false;
            }
                loading.IsVisible = false;
                loading.IsRunning = false;
                loadingIndicator.IsVisible = false;
                GalleryList.IsRefreshing = false;

        }

        //#region Exit Application
        //private bool _canClose = true;

        //protected override bool OnBackButtonPressed()
        //{
        //    //if (_canClose)
        //    //{
        //    //    ShowExitDialog();
        //    //}
        //    //return _canClose;
        //    Application.Current.MainPage = new SideMenu();
        //    return true;
        //}

        //private async void ShowExitDialog()
        //{
        //    var answer = await DisplayAlert(AppResources.LExit, AppResources.LExitApp, AppResources.LYes, AppResources.LNo);
        //    if (answer)
        //    {
        //        if (Device.OS == TargetPlatform.Android)
        //        {
        //            DependencyService.Get<IAndroidMethods>().CloseApp();
        //        }
        //        _canClose = false;
        //        OnBackButtonPressed();
        //    }
        //}
        //#endregion
    }

    public class DatalData
    {
        public string leftId { get; set; }
        public string leftimage { get; set; }
        public string lefttitle { get; set; }
        public string leftdate { get; set; }

        public string rightId { get; set; }
        public string rightimage { get; set; }
        public string righttitle { get; set; }
        public string rightdate { get; set; }

        public string CalenderIcon { get; set; }
        public Color ColorStackBorderleft { get; set; }
        public Color ColorStackBorderright { get; set; }
        public Color ImageBackgroundright { get; set; }
        public ImageSource PlaceholderRight { get; set; }
		public int SetFontSize { get; set; }
    }
}

