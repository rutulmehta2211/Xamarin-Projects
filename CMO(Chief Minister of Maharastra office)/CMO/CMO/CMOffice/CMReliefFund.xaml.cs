using CMO.MenuController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CMO.CMOffice
{
    public partial class CMReliefFund : ContentPage
    {
        public CMReliefFund()
        {
            InitializeComponent();
            this.Title = AppResources.LCmReliefsFund.ToUpper();
        }
        protected override void OnAppearing()
        {
            Application.Current.Properties["CurrentPage"] = "cmrelieffund";
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
            var answer = await DisplayAlert(AppResources.LExit, AppResources.LExitApp, AppResources.LYes, AppResources.LNo);
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
