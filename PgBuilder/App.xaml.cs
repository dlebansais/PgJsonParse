﻿namespace PgBuilder
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
        public static int CalculateDamage(float baseDamage, int deltaDamage, float modDamage, float modBaseDamage, float boostDamage, float modCriticalDamage)
        {
            if (baseDamage == 214.0F)
            {
            }

            float Result;

            Result = ((baseDamage * modBaseDamage) + (baseDamage + deltaDamage) * (1.0F + modDamage)) + boostDamage;
            return (int)Math.Round(Result, MidpointRounding.AwayFromZero);
        }
    }
}
