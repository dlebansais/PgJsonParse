namespace Translator;

using System.Collections.Generic;
using PgObjects;

internal class AbilitySetDescriptor
{
    public List<PgAbility> AbilityList { get; init; } = new();

    public AbilityTarget Target { get; set; }
}
