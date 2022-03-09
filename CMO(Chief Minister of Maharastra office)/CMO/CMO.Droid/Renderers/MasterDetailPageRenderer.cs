//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using Xamarin.Forms.Platform.Android;
//using CMO.MenuController;
//using Xamarin.Forms;
//using CMO.Droid.Renderers;

//[assembly: ExportRenderer(typeof(Xamarin.Forms.MasterDetailPage), typeof(CustomMasterDetailRenderer))]
//namespace CMO.Droid.Renderers
//{
//    public class CustomMasterDetailRenderer : MasterDetailRenderer
//    {
//        bool firstDone = true;
//        public override void AddView(Android.Views.View child)
//        {
//            if (firstDone)
//            {
//                MyMasterDetailPage page = (MyMasterDetailPage)this.Element;
//                LayoutParams p = (LayoutParams)child.LayoutParameters;
//                p.Width = page.DrawerWidth;
//                base.AddView(child, p);
//            }
//            else
//            {
//                firstDone = true;
//                base.AddView(child);
//            }
//        }
//    }
//}