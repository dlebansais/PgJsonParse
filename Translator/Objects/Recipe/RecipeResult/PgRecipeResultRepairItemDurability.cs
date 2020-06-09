namespace PgJsonObjects
{
    public class PgRecipeResultRepairItemDurability : PgRecipeResultEffect
    {
        public float RepairMinEfficiency { get { return RawRepairMinEfficiency.HasValue ? RawRepairMinEfficiency.Value : 0; } }
        public float? RawRepairMinEfficiency { get; set; }
        public float RepairMaxEfficiency { get { return RawRepairMaxEfficiency.HasValue ? RawRepairMaxEfficiency.Value : 0; } }
        public float? RawRepairMaxEfficiency { get; set; }
        public int RepairCooldown { get { return RawRepairCooldown.HasValue ? RawRepairCooldown.Value : 0; } }
        public int? RawRepairCooldown { get; set; }
        public int MinLevel { get { return RawMinLevel.HasValue ? RawMinLevel.Value : 0; } }
        public int? RawMinLevel { get; set; }
        public int MaxLevel { get { return RawMaxLevel.HasValue ? RawMaxLevel.Value : 0; } }
        public int? RawMaxLevel { get; set; }
    }
}
