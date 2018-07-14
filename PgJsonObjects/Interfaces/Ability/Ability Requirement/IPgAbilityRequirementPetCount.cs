namespace PgJsonObjects
{
    public interface IPgAbilityRequirementPetCount : IPgAbilityRequirement
    {
        double MaxCount { get; }
        double? RawMaxCount { get; }
        RecipeKeyword PetTypeTag { get; }
    }
}
