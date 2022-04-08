using ERecall.CommonClasses;
using ERecall.ServiceLayer;
using ERecall.ServiceLayer.ServiceModel;
using FFImageLoading.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static ERecall.ServiceLayer.ServiceModel.GetAccountProfileByUserId;

namespace ERecall.Views.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SideMenuMaster : ContentPage
    {
        public ListView ListView;

        public StackLayout stkMyAccount_STACK { get { return stkMyAccount; } }
        public StackLayout stkMyFavourite_STACK { get { return stkMyFavourite; } }
        public StackLayout stkAddVehicle_STACK { get { return stkAddVehicle; } }
        public StackLayout stkInviteFriends_STACK { get { return stkInviteFriends; } }
        public StackLayout stkNotification_STACK { get { return stkNotification; } }
        public StackLayout stkSettings_STACK { get { return stkSettings; } }
        public StackLayout stkLogout_STACK { get { return stkLogout; } }
        public CachedImage imgCancel_IMAGE { get { return imgCancel; } }

        public SideMenuMaster()
        {
            InitializeComponent();
            BindingContext = new SideMenuMasterViewModel();
            ((BaseViewModel)BindingContext).Initialize(this);
            //ListView = MenuItemsListView;
        }
        class SideMenuMasterViewModel : BaseViewModel
        {

            List<Response> objResponse;
            #region IsBusy Property
            bool isBusy;
            public bool IsBusy
            {
                get { return isBusy; }
                set
                {
                    if (isBusy == value)
                        return;

                    isBusy = value;
                    OnPropertyChanged("IsBusy");
                }
            }
            #endregion
            #region All Properties
            int _count_ImporedProductsCount = App.User_ImporedProductsCount;
            public int count_ImporedProductsCount
            {
                get { return _count_ImporedProductsCount; }
                set
                {
                    if (_count_ImporedProductsCount == value)
                        return;

                    _count_ImporedProductsCount = value;
                    OnPropertyChanged("count_ImporedProductsCount");
                }
            }
            int _count_OpenTickets = App.User_OpenTickets;
            public int count_OpenTickets
            {
                get { return _count_OpenTickets; }
                set
                {
                    if (_count_OpenTickets == value)
                        return;

                    _count_OpenTickets = value;
                    OnPropertyChanged("count_OpenTickets");
                }
            }
            int _count_AvailableReports = App.User_AvailableReports;
            public int count_AvailableReports
            {
                get { return _count_AvailableReports; }
                set
                {
                    if (_count_AvailableReports == value)
                        return;

                    _count_AvailableReports = value;
                    OnPropertyChanged("count_AvailableReports");
                }
            }
            int _Credits = App.User_Credits;
            public int Credits
            {
                get { return _Credits; }
                set
                {
                    if (_Credits == value)
                        return;

                    _Credits = value;
                    OnPropertyChanged("Credits");
                }
            }
            string _name = App.User_Name;
            public string Name
            {
                get { return _name; }
                set
                {
                    if (_name == value)
                        return;

                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
            int _count_Notifications = App.User_NotificationCount;
            public int count_Notifications
            {
                get { return _count_Notifications; }
                set
                {
                    if (_count_Notifications == value)
                        return;

                    _count_Notifications = value;
                    OnPropertyChanged("count_Notifications");
                }
            }
            #endregion

            ICommand refreshCommand;
            public ICommand RefreshCommand
            {
                get { return refreshCommand ?? (refreshCommand = new Command(async () => await ExecuteRefreshCommand())); }
            }

            async Task ExecuteRefreshCommand()
            {
                if (IsBusy)
                    return;
                if (CheckInternetConnection.IsInternetConnected())
                {
                    IsBusy = true;
                    await GetService_GetAccountProfileByUserId();
                    await GetService_Notifications();
                    IsBusy = false;
                }
                else
                {
                    return;
                }
            }

            public async Task GetService_GetAccountProfileByUserId()
            {
                try
                {
                    var Service_response = await GetResponseFromWebService.GetResponse<GetAccountProfileByUserId.RootObject>(ServiceURLs.GetAccountProfileByUserId + App.User_UserId);
                    if (Service_response != null)
                    {
                        if (Service_response.status)
                        {
                            if (Service_response.response != null && Service_response.response.Count > 0)
                            {
                                objResponse = new List<Response>();
                                for (int i = 0; i < Service_response.response.Count; i++)
                                {
                                    Response objResponse = new Response();
                                    objResponse.Name = Name = _name = Service_response.response[i].Name;
                                    Application.Current.Properties["User_ImporedProductsCount"] = objResponse.ImporedProductsCount = count_ImporedProductsCount = App.User_ImporedProductsCount = Service_response.response[i].ImporedProductsCount;
                                    Application.Current.Properties["User_OpenTickets"] = objResponse.OpenTickets = count_OpenTickets = App.User_OpenTickets = Service_response.response[i].OpenTickets;
                                    Application.Current.Properties["User_AvailableReports"] = objResponse.AvailableReports = count_AvailableReports = App.User_AvailableReports = Service_response.response[i].AvailableReports;
                                    Application.Current.Properties["User_Credits"] = objResponse.Credits = Credits = App.User_Credits = Service_response.response[i].Credits;
                                }
                            }
                        }
                        else
                        {
                            count_AvailableReports = count_ImporedProductsCount = count_AvailableReports = Credits = 0;
                        }
                    }
                    else
                    {
                        count_AvailableReports = count_ImporedProductsCount = count_AvailableReports = Credits = 0;
                    }
                }
                catch (WebException ex)
                {
                    count_AvailableReports = count_ImporedProductsCount = count_AvailableReports = Credits = 0;
                }
            }

            private async Task GetService_Notifications()
            {
                try
                {
                    string url = ServiceURLs.Notifications + "userName=" + App.User_Email;
                    var Service_response = await GetResponseFromWebService.GetResponse<ServiceLayer.ServiceModel.Notifications.RootObject>(url);
                    if (Service_response != null)
                    {
                        if (Service_response.status)
                        {
                            if (Service_response.response != null && Service_response.response.Count > 0)
                            {
                                var objLstNewProducts = Service_response.response.Where(d => d.IsRead == false).ToList();
                                if(objLstNewProducts!=null && objLstNewProducts.Count>0)
                                {
                                    Application.Current.Properties["User_NotificationCount"] = count_Notifications = App.User_NotificationCount = objLstNewProducts.Count;
                                }
                                else
                                {
                                    Application.Current.Properties["User_NotificationCount"] = count_Notifications = App.User_NotificationCount = 0;
                                }
                            }
                        }
                        else
                        {
                            Application.Current.Properties["User_NotificationCount"] = count_Notifications = App.User_NotificationCount = 0;
                        }
                    }
                    else
                    {
                        Application.Current.Properties["User_NotificationCount"] = count_Notifications = App.User_NotificationCount = 0;
                    }
                }
                catch (WebException ex)
                {
                    Application.Current.Properties["User_NotificationCount"] = count_Notifications = App.User_NotificationCount = 0;
                }
            }
        }
    }
}