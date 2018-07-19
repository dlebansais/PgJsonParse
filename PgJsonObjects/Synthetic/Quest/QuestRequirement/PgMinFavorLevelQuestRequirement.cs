using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgMinFavorLevelQuestRequirement : PgQuestRequirement<PgMinFavorLevelQuestRequirement>, IPgMinFavorLevelQuestRequirement
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

        public IPgGameNpc FavorNpc { get { return GetObject(PropertiesOffset + 0, ref _FavorNpc, PgGameNpc.CreateNew); } } private IPgGameNpc _FavorNpc;
        public bool IsEmpty { get { return RawIsEmpty.HasValue && RawIsEmpty.Value; } }
        public bool? RawIsEmpty { get { return GetBool(PropertiesOffset + 4, 0); } }
        public Favor FavorLevel { get { return GetEnum<Favor>(PropertiesOffset + 6); } }
        public string FavorNpcId { get { return GetString(PropertiesOffset + 8); } }
        public string FavorNpcName { get { return GetString(PropertiesOffset + 12); } }
        public MapAreaName FavorNpcArea { get { return GetEnum<MapAreaName>(PropertiesOffset + 16); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType, null, OtherRequirementType.Internal_None) } },
            { "Npc", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Quest.NpcToString(FavorNpcArea, FavorNpcId, FavorNpcName, IsEmpty) } },
            { "Level", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<Favor>.ToString(FavorLevel, null, Favor.Internal_None) } },
        }; } }

        public override IList<IBackLinkable> GetLinkBack()
        {
            return new List<IBackLinkable>() { FavorNpc };
        }
    }
}
