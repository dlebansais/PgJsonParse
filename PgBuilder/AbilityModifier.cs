﻿namespace PgBuilder
{
    using PgJsonObjects;
    using System;
    using System.Diagnostics;

    public class AbilityModifier
    {
        public AbilityModifier(string name, AbilityBaseValueGetter getter, Func<int, string> displayHandler)
        {
            Name = name;
            Getter = getter;
            DisplayHandler = displayHandler;
            Ability = null;
            BaseValue = 0;
            DeltaValue = 0;
        }

        public void SetAbility(IPgAbility ability)
        {
            Ability = ability;
            BaseValue = Ability != null ? Getter(Ability) : 0;
        }

        public string Name { get; }
        public AbilityBaseValueGetter Getter { get; }
        public IPgAbility Ability { get; private set; }
        public double BaseValue { get; private set; }
        public Func<int, string> DisplayHandler { get; }

        protected virtual int DefaultValue { get { return 0; } }
        public bool HasValue { get { return Ability != null && ModifiedValue != DefaultValue || DeltaValue != 0; } }
        public int ModifiedValue { get { return Ability != null ? CalculateDifference(BaseValue, DeltaValue) : DefaultValue; } }
        public string AsString { get { return Ability != null ? DisplayHandler(ModifiedValue) : string.Empty; } }
        public bool? IsModified { get { return Ability != null ? IntModifier(ModifiedValue - BaseValue) : null; } }
        protected double DeltaValue { get; private set; }

        public void Reset()
        {
            //Debug.WriteLine($"({Name}) Reset");

            DeltaValue = 0;
        }

        public void AddValue(double value)
        {
            DeltaValue += value;

            //Debug.WriteLine($"({Name}) AddValue: {value}, new value: {DeltaValue}");
        }

        public void SetValueZero()
        {
            //Debug.WriteLine($"({Name}) SetValueZero");

            DeltaValue = -BaseValue;
        }

        public int CalculateDifference(double baseValue, double deltaValue)
        {
            //Debug.WriteLine($"({Name}) Base: {baseValue}, Delta: {deltaValue}");

            double Result = baseValue + deltaValue;
            return (int)Math.Round(Result);
        }

        public bool? IntModifier(double value)
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
