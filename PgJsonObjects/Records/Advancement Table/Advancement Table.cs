using PgJsonReader;
using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AdvancementTable : MainJsonObject<AdvancementTable>, IPgAdvancementTable
    {
        #region Direct Properties
        public Dictionary<int, Advancement> LevelTable { get; private set; }
        public string InternalName { get; private set; }
        #endregion

        #region Indirect Properties
        public override string SortingName { get { return InternalName; } }
        #endregion

        #region Parsing
        public override void Init(string key, int index, IJsonValue value, bool loadAsArray, ParseErrorInfo ErrorInfo)
        {
            InitializeKey(key, index, value, ErrorInfo);

            LevelTable = new Dictionary<int, Advancement>();

            JsonObject AsJObject;
            Dictionary<string, JsonObject> Levels;
            if ((AsJObject = value as JsonObject) != null)
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

            else if ((Levels = value as Dictionary<string, JsonObject>) != null)
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

        protected override void InitializeKey(string key, int index, IJsonValue value, ParseErrorInfo ErrorInfo)
        {
            base.InitializeKey(key, index, value, ErrorInfo);

            int Index = Key.LastIndexOf('_');
            if (Index >= 0)
                InternalName = Key.Substring(Index + 1);
            else
                InternalName = Key;
        }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser>(); } }
        #endregion

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator, bool openWithKey, bool openWithNullKey)
        {
            Generator.OpenObject(Key);

            foreach (KeyValuePair<int, Advancement> Level in LevelTable)
                Level.Value.GenerateObjectContent(Generator, true, false);

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

        public static IPgAdvancementTable ConnectSingleProperty(ParseErrorInfo ErrorInfo, Dictionary<string, IGenericJsonObject> AdvancementTableTable, string RawAdvancementTableName, IPgAdvancementTable ParsedAdvancementTable, ref bool IsRawAdvancementTableParsed, ref bool IsConnected, IBackLinkable LinkBack)
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

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();

            AddString(InternalName, data, ref offset, BaseOffset, 0, StoredStringtable);

            FinishSerializing(data, ref offset, BaseOffset, 4, StoredStringtable, null, null, null, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
