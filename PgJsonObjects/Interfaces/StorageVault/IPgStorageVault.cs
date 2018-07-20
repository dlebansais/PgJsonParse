using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgStorageVault : IJsonKey, IObjectContentGenerator, IBackLinkable
    {
        int Id { get; }
        int? RawId { get; }
        IPgGameNpc MatchingNpc { get; }
        int NumSlots { get; }
        int? RawNumSlots { get; }
        string RequirementDescription { get; }
        string InteractionFlagRequirement { get; }
        string NpcFriendlyName { get; }
        ItemKeyword RequiredItemKeyword { get; }
        MapAreaName Grouping { get; }
        bool HasAssociatedNpc { get; }
        bool? RawHasAssociatedNpc { get; }
        MapAreaName Area { get; }
        List<int> FavorLevelList { get; }
        Dictionary<Favor, int> FavorLevelTable { get; }
    }
}
