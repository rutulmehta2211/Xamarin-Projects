using CMO.MenuController;
using CMO.ServicesClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CMO.ChiefMinister
{
    public partial class VisionMissionOath : ContentPage
    {
        public VisionMissionOath()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
            this.Title = AppResources.LVisionMissionOath.ToUpper();
        }
        protected async override void OnAppearing()
        {
            Application.Current.Properties["CurrentPage"] = "vision";
            this.Title = AppResources.LVisionMissionOath.ToUpper();

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
                    {lang = Application.Current.Properties["Language"] as string;}

                    if (lang == "en")
                        values.Add(new KeyValuePair<string, string>("page_id", "3"));
                    else
                        values.Add(new KeyValuePair<string, string>("page_id", "305"));

                    var response = await GeneralClass.GetResponse<RootObject>("http://14.141.36.212/maharastracmo/api/getpagecontent", values);
                    if (response != null)
                    {
                        if (response._resultflag == 1)
                        {
                            var webcontent = response.page_content;
                            var HTMLString = CreateHTMLForTeamCMO(webcontent);
                            //  Title = response.page_title;
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
                            };
                        }
                        else
                        {
							if (App.CurrentPage() == "vision")
                            await DisplayAlert(AppResources.LError, AppResources.LSomethingWentWrong, AppResources.LOk);
                        }
                    }
                    else
                    {
						if (App.CurrentPage() == "vision")
                        await DisplayAlert(AppResources.LError, AppResources.LSomethingWentWrong, AppResources.LOk);
                    }
                }

                catch (WebException ex)
                {
					if (App.CurrentPage() == "vision")
					{
						if (ex.Message.Contains("Network"))
						await DisplayAlert(AppResources.LError, AppResources.LWebserverNotResponding, AppResources.LOk);
					}
                }
            }
        }

      //  private string CreateHTMLForTeamCMO(string webcontent)
      //  {
      //      var head = string.Format("<head>"
      //         + "<meta http-equiv=\"Content - Type\" content=\"text / html; charset = utf - 8\" />"
      //    //     + "<meta name=\"viewport\" content=\"width = device - width, initial - scale = 1, maximum - scale = 1, user - scalable = no\">"
      //         + "<meta http-equiv=\"X - UA - Compatible\" content=\"IE = edge\" />"
      //         + "<link href='http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/css/style.css' rel='stylesheet' type='text/css'/>"
      //         + "<link href = 'http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/css/animate.css' rel ='stylesheet' type ='text/css'/>"
      //         + "<link href = 'http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/css/responsive.css' rel ='stylesheet' type ='text/css'/>"
      //         + "<script src=\"http://14.141.36.212/maharastracmo/misc/jquery.js\"></script>"
      //         + "<script src=\"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/js/functions.js\" ></script>"
      //         + "<script src=\"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/js/general.js\" ></script>"
			   //+ "<script src=\"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/css/responsive.css?ogz7rh\" ></script>" /* new design */
             
      //         + "</head>");

      //      var body = string.Format("<body class=\"noJS html not - front not - logged -in no - sidebars page - node page - node - page - node - 15 node - type - article i18n - en \">"
      //          + "{0}"
      //          + "</body>", webcontent);

      //      var finalhtml = string.Format("<html>{0}{1}</html>", head, body);

      //      return finalhtml;
      //  }


		private string CreateHTMLForTeamCMO(string webcontent)
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
    }
}