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
using Xamarin.Forms;
using Android.Provider;
using Android.Content.Res;
using Android.Util;

[assembly: Xamarin.Forms.Dependency(typeof(DeviceId))]
namespace ERecall.Droid.DependencyServices
{
    public class DeviceId : IDeviceId
    {
        public string DeviceIdentifier()
        {
            return Settings.Secure.GetString(Forms.Context.ContentResolver, Settings.Secure.AndroidId);
        }
    }
}