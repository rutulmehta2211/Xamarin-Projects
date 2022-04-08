using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ERecall.Views.CommonViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivityIndicatorView : ContentView
    {
        public ActivityIndicatorView()
        {
            InitializeComponent();
            Device.BeginInvokeOnMainThread(async () =>
            {
                await RotateElement(imgLoading, CancellationToken.None);
            });
        }
        private async Task RotateElement(VisualElement element, CancellationToken cancellation)
        {
            while (!cancellation.IsCancellationRequested)
            {
                await element.RotateTo(360, 800, Easing.Linear);
                await element.RotateTo(0, 0); // reset to initial position
            }
        }
        public ImageSource LoadingImage
        {
            get { return imgLoading.Source; }
            set { imgLoading.Source = value; }
        }
    }
}