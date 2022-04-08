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

[assembly: Xamarin.Forms.Dependency(typeof(CloseApp))]
namespace ERecall.Droid.DependencyServices
{
    public class CloseApp : ICloseApp
    {
        void ICloseApp.CloseApp()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}