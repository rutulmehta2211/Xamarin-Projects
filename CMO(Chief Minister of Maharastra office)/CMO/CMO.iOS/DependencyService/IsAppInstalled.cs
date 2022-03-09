using CMO.MenuController;
using Foundation;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(CMO.iOS.DependencyService.IsAppInstalled))]
namespace CMO.iOS.DependencyService
{
 

        class IsAppInstalled : IIsAppInstalled
        {
            public string AppVersion
            {
                get; set;
            }
            public void NavigatePage(string Package_Name)
            {
            //    UIApplication.SharedApplication.OpenUrl(NSUrl.FromString("UC://"));
                //UIApplication.SharedApplication.OpenUrl(NSUrl.FromString("itms://itunes.com/apps/comgooglemaps://"));
            }

            bool IIsAppInstalled.IsAppInstalled(string ItunesLink)
            {
			// return UIApplication.SharedApplication.CanOpenUrl(NSUrl.FromString("https://itunes.apple.com/in/app/universal-converter/id424203419"));

			return false;
			//return UIApplication.SharedApplication.CanOpenUrl(NSUrl.FromString(ItunesLink));

        }
    }
    }

