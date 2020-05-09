namespace PgBuilder
{
    using System;
    using System.Windows;
    using System.Globalization;

    public partial class App : Application
    {
        public static string DoubleToString(double value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        public static bool? IntModifier(double value)
        {
            if (value > 0)
                return true;
            else if (value < 0)
                return false;
            else
                return null;
        }

        public static int CalculateDamage(double baseDamage, int deltaDamage, double modDamage, double modBaseDamage, double modCriticalDamage)
        {
            if (baseDamage == 809.0)
            {
            }

            float MB1 = (float)baseDamage * (float)modBaseDamage;
            int ModifiedBase = (int)Math.Round(baseDamage * modBaseDamage);
            float MA1 = (float)baseDamage + deltaDamage;
            float MA2 = MA1 * (1.0F + (float)modDamage);
            float M1 = MB1 + MA1;
            int ModifedAbility = (int)Math.Round(((float)baseDamage + deltaDamage) * (1.0F + (float)modDamage));

            float Result = (int)Math.Round(((float)baseDamage * (float)modBaseDamage) + ((float)baseDamage + deltaDamage) * (1.0F + (float)modDamage), MidpointRounding.AwayFromZero);

            //return ((((baseDamage * modBaseDamage + (baseDamage + deltaDamage) * (1 + modDamage)) + SimpleDamage) * DamageDebuff * VulnerabilityDebuff) + (ConditionalFlatDamage * MoreDamageDebuff) + modCriticalDamage;
            if (Result < 0)
                Result = 0;

            return (int)Math.Round(Result);
        }
    }
}
