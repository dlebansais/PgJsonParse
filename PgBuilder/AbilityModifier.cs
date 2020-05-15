namespace PgBuilder
{
    using PgJsonObjects;
    using System;
    using System.Diagnostics;

    public class AbilityModifier
    {
        public AbilityModifier(string name, AbilityBaseValueGetter getter, Func<float, string> displayHandler)
        {
            Name = name;
            Getter = getter;
            DisplayHandler = displayHandler;
            Ability = null;
            BaseValue = 0;
            DeltaValue = 0;
            MultiplierValue = 1.0F;
        }

        public void SetAbility(IPgAbility ability)
        {
            Ability = ability;
            BaseValue = Ability != null ? Getter(Ability) : 0;
        }

        public string Name { get; }
        public AbilityBaseValueGetter Getter { get; }
        public IPgAbility Ability { get; private set; }
        public float BaseValue { get; private set; }
        public Func<float, string> DisplayHandler { get; }

        protected int DefaultValue { get; set; }
        public bool HasValue { get { return Ability != null && ModifiedValue != DefaultValue || DeltaValue != 0; } }
        public string AsString { get { return Ability != null ? DisplayHandler(ModifiedValue) : string.Empty; } }
        public bool? IsModified { get { return Ability != null ? IntModifier(ModifiedValue - BaseValue) : null; } }

        public float ModifiedValue 
        { 
            get 
            {
                if (Name == "PowerCost" && Ability != null && Ability.Name == "Fire Breath 7")
                {
                }

                if (Ability == null)
                    return DefaultValue;

                float Result = (float)Math.Floor((BaseValue + DeltaValue) * MultiplierValue);
                return Result;
            }
        }

        protected float DeltaValue { get; private set; }
        protected float MultiplierValue { get; private set; }

        public void Reset()
        {
            //Debug.WriteLine($"({Name}) Reset");

            DeltaValue = 0;
            MultiplierValue = 1.0F;
        }

        public void AddValue(float value)
        {
            DeltaValue += value;

            //Debug.WriteLine($"({Name}) AddValue: {value}, new value: {DeltaValue}");
        }

        public void SetValueZero()
        {
            //Debug.WriteLine($"({Name}) SetValueZero");

            DeltaValue = -BaseValue;
        }

        public void AddMultiplier(float multiplierValue)
        {
            MultiplierValue += multiplierValue;
        }

        public int CalculateDifference(float baseValue, float deltaValue)
        {
            //Debug.WriteLine($"({Name}) Base: {baseValue}, Delta: {deltaValue}");

            float Result = baseValue + deltaValue;
            return (int)Math.Round(Result);
        }

        public bool? IntModifier(float value)
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
