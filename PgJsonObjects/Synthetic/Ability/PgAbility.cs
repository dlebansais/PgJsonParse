using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbility: GenericPgObject, IPgAbility
    {
        public PgAbility(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public override void Init()
        {
            ConsumedItem = Ability.CreateConsumedItem(ConsumedItemLink, ConsumedItems, RawConsumedItemCount, RawConsumedItemChance, RawConsumedItemChanceToStickInCorpse);
        }

        public AbilityAnimation Animation { get { return GetEnum<AbilityAnimation>(0); } }
        public bool CanBeOnSidebar { get { return RawCanBeOnSidebar.HasValue && RawCanBeOnSidebar.Value; } }
        public bool? RawCanBeOnSidebar { get { return GetBool(2, 0); } }
        public bool CanSuppressMonsterShout { get { return RawCanSuppressMonsterShout.HasValue && RawCanSuppressMonsterShout.Value; } }
        public bool? RawCanSuppressMonsterShout { get { return GetBool(2, 2); } }
        public bool CanTargetUntargetableEnemies { get { return RawCanTargetUntargetableEnemies.HasValue && RawCanTargetUntargetableEnemies.Value; } }
        public bool? RawCanTargetUntargetableEnemies { get { return GetBool(2, 4); } }
        public bool DelayLoopIsAbortedIfAttacked { get { return RawDelayLoopIsAbortedIfAttacked.HasValue && RawDelayLoopIsAbortedIfAttacked.Value; } }
        public bool? RawDelayLoopIsAbortedIfAttacked { get { return GetBool(2, 6); } }
        public bool InternalAbility { get { return RawInternalAbility.HasValue && RawInternalAbility.Value; } }
        public bool? RawInternalAbility { get { return GetBool(2, 8); } }
        public bool IsHarmless { get { return RawIsHarmless.HasValue && RawIsHarmless.Value; } }
        public bool? RawIsHarmless { get { return GetBool(2, 10); } }
        public bool WorksInCombat { get { return RawWorksInCombat.HasValue && RawWorksInCombat.Value; } }
        public bool? RawWorksInCombat { get { return GetBool(2, 12); } }
        public bool WorksUnderwater { get { return RawWorksUnderwater.HasValue && RawWorksUnderwater.Value; } }
        public bool? RawWorksUnderwater { get { return GetBool(2, 14); } }
        public List<Deaths> CausesOfDeathList { get { return GetEnumList(4, ref _CausesOfDeathList); } } private List<Deaths> _CausesOfDeathList;
        public List<RecipeCost> CostList { get { return GetObjectList(8, ref _CostList); } } private List<RecipeCost> _CostList;
        public int CombatRefreshBaseAmount { get { return RawCombatRefreshBaseAmount.HasValue ? RawCombatRefreshBaseAmount.Value : 0; } }
        public int? RawCombatRefreshBaseAmount { get { return GetInt(12); } }
        public PowerSkill CompatibleSkill { get { return GetEnum<PowerSkill>(16); } }
        public DamageType DamageType { get { return GetEnum<DamageType>(18); } }
        public string RawSpecialCasterRequirementsErrorMessage { get { return GetString(20); } }
        public double ConsumedItemChance { get { return RawConsumedItemChance.HasValue ? RawConsumedItemChance.Value : 0; } }
        public double? RawConsumedItemChance { get { return GetInt(24); } }
        public double ConsumedItemChanceToStickInCorpse { get { return RawConsumedItemChanceToStickInCorpse.HasValue ? RawConsumedItemChanceToStickInCorpse.Value : 0; } }
        public double? RawConsumedItemChanceToStickInCorpse { get { return GetDouble(28); } }
        public int ConsumedItemCount { get { return RawConsumedItemCount.HasValue ? RawConsumedItemCount.Value : 0; } }
        public int? RawConsumedItemCount { get { return GetInt(32); } }
        public string DelayLoopMessage { get { return GetString(36); } }
        public double DelayLoopTime { get { return RawDelayLoopTime.HasValue ? RawDelayLoopTime.Value : 0; } }
        public double? RawDelayLoopTime { get { return GetDouble(40); } }
        public string Description { get { return GetString(44); } }
        public AbilityIndicatingEnabled EffectKeywordsIndicatingEnabled { get { return GetEnum<AbilityIndicatingEnabled>(48); } }
        public AbilityPetType PetTypeTagReq { get { return GetEnum<AbilityPetType>(50); } }
        public int IconId { get { return RawIconId.HasValue ? RawIconId.Value : 0; } }
        public int? RawIconId { get { return GetInt(52); } }
        public string InternalName { get { return GetString(56); } }
        public string ItemKeywordReqErrorMessage { get { return GetString(60); } }
        public List<AbilityItemKeyword> ItemKeywordReqList { get { return GetEnumList(64, ref _ItemKeywordReqList); } } private List<AbilityItemKeyword> _ItemKeywordReqList;
        public List<AbilityKeyword> KeywordList { get { return GetEnumList(68, ref _KeywordList); } } private List<AbilityKeyword> _KeywordList;
        public int Level { get { return RawLevel.HasValue ? RawLevel.Value : 0; } }
        public int? RawLevel { get { return GetInt(72); } }
        public string Name { get { return GetString(76); } }
        public int PetTypeTagReqMax { get { return RawPetTypeTagReqMax.HasValue ? RawPetTypeTagReqMax.Value : 0; } }
        public int? RawPetTypeTagReqMax { get { return GetInt(80); } }
        public Ability Prerequisite { get { return GetObject(84, ref _Prerequisite); } } private Ability _Prerequisite;
        public AbilityProjectile Projectile { get { return GetEnum<AbilityProjectile>(88); } }
        public AbilityTarget Target { get { return GetEnum<AbilityTarget>(90); } }
        public AbilityPvX PvE { get { return GetObject(92, ref _PvE); } } private AbilityPvX _PvE;
        public AbilityPvX PvP { get { return GetObject(96, ref _PvP); } } private AbilityPvX _PvP;
        public double ResetTime { get { return RawResetTime.HasValue ? RawResetTime.Value : 0; } }
        public double? RawResetTime { get { return GetDouble(100); } }
        public string SelfParticle { get { return GetString(104); } }
        public Ability SharesResetTimerWith { get { return GetObject(108, ref _SharesResetTimerWith); } } private Ability _SharesResetTimerWith;
        public Skill Skill { get { return GetObject(112, ref _Skill); } } private Skill _Skill;
        public List<AbilityRequirement> CombinedRequirementList { get { return GetObjectList(116, ref _CombinedRequirementList); } } private List<AbilityRequirement> _CombinedRequirementList;
        public string SpecialInfo { get { return GetString(120); } }
        public int SpecialTargetingTypeReq { get { return RawSpecialTargetingTypeReq.HasValue ? RawSpecialTargetingTypeReq.Value : 0; } }
        public int? RawSpecialTargetingTypeReq { get { return GetInt(124); } }
        public TargetEffectKeyword TargetEffectKeywordReq { get { return GetEnum<TargetEffectKeyword>(128); } }
        public AbilityTargetParticle TargetParticle { get { return GetEnum<AbilityTargetParticle>(130); } }
        public Ability UpgradeOf { get { return GetObject(132, ref _UpgradeOf); } } private Ability _UpgradeOf;
        public TooltipsExtraKeywords ExtraKeywordsForTooltips { get { return GetEnum<TooltipsExtraKeywords>(136); } }
        public ConsumedItems ConsumedItems { get { return GetEnum<ConsumedItems>(138); } }
        public Ability AbilityGroup { get { return GetObject(140, ref _AbilityGroup); } } private Ability _AbilityGroup;
        public Item ConsumedItemLink { get { return GetObject(144, ref _ConsumedItemLink); } } private Item _ConsumedItemLink;
        //public List<GenericSource> SourceList { get { return GetObjectList(144, ref _SourceList); } } private List<GenericSource> _SourceList;
        public bool WorksWhileFalling { get { return RawWorksWhileFalling.HasValue && RawWorksWhileFalling.Value; } }
        public bool? RawWorksWhileFalling { get { return GetBool(148, 0); } }
        public bool IgnoreEffectErrors { get { return RawIgnoreEffectErrors.HasValue && RawIgnoreEffectErrors.Value; } }
        public bool? RawIgnoreEffectErrors { get { return GetBool(148, 2); } }
        public ConsumedItem ConsumedItem { get; private set; }
    }
}
