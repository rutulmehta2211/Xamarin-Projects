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
using CMO.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Graphics;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Switch), typeof(SwitchRendering))]
namespace CMO.Droid.Renderers
{
    public class SwitchRendering : SwitchRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Switch> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                //for switch dott color
                //var x = Control.ThumbDrawable;
                //Android.Graphics.ColorFilter xyz = new Android.Graphics.ColorFilter();
                //x.SetColorFilter(Android.Graphics.Color.Orange, PorterDuff.Mode.Multiply);

                ////for switch background bar color
                var y = Control.TrackDrawable;
                y.SetColorFilter(Android.Graphics.Color.White, PorterDuff.Mode.Multiply);
            }
        }
    }
}