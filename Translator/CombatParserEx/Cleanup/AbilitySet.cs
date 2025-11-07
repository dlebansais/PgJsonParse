using PgObjects;

namespace Translator;

internal record AbilitySet
{
    public required AbilityKeyword Ability0 { get; init; }

    public AbilityKeyword? Ability1 { get; init; }

    public AbilityKeyword? Ability2 { get; init; }
}
