namespace Translator
{
    using PgJsonObjects;
    using PgJsonReader;
    using System.Collections.Generic;

    public class ParserAdvancement : Parser
    {
        public override object CreateItem()
        {
            return new PgAdvancement();
        }

        public override bool FinishItem(ref object item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            if (!(item is PgAdvancement AsPgAdvancement))
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgAdvancement, contentTable, ContentTypeTable, itemCollection, LastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgAdvancement item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            bool Result = true;

            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                switch (Key)
                {
                    case "VULN_CRUSHING":
                        Result = ParseVulnerability(item, DamageType.Crushing, Value);
                        break;
                    case "VULN_SLASHING":
                        Result = ParseVulnerability(item, DamageType.Slashing, Value);
                        break;
                    case "VULN_NATURE":
                        Result = ParseVulnerability(item, DamageType.Nature, Value);
                        break;
                    case "VULN_FIRE":
                        Result = ParseVulnerability(item, DamageType.Fire, Value);
                        break;
                    case "VULN_COLD":
                        Result = ParseVulnerability(item, DamageType.Cold, Value);
                        break;
                    case "VULN_PIERCING":
                        Result = ParseVulnerability(item, DamageType.Piercing, Value);
                        break;
                    case "VULN_PSYCHIC":
                        Result = ParseVulnerability(item, DamageType.Psychic, Value);
                        break;
                    case "VULN_TRAUMA":
                        Result = ParseVulnerability(item, DamageType.Trauma, Value);
                        break;
                    case "VULN_ELECTRICITY":
                        Result = ParseVulnerability(item, DamageType.Electricity, Value);
                        break;
                    case "VULN_POISON":
                        Result = ParseVulnerability(item, DamageType.Poison, Value);
                        break;
                    case "VULN_ACID":
                        Result = ParseVulnerability(item, DamageType.Acid, Value);
                        break;
                    case "VULN_DARKNESS":
                        Result = ParseVulnerability(item, DamageType.Darkness, Value);
                        break;
                    case "VULN_BURST":
                        Result = SetFloatProperty((float valueFloat) => item.RawVulnerabilityToBurst = valueFloat, Value);
                        break;
                    case "MITIGATION_CRUSHING":
                        Result = ParseMitigation(item, DamageType.Crushing, Value);
                        break;
                    case "MITIGATION_SLASHING":
                        Result = ParseMitigation(item, DamageType.Slashing, Value);
                        break;
                    case "MITIGATION_NATURE":
                        Result = ParseMitigation(item, DamageType.Nature, Value);
                        break;
                    case "MITIGATION_FIRE":
                        Result = ParseMitigation(item, DamageType.Fire, Value);
                        break;
                    case "MITIGATION_PIERCING":
                        Result = ParseMitigation(item, DamageType.Piercing, Value);
                        break;
                    case "MITIGATION_TRAUMA_DIRECT":
                        Result = ParseMitigationDirect(item, DamageType.Trauma, Value);
                        break;
                    case "MITIGATION_TRAUMA_INDIRECT":
                        Result = ParseMitigationIndirect(item, DamageType.Trauma, Value);
                        break;
                    case "MITIGATION_POISON":
                        Result = ParseMitigation(item, DamageType.Poison, Value);
                        break;
                    case "MITIGATION_POISON_DIRECT":
                        Result = ParseMitigationDirect(item, DamageType.Poison, Value);
                        break;
                    case "MITIGATION_POISON_INDIRECT":
                        Result = ParseMitigationIndirect(item, DamageType.Poison, Value);
                        break;
                    case "MOD_FIRE_DIRECT":
                        Result = ParseModDirect(item, DamageType.Fire, Value);
                        break;
                    case "MOD_ELECTRICITY_DIRECT":
                        Result = ParseModDirect(item, DamageType.Electricity, Value);
                        break;
                    case "MOD_DARKNESS_DIRECT":
                        Result = ParseModDirect(item, DamageType.Darkness, Value);
                        break;
                    case "MOD_FIRE_INDIRECT":
                        Result = ParseModIndirect(item, DamageType.Fire, Value);
                        break;
                    case "IGNORE_CHANCE_FEAR":
                        Result = SetFloatProperty((float valueFloat) => item.RawIgnoreChanceFear = valueFloat, Value);
                        break;
                    case "IGNORE_CHANCE_MEZ":
                        Result = SetFloatProperty((float valueFloat) => item.RawIgnoreChanceMez = valueFloat, Value);
                        break;
                    case "IGNORE_CHANCE_KNOCKBACK":
                        Result = SetFloatProperty((float valueFloat) => item.RawIgnoreChanceKnockback = valueFloat, Value);
                        break;
                    case "MENTAL_DEFENSE_RATING":
                        Result = SetFloatProperty((float valueFloat) => item.RawMentalDefenseRating = valueFloat, Value);
                        break;
                    case "NONCOMBAT_REGEN_HEALTH_MOD":
                        Result = SetFloatProperty((float valueFloat) => item.RawNonCombatRegenHealthMod = valueFloat, Value);
                        break;
                    case "COMBAT_REGEN_HEALTH_MOD":
                        Result = SetFloatProperty((float valueFloat) => item.RawCombatRegenHealthMod = valueFloat, Value);
                        break;
                    case "COMBAT_REGEN_HEALTH_DELTA":
                        Result = SetFloatProperty((float valueFloat) => item.RawCombatRegenHealthDelta = valueFloat, Value);
                        break;
                    case "NONCOMBAT_REGEN_ARMOR_MOD":
                        Result = SetFloatProperty((float valueFloat) => item.RawNonCombatRegenArmorMod = valueFloat, Value);
                        break;
                    case "NONCOMBAT_REGEN_ARMOR_DELTA":
                        Result = SetFloatProperty((float valueFloat) => item.RawNonCombatRegenArmorDelta = valueFloat, Value);
                        break;
                    case "COMBAT_REGEN_ARMOR_MOD":
                        Result = SetFloatProperty((float valueFloat) => item.RawCombatRegenArmorMod = valueFloat, Value);
                        break;
                    case "NONCOMBAT_REGEN_POWER_MOD":
                        Result = SetFloatProperty((float valueFloat) => item.RawNonCombatRegenPowerMod = valueFloat, Value);
                        break;
                    case "COMBAT_REGEN_POWER_MOD":
                        Result = SetFloatProperty((float valueFloat) => item.RawCombatRegenPowerMod = valueFloat, Value);
                        break;
                    case "SPRINT_BOOST":
                        Result = SetFloatProperty((float valueFloat) => item.RawSprintBoost = valueFloat, Value);
                        break;
                    case "TAUNT_MOD":
                        Result = SetFloatProperty((float valueFloat) => item.RawTauntMod = valueFloat, Value);
                        break;
                    case "EVASION_CHANCE":
                        Result = SetFloatProperty((float valueFloat) => item.RawEvasionChance = valueFloat, Value);
                        break;
                    case "EVASION_CHANCE_PROJECTILE":
                        Result = SetFloatProperty((float valueFloat) => item.RawEvasionChanceProjectile = valueFloat, Value);
                        break;
                    case "EVASION_CHANCE_MELEE":
                        Result = SetFloatProperty((float valueFloat) => item.RawEvasionChanceMelee = valueFloat, Value);
                        break;
                    case "MOD_CRITICAL_HIT_DAMAGE_RAGEATTACK":
                        Result = SetFloatProperty((float valueFloat) => item.RawModCriticalHitDamageRageAttack = valueFloat, Value);
                        break;
                    case "BOOST_WEREWOLFMETABOLISM_HEALTHREGEN":
                        Result = SetFloatProperty((float valueFloat) => item.RawBoostWerewolfMetabolismHeathRegen = valueFloat, Value);
                        break;
                    case "BOOST_WEREWOLFMETABOLISM_POWERREGEN":
                        Result = SetFloatProperty((float valueFloat) => item.RawBoostWerewolfMetabolismPowerRegen = valueFloat, Value);
                        break;
                    case "LOOT_BOOST_CHANCE_UNCOMMON":
                        Result = SetFloatProperty((float valueFloat) => item.RawLootBoostChanceUncommon = valueFloat, Value);
                        break;
                    case "LOOT_BOOST_CHANCE_RARE":
                        Result = SetFloatProperty((float valueFloat) => item.RawLootBoostChanceRare = valueFloat, Value);
                        break;
                    case "LOOT_BOOST_CHANCE_EXCEPTIONAL":
                        Result = SetFloatProperty((float valueFloat) => item.RawLootBoostChanceExceptional = valueFloat, Value);
                        break;
                    case "LOOT_BOOST_CHANCE_EPIC":
                        Result = SetFloatProperty((float valueFloat) => item.RawLootBoostChanceEpic = valueFloat, Value);
                        break;
                    case "LOOT_BOOST_CHANCE_LEGENDARY":
                        Result = SetFloatProperty((float valueFloat) => item.RawLootBoostChanceLegendary = valueFloat, Value);
                        break;
                    case "MAX_HEALTH":
                        Result = SetFloatProperty((float valueFloat) => item.RawMaxHealth = valueFloat, Value);
                        break;
                    case "MAX_ARMOR":
                        Result = SetFloatProperty((float valueFloat) => item.RawMaxArmor = valueFloat, Value);
                        break;
                    case "MAX_RAGE":
                        Result = SetFloatProperty((float valueFloat) => item.RawMaxRage = valueFloat, Value);
                        break;
                    case "MAX_POWER":
                        Result = SetFloatProperty((float valueFloat) => item.RawMaxPower = valueFloat, Value);
                        break;
                    case "MAX_BREATH":
                        Result = SetFloatProperty((float valueFloat) => item.RawMaxBreath = valueFloat, Value);
                        break;
                    case "BOOST_UNIVERSAL_DIRECT":
                        Result = SetFloatProperty((float valueFloat) => item.RawBoostUniversalDirect = valueFloat, Value);
                        break;
                    case "BOOST_ABILITY_RAGEATTACK":
                        Result = SetFloatProperty((float valueFloat) => item.RawBoostAbilityRageAttack = valueFloat, Value);
                        break;
                    case "MOD_ABILITY_RAGEATTACK":
                        Result = SetFloatProperty((float valueFloat) => item.RawModAbilityRageAttack = valueFloat, Value);
                        break;
                    case "MONSTER_COMBAT_XP_VALUE":
                        Result = SetFloatProperty((float valueFloat) => item.RawMonsterCombatXpValue = valueFloat, Value);
                        break;
                    case "COMBAT_REGEN_ARMOR_DELTA":
                        Result = SetFloatProperty((float valueFloat) => item.RawCombatRegenArmorDelta = valueFloat, Value);
                        break;
                    case "COMBAT_REGEN_POWER_DELTA":
                        Result = SetFloatProperty((float valueFloat) => item.RawCombatRegenPowerDelta = valueFloat, Value);
                        break;
                    case "MAX_INVENTORY_SIZE":
                        Result = SetFloatProperty((float valueFloat) => item.RawMaxInventorySize = valueFloat, Value);
                        break;
                    case "MAX_METABOLISM":
                        Result = SetFloatProperty((float valueFloat) => item.RawMaxMetabolism = valueFloat, Value);
                        break;
                    case "NPC_MOD_FAVORFROMGIFTS":
                        Result = SetFloatProperty((float valueFloat) => item.RawNpcModFavorFromGifts = valueFloat, Value);
                        break;
                    case "NPC_MOD_FAVORFROMHANGOUTS":
                        Result = SetFloatProperty((float valueFloat) => item.RawNpcModFavorFromHangouts = valueFloat, Value);
                        break;
                    case "NPC_MOD_MAXSALESVALUE":
                        Result = SetFloatProperty((float valueFloat) => item.RawNpcModMaxSalesValue = valueFloat, Value);
                        break;
                    case "NPC_MOD_TRAININGCOST":
                        Result = SetFloatProperty((float valueFloat) => item.RawNpcModTrainingCost = valueFloat, Value);
                        break;
                    case "NUM_INVENTORY_FOLDERS":
                        Result = SetIntProperty((int valueInt) => item.RawNumInventoryFolders = valueInt, Value);
                        break;
                    case "HIGH_CLEANLINESS_XP_EARNED_MOD":
                        Result = SetFloatProperty((float valueFloat) => item.RawHighCleanlinessXpEarnedMod = valueFloat, Value);
                        break;
                    case "LOW_CLEANLINESS_XP_EARNED_MOD":
                        Result = SetFloatProperty((float valueFloat) => item.RawLowCleanlinessXpEarnedMod = valueFloat, Value);
                        break;
                    case "SHOW_CLEANLINESS_INDICATORS":
                        Result = SetIntProperty((int valueInt) => item.RawShowCleanlinessIndicators = valueInt, Value);
                        break;
                    case "HIGH_COMMUNITY_XP_EARNED_MOD":
                        Result = SetFloatProperty((float valueFloat) => item.RawHighCommunityXpEarnedMod = valueFloat, Value);
                        break;
                    case "LOW_COMMUNITY_XP_EARNED_MOD":
                        Result = SetFloatProperty((float valueFloat) => item.RawLowCommunityXpEarnedMod = valueFloat, Value);
                        break;
                    case "SHOW_COMMUNITY_INDICATORS":
                        Result = SetIntProperty((int valueInt) => item.RawShowCommunityIndicators = valueInt, Value);
                        break;
                    case "HIGH_PEACEABLENESS_XP_EARNED_MOD":
                        Result = SetFloatProperty((float valueFloat) => item.RawHighPeaceablenessXpEarnedMod = valueFloat, Value);
                        break;
                    case "LOW_PEACEABLENESS_XP_EARNED_MOD":
                        Result = SetFloatProperty((float valueFloat) => item.RawLowPeaceablenessXpEarnedMod = valueFloat, Value);
                        break;
                    case "SHOW_PEACEABLENESS_INDICATORS":
                        Result = SetIntProperty((int valueInt) => item.RawShowPeaceablenessIndicators = valueInt, Value);
                        break;
                    case "STAFF_ARMOR_AUTOHEAL":
                        Result = SetFloatProperty((float valueFloat) => item.RawStaffArmorAutoHeal = valueFloat, Value);
                        break;
                    case "MAX_MAP_PINS_PER_AREA":
                        Result = SetIntProperty((int valueInt) => item.RawMaxMapPinsPerArea = valueInt, Value);
                        break;
                    case "MAX_MAP_PIN_ICONS":
                        Result = SetIntProperty((int valueInt) => item.RawMaxMapPinIcons = valueInt, Value);
                        break;
                    case "WORKORDER_COIN_REWARD_MOD":
                        Result = SetFloatProperty((float valueFloat) => item.RawWorkOrderCoinRewardMod = valueFloat, Value);
                        break;
                    case "MAX_ACTIVE_WORKORDERS":
                        Result = SetIntProperty((int valueInt) => item.RawMaxActiveWorkOrders = valueInt, Value);
                        break;
                    case "PLAYER_ORDERS_MAX_ACTIVE":
                        Result = SetIntProperty((int valueInt) => item.RawPlayerOrdersMaxActive = valueInt, Value);
                        break;
                    case "SHOP_INVENTORY_SIZE_DELTA":
                        Result = SetIntProperty((int valueInt) => item.RawShopInventorySizeDelta = valueInt, Value);
                        break;
                    case "MAIL_SHOP_NUMFREE":
                        Result = SetIntProperty((int valueInt) => item.RawMailShopNumFree = valueInt, Value);
                        break;
                    case "SHOP_HIRING_MAX_PREPAY_DAYS":
                        Result = SetIntProperty((int valueInt) => item.RawShopHiringMaxPrepDays = valueInt, Value);
                        break;
                    case "SHOP_LOG_DAYSKEPT":
                        Result = SetIntProperty((int valueInt) => item.RawShopLogDaysKept = valueInt, Value);
                        break;
                    case "SHOP_HIRING_NUMFREE":
                        Result = SetIntProperty((int valueInt) => item.RawShopHiringNumFree = valueInt, Value);
                        break;
                    case "MOD_CRITICAL_HIT_DAMAGE":
                        Result = SetFloatProperty((float valueFloat) => item.RawModCriticalHitDamage = valueFloat, Value);
                        break;
                    case "MONSTER_CRIT_CHANCE":
                        Result = SetFloatProperty((float valueFloat) => item.RawMonsterCritChance = valueFloat, Value);
                        break;
                    case "ACCURACY_BOOST":
                        Result = SetFloatProperty((float valueFloat) => item.RawAccuracyBoost = valueFloat, Value);
                        break;
                    case "BOOST_POISON_INDIRECT":
                        Result = ParseBoostIndirect(item, DamageType.Poison, Value);
                        break;
                    case "MONSTER_MATCH_OWNER_SPEED":
                        Result = SetIntProperty((int valueInt) => item.RawMonsterMatchOwnerSpeed = valueInt, Value);
                        break;
                    case "ARMOR_MITIGATION_MOD":
                        Result = SetFloatProperty((float valueFloat) => item.RawArmorMitigationMod = valueFloat, Value);
                        break;
                    case "AUTOHEAL_HEALTH_MOD":
                        Result = SetFloatProperty((float valueFloat) => item.RawAutoHealHealthMod = valueFloat, Value);
                        break;
                    case "AUTOHEAL_ARMOR_MOD":
                        Result = SetFloatProperty((float valueFloat) => item.RawAutoHealArmorMod = valueFloat, Value);
                        break;
                    case "ARMOR_MITIGATION_RATIO":
                        Result = SetFloatProperty((float valueFloat) => item.RawArmorMitigationRatio = valueFloat, Value);
                        break;
                    case "SHOW_FAIRYENERGY_INDICATORS":
                        Result = SetIntProperty((int valueInt) => item.RawShowFairyEnergyIndicator = valueInt, Value);
                        break;
                    case "BOOST_ABILITY_PET_SPECIALATTACK":
                        Result = SetIntProperty((int valueInt) => item.RawBoostAbilityPetSpecialAttack = valueInt, Value);
                        break;
                    case "BOOST_ABILITY_PET_SPECIALTRICK":
                        Result = SetIntProperty((int valueInt) => item.RawBoostAbilityPetSpecialTrick = valueInt, Value);
                        break;
                    case "BOOST_ABILITY_PET_BASICATTACK":
                        Result = SetIntProperty((int valueInt) => item.RawBoostAbilityPetBasicAttack = valueInt, Value);
                        break;
                    case "BOOST_AUTOHEAL_HEALTH_SENDER":
                        Result = SetFloatProperty((float valueFloat) => item.RawBoostAutoHealHealthSender = valueFloat, Value);
                        break;
                    case "BOOST_AUTOHEAL_ARMOR_SENDER":
                        Result = SetFloatProperty((float valueFloat) => item.RawBoostAutoHealArmorSender = valueFloat, Value);
                        break;
                    case "BOOST_TRAUMA_INDIRECT":
                        Result = ParseBoostIndirect(item, DamageType.Trauma, Value);
                        break;
                    default:
                        Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}'not handled");
                        break;
                }

