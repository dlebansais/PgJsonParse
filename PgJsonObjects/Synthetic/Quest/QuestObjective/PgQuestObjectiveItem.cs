using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestObjectiveItem : PgQuestObjective<PgQuestObjectiveItem>, IPgQuestObjectiveItem
    {
        public PgQuestObjectiveItem(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveItem CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestObjectiveItem CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestObjectiveItem(data, ref offset);
        }

        public IPgItem QuestItem { get { return GetObject(PropertiesOffset + 0, ref _QuestItem, PgItem.CreateNew); } } private IPgItem _QuestItem;
        public ItemCollection TargetItemList { get { return GetObjectList(PropertiesOffset + 4, ref _TargetItemList, ItemCollection.CreateItem, () => new ItemCollection()); } } private ItemCollection _TargetItemList;
        public IPgQuestObjectiveRequirement QuestObjectiveRequirement { get { return GetObject(PropertiesOffset + 0, ref _QuestObjectiveRequirement, PgQuestObjectiveRequirement.CreateNew); } } private IPgQuestObjectiveRequirement _QuestObjectiveRequirement;
        public ItemKeyword Target { get { return GetEnum<ItemKeyword>(PropertiesOffset + 8); } }

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
            { "Requirements", new FieldParser() {
                Type = FieldType.Object,
                GetObject = () => QuestObjectiveRequirement as IObjectContentGenerator } },
            { "Target", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<ItemKeyword>.ToString(Target, null, ItemKeyword.Internal_None) } },
            { "ItemName", new FieldParser() {
                Type = FieldType.String,
                GetString = () => QuestItem != null ? QuestItem.InternalName : null } },
        }; } }
    }
}
