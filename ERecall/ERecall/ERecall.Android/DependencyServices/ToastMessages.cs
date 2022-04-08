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

[assembly: Xamarin.Forms.Dependency(typeof(ToastMessages))]
namespace ERecall.Droid.DependencyServices
{
    public class ToastMessages : IToastMessages
    {
        public void LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
    }
}