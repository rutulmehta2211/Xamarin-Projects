using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ERecall.CommonClasses
{
    public class Conversion
    {
        public static Stream Base64ToStream(string base64String)
        {
            byte[] fileBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(fileBytes, 0, fileBytes.Length);
            return ms;
        }

        public static string randomestring()
        {
            Guid g;
            // Create and display the value of two GUIDs.
            g = Guid.NewGuid();
            return g.ToString();
        }

        public static string NullToString(object value)
        {
            return value == null ? "" : value.ToString();
        }
        public static int NullToZero(object value)
        {
            return value == null ? 0 : Convert.ToInt32(value);
        }
        public static string ConvertedDate(string date)
        {
            if(!string.IsNullOrEmpty(date))
            {
                DateTime returnValue;
                bool flag = DateTime.TryParseExact(date, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out returnValue);
                return flag ? returnValue.ToString("MM.dd.yyyy") : null;
            }
            else
            {
                return null;
            }
        }

        public static string ConvertedDate2(string date)
        {
            if (!string.IsNullOrEmpty(date))
            {
                DateTime datetime = Convert.ToDateTime(date);
                return datetime.ToString("MM.dd.yyyy");
            }
            else
            {
                return null;
            }
        }

        public static System.Drawing.Color FromHex(string hex)
        {
            var c = Color.FromHex(hex);
            var dc = System.Drawing.Color.FromArgb((int)c.A, (int)c.R, (int)c.G, (int)c.B);
            return dc;
        }
    }
}
