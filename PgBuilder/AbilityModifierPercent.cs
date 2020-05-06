namespace PgBuilder
{
    using System;

    public class AbilityModifierPercent : AbilityModifier
    {
        public AbilityModifierPercent(string name, AbilityBaseValueGetter getter, Func<int, string> displayHandler)
            : base(name, getter, displayHandler)
        {
        }

        protected override int DefaultValue { get { return 100; } }
    }
}
