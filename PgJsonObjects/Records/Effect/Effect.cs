using PgJsonReader;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class Effect : GenericJsonObject<Effect>
    {
        #region Direct Properties
        public string Name { get; private set; }
        public string Desc { get; private set; }
        public int IconId { get { return RawIconId.HasValue ? RawIconId.Value : 0; } }
        private int? RawIconId;
        public EffectDisplayMode DisplayMode { get; private set; }
        public string SpewText { get; private set; }
        public EffectParticle Particle { get; private set; }
        public EffectStackingType StackingType { get; private set; }
        public int StackingPriority { get { return RawStackingPriority.HasValue ? RawStackingPriority.Value : 0; } }
        public int? RawStackingPriority { get; private set; }
        public int Duration { get { return RawDuration.HasValue ? RawDuration.Value : 0; } }
        public int? RawDuration { get; private set; }
        public List<EffectKeyword> KeywordList { get; } = new List<EffectKeyword>();
        public bool IsKeywordListEmpty { get; private set; }
        public bool HasTSysKeyword { get; private set; }
        public List<AbilityKeyword> AbilityKeywordList { get; } = new List<AbilityKeyword>();
        public bool IsAbilityKeywordListEmpty { get; private set; }
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return Name; } }
        public string SearchResultIconFileName { get { return RawIconId.HasValue ? "icon_" + RawIconId.Value : null; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get; } = new Dictionary<string, FieldParser>()
        {
            { "Name", ParseFieldName },
            { "Desc", ParseFieldDesc },
            { "IconId", ParseFieldIconId },
            { "DisplayMode", ParseFieldDisplayMode },
            { "SpewText", ParseFieldSpewText },
            { "Particle", ParseFieldParticle },
            { "StackingType", ParseFieldStackingType },
            { "StackingPriority", ParseFieldStackingPriority },
            { "Duration", ParseFieldDuration },
            { "Keywords", ParseFieldKeywords },
            { "AbilityKeywords", ParseFieldAbilityKeywords },
        };

        public static readonly Dictionary<EffectStackingType, string> StackingTypeStringMap = new Dictionary<EffectStackingType, string>()
        {
            { EffectStackingType.LamiasGaze, "Lamia's Gaze" },
            { EffectStackingType.One, "1" },
        };

        public static readonly Dictionary<EffectKeyword, string> KeywordStringMap = new Dictionary<EffectKeyword, string>()
        {
            { EffectKeyword.Hyphen, "-" },
        };

        private static void ParseFieldName(Effect This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "Effect Name", This.ParseName);
        }

        private void ParseName(string RawName, ParseErrorInfo ErrorInfo)
        {
            Name = RawName;
        }

        private static void ParseFieldDesc(Effect This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "Effect Desc", This.ParseDesc);
        }

        private void ParseDesc(string RawDesc, ParseErrorInfo ErrorInfo)
        {
            Desc = RawDesc;
        }

        private static void ParseFieldIconId(Effect This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueInteger(Value, ErrorInfo, "Effect IconId", This.ParseIconId);
        }

        private void ParseIconId(long RawIconId, ParseErrorInfo ErrorInfo)
        {
            if (RawIconId > 0)
            {
                this.RawIconId = (int)RawIconId;
                ErrorInfo.AddIconId((int)RawIconId);
            }
            else
                this.RawIconId = null;
        }

        private static void ParseFieldDisplayMode(Effect This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "Effect DisplayMode", This.ParseDisplayMode);
        }

        private void ParseDisplayMode(string RawDisplayMode, ParseErrorInfo ErrorInfo)
        {
            EffectDisplayMode ConvertedDisplayMode;
            StringToEnumConversion<EffectDisplayMode>.TryParse(RawDisplayMode, out ConvertedDisplayMode, ErrorInfo);
            DisplayMode = ConvertedDisplayMode;
        }

        private static void ParseFieldSpewText(Effect This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "Effect SpewText", This.ParseSpewText);
        }

        private void ParseSpewText(string RawSpewText, ParseErrorInfo ErrorInfo)
        {
            SpewText = RawSpewText;
        }

        private static void ParseFieldParticle(Effect This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "Effect Particle", This.ParseParticle);
        }

        private void ParseParticle(string RawParticle, ParseErrorInfo ErrorInfo)
        {
            EffectParticle ConvertedParticle;
            StringToEnumConversion<EffectParticle>.TryParse(RawParticle, out ConvertedParticle, ErrorInfo);
            Particle = ConvertedParticle;
        }

        private static void ParseFieldStackingType(Effect This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "Effect StackingType", This.ParseStackingType);
        }

        private void ParseStackingType(string RawStackingType, ParseErrorInfo ErrorInfo)
        {
            EffectStackingType ConvertedStackingType;
            StringToEnumConversion<EffectStackingType>.TryParse(RawStackingType, StackingTypeStringMap, out ConvertedStackingType, ErrorInfo);
            StackingType = ConvertedStackingType;
        }

        private static void ParseFieldStackingPriority(Effect This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueInteger(Value, ErrorInfo, "Effect StackingPriority", This.ParseStackingPriority);
        }

        private void ParseStackingPriority(long RawStackingPriority, ParseErrorInfo ErrorInfo)
        {
            this.RawStackingPriority = (int)RawStackingPriority;
        }

        private static void ParseFieldDuration(Effect This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueInteger(Value, ErrorInfo, "Effect Duration", This.ParseDuration);
        }

        private void ParseDuration(long RawDuration, ParseErrorInfo ErrorInfo)
        {
            this.RawDuration = (int)RawDuration;
        }

        private static void ParseFieldKeywords(Effect This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueStringArray(Value, ErrorInfo, "Effect Keywords", This.ParseKeywords);
        }

        private bool ParseKeywords(string RawKeyword, ParseErrorInfo ErrorInfo)
        {
            StringToEnumConversion<EffectKeyword>.TryParse(RawKeyword, KeywordStringMap, out EffectKeyword ParsedEffectKeyword, ErrorInfo);
            if (ParsedEffectKeyword != EffectKeyword.TSys)
                KeywordList.Add(ParsedEffectKeyword);
            else
                HasTSysKeyword = true;

            //IsKeywordListEmpty = (RawKeywords != null && ParsedKeywordList.Count == 0);
            return true;
        }

        private static void ParseFieldAbilityKeywords(Effect This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueStringArray(Value, ErrorInfo, "Effect AbilityKeywords", This.ParseAbilityKeywords);
        }

        private bool ParseAbilityKeywords(string RawAbilityKeyword, ParseErrorInfo ErrorInfo)
        {
            StringToEnumConversion<AbilityKeyword>.TryParse(RawAbilityKeyword, out AbilityKeyword ParsedAbilityKeyword, ErrorInfo);
            AbilityKeywordList.Add(ParsedAbilityKeyword);
            //IsAbilityKeywordListEmpty = (RawAbilityKeywords != null && RawAbilityKeywords.Count == 0);
            return true;
        }
        #endregion

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddString("Name", Name);
            Generator.AddString("Desc", Desc);
            Generator.AddInteger("IconId", RawIconId);
            Generator.AddString("DisplayMode", StringToEnumConversion<EffectDisplayMode>.ToString(DisplayMode, null, EffectDisplayMode.Internal_None));
            Generator.AddString("SpewText", SpewText);
            Generator.AddString("Particle", StringToEnumConversion<EffectParticle>.ToString(Particle, null, EffectParticle.Internal_None));
            Generator.AddString("StackingType", StringToEnumConversion<EffectStackingType>.ToString(StackingType, StackingTypeStringMap, EffectStackingType.Internal_None));
            Generator.AddInteger("StackingPriority", RawStackingPriority);
            Generator.AddInteger("Duration", RawDuration);
            if (IsKeywordListEmpty)
                Generator.AddEmptyArray("Keywords");
            else
                StringToEnumConversion<EffectKeyword>.ListToString(Generator, "Keywords", KeywordList, KeywordStringMap);
            if (IsAbilityKeywordListEmpty)
                Generator.AddEmptyArray("AbilityKeywords");
            else
                StringToEnumConversion<AbilityKeyword>.ListToString(Generator, "AbilityKeywords", AbilityKeywordList);

            Generator.CloseObject();
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
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            return false;
        }

        public static Effect ConnectSingleProperty(ParseErrorInfo ErrorInfo, Dictionary<string, IGenericJsonObject> EffectTable, string RawEffectName, Effect ParsedEffect, ref bool IsRawEffectParsed, ref bool IsConnected, GenericJsonObject LinkBack)
        {
            if (IsRawEffectParsed)
                return ParsedEffect;

            IsRawEffectParsed = true;

            if (RawEffectName == null)
                return null;

            foreach (KeyValuePair<string, IGenericJsonObject> Entry in EffectTable)
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
    }
}
