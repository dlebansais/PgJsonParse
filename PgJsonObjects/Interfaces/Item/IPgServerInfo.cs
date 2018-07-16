using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgServerInfo
    {
        IPgServerInfoEffectCollection ServerInfoEffectList { get; }
        IPgItemCollection GiveItemList { get; }
        int NumItemsToGive { get; }
        int? RawNumItemsToGive { get; }
        IPgAbilityRequirementCollection OtherRequirementList { get; }
        ItemRequiredHotspot RequiredHotspot { get; }
        bool IsServerInfoEffectListEmpty { get; }
        bool? RawIsServerInfoEffectListEmpty { get; }
        bool IsOtherRequirementListEmpty { get; }
        bool? RawIsOtherRequirementListEmpty { get; }
    }
}
