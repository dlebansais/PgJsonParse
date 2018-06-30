using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgMinFavorLevelQuestRequirement : GenericPgObject<PgMinFavorLevelQuestRequirement>, IPgMinFavorLevelQuestRequirement
    {
        public PgMinFavorLevelQuestRequirement(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgMinFavorLevelQuestRequirement CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgMinFavorLevelQuestRequirement CreateNew(byte[] data, ref int offset)
        {
            return new PgMinFavorLevelQuestRequirement(data, ref offset);
        }

        public override string Key { get { return GetString(4); } }
        public IPgGameNpc FavorNpc { get { return GetObject(8, ref _FavorNpc, PgGameNpc.CreateNew); } } private IPgGameNpc _FavorNpc;
        protected override List<string> FieldTableOrder { get { return GetStringList(12, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public bool IsEmpty { get { return RawIsEmpty.HasValue && RawIsEmpty.Value; } }
        public bool? RawIsEmpty { get { return GetBool(16, 0); } }
        public Favor FavorLevel { get { return GetEnum<Favor>(18); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
    }
}
