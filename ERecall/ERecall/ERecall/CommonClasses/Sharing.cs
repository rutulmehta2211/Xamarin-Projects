using Plugin.Share;
using Plugin.Share.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERecall.CommonClasses
{
    public class Sharing
    {
        public static void Share(string title,string msg,string url)
        {
            if (!CrossShare.IsSupported)
                return;

            CrossShare.Current.Share(new ShareMessage
            {
                Title = title,
                Text = msg,
                Url = url
            });
        }
    }
}
