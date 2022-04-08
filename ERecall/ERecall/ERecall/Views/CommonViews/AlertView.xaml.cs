using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ERecall.Views.CommonViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlertView : ContentView
    {
        public AlertView()
        {
            InitializeComponent();
        }
        public Color AlertBackground
        {
            get { return grdAlert.BackgroundColor; }
            set { grdAlert.BackgroundColor = value; }
        }
        public string AlertTitle
        {
            get { return lblAlertTitle.Text; }
            set { lblAlertTitle.Text = value; }
        }
        public string AlertMessage
        {
            get { return lblAlertMessage.Text; }
            set { lblAlertMessage.Text = value; }
        }
        public ImageSource AlertImage
        {
            get { return imgAlert.Source; }
            set { imgAlert.Source = value; }
        }
        public double AlertHeight
        {
            get { return grdAlert.HeightRequest; }
            set { grdAlert.HeightRequest = value; }
        }
        public double AlertWidth
        {
            get { return grdAlert.WidthRequest; }
            set { grdAlert.WidthRequest = value; }
        }
    }
}