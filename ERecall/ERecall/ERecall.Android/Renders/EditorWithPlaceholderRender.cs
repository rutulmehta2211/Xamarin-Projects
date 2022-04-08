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
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ERecall.Droid.Renders;
using ERecall.Controls;

[assembly: ExportRenderer(typeof(EditorWithPlaceholder), typeof(EditorWithPlaceholderRender))]
namespace ERecall.Droid.Renders
{
    public class EditorWithPlaceholderRender : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (Element == null)
                return;

            var element = (EditorWithPlaceholder)Element;

            Control.Hint = element.Placeholder;
            Control?.SetHintTextColor(element.PlaceholderColor.ToAndroid());
            Control?.SetBackgroundColor(Android.Graphics.Color.Transparent);
        }
    }
}