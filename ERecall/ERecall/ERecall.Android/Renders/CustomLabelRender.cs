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
using ERecall.Controls;
using ERecall.Droid.Renders;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Text;

[assembly: ExportRenderer(typeof(CustomLabel), typeof(CustomLabelRender))]
namespace ERecall.Droid.Renders
{
    public class CustomLabelRender : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.LayoutChange += (s, args) =>
                {
                    Control.Ellipsize = TextUtils.TruncateAt.End;
                    Control.SetMaxLines(2);
                };
            }
        }
    }
}