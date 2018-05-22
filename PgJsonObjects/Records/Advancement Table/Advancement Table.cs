using PgJsonReader;
using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AdvancementTable : GenericJsonObject<AdvancementTable>
    {
        #region Direct Properties
        public Dictionary<int, Advancement> LevelTable { get; private set; }
        public string InternalName { get; private set; }
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return InternalName; } }
        #endregion

        #region Parsing
        public override void Init(KeyValuePair<string, IJsonValue> EntryRaw, ParseErrorInfo ErrorInfo)
        {
            InitializeKey(EntryRaw, ErrorInfo);

            LevelTable = new Dictionary<int, Advancement>();

            JsonObject AsJObject;
            Dictionary<string, JsonObject> Levels;
            if ((AsJObject = EntryRaw.Value as JsonObject) != null)
            {
                foreach (KeyValuePair<string, IJsonValue> Token in AsJObject)
                {
                    JsonObject AsSubObject;
                    if ((AsSubObject = Token.Value as JsonObject) != null)
                        Init(Token.Key, AsSubObject, ErrorInfo);
                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("AdvancementTable: " + Key);
                        break;
                    }
                }
            }

            else if ((Levels = EntryRaw.Value as Dictionary<string, JsonObject>) != null)
            {
                foreach (KeyValuePair<string, JsonObject> Level in Levels)
                    Init(Level.Key, Level.Value, ErrorInfo);
            }
            else
                ErrorInfo.AddInvalidObjectFormat("AdvancementTable: " + Key);
        }

        private void Init(string LevelKey, JsonObject LevelValue, ParseErrorInfo ErrorInfo)
        {
            if (LevelKey.StartsWith("Level_"))
            {
                int EntryLevel;
                if (int.TryParse(LevelKey.Substring(6), out EntryLevel))
                {
                    Advancement ParsedAdvancement;
                    JsonObjectParser<Advancement>.InitAsSubitem(LevelKey, LevelValue, out ParsedAdvancement, ErrorInfo);
                    LevelTable.Add(EntryLevel, ParsedAdvancement);
                }
                else
                    ErrorInfo.AddInvalidObjectFormat("AdvancementTable: " + Key + ", " + LevelKey);
            }
            else
                ErrorInfo.AddInvalidObjectFormat("AdvancementTable: " + Key + ", " + LevelKey);
        }

        protected override void InitializeKey(KeyValuePair<string, IJsonValue> EntryRaw, ParseErrorInfo ErrorInfo)
        {
            base.InitializeKey(EntryRaw, ErrorInfo);

            int Index = Key.LastIndexOf('_');
            if (Index >= 0)
                InternalName = Key.Substring(Index + 1);
            else
                InternalName = Key;
        }

        protected override Dictionary<string, FieldParser> FieldTable { get { return null; } }
        #endregion

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            foreach (KeyValuePair<int, Advancement> Level in LevelTable)
                Level.Value.GenerateObjectContent(Generator);

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

        public static AdvancementTable ConnectSingleProperty(ParseErrorInfo ErrorInfo, Dictionary<string, IGenericJsonObject> AdvancementTableTable, string RawAdvancementTableName, AdvancementTable ParsedAdvancementTable, ref bool IsRawAdvancementTableParsed, ref bool IsConnected, GenericJsonObject LinkBack)
        {
            if (IsRawAdvancementTableParsed)
                return ParsedAdvancementTable;

            IsRawAdvancementTableParsed = true;

            if (RawAdvancementTableName == null)
                return null;

            foreach (KeyValuePair<string, IGenericJsonObject> Entry in AdvancementTableTable)
            {
                AdvancementTable AdvancementTableValue = Entry.Value as AdvancementTable;
                if (AdvancementTableValue.InternalName == RawAdvancementTableName)
                {
                    IsConnected = true;
                    //Entry.Value.AddLinkBack(LinkBack);
                    return AdvancementTableValue;
                }
            }

            if (ErrorInfo != null)
                ErrorInfo.AddMissingKey(RawAdvancementTableName);

            return null;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "AdvancementTable"; } }
        #endregion
    }
}
