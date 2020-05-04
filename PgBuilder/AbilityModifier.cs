namespace PgBuilder
{
    using PgJsonObjects;
    using System;

    public class AbilityModifier
    {
        public AbilityModifier(AbilityBaseValueGetter getter)
        {
            Getter = getter;
            Ability = null;
            BaseValue = 0;
            DeltaValue = 0;
        }

        public void SetAbility(IPgAbility ability)
        {
            Ability = ability;
            BaseValue = Ability != null ? Getter(Ability) : 0;
        }

        public AbilityBaseValueGetter Getter { get; }
        public IPgAbility Ability { get; private set; }
        public double BaseValue { get; private set; }

        protected virtual int DefaultValue { get { return 0; } }
        public bool HasValue { get { return Ability != null && ModifiedValue != DefaultValue; } }
        public int ModifiedValue { get { return Ability != null ? CalculateDifference(BaseValue, DeltaValue) : DefaultValue; } }
        public string AsString { get { return Ability != null ? ModifiedValue.ToString() : string.Empty; } }
        public bool? IsModified { get { return Ability != null ? IntModifier(ModifiedValue - BaseValue) : null; } }
        protected double DeltaValue { get; private set; }

        public void Reset()
        {
            DeltaValue = 0;
        }

        public void AddValue(double value)
        {
            DeltaValue += value;
        }

        public void SetValueZero()
        {
            DeltaValue = -BaseValue;
        }

        public int CalculateDifference(double baseValue, double deltaValue)
        {
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
