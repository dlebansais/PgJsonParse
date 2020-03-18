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

        public static double CalculateDamage(double baseDamage, double deltaDamage, double modDamage, double modBaseDamage, double modCriticalDamage)
        {
            double SimpleDamage = 0;
            double DamageDebuff = 1.0;
            double VulnerabilityDebuff = 1.0;
            double MoreDamageDebuff = 1.0;
            double ConditionalFlatDamage = 0;
            return ((((baseDamage + deltaDamage) * (1 + modDamage) + (baseDamage * modBaseDamage)) + SimpleDamage) * DamageDebuff * VulnerabilityDebuff) + (ConditionalFlatDamage * MoreDamageDebuff) + modCriticalDamage;
        }
    }
}
