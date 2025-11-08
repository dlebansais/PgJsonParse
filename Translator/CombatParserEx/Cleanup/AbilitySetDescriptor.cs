namespace Translator;

using System.Collections.Generic;
using PgObjects;

internal class AbilitySetDescriptor
{
    public List<PgAbility> AbilityList { get; init; } = new();

    public TargetCategories TargetCategories { get; set; }
}
