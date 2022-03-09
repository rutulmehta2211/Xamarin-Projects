using CMO.MenuController;
using CMO.ServicesClasses;
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
    public partial class Eventslists : ContentPage
    {
        int widthdevice;
        int deviceheight;
        public string SearchFilterText = "";
        int index = 0;
        int totalItems = 0;
        int totalListItems = 0;
        decimal MaxIndex = 0;
        ObservableCollection<Event> EventList;
        public Eventslists()
        {
            InitializeComponent();
			FilterEventVisitList.FontSize = App.GetFontSizeMedium();
            lblNoRecord.IsVisible = false;
            #region Design xaml from backend
            FilterEventVisitList.Placeholder = AppResources.LSearch;
            this.Title = AppResources.LEvents.ToUpper();
            NavigationPage.SetBackButtonTitle(this, "");
            deviceheight = App.DeviceHeight;
            widthdevice = App.DeviceWidth;
            deviceheight = App.DeviceHeight;
            if (TargetPlatform.Android == Device.OS)
            {
              //  EntryOuterStack.Padding = new Thickness(5, 7, 0, 0);
            }

            #endregion
            #region Event List events
            EventVisitLists.ItemTapped += EventVisitLists_ItemTapped;
            EventVisitLists.ItemAppearing += EventVisitLists_ItemAppearing1;
            EventVisitLists.Refreshing += EventVisitLists_Refreshing;
            #endregion
        }

        private async void EventVisitLists_Refreshing(object sender, EventArgs e)
        {
            index = 0;
            loadingIndicator.IsVisible = false;
            loading.IsVisible = false;
            loading.IsRunning = false;
            if (lblNoRecord.IsVisible == true)
            {
                SearchFilterText = "";
                FilterEventVisitList.Text = "";
            }
            await CallWebServiceForEventVisitsDetail(index);
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
                        index = 0;
                        loadingIndicator.IsVisible = true;
                        loading.IsRunning = true;
                        loading.IsVisible = true;
                        await CallWebServiceForEventVisitsDetail(index);
                    }
                }
            }
            Application.Current.Properties["CurrentPage"] = "events";
			lblNoRecord.FontSize = App.GetFontSizeMedium();

        }
        private async void EventVisitLists_ItemAppearing1(object sender, ItemVisibilityEventArgs e)
        {
            try
            {
                if (totalItems != 0)
                {
                    MaxIndex = Math.Ceiling(((decimal)totalListItems) / 5);
                    if (index < MaxIndex)
                    {
                        if (EventList != null && e.Item != null && e.Item == EventList[EventList.Count - 1])
                        {
                            index++;
                            if (index != MaxIndex)
                            {
                                loadingIndicator.IsVisible = true;
                                loading.IsVisible = true;
                                loading.IsRunning = true;

                                await CallWebServiceForEventVisitsDetail(index);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
				if (App.CurrentPage() == "events")
                await DisplayAlert(AppResources.LError, AppResources.LSomethingWentWrong, AppResources.LOk);

            }
        }
        private void EventVisitLists_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var stack = Navigation.NavigationStack;
            if (stack[stack.Count - 1].GetType() != typeof(EventsDetail))
            {
                var selectedMaharashtraListItem = sender as Xamarin.Forms.ListView;
                var eventItem = selectedMaharashtraListItem.SelectedItem as Event;
                if (eventItem != null)
                {
                    Application.Current.Properties["navigationflag"] = "1";
                    Navigation.PushAsync(new CMO.CMVisits.EventsDetail(eventItem));
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
                    if (FilterEventVisitList.Text == "" || FilterEventVisitList.Text == null)
                    { SearchFilterText = ""; }
                    else
                    {
                        SearchFilterText = FilterEventVisitList.Text;
                    }
                    index = 0;
                    loadingIndicator.IsVisible = true;
                    loading.IsRunning = true;
                    loading.IsVisible = true;
                    EventList.Clear();
                    await CallWebServiceForEventVisitsDetail(index);
                }
            }
        }
        public async Task CallWebServiceForEventVisitsDetail(int index)
        {
            if (!App.CheckInternetConnection())
            {
                await DisplayAlert(AppResources.LNetworkError, AppResources.LNoInternetConnection, AppResources.LOk);
            }
            else
            {
                try
                {
                    #region Key value pairs
                    string lang = "";
                    if (Application.Current.Properties.ContainsKey("Language"))
                    { lang = Application.Current.Properties["Language"] as string; }
                    List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>();
                    values.Add(new KeyValuePair<string, string>("lang", lang));
                    values.Add(new KeyValuePair<string, string>("title", SearchFilterText));
                    values.Add(new KeyValuePair<string, string>("index", Convert.ToString(index)));
                    if (Device.Idiom == TargetIdiom.Phone)
                    { values.Add(new KeyValuePair<string, string>("limit", "7")); }
                    else
                    { values.Add(new KeyValuePair<string, string>("limit", "10")); }
                    //lang=en&title=&index=0&limit=10
                    #endregion
                    var response = await GeneralClass.GetResponse<CMO.ServicesClasses.RootObjectEventList>("http://14.141.36.212/maharastracmo/api/eventslisting", values);
                    if (response != null)
                    {
                        if (response._resultflag == 1)
                        {
                            totalItems = response.total_results;
                            totalListItems = response.search_results;
                            var x = EventVisitLists.RowHeight;
                            for (int i = 0; i < response.@event.Count; i++)
                            {
                                DateTime oDate = Convert.ToDateTime(response.@event[i].event_start_date);
                                response.@event[i].event_start_date = oDate.ToString("MMM. dd, yyyy");
                            }
                            if (EventList == null || index == 0)
                            {
                                EventList = new ObservableCollection<Event>();
                            }
                            for (int i = 0; i < response.@event.Count; i++)
                            {
                                var ObjectEvent = new Event();
								ObjectEvent.SetFontSize = App.GetFontSizeMedium();
                                ObjectEvent.image = response.@event[i].image;
                                ObjectEvent.title = response.@event[i].title;
                                ObjectEvent.event_start_date = response.@event[i].event_start_date;
                                ObjectEvent.event_end_date = response.@event[i].event_end_date;
                                ObjectEvent.description = response.@event[i].description;
                                #region (Text) Specific Design implemention for phone and tablet
                                if (Device.Idiom == TargetIdiom.Phone)
                                {
                                    ObjectEvent.TitleWidth = new GridLength(3, GridUnitType.Star);
                                    ObjectEvent.ImageWidth = new GridLength(2, GridUnitType.Star);
								//	ObjectEvent.TitleFontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
                                }
                                else if (Device.Idiom == TargetIdiom.Tablet)
                                {
                                    ObjectEvent.TitleWidth = new GridLength(4, GridUnitType.Star);
                                    ObjectEvent.ImageWidth = new GridLength(2, GridUnitType.Star);
                                 //   ObjectEvent.TitleFontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
                                }
                                else
                                {
                                    ObjectEvent.TitleWidth = new GridLength(4, GridUnitType.Star);
                                    ObjectEvent.ImageWidth = new GridLength(2, GridUnitType.Star);
                                 //   ObjectEvent.TitleFontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
                                }
                                #endregion
                                EventList.Add(ObjectEvent);
                            }
                            //if (EventVisitLists.IsVisible == false)
                            //{
                            //    EventVisitLists.IsVisible = true;
                            //}
                            #region (Row Height) Specific Design implemention for phone and tablet
                            if (Device.Idiom == TargetIdiom.Phone)
                            { EventVisitLists.RowHeight = 100; }
                            else if (Device.Idiom == TargetIdiom.Tablet)
                            { EventVisitLists.RowHeight = 150; }
                            else if (Device.Idiom == TargetIdiom.Desktop)
                            { EventVisitLists.RowHeight = 3 * 100; }
                            else
                            { EventVisitLists.RowHeight = 150; }
                            #endregion
                            EventVisitLists.ItemsSource = EventList;
                            EventVisitLists.IsRefreshing = false;
                           // EventVisitLists.IsVisible = true;
                            
                        }
                        else
                        {
                            if (index == 0)
                            {
                              //  EventList = new ObservableCollection<Event>();
                                 lblNoRecord.IsVisible = true;
                                //   EventVisitLists.IsVisible = true;
                                EventList.Clear();
                                EventVisitLists.IsRefreshing = false;
                            }
                        }
                    }
                    else
                    {
						if (App.CurrentPage() == "events")
							await DisplayAlert(AppResources.LError, AppResources.LSomethingWentWrong, AppResources.LOk);      
					}

                }

                catch (WebException exception)
                {
					if (App.CurrentPage() == "events")
					{
						if (exception.Message.Contains("Network"))
							await DisplayAlert(AppResources.LError, AppResources.LWebserverNotResponding, AppResources.LOk);
					}
                }
            }
            loadingIndicator.IsVisible = false;
            loading.IsRunning = false;
            loading.IsVisible = false;
            EventVisitLists.IsRefreshing = false;
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
            if (Navigation.NavigationStack.Count == 1)
            {
                Application.Current.MainPage = new SideMenu();
                return true;
            }
            else
            {
                return false;
            }
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
}
