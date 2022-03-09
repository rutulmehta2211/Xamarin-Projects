//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;


//using Xamarin.Forms.Platform.iOS;
//using Xamarin.Forms;
//using CMO.iOS.Renderer;
//using UIKit;

//[assembly: ExportRenderer(typeof(NavigationPage), typeof(CustomNavigationRenderer))]
//namespace CMO.iOS.Renderer
//{

//    public class CustomNavigationRenderer : NavigationRenderer
//    {
//        public override void SetViewControllers(UIViewController[] controllers, bool animated)
//        {
//            base.SetViewControllers(controllers, animated);
//            foreach (var controller in controllers)
//            {
//                // Disable swipe back
//                ((UINavigationController)controller).InteractivePopGestureRecognizer.Enabled = false;
//            }
//        }
//        public override void ViewDidLoad()
//        {
//            base.ViewDidLoad();
//            if (InteractivePopGestureRecognizer != null)
//            {
//                InteractivePopGestureRecognizer.Enabled = false;
//            }
//        }
//    }

//}