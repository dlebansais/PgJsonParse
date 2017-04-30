using PgJsonObjects;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Converters
{
    [ValueConversion(typeof(PowerSkill), typeof(string))]
    public class FriendlyNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string s;

            if (value is PowerSkill)
            {
                PowerSkill skill = (PowerSkill)value;
                s = skill.ToString();
            }
            else
                s = value as string;

            if (s == null)
                s = "";

            int i = 0;
            while (i < s.Length)
            {
                if (char.IsUpper(s[i]) && i > 0)
                {
                    s = s.Substring(0, i) + " " + s.Substring(i);
                    i++;
                }

                i++;
            }

            return s;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
