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
    }
}
