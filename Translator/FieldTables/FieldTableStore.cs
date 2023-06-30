namespace Translator;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using PgObjects;

public class FieldTableStore
{
    public static Dictionary<string, Type> TableAbility { get; } = new Dictionary<string, Type>()
    {
        { "AbilityGroup", typeof(string) },
        { "Animation", typeof(string) },
        { "AttributesThatModAmmoConsumeChance", typeof(string[]) },
        { "AttributesThatDeltaDelayLoopTime", typeof(string[]) },
        { "AttributesThatDeltaPowerCost", typeof(string[]) },
        { "AttributesThatDeltaResetTime", typeof(string[]) },
        { "AttributesThatDeltaWorksWhileStunned", typeof(string[]) },
        { "AttributesThatModPowerCost", typeof(string[]) },
        { "CanBeOnSidebar", typeof(bool) },
        { "CanSuppressMonsterShout", typeof(bool) },
        { "CanTargetUntargetableEnemies", typeof(bool) },
        { "CausesOfDeath", typeof(string[]) },
        { "Costs", typeof(PgRecipeCost[]) },
        { "CombatRefreshBaseAmount", typeof(int) },
        { "DamageType", typeof(string) },
        { "DelayLoopIsAbortedIfAttacked", typeof(bool) },
        { "DelayLoopMessage", typeof(string) },
        { "DelayLoopTime", typeof(float) },
        { "Description", typeof(string) },
        { "DigitStrippedName", typeof(string) },
        { "EffectKeywordsIndicatingEnabled", typeof(string[]) },
        { "ExtraKeywordsForTooltips", typeof(string[]) },
        { "IconID", typeof(int) },
        { "IgnoreEffectErrors", typeof(bool) },
        { "IsInternalAbility", typeof(bool) },
        { "InternalName", typeof(string) },
        { "IsHarmless", typeof(bool) },
        { "ItemKeywordRequirementErrorMessage", typeof(string) },
        { "ItemKeywordRequirements", typeof(string[]) },
        { "Keywords", typeof(string[]) },
        { "Level", typeof(int) },
        { "Name", typeof(string) },
        { "PetTypeTagRequirement", typeof(string) },
        { "PetTypeTagRequirementMax", typeof(int) },
        { "Prerequisite", typeof(string) },
        { "Projectile", typeof(string) },
        { "PvE", typeof(PgAbilityPvX) },
        { "ResetTime", typeof(float) },
        { "SelfParticle", typeof(PgSelfParticle) },
        { "AmmoDescription", typeof(string) },
        { "SharesResetTimerWith", typeof(string) },
        { "Skill", typeof(string) },
        { "SpecialCasterRequirements", typeof(PgAbilityRequirement[]) },
        { "SpecialCasterRequirementsErrorMessage", typeof(string) },
        { "SpecialInfo", typeof(string) },
        { "SpecialTargetingTypeRequirement", typeof(int) },
        { "Target", typeof(string) },
        { "TargetEffectKeywordRequirement", typeof(string) },
        { "TargetParticle", typeof(PgTargetParticle) },
        { "UpgradeOf", typeof(string) },
        { "WorksInCombat", typeof(bool) },
        { "WorksUnderwater", typeof(bool) },
        { "WorksWhileFalling", typeof(bool) },
        { "DelayLoopIsOnlyUsedInCombat", typeof(bool) },
        { "AmmoKeywords", typeof(PgAbilityAmmo[]) },
        { "AmmoConsumeChance", typeof(float) },
        { "AmmoStickChance", typeof(float) },
        { "TargetTypeTagRequirement", typeof(string) },
        { "WorksWhileMounted", typeof(bool) },
        { "SelfPreParticle", typeof(PgSelfPreParticle) },
        { "IsCosmeticPet", typeof(bool) },
        { "WorksWhileStunned", typeof(bool) },
        { "AbilityGroupName", typeof(string) },
        { "Rank", typeof(int) },
        { "InventoryKeywordRequirementErrorMessage", typeof(string) },
        { "InventoryKeywordRequirements", typeof(string[]) },
        { "IsAoECenteredOnCaster", typeof(bool) },
    };

    public static Dictionary<string, Type> TableSelfParticle { get; } = new Dictionary<string, Type>()
    {
        { "ParticleName", typeof(string) },
        { "PrimaryColor", typeof(string) },
        { "SecondaryColor", typeof(string) },
    };

    public static Dictionary<string, Type> TableSelfPreParticle { get; } = new Dictionary<string, Type>()
    {
        { "AoEColor", typeof(string) },
        { "AoERange", typeof(int) },
        { "ParticleName", typeof(string) },
        { "PrimaryColor", typeof(string) },
        { "SecondaryColor", typeof(string) },
    };

    public static Dictionary<string, Type> TableTargetParticle { get; } = new Dictionary<string, Type>()
    {
        { "ParticleName", typeof(string) },
        { "PrimaryColor", typeof(string) },
        { "SecondaryColor", typeof(string) },
    };

    public static Dictionary<string, Type> TableAbilityAmmo { get; } = new Dictionary<string, Type>()
    {
        { "ItemKeyword", typeof(string) },
        { "Count", typeof(int) },
    };

