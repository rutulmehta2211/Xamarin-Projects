using ERecall.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ERecall.CommonClasses
{
    public class CheckInternetConnection
    {
        public static bool IsInternetConnected()
        {
            var networkConnection = DependencyService.Get<IInternetConnectivity>();
            return networkConnection.IsInternetConnected();
        }
    }
}