                if (!Result)
                    break;
            }

            return Result;
        }

        private bool ParseVulnerability(PgAdvancement item, DamageType damageType, object value)
        {
            return ParseDamageTypeEffect(item.VulnerabilityTable, damageType, value);
        }

        private bool ParseMitigation(PgAdvancement item, DamageType damageType, object value)
        {
            return ParseDamageTypeEffect(item.MitigationTable, damageType, value);
        }

        private bool ParseMitigationDirect(PgAdvancement item, DamageType damageType, object value)
        {
            return ParseDamageTypeEffect(item.MitigationDirectTable, damageType, value);
        }

        private bool ParseMitigationIndirect(PgAdvancement item, DamageType damageType, object value)
        {
            return ParseDamageTypeEffect(item.MitigationIndirectTable, damageType, value);
        }

        private bool ParseModDirect(PgAdvancement item, DamageType damageType, object value)
        {
            return ParseDamageTypeEffect(item.ModDirectTable, damageType, value);
        }

        private bool ParseModIndirect(PgAdvancement item, DamageType damageType, object value)
        {
            return ParseDamageTypeEffect(item.ModIndirectTable, damageType, value);
        }

        private bool ParseBoostIndirect(PgAdvancement item, DamageType damageType, object value)
        {
            return ParseDamageTypeEffect(item.BoostIndirectTable, damageType, value);
        }

        private bool ParseDamageTypeEffect(Dictionary<DamageType, float> effectTable, DamageType damageType, object value)
        {
            float VulnerabilityValue;

            if (value is float ValueFloat)
                VulnerabilityValue = ValueFloat;
            else if (value is int ValueInt)
                VulnerabilityValue = ValueInt;
            else
                return Program.ReportFailure($"Value {value} was expected to be a float");

            if (effectTable.ContainsKey(damageType))
                return Program.ReportFailure($"Effect of {damageType} already added");

            StringToEnumConversion<DamageType>.SetCustomParsedEnum(damageType);

            effectTable.Add(damageType, VulnerabilityValue);
            return true;
        }
    }
}
