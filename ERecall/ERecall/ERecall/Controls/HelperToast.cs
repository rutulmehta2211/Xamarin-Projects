using ERecall.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ERecall.Controls
{
    public class HelperToast
    {
        public static void ShortMessage(string message)
        {
            DependencyService.Get<IToastMessages>().ShortAlert(message);
        }
        public static void LongMessage(string message)
        {
            DependencyService.Get<IToastMessages>().LongAlert(message);
        }
    }
}
