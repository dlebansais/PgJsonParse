using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestObjectiveScriptedReceiveItem : QuestObjective, IPgQuestObjectiveScriptedReceiveItem
    {
        #region Indexing
        public QuestObjectiveScriptedReceiveItem(QuestObjectiveType Type, string Description, int? RawNumber, bool? RawMustCompleteEarlierObjectivesFirst, int? MinHour, int? MaxHour, MapAreaName DeliverNpcArea, string DeliverNpcId, string DeliverNpcName, string RawItemName)
            : base(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour)
        {
            this.DeliverNpcArea = DeliverNpcArea;
            this.DeliverNpcId = DeliverNpcId;
            this.DeliverNpcName = DeliverNpcName;
            this.RawItemName = RawItemName;
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
                GetString = () => Quest.NpcToString(DeliverNpcArea, DeliverNpcId, DeliverNpcName, false) } },
            { "Item", new FieldParser() {
                Type = FieldType.String,
                GetString = () => RawItemName } },
        }; } }
        #endregion

        #region Properties
        public IPgGameNpc DeliverNpc { get; private set; }
        public IPgItem QuestItem { get; private set; }

        private MapAreaName DeliverNpcArea;
        private string DeliverNpcId;
        private string DeliverNpcName;
        private bool IsDeliverNpcParsed;
        private string RawItemName;
        private bool IsItemNameParsed;
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = base.TextContent;

                AddWithFieldSeparator(ref Result, TextMaps.MapAreaNameTextMap[DeliverNpcArea]);
                AddWithFieldSeparator(ref Result, DeliverNpcName);
                if (QuestItem != null)
                    AddWithFieldSeparator(ref Result, QuestItem.Name);

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

            QuestItem = Item.ConnectSingleProperty(ErrorInfo, ItemTable, RawItemName, QuestItem, ref IsItemNameParsed, ref IsConnected, this);
            DeliverNpc = GameNpc.ConnectByKey(ErrorInfo, GameNpcTable, DeliverNpcId, DeliverNpc, ref IsDeliverNpcParsed, ref IsConnected, this);
            if (DeliverNpcId != null && DeliverNpc == null)
            {
                SpecialNpc ParsedSpecialNpc;
                if (StringToEnumConversion<SpecialNpc>.TryParse(DeliverNpcId, out ParsedSpecialNpc, ErrorInfo))
                    DeliverNpcName = TextMaps.SpecialNpcTextMap[ParsedSpecialNpc];
            }

            return IsConnected;
        }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();

            AddObject(DeliverNpc as ISerializableJsonObject, data, ref offset, BaseOffset, 0, StoredObjectTable);
            AddObject(QuestItem as ISerializableJsonObject, data, ref offset, BaseOffset, 4, StoredObjectTable);

            FinishSerializing(data, ref offset, BaseOffset, 8, null, StoredObjectTable, null, null, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
