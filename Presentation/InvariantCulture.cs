using System;
using System.Drawing;
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
            return Math.Round(v, 2).ToString(CultureInfo.InvariantCulture);
        }

        public static string DoubleToString(double v, string format)
        {
            return v.ToString(format, CultureInfo.InvariantCulture);
        }
        #endregion

        #region Single
        public static bool TryParseSingle(string s, out float v)
        {
            return float.TryParse(s, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out v);
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

        #region Color
        public static bool TryParseColor(string s, out uint Value)
        {
            if (s == null)
            {
                Value = 0;
                return false;
            }

            Color TryNamed = Color.FromName(s);
            if (TryNamed.ToArgb() != 0)
            {
                Value = (uint)TryNamed.ToArgb();
                return true;
            }

            if (s.Length != 6)
            {
                Value = 0;
                return false;
            }

            byte R, G, B;

            if (!TryParseByteHex(s.Substring(0, 2), out R) ||
                !TryParseByteHex(s.Substring(2, 2), out G) ||
                !TryParseByteHex(s.Substring(4, 2), out B))
            {
                Value = 0;
                return false;
            }

            Color c = Color.FromArgb(0xFF, R, G, B);
            Value = (0xFF000000 + ((uint)R << 16) + ((uint)G << 8) + ((uint)B << 0));
            return true;
        }

        public static string ColorToString(uint Value)
        {
            byte R = (byte)((Value >> 16) & 0xFF);
            byte G = (byte)((Value >> 8) & 0xFF);
            byte B = (byte)((Value >> 0) & 0xFF);
            Color c = Color.FromArgb(0xFF, R, G, B);
            return c.R.ToString("X02") + c.G.ToString("X02") + c.B.ToString("X02");
        }

        public static System.Windows.Media.Brush ColorToBrush(uint? rgbColor)
        {
            if (rgbColor.HasValue)
            {
                byte R = (byte)((rgbColor.Value >> 16) & 0xFF);
                byte G = (byte)((rgbColor.Value >> 8) & 0xFF);
                byte B = (byte)((rgbColor.Value >> 0) & 0xFF);
                System.Windows.Media.Color c = System.Windows.Media.Color.FromArgb(0xFF, R, G, B);
                return new System.Windows.Media.SolidColorBrush(c);
            }
            else
                return new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Transparent);
        }
        #endregion

        #region Enum
        public static bool TryParseEnum<T>(string s, out T enumValue, out int enumIndex)
        {
            if (!string.IsNullOrEmpty(s))
            {
                string[] EnumNames = Enum.GetNames(typeof(T));
                Array EnumValues = Enum.GetValues(typeof(T));

                for (int i = 0; i < EnumNames.Length; i++)
                    if (s == EnumNames[i])
                    {
                        enumValue = (T)EnumValues.GetValue(i);
                        enumIndex = i;
                        return true;
                    }
            }

            enumValue = default(T);
            enumIndex = -1;
            return false;
        }

        public static bool TryFindIndex<T>(T enumValue, out int enumIndex)
        {
            Array EnumValues = Enum.GetValues(typeof(T));

            for (int i = 0; i < EnumValues.Length; i++)
                if ((int)EnumValues.GetValue(i) == (int)(object)enumValue)
                {
                    enumIndex = i;
                    return true;
                }

            enumIndex = -1;
            return false;
        }
        #endregion

        #region Misc
        public static string NewLine { get; } = "\r\n";
        #endregion
    }
}
