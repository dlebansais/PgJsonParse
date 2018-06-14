namespace PgJsonObjects
{
    public interface IPgAbilityRequirementGardenPlantMax
    {
        int Max { get; }
        int? RawMax { get; }
        AbilityTypeTag TypeTag { get; }
    }
}