    public static Dictionary<string, Type> TableAbilityPvX { get; } = new Dictionary<string, Type>()
    {
        { "Damage", typeof(int) },
        { "HealthSpecificDamage", typeof(int) },
        { "ExtraDamageIfTargetVulnerable", typeof(int) },
        { "ArmorSpecificDamage", typeof(int) },
        { "Range", typeof(int) },
        { "PowerCost", typeof(int) },
        { "ArmorMitigationRatio", typeof(int) },
        { "AoE", typeof(int) },
        { "SelfPreEffects", typeof(PgSelfPreEffect[]) },
        { "RageBoost", typeof(int) },
        { "RageMultiplier", typeof(float) },
        { "Accuracy", typeof(float) },
        { "AttributesThatDeltaDamage", typeof(string[]) },
        { "AttributesThatModDamage", typeof(string[]) },
        { "AttributesThatModBaseDamage", typeof(string[]) },
        { "AttributesThatDeltaTaunt", typeof(string[]) },
        { "AttributesThatModTaunt", typeof(string[]) },
        { "AttributesThatDeltaRage", typeof(string[]) },
        { "AttributesThatModRage", typeof(string[]) },
        { "AttributesThatDeltaRange", typeof(string[]) },
        { "AttributesThatDeltaAccuracy", typeof(string[]) },
        { "AttributesThatModCritDamage", typeof(string[]) },
        { "AttributesThatDeltaTempTaunt", typeof(string[]) },
        { "SpecialValues", typeof(PgSpecialValue[]) },
        { "TauntDelta", typeof(int) },
        { "TempTauntDelta", typeof(int) },
        { "RageCost", typeof(int) },
        { "RageCostMod", typeof(float) },
        { "DoTs", typeof(PgDoT[]) },
        { "CritDamageMod", typeof(float) },
        { "SelfEffectsOnCrit", typeof(string[]) },
    };

    public static Dictionary<string, Type> TableSelfPreEffect { get; } = new Dictionary<string, Type>()
    {
        { "Enhancement", typeof(string) },
        { "Name", typeof(string) },
        { "Value", typeof(int) },
    };

    public static Dictionary<string, Type> TableAbilityRequirement { get; } = new Dictionary<string, Type>()
    {
        { "T", typeof(string) },
        { "Keyword", typeof(string) },
        { "Name", typeof(string) },
        { "Item", typeof(string) },
        { "MinCount", typeof(int) },
        { "DisallowedStates", typeof(string[]) },
        { "PetTypeTag", typeof(string) },
        { "MaxCount", typeof(float) },
        { "Recipe", typeof(string) },
        { "InteractionFlag", typeof(string) },
        { "Appearance", typeof(string) },
        { "MinHour", typeof(int) },
        { "MaxHour", typeof(int) },
        { "MaxTimesUsed", typeof(int) },
        { "ClearSky", typeof(bool) },
        { "MoonPhase", typeof(string) },
        { "Distance", typeof(int) },
        { "EntityTypeTag", typeof(string) },
        { "ErrorMessage", typeof(string) },
        { "AllowedStates", typeof(string[]) },
    };

    public static Dictionary<string, Type> TableDoT { get; } = new Dictionary<string, Type>()
    {
        { "DamagePerTick", typeof(int) },
        { "NumTicks", typeof(int) },
        { "Duration", typeof(int) },
        { "DamageType", typeof(string) },
        { "SpecialRules", typeof(string[]) },
        { "AttributesThatDelta", typeof(string[]) },
        { "AttributesThatMod", typeof(string[]) },
        { "Preface", typeof(string) },
    };

    public static Dictionary<string, Type> TableSpecialValue { get; } = new Dictionary<string, Type>()
    {
        { "Label", typeof(string) },
        { "Suffix", typeof(string) },
        { "Value", typeof(float) },
        { "AttributesThatDelta", typeof(string[]) },
        { "AttributesThatMod", typeof(string[]) },
        { "AttributesThatModBase", typeof(string[]) },
        { "DisplayType", typeof(string) },
        { "SkipIfZero", typeof(bool) },
    };

    public static Dictionary<string, Type> TableAdvancementTable { get; } = new Dictionary<string, Type>()
    {
        { "Name", typeof(string) },
        { "Levels", typeof(PgAdvancement[]) },
    };

    public static Dictionary<string, Type> TableAdvancement { get; } = new Dictionary<string, Type>()
    {
        { "Attributes", typeof(PgAdvancementEffectAttribute[]) },
        { "Level", typeof(int) },
    };

    public static Dictionary<string, Type> TableAdvancementEffectAttribute { get; } = new Dictionary<string, Type>()
    {
        { "Attribute", typeof(string) },
        { "Value", typeof(float) },
    };

    public static Dictionary<string, Type> TableAI { get; } = new Dictionary<string, Type>()
    {
        { "Abilities", typeof(PgAIAbilitySet) },
        { "Melee", typeof(bool) },
        { "Comment", typeof(string) },
        { "UncontrolledPet", typeof(bool) },
        { "ServerDriven", typeof(bool) },
        { "MinDelayBetweenAbilities", typeof(float) },
        { "UseAbilitiesWithoutEnemyTarget", typeof(bool) },
        { "Swimming", typeof(bool) },
        { "MobilityType", typeof(string) },
        { "Flying", typeof(bool) },
        { "Description", typeof(string) },
    };

    public static Dictionary<string, Type> TableAIAbility { get; } = new Dictionary<string, Type>()
    {
        { "MinLevel", typeof(int) },
        { "MaxLevel", typeof(int) },
        { "MinRange", typeof(float) },
        { "MaxRange", typeof(int) },
        { "Cue", typeof(string) },
        { "CueVal", typeof(int) },
    };

    public static Dictionary<string, Type> TableArea { get; } = new Dictionary<string, Type>()
    {
        { "FriendlyName", typeof(string) },
        { "ShortFriendlyName", typeof(string) },
    };

