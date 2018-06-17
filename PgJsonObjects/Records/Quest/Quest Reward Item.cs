using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestRewardItem : GenericJsonObject<QuestRewardItem>, IPgQuestRewardItem
    {
        #region Direct Properties
        public Item QuestItem { get; private set; }
        public int StackSize { get { return RawStackSize.HasValue ? RawStackSize.Value : 1; } }
        public int? RawStackSize { get; private set; }
        public bool HasStackSize { get { return RawStackSize.HasValue && RawStackSize.Value > 1; } }
        private string RawItem;
        private bool IsRawItemParsed;
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return null; } }
        public Quest ParentQuest { get; private set; }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Item", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => RawItem = value,
                GetString = () => RawItem } },
            { "StackSize", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = ParseStackSize,
                GetInteger = () => RawStackSize } },
        }; } }

        private void ParseStackSize(int value, ParseErrorInfo ErrorInfo)
        {
            if (value < 1)
                ErrorInfo.AddInvalidObjectFormat("QuestRewardItem StackSize");
            else
                RawStackSize = value;
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                if (QuestItem != null)
                    AddWithFieldSeparator(ref Result, QuestItem.Name);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = false;
            Dictionary<string, IGenericJsonObject> ItemTable = AllTables[typeof(Item)];

            ParentQuest = Parent as Quest;

            QuestItem = Item.ConnectSingleProperty(ErrorInfo, ItemTable, RawItem, QuestItem, ref IsRawItemParsed, ref IsConnected, this);

            return IsConnected;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "QuestRewardItem"; } }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();

            AddObject(QuestItem, data, ref offset, BaseOffset, 0, StoredObjectTable);
            AddInt(RawStackSize, data, ref offset, BaseOffset, 4);

            FinishSerializing(data, ref offset, BaseOffset, 8, null, StoredObjectTable, null, null, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
