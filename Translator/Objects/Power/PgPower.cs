namespace PgJsonObjects
{
    using System.Collections.Generic;

    public class PgPower
    {
        public string Prefix { get; set; } = string.Empty;
        public string Suffix { get; set; } = string.Empty;
        public List<ItemSlot> SlotList { get; } = new List<ItemSlot>();
        public PgSkill Skill { get; set; }
        public bool IsUnavailable { get { return RawIsUnavailable.HasValue && RawIsUnavailable.Value; } }
        public bool? RawIsUnavailable { get; set; }
        public PowerSkill RawSkill { get; set; }
        public PgPowerTierCollection TierEffectList { get; } = new PgPowerTierCollection();
        public int TierOffset { get { return RawTierOffset.HasValue ? RawTierOffset.Value : 0; } }
        public int? RawTierOffset { get; set; }
    }
}
