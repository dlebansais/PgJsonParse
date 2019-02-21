using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestObjectiveLoot : QuestObjective, IPgQuestObjectiveLoot
    {
        #region Init
        public QuestObjectiveLoot(QuestObjectiveType Type, string Description, string RawItemName, int? RawNumber, MonsterTypeTag MonsterTypeTag, bool? RawMustCompleteEarlierObjectivesFirst)
            : base(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst)
        {
            this.RawItemName = RawItemName;
            this.MonsterTypeTag = MonsterTypeTag;
        }

        public QuestObjectiveLoot(QuestObjectiveType Type, string Description, ItemKeyword ItemTarget, int? RawNumber, MonsterTypeTag MonsterTypeTag, bool? RawMustCompleteEarlierObjectivesFirst)
            : base(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst)
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
                GetString = () => StringToEnumConversion<ItemKeyword>.ToString(ItemTarget, TextMaps.ItemKeywordStringMap, ItemKeyword.Internal_None) } },
            { "ItemName", new FieldParser() {
                Type = FieldType.String,
                GetString = () => QuestItem != null ? QuestItem.InternalName : null } },
            { "MonsterTypeTag", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<MonsterTypeTag>.ToString(MonsterTypeTag, null, MonsterTypeTag.Internal_None) } },
        }; } }
        #endregion

        #region Properties
        public IPgItem QuestItem { get; private set; }
        public IPgItemCollection ItemList { get; private set; } = new ItemCollection();
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
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, IBackLinkable Parent, Dictionary<Type, Dictionary<string, IJsonKey>> AllTables)
        {
            bool IsConnected = base.ConnectFields(ErrorInfo, Parent, AllTables);
            Dictionary<string, IJsonKey> ItemTable = AllTables[typeof(Item)];
            Dictionary<string, IJsonKey> SkillTable = AllTables[typeof(Skill)];

            QuestItem = Item.ConnectSingleProperty(ErrorInfo, ItemTable, RawItemName, QuestItem, ref IsItemNameParsed, ref IsConnected, Parent);
            ItemList = PgJsonObjects.Item.ConnectByKeyword(ErrorInfo, ItemTable, ItemTarget, ItemList, ref IsTargetParsed, ref IsConnected, Parent);

            return IsConnected;
        }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            SerializeJsonObjectInternalProlog(data, ref offset, StoredStringtable, StoredStringListTable);
            int BaseOffset = offset;

            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();
            Dictionary<int, IPgCollection> StoredObjectListTable = new Dictionary<int, IPgCollection>();

            AddObject(QuestItem as ISerializableJsonObject, data, ref offset, BaseOffset, 0, StoredObjectTable);
            AddObjectList(ItemList, data, ref offset, BaseOffset, 4, StoredObjectListTable);
            AddEnum(ItemTarget, data, ref offset, BaseOffset, 8);
            AddEnum(MonsterTypeTag, data, ref offset, BaseOffset, 10);

            FinishSerializing(data, ref offset, BaseOffset, 12, StoredStringtable, StoredObjectTable, null, null, null, null, StoredStringListTable, StoredObjectListTable);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
