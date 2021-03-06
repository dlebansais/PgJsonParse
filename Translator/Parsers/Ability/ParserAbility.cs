﻿namespace Translator
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using PgJsonReader;
    using PgObjects;

    public class ParserAbility : Parser
    {
        public override object CreateItem()
        {
            return new PgAbility();
        }

        public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
        {
            if (item is not PgAbility AsPgAbility)
                return Program.ReportFailure("Unexpected failure");

            AsPgAbility.Key = objectKey;

            return FinishItem(AsPgAbility, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgAbility item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
        {
            bool Result = true;

            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                switch (Key)
                {
                    case "AbilityGroup":
                        Result = Inserter<PgAbility>.SetItemByInternalName((PgAbility valueAbility) => item.AbilityGroup = valueAbility, Value);
                        break;
                    case "Animation":
                        Result = StringToEnumConversion<AbilityAnimation>.SetEnum((AbilityAnimation valueEnum) => item.Animation = valueEnum, Value);
                        break;
                    case "AttributesThatModAmmoConsumeChance":
                        Result = Inserter<PgAttribute>.AddArrayByKey(item.AttributesThatModAmmoConsumeChanceList, Value);
                        break;
                    case "AttributesThatDeltaDelayLoopTime":
                        Result = Inserter<PgAttribute>.AddArrayByKey(item.AttributesThatDeltaDelayLoopTimeList, Value);
                        break;
                    case "AttributesThatDeltaPowerCost":
                        Result = ParseCostDeltaAttribute(item, Value, parsedFile, parsedKey);
                        break;
                    case "AttributesThatDeltaResetTime":
                        Result = Inserter<PgAttribute>.AddArrayByKey(item.AttributesThatDeltaResetTimeList, Value);
                        break;
                    case "AttributesThatModPowerCost":
                        Result = Inserter<PgAttribute>.AddArrayByKey(item.AttributesThatModPowerCostList, Value);
                        break;
                    case "CanBeOnSidebar":
                        Result = SetBoolProperty((bool valueBool) => item.RawCanBeOnSidebar = valueBool, Value);
                        break;
                    case "CanSuppressMonsterShout":
                        Result = SetBoolProperty((bool valueBool) => item.RawCanSuppressMonsterShout = valueBool, Value);
                        break;
                    case "CanTargetUntargetableEnemies":
                        Result = SetBoolProperty((bool valueBool) => item.RawCanTargetUntargetableEnemies = valueBool, Value);
                        break;
                    case "CausesOfDeath":
                        Result = StringToEnumConversion<Deaths>.TryParseList(Value, item.CausesOfDeathList);
                        break;
                    case "Costs":
                        Result = ParseCosts(item, Value, parsedFile, parsedKey);
                        break;
                    case "CombatRefreshBaseAmount":
                        Result = SetIntProperty((int valueInt) => item.RawCombatRefreshBaseAmount = valueInt, Value);
                        break;
                    case "DamageType":
                        Result = StringToEnumConversion<DamageType>.SetEnum((DamageType valueEnum) => item.DamageType = valueEnum, DamageType.Internal_None, DamageType.Internal_Empty, Value);
                        break;
                    case "DelayLoopIsAbortedIfAttacked":
                        Result = SetBoolProperty((bool valueBool) => item.RawDelayLoopIsAbortedIfAttacked = valueBool, Value);
                        break;
                    case "DelayLoopMessage":
                        Result = SetStringProperty((string valueString) => item.DelayLoopMessage = valueString, Value);
                        break;
                    case "DelayLoopTime":
                        Result = SetFloatProperty((float valueFloat) => item.RawDelayLoopTime = valueFloat, Value);
                        break;
                    case "Description":
                        Result = SetStringProperty((string valueString) => item.Description = valueString, Value);
                        break;
                    case "EffectKeywordsIndicatingEnabled":
                        Result = StringToEnumConversion<AbilityIndicatingEnabled>.SetEnum((AbilityIndicatingEnabled valueEnum) => item.EffectKeywordsIndicatingEnabled = valueEnum, Value);
                        break;
                    case "ExtraKeywordsForTooltips":
                        Result = StringToEnumConversion<TooltipsExtraKeywords>.SetEnum((TooltipsExtraKeywords valueEnum) => item.ExtraKeywordsForTooltips = valueEnum, Value);
                        break;
                    case "IconID":
                        Result = SetIntProperty((int valueInt) => item.RawIconId = valueInt, Value);
                        break;
                    case "IgnoreEffectErrors":
                        Result = SetBoolProperty((bool valueBool) => item.RawIgnoreEffectErrors = valueBool, Value);
                        break;
                    case "InternalAbility":
                        Result = SetBoolProperty((bool valueBool) => item.RawInternalAbility = valueBool, Value);
                        break;
                    case "InternalName":
                        Result = SetStringProperty((string valueString) => item.InternalName = valueString, Value);
                        break;
                    case "IsHarmless":
                        Result = SetBoolProperty((bool valueBool) => item.RawIsHarmless = valueBool, Value);
                        break;
                    case "ItemKeywordReqErrorMessage":
                        Result = SetStringProperty((string valueString) => item.ItemKeywordReqErrorMessage = valueString, Value);
                        break;
                    case "ItemKeywordReqs":
                        Result = StringToEnumConversion<AbilityItemKeyword>.TryParseList(Value, item.ItemKeywordReqList);
                        break;
                    case "Keywords":
                        Result = StringToEnumConversion<AbilityKeyword>.TryParseList(Value, item.KeywordList);
                        break;
                    case "Level":
                        Result = SetIntProperty((int valueInt) => item.RawLevel = valueInt, Value);
                        break;
                    case "Name":
                        Result = SetStringProperty((string valueString) => item.Name = valueString, Value);
                        break;
                    case "PetTypeTagReq":
                        Result = StringToEnumConversion<AbilityPetType>.SetEnum((AbilityPetType valueEnum) => item.PetTypeTagReq = valueEnum, Value);
                        break;
                    case "PetTypeTagReqMax":
                        Result = SetIntProperty((int valueInt) => item.RawPetTypeTagReqMax = valueInt, Value);
                        break;
                    case "Prerequisite":
                        Result = Inserter<PgAbility>.SetItemByInternalName((PgAbility valueAbility) => item.Prerequisite = valueAbility, Value);
                        break;
                    case "Projectile":
                        Result = StringToEnumConversion<AbilityProjectile>.SetEnum((AbilityProjectile valueEnum) => item.Projectile = valueEnum, Value);
                        break;
                    case "PvE":
                        Result = Inserter<PgAbilityPvX>.SetItemProperty((PgAbilityPvX valueAbilityPvX) => item.PvE = valueAbilityPvX, Value);
                        break;
                    case "PvP":
                        Result = Inserter<PgAbilityPvX>.SetItemProperty((PgAbilityPvX valueAbilityPvX) => item.PvP = valueAbilityPvX, Value);
                        break;
                    case "ResetTime":
                        Result = SetFloatProperty((float valueFloat) => item.RawResetTime = valueFloat, Value);
                        break;
                    case "SelfParticle":
                        Result = StringToEnumConversion<SelfParticle>.SetEnum((SelfParticle valueEnum) => item.SelfParticle = valueEnum, Value);
                        break;
                    case "AmmoDescription":
                        Result = SetStringProperty((string valueString) => item.AmmoDescription = valueString, Value);
                        break;
                    case "SharesResetTimerWith":
                        Result = Inserter<PgAbility>.SetItemByInternalName((PgAbility valueAbility) => item.SharesResetTimerWith = valueAbility, Value);
                        break;
                    case "Skill":
                        Result = ParserSkill.Parse((PgSkill valueSkill) => item.Skill = valueSkill, Value, parsedFile, parsedKey);
                        break;
                    case "SpecialCasterRequirements":
                        Result = Inserter<PgAbilityRequirement>.AddKeylessArray(item.SpecialCasterRequirementList, Value);
                        break;
                    case "SpecialCasterRequirementsErrorMessage":
                        Result = SetStringProperty((string valueString) => item.SpecialCasterRequirementsErrorMessage = valueString, Value);
                        break;
                    case "SpecialInfo":
                        Result = SetStringProperty((string valueString) => item.SpecialInfo = valueString, Value);
                        break;
                    case "SpecialTargetingTypeReq":
                        Result = SetIntProperty((int valueInt) => item.RawSpecialTargetingTypeReq = valueInt, Value);
                        break;
                    case "Target":
                        Result = StringToEnumConversion<AbilityTarget>.SetEnum((AbilityTarget valueEnum) => item.Target = valueEnum, Value);
                        break;
                    case "TargetEffectKeywordReq":
                        Result = StringToEnumConversion<TargetEffectKeyword>.SetEnum((TargetEffectKeyword valueEnum) => item.TargetEffectKeywordReq = valueEnum, Value);
                        break;
                    case "TargetParticle":
                        Result = StringToEnumConversion<AbilityTargetParticle>.SetEnum((AbilityTargetParticle valueEnum) => item.TargetParticle = valueEnum, Value);
                        break;
                    case "UpgradeOf":
                        Result = Inserter<PgAbility>.SetItemByInternalName((PgAbility valueAbility) => item.UpgradeOf = valueAbility, Value);
                        break;
                    case "WorksInCombat":
                        Result = SetBoolProperty((bool valueBool) => item.RawWorksInCombat = valueBool, Value);
                        break;
                    case "WorksUnderwater":
                        Result = SetBoolProperty((bool valueBool) => item.RawWorksUnderwater = valueBool, Value);
                        break;
                    case "WorksWhileFalling":
                        Result = SetBoolProperty((bool valueBool) => item.RawWorksWhileFalling = valueBool, Value);
                        break;
                    case "DelayLoopIsOnlyUsedInCombat":
                        Result = SetBoolProperty((bool valueBool) => item.RawDelayLoopIsOnlyUsedInCombat = valueBool, Value);
                        break;
                    case "AmmoKeywords":
                        Result = Inserter<PgAbilityAmmo>.AddKeylessArray(item.AmmoKeywordList, Value);
                        break;
                    case "AmmoConsumeChance":
                        Result = SetFloatProperty((float valueFloat) => item.RawAmmoConsumeChance = valueFloat, Value);
                        break;
                    case "AmmoStickChance":
                        Result = SetFloatProperty((float valueFloat) => item.RawAmmoStickChance = valueFloat, Value);
                        break;
                    default:
                        Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}' not handled");
                        break;
                }

                if (!Result)
                    break;
            }

            if (Result)
            {
                if (item.PvE == null)
                    return Program.ReportFailure(parsedFile, parsedKey, $"PvE info missing");

                item.DigitStrippedName = CuteDigitStrippedName(item);
            }

            return Result;
        }

        private bool ParseCostDeltaAttribute(PgAbility item, object value, string parsedFile, string parsedKey)
        {
            if (!(value is List<object> ArrayKey))
                return Program.ReportFailure($"Value '{value}' was expected to be a list");

            List<object> ValueCopy = new List<object>();
            foreach (object Item in ArrayKey)
            {
                if (!(Item is string ValueKey))
                    return Program.ReportFailure($"Value '{Item}' was expected to be a string");

                if (ValueKey != "COCKATRICEDEBUFF_COST_DELTA" && ValueKey != "LAMIADEBUFF_COST_DELTA")
                    ValueCopy.Add(ValueKey);
                else
                    ValueCopy.Add(ValueKey);
            }

            bool Result = Inserter<PgAttribute>.AddArrayByKey(item.AttributesThatDeltaPowerCostList, ValueCopy);
            return Result;
        }

        private bool ParseCosts(PgAbility item, object value, string parsedFile, string parsedKey)
        {
            PgRecipeCostCollection CostList = new PgRecipeCostCollection();
            if (!Inserter<PgRecipeCost>.AddKeylessArray(CostList, value))
                return false;

            if (CostList.Count > 1)
                return Program.ReportFailure(parsedFile, parsedKey, $"Only one cost expected");
            else if (CostList.Count == 1)
                item.Cost = CostList[0];

            return true;
        }

        public static void UpdateIconsAndNames()
        {
            Dictionary<string, ParsingContext> AbilityParsingTable = ParsingContext.ObjectKeyTable[typeof(PgAbility)];
            foreach (KeyValuePair<string, ParsingContext> Entry in AbilityParsingTable)
            {
                PgAbility Ability = (PgAbility)Entry.Value.Item;

                if (Ability.IconId > 0)
                    Ability.FriendlyIconId = Ability.IconId;
                else
                    Ability.FriendlyIconId = PgObject.AbilityIconId;

                Debug.Assert(Ability.ObjectIconId != 0);
                Debug.Assert(Ability.ObjectName.Length > 0);
            }
        }

        public static bool UpdateSource()
        {
            Dictionary<string, ParsingContext> SourceParsingTable = ParsingContext.ObjectKeyTable[typeof(PgSource)];
            Dictionary<string, ParsingContext> AbilityParsingTable = ParsingContext.ObjectKeyTable[typeof(PgAbility)];

            foreach (KeyValuePair<string, ParsingContext> Entry in SourceParsingTable)
            {
                PgSource AbilitySource = (PgSource)Entry.Value.Item;
                string Key = AbilitySource.SourceKey;

                if (Key.StartsWith("ability_"))
                {
                    if (!AbilityParsingTable.ContainsKey(Key))
                        return Program.ReportFailure($"Source for '{Key}' but no such object");

                    PgAbility Ability = (PgAbility)AbilityParsingTable[Key].Item;
                    Ability.SourceList.Add(AbilitySource);
                }
            }

            return true;
        }

        private static string CuteDigitStrippedName(PgAbility ability)
        {
            string DigitStrippedName = GetDigitStrippedName(ability);
            int Index;

            Index = 0;
            while (Index < DigitStrippedName.Length)
                if (char.IsDigit(DigitStrippedName[Index]))
                    DigitStrippedName = DigitStrippedName.Substring(0, Index) + DigitStrippedName.Substring(Index + 1);
                else
                    Index++;

            if (IdenticalNameTable.ContainsKey(DigitStrippedName))
                DigitStrippedName = IdenticalNameTable[DigitStrippedName];

            Index = 0;
            while (Index < DigitStrippedName.Length)
            {
                if (char.IsUpper(DigitStrippedName[Index]) && Index > 0)
                {
                    DigitStrippedName = DigitStrippedName.Substring(0, Index) + " " + DigitStrippedName.Substring(Index);
                    Index++;
                }

                Index++;
            }

            return DigitStrippedName;
        }

        private static string GetDigitStrippedName(PgAbility ability)
        {
            string DigitStrippedName = ability.InternalName;
            string LineIndexString = string.Empty;

            while (DigitStrippedName.Length > 0 && char.IsDigit(DigitStrippedName[DigitStrippedName.Length - 1]))
            {
                LineIndexString = DigitStrippedName.Substring(DigitStrippedName.Length - 1) + LineIndexString;
                DigitStrippedName = DigitStrippedName.Substring(0, DigitStrippedName.Length - 1);
            }

            return DigitStrippedName;
        }

        private static readonly Dictionary<string, string> IdenticalNameTable = new Dictionary<string, string>()
        {
            { "StabledPetLiving", "StabledPet" },
            { "TameRat", "TameRat" },
            { "TameCat", "TameRat" },
            { "TameBear", "TameRat" },
            { "TameBee", "TameRat" },
            { "BasicShotB", "BasicShot" },
            { "AimedShotB", "AimedShot" },
            { "BlitzShotB", "BlitzShot" },
            { "ToxinBombB", "MycotoxinFormula" },
            { "ToxinBombC", "AcidBomb" },
            { "FireWallB", "FireWall" },
            { "IceVeinsB", "IceVeins" },
            { "SliceB", "DuelistsSlash" },
            { "WerewolfPounceB", "PouncingRend" },
            { "WerewolfPounceBB", "PouncingRend" },
        };
    }
}
