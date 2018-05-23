using PgJsonReader;
using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class DirectedGoal : GenericJsonObject<DirectedGoal>
    {
        #region Direct Properties
        public int Id { get { return RawId.HasValue ? RawId.Value : 0; } }
        private int? RawId;
        public string Label { get; private set; }
        public string Zone { get; private set; }
        public bool IsCategoryGate { get { return RawIsCategoryGate.HasValue ? RawIsCategoryGate.Value : false; } }
        public bool? RawIsCategoryGate { get; private set; }
        public string LargeHint { get; private set; }
        public string SmallHint { get; private set; }
        public int CategoryGateId { get { return RawCategoryGateId.HasValue ? RawCategoryGateId.Value : 0; } }
        public int? RawCategoryGateId { get; private set; }
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return Label; } }
        public const int SearchResultIconId = 2118;
        public string SearchResultIconFileName { get { return "icon_" + SearchResultIconId; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Id", new FieldParser() { Type = FieldType.Integer, ParserInteger = (int value, ParseErrorInfo errorInfo) => { RawId = value; }} },
            { "Label", new FieldParser() { Type = FieldType.String, ParserString = ParseLabel } },
            { "Zone", new FieldParser() { Type = FieldType.String, ParserString = (string value, ParseErrorInfo errorInfo) => { Zone = value; }} },
            { "IsCategoryGate", new FieldParser() { Type = FieldType.Bool, ParserBool = (bool value, ParseErrorInfo errorInfo) => { RawIsCategoryGate = value; }} },
            { "LargeHint", new FieldParser() { Type = FieldType.String, ParserString = (string value, ParseErrorInfo errorInfo) => { LargeHint = value; }} },
            { "SmallHint", new FieldParser() { Type = FieldType.String, ParserString = (string value, ParseErrorInfo errorInfo) => { SmallHint = value; }} },
            { "CategoryGateId", new FieldParser() { Type = FieldType.Integer, ParserInteger = (int value, ParseErrorInfo errorInfo) => { RawCategoryGateId = value; }} },
        }; } }

        private void ParseLabel(string RawLabel, ParseErrorInfo ErrorInfo)
        {
            Label = RawLabel;
            ErrorInfo.AddIconId(SearchResultIconId);
        }
        #endregion

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddString("Label", Label);
            Generator.AddString("Zone", Zone);

            Generator.CloseObject();
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, Label);
                AddWithFieldSeparator(ref Result, Zone);
                AddWithFieldSeparator(ref Result, LargeHint);
                AddWithFieldSeparator(ref Result, SmallHint);

                if (RawIsCategoryGate.HasValue && RawIsCategoryGate.Value)
                    AddWithFieldSeparator(ref Result, "Is Category Gate");

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = false;

            return IsConnected;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "DirectedGoal"; } }
        #endregion
    }
}
