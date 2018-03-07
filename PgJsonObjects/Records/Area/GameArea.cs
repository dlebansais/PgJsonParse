using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class GameArea : GenericJsonObject<GameArea>
    {
        #region Direct Properties
        public MapAreaName KeyArea { get; private set; }
        public string FriendlyName { get; private set; }
        public string ShortFriendlyName { get; private set; }
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return Key; } }
        public const int SearchResultIconId = 2118;
        public string SearchResultIconFileName { get { return "icon_" + SearchResultIconId; } }
        #endregion

        #region Parsing
        protected override void InitializeKey(KeyValuePair<string, object> EntryRaw, ParseErrorInfo ErrorInfo)
        {
            base.InitializeKey(EntryRaw, ErrorInfo);

            if (Key.StartsWith("Area"))
            {
                MapAreaName ParsedMapAreaName;
                StringToEnumConversion<MapAreaName>.TryParse(Key.Substring(4), TextMaps.MapAreaNameStringMap, out ParsedMapAreaName, ErrorInfo);
                KeyArea = ParsedMapAreaName;
            }
            else
                ErrorInfo.AddInvalidObjectFormat("Area Key");
        }

        protected override Dictionary<string, FieldValueHandler> FieldTable { get; } = new Dictionary<string, FieldValueHandler>()
        {
            { "FriendlyName", ParseFieldFriendlyName },
            { "ShortFriendlyName", ParseFieldShortFriendlyName },
        };

        private static void ParseFieldFriendlyName(GameArea This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "Area FriendlyName", This.ParseFriendlyName);
        }

        private void ParseFriendlyName(string RawFriendlyName, ParseErrorInfo ErrorInfo)
        {
            FriendlyName = RawFriendlyName;
            if (KeyArea != MapAreaName.Internal_None && FriendlyName != TextMaps.MapAreaNameTextMap[KeyArea])
                ErrorInfo.AddInvalidObjectFormat("Area FriendlyName");
        }

        private static void ParseFieldShortFriendlyName(GameArea This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "Area ShortFriendlyName", This.ParseShortFriendlyName);
        }

        private void ParseShortFriendlyName(string RawShortFriendlyName, ParseErrorInfo ErrorInfo)
        {
            ShortFriendlyName = RawShortFriendlyName;
        }
        #endregion

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddString("FriendlyName", FriendlyName);
            Generator.AddString("ShortFriendlyName", ShortFriendlyName);

            Generator.CloseObject();
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, FriendlyName);
                AddWithFieldSeparator(ref Result, ShortFriendlyName);

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
        protected override string FieldTableName { get { return "Area"; } }
        #endregion
    }
}
