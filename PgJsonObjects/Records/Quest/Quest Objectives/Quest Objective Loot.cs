using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestObjectiveLoot : QuestObjective, IPgQuestObjectiveLoot
    {
        #region Init
        public QuestObjectiveLoot(QuestObjectiveType Type, string Description, string RawItemName, int? RawNumber, MonsterTypeTag MonsterTypeTag, bool? RawMustCompleteEarlierObjectivesFirst, int? MinHour, int? MaxHour)
            : base(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour)
        {
            this.RawItemName = RawItemName;
            this.MonsterTypeTag = MonsterTypeTag;
        }

        public QuestObjectiveLoot(QuestObjectiveType Type, string Description, ItemKeyword ItemTarget, int? RawNumber, MonsterTypeTag MonsterTypeTag, bool? RawMustCompleteEarlierObjectivesFirst, int? MinHour, int? MaxHour)
            : base(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour)
        {
            this.ItemTarget = ItemTarget;
            this.MonsterTypeTag = MonsterTypeTag;
        }

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
                GetString = () => RawItemName } },
            { "MonsterTypeTag", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<MonsterTypeTag>.ToString(MonsterTypeTag, null, MonsterTypeTag.Internal_None) } },
        }; } }
        #endregion

        #region Properties
        public IPgItem QuestItem { get; private set; }
        public ItemCollection ItemList { get; private set; } = new ItemCollection();
        public ItemKeyword ItemTarget { get; private set; }
        public MonsterTypeTag MonsterTypeTag { get; private set; }
        public bool HasMonsterTypeTag { get { return MonsterTypeTag != MonsterTypeTag.Internal_None; } }

        private string RawItemName;
        private bool IsItemNameParsed;
        private bool IsTargetParsed;
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = base.TextContent;

                if (QuestItem != null)
                    AddWithFieldSeparator(ref Result, QuestItem.Name);
                if (ItemTarget != ItemKeyword.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.ItemKeywordTextMap[ItemTarget]);
                foreach (Item Item in ItemList)
                    AddWithFieldSeparator(ref Result, Item.Name);
                if (MonsterTypeTag != MonsterTypeTag.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.MonsterTypeTagTextMap[MonsterTypeTag]);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = base.ConnectFields(ErrorInfo, Parent, AllTables);
            Dictionary<string, IGenericJsonObject> ItemTable = AllTables[typeof(Item)];
            Dictionary<string, IGenericJsonObject> SkillTable = AllTables[typeof(Skill)];

            QuestItem = Item.ConnectSingleProperty(ErrorInfo, ItemTable, RawItemName, QuestItem, ref IsItemNameParsed, ref IsConnected, this);
            ItemList = PgJsonObjects.Item.ConnectByKeyword(ErrorInfo, ItemTable, ItemTarget, ItemList, ref IsTargetParsed, ref IsConnected, ParentQuest);

            return IsConnected;
        }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();
            Dictionary<int, ISerializableJsonObjectCollection> StoredObjectListTable = new Dictionary<int, ISerializableJsonObjectCollection>();

            AddString(Key, data, ref offset, BaseOffset, 0, StoredStringtable);
            AddObject(QuestItem as ISerializableJsonObject, data, ref offset, BaseOffset, 4, StoredObjectTable);
            AddObjectList(ItemList, data, ref offset, BaseOffset, 8, StoredObjectListTable);
            AddEnum(ItemTarget, data, ref offset, BaseOffset, 12);
            AddEnum(MonsterTypeTag, data, ref offset, BaseOffset, 14);
            AddStringList(FieldTableOrder, data, ref offset, BaseOffset, 16, StoredStringListTable);

            FinishSerializing(data, ref offset, BaseOffset, 20, StoredStringtable, StoredObjectTable, null, null, null, null, StoredStringListTable, StoredObjectListTable);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
