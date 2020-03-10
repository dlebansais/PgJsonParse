﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class Effect : MainJsonObject<Effect>, IPgEffect
    {
        #region Direct Properties
        public string Name { get; private set; }
        public string Desc { get; private set; }
        public int IconId { get { return RawIconId.HasValue ? RawIconId.Value : 0; } }
        public int? RawIconId { get; private set; }
        public string SpewText { get; private set; }
        public EffectStackingType StackingType { get; private set; }
        public EffectDisplayMode DisplayMode { get; private set; }
        public int StackingPriority { get { return RawStackingPriority.HasValue ? RawStackingPriority.Value : 0; } }
        public int? RawStackingPriority { get; private set; }
        public int Duration { get { return RawDuration.HasValue ? RawDuration.Value : 0; } }
        public int? RawDuration { get; private set; }
        public List<EffectKeyword> KeywordList { get; } = new List<EffectKeyword>();
        public List<AbilityKeyword> AbilityKeywordList { get; } = new List<AbilityKeyword>();
        public EffectParticle Particle { get; private set; }
        public int TSysKeywordIndex { get { return RawTSysKeywordIndex.HasValue ? RawTSysKeywordIndex.Value : 0; } }
        public int? RawTSysKeywordIndex { get; private set; }
        public bool IsKeywordListEmpty { get; private set; }
        public bool IsAbilityKeywordListEmpty { get; private set; }
        #endregion

        #region Indirect Properties
        public override string SortingName { get { return Name; } }
        public string SearchResultIconFileName { get { return RawIconId.HasValue && RawIconId.Value > 0 ? "icon_" + RawIconId.Value : null; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Name", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Name = value,
                GetString = () => Name } },
            { "Desc", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Desc = value,
                GetString = () => Desc } },
            { "IconId", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = ParseIconId,
                GetInteger = () => RawIconId } },
            { "DisplayMode", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => DisplayMode = StringToEnumConversion<EffectDisplayMode>.Parse(value, errorInfo),
                GetString = () => StringToEnumConversion<EffectDisplayMode>.ToString(DisplayMode, null, EffectDisplayMode.Internal_None) } },
            { "SpewText", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => SpewText = value,
                GetString = () => SpewText } },
            { "Particle", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Particle = StringToEnumConversion<EffectParticle>.Parse(value, TextMaps.EffectParticleStringMap, errorInfo),
                GetString = () => StringToEnumConversion<EffectParticle>.ToString(Particle, TextMaps.EffectParticleStringMap, EffectParticle.Internal_None) } },
            { "StackingType", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => StackingType = StringToEnumConversion<EffectStackingType>.Parse(value, TextMaps.StackingTypeStringMap, errorInfo),
                GetString = () => StringToEnumConversion<EffectStackingType>.ToString(StackingType, TextMaps.StackingTypeStringMap, EffectStackingType.Internal_None) } },
            { "StackingPriority", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawStackingPriority = value,
                GetInteger = () => RawStackingPriority } },
            { "Duration", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawDuration = value,
                GetInteger = () => RawDuration } },
            { "Keywords", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = ParseKeywords,
                SetArrayIsEmpty = () => IsKeywordListEmpty = true,
                GetStringArray = GetKeywords,
                GetArrayIsEmpty = () => IsKeywordListEmpty } },
            { "AbilityKeywords", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => StringToEnumConversion<AbilityKeyword>.ParseList(value, AbilityKeywordList, errorInfo),
                SetArrayIsEmpty = () => IsAbilityKeywordListEmpty = true,
                GetStringArray = () => StringToEnumConversion<AbilityKeyword>.ToStringList(AbilityKeywordList),
                GetArrayIsEmpty = () => IsAbilityKeywordListEmpty } },
        }; } }

        private void ParseIconId(int value, ParseErrorInfo ErrorInfo)
        {
            RawIconId = value;

            if (value > 0)
                ErrorInfo.AddIconId(value);
        }

        private void ParseKeywords(string value, ParseErrorInfo ErrorInfo)
        {
            if (StringToEnumConversion<EffectKeyword>.TryParse(value, TextMaps.KeywordStringMap, out EffectKeyword ParsedEffectKeyword, ErrorInfo))
                if (ParsedEffectKeyword != EffectKeyword.TSys)
                    KeywordList.Add(ParsedEffectKeyword);
                else
                    RawTSysKeywordIndex = KeywordList.Count;
        }

        private List<string> GetKeywords()
        {
            List<string> Result = StringToEnumConversion<EffectKeyword>.ToStringList(KeywordList, TextMaps.KeywordStringMap);

            if (RawTSysKeywordIndex.HasValue)
                Result.Insert(RawTSysKeywordIndex.Value, StringToEnumConversion<EffectKeyword>.ToString(EffectKeyword.TSys, TextMaps.KeywordStringMap));

            return Result;
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, Name);
                AddWithFieldSeparator(ref Result, Desc);
                if (DisplayMode != EffectDisplayMode.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.EffectDisplayModeTextMap[DisplayMode]);
                AddWithFieldSeparator(ref Result, SpewText);
                if (Particle != EffectParticle.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.EffectParticleTextMap[Particle]);
                if (StackingType != EffectStackingType.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.EffectStackingTypeTextMap[StackingType]);
                foreach(EffectKeyword Keyword in KeywordList)
                    AddWithFieldSeparator(ref Result, TextMaps.EffectKeywordTextMap[Keyword]);
                foreach (AbilityKeyword Keyword in AbilityKeywordList)
                    AddWithFieldSeparator(ref Result, TextMaps.AbilityKeywordTextMap[Keyword]);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, IBackLinkable Parent, Dictionary<Type, Dictionary<string, IJsonKey>> AllTables)
        {
            return false;
        }

        public static IPgEffect ConnectSingleProperty(ParseErrorInfo ErrorInfo, Dictionary<string, IJsonKey> EffectTable, string RawEffectName, IPgEffect ParsedEffect, ref bool IsRawEffectParsed, ref bool IsConnected, IBackLinkable LinkBack)
        {
            if (IsRawEffectParsed)
                return ParsedEffect;

            IsRawEffectParsed = true;

            if (RawEffectName == null)
                return null;

            foreach (KeyValuePair<string, IJsonKey> Entry in EffectTable)
            {
                Effect EffectValue = Entry.Value as Effect;
                if (EffectValue.Name == RawEffectName)
                {
                    IsConnected = true;
                    EffectValue.AddLinkBack(LinkBack);
                    return EffectValue;
                }
            }

            if (ErrorInfo != null)
                ErrorInfo.AddMissingKey(RawEffectName);

            return null;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "Effect"; } }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BitOffset = 0;
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, IList> StoredEnumListTable = new Dictionary<int, IList>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            AddString(Key, data, ref offset, BaseOffset, 0, StoredStringtable);
            AddString(Name, data, ref offset, BaseOffset, 4, StoredStringtable);
            AddString(Desc, data, ref offset, BaseOffset, 8, StoredStringtable);
            AddInt(RawIconId, data, ref offset, BaseOffset, 12);
            AddString(SpewText, data, ref offset, BaseOffset, 16, StoredStringtable);
            AddEnum(StackingType, data, ref offset, BaseOffset, 20);
            AddEnum(DisplayMode, data, ref offset, BaseOffset, 22);
            AddInt(RawStackingPriority, data, ref offset, BaseOffset, 24);
            AddInt(RawDuration, data, ref offset, BaseOffset, 28);
            AddEnumList(KeywordList, data, ref offset, BaseOffset, 32, StoredEnumListTable);
            AddEnumList(AbilityKeywordList, data, ref offset, BaseOffset, 36, StoredEnumListTable);
            AddStringList(FieldTableOrder, data, ref offset, BaseOffset, 40, StoredStringListTable);
            AddInt(RawTSysKeywordIndex, data, ref offset, BaseOffset, 44);
            AddEnum(Particle, data, ref offset, BaseOffset, 48);
            AddBool(IsKeywordListEmpty, data, ref offset, ref BitOffset, BaseOffset, 50, 0);
            AddBool(IsAbilityKeywordListEmpty, data, ref offset, ref BitOffset, BaseOffset, 50, 2);
            CloseBool(ref offset, ref BitOffset);

            FinishSerializing(data, ref offset, BaseOffset, 52, StoredStringtable, null, null, StoredEnumListTable, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
