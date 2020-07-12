namespace PgObjects
{
    using System;

    public class PgRecipeResultRepairItemDurability : PgRecipeResultEffect
    {
        public float RepairMinEfficiency { get { return RawRepairMinEfficiency.HasValue ? RawRepairMinEfficiency.Value : 0; } }
        public float? RawRepairMinEfficiency { get; set; }
        public float RepairMaxEfficiency { get { return RawRepairMaxEfficiency.HasValue ? RawRepairMaxEfficiency.Value : 0; } }
        public float? RawRepairMaxEfficiency { get; set; }
        public TimeSpan RepairCooldown { get { return RawRepairCooldown.HasValue ? RawRepairCooldown.Value : TimeSpan.Zero; } }
        public TimeSpan? RawRepairCooldown { get; set; }
        public int MinLevel { get { return RawMinLevel.HasValue ? RawMinLevel.Value : 0; } }
        public int? RawMinLevel { get; set; }
        public int MaxLevel { get { return RawMaxLevel.HasValue ? RawMaxLevel.Value : 0; } }
        public int? RawMaxLevel { get; set; }
    }
}
