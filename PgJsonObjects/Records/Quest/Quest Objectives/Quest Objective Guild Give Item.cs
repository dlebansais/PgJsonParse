using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestObjectiveGuildGiveItem : QuestObjective, IPgQuestObjectiveGuildGiveItem
    {
        #region Init
        public QuestObjectiveGuildGiveItem(QuestObjectiveType Type, string Description, int? RawNumber, bool? RawMustCompleteEarlierObjectivesFirst, string DeliverNpcId, string DeliverNpcName, string RawItemName)
            : base(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst)
        {
            this.DeliverNpcId = DeliverNpcId;
            this.DeliverNpcName = DeliverNpcName;
            this.RawItemName = RawItemName;
        }

        public QuestObjectiveGuildGiveItem(QuestObjectiveType Type, string Description, int? RawNumber, bool? RawMustCompleteEarlierObjectivesFirst, string DeliverNpcId, string DeliverNpcName, ItemKeyword ItemKeyword)
            : base(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst)
        {
            this.DeliverNpcId = DeliverNpcId;
            this.DeliverNpcName = DeliverNpcName;
            this.ItemKeyword = ItemKeyword;
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
                GetString = () => Quest.NpcToString(DeliverNpcId, DeliverNpcName) } },
            { "ItemKeyword", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<ItemKeyword>.ToString(ItemKeyword, null, ItemKeyword.Internal_None) } },
            { "ItemName", new FieldParser() {
                Type = FieldType.String,
                GetString = () => QuestItem != null ? QuestItem.InternalName : null } },
        }; } }
        #endregion

        #region Properties
        public IPgItem QuestItem { get; private set; }
        public IPgGameNpc DeliverNpc { get; private set; }
        public ItemKeyword ItemKeyword { get; private set; }
        public IPgItemCollection ItemList { get; private set; } = new ItemCollection();
        public string DeliverNpcId { get; private set; }
        public string DeliverNpcName { get; private set; }

        private bool IsTargetParsed;
        private string RawItemName;
        private bool IsItemNameParsed;
        private bool IsDeliverNpcParsed;
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = base.TextContent;

                if (QuestItem != null)
                    AddWithFieldSeparator(ref Result, QuestItem.Name);
                AddWithFieldSeparator(ref Result, DeliverNpcName);
                if (ItemKeyword != ItemKeyword.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.ItemKeywordTextMap[ItemKeyword]);
                foreach (Item Item in ItemList)
                    AddWithFieldSeparator(ref Result, Item.Name);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IJsonKey>> AllTables)
        {
            bool IsConnected = base.ConnectFields(ErrorInfo, Parent, AllTables);
            Dictionary<string, IJsonKey> ItemTable = AllTables[typeof(Item)];
            Dictionary<string, IJsonKey> GameNpcTable = AllTables[typeof(GameNpc)];

            DeliverNpc = GameNpc.ConnectByKey(ErrorInfo, GameNpcTable, DeliverNpcId, DeliverNpc, ref IsDeliverNpcParsed, ref IsConnected, this);
            if (DeliverNpcId != null && DeliverNpc == null)
            {
                SpecialNpc ParsedSpecialNpc;
                if (StringToEnumConversion<SpecialNpc>.TryParse(DeliverNpcId, out ParsedSpecialNpc, ErrorInfo))
                    DeliverNpcName = TextMaps.SpecialNpcTextMap[ParsedSpecialNpc];
            }

            QuestItem = Item.ConnectSingleProperty(ErrorInfo, ItemTable, RawItemName, QuestItem, ref IsItemNameParsed, ref IsConnected, this);
            ItemList = PgJsonObjects.Item.ConnectByKeyword(ErrorInfo, ItemTable, ItemKeyword, ItemList, ref IsTargetParsed, ref IsConnected, ParentQuest);
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
            AddObject(DeliverNpc as ISerializableJsonObject, data, ref offset, BaseOffset, 4, StoredObjectTable);
            AddObjectList(ItemList, data, ref offset, BaseOffset, 8, StoredObjectListTable);
            AddString(DeliverNpcId, data, ref offset, BaseOffset, 12, StoredStringtable);
            AddString(DeliverNpcName, data, ref offset, BaseOffset, 16, StoredStringtable);
            AddEnum(ItemKeyword, data, ref offset, BaseOffset, 20);

            FinishSerializing(data, ref offset, BaseOffset, 22, StoredStringtable, StoredObjectTable, null, null, null, null, StoredStringListTable, StoredObjectListTable);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
