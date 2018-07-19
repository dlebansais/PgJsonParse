using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestObjectiveGuildGiveItem : PgQuestObjective<PgQuestObjectiveGuildGiveItem>, IPgQuestObjectiveGuildGiveItem
    {
        public PgQuestObjectiveGuildGiveItem(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveGuildGiveItem CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestObjectiveGuildGiveItem CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestObjectiveGuildGiveItem(data, ref offset);
        }

        public IPgItem QuestItem { get { return GetObject(PropertiesOffset + 0, ref _QuestItem, PgItem.CreateNew); } } private IPgItem _QuestItem;
        public IPgGameNpc DeliverNpc { get { return GetObject(PropertiesOffset + 4, ref _DeliverNpc, PgGameNpc.CreateNew); } } private IPgGameNpc _DeliverNpc;
        public IPgItemCollection ItemList { get { return GetObjectList(PropertiesOffset + 8, ref _ItemList, PgItemCollection.CreateItem, () => new PgItemCollection()); } } private IPgItemCollection _ItemList;
        public string DeliverNpcId { get { return GetString(PropertiesOffset + 12); } }
        public string DeliverNpcName { get { return GetString(PropertiesOffset + 16); } }
        public ItemKeyword ItemKeyword { get { return GetEnum<ItemKeyword>(PropertiesOffset + 20); } }

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
                GetString = () => Quest.NpcToString(DeliverNpcId, DeliverNpcName) } },
            { "ItemKeyword", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<ItemKeyword>.ToString(ItemKeyword, null, ItemKeyword.Internal_None) } },
            { "ItemName", new FieldParser() {
                Type = FieldType.String,
                GetString = () => QuestItem != null ? QuestItem.InternalName : null } },
        }; } }

        public override IList<IBackLinkable> GetLinkBack()
        {
            List<IBackLinkable> Result = new List<IBackLinkable>();
            Result.Add(QuestItem);
            Result.Add(DeliverNpc);

            if (ItemList != null)
                Result.AddRange(ItemList);

            return Result;
        }

        public override IPgQuestObjectiveRequirement QuestObjectiveRequirement { get { return null; } }
    }
}
