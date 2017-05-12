﻿using PgJsonParse;
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
            string FileName;

            if ((FileName = value as string) != null)
            {
                string FilePath = Path.Combine(MainWindow.CurrentVersionCacheFolder, FileName);
                FilePath = Path.ChangeExtension(FilePath, ".png");

                if (File.Exists(FilePath))
                    return new BitmapImage(new Uri(FilePath));
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
