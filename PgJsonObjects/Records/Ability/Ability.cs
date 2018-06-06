using PgJsonReader;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class Ability : GenericJsonObject<Ability>
    {
        #region Direct Properties
        public AbilityAnimation Animation { get; private set; }
        public Dictionary<string, Attribute> AttributesThatDeltaPowerCostTable { get; } = new Dictionary<string, Attribute>();
        public Dictionary<string, Attribute> AttributesThatDeltaResetTimeTable { get; } = new Dictionary<string, Attribute>();
        public Dictionary<string, Attribute> AttributesThatModPowerCostTable { get; } = new Dictionary<string, Attribute>();
        public bool CanBeOnSidebar { get { return RawCanBeOnSidebar.HasValue && RawCanBeOnSidebar.Value; } }
        public bool? RawCanBeOnSidebar { get; private set; }
        public bool CanSuppressMonsterShout { get { return RawCanSuppressMonsterShout.HasValue && RawCanSuppressMonsterShout.Value; } }
        public bool? RawCanSuppressMonsterShout { get; private set; }
        public bool CanTargetUntargetableEnemies { get { return RawCanTargetUntargetableEnemies.HasValue && RawCanTargetUntargetableEnemies.Value; } }
        public bool? RawCanTargetUntargetableEnemies { get; private set; }
        public List<Deaths> CausesOfDeathList { get; } = new List<Deaths>();
        public List<RecipeCost> CostList { get; } = new List<RecipeCost>();
        public int CombatRefreshBaseAmount { get { return RawCombatRefreshBaseAmount.HasValue ? RawCombatRefreshBaseAmount.Value : 0; } }
        public int? RawCombatRefreshBaseAmount { get; private set; }
        public PowerSkill CompatibleSkill { get; private set; }
        private List<PowerSkill> RawCompatibleSkillList { get; } = new List<PowerSkill>();
        private string RawConsumedItemKeyword;
        public Item ConsumedItemLink { get; private set; }
        private bool IsRawConsumedItemKeywordParsed;
        public double ConsumedItemChance { get { return RawConsumedItemChance.HasValue ? RawConsumedItemChance.Value : 0; } }
        private double? RawConsumedItemChance;
        public double ConsumedItemChanceToStickInCorpse { get { return RawConsumedItemChanceToStickInCorpse.HasValue ? RawConsumedItemChanceToStickInCorpse.Value : 0; } }
        private double? RawConsumedItemChanceToStickInCorpse;
        public int ConsumedItemCount { get { return RawConsumedItemCount.HasValue ? RawConsumedItemCount.Value : 0; } }
        private int? RawConsumedItemCount;
        public DamageType DamageType { get; private set; }
        public bool DelayLoopIsAbortedIfAttacked { get { return RawDelayLoopIsAbortedIfAttacked.HasValue && RawDelayLoopIsAbortedIfAttacked.Value; } }
        public bool? RawDelayLoopIsAbortedIfAttacked { get; private set; }
        public string DelayLoopMessage { get; private set; }
        public double DelayLoopTime { get { return RawDelayLoopTime.HasValue ? RawDelayLoopTime.Value : 0; } }
        public double? RawDelayLoopTime { get; private set; }
        public string Description { get; private set; }
        public AbilityIndicatingEnabled EffectKeywordsIndicatingEnabled { get; private set; }
        public int IconId { get { return RawIconId.HasValue ? RawIconId.Value : 0; } }
        private int? RawIconId;
        public bool InternalAbility { get { return RawInternalAbility.HasValue && RawInternalAbility.Value; } }
        public bool? RawInternalAbility { get; private set; }
        public string InternalName { get; private set; }
        public bool IsHarmless { get { return RawIsHarmless.HasValue && RawIsHarmless.Value; } }
        public bool? RawIsHarmless { get; private set; }
        public string ItemKeywordReqErrorMessage { get; private set; }
        public List<AbilityItemKeyword> ItemKeywordReqList { get; } = new List<AbilityItemKeyword>();
        public List<AbilityKeyword> KeywordList { get; } = new List<AbilityKeyword>();
        public int Level { get { return RawLevel.HasValue ? RawLevel.Value : 0; } }
        public int? RawLevel { get; private set; }
        public string Name { get; private set; }
        public AbilityPetType PetTypeTagReq { get; private set; }
        public int PetTypeTagReqMax { get; private set; }
        public int? RawPetTypeTagReqMax { get; private set; }
        public Ability Prerequisite { get; private set; }
        public AbilityProjectile Projectile { get; private set; }
        public AbilityPvX PvE { get; private set; }
        public AbilityPvX PvP { get; private set; }
        public double ResetTime { get { return RawResetTime.HasValue ? RawResetTime.Value : 0; } }
        public double? RawResetTime { get; private set; }
        public string SelfParticle { get; private set; }
        public Ability SharesResetTimerWith { get; private set; }
        public PowerSkill Skill { get; private set; }
        public Skill ConnectedSkill { get; private set; }
        private bool IsSkillParsed;
        public List<AbilityRequirement> SpecialCasterRequirementList { get; } = new List<AbilityRequirement>();
        public string SpecialInfo { get; private set; }
        public int SpecialTargetingTypeReq { get { return RawSpecialTargetingTypeReq.HasValue ? RawSpecialTargetingTypeReq.Value : 0; } }
        private int? RawSpecialTargetingTypeReq;
        public AbilityTarget Target { get; private set; }
        public TargetEffectKeyword TargetEffectKeywordReq { get; private set; }
        public AbilityTargetParticle TargetParticle { get; private set; }
        public Ability UpgradeOf { get; private set; }
        public bool WorksInCombat { get { return RawWorksInCombat.HasValue && RawWorksInCombat.Value; } }
        public bool? RawWorksInCombat { get; private set; }
        public bool WorksUnderwater { get { return RawWorksUnderwater.HasValue && RawWorksUnderwater.Value; } }
        public bool? RawWorksUnderwater { get; private set; }
        public bool WorksWhileFalling { get { return RawWorksWhileFalling.HasValue && RawWorksWhileFalling.Value; } }
        public bool? RawWorksWhileFalling { get; private set; }
        public TooltipsExtraKeywords ExtraKeywordsForTooltips { get; private set; }
        public Ability AbilityGroup { get; private set; }
        public string RawSpecialCasterRequirementsErrorMessage { get; private set; }
        public bool IgnoreEffectErrors { get { return RawIgnoreEffectErrors.HasValue && RawIgnoreEffectErrors.Value; } }
        public bool? RawIgnoreEffectErrors { get; private set; }
        public Dictionary<string, Attribute> AttributesThatDeltaAmmoStickChanceTable { get; } = new Dictionary<string, Attribute>();
        public Dictionary<string, Attribute> AttributesThatDeltaDelayLoopTimeTable { get; } = new Dictionary<string, Attribute>();
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return Name; } }
        public string DigitStrippedName { get; private set; }
        public int LineIndex { get; private set; }
        public List<AbilityAdditionalResult> AbilityAdditionalResultList { get; } = new List<AbilityAdditionalResult>();
        public string SearchResultIconFileName { get { return RawIconId.HasValue && RawIconId.Value > 0 ? "icon_" + RawIconId.Value : null; } }
        public List<AbilityRequirement> CombinedRequirementList { get; } = new List<AbilityRequirement>();
        public ConsumedItem ConsumedItem { get; private set; }
        public List<GenericSource> SourceList { get; private set; } = new List<GenericSource>();

        public override void SetIndirectProperties(Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables, ParseErrorInfo ErrorInfo)
        {
            string DigitStrippedName = InternalName;
            string LineIndexString = "";

            while (DigitStrippedName.Length > 0 && Char.IsDigit(DigitStrippedName[DigitStrippedName.Length - 1]))
            {
                LineIndexString = DigitStrippedName.Substring(DigitStrippedName.Length - 1) + LineIndexString;
                DigitStrippedName = DigitStrippedName.Substring(0, DigitStrippedName.Length - 1);
            }

            this.DigitStrippedName = DigitStrippedName;

            int LineIndex;
            if (int.TryParse(LineIndexString, out LineIndex))
                this.LineIndex = LineIndex;
            else
                this.LineIndex = -1;

            foreach (AbilityItemKeyword Keyword in ItemKeywordReqList)
                CombinedRequirementList.Add(new InternalAbilityRequirement(Keyword));

            foreach (AbilityRequirement Item in SpecialCasterRequirementList)
                CombinedRequirementList.Add(Item);

            if (ConsumedItemLink != null)
                ConsumedItem = new ConsumedItemDirect(ConsumedItemLink, RawConsumedItemCount, RawConsumedItemChance, RawConsumedItemChanceToStickInCorpse);
            else
            {
                ConsumedItems ParsedConsumedItem;
                if (StringToEnumConversion<ConsumedItems>.TryParse(RawConsumedItemKeyword, null, ConsumedItems.Internal_None, out ParsedConsumedItem, null))
                    ConsumedItem = new ConsumedItemByKeyword(ParsedConsumedItem, RawConsumedItemCount, RawConsumedItemChance, RawConsumedItemChanceToStickInCorpse);
            }
        }

        public void SetSource(GenericSource Source, ParseErrorInfo ErrorInfo)
        {
            if (Source == null)
                return;

            if (SourceList.Contains(Source))
                ErrorInfo.AddInvalidObjectFormat("Ability Source");
            else
                SourceList.Add(Source);
        }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "AbilityGroup", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseAbilityGroup,
                GetString = () => RawAbilityGroup } },
            { "Animation", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Animation = StringToEnumConversion<AbilityAnimation>.Parse(value, errorInfo),
                GetString = () => StringToEnumConversion<AbilityAnimation>.ToString(Animation, null, AbilityAnimation.Internal_None) } },
            { "AttributesThatDeltaAmmoStickChance", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => RawAttributesThatDeltaAmmoStickChanceList.Add(value),
                SetArrayIsEmpty = () => RawAttributesThatDeltaAmmoStickChanceListIsEmpty = true,
                GetStringArray = () => RawAttributesThatDeltaAmmoStickChanceList,
                GetArrayIsEmpty = () => RawAttributesThatDeltaAmmoStickChanceListIsEmpty } },
            { "AttributesThatDeltaDelayLoopTime", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => RawAttributesThatDeltaDelayLoopTimeList.Add(value),
                SetArrayIsEmpty = () => RawAttributesThatDeltaDelayLoopTimeListIsEmpty = true,
                GetStringArray = () => RawAttributesThatDeltaDelayLoopTimeList,
                GetArrayIsEmpty = () => RawAttributesThatDeltaDelayLoopTimeListIsEmpty } },
            { "AttributesThatDeltaPowerCost", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => RawAttributesThatDeltaPowerCostList.Add(value),
                SetArrayIsEmpty = () => RawAttributesThatDeltaPowerCostListIsEmpty = true,
                GetStringArray = () => RawAttributesThatDeltaPowerCostList,
                GetArrayIsEmpty = () => RawAttributesThatDeltaPowerCostListIsEmpty } },
            { "AttributesThatDeltaResetTime", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => RawAttributesThatDeltaResetTimeList.Add(value),
                SetArrayIsEmpty = () => RawAttributesThatDeltaResetTimeListIsEmpty = true,
                GetStringArray = () => RawAttributesThatDeltaResetTimeList,
                GetArrayIsEmpty = () => RawAttributesThatDeltaResetTimeListIsEmpty } },
            { "AttributesThatModPowerCost", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => RawAttributesThatModPowerCostList.Add(value),
                SetArrayIsEmpty = () => RawAttributesThatModPowerCostListIsEmpty = true,
                GetStringArray = () => RawAttributesThatModPowerCostList,
                GetArrayIsEmpty = () => RawAttributesThatModPowerCostListIsEmpty } },
            { "CanBeOnSidebar", new FieldParser() {
                Type = FieldType.Bool,
                ParseBool = (bool value, ParseErrorInfo errorInfo) => RawCanBeOnSidebar = value,
                GetBool = () => RawCanBeOnSidebar } },
            { "CanSuppressMonsterShout", new FieldParser() {
                Type = FieldType.Bool,
                ParseBool = (bool value, ParseErrorInfo errorInfo) => RawCanSuppressMonsterShout = value,
                GetBool = () => RawCanSuppressMonsterShout } },
            { "CanTargetUntargetableEnemies", new FieldParser() {
                Type = FieldType.Bool,
                ParseBool = (bool value, ParseErrorInfo errorInfo) => RawCanTargetUntargetableEnemies = value,
                GetBool = () => RawCanTargetUntargetableEnemies } },
            { "CausesOfDeath", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => StringToEnumConversion<Deaths>.ParseList(value, CausesOfDeathList, errorInfo),
                GetStringArray = () => StringToEnumConversion<Deaths>.ToStringList(CausesOfDeathList) } },
            { "Costs", new FieldParser() {
                Type = FieldType.ObjectArray,
                ParseObjectArray = (JsonObject value, ParseErrorInfo errorInfo) => JsonObjectParser<RecipeCost>.ParseList("Costs", value, CostList, errorInfo),
                GetObjectArray = () => CostList } },
            { "CombatRefreshBaseAmount", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawCombatRefreshBaseAmount = value,
                GetInteger = () => RawCombatRefreshBaseAmount } },
            { "CompatibleSkills", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = ParseCompatibleSkills,
                GetStringArray = () => StringToEnumConversion<PowerSkill>.ToStringList(RawCompatibleSkillList) } },
            { "ConsumedItemChance", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = ParseConsumedItemChance,
                GetFloat = () => RawConsumedItemChance } },
            { "ConsumedItemChanceToStickInCorpse", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = ParseConsumedItemChanceToStickInCorpse,
                GetFloat = () => RawConsumedItemChanceToStickInCorpse } },
            { "ConsumedItemCount", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = ParseConsumedItemCount,
                GetInteger = () => RawConsumedItemCount } },
            { "ConsumedItemKeyword", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => RawConsumedItemKeyword = value,
                GetString = () => RawConsumedItemKeyword  } },
            { "DamageType", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => DamageType = StringToEnumConversion<DamageType>.Parse(value, null, DamageType.Internal_None, DamageType.Internal_Empty, errorInfo),
                GetString = () => StringToEnumConversion<DamageType>.ToString(DamageType, null, DamageType.Internal_None, DamageType.Internal_Empty) } },
            { "DelayLoopIsAbortedIfAttacked", new FieldParser() {
                Type = FieldType.Bool,
                ParseBool = (bool value, ParseErrorInfo errorInfo) => RawDelayLoopIsAbortedIfAttacked = value,
                GetBool = () => RawDelayLoopIsAbortedIfAttacked } },
            { "DelayLoopMessage", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => DelayLoopMessage = value,
                GetString = () => DelayLoopMessage } },
            { "DelayLoopTime", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = ParseDelayLoopTime,
                GetFloat = () => RawDelayLoopTime } },
            { "Description", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Description = value,
                GetString = () => Description } },
            { "EffectKeywordsIndicatingEnabled", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = ParseEffectKeywordsIndicatingEnabled,
                GetStringArray = () => StringToEnumConversion<AbilityIndicatingEnabled>.ToSingleStringList(EffectKeywordsIndicatingEnabled) } },
            { "ExtraKeywordsForTooltips", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = ParseExtraKeywordsForTooltips,
                GetStringArray = () => StringToEnumConversion<TooltipsExtraKeywords>.ToSingleStringList(ExtraKeywordsForTooltips) } },
            { "IconID", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = ParseIconId,
                GetInteger = () => RawIconId } },
            { "IgnoreEffectErrors", new FieldParser() {
                Type = FieldType.Bool,
                ParseBool = (bool value, ParseErrorInfo errorInfo) => RawIgnoreEffectErrors = value,
                GetBool = () => RawIgnoreEffectErrors } },
            { "InternalAbility", new FieldParser() {
                Type = FieldType.Bool,
                ParseBool = (bool value, ParseErrorInfo errorInfo) => RawInternalAbility = value,
                GetBool = () => RawInternalAbility } },
            { "InternalName", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => InternalName = value,
                GetString = () => InternalName } },
            { "IsHarmless", new FieldParser() {
                Type = FieldType.Bool,
                ParseBool = (bool value, ParseErrorInfo errorInfo) => RawIsHarmless = value,
                GetBool = () => RawIsHarmless } },
            { "ItemKeywordReqErrorMessage", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => ItemKeywordReqErrorMessage = value,
                GetString = () => ItemKeywordReqErrorMessage } },
            { "ItemKeywordReqs", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => StringToEnumConversion<AbilityItemKeyword>.ParseList(value, TextMaps.AbilityItemKeywordStringMap, ItemKeywordReqList, errorInfo),
                GetStringArray = () => StringToEnumConversion<AbilityItemKeyword>.ToStringList(ItemKeywordReqList, TextMaps.AbilityItemKeywordStringMap) } },
            { "Keywords", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = ParseKeywords,
                GetStringArray = () => StringToEnumConversion<AbilityKeyword>.ToStringList(KeywordList) } },
            { "Level", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawLevel = value,
                GetInteger = () => RawLevel } },
            { "Name", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Name = value,
                GetString = () => Name } },
            { "PetTypeTagReq", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => PetTypeTagReq = StringToEnumConversion<AbilityPetType>.Parse(value, errorInfo),
                GetString = () => StringToEnumConversion<AbilityPetType>.ToString(PetTypeTagReq, null, AbilityPetType.Internal_None) } },
            { "PetTypeTagReqMax", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = ParsePetTypeTagReqMax,
                GetInteger = () => RawPetTypeTagReqMax } },
            { "Prerequisite", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParsePrerequisite,
                GetString = () => RawPrerequisite } },
            { "Projectile", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Projectile = StringToEnumConversion<AbilityProjectile>.Parse(value, errorInfo),
                GetString = () => StringToEnumConversion<AbilityProjectile>.ToString(Projectile, null, AbilityProjectile.Internal_None) } },
            { "PvE", new FieldParser() {
                Type = FieldType.Object,
                ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => PvE = JsonObjectParser<AbilityPvX>.Parse("PvE", value, errorInfo),
                GetObject = () => PvE } },
            { "PvP", new FieldParser() {
                Type = FieldType.Object,
                ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => PvP = JsonObjectParser<AbilityPvX>.Parse("PvP", value, errorInfo),
                GetObject = () => PvP } },
            { "ResetTime", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawResetTime = value,
                GetFloat = () => RawResetTime } },
            { "SelfParticle", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => SelfParticle = value,
                GetString = () => SelfParticle } },
            { "SharesResetTimerWith", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseSharesResetTimerWith,
                GetString = () => RawSharesResetTimerWith } },
            { "Skill", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseSkill,
                GetString = () => StringToEnumConversion<PowerSkill>.ToString(Skill, null, PowerSkill.Internal_None) } },
            { "SpecialCasterRequirements", new FieldParser() {
                Type = FieldType.ObjectArray,
                ParseObjectArray = (JsonObject value, ParseErrorInfo errorInfo) => JsonObjectParser<AbilityRequirement>.ParseList("SpecialCasterRequirement", value, SpecialCasterRequirementList, errorInfo),
                GetObjectArray = () => SpecialCasterRequirementList,
                SimplifyArray = true } },
            { "SpecialCasterRequirementsErrorMessage", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => RawSpecialCasterRequirementsErrorMessage = value,
                GetString = () => RawSpecialCasterRequirementsErrorMessage } },
            { "SpecialInfo", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseSpecialInfo,
                GetString = () => SpecialInfo } },
            { "SpecialTargetingTypeReq", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawSpecialTargetingTypeReq = value,
                GetInteger = () => RawSpecialTargetingTypeReq } },
            { "Target", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Target = StringToEnumConversion<AbilityTarget>.Parse(value, errorInfo),
                GetString = () => StringToEnumConversion<AbilityTarget>.ToString(Target, null, AbilityTarget.Internal_None) } },
            { "TargetEffectKeywordReq", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => TargetEffectKeywordReq = StringToEnumConversion<TargetEffectKeyword>.Parse(value, errorInfo),
                GetString = () => StringToEnumConversion<TargetEffectKeyword>.ToString(TargetEffectKeywordReq, null, TargetEffectKeyword.Internal_None) } },
            { "TargetParticle", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => TargetParticle = StringToEnumConversion<AbilityTargetParticle>.Parse(value, errorInfo),
                GetString = () => StringToEnumConversion<AbilityTargetParticle>.ToString(TargetParticle, null, AbilityTargetParticle.Internal_None) } },
            { "UpgradeOf", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseUpgradeOf,
                GetString = () => RawUpgradeOf } },
            { "WorksInCombat", new FieldParser() {
                Type = FieldType.Bool,
                ParseBool = (bool value, ParseErrorInfo errorInfo) => RawWorksInCombat = value,
                GetBool = () => RawWorksInCombat } },
            { "WorksUnderwater", new FieldParser() {
                Type = FieldType.Bool,
                ParseBool = (bool value, ParseErrorInfo errorInfo) => RawWorksUnderwater = value,
                GetBool = () => RawWorksUnderwater } },
            { "WorksWhileFalling", new FieldParser() {
                Type = FieldType.Bool,
                ParseBool = (bool value, ParseErrorInfo errorInfo) => RawWorksWhileFalling = value,
                GetBool = () => RawWorksWhileFalling } },
        }; } }

        private void ParseAbilityGroup(string RawAbilityGroup, ParseErrorInfo ErrorInfo)
        {
            this.RawAbilityGroup = RawAbilityGroup;
            AbilityGroup = null;
            IsRawAbilityGroupParsed = false;
        }

        private void ParseCompatibleSkills(string RawCompatibleSkill, ParseErrorInfo ErrorInfo)
        {
            if (StringToEnumConversion<PowerSkill>.TryParse(RawCompatibleSkill, out PowerSkill ParsedCompatibleSkill, ErrorInfo))
            {
                if (RawCompatibleSkillList.Count == 0)
                    CompatibleSkill = ParsedCompatibleSkill;

                RawCompatibleSkillList.Add(ParsedCompatibleSkill);
            }
        }

        private void ParseConsumedItemChance(float RawConsumedItemChance, ParseErrorInfo ErrorInfo)
        {
            this.RawConsumedItemChance = RawConsumedItemChance;

            int Percent = (int)(RawConsumedItemChance * 100);
            if (Percent <= 0 || Percent >= 100)
                ErrorInfo.AddInvalidObjectFormat("Ability ConsumedItemChance");
        }

        private void ParseConsumedItemChanceToStickInCorpse(float RawConsumedItemChanceToStickInCorpse, ParseErrorInfo ErrorInfo)
        {
            this.RawConsumedItemChanceToStickInCorpse = RawConsumedItemChanceToStickInCorpse;

            int Percent = (int)(RawConsumedItemChanceToStickInCorpse * 100);
            if (Percent <= 0 || Percent >= 100)
                ErrorInfo.AddInvalidObjectFormat("Ability ConsumedItemChanceToStickInCorpse");
        }

        private void ParseConsumedItemCount(int RawConsumedItemCount, ParseErrorInfo ErrorInfo)
        {
            this.RawConsumedItemCount = (int)RawConsumedItemCount;

            if (RawConsumedItemCount < 1)
                ErrorInfo.AddInvalidObjectFormat("Ability ConsumedItemCount");
        }

        private void ParseDelayLoopTime(float value, ParseErrorInfo ErrorInfo)
        {
            RawDelayLoopTime = value;

            if (value < 0)
                ErrorInfo.AddInvalidObjectFormat("Ability DelayLoopTime");
        }

        private void ParseEffectKeywordsIndicatingEnabled(string RawEffectKeywordIndicatingEnabled, ParseErrorInfo ErrorInfo)
        {
            if (StringToEnumConversion<AbilityIndicatingEnabled>.TryParse(RawEffectKeywordIndicatingEnabled, out AbilityIndicatingEnabled ParsedEffectKeywordIndicatingEnabled, ErrorInfo))
            {
                if (EffectKeywordsIndicatingEnabled != AbilityIndicatingEnabled.Internal_None)
                    ErrorInfo.AddInvalidObjectFormat("Ability EffectKeywordsIndicatingEnabled");
                else
                    EffectKeywordsIndicatingEnabled = ParsedEffectKeywordIndicatingEnabled;
            }
        }

        private void ParseExtraKeywordsForTooltips(string RawExtraKeywordForTooltips, ParseErrorInfo ErrorInfo)
        {
            if (StringToEnumConversion<TooltipsExtraKeywords>.TryParse(RawExtraKeywordForTooltips, out TooltipsExtraKeywords ParsedTooltipsExtraKeywords, ErrorInfo))
            {
                if (ExtraKeywordsForTooltips != TooltipsExtraKeywords.Internal_None)
                    ErrorInfo.AddInvalidObjectFormat("Ability ExtraKeywordsForTooltips");
                else
                    ExtraKeywordsForTooltips = ParsedTooltipsExtraKeywords;
            }
        }

        private void ParseIconId(int RawIconId, ParseErrorInfo ErrorInfo)
        {
            if (RawIconId > 0)
            {
                this.RawIconId = (int)RawIconId;
                ErrorInfo.AddIconId((int)RawIconId);

                PgJsonObjects.Skill.UpdateAnySkillIcon(Skill, this.RawIconId);
            }
            else
                this.RawIconId = 0;
        }

        private void ParseKeywords(string RawKeywords, ParseErrorInfo ErrorInfo)
        {
            if (StringToEnumConversion<AbilityKeyword>.TryParse(RawKeywords, out AbilityKeyword ParsedAbilityKeyword, ErrorInfo))
            {
                bool HasBasicAttack = ParsedAbilityKeyword == AbilityKeyword.BasicAttack && !KeywordList.Contains(AbilityKeyword.BasicAttack);
                KeywordList.Add(ParsedAbilityKeyword);

                if (HasBasicAttack)
                    PgJsonObjects.Skill.UpdateBasicAttackTable(Skill, this);
            }
        }

        private void ParsePetTypeTagReqMax(int value, ParseErrorInfo ErrorInfo)
        {
            RawPetTypeTagReqMax = value;
            PetTypeTagReqMax = value;
        }

        private void ParsePrerequisite(string RawPrerequisite, ParseErrorInfo ErrorInfo)
        {
            this.RawPrerequisite = RawPrerequisite;
            Prerequisite = null;
            IsRawPrerequisiteParsed = false;
        }

        private void ParseSharesResetTimerWith(string RawSharesResetTimerWith, ParseErrorInfo ErrorInfo)
        {
            this.RawSharesResetTimerWith = RawSharesResetTimerWith;
            SharesResetTimerWith = null;
            IsRawSharesResetTimerWithParsed = false;
        }

        private void ParseSkill(string value, ParseErrorInfo errorInfo)
        {
            if (StringToEnumConversion<PowerSkill>.TryParse(value, out PowerSkill ParsedPowerSkill, errorInfo))
            {
                Skill = ParsedPowerSkill;

                PgJsonObjects.Skill.UpdateAnySkillIcon(Skill, RawIconId.HasValue && RawIconId.Value > 0 ? RawIconId : null);
                if (KeywordList.Contains(AbilityKeyword.BasicAttack))
                    PgJsonObjects.Skill.UpdateBasicAttackTable(Skill, this);
            }
        }

        private void ParseSpecialInfo(string RawSpecialInfo, ParseErrorInfo ErrorInfo)
        {
            if (SpecialInfo == null)
                SpecialInfo = RawSpecialInfo;

            ParseCompleteSpecialInfo(RawSpecialInfo, ErrorInfo);
        }

        private void ParseUpgradeOf(string RawUpgradeOf, ParseErrorInfo ErrorInfo)
        {
            this.RawUpgradeOf = RawUpgradeOf;
            UpgradeOf = null;
            IsRawUpgradeOfParsed = false;
        }

        public void ParseCompleteSpecialInfo(string s, ParseErrorInfo ErrorInfo)
        {
            if (s == null)
                return;

            s = s.Replace("vs.", "vs");

            int Index = s.IndexOf(". ");
            if (Index >= 0)
            {
                string s1 = s.Substring(0, Index);
                string s2 = s.Substring(Index + 2).Trim();

                ParseSpecialInfo(s1, ErrorInfo);
                ParseSpecialInfo(s2, ErrorInfo);
                return;
            }

            while (s.EndsWith("."))
                s = s.Substring(0, s.Length - 1);

            AbilityAdditionalResult AdditionalResult;
            object[] args = new object[10];

            if (Tools.Scan(s, "Stuns opponent if their armor is below %d%%", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Stun, TimeSpan.Zero);
                AdditionalResult.Conditional = AbilityEffectConditional.BelowArmorThreshold;
                AdditionalResult.ConditionalValue = (int)args[0];
                AddResult(AdditionalResult);
            }

            else if (s == "Target is stunned" || s == "Stuns opponent")
                AddResult(new AbilityAdditionalResult(AbilityEffect.Stun, TimeSpan.Zero));

            else if (s == "Flings opponent backwards" || s == "Target is knocked backwards")
                AddResult(new AbilityAdditionalResult(AbilityEffect.Knockback, TimeSpan.Zero));

            else if (s == "Targets are flung away")
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Knockback, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.PointBlank;
                AdditionalResult.AoERange = (PvE != null )? PvE.AoE : 0;
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Deals %d %{DamageType} damage over %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.DoT, TimeSpan.FromSeconds((int)args[2]));
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], DamageType = (DamageType)args[1] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Deals %d %{DamageType} damage", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Damage, TimeSpan.Zero);
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], DamageType = (DamageType)args[1] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Deals %d %{DamageType} damage if target's armor is below %d%%", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Damage, TimeSpan.Zero);
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], DamageType = (DamageType)args[1] });
                AdditionalResult.Conditional = AbilityEffectConditional.BelowArmorThreshold;
                AdditionalResult.ConditionalValue = (int)args[2];
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Target takes %d%% more damage from direct %{DamageType} attacks for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.DirectVulnerability, TimeSpan.FromSeconds((int)args[2]));
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], IsPercent = true, DamageType = (DamageType)args[1] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Target takes %d%% more damage from %{DamageType} for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Vulnerability, TimeSpan.FromSeconds((int)args[2]));
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], IsPercent = true, DamageType = (DamageType)args[1] });
                AddResult(AdditionalResult);
            }

            else if (s == "Target is pulled toward you")
                AddResult(new AbilityAdditionalResult(AbilityEffect.Pull, TimeSpan.Zero));

            else if (s == "Hits all targets within range")
                AddResult(new AbilityAdditionalResult(AbilityEffect.PointBlankAoE, TimeSpan.Zero));

            else if (Tools.Scan(s, "Target becomes mesmerized for %d seconds, or until attacked", args))
                AddResult(new AbilityAdditionalResult(AbilityEffect.Mezz, TimeSpan.FromSeconds((int)args[0])));

            else if (Tools.Scan(s, "Your next attack deals %d damage", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.NextAttackDamage, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Target has a chance to flee for %d seconds", args))
                AddResult(new AbilityAdditionalResult(AbilityEffect.Fear, TimeSpan.FromSeconds((int)args[0])));

            else if (Tools.Scan(s, "Target's next attack deals %d damage", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.NextAttackDamage, TimeSpan.Zero);
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Target's attacks deal %d%% damage for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.DamageBoost, TimeSpan.FromSeconds((int)args[1]));
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], IsPercent = true });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Undead targets take %d additional %{DamageType} damage", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Damage, TimeSpan.Zero);
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], DamageType = (DamageType)args[1] });
                AdditionalResult.Conditional = AbilityEffectConditional.Undead;
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Up to %d Rage is taken from the target and %d%% of it is turned into %{DamageType} damage", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.RageToDamageConversion, TimeSpan.Zero);
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[1], IsPercent = true, DamageType = (DamageType)args[2] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Slightly restores body heat in cold environments", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.RestoreHeat, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Slightly restores target's body heat in cold environments", args))
                AddResult(new AbilityAdditionalResult(AbilityEffect.RestoreHeat, TimeSpan.Zero));

            else if (Tools.Scan(s, "%d %{DamageType} Mitigation for %d minutes", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Mitigation, TimeSpan.FromMinutes((int)args[2]));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], DamageType = (DamageType)args[1] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Target takes %d Armor damage over %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.ArmorDoT, TimeSpan.FromSeconds((int)args[1]));
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Deals %d additional damage over %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.DoT, TimeSpan.FromSeconds((int)args[1]));
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], DamageType = this.DamageType });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Future Pack Attacks to this target deal %d damage", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.FuturePackDamage, TimeSpan.FromSeconds(666));
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "If you are hit by %d attacks within the next %d seconds (and are within 40 meters of your Shadow Feint position) you blink back to this position", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.ShadowFeintReturn, TimeSpan.FromSeconds((int)args[1]));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Deals %d Health damage over %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.DoT, TimeSpan.FromSeconds((int)args[1]));
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Boosts %{DamageType} and %{DamageType} damage %d%% for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.DamageBoost, TimeSpan.FromSeconds((int)args[3]));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[2], IsPercent = true, DamageType = (DamageType)args[0] });
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[2], IsPercent = true, DamageType = (DamageType)args[1] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "You gain %d sprint speed for %d seconds or until you enter combat", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.OutOfCombatSprintSpeed, TimeSpan.FromSeconds((int)args[1]));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Monsters must get %d meters closer than normal to be able to see you, but your Sprint Speed is %d%%", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.AggroRangeReduction, TimeSpan.MaxValue);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);

                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.SprintSpeed, TimeSpan.MaxValue);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[1], IsPercent = true });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Your next attack deals +%d damage and ends the effect", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.NextAttackDamage, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);

                foreach (AbilityAdditionalResult Item in AbilityAdditionalResultList)
                    Item.Reset = AdditionalResult;
            }

            else if (Tools.Scan(s, "You and nearby allies deal %d%% %{DamageType} damage for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.DamageBoost, TimeSpan.FromSeconds((int)args[2]));
                AdditionalResult.Target = AbilityEffectTarget.SelfAndAllies;
                AdditionalResult.AoERange = (PvE != null )? PvE.AoE : 0;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], IsPercent = true, DamageType = (DamageType)args[1] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Target needs %d%% more Rage to fill their Rage Meter", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.RageMeterChange, TimeSpan.Zero);
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], IsPercent = true });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Target flees for %d seconds", args))
                AddResult(new AbilityAdditionalResult(AbilityEffect.Fear, TimeSpan.FromSeconds((int)args[0])));

            else if (Tools.Scan(s, "You gain %f sprint speed for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.SprintSpeed, TimeSpan.FromSeconds((int)args[1]));
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (double)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "You and your allies' melee attacks deal %d damage for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.MeleeDamageBoost, TimeSpan.FromSeconds((int)args[1]));
                AdditionalResult.Target = AbilityEffectTarget.SelfAndAllies;
                AdditionalResult.AoERange = (PvE != null )? PvE.AoE : 0;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Targets flee for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Fear, TimeSpan.FromSeconds((int)args[0]));
                AdditionalResult.Target = AbilityEffectTarget.TargetsAndAround;
                AdditionalResult.AoERange = (PvE != null )? PvE.AoE : 0;
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Target decides not to attack you for %d-%d seconds (even if you attack it during that time)", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Peacefulness, TimeSpan.FromSeconds((int)args[0]));
                AdditionalResult.RandomDuration = TimeSpan.FromSeconds((int)args[1]) - TimeSpan.FromSeconds((int)args[0]);
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "You and your allies gain %d%% Evasion vs Melee and Burst attacks for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.EvasionVsMelee, TimeSpan.FromSeconds((int)args[1]));
                AdditionalResult.Target = AbilityEffectTarget.SelfAndAllies;
                AdditionalResult.AoERange = (PvE != null )? PvE.AoE : 0;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], IsPercent = true });
                AddResult(AdditionalResult);

                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.EvasionVsBurstAttack, TimeSpan.FromSeconds((int)args[1]));
                AdditionalResult.Target = AbilityEffectTarget.SelfAndAllies;
                AdditionalResult.AoERange = (PvE != null )? PvE.AoE : 0;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], IsPercent = true });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "You mitigate %d damage from %{DamageType}, %{DamageType}, and %{DamageType} attacks", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Mitigation, TimeSpan.FromSeconds(666));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], DamageType = (DamageType)args[1] });
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], DamageType = (DamageType)args[2] });
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], DamageType = (DamageType)args[3] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "(%d seconds)", args))
            {
                if (AbilityAdditionalResultList.Count > 0)
                {
                    AdditionalResult = AbilityAdditionalResultList[AbilityAdditionalResultList.Count - 1];
                    AdditionalResult.Duration = TimeSpan.FromSeconds((int)args[0]);
                }
                else
                {
                    ErrorInfo.AddUnparsedSpecialInfo(s);
                    return;
                }
            }

            else if (Tools.Scan(s, "You gain %d sprint speed and enhanced jump control for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.SprintSpeed, TimeSpan.FromSeconds((int)args[1]));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);

                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.JumpControl, TimeSpan.FromSeconds((int)args[1]));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Consumes %d Grass", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.ConsumeGrass, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Consumes %d Carrot", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.ConsumeCarrot, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Consumes one Basic Mushroom Turret", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.ConsumeMushroomTurret, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = 1 });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Consumes one Potent Mushroom Turret", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.ConsumeMushroomTurret, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = 1 });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Consumes one Deadly Mushroom Turret", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.ConsumeMushroomTurret, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = 1 });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Consumes one Malevolent Mushroom Turret", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.ConsumeMushroomTurret, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = 1 });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Consumes one Horrific Mushroom Turret", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.ConsumeMushroomTurret, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = 1 });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Consumes one Disastrous Mushroom Turret", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.ConsumeMushroomTurret, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = 1 });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Consumes one Nightmarish Mushroom Turret", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.ConsumeMushroomTurret, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = 1 });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "You obtain %d Grass", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.ObtainGrass, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "You collect milk in an empty bottle", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.ObtainMilk, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = 1 });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "You gain %d sprint speed and %d damage from all attacks for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.SprintSpeed, TimeSpan.FromSeconds((int)args[2]));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);

                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Mitigation, TimeSpan.FromSeconds((int)args[2]));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = -((int)args[1]) });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "If target attacks and damages you within %d seconds, you regain %d Health", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.HealFromAttack, TimeSpan.FromSeconds((int)args[0]));
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[1] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Summon Pet from Stable Slot #%d", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.SummonPet, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Pet gains %d speed and %d damage for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.SprintSpeed, TimeSpan.FromSeconds((int)args[2]));
                AdditionalResult.Target = AbilityEffectTarget.Pet;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);

                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.DamageBoost, TimeSpan.FromSeconds((int)args[2]));
                AdditionalResult.Target = AbilityEffectTarget.Pet;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[1] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Pet attempts to use its pet-specific Special Attack if it has sufficient Power", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.SpecialAttack, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.Pet;
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "For %d seconds, pet's attacks taunt as if they did %d%% more damage, and pet gains %d mitigation versus %{DamageType}, %{DamageType}, and %{DamageType} damage", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Taunt, TimeSpan.FromSeconds((int)args[0]));
                AdditionalResult.Target = AbilityEffectTarget.Pet;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[1] });
                AddResult(AdditionalResult);

                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Mitigation, TimeSpan.FromSeconds((int)args[0]));
                AdditionalResult.Target = AbilityEffectTarget.Pet;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[2], DamageType = (DamageType)args[3] });
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[2], DamageType = (DamageType)args[4] });
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[2], DamageType = (DamageType)args[5] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Pet switches to Defend mode, and is directed to attack the foes that are focused on you", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.DefendMode, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.Pet;
                AddResult(AdditionalResult);

                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Focus, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.Pet;
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "(Potency depends on Loyalty.)", args))
                return;

            else if (Tools.Scan(s, "Pet deals %d%% damage for one attack", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.NextAttackDamageBoost, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.Pet;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], IsPercent = true });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Pet recovers %d Health/Sec and gains Universal Mitigation %d for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Regeneration, TimeSpan.FromSeconds((int)args[2]));
                AdditionalResult.Target = AbilityEffectTarget.Pet;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], IsPercent = true });
                AddResult(AdditionalResult);

                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Mitigation, TimeSpan.FromSeconds((int)args[2]));
                AdditionalResult.Target = AbilityEffectTarget.Pet;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[1] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Pet gains %d%% Projectile Evasion, %d%% Burst Evasion, and %d%% Melee Evasion for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.EvasionVsProjectile, TimeSpan.FromSeconds((int)args[3]));
                AdditionalResult.Target = AbilityEffectTarget.Pet;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], IsPercent = true });
                AddResult(AdditionalResult);

                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.EvasionVsBurstAttack, TimeSpan.FromSeconds((int)args[3]));
                AdditionalResult.Target = AbilityEffectTarget.Pet;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[1], IsPercent = true });
                AddResult(AdditionalResult);

                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.EvasionVsMelee, TimeSpan.FromSeconds((int)args[3]));
                AdditionalResult.Target = AbilityEffectTarget.Pet;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[1], IsPercent = true });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Pet takes %d damage and deals %d damage for one attack", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Heal, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.Pet;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = -((int)args[0]) });
                AddResult(AdditionalResult);

                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.NextAttackDamageBoost, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.Pet;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[1] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Pet recovers %d Armor", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Armor, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.Pet;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Pet-specific clever trick is performed, if pet has sufficient Power remaining", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.SpecialAttack, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.Pet;
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Sprint Speed %d for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.SprintSpeed, TimeSpan.FromSeconds((int)args[1]));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Kicks deal %d%% damage", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.KickBonus, TimeSpan.MaxValue);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], IsPercent = true });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Direct Damage Mitigation %d, Indirect Damage Mitigation %d", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.DirectMitigation, TimeSpan.MaxValue);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);

                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.IndirectMitigation, TimeSpan.MaxValue);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[1] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "In-Combat Power Regeneration %d, Out-Of-Combat Power Regeneration %d", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.PowerRegeneration, TimeSpan.MaxValue);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);

                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.OutOfCombatPowerRegeneration, TimeSpan.MaxValue);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[1] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Out-of-Combat Sprint Speed %d", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.OutOfCombatSprintSpeed, TimeSpan.MaxValue);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "%{DamageType} Mitigation %d", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Mitigation, TimeSpan.MaxValue);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[1], DamageType = (DamageType)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Randomly cures broken bones", args) || Tools.Scan(s, "Occasionally cures broken bones", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.CureBrokenBone, TimeSpan.MaxValue);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "You take %d%% less damage from all attacks for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Mitigation, TimeSpan.FromSeconds((int)args[1]));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], IsPercent = true });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "For the next ten seconds, any physical damage will be negated (%{DamageType}, %{DamageType}, or %{DamageType})", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Immunity, TimeSpan.FromSeconds(10));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { DamageType = (DamageType)args[0] });
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { DamageType = (DamageType)args[1] });
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { DamageType = (DamageType)args[2] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "You take no damage from falls for %d minutes, and gravity affects you less, allowing you to steer during long falls", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.NegateFallDamage, TimeSpan.FromMinutes((int)args[0]));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Target's speed is reduced by %d%% for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.SprintSpeed, TimeSpan.FromSeconds((int)args[1]));
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], IsPercent = true });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Target's run speed is reduced by %d%% for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.SprintSpeed, TimeSpan.FromSeconds((int)args[1]));
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], IsPercent = true });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Vulnerable targets are stunned", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.SprintSpeed, TimeSpan.Zero);
                AdditionalResult.Conditional = AbilityEffectConditional.Vulnerable;
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "For %d seconds, melee attackers take %d %{DamageType} damage", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.DamageShield, TimeSpan.FromSeconds((int)args[0]));
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[1], DamageType = (DamageType)args[2] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Zombie deals %d damage with each attack, gains %d Max Health, and Heals %d", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.SummonZombie, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AddResult(AdditionalResult);

                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.DamageBoost, TimeSpan.MaxValue);
                AdditionalResult.Target = AbilityEffectTarget.Zombie;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);

                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.MaxHealthBoost, TimeSpan.MaxValue);
                AdditionalResult.Target = AbilityEffectTarget.Zombie;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[1] });
                AddResult(AdditionalResult);

                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Heal, TimeSpan.MaxValue);
                AdditionalResult.Target = AbilityEffectTarget.Zombie;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[2] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Steals %d Health", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Lifetap, TimeSpan.Zero);
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Boosts Max Health by %d for %d minutes", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.MaxHealthBoost, TimeSpan.FromMinutes((int)args[1]));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Heals %d", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Heal, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Boosts Max Power by %d for %d minutes", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.MaxPowerBoost, TimeSpan.FromMinutes((int)args[1]));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Restores %d Power", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.MaxPowerBoost, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Target is immobilized for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Root, TimeSpan.FromSeconds((int)args[0]));
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "For %d seconds, all Summoned Undead within %d meters deal %d damage with each attack", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.DamageBoost, TimeSpan.FromSeconds((int)args[0]));
                AdditionalResult.Target = AbilityEffectTarget.SummonedUndeadsInRange;
                AdditionalResult.AoERange = (int)args[1];
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[2] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Direct attacks deal %d%% extra damage", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.DirectDamageBoost, TimeSpan.FromSeconds(666));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], IsPercent = true });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Uniformly diminishes target's entire aggro list by %d%%, making them less locked in to their aggro choices and more easily susceptible to additional taunts and detaunts", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.AggroReduced, TimeSpan.Zero);
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], IsPercent = true });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Bubble lasts for %d seconds or until %d damage is absorbed", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.BubbleBoost, TimeSpan.FromSeconds((int)args[0]));
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[1] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Deals %d additional %{DamageType} damage over %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.DoT, TimeSpan.FromSeconds((int)args[2]));
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], DamageType = (DamageType)args[1] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Deals %d %{DamageType} damage to armor over %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.ArmorDoT, TimeSpan.FromSeconds((int)args[2]));
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], DamageType = (DamageType)args[1] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Your egg incubates for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.ArmorDoT, TimeSpan.FromSeconds((int)args[0]));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Take a humanoid shape for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.HumanoidIllusion, TimeSpan.FromSeconds((int)args[0]));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "For %d seconds, additional Infinite Legs attacks deal %d damage", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.InfiniteLegsBoost, TimeSpan.FromSeconds((int)args[0]));
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[1] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Create one Spiderweb", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.CreateSpiderweb, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = 1 });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Target is taunted %d", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Taunt, TimeSpan.Zero);
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "%{DamageType} Damage boosted %d%%", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.DamageBoost, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[1], IsPercent = true, DamageType = (DamageType)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "For the next ten seconds, any elemental damage will be negated (%{DamageType}, %{DamageType}, or %{DamageType})", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Immunity, TimeSpan.FromSeconds(10));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { DamageType = (DamageType)args[0] });
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { DamageType = (DamageType)args[1] });
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { DamageType = (DamageType)args[2] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "All abilities gain Accuracy %d for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.AccuracyBoost, TimeSpan.FromSeconds((int)args[1]));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "For %d seconds, you gain %d sprint speed and all attacks taunt as if they did %d%% more damage", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.SprintSpeed, TimeSpan.FromSeconds((int)args[0]));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[1] });
                AddResult(AdditionalResult);

                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Taunt, TimeSpan.FromSeconds((int)args[0]));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[2], IsPercent = true });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "For %d seconds, melee attackers take %d %{DamageType} damage", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.DamageShield, TimeSpan.FromSeconds((int)args[0]));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[1], DamageType = (DamageType)args[2] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Targets are knocked back and taunted %d", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Taunt, TimeSpan.Zero);
                AdditionalResult.AoERange = (PvE != null )? PvE.AoE : 0;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);

                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Knockback, TimeSpan.Zero);
                AdditionalResult.AoERange = (PvE != null )? PvE.AoE : 0;
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Enemy is knocked down", args))
                AddResult(new AbilityAdditionalResult(AbilityEffect.Knockdown, TimeSpan.Zero));

            else if (Tools.Scan(s, "While down, enemies cannot attack or move and take %d%% damage from all attacks", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.KnockdownVulnerability, TimeSpan.Zero);
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], IsPercent = true });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "You lose %d Armor (if you have that much remaining)", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Armor, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = -((int)args[0]) });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Target takes %d %{DamageType} damage after a %d second delay", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.DelayedDamage, TimeSpan.FromSeconds((int)args[2]));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], DamageType = (DamageType)args[1] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Target takes %d %{DamageType} damage after an %d second delay", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.DelayedDamage, TimeSpan.FromSeconds((int)args[2]));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], DamageType = (DamageType)args[1] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "This damage is aborted if target is standing in water", args))
                return;

            else if (Tools.Scan(s, "For %d seconds, all targets deal %d%% %{DamageType} damage", args))
            {
                //TODO: should be aoe range?
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.DamageBoost, TimeSpan.FromSeconds((int)args[0]));
                AdditionalResult.AoERange = (PvE != null) ? PvE.Range : 0;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[1], IsPercent = true, DamageType = (DamageType)args[2] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Target's attacks deal %d damage for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.DamageBoost, TimeSpan.FromSeconds((int)args[0]));
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[1] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Target takes %d damage from all attacks for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.ExtraDamageFromGroup, TimeSpan.FromSeconds((int)args[0]));
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[1] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Melee attackers take %d %{DamageType} damage", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.DamageShield, TimeSpan.Zero);
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], DamageType = (DamageType)args[1] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Target's %{DamageType} attacks deal %d damage, and %{DamageType} damage-over-time attacks deal %d per tick", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.DamageBoost, TimeSpan.FromSeconds(666));
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[1], DamageType = (DamageType)args[0] });
                AddResult(AdditionalResult);

                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.DoTBoost, TimeSpan.FromSeconds(666));
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[3], DamageType = (DamageType)args[2] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Sanctuary heals nearby allies %d every %d seconds while in combat", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.HoT, TimeSpan.FromSeconds(666));
                AdditionalResult.Target = AbilityEffectTarget.SelfAndAllies;
                AdditionalResult.AoERange = (PvE != null )? PvE.AoE : 0;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Monster-detection range reduced by %d meters for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.AggroRangeReduction, TimeSpan.FromSeconds((int)args[1]));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Boosts Movement Speed by %f for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.SprintSpeed, TimeSpan.FromSeconds((int)args[1]));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (double)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Target takes %d %{DamageType} damage over %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.DoT, TimeSpan.FromSeconds((int)args[2]));
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], DamageType = (DamageType)args[1] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "For %d seconds, target's attacks have a %d%% chance to miss", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.AccuracyBoost, TimeSpan.FromSeconds((int)args[0]));
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[1], IsPercent = true });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "(This effect does not stack with other druids' castings.)", args))
                return;

            else if (Tools.Scan(s, "%{DamageType}, %{DamageType}, %{DamageType}, and %{DamageType} attack mitigation %d, Speed %d%%", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Mitigation, TimeSpan.FromSeconds(666));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[4], DamageType = (DamageType)args[0] });
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[4], DamageType = (DamageType)args[1] });
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[4], DamageType = (DamageType)args[2] });
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[4], DamageType = (DamageType)args[3] });
                AddResult(AdditionalResult);

                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.SprintSpeed, TimeSpan.FromSeconds(666));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[5] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Target is randomly stunned often during the next %d seconds", args))
                AddResult(new AbilityAdditionalResult(AbilityEffect.RandomStun, TimeSpan.FromSeconds((int)args[0])));

            else if (Tools.Scan(s, "Target is randomly stunned periodically during the next %d seconds", args))
                AddResult(new AbilityAdditionalResult(AbilityEffect.RandomStun, TimeSpan.FromSeconds((int)args[0])));

            else if (Tools.Scan(s, "Summoned sphere exists for %d seconds or until killed", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.SummonedSphereDuration, TimeSpan.FromSeconds((int)args[0]));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Targets are immobilized for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Root, TimeSpan.FromSeconds((int)args[0]));
                AdditionalResult.AoERange = (PvE != null )? PvE.AoE : 0;
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Targets are slowed %d%% for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Snare, TimeSpan.FromSeconds((int)args[1]));
                AdditionalResult.AoERange = (PvE != null )? PvE.AoE : 0;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], IsPercent = true });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Gain %d%% resistance to %{DamageType} and %{DamageType}, %d%% resistance to %{DamageType}, %d%% resistance to %{DamageType}, and %d%% resistance to %{DamageType} and %{DamageType}", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Mitigation, TimeSpan.FromSeconds(666));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], IsPercent = true, DamageType = (DamageType)args[1] });
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], IsPercent = true, DamageType = (DamageType)args[2] });
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[3], IsPercent = true, DamageType = (DamageType)args[4] });
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[5], IsPercent = true, DamageType = (DamageType)args[6] });
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[7], IsPercent = true, DamageType = (DamageType)args[8] });
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[7], IsPercent = true, DamageType = (DamageType)args[9] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Restore %d Body Heat", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.RestoreHeat, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Requires (and dispels) Ice Armor", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.DispelIceArmor, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "%d %{DamageType} Protection for %d minutes", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Mitigation, TimeSpan.FromMinutes((int)args[2]));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], DamageType = (DamageType)args[1] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, " Applies Moderate Concussion status: target is prone to random self-stuns", args))
                //TODO: bug, first space
                AddResult(new AbilityAdditionalResult(AbilityEffect.RandomStun, TimeSpan.FromSeconds(666)));

            else if (Tools.Scan(s, "Regain %d Armor; For %d seconds you become immune to knockbacks and stuns", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Armor, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);

                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.KnockbackImmunity, TimeSpan.FromSeconds((int)args[1]));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AddResult(AdditionalResult);

                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.StunImmunity, TimeSpan.FromSeconds((int)args[1]));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Summons a figment of you that taunts extremely aggressively", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.SummonFigment, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Lasts %d seconds", args))
            {
                if (AbilityAdditionalResultList.Count > 0)
                {
                    AdditionalResult = AbilityAdditionalResultList[AbilityAdditionalResultList.Count - 1];
                    AdditionalResult.Duration = TimeSpan.FromSeconds((int)args[0]);
                }
                else
                {
                    ErrorInfo.AddUnparsedSpecialInfo(s);
                    return;
                }
            }

            else if (Tools.Scan(s, "Your Knife Base Damage is boosted by %d%% for your next attack", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.NextAttackDamage, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], IsPercent = true });
                AdditionalResult.Conditional = AbilityEffectConditional.AnyKnifeDamage;
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Your %{DamageType} Base Damage is boosted by %d%% for your next attack", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.NextAttackDamage, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[1], IsPercent = true, DamageType = (DamageType)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Target's Evasion is reduced by %d for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.EvasionVsAll, TimeSpan.FromSeconds((int)args[1]));
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = -((int)args[0]) });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "(This effect stacks with itself.)", args))
                return;

            else if (Tools.Scan(s, "You gain %d%% Melee Evasion for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.EvasionVsAll, TimeSpan.FromSeconds((int)args[1]));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], IsPercent = true });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "You gain Direct %{DamageType} Damage %d and Indirect %{DamageType} Damage %d for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.DirectDamageBoost, TimeSpan.FromSeconds((int)args[4]));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[1], DamageType = (DamageType)args[0] });
                AddResult(AdditionalResult);

                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.IndirectDamageBoost, TimeSpan.FromSeconds((int)args[4]));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[3], DamageType = (DamageType)args[2] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Target takes %d indirect %{DamageType} damage over %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.DoT, TimeSpan.FromSeconds((int)args[2]));
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], DamageType = (DamageType)args[1] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "If target's focus is not on you, target takes %d indirect %{DamageType} damage over %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.DoT, TimeSpan.FromSeconds((int)args[2]));
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], DamageType = (DamageType)args[1] });
                AdditionalResult.Conditional = AbilityEffectConditional.NotFocused;
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Target's movement speeds are reduced by %d%% for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Snare, TimeSpan.FromSeconds((int)args[1]));
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], IsPercent = true });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Deals only half damage if target's focus is on you", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.DamageReduction, TimeSpan.Zero);
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = 50, IsPercent = true });
                AdditionalResult.Conditional = AbilityEffectConditional.Focused;
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Deals %d damage over %d seconds to wolfsbane-vulnerable targets", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.DoT, TimeSpan.FromSeconds((int)args[1]));
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], DamageType = this.DamageType });
                AdditionalResult.Conditional = AbilityEffectConditional.WolfbaneVulnerable;
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "%d Indirect %{DamageType} Mitigation, %d indirect %{DamageType} Mitigation", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.BecomeWerewolf, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AddResult(AdditionalResult);

                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.IndirectMitigation, TimeSpan.MaxValue);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], DamageType = (DamageType)args[1] });
                AddResult(AdditionalResult);

                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.IndirectMitigation, TimeSpan.MaxValue);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[2], DamageType = (DamageType)args[3] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Darkvision, %d %{DamageType} Mitigation, %d %{DamageType} Mitigation", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.DarkVision, TimeSpan.MaxValue);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AddResult(AdditionalResult);

                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Mitigation, TimeSpan.MaxValue);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], DamageType = (DamageType)args[1] });
                AddResult(AdditionalResult);

                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Mitigation, TimeSpan.MaxValue);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[2], DamageType = (DamageType)args[3] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Boosts %{DamageType} damage %d%% for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.DamageBoost, TimeSpan.FromSeconds((int)args[2]));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[1], IsPercent = true, DamageType = (DamageType)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Restores %d Health and regenerates %d Health per second for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Heal, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);

                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Regeneration, TimeSpan.FromSeconds((int)args[2]));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[1] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Restores %d Health", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Heal, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Repairs %d broken bone", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.CureBrokenBone, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Pet gains %d speed for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.SprintSpeed, TimeSpan.FromSeconds((int)args[1]));
                AdditionalResult.Target = AbilityEffectTarget.Pet;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Pet gains %d%% Projectile Evasion for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.EvasionVsProjectile, TimeSpan.FromSeconds((int)args[1]));
                AdditionalResult.Target = AbilityEffectTarget.Pet;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], IsPercent = true });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Pet gains %d%% Projectile Evasion and %d%% Burst Evasion for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.EvasionVsProjectile, TimeSpan.FromSeconds((int)args[2]));
                AdditionalResult.Target = AbilityEffectTarget.Pet;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], IsPercent = true });
                AddResult(AdditionalResult);

                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.EvasionVsBurstAttack, TimeSpan.FromSeconds((int)args[2]));
                AdditionalResult.Target = AbilityEffectTarget.Pet;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[1], IsPercent = true });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "You take %d%% less damage from falls for %d minutes", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.FallDamageMitigation, TimeSpan.FromMinutes((int)args[1]));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], IsPercent = true });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "You take %d%% less damage from falls for %d minutes, and you can change direction in mid-air (during long falls)", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.FallDamageMitigation, TimeSpan.FromMinutes((int)args[1]));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], IsPercent = true });
                AddResult(AdditionalResult);

                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.JumpControl, TimeSpan.FromMinutes((int)args[1]));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Direct attacks deal %d extra damage", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.DirectDamageBoost, TimeSpan.FromSeconds(666));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "(But always consumes exactly %d arrows.)", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.ConsumeArrows, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Sigil explodes to deal %d burst %{DamageType} damage plus %d %{DamageType} damage over %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Damage, TimeSpan.FromSeconds(666));
                AdditionalResult.Target = AbilityEffectTarget.Sigil;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], DamageType = (DamageType)args[1] });
                AddResult(AdditionalResult);

                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.DoT, TimeSpan.FromSeconds((int)args[4]));
                AdditionalResult.Target = AbilityEffectTarget.Sigil;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[2], DamageType = (DamageType)args[3] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Sigil heals nearby allies for %d health every %d seconds while in combat", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.HoT, TimeSpan.FromSeconds((int)args[1]));
                AdditionalResult.Target = AbilityEffectTarget.Sigil;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Sigil uses burst attacks that deal %d %{DamageType} damage", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Damage, TimeSpan.FromSeconds(666));
                AdditionalResult.Target = AbilityEffectTarget.Sigil;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], DamageType = (DamageType)args[1] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Sigil uses long range attacks that deal %d %{DamageType} damage plus %d armor damage over %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Damage, TimeSpan.FromSeconds(666));
                AdditionalResult.Target = AbilityEffectTarget.Sigil;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], DamageType = (DamageType)args[1] });
                AddResult(AdditionalResult);

                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.ArmorDoT, TimeSpan.FromSeconds((int)args[3]));
                AdditionalResult.Target = AbilityEffectTarget.Sigil;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[2], DamageType = (DamageType)args[1] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Sigil can teleport willing users to a random Teleportation Circle that they have used recently", args))
                AddResult(new AbilityAdditionalResult(AbilityEffect.SigilTeleportation, TimeSpan.Zero));

            else if (Tools.Scan(s, "Instantly heals you for %d health, increased by any First Aid or Major Healing bonuses you have", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Heal, TimeSpan.Zero);
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "You recover %d Power", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.RestorePower, TimeSpan.Zero);
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "You recover %d Health", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Heal, TimeSpan.Zero);
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "You lose up to %d Armor and regain that much Health", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.ArmorToHealth, TimeSpan.Zero);
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Corporeal undead are stunned", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Stun, TimeSpan.Zero);
                AdditionalResult.Conditional = AbilityEffectConditional.CorporealUndead;
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "This ability can only be used when you are wearing four or more pieces of Gazluk Death Trooper Armor", args))
            {
                if (AbilityAdditionalResultList.Count > 0)
                {
                    AdditionalResult = AbilityAdditionalResultList[AbilityAdditionalResultList.Count - 1];
                    AdditionalResult.Conditional = AbilityEffectConditional.GazlukDeathTrooperArmor;
                }
                return;
            }

            else if (Tools.Scan(s, "If Long Ear was active, it ends immediately", args))
            {
                if (AbilityAdditionalResultList.Count > 0)
                {
                    AdditionalResult = AbilityAdditionalResultList[AbilityAdditionalResultList.Count - 1];
                    AdditionalResult.Conditional = AbilityEffectConditional.RemoveLongEar;
                }
                return;
            }

            else if (Tools.Scan(s, "You gain %d%% Projectile Evasion for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.EvasionVsProjectile, TimeSpan.FromSeconds((int)args[1]));
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], IsPercent = true });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "This ability can only be used when you are wearing four or more pieces of Gazluk Misery Trooper Armor", args))
            {
                if (AbilityAdditionalResultList.Count > 0)
                {
                    AdditionalResult = AbilityAdditionalResultList[AbilityAdditionalResultList.Count - 1];
                    AdditionalResult.Conditional = AbilityEffectConditional.GazlukMiseryTrooperArmor;
                }
                return;
            }

            else if (Tools.Scan(s, "This ability can only be used when you are wearing four or more pieces of Gazluk Treachery Trooper Armor", args))
            {
                if (AbilityAdditionalResultList.Count > 0)
                {
                    AdditionalResult = AbilityAdditionalResultList[AbilityAdditionalResultList.Count - 1];
                    AdditionalResult.Conditional = AbilityEffectConditional.GazlukTreacheryTrooperArmor;
                }
                return;
            }

            else if (Tools.Scan(s, "This ability can only be used when you are wearing four or more pieces of Gazluk Officer Armor", args))
            {
                if (AbilityAdditionalResultList.Count > 0)
                {
                    AdditionalResult = AbilityAdditionalResultList[AbilityAdditionalResultList.Count - 1];
                    AdditionalResult.Conditional = AbilityEffectConditional.GazlukOfficerArmor;
                }
                return;
            }

            else if (Tools.Scan(s, "This ability can only be used when you are wearing four or more pieces of Gazluk War-Wizard Armor", args))
            {
                if (AbilityAdditionalResultList.Count > 0)
                {
                    AdditionalResult = AbilityAdditionalResultList[AbilityAdditionalResultList.Count - 1];
                    AdditionalResult.Conditional = AbilityEffectConditional.GazlukWarWizardArmor;
                }
                return;
            }

            else if (Tools.Scan(s, "This ability can only be used when you are wearing four or more pieces of Mutterer Necromancer Armor", args))
            {
                if (AbilityAdditionalResultList.Count > 0)
                {
                    AdditionalResult = AbilityAdditionalResultList[AbilityAdditionalResultList.Count - 1];
                    AdditionalResult.Conditional = AbilityEffectConditional.GazlukNecromancerArmor;
                }
                return;
            }

            else if (Tools.Scan(s, "This ability can only be used when you are wearing four or more pieces of Mutterer Cabalist Armor", args))
            {
                if (AbilityAdditionalResultList.Count > 0)
                {
                    AdditionalResult = AbilityAdditionalResultList[AbilityAdditionalResultList.Count - 1];
                    AdditionalResult.Conditional = AbilityEffectConditional.GazlukCabalistArmor;
                }
                return;
            }

            else if (Tools.Scan(s, "You gain %d%% Stun Evasion and %d%% Knockback Evasion for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.StunEvasion, TimeSpan.FromSeconds((int)args[2]));
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], IsPercent = true });
                AddResult(AdditionalResult);

                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.KnockbackEvasion, TimeSpan.FromSeconds((int)args[2]));
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[1], IsPercent = true });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "You regain health, and your target decides not to attack you for %d-%d seconds (even if you attack it during that time)", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Heal, TimeSpan.Zero);
                if (PvE != null && PvE.SpecialValueList.Count > 0)
                    AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = PvE.SpecialValueList[0].Value });
                AddResult(AdditionalResult);

                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Peacefulness, TimeSpan.FromSeconds((int)args[0]));
                AdditionalResult.RandomDuration = TimeSpan.FromSeconds((int)args[1]) - TimeSpan.FromSeconds((int)args[0]);
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Automatically dispelled when indoors", args))
                AddResult(new AbilityAdditionalResult(AbilityEffect.DispelledIndoor, TimeSpan.Zero));

            else if (Tools.Scan(s, "Owl has nightvision", args))
                AddResult(new AbilityAdditionalResult(AbilityEffect.NightVision, TimeSpan.MaxValue));

            else if (Tools.Scan(s, "Mallard has enhanced swim speed", args))
                AddResult(new AbilityAdditionalResult(AbilityEffect.SwimSpeed, TimeSpan.MaxValue));

            else if (Tools.Scan(s, "Snow raven has %d indirect %{DamageType} mitigation and %d indirect %{DamageType} mitigation", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.IndirectMitigation, TimeSpan.MaxValue);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], DamageType = (DamageType)args[1] });
                AddResult(AdditionalResult);

                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.IndirectMitigation, TimeSpan.MaxValue);
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[2], DamageType = (DamageType)args[3] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Target is randomly stunned occasionally during the next %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.RandomStun, TimeSpan.FromSeconds((int)args[0]));
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Restoration is boosted by items that boost Patch Armor", args))
                AddResult(new AbilityAdditionalResult(AbilityEffect.PatchArmorBoost, TimeSpan.Zero));

            else if (Tools.Scan(s, "Target has a %d%% chance to evade all attacks, and regains %d Power over %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.EvasionVsAll, TimeSpan.FromSeconds(666));
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], IsPercent = true });
                AddResult(AdditionalResult);

                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.RestorePower, TimeSpan.FromSeconds((int)args[2]));
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[1] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "For %d seconds, Melee attackers take %d %{DamageType} damage", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.DamageShield, TimeSpan.FromSeconds((int)args[0]));
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[1], DamageType = (DamageType)args[2] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Other werewolves' Pack Attacks deal %d damage", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.PackAttackBonus, TimeSpan.MaxValue);
                AdditionalResult.Target = AbilityEffectTarget.SelfAndAllies;
                AdditionalResult.Conditional = AbilityEffectConditional.Werewolf;
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Direct and Indirect %{DamageType} Mitigation %d for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.DirectMitigation, TimeSpan.FromSeconds((int)args[2]));
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[1], DamageType = (DamageType)args[0] });
                AddResult(AdditionalResult);

                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.IndirectMitigation, TimeSpan.FromSeconds((int)args[2]));
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[1], DamageType = (DamageType)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Effect continues for %d seconds, or until you move or use a different Song", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.ProlongedEffect, TimeSpan.FromSeconds((int)args[0]));
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Every %d seconds, damages all enemies within %d meters, skipping enemies that are not yet engaged in combat", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.DamageEngaged, TimeSpan.MaxValue);
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "All nearby allies gain %d%% Projectile Evasion for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.EvasionVsProjectile, TimeSpan.FromSeconds((int)args[1]));
                AdditionalResult.Target = AbilityEffectTarget.SelfAndAllies;
                AdditionalResult.AoERange = (PvE != null )? PvE.AoE : 0;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], IsPercent = true });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "All nearby allies gain %d%% Melee Evasion for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.EvasionVsMelee, TimeSpan.FromSeconds((int)args[1]));
                AdditionalResult.Target = AbilityEffectTarget.SelfAndAllies;
                AdditionalResult.AoERange = (PvE != null )? PvE.AoE : 0;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], IsPercent = true });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "For %d seconds, target's attacks deal %d%% direct damage and cost %d Power", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.DamageBoost, TimeSpan.FromSeconds((int)args[0]));
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[1], IsPercent = true });
                AddResult(AdditionalResult);

                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.PowerCostReduction, TimeSpan.FromSeconds((int)args[0]));
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[2] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "Eligible dead allies return to life with %d%% of their Max Health", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Rezz, TimeSpan.Zero);
                AdditionalResult.Target = AbilityEffectTarget.SelfAndAllies;
                AdditionalResult.AoERange = (PvE != null )? PvE.AoE : 0;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], IsPercent = true });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "For %d seconds, all nearby allies take %d%% less damage from %{DamageType}/%{DamageType}/%{DamageType} attacks", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Mitigation, TimeSpan.FromSeconds((int)args[0]));
                AdditionalResult.Target = AbilityEffectTarget.SelfAndAllies;
                AdditionalResult.AoERange = (PvE != null )? PvE.AoE : 0;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], IsPercent = true, DamageType = (DamageType)args[1] });
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], IsPercent = true, DamageType = (DamageType)args[2] });
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[0], IsPercent = true, DamageType = (DamageType)args[3] });
                AddResult(AdditionalResult);
            }

            else if (Tools.Scan(s, "For %d minutes, the target's liver metabolizes alcohol twice as fast as normal", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "Instantly reduce target's blood alcohol level by one tier", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "For %d minutes, the target's liver metabolizes alcohol three times faster than normal", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "You gain %d sprint speed and for %d seconds, all attacks deal +%d damage", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "Target takes +%d damage from all direct attacks for %d seconds", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "Targets decide not to attack you for about %d seconds (unless you attack them first)", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "For %d seconds, sprint speed is reduced by %d%% and monster-detection range is reduced by %d meters", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "+%d %{DamageType} Protection (direct and indirect) for %d minutes", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "Target is stunned after a %d second delay", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "If target succumbs to fear, it flees for %d seconds", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "You must be wearing a %s{to use this} to use this ability", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "You must be wearing an %s{to use this} to use this ability", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "After using this ability, you take %d% more damage from all attacks for %d seconds", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "You evade %d% of all attacks", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "Your movement speed is %d", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "Pet attempts to perform its pet-specific Clever Trick", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "Pet gains +%d speed for %d seconds and pet attempts to use its pet-specific Special Attack", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "(How well the pet focuses on your enemies depends in part on the pet's Bond Level.)", args))
            {
            }

            else if (Tools.Scan(s, "Targets are knocked back", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "For %d seconds, you gain Direct Poison Damage +%d and Indirect Poison Damage +%d per tick", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "Undead take +%d% damage", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "Dispels stun, root, or slow effects", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "Grants immunity to stun, root, and slow effects for %d seconds", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "When the target next uses a Rage Attack, they are stunned", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "Removes all ongoing Poison effects from the target (up to %d dmg/sec)", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "First ally to die will resurrect a few seconds after death with %d% of their Health and Power", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "Target gains Indirect Cold Mitigation +%d for %d seconds", args))
            {
                //TODO
            }

            else
            {
                ErrorInfo.AddUnparsedSpecialInfo(s);
                return;
            }
        }

        private void AddResult(AbilityAdditionalResult AdditionalResult)
        {
            AbilityAdditionalResultList.Add(AdditionalResult);
        }
        #endregion

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddString("AbilityGroup", RawAbilityGroup);
            Generator.AddString("Animation", Animation.ToString());
            Generator.AddStringList("AttributesThatDeltaPowerCost", RawAttributesThatDeltaPowerCostList, RawAttributesThatDeltaPowerCostListIsEmpty);
            Generator.AddStringList("AttributesThatDeltaResetTime", RawAttributesThatDeltaResetTimeList, RawAttributesThatDeltaResetTimeListIsEmpty);
            Generator.AddStringList("AttributesThatModPowerCost", RawAttributesThatModPowerCostList, RawAttributesThatModPowerCostListIsEmpty);
            Generator.AddBoolean("CanBeOnSidebar", RawCanBeOnSidebar);
            Generator.AddBoolean("CanSuppressMonsterShout", RawCanSuppressMonsterShout);
            Generator.AddBoolean("CanTargetUntargetableEnemies", RawCanTargetUntargetableEnemies);
            StringToEnumConversion<Deaths>.ListToString(Generator, "CausesOfDeath", CausesOfDeathList);

            if (CostList.Count > 0)
            {
                Generator.OpenArray("Costs");

                foreach (RecipeCost Cost in CostList)
                    Cost.GenerateObjectContent(Generator);

                Generator.CloseArray();
            }

            Generator.AddInteger("CombatRefreshBaseAmount", RawCombatRefreshBaseAmount);
            StringToEnumConversion<PowerSkill>.ListToString(Generator, "CompatibleSkills", RawCompatibleSkillList);
            Generator.AddDouble("ConsumedItemChance", RawConsumedItemChance);
            Generator.AddDouble("ConsumedItemChanceToStickInCorpse", RawConsumedItemChanceToStickInCorpse);
            Generator.AddInteger("ConsumedItemCount", RawConsumedItemCount);
            Generator.AddString("ConsumedItemKeyword", RawConsumedItemKeyword);
            Generator.AddString("DamageType", StringToEnumConversion<DamageType>.ToString(DamageType, null, DamageType.Internal_None, DamageType.Internal_Empty));
            Generator.AddBoolean("DelayLoopIsAbortedIfAttacked", RawDelayLoopIsAbortedIfAttacked);
            Generator.AddString("DelayLoopMessage", DelayLoopMessage);
            Generator.AddDouble("DelayLoopTime", RawDelayLoopTime);
            Generator.AddString("Description", Description);

            List<AbilityIndicatingEnabled> EffectKeywordsIndicatingEnabledList = new List<AbilityIndicatingEnabled>();
            if (EffectKeywordsIndicatingEnabled != AbilityIndicatingEnabled.Internal_None)
                EffectKeywordsIndicatingEnabledList.Add(EffectKeywordsIndicatingEnabled);
            StringToEnumConversion<AbilityIndicatingEnabled>.ListToString(Generator, "EffectKeywordsIndicatingEnabled", EffectKeywordsIndicatingEnabledList);

            List<TooltipsExtraKeywords> ExtraKeywordsForTooltipList = new List<TooltipsExtraKeywords>();
            if (ExtraKeywordsForTooltips != TooltipsExtraKeywords.Internal_None)
                ExtraKeywordsForTooltipList.Add(ExtraKeywordsForTooltips);
            StringToEnumConversion<TooltipsExtraKeywords>.ListToString(Generator, "ExtraKeywordsForTooltips", ExtraKeywordsForTooltipList);

            Generator.AddInteger("IconID", RawIconId);
            Generator.AddBoolean("InternalAbility", RawInternalAbility);
            Generator.AddString("InternalName", InternalName);
            Generator.AddBoolean("IsHarmless", RawIsHarmless);
            Generator.AddString("ItemKeywordReqErrorMessage", ItemKeywordReqErrorMessage);
            StringToEnumConversion<AbilityItemKeyword>.ListToString(Generator, "ItemKeywordReqs", ItemKeywordReqList, TextMaps.AbilityItemKeywordStringMap);
            StringToEnumConversion<AbilityKeyword>.ListToString(Generator, "Keywords", KeywordList);
            Generator.AddInteger("Level", RawLevel);
            Generator.AddString("Name", Name);
            Generator.AddEnum("PetTypeTagReq", PetTypeTagReq);
            Generator.AddInteger("PetTypeTagReqMax", RawPetTypeTagReqMax);
            Generator.AddString("Prerequisite", RawPrerequisite);
            Generator.AddEnum("Projectile", Projectile);

            if (PvE != null)
                PvE.GenerateObjectContent(Generator);

            if (PvP != null)
                PvP.GenerateObjectContent(Generator);

            Generator.AddDouble("ResetTime", RawResetTime);
            Generator.AddString("SelfParticle", SelfParticle);
            Generator.AddString("SharesResetTimerWith", RawSharesResetTimerWith);
            Generator.AddEnum("Skill", Skill);

            if (SpecialCasterRequirementList.Count > 1)
            {
                Generator.OpenArray("SpecialCasterRequirements");

                foreach (AbilityRequirement Item in SpecialCasterRequirementList)
                {
                    Generator.OpenObject(null);

                    Item.GenerateObjectContent(Generator);

                    Generator.CloseObject();
                }

                Generator.CloseArray();
            }
            else if (SpecialCasterRequirementList.Count > 0)
            {
                Generator.OpenObject("SpecialCasterRequirements");

                AbilityRequirement Item = SpecialCasterRequirementList[0];
                Item.GenerateObjectContent(Generator);

                Generator.CloseObject();
            }

            Generator.AddString("SpecialInfo", SpecialInfo);
            Generator.AddInteger("SpecialTargetingTypeReq", RawSpecialTargetingTypeReq);
            Generator.AddEnum("Target", Target);
            Generator.AddString("TargetEffectKeywordReq", StringToEnumConversion<TargetEffectKeyword>.ToString(TargetEffectKeywordReq, null, TargetEffectKeyword.Internal_None));
            Generator.AddEnum("TargetParticle", TargetParticle);
            Generator.AddString("UpgradeOf", RawUpgradeOf);
            Generator.AddBoolean("WorksInCombat", RawWorksInCombat);
            Generator.AddBoolean("WorksUnderwater", RawWorksUnderwater);
            Generator.AddBoolean("WorksWhileFalling", RawWorksWhileFalling); 

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
                if (Animation != AbilityAnimation.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.AbilityAnimationTextMap[Animation]);
                if (RawCanBeOnSidebar.HasValue)
                    AddWithFieldSeparator(ref Result, "Can Be On Sidebar");
                if (RawCanSuppressMonsterShout.HasValue)
                    AddWithFieldSeparator(ref Result, "Suppress Monster Shout");
                if (RawCanTargetUntargetableEnemies.HasValue)
                    AddWithFieldSeparator(ref Result, "Can Target Untargetable");
                foreach (Deaths Item in CausesOfDeathList)
                    AddWithFieldSeparator(ref Result, TextMaps.DeathsTextMap[Item]);
                foreach (RecipeCost Item in CostList)
                    AddWithFieldSeparator(ref Result, TextMaps.RecipeCurrencyTextMap[Item.Currency]);
                if (CompatibleSkill != PowerSkill.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.PowerSkillTextMap[CompatibleSkill]);
                if (ConsumedItem != null)
                    AddWithFieldSeparator(ref Result, ConsumedItem.TextContent);
                if (DamageType != DamageType.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.DamageTypeTextMap[DamageType]);
                AddWithFieldSeparator(ref Result, DelayLoopMessage);
                if (RawDelayLoopIsAbortedIfAttacked.HasValue)
                    AddWithFieldSeparator(ref Result, RawDelayLoopIsAbortedIfAttacked.Value ? "aborted if attacked" : "not aborted if attacked");
                AddWithFieldSeparator(ref Result, Description);
                if (EffectKeywordsIndicatingEnabled != AbilityIndicatingEnabled.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.AbilityIndicatingEnabledTextMap[EffectKeywordsIndicatingEnabled]);
                if (RawInternalAbility.HasValue)
                    AddWithFieldSeparator(ref Result, "Is Internal");
                if (RawIsHarmless.HasValue)
                    AddWithFieldSeparator(ref Result, "Is Harmless");
                AddWithFieldSeparator(ref Result, ItemKeywordReqErrorMessage);
                foreach (AbilityRequirement Requirement in CombinedRequirementList)
                    AddWithFieldSeparator(ref Result, Requirement.TextContent);
                if (ExtraKeywordsForTooltips != TooltipsExtraKeywords.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.TooltipsExtraKeywordsTextMap[ExtraKeywordsForTooltips]);
                foreach (AbilityKeyword Item in KeywordList)
                    AddWithFieldSeparator(ref Result, TextMaps.AbilityKeywordTextMap[Item]);
                AddWithFieldSeparator(ref Result, Name);
                if (PetTypeTagReq != AbilityPetType.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.AbilityPetTypeTextMap[PetTypeTagReq]);
                if (Prerequisite != null)
                    AddWithFieldSeparator(ref Result, Prerequisite.Name);
                if (Projectile != AbilityProjectile.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.AbilityProjectileTextMap[Projectile]);
                //TODO PvE, PvP
                if (SharesResetTimerWith != null)
                    AddWithFieldSeparator(ref Result, SharesResetTimerWith.Name);
                if (ConnectedSkill != null)
                    AddWithFieldSeparator(ref Result, ConnectedSkill.Name);
                AddWithFieldSeparator(ref Result, SpecialInfo);
                if (Target != AbilityTarget.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.AbilityTargetTextMap[Target]);
                if (TargetEffectKeywordReq != TargetEffectKeyword.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.TargetEffectKeywordTextMap[TargetEffectKeywordReq]);
                if (TargetParticle != AbilityTargetParticle.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.AbilityTargetParticleTextMap[TargetParticle]);
                if (UpgradeOf != null)
                    AddWithFieldSeparator(ref Result, UpgradeOf.Name);
                if (RawWorksInCombat.HasValue)
                    AddWithFieldSeparator(ref Result, "Works In Combat");
                if (RawWorksUnderwater.HasValue)
                    AddWithFieldSeparator(ref Result, "Works Underwater");
                if (RawWorksWhileFalling.HasValue)
                    AddWithFieldSeparator(ref Result, "Works While Falling");
                if (AbilityGroup != null)
                    AddWithFieldSeparator(ref Result, AbilityGroup.Name);
                if (RawIgnoreEffectErrors.HasValue)
                    AddWithFieldSeparator(ref Result, "Ignore Effect Errors");

                return Result;
            }
        }

        private List<string> RawAttributesThatDeltaPowerCostList = new List<string>();
        private bool RawAttributesThatDeltaPowerCostListIsEmpty;
        private List<string> RawAttributesThatDeltaResetTimeList = new List<string>();
        private bool RawAttributesThatDeltaResetTimeListIsEmpty;
        private List<string> RawAttributesThatModPowerCostList = new List<string>();
        private bool RawAttributesThatModPowerCostListIsEmpty;
        private string RawPrerequisite;
        private bool IsRawPrerequisiteParsed;
        private string RawSharesResetTimerWith;
        private bool IsRawSharesResetTimerWithParsed;
        private string RawUpgradeOf;
        private bool IsRawUpgradeOfParsed;
        private string RawAbilityGroup;
        private bool IsRawAbilityGroupParsed;
        private List<string> RawAttributesThatDeltaAmmoStickChanceList = new List<string>();
        private bool RawAttributesThatDeltaAmmoStickChanceListIsEmpty;
        private List<string> RawAttributesThatDeltaDelayLoopTimeList = new List<string>();
        private bool RawAttributesThatDeltaDelayLoopTimeListIsEmpty;
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = false;
            Dictionary<string, IGenericJsonObject> AttributeTable = AllTables[typeof(Attribute)];
            Dictionary<string, IGenericJsonObject> ItemTable = AllTables[typeof(Item)];
            Dictionary<string, IGenericJsonObject> AbilityTable = AllTables[typeof(Ability)];
            Dictionary<string, IGenericJsonObject> SkillTable = AllTables[typeof(Skill)];

            IsConnected |= Attribute.ConnectTable(ErrorInfo, AttributeTable, RawAttributesThatDeltaPowerCostList, AttributesThatDeltaPowerCostTable);
            IsConnected |= Attribute.ConnectTable(ErrorInfo, AttributeTable, RawAttributesThatDeltaResetTimeList, AttributesThatDeltaResetTimeTable);
            IsConnected |= Attribute.ConnectTable(ErrorInfo, AttributeTable, RawAttributesThatModPowerCostList, AttributesThatModPowerCostTable);

            Prerequisite = Ability.ConnectSingleProperty(ErrorInfo, AbilityTable, RawPrerequisite, Prerequisite, ref IsRawPrerequisiteParsed, ref IsConnected, this);
            AbilityGroup = Ability.ConnectSingleProperty(ErrorInfo, AbilityTable, RawAbilityGroup, AbilityGroup, ref IsRawAbilityGroupParsed, ref IsConnected, this);

            if (PvE != null)
                IsConnected |= PvE.Connect(ErrorInfo, this, AllTables);

            if (PvP != null)
                IsConnected |= PvP.Connect(ErrorInfo, this, AllTables);

            foreach (AbilityRequirement Item in SpecialCasterRequirementList)
                IsConnected |= Item.Connect(ErrorInfo, this, AllTables);

            SharesResetTimerWith = Ability.ConnectSingleProperty(ErrorInfo, AbilityTable, RawSharesResetTimerWith, SharesResetTimerWith, ref IsRawSharesResetTimerWithParsed, ref IsConnected, this);
            UpgradeOf = Ability.ConnectSingleProperty(ErrorInfo, AbilityTable, RawUpgradeOf, UpgradeOf, ref IsRawUpgradeOfParsed, ref IsConnected, this);

            if (Skill != PowerSkill.Internal_None && Skill != PowerSkill.AnySkill && Skill != PowerSkill.Unknown)
                ConnectedSkill = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, Skill, ConnectedSkill, ref IsSkillParsed, ref IsConnected, this);

            foreach (RecipeCost Item in CostList)
                Item.Connect(ErrorInfo, this, AllTables);

            ConsumedItemLink = Item.ConnectSingleProperty(null, ItemTable, RawConsumedItemKeyword, ConsumedItemLink, ref IsRawConsumedItemKeywordParsed, ref IsConnected, this);

            IsConnected |= Attribute.ConnectTable(ErrorInfo, AttributeTable, RawAttributesThatDeltaAmmoStickChanceList, AttributesThatDeltaAmmoStickChanceTable);
            IsConnected |= Attribute.ConnectTable(ErrorInfo, AttributeTable, RawAttributesThatDeltaDelayLoopTimeList, AttributesThatDeltaDelayLoopTimeTable);

            return IsConnected;
        }

        public static Ability ConnectSingleProperty(ParseErrorInfo ErrorInfo, Dictionary<string, IGenericJsonObject> AbilityTable, string RawAbilityName, Ability ParsedAbility, ref bool IsRawAbilityParsed, ref bool IsConnected, GenericJsonObject LinkBack)
        {
            if (IsRawAbilityParsed)
                return ParsedAbility;

            IsRawAbilityParsed = true;

            if (RawAbilityName == null)
                return null;

            foreach (KeyValuePair<string, IGenericJsonObject> Entry in AbilityTable)
            {
                Ability AbilityValue = Entry.Value as Ability;
                if (AbilityValue.InternalName == RawAbilityName)
                {
                    IsConnected = true;
                    AbilityValue.AddLinkBack(LinkBack);
                    return AbilityValue;
                }
            }

            if (ErrorInfo != null)
                ErrorInfo.AddMissingKey(RawAbilityName);

            return null;
        }

        public static List<Ability> ConnectByKeyword(ParseErrorInfo ErrorInfo, Dictionary<string, IGenericJsonObject> AbilityTable, AbilityKeyword Keyword, List<Ability> AbilityList, ref bool IsRawAbilityParsed, ref bool IsConnected, GenericJsonObject LinkBack)
        {
            if (IsRawAbilityParsed)
                return AbilityList;

            IsRawAbilityParsed = true;

            if (Keyword == AbilityKeyword.Internal_None)
                return AbilityList;

            AbilityList = new List<Ability>();
            IsConnected = true;

            foreach (KeyValuePair<string, IGenericJsonObject> AbilityEntry in AbilityTable)
            {
                Ability AbilityValue = AbilityEntry.Value as Ability;
                if (AbilityValue.KeywordList.Contains(Keyword))
                {
                    AbilityValue.AddLinkBack(LinkBack);
                    AbilityList.Add(AbilityValue);
                }
            }

            if (AbilityList.Count == 0 && ErrorInfo != null)
                ErrorInfo.AddMissingKey(Keyword.ToString());

            return AbilityList;
        }

        public static Ability ConnectByKey(ParseErrorInfo ErrorInfo, Dictionary<string, IGenericJsonObject> AbilityTable, int AbilityId, Ability Ability, ref bool IsRawAbilityParsed, ref bool IsConnected, GenericJsonObject LinkBack)
        {
            if (IsRawAbilityParsed)
                return Ability;

            IsRawAbilityParsed = true;
            string RawAbilityId = "ability_" + AbilityId;

            foreach (KeyValuePair<string, IGenericJsonObject> Entry in AbilityTable)
            {
                Ability AbilityValue = Entry.Value as Ability;
                if (AbilityValue.Key == RawAbilityId)
                {
                    IsConnected = true;
                    AbilityValue.AddLinkBack(LinkBack);
                    return AbilityValue;
                }
            }

            if (ErrorInfo != null)
                ErrorInfo.AddMissingKey(RawAbilityId);

            return null;
        }
        #endregion

        #region Crunching
        public bool IsAbilityValidForCrunch
        {
            get
            {
                if (!RawResetTime.HasValue)
                    return false;

                if (PvE == null)
                    return false;

                return true;
            }
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "Ability"; } }

        public override string ToString()
        {
            return Name;
        }
        #endregion
    }
}
