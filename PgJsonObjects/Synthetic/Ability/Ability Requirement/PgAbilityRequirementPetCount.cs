namespace PgJsonObjects
{
    public class PgAbilityRequirementPetCount: GenericPgObject, IPgAbilityRequirementPetCount
    {
        public PgAbilityRequirementPetCount(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public double MaxCount { get { return RawMaxCount.HasValue ? RawMaxCount.Value : 0; } }
        public double? RawMaxCount { get { return GetInt(4); } }
        public RecipeKeyword PetTypeTag { get { return GetEnum<RecipeKeyword>(8); } }
    }
}
