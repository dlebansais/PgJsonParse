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
        protected override Dictionary<string, FieldValueHandler> FieldTable {  get; } = new Dictionary<string, FieldValueHandler>()
        {
            { "Id", ParseFieldId },
            { "Label", ParseFieldLabel },
            { "Zone", ParseFieldZone },
            { "IsCategoryGate", ParseFieldIsCategoryGate },
            { "LargeHint", ParseFieldLargeHint },
            { "SmallHint", ParseFieldSmallHint },
            { "CategoryGateId", ParseFieldCategoryGateId },
        };

        private static void ParseFieldId(DirectedGoal This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueInteger(Value, ErrorInfo, "DirectedGoal Id", This.ParseId);
        }

        private void ParseId(long RawId, ParseErrorInfo ErrorInfo)
        {
            this.RawId = (int)RawId;
        }

        private static void ParseFieldLabel(DirectedGoal This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "DirectedGoal Label", This.ParseLabel);
        }

        private void ParseLabel(string RawLabel, ParseErrorInfo ErrorInfo)
        {
            Label = RawLabel;
            ErrorInfo.AddIconId(SearchResultIconId);
        }

        private static void ParseFieldZone(DirectedGoal This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "DirectedGoal Zone", This.ParseZone);
        }

        private void ParseZone(string RawZone, ParseErrorInfo ErrorInfo)
        {
            Zone = RawZone;
        }

        private static void ParseFieldIsCategoryGate(DirectedGoal This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueBool(Value, ErrorInfo, "DirectedGoal IsCategoryGate", This.ParseIsCategoryGate);
        }

        private void ParseIsCategoryGate(bool RawIsCategoryGate, ParseErrorInfo ErrorInfo)
        {
            this.RawIsCategoryGate = RawIsCategoryGate;
        }

        private static void ParseFieldLargeHint(DirectedGoal This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "DirectedGoal LargeHint", This.ParseLargeHint);
        }

        private void ParseLargeHint(string RawLargeHint, ParseErrorInfo ErrorInfo)
        {
            LargeHint = RawLargeHint;
        }

        private static void ParseFieldSmallHint(DirectedGoal This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "DirectedGoal SmallHint", This.ParseSmallHint);
        }

        private void ParseSmallHint(string RawSmallHint, ParseErrorInfo ErrorInfo)
        {
            SmallHint = RawSmallHint;
        }

        private static void ParseFieldCategoryGateId(DirectedGoal This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueInteger(Value, ErrorInfo, "DirectedGoal CategoryGateId", This.ParseCategoryGateId);
        }

        private void ParseCategoryGateId(long RawCategoryGateId, ParseErrorInfo ErrorInfo)
        {
            this.RawCategoryGateId = (int)RawCategoryGateId;
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
