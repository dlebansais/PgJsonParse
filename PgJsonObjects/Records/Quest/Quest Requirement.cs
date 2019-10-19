using PgJsonReader;
using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestRequirement : GenericJsonObject<QuestRequirement>, IPgQuestRequirement, ISpecificRecord
    {
        #region Init
        public QuestRequirement()
        {
        }

        protected QuestRequirement(OtherRequirementType OtherRequirementType)
        {
            this.OtherRequirementType = OtherRequirementType;
        }
        #endregion

        #region Indirect Properties
        public override string SortingName { get { return null; } }
        #endregion

        #region Parsing
        public object ToSpecific(ParseErrorInfo errorInfo)
        {
            QuestRequirement Result = ToSpecificQuestRequirement(errorInfo);

            if (Result != null)
                Result.CopyFieldTableOrder(null, FieldTableOrder);

            return Result;
        }

        public void CopyFieldTableOrder(string key, List<string> fieldTableOrder)
        {
            if (key != null)
                InitializeKey(key, 0, null, null);

            foreach (string Key in fieldTableOrder)
                FieldTableOrder.Add(Key);
        }

        public QuestRequirement ToSpecificQuestRequirement(ParseErrorInfo ErrorInfo)
        {
            IList<IPgQuestRequirement> AsRequirementOrList = RequirementOrList;

            switch (OtherRequirementType)
            {
                case OtherRequirementType.Or:
                    if (AsRequirementOrList.Count >= 2)
                        return new OrQuestRequirement(OtherRequirementType, RequirementOrList);
                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("QuestRequirement List");
                        return null;
                    }

                case OtherRequirementType.QuestCompleted:
                    if (RawRequirementQuestList.Count > 0)
                        return new QuestCompletedQuestRequirement(OtherRequirementType, RawRequirementQuestList);
                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("QuestRequirement Quest");
                        return null;
                    }

                case OtherRequirementType.GuildQuestCompleted:
                    if (RawRequirementQuestList.Count > 0)
                        return new GuildQuestCompletedQuestRequirement(OtherRequirementType, RawRequirementQuestList);
                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("QuestRequirement Quest");
                        return null;
                    }

                case OtherRequirementType.MinFavorLevel:
                    if ((RequirementFavorNpcId != null || RequirementFavorNpcName != null) && RequirementFavorLevel != Favor.Internal_None)
                        return new MinFavorLevelQuestRequirement(OtherRequirementType, RequirementFavorNpcArea, RequirementFavorNpcId, RequirementFavorNpcName, RequirementFavorLevel);

                    else if (RequirementFavorNpcId == null && RequirementFavorNpcName == null)
                    {
                        // Ignore bugged entry...
                        //ErrorInfo.AddInvalidObjectFormat("QuestRequirement Npc");
                        return new MinFavorLevelQuestRequirement(OtherRequirementType, RequirementFavorLevel);
                    }
                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("QuestRequirement Level");
                        return null;
                    }

                case OtherRequirementType.MinSkillLevel:
                    if (RequirementSkill != PowerSkill.Internal_None && RawRequirementSkillLevel.HasValue)
                        return new MinSkillLevelQuestRequirement(OtherRequirementType, RequirementSkill, RawRequirementSkillLevel);

                    else if (RequirementSkill == PowerSkill.Internal_None)
                    {
                        ErrorInfo.AddInvalidObjectFormat("QuestRequirement Skill");
                        return null;
                    }
                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("QuestRequirement Level");
                        return null;
                    }

                case OtherRequirementType.RuntimeBehaviorRuleSet:
                    if (RequirementRule != null)
                        return new RunTimeBehaviorRuleSetQuestRequirement(OtherRequirementType, RequirementRule);

                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("QuestRequirement Rule");
                        return null;
                    }

                case OtherRequirementType.HasEffectKeyword:
                    if (RequirementKeyword != EffectKeyword.Internal_None)
                        return new HasEffectKeywordQuestRequirement(OtherRequirementType, RequirementKeyword);

                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("QuestRequirement Keyword");
                        return null;
                    }

                case OtherRequirementType.IsLongtimeAnimal:
                    return new IsLongTimeAnimalQuestRequirement(OtherRequirementType);

                case OtherRequirementType.InteractionFlagSet:
                    if (RequirementInteractionFlag != null)
                        return new InteractionFlagSetQuestRequirement(OtherRequirementType, RequirementInteractionFlag);

                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("QuestRequirement InteractionFlag");
                        return null;
                    }

                case OtherRequirementType.HangOutCompleted:
                    if (RequirementHangOut != null)
                        return new HangOutCompletedQuestRequirement(OtherRequirementType, RequirementHangOut);

                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("QuestRequirement HangOut");
                        return null;
                    }

                case OtherRequirementType.IsWarden:
                    return new IsWardenQuestRequirement(OtherRequirementType);

                case OtherRequirementType.AreaEventOn:
                    if (RequirementArea != MapAreaName.Internal_None)
                        return new AreaEventOnQuestRequirement(OtherRequirementType, RequirementArea);
                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("QuestRequirement Area Event");
                        return null;
                    }

                default:
                    ErrorInfo.AddInvalidObjectFormat("QuestRequirement (T=" + OtherRequirementType + ")");
                    return null;
            }
        }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => OtherRequirementType = StringToEnumConversion<OtherRequirementType>.Parse(value, errorInfo),
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType, null, OtherRequirementType.Internal_None) } },
            { "Quest", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseQuest,
                GetString = () => RawRequirementQuestList.Count > 0 ? RawRequirementQuestList[0] : null } },
            { "Keyword", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseKeyword,
                GetString = () => StringToEnumConversion<EffectKeyword>.ToString(RequirementKeyword, null, EffectKeyword.Internal_None) } },
            { "Npc", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseNpc,
                GetString = () => Quest.NpcToString(RequirementFavorNpcId, RequirementFavorNpcName) } },
            { "Level", new FieldParser() {
                Type = FieldType.Unknown,
                ParseUnknown = ParseLevel,
                GetString = () => StringToEnumConversion<Favor>.ToString(RequirementFavorLevel, null, Favor.Internal_None) } },
            { "Skill", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseSkill,
                GetString = () => StringToEnumConversion<PowerSkill>.ToString(RequirementSkill, null, PowerSkill.Internal_None) } },
            { "List", new FieldParser() {
                Type = FieldType.ObjectArray,
                ParseObjectArray = ParseList,
                GetObjectArray = () => RequirementOrList } },
            { "Rule", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseRule,
                GetString = () => RequirementRule } },
            { "InteractionFlag", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseInteractionFlag,
                GetString = () => RequirementInteractionFlag } },
            { "HangOut", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseHangOut,
                GetString = () => RequirementHangOut } },
            { "AreaEvent", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseAreaEvent,
                GetString = () => StringToEnumConversion<MapAreaName>.ToString(RequirementArea, null, MapAreaName.Internal_None) } },
        }; } }

        private void ParseQuest(string value, ParseErrorInfo ErrorInfo)
        {
            if (OtherRequirementType == OtherRequirementType.QuestCompleted || OtherRequirementType == OtherRequirementType.GuildQuestCompleted)
                RawRequirementQuestList.Add(value);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestRequirement Quest (" + OtherRequirementType + ")");
        }

        private void ParseKeyword(string value, ParseErrorInfo ErrorInfo)
        {
            if (OtherRequirementType == OtherRequirementType.HasEffectKeyword)
                RequirementKeyword = StringToEnumConversion<EffectKeyword>.Parse(value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestRequirement Keyword (" + OtherRequirementType + ")");
        }

        private void ParseNpc(string value, ParseErrorInfo ErrorInfo)
        {
            if (OtherRequirementType == OtherRequirementType.MinFavorLevel)
            {
                MapAreaName ParsedArea;
                string NpcId;
                string NpcName;
                if (Quest.TryParseNPC(value, out ParsedArea, out NpcId, out NpcName, ErrorInfo))
                {
                    RequirementFavorNpcArea = ParsedArea;
                    RequirementFavorNpcId = NpcId;
                    RequirementFavorNpcName = NpcName;
                }
                else if (value.Length > 0)
                    ErrorInfo.AddInvalidObjectFormat("QuestRequirement Npc");
            }
            else
                ErrorInfo.AddInvalidObjectFormat("QuestRequirement Npc (" + OtherRequirementType + ")");
        }

        private void ParseLevel(object value, ParseErrorInfo ErrorInfo)
        {
            if (OtherRequirementType == OtherRequirementType.MinFavorLevel && value is JsonString StringValue)
                RequirementFavorLevel = StringToEnumConversion<Favor>.Parse(StringValue.String, ErrorInfo);

            else if (OtherRequirementType == OtherRequirementType.MinSkillLevel && value is JsonInteger IntegerValue)
                RawRequirementSkillLevel = IntegerValue.Number;

            else
                ErrorInfo.AddInvalidObjectFormat("QuestRequirement Level (" + OtherRequirementType + ")");
        }

        private void ParseSkill(string value, ParseErrorInfo ErrorInfo)
        {
            if (OtherRequirementType == OtherRequirementType.MinSkillLevel)
                RequirementSkill = StringToEnumConversion<PowerSkill>.Parse(value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestRequirement Skill (" + OtherRequirementType + ")");
        }

        private void ParseList(JsonObject RawList, ParseErrorInfo ErrorInfo)
        {
            if (OtherRequirementType == OtherRequirementType.Or)
            {
                QuestRequirement ParsedQuestRequirement;
                JsonObjectParser<QuestRequirement>.InitAsSubitem("List", RawList, out ParsedQuestRequirement, ErrorInfo);

                IPgQuestRequirement ConvertedQuestRequirement = ParsedQuestRequirement.ToSpecific(ErrorInfo) as QuestRequirement;
                if (ConvertedQuestRequirement != null)
                    RequirementOrList.Add(ConvertedQuestRequirement);
            }
            else
                ErrorInfo.AddInvalidObjectFormat("QuestRequirement List (" + OtherRequirementType + ")");
        }

        private void ParseRule(string value, ParseErrorInfo ErrorInfo)
        {
            if (OtherRequirementType == OtherRequirementType.RuntimeBehaviorRuleSet)
                RequirementRule = value;
            else
                ErrorInfo.AddInvalidObjectFormat("QuestRequirement Rule (" + OtherRequirementType + ")");
        }

        private void ParseInteractionFlag(string value, ParseErrorInfo ErrorInfo)
        {
            if (OtherRequirementType == OtherRequirementType.InteractionFlagSet)
                RequirementInteractionFlag = value;
            else
                ErrorInfo.AddInvalidObjectFormat("QuestRequirement InteractionFlag (" + OtherRequirementType + ")");
        }

        private void ParseHangOut(string value, ParseErrorInfo ErrorInfo)
        {
            if (OtherRequirementType == OtherRequirementType.HangOutCompleted)
                RequirementHangOut = value;
            else
                ErrorInfo.AddInvalidObjectFormat("QuestRequirement HangOut (" + OtherRequirementType + ")");
        }

        private void ParseAreaEvent(string value, ParseErrorInfo ErrorInfo)
        {
            if (OtherRequirementType == OtherRequirementType.AreaEventOn)
            {
                int AreaIndex = value.LastIndexOf('_');
                if (AreaIndex > 0)
                {
                    int KeyIndex = value.LastIndexOf('_', AreaIndex - 1);
                    if (KeyIndex > 0)
                    {
                        string AreaName = value.Substring(AreaIndex + 1);
                        string KeyName = value.Substring(KeyIndex + 1, AreaIndex - KeyIndex - 1);
                        string QuestName = value.Substring(0, KeyIndex);

                        if (AreaName == "Ilmari")
                            AreaName = "Desert1";
                        else if (AreaName == "Kur")
                            AreaName = "KurMountains";

                        RequirementArea = StringToEnumConversion<MapAreaName>.Parse(AreaName, ErrorInfo);
                        return;
                    }
                }
            }

            ErrorInfo.AddInvalidObjectFormat("QuestRequirement Area Event (" + OtherRequirementType + ")");
        }

        protected OtherRequirementType OtherRequirementType;
        private Favor RequirementFavorLevel;
        private int? RawRequirementSkillLevel;
        private MapAreaName RequirementFavorNpcArea;
        private string RequirementFavorNpcId;
        private string RequirementFavorNpcName;
        private List<string> RawRequirementQuestList = new List<string>();
        private EffectKeyword RequirementKeyword;
        private PowerSkill RequirementSkill;
        private IPgQuestRequirementCollection RequirementOrList = new QuestRequirementCollection();
        private string RequirementRule;
        private string RequirementInteractionFlag;
        private string RequirementHangOut;
        private MapAreaName RequirementArea;
        #endregion

        #region Indexing
        public override string TextContent
        {
            get { return ""; }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, IBackLinkable Parent, Dictionary<Type, Dictionary<string, IJsonKey>> AllTables)
        {
            return false;
        }

        public IList<IBackLinkable> GetLinkBack()
        {
            return null;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "QuestRequirement"; } }
        #endregion

        #region Serializing
        protected void SerializeJsonObjectInternalProlog(byte[] data, ref int offset, Dictionary<int, string> StoredStringtable, Dictionary<int, List<string>> StoredStringListTable)
        {
            int BaseOffset = offset;

            AddInt((int)OtherRequirementType, data, ref offset, BaseOffset, 0);
            AddString(Key, data, ref offset, BaseOffset, 4, StoredStringtable);
            AddStringList(FieldTableOrder, data, ref offset, BaseOffset, 8, StoredStringListTable);
        }

        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            throw new InvalidOperationException();
        }
        #endregion
    }
}
