using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbility : MainPgObject<PgAbility>, IPgAbility
    {
        public PgAbility(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 192;
            SerializableJsonObject.AlignSerializedLength(ref offset);
        }

        protected override PgAbility CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbility CreateNew(byte[] data, ref int offset)
        {
            PgAbility Result = new PgAbility(data, ref offset);
            return Result;
        }

        public override void Init()
        {
            PgAbility.GetDigitStrippedName(InternalName, out string DigitStrippedName, out int LineIndex);
            this.DigitStrippedName = DigitStrippedName;
            this.LineIndex = LineIndex;

            CombinedRequirementList = new PgAbilityRequirementCollection();

            foreach (AbilityItemKeyword Keyword in ItemKeywordReqList)
                CombinedRequirementList.Add(new AbilityRequirementInternal(Keyword));

            foreach (IPgAbilityRequirement Item in SpecialCasterRequirementList)
                CombinedRequirementList.Add(Item);

            ConsumedItem = Ability.CreateConsumedItem(ConsumedItemLink, ConsumedItems, RawConsumedItemCount, RawConsumedItemChance, RawConsumedItemChanceToStickInCorpse);

            //AddLinkBackCollection(CostList);
            AddLinkBack(Prerequisite);
            //AddLinkBack(PvE);
            //AddLinkBack(PvP);
            AddLinkBack(SharesResetTimerWith);
            AddLinkBack(Skill);
            AddLinkBack(UpgradeOf);
            AddLinkBack(AbilityGroup);
            AddLinkBackCollection(SpecialCasterRequirementList, (IPgAbilityRequirement value) => value.GetLinkBack());
            AddLinkBack(ConsumedItemLink);
            AddLinkBack(ConsumedItemDescription);
        }

        public override string Key { get { return GetString(0); } }
        public AbilityAnimation Animation { get { return GetEnum<AbilityAnimation>(4); } }
        public bool CanBeOnSidebar { get { return RawCanBeOnSidebar.HasValue && RawCanBeOnSidebar.Value; } }
        public bool? RawCanBeOnSidebar { get { return GetBool(6, 0); } }
        public bool CanSuppressMonsterShout { get { return RawCanSuppressMonsterShout.HasValue && RawCanSuppressMonsterShout.Value; } }
        public bool? RawCanSuppressMonsterShout { get { return GetBool(6, 2); } }
        public bool CanTargetUntargetableEnemies { get { return RawCanTargetUntargetableEnemies.HasValue && RawCanTargetUntargetableEnemies.Value; } }
        public bool? RawCanTargetUntargetableEnemies { get { return GetBool(6, 4); } }
        public bool DelayLoopIsAbortedIfAttacked { get { return RawDelayLoopIsAbortedIfAttacked.HasValue && RawDelayLoopIsAbortedIfAttacked.Value; } }
        public bool? RawDelayLoopIsAbortedIfAttacked { get { return GetBool(6, 6); } }
        public bool InternalAbility { get { return RawInternalAbility.HasValue && RawInternalAbility.Value; } }
        public bool? RawInternalAbility { get { return GetBool(6, 8); } }
        public bool IsHarmless { get { return RawIsHarmless.HasValue && RawIsHarmless.Value; } }
        public bool? RawIsHarmless { get { return GetBool(6, 10); } }
        public bool WorksInCombat { get { return RawWorksInCombat.HasValue && RawWorksInCombat.Value; } }
        public bool? RawWorksInCombat { get { return GetBool(6, 12); } }
        public bool WorksUnderwater { get { return RawWorksUnderwater.HasValue && RawWorksUnderwater.Value; } }
        public bool? RawWorksUnderwater { get { return GetBool(6, 14); } }
        public List<Deaths> CausesOfDeathList { get { return GetEnumList(8, ref _CausesOfDeathList); } } private List<Deaths> _CausesOfDeathList;
        public IPgRecipeCostCollection CostList { get { return GetObjectList(12, ref _CostList, PgRecipeCostCollection.CreateItem, () => new PgRecipeCostCollection()); } } private IPgRecipeCostCollection _CostList;
        public int CombatRefreshBaseAmount { get { return RawCombatRefreshBaseAmount.HasValue ? RawCombatRefreshBaseAmount.Value : 0; } }
        public int? RawCombatRefreshBaseAmount { get { return GetInt(16); } }
        public PowerSkill CompatibleSkill { get { return GetEnum<PowerSkill>(20); } }
        public DamageType DamageType { get { return GetEnum<DamageType>(22); } }
        public string SpecialCasterRequirementsErrorMessage { get { return GetString(24); } }
        public double ConsumedItemChance { get { return RawConsumedItemChance.HasValue ? RawConsumedItemChance.Value : 0; } }
        public double? RawConsumedItemChance { get { return GetDouble(28); } }
        public double ConsumedItemChanceToStickInCorpse { get { return RawConsumedItemChanceToStickInCorpse.HasValue ? RawConsumedItemChanceToStickInCorpse.Value : 0; } }
        public double? RawConsumedItemChanceToStickInCorpse { get { return GetDouble(32); } }
        public int ConsumedItemCount { get { return RawConsumedItemCount.HasValue ? RawConsumedItemCount.Value : 0; } }
        public int? RawConsumedItemCount { get { return GetInt(36); } }
        public string DelayLoopMessage { get { return GetString(40); } }
        public double DelayLoopTime { get { return RawDelayLoopTime.HasValue ? RawDelayLoopTime.Value : 0; } }
        public double? RawDelayLoopTime { get { return GetDouble(44); } }
        public string Description { get { return GetString(48); } }
        public AbilityIndicatingEnabled EffectKeywordsIndicatingEnabled { get { return GetEnum<AbilityIndicatingEnabled>(52); } }
        public AbilityPetType PetTypeTagReq { get { return GetEnum<AbilityPetType>(54); } }
        public int IconId { get { return RawIconId.HasValue ? RawIconId.Value : 0; } }
        public int? RawIconId { get { return GetInt(56); } }
        public string InternalName { get { return GetString(60); } }
        public string ItemKeywordReqErrorMessage { get { return GetString(64); } }
        public List<AbilityItemKeyword> ItemKeywordReqList { get { return GetEnumList(68, ref _ItemKeywordReqList); } } private List<AbilityItemKeyword> _ItemKeywordReqList;
        public List<AbilityKeyword> KeywordList { get { return GetEnumList(72, ref _KeywordList); } } private List<AbilityKeyword> _KeywordList;
        public int Level { get { return RawLevel.HasValue ? RawLevel.Value : 0; } }
        public int? RawLevel { get { return GetInt(76); } }
        public string Name { get { return GetString(80); } }
        public int PetTypeTagReqMax { get { return RawPetTypeTagReqMax.HasValue ? RawPetTypeTagReqMax.Value : 0; } }
        public int? RawPetTypeTagReqMax { get { return GetInt(84); } }
        public IPgAbility Prerequisite { get { return GetObject(88, ref _Prerequisite, PgAbility.CreateNew); } } private IPgAbility _Prerequisite;
        public AbilityProjectile Projectile { get { return GetEnum<AbilityProjectile>(92); } }
        public AbilityTarget Target { get { return GetEnum<AbilityTarget>(94); } }
        public IPgAbilityPvX PvE { get { return GetObject(96, ref _PvE, PgAbilityPvX.CreateNew); } } private IPgAbilityPvX _PvE;
        public IPgAbilityPvX PvP { get { return GetObject(100, ref _PvP, PgAbilityPvX.CreateNew); } } private IPgAbilityPvX _PvP;
        public double ResetTime { get { return RawResetTime.HasValue ? RawResetTime.Value : 0; } }
        public double? RawResetTime { get { return GetDouble(104); } }
        public string SelfParticle { get { return GetString(108); } }
        public IPgAbility SharesResetTimerWith { get { return GetObject(112, ref _SharesResetTimerWith, CreateNew); } } private IPgAbility _SharesResetTimerWith;
        public IPgSkill Skill { get { return GetObject(116, ref _Skill, PgSkill.CreateNew); } } private IPgSkill _Skill;
        public string SpecialInfo { get { return GetString(124); } }
        public int SpecialTargetingTypeReq { get { return RawSpecialTargetingTypeReq.HasValue ? RawSpecialTargetingTypeReq.Value : 0; } }
        public int? RawSpecialTargetingTypeReq { get { return GetInt(128); } }
        public TargetEffectKeyword TargetEffectKeywordReq { get { return GetEnum<TargetEffectKeyword>(132); } }
        public AbilityTargetParticle TargetParticle { get { return GetEnum<AbilityTargetParticle>(134); } }
        public IPgAbility UpgradeOf { get { return GetObject(136, ref _UpgradeOf, CreateNew); } } private IPgAbility _UpgradeOf;
        public TooltipsExtraKeywords ExtraKeywordsForTooltips { get { return GetEnum<TooltipsExtraKeywords>(140); } }
        public ConsumedItems ConsumedItems { get { return GetEnum<ConsumedItems>(142); } }
        public IPgAbility AbilityGroup { get { return GetObject(144, ref _AbilityGroup, CreateNew); } } private IPgAbility _AbilityGroup;
        public IPgAbilityRequirementCollection SpecialCasterRequirementList { get { return GetObjectList(148, ref _SpecialCasterRequirementList, PgAbilityRequirementCollection.CreateItem, () => new PgAbilityRequirementCollection()); } } private IPgAbilityRequirementCollection _SpecialCasterRequirementList;
        public IPgAttributeCollection AttributesThatModAmmoConsumeChanceList { get { return GetObjectList(152, ref _AttributesThatModAmmoConsumeChanceList, PgAttributeCollection.CreateItem, () => new PgAttributeCollection()); } } private IPgAttributeCollection _AttributesThatModAmmoConsumeChanceList;
        public IPgAttributeCollection AttributesThatDeltaDelayLoopTimeList { get { return GetObjectList(156, ref _AttributesThatDeltaDelayLoopTimeList, PgAttributeCollection.CreateItem, () => new PgAttributeCollection()); } } private IPgAttributeCollection _AttributesThatDeltaDelayLoopTimeList;
        public IPgAttributeCollection AttributesThatDeltaPowerCostList { get { return GetObjectList(160, ref _AttributesThatDeltaPowerCostList, PgAttributeCollection.CreateItem, () => new PgAttributeCollection()); } } private IPgAttributeCollection _AttributesThatDeltaPowerCostList;
        public IPgAttributeCollection AttributesThatDeltaResetTimeList { get { return GetObjectList(164, ref _AttributesThatDeltaResetTimeList, PgAttributeCollection.CreateItem, () => new PgAttributeCollection()); } } private IPgAttributeCollection _AttributesThatDeltaResetTimeList;
        public IPgAttributeCollection AttributesThatModPowerCostList { get { return GetObjectList(168, ref _AttributesThatModPowerCostList, PgAttributeCollection.CreateItem, () => new PgAttributeCollection()); } } private IPgAttributeCollection _AttributesThatModPowerCostList;
        protected override List<string> FieldTableOrder { get { return GetStringList(172, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public IPgItem ConsumedItemLink { get { return GetObject(176, ref _ConsumedItemLink, PgItem.CreateNew); } } private IPgItem _ConsumedItemLink;
        public bool WorksWhileFalling { get { return RawWorksWhileFalling.HasValue && RawWorksWhileFalling.Value; } }
        public bool? RawWorksWhileFalling { get { return GetBool(180, 0); } }
        public bool IgnoreEffectErrors { get { return RawIgnoreEffectErrors.HasValue && RawIgnoreEffectErrors.Value; } }
        public bool? RawIgnoreEffectErrors { get { return GetBool(180, 2); } }
        public bool RawAttributesThatModAmmoConsumeChanceListIsEmpty { get { return GetBool(180, 4).Value; } }
        public bool RawAttributesThatDeltaDelayLoopTimeListIsEmpty { get { return GetBool(180, 6).Value; } }
        public bool RawAttributesThatDeltaPowerCostListIsEmpty { get { return GetBool(180, 8).Value; } }
        public bool RawAttributesThatDeltaResetTimeListIsEmpty { get { return GetBool(180, 10).Value; } }
        public bool RawAttributesThatModPowerCostListIsEmpty { get { return GetBool(180, 12).Value; } }
        public PowerSkill RawSkill { get { return GetEnum<PowerSkill>(182); } }
        public IPgGenericSourceCollection SourceList { get { return GetObjectList(184, ref _SourceList, (byte[] data, ref int offset) => PgGenericSourceCollection.CreateItem(this, data, ref offset), () => new PgGenericSourceCollection()); } } private PgGenericSourceCollection _SourceList;
        public IPgItem ConsumedItemDescription { get { return GetObject(188, ref _ConsumedItemDescription, PgItem.CreateNew); } } private IPgItem _ConsumedItemDescription;

        public ConsumedItem ConsumedItem { get; private set; }
        public IPgAbilityRequirementCollection CombinedRequirementList { get; private set; }

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "AbilityGroup", new FieldParser() {
                Type = FieldType.String,
                GetString = () => AbilityGroup != null ? AbilityGroup.InternalName : null } },
            { "Animation", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<AbilityAnimation>.ToString(Animation, null, AbilityAnimation.Internal_None) } },
            { "AttributesThatModAmmoConsumeChance", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = () => AttributesThatModAmmoConsumeChanceList.ToKeyList,
                GetArrayIsEmpty = () => RawAttributesThatModAmmoConsumeChanceListIsEmpty } },
            { "AttributesThatDeltaDelayLoopTime", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = () => AttributesThatDeltaDelayLoopTimeList.ToKeyList,
                GetArrayIsEmpty = () => RawAttributesThatDeltaDelayLoopTimeListIsEmpty } },
            { "AttributesThatDeltaPowerCost", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = () => AttributesThatDeltaPowerCostList.ToKeyList,
                GetArrayIsEmpty = () => RawAttributesThatDeltaPowerCostListIsEmpty } },
            { "AttributesThatDeltaResetTime", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = () => AttributesThatDeltaResetTimeList.ToKeyList,
                GetArrayIsEmpty = () => RawAttributesThatDeltaResetTimeListIsEmpty } },
            { "AttributesThatModPowerCost", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = () => AttributesThatModPowerCostList.ToKeyList,
                GetArrayIsEmpty = () => RawAttributesThatModPowerCostListIsEmpty } },
            { "CanBeOnSidebar", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawCanBeOnSidebar } },
            { "CanSuppressMonsterShout", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawCanSuppressMonsterShout } },
            { "CanTargetUntargetableEnemies", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawCanTargetUntargetableEnemies } },
            { "CausesOfDeath", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = () => StringToEnumConversion<Deaths>.ToStringList(CausesOfDeathList) } },
            { "Costs", new FieldParser() {
                Type = FieldType.ObjectArray,
                GetObjectArray = () => CostList } },
            { "CombatRefreshBaseAmount", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawCombatRefreshBaseAmount } },
            { "CompatibleSkills", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = () => StringToEnumConversion<PowerSkill>.ToSingleOrEmptyStringList(CompatibleSkill) } },
            { "ConsumedItemChance", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawConsumedItemChance } },
            { "ConsumedItemChanceToStickInCorpse", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawConsumedItemChanceToStickInCorpse } },
            { "ConsumedItemCount", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawConsumedItemCount } },
            { "ConsumedItemKeyword", new FieldParser() {
                Type = FieldType.String,
                GetString = GetConsumedItemKeyword } },
            { "DamageType", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<DamageType>.ToString(DamageType, null, DamageType.Internal_None, DamageType.Internal_Empty) } },
            { "DelayLoopIsAbortedIfAttacked", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawDelayLoopIsAbortedIfAttacked } },
            { "DelayLoopMessage", new FieldParser() {
                Type = FieldType.String,
                GetString = () => DelayLoopMessage } },
            { "DelayLoopTime", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawDelayLoopTime } },
            { "Description", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Description } },
            { "EffectKeywordsIndicatingEnabled", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = () => StringToEnumConversion<AbilityIndicatingEnabled>.ToSingleOrEmptyStringList(EffectKeywordsIndicatingEnabled) } },
            { "ExtraKeywordsForTooltips", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = () => StringToEnumConversion<TooltipsExtraKeywords>.ToSingleOrEmptyStringList(ExtraKeywordsForTooltips) } },
            { "IconID", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawIconId } },
            { "IgnoreEffectErrors", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawIgnoreEffectErrors } },
            { "InternalAbility", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawInternalAbility } },
            { "InternalName", new FieldParser() {
                Type = FieldType.String,
                GetString = () => InternalName } },
            { "IsHarmless", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawIsHarmless } },
            { "ItemKeywordReqErrorMessage", new FieldParser() {
                Type = FieldType.String,
                GetString = () => ItemKeywordReqErrorMessage } },
            { "ItemKeywordReqs", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = () => StringToEnumConversion<AbilityItemKeyword>.ToStringList(ItemKeywordReqList, TextMaps.AbilityItemKeywordStringMap) } },
            { "Keywords", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = () => StringToEnumConversion<AbilityKeyword>.ToStringList(KeywordList) } },
            { "Level", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawLevel } },
            { "Name", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Name } },
            { "PetTypeTagReq", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<AbilityPetType>.ToString(PetTypeTagReq, null, AbilityPetType.Internal_None) } },
            { "PetTypeTagReqMax", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawPetTypeTagReqMax } },
            { "Prerequisite", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Prerequisite != null ? Prerequisite.InternalName : null } },
            { "Projectile", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<AbilityProjectile>.ToString(Projectile, null, AbilityProjectile.Internal_None) } },
            { "PvE", new FieldParser() {
                Type = FieldType.Object,
                GetObject = () => PvE as IObjectContentGenerator} },
            { "PvP", new FieldParser() {
                Type = FieldType.Object,
                GetObject = () => PvP as IObjectContentGenerator } },
            { "ResetTime", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawResetTime } },
            { "SelfParticle", new FieldParser() {
                Type = FieldType.String,
                GetString = () => SelfParticle } },
            { "SharesResetTimerWith", new FieldParser() {
                Type = FieldType.String,
                GetString = () => SharesResetTimerWith != null ? SharesResetTimerWith.InternalName : null } },
            { "Skill", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<PowerSkill>.ToString(RawSkill, null, PowerSkill.Internal_None) } },
            { "SpecialCasterRequirements", new FieldParser() {
                Type = FieldType.ObjectArray,
                GetObjectArray = () => SpecialCasterRequirementList,
                SimplifyArray = true } },
            { "SpecialCasterRequirementsErrorMessage", new FieldParser() {
                Type = FieldType.String,
                GetString = () => SpecialCasterRequirementsErrorMessage } },
            { "SpecialInfo", new FieldParser() {
                Type = FieldType.String,
                GetString = () => SpecialInfo } },
            { "SpecialTargetingTypeReq", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawSpecialTargetingTypeReq } },
            { "Target", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<AbilityTarget>.ToString(Target, null, AbilityTarget.Internal_None) } },
            { "TargetEffectKeywordReq", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<TargetEffectKeyword>.ToString(TargetEffectKeywordReq, null, TargetEffectKeyword.Internal_None) } },
            { "TargetParticle", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<AbilityTargetParticle>.ToString(TargetParticle, null, AbilityTargetParticle.Internal_None) } },
            { "UpgradeOf", new FieldParser() {
                Type = FieldType.String,
                GetString = () => UpgradeOf != null ? UpgradeOf.InternalName: null } },
            { "WorksInCombat", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawWorksInCombat } },
            { "WorksUnderwater", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawWorksUnderwater } },
            { "WorksWhileFalling", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawWorksWhileFalling } },
            { "ConsumedItemDescription", new FieldParser() {
                Type = FieldType.String,
                GetString = () => ConsumedItemDescription != null ? ConsumedItemDescription.Name : null } },
        }; } }

        private string GetConsumedItemKeyword()
        {
            if (ConsumedItemLink != null)
                return ConsumedItemLink.InternalName;
            else if (ConsumedItems != ConsumedItems.Internal_None)
                return StringToEnumConversion<ConsumedItems>.ToString(ConsumedItems);
            else
                return null;
        }
        #endregion

        public override string SortingName { get { return Name; } }
        public string SearchResultIconFileName { get { return RawIconId.HasValue && RawIconId.Value > 0 ? "icon_" + RawIconId.Value : null; } }
        public string DigitStrippedName { get; private set; }
        public int LineIndex { get; private set; }

        public static void GetDigitStrippedName(string internalName, out string digitStrippedName, out int lineIndex)
        {
            digitStrippedName = internalName;
            string LineIndexString = "";

            while (digitStrippedName.Length > 0 && Char.IsDigit(digitStrippedName[digitStrippedName.Length - 1]))
            {
                LineIndexString = digitStrippedName.Substring(digitStrippedName.Length - 1) + LineIndexString;
                digitStrippedName = digitStrippedName.Substring(0, digitStrippedName.Length - 1);
            }

            if (!int.TryParse(LineIndexString, out lineIndex))
                lineIndex = -1;
        }
    }
}
