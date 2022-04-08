using ERecall.Controls;
using ERecall.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ERecall.Views.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SideMenuDetail : TabbedPage
    {
        bool IsPressFirstTime = true;

        public SideMenuDetail()
        {
            InitializeComponent();
            IsPressFirstTime = true;
        }
        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Title = CurrentPage?.Title;
        }
        protected override bool OnBackButtonPressed()
        {

            if (IsPressFirstTime)
            {
                IsPressFirstTime = false;
                HelperToast.LongMessage("Press again to exit...");
                Device.StartTimer(TimeSpan.FromSeconds(5), () =>
                {
                    IsPressFirstTime = true;
                    return false; // True = Repeat again, False = Stop the timer
                });
            }
            else
            {
                DependencyService.Get<ICloseApp>().CloseApp();
            }
            return true;
        }
    }
}