using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgStorageVault : MainPgObject<PgStorageVault>, IPgStorageVault
    {
        public PgStorageVault(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 38;
            SerializableJsonObject.AlignSerializedLength(ref offset);
        }

        protected override PgStorageVault CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgStorageVault CreateNew(byte[] data, ref int offset)
        {
            return new PgStorageVault(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public int Id { get { return RawId.HasValue ? RawId.Value : 0; } }
        public int? RawId { get { return GetInt(4); } }
        public IPgGameNpc MatchingNpc { get { return GetObject(8, ref _MatchingNpc, PgGameNpc.CreateNew); } } private IPgGameNpc _MatchingNpc;
        public int NumSlots { get { return RawNumSlots.HasValue ? RawNumSlots.Value : 0; } }
        public int? RawNumSlots { get { return GetInt(12); } }
        public string RequirementDescription { get { return GetString(16); } }
        public string InteractionFlagRequirement { get { return GetString(20); } }
        public string NpcFriendlyName { get { return GetString(24); } }
        public ItemKeyword RequiredItemKeyword { get { return GetEnum<ItemKeyword>(28); } }
        public MapAreaName Grouping { get { return GetEnum<MapAreaName>(30); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(32, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public bool HasAssociatedNpc { get { return RawHasAssociatedNpc.HasValue && RawHasAssociatedNpc.Value; } }
        public bool? RawHasAssociatedNpc { get { return GetBool(36, 0); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
    }
}
