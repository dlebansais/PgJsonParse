using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class LoreBookInfo : GenericJsonObject<LoreBookInfo>
    {
        #region Direct Properties
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return ""; } }
        public const int SearchResultIconId = 5792;
        public string SearchResultIconFileName { get { return "icon_" + SearchResultIconId; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldValueHandler> FieldTable {  get; } = new Dictionary<string, FieldValueHandler>()
        {
            { "Gods", ParseFieldGods },
            { "Misc", ParseFieldMisc },
            { "History", ParseFieldHistory },
            { "Plot", ParseFieldPlot },
            { "Stories", ParseFieldStories },
            { "GuideProgram", ParseFieldGuideProgram },
        };

        private static void ParseFieldGods(LoreBookInfo This, object Value, ParseErrorInfo ErrorInfo)
        {
        }

        private static void ParseFieldMisc(LoreBookInfo This, object Value, ParseErrorInfo ErrorInfo)
        {
        }

        private static void ParseFieldHistory(LoreBookInfo This, object Value, ParseErrorInfo ErrorInfo)
        {
        }

        private static void ParseFieldPlot(LoreBookInfo This, object Value, ParseErrorInfo ErrorInfo)
        {
        }

        private static void ParseFieldStories(LoreBookInfo This, object Value, ParseErrorInfo ErrorInfo)
        {
        }

        private static void ParseFieldGuideProgram(LoreBookInfo This, object Value, ParseErrorInfo ErrorInfo)
        {
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
        protected override string FieldTableName { get { return "LoreBookInfo"; } }
        #endregion
    }
}
