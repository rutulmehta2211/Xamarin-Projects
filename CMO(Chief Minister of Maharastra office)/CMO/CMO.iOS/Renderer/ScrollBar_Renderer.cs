using CMO.iOS.Renderer;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ScrollView), typeof(ScrollBar_Renderer))]
namespace CMO.iOS.Renderer
{
  public class ScrollBar_Renderer : ScrollViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            UIScrollView iosScrollView = (UIScrollView)NativeView;
           // iosScrollView.PagingEnabled = true;
            iosScrollView.ShowsVerticalScrollIndicator = false;
        }
    }
}
