﻿namespace PgObjects
{
    using System.Collections.Generic;

    public class PgStorageVault : PgObject
    {
        public string Key { get; set; } = string.Empty;
        public int Id { get { return RawId.HasValue ? RawId.Value : 0; } }
        public int? RawId { get; set; }
        public string NpcFriendlyName { get; set; } = string.Empty;
        public MapAreaName Area { get; set; }
        public int NumSlots { get { return RawNumSlots.HasValue ? RawNumSlots.Value : 0; } }
        public int? RawNumSlots { get; set; }
        public bool HasAssociatedNpc { get { return RawHasAssociatedNpc.HasValue && RawHasAssociatedNpc.Value; } }
        public bool? RawHasAssociatedNpc { get; set; }
        public PgNpcLocation AssociatedNpc { get; set; } = null!;
        public PgStorageFavorLevel Levels { get; set; } = null!;
        public PgStorageRequirement Requirement { get; set; } = null!;
        public string RequirementDescription { get; set; } = string.Empty;
        public MapAreaName Grouping { get; set; }
        public List<ItemKeyword> RequiredItemKeywordList { get; set; } = new List<ItemKeyword>();
        public PgAttribute SlotAttribute { get; set; } = null!;

        public override int ObjectIconId { get { return PgObject.StorageVaultIconId; } }
        public override string ObjectName { get { return NpcFriendlyName; } }
        public override string ToString() { return NpcFriendlyName; }
    }
}
