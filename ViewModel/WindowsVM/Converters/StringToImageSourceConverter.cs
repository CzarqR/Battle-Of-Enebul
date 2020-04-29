using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ProjectB.ViewModel.WindowsVM.Converters
{
    [ValueConversion(typeof(string), typeof(ImageSource))]
    public class StringToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var path = value.ToString();
            return new BitmapImage(new Uri(path, UriKind.Absolute));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
