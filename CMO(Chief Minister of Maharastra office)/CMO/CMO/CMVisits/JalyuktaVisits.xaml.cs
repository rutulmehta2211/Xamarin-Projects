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
    public partial class JalyuktaVisits : ContentPage
    {
        public string SearchFilterText = "";
        int index = 0;
        int totalItems = 0;
        int totalListItems = 0;
        int widthdevice;
        int deviceheight;
        decimal MaxIndex = 0;
        ObservableCollection<CmVisit> CmVisitList;
        public JalyuktaVisits()
        {

            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
			FilterJalyuktVisitList.FontSize = App.GetFontSizeMedium();
            widthdevice = App.DeviceWidth;
            deviceheight = App.DeviceHeight;
            //  firstrow.Height = (deviceheight * 7) / 100;
            if (TargetPlatform.Android == Device.OS)
            {
             //   EntryOuterStack.Padding = new Thickness(5, 7, 0, 0);
            }
            FilterJalyuktVisitList.Placeholder = AppResources.LSearch;
            JalyuktVisitLists.ItemTapped += JalyuktVisitLists_ItemTapped;
            JalyuktVisitLists.ItemAppearing += JalyuktVisitLists_ItemAppearing1;
            JalyuktVisitLists.Refreshing += JalyuktVisitLists_Refreshing;
            Title = AppResources.LJalyuktaVisits.ToUpper();
        }
        protected async override void OnAppearing()
        {
			Application.Current.Properties["CurrentPage"] = "jalyuktavisits";
            if (Application.Current.Properties.ContainsKey("navigationflag"))
            {
                string navflag = string.Empty;
                navflag = Application.Current.Properties["navigationflag"].ToString();
                if (navflag == "0")
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
                       await CallWebServiceForJalyuktVisitsDetail(index);
                    }
                }
            }
            Application.Current.Properties["CurrentPage"] = "jalyuktavisits";
			lblNoRecord.FontSize = App.GetFontSizeMedium();

        }
        private async void JalyuktVisitLists_Refreshing(object sender, EventArgs e)
        {
            index = 0;
            loadingIndicator.IsVisible = false;
            loading.IsVisible = false;
            loading.IsRunning = false;
            if (lblNoRecord.IsVisible == true)
            {
                SearchFilterText = "";
                FilterJalyuktVisitList.Text = "";
            }
           await CallWebServiceForJalyuktVisitsDetail(index);
        }
        private async void JalyuktVisitLists_ItemAppearing1(object sender, ItemVisibilityEventArgs e)
        {
            try
            {
                if (totalItems != 0)
                {
                    MaxIndex = Math.Ceiling(((decimal)totalListItems) / 5);
                    if (index < MaxIndex)
                    {
                        if (CmVisitList != null && e.Item != null && e.Item == CmVisitList[CmVisitList.Count - 1])
                        {
                            index++;
                            if (index != MaxIndex)
                            {
                                loadingIndicator.IsVisible = true;
                                loading.IsVisible = true;
                                loading.IsRunning = true;
                               await CallWebServiceForJalyuktVisitsDetail(index);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
              if (App.CurrentPage() == "jalyuktavisits")
				{
					if (ex.Message.Contains("Network"))
						await DisplayAlert(AppResources.LError, AppResources.LWebserverNotResponding, AppResources.LOk);
				}
            }
        }
        private void JalyuktVisitLists_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var stack = Navigation.NavigationStack;
            if (stack[stack.Count - 1].GetType() != typeof(JalyuktaVisitsDetail))
            {
                var selectedMaharashtraListItem = sender as Xamarin.Forms.ListView;
                var objectt = selectedMaharashtraListItem.SelectedItem as CmVisit;
                if (objectt != null)
                {
                    Application.Current.Properties["navigationflag"] = "1";
                    Navigation.PushAsync(new CMO.CMVisits.JalyuktaVisitsDetail(objectt));
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
                    if (FilterJalyuktVisitList.Text == "" || FilterJalyuktVisitList.Text == null)
                    { SearchFilterText = ""; }
                    else
                    {
                        SearchFilterText = FilterJalyuktVisitList.Text;
                    }
                    index = 0;
                    loadingIndicator.IsVisible = true;
                    loading.IsRunning = true;
                    loading.IsVisible = true;
                    CmVisitList.Clear();
                   await CallWebServiceForJalyuktVisitsDetail(index);
                }
            }
        }
        public async Task CallWebServiceForJalyuktVisitsDetail(int index)
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
                    values.Add(new KeyValuePair<string, string>("index", Convert.ToString(index)));
                    if (Device.Idiom == TargetIdiom.Phone)
                    { values.Add(new KeyValuePair<string, string>("limit", "7")); }
                    else
                    { values.Add(new KeyValuePair<string, string>("limit", "10")); }
                    //lang=en&title=&index=0&limit=10
                    var response = await GeneralClass.GetResponse<CMO.ServicesClasses.RootObjectCMVisitList>("http://14.141.36.212/maharastracmo/api/cmjalyuktavisits", values);
                    if (response != null)
                    {
                        if (response._resultflag == 1)
                        {
                            totalItems = response.total_results;
                            totalListItems = response.search_results;
                            var x = JalyuktVisitLists.RowHeight;
                            for (int i = 0; i < response.cm_visit.Count; i++)
                            {
                                DateTime oDate = Convert.ToDateTime(response.cm_visit[i].date);
                                response.cm_visit[i].date = oDate.ToString("MMM. dd, yyyy");
                            }
                            if (CmVisitList == null || index == 0)
                            {
                                CmVisitList = new ObservableCollection<CmVisit>();
                            }
                            for (int i = 0; i < response.cm_visit.Count; i++)
                            {
                                var ObjectCmVisitList = new CmVisit();
								ObjectCmVisitList.SetFontSize = App.GetFontSizeMedium();
                                ObjectCmVisitList.date = response.cm_visit[i].date;
                                ObjectCmVisitList.image = response.cm_visit[i].image;
                                ObjectCmVisitList.title = response.cm_visit[i].title;
                                ObjectCmVisitList.description = response.cm_visit[i].description;
                                #region (Text) Specific Design implemention for phone and tablet
                                if (Device.Idiom == TargetIdiom.Phone)
                                {
                                    ObjectCmVisitList.TitleWidth = new GridLength(3, GridUnitType.Star);
                                    ObjectCmVisitList.ImageWidth = new GridLength(2, GridUnitType.Star);
								//	ObjectCmVisitList.TitleFontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
                                }
                                else if (Device.Idiom == TargetIdiom.Tablet)
                                {
                                    ObjectCmVisitList.TitleWidth = new GridLength(4, GridUnitType.Star);
                                    ObjectCmVisitList.ImageWidth = new GridLength(2, GridUnitType.Star);
                               //     ObjectCmVisitList.TitleFontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
                                }
                                else
                                {
                                    ObjectCmVisitList.TitleWidth = new GridLength(4, GridUnitType.Star);
                                    ObjectCmVisitList.ImageWidth = new GridLength(2, GridUnitType.Star);
                                  //  ObjectCmVisitList.TitleFontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
                                }
                                #endregion

                                CmVisitList.Add(ObjectCmVisitList);
                            }
                            //if (JalyuktVisitLists.IsVisible == false)
                            //{
                            //    JalyuktVisitLists.IsVisible = true;
                            //}
                            #region (Row Height) Specific Design implemention for phone and tablet
                            if (Device.Idiom == TargetIdiom.Phone)
                            { JalyuktVisitLists.RowHeight = 100; }
                            else if (Device.Idiom == TargetIdiom.Tablet)
                            { JalyuktVisitLists.RowHeight = 150; }
                            else if (Device.Idiom == TargetIdiom.Desktop)
                            { JalyuktVisitLists.RowHeight = 3 * 100; }
                            else
                            { JalyuktVisitLists.RowHeight = 150; }
                            #endregion

                            JalyuktVisitLists.ItemsSource = CmVisitList;
                            JalyuktVisitLists.IsRefreshing = false;
                           // JalyuktVisitLists.IsVisible = true;
                        }
                        else
                        {
                            if (index == 0)
                            {
                                lblNoRecord.IsVisible = true;
                                CmVisitList.Clear();
                               // JalyuktVisitLists.IsVisible = false;
                                JalyuktVisitLists.IsRefreshing = false;
                            }
                        }
                    }
                    else
                    {
                         if (App.CurrentPage() == "jalyuktavisits")
							await DisplayAlert(AppResources.LError, AppResources.LSomethingWentWrong, AppResources.LOk);
                    }
                }

                catch (WebException ex)
                {
                    if (App.CurrentPage() == "jalyuktavisits")
					{
						if (ex.Message.Contains("Network"))
							await DisplayAlert(AppResources.LError, AppResources.LWebserverNotResponding, AppResources.LOk);
					}
                }
            }
            loading.IsVisible = false;
            loading.IsRunning = false;
            loadingIndicator.IsVisible = false;
            JalyuktVisitLists.IsRefreshing = false;

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
