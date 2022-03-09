//using CMO.Droid.DependencyService;
using CMO.iOS.DependencyService;
using System;
using System.Collections.Generic;
using System.Text;
[assembly: Xamarin.Forms.Dependency(typeof(PhoneDialer))]
namespace CMO.iOS.DependencyService
{
    public class PhoneDialer : CMOffice.IDialer
    {

        public bool Dial(string number)
        {
            return UIKit.UIApplication.SharedApplication.OpenUrl(
                new Foundation.NSUrl("tel:" + number));
        }
    }
}
