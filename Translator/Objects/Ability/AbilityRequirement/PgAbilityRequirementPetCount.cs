﻿namespace PgObjects
{
    public class PgAbilityRequirementPetCount : PgAbilityRequirement
    {
        public float MaxCount { get { return RawMaxCount.HasValue ? RawMaxCount.Value : 0; } }
        public float? RawMaxCount { get; set; }
        public RecipeKeyword PetTypeTag { get; set; }
    }
}
