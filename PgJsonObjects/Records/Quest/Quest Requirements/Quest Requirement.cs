﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestRequirement : GenericJsonObject<QuestRequirement>
    {
        #region Indirect Properties
        protected override string SortingName { get { return null; } }
        #endregion

        #region Parsing
        public QuestRequirement ToSpecificQuestRequirement(ParseErrorInfo ErrorInfo)
        {
            switch (T)
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
                        ErrorInfo.AddInvalidObjectFormat("QuestRequirement Npc");
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
                    ErrorInfo.AddInvalidObjectFormat("QuestRequirement (T=" + T + ")");
                    return null;
            }
        }

        protected override Dictionary<string, FieldValueHandler> FieldTable { get; } = new Dictionary<string, FieldValueHandler>()
        {
            { "T", ParseFieldT },
            { "Quest", ParseFieldQuest },
            { "Keyword", ParseFieldKeyword },
            { "Npc", ParseFieldNpc },
            { "Level", ParseFieldLevel },
            { "Skill", ParseFieldSkill },
            { "List", ParseFieldList },
            { "Rule", ParseFieldRule },
            { "InteractionFlag", ParseFieldInteractionFlag },
        };

        private static void ParseFieldT(QuestRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawT;
            if ((RawT = Value as string) != null)
                This.ParseT(RawT, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestRequirement T");
        }

        private void ParseT(string RawT, ParseErrorInfo ErrorInfo)
        {
            OtherRequirementType ParsedT;
            StringToEnumConversion<OtherRequirementType>.TryParse(RawT, out ParsedT, ErrorInfo);
            T = ParsedT;
        }

        private static void ParseFieldQuest(QuestRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawQuest;
            if ((RawQuest = Value as string) != null)
                This.ParseQuest(RawQuest, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestRequirement Quest");
        }

        private void ParseQuest(string RawQuest, ParseErrorInfo ErrorInfo)
        {
            if (T == OtherRequirementType.QuestCompleted || T == OtherRequirementType.GuildQuestCompleted)
                RawRequirementQuestList.Add(RawQuest);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestRequirement Quest (" + T + ")");
        }

        private static void ParseFieldKeyword(QuestRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawKeyword;
            if ((RawKeyword = Value as string) != null)
                This.ParseKeyword(RawKeyword, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestRequirement Keyword");
        }

        private void ParseKeyword(string RawKeyword, ParseErrorInfo ErrorInfo)
        {
            if (T == OtherRequirementType.HasEffectKeyword)
            {
                EffectKeyword ParsedEffectKeyword;
                StringToEnumConversion<EffectKeyword>.TryParse(RawKeyword, out ParsedEffectKeyword, ErrorInfo);
                RequirementKeyword = ParsedEffectKeyword;
            }
            else
                ErrorInfo.AddInvalidObjectFormat("QuestRequirement Keyword (" + T + ")");
        }

        private static void ParseFieldNpc(QuestRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawNpc;
            if ((RawNpc = Value as string) != null)
                This.ParseNpc(RawNpc, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestRequirement Npc");
        }

        private void ParseNpc(string RawNpc, ParseErrorInfo ErrorInfo)
        {
            if (T == OtherRequirementType.MinFavorLevel)
            {
                MapAreaName ParsedArea;
                string NpcId;
                string NpcName;
                if (Quest.TryParseNPC(RawNpc, out ParsedArea, out NpcId, out NpcName, ErrorInfo))
                {
                    RequirementFavorNpcArea = ParsedArea;
                    RequirementFavorNpcId = NpcId;
                    RequirementFavorNpcName = NpcName;
                }
                else
                    ErrorInfo.AddInvalidObjectFormat("QuestRequirement Npc");
            }
            else
                ErrorInfo.AddInvalidObjectFormat("QuestRequirement Npc (" + T + ")");
        }

        private static void ParseFieldLevel(QuestRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawLevel;
            if ((RawLevel = Value as string) != null)
                This.ParseLevelForFavor(RawLevel, ErrorInfo);
            else if (Value is int)
                This.ParseLevelForSkill((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestRequirement Level");
        }

        private void ParseLevelForFavor(string RawLevel, ParseErrorInfo ErrorInfo)
        {
            if (T == OtherRequirementType.MinFavorLevel)
            {
                Favor ParsedFavor;
                StringToEnumConversion<Favor>.TryParse(RawLevel, out ParsedFavor, ErrorInfo);
                RequirementFavorLevel = ParsedFavor;
            }
            else
                ErrorInfo.AddInvalidObjectFormat("QuestRequirement Level (" + T + ")");
        }

        private void ParseLevelForSkill(int RawLevel, ParseErrorInfo ErrorInfo)
        {
            if (T == OtherRequirementType.MinSkillLevel)
                RawRequirementSkillLevel = RawLevel;
            else
                ErrorInfo.AddInvalidObjectFormat("QuestRequirement Level (" + T + ")");
        }

        private static void ParseFieldSkill(QuestRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawSkill;
            if ((RawSkill = Value as string) != null)
                This.ParseSkill(RawSkill, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestRequirement Skill");
        }

        private void ParseSkill(string RawSkill, ParseErrorInfo ErrorInfo)
        {
            if (T == OtherRequirementType.MinSkillLevel)
            {
                PowerSkill ParsedSkill;
                StringToEnumConversion<PowerSkill>.TryParse(RawSkill, out ParsedSkill, ErrorInfo);
                RequirementSkill = ParsedSkill;
            }
            else
                ErrorInfo.AddInvalidObjectFormat("QuestRequirement Skill (" + T + ")");
        }

        private static void ParseFieldList(QuestRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            Dictionary<string, object> AsDictionary;
            ArrayList RawList;

            if ((AsDictionary = Value as Dictionary<string, object>) != null)
                This.ParseListAsDictionary(AsDictionary, ErrorInfo);

            else if ((RawList = Value as ArrayList) != null)
                This.ParseList(RawList, ErrorInfo);

            else
                ErrorInfo.AddInvalidObjectFormat("QuestRequirement List");
        }

        private void ParseList(ArrayList RawRequirementList, ParseErrorInfo ErrorInfo)
        {
            foreach (object Item in RawRequirementList)
            {
                Dictionary<string, object> AsDictionary;

                if ((AsDictionary = Item as Dictionary<string, object>) != null)
                    ParseListAsDictionary(AsDictionary, ErrorInfo);
                else
                    ErrorInfo.AddInvalidObjectFormat("QuestRequirement List");
            }
        }

        private void ParseListAsDictionary(Dictionary<string, object> RawList, ParseErrorInfo ErrorInfo)
        {
            if (T == OtherRequirementType.Or)
            {
                QuestRequirement ParsedQuestRequirement;
                JsonObjectParser<QuestRequirement>.InitAsSubitem("List", RawList, out ParsedQuestRequirement, ErrorInfo);

                QuestRequirement ConvertedQuestRequirement = ParsedQuestRequirement.ToSpecificQuestRequirement(ErrorInfo);
                if (ConvertedQuestRequirement != null)
                    RequirementOrList.Add(ConvertedQuestRequirement);
            }
            else
                ErrorInfo.AddInvalidObjectFormat("QuestRequirement List (" + T + ")");
        }

        private static void ParseFieldRule(QuestRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawRule;
            if ((RawRule = Value as string) != null)
                This.ParseRule(RawRule, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestRequirement Rule");
        }

        private void ParseRule(string RawRule, ParseErrorInfo ErrorInfo)
        {
            if (T == OtherRequirementType.RuntimeBehaviorRuleSet)
                RequirementRule = RawRule;
            else
                ErrorInfo.AddInvalidObjectFormat("QuestRequirement Rule (" + T + ")");
        }

        private static void ParseFieldInteractionFlag(QuestRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawInteractionFlag;
            if ((RawInteractionFlag = Value as string) != null)
                This.ParseInteractionFlag(RawInteractionFlag, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestRequirement InteractionFlag");
        }

        private void ParseInteractionFlag(string RawInteractionFlag, ParseErrorInfo ErrorInfo)
        {
            if (T == OtherRequirementType.InteractionFlagSet)
                RequirementInteractionFlag = RawInteractionFlag;
            else
                ErrorInfo.AddInvalidObjectFormat("QuestRequirement InteractionFlag (" + T + ")");
        }

        private OtherRequirementType T;
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