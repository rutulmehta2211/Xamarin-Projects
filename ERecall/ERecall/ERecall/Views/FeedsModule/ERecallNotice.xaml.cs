using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ERecall.Views.FeedsModule
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ERecallNotice : ContentPage
	{
		public ERecallNotice (string htmlString)
		{
			InitializeComponent ();
            string html = CreateHTML(htmlString);
            HtmlWebViewSource htmlWebViewSource = new HtmlWebViewSource();
            htmlWebViewSource.Html = html;
            webVieweRecallNotice.Source = htmlWebViewSource;

            webVieweRecallNotice.Navigating += (s, e) =>
            {
                if (e.Url.StartsWith("http") || e.Url.StartsWith("www"))
                {
                    var uri = new Uri(e.Url);
                    Device.OpenUri(uri);
                }
            };
        }
        private string CreateHTML(string webcontent)
        {
            string content = @"<!doctype html>" +
                              "<body>" +
                                  webcontent +
                              "</body>" +
                          "</html>";
            return content;
        }
    }
}