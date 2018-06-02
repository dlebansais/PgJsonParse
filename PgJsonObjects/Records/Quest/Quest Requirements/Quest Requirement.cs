using PgJsonReader;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestRequirement : GenericJsonObject<QuestRequirement>, ISpecificRecord
    {
        #region Indirect Properties
        protected override string SortingName { get { return null; } }
        #endregion

        #region Parsing
        public object ToSpecific(ParseErrorInfo errorInfo)
        {
            return ToSpecificQuestRequirement(errorInfo);
        }

        public QuestRequirement ToSpecificQuestRequirement(ParseErrorInfo ErrorInfo)
        {
            switch (OtherRequirementType)
            {
                case OtherRequirementType.Or:
                    if (RequirementOrList.Count >= 2)
                        return new OrQuestRequirement(RequirementOrList);
                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("QuestRequirement List");
                        return null;
                    }

                case OtherRequirementType.QuestCompleted:
                    if (RawRequirementQuestList.Count > 0)
                        return new QuestCompletedQuestRequirement(RawRequirementQuestList);
                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("QuestRequirement Quest");
                        return null;
                    }

                case OtherRequirementType.GuildQuestCompleted:
                    if (RawRequirementQuestList.Count > 0)
                        return new GuildQuestCompletedQuestRequirement(RawRequirementQuestList);
                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("QuestRequirement Quest");
                        return null;
                    }

                case OtherRequirementType.MinFavorLevel:
                    if ((RequirementFavorNpcId != null || RequirementFavorNpcName != null) && RequirementFavorLevel != Favor.Internal_None)
                        return new MinFavorLevelQuestRequirement(RequirementFavorNpcArea, RequirementFavorNpcId, RequirementFavorNpcName, RequirementFavorLevel);

                    else if (RequirementFavorNpcId == null && RequirementFavorNpcName == null)
                    {
                        // Ignore bugged entry...
                        //ErrorInfo.AddInvalidObjectFormat("QuestRequirement Npc");
                        return null;
                    }
                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("QuestRequirement Level");
                        return null;
                    }

                case OtherRequirementType.MinSkillLevel:
                    if (RequirementSkill != PowerSkill.Internal_None && RawRequirementSkillLevel.HasValue)
                        return new MinSkillLevelQuestRequirement(RequirementSkill, RawRequirementSkillLevel);

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
                        return new RunTimeBehaviorRuleSetQuestRequirement(RequirementRule);

                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("QuestRequirement Rule");
                        return null;
                    }

                case OtherRequirementType.HasEffectKeyword:
                    if (RequirementKeyword != EffectKeyword.Internal_None)
                        return new HasEffectKeywordQuestRequirement(RequirementKeyword);

                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("QuestRequirement Keyword");
                        return null;
                    }

                case OtherRequirementType.IsLongtimeAnimal:
                    return new IsLongTimeAnimalQuestRequirement();

                case OtherRequirementType.InteractionFlagSet:
                    if (RequirementInteractionFlag != null)
                        return new InteractionFlagSetQuestRequirement(RequirementInteractionFlag);

                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("QuestRequirement InteractionFlag");
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
                ParseString = (string value, ParseErrorInfo errorInfo) => { OtherRequirementType = StringToEnumConversion<OtherRequirementType>.Parse(value, errorInfo); },
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType) } },
            { "Quest", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseQuest,
                GetString = () => RawRequirementQuestList.Count > 0 ? RawRequirementQuestList[0] : null } },
            { "Keyword", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseKeyword,
                GetString = () => StringToEnumConversion<EffectKeyword>.ToString(RequirementKeyword) } },
            { "Npc", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseNpc,
                GetString = () => Quest.NpcToString(RequirementFavorNpcId, RequirementFavorNpcName) } },
            { "Level", new FieldParser() {
                Type = FieldType.Unknown,
                ParseUnknown = ParseLevel,
                GetString = () => StringToEnumConversion<Favor>.ToString(RequirementFavorLevel) } },
            { "Skill", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseSkill,
                GetString = () => StringToEnumConversion<PowerSkill>.ToString(RequirementSkill) } },
            { "List", new FieldParser() {
                Type = FieldType.ObjectArray,
                ParseObjectArray = ParseList,
                GetObjectArray = () => null } },
            { "Rule", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseRule,
                GetString = () => RequirementRule } },
            { "InteractionFlag", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseInteractionFlag,
                GetString = () => RequirementInteractionFlag } },
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

                QuestRequirement ConvertedQuestRequirement = ParsedQuestRequirement.ToSpecificQuestRequirement(ErrorInfo);
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

        private OtherRequirementType OtherRequirementType;
        private Favor RequirementFavorLevel;
        private int? RawRequirementSkillLevel;
        private MapAreaName RequirementFavorNpcArea;
        private string RequirementFavorNpcId;
        private string RequirementFavorNpcName;
        private List<string> RawRequirementQuestList = new List<string>();
        private EffectKeyword RequirementKeyword;
        private PowerSkill RequirementSkill;
        private List<QuestRequirement> RequirementOrList = new List<QuestRequirement>();
        private string RequirementRule;
        private string RequirementInteractionFlag;
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
            get { return ""; }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            return false;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "QuestRequirement"; } }
        #endregion
    }
}