    public static Dictionary<string, Type> TableAttribute { get; } = new Dictionary<string, Type>()
    {
        { "Label", typeof(string) },
        { "IconIds", typeof(int[]) },
        { "Tooltip", typeof(string) },
        { "DisplayType", typeof(string) },
        { "IsHidden", typeof(bool) },
        { "DisplayRule", typeof(string) },
        { "DefaultValue", typeof(float) },
    };

    public static Dictionary<string, Type> TableDirectedGoal { get; } = new Dictionary<string, Type>()
    {
        //{ "Id", typeof(int) },
        { "Label", typeof(string) },
        { "Zone", typeof(string) },
        { "IsCategoryGate", typeof(bool) },
        { "LargeHint", typeof(string) },
        { "SmallHint", typeof(string) },
        { "CategoryGateId", typeof(int) },
        { "ForRaces", typeof(string[]) },
    };

    public static Dictionary<string, Type> TableEffect { get; } = new Dictionary<string, Type>()
    {
        { "Name", typeof(string) },
        { "Description", typeof(string) },
        { "IconId", typeof(int) },
        { "DisplayMode", typeof(string) },
        { "SpewText", typeof(string) },
        { "Particle", typeof(PgEffectParticle) },
        { "StackingType", typeof(string) },
        { "StackingPriority", typeof(int) },
        { "Duration", typeof(int) },
        { "Keywords", typeof(string[]) },
        { "AbilityKeywords", typeof(string[]) },
    };

    public static Dictionary<string, Type> TableEffectParticle { get; } = new Dictionary<string, Type>()
    {
        { "AoEColor", typeof(string) },
        { "AoERange", typeof(int) },
        { "ParticleName", typeof(string) },
    };

    public static Dictionary<string, Type> TableItem { get; } = new Dictionary<string, Type>()
    {
        { "BestowRecipes", typeof(string[]) },
        { "BestowAbility", typeof(string) },
        { "BestowQuest", typeof(string) },
        { "AllowPrefix", typeof(bool) },
        { "AllowSuffix", typeof(bool) },
        { "CraftPoints", typeof(int) },
        { "CraftingTargetLevel", typeof(int) },
        { "Description", typeof(string) },
        { "DroppedAppearance", typeof(PgDroppedAppearance) },
        { "EffectDescriptions", typeof(PgItemEffect[]) },
        { "DyeColor", typeof(string) },
        { "EquipAppearance", typeof(string) },
        { "EquipSlot", typeof(string) },
        { "IconId", typeof(int) },
        { "FoodDescription", typeof(string) },
        { "InternalName", typeof(string) },
        { "IsTemporary", typeof(bool) },
        { "IsCrafted", typeof(bool) },
        { "Keywords", typeof(PgItemKeywordValues[]) },
        { "MacGuffinQuestName", typeof(string) },
        { "MaxCarryable", typeof(int) },
        { "MaxOnVendor", typeof(int) },
        { "MaxStackSize", typeof(int) },
        { "Name", typeof(string) },
        { "RequiredAppearance", typeof(string) },
        { "SkillRequirements", typeof(PgItemSkillLink) },
        { "StockDye", typeof(PgStockDye) },
        { "TSysProfile", typeof(string) },
        { "Value", typeof(float) },
        { "NumberOfUses", typeof(int) },
        { "DestroyWhenUsedUp", typeof(bool) },
        { "Behaviors", typeof(PgItemBehavior[]) },
        { "DynamicCraftingSummary", typeof(string) },
        { "IsSkillRequirementsDefaults", typeof(bool) },
        { "BestowTitle", typeof(int) },
        { "BestowLoreBook", typeof(int) },
        { "Lint_VendorNpc", typeof(string) },
        { "MountedAppearance", typeof(string) },
        { "AttuneOnPickup", typeof(bool) },
    };

    public static Dictionary<string, Type> TableItemBehavior { get; } = new Dictionary<string, Type>()
    {
        { "UseVerb", typeof(string) },
        { "UseRequirements", typeof(string[]) },
        { "UseAnimation", typeof(string) },
        { "UseDelayAnimation", typeof(string) },
        { "MetabolismCost", typeof(int) },
        { "UseDelay", typeof(float) },
        { "MinStackSizeNeeded", typeof(int) },
    };

    public static Dictionary<string, Type> TableDroppedAppearance { get; } = new Dictionary<string, Type>()
    {
        { "Appearance", typeof(string) },
        { "Skin", typeof(string) },
        { "Cork", typeof(string) },
        { "Food", typeof(string) },
        { "Plate", typeof(string) },
        { "Color", typeof(string) },
        { "SkinColor", typeof(string) },
    };

    public static Dictionary<string, Type> TableItemKeywordValues { get; } = new Dictionary<string, Type>()
    {
        { "Keyword", typeof(string) },
        { "Values", typeof(float[]) },
    };

    public static Dictionary<string, Type> TableItemEffect { get; } = new Dictionary<string, Type>()
    {
        { "Description", typeof(string) },
        { "AttributeName", typeof(string) },
        { "AttributeEffect", typeof(float) },
    };

    public static Dictionary<string, Type> TableItemUse { get; } = new Dictionary<string, Type>()
    {
        { "RecipesThatUseItem", typeof(int[]) },
    };

    public static Dictionary<string, Type> TableStockDye { get; } = new Dictionary<string, Type>()
    {
        { "Color1", typeof(string) },
        { "Color2", typeof(string) },
        { "Color3", typeof(string) },
        { "IsGlowEnabled", typeof(bool) },
    };

