using System.Collections.Generic;

namespace PgJsonObjects
{
    interface IPgPower
    {
        string Prefix { get; }
        string Suffix { get; }
        List<ItemSlot> SlotList { get; }
        Skill ConnectedSkill { get; }
        bool IsUnavailable { get; }
        bool? RawIsUnavailable { get; }
    }
}
