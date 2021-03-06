﻿namespace PgObjects
{
    public class PgRecipeResultExtractAugment : PgRecipeResultEffect
    {
        public Augment Augment { get; set; }
        public PgSkill Skill { get; set; } = null!;
        public int MinLevel { get { return RawMinLevel.HasValue ? RawMinLevel.Value : 0; } }
        public int? RawMinLevel { get; set; }
        public int MaxLevel { get { return RawMaxLevel.HasValue ? RawMaxLevel.Value : 0; } }
        public int? RawMaxLevel { get; set; }
    }
}