    public static Dictionary<string, Type> TableLoreBookInfoCategory { get; } = new Dictionary<string, Type>()
    {
        { "Title", typeof(string) },
        { "SubTitle", typeof(string) },
        { "SortTitle", typeof(string) },
    };

    public static Dictionary<string, Type> TableLoreBook { get; } = new Dictionary<string, Type>()
    {
        { "Title", typeof(string) },
        { "LocationHint", typeof(string) },
        { "Category", typeof(string) },
        { "Keywords", typeof(string[]) },
        { "IsClientLocal", typeof(bool) },
        { "Visibility", typeof(string) },
        { "InternalName", typeof(string) },
        { "Text", typeof(string) },
    };

    public static Dictionary<string, Type> TableNpc { get; } = new Dictionary<string, Type>()
    {
        { "Name", typeof(string) },
        { "AreaName", typeof(string) },
        { "AreaFriendlyName", typeof(string) },
        { "Preferences", typeof(PgNpcPreference[]) },
    };

    public static Dictionary<string, Type> TableNpcPreference { get; } = new Dictionary<string, Type>()
    {
        { "ItemKeywords", typeof(string[]) },
        { "PreferenceMultiplier", typeof(float) },
        { "MinValueRequirement", typeof(int) },
        { "Favor", typeof(string) },
        { "Desire", typeof(string) },
        { "SkillRequirement", typeof(string) },
        { "SlotRequirement", typeof(string) },
        { "MinRarityRequirement", typeof(string) },
        { "RarityRequirement", typeof(string) },
    };

    public static Dictionary<string, Type> TablePlayerTitle { get; } = new Dictionary<string, Type>()
    {
        { "Color", typeof(string) },
        { "Title", typeof(string) },
        { "Tooltip", typeof(string) },
        { "Keywords", typeof(string[]) },
    };

    public static Dictionary<string, Type> TablePower { get; } = new Dictionary<string, Type>()
    {
        { "Prefix", typeof(string) },
        { "Suffix", typeof(string) },
        { "Tiers", typeof(PgPowerTierList) },
        { "Slots", typeof(string[]) },
        { "Skill", typeof(string) },
        { "IsUnavailable", typeof(bool) },
    };

    public static Dictionary<string, Type> TablePowerTier { get; } = new Dictionary<string, Type>()
    {
        { "EffectDescs", typeof(string[]) },
        { "SkillLevelPrereq", typeof(int) },
        { "MinLevel", typeof(int) },
        { "MaxLevel", typeof(int) },
        { "MinRarity", typeof(string) },
    };

    public static Dictionary<string, Type> TableQuest { get; } = new Dictionary<string, Type>()
    {
        { "InternalName", typeof(string) },
        { "Name", typeof(string) },
        { "Description", typeof(string) },
        { "Version", typeof(int) },
        { "RequirementsToSustain", typeof(PgQuestRequirement[]) },
        { "ReuseTime", typeof(PgQuestTime) },
        { "IsCancellable", typeof(bool) },
        { "Objectives", typeof(PgQuestObjective[]) },
        { "QuestNpc", typeof(string) },
        { "FavorNpc", typeof(string) },
        { "PrefaceText", typeof(string) },
        { "SuccessText", typeof(string) },
        { "MidwayText", typeof(string) },
        { "PrerequisiteFavorLevel", typeof(string) },
        { "Requirements", typeof(PgQuestRequirement[]) },
        { "Rewards", typeof(PgQuestReward[]) },
        { "PreGiveItems", typeof(PgQuestRewardItem[]) },
        { "TSysLevel", typeof(int) },
        { "PreGiveRecipes", typeof(string[]) },
        { "Keywords", typeof(string[]) },
        { "IsAutoPreface", typeof(bool) },
        { "IsAutoWrapUp", typeof(bool) },
        { "GroupingName", typeof(string) },
        { "IsGuildQuest", typeof(bool) },
        { "NumberOfExpectedParticipants", typeof(int) },
        { "Level", typeof(int) },
        { "WorkOrderSkill", typeof(string) },
        { "DisplayedLocation", typeof(string) },
        { "FollowUpQuests", typeof(string[]) },
        { "PreGiveEffects", typeof(PgQuestPreGiveEffect[]) },
        { "MidwayGiveItems", typeof(PgQuestRewardItem[]) },
    };

    public static Dictionary<string, Type> TableQuestRequirement { get; } = new Dictionary<string, Type>()
    {
        { "T", typeof(string) },
        { "Quest", typeof(string) },
        { "Keyword", typeof(string) },
        { "Npc", typeof(string) },
        { "Level", typeof(int) },
        { "FavorLevel", typeof(string) },
        { "Skill", typeof(string) },
        { "List", typeof(PgQuestRequirement[]) },
        { "Rule", typeof(string) },
        { "InteractionFlag", typeof(string) },
        { "HangOut", typeof(string) },
        { "AreaEvent", typeof(string) },
        { "DisallowedRace", typeof(string) },
        { "AllowedRace", typeof(string) },
        { "MoonPhase", typeof(string) },
        { "MinFavor", typeof(int) },
        { "AtomicVar", typeof(string) },
        { "Value", typeof(string) },
        { "Shape", typeof(string) },
        { "Appearance", typeof(string) },
        { "AreaName", typeof(string) },
        { "EventQuest", typeof(string) },
        { "EventSkill", typeof(string) },
    };

