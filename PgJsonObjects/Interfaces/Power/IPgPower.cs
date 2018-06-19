using System.Collections.Generic;

namespace PgJsonObjects
{
    interface IPgPower
    {
        string Prefix { get; }
        string Suffix { get; }
        List<ItemSlot> SlotList { get; }
        IPgSkill Skill { get; }
        bool IsUnavailable { get; }
        bool? RawIsUnavailable { get; }
        PowerSkill RawSkill { get; }
    }
}
