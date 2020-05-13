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

        //return ((((baseDamage * modBaseDamage + (baseDamage + deltaDamage) * (1 + modDamage)) + SimpleDamage) * DamageDebuff * VulnerabilityDebuff) + (ConditionalFlatDamage * MoreDamageDebuff) + modCriticalDamage;
        public static int CalculateDamage(double baseDamage, int deltaDamage, double modDamage, double modBaseDamage, double boostDamage, double modCriticalDamage, double fireballModDamage)
        {
            if (baseDamage == 127.0)
            {
            }

            float Result;

            if (fireballModDamage != 0)
            {
                Result = (float)baseDamage * (float)modBaseDamage;
                Result = (float)Math.Floor(Result) + (float)baseDamage + deltaDamage;
                Result *= (float)(1.0 + fireballModDamage);

                return (int)Math.Floor(Result);
            }
            else
            {
                Result = (((float)baseDamage * (float)modBaseDamage) + ((float)baseDamage + deltaDamage) * (1.0F + (float)modDamage)) + (float)boostDamage;
                return (int)Math.Round(Result, MidpointRounding.AwayFromZero);
            }
        }
    }
}
