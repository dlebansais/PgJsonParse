namespace PgBuilder
{
    using System;

    public class AbilityModifierPercent : AbilityModifier
    {
        public AbilityModifierPercent(string name, int defaultValue, AbilityBaseValueGetter getter, Func<float, string> displayHandler)
            : base(name, getter, displayHandler)
        {
            DefaultValue = defaultValue;
        }
    }
}
