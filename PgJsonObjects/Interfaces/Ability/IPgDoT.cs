using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgDoT
    {
        int DamagePerTick { get; }
        int? RawDamagePerTick { get; }
        int NumTicks { get; }
        int? RawNumTicks { get; }
        int Duration { get; }
        int? RawDuration { get; }
        List<DoTSpecialRule> SpecialRuleList { get; }
        string RawPreface { get; }
        DamageType DamageType { get; }
        IPgAttributeCollection AttributesThatDeltaList { get; }
        IPgAttributeCollection AttributesThatModList { get; }
        bool RawAttributesThatDeltaListIsEmpty { get; }
        bool RawAttributesThatModListIsEmpty { get; }
    }
}
