using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class Effect : GenericJsonObject<Effect>
    {
        #region Constants
        public static readonly Dictionary<EffectStackingType, string> StackingTypeStringMap = new Dictionary<EffectStackingType, string>()
        {
            { EffectStackingType.LamiasGaze, "Lamia's Gaze" },
            { EffectStackingType.One, "1" },
        };

        public static readonly Dictionary<EffectKeyword, string> KeywordStringMap = new Dictionary<EffectKeyword, string>()
        {
            { EffectKeyword.Hyphen, "-" },
        };

        private Dictionary<string, FieldValueHandler> _FieldTable = new Dictionary<string, FieldValueHandler>()
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
        #endregion

        #region Properties
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
        public List<EffectKeyword> KeywordList { get; private set; }
        public bool IsKeywordListEmpty { get; private set; }
        public List<AbilityKeyword> AbilityKeywordList { get; private set; }
        public bool IsAbilityKeywordListEmpty { get; private set; }

        public string SearchResultIconFileName { get { return RawIconId.HasValue ? "icon_" + RawIconId.Value : null; } }

        public string CombinedSpewText
        {
            get
            {
                if (SpewText == null)
                    return "";

                string Result = SpewText;
                Result  = Result.Replace("%NAME%", "Target");

                return Result;
            }
        }

        public string CombinedKeywords
        {
            get
            {
                if (KeywordList.Count == 0)
                    return "None";

                List<string> KeywordStringList = new List<string>();
                foreach (EffectKeyword Item in KeywordList)
                {
                    string ItemText = TextMaps.EffectKeywordTextMap[Item];
                    if (ItemText.Length > 0 && !KeywordStringList.Contains(ItemText))
                        KeywordStringList.Add(ItemText);
                }

                string Result = "";

                foreach (string Item in KeywordStringList)
                {
                    if (Result.Length > 0)
                        Result += ", ";

                    Result += Item;
                }

                return Result;
            }
        }

        public string CombinedAbilityKeywords
        {
            get
            {
                if (AbilityKeywordList.Count == 0)
                    return "None";

                string Result = "";

                foreach (AbilityKeyword Keyword in AbilityKeywordList)
                {
                    if (Result.Length > 0)
                        Result += ", ";

                    Result += TextMaps.AbilityKeywordTextMap[Keyword];
                }

                return Result;
            }
        }
        #endregion

        #region Client Interface
        private static void ParseFieldName(Effect This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawName;
            if ((RawName = Value as string) != null)
                This.ParseName(RawName, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Effect Name");
        }

        private void ParseName(string RawName, ParseErrorInfo ErrorInfo)
        {
            Name = RawName;
        }

        private static void ParseFieldDesc(Effect This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawDesc;
            if ((RawDesc = Value as string) != null)
                This.ParseDesc(RawDesc, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Effect Desc");
        }

        private void ParseDesc(string RawDesc, ParseErrorInfo ErrorInfo)
        {
            Desc = RawDesc;
        }

        private static void ParseFieldIconId(Effect This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseIconId((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Effect IconId");
        }

        private void ParseIconId(int RawIconId, ParseErrorInfo ErrorInfo)
        {
            if (RawIconId > 0)
            {
                this.RawIconId = RawIconId;
                ErrorInfo.AddIconId(RawIconId);
            }
            else
                this.RawIconId = null;
        }

        private static void ParseFieldDisplayMode(Effect This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawDisplayMode;
            if ((RawDisplayMode = Value as string) != null)
                This.ParseDisplayMode(RawDisplayMode, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Effect DisplayMode");
        }

        private void ParseDisplayMode(string RawDisplayMode, ParseErrorInfo ErrorInfo)
        {
            EffectDisplayMode ConvertedDisplayMode;
            StringToEnumConversion<EffectDisplayMode>.TryParse(RawDisplayMode, out ConvertedDisplayMode, ErrorInfo);
            DisplayMode = ConvertedDisplayMode;
        }

        private static void ParseFieldSpewText(Effect This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawSpewText;
            if ((RawSpewText = Value as string) != null)
                This.ParseSpewText(RawSpewText, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Effect SpewText");
        }

        private void ParseSpewText(string RawSpewText, ParseErrorInfo ErrorInfo)
        {
            SpewText = RawSpewText;
        }

        private static void ParseFieldParticle(Effect This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawParticle;
            if ((RawParticle = Value as string) != null)
                This.ParseParticle(RawParticle, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Effect Particle");
        }

        private void ParseParticle(string RawParticle, ParseErrorInfo ErrorInfo)
        {
            EffectParticle ConvertedParticle;
            StringToEnumConversion<EffectParticle>.TryParse(RawParticle, out ConvertedParticle, ErrorInfo);
            Particle = ConvertedParticle;
        }

        private static void ParseFieldStackingType(Effect This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawStackingType;
            if ((RawStackingType = Value as string) != null)
                This.ParseStackingType(RawStackingType, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Effect StackingType");
        }

        private void ParseStackingType(string RawStackingType, ParseErrorInfo ErrorInfo)
        {
            EffectStackingType ConvertedStackingType;
            StringToEnumConversion<EffectStackingType>.TryParse(RawStackingType, StackingTypeStringMap, out ConvertedStackingType, ErrorInfo);
            StackingType = ConvertedStackingType;
        }

        private static void ParseFieldStackingPriority(Effect This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseStackingPriority((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Effect StackingPriority");
        }

        private void ParseStackingPriority(int RawStackingPriority, ParseErrorInfo ErrorInfo)
        {
            this.RawStackingPriority = RawStackingPriority;
        }

        private static void ParseFieldDuration(Effect This, object Value, ParseErrorInfo ErrorInfo)
        {
            string AsString;

            if (Value is int)
                This.ParseDuration((int)Value, ErrorInfo);

            else if ((AsString = Value as string) != null)
            {
                int RawDuration;
                if (int.TryParse(AsString, out RawDuration))
                    This.ParseDuration(RawDuration, ErrorInfo);
                else
                    ErrorInfo.AddInvalidObjectFormat("Effect Duration");
            }
            else
                ErrorInfo.AddInvalidObjectFormat("Effect Duration");
        }

        private void ParseDuration(int RawDuration, ParseErrorInfo ErrorInfo)
        {
            this.RawDuration = RawDuration;
        }

        private static void ParseFieldKeywords(Effect This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawKeywords;
            if ((RawKeywords = Value as ArrayList) != null)
                This.ParseKeywords(RawKeywords, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Effect Keywords");
        }

        private void ParseKeywords(ArrayList RawKeywords, ParseErrorInfo ErrorInfo)
        {
            StringToEnumConversion<EffectKeyword>.ParseList(RawKeywords, KeywordStringMap, KeywordList, ErrorInfo);
            IsKeywordListEmpty = (RawKeywords != null && RawKeywords.Count == 0);
        }

        private static void ParseFieldAbilityKeywords(Effect This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawAbilityKeywords;
            if ((RawAbilityKeywords = Value as ArrayList) != null)
                This.ParseAbilityKeywords(RawAbilityKeywords, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Effect AbilityKeywords");
        }

        private void ParseAbilityKeywords(ArrayList RawAbilityKeywords, ParseErrorInfo ErrorInfo)
        {
            StringToEnumConversion<AbilityKeyword>.ParseList(RawAbilityKeywords, AbilityKeywordList, ErrorInfo);
            IsAbilityKeywordListEmpty = (RawAbilityKeywords != null && RawAbilityKeywords.Count == 0);
        }

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

        public static Effect ConnectSingleProperty(ParseErrorInfo ErrorInfo, Dictionary<string, Effect> EffectTable, string RawEffectName, Effect ParsedEffect, ref bool IsRawEffectParsed, ref bool IsConnected)
        {
            if (IsRawEffectParsed)
                return ParsedEffect;

            IsRawEffectParsed = true;

            if (RawEffectName == null)
                return null;

            foreach (KeyValuePair<string, Effect> Entry in EffectTable)
                if (Entry.Value.Name == RawEffectName)
                {
                    IsConnected = true;
                    return Entry.Value;
                }

            ErrorInfo.AddMissingKey(RawEffectName);
            return null;
        }

        public override string TextContent
        {
            get
            {
                string Result = "";

                if (RawIconId.HasValue)
                {
                    AddWithFieldSeparator(ref Result, Name);
                    AddWithFieldSeparator(ref Result, Desc);
                    if (DisplayMode != EffectDisplayMode.Internal_None)
                        AddWithFieldSeparator(ref Result, TextMaps.EffectDisplayModeTextMap[DisplayMode]);
                    AddWithFieldSeparator(ref Result, CombinedSpewText);
                    if (Particle != EffectParticle.Internal_None)
                        AddWithFieldSeparator(ref Result, TextMaps.EffectParticleTextMap[Particle]);
                    if (StackingType != EffectStackingType.Internal_None)
                        AddWithFieldSeparator(ref Result, TextMaps.EffectStackingTypeTextMap[StackingType]);
                    AddWithFieldSeparator(ref Result, CombinedKeywords);
                    AddWithFieldSeparator(ref Result, CombinedAbilityKeywords);
                }

                return Result;
            }
        }
        #endregion

        #region Ancestor Interface
        protected override Dictionary<string, FieldValueHandler> FieldTable { get { return _FieldTable; } }
        protected override string FieldTableName { get { return "Effect"; } }

        protected override void InitializeFields()
        {
            KeywordList = new List<EffectKeyword>();
            IsKeywordListEmpty = false;
            AbilityKeywordList = new List<AbilityKeyword>();
            IsAbilityKeywordListEmpty = false;
        }

        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable, Dictionary<string, XpTable> XpTableTable, Dictionary<string, AdvancementTable> AdvancementTableTable)
        {
            return false;
        }
        #endregion
    }
}
