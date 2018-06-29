using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbility: MainPgObject<PgAbility>, IPgAbility
    {
        public PgAbility(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 150;
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
            ConsumedItem = Ability.CreateConsumedItem(ConsumedItemLink, ConsumedItems, RawConsumedItemCount, RawConsumedItemChance, RawConsumedItemChanceToStickInCorpse);
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
        public RecipeCostCollection CostList { get { return GetObjectList(12, ref _CostList, RecipeCostCollection.CreateItem, () => new RecipeCostCollection()); } } private RecipeCostCollection _CostList;
        public int CombatRefreshBaseAmount { get { return RawCombatRefreshBaseAmount.HasValue ? RawCombatRefreshBaseAmount.Value : 0; } }
        public int? RawCombatRefreshBaseAmount { get { return GetInt(16); } }
        public PowerSkill CompatibleSkill { get { return GetEnum<PowerSkill>(20); } }
        public DamageType DamageType { get { return GetEnum<DamageType>(22); } }
        public string RawSpecialCasterRequirementsErrorMessage { get { return GetString(24); } }
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
        public IPgAbility SharesResetTimerWith { get { return GetObject(1112, ref _SharesResetTimerWith, CreateNew); } } private IPgAbility _SharesResetTimerWith;
        public IPgSkill Skill { get { return GetObject(116, ref _Skill, PgSkill.CreateNew); } } private IPgSkill _Skill;
        public AbilityRequirementCollection CombinedRequirementList { get { return GetObjectList(116, ref _CombinedRequirementList, AbilityRequirementCollection.CreateItem, () => new AbilityRequirementCollection()); } } private AbilityRequirementCollection _CombinedRequirementList;
        public string SpecialInfo { get { return GetString(124); } }
        public int SpecialTargetingTypeReq { get { return RawSpecialTargetingTypeReq.HasValue ? RawSpecialTargetingTypeReq.Value : 0; } }
        public int? RawSpecialTargetingTypeReq { get { return GetInt(128); } }
        public TargetEffectKeyword TargetEffectKeywordReq { get { return GetEnum<TargetEffectKeyword>(132); } }
        public AbilityTargetParticle TargetParticle { get { return GetEnum<AbilityTargetParticle>(134); } }
        public IPgAbility UpgradeOf { get { return GetObject(136, ref _UpgradeOf, CreateNew); } } private IPgAbility _UpgradeOf;
        public TooltipsExtraKeywords ExtraKeywordsForTooltips { get { return GetEnum<TooltipsExtraKeywords>(140); } }
        public ConsumedItems ConsumedItems { get { return GetEnum<ConsumedItems>(142); } }
        public IPgAbility AbilityGroup { get { return GetObject(144, ref _AbilityGroup, CreateNew); } } private IPgAbility _AbilityGroup;
        public IPgItem ConsumedItemLink { get { return GetObject(148, ref _ConsumedItemLink, PgItem.CreateNew); } } private IPgItem _ConsumedItemLink;
        //public List<GenericSource> SourceList { get { return GetObjectList(148, ref _SourceList); } } private List<GenericSource> _SourceList;
        public bool WorksWhileFalling { get { return RawWorksWhileFalling.HasValue && RawWorksWhileFalling.Value; } }
        public bool? RawWorksWhileFalling { get { return GetBool(152, 0); } }
        public bool IgnoreEffectErrors { get { return RawIgnoreEffectErrors.HasValue && RawIgnoreEffectErrors.Value; } }
        public bool? RawIgnoreEffectErrors { get { return GetBool(152, 2); } }
        public ConsumedItem ConsumedItem { get; private set; }
    }
}
