using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestObjectiveDeliver : QuestObjective, IPgQuestObjectiveDeliver
    {
        #region Init
        public QuestObjectiveDeliver(QuestObjectiveType Type, string Description, int? RawNumber, bool? RawMustCompleteEarlierObjectivesFirst, MapAreaName DeliverNpcArea, string DeliverNpcId, string DeliverNpcName, string RawItemName, int? RawNumToDeliver)
            : base(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst)
        {
            this.DeliverNpcArea = DeliverNpcArea;
            this.DeliverNpcId = DeliverNpcId;
            this.DeliverNpcName = DeliverNpcName;
            this.RawItemName = RawItemName;
            this.RawNumToDeliver = RawNumToDeliver;
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
            { "ItemName", new FieldParser() {
                Type = FieldType.String,
                GetString = () => QuestItem != null ? QuestItem.InternalName : null } },
            { "NumToDeliver", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawNumToDeliver } },
        }; } }
        #endregion

        #region Properties
        public IPgGameNpc DeliverNpc { get; private set; }
        public IPgItem QuestItem { get; private set; }
        public int NumToDeliver { get { return RawNumToDeliver.HasValue ? RawNumToDeliver.Value : 0; } }
        public int? RawNumToDeliver { get; private set; }
        public string DeliverNpcId { get; private set; }
        public string DeliverNpcName { get; private set; }
        public MapAreaName DeliverNpcArea { get; private set; }

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
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IJsonKey>> AllTables)
        {
            bool IsConnected = base.ConnectFields(ErrorInfo, Parent, AllTables);
            Dictionary<string, IJsonKey> ItemTable = AllTables[typeof(Item)];
            Dictionary<string, IJsonKey> GameNpcTable = AllTables[typeof(GameNpc)];

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
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            SerializeJsonObjectInternalProlog(data, ref offset, StoredStringtable, StoredStringListTable);
            int BaseOffset = offset;

            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();

            AddObject(DeliverNpc as ISerializableJsonObject, data, ref offset, BaseOffset, 0, StoredObjectTable);
            AddObject(QuestItem as ISerializableJsonObject, data, ref offset, BaseOffset, 4, StoredObjectTable);
            AddInt(RawNumToDeliver, data, ref offset, BaseOffset, 8);
            AddString(DeliverNpcId, data, ref offset, BaseOffset, 12, StoredStringtable);
            AddString(DeliverNpcName, data, ref offset, BaseOffset, 16, StoredStringtable);
            AddEnum(DeliverNpcArea, data, ref offset, BaseOffset, 20);

            FinishSerializing(data, ref offset, BaseOffset, 22, StoredStringtable, StoredObjectTable, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
