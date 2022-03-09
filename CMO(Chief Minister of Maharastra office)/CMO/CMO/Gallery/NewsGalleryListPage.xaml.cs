using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CMO.ServicesClasses;

using Xamarin.Forms;
using System.Collections.ObjectModel;
using CMO.MenuController;

namespace CMO.Gallery
{
    public partial class NewsGalleryListPage : ContentPage
    {
        int totalItems = 0;
        decimal MaxIndex = 0;
        int index = 0;
        ObservableCollection<Newslist> NewsList;
        public NewsGalleryListPage()
        {
            InitializeComponent();
            this.Title = AppResources.LNewsGallery.ToUpper();
            NavigationPage.SetBackButtonTitle(this, "");
            NewsGalleryLists.ItemTapped += NewsGalleryLists_ItemTapped;
            NewsGalleryLists.ItemAppearing += NewsGalleryLists_ItemAppearing1;
            NewsGalleryLists.Refreshing += NewsGalleryLists_Refreshing;
            //CallWebServiceForNewsGalleryList(index);
        }

        private async void NewsGalleryLists_Refreshing(object sender, EventArgs e)
        {
            index = 0;
            loadingIndicator.IsVisible = false;
            loading.IsVisible = false;
            loading.IsRunning = false;
            await CallWebServiceForNewsGalleryList(index);
        }

