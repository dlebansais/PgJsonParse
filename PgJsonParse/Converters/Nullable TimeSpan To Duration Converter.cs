using System;
using System.Globalization;
using System.Windows.Data;

namespace Converters
{
    [ValueConversion(typeof(TimeSpan?), typeof(string))]
    public class NullableTimeSpanToDurationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan? TimeSpanValue = value as TimeSpan?;
            if (!TimeSpanValue.HasValue)
                return "";

            TimeSpan Duration = TimeSpanValue.Value;
            string Result = "";

            if (Duration.TotalDays >= 1)
            {
                int TotalDays = (int)Duration.TotalDays;

                if (Result.Length > 0)
                    Result += " ";

                if (TotalDays > 1)
                    Result += TotalDays.ToString() + " days";
                else
                    Result += TotalDays.ToString() + " day";

                Duration -= TimeSpan.FromDays(TotalDays);
            }

            if (Duration.TotalHours >= 1)
            {
                int TotalHours = (int)Duration.TotalHours;

                if (Result.Length > 0)
                    Result += " ";

                if (TotalHours > 1)
                    Result += TotalHours.ToString() + " hours";
                else
                    Result += TotalHours.ToString() + " hour";

                Duration -= TimeSpan.FromHours(TotalHours);
            }

            if (Duration.TotalMinutes >= 1)
            {
                int TotalMinutes = (int)Duration.TotalMinutes;

                if (Result.Length > 0)
                    Result += " ";

                if (TotalMinutes > 1)
                    Result += TotalMinutes.ToString() + " hours";
                else
                    Result += TotalMinutes.ToString() + " hour";

                Duration -= TimeSpan.FromMinutes(TotalMinutes);
            }

            return Result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return -1;
        }
    }
}
