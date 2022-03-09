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
    public partial class FormerCMs : ContentPage
    {
        public FormerCMs()
        {
            InitializeComponent();
           
            this.Title = AppResources.LFormercmo.ToUpper();
        }
        protected async override void OnAppearing()
        {
            Application.Current.Properties["CurrentPage"] = "formercm";
            if (!App.CheckInternetConnection())
            {
                await DisplayAlert(AppResources.LNetworkError, AppResources.LNoInternetConnection, AppResources.LOk);
            }
            else
            {
               await CallWebService();
            }
            
        }
        private async Task CallWebService()
        {
            if (!App.CheckInternetConnection())
            {
                await DisplayAlert(AppResources.LNetworkError, AppResources.LNoInternetConnection, AppResources.LOk);
            }
            else
            {
                try
                {
                    List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>();

                    string lang = "";
                    if (Application.Current.Properties.ContainsKey("Language"))
                    { lang = Application.Current.Properties["Language"] as string; }

                    if (lang == "en")
                        values.Add(new KeyValuePair<string, string>("page_id", "15"));
                    else
                        values.Add(new KeyValuePair<string, string>("page_id", "304"));

                    var response = await GeneralClass.GetResponse<CMO.ServicesClasses.RootObject>("http://14.141.36.212/maharastracmo/api/getpagecontent", values);
					if (response != null)
					{
						if (response._resultflag == 1)
						{
							string webcontent = response.page_content;
							var HTMLString = CreateHTMLForFormerCMs(webcontent);
							var html = new HtmlWebViewSource
							{
								Html = HTMLString
							};

							web.Source = html;
							web.Navigating += async (s, e) =>
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
										await DisplayAlert(AppResources.LError, App.CatchMessage, AppResources.LOk);
									}

									e.Cancel = true;
								}
							};
						}
						else
						{
							if (App.CurrentPage() == "formercm")
								await DisplayAlert(AppResources.LError, AppResources.LSomethingWentWrong, AppResources.LOk);
							
						}
					}
					else
					{
						if (App.CurrentPage() == "formercm")
						await DisplayAlert(AppResources.LError, AppResources.LSomethingWentWrong, AppResources.LOk);
					}
						
                }

                catch (WebException ex)
                {
					if (App.CurrentPage() == "formercm")
					{
						if (ex.Message.Contains("Network"))
							await DisplayAlert(AppResources.LError, AppResources.LWebserverNotResponding, AppResources.LOk);
					}
                }
            }
        }
		//     private string CreateHTMLForFormerCMs(string webcontent)
		//     {
		//         //var webcontentnew = "<div  class=\"content contentarea>\"" + webcontent + "</div>"; 
		//         //var htmlScript = "<script type=\"text/javascript\" src=\"http://192.168.5.123/maharastracmolatest/latest/misc/jquery.js?v=1.4.4\" > "
		//         //                 + "</script>"
		//         //                 + "<script type=\"text/javascript\" src=\"http://192.168.5.123/maharastracmolatest/latest/sites/all/themes/maharastracmonew/js/functions.js?obsm2a\">"
		//         //                 + "</script>"
		//         //                 + "<script type =\"text/javascript\" src=\"http://192.168.5.123/maharastracmolatest/latest/sites/all/themes/maharastracmonew/js/general.js?obsm2a\">" 
		//         //                 + "</script>";
		//         //var htmlScript = "<script type=\"text/javascript \">"
		//         //              + "function getHeight()  {"
		//         //              + "return document.getElementById(\"bgColorWhite\").offsetHeight.toString(); }"  
		//         //              +"</script>";

		//         //var css = "<link href='http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/css/responsive.css' rel='stylesheet' type='text/css'/>" +
		//         //           "<link href='http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/css/style.css' rel='stylesheet' type='text/css'/>"

		//         //            + "<script src=\"http://192.168.5.123/maharastracmolatest/latest/misc/jquery.js?v=1.4.4\"></script>"
		//         //           + "<script src=\"http://192.168.5.123/maharastracmolatest/latest/sites/all/themes/maharastracmonew/js/functions.js?obsm2a\"></script>"
		//         //           + "<script src=\"http://192.168.5.123/maharastracmolatest/latest/sites/all/themes/maharastracmonew/js/general.js?obsm2a\"></script>"
		//         //           + "<script src=\"http://14.141.36.212/maharastracmo/misc/ui/jquery.ui.draggable.min.js?v=1.8.7\"></script>";

		//         //var css = "<link href='http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/css/style.css' rel='stylesheet' type='text/css'/>";

		//         //var htmlConcat = string.Format("<html><head><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" /><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\" /><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><meta name=\"apple-mobile-web-app-capable\" content=\"yes\" /><meta name=\"apple-mobile-web-app-status-bar-style\" content=\"black\" user-scalable=\"no\" />{2}</head>" +
		//         //                                "<body style=\"margin:0;padding:0;text-align:justify;background-color:transparent;-ms-content-zooming: none !important; \" " +
		//         //                                ">" +
		//         //                                "<div id=\"wrapper\" class=\"content contentarea>\" ><section class=\"content contentarea>\" {0}</section></div></body>{1}</html>", webcontent, htmlScript, css);

		//         //var htmlConcat = string.Format("<html style=\"background: #ffffff\"><head> <meta name=\"viewport\" content=\"width=device-width, initial-scale=1 , maximum-scale=1,  user-scalable=no\" /><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />{1}</head>" +
		//         //                                          "<body>"
		//         //                                          +
		//         //                                          "<div class=\"bgColorWhite\"><section class=\"content contentarea\">{0}</section></div></body></html>", webcontent, css); 

		//         var head = string.Format("<!DOCTYPE html>" + "<head>"
		//             + "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />"
		//             + "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no\">"
		//             + "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" />"
		//             + "<link href='http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/css/style.css' rel='stylesheet' type='text/css'/>"
		//             + "<link href = 'http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/css/animate.css' rel ='stylesheet' type ='text/css'/>"
		//            // + "<link href = 'http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/css/responsive.css' rel ='stylesheet' type ='text/css'/>" 
		//    + "<link href = 'http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/css/mobile-app-res.css' rel ='stylesheet' type ='text/css'/>"
		//            // + "<script src=\"http://14.141.36.212/maharastracmo/misc/jquery.js\"></script>"

		//            // + "<link href=\"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/css/responsive.css?ogz7rh\" rel='stylesheet' type='text/css'/ >" /* new design */
		//           // + "<script src=\"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/js/jquery-1.9.1.min.js\" ></script>" /* new design for setting name + designation*/
		//            + "<script type =\"text/javascript\" src=\"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/js/jquery-1.9.1.min.js\"></script>"
		//      		//+"<script type=\"text/javascript\" src=\"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/js/modernizr.js?obp57t\"></script>"
		//            //+"<script type=\"text/javascript\" src=\"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/js/bruteforce.js?obp57t\"></script>"
		//             + "<script src=\"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/js/functions.js\" type=\"text/javascript\"></script>"
		//		  + "<script src=\"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/js/general.js\" type=\"text/javascript\"></script>"

		//             +"</head>");



		//         var body= string.Format("<body class=\"noJS html not - front not - logged -in no - sidebars page - node page - node - page - node - 15 node - type - article i18n - en \">"





		//                        //+"<div id = \"wrapper\" class=\"bg\">"
		//     //+"<section class=\"content contentarea\">"
		//        // + "<div class=\"wrapInner\">"
		//             //+ "<div class=\"contentInnerPage\">"
		//                 //+ "<div class=\"innerContentIn\">"
		//                     //+ "<div class=\"region region-content\">"
		//                         //+ "<div id = \"block -system-main\" class=\"block block-system\">"
		//                             + "<div class=\"content\">"
		//                                 + "<div id = \"node -15\" class=\"node node-article clearfix\" about=\"/maharastracmo/en/former-cms\" typeof=\"sioc:Item foaf:Document\">"
		//                                     + "<div class=\"field field-name-body field-type-text-with-summary field-label-hidden\">"
		//                                         + "<div class=\"field -items\">"
		//                                             + "<div class=\"field -item even\" property=\"content:encoded\">"
		//                                                + " {0}"
		//                                             + "</div>"
		//                                         + "</div>"
		//                                     + "</div>"
		//                                 + "</div>"
		//                             + "</div>"
		//                         //+ "</div>"
		//                     //+ "</div>"
		//                // + "</div>"
		//            // + "</div>"
		//        // + "</div>"
		//     //+ "</section>"
		// //+ "</div>"

		//       // +"<script type = \"text /javascript\" src=\"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/js/functions.js?obp57t\"></script>"
		//       //  +"<script type = \"text /javascript\" src=\"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/js/general.js?obp57t\"></script>"
		//           +"</body>", webcontent);

		//         var finalhtml = string.Format("<html>{0}{1}</html>", head, body);

		////finalhtml = 
		//         return finalhtml;
		//     }

		private string CreateHTMLForFormerCMs(string webcontent)
		{
			string css = "<!doctype html>" +
									   "<head>" +
									   "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no\">" +
									   "<link href='http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/css/style.css' rel=\"stylesheet\">" +
									   "<link href='http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/css/responsive.css' rel=\"stylesheet\">" +
									   "<script src=\"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/js/jquery-min.js\">" +
									   "</script>" +
									   "<script>" +
									   "$(document).ready(function(){ if( $(\".tableData\").length > 0){$('.tableData').each(function(){$(this).find('tr').each(function(){$(this).find('td:first').addClass('firstTd'); $(this).find('th:first').addClass('firstTh');$(this).find('th:last').addClass('lastTh');});$(this).find('tr:last').addClass('lastTr');$(this).find('tr:even').addClass('evenRow');$(this).find('tr:nth-child(2)').find('th:first').removeClass('firstTh');});};if( $(\".responsiveTable, .Responsivetable, .views-table\").length){$(\".responsiveTable, .Responsivetable, .views-table\").each(function(){$(this).wrap('<div class=\"tableOut\"></div>');$(this).find('td').removeAttr('width');var head_col_count =  $(this).find('tr th').size();for ( i=0; i <= head_col_count; i++ )  {var head_col_label = $(this).find('tr th:nth-child('+ i +')').text();$(this).find('tr td:nth-child('+ i +')').attr(\"data-label\", head_col_label);}});};if( $(\".tableScroll\").length){$(\".tableScroll\").each(function(){$(this).wrap('<div class=\"tableOut\"></div>');});};});</script><style>.bgColorWhite { padding: 10px; background-color: #fff; }.respImg img { width: 100%%!important; height: auto!important; }.respImg img.pdfIcon { width: auto!important; }.respImg img.extIcon { width: auto!important; }.respImg img.file-icon { width: auto!important; }.respImg img.rtecenter { width: auto!important; } " +
									   "</style>" +
									   "</head>" +
									   "<body class=\"respImg\">" +
									   webcontent +
									  "</body>" +
				"</html>";

			return css;
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
            var answer = await DisplayAlert(AppResources.LExit, AppResources.LExitApp,AppResources.LYes,AppResources.LNo);
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
