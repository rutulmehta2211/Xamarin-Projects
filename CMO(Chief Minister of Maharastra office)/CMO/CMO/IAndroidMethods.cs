using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMO
{
    public interface IAndroidMethods
    {
        void CloseApp();
    }

    public interface INetworkConnection
    {
        bool IsConnected { get; }
        void CheckNetworkConnection();
    }

}
