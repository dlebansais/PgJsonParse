﻿namespace PgJsonObjects
{
    public class PgAbilityRequirementPetCount: GenericPgObject<PgAbilityRequirementPetCount>, IPgAbilityRequirementPetCount
    {
        public PgAbilityRequirementPetCount(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementPetCount CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityRequirementPetCount CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityRequirementPetCount(data, ref offset);
        }

        public double MaxCount { get { return RawMaxCount.HasValue ? RawMaxCount.Value : 0; } }
        public double? RawMaxCount { get { return GetInt(4); } }
        public RecipeKeyword PetTypeTag { get { return GetEnum<RecipeKeyword>(8); } }
    }
}
