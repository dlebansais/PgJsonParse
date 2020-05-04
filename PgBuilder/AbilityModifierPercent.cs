namespace PgBuilder
{
    using System;

    public class AbilityModifierPercent : AbilityModifier
    {
        public AbilityModifierPercent(AbilityBaseValueGetter getter)
            : base(getter)
        {
        }

        protected override int DefaultValue { get { return 100; } }
    }
}
