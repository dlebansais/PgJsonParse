using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgPower : IJsonKey, IObjectContentGenerator
    {
        string Prefix { get; }
        string Suffix { get; }
        List<ItemSlot> SlotList { get; }
        IPgSkill Skill { get; }
        bool IsUnavailable { get; }
        bool? RawIsUnavailable { get; }
        PowerSkill RawSkill { get; }
        PowerTierCollection TierEffectList { get; }
        int TierOffset { get; }
        int? RawTierOffset { get; }
    }
}
