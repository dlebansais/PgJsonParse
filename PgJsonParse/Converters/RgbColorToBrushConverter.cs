using Presentation;
using System.Windows.Data;

namespace Converters
{
    [ValueConversion(typeof(uint?), typeof(object))]
    public class RgbColorToBrushConverter : GenericValueConverter
    {
        protected override object Convert(object value, object parameter)
        {
            uint? rgbColor = value as uint?;
            return InvariantCulture.ColorToBrush(rgbColor);
        }
    }
}
