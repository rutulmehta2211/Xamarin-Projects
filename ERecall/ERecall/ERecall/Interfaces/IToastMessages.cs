using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERecall.Interfaces
{
    public interface IToastMessages
    {
        void LongAlert(string message);
        void ShortAlert(string message);
    }
}
