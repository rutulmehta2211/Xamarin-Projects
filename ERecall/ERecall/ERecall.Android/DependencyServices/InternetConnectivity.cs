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
using ERecall.Interfaces;
using ERecall.Droid.DependencyServices;

[assembly: Xamarin.Forms.Dependency(typeof(InternetConnectivity))]
namespace ERecall.Droid.DependencyServices
{
    public class InternetConnectivity : IInternetConnectivity
    {
        public bool IsInternetConnected()
        {
            var connectivityManager = (Android.Net.ConnectivityManager)Android.App.Application.Context.GetSystemService(Context.ConnectivityService);
            var activeNetworkInfo = connectivityManager.ActiveNetworkInfo;
            return (activeNetworkInfo != null && activeNetworkInfo.IsConnectedOrConnecting);
        }
    }
}