using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestRewardItem : GenericJsonObject<QuestRewardItem>
    {
        #region Constants
        protected override string FieldTableName { get { return "QuestRewardItem"; } }

        protected override Dictionary<string, FieldValueHandler> FieldTable { get; } = new Dictionary<string, FieldValueHandler>()
        {
            { "Item", ParseFieldItem },
            { "StackSize", ParseFieldStackSize },
        };
        #endregion

        #region Properties
        public Item QuestItem { get; private set; }
        private string RawItem;
        private bool IsRawItemParsed;
        public int StackSize { get { return RawStackSize.HasValue ? RawStackSize.Value : 0; } }
        private int? RawStackSize;
        public bool HasStackSize { get { return RawStackSize.HasValue && RawStackSize.Value > 1; } }

        protected override string SortingName { get { return null; } }
        public Quest ParentQuest { get; private set; }

        public string CombinedName
        {
            get
            {
                if (StackSize > 1)
                    return QuestItem.Name + " x" + StackSize;
                else
                    return QuestItem.Name;
            }
        }
        #endregion

        #region Client Interface
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
            this.RawStackSize = RawStackSize;
        }

        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.CloseObject();
        }

        public override string TextContent
        {
            get
            {
                string Result = "";

                if (QuestItem != null)
                    Result += QuestItem.TextContent + JsonGenerator.FieldSeparator;

                return Result;
            }
        }
        #endregion

        #region Ancestor Interface
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable, Dictionary<string, XpTable> XpTableTable, Dictionary<string, AdvancementTable> AdvancementTableTable)
        {
            bool IsConnected = false;

            ParentQuest = Parent as Quest;

            QuestItem = Item.ConnectSingleProperty(ErrorInfo, ItemTable, RawItem, QuestItem, ref IsRawItemParsed, ref IsConnected, this);

            return IsConnected;
        }
        #endregion
    }
}
