using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestObjectiveItem : QuestObjective, IPgQuestObjectiveItem
    {
        #region Init
        public QuestObjectiveItem(QuestObjectiveType Type, string Description, int? RawNumber, bool? RawMustCompleteEarlierObjectivesFirst, int? MinHour, int? MaxHour, string RawItemName, QuestObjectiveRequirement QuestObjectiveRequirement)
            : base(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour)
        {
            this.RawItemName = RawItemName;
            this.QuestObjectiveRequirement = QuestObjectiveRequirement;
        }

        public QuestObjectiveItem(QuestObjectiveType Type, string Description, int? RawNumber, bool? RawMustCompleteEarlierObjectivesFirst, int? MinHour, int? MaxHour, ItemKeyword Target, QuestObjectiveRequirement QuestObjectiveRequirement)
            : base(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour)
        {
            this.Target = Target;
            this.QuestObjectiveRequirement = QuestObjectiveRequirement;
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
            { "Requirements", new FieldParser() {
                Type = FieldType.Object,
                GetObject = () => QuestObjectiveRequirement } },
            { "Target", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<ItemKeyword>.ToString(Target, null, ItemKeyword.Internal_None) } },
            { "ItemName", new FieldParser() {
                Type = FieldType.String,
                GetString = () => RawItemName } },
        }; } }
        #endregion

        #region Properties
        public IPgItem QuestItem { get; private set; }
        public ItemCollection TargetItemList { get; private set; } = new ItemCollection();
        public ItemKeyword Target { get; private set; }
        private bool IsTargetParsed;
        private string RawItemName;
        private bool IsItemNameParsed;
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = base.TextContent;

                if (QuestItem != null)
                    AddWithFieldSeparator(ref Result, QuestItem.Name);
                if (Target != ItemKeyword.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.ItemKeywordTextMap[Target]);
                foreach (Item Item in TargetItemList)
                    AddWithFieldSeparator(ref Result, Item.Name);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = base.ConnectFields(ErrorInfo, Parent, AllTables);
            Dictionary<string, IGenericJsonObject> ItemTable = AllTables[typeof(Item)];

            QuestItem = Item.ConnectSingleProperty(ErrorInfo, ItemTable, RawItemName, QuestItem, ref IsItemNameParsed, ref IsConnected, this);
            TargetItemList = PgJsonObjects.Item.ConnectByKeyword(ErrorInfo, ItemTable, Target, TargetItemList, ref IsTargetParsed, ref IsConnected, ParentQuest);

            return IsConnected;
        }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();
            Dictionary<int, ISerializableJsonObjectCollection> StoredObjectListTable = new Dictionary<int, ISerializableJsonObjectCollection>();

            AddObject(QuestItem as ISerializableJsonObject, data, ref offset, BaseOffset, 0, StoredObjectTable);
            AddObjectList(TargetItemList, data, ref offset, BaseOffset, 4, StoredObjectListTable);
            AddEnum(Target, data, ref offset, BaseOffset, 8);

            FinishSerializing(data, ref offset, BaseOffset, 10, null, StoredObjectTable, null, null, null, null, null, StoredObjectListTable);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
