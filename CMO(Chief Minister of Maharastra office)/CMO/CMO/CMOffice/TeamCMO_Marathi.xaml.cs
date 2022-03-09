
using CMO.MenuController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CMO.CMOffice
{
    public partial class TeamCMO_Marathi : ContentPage
    {
        public TeamCMO_Marathi()
        {
            InitializeComponent();
            CallWebService();
        }
        private async void CallWebService()
        {
            if (!App.CheckInternetConnection())
            {
               // await DisplayAlert("Error", App.NoInternetConnectionMessage, "OK");
				  await DisplayAlert(AppResources.LError, AppResources.LWebserverNotResponding, AppResources.LOk);

            }
            else
            {
                try
                {
                    List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>();
                    values.Add(new KeyValuePair<string, string>("page_id", "303"));

                    var response = await GeneralClass.GetResponse<CMO.ServicesClasses.RootObject>("http://14.141.36.212/maharastracmo/api/getpagecontent", values);
                    if (response != null)
                    {
                        if (response._resultflag == 1)
                        {
                            var webcontent = response.page_content;
                            var HTMLString = CreateHTMLForTeamCMO(webcontent);
                            var html = new HtmlWebViewSource
                            {
                                Html = HTMLString
                            };

                            web.Source = html;
                            web.Navigating += (s, e) =>
                            {
                                if (e.Url.StartsWith("http"))
                                {
                                    try
                                    {
                                        var uri = new Uri(e.Url);
                                        Device.OpenUri(uri);
                                    }
                                    catch (Exception)
                                    {
                                        DisplayAlert("Error", App.CatchMessage, "OK");
                                    }

                                    e.Cancel = true;
                                }
                            };
                        }
                        else
                        {
                            await DisplayAlert("Error", App.responseflagMessage, "Ok");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Error", App.responseflagMessage, "Ok");
                    }
                }

                catch (WebException ex)
                {
                    await DisplayAlert("Error", App.CatchMessage, "OK");
                }
            }
        }

        private string CreateHTMLForTeamCMO(string webcontent)
        {
            //var htmlScript = "<script type=\"text/javascript\">"
            //                  + "function getHeight()  {"
            //                  + "return document.getElementById(\"wrapper\").offsetHeight.toString(); }; </script>";

            ////  var externallink ="<script type=\"text/javascript\">for (var i = 0; i < document.links.length; i++) { document.links[i].onclick = function() { window.external.notify('LaunchLink:' + this.href); return false; } }</script>";

            //var css = "<link href='http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/css/style.css' rel='stylesheet' type='text/css'/>";

            //var htmlConcat = string.Format("<html><head>{1}</head>" +
            //                                "<body" +
            //                                ">" +
            //                                "<div>{0}</div></body></html>", webcontent, css);

            //return htmlConcat;

            var head = string.Format("<head>"
               + "<meta http-equiv=\"Content - Type\" content=\"text / html; charset = utf - 8\" />"
               + "<meta name=\"viewport\" content=\"width = device - width, initial - scale = 1, maximum - scale = 1, user - scalable = no\">"
               + "<meta http-equiv=\"X - UA - Compatible\" content=\"IE = edge\" />"
               + "<link href='http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/css/style.css' rel='stylesheet' type='text/css'/>"
               + "<link href = 'http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/css/animate.css' rel ='stylesheet' type ='text/css'/>"
               + "<link href = 'http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/css/responsive.css' rel ='stylesheet' type ='text/css'/>"
               + "<script src=\"http://14.141.36.212/maharastracmo/misc/jquery.js\"></script>"
               + "<script src=\"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/js/functions.js\" ></script>"
               + "<script src=\"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/js/general.js\" ></script>"
               + "</head>");

            var body = string.Format("<body class=\"noJS html not - front not - logged -in no - sidebars page - node page - node - page - node - 15 node - type - article i18n - en \">"
                           //+ "<div id = \"wrapper\" class=\"bg\">"
            //+"<section class=\"content contentarea\">"
            //+ "<div class=\"wrapInner\">"
                //+ "<div class=\"contentInnerPage\">"
                   // + "<div class=\"innerContentIn\">"
                       // + "<div class=\"region region-content\">"
                           // + "<div id = \"block -system-main\" class=\"block block-system\">"
                                + "<div class=\"content\">"
                                    + "<div id = \"node -15\" class=\"node node-article clearfix\" about=\"/maharastracmo/en/former-cms\" typeof=\"sioc:Item foaf:Document\">"
                                        + "<div class=\"field field-name-body field-type-text-with-summary field-label-hidden\">"
                                            + "<div class=\"field -items\">"
                                                + "<div class=\"field -item even\" property=\"content:encoded\">"
                                                   + " {0}"
                                                + "</div>"
                                            + "</div>"
                                        + "</div>"
                                    + "</div>"
                                + "</div>"
                           // + "</div>"
                      //  + "</div>"
                   // + "</div>"
               // + "</div>"
           // + "</div>"
    //+ "</section>"
   // + "</div>"

    + "<script type = \"text /javascript\" src=\"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/js/jquery-min.js?obp57t\"></script>"
            + "<script type = \"text /javascript\" src=\"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/js/modernizr.js?obp57t\"></script>"
            + "<script type = \"text /javascript\" src=\"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/js/functions.js?obp57t\"></script>"
            + "<script type = \"text /javascript\" src=\"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/js/general.js?obp57t\"></script>"
            + "<script type = \"text /javascript\" src=\"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/js/bruteforce.js?obp57t\"></script>"
            + "</body>", webcontent);

            var finalhtml = string.Format("<html>{0}{1}</html>", head, body);

            return finalhtml;
        }

        #region Exit Application
        private bool _canClose = true;

        protected override bool OnBackButtonPressed()
        {
            //if (_canClose)
            //{
            //    ShowExitDialog();
            //}
            //return _canClose;
            Application.Current.MainPage = new SideMenu();
            return true;
        }

        private async void ShowExitDialog()
        {
            var answer = await DisplayAlert("Exit", "Do you want to exit the App?", "Yes", "No");
            if (answer)
            {
                if (Device.OS == TargetPlatform.Android)
                {
                    DependencyService.Get<IAndroidMethods>().CloseApp();
                }
                _canClose = false;
                OnBackButtonPressed();
            }
        }
        #endregion
    }
}
