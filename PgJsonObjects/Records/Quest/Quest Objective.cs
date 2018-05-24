using PgJsonReader;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestObjective : GenericJsonObject<QuestObjective>, ISpecificRecord
    {
        #region Init
        public QuestObjective()
        {
        }

        public QuestObjective(string Description, int? RawNumber, bool? RawMustCompleteEarlierObjectivesFirst, int? MinHour, int? MaxHour)
        {
            this.Description = Description;
            this.RawNumber = RawNumber;
            this.RawMustCompleteEarlierObjectivesFirst = RawMustCompleteEarlierObjectivesFirst;
            this.MinHour = MinHour;
            this.MaxHour = MaxHour;
        }
        #endregion

        #region Direct Properties
        public QuestObjectiveType Type { get; private set; }
        public string Description { get; private set; }
        public int Number { get { return RawNumber.HasValue ? RawNumber.Value : 1; } }
        public int? RawNumber { get; private set; }
        public bool MustCompleteEarlierObjectivesFirst { get { return RawMustCompleteEarlierObjectivesFirst.HasValue && RawMustCompleteEarlierObjectivesFirst.Value; } }
        public bool? RawMustCompleteEarlierObjectivesFirst;
        public int? MinHour { get; private set; }
        public int? MaxHour { get; private set; }
        private int? RawMinAmount;
        private int? RawMaxAmount;
        private QuestObjectiveKillTarget KillTarget;
        private ItemKeyword ItemTarget;
        private RecipeKeyword RecipeTarget;
        private AbilityKeyword AbilityTarget;
        private string AbilityKeyword;
        public List<string> RawInteractionFlagList = new List<string>();
        private string RawItemName;
        public string RawInteractionFlag;
        private MapAreaName DeliverNpcArea;
        private string DeliverNpcId;
        private string DeliverNpcName;
        private float? RawMinFavorReceived;
        private float? RawMaxFavorReceived;
        private PowerSkill Skill;
        private string StringParam;
        private ItemKeyword ResultItemKeyword;
        private PowerSkill AnatomyType;
        private ItemKeyword ItemKeyword;
        private MonsterTypeTag MonsterTypeTag;
        private EffectKeyword EffectRequirement;
        protected int? RawNumToDeliver;
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return null; } }
        public Quest ParentQuest { get; private set; }
        public bool HasMinAndMaxHours { get { return MinHour.HasValue && MaxHour.HasValue; } }
        public string TimeCompletion
        {
            get
            {
                if (!MinHour.HasValue || !MaxHour.HasValue)
                    return null;

                return "(this step must be completed between " + MinHour.Value.ToString("D02") + ":00" + " and " + MaxHour.Value.ToString("D02") + ":00" + ")";
            }
        }
        #endregion

        #region Parsing
        public object ToSpecific(ParseErrorInfo errorInfo)
        {
            return ToSpecificQuestObjective(errorInfo);
        }

        public QuestObjective ToSpecificQuestObjective(ParseErrorInfo ErrorInfo)
        {
            switch (Type)
            {
                case QuestObjectiveType.Kill:
                case QuestObjectiveType.KillElite:
                    return new QuestObjectiveKill(Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour, KillTarget, AbilityKeyword, EffectRequirement);

                case QuestObjectiveType.TipPlayer:
                    return new QuestObjectiveTipPlayer(Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour, RawMinAmount);

                case QuestObjectiveType.Special:
                    return new QuestObjectiveSpecial(Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour, RawMinAmount, RawMaxAmount, StringParam);

                case QuestObjectiveType.MultipleInteractionFlags:
                    return new QuestObjectiveMultipleInteractionFlags(Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour, RawInteractionFlagList);

                case QuestObjectiveType.Deliver:
                    if (DeliverNpcArea != MapAreaName.Internal_None && DeliverNpcName != null)
                        return new QuestObjectiveDeliver(Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour, DeliverNpcArea, DeliverNpcId, DeliverNpcName, RawItemName, RawNumToDeliver.HasValue && RawNumToDeliver.Value > 0 ? RawNumToDeliver.Value : -1);
                    else
                        return this;

                case QuestObjectiveType.Collect:
                case QuestObjectiveType.Have:
                case QuestObjectiveType.Harvest:
                case QuestObjectiveType.UseItem:
                    if (RawItemName != null && ItemTarget == ItemKeyword.Internal_None)
                        return new QuestObjectiveItem(Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour, RawItemName);
                    else if (RawItemName == null && ItemTarget != ItemKeyword.Internal_None)
                        return new QuestObjectiveItem(Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour, ItemTarget);
                    else
                        return this;

                case QuestObjectiveType.GuildGiveItem:
                    if (RawItemName != null && ItemKeyword == ItemKeyword.Internal_None)
                        return new QuestObjectiveGuildGiveItem(Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour, DeliverNpcId, DeliverNpcName, RawItemName);
                    else if (RawItemName == null && ItemKeyword != ItemKeyword.Internal_None)
                        return new QuestObjectiveGuildGiveItem(Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour, DeliverNpcId, DeliverNpcName, ItemKeyword);
                    else
                        return this;

                case QuestObjectiveType.InteractionFlag:
                    return new QuestObjectiveInteractionFlag(Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour, RawInteractionFlag);

                case QuestObjectiveType.GiveGift:
                    return new QuestObjectiveGiveGift(Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour, RawMinFavorReceived, RawMaxFavorReceived);

                case QuestObjectiveType.UseRecipe:
                    return new QuestObjectiveUseRecipe(Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour, Skill, RecipeTarget, ResultItemKeyword);

                case QuestObjectiveType.BeAttacked:
                case QuestObjectiveType.Bury:
                    return new QuestObjectiveAnatomy(Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour, AnatomyType);

                case QuestObjectiveType.UseAbility:
                    return new QuestObjectiveUseAbility(Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour, AbilityTarget);

                case QuestObjectiveType.Loot:
                    if (RawItemName != null && ItemTarget == ItemKeyword.Internal_None)
                        return new QuestObjectiveLoot(Description, RawItemName, RawNumber, MonsterTypeTag, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour);
                    else if (RawItemName == null && ItemTarget != ItemKeyword.Internal_None)
                        return new QuestObjectiveLoot(Description, ItemTarget, RawNumber, MonsterTypeTag, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour);
                    else
                        return this;

                case QuestObjectiveType.Scripted:
                case QuestObjectiveType.SayInChat:
                case QuestObjectiveType.UniqueSpecial:
                case QuestObjectiveType.GuildKill:
                case QuestObjectiveType.DruidKill:
                case QuestObjectiveType.DruidScripted:
                    return this;

                case QuestObjectiveType.ScriptedReceiveItem:
                    if (DeliverNpcArea != MapAreaName.Internal_None && DeliverNpcName != null)
                        return new QuestObjectiveScriptedReceiveItem(Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour, DeliverNpcArea, DeliverNpcId, DeliverNpcName, RawItemName);
                    else
                        return this;

                default:
                    return this;
            }
        }

        public static readonly Dictionary<QuestObjectiveKillTarget, string> KillTargetStringMap = new Dictionary<QuestObjectiveKillTarget, string>()
        {
            { QuestObjectiveKillTarget.Any, "*" },
        };

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Type", new FieldParser() { Type = FieldType.String, ParserString = (string value, ParseErrorInfo errorInfo) => { Type = StringToEnumConversion<QuestObjectiveType>.Parse(value, errorInfo); }} },
            { "Target", new FieldParser() { Type = FieldType.String, ParserString = ParseTarget } },
            { "Description", new FieldParser() { Type = FieldType.String, ParserString = (string value, ParseErrorInfo errorInfo) => { Description = value; }} },
            { "Number", new FieldParser() { Type = FieldType.Integer, ParserInteger = ParseNumber } },
            { "InteractionFlags", new FieldParser() { Type = FieldType.StringArray, ParserStringArray = ParseInteractionFlags } },
            { "ItemName", new FieldParser() { Type = FieldType.String, ParserString = ParseItemName } },
            { "MustCompleteEarlierObjectivesFirst", new FieldParser() { Type = FieldType.Bool, ParserBool = (bool value, ParseErrorInfo errorInfo) => { RawMustCompleteEarlierObjectivesFirst = value; }} },
            { "InteractionFlag", new FieldParser() { Type = FieldType.String, ParserString = ParseInteractionFlag } },
            { "MinAmount", new FieldParser() { Type = FieldType.String, ParserString = ParseMinAmount } },
            { "MinFavorReceived", new FieldParser() { Type = FieldType.String, ParserString = ParseMinFavorReceived } },
            { "MaxFavorReceived", new FieldParser() { Type = FieldType.String, ParserString = ParseMaxFavorReceived } },
            { "Skill", new FieldParser() { Type = FieldType.String, ParserString = ParseSkill } },
            { "StringParam", new FieldParser() { Type = FieldType.String, ParserString = ParseStringParam } },
            { "ResultItemKeyword", new FieldParser() { Type = FieldType.String, ParserString = ParseResultItemKeyword } },
            { "AbilityKeyword", new FieldParser() { Type = FieldType.String, ParserString = ParseAbilityKeyword } },
            { "MaxAmount", new FieldParser() { Type = FieldType.String, ParserString = ParseMaxAmount } },
            { "AnatomyType", new FieldParser() { Type = FieldType.String, ParserString = ParseAnatomyType } },
            { "ItemKeyword", new FieldParser() { Type = FieldType.String, ParserString = ParseItemKeyword } },
            { "MonsterTypeTag", new FieldParser() { Type = FieldType.String, ParserString = ParseMonsterTypeTag } },
            { "Requirements", new FieldParser() { Type = FieldType.Object, ParserObject = ParseRequirements } },
            { "Item", new FieldParser() { Type = FieldType.String, ParserString = ParseItem } },
            { "NumToDeliver", new FieldParser() { Type = FieldType.Integer, ParserInteger = ParseNumToDeliver } },
        }; } }

        private void ParseTarget(string RawTarget, ParseErrorInfo ErrorInfo)
        {
            if (Type == QuestObjectiveType.Kill || Type == QuestObjectiveType.KillElite)
                KillTarget = StringToEnumConversion<QuestObjectiveKillTarget>.Parse(RawTarget, KillTargetStringMap, ErrorInfo);

            else if (Type == QuestObjectiveType.Deliver)
            {
                MapAreaName ParsedArea;
                string ParsedNpcId;
                string ParsedNpcName;
                if (Quest.TryParseNPC(RawTarget, out ParsedArea, out ParsedNpcId, out ParsedNpcName, ErrorInfo))
                {
                    DeliverNpcArea = ParsedArea;
                    DeliverNpcId = ParsedNpcId;
                    DeliverNpcName = ParsedNpcName;
                }
                else
                    ErrorInfo.AddInvalidObjectFormat("QuestObjective Target (for deliver)");
            }

            else if (Type == QuestObjectiveType.ScriptedReceiveItem)
            {
                MapAreaName ParsedArea;
                string ParsedNpcId;
                string ParsedNpcName;
                if (Quest.TryParseNPC(RawTarget, out ParsedArea, out ParsedNpcId, out ParsedNpcName, ErrorInfo))
                {
                    DeliverNpcArea = ParsedArea;
                    DeliverNpcId = ParsedNpcId;
                    DeliverNpcName = ParsedNpcName;
                }
                else
                    ErrorInfo.AddInvalidObjectFormat("QuestObjective Target (for scripted receive item)");
            }

            else if (Type == QuestObjectiveType.GuildGiveItem)
            {
                string ParsedNpcId;
                string ParsedNpcName;
                if (Quest.TryParseNPC(RawTarget, out ParsedNpcId, out ParsedNpcName, ErrorInfo))
                {
                    DeliverNpcId = ParsedNpcId;
                    DeliverNpcName = ParsedNpcName;
                }
                else
                    ErrorInfo.AddInvalidObjectFormat("QuestObjective Target (for Guild Give Item)");
            }

            else if (Type == QuestObjectiveType.Harvest ||
                Type == QuestObjectiveType.Collect ||
                Type == QuestObjectiveType.Have ||
                Type == QuestObjectiveType.Loot ||
                Type == QuestObjectiveType.UseItem)
                ItemTarget = StringToEnumConversion<ItemKeyword>.Parse(RawTarget, ErrorInfo);

            else if (Type == QuestObjectiveType.UseRecipe)
                RecipeTarget = StringToEnumConversion<RecipeKeyword>.Parse(RawTarget, ErrorInfo);

            else if (Type == QuestObjectiveType.UseAbility)
                AbilityTarget = StringToEnumConversion<AbilityKeyword>.Parse(RawTarget, ErrorInfo);

            else if (Type == QuestObjectiveType.Special ||
                Type == QuestObjectiveType.UniqueSpecial ||
                Type == QuestObjectiveType.GuildKill ||
                Type == QuestObjectiveType.DruidKill ||
                Type == QuestObjectiveType.DruidScripted ||
                Type == QuestObjectiveType.InteractionFlag ||
                Type == QuestObjectiveType.SayInChat)
                return;

            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective Target (Type)");
        }

        private void ParseNumber(int value, ParseErrorInfo ErrorInfo)
        {
            if (value != 1)
                RawNumber = value;
        }

        private bool ParseInteractionFlags(string RawInteractionFlag, ParseErrorInfo ErrorInfo)
        {
            if (Type == QuestObjectiveType.MultipleInteractionFlags)
            {
                RawInteractionFlagList.Add(RawInteractionFlag);
                return true;
            }
            else
            {
                ErrorInfo.AddInvalidObjectFormat("QuestObjective InteractionFlags (Type)");
                return false;
            }
        }

        private void ParseItemName(string RawItemName, ParseErrorInfo ErrorInfo)
        {
            if (Type == QuestObjectiveType.Collect ||
                Type == QuestObjectiveType.Deliver ||
                Type == QuestObjectiveType.Have ||
                Type == QuestObjectiveType.Harvest ||
                Type == QuestObjectiveType.UseItem ||
                Type == QuestObjectiveType.Loot ||
                Type == QuestObjectiveType.GuildGiveItem)
                this.RawItemName = RawItemName;
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective ItemName (Type)");
        }

        private void ParseInteractionFlag(string RawInteractionFlag, ParseErrorInfo ErrorInfo)
        {
            if (Type == QuestObjectiveType.InteractionFlag)
                this.RawInteractionFlag = RawInteractionFlag;
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective InteractionFlag (Type)");
        }

        private void ParseMinAmount(string RawMinAmount, ParseErrorInfo ErrorInfo)
        {
            if (Type == QuestObjectiveType.Special || Type == QuestObjectiveType.TipPlayer)
            {
                int ParsedMinAmount;
                if (int.TryParse(RawMinAmount, out ParsedMinAmount))
                    this.RawMinAmount = ParsedMinAmount;
                else
                    ErrorInfo.AddInvalidObjectFormat("QuestObjective MinAmount");
            }
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective MinAmount (Type)");
        }

        private void ParseMinFavorReceived(string RawMinFavorReceived, ParseErrorInfo ErrorInfo)
        {
            if (Type == QuestObjectiveType.GiveGift)
            {
                float ParsedMinFavorReceived;
                FloatFormat Format;
                if (Tools.TryParseFloat(RawMinFavorReceived, out ParsedMinFavorReceived, out Format))
                    this.RawMinFavorReceived = ParsedMinFavorReceived;
                else
                    ErrorInfo.AddInvalidObjectFormat("QuestObjective MinFavorReceived");
            }
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective MinFavorReceived (Type)");
        }

        private void ParseMaxFavorReceived(string RawMaxFavorReceived, ParseErrorInfo ErrorInfo)
        {
            if (Type == QuestObjectiveType.GiveGift)
            {
                float ParsedMaxFavorReceived;
                FloatFormat Format;
                if (Tools.TryParseFloat(RawMaxFavorReceived, out ParsedMaxFavorReceived, out Format))
                    this.RawMaxFavorReceived = ParsedMaxFavorReceived;
                else
                    ErrorInfo.AddInvalidObjectFormat("QuestObjective MaxFavorReceived");
            }
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective MaxFavorReceived (Type)");
        }

        private void ParseSkill(string RawSkill, ParseErrorInfo ErrorInfo)
        {
            if (Type == QuestObjectiveType.UseRecipe)
                Skill = StringToEnumConversion<PowerSkill>.Parse(RawSkill, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective Skill (Type)");
        }

        private void ParseStringParam(string RawStringParam, ParseErrorInfo ErrorInfo)
        {
            if (Type == QuestObjectiveType.Special)
                StringParam = RawStringParam;
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective StringParam (Type");
        }

        private void ParseResultItemKeyword(string RawResultItemKeyword, ParseErrorInfo ErrorInfo)
        {
            if (Type == QuestObjectiveType.UseRecipe)
                ResultItemKeyword = StringToEnumConversion<ItemKeyword>.Parse(RawResultItemKeyword, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective ResultItemKeyword (Type)");
        }

        private void ParseAbilityKeyword(string RawAbilityKeyword, ParseErrorInfo ErrorInfo)
        {
            if (Type == QuestObjectiveType.Kill)
                AbilityKeyword = RawAbilityKeyword;
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective AbilityKeyword (Type)");
        }

        private void ParseMaxAmount(string RawMaxAmount, ParseErrorInfo ErrorInfo)
        {
            if (Type == QuestObjectiveType.Special)
            {
                int ParsedMaxAmount;
                if (int.TryParse(RawMaxAmount, out ParsedMaxAmount))
                    this.RawMaxAmount = ParsedMaxAmount;
                else
                    ErrorInfo.AddInvalidObjectFormat("QuestObjective MaxAmount");
            }
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective MaxAmount (Type)");
        }

        private void ParseAnatomyType(string RawAnatomyType, ParseErrorInfo ErrorInfo)
        {
            if (Type == QuestObjectiveType.BeAttacked || Type == QuestObjectiveType.Bury)
                AnatomyType = StringToEnumConversion<PowerSkill>.Parse("Anatomy_" + RawAnatomyType, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective AnatomyType (Type)");
        }

        private void ParseItemKeyword(string RawItemKeyword, ParseErrorInfo ErrorInfo)
        {
            if (Type == QuestObjectiveType.GuildGiveItem)
                ItemKeyword = StringToEnumConversion<ItemKeyword>.Parse(RawItemKeyword, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective ItemKeyword (Type)");
        }

        private void ParseMonsterTypeTag(string RawMonsterTypeTag, ParseErrorInfo ErrorInfo)
        {
            if (Type == QuestObjectiveType.Loot)
                MonsterTypeTag = StringToEnumConversion<MonsterTypeTag>.Parse(RawMonsterTypeTag, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective MonsterTypeTag (Type)");
        }

        private void ParseRequirements(JsonObject RawRequirement, ParseErrorInfo ErrorInfo)
        {
            if (RawRequirement.Has("T"))
            {
                JsonString AsJsonString;
                if ((AsJsonString = RawRequirement["T"] as JsonString) != null)
                {
                    string RequirementType = AsJsonString.String;
                    if (RequirementType == "TimeOfDay")
                    {
                        if (RawRequirement.Has("MinHour") && RawRequirement.Has("MaxHour"))
                        {
                            JsonInteger MinHourJValue;
                            JsonInteger MaxHourJValue;
                            if (((MinHourJValue = RawRequirement["MinHour"] as JsonInteger) != null) && ((MaxHourJValue = RawRequirement["MaxHour"] as JsonInteger) != null))
                            {
                                MinHour = MinHourJValue.Number;
                                MaxHour = MaxHourJValue.Number;
                            }
                            else
                                ErrorInfo.AddInvalidObjectFormat("QuestObjective Requirements");
                        }
                        else
                            ErrorInfo.AddInvalidObjectFormat("QuestObjective Requirements");
                    }
                    else if (RequirementType == "HasEffectKeyword")
                    {
                        if (RawRequirement.Has("Keyword"))
                        {
                            JsonString KeywordJValue;
                            if ((KeywordJValue = RawRequirement["Keyword"] as JsonString) != null)
                            {
                                if (StringToEnumConversion<EffectKeyword>.TryParse(KeywordJValue.String, out EffectKeyword ParsedEffectKeyword, ErrorInfo))
                                    EffectRequirement = ParsedEffectKeyword;
                                else
                                    ErrorInfo.AddInvalidObjectFormat("QuestObjective Requirements");
                            }
                            else
                                ErrorInfo.AddInvalidObjectFormat("QuestObjective Requirements");
                        }
                        else
                            ErrorInfo.AddInvalidObjectFormat("QuestObjective Requirements");
                    }
                    else
                        ErrorInfo.AddInvalidObjectFormat("QuestObjective Requirements");
                }
                else
                    ErrorInfo.AddInvalidObjectFormat("QuestObjective Requirements");
            }
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective Requirements");
        }

        private void ParseItem(string RawItemName, ParseErrorInfo ErrorInfo)
        {
            if (Type == QuestObjectiveType.ScriptedReceiveItem)
                this.RawItemName = RawItemName;
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective Item (Type)");
        }

        private void ParseNumToDeliver(int value, ParseErrorInfo ErrorInfo)
        {
            if (Type == QuestObjectiveType.Deliver)
                RawNumToDeliver = value;
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective NumToDeliver (Type)");
        }
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
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, Description);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = false;

            ParentQuest = Parent as Quest;

            return IsConnected;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "QuestObjective"; } }
        #endregion
    }
}
