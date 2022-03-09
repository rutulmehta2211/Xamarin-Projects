using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace CMO
{
    public class App : Application
    {
		public static int FontSize;


        public static int DeviceWidth;
        public static int DeviceHeight;
        public static string responseflagMessage;
        public static string CatchMessage;
        public static string NoInternetConnectionMessage;
		public App(int _deviceWidth, int _deviceHeight)
		{
			DeviceWidth = _deviceWidth;
			DeviceHeight = _deviceHeight;
			Application.Current.Properties["Language"] = "en";
			Application.Current.Properties["CurrentPage"] = "home";
			AppResources.Culture = new System.Globalization.CultureInfo("en");

			MainPage = new CMO.MenuController.SideMenu();
			// = NavigationPage( );
		}
		public static int GetFontSizeSmall()
		{
			#region ios
			if (TargetPlatform.iOS == Device.OS)
			{
				if (Device.Idiom == TargetIdiom.Tablet)
				{ return 15; }
				else if (Device.Idiom == TargetIdiom.Phone)
				{ return 10; }
			}
			#endregion
			#region android
			else if (TargetPlatform.Android == Device.OS)
			{

				if (Device.Idiom == TargetIdiom.Tablet)
				{ return 14; }
				else if (Device.Idiom == TargetIdiom.Phone)
				{ return 12; }
			}
			#endregion
			return 10;
		}

		public static int GetFontSizeMedium()
		{
			#region ios
			if (TargetPlatform.iOS == Device.OS)
			{
				if (Device.Idiom == TargetIdiom.Tablet)
				{ return 16; }
				else if (Device.Idiom == TargetIdiom.Phone)
				{ return 12; }
			}
			#endregion
			#region android
			else if (TargetPlatform.Android == Device.OS)
			{

				if (Device.Idiom == TargetIdiom.Tablet)
				{ return 17; }
				else if (Device.Idiom == TargetIdiom.Phone)
				{ return 14; }
			}
			#endregion
			return 10;
		}
		public static int GetFontSizeTitle()
		{
			#region ios
			if (TargetPlatform.iOS == Device.OS)
			{
				if (Device.Idiom == TargetIdiom.Tablet)
				{ return 17; }
				else if (Device.Idiom == TargetIdiom.Phone)
				{ return 13; }
			}
			#endregion
			#region android
			else if (TargetPlatform.Android == Device.OS)
			{
				if (Device.Idiom == TargetIdiom.Tablet)
				{ return 19; }
				else if (Device.Idiom == TargetIdiom.Phone)
				{ return 15; }
            }
			#endregion
			return 15;
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
        public static bool CheckInternetConnection()
        {
            var networkConnection = DependencyService.Get<INetworkConnection>();
            networkConnection.CheckNetworkConnection();
            if (Device.OS == TargetPlatform.Android)
            {
                var networkStatus = networkConnection.IsConnected ? "Connected" : "Not Connected";
                return networkConnection.IsConnected;
            }

            if (Device.OS == TargetPlatform.iOS)
            {
                var ntwrkIOS = CrossConnectivity.Current.IsConnected ? "Connected" : "No Connection";
                return CrossConnectivity.Current.IsConnected;
            }
            return false;
        }
        public static string CurrentPage()
        {
            string _currentPage = "";
            if (Application.Current.Properties.ContainsKey("CurrentPage"))
            { _currentPage = Application.Current.Properties["CurrentPage"] as string; }
            return _currentPage;
        }

        static ContentPage page;
        public App()
        {
			var connectivityButton = new Button
            {
                Text = "Connectivity Test"
            };

            var connected = new Label
            {
                Text = "Is Connected: "
            };

            var connectionTypes = new Label
            {
                Text = "Connection Types: "
            };

            var bandwidths = new Label
            {
                Text = "Bandwidths"
            };

            var host = new Entry
            {
                Text = "127.0.0.1"
            };


            var host2 = new Entry
            {
                Text = "montemagno.com"
            };

            var port = new Entry
            {
                Text = "80",
                Keyboard = Keyboard.Numeric
            };

            var canReach1 = new Label
            {
                Text = "Can reach1: "
            };

            var canReach2 = new Label
            {
                Text = "Can reach2: "
            };


            connectivityButton.Clicked += async (sender, args) =>
            {
                connected.Text = CrossConnectivity.Current.IsConnected ? "Connected" : "No Connection";
                bandwidths.Text = "Bandwidths: ";
                foreach (var band in CrossConnectivity.Current.Bandwidths)
                {
                    bandwidths.Text += band.ToString() + ", ";
                }
                connectionTypes.Text = "ConnectionTypes:  ";
                foreach (var band in CrossConnectivity.Current.ConnectionTypes)
                {
                    connectionTypes.Text += band.ToString() + ", ";
                }

                try
                {
                    canReach1.Text = await CrossConnectivity.Current.IsReachable(host.Text) ? "Reachable" : "Not reachable";

                }
                catch (Exception ex)
                {

                }
                try
                {
                    canReach2.Text = await CrossConnectivity.Current.IsRemoteReachable(host2.Text, int.Parse(port.Text)) ? "Reachable" : "Not reachable";

                }
                catch (Exception ex)
                {

                }


            };


            CrossConnectivity.Current.ConnectivityChanged += (sender, args) =>
            {
                page.DisplayAlert("Connectivity Changed", "IsConnected: " + args.IsConnected.ToString(), "OK");
            };


            // The root page of your application
            MainPage = page = new ContentPage
            {
                Content = new StackLayout
                {
                    Padding = 50,
                    VerticalOptions = LayoutOptions.Center,
                    Children =
                    {
                          connectivityButton,
                          connected,
                          bandwidths,
                          connectionTypes,
                          host,
                          host2,
                          port,
                          canReach1,
                          canReach2,
                    }
                }
            };
        }
    }
}
