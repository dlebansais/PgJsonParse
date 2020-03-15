namespace PgBuilder
{
    using System.Windows;
    using System.Globalization;

    public partial class App : Application
    {
        public static string DoubleToString(double value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        public static bool? IntModifier(int value)
        {
            if (value > 0)
                return true;
            else if (value < 0)
                return false;
            else
                return null;
        }
    }
}
