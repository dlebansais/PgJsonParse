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

        public static int CalculatePowerCost(double basePowerCost, double deltaPowerCost)
        {
            double Result = basePowerCost + deltaPowerCost;
            if (Result < 0)
                Result = 0;

            return (int)Math.Round(Result);
        }

        public static int CalculateResetTime(double baseResetTime, double deltaResetTime)
        {
            double Result = baseResetTime + deltaResetTime;
            if (Result < 0)
                Result = 0;

            return (int)Math.Round(Result);
        }

        public static int CalculateDamage(double baseDamage, double deltaDamage, double modDamage, double modBaseDamage, double modCriticalDamage)
        {
            double Result = baseDamage * modBaseDamage;
            //return ((((baseDamage + deltaDamage) * (1 + modDamage) + (baseDamage * modBaseDamage)) + SimpleDamage) * DamageDebuff * VulnerabilityDebuff) + (ConditionalFlatDamage * MoreDamageDebuff) + modCriticalDamage;
            if (Result < 0)
                Result = 0;

            return (int)Math.Round(Result);
        }
    }
}
