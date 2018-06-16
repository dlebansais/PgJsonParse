using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgServerInfo
    {
        List<ServerInfoEffect> ServerInfoEffectList { get; }
        List<Item> GiveItemList { get; }
        int NumItemsToGive { get; }
        int? RawNumItemsToGive { get; }
        List<AbilityRequirement> OtherRequirementList { get; }
        ItemRequiredHotspot RequiredHotspot { get; }
    }
}
