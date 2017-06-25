using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class XpTable : GenericJsonObject<XpTable>
    {
        #region Constants
        private Dictionary<string, FieldValueHandler> _FieldTable = new Dictionary<string, FieldValueHandler>()
        {
            { "InternalName", ParseFieldInternalName },
            { "XpAmounts", ParseFieldXpAmounts },
        };
        #endregion

        #region Properties
        public string InternalName { get; private set; }
        public List<int> XpAmountList { get; private set; }
        private bool IsXpAmountListEmpty;

        protected override string SortingName { get { return InternalName; } }
        #endregion

        #region Client Interface
        private static void ParseFieldInternalName(XpTable This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawInternalName;
            if ((RawInternalName = Value as string) != null)
                This.ParseInternalName(RawInternalName, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("XpTable InternalName");
        }

        private void ParseInternalName(string RawInternalName, ParseErrorInfo ErrorInfo)
        {
            InternalName = RawInternalName;
        }

        private static void ParseFieldXpAmounts(XpTable This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawXpAmounts;
            if ((RawXpAmounts = Value as ArrayList) != null)
                This.ParseXpAmounts(RawXpAmounts, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("XpTable XpAmounts");
        }

        private void ParseXpAmounts(ArrayList RawXpAmounts, ParseErrorInfo ErrorInfo)
        {
            ParseIntTable(RawXpAmounts, XpAmountList, "XpAmounts", ErrorInfo, out IsXpAmountListEmpty);
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

                return Result;
            }
        }

        public static XpTable ConnectSingleProperty(ParseErrorInfo ErrorInfo, Dictionary<string, XpTable> XpTableTable, string RawXpTableName, XpTable ParsedXpTable, ref bool IsRawXpTableParsed, ref bool IsConnected, GenericJsonObject LinkBack)
        {
            if (IsRawXpTableParsed)
                return ParsedXpTable;

            IsRawXpTableParsed = true;

            if (RawXpTableName == null)
                return null;

            foreach (KeyValuePair<string, XpTable> Entry in XpTableTable)
                if (Entry.Value.InternalName == RawXpTableName)
                {
                    IsConnected = true;
                    //Entry.Value.AddLinkBack(LinkBack);
                    return Entry.Value;
                }

            ErrorInfo.AddMissingKey(RawXpTableName);
            return null;
        }
        #endregion

        #region Ancestor Interface
        protected override Dictionary<string, FieldValueHandler> FieldTable { get { return _FieldTable; } }
        protected override string FieldTableName { get { return "XpTable"; } }

        protected override void InitializeFields()
        {
            XpAmountList = new List<int>();
            IsXpAmountListEmpty = true;
        }

        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable, Dictionary<string, XpTable> XpTableTable, Dictionary<string, AdvancementTable> AdvancementTableTable)
        {
            return false;
        }
        #endregion
    }
}
