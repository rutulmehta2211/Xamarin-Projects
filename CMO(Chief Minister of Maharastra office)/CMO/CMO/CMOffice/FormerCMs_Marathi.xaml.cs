
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
    public partial class FormerCMs_Marathi : ContentPage
    {
        public FormerCMs_Marathi()
        {
            InitializeComponent();
            CallWebService();
        }
        private async void CallWebService()
        {
            if (!App.CheckInternetConnection())
            {
                await DisplayAlert("Error", App.NoInternetConnectionMessage, "OK");
            }
            else
            {
                try
                {
                    List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>();
                    values.Add(new KeyValuePair<string, string>("block_id", "27"));

                    var response = await GeneralClass.GetResponse<CMO.CMOffice.RootObject>("http://14.141.36.212/maharastracmo/api/getblockcontent", values);
                    if (response != null)
                    {
                        if (response._resultflag == 1)
                        {
                            string webcontent = response.block_content;
                            var HTMLString = CreateHTMLFromerCMS(webcontent);
                            var html = new HtmlWebViewSource
                            {
                                Html = webcontent
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
                                      await  DisplayAlert("Error", App.CatchMessage, "OK");
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

		public string CreateHTMLFromerCMS(string webcontent)
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


    public class RootObject
    {
        public int _resultflag { get; set; }
        public string block_subject { get; set; }
        public string block_content { get; set; }
    }

}