    public static Dictionary<string, Type> TableQuestObjective { get; } = new Dictionary<string, Type>()
    {
        { "Type", typeof(string) },
        { "Target", typeof(string) },
        { "Description", typeof(string) },
        { "Number", typeof(int) },
        { "InteractionFlags", typeof(string[]) },
        { "ItemName", typeof(string) },
        { "MinAmount", typeof(string) },
        { "MinFavorReceived", typeof(string) },
        { "MaxFavorReceived", typeof(string) },
        { "Skill", typeof(string) },
        { "StringParam", typeof(string) },
        { "ResultItemKeyword", typeof(string) },
        { "AbilityKeyword", typeof(string) },
        { "MaxAmount", typeof(string) },
        { "AnatomyType", typeof(string) },
        { "ItemKeyword", typeof(string) },
        { "MonsterTypeTag", typeof(string) },
        { "Requirements", typeof(PgQuestObjectiveRequirement[]) },
        { "Item", typeof(string) },
        { "NumberToDeliver", typeof(int) },
        { "IsHiddenUntilEarlierObjectivesComplete", typeof(bool) },
        { "InternalName", typeof(string) },
        { "GroupId", typeof(int) },
        { "BehaviorId", typeof(string) },
        { "AllowedFishingZone", typeof(string) },
        { "FishConfig", typeof(string) },
    };

    public static Dictionary<string, Type> TableQuestObjectiveRequirement { get; } = new Dictionary<string, Type>()
    {
        { "T", typeof(string) },
        { "MinHour", typeof(int) },
        { "MaxHour", typeof(int) },
        { "Keyword", typeof(string) },
        { "Appearance", typeof(string) },
        { "Skill", typeof(string) },
        { "MinCount", typeof(int) },
        { "MaxCount", typeof(int) },
        { "PetTypeTag", typeof(string) },
        { "AllowedStates", typeof(string[]) },
        { "Slot", typeof(string) },
        { "HangOut", typeof(string) },
        { "AbilityKeyword", typeof(string) },
        { "Daytime", typeof(bool) },
        { "AreaName", typeof(string) },
    };

    public static Dictionary<string, Type> TableQuestRewardItem { get; } = new Dictionary<string, Type>()
    {
        { "Item", typeof(string) },
        { "StackSize", typeof(int) },
    };

    public static Dictionary<string, Type> TableQuestReward { get; } = new Dictionary<string, Type>()
    {
        { "T", typeof(string) },
        { "Skill", typeof(string) },
        { "Xp", typeof(int) },
        { "Recipe", typeof(string) },
        { "Credits", typeof(int) },
        { "Amount", typeof(int) },
        { "Currency", typeof(string) },
        { "Ability", typeof(string) },
        { "Level", typeof(int) },
        { "Favor", typeof(int) },
        { "NamedLootProfile", typeof(string) },
        { "InteractionFlag", typeof(string) },
        { "LoreBook", typeof(string) },
        { "Title", typeof(int) },
        { "Npc", typeof(string) },
        { "Effect", typeof(string) },
        { "Item", typeof(string) },
        { "StackSize", typeof(int) },
    };

    public static Dictionary<string, Type> TableQuestPreGiveEffect { get; } = new Dictionary<string, Type>()
    {
        { "T", typeof(string) },
        { "Ability", typeof(string) },
        { "Description", typeof(string) },
        { "Item", typeof(string) },
        { "QuestGroup", typeof(string) },
        { "InteractionFlag", typeof(string) },
    };

    public static Dictionary<string, Type> TableQuestTime { get; } = new Dictionary<string, Type>()
    {
        { "Days", typeof(int) },
        { "Hours", typeof(int) },
        { "Minutes", typeof(int) },
    };

    public static Dictionary<string, Type> TableRecipe { get; } = new Dictionary<string, Type>()
    {
        { "Description", typeof(string) },
        { "IconId", typeof(int) },
        { "Ingredients", typeof(PgRecipeItem[]) },
        { "InternalName", typeof(string) },
        { "Name", typeof(string) },
        { "ResultItems", typeof(PgRecipeItem[]) },
        { "Skill", typeof(string) },
        { "SkillLevelRequirement", typeof(int) },
        { "ResultEffects", typeof(PgRecipeResultEffect[]) },
        { "SortSkill", typeof(string) },
        { "Keywords", typeof(string[]) },
        { "ActionLabel", typeof(string) },
        { "UsageDelay", typeof(float) },
        { "UsageDelayMessage", typeof(string) },
        { "UsageAnimation", typeof(string) },
        { "OtherRequirements", typeof(PgAbilityRequirement[]) },
        { "Costs", typeof(PgRecipeCost[]) },
        { "NumberOfResultItems", typeof(int) },
        { "UsageAnimationEnd", typeof(string) },
        { "ResetTimeInSeconds", typeof(int) },
        { "DyeColor", typeof(string) },
        { "RewardSkill", typeof(string) },
        { "RewardSkillXp", typeof(int) },
        { "RewardSkillXpDropOffLevel", typeof(int) },
        { "RewardSkillXpDropOffPct", typeof(float) },
        { "RewardSkillXpDropOffRate", typeof(int) },
        { "RewardSkillXpFirstTime", typeof(int) },
        { "SharesResetTimerWith", typeof(string) },
        { "ItemMenuLabel", typeof(string) },
        { "ItemMenuKeywordRequirement", typeof(string) },
        { "IsItemMenuKeywordRequirementSufficient", typeof(bool) },
        { "ItemMenuCategory", typeof(string) },
        { "ItemMenuCategoryLevel", typeof(int) },
        { "PrereqRecipe", typeof(string) },
        { "ValidationIngredientKeywords", typeof(string[]) },
        { "ProtoResultItems", typeof(PgRecipeItem[]) },
        { "RewardAllowBonusXp", typeof(bool) },
        { "RequiredAttributeNonZero", typeof(string) },
        { "LoopParticle", typeof(PgRecipeParticle) },
        { "Particle", typeof(PgRecipeParticle) },
        { "MaxUses", typeof(int) },
    };

