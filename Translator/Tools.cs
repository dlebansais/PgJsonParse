namespace PgObjects
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using Translator;

    public static class Tools
    {
        #region Float
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

        public static bool TryParseFloat(string s, out float value, out FloatFormat format)
        {
            Debug.Assert(TryParseSingle(s, out value));
            Debug.Assert(s == SingleToString(value));
            format = FloatFormat.Standard;
            StringToEnumConversion<FloatFormat>.SetCustomParsedEnum(format);
            return true;
            /*
            if (TryParseSingle(s, out Value))
            {
                if (s == SingleToString(Value))
                    Format = FloatFormat.Standard;
                else if (s == SingleToString(Value, "0.0#"))
                    Format = FloatFormat.WithEndingZero;
                else
                    Format = FloatFormat.Other;

                StringToEnumConversion<FloatFormat>.SetCustomParsedEnum(Format);
                return true;
            }

            Format = FloatFormat.Other;
            return false;
            */
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
        public static bool TryParseColor(string s, out uint value)
        {
            if (s == null)
            {
                value = 0;
                return false;
            }

            Color TryNamed = Color.FromName(s);
            if (TryNamed.ToArgb() != 0)
            {
                value = (uint)TryNamed.ToArgb();
                return true;
            }

            if (s.Length != 6 && s.Length != 8)
            {
                value = 0;
                return false;
            }

            byte A, R, G, B;

            if (s.Length == 6)
            {
                if (!TryParseByteHex(s.Substring(0, 2), out R) ||
                    !TryParseByteHex(s.Substring(2, 2), out G) ||
                    !TryParseByteHex(s.Substring(4, 2), out B))
                {
                    value = 0;
                    return false;
                }

                A = 0xFF;
            }
            else
            {
                if (!TryParseByteHex(s.Substring(0, 2), out A) ||
                    !TryParseByteHex(s.Substring(2, 2), out R) ||
                    !TryParseByteHex(s.Substring(4, 2), out G) ||
                    !TryParseByteHex(s.Substring(6, 2), out B))
                {
                    value = 0;
                    return false;
                }
            }

            Color c = Color.FromArgb(A, R, G, B);
            value = 0x0000000 + ((uint)A << 24) + ((uint)R << 16) + ((uint)G << 8) + ((uint)B << 0);
            return true;
        }

        public static bool IsColorName(string s)
        {
            if (s == null)
                return false;

            Color TryNamed = Color.FromName(s);
            if (TryNamed.ToArgb() != 0)
                return true;

            return false;
        }

        public static string ColorToString(uint value)
        {
            byte R = (byte)((value >> 16) & 0xFF);
            byte G = (byte)((value >> 8) & 0xFF);
            byte B = (byte)((value >> 0) & 0xFF);
            Color c = Color.FromArgb(0xFF, R, G, B);
            return c.R.ToString("X02", CultureInfo.InvariantCulture) + c.G.ToString("X02", CultureInfo.InvariantCulture) + c.B.ToString("X02", CultureInfo.InvariantCulture);
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

        #region String
        public static string CleanedUpString(string value)
        {
            return value.Replace("--", "—");
        }
        #endregion

        #region Misc
        public static void VerifyTables(Type type)
        {
        }
        #endregion
    }
}
