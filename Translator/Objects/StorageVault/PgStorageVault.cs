namespace PgJsonObjects
{
    using System.Collections.Generic;

    public class PgStorageVault
    {
        public int Id { get { return RawId.HasValue ? RawId.Value : 0; } }
        public int? RawId { get; set; }
        public PgNpc MatchingNpc { get; set; }
        public int NumSlots { get { return RawNumSlots.HasValue ? RawNumSlots.Value : 0; } }
        public int? RawNumSlots { get; set; }
        public string RequirementDescription { get; set; }
        public string InteractionFlagRequirement { get; set; }
        public string NpcFriendlyName { get; set; }
        public ItemKeyword RequiredItemKeyword { get; set; }
        public MapAreaName Grouping { get; set; }
        public bool HasAssociatedNpc { get { return RawHasAssociatedNpc.HasValue && RawHasAssociatedNpc.Value; } }
        public bool? RawHasAssociatedNpc { get; set; }
        public MapAreaName Area { get; set; }
        public List<int> FavorLevelList { get; } = new List<int>();
        public Dictionary<Favor, int> FavorLevelTable { get; } = new Dictionary<Favor, int>();
        public List<ItemKeyword> RequiredItemKeywordList { get; } = new List<ItemKeyword>();
        public string SlotAttribute { get; set; }
    }
}
