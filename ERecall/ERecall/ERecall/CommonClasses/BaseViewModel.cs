using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ERecall.CommonClasses
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected Page CurrentPage { get; private set; }
        public void Initialize(Page page)
        {
            CurrentPage = page;

            CurrentPage.Appearing += CurrentPageOnAppearing;
            CurrentPage.Disappearing += CurrentPageOnDisappearing;
        }

        protected virtual void CurrentPageOnAppearing(object sender, EventArgs eventArgs) { }

        protected virtual void CurrentPageOnDisappearing(object sender, EventArgs eventArgs) { }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
