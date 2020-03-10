using PgJsonReader;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class Ability : MainJsonObject<Ability>, IPgAbility
    {
        #region Direct Properties
        public AbilityAnimation Animation { get; private set; }
        public bool CanBeOnSidebar { get { return RawCanBeOnSidebar.HasValue && RawCanBeOnSidebar.Value; } }
        public bool? RawCanBeOnSidebar { get; private set; }
        public bool CanSuppressMonsterShout { get { return RawCanSuppressMonsterShout.HasValue && RawCanSuppressMonsterShout.Value; } }
        public bool? RawCanSuppressMonsterShout { get; private set; }
        public bool CanTargetUntargetableEnemies { get { return RawCanTargetUntargetableEnemies.HasValue && RawCanTargetUntargetableEnemies.Value; } }
        public bool? RawCanTargetUntargetableEnemies { get; private set; }
        public bool DelayLoopIsAbortedIfAttacked { get { return RawDelayLoopIsAbortedIfAttacked.HasValue && RawDelayLoopIsAbortedIfAttacked.Value; } }
        public bool? RawDelayLoopIsAbortedIfAttacked { get; private set; }
        public bool InternalAbility { get { return RawInternalAbility.HasValue && RawInternalAbility.Value; } }
        public bool? RawInternalAbility { get; private set; }
        public bool IsHarmless { get { return RawIsHarmless.HasValue && RawIsHarmless.Value; } }
        public bool? RawIsHarmless { get; private set; }
        public bool WorksInCombat { get { return RawWorksInCombat.HasValue && RawWorksInCombat.Value; } }
        public bool? RawWorksInCombat { get; private set; }
        public bool WorksUnderwater { get { return RawWorksUnderwater.HasValue && RawWorksUnderwater.Value; } }
        public bool? RawWorksUnderwater { get; private set; }
        public List<Deaths> CausesOfDeathList { get; } = new List<Deaths>();
        public IPgRecipeCostCollection CostList { get; } = new RecipeCostCollection();
        public int CombatRefreshBaseAmount { get { return RawCombatRefreshBaseAmount.HasValue ? RawCombatRefreshBaseAmount.Value : 0; } }
        public int? RawCombatRefreshBaseAmount { get; private set; }
        public PowerSkill CompatibleSkill { get; private set; }
        public DamageType DamageType { get; private set; }
        public string SpecialCasterRequirementsErrorMessage { get; private set; }
        public double ConsumedItemChance { get { return RawConsumedItemChance.HasValue ? RawConsumedItemChance.Value : 0; } }
        public double? RawConsumedItemChance { get; private set; }
        public double ConsumedItemChanceToStickInCorpse { get { return RawConsumedItemChanceToStickInCorpse.HasValue ? RawConsumedItemChanceToStickInCorpse.Value : 0; } }
        public double? RawConsumedItemChanceToStickInCorpse { get; private set; }
        public int ConsumedItemCount { get { return RawConsumedItemCount.HasValue ? RawConsumedItemCount.Value : 0; } }
        public int? RawConsumedItemCount { get; private set; }
        public string DelayLoopMessage { get; private set; }
        public double DelayLoopTime { get { return RawDelayLoopTime.HasValue ? RawDelayLoopTime.Value : 0; } }
        public double? RawDelayLoopTime { get; private set; }
        public string Description { get; private set; }
        public AbilityIndicatingEnabled EffectKeywordsIndicatingEnabled { get; private set; }
        public AbilityPetType PetTypeTagReq { get; private set; }
        public int IconId { get { return RawIconId.HasValue ? RawIconId.Value : 0; } }
        public int? RawIconId { get; private set; }
        public string InternalName { get; private set; }
        public string ItemKeywordReqErrorMessage { get; private set; }
        public List<AbilityItemKeyword> ItemKeywordReqList { get; } = new List<AbilityItemKeyword>();
        public List<AbilityKeyword> KeywordList { get; } = new List<AbilityKeyword>();
        public int Level { get { return RawLevel.HasValue ? RawLevel.Value : 0; } }
        public int? RawLevel { get; private set; }
        public string Name { get; private set; }
        public int PetTypeTagReqMax { get { return RawPetTypeTagReqMax.HasValue ? RawPetTypeTagReqMax.Value : 0; } }
        public int? RawPetTypeTagReqMax { get; private set; }
        public IPgAbility Prerequisite { get; private set; }
        public AbilityProjectile Projectile { get; private set; }
        public AbilityTarget Target { get; private set; }
        public IPgAbilityPvX PvE { get; private set; }
        public IPgAbilityPvX PvP { get; private set; }
        public double ResetTime { get { return RawResetTime.HasValue ? RawResetTime.Value : 0; } }
        public double? RawResetTime { get; private set; }
        public string SelfParticle { get; private set; }
        public IPgAbility SharesResetTimerWith { get; private set; }
        public IPgSkill Skill { get; private set; }
        public IPgAbilityRequirementCollection CombinedRequirementList { get; private set; }
        public string SpecialInfo { get; private set; }
        public int SpecialTargetingTypeReq { get { return RawSpecialTargetingTypeReq.HasValue ? RawSpecialTargetingTypeReq.Value : 0; } }
        public int? RawSpecialTargetingTypeReq { get; private set; }
        public TargetEffectKeyword TargetEffectKeywordReq { get; private set; }
        public AbilityTargetParticle TargetParticle { get; private set; }
        public IPgAbility UpgradeOf { get; private set; }
        public TooltipsExtraKeywords ExtraKeywordsForTooltips { get; private set; }
        public ConsumedItemCategory ConsumedItems { get; private set; }
        public IPgAbility AbilityGroup { get; private set; }
        public IPgAbilityRequirementCollection SpecialCasterRequirementList { get; } = new AbilityRequirementCollection();
        public IPgAttributeCollection AttributesThatModAmmoConsumeChanceList { get; private set; } = null;
        public IPgAttributeCollection AttributesThatDeltaDelayLoopTimeList { get; private set; } = null;
        public IPgAttributeCollection AttributesThatDeltaPowerCostList { get; private set; } = null;
        public IPgAttributeCollection AttributesThatDeltaResetTimeList { get; private set; } = null;
        public IPgAttributeCollection AttributesThatModPowerCostList { get; private set; } = null;
        public IPgItem ConsumedItemLink { get; private set; }
        public bool WorksWhileFalling { get { return RawWorksWhileFalling.HasValue && RawWorksWhileFalling.Value; } }
        public bool? RawWorksWhileFalling { get; private set; }
        public bool IgnoreEffectErrors { get { return RawIgnoreEffectErrors.HasValue && RawIgnoreEffectErrors.Value; } }
        public bool? RawIgnoreEffectErrors { get; private set; }
        public bool RawAttributesThatModAmmoConsumeChanceListIsEmpty { get; private set; }
        public bool RawAttributesThatDeltaDelayLoopTimeListIsEmpty { get; private set; }
        public bool RawAttributesThatDeltaPowerCostListIsEmpty { get; private set; }
        public bool RawAttributesThatDeltaResetTimeListIsEmpty { get; private set; }
        public bool RawAttributesThatModPowerCostListIsEmpty { get; private set; }
        public PowerSkill RawSkill { get; private set; }
        public IPgGenericSourceCollection SourceList { get; private set; } = new PgGenericSourceCollection();
        public IPgItem ConsumedItemDescription { get; private set; }
        public bool DelayLoopIsOnlyUsedInCombat { get { return RawDelayLoopIsOnlyUsedInCombat.HasValue && RawDelayLoopIsOnlyUsedInCombat.Value; } }
        public bool? RawDelayLoopIsOnlyUsedInCombat { get; private set; }

        private bool IsSkillParsed;
        private bool IsRawConsumedItemKeywordParsed;
        private ItemKeyword ConsumedItemKeyword;
        private string RawConsumedItemDescription;
        private bool IsRawConsumedItemDescriptionParsed;
        #endregion

        #region Indirect Properties
        public override string SortingName { get { return Name; } }
        public string DigitStrippedName { get; private set; }
        public int LineIndex { get; private set; }
        public List<AbilityAdditionalResult> AbilityAdditionalResultList { get; } = new List<AbilityAdditionalResult>();
        public string SearchResultIconFileName { get { return RawIconId.HasValue && RawIconId.Value > 0 ? "icon_" + RawIconId.Value : null; } }
        public ConsumedItem ConsumedItem { get; private set; }

        public override void SetIndirectProperties(Dictionary<Type, Dictionary<string, IJsonKey>> AllTables, ParseErrorInfo ErrorInfo)
        {
            PgAbility.GetDigitStrippedName(InternalName, out string DigitStrippedName, out int LineIndex);
            this.DigitStrippedName = DigitStrippedName;
            this.LineIndex = LineIndex;

            CombinedRequirementList = new AbilityRequirementCollection();

            foreach (AbilityItemKeyword Keyword in ItemKeywordReqList)
                CombinedRequirementList.Add(new AbilityRequirementInternal(Keyword));

            foreach (IPgAbilityRequirement Item in SpecialCasterRequirementList)
                CombinedRequirementList.Add(Item);

            ConsumedItem = CreateConsumedItem(ConsumedItemLink, ConsumedItems, RawConsumedItemCount, RawConsumedItemChance, RawConsumedItemChanceToStickInCorpse);
        }

        public static ConsumedItem CreateConsumedItem(IPgItem ConsumedItemLink, ConsumedItemCategory ConsumedItems, int? RawConsumedItemCount, double? RawConsumedItemChance, double? RawConsumedItemChanceToStickInCorpse)
        {
            ConsumedItem Result;

            if (ConsumedItemLink != null)
                Result = new ConsumedItemDirect(ConsumedItemLink, RawConsumedItemCount, RawConsumedItemChance, RawConsumedItemChanceToStickInCorpse);
            else if (ConsumedItems != ConsumedItemCategory.Internal_None)
                Result = new ConsumedItemByKeyword(ConsumedItems, RawConsumedItemCount, RawConsumedItemChance, RawConsumedItemChanceToStickInCorpse);
            else
                Result = null;

            return Result;
        }

        public void SetSource(IPgGenericSource Source, ParseErrorInfo ErrorInfo)
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
                ParseString = (string value, ParseErrorInfo errorInfo) => RawAbilityGroup = value,
                GetString = () => AbilityGroup != null ? AbilityGroup.InternalName : null } },
            { "Animation", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Animation = StringToEnumConversion<AbilityAnimation>.Parse(value, errorInfo),
                GetString = () => StringToEnumConversion<AbilityAnimation>.ToString(Animation, null, AbilityAnimation.Internal_None) } },
            { "AttributesThatModAmmoConsumeChance", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => RawAttributesThatModAmmoConsumeChanceList.Add(value),
                SetArrayIsEmpty = () => RawAttributesThatModAmmoConsumeChanceListIsEmpty = true,
                GetStringArray = () => AttributesThatModAmmoConsumeChanceList.ToKeyList,
                GetArrayIsEmpty = () => RawAttributesThatModAmmoConsumeChanceListIsEmpty } },
            { "AttributesThatDeltaDelayLoopTime", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => RawAttributesThatDeltaDelayLoopTimeList.Add(value),
                SetArrayIsEmpty = () => RawAttributesThatDeltaDelayLoopTimeListIsEmpty = true,
                GetStringArray = () => AttributesThatDeltaDelayLoopTimeList.ToKeyList,
                GetArrayIsEmpty = () => RawAttributesThatDeltaDelayLoopTimeListIsEmpty } },
            { "AttributesThatDeltaPowerCost", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => RawAttributesThatDeltaPowerCostList.Add(value),
                SetArrayIsEmpty = () => RawAttributesThatDeltaPowerCostListIsEmpty = true,
                GetStringArray = () => AttributesThatDeltaPowerCostList.ToKeyList,
                GetArrayIsEmpty = () => RawAttributesThatDeltaPowerCostListIsEmpty } },
            { "AttributesThatDeltaResetTime", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => RawAttributesThatDeltaResetTimeList.Add(value),
                SetArrayIsEmpty = () => RawAttributesThatDeltaResetTimeListIsEmpty = true,
                GetStringArray = () => AttributesThatDeltaResetTimeList.ToKeyList,
                GetArrayIsEmpty = () => RawAttributesThatDeltaResetTimeListIsEmpty } },
            { "AttributesThatModPowerCost", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => RawAttributesThatModPowerCostList.Add(value),
                SetArrayIsEmpty = () => RawAttributesThatModPowerCostListIsEmpty = true,
                GetStringArray = () => AttributesThatModPowerCostList.ToKeyList,
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
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => { StringToEnumConversion<Deaths>.ParseList(value, CausesOfDeathList, errorInfo); /* StringToEnumConversion<Deaths>.ParseList(value, CausesOfDeathList, errorInfo);*/ },
                GetStringArray = () => StringToEnumConversion<Deaths>.ToStringList(CausesOfDeathList) } },
            { "Costs", new FieldParser() {
                Type = FieldType.ObjectArray,
                ParseObjectArray = (JsonObject value, ParseErrorInfo errorInfo) => JsonObjectParser<RecipeCost>.ParseList("Costs", value, CostList, errorInfo),
                GetObjectArray = () => CostList } },
            { "CombatRefreshBaseAmount", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawCombatRefreshBaseAmount = value,
                GetInteger = () => RawCombatRefreshBaseAmount } },
            /*{ "CompatibleSkills", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => CompatibleSkill = StringToEnumConversion<PowerSkill>.Parse(value, errorInfo),
                GetStringArray = () => StringToEnumConversion<PowerSkill>.ToSingleOrEmptyStringList(CompatibleSkill) } },*/
            { "ConsumedItemChance", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawConsumedItemChance = value,
                GetFloat = () => RawConsumedItemChance } },
            { "ConsumedItemChanceToStickInCorpse", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawConsumedItemChanceToStickInCorpse = value,
                GetFloat = () => RawConsumedItemChanceToStickInCorpse } },
            { "ConsumedItemCount", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawConsumedItemCount = value,
                GetInteger = () => RawConsumedItemCount } },
            { "ConsumedItemKeyword", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseConsumedItemKeyword,
                GetString = GetConsumedItemKeyword } },
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
                ParseFloat = (float value, ParseErrorInfo ErrorInfo) => RawDelayLoopTime = value,
                GetFloat = () => RawDelayLoopTime } },
            { "Description", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Description = value,
                GetString = () => Description } },
            { "EffectKeywordsIndicatingEnabled", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => EffectKeywordsIndicatingEnabled = StringToEnumConversion<AbilityIndicatingEnabled>.Parse(value, errorInfo),
                GetStringArray = () => StringToEnumConversion<AbilityIndicatingEnabled>.ToSingleOrEmptyStringList(EffectKeywordsIndicatingEnabled) } },
            { "ExtraKeywordsForTooltips", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => ExtraKeywordsForTooltips = StringToEnumConversion<TooltipsExtraKeywords>.Parse(value, errorInfo),
                GetStringArray = () => StringToEnumConversion<TooltipsExtraKeywords>.ToSingleOrEmptyStringList(ExtraKeywordsForTooltips) } },
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
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawPetTypeTagReqMax = value,
                GetInteger = () => RawPetTypeTagReqMax } },
            { "Prerequisite", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => RawPrerequisite = value,
                GetString = () => Prerequisite != null ? Prerequisite.InternalName : null } },
            { "Projectile", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Projectile = StringToEnumConversion<AbilityProjectile>.Parse(value, errorInfo),
                GetString = () => StringToEnumConversion<AbilityProjectile>.ToString(Projectile, null, AbilityProjectile.Internal_None) } },
            { "PvE", new FieldParser() {
                Type = FieldType.Object,
                ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => PvE = JsonObjectParser<AbilityPvX>.Parse("PvE", value, errorInfo),
                GetObject = () => PvE as IObjectContentGenerator} },
            { "PvP", new FieldParser() {
                Type = FieldType.Object,
                ParseObject = (JsonObject value, ParseErrorInfo errorInfo) => PvP = JsonObjectParser<AbilityPvX>.Parse("PvP", value, errorInfo),
                GetObject = () => PvP as IObjectContentGenerator } },
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
                ParseString = (string value, ParseErrorInfo errorInfo) => RawSharesResetTimerWith = value,
                GetString = () => SharesResetTimerWith != null ? SharesResetTimerWith.InternalName : null } },
            { "Skill", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseSkill,
                GetString = () => StringToEnumConversion<PowerSkill>.ToString(RawSkill, null, PowerSkill.Internal_None) } },
            { "SpecialCasterRequirements", new FieldParser() {
                Type = FieldType.ObjectArray,
                ParseObjectArray = (JsonObject value, ParseErrorInfo errorInfo) => JsonObjectParser<AbilityRequirement>.ParseList("SpecialCasterRequirement", value, SpecialCasterRequirementList, errorInfo),
                GetObjectArray = () => SpecialCasterRequirementList,
                SimplifyArray = true } },
            { "SpecialCasterRequirementsErrorMessage", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => SpecialCasterRequirementsErrorMessage = value,
                GetString = () => SpecialCasterRequirementsErrorMessage } },
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
                ParseString = (string value, ParseErrorInfo errorInfo) => RawUpgradeOf = value,
                GetString = () => UpgradeOf != null ? UpgradeOf.InternalName: null } },
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
            { "ConsumedItemDescription", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => RawConsumedItemDescription = value,
                GetString = () => RawConsumedItemDescription } },
            { "DelayLoopIsOnlyUsedInCombat", new FieldParser() {
                Type = FieldType.Bool,
                ParseBool = (bool value, ParseErrorInfo errorInfo) => RawDelayLoopIsOnlyUsedInCombat = value,
                GetBool = () => RawDelayLoopIsOnlyUsedInCombat } },
        }; } }

        private void ParseConsumedItemKeyword(string value, ParseErrorInfo errorInfo)
        {
            ConsumedItems = StringToEnumConversion<ConsumedItemCategory>.Parse(value, null);
            if (ConsumedItems == ConsumedItemCategory.Internal_None)
                ConsumedItemKeyword = StringToEnumConversion<ItemKeyword>.Parse(value, TextMaps.ItemKeywordStringMap, errorInfo);
        }

        private string GetConsumedItemKeyword()
        {
            if (ConsumedItemLink != null)
                return ConsumedItemLink.InternalName;
            else if (ConsumedItems != ConsumedItemCategory.Internal_None)
                return StringToEnumConversion<ConsumedItemCategory>.ToString(ConsumedItems);
            else
                return null;
        }

        private void ParseIconId(int value, ParseErrorInfo errorInfo)
        {
            RawIconId = value;

            if (value > 0)
            {
                errorInfo.AddIconId(value);
                PgJsonObjects.Skill.UpdateAnySkillIcon(RawSkill, value);
            }
        }

        private void ParseKeywords(string RawKeywords, ParseErrorInfo ErrorInfo)
        {
            if (StringToEnumConversion<AbilityKeyword>.TryParse(RawKeywords, out AbilityKeyword ParsedAbilityKeyword, ErrorInfo))
            {
                bool HasBasicAttack = ParsedAbilityKeyword == AbilityKeyword.BasicAttack && !KeywordList.Contains(AbilityKeyword.BasicAttack);
                KeywordList.Add(ParsedAbilityKeyword);

                if (HasBasicAttack)
                    PgJsonObjects.Skill.UpdateBasicAttackTable(RawSkill, this);
            }
        }

        private void ParseSkill(string value, ParseErrorInfo errorInfo)
        {
            if (StringToEnumConversion<PowerSkill>.TryParse(value, out PowerSkill ParsedPowerSkill, errorInfo))
            {
                RawSkill = ParsedPowerSkill;

                PgJsonObjects.Skill.UpdateAnySkillIcon(RawSkill, RawIconId.HasValue && RawIconId.Value > 0 ? RawIconId : null);
                if (KeywordList.Contains(AbilityKeyword.BasicAttack))
                    PgJsonObjects.Skill.UpdateBasicAttackTable(RawSkill, this);
            }
        }

        private void ParseSpecialInfo(string value, ParseErrorInfo errorInfo)
        {
            SpecialInfo = value;
            ParseCompleteSpecialInfo(value, errorInfo);
        }

        public void ParseCompleteSpecialInfo(string s, ParseErrorInfo ErrorInfo)
        {
            if (s == null)
                return;

            s = s.Replace("vs.", "vs");

            int Index = 0;

            do
            {
                Index = s.IndexOf(". ", Index + 1);
            }
            while ((Index >= 0 && s.Length >= Index + 3 && s.Substring(Index, 3) == ". (") || (Index >= 6 && s.Substring(Index - 6, 7) == "approx."));

            if (Index >= 0)
            {
                string s1 = s.Substring(0, Index);
                string s2 = s.Substring(Index + 2).Trim();

                ParseCompleteSpecialInfo(s1, ErrorInfo);
                ParseCompleteSpecialInfo(s2, ErrorInfo);
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

            else if (s == "Hits all targets within range. (But always consumes exactly 5 arrows.)")
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

            else if (Tools.Scan(s, "You mitigate %d damage from %{DamageType}, %{DamageType}, and %{DamageType} attacks. (10 seconds)", args))
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

            else if (Tools.Scan(s, "You gain %f sprint speed and enhanced jump control for %d seconds", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.SprintSpeed, TimeSpan.FromSeconds((int)args[1]));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)(double)args[0] });
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

            else if (Tools.Scan(s, "Pet switches to Defend mode, and is directed to attack the foes that are focused on you. (How well the pet focuses on your enemies depends in part on the pet's Bond Level.)", args))
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

            else if (Tools.Scan(s, "For %d seconds, you gain %f sprint speed and all attacks taunt as if they did %d%% more damage", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.SprintSpeed, TimeSpan.FromSeconds((int)args[0]));
                AdditionalResult.Target = AbilityEffectTarget.Self;
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)(double)args[1] });
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

            else if (Tools.Scan(s, "For %d seconds, target's attacks have a %d%% chance to miss. (This effect does not stack with other druids' castings.)", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.AccuracyBoost, TimeSpan.FromSeconds((int)args[0]));
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = (int)args[1], IsPercent = true });
                AddResult(AdditionalResult);
            }

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

            else if (Tools.Scan(s, "Target's Evasion is reduced by %d for %d seconds. (This effect stacks with itself.)", args))
            {
                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.EvasionVsAll, TimeSpan.FromSeconds((int)args[1]));
                AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = -((int)args[0]) });
                AddResult(AdditionalResult);
            }

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
                IList<IPgSpecialValue> SpecialValueList = PvE.SpecialValueList;

                AdditionalResult = new AbilityAdditionalResult(AbilityEffect.Heal, TimeSpan.Zero);
                if (PvE != null && SpecialValueList.Count > 0)
                    AdditionalResult.Parameters.Add(new AbilityEffectParameters() { Value = SpecialValueList[0].Value });
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

            else if (Tools.Scan(s, "Note: the electricity damage is dispersed harmlessly if if target is in water when the latent charge ends", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "Creatures with Fish anatomy take double damage", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "You must have hands to use this ability", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "This is considered a (non-bleed-inducing) variant of Gut for purposes of equipment and combos", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "Target's movement speed is reduced %d%", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "Creatures with Plant anatomy take double damage", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "This is considered a variant of Front Kick for purposes of equipment and combos", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "Consumes one Evil Pumpkin", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "Each time target attacks and damages you within %d seconds, you regain %d Health", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "Summon Pet from first Stable Slot with a Living Pet", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "Arthropods are knocked backwards", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "If you use Shadow Feint again within the next %d seconds (and are within %d meters of your Shadow Feint position) you blink back to this position", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "For %d seconds, target is prone to random self-stuns", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "Deals %d% Trauma damage if target's armor is below 33%", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "For 60 minutes, target gains +%d Trauma Mitigation, +%d Psychic Mitigation, and +%d Poison Mitigation", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "Pet gains +%d speed and +%d damage for 10 seconds, and pet attempts to use its pet-specific Special Attack", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "For the next %d seconds, any elemental damage will be negated (Fire, Cold, or Electricity)", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "For %d seconds you become immune to knockbacks and stuns", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "Gain %d% resistance to slashing, crushing, and piercing, %d% resistance to cold, and %d% resistance to electricity and trauma", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "For 30 seconds, melee attacks that damage you deal %d poison damage to the attacker", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "%d% Chance to consume 1 Carrot", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "Target takes +%d% Electricity damage from future attacks for %d seconds", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "Enemies is knocked forward", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "Animals take -%d% damage", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "Summoned trap exists for %d seconds or until triggered. (Base damage approx. %d)", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "You can fly for %d seconds", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "Each allies' next Melee attack deals +%d damage", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "Target is knocked forward", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "Target's Evasion is reduced by %d% (all Evasion types)", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "Allies recover %d Power every %d seconds while in area", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "For 10 minutes, base Power cost of flight (out of combat) is -%d/sec and Fly-Sprint cost is -%d/sec", args))
            {
                //TODO
            }

            else if (Tools.Scan(s, "For 10 minutes, base Power cost of flight (out of combat) is -%d/sec", args))
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
                AddWithFieldSeparator(ref Result, SpecialCasterRequirementsErrorMessage);
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
                if (Skill != null)
                    AddWithFieldSeparator(ref Result, Skill.Name);
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
                AddWithFieldSeparator(ref Result, RawConsumedItemDescription);
                if (RawDelayLoopIsOnlyUsedInCombat.HasValue)
                    AddWithFieldSeparator(ref Result, "Delay Loop Is Only Used In Combat");

                return Result;
            }
        }

        private List<string> RawAttributesThatModAmmoConsumeChanceList = new List<string>();
        private List<string> RawAttributesThatDeltaDelayLoopTimeList = new List<string>();
        private List<string> RawAttributesThatDeltaPowerCostList = new List<string>();
        private List<string> RawAttributesThatDeltaResetTimeList = new List<string>();
        private List<string> RawAttributesThatModPowerCostList = new List<string>();
        private string RawPrerequisite;
        private bool IsRawPrerequisiteParsed;
        private string RawSharesResetTimerWith;
        private bool IsRawSharesResetTimerWithParsed;
        private string RawUpgradeOf;
        private bool IsRawUpgradeOfParsed;
        private string RawAbilityGroup;
        private bool IsRawAbilityGroupParsed;
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, IBackLinkable Parent, Dictionary<Type, Dictionary<string, IJsonKey>> AllTables)
        {
            bool IsConnected = false;
            Dictionary<string, IJsonKey> AttributeTable = AllTables[typeof(Attribute)];
            Dictionary<string, IJsonKey> ItemTable = AllTables[typeof(Item)];
            Dictionary<string, IJsonKey> AbilityTable = AllTables[typeof(Ability)];
            Dictionary<string, IJsonKey> SkillTable = AllTables[typeof(Skill)];

            Prerequisite = Ability.ConnectSingleProperty(ErrorInfo, AbilityTable, RawPrerequisite, Prerequisite, ref IsRawPrerequisiteParsed, ref IsConnected, this);
            AbilityGroup = Ability.ConnectSingleProperty(ErrorInfo, AbilityTable, RawAbilityGroup, AbilityGroup, ref IsRawAbilityGroupParsed, ref IsConnected, this);

            if (PvE != null)
                IsConnected |= (PvE as AbilityPvX).Connect(ErrorInfo, this, AllTables);

            if (PvP != null)
                IsConnected |= (PvP as AbilityPvX).Connect(ErrorInfo, this, AllTables);

            foreach (AbilityRequirement Item in SpecialCasterRequirementList)
                IsConnected |= Item.Connect(ErrorInfo, this, AllTables);

            SharesResetTimerWith = Ability.ConnectSingleProperty(ErrorInfo, AbilityTable, RawSharesResetTimerWith, SharesResetTimerWith, ref IsRawSharesResetTimerWithParsed, ref IsConnected, this);
            UpgradeOf = Ability.ConnectSingleProperty(ErrorInfo, AbilityTable, RawUpgradeOf, UpgradeOf, ref IsRawUpgradeOfParsed, ref IsConnected, this);

            if (RawSkill != PowerSkill.Internal_None && RawSkill != PowerSkill.AnySkill && RawSkill != PowerSkill.Unknown)
                Skill = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, RawSkill, Skill, ref IsSkillParsed, ref IsConnected, this);

            foreach (RecipeCost Item in CostList)
                Item.Connect(ErrorInfo, this, AllTables);

            if (ConsumedItemKeyword != ItemKeyword.Internal_None)
            {
                string ConsumedItemName = StringToEnumConversion<ItemKeyword>.ToString(ConsumedItemKeyword, TextMaps.ItemKeywordStringMap, ItemKeyword.Internal_None);
                ConsumedItemLink = Item.ConnectSingleProperty(null, ItemTable, ConsumedItemName, ConsumedItemLink, ref IsRawConsumedItemKeywordParsed, ref IsConnected, this);
            }

            AttributesThatModAmmoConsumeChanceList = ConnectAttributes(ErrorInfo, AttributeTable, RawAttributesThatModAmmoConsumeChanceList, AttributesThatModAmmoConsumeChanceList, ref IsConnected);
            AttributesThatDeltaDelayLoopTimeList = ConnectAttributes(ErrorInfo, AttributeTable, RawAttributesThatDeltaDelayLoopTimeList, AttributesThatDeltaDelayLoopTimeList, ref IsConnected);
            AttributesThatDeltaPowerCostList = ConnectAttributes(ErrorInfo, AttributeTable, RawAttributesThatDeltaPowerCostList, AttributesThatDeltaPowerCostList, ref IsConnected);
            AttributesThatDeltaResetTimeList = ConnectAttributes(ErrorInfo, AttributeTable, RawAttributesThatDeltaResetTimeList, AttributesThatDeltaResetTimeList, ref IsConnected);
            AttributesThatModPowerCostList = ConnectAttributes(ErrorInfo, AttributeTable, RawAttributesThatModPowerCostList, AttributesThatModPowerCostList, ref IsConnected);

            ConsumedItemDescription = Item.ConnectSingleProperty(null, ItemTable, RawConsumedItemDescription, ConsumedItemDescription, ref IsRawConsumedItemDescriptionParsed, ref IsConnected, this);

            return IsConnected;
        }

        private IPgAttributeCollection ConnectAttributes(ParseErrorInfo ErrorInfo, Dictionary<string, IJsonKey> AttributeTable, List<string> RawAttributes, IPgAttributeCollection Attributes, ref bool IsConnected)
        {
            if (Attributes == null)
            {
                Attributes = new AttributeCollection();
                foreach (string RawAttribute in RawAttributes)
                {
                    IPgAttribute ConnectedAttribute = null;
                    bool IsAttributeParsed = false;
                    ConnectedAttribute = Attribute.ConnectSingleProperty(ErrorInfo, AttributeTable, RawAttribute, ConnectedAttribute, ref IsAttributeParsed, ref IsConnected);
                    if (ConnectedAttribute != null)
                        Attributes.Add(ConnectedAttribute);
                }
            }

            return Attributes;
        }

        public static IPgAbility ConnectSingleProperty(ParseErrorInfo ErrorInfo, Dictionary<string, IJsonKey> AbilityTable, string RawAbilityName, IPgAbility ParsedAbility, ref bool IsRawAbilityParsed, ref bool IsConnected, IBackLinkable LinkBack)
        {
            if (IsRawAbilityParsed)
                return ParsedAbility;

            IsRawAbilityParsed = true;

            if (RawAbilityName == null)
                return null;

            foreach (KeyValuePair<string, IJsonKey> Entry in AbilityTable)
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

        public static IPgAbilityCollection ConnectByKeyword(ParseErrorInfo ErrorInfo, Dictionary<string, IJsonKey> AbilityTable, AbilityKeyword Keyword, IPgAbilityCollection AbilityList, ref bool IsRawAbilityParsed, ref bool IsConnected, IBackLinkable LinkBack)
        {
            if (IsRawAbilityParsed)
                return AbilityList;

            IsRawAbilityParsed = true;

            if (Keyword == AbilityKeyword.Internal_None)
                return AbilityList;

            AbilityList = new AbilityCollection();
            IsConnected = true;

            foreach (KeyValuePair<string, IJsonKey> AbilityEntry in AbilityTable)
            {
                Ability AbilityValue = AbilityEntry.Value as Ability;
                if (AbilityValue.KeywordList.Contains(Keyword))
                {
                    AbilityValue.AddLinkBack(LinkBack);
                    AbilityList.Add(AbilityValue);
                }
            }

            if ((AbilityList as IList<IPgAbility>).Count == 0 && ErrorInfo != null)
                ErrorInfo.AddMissingKey(Keyword.ToString());

            return AbilityList;
        }

        public static IPgAbility ConnectByKey(ParseErrorInfo ErrorInfo, Dictionary<string, IJsonKey> AbilityTable, int AbilityId, IPgAbility Ability, ref bool IsRawAbilityParsed, ref bool IsConnected, IBackLinkable LinkBack)
        {
            if (IsRawAbilityParsed)
                return Ability;

            IsRawAbilityParsed = true;
            string RawAbilityId = "ability_" + AbilityId;

            foreach (KeyValuePair<string, IJsonKey> Entry in AbilityTable)
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

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BitOffset = 0;
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();
            Dictionary<int, IList> StoredEnumListTable = new Dictionary<int, IList>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();
            Dictionary<int, IPgCollection> StoredObjectListTable = new Dictionary<int, IPgCollection>();

            AddString(Key, data, ref offset, BaseOffset, 0, StoredStringtable);
            AddEnum(Animation, data, ref offset, BaseOffset, 4);
            AddBool(RawCanBeOnSidebar, data, ref offset, ref BitOffset, BaseOffset, 6, 0);
            AddBool(RawCanSuppressMonsterShout, data, ref offset, ref BitOffset, BaseOffset, 6, 2);
            AddBool(RawCanTargetUntargetableEnemies, data, ref offset, ref BitOffset, BaseOffset, 6, 4);
            AddBool(RawDelayLoopIsAbortedIfAttacked, data, ref offset, ref BitOffset, BaseOffset, 6, 6);
            AddBool(RawInternalAbility, data, ref offset, ref BitOffset, BaseOffset, 6, 8);
            AddBool(RawIsHarmless, data, ref offset, ref BitOffset, BaseOffset, 6, 10);
            AddBool(RawWorksInCombat, data, ref offset, ref BitOffset, BaseOffset, 6, 12);
            AddBool(RawWorksUnderwater, data, ref offset, ref BitOffset, BaseOffset, 6, 14);
            AddEnumList(CausesOfDeathList, data, ref offset, BaseOffset, 8, StoredEnumListTable);
            AddObjectList(CostList, data, ref offset, BaseOffset, 12, StoredObjectListTable);
            AddInt(RawCombatRefreshBaseAmount, data, ref offset, BaseOffset, 16);
            AddEnum(CompatibleSkill, data, ref offset, BaseOffset, 20);
            AddEnum(DamageType, data, ref offset, BaseOffset, 22);
            AddString(SpecialCasterRequirementsErrorMessage, data, ref offset, BaseOffset, 24, StoredStringtable);
            AddDouble(RawConsumedItemChance, data, ref offset, BaseOffset, 28);
            AddDouble(RawConsumedItemChanceToStickInCorpse, data, ref offset, BaseOffset, 32);
            AddInt(RawConsumedItemCount , data, ref offset, BaseOffset, 36);
            AddString(DelayLoopMessage, data, ref offset, BaseOffset, 40, StoredStringtable);
            AddDouble(RawDelayLoopTime, data, ref offset, BaseOffset, 44);
            AddString(Description, data, ref offset, BaseOffset, 48, StoredStringtable);
            AddEnum(EffectKeywordsIndicatingEnabled, data, ref offset, BaseOffset, 52);
            AddEnum(PetTypeTagReq, data, ref offset, BaseOffset, 54);
            AddInt(RawIconId, data, ref offset, BaseOffset, 56);
            AddString(InternalName, data, ref offset, BaseOffset, 60, StoredStringtable);
            AddString(ItemKeywordReqErrorMessage, data, ref offset, BaseOffset, 64, StoredStringtable);
            AddEnumList(ItemKeywordReqList, data, ref offset, BaseOffset, 68, StoredEnumListTable);
            AddEnumList(KeywordList, data, ref offset, BaseOffset, 72, StoredEnumListTable);
            AddInt(RawLevel, data, ref offset, BaseOffset, 76);
            AddString(Name, data, ref offset, BaseOffset, 80, StoredStringtable);
            AddInt(RawPetTypeTagReqMax, data, ref offset, BaseOffset, 84);
            AddObject(Prerequisite as ISerializableJsonObject, data, ref offset, BaseOffset, 88, StoredObjectTable);
            AddEnum(Projectile, data, ref offset, BaseOffset, 92);
            AddEnum(Target, data, ref offset, BaseOffset, 94);
            AddObject(PvE as ISerializableJsonObject, data, ref offset, BaseOffset, 96, StoredObjectTable);
            AddObject(PvP as ISerializableJsonObject, data, ref offset, BaseOffset, 100, StoredObjectTable);
            AddDouble(RawResetTime, data, ref offset, BaseOffset, 104);
            AddString(SelfParticle, data, ref offset, BaseOffset, 108, StoredStringtable);
            AddObject(SharesResetTimerWith as ISerializableJsonObject, data, ref offset, BaseOffset, 112, StoredObjectTable);
            AddObject(Skill as ISerializableJsonObject, data, ref offset, BaseOffset, 116, StoredObjectTable);
            
            //AddObjectList(CombinedRequirementList, data, ref offset, BaseOffset, 120, StoredObjectListTable);
            offset += 4;

            AddString(SpecialInfo, data, ref offset, BaseOffset, 124, StoredStringtable);
            AddInt(RawSpecialTargetingTypeReq, data, ref offset, BaseOffset, 128);
            AddEnum(TargetEffectKeywordReq, data, ref offset, BaseOffset, 132);
            AddEnum(TargetParticle, data, ref offset, BaseOffset, 134);
            AddObject(UpgradeOf as ISerializableJsonObject, data, ref offset, BaseOffset, 136, StoredObjectTable);
            AddEnum(ExtraKeywordsForTooltips, data, ref offset, BaseOffset, 140);
            AddEnum(ConsumedItems, data, ref offset, BaseOffset, 142);
            AddObject(AbilityGroup as ISerializableJsonObject, data, ref offset, BaseOffset, 144, StoredObjectTable);
            AddObjectList(SpecialCasterRequirementList, data, ref offset, BaseOffset, 148, StoredObjectListTable);
            AddObjectList(AttributesThatModAmmoConsumeChanceList, data, ref offset, BaseOffset, 152, StoredObjectListTable);
            AddObjectList(AttributesThatDeltaDelayLoopTimeList, data, ref offset, BaseOffset, 156, StoredObjectListTable);
            AddObjectList(AttributesThatDeltaPowerCostList, data, ref offset, BaseOffset, 160, StoredObjectListTable);
            AddObjectList(AttributesThatDeltaResetTimeList, data, ref offset, BaseOffset, 164, StoredObjectListTable);
            AddObjectList(AttributesThatModPowerCostList, data, ref offset, BaseOffset, 168, StoredObjectListTable);
            AddStringList(FieldTableOrder, data, ref offset, BaseOffset, 172, StoredStringListTable);
            AddObject(ConsumedItemLink as ISerializableJsonObject, data, ref offset, BaseOffset, 176, StoredObjectTable);
            AddBool(RawWorksWhileFalling, data, ref offset, ref BitOffset, BaseOffset, 180, 0);
            AddBool(RawIgnoreEffectErrors, data, ref offset, ref BitOffset, BaseOffset, 180, 2);
            AddBool(RawAttributesThatModAmmoConsumeChanceListIsEmpty, data, ref offset, ref BitOffset, BaseOffset, 180, 4);
            AddBool(RawAttributesThatDeltaDelayLoopTimeListIsEmpty, data, ref offset, ref BitOffset, BaseOffset, 180, 6);
            AddBool(RawAttributesThatDeltaPowerCostListIsEmpty, data, ref offset, ref BitOffset, BaseOffset, 180, 8);
            AddBool(RawAttributesThatDeltaResetTimeListIsEmpty, data, ref offset, ref BitOffset, BaseOffset, 180, 10);
            AddBool(RawAttributesThatModPowerCostListIsEmpty, data, ref offset, ref BitOffset, BaseOffset, 180, 12);
            AddBool(RawDelayLoopIsOnlyUsedInCombat, data, ref offset, ref BitOffset, BaseOffset, 180, 14);
            CloseBool(ref offset, ref BitOffset);
            AddEnum(RawSkill, data, ref offset, BaseOffset, 182);
            AddObjectList(SourceList, data, ref offset, BaseOffset, 184, StoredObjectListTable);
            AddObject(ConsumedItemDescription as ISerializableJsonObject, data, ref offset, BaseOffset, 188, StoredObjectTable);

            FinishSerializing(data, ref offset, BaseOffset, 192, StoredStringtable, StoredObjectTable, null, StoredEnumListTable, null, null, StoredStringListTable, StoredObjectListTable);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
