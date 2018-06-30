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
        public bool IsEmpty { get { return RawIsEmpty.HasValue && RawIsEmpty.Value; } }
        public bool? RawIsEmpty { get { return GetBool(12, 0); } }
        public Favor FavorLevel { get { return GetEnum<Favor>(14); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
        protected override List<string> FieldTableOrder { get { return FieldTableOrder; } }
    }
}