    public static Dictionary<string, Type> TableRecipeItem { get; } = new Dictionary<string, Type>()
    {
        { "ItemCode", typeof(int) },
        { "StackSize", typeof(int) },
        { "PercentChance", typeof(float) },
        { "ItemKeys", typeof(string[]) },
        { "Description", typeof(string) },
        { "ChanceToConsume", typeof(float) },
        { "DurabilityConsumed", typeof(float) },
        { "AttuneToCrafter", typeof(bool) },
    };

    public static Dictionary<string, Type> TableRecipeCost { get; } = new Dictionary<string, Type>()
    {
        { "Currency", typeof(string) },
        { "Price", typeof(float) },
    };

    public static Dictionary<string, Type> TableRecipeParticle { get; } = new Dictionary<string, Type>()
    {
        { "ParticleName", typeof(string) },
        { "PrimaryColor", typeof(string) },
        { "SecondaryColor", typeof(string) },
        { "LightColor", typeof(string) },
    };

    public static Dictionary<string, Type> TableRecipeResultEffect { get; } = new Dictionary<string, Type>()
    {
        { "AddedQuantity", typeof(float) },
        { "AdditionalEnchantments", typeof(int) },
        { "AdjustedReuseTime", typeof(PgQuestTime) },
        { "Area", typeof(string) },
        { "Augment", typeof(string) },
        { "Boost", typeof(string) },
        { "BoostLevel", typeof(int) },
        { "BoostedAnimal", typeof(string) },
        { "BrewLine", typeof(int) },
        { "BrewParts", typeof(string[]) },
        { "BrewResults", typeof(string[]) },
        { "BrewStrength", typeof(int) },
        { "Color", typeof(string) },
        { "ConsumedEnhancementPoints", typeof(int) },
        { "ConsumedUses", typeof(int) },
        { "Delta", typeof(int) },
        { "DurationInSeconds", typeof(int) },
        { "Effect", typeof(string) },
        { "Enhancement", typeof(string) },
        { "IsCamouflaged", typeof(bool) },
        { "Item", typeof(string) },
        { "Keyword", typeof(string) },
        { "MaxHitCount", typeof(int) },
        { "MaxLevel", typeof(int) },
        { "MinLevel", typeof(int) },
        { "MoonPhase", typeof(string) },
        { "Other", typeof(string) },
        { "PowerLevel", typeof(int) },
        { "PowerWaxType", typeof(string) },
        { "RepairCooldown", typeof(PgQuestTime) },
        { "RepairMaxEfficiency", typeof(int) },
        { "RepairMinEfficiency", typeof(int) },
        { "Skill", typeof(string) },
        { "Slot", typeof(string) },
        { "Tier", typeof(int) },
        { "Type", typeof(string) },
    };

    public static Dictionary<string, Type> TableSkill { get; } = new Dictionary<string, Type>()
    {
        { "Id", typeof(int) },
        { "Description", typeof(string) },
        { "HideWhenZero", typeof(bool) },
        { "XpTable", typeof(string) },
        { "AdvancementTable", typeof(string) },
        { "Combat", typeof(bool) },
        { "TSysCompatibleCombatSkills", typeof(string[]) },
        { "MaxBonusLevels", typeof(int) },
        { "InteractionFlagLevelCaps", typeof(PgLevelCapInteraction[]) },
        { "AdvancementHints", typeof(PgAdvancementHint[]) },
        { "Rewards", typeof(PgReward[]) },
        { "Reports", typeof(PgReport[]) },
        { "Name", typeof(string) },
        { "Parents", typeof(string[]) },
        { "SkipBonusLevelsIfSkillUnlearned", typeof(bool) },
        { "AuxCombat", typeof(bool) },
        { "RecipeIngredientKeywords", typeof(string[]) },
        { "GuestLevelCap", typeof(int) },
        { "IsFakeCombatSkill", typeof(bool) },
        { "IsUmbrellaSkill", typeof(bool) },
        { "SkillLevelDisparityApplies", typeof(bool) },
    };

    public static Dictionary<string, Type> TableLevelCapInteraction { get; } = new Dictionary<string, Type>()
    {
        { "Skill", typeof(string) },
        { "Level", typeof(int) },
        { "SkillCap", typeof(int) },
        { "IsPerformanceSkill", typeof(bool) },
    };

    public static Dictionary<string, Type> TableAdvancementHint { get; } = new Dictionary<string, Type>()
    {
        { "Hint", typeof(string) },
        { "Level", typeof(int) },
        { "Npcs", typeof(string[]) },
    };

    public static Dictionary<string, Type> TableReward { get; } = new Dictionary<string, Type>()
    {
        { "Abilities", typeof(StringOrStringArray) },
        { "BonusToSkill", typeof(string) },
        { "Recipe", typeof(string) },
        { "Notes", typeof(string) },
        { "Level", typeof(int) },
        { "Races", typeof(string[]) },
    };

    public static Dictionary<string, Type> TableReport { get; } = new Dictionary<string, Type>()
    {
        { "Report", typeof(string) },
        { "Level", typeof(int) },
    };

