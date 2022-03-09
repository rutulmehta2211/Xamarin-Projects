using CMO.iOS.Renderer;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRender_Ios))]
namespace CMO.iOS.Renderer
{
    public class CustomEntryRender_Ios : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
               Control.BorderStyle = UITextBorderStyle.None;
            }
        }
    }
}
