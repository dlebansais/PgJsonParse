using System.Globalization;

namespace Presentation
{
    public class InvariantCulture
    {
        #region Double
        public static bool TryParseDouble(string s, out double v)
        {
            return double.TryParse(s, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out v);
        }

        public static string DoubleToString(double v)
        {
            return v.ToString(CultureInfo.InvariantCulture);
        }

        public static string DoubleToString(double v, string format)
        {
            return v.ToString(format, CultureInfo.InvariantCulture);
        }
        #endregion

        #region Single
        public static bool TryParseSingle(string s, out float v)
        {
            if (TryParseDouble(s, out double d))
            {
                v = (float)d;
                return true;
            }
            else
            {
                v = float.NaN;
                return false;
            }
        }

        public static string SingleToString(float v)
        {
            return v.ToString(CultureInfo.InvariantCulture);
        }

        public static string SingleToString(float v, string format)
        {
            return v.ToString(format, CultureInfo.InvariantCulture);
        }
        #endregion

        #region Byte
        public static bool TryParseByteHex(string s, out byte v)
        {
            return byte.TryParse(s, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out v);
        }

        public static string ByteToString(byte v)
        {
            return v.ToString(CultureInfo.InvariantCulture);
        }
        #endregion
    }
}
