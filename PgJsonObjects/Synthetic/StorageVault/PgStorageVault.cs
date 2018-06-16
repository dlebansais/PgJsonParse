namespace PgJsonObjects
{
    public class PgStorageVault : GenericPgObject, IPgStorageVault
    {
        public PgStorageVault(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public int Id { get { return RawId.HasValue ? RawId.Value : 0; } }
        public int? RawId { get { return GetInt(0); } }
        public GameNpc MatchingNpc { get { return GetObject(4, ref _MatchingNpc); } } private GameNpc _MatchingNpc;
        public int NumSlots { get { return RawNumSlots.HasValue ? RawNumSlots.Value : 0; } }
        public int? RawNumSlots { get { return GetInt(8); } }
        public string RequirementDescription { get { return GetString(12); } }
        public string InteractionFlagRequirement { get { return GetString(16); } }
        public ItemKeyword RequiredItemKeyword { get { return GetEnum<ItemKeyword>(20); } }
        public MapAreaName Grouping { get { return GetEnum<MapAreaName>(22); } }
        public bool HasAssociatedNpc { get { return RawHasAssociatedNpc.HasValue && RawHasAssociatedNpc.Value; } }
        public bool? RawHasAssociatedNpc { get { return GetBool(24, 0); } }
    }
}
