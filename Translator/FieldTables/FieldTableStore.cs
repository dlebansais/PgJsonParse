namespace Translator
{
    using PgJsonObjects;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    public class FieldTableStore
    {
        public static Dictionary<string, Type> TableAbility = new Dictionary<string, Type>()
        {
            { "AbilityGroup", typeof(string) },
            { "Animation", typeof(string) },
            { "AttributesThatModAmmoConsumeChance", typeof(string[]) },
            { "AttributesThatDeltaDelayLoopTime", typeof(string[]) },
            { "AttributesThatDeltaPowerCost", typeof(string[]) },
            { "AttributesThatDeltaResetTime", typeof(string[]) },
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
            { "EffectKeywordsIndicatingEnabled", typeof(string[]) },
            { "ExtraKeywordsForTooltips", typeof(string[]) },
            { "IconID", typeof(int) },
            { "IgnoreEffectErrors", typeof(bool) },
            { "InternalAbility", typeof(bool) },
            { "InternalName", typeof(string) },
            { "IsHarmless", typeof(bool) },
            { "ItemKeywordReqErrorMessage", typeof(string) },
            { "ItemKeywordReqs", typeof(string[]) },
            { "Keywords", typeof(string[]) },
            { "Level", typeof(int) },
            { "Name", typeof(string) },
            { "PetTypeTagReq", typeof(string) },
            { "PetTypeTagReqMax", typeof(int) },
            { "Prerequisite", typeof(string) },
            { "Projectile", typeof(string) },
            { "PvE", typeof(PgAbilityPvX) },
            { "PvP", typeof(PgAbilityPvX) },
            { "ResetTime", typeof(float) },
            { "SelfParticle", typeof(string) },
            { "AmmoDescription", typeof(string) },
            { "SharesResetTimerWith", typeof(string) },
            { "Skill", typeof(string) },
            { "SpecialCasterRequirements", typeof(PgAbilityRequirement[]) },
            { "SpecialCasterRequirementsErrorMessage", typeof(string) },
            { "SpecialInfo", typeof(string) },
            { "SpecialTargetingTypeReq", typeof(int) },
            { "Target", typeof(string) },
            { "TargetEffectKeywordReq", typeof(string) },
            { "TargetParticle", typeof(string) },
            { "UpgradeOf", typeof(string) },
            { "WorksInCombat", typeof(bool) },
            { "WorksUnderwater", typeof(bool) },
            { "WorksWhileFalling", typeof(bool) },
            { "DelayLoopIsOnlyUsedInCombat", typeof(bool) },
            { "AmmoKeywords", typeof(PgAbilityAmmo[]) },
            { "AmmoConsumeChance", typeof(float) },
            { "AmmoStickChance", typeof(float) },
        };

        public static Dictionary<string, Type> TableAbilityAmmo = new Dictionary<string, Type>()
        {
            { "ItemKeyword", typeof(string) },
            { "Count", typeof(int)},
        };

        public static Dictionary<string, Type> TableAbilityPvX = new Dictionary<string, Type>()
        {
            { "Damage", typeof(int) },
            { "HealthSpecificDamage", typeof(int)},
            { "ExtraDamageIfTargetVulnerable", typeof(int)},
            { "ArmorSpecificDamage", typeof(int)},
            { "Range", typeof(int)},
            { "PowerCost", typeof(int)},
            { "MetabolismCost", typeof(int)},
            { "ArmorMitigationRatio", typeof(int)},
            { "AoE", typeof(int)},
            { "SelfPreEffects", typeof(string[])},
            { "RageBoost", typeof(int)},
            { "RageMultiplier", typeof(float)},
            { "Accuracy", typeof(float)},
            { "AttributesThatDeltaDamage", typeof(string[])},
            { "AttributesThatModDamage", typeof(string[])},
            { "AttributesThatModBaseDamage", typeof(string[])},
            { "AttributesThatDeltaTaunt", typeof(string[])},
            { "AttributesThatModTaunt", typeof(string[])},
            { "AttributesThatDeltaRage", typeof(string[])},
            { "AttributesThatModRage", typeof(string[])},
            { "AttributesThatDeltaRange", typeof(string[])},
            { "AttributesThatDeltaAccuracy", typeof(string[])},
            { "AttributesThatModCritDamage", typeof(string[])},
            { "SpecialValues", typeof(PgSpecialValue[])},
            { "TauntDelta", typeof(int)},
            { "TempTauntDelta", typeof(int)},
            { "RageCost", typeof(int)},
            { "RageCostMod", typeof(float)},
            { "DoTs", typeof(PgDoT[])},
            { "CritDamageMod", typeof(float)},
            { "SelfEffectsOnCrit", typeof(string[])},
        };

        public static Dictionary<string, Type> TableAbilityRequirement = new Dictionary<string, Type>()
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
        };

        public static Dictionary<string, Type> TableDoT = new Dictionary<string, Type>()
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

        public static Dictionary<string, Type> TableSpecialValue = new Dictionary<string, Type>()
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

        public static Dictionary<string, Type> TableAdvancement = new Dictionary<string, Type>()
        {
            { "VULN_CRUSHING", typeof(float) },
            { "VULN_SLASHING", typeof(float) },
            { "VULN_NATURE", typeof(float) },
            { "VULN_FIRE", typeof(float) },
            { "VULN_COLD", typeof(float) },
            { "VULN_PIERCING", typeof(float) },
            { "VULN_PSYCHIC", typeof(float) },
            { "VULN_TRAUMA", typeof(float) },
            { "VULN_ELECTRICITY", typeof(float) },
            { "VULN_POISON", typeof(float) },
            { "VULN_ACID", typeof(float) },
            { "VULN_DARKNESS", typeof(float) },
            { "VULN_BURST", typeof(float) },
            { "MITIGATION_CRUSHING", typeof(float) },
            { "MITIGATION_SLASHING", typeof(float) },
            { "MITIGATION_NATURE", typeof(float) },
            { "MITIGATION_FIRE", typeof(float) },
            { "MITIGATION_PIERCING", typeof(float) },
            { "MITIGATION_TRAUMA_DIRECT", typeof(float) },
            { "MITIGATION_TRAUMA_INDIRECT", typeof(float) },
            { "MITIGATION_POISON", typeof(float) },
            { "MITIGATION_POISON_DIRECT", typeof(float) },
            { "MITIGATION_POISON_INDIRECT", typeof(float) },
            { "MOD_FIRE_DIRECT", typeof(float) },
            { "MOD_ELECTRICITY_DIRECT", typeof(float) },
            { "MOD_DARKNESS_DIRECT", typeof(float) },
            { "MOD_FIRE_INDIRECT", typeof(float) },
            { "IGNORE_CHANCE_FEAR", typeof(float) },
            { "IGNORE_CHANCE_MEZ", typeof(float) },
            { "IGNORE_CHANCE_KNOCKBACK", typeof(float) },
            { "MENTAL_DEFENSE_RATING", typeof(float) },
            { "NONCOMBAT_REGEN_HEALTH_MOD", typeof(float) },
            { "COMBAT_REGEN_HEALTH_MOD", typeof(float) },
            { "COMBAT_REGEN_HEALTH_DELTA", typeof(float) },
            { "NONCOMBAT_REGEN_ARMOR_MOD", typeof(float) },
            { "NONCOMBAT_REGEN_ARMOR_DELTA", typeof(float) },
            { "COMBAT_REGEN_ARMOR_MOD", typeof(float) },
            { "NONCOMBAT_REGEN_POWER_MOD", typeof(float) },
            { "COMBAT_REGEN_POWER_MOD", typeof(float) },
            { "SPRINT_BOOST", typeof(float) },
            { "TAUNT_MOD", typeof(float) },
            { "EVASION_CHANCE", typeof(float) },
            { "EVASION_CHANCE_PROJECTILE", typeof(float) },
            { "EVASION_CHANCE_MELEE", typeof(float) },
            { "MOD_CRITICAL_HIT_DAMAGE_RAGEATTACK", typeof(float) },
            { "BOOST_WEREWOLFMETABOLISM_HEALTHREGEN", typeof(float) },
            { "BOOST_WEREWOLFMETABOLISM_POWERREGEN", typeof(float) },
            { "LOOT_BOOST_CHANCE_UNCOMMON", typeof(float) },
            { "LOOT_BOOST_CHANCE_RARE", typeof(float) },
            { "LOOT_BOOST_CHANCE_EXCEPTIONAL", typeof(float) },
            { "LOOT_BOOST_CHANCE_EPIC", typeof(float) },
            { "LOOT_BOOST_CHANCE_LEGENDARY", typeof(float) },
            { "MAX_HEALTH", typeof(float) },
            { "MAX_ARMOR", typeof(float) },
            { "MAX_RAGE", typeof(float) },
            { "MAX_POWER", typeof(float) },
            { "MAX_BREATH", typeof(float) },
            { "BOOST_UNIVERSAL_DIRECT", typeof(float) },
            { "BOOST_ABILITY_RAGEATTACK", typeof(float) },
            { "MOD_ABILITY_RAGEATTACK", typeof(float) },
            { "MONSTER_COMBAT_XP_VALUE", typeof(float) },
            { "COMBAT_REGEN_ARMOR_DELTA", typeof(float) },
            { "COMBAT_REGEN_POWER_DELTA", typeof(float) },
            { "MAX_INVENTORY_SIZE", typeof(float) },
            { "MAX_METABOLISM", typeof(float) },
            { "NPC_MOD_FAVORFROMGIFTS", typeof(float) },
            { "NPC_MOD_FAVORFROMHANGOUTS", typeof(float) },
            { "NPC_MOD_MAXSALESVALUE", typeof(float) },
            { "NPC_MOD_TRAININGCOST", typeof(float) },
            { "NUM_INVENTORY_FOLDERS", typeof(int) },
            { "HIGH_CLEANLINESS_XP_EARNED_MOD", typeof(float) },
            { "LOW_CLEANLINESS_XP_EARNED_MOD", typeof(float) },
            { "SHOW_CLEANLINESS_INDICATORS", typeof(float) },
            { "HIGH_COMMUNITY_XP_EARNED_MOD", typeof(float) },
            { "LOW_COMMUNITY_XP_EARNED_MOD", typeof(float) },
            { "SHOW_COMMUNITY_INDICATORS", typeof(float) },
            { "HIGH_PEACEABLENESS_XP_EARNED_MOD", typeof(float) },
            { "LOW_PEACEABLENESS_XP_EARNED_MOD", typeof(float) },
            { "SHOW_PEACEABLENESS_INDICATORS", typeof(float) },
            { "STAFF_ARMOR_AUTOHEAL", typeof(float) },
            { "MAX_MAP_PINS_PER_AREA", typeof(float) },
            { "MAX_MAP_PIN_ICONS", typeof(float) },
            { "WORKORDER_COIN_REWARD_MOD", typeof(float) },
            { "MAX_ACTIVE_WORKORDERS", typeof(float) },
            { "PLAYER_ORDERS_MAX_ACTIVE", typeof(float) },
            { "SHOP_INVENTORY_SIZE_DELTA", typeof(float) },
            { "MAIL_SHOP_NUMFREE", typeof(float) },
            { "SHOP_HIRING_MAX_PREPAY_DAYS", typeof(float) },
            { "SHOP_LOG_DAYSKEPT", typeof(float) },
            { "SHOP_HIRING_NUMFREE", typeof(float) },
            { "MOD_CRITICAL_HIT_DAMAGE", typeof(float) },
            { "MONSTER_CRIT_CHANCE", typeof(float) },
            { "ACCURACY_BOOST", typeof(float) },
            { "BOOST_POISON_INDIRECT", typeof(float) },
            { "MONSTER_MATCH_OWNER_SPEED", typeof(int) },
            { "ARMOR_MITIGATION_MOD", typeof(float) },
            { "AUTOHEAL_HEALTH_MOD", typeof(float) },
            { "AUTOHEAL_ARMOR_MOD", typeof(float) },
            { "ARMOR_MITIGATION_RATIO", typeof(float) },
            { "SHOW_FAIRYENERGY_INDICATORS", typeof(int) },
            { "BOOST_ABILITY_PET_SPECIALATTACK", typeof(int) },
            { "BOOST_ABILITY_PET_SPECIALTRICK", typeof(int) },
            { "BOOST_ABILITY_PET_BASICATTACK", typeof(int) },
            { "BOOST_AUTOHEAL_HEALTH_SENDER", typeof(float) },
            { "BOOST_AUTOHEAL_ARMOR_SENDER", typeof(float) },
            { "BOOST_TRAUMA_INDIRECT", typeof(float) },
        };

        public static Dictionary<string, Type> TableAI = new Dictionary<string, Type>()
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
            { "FollowClose", typeof(bool) },
            { "Description", typeof(string) },
        };

        public static Dictionary<string, Type> TableAIAbility = new Dictionary<string, Type>()
        {
            { "minLevel", typeof(int) },
            { "maxLevel", typeof(int) },
            { "minDistance", typeof(int) },
            { "minRange", typeof(int) },
            { "maxRange", typeof(int) },
            { "cue", typeof(string) },
            { "cueVal", typeof(int) },
        };

        public static Dictionary<string, Type> TableArea = new Dictionary<string, Type>()
        {
            { "FriendlyName", typeof(string) },
            { "ShortFriendlyName", typeof(string) },
        };

        public static Dictionary<string, Type> TableAttribute = new Dictionary<string, Type>()
        {
            { "Label", typeof(string) },
            { "IconIds", typeof(int[]) },
            { "Tooltip", typeof(string) },
            { "DisplayType", typeof(string) },
            { "IsHidden", typeof(bool) },
            { "DisplayRule", typeof(string) },
            { "DefaultValue", typeof(float) },
        };

        public static Dictionary<string, Type> TableDirectedGoal = new Dictionary<string, Type>()
        {
            { "Id", typeof(int) },
            { "Label", typeof(string) },
            { "Zone", typeof(string) },
            { "IsCategoryGate", typeof(bool) },
            { "LargeHint", typeof(string) },
            { "SmallHint", typeof(string) },
            { "CategoryGateId", typeof(int) },
            { "ForRaces", typeof(string[]) },
        };

        public static Dictionary<string, Type> TableEffect = new Dictionary<string, Type>()
        {
            { "Name", typeof(string) },
            { "Desc", typeof(string) },
            { "IconId", typeof(int) },
            { "DisplayMode", typeof(string) },
            { "SpewText", typeof(string) },
            { "Particle", typeof(string) },
            { "StackingType", typeof(string) },
            { "StackingPriority", typeof(int) },
            { "Duration", typeof(int) },
            { "Keywords", typeof(string[]) },
            { "AbilityKeywords", typeof(string[]) },
        };

        public static Dictionary<string, Type> TableItem = new Dictionary<string, Type>()
        {
            { "BestowRecipes", typeof(string[]) },
            { "BestowAbility", typeof(string) },
            { "BestowQuest", typeof(string) },
            { "AllowPrefix", typeof(bool) },
            { "AllowSuffix", typeof(bool) },
            { "CraftPoints", typeof(int) },
            { "CraftingTargetLevel", typeof(int) },
            { "Description", typeof(string) },
            { "DroppedAppearance", typeof(string) },
            { "EffectDescs", typeof(string[]) },
            { "DyeColor", typeof(string) },
            { "EquipAppearance", typeof(string) },
            { "EquipSlot", typeof(string) },
            { "IconId", typeof(int) },
            { "InternalName", typeof(string) },
            { "IsTemporary", typeof(bool) },
            { "IsCrafted", typeof(bool) },
            { "Keywords", typeof(string[]) },
            { "MacGuffinQuestName", typeof(string) },
            { "MaxCarryable", typeof(int) },
            { "MaxOnVendor", typeof(int) },
            { "MaxStackSize", typeof(int) },
            { "Name", typeof(string) },
            { "RequiredAppearance", typeof(string) },
            { "SkillReqs", typeof(PgItemSkillLink) },
            { "StockDye", typeof(string) },
            { "Value", typeof(float) },
            { "NumUses", typeof(int) },
            { "DestroyWhenUsedUp", typeof(bool) },
            { "Behaviors", typeof(PgItemBehavior[]) },
            { "DynamicCraftingSummary", typeof(string) },
            { "IsSkillReqsDefaults", typeof(bool) },
            { "BestowTitle", typeof(int) },
            { "BestowLoreBook", typeof(int) },
            { "Lint_VendorNpc", typeof(string) },
        };

        public static Dictionary<string, Type> TableItemBehavior = new Dictionary<string, Type>()
        {
            { "UseVerb", typeof(string) },
            { "UseRequirements", typeof(string[]) },
            { "UseAnimation", typeof(string) },
            { "UseDelayAnimation", typeof(string) },
            { "MetabolismCost", typeof(int) },
            { "UseDelay", typeof(float) },
        };

        public static Dictionary<string, Type> TableItemUse = new Dictionary<string, Type>()
        {
            { "RecipesThatUseItem", typeof(int[]) },
        };

        public static Dictionary<string, Type> TableLoreBookInfoCategory = new Dictionary<string, Type>()
        {
            { "Title", typeof(string) },
            { "SubTitle", typeof(string) },
            { "SortTitle", typeof(string) },
        };

        public static Dictionary<string, Type> TableLoreBook = new Dictionary<string, Type>()
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

        public static Dictionary<string, Type> TableNpc = new Dictionary<string, Type>()
        {
            { "Name", typeof(string) },
            { "AreaName", typeof(string) },
            { "AreaFriendlyName", typeof(string) },
            { "Preferences", typeof(PgNpcPreference[]) },
        };

        public static Dictionary<string, Type> TableNpcPreference = new Dictionary<string, Type>()
        {
            { "Keywords", typeof(string[]) },
            { "Pref", typeof(float) },
        };

        public static Dictionary<string, Type> TablePlayerTitle = new Dictionary<string, Type>()
        {
            { "Title", typeof(string) },
            { "Tooltip", typeof(string) },
            { "Keywords", typeof(string[]) },
        };

        public static Dictionary<string, Type> TablePower = new Dictionary<string, Type>()
        {
            { "Prefix", typeof(string) },
            { "Suffix", typeof(string) },
            { "Tiers", typeof(PgPowerTierList) },
            { "Slots", typeof(string[]) },
            { "Skill", typeof(string) },
            { "IsUnavailable", typeof(bool) },
        };

        public static Dictionary<string, Type> TablePowerTier = new Dictionary<string, Type>()
        {
            { "EffectDescs", typeof(string[]) },
            { "SkillLevelPrereq", typeof(int) },
        };

        public static Dictionary<string, Type> TableQuest = new Dictionary<string, Type>()
        {
            { "InternalName", typeof(string) },
            { "Name", typeof(string) },
            { "Description", typeof(string) },
            { "Version", typeof(int) },
            { "RequirementsToSustain", typeof(PgQuestRequirement[]) },
            { "ReuseTime_Minutes", typeof(int) },
            { "IsCancellable", typeof(bool) },
            { "Objectives", typeof(PgQuestObjective[]) },
            { "Rewards_XP", typeof(PgQuestRewardXp) },
            { "Rewards_Currency", typeof(PgQuestRewardCurrency) },
            { "Rewards_Items", typeof(PgQuestRewardItem[]) },
            { "ReuseTime_Days", typeof(int) },
            { "ReuseTime_Hours", typeof(int) },
            { "Reward_CombatXP", typeof(int) },
            { "FavorNpc", typeof(string) },
            { "PrefaceText", typeof(string) },
            { "SuccessText", typeof(string) },
            { "MidwayText", typeof(string) },
            { "PrerequisiteFavorLevel", typeof(string) },
            { "Rewards_Favor", typeof(int) },
            { "Rewards_Recipes", typeof(string[]) },
            { "Rewards_Ability", typeof(string) },
            { "Requirements", typeof(PgQuestRequirement[]) },
            { "Reward_Favor", typeof(int) },
            { "Rewards", typeof(PgQuestReward[]) },
            { "PreGiveItems", typeof(PgQuestRewardItem[]) },
            { "TSysLevel", typeof(int) },
            { "Reward_Gold", typeof(int) },
            { "Rewards_NamedLootProfile", typeof(string) },
            { "PreGiveRecipes", typeof(string[]) },
            { "Keywords", typeof(string[]) },
            { "Rewards_Effects", typeof(string[]) },
            { "IsAutoPreface", typeof(bool) },
            { "IsAutoWrapUp", typeof(bool) },
            { "GroupingName", typeof(string) },
            { "IsGuildQuest", typeof(bool) },
            { "NumExpectedParticipants", typeof(int) },
            { "Level", typeof(int) },
            { "WorkOrderSkill", typeof(string) },
            { "DisplayedLocation", typeof(string) },
            { "FollowUpQuests", typeof(string[]) },
            { "PreGiveEffects", typeof(string[]) },
            { "MidwayGiveItems", typeof(PgQuestRewardItem[]) },
        };

        public static Dictionary<string, Type> TableQuestRequirement = new Dictionary<string, Type>()
        {
            { "T", typeof(string) },
            { "Quest", typeof(string) },
            { "Keyword", typeof(string) },
            { "Npc", typeof(string) },
            { "Level", typeof(StringOrInteger) },
            { "Skill", typeof(string) },
            { "List", typeof(PgQuestRequirement[]) },
            { "Rule", typeof(string) },
            { "InteractionFlag", typeof(string) },
            { "HangOut", typeof(string) },
            { "AreaEvent", typeof(string) },
            { "DisallowedRace", typeof(string) },
            { "AllowedRace", typeof(string) },
        };

        public static Dictionary<string, Type> TableQuestObjective = new Dictionary<string, Type>()
        {
            { "Type", typeof(string) },
            { "Target", typeof(string) },
            { "Description", typeof(string) },
            { "Number", typeof(int) },
            { "InteractionFlags", typeof(string[]) },
            { "ItemName", typeof(string) },
            { "InteractionFlag", typeof(string) },
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
            { "NumToDeliver", typeof(int) },
            { "IsHiddenUntilEarlierObjectivesComplete", typeof(bool) },
            { "InternalName", typeof(string) },
            { "GroupId", typeof(int) },
        };

        public static Dictionary<string, Type> TableQuestObjectiveRequirement = new Dictionary<string, Type>()
        {
            { "T", typeof(string) },
            { "MinHour", typeof(int) },
            { "MaxHour", typeof(int) },
            { "Keyword", typeof(string) },
            { "Appearance", typeof(string) },
        };

        public static Dictionary<string, Type> TableQuestRewardItem = new Dictionary<string, Type>()
        {
            { "Item", typeof(string) },
            { "StackSize", typeof(int) },
        };

        public static Dictionary<string, Type> TableQuestReward = new Dictionary<string, Type>()
        {
            { "T", typeof(string) },
            { "Skill", typeof(string) },
            { "Xp", typeof(int) },
            { "Recipe", typeof(string) },
            { "Credits", typeof(int) },
        };

        public static Dictionary<string, Type> TableRecipe = new Dictionary<string, Type>()
        {
            { "Description", typeof(string) },
            { "IconId", typeof(int) },
            { "Ingredients", typeof(PgRecipeItem[]) },
            { "InternalName", typeof(string) },
            { "Name", typeof(string) },
            { "ResultItems", typeof(PgRecipeItem[]) },
            { "Skill", typeof(string) },
            { "SkillLevelReq", typeof(int) },
            { "ResultEffects", typeof(string[]) },
            { "SortSkill", typeof(string) },
            { "Keywords", typeof(string[]) },
            { "ActionLabel", typeof(string) },
            { "UsageDelay", typeof(float) },
            { "UsageDelayMessage", typeof(string) },
            { "UsageAnimation", typeof(string) },
            { "OtherRequirements", typeof(PgAbilityRequirement[]) },
            { "Costs", typeof(PgRecipeCost[]) },
            { "NumResultItems", typeof(int) },
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
            { "ItemMenuKeywordReq", typeof(string) },
            { "IsItemMenuKeywordReqSufficient", typeof(bool) },
            { "ItemMenuCategory", typeof(string) },
            { "ItemMenuCategoryLevel", typeof(int) },
            { "PrereqRecipe", typeof(string) },
            { "ValidationIngredientKeywords", typeof(string[]) },
            { "ProtoResultItems", typeof(PgRecipeItem[]) },
            { "RewardAllowBonusXp", typeof(bool) },
        };

        public static Dictionary<string, Type> TableRecipeItem = new Dictionary<string, Type>()
        {
            { "ItemCode", typeof(int) },
            { "StackSize", typeof(int) },
            { "PercentChance", typeof(float) },
            { "ItemKeys", typeof(string[]) },
            { "Desc", typeof(string) },
            { "ChanceToConsume", typeof(float) },
            { "DurabilityConsumed", typeof(float) },
            { "AttuneToCrafter", typeof(bool) },
        };

        public static Dictionary<string, Type> TableRecipeCost = new Dictionary<string, Type>()
        {
            { "Currency", typeof(string) },
            { "Price", typeof(float) },
        };

        public static Dictionary<string, Type> TableSkill = new Dictionary<string, Type>()
        {
            { "Id", typeof(int) },
            { "Description", typeof(string) },
            { "HideWhenZero", typeof(bool) },
            { "XpTable", typeof(string) },
            { "AdvancementTable", typeof(string) },
            { "Combat", typeof(bool) },
            { "TSysCompatibleCombatSkills", typeof(string[]) },
            { "MaxBonusLevels", typeof(int) },
            { "InteractionFlagLevelCaps", typeof(PgLevelCapInteraction) },
            { "AdvancementHints", typeof(PgAdvancementHint) },
            { "Rewards", typeof(PgRewardList) },
            { "Reports", typeof(PgReport) },
            { "Name", typeof(string) },
            { "Parents", typeof(string[]) },
            { "SkipBonusLevelsIfSkillUnlearned", typeof(bool) },
            { "AuxCombat", typeof(bool) },
            { "RecipeIngredientKeywords", typeof(string[]) },
            { "_RecipeIngredientKeywords", typeof(string[]) },
            { "GuestLevelCap", typeof(int) },
            { "IsFakeCombatSkill", typeof(bool) },
        };

        public static Dictionary<string, Type> TableReward = new Dictionary<string, Type>()
        {
            { "Ability", typeof(string) },
            { "BonusToSkill", typeof(string) },
            { "Recipe", typeof(string) },
            { "Notes", typeof(string) },
        };

        public static Dictionary<string, Type> TableSource = new Dictionary<string, Type>()
        {
            { "SkillTypeId", typeof(string) },
            { "Type", typeof(string) },
            { "ItemTypeId", typeof(int) },
            { "Npc", typeof(string) },
            { "EffectName", typeof(string) },
            { "EffectTypeId", typeof(string) },
            { "QuestId", typeof(int) },
        };

        public static Dictionary<string, Type> TableStorageVault = new Dictionary<string, Type>()
        {
            { "ID", typeof(int) },
            { "NpcFriendlyName", typeof(string) },
            { "Area", typeof(string) },
            { "NumSlots", typeof(int) },
            { "HasAssociatedNpc", typeof(bool) },
            { "Levels", typeof(PgStorageFavorLevel) },
            { "Requirements", typeof(PgStorageRequirement) },
            { "RequirementDescription", typeof(string) },
            { "Grouping", typeof(string) },
            { "RequiredItemKeywords", typeof(string[]) },
            { "SlotAttribute", typeof(string) },
        };

        public static Dictionary<string, Type> TableStorageFavorLevel = new Dictionary<string, Type>()
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

        public static Dictionary<string, Type> TableStorageRequirement = new Dictionary<string, Type>()
        {
            { "T", typeof(string) },
            { "InteractionFlag", typeof(string) },
        };

        public static Dictionary<string, Type> TableXpTable = new Dictionary<string, Type>()
        {
            { "InternalName", typeof(string) },
            { "XpAmounts", typeof(int[]) },
        };

        private static Dictionary<Type, FieldTable> Tables = new Dictionary<Type, FieldTable>()
        {
            { typeof(PgAbility), new FixedFieldTable(TableAbility) },
            { typeof(PgAbilityAmmo), new FixedFieldTable(TableAbilityAmmo) },
            { typeof(PgAbilityPvX), new FixedFieldTable(TableAbilityPvX) },
            { typeof(PgAbilityRequirement), new FixedFieldTable(TableAbilityRequirement) },
            { typeof(PgDoT), new FixedFieldTable(TableDoT) },
            { typeof(PgSpecialValue), new FixedFieldTable(TableSpecialValue) },
            { typeof(PgAdvancement), new FixedFieldTable(TableAdvancement) },
            { typeof(PgAdvancementTable), new VariadicFieldTable(typeof(PgAdvancement)) },
            { typeof(PgAI), new FixedFieldTable(TableAI) },
            { typeof(PgAIAbilitySet), new VariadicFieldTable(typeof(PgAIAbility)) },
            { typeof(PgAIAbility), new FixedFieldTable(TableAIAbility) },
            { typeof(PgArea), new FixedFieldTable(TableArea) },
            { typeof(PgAttribute), new FixedFieldTable(TableAttribute) },
            { typeof(PgDirectedGoal), new FixedFieldTable(TableDirectedGoal) },
            { typeof(PgEffect), new FixedFieldTable(TableEffect) },
            { typeof(PgItem), new FixedFieldTable(TableItem) },
            { typeof(PgItemSkillLink), new VariadicFieldTable(typeof(int)) },
            { typeof(PgItemBehavior), new FixedFieldTable(TableItemBehavior) },
            { typeof(PgItemUse), new FixedFieldTable(TableItemUse) },
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
            { typeof(PgQuestRewardXp), new VariadicFieldTable(typeof(int)) },
            { typeof(PgQuestRewardCurrency), new VariadicFieldTable(typeof(int)) },
            { typeof(PgQuestRewardItem), new FixedFieldTable(TableQuestRewardItem) },
            { typeof(PgQuestReward), new FixedFieldTable(TableQuestReward) },
            { typeof(PgRecipe), new FixedFieldTable(TableRecipe) },
            { typeof(PgRecipeItem), new FixedFieldTable(TableRecipeItem) },
            { typeof(PgRecipeCost), new FixedFieldTable(TableRecipeCost) },
            { typeof(PgSkill), new FixedFieldTable(TableSkill) },
            { typeof(PgLevelCapInteraction), new VariadicFieldTable(typeof(int)) },
            { typeof(PgAdvancementHint), new VariadicFieldTable(typeof(string)) },
            { typeof(PgRewardList), new VariadicFieldTable(typeof(PgReward)) },
            { typeof(PgReward), new FixedFieldTable(TableReward) },
            { typeof(PgReport), new VariadicFieldTable(typeof(string)) },
            { typeof(PgSource), new FixedFieldTable(TableSource) },
            { typeof(PgStorageVault), new FixedFieldTable(TableStorageVault) },
            { typeof(PgStorageFavorLevel), new FixedFieldTable(TableStorageFavorLevel) },
            { typeof(PgStorageRequirement), new FixedFieldTable(TableStorageRequirement) },
            { typeof(PgXpTable), new FixedFieldTable(TableXpTable) },
        };

        private static List<Type> UsedTableList = new List<Type>();

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

        public static bool VerifyTablesCompletion()
        {
            bool Result = true;

            foreach (KeyValuePair<Type, FieldTable> Entry in Tables)
            {
                Type Key = Entry.Key;

                if (!UsedTableList.Contains(Key))
                {
                    Debug.WriteLine($"Type {Key} was not used during parsing");
                    Result = false;
                    continue;
                }

                Result &= Entry.Value.VerifyTableCompletion(Key);
            }

            return Result;
        }
    }
}
