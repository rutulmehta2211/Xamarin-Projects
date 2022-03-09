using CMO.MenuController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CMO.Initiatives
{
    public partial class InitiativesMain : ContentPage
    {
        public InitiativesMain()
        {
            InitializeComponent();
           
            this.Title = AppResources.LInitiativesTitle.ToUpper();
        }
        protected async override void OnAppearing()
        {
            Application.Current.Properties["CurrentPage"] = "initiative";
            if (!App.CheckInternetConnection())
            {
               // Loading(false);
                await DisplayAlert(AppResources.LNetworkError, AppResources.LNoInternetConnection, AppResources.LOk);
            }
            else
            {
                CallWebService();
            }
        }
        private async void CallWebService()
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

                    values.Add(new KeyValuePair<string, string>("lang", lang));
                    var response = await GeneralClass.GetResponse<ServicesClasses.RootObject>("http://14.141.36.212/maharastracmo/api/getinitiativeslist", values);
                    if (response != null)
                    {
                        if (response._resultflag == 1)
                        {
                            string webcontent = response.page_content;
                            //string finalwebcontent = webcontent.Replace("<?php echo $base_url; ?>", "http://14.141.36.212/maharastracmo");
                            Title = response.page_title.ToUpper();
                            var HTMLString = CreateHTMLForFormerCMs(webcontent);
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
                                        var jalyuktaurl = new Uri("http://14.141.36.212/maharastracmo/cm-visits-jalyukta");
                                        if (uri ==jalyuktaurl)
                                        {
                                            Navigation.PushAsync(new CMO.CMVisits.JalyuktaVisits());
                                        }
                                        else
                                        {
                                            Device.OpenUri(uri);
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        DisplayAlert(AppResources.LError, AppResources.LWebserverNotResponding, AppResources.LOk);
                                    }

                                    e.Cancel = true;
                                }
                            };
                        }
                        else
                        {
                           if (App.CurrentPage() == "initiative")
									await DisplayAlert(AppResources.LError, AppResources.LSomethingWentWrong, AppResources.LOk);
						}
                    }
                    else
                    {
                      if (App.CurrentPage() == "initiative")
							await DisplayAlert(AppResources.LError, AppResources.LSomethingWentWrong, AppResources.LOk);
                    }

                }
                catch (WebException ex)
                {
                    if (App.CurrentPage() == "initiative")
					{
						if (ex.Message.Contains("Network"))
							await DisplayAlert(AppResources.LError, AppResources.LWebserverNotResponding, AppResources.LOk);
					}
                }
            }
        }
        
        private string CreateHTMLForFormerCMs(string webcontent)
		{
				var initiavesclosefirstitem = "<script>" +
			"$(document).ready(function(){$(\".accordionRow:first .accTrigger\").trigger(\"click\");});</script>";

			var head = string.Format
				(
				"<head>"
				+ "<meta http - equiv = \"Content - Type\" content = \"text / html; charset = utf - 8\" />"
				+ "<meta name = \"viewport\" content = \"width =device - width, initial - scale=1, maximum - scale=1, user - scalable=no\" >"
				+ "<meta http - equiv = \"X - UA - Compatible\" content = \"IE =edge\" />"
				//   + "<meta name = \"viewport\" content = \"width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no\" >"
				+ "<meta http - equiv = \"X -UA-Compatible\" content = \"IE=edge\" />"

				+ "<link href = 'http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/css/style.css' rel = 'stylesheet' type = 'text/css' />"
				+ "<link href = 'http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/css/animate.css' rel = 'stylesheet' type = 'text/css' />"
				+ "<link href = 'http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/css/responsive.css' rel = 'stylesheet' type = 'text/css' />"
				+ "<link href = 'http://14.141.36.212/maharastracmo/sites/all/modules/lang_dropdown/msdropdown/dd.css' rel = 'stylesheet' type = 'text/css' />"
				+ "<link href = 'http://14.141.36.212/maharastracmo/sites/all/modules/event_calendar/event_popup/css/event_popup.css' rel = 'stylesheet' type = 'text/css' />"
				+ "<link href = 'http://14.141.36.212/maharastracmo/sites/default/files/event_calendar_colors/event_calendar_colors.css' rel = 'stylesheet' type = 'text/css' />"
				+ "<link href = 'http://14.141.36.212/maharastracmo/sites/all/modules/ctools/css/ctools.css' rel = 'stylesheet' type = 'text/css' />"
				+ "<link href = 'http://14.141.36.212/maharastracmo/sites/all/modules/colorbox/styles/default/colorbox_style.css' rel = 'stylesheet' type = 'text/css' />"
				+ "<link href = 'http://14.141.36.212/maharastracmo/misc/ui/jquery.ui.core.css' rel = 'stylesheet' type = 'text/css' />"
				+ "<link href = 'http://14.141.36.212/maharastracmo/misc/ui/jquery.ui.theme.css' rel = 'stylesheet' type = 'text/css' />"
				+ "<link href = 'http://14.141.36.212/maharastracmo/misc/ui/jquery.ui.button.css' rel = 'stylesheet' type = 'text/css' />"
				+ "<link href = 'http://14.141.36.212/maharastracmo/misc/ui/jquery.ui.resizable.css' rel = 'stylesheet' type = 'text/css' />"
				+ "<link href = 'http://14.141.36.212/maharastracmo/misc/ui/jquery.ui.dialog.css' rel = 'stylesheet' type = 'text/css' />"
				+ "<link href = 'http://14.141.36.212/maharastracmo/modules/system/system.theme.css' rel = 'stylesheet' type = 'text/css' />"
				+ "<link href = 'http://14.141.36.212/maharastracmo/modules/system/system.messages.css' rel = 'stylesheet' type = 'text/css' />"
				+ "<link href = 'http://14.141.36.212/maharastracmo/modules/system/system.menus.css' rel = 'stylesheet' type = 'text/css' />"
				+ "<link href = 'http://14.141.36.212/maharastracmo/modules/system/system.base.css' rel = 'stylesheet' type = 'text/css' />"

				+ "<link href = 'http://14.141.36.212/maharastracmo/sites/all/modules/calendar/css/calendar_multiday.css' rel = 'stylesheet' type = 'text/css' />"
				+ "<link href = 'http://14.141.36.212/maharastracmo/modules/comment/comment.css' rel = 'stylesheet' type = 'text/css' />"
				+ "<link href = 'http://14.141.36.212/maharastracmo/sites/all/modules/date/date_api/date.css' rel = 'stylesheet' type = 'text/css' />"
				+ "<link href = 'http://14.141.36.212/maharastracmo/sites/all/modules/date/date_popup/themes/datepicker.1.7.css' rel = 'stylesheet' type = 'text/css' />"
				+ "<link href = 'http://14.141.36.212/maharastracmo/sites/all/modules/date/date_repeat_field/date_repeat_field.css' rel = 'stylesheet' type = 'text/css' />"
				+ "<link href = 'http://14.141.36.212/maharastracmo/modules/field/theme/field.css' rel = 'stylesheet' type = 'text/css' />"
				+ "<link href = 'http://14.141.36.212/maharastracmo/modules/node/node.css' rel = 'stylesheet' type = 'text/css' />"
				+ "<link href = 'http://14.141.36.212/maharastracmo/modules/search/search.css' rel = 'stylesheet' type = 'text/css' />"
				+ "<link href = 'http://14.141.36.212/maharastracmo/modules/user/user.css' rel = 'stylesheet' type = 'text/css' />"
				+ "<link href = 'http://14.141.36.212/maharastracmo/sites/all/modules/youtube/css/youtube.css' rel = 'stylesheet' type = 'text/css' />"
				+ "<link href = 'http://14.141.36.212/maharastracmo/modules/forum/forum.css?oc208t' rel = 'stylesheet' type = 'text/css' />"
				+ "<link href = 'http://14.141.36.212/maharastracmo/sites/all/modules/views/css/views.css' rel = 'stylesheet' type = 'text/css' />"

				+ "<script src = \"http://14.141.36.212/maharastracmo/misc/jquery.js\"></script >"
				+ "<script src = \"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/js/functions.js\"></script >"
				+ "<script src = \"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/js/general.js\"></script >"
				+ "<script src=\"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/js/removeCookiesDiv.js\"></script>"

				+ "<script type = \"text/javascript\" src = \"http://14.141.36.212/maharastracmo/misc/jquery.js?v=1.4.4\"></script>"
				+ "<script type = \"text/javascript\" src = \"http://14.141.36.212/maharastracmo/misc/jquery.once.js?v=1.2\"></script>"
				+ "<script type = \"text/javascript\" src = \"http://14.141.36.212/maharastracmo/misc/drupal.js?oc208t\"></script>"
				+ "<script type = \"text/javascript\" src = \"http://14.141.36.212/maharastracmo/misc/ui/jquery.ui.core.min.js?v=1.8.7\"></script>"
				+ "<script type = \"text/javascript\" src = \"http://14.141.36.212/maharastracmo/misc/ui/jquery.ui.widget.min.js?v=1.8.7\"></script>"
				+ "<script type = \"text/javascript\" src = \"http://14.141.36.212/maharastracmo/misc/ui/jquery.ui.button.min.js?v=1.8.7\"></script>"
				+ "<script type = \"text/javascript\" src = \"http://14.141.36.212/maharastracmo/misc/ui/jquery.ui.mouse.min.js?v=1.8.7\"></script>"
				+ "<script type = \"text/javascript\" src = \"http://14.141.36.212/maharastracmo/misc/ui/jquery.ui.draggable.min.js?v=1.8.7\"></script>"
				+ "<script type = \"text/javascript\" src = \"http://14.141.36.212/maharastracmo/misc/ui/jquery.ui.position.min.js?v=1.8.7\"></script>"
				+ "<script type = \"text/javascript\" src = \"http://14.141.36.212/maharastracmo/misc/ui/jquery.ui.resizable.min.js?v=1.8.7\"></script>"
				+ "<script type = \"text/javascript\" src = \"http://14.141.36.212/maharastracmo/misc/ui/jquery.ui.dialog.min.js?v=1.8.7\"></script>"
				+ "<script type = \"text/javascript\" src = \"http://14.141.36.212/maharastracmo/sites/all/modules/views/js/jquery.ui.dialog.patch.js?v=1.8.7\"></script>"
				+ "<script type = \"text/javascript\" src = \"http://14.141.36.212/maharastracmo/sites/all/libraries/jquery/jquery-1.11.min.js?oc208t\"></script>"
				+ "<script type = \"text/javascript\" src = \"http://14.141.36.212/maharastracmo/sites/all/modules/jqmulti/js/switch.js?oc208t\"></script>"
				+ "<script type = \"text/javascript\" src = \"http://14.141.36.212/maharastracmo/sites/all/libraries/colorbox/jquery.colorbox-min.js?oc208t\"></script>"
				+ "<script type = \"text/javascript\" src = \"http://14.141.36.212/maharastracmo/sites/all/modules/colorbox/js/colorbox.js?oc208t\"></script>"
				+ "<script type = \"text/javascript\" src = \"http://14.141.36.212/maharastracmo/sites/all/modules/colorbox/styles/default/colorbox_style.js?oc208t\"></script>"
				+ "<script type = \"text/javascript\" src = \"http://14.141.36.212/maharastracmo/sites/all/modules/colorbox/js/colorbox_load.js?oc208t\"></script>"
				+ "<script type = \"text/javascript\" src = \"http://14.141.36.212/maharastracmo/sites/all/modules/event_calendar/event_popup/js/event_popup.js?oc208t\"></script>"
				+ "<script type = \"text/javascript\" src = \"http://14.141.36.212/maharastracmo/sites/all/modules/event_calendar/event_popup/js/event_popup_validate.js?oc208t\"></script>"

				+ "</head>"
				);

			var body = string.Format
				(
				"<body class=\"noJS html not-front not-logged-in no-sidebars page-node page-node- page-node-14 node-type-article i18n-en \">"
						+ "<div class=\"wrapInner\">"
							+ "<div class=\"contentInnerPage\">"
								+ "<div class=\"innerContentIn\">"
									+ "<div class=\"region region-content\">"
										+ "<div id = \"block -system-main\" class=\"block block-system\">"
											+ "<div class=\"content\">"
												+ "{0}"
											+ "</div>"
										+ "</div>"
									+ "</div>"
								+ "</div>"
							+ "</div>"
						+ "</div>"
				+ "<script type = \"text/javascript\" src=\"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/js/jquery-min.js\"></script>"
				+ "<script type = \"text/javascript\" src=\"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/js/modernizr.js\"></script>"
		        + "<script type = \"text/javascript\" src=\"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/js/functions.js\"></script>"
				+ "<script type = \"text/javascript\" src=\"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/js/general.js\"></script>"
				                 + "{1}"
				+ "<script type = \"text/javascript\" src=\"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/js/removeCookiesDiv.js\"></script>"
				+ "<script type = \"text/javascript\" src=\"http://14.141.36.212/maharastracmo/sites/all/themes/maharastracmonew/js/bruteforce.js\"></script>"
				
				+ "</body>"
				, webcontent, initiavesclosefirstitem);
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
            var answer = await DisplayAlert(AppResources.LExit, AppResources.LExitApp,AppResources.LYes, AppResources.LNo);
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
