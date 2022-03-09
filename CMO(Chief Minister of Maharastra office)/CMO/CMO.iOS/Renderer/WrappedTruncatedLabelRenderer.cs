using CMO.Gallery;
using CMO.iOS.Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(WrappedTruncatedLabel), typeof(WrappedTruncatedLabelRenderer))]
namespace CMO.iOS.Renderer
{
    public class WrappedTruncatedLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.LineBreakMode = UILineBreakMode.TailTruncation;
                Control.Lines = 2;
            }

          //  UIFont font;

            //Control.Font = font;
        }
    }
}