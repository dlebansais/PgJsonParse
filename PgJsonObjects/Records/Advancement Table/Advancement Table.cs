using PgJsonReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace PgJsonObjects
{
    public class AdvancementTable : MainJsonObject<AdvancementTable>, IPgAdvancementTable, INotifyPropertyChanged
    {
        #region Direct Properties
        public Dictionary<int, IPgAdvancement> LevelTable { get; private set; }
        #endregion

        #region Indirect Properties
        public override string SortingName { get { return InternalName; } }
        public const int SearchResultIconId = 2118;
        public string SearchResultIconFileName { get { return "icon_" + AdvancementTable.SearchResultIconId; } }
        public int Id { get { return PgAdvancementTable.KeyToId(Key); } }
        public string InternalName { get { return PgAdvancementTable.KeyToInternalName(Key); } }
        public string FriendlyName { get { return PgAdvancementTable.KeyToFriendlyName(Key); } }

        public bool HasManyLevels { get { return LevelTable.Count > 1; } }

        public int CurrentLevel
        {
            get
            {
                int Index = _CurrentLevelIndex;
                foreach (KeyValuePair<int, IPgAdvancement> Entry in LevelTable)
                    if (Index-- <= 0)
                        return Entry.Key;

                return 0;
            }
        }
        private static int _CurrentLevelIndex;

        public IPgAdvancement CurrentAdvancement
        {
            get
            {
                int Index = _CurrentLevelIndex;
                foreach (KeyValuePair<int, IPgAdvancement> Entry in LevelTable)
                    if (Index-- <= 0)
                        return Entry.Value;

                return null;
            }
        }
        #endregion

        #region Parsing
        public override void Init(string key, int index, IJsonValue value, bool loadAsArray, ParseErrorInfo ErrorInfo)
        {
            InitializeKey(key, index, value, ErrorInfo);

            LevelTable = new Dictionary<int, IPgAdvancement>();

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

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser>(); } }
        #endregion

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator, bool openWithKey, bool openWithNullKey)
        {
            Generator.OpenObject(Key);

            foreach (KeyValuePair<int, IPgAdvancement> Level in LevelTable)
                (Level.Value as Advancement).GenerateObjectContent(Generator, true, false);

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

                string Longest = "";
                foreach (KeyValuePair<int, IPgAdvancement> Entry in LevelTable)
                {
                    string Content = (Entry.Value as Advancement).TextContent;
                    if (Content != null && Longest.Length < Content.Length)
                        Longest = Content;
                }
                AddWithFieldSeparator(ref Result, Longest);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, IBackLinkable Parent, Dictionary<Type, Dictionary<string, IJsonKey>> AllTables)
        {
            if (FriendlyName == null)
                ErrorInfo.AddInvalidObjectFormat("Internal Name " + InternalName + " not recognized");

            return false;
        }

        public static IPgAdvancementTable ConnectSingleProperty(ParseErrorInfo ErrorInfo, Dictionary<string, IJsonKey> AdvancementTableTable, string RawAdvancementTableName, IPgAdvancementTable ParsedAdvancementTable, ref bool IsRawAdvancementTableParsed, ref bool IsConnected, IBackLinkable LinkBack)
        {
            if (IsRawAdvancementTableParsed)
                return ParsedAdvancementTable;

            IsRawAdvancementTableParsed = true;

            if (RawAdvancementTableName == null)
                return null;

            foreach (KeyValuePair<string, IJsonKey> Entry in AdvancementTableTable)
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
            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            FieldTableOrder.Clear();

            AddString(Key, data, ref offset, BaseOffset, 0, StoredStringtable);
            AddStringList(FieldTableOrder, data, ref offset, BaseOffset, 4, StoredStringListTable);

            int LevelOffset = 8;
            foreach (KeyValuePair<int, IPgAdvancement> Level in LevelTable)
            {
                FieldTableOrder.Add("level_" + Level.Key);
                AddObject(Level.Value as ISerializableJsonObject, data, ref offset, BaseOffset, LevelOffset, StoredObjectTable);
                LevelOffset += 4;
            }

            FinishSerializing(data, ref offset, BaseOffset, LevelOffset, StoredStringtable, StoredObjectTable, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion

        public void OnLevelChange(int change)
        {
            int NewIndex = _CurrentLevelIndex + change;

            if (NewIndex >= 0 && NewIndex < LevelTable.Count)
            {
                _CurrentLevelIndex = NewIndex;
                NotifyPropertyChanged(nameof(CurrentLevel));
                NotifyPropertyChanged(nameof(CurrentAdvancement));
            }
        }

        #region Implementation of INotifyPropertyChanged
        /// <summary>
        ///     Implements the PropertyChanged event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        internal void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "Default parameter is mandatory with [CallerMemberName]")]
        internal void NotifyThisPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