    public static Dictionary<string, Type> TableSourceEntries { get; } = new Dictionary<string, Type>()
    {
        { "Entries", typeof(PgSource[]) },
    };

    public static Dictionary<string, Type> TableSource { get; } = new Dictionary<string, Type>()
    {
/*            { "SkillTypeId", typeof(string) },
        { "EffectName", typeof(string) },
        { "EffectTypeId", typeof(string) },
*/
        { "Skill", typeof(string) },
        { "Type", typeof(string) },
        { "ItemTypeId", typeof(int) },
        { "Npc", typeof(string) },
        { "QuestId", typeof(int) },
        { "HangOutId", typeof(int) },
    };

    public static Dictionary<string, Type> TableStorageVault { get; } = new Dictionary<string, Type>()
    {
        { "ID", typeof(int) },
        { "NpcFriendlyName", typeof(string) },
        { "Area", typeof(string) },
        { "NumberOfSlots", typeof(int) },
        { "HasAssociatedNpc", typeof(bool) },
        { "Levels", typeof(PgStorageFavorLevel) },
        { "Requirements", typeof(PgStorageRequirement[]) },
        { "RequirementDescription", typeof(string) },
        { "Grouping", typeof(string) },
        { "RequiredItemKeywords", typeof(string[]) },
        { "SlotAttribute", typeof(string) },
        { "EventLevels", typeof(PgStorageEventList) },
    };

    public static Dictionary<string, Type> TableStorageEventList { get; } = new Dictionary<string, Type>()
    {
        { "RiShinShrine_Storage1On", typeof(int) },
        { "RiShinShrine_Storage2On", typeof(int) },
    };

    public static Dictionary<string, Type> TableStorageFavorLevel { get; } = new Dictionary<string, Type>()
    {
        { "Despised", typeof(int) },
        { "Neutral", typeof(int) },
        { "Comfortable", typeof(int) },
        { "Friends", typeof(int) },
        { "CloseFriends", typeof(int) },
        { "BestFriends", typeof(int) },
        { "LikeFamily", typeof(int) },
        { "SoulMates", typeof(int) },
    };

    public static Dictionary<string, Type> TableStorageRequirement { get; } = new Dictionary<string, Type>()
    {
        { "T", typeof(string) },
        { "InteractionFlag", typeof(string) },
    };

    public static Dictionary<string, Type> TableXpTable { get; } = new Dictionary<string, Type>()
    {
        { "InternalName", typeof(string) },
        { "XpAmounts", typeof(int[]) },
    };

