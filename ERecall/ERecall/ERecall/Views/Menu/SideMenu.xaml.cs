using ERecall.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ERecall.Views.Settings;
using ERecall.Views.InviteFriendsModule;
using ERecall.Views.Notifications;
using ERecall.Controls;

namespace ERecall.Views.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SideMenu : MasterDetailPage
    {
        
        public SideMenu()
        {
            InitializeComponent();
            CreateTapEvents();
            IsPresentedChanged += (object sender, EventArgs e) =>
            {
                DependencyService.Get<IHideKeyboard>().HideKeyboard();
            };

        }

        private void CreateTapEvents()
        {
            var MyAccountTap = new TapGestureRecognizer();
            MyAccountTap.Tapped += MyAccountTap_Tapped;
            MasterPage.stkMyAccount_STACK.GestureRecognizers.Add(MyAccountTap);

            var MyFavouriteTap = new TapGestureRecognizer();
            MyFavouriteTap.Tapped += MyFavourite_Tapped;
            MasterPage.stkMyFavourite_STACK.GestureRecognizers.Add(MyFavouriteTap);

            var VehiclesTap = new TapGestureRecognizer();
            VehiclesTap.Tapped += VehiclesTap_Tapped;
            MasterPage.stkAddVehicle_STACK.GestureRecognizers.Add(VehiclesTap);

            var InviteFriendsTap = new TapGestureRecognizer();
            InviteFriendsTap.Tapped += InviteFriendsTap_Tapped;
            MasterPage.stkInviteFriends_STACK.GestureRecognizers.Add(InviteFriendsTap);

            var NotificationsTap = new TapGestureRecognizer();
            NotificationsTap.Tapped += NotificationsTap_Tapped;
            MasterPage.stkNotification_STACK.GestureRecognizers.Add(NotificationsTap);

            var SettingsTap = new TapGestureRecognizer();
            SettingsTap.Tapped += SettingsTap_Tapped;
            MasterPage.stkSettings_STACK.GestureRecognizers.Add(SettingsTap);

            var LogoutTap = new TapGestureRecognizer();
            LogoutTap.Tapped += LogoutTap_Tapped;
            MasterPage.stkLogout_STACK.GestureRecognizers.Add(LogoutTap);

            var imgCancelTap = new TapGestureRecognizer();
            imgCancelTap.Tapped += ImgCancelTap_Tapped; ;
            MasterPage.imgCancel_IMAGE.GestureRecognizers.Add(imgCancelTap);
        }

        private void ImgCancelTap_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
        }

        private async void LogoutTap_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
            var answer = await DisplayAlert("Logout", "Are you sure?", "Yes", "No");
            if(answer)
            {
                Application.Current.Properties.Clear();
                Application.Current.MainPage = new SignIn_SignUp();
            }
            
        }

        private void SettingsTap_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(ERecall.Views.Settings.Settings)));
        }

        private void NotificationsTap_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(Notification_Tabbed)));
        }

        private void InviteFriendsTap_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(InviteFriends)));
        }

        private void VehiclesTap_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(Vehicle)));
        }

        private void MyFavourite_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(MyeRecalls)));
        }

        private void MyAccountTap_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(MyAccount)));
        }
    }
}