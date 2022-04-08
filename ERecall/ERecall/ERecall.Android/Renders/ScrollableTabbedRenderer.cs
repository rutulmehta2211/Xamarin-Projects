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
using ERecall.Droid.Renders;
using ERecall.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;
using Android.Support.Design.Widget;

[assembly: ExportRenderer(typeof(ScrollableTabbed), typeof(ScrollableTabbedRenderer))]
namespace ERecall.Droid.Renders
{
    public class ScrollableTabbedRenderer : TabbedPageRenderer
    {
        public override void OnViewAdded(Android.Views.View child)
        {
            base.OnViewAdded(child);
            var tabLayout = child as TabLayout;
            if (tabLayout != null)
            {
                tabLayout.TabMode = TabLayout.ModeScrollable;
            }
        }
    }
}