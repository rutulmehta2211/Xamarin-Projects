using Acr.UserDialogs;
using ERecall.Controls;
using ERecall.Interfaces;
using ERecall.Views.Menu;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ERecall.Views.InviteFriendsModule
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InviteFriends : ContentPage
    {
        public static bool IsSMS = false;
        public static bool IsEmail = false;
        public InviteFriends()
        {
            InitializeComponent();
        }

        private async void InviteViaMail_Tapped(object sender, EventArgs e)
        {
            try
            {
                IsSMS = false;
                IsEmail = true;
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Contacts);
                if (status != PermissionStatus.Granted)
                {
                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Contacts);
                    status = results[Permission.Contacts];
                }
                if (status == PermissionStatus.Granted)
                {
                    DependencyService.Get<IOpenContacts>().OpenContactApplication();
                }
            }
            catch (Exception ex)
            {
                new ShowUserDialog(UserDialogs.Instance, ex.Message, System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }

        private async void InviteViaMessage_Tapped(object sender, EventArgs e)
        {
            try
            {
                IsSMS = true;
                IsEmail = false;
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Contacts);
                if (status != PermissionStatus.Granted)
                {
                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Contacts);
                    status = results[Permission.Contacts];
                }
                if (status == PermissionStatus.Granted)
                {
                    DependencyService.Get<IOpenContacts>().OpenContactApplication();
                }
            }
            catch (Exception ex)
            {
                new ShowUserDialog(UserDialogs.Instance, ex.Message, System.Drawing.Color.Red, System.Drawing.Color.White);
            }
        }

        protected override bool OnBackButtonPressed()
        {
            Application.Current.MainPage = new SideMenu();
            return true;
        }
    }
}