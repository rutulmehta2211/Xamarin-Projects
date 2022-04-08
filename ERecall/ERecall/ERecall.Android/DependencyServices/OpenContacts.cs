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
using ERecall.Droid.Activities;

[assembly: Xamarin.Forms.Dependency(typeof(OpenContacts))]
namespace ERecall.Droid.DependencyServices
{
    class OpenContacts : IOpenContacts
    {
        public void OpenContactApplication()
        {
            var intent = new Intent(Forms.Context, typeof(ContactApplicationActivity));
            Forms.Context.StartActivity(intent);
        }
    }
}