using PgJsonReader;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class XpTable : GenericJsonObject<XpTable>
    {
        #region Direct Properties
        public string InternalName { get; private set; }
        public XpTableEnum EnumName { get; private set; }
        public List<XpTableLevel> XpAmountList { get; } = new List<XpTableLevel>();
        private int TotalXp = 0;
        private int Level = 0;
        private bool IsXpAmountListEmpty = true;
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return InternalName; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get; } = new Dictionary<string, FieldParser>()
        {
            { "InternalName", ParseFieldInternalName },
            { "XpAmounts", ParseFieldXpAmounts },
        };

        private static void ParseFieldInternalName(XpTable This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "XpTable InternalName", This.ParseInternalName);
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
            ParseFieldValueIntegerArray(Value, ErrorInfo, "XpTable XpAmounts", This.ParseXpAmounts);
        }

        private bool ParseXpAmounts(long RawXpAmount, ParseErrorInfo ErrorInfo)
        {
            TotalXp += (int)RawXpAmount;
            Level++;
            XpAmountList.Add(new XpTableLevel(Level, (int)RawXpAmount, TotalXp));
            return true;
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
            get { return ""; }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            return false;
        }

        public static XpTable ConnectSingleProperty(ParseErrorInfo ErrorInfo, Dictionary<string, IGenericJsonObject> XpTableTable, string RawXpTableName, XpTable ParsedXpTable, ref bool IsRawXpTableParsed, ref bool IsConnected, GenericJsonObject LinkBack)
        {
            if (IsRawXpTableParsed)
                return ParsedXpTable;

            IsRawXpTableParsed = true;

            if (RawXpTableName == null)
                return null;

            foreach (KeyValuePair<string, IGenericJsonObject> Entry in XpTableTable)
            {
                XpTable XpTableValue = Entry.Value as XpTable;
                if (XpTableValue.InternalName == RawXpTableName)
                {
                    IsConnected = true;
                    return XpTableValue;
                }
            }

            ErrorInfo.AddMissingKey(RawXpTableName);
            return null;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "XpTable"; } }
        #endregion
    }
}
