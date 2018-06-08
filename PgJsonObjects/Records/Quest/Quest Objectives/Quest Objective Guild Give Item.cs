using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestObjectiveGuildGiveItem : QuestObjective
    {
        #region Init
        public QuestObjectiveGuildGiveItem(QuestObjectiveType Type, string Description, int? RawNumber, bool? RawMustCompleteEarlierObjectivesFirst, int? MinHour, int? MaxHour, string DeliverNpcId, string DeliverNpcName, string RawItemName)
            : base(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour)
        {
            this.DeliverNpcId = DeliverNpcId;
            this.DeliverNpcName = DeliverNpcName;
            this.RawItemName = RawItemName;
        }

        public QuestObjectiveGuildGiveItem(QuestObjectiveType Type, string Description, int? RawNumber, bool? RawMustCompleteEarlierObjectivesFirst, int? MinHour, int? MaxHour, string DeliverNpcId, string DeliverNpcName, ItemKeyword ItemKeyword)
            : base(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour)
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
                GetString = () => RawItemName } },
        }; } }
        #endregion

        #region Properties
        public Item QuestItem { get; private set; }
        private string RawItemName;
        private bool IsItemNameParsed;
        public string DeliverNpcId { get; private set; }
        public string DeliverNpcName { get; private set; }
        private bool IsDeliverNpcParsed;
        public GameNpc DeliverNpc { get; private set; }
        public ItemKeyword ItemKeyword { get; private set; }
        public List<Item> ItemList { get; private set; } = new List<Item>();
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
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = base.ConnectFields(ErrorInfo, Parent, AllTables);
            Dictionary<string, IGenericJsonObject> ItemTable = AllTables[typeof(Item)];
            Dictionary<string, IGenericJsonObject> GameNpcTable = AllTables[typeof(GameNpc)];

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
    }
}
