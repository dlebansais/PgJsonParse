namespace PgJsonObjects
{
    using System.Collections.Generic;

    public class PgStorageVault
    {
        public int Id { get { return RawId.HasValue ? RawId.Value : 0; } }
        public int? RawId { get; private set; }
        public PgNpc MatchingNpc { get; private set; }
        public int NumSlots { get { return RawNumSlots.HasValue ? RawNumSlots.Value : 0; } }
        public int? RawNumSlots { get; private set; }
        public string RequirementDescription { get; private set; }
        public string InteractionFlagRequirement { get; private set; }
        public string NpcFriendlyName { get; private set; }
        public ItemKeyword RequiredItemKeyword { get; private set; }
        public MapAreaName Grouping { get; private set; }
        public bool HasAssociatedNpc { get { return RawHasAssociatedNpc.HasValue && RawHasAssociatedNpc.Value; } }
        public bool? RawHasAssociatedNpc { get; private set; }
        public MapAreaName Area { get; private set; }
        public List<int> FavorLevelList { get; private set; } = new List<int>();
        public Dictionary<Favor, int> FavorLevelTable { get; private set; } = new Dictionary<Favor, int>();
        public List<ItemKeyword> RequiredItemKeywordList { get; } = new List<ItemKeyword>();
        public string SlotAttribute { get; private set; }
    }
}
