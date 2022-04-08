using ERecall.Views;
using ERecall.Views.CommonPages;
using ERecall.Views.Menu;
using ERecall.Views.MyERecallsModule;
using ERecall.Views.Notifications;
using ERecall.Views.ResolutionModule;
using ERecall.Views.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ERecall
{
    public partial class App : Application
    {
        #region All Static variables
        public static int DeviceWidth;
        public static int DeviceHeight;

        public static string User_Email = string.Empty;
        public static string User_Name = string.Empty;
        public static string User_UserName = string.Empty;
        public static int User_UserId = 0;
        public static string User_Id = string.Empty;
        public static int User_ImporedProductsCount = 0;
        public static int User_OpenTickets = 0;
        public static int User_AvailableReports = 0;
        public static int User_Credits = 0;
        public static int User_NotificationCount = 0;
        public static int User_AccountId = 0;
        public static int User_AccountTypeId = 0;
        public static string User_AccountTypeName = string.Empty;
        
        #endregion

        public App(int Width,int Height)
        {
            InitializeComponent();
            DeviceWidth = Width;
            DeviceHeight = Height;
            if(Application.Current.Properties.ContainsKey("User_Email") &&
               Application.Current.Properties.ContainsKey("User_Name") &&
               Application.Current.Properties.ContainsKey("User_UserName") &&
               Application.Current.Properties.ContainsKey("User_UserId") &&
               Application.Current.Properties.ContainsKey("User_Id") &&
               Application.Current.Properties.ContainsKey("User_ImporedProductsCount") &&
               Application.Current.Properties.ContainsKey("User_OpenTickets") &&
               Application.Current.Properties.ContainsKey("User_AvailableReports") &&
               Application.Current.Properties.ContainsKey("User_Credits") &&
               Application.Current.Properties.ContainsKey("User_AccountTypeId") &&
               Application.Current.Properties.ContainsKey("User_AccountTypeName") &&
               Application.Current.Properties.ContainsKey("User_AccountId") &&
               Application.Current.Properties.ContainsKey("User_NotificationCount"))
            {
                App.User_Email = (string)Application.Current.Properties["User_Email"];
                App.User_Name = (string)Application.Current.Properties["User_Name"];
                App.User_UserName = (string)Application.Current.Properties["User_UserName"];
                App.User_UserId = (int)Application.Current.Properties["User_UserId"];
                App.User_Id = (string)Application.Current.Properties["User_Id"];
                App.User_ImporedProductsCount = (int)Application.Current.Properties["User_ImporedProductsCount"];
                App.User_OpenTickets = (int)Application.Current.Properties["User_OpenTickets"];
                App.User_AvailableReports = (int)Application.Current.Properties["User_AvailableReports"];
                App.User_Credits = (int)Application.Current.Properties["User_Credits"];
                App.User_AccountTypeId = (int)Application.Current.Properties["User_AccountTypeId"];
                App.User_AccountTypeName = (string)Application.Current.Properties["User_AccountTypeName"];
                App.User_AccountId = (int)Application.Current.Properties["User_AccountId"];
                App.User_NotificationCount = (int)Application.Current.Properties["User_NotificationCount"];

                MainPage = new SideMenu();
            }
            else
            { 
                MainPage = new SignIn_SignUp();
            }
            //MainPage = new NavigationPage(new ResolutionDetail("20"));
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
