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
using Android.Util;
using Android.Content.Res;

[assembly: Xamarin.Forms.Dependency(typeof(CMO.Droid.DependencyService.ActionBarHeight))]
namespace CMO.Droid.DependencyService
{
    public class ActionBarHeight : IActionBarHeight
    {
        public string GetActionBarHeight()
        {
            TypedValue tv = new TypedValue();

            //if (getTheme().resolveAttribute(android.R.attr.actionBarSize, tv, true))
            //{
            //  int y=  Resource.Styleable.ActionBar_height;
            //    //DisplayMetrics metrics = Resources.DisplayMetrics;
            //  //  var y = TypedValue.ComplexToDimensionPixelSize(tv.Data,)
            //  //  actionBarHeight = TypedValue.complexToDimensionPixelSize(tv.Data, getResources().getDisplayMetrics());
            //}
            int y = Resource.Styleable.ActionBar_height;
            var x = Android.Resource.Attribute.ActionBarSize;
            return "hello";
        }
    }
}