        protected async override void OnAppearing()
        {
            if (Application.Current.Properties.ContainsKey("navigationflag"))
            {
                var _currentPage = Application.Current.Properties["navigationflag"] as string;
                if (_currentPage == "0")
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
                        loadingIndicator.IsVisible = true;
                        loading.IsRunning = true;
                        loading.IsVisible = true;
                        await CallWebServiceForNewsGalleryList(index);
                    }
                }
            }
            Application.Current.Properties["CurrentPage"] = "newsgallerylist";
        }
        private async void NewsGalleryLists_ItemAppearing1(object sender, ItemVisibilityEventArgs e)
        {
            try
            {
                if (totalItems != 0)
                {
                    MaxIndex = Math.Ceiling(((decimal)totalItems) / 5);
                    if (index < MaxIndex)
                    {
                        if (NewsList != null && e.Item != null && e.Item == NewsList[NewsList.Count - 1])
                        {
                            index++;
                            if (index != MaxIndex)
                            {
                                loadingIndicator.IsVisible = true;
                                loading.IsVisible = true;
                                loading.IsRunning = true;
                              await  CallWebServiceForNewsGalleryList(index);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
				if (App.CurrentPage() == "newsgallerylist")
				{
					if (ex.Message.Contains("Network"))
						await DisplayAlert(AppResources.LError, AppResources.LSomethingWentWrong, AppResources.LOk);
				}
            }
        }

        private void NewsGalleryLists_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var stack = Navigation.NavigationStack;
            if (stack[stack.Count - 1].GetType() != typeof(NewsGalleryDetail))
            {
                var selectedNewsGalleryListItem = sender as Xamarin.Forms.ListView;
                var obj = selectedNewsGalleryListItem.SelectedItem as CMO.ServicesClasses.Newslist;
                if (obj != null)
                {
                    Application.Current.Properties["navigationflag"] = "1";
                    Navigation.PushAsync(new CMO.Gallery.NewsGalleryDetail(obj));
                }
            }
        }
        public async Task CallWebServiceForNewsGalleryList(int index)
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
                    values.Add(new KeyValuePair<string, string>("title", ""));
                    values.Add(new KeyValuePair<string, string>("index", Convert.ToString(index)));
                    if (Device.Idiom == TargetIdiom.Phone)
                    { values.Add(new KeyValuePair<string, string>("limit", "6")); }
                    else
                    { values.Add(new KeyValuePair<string, string>("limit", "7")); }
                   
                    var response = await GeneralClass.GetResponse<CMO.ServicesClasses.RootObjectNewsGalleryList>("http://14.141.36.212/maharastracmo/api/getnewslist", values);
                    if (response != null)
                    {
                        if (response._resultflag == 1)
                        {
                            if (NewsList == null || index == 0)
                            {
                                NewsList = new ObservableCollection<Newslist>();
                            }
                            for (int i = 0; i < response.newslist.Count; i++)
                            {
                                var ObjectNewslist = new Newslist();
								ObjectNewslist.SetFontSize = App.GetFontSizeMedium();
                                ObjectNewslist.page_id = response.newslist[i].page_id;
                                ObjectNewslist.title = response.newslist[i].title;
                                ObjectNewslist.date = response.newslist[i].date;
                                ObjectNewslist.large_news_photo = response.newslist[i].large_news_photo;
                                ObjectNewslist.news_photo = response.newslist[i].news_photo;
                                ObjectNewslist.content = response.newslist[i].content;
                                #region (Text) Specific Design implemention for phone and tablet
                                if (Device.Idiom == TargetIdiom.Phone)
                                {
                                    ObjectNewslist.TitleWidth = new GridLength(3, GridUnitType.Star);
                                    ObjectNewslist.ImageWidth = new GridLength(2, GridUnitType.Star);
									//ObjectNewslist.TitleFontSize =  Device.GetNamedSize(NamedSize.Small, typeof(Label));
                                }
                                else if (Device.Idiom == TargetIdiom.Tablet)
                                {
                                    ObjectNewslist.TitleWidth = new GridLength(4, GridUnitType.Star); 
                                    ObjectNewslist.ImageWidth = new GridLength(2, GridUnitType.Star);
                                  //  ObjectNewslist.TitleFontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
                                }
                                else
                                {
                                    ObjectNewslist.TitleWidth = new GridLength(4, GridUnitType.Star);
                                    ObjectNewslist.ImageWidth = new GridLength(2, GridUnitType.Star);
                                    //ObjectNewslist.TitleFontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
                                }
                                #endregion
								NewsList.Add(ObjectNewslist);
                            }
                            totalItems = response.total_results;
                            #region (Row Height) Specific Design implemention for phone and tablet
                            if (Device.Idiom == TargetIdiom.Phone)
                            { NewsGalleryLists.RowHeight = 100; }
                            else if (Device.Idiom == TargetIdiom.Tablet)
                            { NewsGalleryLists.RowHeight = 150; }
                            else if (Device.Idiom == TargetIdiom.Desktop)
                            { NewsGalleryLists.RowHeight = 3 * 100; }
                            else
                            { NewsGalleryLists.RowHeight = 150; }
                            #endregion
                            NewsGalleryLists.ItemsSource = NewsList;
                            NewsGalleryLists.IsRefreshing = false;
                            NewsGalleryLists.IsVisible = true;
                        }
                        else
                        {
                            if ((response._resultflag == 0 ) && (index==0)) 
                            {
                                if (App.CurrentPage() == "newsgallerylist")
								await DisplayAlert(AppResources.LError, AppResources.LSomethingWentWrong, AppResources.LOk);
	                        }
                        }
                    }
                    else
                    {
						if (App.CurrentPage() == "newsgallerylist")
						await DisplayAlert(AppResources.LError, AppResources.LSomethingWentWrong, AppResources.LOk);
                    }
                }
                catch (WebException exception)
                {
					if (App.CurrentPage() == "newsgallerylist")
					{
						if (exception.Message.Contains("Network"))
						await DisplayAlert(AppResources.LError, AppResources.LWebserverNotResponding, AppResources.LOk);
					}	
                 
                }
            }
                loading.IsVisible = false;
                loading.IsRunning = false;
                loadingIndicator.IsVisible = false;
                NewsGalleryLists.IsRefreshing = false;

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
            var answer = await DisplayAlert(AppResources.LExit,AppResources.LExitApp,AppResources.LYes,AppResources.LNo);
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
}
