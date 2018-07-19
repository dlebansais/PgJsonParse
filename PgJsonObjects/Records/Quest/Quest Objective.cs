using PgJsonReader;
using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestObjective : GenericJsonObject<QuestObjective>, IPgQuestObjective, ISpecificRecord
    {
        #region Init
        public QuestObjective()
        {
        }

        public QuestObjective(QuestObjectiveType Type, string Description, int? RawNumber, bool? RawMustCompleteEarlierObjectivesFirst)
        {
            this.Type = Type;
            this.Description = Description;
            this.RawNumber = RawNumber;
            this.RawMustCompleteEarlierObjectivesFirst = RawMustCompleteEarlierObjectivesFirst;
        }
        #endregion

        #region Direct Properties
        public QuestObjectiveType Type { get; private set; }
        public string Description { get; private set; }
        public int Number { get { return RawNumber.HasValue ? RawNumber.Value : 1; } }
        public bool HasNumber { get { return RawNumber.HasValue && RawNumber.Value != 1; } }
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
        private string InteractionTarget;
        protected QuestObjectiveRequirement QuestObjectiveRequirement;
        private int? RawNumToDeliver;
        #endregion

        #region Indirect Properties
        public override string SortingName { get { return null; } }
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
            IPgQuestObjective Result = ToSpecificQuestObjective(errorInfo);

            if (Result != null && Result != this)
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

        public IPgQuestObjective ToSpecificQuestObjective(ParseErrorInfo ErrorInfo)
        {
            switch (Type)
            {
                case QuestObjectiveType.Kill:
                case QuestObjectiveType.KillElite:
                    return new QuestObjectiveKill(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, KillTarget, AbilityKeyword, EffectRequirement, QuestObjectiveRequirement);

                case QuestObjectiveType.TipPlayer:
                    return new QuestObjectiveTipPlayer(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, RawMinAmount);

                case QuestObjectiveType.Special:
                    return new QuestObjectiveSpecial(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, RawMinAmount, RawMaxAmount, StringParam, InteractionTarget, QuestObjectiveRequirement);

                case QuestObjectiveType.MultipleInteractionFlags:
                    return new QuestObjectiveMultipleInteractionFlags(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, RawInteractionFlagList);

                case QuestObjectiveType.Deliver:
                    if (DeliverNpcArea != MapAreaName.Internal_None && DeliverNpcName != null)
                        return new QuestObjectiveDeliver(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, DeliverNpcArea, DeliverNpcId, DeliverNpcName, RawItemName, RawNumToDeliver);
                    else
                        return this;

                case QuestObjectiveType.Collect:
                case QuestObjectiveType.Have:
                case QuestObjectiveType.Harvest:
                case QuestObjectiveType.UseItem:
                    if (RawItemName != null && ItemTarget == ItemKeyword.Internal_None)
                        return new QuestObjectiveItem(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, RawItemName, QuestObjectiveRequirement);
                    else if (RawItemName == null && ItemTarget != ItemKeyword.Internal_None)
                        return new QuestObjectiveItem(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, ItemTarget, QuestObjectiveRequirement);
                    else
                        return this;

                case QuestObjectiveType.GuildGiveItem:
                    if (RawItemName != null && ItemKeyword == ItemKeyword.Internal_None)
                        return new QuestObjectiveGuildGiveItem(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, DeliverNpcId, DeliverNpcName, RawItemName);
                    else if (RawItemName == null && ItemKeyword != ItemKeyword.Internal_None)
                        return new QuestObjectiveGuildGiveItem(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, DeliverNpcId, DeliverNpcName, ItemKeyword);
                    else
                        return this;

                case QuestObjectiveType.InteractionFlag:
                    return new QuestObjectiveInteractionFlag(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, RawInteractionFlag, InteractionTarget);

                case QuestObjectiveType.GiveGift:
                    return new QuestObjectiveGiveGift(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, RawMinFavorReceived, RawMaxFavorReceived);

                case QuestObjectiveType.UseRecipe:
                    return new QuestObjectiveUseRecipe(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, Skill, RecipeTarget, ResultItemKeyword);

                case QuestObjectiveType.BeAttacked:
                case QuestObjectiveType.Bury:
                    return new QuestObjectiveAnatomy(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, AnatomyType);

                case QuestObjectiveType.UseAbility:
                    return new QuestObjectiveUseAbility(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, AbilityTarget);

                case QuestObjectiveType.Loot:
                    if (RawItemName != null && ItemTarget == ItemKeyword.Internal_None)
                        return new QuestObjectiveLoot(Type, Description, RawItemName, RawNumber, MonsterTypeTag, RawMustCompleteEarlierObjectivesFirst);
                    else if (RawItemName == null && ItemTarget != ItemKeyword.Internal_None)
                        return new QuestObjectiveLoot(Type, Description, ItemTarget, RawNumber, MonsterTypeTag, RawMustCompleteEarlierObjectivesFirst);
                    else
                        return this;

                case QuestObjectiveType.Scripted:
                case QuestObjectiveType.SayInChat:
                case QuestObjectiveType.UniqueSpecial:
                case QuestObjectiveType.GuildKill:
                case QuestObjectiveType.DruidKill:
                case QuestObjectiveType.DruidScripted:
                    return new QuestObjectiveSimple(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, InteractionTarget);

                case QuestObjectiveType.ScriptedReceiveItem:
                    if (DeliverNpcArea != MapAreaName.Internal_None && DeliverNpcName != null)
                        return new QuestObjectiveScriptedReceiveItem(Type, Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, DeliverNpcArea, DeliverNpcId, DeliverNpcName, RawItemName);
                    else
                        return this;

                default:
                    return null;
            }
        }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Type", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Type = StringToEnumConversion<QuestObjectiveType>.Parse(value, errorInfo),
                GetString = () => StringToEnumConversion<QuestObjectiveType>.ToString(Type, null, QuestObjectiveType.Internal_None) } },
            { "Target", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseTarget,
                GetString = () => null } },
            { "Description", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Description = value,
                GetString = () => Description } },
            { "Number", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = ParseNumber,
                GetInteger = () => RawNumber } },
            { "InteractionFlags", new FieldParser() {
                Type = FieldType.StringArray,
                ParseStringArray = ParseInteractionFlags,
                GetStringArray = () => RawInteractionFlagList } },
            { "ItemName", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseItemName,
                GetString = () => RawItemName } },
            { "MustCompleteEarlierObjectivesFirst", new FieldParser() {
                Type = FieldType.Bool,
                ParseBool = (bool value, ParseErrorInfo errorInfo) => RawMustCompleteEarlierObjectivesFirst = value,
                GetBool = () => RawMustCompleteEarlierObjectivesFirst } },
            { "InteractionFlag", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseInteractionFlag,
                GetString = () => RawInteractionFlag } },
            { "MinAmount", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseMinAmount,
                GetString = () => RawMinAmount.ToString() } },
            { "MinFavorReceived", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseMinFavorReceived,
                GetString = () => RawMinFavorReceived.ToString() } },
            { "MaxFavorReceived", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseMaxFavorReceived,
                GetString = () => RawMaxFavorReceived.ToString() } },
            { "Skill", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseSkill,
                GetString = () => StringToEnumConversion<PowerSkill>.ToString(Skill, null, PowerSkill.Internal_None) } },
            { "StringParam", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseStringParam,
                GetString = () => StringParam } },
            { "ResultItemKeyword", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseResultItemKeyword,
                GetString = () => StringToEnumConversion<ItemKeyword>.ToString(ResultItemKeyword, null, ItemKeyword.Internal_None) } },
            { "AbilityKeyword", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseAbilityKeyword,
                GetString = () => AbilityKeyword } },
            { "MaxAmount", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseMaxAmount,
                GetString = () => RawMaxAmount.ToString() } },
            { "AnatomyType", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseAnatomyType,
                GetString = () => StringToEnumConversion<PowerSkill>.ToString(AnatomyType, null, PowerSkill.Internal_None)} },
            { "ItemKeyword", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseItemKeyword,
                GetString = () => StringToEnumConversion<ItemKeyword>.ToString(ItemKeyword, null, ItemKeyword.Internal_None) } },
            { "MonsterTypeTag", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseMonsterTypeTag,
                GetString = () => StringToEnumConversion<MonsterTypeTag>.ToString(MonsterTypeTag, null, MonsterTypeTag.Internal_None) } },
            { "Requirements", new FieldParser() {
                Type = FieldType.Object,
                ParseObject = ParseRequirements,
                GetObject = () => QuestObjectiveRequirement } },
            { "Item", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseItem,
                GetString = () => RawItemName } },
            { "NumToDeliver", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = ParseNumToDeliver,
                GetInteger = () => RawNumToDeliver } },
        }; } }

        private void ParseTarget(string RawTarget, ParseErrorInfo ErrorInfo)
        {
            if (Type == QuestObjectiveType.Kill || Type == QuestObjectiveType.KillElite)
                KillTarget = StringToEnumConversion<QuestObjectiveKillTarget>.Parse(RawTarget, TextMaps.KillTargetStringMap, ErrorInfo);

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
                     Type == QuestObjectiveType.InteractionFlag ||
                     Type == QuestObjectiveType.GuildKill ||
                     Type == QuestObjectiveType.DruidKill ||
                     Type == QuestObjectiveType.DruidScripted ||
                     Type == QuestObjectiveType.SayInChat)
                InteractionTarget = RawTarget;

            else if (Type == QuestObjectiveType.InteractionFlag)
                return;

            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective Target (Type)");
        }

        private void ParseNumber(int value, ParseErrorInfo ErrorInfo)
        {
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

        private void ParseItemName(string value, ParseErrorInfo ErrorInfo)
        {
            if (Type == QuestObjectiveType.Collect ||
                Type == QuestObjectiveType.Deliver ||
                Type == QuestObjectiveType.Have ||
                Type == QuestObjectiveType.Harvest ||
                Type == QuestObjectiveType.UseItem ||
                Type == QuestObjectiveType.Loot ||
                Type == QuestObjectiveType.GuildGiveItem)
                RawItemName = value;
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective ItemName (Type)");
        }

        private void ParseInteractionFlag(string value, ParseErrorInfo ErrorInfo)
        {
            if (Type == QuestObjectiveType.InteractionFlag)
                RawInteractionFlag = value;
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective InteractionFlag (Type)");
        }

        private void ParseMinAmount(string value, ParseErrorInfo ErrorInfo)
        {
            if (Type == QuestObjectiveType.Special || Type == QuestObjectiveType.TipPlayer)
            {
                int ParsedMinAmount;
                if (int.TryParse(value, out ParsedMinAmount))
                    RawMinAmount = ParsedMinAmount;
                else
                    ErrorInfo.AddInvalidObjectFormat("QuestObjective MinAmount");
            }
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective MinAmount (Type)");
        }

        private void ParseMinFavorReceived(string value, ParseErrorInfo ErrorInfo)
        {
            if (Type == QuestObjectiveType.GiveGift)
            {
                float ParsedMinFavorReceived;
                FloatFormat Format;
                if (Tools.TryParseFloat(value, out ParsedMinFavorReceived, out Format))
                    RawMinFavorReceived = ParsedMinFavorReceived;
                else
                    ErrorInfo.AddInvalidObjectFormat("QuestObjective MinFavorReceived");
            }
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective MinFavorReceived (Type)");
        }

        private void ParseMaxFavorReceived(string value, ParseErrorInfo ErrorInfo)
        {
            if (Type == QuestObjectiveType.GiveGift)
            {
                float ParsedMaxFavorReceived;
                FloatFormat Format;
                if (Tools.TryParseFloat(value, out ParsedMaxFavorReceived, out Format))
                    RawMaxFavorReceived = ParsedMaxFavorReceived;
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

        private void ParseStringParam(string value, ParseErrorInfo ErrorInfo)
        {
            if (Type == QuestObjectiveType.Special)
                StringParam = value;
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective StringParam (Type");
        }

        private void ParseResultItemKeyword(string value, ParseErrorInfo ErrorInfo)
        {
            if (Type == QuestObjectiveType.UseRecipe)
                ResultItemKeyword = StringToEnumConversion<ItemKeyword>.Parse(value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective ResultItemKeyword (Type)");
        }

        private void ParseAbilityKeyword(string value, ParseErrorInfo ErrorInfo)
        {
            if (Type == QuestObjectiveType.Kill)
                AbilityKeyword = value;
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective AbilityKeyword (Type)");
        }

        private void ParseMaxAmount(string value, ParseErrorInfo ErrorInfo)
        {
            if (Type == QuestObjectiveType.Special)
            {
                int ParsedMaxAmount;
                if (int.TryParse(value, out ParsedMaxAmount))
                    RawMaxAmount = ParsedMaxAmount;
                else
                    ErrorInfo.AddInvalidObjectFormat("QuestObjective MaxAmount");
            }
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective MaxAmount (Type)");
        }

        private void ParseAnatomyType(string value, ParseErrorInfo ErrorInfo)
        {
            if (Type == QuestObjectiveType.BeAttacked || Type == QuestObjectiveType.Bury)
                AnatomyType = StringToEnumConversion<PowerSkill>.Parse("Anatomy_" + value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective AnatomyType (Type)");
        }

        private void ParseItemKeyword(string value, ParseErrorInfo ErrorInfo)
        {
            if (Type == QuestObjectiveType.GuildGiveItem)
                ItemKeyword = StringToEnumConversion<ItemKeyword>.Parse(value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective ItemKeyword (Type)");
        }

        private void ParseMonsterTypeTag(string value, ParseErrorInfo ErrorInfo)
        {
            if (Type == QuestObjectiveType.Loot)
                MonsterTypeTag = StringToEnumConversion<MonsterTypeTag>.Parse(value, ErrorInfo);
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

                                QuestObjectiveRequirement NewRequirement = new QuestObjectiveRequirement(RequirementType);
                                NewRequirement.RawMinHour = MinHour;
                                NewRequirement.RawMaxHour = MaxHour;
                                NewRequirement.AddFieldTableOrder("T");
                                NewRequirement.AddFieldTableOrder("MinHour");
                                NewRequirement.AddFieldTableOrder("MaxHour");
                                QuestObjectiveRequirement = NewRequirement;
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
                                {
                                    EffectRequirement = ParsedEffectKeyword;

                                    QuestObjectiveRequirement NewRequirement = new QuestObjectiveRequirement(RequirementType);
                                    NewRequirement.Keyword = EffectRequirement;
                                    NewRequirement.AddFieldTableOrder("T");
                                    NewRequirement.AddFieldTableOrder("Keyword");
                                    QuestObjectiveRequirement = NewRequirement;
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
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective Requirements");
        }

        private void ParseItem(string value, ParseErrorInfo ErrorInfo)
        {
            if (Type == QuestObjectiveType.ScriptedReceiveItem)
                RawItemName = value;
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
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, IBackLinkable Parent, Dictionary<Type, Dictionary<string, IJsonKey>> AllTables)
        {
            bool IsConnected = false;

            return IsConnected;
        }

        public IList<IBackLinkable> GetLinkBack()
        {
            return null;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "QuestObjective"; } }
        #endregion

        #region Serializing
        protected void SerializeJsonObjectInternalProlog(byte[] data, ref int offset, Dictionary<int, string> StoredStringtable, Dictionary<int, List<string>> StoredStringListTable)
        {
            int BitOffset = 0;
            int BaseOffset = offset;

            AddInt((int)Type, data, ref offset, BaseOffset, 0);
            AddString(Key, data, ref offset, BaseOffset, 4, StoredStringtable);
            AddString(Description, data, ref offset, BaseOffset, 8, StoredStringtable);
            AddInt(RawNumber, data, ref offset, BaseOffset, 12);
            AddStringList(FieldTableOrder, data, ref offset, BaseOffset, 16, StoredStringListTable);
            AddBool(RawMustCompleteEarlierObjectivesFirst, data, ref offset, ref BitOffset, BaseOffset, 20, 0);
            CloseBool(ref offset, ref BitOffset);
            offset += 2;
        }

        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            throw new InvalidOperationException();
        }
        #endregion
    }
}
