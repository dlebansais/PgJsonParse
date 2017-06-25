using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class XpTable : GenericJsonObject<XpTable>
    {
        #region Constants
        protected override string FieldTableName { get { return "XpTable"; } }

        protected override Dictionary<string, FieldValueHandler> FieldTable { get; } = new Dictionary<string, FieldValueHandler>()
        {
            { "InternalName", ParseFieldInternalName },
            { "XpAmounts", ParseFieldXpAmounts },
        };
        #endregion

        #region Properties
        public string InternalName { get; private set; }
        public XpTableEnum EnumName { get; private set; }
        public List<XpTableLevel> XpAmountList { get; } = new List<XpTableLevel>();
        private bool IsXpAmountListEmpty = true;

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
            XpTableEnum ParsedEnumName;
            InternalName = RawInternalName;

            StringToEnumConversion<XpTableEnum>.TryParse(RawInternalName, out ParsedEnumName, ErrorInfo);
            EnumName = ParsedEnumName;
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
            List<int> XpList = new List<int>();
            ParseIntTable(RawXpAmounts, XpList, "XpAmounts", ErrorInfo, out IsXpAmountListEmpty);

            int Level = 0;
            int TotalXp = 0;
            foreach (int Xp in XpList)
            {
                TotalXp += Xp;
                Level++;
                XpAmountList.Add(new XpTableLevel(Level, Xp, TotalXp));
            }
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
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable, Dictionary<string, XpTable> XpTableTable, Dictionary<string, AdvancementTable> AdvancementTableTable)
        {
            return false;
        }
        #endregion
    }
}
