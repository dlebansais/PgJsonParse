namespace PgJsonObjects
{
    public interface IPgAbilityRequirementPetCount
    {
        double MaxCount { get; }
        double? RawMaxCount { get; }
        RecipeKeyword PetTypeTag { get; }
    }
}
