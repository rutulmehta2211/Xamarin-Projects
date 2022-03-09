using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CMO.CMVisits
{
    public partial class MaharashtraVisit : ContentPage
    {
        int widthdevice;
        public MaharashtraVisit()
        {
            InitializeComponent();
            widthdevice = App.DeviceWidth;
            this.Title = AppResources.LMaharashtraVisits.ToUpper();
        }
        private void LeftPhotoTapped(object sender, EventArgs e)
        {
            var stack = Navigation.NavigationStack;
            if (stack[stack.Count - 1].GetType() != typeof(MakeInMaharashtraGoesInternationalDetail))
            {
                var PhotoID = sender as Grid;
                Navigation.PushAsync(new CMO.MakeInMaharashtraGoesInternationalDetail(widthdevice, PhotoID.ClassId));
            }
        }

        private void RightPhotoTapped(object sender, EventArgs e)
        {
            var stack = Navigation.NavigationStack;
            if (stack[stack.Count - 1].GetType() != typeof(MakeInMaharashtraGoesInternationalDetail))
            {
                var PhotoID = sender as Grid;
                Navigation.PushAsync(new CMO.MakeInMaharashtraGoesInternationalDetail(widthdevice, PhotoID.ClassId));
            }
        }
    }
}
