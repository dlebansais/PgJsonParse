using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgItemBehavior
    {
        IPgServerInfo ServerInfo { get; }
        List<ItemUseRequirement> UseRequirementList { get; }
        ItemUseAnimation UseAnimation { get; }
        ItemUseAnimation UseDelayAnimation { get; }
        int MetabolismCost { get; }
        int? RawMetabolismCost { get; }
        double UseDelay { get; }
        double? RawUseDelay { get; }
        ItemUseVerb UseVerb { get; }
    }
}
