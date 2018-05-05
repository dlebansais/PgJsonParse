using Presentation;
using System;
using System.Windows.Data;

namespace Converters
{
    [ValueConversion(typeof(TimeSpan?), typeof(string))]
    public class NullableTimeSpanToDurationConverter : GenericValueConverter
    {
        protected override object Convert(object value, object parameter)
        {
            TimeSpan? TimeSpanValue = value as TimeSpan?;
            if (!TimeSpanValue.HasValue)
                return "";

            TimeSpan Duration = TimeSpanValue.Value;
            return PgJsonObjects.Tools.TimeSpanToString(Duration);
        }
    }
}
