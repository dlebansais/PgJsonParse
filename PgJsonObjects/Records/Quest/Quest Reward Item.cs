using PgJsonReader;
using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestRewardItem : GenericJsonObject<QuestRewardItem>
    {
        #region Direct Properties
        public Item QuestItem { get; private set; }
        private string RawItem;
        private bool IsRawItemParsed;
        public int StackSize { get { return RawStackSize.HasValue ? RawStackSize.Value : 1; } }
        public int? RawStackSize { get; private set; }
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
            if (value > 1)
                RawStackSize = value;
            else if (value < 1)
                ErrorInfo.AddInvalidObjectFormat("QuestRewardItem StackSize");
        }
        #endregion

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.CloseObject();
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
    }
}
