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
        protected override Dictionary<string, FieldValueHandler> FieldTable { get; } = new Dictionary<string, FieldValueHandler>()
        {
            { "Item", ParseFieldItem },
            { "StackSize", ParseFieldStackSize },
        };

        private static void ParseFieldItem(QuestRewardItem This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawItem;
            if ((RawItem = Value as string) != null)
                This.ParseItem(RawItem, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestRewardItem Item");
        }

        private void ParseItem(string RawItem, ParseErrorInfo ErrorInfo)
        {
            this.RawItem = RawItem;
        }

        private static void ParseFieldStackSize(QuestRewardItem This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseStackSize((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestRewardItem StackSize");
        }

        private void ParseStackSize(int RawStackSize, ParseErrorInfo ErrorInfo)
        {
            if (RawStackSize > 1)
                this.RawStackSize = RawStackSize;
            else if (RawStackSize < 1)
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
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable, Dictionary<string, XpTable> XpTableTable, Dictionary<string, AdvancementTable> AdvancementTableTable)
        {
            bool IsConnected = false;

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
