using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestObjectiveDeliver : PgQuestObjective<PgQuestObjectiveDeliver>, IPgQuestObjectiveDeliver
    {
        public PgQuestObjectiveDeliver(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveDeliver CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestObjectiveDeliver CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestObjectiveDeliver(data, ref offset);
        }

        public IPgGameNpc DeliverNpc { get { return GetObject(PropertiesOffset + 0, ref _DeliverNpc, PgGameNpc.CreateNew); } } private IPgGameNpc _DeliverNpc;
        public IPgItem QuestItem { get { return GetObject(PropertiesOffset + 4, ref _QuestItem, PgItem.CreateNew); } } private IPgItem _QuestItem;
        public int NumToDeliver { get { return RawNumToDeliver.HasValue ? RawNumToDeliver.Value : 0; } }
        public int? RawNumToDeliver { get { return GetInt(PropertiesOffset + 8); } }
        public string DeliverNpcId { get { return GetString(PropertiesOffset + 12); } }
        public string DeliverNpcName { get { return GetString(PropertiesOffset + 16); } }
        public MapAreaName DeliverNpcArea { get { return GetEnum<MapAreaName>(PropertiesOffset + 20); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Type", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<QuestObjectiveType>.ToString(Type, null, QuestObjectiveType.Internal_None) } },
            { "MustCompleteEarlierObjectivesFirst", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawMustCompleteEarlierObjectivesFirst } },
            { "Description", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Description } },
            { "Number", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawNumber } },
            { "Target", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Quest.NpcToString(DeliverNpcArea, DeliverNpcId, DeliverNpcName, false) } },
            { "ItemName", new FieldParser() {
                Type = FieldType.String,
                GetString = () => QuestItem != null ? QuestItem.InternalName : null } },
            { "NumToDeliver", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawNumToDeliver } },
        }; } }

        public override IList<IBackLinkable> GetLinkBack()
        {
            return new List<IBackLinkable>() { DeliverNpc, QuestItem };
        }
    }
}
