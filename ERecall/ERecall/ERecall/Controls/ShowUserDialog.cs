using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ERecall.Controls
{
    public class ShowUserDialog
    {
        public ShowUserDialog(IUserDialogs dialogs, String Message, System.Drawing.Color BackgroundColor, System.Drawing.Color TextColor)
        {
            dialogs.Toast(new ToastConfig(Message)
                    .SetBackgroundColor(BackgroundColor)
                    .SetMessageTextColor(TextColor)
                    .SetDuration(TimeSpan.FromSeconds(2))
                );
        }
    }
}
