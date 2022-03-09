using CMO.Droid;
using Mobile.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(ListViewRenderer), typeof(CustomListViewRenderer_Droid))]
namespace Mobile.Droid.Renderers
{
    public class CustomListViewRenderer_Droid : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);
         Control.SetSelector(Resource.Layout.no_selector);
        }
    }
}
