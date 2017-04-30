using PgJsonParse;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Converters
{
    [ValueConversion(typeof(string), typeof(BitmapImage))]
    public class FileNameToBitmapImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string FileName = value as string;

            string FilePath = Path.Combine(MainWindow.CurrentVersionCacheFolder, FileName);
            FilePath = Path.ChangeExtension(FilePath, ".png");

            return new BitmapImage(new Uri(FilePath));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
