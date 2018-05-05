using PgJsonObjects;
using Presentation;
using System.Windows.Data;

namespace Converters
{
    [ValueConversion(typeof(PowerSkill), typeof(string))]
    public class FriendlyNameConverter : GenericValueConverter
    {
        protected override object Convert(object value, object parameter)
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
    }
}