    public static Dictionary<Type, FieldTable> Tables { get; } = new Dictionary<Type, FieldTable>()
    {
        { typeof(PgAbility), new FixedFieldTable(TableAbility) },
        { typeof(PgAbilityAmmo), new FixedFieldTable(TableAbilityAmmo) },
        { typeof(PgAbilityPvX), new FixedFieldTable(TableAbilityPvX) },
        { typeof(PgSelfPreEffect), new FixedFieldTable(TableSelfPreEffect) },
        { typeof(PgAbilityRequirement), new FixedFieldTable(TableAbilityRequirement) },
        { typeof(PgSelfParticle), new FixedFieldTable(TableSelfParticle) },
        { typeof(PgSelfPreParticle), new FixedFieldTable(TableSelfPreParticle) },
        { typeof(PgTargetParticle), new FixedFieldTable(TableTargetParticle) },
        { typeof(PgDoT), new FixedFieldTable(TableDoT) },
        { typeof(PgSpecialValue), new FixedFieldTable(TableSpecialValue) },
        { typeof(PgAdvancementTable), new FixedFieldTable(TableAdvancementTable) },
        { typeof(PgAdvancement), new FixedFieldTable(TableAdvancement) },
        { typeof(PgAdvancementEffectAttribute), new FixedFieldTable(TableAdvancementEffectAttribute) },
        { typeof(PgAI), new FixedFieldTable(TableAI) },
        { typeof(PgAIAbilitySet), new VariadicFieldTable(typeof(PgAIAbility)) },
        { typeof(PgAIAbility), new FixedFieldTable(TableAIAbility) },
        { typeof(PgArea), new FixedFieldTable(TableArea) },
        { typeof(PgAttribute), new FixedFieldTable(TableAttribute) },
        { typeof(PgDirectedGoal), new FixedFieldTable(TableDirectedGoal) },
        { typeof(PgEffect), new FixedFieldTable(TableEffect) },
        { typeof(PgEffectParticle), new FixedFieldTable(TableEffectParticle) },
        { typeof(PgItem), new FixedFieldTable(TableItem) },
        { typeof(PgItemSkillLink), new VariadicFieldTable(typeof(int)) },
        { typeof(PgItemBehavior), new FixedFieldTable(TableItemBehavior) },
        { typeof(PgDroppedAppearance), new FixedFieldTable(TableDroppedAppearance) },
        { typeof(PgItemKeywordValues), new FixedFieldTable(TableItemKeywordValues) },
        { typeof(PgItemEffect), new FixedFieldTable(TableItemEffect) },
        { typeof(PgItemUse), new FixedFieldTable(TableItemUse) },
        { typeof(PgStockDye), new FixedFieldTable(TableStockDye) },
        { typeof(PgLoreBookInfo), new VariadicFieldTable(typeof(PgLoreBookInfoCategory)) },
        { typeof(PgLoreBookInfoCategory), new FixedFieldTable(TableLoreBookInfoCategory) },
        { typeof(PgLoreBook), new FixedFieldTable(TableLoreBook) },
        { typeof(PgNpc), new FixedFieldTable(TableNpc) },
        { typeof(PgNpcPreference), new FixedFieldTable(TableNpcPreference) },
        { typeof(PgPlayerTitle), new FixedFieldTable(TablePlayerTitle) },
        { typeof(PgPower), new FixedFieldTable(TablePower) },
        { typeof(PgPowerTierList), new VariadicFieldTable(typeof(PgPowerTier)) },
        { typeof(PgPowerTier), new FixedFieldTable(TablePowerTier) },
        { typeof(PgQuest), new FixedFieldTable(TableQuest) },
        { typeof(PgQuestRequirement), new FixedFieldTable(TableQuestRequirement) },
        { typeof(PgQuestObjective), new FixedFieldTable(TableQuestObjective) },
        { typeof(PgQuestObjectiveRequirement), new FixedFieldTable(TableQuestObjectiveRequirement) },
        { typeof(PgQuestRewardItem), new FixedFieldTable(TableQuestRewardItem) },
        { typeof(PgQuestReward), new FixedFieldTable(TableQuestReward) },
        { typeof(PgQuestPreGiveEffect), new FixedFieldTable(TableQuestPreGiveEffect) },
        { typeof(PgQuestTime), new FixedFieldTable(TableQuestTime) },
        { typeof(PgRecipe), new FixedFieldTable(TableRecipe) },
        { typeof(PgRecipeItem), new FixedFieldTable(TableRecipeItem) },
        { typeof(PgRecipeCost), new FixedFieldTable(TableRecipeCost) },
        { typeof(PgRecipeParticle), new FixedFieldTable(TableRecipeParticle) },
        { typeof(PgRecipeResultEffect), new FixedFieldTable(TableRecipeResultEffect) },
        { typeof(PgSkill), new FixedFieldTable(TableSkill) },
        { typeof(PgLevelCapInteraction), new FixedFieldTable(TableLevelCapInteraction) },
        { typeof(PgAdvancementHint), new FixedFieldTable(TableAdvancementHint) },
        { typeof(PgReward), new FixedFieldTable(TableReward) },
        { typeof(PgReport), new FixedFieldTable(TableReport) },
        { typeof(PgSource), new FixedFieldTable(TableSource) },
        { typeof(PgSourceEntriesAbility), new FixedFieldTable(TableSourceEntries) },
        { typeof(PgSourceEntriesRecipe), new FixedFieldTable(TableSourceEntries) },
        { typeof(PgStorageVault), new FixedFieldTable(TableStorageVault) },
        { typeof(PgStorageEventList), new FixedFieldTable(TableStorageEventList) },
        { typeof(PgStorageFavorLevel), new FixedFieldTable(TableStorageFavorLevel) },
        { typeof(PgStorageRequirement), new VariadicFieldTable(typeof(string)) },
        { typeof(PgXpTable), new FixedFieldTable(TableXpTable) },
    };

    private static List<Type> TypeWithNameList = new List<Type>()
    {
        typeof(PgEffect),
        typeof(PgRecipe),
    };

    private static List<Type> TypeWithInternalNameList = new List<Type>()
    {
        typeof(PgAbility),
        typeof(PgItem),
        typeof(PgLoreBook),
        typeof(PgQuest),
        typeof(PgRecipe),
        typeof(PgXpTable),
    };

    private static List<Type> UsedTableList = new List<Type>();
    private static List<Type> UsedNameList = new List<Type>();
    private static List<Type> UsedInternalNameList = new List<Type>();

    public static bool GetTable(Type type, out FieldTable table)
    {
        if (!Tables.ContainsKey(type))
        {
            table = FixedFieldTable.Empty;
            return false;
        }

        table = Tables[type];

        if (!UsedTableList.Contains(type))
            UsedTableList.Add(type);

        return true;
    }

    public static bool IsTypeWithName(Type type)
    {
        if (!TypeWithNameList.Contains(type))
            return false;

        if (!UsedNameList.Contains(type))
            UsedNameList.Add(type);

        return true;
    }

    public static bool IsTypeWithInternalName(Type type)
    {
        if (!TypeWithInternalNameList.Contains(type))
            return false;

        if (!UsedInternalNameList.Contains(type))
            UsedInternalNameList.Add(type);

        return true;
    }

    public static bool VerifyTablesCompletion()
    {
        if (!VerifyTablesCompletion(Tables, UsedTableList))
            return false;

        if (!VerifyTablesCompletion(TypeWithNameList, UsedNameList))
            return false;

        if (!VerifyTablesCompletion(TypeWithInternalNameList, UsedInternalNameList))
            return false;

        return true;
    }

    public static bool VerifyTablesCompletion(Dictionary<Type, FieldTable> table, List<Type> usedList)
    {
        bool Result = true;

        foreach (KeyValuePair<Type, FieldTable> Entry in table)
        {
            Type Key = Entry.Key;

            if (!usedList.Contains(Key))
            {
                Result &= Program.ReportFailure($"Type {Key} was not used during parsing");
                continue;
            }

            Result &= Entry.Value.VerifyTableCompletion(Key);
        }

        return Result;
    }

    public static bool VerifyTablesCompletion(List<Type> list, List<Type> usedList)
    {
        bool Result = true;

        foreach (Type Key in list)
            if (!usedList.Contains(Key))
                Result &= Program.ReportFailure($"Type {Key} was not used during parsing");

        return Result;
    }
}
