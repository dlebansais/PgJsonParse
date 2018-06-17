using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgServerInfo
    {
        ServerInfoEffectCollection ServerInfoEffectList { get; }
        ItemCollection GiveItemList { get; }
        int NumItemsToGive { get; }
        int? RawNumItemsToGive { get; }
        AbilityRequirementCollection OtherRequirementList { get; }
        ItemRequiredHotspot RequiredHotspot { get; }
    }
}
