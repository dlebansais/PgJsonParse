using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestObjectiveLoot : PgQuestObjective<PgQuestObjectiveLoot>, IPgQuestObjectiveLoot
    {
        public PgQuestObjectiveLoot(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveLoot CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestObjectiveLoot CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestObjectiveLoot(data, ref offset);
        }

        public IPgItem QuestItem { get { return GetObject(PropertiesOffset + 0, ref _QuestItem, PgItem.CreateNew); } } private IPgItem _QuestItem;
        public IPgItemCollection ItemList { get { return GetObjectList(PropertiesOffset + 4, ref _ItemList, PgItemCollection.CreateItem, () => new PgItemCollection()); } } private IPgItemCollection _ItemList;
        public ItemKeyword ItemTarget { get { return GetEnum<ItemKeyword>(PropertiesOffset + 8); } }
        public MonsterTypeTag MonsterTypeTag { get { return GetEnum<MonsterTypeTag>(PropertiesOffset + 10); } }

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
                GetString = () => StringToEnumConversion<ItemKeyword>.ToString(ItemTarget, null, ItemKeyword.Internal_None) } },
            { "ItemName", new FieldParser() {
                Type = FieldType.String,
                GetString = () => QuestItem != null ? QuestItem.InternalName : null } },
            { "MonsterTypeTag", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<MonsterTypeTag>.ToString(MonsterTypeTag, null, MonsterTypeTag.Internal_None) } },
        }; } }
    }
}
