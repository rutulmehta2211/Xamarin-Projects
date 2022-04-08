using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using FFImageLoading.Forms.Droid;
using Refractored.XamForms.PullToRefresh.Droid;
using CarouselView.FormsPlugin.Android;
using Android.Content;
using Xamarin.Forms;
using ERecall.Views.InviteFriendsModule;
using System.Linq;
using Plugin.Messaging;
using HockeyApp.Android;
using HockeyApp.Android.Metrics;
using Acr.UserDialogs;
using Android.Gms.Common;
using Android.Util;

namespace ERecall.Droid
{
    [Activity(Label = "ERecall", Icon = "@drawable/icon", Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait, MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static Android.Support.V7.Widget.Toolbar ToolBar { get; private set; }

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            #region Custom Control Initializations
            CachedImageRenderer.Init();
            PullToRefreshLayoutRenderer.Init();
            CarouselViewRenderer.Init();
            UserDialogs.Init(this);
            #endregion

            global::Xamarin.Forms.Forms.Init(this, bundle);

            #region Get Device Width and Height
            var metrics = Resources.DisplayMetrics;
            int widthInDp = (int)(metrics.WidthPixels / metrics.Density);
            int heightInDp = (int)(metrics.HeightPixels / metrics.Density);
            #endregion

            //Add User matrics
            MetricsManager.Register(Application, "17733d00adc94e5cae44023c7791a94d");

            //Add Update Distributions
            CheckForUpdates();

            //Google play service available or not
            IsPlayServicesAvailable();

            LoadApplication(new App(widthInDp, heightInDp));
        }

        protected override void OnResume()
        {
            base.OnResume();
            //Add Crash reports
            CrashManager.Register(this, "17733d00adc94e5cae44023c7791a94d");

            //Google play service available or not
            IsPlayServicesAvailable();
        }
        
        protected override void OnPause()
        {
            base.OnPause();
            UnregisterManagers();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            UnregisterManagers();
        }

        private void CheckForUpdates()
        {
            // Remove this for store builds!
            UpdateManager.Register(this, "17733d00adc94e5cae44023c7791a94d");
        }

        private void UnregisterManagers()
        {
            UpdateManager.Unregister();
        }

        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    Log.Debug("IsPlayServicesAvailable", GoogleApiAvailability.Instance.GetErrorString(resultCode));
                else
                {
                    Log.Debug("IsPlayServicesAvailable", "This device is not supported");
                    Finish();
                }
                return false;
            }
            else
            {
                Log.Debug("IsPlayServicesAvailable", "Google Play Services is available.");
                return true;
            }
        }

    }
}

