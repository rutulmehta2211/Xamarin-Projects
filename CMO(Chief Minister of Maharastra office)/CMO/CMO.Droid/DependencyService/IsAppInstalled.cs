using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CMO.MenuController;
using Android.Content.PM;

[assembly: Xamarin.Forms.Dependency(typeof(CMO.Droid.DependencyService.IsAppInstalled))]
namespace CMO.Droid.DependencyService
{
    public class IsAppInstalled : IIsAppInstalled
    {
        public string AppVersion { get; set; }
        public void NavigatePage(string Package_Name)
        {
            try
            {
                Intent intent = Application.Context.PackageManager.GetLaunchIntentForPackage(Package_Name);
                Application.Context.StartActivity(intent);
            }
            catch (Exception ex)
            {
                var intent = new Intent(Intent.ActionView, Android.Net.Uri.Parse("market://details?id=" + Package_Name));
                intent.AddFlags(ActivityFlags.NewTask);
                Application.Context.StartActivity(intent);
            }
        }
        bool IIsAppInstalled.IsAppInstalled(String packageName)
        {
            PackageManager pm = Application.Context.PackageManager;
            bool installed = false;
            try
            {
                pm.GetPackageInfo(packageName, PackageInfoFlags.Activities);
                installed = true;
            }
            catch (PackageManager.NameNotFoundException e)
            {
                installed = false;
            }
            return installed;
        }
    }
}