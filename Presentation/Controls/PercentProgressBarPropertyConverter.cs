using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Presentation
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
                    double Maximum;
                    double Margin;

                    switch (Ctrl.Dock)
                    {
                        default:
                        case System.Windows.Controls.Dock.Left:
                            Maximum = Ctrl.ActualWidth;
                            Margin = Ctrl.ActualWidth * ((100 - DoubleValue) / 100);
                            return new Thickness(0, 0, Margin, 0);

                        case System.Windows.Controls.Dock.Right:
                            Maximum = Ctrl.ActualWidth;
                            Margin = Ctrl.ActualWidth * ((DoubleValue) / 100);
                            return new Thickness(Margin, 0, 0, 0);

                        case System.Windows.Controls.Dock.Top:
                            Maximum = Ctrl.ActualHeight;
                            Margin = Ctrl.ActualHeight * ((100 - DoubleValue) / 100);
                            return new Thickness(0, 0, 0, Margin);

                        case System.Windows.Controls.Dock.Bottom:
                            Maximum = Ctrl.ActualHeight;
                            Margin = Ctrl.ActualHeight * ((DoubleValue) / 100);
                            return new Thickness(0, Margin, 0, 0);
                    }
                }

            return new Thickness(0, 0, 0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}
