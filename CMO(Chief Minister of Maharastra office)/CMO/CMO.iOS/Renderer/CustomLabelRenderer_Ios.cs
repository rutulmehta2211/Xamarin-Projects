using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using CMO.iOS.Renderer;
using UIKit;

[assembly: ExportRenderer(typeof(Label), typeof(CustomLabelRenderer_Ios))]
namespace CMO.iOS.Renderer
{
    public class CustomLabelRenderer_Ios : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
			//if (Element.ClassId == null)
			//{
			//	UIFont font;
			//	var size = Element.FontSize;
			//	if (size == Device.GetNamedSize(NamedSize.Large, typeof(Label)))
			//	{
			//		font = Control.Font.WithSize(12);
			//	}
			//	else if (size == Device.GetNamedSize(NamedSize.Small, typeof(Label)))
			//	{
			//		font = Control.Font.WithSize(8);
			//	}
			//	else if (size == Device.GetNamedSize(NamedSize.Micro, typeof(Label)))
			//	{
			//		font = Control.Font.WithSize(6);
			//	}
			//	else if (size == Device.GetNamedSize(NamedSize.Medium, typeof(Label)))
			//	{
			//		font = Control.Font.WithSize(10);
			//	}
			//	else {
			//		font = Control.Font.WithSize(10);
			//	}
			//	Control.Font = font;
			//}
		}
    }
}