using CustomControls;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Converters
{
    public class PercentProgressBarPropertyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double DoubleValue = (double)value;
            if (double.IsNaN(DoubleValue))
                DoubleValue = 0;

            string ControlName = parameter as string;
            foreach (PercentProgressBar Ctrl in PercentProgressBar.ControlList)
                if (Ctrl.Name == ControlName)
                {
                    double Maximum = Ctrl.ActualWidth;
                    double Margin = Ctrl.ActualWidth * ((100 - DoubleValue) / 100);
                    return new Thickness(0, 0, Margin, 0);
                }

            return new Thickness(0, 0, 0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}
