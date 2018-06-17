namespace PgJsonObjects
{
    public class PgStorageVault : MainPgObject<PgStorageVault>, IPgStorageVault
    {
        public PgStorageVault(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgStorageVault CreateItem(byte[] data, int offset)
        {
            return new PgStorageVault(data, offset);
        }

        public int Id { get { return RawId.HasValue ? RawId.Value : 0; } }
        public int? RawId { get { return GetInt(0); } }
        public GameNpc MatchingNpc { get { return GetObject(4, ref _MatchingNpc); } } private GameNpc _MatchingNpc;
        public int NumSlots { get { return RawNumSlots.HasValue ? RawNumSlots.Value : 0; } }
        public int? RawNumSlots { get { return GetInt(8); } }
        public string RequirementDescription { get { return GetString(12); } }
        public string InteractionFlagRequirement { get { return GetString(16); } }
        public string NpcFriendlyName { get { return GetString(20); } }
        public ItemKeyword RequiredItemKeyword { get { return GetEnum<ItemKeyword>(24); } }
        public MapAreaName Grouping { get { return GetEnum<MapAreaName>(26); } }
        public bool HasAssociatedNpc { get { return RawHasAssociatedNpc.HasValue && RawHasAssociatedNpc.Value; } }
        public bool? RawHasAssociatedNpc { get { return GetBool(28, 0); } }
    }
}
