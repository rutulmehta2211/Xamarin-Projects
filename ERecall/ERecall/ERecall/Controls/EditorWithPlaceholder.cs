using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ERecall.Controls
{
    public class EditorWithPlaceholder : Editor
    {
        public static BindableProperty PlaceholderProperty
            = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(EditorWithPlaceholder));

        public static BindableProperty PlaceholderColorProperty
            = BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(EditorWithPlaceholder), Color.Gray);

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public Color PlaceholderColor
        {
            get { return (Color)GetValue(PlaceholderColorProperty); }
            set { SetValue(PlaceholderColorProperty, value); }
        }
    }
}
