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
using Xamarin.Forms.Platform.Android;
using WebView = Android.Webkit.WebView;
using Xamarin.Forms;
using ERecall.Controls;
using ERecall.Droid.Renders;

[assembly: ExportRenderer(typeof(ExtendedWebview), typeof(ExtendedWebviewRender))]
namespace ERecall.Droid.Renders
{
    public class ExtendedWebviewRender : WebViewRenderer
    {
        static ExtendedWebview _xwebView = null;
        WebView _webView;

        class ExtendedWebViewClient : Android.Webkit.WebViewClient
        {
            public override async void OnPageFinished(WebView view, string url)
            {
                if (_xwebView != null)
                {
                    int i = 10;
                    while (view.ContentHeight == 0 && i-- > 0) // wait here till content is rendered
                        await System.Threading.Tasks.Task.Delay(100);
                    double newHeight = view.ContentHeight - ((view.ContentHeight * 83.14) / 100);
                    //_xwebView.HeightRequest = view.ContentHeight;
                    _xwebView.HeightRequest = newHeight;
                }
                base.OnPageFinished(view, url);
            }
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
        {
            base.OnElementChanged(e);
            _xwebView = e.NewElement as ExtendedWebview;
            _webView = Control;

            if (e.OldElement == null)
            {
                _webView.SetWebViewClient(new ExtendedWebViewClient());
            }
        }
    }
}