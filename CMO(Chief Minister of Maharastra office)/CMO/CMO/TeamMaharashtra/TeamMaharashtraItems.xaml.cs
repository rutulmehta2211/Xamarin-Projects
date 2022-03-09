using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CMO.MenuController;

using Xamarin.Forms;
using CMO.ServicesClasses;

namespace CMO.TeamMaharashtra
{
    public partial class Comman_WebView_TeamMaharashtra : ContentPage
    {
        public Comman_WebView_TeamMaharashtra()
        {
            InitializeComponent();
            
         //   this.Title = AppResources.LTeamMaharashtra;
        }
        protected async override void OnAppearing()
        {
            if (!App.CheckInternetConnection())
            {
               // Loading(false);
                await DisplayAlert(AppResources.LNetworkError, AppResources.LNoInternetConnection, AppResources.LOk);
            }
            else
            {
               await CallWebService();
            }
            //   Application.Current.Properties["CurrentPage"] = "teammaharashtra";
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

                    string pid = CMO.MenuController.SideMenu.TeamMaharashtraPageID;
                    if (lang == "en")
                    {
                        if (pid == "40")
                        {
                            values.Add(new KeyValuePair<string, string>("page_id", pid));
                            this.Title = AppResources.LGovernor.ToUpper();
                        }
                        if (pid == "41")
                        {
                            values.Add(new KeyValuePair<string, string>("page_id", pid));
                            this.Title = AppResources.LCabinetMinister.ToUpper();
                        }
                        if (pid == "42")
                        {
                            values.Add(new KeyValuePair<string, string>("page_id", pid));
                            this.Title = AppResources.LStateMinisters.ToUpper();
                        }
                        if (pid == "43")
                        {
                            values.Add(new KeyValuePair<string, string>("page_id", pid));
                            this.Title = AppResources.LMenuChiefMinister.ToUpper();
                        }
                        if (pid == "44")
                        {
                            values.Add(new KeyValuePair<string, string>("page_id", pid));
                            this.Title = AppResources.LSecretaries.ToUpper();
                        }
                        if (pid == "45")
                        {
                            values.Add(new KeyValuePair<string, string>("page_id", pid));
                            this.Title = AppResources.LCollectors.ToUpper();
                        }
                        if (pid == "47")
                        {
                            values.Add(new KeyValuePair<string, string>("page_id", pid));
                            this.Title = AppResources.LGovtDepartment.ToUpper();
                        }
                    }
                    else
                    {
                        if (pid == "40")
                        {
                            values.Add(new KeyValuePair<string, string>("page_id", "310"));
                            this.Title = AppResources.LGovernor.ToUpper();
                        }
                        if (pid == "41")
                        {
                            values.Add(new KeyValuePair<string, string>("page_id", "309"));
                            this.Title = AppResources.LCabinetMinister.ToUpper();
                        }
                        if (pid == "42")
                        {
                            values.Add(new KeyValuePair<string, string>("page_id", "298"));
                            this.Title = AppResources.LStateMinisters.ToUpper();
                        }
                        if (pid == "43")
                        {
                            values.Add(new KeyValuePair<string, string>("page_id", "311"));
                            this.Title = AppResources.LMenuChiefMinister.ToUpper();
                        }
                        if (pid == "44")
                        {
                            values.Add(new KeyValuePair<string, string>("page_id", "312"));
                            this.Title = AppResources.LSecretaries.ToUpper();
                        }
                        if (pid == "45")
                        {
                            values.Add(new KeyValuePair<string, string>("page_id", "313"));
                            this.Title = AppResources.LCollectors.ToUpper();
                        }
                        if (pid == "47")
                        {
                            values.Add(new KeyValuePair<string, string>("page_id", "314"));
                            this.Title = AppResources.LGovtDepartment.ToUpper();
                        }
                    }
					// values.Add(new KeyValuePair<string, string>("id", "124"));
                    var response = await GeneralClass.GetResponse<RootObject>("http://14.141.36.212/maharastracmo/api/getpagecontent", values);
                    if (response != null)
                    {
                        if (response._resultflag == 1)
                        {
       //                     Application.Current.Properties["CurrentPage"] = pid;
							//string webcontent = response.pagecontent.body;
							// string finalwebcontent = webcontent.Replace("<?php echo $base_url; ?>", "https://www.indiannavy.nic.in/api/");
							////   Title = response.page_title.ToUpper();
							//Title = "ABC";

							Application.Current.Properties["CurrentPage"] = pid;
							string webcontent = response.page_content;
							string finalwebcontent = webcontent.Replace("<?php echo $base_url; ?>", response.base_url);
							Title = response.page_title.ToUpper();
						//	var HTMLString = CreateHTMLForFormerCMs(finalwebcontent);
							var HTMLString = indiannavy(finalwebcontent);
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
                                       await DisplayAlert(AppResources.LError, AppResources.LWebserverNotResponding, AppResources.LOk);
                                    }

                                    e.Cancel = true;
                                }
                                if (e.Url.EndsWith("gov.in"))
                                {
                                    try
                                    {
                                        string EmailId = "mailto:" + e.Url;
                                        var uri = new Uri(e.Url);
										//   Device.OpenUri(new Uri("mailto:ryan.hatfield@test.com"));
                                    Device.OpenUri(uri);
                                    }
                                    catch (Exception ex)
                                    {
                                        await DisplayAlert(AppResources.LError, AppResources.LWebserverNotResponding, AppResources.LOk);
									}

                                    e.Cancel = true;
                                }
                            };
                        }
                        else
                        {
							if (App.CurrentPage() != pid)
							await DisplayAlert(AppResources.LError, AppResources.LSomethingWentWrong, AppResources.LOk);
                        }
                        CMO.MenuController.SideMenu.TeamMaharashtraPageID = null;
                    }
                    else
                    {
						if (App.CurrentPage() != pid)
                        await DisplayAlert(AppResources.LError, AppResources.LSomethingWentWrong,AppResources.LOk);
                    }
                }

                catch (WebException ex)
                {
					if (App.CurrentPage() != CMO.MenuController.SideMenu.TeamMaharashtraPageID)
					{
						if (ex.Message.Contains("Network"))
						await DisplayAlert(AppResources.LError, AppResources.LWebserverNotResponding, AppResources.LOk);
					}
				}
            }
        }
        private string CreateHTMLForFormerCMs(string webcontent)
        {
            var head = string.Format("<!DOCTYPE html>" + "<head>"
				+ "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />"
				+ "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no\">"
				+ "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" />"
				+ "<link href='http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/css/style.css' rel='stylesheet' type='text/css'/>"
				+ "<link href = 'http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/css/animate.css' rel ='stylesheet' type ='text/css'/>"
				+ "<link href = 'http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/css/responsive.css' rel ='stylesheet' type ='text/css'/>"
				//+ "<link href = 'http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/css/mobile-app-res.css' rel ='stylesheet' type ='text/css'/>"
			   // + "<script src=\"http://14.141.36.212/maharastracmo/misc/jquery.js\"></script>"

			   // + "<link href=\"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/css/responsive.css?ogz7rh\" rel='stylesheet' type='text/css'/ >" /* new design */
			   // + "<script    src=\"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/js/jquery-1.9.1.min.js\" ></script>" /* new design for setting name + designation*/
			   + "<script type =\"text/javascript\" src=\"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/js/jquery-1.9.1.min.js\"></script>"
				//+"<script type=\"text/javascript\" src=\"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/js/modernizr.js?obp57t\"></script>"
				//+"<script type=\"text/javascript\" src=\"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/js/bruteforce.js?obp57t\"></script>"
				+ "<script src=\"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/js/functions.js\" type=\"text/javascript\"></script>"
				+ "<script src=\"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/js/general.js\" type=\"text/javascript\"></script>"

				+ "</head>");

            var body = string.Format("<body class=\"noJS html not - front not - logged -in no - sidebars page - node page - node - page - node - 15 node - type - article i18n - en \">"
                           + "{0}"

         //   + "<script type = \"text /javascript\" src=\"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/js/jquery-min.js?obp57t\"></script>"
          //  + "<script type = \"text /javascript\" src=\"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/js/modernizr.js?obp57t\"></script>"
            //+ "<script type = \"text /javascript\" src=\"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/js/functions.js?obp57t\"></script>"
            //+ "<script type = \"text /javascript\" src=\"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/js/general.js?obp57t\"></script>"
         //   + "<script type = \"text /javascript\" src=\"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/js/bruteforce.js?obp57t\"></script>"
            + "</body>", webcontent);

            var finalhtml = string.Format("<html>{0}{1}</html>", head, body);





            return finalhtml;
        }

        #region commentdf

        //public static string CreateHTMLForFormerCMs(string htmlContent)
        //{
        //	var htmlScript = "<script type=\"text/javascript \">"
        //					 + "function getHeight()  {"
        //					 + "return document.getElementById(\"bgColorWhite\").offsetHeight.toString(); };  </script>";

        //	//Stage url
        //	//http://14.141.36.212/indiannavy/code/sites/default/themes/indiannavy/css/style.css
        //	var css = "<link href='http://indiannavy.nic.in/sites/default/themes/indiannavy/css/style.css' rel='stylesheet' type='text/css'/>" +
        //	"<link href='http://indiannavy.nic.in/sites/default/themes/indiannavy/css/responsive.css' rel='stylesheet' type='text/css'/>"
        //	+ "<script src=\"http://indiannavy.nic.in/sites/default/themes/indiannavy/js/jquery-min.js\"></script>"
        //	+ " <script>$(document).ready(function(){if($(\".tableData\").length > 0){$(tableData').each(function(){"
        //	+ " $(this).find('td:first').addClass('firstTd');$(this).find('th:first').addClass('firstTh');$(this).find('th:last').addClass('lastTh');}); "
        //	+ " $(this).find('tr:last').addClass('lastTr');"
        //	+ " $(this).find('tr:even').addClass('evenRow');"
        //	+ " $(this).find('tr:nth-child(2)').find('th:first').removeClass('firstTh'); }); };"

        //	+

        //	"if( $(\".responsiveTable, .Responsivetable, .views-table\").length){$(\".responsiveTable, .Responsivetable, .views-table\").each(function(){ "
        //	   + " $(this).wrap('<div class=\"tableOut\"></div>');"
        //	   + "$(this).find('td').removeAttr('width');"
        //	   //$(this).find('td').removeAttr('align');
        //	   + " var head_col_count =  $(this).find('tr th').size();"

        //	   // loop which replaces td
        //	   + "for ( i=0; i <= head_col_count; i++ )  {"
        //	   + "var head_col_label = $(this).find('tr th:nth-child('+ i +')').text();"
        //	   + "$(this).find('tr td:nth-child('+ i +')').attr(\"data-label\", head_col_label);}});};"


        //	+
        //		  "if( $(\".tableScroll\").length){"
        //		  + "$(\".tableScroll\").each(function(){"
        //		  + "$(this).wrap('<div class=\"tableOut\"></div>');});};"

        //	+ " }); </script>"

        //	+ " <style>.bgColorWhite { padding: 10px; background-color: #fff; }</style>";


        //	//"<script src='http://14.141.36.212/indiannavy/code/sites/default/themes/indiannavy/js/jquery-min.js'/>";


        //	//var css = "<link href='http://14.141.36.221/IFR/writereaddata/Portal/Design_CSS/2.css' rel='stylesheet' type='text/css'/>"; 

        //	//<meta name=\"viewport\" content=\"initial-scale=1, maximum-scale=1\">
        //	//-ms-touch-action: none !important;
        //	//overflow: hidden;
        //	var htmlConcat = string.Format("<html style=\"background: #ffffff\"><head> <meta name=\"viewport\" content=\"width=device-width, initial-scale=1 , maximum-scale=1,  user-scalable=no\" /><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />{1}</head>" +
        //											  "<body>"
        //											  +
        //											  "<div class=\"bgColorWhite\">{0}</div></body></html>", htmlContent, css);

        //	return htmlConcat;
        //}

        #region Exit Application
        private bool _canClose = true;

        protected override bool OnBackButtonPressed()
        {
            //    if (_canClose)
            //    {
            //        ShowExitDialog();
            //    }
            //    return _canClose;
            if (Navigation.NavigationStack.Count == 1)
            {
                Application.Current.MainPage = new SideMenu();
                return true;
            }
            else
            {
                return false;
            }
        }

        private async void ShowExitDialog()
        {
            var answer = await DisplayAlert(AppResources.LExit, AppResources.LExitApp, AppResources.LYes, AppResources.LNo);
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
        #endregion


        public string indiannavy(string webcontent)
		{


			string css = "<!doctype html>" +
									   "<head>" +
									   "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no\">" +
									   "<link href='http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/css/style.css' rel=\"stylesheet\">" +
									   "<link href='http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/css/responsive.css' rel=\"stylesheet\">" +
									   "<script src=\"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/js/jquery-min.js\">" +
									   "</script>" +
									   "<script>" +
									   "$(document).ready(function(){ if( $(\".tableData\").length > 0){$('.tableData').each(function(){$(this).find('tr').each(function(){$(this).find('td:first').addClass('firstTd'); $(this).find('th:first').addClass('firstTh');$(this).find('th:last').addClass('lastTh');});$(this).find('tr:last').addClass('lastTr');$(this).find('tr:even').addClass('evenRow');$(this).find('tr:nth-child(2)').find('th:first').removeClass('firstTh');});};if( $(\".responsiveTable, .Responsivetable, .views-table\").length){$(\".responsiveTable, .Responsivetable, .views-table\").each(function(){$(this).wrap('<div class=\"tableOut\"></div>');$(this).find('td').removeAttr('width');var head_col_count =  $(this).find('tr th').size();for ( i=0; i <= head_col_count; i++ )  {var head_col_label = $(this).find('tr th:nth-child('+ i +')').text();$(this).find('tr td:nth-child('+ i +')').attr(\"data-label\", head_col_label);}});};if( $(\".tableScroll\").length){$(\".tableScroll\").each(function(){$(this).wrap('<div class=\"tableOut\"></div>');});};});</script>" +
									   "</head>" +
									   "<body class=\"respImg\">" +
									   webcontent +
									  "</body>" +
				"</html>";
			
			return css;
		}
    }




//public class Pagecontent
//	{
//		public string id { get; set; }
//		public string title { get; set; }
//		public string body { get; set; }
//	}

//	public class RootObject
//	{
//		public int _resultflag { get; set; }
//		public string message { get; set; }
//		public Pagecontent pagecontent { get; set; }
//	}

}
