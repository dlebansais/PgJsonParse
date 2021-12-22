namespace PgObjects
{
    public class PgPowerTier
    {
        public int Level { get; set; }
        public PgPowerEffectCollection EffectList { get; set; } = new PgPowerEffectCollection();
        public int SkillLevelPrereq { get { return RawSkillLevelPrereq.HasValue ? RawSkillLevelPrereq.Value : 0; } }
        public int? RawSkillLevelPrereq { get; set; }
        public int MinLevel { get { return RawMinLevel.HasValue ? RawMinLevel.Value : 0; } }
        public int? RawMinLevel { get; set; }
        public int MaxLevel { get { return RawMaxLevel.HasValue ? RawMaxLevel.Value : 0; } }
        public int? RawMaxLevel { get; set; }
        public RecipeItemKey MinRarity { get; set; }

        public override string ToString()
        {
            string Result = string.Empty;

            foreach (PgPowerEffect Item in EffectList)
            {
                if (Result.Length > 0)
                    Result += "\n";

                Result += Item.ToString();
            }

            return Result;
        }
    }
}
