using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestObjective : GenericJsonObject<QuestObjective>
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
        private List<string> InteractionFlagList = new List<string>();
        private string RawItemName;
        private string InteractionFlag;
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
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return null; } }
        public Quest ParentQuest { get; private set; }
        #endregion

        #region Parsing
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
                    return new QuestObjectiveMultipleInteractionFlags(Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour, InteractionFlagList);

                case QuestObjectiveType.Deliver:
                    if (DeliverNpcArea != MapAreaName.Internal_None && DeliverNpcName != null)
                        return new QuestObjectiveDeliver(Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour, DeliverNpcArea, DeliverNpcId, DeliverNpcName, RawItemName);
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
                    return new QuestObjectiveInteractionFlag(Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour, InteractionFlag);

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

                default:
                    return this;
            }
        }

        public static readonly Dictionary<QuestObjectiveKillTarget, string> KillTargetStringMap = new Dictionary<QuestObjectiveKillTarget, string>()
        {
            { QuestObjectiveKillTarget.Any, "*" },
        };

        protected override Dictionary<string, FieldValueHandler> FieldTable { get; } = new Dictionary<string, FieldValueHandler>()
        {
            { "Type", ParseFieldType },
            { "Target", ParseFieldTarget },
            { "Description", ParseFieldDescription },
            { "Number", ParseFieldNumber },
            { "InteractionFlags", ParseFieldInteractionFlags },
            { "ItemName", ParseFieldItemName },
            { "MustCompleteEarlierObjectivesFirst", ParseFieldMustCompleteEarlierObjectivesFirst },
            { "InteractionFlag", ParseFieldInteractionFlag },
            { "MinAmount", ParseFieldMinAmount },
            { "MinFavorReceived", ParseFieldMinFavorReceived },
            { "MaxFavorReceived", ParseFieldMaxFavorReceived },
            { "Skill", ParseFieldSkill },
            { "StringParam", ParseFieldStringParam },
            { "ResultItemKeyword", ParseFieldResultItemKeyword },
            { "AbilityKeyword", ParseFieldAbilityKeyword },
            { "MaxAmount", ParseFieldMaxAmount },
            { "AnatomyType", ParseFieldAnatomyType },
            { "ItemKeyword", ParseFieldItemKeyword },
            { "Requirements", ParseFieldRequirements },
            { "MonsterTypeTag", ParseFieldMonsterTypeTag },
        };

        private static void ParseFieldType(QuestObjective This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawType;
            if ((RawType = Value as string) != null)
                This.ParseType(RawType, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective Type");
        }

        private void ParseType(string RawType, ParseErrorInfo ErrorInfo)
        {
            QuestObjectiveType ParsedType;
            StringToEnumConversion<QuestObjectiveType>.TryParse(RawType, out ParsedType, ErrorInfo);
            Type = ParsedType;
        }

        private static void ParseFieldTarget(QuestObjective This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawTarget;
            if ((RawTarget = Value as string) != null)
                This.ParseTarget(RawTarget, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective Target");
        }

        private void ParseTarget(string RawTarget, ParseErrorInfo ErrorInfo)
        {
            if (Type == QuestObjectiveType.Kill || Type == QuestObjectiveType.KillElite)
            {
                QuestObjectiveKillTarget ParsedTarget;
                StringToEnumConversion<QuestObjectiveKillTarget>.TryParse(RawTarget, KillTargetStringMap, out ParsedTarget, ErrorInfo);
                KillTarget = ParsedTarget;
            }

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
            {
                ItemKeyword ParsedTarget;
                StringToEnumConversion<ItemKeyword>.TryParse(RawTarget, out ParsedTarget, ErrorInfo);
                ItemTarget = ParsedTarget;
            }

            else if (Type == QuestObjectiveType.UseRecipe)
            {
                RecipeKeyword ParsedTarget;
                StringToEnumConversion<RecipeKeyword>.TryParse(RawTarget, out ParsedTarget, ErrorInfo);
                RecipeTarget = ParsedTarget;
            }

            else if (Type == QuestObjectiveType.UseAbility)
            {
                AbilityKeyword ParsedTarget;
                StringToEnumConversion<AbilityKeyword>.TryParse(RawTarget, out ParsedTarget, ErrorInfo);
                AbilityTarget = ParsedTarget;
            }

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

        private static void ParseFieldDescription(QuestObjective This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawDescription;
            if ((RawDescription = Value as string) != null)
                This.ParseDescription(RawDescription, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective Description");
        }

        private void ParseDescription(string RawDescription, ParseErrorInfo ErrorInfo)
        {
            Description = RawDescription;
        }

        private static void ParseFieldNumber(QuestObjective This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseNumber((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective Number");
        }

        private void ParseNumber(int RawNumber, ParseErrorInfo ErrorInfo)
        {
            if (RawNumber != 1)
                this.RawNumber = RawNumber;
        }

        private static void ParseFieldInteractionFlags(QuestObjective This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawInteractionFlags;
            if ((RawInteractionFlags = Value as ArrayList) != null)
                This.ParseInteractionFlags(RawInteractionFlags, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective InteractionFlags");
        }

        private void ParseInteractionFlags(ArrayList RawInteractionFlags, ParseErrorInfo ErrorInfo)
        {
            if (Type == QuestObjectiveType.MultipleInteractionFlags)
            {
                foreach (object RawInteractionFlag in RawInteractionFlags)
                {
                    string AsString;
                    if ((AsString = RawInteractionFlag as string) != null)
                        InteractionFlagList.Add(AsString);
                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("QuestObjective InteractionFlags");
                        break;
                    }
                }
            }
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective InteractionFlags (Type)");
        }

        private static void ParseFieldItemName(QuestObjective This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawItemName;
            if ((RawItemName = Value as string) != null)
                This.ParseItemName(RawItemName, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective ItemName");
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

        private static void ParseFieldMustCompleteEarlierObjectivesFirst(QuestObjective This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is bool)
                This.ParseMustCompleteEarlierObjectivesFirst((bool)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective MustCompleteEarlierObjectivesFirst");
        }

        private void ParseMustCompleteEarlierObjectivesFirst(bool RawMustCompleteEarlierObjectivesFirst, ParseErrorInfo ErrorInfo)
        {
            this.RawMustCompleteEarlierObjectivesFirst = RawMustCompleteEarlierObjectivesFirst;
        }

        private static void ParseFieldInteractionFlag(QuestObjective This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawInteractionFlag;
            if ((RawInteractionFlag = Value as string) != null)
                This.ParseInteractionFlag(RawInteractionFlag, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective InteractionFlag");
        }

        private void ParseInteractionFlag(string RawInteractionFlag, ParseErrorInfo ErrorInfo)
        {
            if (Type == QuestObjectiveType.InteractionFlag)
                InteractionFlag = RawInteractionFlag;
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective InteractionFlag (Type)");
        }

        private static void ParseFieldMinAmount(QuestObjective This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawMinAmount;
            if ((RawMinAmount = Value as string) != null)
                This.ParseMinAmount(RawMinAmount, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective MinAmount");
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

        private static void ParseFieldMinFavorReceived(QuestObjective This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawMinFavorReceived;
            if ((RawMinFavorReceived = Value as string) != null)
                This.ParseMinFavorReceived(RawMinFavorReceived, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective MinFavorReceived");
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

        private static void ParseFieldMaxFavorReceived(QuestObjective This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawMaxFavorReceived;
            if ((RawMaxFavorReceived = Value as string) != null)
                This.ParseMaxFavorReceived(RawMaxFavorReceived, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective MaxFavorReceived");
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

        private static void ParseFieldSkill(QuestObjective This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawSkill;
            if ((RawSkill = Value as string) != null)
                This.ParseSkill(RawSkill, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective Skill");
        }

        private void ParseSkill(string RawSkill, ParseErrorInfo ErrorInfo)
        {
            if (Type == QuestObjectiveType.UseRecipe)
            {
                PowerSkill ParsedSkill;
                StringToEnumConversion<PowerSkill>.TryParse(RawSkill, out ParsedSkill, ErrorInfo);
                Skill = ParsedSkill;
            }
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective Skill (Type)");
        }

        private static void ParseFieldStringParam(QuestObjective This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawStringParam;
            if ((RawStringParam = Value as string) != null)
                This.ParseStringParam(RawStringParam, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective StringParam");
        }

        private void ParseStringParam(string RawStringParam, ParseErrorInfo ErrorInfo)
        {
            if (Type == QuestObjectiveType.Special)
                StringParam = RawStringParam;
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective StringParam (Type");
        }

        private static void ParseFieldResultItemKeyword(QuestObjective This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawResultItemKeyword;
            if ((RawResultItemKeyword = Value as string) != null)
                This.ParseResultItemKeyword(RawResultItemKeyword, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective ResultItemKeyword");
        }

        private void ParseResultItemKeyword(string RawResultItemKeyword, ParseErrorInfo ErrorInfo)
        {
            if (Type == QuestObjectiveType.UseRecipe)
            {
                ItemKeyword ParsedResultItemKeyword;
                StringToEnumConversion<ItemKeyword>.TryParse(RawResultItemKeyword, out ParsedResultItemKeyword, ErrorInfo);
                ResultItemKeyword = ParsedResultItemKeyword;
            }
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective ResultItemKeyword (Type)");
        }

        private static void ParseFieldAbilityKeyword(QuestObjective This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawAbilityKeyword;
            if ((RawAbilityKeyword = Value as string) != null)
                This.ParseAbilityKeyword(RawAbilityKeyword, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective AbilityKeyword");
        }

        private void ParseAbilityKeyword(string RawAbilityKeyword, ParseErrorInfo ErrorInfo)
        {
            if (Type == QuestObjectiveType.Kill)
                AbilityKeyword = RawAbilityKeyword;
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective AbilityKeyword (Type)");
        }

        private static void ParseFieldMaxAmount(QuestObjective This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawMaxAmount;
            if ((RawMaxAmount = Value as string) != null)
                This.ParseMaxAmount(RawMaxAmount, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective MaxAmount");
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

        private static void ParseFieldAnatomyType(QuestObjective This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawAnatomyType;
            if ((RawAnatomyType = Value as string) != null)
                This.ParseAnatomyType(RawAnatomyType, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective AnatomyType");
        }

        private void ParseAnatomyType(string RawAnatomyType, ParseErrorInfo ErrorInfo)
        {
            if (Type == QuestObjectiveType.BeAttacked || Type == QuestObjectiveType.Bury)
            {
                PowerSkill ParsedAnatomyType;
                StringToEnumConversion<PowerSkill>.TryParse("Anatomy_" + RawAnatomyType, out ParsedAnatomyType, ErrorInfo);
                AnatomyType = ParsedAnatomyType;
            }
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective AnatomyType (Type)");
        }

        private static void ParseFieldItemKeyword(QuestObjective This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawItemKeyword;
            if ((RawItemKeyword = Value as string) != null)
                This.ParseItemKeyword(RawItemKeyword, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective ItemKeyword");
        }

        private void ParseItemKeyword(string RawItemKeyword, ParseErrorInfo ErrorInfo)
        {
            if (Type == QuestObjectiveType.GuildGiveItem)
            {
                ItemKeyword ParsedItemKeyword;
                StringToEnumConversion<ItemKeyword>.TryParse(RawItemKeyword, out ParsedItemKeyword, ErrorInfo);
                ItemKeyword = ParsedItemKeyword;
            }
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective ItemKeyword (Type)");
        }

        private static void ParseFieldRequirements(QuestObjective This, object Value, ParseErrorInfo ErrorInfo)
        {
            Dictionary<string, object> AsDictionary;
            if ((AsDictionary = Value as Dictionary<string, object>) != null)
                This.ParseRequirements(AsDictionary, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective Requirements");
        }

        private static void ParseFieldMonsterTypeTag(QuestObjective This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawMonsterTypeTag;
            if ((RawMonsterTypeTag = Value as string) != null)
                This.ParseMonsterTypeTag(RawMonsterTypeTag, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective MonsterTypeTag");
        }

        private void ParseMonsterTypeTag(string RawMonsterTypeTag, ParseErrorInfo ErrorInfo)
        {
            if (Type == QuestObjectiveType.Loot)
            {
                MonsterTypeTag ParsedMonsterTypeTag;
                StringToEnumConversion<MonsterTypeTag>.TryParse(RawMonsterTypeTag, out ParsedMonsterTypeTag, ErrorInfo);
                MonsterTypeTag = ParsedMonsterTypeTag;
            }
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective MonsterTypeTag (Type)");
        }

        private void ParseRequirements(Dictionary<string, object> RawRequirement, ParseErrorInfo ErrorInfo)
        {
            if (RawRequirement.ContainsKey("T"))
            {
                string RequirementType;
                if ((RequirementType = RawRequirement["T"] as string) != null)
                {
                    if (RequirementType == "TimeOfDay")
                    {
                        if (RawRequirement.ContainsKey("MinHour") && RawRequirement.ContainsKey("MaxHour"))
                        {
                            if ((RawRequirement["MinHour"] is int) && (RawRequirement["MaxHour"] is int))
                            {
                                MinHour = (int)RawRequirement["MinHour"];
                                MaxHour = (int)RawRequirement["MaxHour"];
                            }
                            else
                                ErrorInfo.AddInvalidObjectFormat("QuestObjective Requirements");
                        }
                        else
                            ErrorInfo.AddInvalidObjectFormat("QuestObjective Requirements");
                    }
                    else if (RequirementType == "HasEffectKeyword")
                    {
                        if (RawRequirement.ContainsKey("Keyword"))
                        {
                            string EffectKeyword = RawRequirement["Keyword"] as string;
                            EffectKeyword ParsedEffectKeyword;
                            if (StringToEnumConversion<EffectKeyword>.TryParse(EffectKeyword, out ParsedEffectKeyword, ErrorInfo))
                            {
                                EffectRequirement = ParsedEffectKeyword;
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
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable, Dictionary<string, XpTable> XpTableTable, Dictionary<string, AdvancementTable> AdvancementTableTable, Dictionary<string, GameNpc> GameNpcTable, Dictionary<string, StorageVault> StorageVaultTable, Dictionary<string, AbilitySource> AbilitySourceTable)
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
