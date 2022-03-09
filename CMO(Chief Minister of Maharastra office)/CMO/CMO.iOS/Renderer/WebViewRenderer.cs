using CMO.iOS.Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using CoreGraphics;

[assembly: ExportRenderer(typeof(WebView), typeof(WebViewRendererCustom))]
namespace CMO.iOS.Renderer
{
    public class WebViewRendererCustom : WebViewRenderer
    {
        void WebView_LoadFinished (object sender, EventArgs e)
		{
			var webview = this;

		}

		protected override void OnElementChanged(VisualElementChangedEventArgs e)
		{
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {   // perform initial setup
                // lets get a reference to the native UIWebView
                var webView = this;
                webView.ScrollView.Delegate = new ScrollViewDelegate();
                webView.ScalesPageToFit = false;
				webView.BackgroundColor = UIColor.FromRGB(9, 9, 26);
				webView.ScrollView.ScrollEnabled = true;
                webView.ScrollView.AlwaysBounceVertical = false;
				webView.ScrollView.Bounces = false;
				webView.ScrollView.BouncesZoom = false;
				webView.ScrollView.AlwaysBounceHorizontal = false;
              //  webView.ContentMode = UIViewContentMode.ScaleAspectFit;
			//	CGSize contentSize = webView.ScrollView.ContentSize;
			//	CGSize viewSize = webView.Bounds.Size;
			//	nfloat rw = viewSize.Width / contentSize.Width;
				//webView.ScrollView.MinimumZoomScale = rw;
				//	webView.ScrollView.MaximumZoomScale = rw;
				//	webView.ScrollView.ZoomScale = rw;
			//	webView.EvaluateJavascript("document.body.style.zoom = 1.5;");

				//webView.Frame = webView.Bounds;
            }
        }
    }
    public class ScrollViewDelegate : UIScrollViewDelegate
    {
        public override void DraggingStarted(UIScrollView scrollView)
        {
            Console.WriteLine("DraggingStarted");
        }
    }


}