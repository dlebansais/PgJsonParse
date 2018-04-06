using System.Globalization;

namespace Presentation
{
    public class StringParser
    {
        public static bool TryParseDouble(string s, out double v)
        {
            return double.TryParse(s, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out v);
        }
    }
}
