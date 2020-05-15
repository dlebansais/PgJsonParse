namespace PgBuilder
{
    using System;
    using System.Windows;
    using System.Globalization;

    public partial class App : Application
    {
        public static string DoubleToString(float value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        public static bool? IntModifier(float value)
        {
            if (value > 0)
                return true;
            else if (value < 0)
                return false;
            else
                return null;
        }

        //return ((((baseDamage * modBaseDamage + (baseDamage + deltaDamage) * (1 + modDamage)) + SimpleDamage) * DamageDebuff * VulnerabilityDebuff) + (ConditionalFlatDamage * MoreDamageDebuff) + modCriticalDamage;
        public static int CalculateDamage(float baseDamage, int deltaDamage, float modDamage, float modBaseDamage, float boostDamage, float modCriticalDamage, float fireballModDamage)
        {
            if (baseDamage == 214.0)
            {
            }

            float Result;

            if (fireballModDamage != 0)
            {
                Result = baseDamage * modBaseDamage;
                Result = (float)Math.Floor(Result) + baseDamage + deltaDamage;
                Result *= 1.0F + fireballModDamage;

                return (int)Math.Floor(Result);
            }
            else
            {
                Result = ((baseDamage * modBaseDamage) + (baseDamage + deltaDamage) * (1.0F + modDamage)) + boostDamage;
                return (int)Math.Round(Result, MidpointRounding.AwayFromZero);
            }
        }
    }
}
