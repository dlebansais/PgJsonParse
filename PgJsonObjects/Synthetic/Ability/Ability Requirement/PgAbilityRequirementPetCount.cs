namespace PgJsonObjects
{
    public class PgAbilityRequirementPetCount: GenericPgObject<PgAbilityRequirementPetCount>, IPgAbilityRequirementPetCount
    {
        public PgAbilityRequirementPetCount(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementPetCount CreateItem(byte[] data, int offset)
        {
            return new PgAbilityRequirementPetCount(data, offset);
        }

        public double MaxCount { get { return RawMaxCount.HasValue ? RawMaxCount.Value : 0; } }
        public double? RawMaxCount { get { return GetInt(4); } }
        public RecipeKeyword PetTypeTag { get { return GetEnum<RecipeKeyword>(8); } }
    }
}
