namespace PgBuilder
{
    using System.Collections.Generic;
    using PgJsonObjects;

    public class AbilityTierList : List<IPgAbility>
    {
        public AbilityTierList(IPgAbility source)
        {
            Source = source;
            Name = AbilitySlot.CuteDigitStrippedName(source);

            Add(source);
        }

        public IPgAbility Source { get; }
        public string Name { get; }
    }
}
