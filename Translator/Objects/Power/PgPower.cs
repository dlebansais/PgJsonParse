namespace PgObjects
{
    using System.Collections.Generic;

    public class PgPower : PgObject
    {
        public string Key { get; set; } = string.Empty;
        public string Prefix { get; set; } = string.Empty;
        public string Suffix { get; set; } = string.Empty;
        public List<ItemSlot> SlotList { get; set; } = new List<ItemSlot>();
        public PgSkill Skill { get; set; }
        public bool IsUnavailable { get { return RawIsUnavailable.HasValue && RawIsUnavailable.Value; } }
        public bool? RawIsUnavailable { get; set; }
        public PgPowerTierCollection TierList { get; set; } = new PgPowerTierCollection();

        public int IconId { get; set; }
        public string ComposedName { get; set; } = string.Empty;

        public override int ObjectIconId { get { return IconId; } }
        public override string ObjectName { get { return ComposedName; } }
    }
}
