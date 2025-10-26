namespace TranslatorCombatParserEx;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using PgObjects;
using Translator;

internal partial class CombatParserEx
{
    private void AnalyzeMatchingPowersAndEffects(List<string[]> stringKeyTable, List<PgModEffectCollectionEx> powerKeyToCompleteEffectTable)
    {
        foreach (KeyValuePair<PgPower, List<PgEffect>> Entry in powerToEffectTable)
        {
            PgPower ItemPower = Entry.Key;
            List<PgEffect> ItemEffectList = Entry.Value;

            if (AnalyzeMatchingPowersAndEffects(ItemPower, ItemEffectList, out string[] stringKeyArray, out PgModEffectCollectionEx ModEffectArray, out PgCombatModCollectionEx pgCombatModCollectionEx))
            {
                stringKeyTable.Add(stringKeyArray);
                powerKeyToCompleteEffectTable.Add(ModEffectArray);

                foreach (PgCombatModEx pgCombatModEx in pgCombatModCollectionEx)
                {
                    foreach (PgPermanentModEffectEx pgPermanentModEffectEx in pgCombatModEx.PermanentEffects)
                        StringToEnumConversion<CombatKeywordEx>.SetCustomParsedEnum(pgPermanentModEffectEx.Keyword);
                    foreach (PgCombatModEffectEx pgCombatModEffectEx in pgCombatModEx.DynamicEffects)
                        StringToEnumConversion<CombatKeywordEx>.SetCustomParsedEnum(pgCombatModEffectEx.Keyword);
                }

                if (!KnownCombatPowers.KnownMatchingPowers.ContainsKey(ItemPower.Key))
                {
                    pgCombatModCollectionEx.Display(ItemPower.Key);
                }
            }
        }
    }

    private bool AnalyzeMatchingPowersAndEffects(PgPower itemPower, List<PgEffect> itemEffectList, out string[] stringKeyArray, out PgModEffectCollectionEx modEffectArray, out PgCombatModCollectionEx pgCombatModCollectionEx)
    {
        PgPowerTierCollection TierList = itemPower.TierList;
        Debug.Assert(itemPower.Skill_Key is not null);
        string skill_Key = itemPower.Skill_Key!;

        Debug.Assert(TierList.Count == itemEffectList.Count);
        Debug.Assert(TierList.Count > 0);

        int ValidationIndex = 0;
        if (TierList.Count >= 2)
            ValidationIndex = 1;
        if (TierList.Count >= 4)
            ValidationIndex = 2;
        if (TierList.Count >= 8)
            ValidationIndex = TierList.Count - 2;

        List<CombatKeywordEx>[] PowerTierKeywordListArray = new List<CombatKeywordEx>[TierList.Count];
        List<CombatKeywordEx>[] EffectKeywordListArray = new List<CombatKeywordEx>[TierList.Count];
        stringKeyArray = new string[TierList.Count];
        modEffectArray = new PgModEffectCollectionEx(skill_Key, TierList.Count);
        pgCombatModCollectionEx = new();

        int LastTierIndex = TierList.Count - 1;
        PgPowerTier LastTier = TierList[LastTierIndex];

        if (itemPower.Key == "25073")
        {
        }

        if (!AnalyzeMatchingPowersAndEffects(LastTier, itemEffectList[LastTierIndex], skill_Key, itemPower.Key, out List<CombatKeywordEx> ExtractedPowerTierKeywordList, out List<CombatKeywordEx> ExtractedEffectKeywordList, out PgModEffectEx ExtractedModEffect, out PgCombatModEx pgCombatModEx, true))
            return false;

        PowerTierKeywordListArray[LastTierIndex] = ExtractedPowerTierKeywordList;
        EffectKeywordListArray[LastTierIndex] = ExtractedEffectKeywordList;
        modEffectArray.Items[LastTierIndex] = ExtractedModEffect;
        pgCombatModCollectionEx.Insert(0, pgCombatModEx);

        for (int i = 0; i + 1 < TierList.Count; i++)
        {
            if (!AnalyzeMatchingPowersAndEffects(TierList[i], itemEffectList[i], skill_Key, itemPower.Key, out List<CombatKeywordEx> ComparedPowerTierKeywordList, out List<CombatKeywordEx> ComparedEffectKeywordList, out PgModEffectEx ParsedModEffect, out pgCombatModEx, false))
                return false;

            bool AllowIncomplete = i < ValidationIndex;

            if (!IsSameCombatKeywordList(ExtractedPowerTierKeywordList, ComparedPowerTierKeywordList, AllowIncomplete))
            {
                Debug.WriteLine($"Mismatching power at tier #{i}");
                return false;
            }

            if (!IsSameCombatKeywordList(ExtractedEffectKeywordList, ComparedEffectKeywordList, AllowIncomplete))
            {
                Debug.WriteLine($"Mismatching effect at tier #{i}");
                return false;
            }

            PowerTierKeywordListArray[i] = ComparedPowerTierKeywordList;
            EffectKeywordListArray[i] = ComparedEffectKeywordList;
            modEffectArray.Items[i] = ParsedModEffect;
            pgCombatModCollectionEx.Insert(i, pgCombatModEx);
        }

        for (int i = 0; i < TierList.Count; i++)
        {
            string Key = PowerEffectPairKey(itemPower, i);
            stringKeyArray[i] = Key;
        }

        return true;
    }

    private bool IsSameCombatKeywordList(List<CombatKeywordEx> list1, List<CombatKeywordEx> list2, bool allowIncomplete)
    {
        if ((!allowIncomplete && list1.Count != list2.Count) || (allowIncomplete && list1.Count < list2.Count))
            return false;

        Debug.Assert(list1.Count >= list2.Count);

        List<CombatKeywordEx> MissingKeywordList = new List<CombatKeywordEx>();
        for (int i = 0; i < list2.Count; i++)
            if (!list1.Contains(list2[i]))
                MissingKeywordList.Add(list2[i]);

        if (MissingKeywordList.Count == 0)
            return true;

        return false;
    }

    public static string PowerEffectPairKey(PgPower power, int tierIndex)
    {
        string PowerTierString = (tierIndex + 1).ToString("D03");
        string Key = $"{power.Key}_{PowerTierString}";

        return Key;
    }

    private bool AnalyzeMatchingPowersAndEffects(PgPowerTier powerTier, PgEffect effect, string skill_Key, string powerKey, out List<CombatKeywordEx> extractedPowerTierKeywordList, out List<CombatKeywordEx> extractedEffectKeywordList, out PgModEffectEx modEffect, out PgCombatModEx pgCombatModEx, bool displayAnalysisResult)
    {
        IList<PgPowerEffect> EffectList = powerTier.EffectList;
        Debug.Assert(EffectList.Count == 1);
        PgPowerEffectSimple powerSimpleEffect = (PgPowerEffectSimple)EffectList[0];

        string ModText = powerSimpleEffect.Description;
        /*
        if (ModText.Contains("Necromancy Base Damage") && ModText.Contains("Summoned Skeletons deal"))
        {
            string[] Split = ModText.Split(new string[] { ";" }, StringSplitOptions.None);
            PgEffect NoEffect = new();
            bool Result = true;
            Result &= AnalyzeMatchingPowersAndEffects(powerTier, Split[0], NoEffect, skill_Key, powerKey, out extractedPowerTierKeywordList, out extractedEffectKeywordList, out modEffect, out pgCombatModEx, displayAnalysisResult);
            Result &= AnalyzeMatchingPowersAndEffects(powerTier, Split[1], effect, skill_Key, powerKey, out extractedPowerTierKeywordList, out extractedEffectKeywordList, out PgModEffectEx SecondaryModEffect, out _, displayAnalysisResult);

            modEffect.SecondaryModEffect = SecondaryModEffect;
            return Result;
        }*/

        return AnalyzeMatchingPowersAndEffects(powerTier, ModText, effect, skill_Key, powerKey, out extractedPowerTierKeywordList, out extractedEffectKeywordList, out modEffect, out pgCombatModEx, displayAnalysisResult);
    }

    private bool AnalyzeMatchingPowersAndEffects(PgPowerTier powerTier, string modText, PgEffect effect, string skill_Key, string powerKey, out List<CombatKeywordEx> extractedPowerTierKeywordList, out List<CombatKeywordEx> extractedEffectKeywordList, out PgModEffectEx modEffect, out PgCombatModEx pgCombatModEx, bool displayAnalysisResult)
    {
        extractedPowerTierKeywordList = new();
        extractedEffectKeywordList = new();
        modEffect = null!;
        pgCombatModEx = null!;

        IList<PgPowerEffect> EffectList = powerTier.EffectList;
        Debug.Assert(EffectList.Count == 1);
        PgPowerEffectSimple powerSimpleEffect = (PgPowerEffectSimple)EffectList[0];

        string EffectText = effect.Description;
        bool IsGolemAbility = modText.StartsWith("Your golem minion");

        bool IsMod = false;
        if (EffectText == modText)
            IsMod = true;
        bool IsEffectTextEmpty = EffectText.Length == 0;

        AnalyzeText(modText, true, IsGolemAbility, out List<AbilityKeyword> ModAbilityList, out PgCombatEffectCollectionEx ModCombatList, out List<AbilityKeyword> ModTargetAbilityList);
        AnalyzeText(EffectText, IsMod, IsGolemAbility, out List<AbilityKeyword> EffectAbilityList, out PgCombatEffectCollectionEx EffectCombatList, out List<AbilityKeyword> EffectTargetAbilityList);

        // Hack for animal pets.
        if (EffectTargetAbilityList.Contains(AbilityKeyword.StabledPet) && ModAbilityList.Contains(AbilityKeyword.StabledPet))
        {
            EffectTargetAbilityList.Remove(AbilityKeyword.StabledPet);
            ModTargetAbilityList.AddRange(ModAbilityList);
            ModTargetAbilityList.Remove(AbilityKeyword.StabledPet);
        }

        // Hack for +Crystal Ice
        if (EffectCombatList.Exists(c => c.Keyword == CombatKeywordEx.RestoreCrystalIce))
        {
            EffectTargetAbilityList = ModTargetAbilityList;
        }

        // Hask for Cow Stampede
        if (EffectTargetAbilityList.Count == 1 && EffectTargetAbilityList[0] == AbilityKeyword.CowStampede &&
            ModAbilityList.Count == 1 && ModAbilityList[0] == AbilityKeyword.CowStampede &&
            ModTargetAbilityList.Count == 0)
        {
            ModTargetAbilityList = EffectTargetAbilityList;
        }

        // Hask for Shadow Feint
        if (EffectTargetAbilityList.Count == 1 && EffectTargetAbilityList[0] == AbilityKeyword.ShadowFeint &&
            ModAbilityList.Count == 1 && ModAbilityList[0] == AbilityKeyword.ShadowFeint &&
            ModTargetAbilityList.Count == 0)
        {
            ModTargetAbilityList = EffectTargetAbilityList;
        }

        // Hack for fire walls
        if (ModAbilityList.Count == 1 && ModAbilityList[0] == AbilityKeyword.SummonedFireWall &&
            ModTargetAbilityList.Count == 1 && ModTargetAbilityList[0] == AbilityKeyword.FireWall &&
            EffectTargetAbilityList.Count == 0)
        {
            EffectTargetAbilityList = ModTargetAbilityList;
        }

        // Hack for Carrot Power
        if (ModAbilityList.Count == 1 && ModAbilityList[0] == AbilityKeyword.CarrotPower &&
            ModTargetAbilityList.Count == 1 && ModTargetAbilityList[0] == AbilityKeyword.Kick &&
            EffectTargetAbilityList.Count == 0)
        {
            EffectTargetAbilityList = ModTargetAbilityList;
        }

        // Hack for Blur Step
        if (ModAbilityList.Count == 1 && ModAbilityList[0] == AbilityKeyword.BlurStep &&
            ModTargetAbilityList.Count == 1 && (ModTargetAbilityList[0] == AbilityKeyword.SoulBite || ModTargetAbilityList[0] == AbilityKeyword.ParadoxTrot) &&
            (EffectTargetAbilityList.Count == 0 || (EffectTargetAbilityList.Count == 1 && EffectTargetAbilityList[0] == AbilityKeyword.BlurStep)))
        {
            EffectTargetAbilityList = ModTargetAbilityList;
        }

        // Hack for Cloud Trick
        if (ModAbilityList.Count == 1 && ModAbilityList[0] == AbilityKeyword.CloudTrick &&
            ModTargetAbilityList.Count == 1 && ModTargetAbilityList[0] == AbilityKeyword.SummonTornado &&
            EffectTargetAbilityList.Count == 2 && EffectTargetAbilityList[0] == AbilityKeyword.CloudTrick && EffectTargetAbilityList[1] == AbilityKeyword.SummonTornado)
        {
            EffectTargetAbilityList = ModTargetAbilityList;
        }

        // Hack for Snare Arrow
        if (ModAbilityList.Count == 1 && ModAbilityList[0] == AbilityKeyword.SnareArrow &&
            EffectCombatList.Exists(c => c.Keyword == CombatKeywordEx.IncreaseMaxRage) &&
            EffectCombatList.Find(c => c.Keyword == CombatKeywordEx.EffectDuration) is PgCombatEffectEx SnareArrowDuration &&
            ModCombatList.Exists(c => c.Keyword == CombatKeywordEx.IncreaseMaxRage) &&
            !ModCombatList.Exists(c => c.Keyword == CombatKeywordEx.EffectDuration))
        {
            ModCombatList.Add(SnareArrowDuration);
        }

        // Hack for zero power cost increase
        if (EffectCombatList.Find(c => c.Keyword == CombatKeywordEx.IncreasePowerCost && c.Data.Value == 0) is PgCombatEffectEx ZeroPowerCostEffect)
        {
            EffectCombatList.Insert(EffectCombatList.IndexOf(ZeroPowerCostEffect), new PgCombatEffectEx() { Keyword = CombatKeywordEx.ZeroPowerCost, Data = new() } );
            EffectCombatList.Remove(ZeroPowerCostEffect);
        }

        // Hack for 100% slow immunity
        if (ModCombatList.Find(c => c.Keyword == CombatKeywordEx.GrantSlowRootImmunity && !c.Data.RawValue.HasValue) is PgCombatEffectEx StandardRootImmunity)
        {
            ModCombatList.Insert(ModCombatList.IndexOf(StandardRootImmunity), new PgCombatEffectEx() { Keyword = CombatKeywordEx.GrantSlowRootImmunity, Data = new() { RawValue = 100, RawIsPercent = true } });
            ModCombatList.Remove(StandardRootImmunity);
        }

        // Hack for fae Conduit
        if (EffectCombatList.Count >= 2 && EffectCombatList[EffectCombatList.Count - 2].Keyword == CombatKeywordEx.EffectDuration && EffectCombatList[EffectCombatList.Count - 1].Keyword == CombatKeywordEx.RecurringEffect &&
            !ModCombatList.Exists(c => c.Keyword == CombatKeywordEx.RecurringEffect))
        {
            ModCombatList.Add(EffectCombatList[EffectCombatList.Count - 1]);
        }

        if (EffectCombatList.Count == 0 && ModCombatList.Count == 0)
            return false;

        string ParsedEffectString = CombatEffectListToString(EffectCombatList, out extractedEffectKeywordList);
        string ParsedEffectTargetAbilityList = AbilityKeywordListToShortString(EffectTargetAbilityList);
        string ParsedAbilityList = AbilityKeywordListToShortString(ModAbilityList);
        string ParsedPowerString = CombatEffectListToString(ModCombatList, out extractedPowerTierKeywordList);
        string ParsedModTargetAbilityList = AbilityKeywordListToShortString(ModTargetAbilityList);

        bool IsSameTarget = IsSameAbilityKeywordList(ModTargetAbilityList, EffectTargetAbilityList);
        if (!IsSameTarget)
        {
            Debug.WriteLine(string.Empty);
            Debug.WriteLine("BAD TARGET!");
            Debug.WriteLine($"   Effect: {effect.Description}");
            Debug.WriteLine($"Parsed as: {ParsedEffectString}, Target: {ParsedEffectTargetAbilityList}");
            Debug.WriteLine($"    Power: {powerSimpleEffect.Description}");
            Debug.WriteLine($"Parsed as: {{{ParsedAbilityList}}} {ParsedPowerString}, Target: {ParsedModTargetAbilityList}");
            return false;
        }

        bool IsContained = CombatEffectContains(ModCombatList, EffectCombatList, out PgCombatEffectCollectionEx DynamicCombatEffectList, out PgCombatEffectCollectionEx StaticCombatEffectList);
        if (!IsContained)
        {
            Debug.WriteLine(string.Empty);
            Debug.WriteLine("UNPARSED!");
            Debug.WriteLine($"   Effect: {effect.Description}");
            Debug.WriteLine($"Parsed as: {ParsedEffectString}, Target: {ParsedEffectTargetAbilityList}");
            Debug.WriteLine($"    Power: {powerSimpleEffect.Description}");
            Debug.WriteLine($"Parsed as: {{{ParsedAbilityList}}} {ParsedPowerString}, Target: {ParsedModTargetAbilityList}");
            return false;
        }

        modEffect = new PgModEffectEx()
        {
            EffectKey = IsEffectTextEmpty ? string.Empty : effect.Key,
            Description = powerSimpleEffect.Description,
            AbilityList = ModAbilityList,
            StaticCombatEffectList = DynamicCombatEffectList,
            DynamicCombatEffectList = StaticCombatEffectList,
            TargetAbilityList = ModTargetAbilityList,
        };

        BuildModEffect(powerKey, effect, powerSimpleEffect.Description, IsGolemAbility, ModAbilityList, DynamicCombatEffectList, StaticCombatEffectList, ModTargetAbilityList, out pgCombatModEx);

        if (pgCombatModEx.DynamicEffects.Count > 0 || pgCombatModEx.PermanentEffects.Count > 0)
            return true;
        else
            return false;
    }

    private void AnalyzeText(string text, bool isMod, bool isGolemAbility, out List<AbilityKeyword> extractedAbilityList, out PgCombatEffectCollectionEx extractedCombatEffectList, out List<AbilityKeyword> extractedTargetAbilityList)
    {
        RemoveDecorationText(ref text);
        SimplifyGrammar(ref text);
        SimplifyRandom(ref text);
        SimplifyDuplicate(ref text);

        int RemoveCount;

        if (isMod)
            ExtractAbilityList(true, false, ref text, out extractedAbilityList, out RemoveCount);
        else
        {
            extractedAbilityList = new List<AbilityKeyword>();
            RemoveCount = 0;
        }

        ExtractAbilityList(false, isGolemAbility, ref text, out extractedTargetAbilityList, out int TargetRemoveCount);

        ExtractAttributesFull(text, extractedAbilityList, out extractedCombatEffectList);
    }

    private void RemoveDecorationText(ref string text)
    {
        int IndexFound = 0;
        RemoveDecorativeText(ref text, "(wax) ", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "(including from Toxic Irritant)", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "(such as spiders, mantises, and beetles)", out _, ref IndexFound);
        RemoveDecorativeText(ref text, ", requiring more Rage to use their Rage Abilities", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "(if any)", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "(Accuracy cancels out the Evasion that certain monsters have)", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "(which cancels out the Evasion that certain monsters have)", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "(which boosts XP earned and critical-hit chance)", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "(ignores a fatal attack once; resets after 15 minutes)", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "damage (such as via Insect Egg implantation),", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "Your golem minion's", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "(including Toxic Irritant)", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "have less than a third of their Armor", "have less than 33% of their Armor", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "have less than a third of their Max Rage", "have less than 33% of their Max Rage", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "has less than a third of their Max Rage", "have less than 33% of their Max Rage", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "have less than a third of their Armor", "have less than 33% of their Armor", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "have less than a third of their Max Rage", "have less than 33% of their Max Rage", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "has less than a third of their Max Rage", "have less than 33% of their Max Rage", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "physical damage (Crushing, Slashing, Piercing)", "Crushing, Slashing, and Piercing damage", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "Fire Wall reuse time", "Wall of Fire reuse time", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "Electricity Mitigation and Nature Mitigation (both direct and indirect)", "Electricity and Nature Mitigation", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "Electricity Damage and Nature Damage (both direct and indirect)", "Electricity and Nature Damage", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "Poison Damage and Acid Damage (both direct and indirect)", "Poison and Acid Damage", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "while in Blood Mist form", "while in Blood-Mist form", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "vile blood eruptions from Blood Mist", "vile blood eruptions from Blood-Mist", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "Blood Mist Eruption", "Blood-Mist Eruption", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "while Bulwark Mode is active", "while Bulwark-Mode is active", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "while Bulwark Mode is enabled", "while Bulwark-Mode is active", out _, ref IndexFound);

        RemoveDecorativeText(ref text, "(This cannot happen more than once per second", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "vile blood: a 5m Burst", "vile blood: a Burst", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "Poison Vulnerability and Electricity Vulnerability", "Poison and Electricity Vulnerability", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "This extra Fire Arrow consumes ammunition as normal.", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "will also shoot a Fire Arrow", "will also shoot", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "will simultaneously shoot a Fire Arrow", "will simultaneously shoot", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "will also fire a Mangling Shot", "will also fire", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "will simultaneously shoot a Mangling Shot", "will simultaneously shoot", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "(You can negate the latent psychic damage by using First Aid 4+ on your pet.)", "(You can negate the latent psychic damage.)", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "mitigation from Slashing and direct Acid damage", "mitigation from Slashing and Acid damage", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "mitigation from Piercing and direct Poison damage", "mitigation from Piercing and Poison damage", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "animal handling pets' basic attacks", "pet's basic attacks", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "pet's basic attacks", "animal handling pets' basic attacks", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "Pet basic attack", "animal handling pets' basic attacks", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "Fire damage no longer dispels your Ice Armor", "Fire damage no longer dispels it", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "Fire damage no longer dispels Ice Armor", "Fire damage no longer dispels it", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "remaining Armor (absorbing 0% when armor is empty, up", "remaining Armor (absorbing zero when armor is empty, up", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "until you trigger the teleport", "for 20 second until you trigger the teleport", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "accelerates the current reuse time of", "Shortens by 10 seconds the remaining reset time of", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "becomes a 10m Burst attack", "becomes a 10m Burst", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "react to incoming Melee attacks with", "react to incoming Melee with", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "with melee attack", "with melee", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "with any Melee attack", "with any Melee", out _, ref IndexFound);
        //RemoveDecorativeText(ref text, "all allies' Melee attacks", "all allies' Melee", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "cost of melee attack", "cost of melee", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "avoid being hit by burst attacks", "avoid being hit by burst", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "evasion of burst attacks", "evasion of burst", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "damage from Burst attacks", "damage from Burst", out _, ref IndexFound);
        ReplaceCaseInsensitive(ref text, " to you (or armor if health is full)", "/Armor to you");
        ReplaceCaseInsensitive(ref text, " (or armor if health is full)", "/Armor");
        ReplaceCaseInsensitive(ref text, " (or armor, if health is full)", "/Armor");

        if (text.StartsWith("Look At My Hammer Power Restoration") && text.EndsWith("for 30 seconds") && text.Length <= 56)
            text = "Your next Core attacks Power Restoration" + text.Substring(35, text.Length - 50);

        if (text.Contains("While Shield skill active: Mitigate Slashing"))
        {
            text = text.Replace(", Piercing +", ", Mitigate Piercing +");
            text = text.Replace(", and Crushing +", ", Mitigate Crushing +");
        }
    }

    private static void RemoveDecorativeText(ref string text, string pattern, out bool isRemoved, ref int indexFound)
    {
        RemoveDecorativeText(ref text, pattern, string.Empty, out isRemoved, ref indexFound);
    }

    private static void RemoveDecorativeText(ref string text, string pattern, string replacement, out bool isRemoved, ref int indexFound)
    {
        isRemoved = false;

        string LowerText = text.ToLowerInvariant();
        string LowerPattern = pattern.ToLowerInvariant();
        int Index = LowerText.IndexOf(LowerPattern);

        if (Index >= 0 && IsStartingSentenceIndex(LowerText, Index) && IsEndingSentenceIndex(LowerText, Index + LowerPattern.Length))
        {
            string Prolog = text.Substring(0, Index).TrimEnd();
            string Epilog = text.Substring(Index + pattern.Length).TrimStart();

            if (Prolog.Length > 0 && Epilog.Length > 0)
                if (replacement.Length > 0)
                    text = Prolog + " " + replacement + " " + Epilog;
                else
                    text = Prolog + " " + Epilog;
            else if (Prolog.Length > 0)
                if (replacement.Length > 0)
                    text = Prolog + " " + replacement;
                else
                    text = Prolog;
            else
                if (replacement.Length > 0)
                text = replacement + " " + Epilog;
            else
                text = Epilog;

            indexFound = Index;
            isRemoved = true;
        }
    }

    private static bool IsStartingSentenceIndex(string text, int index)
    {
        return index == 0 || text[index - 1] == ' ' || text[index - 1] == ',' || text[index - 1] == '\"';
    }

    private static bool IsEndingSentenceIndex(string text, int index)
    {
        return index + 1 >= text.Length || text[index] == ' ' || text[index] == ',' || text[index] == '\"' || text[index] == '.' || (index + 1 < text.Length && text[index] == '\'' && (text[index + 1] == 's' || text[index - 1] == 's'));
    }

    private static void ReplaceCaseInsensitive(ref string text, string searchPattern, string replacementPattern)
    {
        string LowerText = text.ToLowerInvariant();
        int Index;

        while ((Index = LowerText.IndexOf(searchPattern)) >= 0)
        {
            text = text.Substring(0, Index) + replacementPattern + text.Substring(Index + searchPattern.Length);
            LowerText = text.ToLowerInvariant();
        }
    }

    private static void SimplifyGrammar(ref string text)
    {
        ReplaceCaseInsensitive(ref text, "reduces ", "reduce ");
        ReplaceCaseInsensitive(ref text, "removes ", "remove ");
        ReplaceCaseInsensitive(ref text, "deals ", "deal ");
        ReplaceCaseInsensitive(ref text, "generates ", "generate ");
        ReplaceCaseInsensitive(ref text, "taunts ", "taunt ");
        ReplaceCaseInsensitive(ref text, "costs ", "cost ");
        ReplaceCaseInsensitive(ref text, "restores ", "restore ");
        ReplaceCaseInsensitive(ref text, "becomes ", "become ");
        ReplaceCaseInsensitive(ref text, "ignites ", "ignite ");
        ReplaceCaseInsensitive(ref text, "boosts ", "boost ");
        ReplaceCaseInsensitive(ref text, "heals ", "heal ");
        ReplaceCaseInsensitive(ref text, "lowers ", "lower ");
        ReplaceCaseInsensitive(ref text, "mitigates ", "mitigate ");
        ReplaceCaseInsensitive(ref text, "grants ", "grant ");
        ReplaceCaseInsensitive(ref text, "coats ", "coat ");
        ReplaceCaseInsensitive(ref text, "depletes ", "deplete ");
        ReplaceCaseInsensitive(ref text, "reaps ", "reap ");
        ReplaceCaseInsensitive(ref text, "steals ", "steal ");
        ReplaceCaseInsensitive(ref text, "hits ", "hit ");
        ReplaceCaseInsensitive(ref text, "stuns ", "stun ");
        ReplaceCaseInsensitive(ref text, "knocks ", "knock ");
        ReplaceCaseInsensitive(ref text, "knocked ", "knock ");
        ReplaceCaseInsensitive(ref text, " backwards", " backward");
        ReplaceCaseInsensitive(ref text, "causes ", "cause ");
        ReplaceCaseInsensitive(ref text, "lowers ", "lower ");
        ReplaceCaseInsensitive(ref text, "shortens ", "shorten ");
        ReplaceCaseInsensitive(ref text, "hastens ", "hasten ");
        ReplaceCaseInsensitive(ref text, "raises ", "raise ");
        ReplaceCaseInsensitive(ref text, "increases ", "increase ");
        ReplaceCaseInsensitive(ref text, "gives ", "give ");
        ReplaceCaseInsensitive(ref text, "takes ", "take ");
        ReplaceCaseInsensitive(ref text, "bleeds ", "bleed ");
        ReplaceCaseInsensitive(ref text, "gains ", "gain ");
        ReplaceCaseInsensitive(ref text, "slows ", "slow ");
        ReplaceCaseInsensitive(ref text, "suffers ", "suffer ");
        ReplaceCaseInsensitive(ref text, "triggers ", "trigger ");
        ReplaceCaseInsensitive(ref text, "makes ", "make ");
        ReplaceCaseInsensitive(ref text, "debuffs ", "debuff ");
        ReplaceCaseInsensitive(ref text, "buffs ", "buff ");
        ReplaceCaseInsensitive(ref text, "repairs ", "repair ");
        ReplaceCaseInsensitive(ref text, "ignores ", "ignore ");
        ReplaceCaseInsensitive(ref text, "resets ", "reset ");
        ReplaceCaseInsensitive(ref text, " dispels", " dispel");
        ReplaceCaseInsensitive(ref text, "dispels ", "dispel ");
        ReplaceCaseInsensitive(ref text, " seconds", " second");
        ReplaceCaseInsensitive(ref text, "-second", " second");
        ReplaceCaseInsensitive(ref text, " secs", " second");
        ReplaceCaseInsensitive(ref text, " minutes", " minute");
        ReplaceCaseInsensitive(ref text, " meters", " meter");
        ReplaceCaseInsensitive(ref text, " timer ", " time ");
        ReplaceCaseInsensitive(ref text, "haven't ", "have not ");
        ReplaceCaseInsensitive(ref text, " an additional ", " ");
        ReplaceCaseInsensitive(ref text, " additional ", " ");
        ReplaceCaseInsensitive(ref text, "attacks ", "attack ");
        ReplaceCaseInsensitive(ref text, "debuffs ", "debuff ");
        ReplaceCaseInsensitive(ref text, "spells ", "spell ");
        ReplaceCaseInsensitive(ref text, "mutations ", "mutation ");
        ReplaceCaseInsensitive(ref text, "out-of-combat", "out of combat");
        ReplaceCaseInsensitive(ref text, "vs. ", "vs ");
        ReplaceCaseInsensitive(ref text, " versus ", " vs ");
        ReplaceCaseInsensitive(ref text, " it's ", " it is ");
        ReplaceCaseInsensitive(ref text, "+Up ", "Add up ");
        ReplaceCaseInsensitive(ref text, " direct-damage ", " direct damage ");
        ReplaceCaseInsensitive(ref text, " abilities ", " ability ");
        ReplaceCaseInsensitive(ref text, "implants ", "implant ");
        ReplaceCaseInsensitive(ref text, "summons ", "summon ");
        ReplaceCaseInsensitive(ref text, "absorbs ", "absorb ");
    }

    private static void SimplifyRandom(ref string text)
    {
        int Index = text.IndexOf(" between ");
        if (Index < 0)
            return;

        string Prolog = text.Substring(0, Index);

        string MinString = string.Empty;
        Index += 9;
        while (Index < text.Length && (char.IsDigit(text[Index]) || text[Index] == '+'))
            MinString += text[Index++];

        if (MinString.Length == 0)
            return;

        if (Index + 5 >= text.Length || text.Substring(Index, 5) != " and ")
            return;

        string MaxString = string.Empty;
        Index += 5;
        while (Index < text.Length && (char.IsDigit(text[Index]) || text[Index] == '+'))
            MaxString += text[Index++];

        if (MaxString.Length == 0)
            return;

        if (text[Index++] != ' ')
            return;

        string Epilog = text.Substring(Index);

        int Min = int.Parse(MinString);
        int Max = int.Parse(MaxString);
        int RandomDamage = Min <= 1 ? Max : (Max - Min);

        string ConstantDamage = Min <= 1 ? "0" : Min.ToString();

        string RandomlyDetermined = "(randomly determined)";
        text = $"{Prolog} Deal {ConstantDamage} damage; Add up to {RandomDamage} {Epilog}";

        if (!text.Contains(RandomlyDetermined))
            text += " " + RandomlyDetermined;
    }

    private static void SimplifyDuplicate(ref string text)
    {
        if (text.Contains(" Body Heat"))
            return;

        int Index = text.IndexOf(" Health and ");
        if (Index < 0)
            return;

        string LeftString = string.Empty;
        while (Index > 0 && (char.IsDigit(text[Index - 1]) || text[Index - 1] == '+'))
            LeftString = text[--Index] + LeftString;

        if (LeftString.Length == 0)
            return;

        Index += LeftString.Length + 12;

        string RightString = string.Empty;
        while (Index < text.Length && (char.IsDigit(text[Index]) || text[Index] == '+'))
            RightString += text[Index++];

        if (RightString.Length == 0)
            return;

        text = text.Substring(0, Index - RightString.Length - 1) + text.Substring(Index);
    }

    private void ExtractAbilityList(bool limitParsing, bool isGolemAbility, ref string text, out List<AbilityKeyword> extractedAbilityList, out int removeCount)
    {
        extractedAbilityList = new List<AbilityKeyword>();
        removeCount = 0;

        Dictionary<int, string> ExtractedTable = new Dictionary<int, string>();

        foreach (string AbilityName in abilityNameList)
            if (nameToKeyword.ContainsKey(AbilityName))
            {
                if (AbilityName.Contains("Other abilities"))
                {
                }

                string NewText = text;
                int NewIndex = -1;

                RemoveDecorativeText(ref NewText, AbilityName, out bool IsRemoved, ref NewIndex);

                if (IsRemoved)
                {
                    bool IsAlreadyExtracted = false;
                    foreach (KeyValuePair<int, string> ExtractedEntry in ExtractedTable)
                    {
                        if (ExtractedEntry.Key == NewIndex)
                            IsAlreadyExtracted = true;

                        int ExistingIndex = ExtractedEntry.Value.IndexOf(AbilityName);
                        if (ExistingIndex >= 0 && ExtractedEntry.Key + ExistingIndex == NewIndex)
                            IsAlreadyExtracted = true;
                    }

                    if (!IsAlreadyExtracted)
                    {
                        if (AbilityName == "Knife ability with 'Cut'" && ExtractedTable.Count == 1 && ExtractedTable.First().Value == "All Knife ability")
                            ExtractedTable.Clear();

                        ExtractedTable.Add(NewIndex, AbilityName);
                    }
                }
            }

        List<string> ExtractedTableValues = new(ExtractedTable.Values);

        // Hack for golem abilities
        if (isGolemAbility)
        {
            if (ExtractedTable.Count == 1)
            {
                if (ExtractedTableValues.Contains("Taunting Punch") ||
                    ExtractedTableValues.Contains("Poison Bomb") ||
                    ExtractedTableValues.Contains("Self Destruct") ||
                    ExtractedTableValues.Contains("Doom Admixture") ||
                    ExtractedTableValues.Contains("Healing Mist") ||
                    ExtractedTableValues.Contains("Invigorating Mist") ||
                    ExtractedTableValues.Contains("Rage Mist") ||
                    ExtractedTableValues.Contains("Rage Acid Toss") ||
                    ExtractedTableValues.Contains("Self Sacrifice") ||
                    ExtractedTableValues.Contains("Healing Injection") ||
                    ExtractedTableValues.Contains("Fire Balm"))
                {
                    ExtractedTable.Clear();
                    ExtractedTableValues.Clear();
                }
            }
            else if (ExtractedTable.Count == 2)
            {
                if (ExtractedTableValues.Contains("Rage Mist") && ExtractedTableValues.Contains("Self Sacrifice"))
                {
                    ExtractedTable.Clear();
                    ExtractedTableValues.Clear();
                }
            }
        }

        if (ExtractedTable.Count > 0)
        {
            int SeparatingIndex = text.IndexOf("boost your");
            if (SeparatingIndex < 0)
                SeparatingIndex = text.Length;

            List<string> KeyList = new List<string>();
            List<string> BestStringList = new List<string>();
            int LastBestIndex = -1;
            bool IsFirstAbilityGeneric = false;

            while (ExtractedTable.Count > 0)
            {
                int BestIndex = -1;
                foreach (KeyValuePair<int, string> Entry in ExtractedTable)
                    if (BestIndex == -1 || BestIndex > Entry.Key)
                        BestIndex = Entry.Key;

                string BestString = ExtractedTable[BestIndex];
                Debug.Assert(nameToKeyword.ContainsKey(BestString));
                List<AbilityKeyword> KeywordList = nameToKeyword[BestString];

                bool IsAbilityGeneric = false;
                foreach (AbilityKeyword Keyword in KeywordList)
                    if (Keyword != AbilityKeyword.PsiHealthWave && Keyword != AbilityKeyword.PsiArmorWave && Keyword != AbilityKeyword.PsiPowerWave)
                        IsAbilityGeneric |= GenericAbilityList.Contains(Keyword);

                if (LastBestIndex == -1)
                    IsFirstAbilityGeneric = IsAbilityGeneric;
                else
                {
                    if (limitParsing && (BestIndex > LastBestIndex + 8 || BestIndex > SeparatingIndex))
                        break;
                    if (IsAbilityGeneric != IsFirstAbilityGeneric)
                        break;
                }

                int length = BestString.Length;
                if (BestIndex + length + 1 < text.Length && text[BestIndex + length] == ' ' && char.IsDigit(text[BestIndex + length + 1]))
                {
                    length++;
                    while (BestIndex + length < text.Length && char.IsDigit(text[BestIndex + length]))
                        length++;

                    string EnergyBowPattern = " (Energy Bow)";
                    if (BestIndex + length + EnergyBowPattern.Length < text.Length && text.Substring(BestIndex + length, EnergyBowPattern.Length) == EnergyBowPattern)
                        length += EnergyBowPattern.Length;

                    string OrcPattern = " (Orc)";
                    if (BestIndex + length + OrcPattern.Length < text.Length && text.Substring(BestIndex + length, OrcPattern.Length) == OrcPattern)
                        length += OrcPattern.Length;
                }

                KeyList.Add(BestString);
                BestStringList.Add(text.Substring(BestIndex, length));

                ExtractedTable.Remove(BestIndex);

                LastBestIndex = BestIndex + length;
            }

            int UnusedIndex = -1;
            for (int i = 0; i < BestStringList.Count; i++)
            {
                string Key = KeyList[i];
                string KeyString = BestStringList[i];

                bool IsRemoved;
                do
                {
                    RemoveDecorativeText(ref text, KeyString, "@", out IsRemoved, ref UnusedIndex);
                    if (IsRemoved)
                        removeCount++;
                }
                while (IsRemoved);

                extractedAbilityList.AddRange(nameToKeyword[Key]);
            }

            extractedAbilityList = extractedAbilityList.Distinct().ToList();
        }
    }

    private void ExtractAttributesFull(string text, List<AbilityKeyword> extractedAbilityList, out PgCombatEffectCollectionEx extractedCombatEffectList)
    {
        List<CombatKeywordEx> SkippedKeywordList = new List<CombatKeywordEx>();
        ExtractAttributes(text, SkippedKeywordList, extractedAbilityList, out extractedCombatEffectList);
    }

    private void ExtractAttributes(string text, List<CombatKeywordEx> skippedKeywordList, List<AbilityKeyword> extractedAbilityList, out PgCombatEffectCollectionEx extractedCombatEffectList)
    {
        extractedCombatEffectList = new PgCombatEffectCollectionEx();
        List<CombatKeywordEx> SkippedKeywordListCopy = new List<CombatKeywordEx>(skippedKeywordList);

        bool IsAttributeExtracted;
        do
        {
            IsAttributeExtracted = ExtractKnownAttribute(SkippedKeywordListCopy, ref text, out List<PgCombatEffectEx> CombatEffectList);

            if (IsAttributeExtracted)
            {
                extractedCombatEffectList.AddRange(CombatEffectList);
                foreach (PgCombatEffectEx Item in CombatEffectList)
                {
                    SkippedKeywordListCopy.Add(Item.Keyword);
                }
            }
        }
        while (IsAttributeExtracted);
    }

    private bool ExtractKnownAttribute(List<CombatKeywordEx> skippedKeywordList, ref string text, out List<PgCombatEffectEx> extractedCombatEffectList)
    {
        List<CombatKeywordEx> ExtractedKeywordList = new List<CombatKeywordEx>();
        PgNumericValue Data1 = new PgNumericValue();
        GameDamageType DamageType = GameDamageType.Internal_None;
        GameCombatSkill CombatSkill = GameCombatSkill.Internal_None;
        int ParsedIndex = -1;
        string ModifiedText = text;
        SentenceEx? SelectedSentence = null;

        foreach (SentenceEx Item in SentenceList)
            ExtractSentence(Item, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref DamageType, ref CombatSkill, ref ParsedIndex, ref SelectedSentence);

        extractedCombatEffectList = new List<PgCombatEffectEx>();

        if (ExtractedKeywordList.Count > 0)
        {
            text = ModifiedText;
            bool IsDataSaved = false;

            foreach (CombatKeywordEx Item in ExtractedKeywordList)
            {
                PgCombatEffectEx ExtractedCombatEffect;
                if (!IsDataSaved)
                {
                    ExtractedCombatEffect = new PgCombatEffectEx() { Keyword = Item, Data = Data1, DamageType = DamageType, CombatSkill = CombatSkill };
                    IsDataSaved = true;
                }
                else
                    ExtractedCombatEffect = new PgCombatEffectEx() { Keyword = Item, Data = new PgNumericValue() { RawIsPercent = false } };

                extractedCombatEffectList.Add(ExtractedCombatEffect);
            }

            Debug.Assert(SelectedSentence != null);
            SelectedSentence?.SetUsed();

            return true;
        }
        else
            return false;
    }

    private void ExtractSentence(SentenceEx sentence, List<CombatKeywordEx> skippedKeywordList, string text, ref string modifiedText, List<CombatKeywordEx> extractedKeywordList, ref PgNumericValue data1, ref GameDamageType damageType, ref GameCombatSkill combatSkill, ref int parsedIndex, ref SentenceEx? selectedSentence)
    {
        string NewText = text;
        List<CombatKeywordEx> NewExtractedKeywordList = new List<CombatKeywordEx>();
        PgNumericValue NewData1 = new PgNumericValue();
        GameDamageType NewDamageType = GameDamageType.Internal_None;
        GameCombatSkill NewCombatSkill = GameCombatSkill.Internal_None;
        int NewParsedIndex = -1;

        bool IsExtracted = ExtractNewSentence(sentence, skippedKeywordList, ref NewText, NewExtractedKeywordList, ref NewData1, ref NewDamageType, ref NewCombatSkill, ref NewParsedIndex);

        if (!IsExtracted)
            return;

        if (parsedIndex < 0 || NewParsedIndex + 1 < parsedIndex)
        {
            modifiedText = NewText;
            extractedKeywordList.Clear();
            extractedKeywordList.AddRange(NewExtractedKeywordList);
            data1 = NewData1;
            damageType = NewDamageType;
            combatSkill = NewCombatSkill;
            parsedIndex = NewParsedIndex;
            selectedSentence = sentence;
        }
    }

    private bool ExtractNewSentence(SentenceEx sentence, List<CombatKeywordEx> skippedKeywordList, ref string text, List<CombatKeywordEx> extractedKeywordList, ref PgNumericValue data1, ref GameDamageType damageType, ref GameCombatSkill combatSkill, ref int parsedIndex)
    {
        string format = sentence.Format;
        List<CombatKeywordEx> associatedKeywordList = sentence.AssociatedKeywordList;
        SignInterpretation signInterpretation = sentence.SignInterpretation;

        string LowerText = text.ToLowerInvariant();
        string LowerFormat = format.ToLowerInvariant();

        int Index = LowerFormat.IndexOf('%');

        if (Index >= 0)
        {
            char FormatType = LowerFormat[Index + 1];

            if (FormatType != 'f')
            {
                Debug.WriteLine($"Format \"{format}\" contains % but for unsupported type {FormatType}");
                return false;
            }

            string BeforePattern = LowerFormat.Substring(0, Index);
            string AfterPattern = LowerFormat.Substring(Index + 2);

            int StartIndex = -1;
            bool ContinueParsing;
            do
            {
                StartIndex++;
                ContinueParsing = ParseFormat(StartIndex, BeforePattern, AfterPattern, LowerText, associatedKeywordList, signInterpretation, ref text, extractedKeywordList, ref data1, ref damageType, ref combatSkill, out int ParsedLength);
                StartIndex += ParsedLength;
            }
            while (ContinueParsing);

            if (extractedKeywordList.Count > 0)
            {
                parsedIndex = StartIndex;
                return true;
            }
            else
                return false;
        }
        else
        {
            int PatternIndex = FindPattern(LowerText, LowerFormat, 0, out GameDamageType ParsedDamageType, out GameCombatSkill ParsedCombatSkill, out AbilityKeyword songKeyword, out int FormatLength);
            if (PatternIndex < 0)
                return false;

            text = text.Substring(0, PatternIndex) + text.Substring(PatternIndex + FormatLength).Trim();
            extractedKeywordList.AddRange(associatedKeywordList);
            damageType = ParsedDamageType;
            combatSkill = ParsedCombatSkill;

            parsedIndex = PatternIndex;
            return true;
        }
    }

    private bool ParseFormat(int startIndex, string beforePattern, string afterPattern, string lowerText, List<CombatKeywordEx> associatedKeywordList, SignInterpretation signInterpretation, ref string text, List<CombatKeywordEx> extractedKeywordList, ref PgNumericValue data1, ref GameDamageType damageType, ref GameCombatSkill combatSkill, out int parsedLength)
    {
        int PatternIndex;
        int AfterPatternIndex;
        GameDamageType DamageTypeBefore;
        GameCombatSkill CombatSkillBefore;
        int PatternLengthBefore;
        GameDamageType DamageTypeAfter;
        GameCombatSkill CombatSkillAfter;
        AbilityKeyword SongKeyword;
        int PatternLengthAfter;

        if (beforePattern.Length > 0)
        {
            PatternIndex = FindPattern(lowerText, beforePattern, startIndex, out DamageTypeBefore, out CombatSkillBefore, out SongKeyword, out PatternLengthBefore);
            DamageTypeAfter = GameDamageType.Internal_None;
            CombatSkillAfter = GameCombatSkill.Internal_None;
            PatternLengthAfter = 0;
        }
        else
        {
            DamageTypeBefore = GameDamageType.Internal_None;
            CombatSkillBefore = GameCombatSkill.Internal_None;
            PatternLengthBefore = 0;

            AfterPatternIndex = FindPattern(lowerText, afterPattern, startIndex, out DamageTypeAfter, out CombatSkillAfter, out SongKeyword, out PatternLengthAfter);
            if (AfterPatternIndex > 0)
            {
                PatternIndex = NumericValueBackwardIndex(lowerText, AfterPatternIndex);
                if (PatternIndex >= 0)
                    startIndex -= AfterPatternIndex - PatternIndex;
            }
            else
                PatternIndex = -1;
        }

        if (PatternIndex < 0)
        {
            parsedLength = 0;
            return false;
        }

        int StartDataIndex = PatternIndex + PatternLengthBefore;
        int EndDataIndex = StartDataIndex;

        if (EndDataIndex + 1 < lowerText.Length && (lowerText[EndDataIndex] == '+' || lowerText[EndDataIndex] == '-') && char.IsDigit(lowerText[EndDataIndex + 1]))
            EndDataIndex++;

        while (EndDataIndex < lowerText.Length && (char.IsDigit(lowerText[EndDataIndex]) || (lowerText[EndDataIndex] == '.' && EndDataIndex > StartDataIndex)))
            EndDataIndex++;

        if (EndDataIndex < lowerText.Length && lowerText[EndDataIndex] == '%')
            EndDataIndex++;

        parsedLength = PatternIndex - startIndex + 1;

        if (EndDataIndex <= StartDataIndex)
            return true;

        PgNumericValue Data = NumericValueParse(lowerText.Substring(StartDataIndex, EndDataIndex - StartDataIndex));

        AfterPatternIndex = FindPattern(lowerText, afterPattern, EndDataIndex, out DamageTypeAfter, out CombatSkillAfter, out SongKeyword, out PatternLengthAfter);
        if (AfterPatternIndex != EndDataIndex)
            return true;

        Debug.Assert(DamageTypeBefore == GameDamageType.Internal_None || DamageTypeAfter == GameDamageType.Internal_None);
        Debug.Assert(CombatSkillBefore == GameCombatSkill.Internal_None || CombatSkillAfter == GameCombatSkill.Internal_None);

        data1 = Data;

        switch (signInterpretation)
        {
            case SignInterpretation.Normal:
                break;
            case SignInterpretation.Opposite:
                if (data1.RawValue.HasValue)
                    data1.RawValue = -data1.RawValue.Value;
                break;
            case SignInterpretation.AlwaysNegative:
                if (data1.RawValue.HasValue && data1.Value > 0)
                    data1.RawValue = -data1.RawValue.Value;
                break;
        }

        text = text.Substring(0, PatternIndex) + text.Substring(EndDataIndex + PatternLengthAfter).Trim();
        extractedKeywordList.AddRange(associatedKeywordList);

        if (DamageTypeBefore != GameDamageType.Internal_None)
            damageType = DamageTypeBefore;
        else if (DamageTypeAfter != GameDamageType.Internal_None)
            damageType = DamageTypeAfter;

        if (CombatSkillBefore != GameCombatSkill.Internal_None)
            combatSkill = CombatSkillBefore;
        else if (CombatSkillAfter != GameCombatSkill.Internal_None)
            combatSkill = CombatSkillAfter;

        return false;
    }

    private int FindPattern(string text, string pattern, int startIndex, out GameDamageType damageType, out GameCombatSkill combatSkill, out AbilityKeyword songKeyword, out int patternLength)
    {
        damageType = GameDamageType.Internal_None;
        combatSkill = GameCombatSkill.Internal_None;
        songKeyword = AbilityKeyword.Internal_None;

        int Value;
        int Index;

        Debug.Assert(typeof(GameDamageType).GetEnumNames().Length == DamageTypeTextMap.Count);

        Index = FindPattern("#D", DamageTypeTextMap, text, pattern, startIndex, out Value, out patternLength);
        if (Index >= 0)
        {
            damageType = (GameDamageType)Value;
            return Index;
        }

        Debug.Assert(typeof(GameCombatSkill).GetEnumNames().Length == SkillTextMap.Count);

        Index = FindPattern("#S", SkillTextMap, text, pattern, startIndex, out Value, out patternLength);
        if (Index >= 0)
        {
            combatSkill = (GameCombatSkill)Value;
            return Index;
        }

        Index = FindPattern("#B", SongMap, text, pattern, startIndex, out Value, out patternLength);
        if (Index >= 0)
        {
            songKeyword = (AbilityKeyword)Value;
            return Index;
        }

        return -1;
    }

    private int FindPattern(string patternString, Dictionary<int, string> textMap, string text, string pattern, int startIndex, out int value, out int patternLength)
    {
        int DamageTypeIndex = pattern.IndexOf(patternString.ToLowerInvariant());

        if (DamageTypeIndex < 0)
        {
            value = 0;
            patternLength = pattern.Length;
            return text.IndexOf(pattern, startIndex);
        }
        else
        {
            string BeforeTypePattern = pattern.Substring(0, DamageTypeIndex);
            string AfterTypePattern = pattern.Substring(DamageTypeIndex + patternString.Length);

            int MinStartIndex = text.IndexOf(BeforeTypePattern, startIndex);
            if (MinStartIndex > startIndex)
                startIndex = MinStartIndex;

            int LowestIndex = -1;
            int LowestValue = 0;
            int LowestLength = -1;

            Dictionary<int, string> FoundTextMap = new Dictionary<int, string>();

            bool IsAddedToFoundTextMap;
            int BestFoundIndex = -1;

            do
            {
                IsAddedToFoundTextMap = false;
                int BestKey = -1;
                int BestKeyIndex = text.Length;

                foreach (KeyValuePair<int, string> Entry in textMap)
                {
                    if (Entry.Key == 0)
                        continue;

                    if (FoundTextMap.ContainsKey(Entry.Key))
                        continue;

                    string ValueText = Entry.Value.ToLowerInvariant();
                    int FoundIndex = text.IndexOf(ValueText, startIndex);

                    if (FoundIndex >= 0)
                        if (BestKeyIndex > FoundIndex)
                            if (BestFoundIndex == -1 || FoundIndex < BestFoundIndex + 17)
                            {
                                BestKeyIndex = FoundIndex;
                                BestKey = Entry.Key;
                            }
                }

                if (BestKey != -1)
                {
                    FoundTextMap.Add(BestKey, textMap[BestKey].ToLowerInvariant());
                    IsAddedToFoundTextMap = true;
                    BestFoundIndex = BestKeyIndex;
                }
            }
            while (IsAddedToFoundTextMap);

            List<int> FoundTextKey = new List<int>(FoundTextMap.Keys);
            Dictionary<string, int> PossibleTextMap = new Dictionary<string, int>();

            int Value = 0;
            for (int i = 0; i < FoundTextMap.Count; i++)
                Value |= FoundTextKey[i];

            switch (FoundTextMap.Count)
            {
                default:
                    CreateDamageTypePermutations(FoundTextMap, FoundTextKey, 0, string.Empty, PossibleTextMap, Value);
                    break;
                case 3:
                    for (int i = 0; i < 3; i++)
                        for (int j = 0; j < 3; j++)
                            if (j != i)
                                for (int k = 0; k < 3; k++)
                                    if (k != i && k != j)
                                    {
                                        string KeyAnd = $"{FoundTextMap[FoundTextKey[i]]}, {FoundTextMap[FoundTextKey[j]]}, and {FoundTextMap[FoundTextKey[k]]}";
                                        PossibleTextMap.Add(KeyAnd, Value);
                                        string KeyOr = $"{FoundTextMap[FoundTextKey[i]]}, {FoundTextMap[FoundTextKey[j]]}, or {FoundTextMap[FoundTextKey[k]]}";
                                        PossibleTextMap.Add(KeyOr, Value);
                                        string KeySlash = $"{FoundTextMap[FoundTextKey[i]]}/{FoundTextMap[FoundTextKey[j]]}/{FoundTextMap[FoundTextKey[k]]}";
                                        PossibleTextMap.Add(KeySlash, Value);
                                    }

                    break;
                case 2:
                    for (int i = 0; i < 2; i++)
                        for (int j = 0; j < 2; j++)
                            if (j != i)
                            {
                                string KeyAnd = $"{FoundTextMap[FoundTextKey[i]]} and {FoundTextMap[FoundTextKey[j]]}";
                                PossibleTextMap.Add(KeyAnd, Value);
                                string KeyOr = $"{FoundTextMap[FoundTextKey[i]]} or {FoundTextMap[FoundTextKey[j]]}";
                                PossibleTextMap.Add(KeyOr, Value);
                            }

                    break;

                case 0:
                    break;

                case 1:
                    PossibleTextMap.Add(FoundTextMap[FoundTextKey[0]], Value);
                    break;
            }

            foreach (KeyValuePair<string, int> Entry in PossibleTextMap)
            {
                string ExtendedPattern = $"{BeforeTypePattern}{Entry.Key}{AfterTypePattern}";
                int Index = text.IndexOf(ExtendedPattern, startIndex);
                if (Index == -1)
                    continue;

                if (LowestIndex == -1 || LowestIndex > Index)
                {
                    LowestIndex = Index;
                    LowestValue = Entry.Value;
                    LowestLength = ExtendedPattern.Length;
                }
            }

            if (LowestIndex >= 0)
            {
                Debug.Assert(LowestValue > 0);
                Debug.Assert(LowestLength > 0);

                value = LowestValue;
                patternLength = LowestLength;

                return LowestIndex;
            }
            else
            {
                value = 0;
                patternLength = pattern.Length;
                return -1;
            }
        }
    }

    public static int NumericValueBackwardIndex(string text, int startIndex)
    {
        int Index = startIndex;

        if (Index > 0 && text[Index - 1] == '%')
            Index--;

        int IndexPercentCharacter = Index;

        while (Index > 0 && char.IsDigit(text[Index - 1]))
            Index--;

        if (Index == IndexPercentCharacter || Index == startIndex)
            return -1;

        while (Index > 0 && (char.IsDigit(text[Index - 1]) || text[Index - 1] == '.' || text[Index - 1] == '-' || text[Index - 1] == '+'))
            Index--;

        return Index;
    }

    private void CreateDamageTypePermutations(Dictionary<int, string> FoundTextMap, List<int> FoundTextKey, int startIndex, string previousPermutation, Dictionary<string, int> PossibleTextMap, int Value)
    {
        if (startIndex + 2 < FoundTextMap.Count)
        {
            for (int i = startIndex; i < FoundTextMap.Count; i++)
            {
                if (startIndex > 0)
                {
                    string NewPermutationComma = $"{previousPermutation}, {FoundTextMap[FoundTextKey[i]]}";
                    CreateDamageTypePermutations(FoundTextMap, FoundTextKey, startIndex + 1, NewPermutationComma, PossibleTextMap, Value);
                    string NewPermutationSlash = $"{previousPermutation}/{FoundTextMap[FoundTextKey[i]]}";
                    CreateDamageTypePermutations(FoundTextMap, FoundTextKey, startIndex + 1, NewPermutationSlash, PossibleTextMap, Value);
                }
                else
                    CreateDamageTypePermutations(FoundTextMap, FoundTextKey, startIndex + 1, FoundTextMap[FoundTextKey[i]], PossibleTextMap, Value);
            }
        }
        else
        {
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                    if (j != i)
                    {
                        string KeyAnd = $"{previousPermutation}, {FoundTextMap[FoundTextKey[startIndex + i]]}, and {FoundTextMap[FoundTextKey[startIndex + j]]}";
                        PossibleTextMap.Add(KeyAnd, Value);
                        string KeyOr = $"{previousPermutation}, {FoundTextMap[FoundTextKey[startIndex + i]]}, or {FoundTextMap[FoundTextKey[startIndex + j]]}";
                        PossibleTextMap.Add(KeyOr, Value);
                        string KeySlash = $"{previousPermutation}/{FoundTextMap[FoundTextKey[startIndex + i]]}/{FoundTextMap[FoundTextKey[startIndex + j]]}";
                        PossibleTextMap.Add(KeySlash, Value);
                    }
        }
    }

    public static PgNumericValue NumericValueParse(string text)
    {
        if (text.EndsWith("%"))
            return NumericValueFromDoublePercent(float.Parse(text.Substring(0, text.Length - 1), NumberStyles.Float, CultureInfo.InvariantCulture));
        else
            return NumericValueFromDouble(float.Parse(text, NumberStyles.Float, CultureInfo.InvariantCulture));
    }

    public static PgNumericValue NumericValueFromDouble(float value)
    {
        if (float.IsNaN(value))
            return new PgNumericValue() { RawIsPercent = false };
        else
            return new PgNumericValue() { RawValue = value, RawIsPercent = false };
    }

    public static PgNumericValue NumericValueFromDoublePercent(float value)
    {
        if (float.IsNaN(value))
            return new PgNumericValue() { RawIsPercent = true };
        else
            return new PgNumericValue() { RawValue = value, RawIsPercent = true };
    }

    public static PgNumericValue NumericValueFromDouble(double value)
    {
        if (double.IsNaN(value))
            return new PgNumericValue() { RawIsPercent = false };
        else
            return new PgNumericValue() { RawValue = (float)value, RawIsPercent = false };
    }

    public static PgNumericValue NumericValueFromDoublePercent(double value)
    {
        if (double.IsNaN(value))
            return new PgNumericValue() { RawIsPercent = true };
        else
            return new PgNumericValue() { RawValue = (float)value, RawIsPercent = true };
    }

    private string CombatEffectListToString(List<PgCombatEffectEx> list, out List<CombatKeywordEx> combatKeywordList)
    {
        string Result = string.Empty;
        combatKeywordList = new List<CombatKeywordEx>();

        foreach (PgCombatEffectEx Item in list)
        {
            if (Result.Length > 0)
                Result += ", ";

            Result += Item.ToString();
            combatKeywordList.Add(Item.Keyword);
        }

        return Result;
    }

    public static string AbilityKeywordListToShortString(List<AbilityKeyword> list)
    {
        string Result = string.Empty;

        foreach (AbilityKeyword Keyword in list)
        {
            if (Result.Length > 0)
                Result += ", ";

            Result += Keyword.ToString();
        }

        return Result;
    }

    public static bool IsSameAbilityKeywordList(List<AbilityKeyword> list1, List<AbilityKeyword> list2)
    {
        if (list1.Count != list2.Count)
            return false;

        for (int i = 0; i < list1.Count; i++)
            if (list1[i] != list2[i])
                return false;

        return true;
    }

    public static bool CombatEffectContains(List<PgCombatEffectEx> list1, List<PgCombatEffectEx> list2, out PgCombatEffectCollectionEx difference, out PgCombatEffectCollectionEx union)
    {
        difference = new PgCombatEffectCollectionEx();
        union = new PgCombatEffectCollectionEx();

        List<PgCombatEffectEx> MatchList = new List<PgCombatEffectEx>();
        List<PgCombatEffectEx> NoMatchList = new List<PgCombatEffectEx>();

        foreach (PgCombatEffectEx Item2 in list2)
        {
            bool IsContained = false;
            foreach (PgCombatEffectEx Item1 in list1)
                if (CombatEffectEquals(Item1, Item2))
                {
                    IsContained = true;
                    MatchList.Add(Item1);
                    break;
                }

            if (!IsContained)
                return false;
        }

        bool AddToDifference = true;

        foreach (PgCombatEffectEx Item in list1)
            if (AddToDifference)
                if (!MatchList.Contains(Item))
                {
                    difference.Add(Item);
                }
                else
                    AddToDifference = false;
            else if (!MatchList.Contains(Item))
                NoMatchList.Add(Item);

        foreach (PgCombatEffectEx Item in list1)
            if (!difference.Contains(Item))
                union.Add(Item);

        return true;
    }

    public static bool CombatEffectEquals(PgCombatEffectEx effect1, PgCombatEffectEx effect2)
    {
        if (effect1.Data.RawValue.HasValue != effect2.Data.RawValue.HasValue && effect2.Data.RawValue.HasValue)
            return false;

        if (effect2.Data.RawValue.HasValue && effect1.Data.Value != effect2.Data.Value)
            return false;

        return true;
    }

    private void BuildModEffect(string powerKey, PgEffect effect, string description, bool isGolemMinion, List<AbilityKeyword> abilityList, PgCombatEffectCollectionEx dynamicCombatEffectList, PgCombatEffectCollectionEx staticCombatEffectList, List<AbilityKeyword> targetAbilityList, out PgCombatModEx pgCombatModEx)
    {
        PgCombatModEx pgExtraCombatModEx;

        switch (powerKey)
        {
            case "1006":
            case "10506":
            case "12053":
            case "15303":
            case "9303":
            case "10404":
            case "1042":
            case "10507":
            case "11603":
            case "12091":
            case "12102":
            case "12103":
            case "14352":
            case "15106":
            case "154":
            case "15401":
            case "15403":
            case "16042":
            case "16083":
            case "162":
            case "17222":
            case "20041":
            case "20042":
            case "2051":
            case "2054":
            case "21024":
            case "21043":
            case "2153":
            case "22004":
            case "22083":
            case "23252":
            case "24151":
            case "24244":
            case "25131":
            case "25192":
            case "257":
            case "3253":
            case "4082":
            case "6003":
            case "7201":
            case "7205":
            case "7206":
            case "8005":
            case "9005":
            case "9085":
            case "12026":
            case "12161":
            case "1004":
            case "1045":
            case "14401":
            case "12052":
            case "13044":
            case "15054":
            case "15152":
            case "15402":
            case "161":
            case "164":
            case "17163":
            case "17223":
            case "20043":
            case "2020":
            case "20301":
            case "20352":
            case "2059":
            case "2103":
            case "21065":
            case "21252":
            case "22043":
            case "2208":
            case "22252":
            case "22302":
            case "22452":
            case "24003":
            case "25012":
            case "25101":
            case "25193":
            case "258":
            case "3022":
            case "3203":
            case "4085":
            case "4305":
            case "9088":
            case "9401":
            case "9402":
            case "9403":
            case "22041":
            case "22402":
            case "9501":
            case "9502":
            case "4034":
            case "7102":
            case "9503":
            case "13053":
            case "25073":
            case "25074":
            case "8103":
            case "9087":
            case "12021":
            case "17304":
            case "22203":
            case "23501":
            case "23554":
            case "18103":
            case "20061":
            case "1026":
            case "1027":
            case "10314":
            case "14054":
            case "17244":
            case "25352":
            case "10454":
            case "12008":
            case "12009":
            case "13055":
            case "13253":
            case "16102":
            case "16242":
            case "17122":
            case "18047":
            case "20405":
            case "21067":
            case "23205":
            case "24002":
            case "24035":
            case "24062":
            case "25022":
            case "25054":
            case "25224":
            case "25304":
            case "28662":
            case "3252":
            case "3304":
            case "5040":
            case "5065":
            case "7207":
            case "11701":
            case "10003":
            case "10082":
            case "10124":
            case "10452":
            case "11501":
            case "12104":
            case "1301":
            case "1351":
            case "15014":
            case "15052":
            case "16011":
            case "2052":
            case "1251":
            case "21062":
            case "21251":
            case "21303":
            case "22301":
            case "23023":
            case "24241":
            case "3003":
            case "3135":
            case "4003":
            case "4112":
            case "5008":
            case "5253":
            case "5434":
            case "6033":
            case "6151":
            case "9754":
            case "1083":
            case "16182":
            case "21253":
            case "21301":
            case "22303":
            case "23101":
            case "4532":
            case "9756":
            case "7473":
            case "7474":
            case "5122":
            case "157":
            case "159":
            case "16004":
            case "15452":
            case "16223":
            case "21353":
            case "20302":
            case "25105":
            case "26094":
            case "26205":
            case "4304":
            case "26053":
            case "27172":
            case "27173":
            case "6088":
            case "7202":
            case "9703":
            case "10509":
            case "20009":
            case "13206":
            case "13104":
            case "13105":
            case "13106":
            case "4036":
            case "9504":
            case "11605":
            case "1028":
            case "1044":
            case "15153":
            case "20011":
            case "2011":
            case "27033":
            case "28621":
            case "3041":
            case "3084":
            case "6034":
            case "13016":
            case "22003":
            case "16111":
            case "15454":
            case "2202":
            case "1062":
            case "158":
            case "17123":
            case "23201":
            case "23451":
            case "6083":
            case "16023":
            case "3006":
            case "4114":
            case "28612":
            case "5203":
            case "15252":
            case "4531":
            case "11702":
            case "11703":
            case "14017":
            case "14053":
            case "22254":
            case "24095":
            case "4202":
            case "6175":
            case "9083":
            case "4032":
            case "6135":
            case "9084":
            case "20016":
            case "20019":
            case "20062":
            case "20065":
            case "20066":
            case "20067":
            case "21044":
            case "21302":
            case "22061":
            case "10508":
            case "13205":
            case "13351":
            case "5254":
            case "5062":
            case "15104":
            case "26221":
            case "4083":
            case "4084":
            case "9086":
            case "28242":
            case "29942":
            case "7306":
            case "7307":
            case "7308":
            case "7309":
            case "7310":
            case "17022":
            case "17023":
            case "17302":
            case "3305":
            case "10083":
            case "10163":
            case "25225":
            case "26223":
            case "28221":
            case "12011":
            case "12025":
            case "21005":
            case "4064":
            case "6306":
            case "8006":
            case "2023":
            case "8007":
            case "1302":
            case "25011":
            case "27075":
            case "1002":
            case "10043":
            case "1009":
            case "1010":
            case "10122":
            case "10309":
            case "11353":
            case "11456":
            case "11652":
            case "12012":
            case "12013":
            case "12014":
            case "12022":
            case "12023":
            case "12024":
            case "13015":
            case "13056":
            case "13152":
            case "13303":
            case "13356":
            case "15031":
            case "15032":
            case "15105":
            case "15203":
            case "15352":
            case "16002":
            case "16224":
            case "17047":
            case "17322":
            case "18034":
            case "20003":
            case "20013":
            case "2003":
            case "2005":
            case "20102":
            case "2015":
            case "2017":
            case "2019":
            case "2201":
            case "22451":
            case "23004":
            case "23022":
            case "23203":
            case "23453":
            case "23552":
            case "24032":
            case "24064":
            case "24094":
            case "24182":
            case "24212":
            case "25132":
            case "25134":
            case "25194":
            case "25197":
            case "256":
            case "26012":
            case "26035":
            case "27054":
            case "28041":
            case "28805":
            case "28806":
            case "3005":
            case "3007":
            case "3026":
            case "4035":
            case "4203":
            case "4302":
            case "5010":
            case "5039":
            case "5092":
            case "5121":
            case "5123":
            case "5353":
            case "7021":
            case "8021":
            case "9034":
            case "9601":
            case "9602":
            case "9603":
            case "9604":
            case "1024":
            case "10307":
            case "10456":
            case "10501":
            case "10125":
            case "10306":
            case "10503":
            case "10552":
            case "11452":
            case "16022":
            case "16062":
            case "17044":
            case "17082":
            case "18102":
            case "2016":
            case "2055":
            case "22002":
            case "2203":
            case "22089":
            case "22355":
            case "23504":
            case "23604":
            case "24063":
            case "24282":
            case "25053":
            case "26013":
            case "28042":
            case "28043":
            case "28103":
            case "28104":
            case "28105":
            case "3025":
            case "3042":
            case "3133":
            case "3202":
            case "4006":
            case "4401":
            case "4402":
            case "5009":
            case "5154":
            case "6133":
            case "8003":
            case "8042":
            case "8043":
            case "8044":
            case "9004":
            case "9702":
            case "9802":
            case "9863":
            case "15353":
            case "28614":
            case "28613":
            case "3254":
            case "10553":
            case "1046":
            case "11301":
            case "11303":
            case "1203":
            case "12105":
            case "12335":
            case "13012":
            case "13057":
            case "13251":
            case "13305":
            case "1354":
            case "14018":
            case "14056":
            case "14158":
            case "15012":
            case "151":
            case "152":
            case "15202":
            case "1065":
            case "12314":
            case "16043":
            case "17241":
            case "17242":
            case "24154":
            case "26202":
            case "26203":
            case "28683":
            case "28782":
            case "28783":
            case "28784":
            case "6308":
            case "21254":
            case "25161":
            case "25354":
            case "3015":
            case "5094":
            case "6155":
            case "153":
            case "16008":
            case "16103":
            case "16202":
            case "8313":
            case "17162":
            case "17203":
            case "18083":
            case "18095":
            case "20020":
            case "21021":
            case "22082":
            case "17243":
            case "22253":
            case "22403":
            case "23005":
            case "23401":
            case "23603":
            case "25023":
            case "25071":
            case "26091":
            case "25355":
            case "26033":
            case "26262":
            case "27073":
            case "27174":
            case "29943":
            case "3082":
            case "3134":
            case "4002":
            case "4063":
            case "4303":
            case "4433":
            case "4534":
            case "5005":
            case "5402":
            case "6035":
            case "6115":
            case "6112":
            case "7311":
            case "8254":
            case "10162":
            case "10451":
            case "1064":
            case "1087":
            case "11203":
            case "11251":
            case "11302":
            case "12141":
            case "12333":
            case "13013":
            case "15253":
            case "16164":
            case "23555":
            case "26073":
            case "5066":
            case "16044":
            case "16165":
            case "22404":
            case "22453":
            case "26263":
            case "15301":
            case "17321":
            case "2207":
            case "22353":
            case "22304":
            case "22354":
            case "23553":
            case "25072":
            case "4005":
            case "20044":
            case "20303":
            case "26092":
            case "26093":
            case "28243":
            case "28244":
            case "8102":
            case "9304":
            case "9814":
            case "1047":
            case "22204":
            case "28187":
            case "15103":
            case "18122":
            case "25196":
            case "6309":
            case "10403":
            case "13103":
            case "15201":
            case "2018":
            case "21255":
            case "22454":
            case "24242":
            case "26261":
            case "29944":
            case "29945":
            case "7208":
            case "7209":
            case "7210":
            case "7214":
            case "8316":
            case "9301":
            case "9302":
            case "9867":
            case "9885":
            case "5303":
            case "17303":
            case "6113":
            case "6301":
            case "6305":
            case "7024":
            case "14104":
            case "27171":
                BuildMatchingModEffect_001(description, effect, isGolemMinion, abilityList, dynamicCombatEffectList, staticCombatEffectList, targetAbilityList, out pgCombatModEx);
                break;
            case "1202":
            case "14151":
            case "14152":
            case "14153":
            case "14154":
            case "14155":
            case "14156":
                BuildMatchingModEffect_001(description, effect, isGolemMinion, abilityList, dynamicCombatEffectList, staticCombatEffectList, targetAbilityList, out pgCombatModEx, disallowPrevioustarget: true);
                break;
            case "1023":
            case "163":
            case "15551":
            case "28762":
            case "2021":
            case "20353":
            case "28611":
            case "1061":
            case "16123":
            case "17042":
            case "10551":
            case "1081":
            case "13018":
            case "13204":
            case "16104":
            case "15305":
            case "16203":
            case "18085":
            case "18094":
            case "20105":
            case "21081":
            case "21154":
            case "2209":
            case "25006":
            case "27076":
            case "28625":
            case "28741":
            case "3132":
            case "4062":
            case "7022":
            case "16112":
            case "1022":
            case "16222":
            case "2057":
            case "21357":
            case "26034":
            case "5093":
            case "5201":
            case "9862":
            case "9873":
                BuildMatchingModEffect_001(description, effect, isGolemMinion, abilityList, dynamicCombatEffectList, staticCombatEffectList, targetAbilityList, out pgCombatModEx, ignoreModifierIndex: 0);
                break;
            case "5006":
            case "8318":
            case "13054":
            case "15102":
            case "17043":
            case "10044":
            case "1063":
            case "14055":
            case "1502":
            case "28062":
            case "6002":
            case "10504":
            case "16243":
            case "18035":
            case "18036":
            case "21023":
            case "2204":
            case "22085":
            case "3306":
            case "6116":
            case "1082":
            case "2058":
            case "27055":
            case "28124":
            case "15033":
            case "28765":
            case "28766":
            case "3023":
            case "5035":
            case "25222":
                BuildMatchingModEffect_001(description, effect, isGolemMinion, abilityList, dynamicCombatEffectList, staticCombatEffectList, targetAbilityList, out pgCombatModEx, ignoreModifierIndex: 1);
                break;
            case "25007":
            case "25008":
                BuildMatchingModEffect_001(description, effect, isGolemMinion, abilityList, dynamicCombatEffectList, staticCombatEffectList, targetAbilityList, out pgCombatModEx, ignoreModifierIndex: 1);
                pgCombatModEx.DynamicEffects[0].ConditionList.AddRange(pgCombatModEx.DynamicEffects[1].ConditionList);
                break;
            case "25223":
            case "4004":
            case "4471":
            case "8022":
            case "8023":
            case "9752":
            case "1303":
            case "3047":
            case "16082":
            case "9605":
            case "11254":
            case "13302":
            case "23003":
            case "3016":
            case "3204":
            case "3303":
            case "3403":
            case "14353":
            case "13107":
            case "2009":
            case "21002":
                BuildMatchingModEffect_001(description, effect, isGolemMinion, abilityList, dynamicCombatEffectList, staticCombatEffectList, targetAbilityList, out pgCombatModEx, ignoreModifierIndex: 2);
                break;
            case "28615":
                BuildMatchingModEffect_001(description, effect, isGolemMinion, abilityList, dynamicCombatEffectList, staticCombatEffectList, targetAbilityList, out pgCombatModEx, ignoreModifierIndex: 3);
                pgCombatModEx.DynamicEffects[1].AbilityList.Clear();
                break;
            case "26224":
            case "13403":
            case "15404":
            case "15405":
                BuildMatchingModEffect_001(description, effect, isGolemMinion, abilityList, dynamicCombatEffectList, staticCombatEffectList, targetAbilityList, out pgCombatModEx, ignoreModifierIndex: 1000);
                break;
            case "15254":
                BuildMatchingModEffect_001(description, effect, isGolemMinion, abilityList, dynamicCombatEffectList, staticCombatEffectList, targetAbilityList, out pgCombatModEx, ignoreModifierIndex: 2000);
                break;
            case "12092":
            case "13004":
                BuildMatchingModEffect_001(description, effect, isGolemMinion, abilityList, dynamicCombatEffectList, staticCombatEffectList, targetAbilityList, out pgCombatModEx, disallowPrevioustarget: true);
                break;
            case "12121":
                staticCombatEffectList.Insert(2, staticCombatEffectList[0]);
                BuildMatchingModEffect_001(description, effect, isGolemMinion, abilityList, dynamicCombatEffectList, staticCombatEffectList, targetAbilityList, out pgCombatModEx);
                break;
            case "15107":
                BuildMatchingModEffect_001(description, effect, isGolemMinion, abilityList, dynamicCombatEffectList, staticCombatEffectList, abilityList, out pgCombatModEx, ignoreModifierIndex: 1000);
                break;
            case "1067":
                BuildMatchingModEffect_001(description, effect, isGolemMinion, abilityList, dynamicCombatEffectList, staticCombatEffectList, targetAbilityList, out pgCombatModEx);
                pgCombatModEx.DynamicEffects[1].AbilityList.Clear();
                break;
            case "17063":
            case "28064":
                BuildMatchingModEffect_001(description, effect, isGolemMinion, targetAbilityList, dynamicCombatEffectList, staticCombatEffectList, abilityList, out pgCombatModEx);
                break;
            case "27175":
            case "21004":
            case "21007":
                staticCombatEffectList.Insert(2, staticCombatEffectList[0]);
                staticCombatEffectList.RemoveAt(0);
                BuildMatchingModEffect_001(description, effect, isGolemMinion, abilityList, dynamicCombatEffectList, staticCombatEffectList, targetAbilityList, out pgCombatModEx);
                break;
            case "28184":
                BuildMatchingModEffect_001(description, effect, isGolemMinion, abilityList, staticCombatEffectList, dynamicCombatEffectList, targetAbilityList, out pgCombatModEx);
                break;
            case "3043":
            case "9404":
                BuildMatchingModEffect_001(description, effect, isGolemMinion, abilityList, staticCombatEffectList, dynamicCombatEffectList, targetAbilityList, out pgCombatModEx, ignoreModifierIndex: 0);
                break;
            case "24033":
                BuildMatchingModEffect_001(description, effect, isGolemMinion, abilityList, dynamicCombatEffectList, staticCombatEffectList, targetAbilityList, out pgCombatModEx);
                pgCombatModEx.DynamicEffects[0] = new()
                {
                    AbilityList = pgCombatModEx.DynamicEffects[0].AbilityList,
                    Data = pgCombatModEx.DynamicEffects[0].Data,
                    Keyword = pgCombatModEx.DynamicEffects[0].Keyword,
                    RandomChance = pgCombatModEx.DynamicEffects[0].RandomChance,
                };
                break;
            case "28687":
                BuildMatchingModEffect_001(description, effect, isGolemMinion, abilityList, dynamicCombatEffectList, new() { new() { Keyword = CombatKeywordEx.OnTrigger, Data = new() }, staticCombatEffectList[0], staticCombatEffectList[1] }, abilityList, out pgCombatModEx);
                BuildMatchingModEffect_001(description, effect, isGolemMinion, abilityList, dynamicCombatEffectList, new() { new() { Keyword = CombatKeywordEx.OnTrigger, Data = new() }, staticCombatEffectList[2] }, targetAbilityList, out pgExtraCombatModEx);
                pgExtraCombatModEx.DynamicEffects[0].ConditionAbilityList.Clear();
                pgExtraCombatModEx.DynamicEffects[0].ConditionAbilityList.AddRange(pgCombatModEx.DynamicEffects[0].ConditionAbilityList);
                pgCombatModEx.DynamicEffects.AddRange(pgExtraCombatModEx.DynamicEffects);
                break;
            case "25103":
                BuildMatchingModEffect_001(description, effect, isGolemMinion, abilityList, dynamicCombatEffectList, new() { staticCombatEffectList[0], staticCombatEffectList[1] }, targetAbilityList, out pgCombatModEx);
                BuildMatchingModEffect_001(description, effect, isGolemMinion, abilityList, dynamicCombatEffectList, new() { staticCombatEffectList[2], staticCombatEffectList[3] }, targetAbilityList, out pgExtraCombatModEx);
                pgCombatModEx.DynamicEffects.AddRange(pgExtraCombatModEx.DynamicEffects);
                break;
            case "28161":
            case "28162":
            case "28164":
                BuildMatchingModEffect_001(description, effect, isGolemMinion, abilityList, dynamicCombatEffectList, new() { staticCombatEffectList[0], staticCombatEffectList[1] }, new(), out pgCombatModEx);
                BuildMatchingModEffect_001(description, effect, isGolemMinion, targetAbilityList, dynamicCombatEffectList, new() { staticCombatEffectList[2] }, new(), out pgExtraCombatModEx);
                pgCombatModEx.DynamicEffects.AddRange(pgExtraCombatModEx.DynamicEffects);
                break;
            case "10045":
                if (staticCombatEffectList.Count == 4)
                {
                    BuildMatchingModEffect_001(description, effect, isGolemMinion, abilityList, new() { staticCombatEffectList[0], staticCombatEffectList[1] }, new(), targetAbilityList, out pgCombatModEx);
                    BuildMatchingModEffect_004(description, effect, abilityList, new(), new() { staticCombatEffectList[2], staticCombatEffectList[3] }, targetAbilityList, out pgExtraCombatModEx);
                }
                else
                {
                    BuildMatchingModEffect_001(description, effect, isGolemMinion, abilityList, new() { dynamicCombatEffectList[0], staticCombatEffectList[0] }, new(), targetAbilityList, out pgCombatModEx);
                    BuildMatchingModEffect_004(description, effect, abilityList, new(), new() { staticCombatEffectList[1], staticCombatEffectList[2] }, targetAbilityList, out pgExtraCombatModEx);
                }
                pgCombatModEx.DynamicEffects.AddRange(pgExtraCombatModEx.DynamicEffects);
                pgCombatModEx.DynamicEffects[0].TargetAbilityList.Clear();
                break;
            case "16006":
                BuildMatchingModEffect_001(description, effect, isGolemMinion, abilityList, dynamicCombatEffectList, new() { staticCombatEffectList[0] }, new(), out pgCombatModEx);
                BuildMatchingModEffect_001(description, effect, isGolemMinion, new(), dynamicCombatEffectList, new() { staticCombatEffectList[1] }, targetAbilityList, out pgExtraCombatModEx);
                pgCombatModEx.DynamicEffects.AddRange(pgExtraCombatModEx.DynamicEffects);
                break;
            case "28626":
                if (dynamicCombatEffectList.Count == 1)
                {
                    BuildMatchingModEffect_001(description, effect, isGolemMinion, abilityList, new(), new() { dynamicCombatEffectList[0], staticCombatEffectList[0] }, targetAbilityList, out pgCombatModEx);
                    BuildMatchingModEffect_001(description, effect, isGolemMinion, abilityList, new(), new() { staticCombatEffectList[1], staticCombatEffectList[2] }, targetAbilityList, out pgExtraCombatModEx);
                    pgCombatModEx.DynamicEffects.AddRange(pgExtraCombatModEx.DynamicEffects);
                }
                else
                {
                    BuildMatchingModEffect_001(description, effect, isGolemMinion, abilityList, new(), new() { staticCombatEffectList[0], staticCombatEffectList[1] }, targetAbilityList, out pgCombatModEx);
                    BuildMatchingModEffect_001(description, effect, isGolemMinion, abilityList, new(), new() { staticCombatEffectList[2], staticCombatEffectList[3] }, targetAbilityList, out pgExtraCombatModEx);
                    pgCombatModEx.DynamicEffects.AddRange(pgExtraCombatModEx.DynamicEffects);
                }
                break;
            case "11502":
            case "2022":
            case "4201":
            case "14402":
                BuildMatchingModEffect_003(description, effect, isGolemMinion, abilityList, dynamicCombatEffectList, staticCombatEffectList, targetAbilityList, out pgCombatModEx);
                break;
            case "14205":
            case "28661":
                BuildMatchingModEffect_003(description, effect, isGolemMinion, abilityList, dynamicCombatEffectList, staticCombatEffectList, targetAbilityList, out pgCombatModEx, ignoreModifierIndex: 0);
                break;
            case "21083":
                BuildMatchingModEffect_003(description, effect, isGolemMinion, abilityList, dynamicCombatEffectList, staticCombatEffectList, targetAbilityList, out pgCombatModEx);
                pgCombatModEx.DynamicEffects[0].ConditionList.Clear();
                break;
            case "10401":
            case "11404":
            case "12122":
            case "5124":
            case "5152":
            case "9008":
            case "12051":
            case "4502":
            case "1086":
            case "1503":
            case "16012":
            case "17081":
            case "20012":
            case "25102":
            case "26113":
            case "5007":
            case "6154":
            case "6307":
            case "6302":
            case "8317":
            case "9854":
            case "12317":
            case "11455":
                BuildMatchingModEffect_004(description, effect, abilityList, dynamicCombatEffectList, staticCombatEffectList, targetAbilityList, out pgCombatModEx);
                break;
            case "10313":
            case "10510":
                BuildMatchingModEffect_004(description, effect, abilityList, staticCombatEffectList, dynamicCombatEffectList, targetAbilityList, out pgCombatModEx);
                break;
            case "22401":
                BuildMatchingModEffect_004(description, effect, abilityList, dynamicCombatEffectList, new() { staticCombatEffectList[0], new() { Keyword = CombatKeywordEx.RequireTargetOfAbility }, staticCombatEffectList[1] }, targetAbilityList, out pgCombatModEx);
                pgCombatModEx.DynamicEffects[1].AbilityList.Clear();
                break;
            case "20004":
                BuildMatchingModEffect_004(description, effect, abilityList, dynamicCombatEffectList, new() { staticCombatEffectList[2], staticCombatEffectList[0] }, targetAbilityList, out pgCombatModEx);
                break;
            case "14354":
                if (staticCombatEffectList.Count == 3)
                    BuildMatchingModEffect_004(description, effect, abilityList, new() { staticCombatEffectList[0] }, new() { staticCombatEffectList[1], new() { Keyword = CombatKeywordEx.ApplyToSelf }, staticCombatEffectList[2] }, targetAbilityList, out pgCombatModEx);
                else
                    BuildMatchingModEffect_004(description, effect, abilityList, new() { dynamicCombatEffectList[0] }, new() { staticCombatEffectList[0], new() { Keyword = CombatKeywordEx.ApplyToSelf }, staticCombatEffectList[1] }, targetAbilityList, out pgCombatModEx);
                break;
            case "26052":
                BuildMatchingModEffect_004(description, effect, abilityList, new(), new() { new() { Keyword = CombatKeywordEx.ApplyToSelf }, dynamicCombatEffectList[0], staticCombatEffectList[0], new() { Keyword = CombatKeywordEx.RequireSameTarget } }, abilityList, out pgCombatModEx);
                break;
            case "16013":
                BuildMatchingModEffect_004(description, effect, abilityList, new(), new() { staticCombatEffectList[0], staticCombatEffectList[1] }, targetAbilityList, out pgCombatModEx);
                BuildMatchingModEffect_004(description, effect, abilityList, new(), new() { staticCombatEffectList[0], staticCombatEffectList[2], staticCombatEffectList[3] }, targetAbilityList, out pgExtraCombatModEx);
                pgCombatModEx.DynamicEffects.AddRange(pgExtraCombatModEx.DynamicEffects);
                break;
            case "5401":
                BuildMatchingModEffect_004(description, effect, abilityList, new(), new() { dynamicCombatEffectList[0], staticCombatEffectList[0], new() { Keyword = CombatKeywordEx.ApplyToSelf } }, targetAbilityList, out pgCombatModEx);
                break;
            case "7401":
            case "7402":
                BuildMatchingModEffect_004(description, effect, abilityList, new(), new() { new() { Keyword = CombatKeywordEx.EffectDuration, Data = new() { RawValue = MutationDuration } }, staticCombatEffectList[1] }, new() { AbilityKeyword.Kick }, out pgCombatModEx);
                break;
            case "7431":
            case "7471":
                BuildMatchingModEffect_004(description, effect, abilityList, new(), new() { new() { Keyword = CombatKeywordEx.EffectDuration, Data = new() { RawValue = MutationDuration } }, staticCombatEffectList[0], staticCombatEffectList[1] }, targetAbilityList, out pgCombatModEx);
                break;
            case "7481":
                BuildMatchingModEffect_004(description, new(), abilityList, new(), new() { new() { Keyword = CombatKeywordEx.EffectDuration, Data = new() { RawValue = MutationDuration } }, staticCombatEffectList[0], staticCombatEffectList[1] }, targetAbilityList, out pgCombatModEx);
                break;
            case "7472":
            case "7482":
                BuildMatchingModEffect_004(description, effect, abilityList, new(), new() { new() { Keyword = CombatKeywordEx.EffectDuration, Data = new() { RawValue = MutationDuration } }, staticCombatEffectList[0] }, targetAbilityList, out pgCombatModEx);
                break;
            case "7432":
                staticCombatEffectList[0].DamageType = GameDamageType.Slashing;
                BuildMatchingModEffect_004(description, new(), abilityList, new(), new() { new() { Keyword = CombatKeywordEx.EffectDuration, Data = new() { RawValue = MutationDuration } }, staticCombatEffectList[0] }, targetAbilityList, out pgCombatModEx);
                staticCombatEffectList[0].Keyword = CombatKeywordEx.AddMitigationDirect;
                staticCombatEffectList[0].DamageType = GameDamageType.Acid;
                BuildMatchingModEffect_004(description, new(), abilityList, new(), new() { new() { Keyword = CombatKeywordEx.EffectDuration, Data = new() { RawValue = MutationDuration } }, staticCombatEffectList[0] }, targetAbilityList, out pgExtraCombatModEx);
                pgCombatModEx.DynamicEffects.AddRange(pgExtraCombatModEx.DynamicEffects);
                break;
            case "7433":
                staticCombatEffectList[0].DamageType = GameDamageType.Piercing;
                BuildMatchingModEffect_004(description, new(), abilityList, new(), new() { new() { Keyword = CombatKeywordEx.EffectDuration, Data = new() { RawValue = MutationDuration } }, staticCombatEffectList[0] }, targetAbilityList, out pgCombatModEx);
                staticCombatEffectList[0].Keyword = CombatKeywordEx.AddMitigationDirect;
                staticCombatEffectList[0].DamageType = GameDamageType.Poison;
                BuildMatchingModEffect_004(description, new(), abilityList, new(), new() { new() { Keyword = CombatKeywordEx.EffectDuration, Data = new() { RawValue = MutationDuration } }, staticCombatEffectList[0] }, targetAbilityList, out pgExtraCombatModEx);
                pgCombatModEx.DynamicEffects.AddRange(pgExtraCombatModEx.DynamicEffects);
                break;
            case "7483":
                BuildMatchingModEffect_004(description, effect, abilityList, new() { staticCombatEffectList[0] }, new() { new() { Keyword = CombatKeywordEx.EffectDuration, Data = new() { RawValue = MutationDuration } } }, targetAbilityList, out pgCombatModEx, ignoreModifierIndex: 0);
                pgCombatModEx.DynamicEffects.Add(pgCombatModEx.DynamicEffects[0]);
                pgCombatModEx.DynamicEffects.RemoveAt(0);
                pgCombatModEx.DynamicEffects[1].AbilityList.Clear();
                break;
            case "7009":
                BuildMatchingModEffect_004(description, effect, abilityList, new() { dynamicCombatEffectList[0] }, new() { dynamicCombatEffectList[2], dynamicCombatEffectList[1], staticCombatEffectList[0] }, targetAbilityList, out pgCombatModEx, ignoreModifierIndex: 0);
                break;
            case "7215":
            case "7216":
                if (staticCombatEffectList.Count == 5)
                    BuildMatchingModEffect_004(description, effect, abilityList, new() { staticCombatEffectList[0] }, new() { staticCombatEffectList[2], staticCombatEffectList[1], staticCombatEffectList[3], staticCombatEffectList[4] }, targetAbilityList, out pgCombatModEx, ignoreModifierIndex: 0);
                else
                    BuildMatchingModEffect_004(description, effect, abilityList, new() { dynamicCombatEffectList[0] }, new() { dynamicCombatEffectList[2], dynamicCombatEffectList[1], staticCombatEffectList[0], staticCombatEffectList[1] }, targetAbilityList, out pgCombatModEx, ignoreModifierIndex: 0);
                break;
            case "7491":
                BuildMatchingModEffect_004(description, effect, abilityList, new() { dynamicCombatEffectList[0], dynamicCombatEffectList[1] }, new() { new() { Keyword = CombatKeywordEx.EffectDuration, Data = new() { RawValue = MutationDuration } }, dynamicCombatEffectList[2] }, targetAbilityList, out pgCombatModEx);
                break;
            case "16009":
                if (staticCombatEffectList.Count == 4)
                    BuildMatchingModEffect_004(description, effect, abilityList, new(), new() { staticCombatEffectList[0], new() { Keyword = CombatKeywordEx.LastingMark, Data = new() }, staticCombatEffectList[2], staticCombatEffectList[3] }, new(), out pgCombatModEx);
                else
                    BuildMatchingModEffect_004(description, effect, abilityList, new(), new() { dynamicCombatEffectList[0], new() { Keyword = CombatKeywordEx.LastingMark, Data = new() }, staticCombatEffectList[0], staticCombatEffectList[1] }, new(), out pgCombatModEx);
                break;
            case "17083":
                BuildMatchingModEffect_004(description, effect, abilityList, new(), new() { new() { Keyword = CombatKeywordEx.ApplyToAllies, Data = new() }, new() { Keyword = CombatKeywordEx.WhilePlayingSong, Data = new() }, staticCombatEffectList[0] }, targetAbilityList, out pgCombatModEx);
                break;
            case "17084":
                BuildMatchingModEffect_004(description, effect, abilityList, new(), new() { new() { Keyword = CombatKeywordEx.WhilePlayingSong, Data = new() }, dynamicCombatEffectList[0], staticCombatEffectList[0] }, targetAbilityList, out pgCombatModEx);
                break;
            case "21041":
                BuildMatchingModEffect_004(description, effect, abilityList, new() { staticCombatEffectList[0] }, new() { staticCombatEffectList[1], staticCombatEffectList[2], staticCombatEffectList[3] }, targetAbilityList, out pgCombatModEx);
                break;
            case "14202":
                if (dynamicCombatEffectList.Count == 2)
                    BuildMatchingModEffect_004(description, effect, abilityList, new() { dynamicCombatEffectList[0] }, new() { new() { Keyword = CombatKeywordEx.ApplyToSelf }, staticCombatEffectList[0], dynamicCombatEffectList[1] }, targetAbilityList, out pgCombatModEx, ignoreModifierIndex: 0);
                else
                    BuildMatchingModEffect_004(description, effect, abilityList, new() { dynamicCombatEffectList[0] }, new() { new() { Keyword = CombatKeywordEx.ApplyToSelf }, staticCombatEffectList[1], staticCombatEffectList[0] }, targetAbilityList, out pgCombatModEx, ignoreModifierIndex: 0);
                break;
            case "20406":
                BuildMatchingModEffect_004(description, effect, abilityList, new() { staticCombatEffectList[0], staticCombatEffectList[1] }, new() { new() { Keyword = CombatKeywordEx.NextAttack }, staticCombatEffectList[2] }, targetAbilityList, out pgCombatModEx);
                break;
            case "12313":
            case "28141":
            case "28142":
            case "12309":
                BuildMatchingModEffect_005(description, isGolemMinion, abilityList, dynamicCombatEffectList, staticCombatEffectList, targetAbilityList, out pgCombatModEx);
                break;
            case "28143":
                BuildMatchingModEffect_005(description, isGolemMinion, abilityList, new PgCombatEffectCollectionEx() { dynamicCombatEffectList[0], staticCombatEffectList[0] }, new(), targetAbilityList, out pgCombatModEx);
                break;
            case "24246":
            case "24247":
                BuildMatchingModEffect_001(description, effect, isGolemMinion, abilityList, dynamicCombatEffectList, new(), targetAbilityList, out pgCombatModEx);
                BuildMatchingModEffect_005(description, isGolemMinion, abilityList, staticCombatEffectList, new(), targetAbilityList, out pgExtraCombatModEx);
                pgCombatModEx.PermanentEffects.AddRange(pgExtraCombatModEx.PermanentEffects);
                break;
            case "28641":
            case "28645":
                BuildMatchingModEffect_001(description, effect, isGolemMinion, abilityList, dynamicCombatEffectList, new(), targetAbilityList, out pgCombatModEx);
                BuildMatchingModEffect_005(description, isGolemMinion, new() { AbilityKeyword.SummonedTornado }, staticCombatEffectList, new(), targetAbilityList, out pgExtraCombatModEx);
                pgCombatModEx.PermanentEffects.AddRange(pgExtraCombatModEx.PermanentEffects);
                break;
            case "27134":
            case "27135":
                BuildMatchingModEffect_001(description, effect, isGolemMinion, abilityList, new() { staticCombatEffectList[1], staticCombatEffectList[2] }, new(), targetAbilityList, out pgCombatModEx);
                BuildMatchingModEffect_005(description, isGolemMinion, abilityList, new() { staticCombatEffectList[0] }, new(), targetAbilityList, out pgExtraCombatModEx);
                pgCombatModEx.PermanentEffects.AddRange(pgExtraCombatModEx.PermanentEffects);
                break;
            case "18115":
                BuildMatchingModEffect_005(description, isGolemMinion, new(), new() { staticCombatEffectList[0], new PgCombatEffectEx() { Keyword = CombatKeywordEx.RequireBloodMistForm } }, new() , new(), out pgCombatModEx);
                break;
            case "12301":
            case "21201":
            case "28644":
            case "12302":
            case "12312":
            case "23302":
            case "14601":
            case "14602":
            case "14603":
            case "23301":
            case "28202":
            case "9881":
            case "9883":
            case "15505":
            case "14605":
            case "8353":
            case "12305":
            case "12307":
            case "12308":
            case "15501":
            case "8308":
            case "8352":
            case "28201":
            case "28203":
            case "15503":
            case "8301":
            case "8302":
            case "8304":
            case "7301":
            case "23305":
            case "21203":
            case "8351":
            case "12303":
            case "12304":
            case "12306":
            case "12315":
            case "12316":
            case "21202":
            case "23304":
            case "24245":
            case "27131":
            case "27132":
            case "7302":
            case "8319":
            case "8321":
            case "8322":
            case "23303":
            case "8004":
            case "8303":
            case "12311":
            case "15502":
            case "12310":
                BuildMatchingModEffect_006(description, isGolemMinion, abilityList, dynamicCombatEffectList, staticCombatEffectList, targetAbilityList, out pgCombatModEx);
                break;
            case "12319":
                BuildMatchingModEffect_006(description, isGolemMinion, abilityList, dynamicCombatEffectList, staticCombatEffectList, targetAbilityList, out pgCombatModEx);
                break;
            case "21204":
            case "28642":
                BuildMatchingModEffect_006(description, isGolemMinion, abilityList, dynamicCombatEffectList, staticCombatEffectList, targetAbilityList, out pgCombatModEx, ignoreModifierIndex: 0);
                break;
            case "15504":
                BuildMatchingModEffect_006(description, isGolemMinion, abilityList, dynamicCombatEffectList, staticCombatEffectList, targetAbilityList, out pgCombatModEx);
                pgCombatModEx.PermanentEffects.Add(pgCombatModEx.PermanentEffects[1]);
                pgCombatModEx.PermanentEffects.RemoveAt(1);
                break;
            case "28643":
                BuildMatchingModEffect_006(description, isGolemMinion, abilityList, dynamicCombatEffectList, new() { dynamicCombatEffectList[0], staticCombatEffectList[0] }, targetAbilityList, out pgCombatModEx);
                break;
            case "7303":
            case "7304":
            case "7305":
                BuildMatchingModEffect_006(description, isGolemMinion, new(), dynamicCombatEffectList, new() { staticCombatEffectList[0] }, targetAbilityList, out pgCombatModEx);
                BuildMatchingModEffect_001(description, effect, isGolemMinion, abilityList, dynamicCombatEffectList, new() { staticCombatEffectList[1] }, targetAbilityList, out pgExtraCombatModEx);
                pgCombatModEx.DynamicEffects.AddRange(pgExtraCombatModEx.DynamicEffects);
                break;
            case "2301":
            case "2302":
            case "2303":
                BuildMatchingModEffect_006(description, isGolemMinion, abilityList, dynamicCombatEffectList, new() { staticCombatEffectList[0] }, new(), out pgCombatModEx);
                BuildMatchingModEffect_001(description, effect, isGolemMinion, targetAbilityList, dynamicCombatEffectList, new() { staticCombatEffectList[1] }, new(), out pgExtraCombatModEx);
                pgCombatModEx.DynamicEffects.AddRange(pgExtraCombatModEx.DynamicEffects);
                break;
            case "8001":
                BuildMatchingModEffect_001(description, effect, isGolemMinion, new(), dynamicCombatEffectList, new(), targetAbilityList, out pgExtraCombatModEx);
                BuildMatchingModEffect_006(description, isGolemMinion, abilityList, new(), staticCombatEffectList, targetAbilityList, out pgCombatModEx);
                pgCombatModEx.DynamicEffects.AddRange(pgExtraCombatModEx.DynamicEffects);
                break;
            case "11503":
                BuildMatchingModEffect_007(description, effect, abilityList, dynamicCombatEffectList, staticCombatEffectList, targetAbilityList, out pgCombatModEx);
                break;
            case "18064":
            case "18065":
                BuildMatchingModEffect_007(description, effect, abilityList, dynamicCombatEffectList, staticCombatEffectList, targetAbilityList, out pgCombatModEx);
                break;
            case "28686":
                BuildMatchingModEffect_008(description, effect, abilityList, dynamicCombatEffectList, staticCombatEffectList, targetAbilityList, new() { 0, 1 }, new() { 0, 2 }, inverseTargets: true, out pgCombatModEx);
                break;
            case "28785":
                BuildMatchingModEffect_008(description, effect, abilityList, dynamicCombatEffectList, new() { staticCombatEffectList[0], new() { Keyword = CombatKeywordEx.RequireBeingHit, Data = new() }, new() { Keyword = CombatKeywordEx.GiveBuff, Data = new() } }, targetAbilityList, new() { 0, 1, 4 }, new() { 3, 2 }, inverseTargets: false, out pgCombatModEx);
                break;
            case "28841":
                BuildMatchingModEffect_001(description, effect, isGolemMinion, abilityList, new() { staticCombatEffectList[0], staticCombatEffectList[1], staticCombatEffectList[2] }, new(), targetAbilityList, out pgCombatModEx);
                BuildMatchingModEffect_004(description, effect, abilityList, new(), new() { staticCombatEffectList[0], new() { Keyword = CombatKeywordEx.NextHit, Data = new() }, staticCombatEffectList[5] }, targetAbilityList, out pgExtraCombatModEx);
                pgCombatModEx.DynamicEffects.AddRange(pgExtraCombatModEx.DynamicEffects);
                break;
            case "13003":
                BuildMatchingModEffect_001(description, effect, isGolemMinion, abilityList, new() { dynamicCombatEffectList[0] }, new(), abilityList, out pgCombatModEx);
                BuildMatchingModEffect_004(description, effect, abilityList, new(), new() { new() { Keyword = CombatKeywordEx.ApplyToSelf, Data = new() }, dynamicCombatEffectList[1], staticCombatEffectList[0] }, targetAbilityList, out pgExtraCombatModEx);
                pgCombatModEx.DynamicEffects.AddRange(pgExtraCombatModEx.DynamicEffects);
                break;
            case "28685":
                BuildMatchingModEffect_008(description, effect, abilityList, dynamicCombatEffectList, new() { staticCombatEffectList[0], staticCombatEffectList[2], new() { Keyword = CombatKeywordEx.OnTrigger, Data = new() }, new() { Keyword = CombatKeywordEx.GiveBuffOneAttack, Data = new() }, new() { Keyword = CombatKeywordEx.RequireDamageType, DamageType = staticCombatEffectList[3].DamageType } }, targetAbilityList, new() { 0, 2, 3 }, new() { 4, 1 }, inverseTargets: false, out pgCombatModEx);
                break;
            case "155":
                BuildMatchingModEffect_008(description, effect, abilityList, dynamicCombatEffectList, new(), targetAbilityList, new() { 0 }, new() { 3, 2, 1 }, inverseTargets: false, out pgCombatModEx);
                pgCombatModEx.DynamicEffects[2].AbilityList.Clear();
                pgCombatModEx.DynamicEffects[2].AbilityList.AddRange(pgCombatModEx.DynamicEffects[2].TargetAbilityList);
                break;
            case "16024":
                BuildMatchingModEffect_008(description, effect, abilityList, dynamicCombatEffectList, staticCombatEffectList, targetAbilityList, new() { 0 }, new() { 3, 2, 1 }, inverseTargets: false, out pgCombatModEx);
                pgCombatModEx.DynamicEffects[2].AbilityList.Clear();
                break;
            case "18114":
                BuildMatchingModEffect_009(description, dynamicCombatEffectList, staticCombatEffectList, out pgCombatModEx);
                break;
            case "24122":
                BuildMatchingModEffect_008(description, effect, abilityList, dynamicCombatEffectList, staticCombatEffectList, targetAbilityList, new() { 0, 1 }, new() { 2, 3 }, inverseTargets: false, out pgCombatModEx);
                break;
            case "28063":
                BuildMatchingModEffect_008(description, effect, abilityList, dynamicCombatEffectList, staticCombatEffectList, targetAbilityList, new() { 2, 1 }, new() { 0 }, inverseTargets: false, out pgCombatModEx);
                break;
            case "12106":
                BuildMatchingModEffect_008(description, effect, abilityList, dynamicCombatEffectList, staticCombatEffectList, targetAbilityList, new() { 0, 1, 2 }, new() { 3, 4, 5 }, inverseTargets: false, out pgCombatModEx);
                break;
            case "6136":
            case "6153":
                BuildMatchingModEffect_008(description, effect, abilityList, dynamicCombatEffectList, new() { staticCombatEffectList[0], new() { Keyword = CombatKeywordEx.GiveBuff, Data = new() }, staticCombatEffectList[1], staticCombatEffectList[2], staticCombatEffectList[3] }, targetAbilityList, new() { 0, 1 }, new() { 2, 3, 4 }, inverseTargets: true, out pgCombatModEx);
                break;
            case "Other":
                BuildMatchingModEffect_001(description, effect, isGolemMinion, abilityList, dynamicCombatEffectList, staticCombatEffectList, targetAbilityList, out pgCombatModEx);
                break;
            default:
                pgCombatModEx = new PgCombatModEx() { Description = description, PermanentEffects = new(), DynamicEffects = new() };
                break;
        }
    }

    private void BuildMatchingModEffect_001(string description, PgEffect effect, bool isGolemMinion, List<AbilityKeyword> abilityList, PgCombatEffectCollectionEx staticCombatEffectList, PgCombatEffectCollectionEx dynamicCombatEffectList, List<AbilityKeyword> targetAbilityList, out PgCombatModEx pgCombatModEx, bool disallowPrevioustarget = false, int ignoreModifierIndex = -1)
    {
        List<PgCombatEffectEx> AllEffects = new(dynamicCombatEffectList);
        AllEffects.AddRange(staticCombatEffectList);

        float RandomChance = GetValueAndRemove(AllEffects, CombatKeywordEx.ApplyWithChance, asProbability: true);
        float DelayInSeconds = GetValueAndRemove(AllEffects, CombatKeywordEx.EffectDelay);
        float DurationInSeconds = GetValueAndRemove(AllEffects, CombatKeywordEx.EffectDuration);
        float DurationInMinutes = GetValueAndRemove(AllEffects, CombatKeywordEx.EffectDurationInMinutes);
        if (float.IsNaN(DurationInSeconds) && !float.IsNaN(DurationInMinutes))
            DurationInSeconds = DurationInMinutes * 60;
        float DurationOverTime = GetValueAndRemove(AllEffects, CombatKeywordEx.EffectOverTime);
        float RecurringDelay = GetValueAndRemove(AllEffects, CombatKeywordEx.RecurringEffect);
        float TargetRange = GetValueAndRemove(AllEffects, CombatKeywordEx.TargetRange);
        bool IsPerSecond = AllEffects.Exists(Item => Item.Keyword == CombatKeywordEx.EffectEverySecond);

        PgCombatConditionCollectionEx ConditionList = new();
        List<AbilityKeyword> ConditionAbilityList = new();
        float ConditionValue = float.NaN;
        float ConditionPercentage = float.NaN;
        int ConditionIndex = -1;
        GameDamageType ConditionDamageType = GameDamageType.Internal_None;
        GameCombatSkill ConditionSkill = GameCombatSkill.Internal_None;
        for (int i = 0; i< AllEffects.Count; i++)
        {
            PgCombatEffectEx CombatEffect = AllEffects[i];
            StringToEnumConversion<CombatKeywordEx>.SetCustomParsedEnum(CombatEffect.Keyword);

            if (KeywordToCondition.TryGetValue(CombatEffect.Keyword, out CombatCondition NewCondition))
            {
                Debug.Assert(NewCondition != CombatCondition.Internal_None);
                ConditionList.Add(NewCondition);
                ConditionIndex = i;

                if (NewCondition == CombatCondition.WhilePlayingSong ||
                    NewCondition == CombatCondition.TargetOfAbility ||
                    NewCondition == CombatCondition.AbilityNotTriggered ||
                    NewCondition == CombatCondition.AbilityTriggered ||
                    NewCondition == CombatCondition.StandingSomewhere)
                {
                    ConditionAbilityList = new(targetAbilityList);
                }

                if (NewCondition == CombatCondition.TargetHasLowRage ||
                    NewCondition == CombatCondition.TargetHasHighRage ||
                    NewCondition == CombatCondition.BelowMaxArmor ||
                    NewCondition == CombatCondition.BelowMaxRage)
                {
                    Debug.Assert(CombatEffect.Data.RawValue.HasValue);
                    Debug.Assert(CombatEffect.Data.RawIsPercent.HasValue);
                    ConditionPercentage = CombatEffect.Data.Value;
                }

                if (NewCondition == CombatCondition.MinimumDistance)
                {
                    Debug.Assert(CombatEffect.Data.RawValue.HasValue);
                    Debug.Assert(CombatEffect.Data.RawIsPercent.HasValue);
                    ConditionValue = CombatEffect.Data.Value;
                }

                if (NewCondition == CombatCondition.SpecificDirectDamageType)
                {
                    Debug.Assert(CombatEffect.DamageType != GameDamageType.Internal_None);
                    ConditionDamageType = CombatEffect.DamageType;
                }

                if (NewCondition == CombatCondition.ActiveSkill || NewCondition == CombatCondition.WhileChanneling || NewCondition == CombatCondition.UsingCombatSkill)
                {
                    ConditionSkill = CombatEffect.CombatSkill;
                }
            }
        }

        List<PgPermanentModEffectEx> PermanentEffects = new();
        for (int i = 0; i < AllEffects.Count; i++)
        {
            PgCombatEffectEx CombatEffect = AllEffects[i];
            CombatKeywordEx CombatKeyword = CombatEffect.Keyword;
            StringToEnumConversion<CombatKeywordEx>.SetCustomParsedEnum(CombatKeyword);

            if (CombatKeyword == CombatKeywordEx.BecomeBurst && !float.IsNaN(TargetRange) && ConditionList.TrueForAll(condition => condition != CombatCondition.MinimumDistance))
            {
                if (float.IsNaN(RandomChance))
                {
                    PgPermanentModEffectEx pgPermanentModEffectEx = new()
                    {
                        Keyword = CombatKeyword,
                        Data = new() { Value = TargetRange, IsPercent = false },
                        Target = CombatTarget.Self,
                        ConditionAbilityList = abilityList,
                    };

                    PermanentEffects.Add(pgPermanentModEffectEx);
                }

                AllEffects.RemoveAt(i);
                i--;
            }
        }

        List<PgCombatModEffectEx> DynamicEffects = new();
        bool IsTargetAbilityListUsed = false;
        GameDamageType PreviousDamageType = GameDamageType.Internal_None;
        PgNumericValueEx? PreviousCritChance = null;
        for (int i = 0; i < AllEffects.Count; i++)
        {
            PgCombatEffectEx CombatEffect = AllEffects[i];
            CombatKeywordEx CombatKeyword = CombatEffect.Keyword;
            bool CanApplyModifier = ignoreModifierIndex < 0 ||
                                    ((i != (ignoreModifierIndex % 1000)) &&
                                     (ignoreModifierIndex < 1000 || (i != ((ignoreModifierIndex / 1000) % 1000))));

            Debug.Assert(CombatKeyword != CombatKeywordEx.BecomeBurst || ConditionList.Exists(condition => condition == CombatCondition.MinimumDistance));

            if (CombatKeyword == CombatKeywordEx.ApplyWithChance ||
                CombatKeyword == CombatKeywordEx.ApplyToSelf ||
                CombatKeyword == CombatKeywordEx.ApplyToAllies ||
                CombatKeyword == CombatKeywordEx.ApplyToSelfAndAllies ||
                CombatKeyword == CombatKeywordEx.ApplyToPet ||
                CombatKeyword == CombatKeywordEx.ApplyToSelfAndPet ||
                CombatKeyword == CombatKeywordEx.ApplyToPetOfTarget ||
                CombatKeyword == CombatKeywordEx.TargetRange ||
                CombatKeyword == CombatKeywordEx.EffectDuration ||
                CombatKeyword == CombatKeywordEx.EffectDurationInMinutes ||
                CombatKeyword == CombatKeywordEx.EffectOverTime ||
                CombatKeyword == CombatKeywordEx.RecurringEffect ||
                CombatKeyword == CombatKeywordEx.EffectEverySecond ||
                CombatKeyword == CombatKeywordEx.EffectDelay ||
                CombatKeyword == CombatKeywordEx.RandomDamage ||
                CombatKeyword == CombatKeywordEx.ApplyToIndirect ||
                CombatKeyword == CombatKeywordEx.EveryOtherUse)
            {
                continue;
            }

            if (KeywordToCondition.TryGetValue(CombatKeyword, out CombatCondition NewCondition))
            {
                continue;
            }

            if (CombatKeyword == CombatKeywordEx.DirectDamageBoost && CombatEffect.DamageType == GameDamageType.Internal_None)
            {
                foreach (AbilityKeyword Keyword in abilityList)
                    Debug.Assert(AbilitiesDealingDirectDamage.Contains(Keyword));

                CombatKeyword = CombatKeywordEx.DamageBoost;
            }

            if (OverTimeEffects.TryGetValue(CombatKeyword, out CombatKeywordEx CombatKeywordOverTime))
            {
                if (float.IsNaN(DurationInSeconds) && !float.IsNaN(DurationOverTime))
                {
                    DurationInSeconds = DurationOverTime;
                    CombatKeyword = CombatKeywordOverTime;
                }
                else if (!float.IsNaN(DurationInSeconds) && IsPerSecond)
                {
                    CombatKeyword = CombatKeywordOverTime;
                }
            }

            bool CanHaveDuration = CombatKeyword == CombatKeywordEx.AddSprintSpeed ||
                                   CombatKeyword == CombatKeywordEx.AddFlySpeed ||
                                   CombatKeyword == CombatKeywordEx.AddSwimSpeed ||
                                   CombatKeyword == CombatKeywordEx.AddOutOfCombatSpeed ||
                                   CombatKeyword == CombatKeywordEx.RestoreHealthOverTime ||
                                   CombatKeyword == CombatKeywordEx.RestorePowerOverTime ||
                                   CombatKeyword == CombatKeywordEx.RestoreArmorOverTime ||
                                   CombatKeyword == CombatKeywordEx.RestoreHealthOrArmorOverTime ||
                                   CombatKeyword == CombatKeywordEx.DamageOverTimeBoost ||
                                   CombatKeyword == CombatKeywordEx.HealthDamageOverTimeBoost ||
                                   CombatKeyword == CombatKeywordEx.IncreaseMaxHealth ||
                                   CombatKeyword == CombatKeywordEx.IncreaseMaxArmor ||
                                   CombatKeyword == CombatKeywordEx.IncreaseMaxPower ||
                                   CombatKeyword == CombatKeywordEx.IncreaseMaxBreath ||
                                   CombatKeyword == CombatKeywordEx.IncreaseMaxRage ||
                                   CombatKeyword == CombatKeywordEx.RegenPercentageOfArmor ||
                                   CombatKeyword == CombatKeywordEx.IncreaseHealEfficiency ||
                                   CombatKeyword == CombatKeywordEx.IncreaseCurrentRefreshTime ||
                                   CombatKeyword == CombatKeywordEx.IncreaseChannelingTime ||
                                   CombatKeyword == CombatKeywordEx.StunImmunity ||
                                   CombatKeyword == CombatKeywordEx.StunResistance ||
                                   CombatKeyword == CombatKeywordEx.Knockback ||
                                   CombatKeyword == CombatKeywordEx.AddSprintPowerCost ||
                                   CombatKeyword == CombatKeywordEx.IncreaseAccuracy ||
                                   CombatKeyword == CombatKeywordEx.AddMeleeAccuracy ||
                                   CombatKeyword == CombatKeywordEx.IncreaseBurstAccuracy ||
                                   CombatKeyword == CombatKeywordEx.DamageBoost ||
                                   CombatKeyword == CombatKeywordEx.BaseDamageBoost ||
                                   CombatKeyword == CombatKeywordEx.DamageBoostDouble ||
                                   CombatKeyword == CombatKeywordEx.DealArmorDamage ||
                                   CombatKeyword == CombatKeywordEx.DealHealthDamage ||
                                   CombatKeyword == CombatKeywordEx.DealHealthAndArmorDamage ||
                                   CombatKeyword == CombatKeywordEx.DirectDamageBoost ||
                                   CombatKeyword == CombatKeywordEx.IndirectDamageBoost ||
                                   CombatKeyword == CombatKeywordEx.IncreasePowerCost ||
                                   CombatKeyword == CombatKeywordEx.AddMitigation ||
                                   CombatKeyword == CombatKeywordEx.AddMitigationDirect ||
                                   CombatKeyword == CombatKeywordEx.AddMitigationIndirect ||
                                   CombatKeyword == CombatKeywordEx.AddMitigationBurst ||
                                   CombatKeyword == CombatKeywordEx.AddMitigationPhysical ||
                                   CombatKeyword == CombatKeywordEx.AddMitigationElemental ||
                                   CombatKeyword == CombatKeywordEx.BestowProtectiveBubble ||
                                   CombatKeyword == CombatKeywordEx.GenerateTaunt ||
                                   CombatKeyword == CombatKeywordEx.RageAttackBoost ||
                                   CombatKeyword == CombatKeywordEx.NonRageAttackBoost ||
                                   CombatKeyword == CombatKeywordEx.IncreaseRage ||
                                   CombatKeyword == CombatKeywordEx.IncreaseDrainHealthMax ||
                                   CombatKeyword == CombatKeywordEx.SelfDamageOverTime ||
                                   CombatKeyword == CombatKeywordEx.IncreaseMeleePowerCost ||
                                   CombatKeyword == CombatKeywordEx.IncreaseEvasion ||
                                   CombatKeyword == CombatKeywordEx.IncreaseEvasionBurst ||
                                   CombatKeyword == CombatKeywordEx.IncreaseEvasionProjectile ||
                                   CombatKeyword == CombatKeywordEx.IncreaseEvasionBurstAndProjectile ||
                                   CombatKeyword == CombatKeywordEx.IncreaseEvasionMelee ||
                                   CombatKeyword == CombatKeywordEx.IncreaseEvasionRanged ||
                                   CombatKeyword == CombatKeywordEx.IncreaseEliteResistance ||
                                   CombatKeyword == CombatKeywordEx.IncreaseCriticalChance ||
                                   CombatKeyword == CombatKeywordEx.GrantCriticalChance ||
                                   CombatKeyword == CombatKeywordEx.Fear ||
                                   CombatKeyword == CombatKeywordEx.SummonDeer ||
                                   CombatKeyword == CombatKeywordEx.IgnoreKnockback ||
                                   CombatKeyword == CombatKeywordEx.IgnoreStun ||
                                   CombatKeyword == CombatKeywordEx.IncreaseJumpHeight ||
                                   CombatKeyword == CombatKeywordEx.TornadoVulnerability ||
                                   CombatKeyword == CombatKeywordEx.IncreasePhysicalReflection ||
                                   CombatKeyword == CombatKeywordEx.DirectDamageImmunity ||
                                   CombatKeyword == CombatKeywordEx.AbsorbDamage ||
                                   CombatKeyword == CombatKeywordEx.IncreaseCombatRefreshHealing ||
                                   CombatKeyword == CombatKeywordEx.IncreaseCombatRefreshPowerRestore ||
                                   CombatKeyword == CombatKeywordEx.IncreaseArmorRegeneration ||
                                   CombatKeyword == CombatKeywordEx.GrantSlowRootImmunity ||
                                   CombatKeyword == CombatKeywordEx.IncreaseProtectionCold ||
                                   CombatKeyword == CombatKeywordEx.GetIgnored ||
                                   CombatKeyword == CombatKeywordEx.IncreaseXpGain ||
                                   CombatKeyword == CombatKeywordEx.FreeMovementWhileLeaping ||
                                   CombatKeyword == CombatKeywordEx.VileBloodDamage ||
                                   CombatKeyword == CombatKeywordEx.GiveBuff;
            bool CanHaveRange = CombatKeyword != CombatKeywordEx.IncreaseCurrentRefreshTime &&
                                CombatKeyword != CombatKeywordEx.IncreasePowerCost &&
                                CombatKeyword != CombatKeywordEx.IncreaseMeleePowerCost;
            bool CanHaveDelay = CombatKeyword != CombatKeywordEx.AddSprintSpeed;
            bool MustHaveDuration = CombatKeyword == CombatKeywordEx.AddSprintSpeed ||
                                   CombatKeyword == CombatKeywordEx.AddFlySpeed ||
                                   CombatKeyword == CombatKeywordEx.AddSwimSpeed ||
                                   CombatKeyword == CombatKeywordEx.AddOutOfCombatSpeed ||
                                   CombatKeyword == CombatKeywordEx.RestoreHealthOverTime ||
                                   CombatKeyword == CombatKeywordEx.RestorePowerOverTime ||
                                   CombatKeyword == CombatKeywordEx.RestoreArmorOverTime ||
                                   CombatKeyword == CombatKeywordEx.RestoreHealthOrArmorOverTime ||
                                   CombatKeyword == CombatKeywordEx.RegenPercentageOfArmor ||
                                   CombatKeyword == CombatKeywordEx.AddMitigation ||
                                   CombatKeyword == CombatKeywordEx.AddMitigationDirect ||
                                   CombatKeyword == CombatKeywordEx.AddMitigationIndirect ||
                                   CombatKeyword == CombatKeywordEx.AddMitigationBurst ||
                                   CombatKeyword == CombatKeywordEx.AddMitigationPhysical ||
                                   CombatKeyword == CombatKeywordEx.AddMitigationElemental ||
                                   CombatKeyword == CombatKeywordEx.SelfDamageOverTime ||
                                   CombatKeyword == CombatKeywordEx.IncreaseEvasion ||
                                   CombatKeyword == CombatKeywordEx.IncreaseEvasionBurst ||
                                   CombatKeyword == CombatKeywordEx.IncreaseEvasionProjectile ||
                                   CombatKeyword == CombatKeywordEx.IncreaseEvasionBurstAndProjectile ||
                                   CombatKeyword == CombatKeywordEx.IncreaseEvasionMelee ||
                                   CombatKeyword == CombatKeywordEx.IncreaseEvasionRanged ||
                                   CombatKeyword == CombatKeywordEx.IncreaseEliteResistance ||
                                   CombatKeyword == CombatKeywordEx.IgnoreKnockback ||
                                   CombatKeyword == CombatKeywordEx.IgnoreStun ||
                                   CombatKeyword == CombatKeywordEx.BestowProtectiveBubble;
            if (MustHaveDuration && float.IsNaN(DurationInSeconds) && ConditionList.TrueForAll(condition => condition != CombatCondition.AbilityNotTriggered) && effect.RawDuration.HasValue)
            {
                Debug.Assert(effect.Duration == -2);
                DurationInSeconds = 30;
            }

            float EffectValue = CombatEffect.Data.RawValue.HasValue ? CombatEffect.Data.RawValue.Value : float.NaN;
            bool EffectIsPercent = CombatEffect.Data.RawIsPercent.HasValue ? CombatEffect.Data.RawIsPercent.Value : false;

            if (IsPerSecond && !float.IsNaN(EffectValue) && !EffectIsPercent && !float.IsNaN(DurationInSeconds))
            {
                EffectValue *= DurationInSeconds;
            }

            if (CombatKeyword == CombatKeywordEx.DamageBoost && EffectValue == 0)
                continue;

            PgNumericValueEx pgNumericValueEx = new() { Value = EffectValue, IsPercent = EffectIsPercent };

            GetTargets(AllEffects, i + 1, abilityList, out CombatTarget Target, out CombatTarget OtherTarget, out bool IsEveryOtherUse);
            if (Target == CombatTarget.Internal_None && !disallowPrevioustarget)
                GetTargets(AllEffects, i - 1, abilityList, out Target, out OtherTarget, out IsEveryOtherUse);

            bool CanHaveTarget = (CombatKeyword != CombatKeywordEx.IncreaseCurrentRefreshTime || Target != CombatTarget.Self) &&
                                 (CombatKeyword != CombatKeywordEx.ResetRefreshTime || Target != CombatTarget.Self) &&
                                 (CombatKeyword != CombatKeywordEx.ZeroPowerCost || CanApplyModifier) &&
                                 (CombatKeyword != CombatKeywordEx.DamageBoost || CanApplyModifier) &&
                                 (CombatKeyword != CombatKeywordEx.DealArmorDamage || CanApplyModifier) &&
                                 (CombatKeyword != CombatKeywordEx.DealHealthDamage || CanApplyModifier) &&
                                 (CombatKeyword != CombatKeywordEx.DealHealthAndArmorDamage || CanApplyModifier) &&
                                 (CombatKeyword != CombatKeywordEx.IncreasePowerCost || CanApplyModifier) &&
                                 (CombatKeyword != CombatKeywordEx.AddOutOfCombatSpeed || CanApplyModifier) &&
                                 (CombatKeyword != CombatKeywordEx.NextAttackMiss || CanApplyModifier) &&
                                 (CombatKeyword != CombatKeywordEx.AddMitigation || CanApplyModifier) &&
                                 (CombatKeyword != CombatKeywordEx.AddMitigationDirect || CanApplyModifier) &&
                                 (CombatKeyword != CombatKeywordEx.AddMitigationIndirect || CanApplyModifier) &&
                                 (CombatKeyword != CombatKeywordEx.AddMitigationBurst || CanApplyModifier) &&
                                 (CombatKeyword != CombatKeywordEx.AddMitigationPhysical || CanApplyModifier) &&
                                 (CombatKeyword != CombatKeywordEx.AddMitigationElemental || CanApplyModifier) &&
                                 (CombatKeyword != CombatKeywordEx.IncreaseMeleePowerCost || CanApplyModifier);

            List<AbilityKeyword> TargetAbilityList = new();
            if (Target == CombatTarget.Internal_None &&
                (CombatKeyword == CombatKeywordEx.IncreaseCurrentRefreshTime ||
                 CombatKeyword == CombatKeywordEx.ResetRefreshTime ||
                 CombatKeyword == CombatKeywordEx.ZeroPowerCost ||
                 CombatKeyword == CombatKeywordEx.GrantCriticalChance ||
                 CombatKeyword == CombatKeywordEx.NoYellForHelp ||
                 CombatKeyword == CombatKeywordEx.IncreasePowerCost ||
                 CombatKeyword == CombatKeywordEx.IncreaseMeleePowerCost))
            {
                if (targetAbilityList.Count > 0)
                {
                    TargetAbilityList = targetAbilityList;
                    IsTargetAbilityListUsed = true;
                }
                else if (abilityList.Count > 0)
                {
                    Debug.Assert(abilityList.Count == 1);
                    TargetAbilityList = new() { abilityList[0] };
                }
            }
            else if (Target == CombatTarget.Internal_None &&
                     (CombatKeyword == CombatKeywordEx.DamageBoost ||
                      CombatKeyword == CombatKeywordEx.DealArmorDamage ||
                      CombatKeyword == CombatKeywordEx.DealHealthDamage ||
                      CombatKeyword == CombatKeywordEx.DealHealthAndArmorDamage) &&
                     targetAbilityList.Count > 0 &&
                     !IsTargetAbilityListUsed &&
                     CanApplyModifier)
            {
                TargetAbilityList = targetAbilityList;
            }
            else if (Target == CombatTarget.Self && CombatKeyword == CombatKeywordEx.TurnMitigationToDamage)
            {
                TargetAbilityList = targetAbilityList;
            }
            else if (Target == CombatTarget.Allies && CombatKeyword == CombatKeywordEx.IncreasePowerCost)
            {
                TargetAbilityList = targetAbilityList;
            }

            GetDamageCategory(AllEffects, i + 1, out GameDamageCategory DamageCategory);
            if (DamageCategory == GameDamageCategory.Internal_None)
                GetDamageCategory(AllEffects, i - 1, out DamageCategory);

            if (PreviousDamageType != GameDamageType.Internal_None &&
                CombatEffect.DamageType == GameDamageType.Internal_None &&
                ConditionList.Exists(condition => condition == CombatCondition.TargetIsElite))
            {
                CombatEffect.DamageType = PreviousDamageType;
            }

            if (CombatKeyword == CombatKeywordEx.MaxMitigatedDamageLimit && Target == CombatTarget.Self)
                Target = CombatTarget.Internal_None;

            if (abilityList.Count == 1 && isGolemMinion)
            {
                if (abilityList[0] == AbilityKeyword.HealingMist)
                    abilityList[0] = AbilityKeyword.GolemHealingMist;
                else if (abilityList[0] == AbilityKeyword.HealingInjection)
                    abilityList[0] = AbilityKeyword.GolemHealingInjection;
            }

            if (ConditionList.Exists(condition => condition == CombatCondition.SpecificDirectDamageType) && CombatKeyword == CombatKeywordEx.DamageBoost)
            {
                Debug.Assert(CombatEffect.DamageType == GameDamageType.Internal_None);
                CombatEffect.DamageType = ConditionDamageType;
            }

            if (CombatKeyword == CombatKeywordEx.DamageBoost && ((i > 0 && AllEffects[i - 1].Keyword == CombatKeywordEx.RandomDamage) || (i + 1 < AllEffects.Count && AllEffects[i + 1].Keyword == CombatKeywordEx.RandomDamage)))
            {
                CombatKeyword = CombatKeywordEx.RandomDamageBoost;
            }

            if (CombatKeyword == CombatKeywordEx.IncreaseCriticalChance && !float.IsNaN(pgNumericValueEx.Value))
            {
                PreviousCritChance = pgNumericValueEx;
            }
            else if (CombatKeyword == CombatKeywordEx.GrantCriticalChance && PreviousCritChance is not null)
                pgNumericValueEx = PreviousCritChance;

            PgCombatModEffectEx pgCombatModEffectEx = new()
            {
                Keyword = CombatKeyword,
                AbilityList = new List<AbilityKeyword>(abilityList),
                Data = pgNumericValueEx,
                DamageType = CombatEffect.DamageType,
                DamageCategory = CanApplyModifier ? DamageCategory : GameDamageCategory.Internal_None,
                CombatSkill = ConditionSkill != GameCombatSkill.Internal_None ? ConditionSkill : CombatEffect.CombatSkill,
                RandomChance = CanApplyModifier ? RandomChance : float.NaN,
                DelayInSeconds = CanHaveDelay && CanApplyModifier ? DelayInSeconds : float.NaN,
                DurationInSeconds = CanHaveDuration && CanApplyModifier ? DurationInSeconds : float.NaN,
                RecurringDelay = CanApplyModifier ? RecurringDelay : float.NaN,
                Target = CanHaveTarget && CanApplyModifier ? Target : CombatTarget.Internal_None,
                TargetRange = CanHaveRange && CanApplyModifier ? TargetRange : float.NaN,
                TargetAbilityList = TargetAbilityList,
                ConditionList = CanApplyModifier && (ConditionIndex < 0 || i + 2 >= ConditionIndex) ? new(ConditionList) : new(),
                ConditionAbilityList = CanApplyModifier ? ConditionAbilityList : new(),
                ConditionValue = CanApplyModifier ? ConditionValue : float.NaN,
                ConditionPercentage = CanApplyModifier ? ConditionPercentage : float.NaN,
                IsEveryOtherUse = IsEveryOtherUse,
            };

            DynamicEffects.Add(pgCombatModEffectEx);

            if (OtherTarget != CombatTarget.Internal_None)
            {
                PgCombatModEffectEx pgOtherCombatModEffectEx = new()
                {
                    Keyword = CombatKeyword,
                    AbilityList = new List<AbilityKeyword>(abilityList),
                    Data = pgNumericValueEx,
                    DamageType = CombatEffect.DamageType,
                    CombatSkill = CombatEffect.CombatSkill,
                    RandomChance = CanApplyModifier ? RandomChance : float.NaN,
                    DelayInSeconds = CanApplyModifier ? DelayInSeconds : float.NaN,
                    DurationInSeconds = CanHaveDuration && CanApplyModifier ? DurationInSeconds : float.NaN,
                    Target = CanHaveTarget ? OtherTarget : CombatTarget.Internal_None,
                    TargetRange = CanHaveRange && CanApplyModifier ? TargetRange : float.NaN,
                    ConditionList = CanApplyModifier ? new(ConditionList) : new(),
                    ConditionAbilityList = CanApplyModifier ? ConditionAbilityList : new(),
                    ConditionValue = CanApplyModifier ? ConditionValue : float.NaN,
                    ConditionPercentage = CanApplyModifier ? ConditionPercentage : float.NaN,
                };

                DynamicEffects.Add(pgOtherCombatModEffectEx);
            }

            if (CombatEffect.DamageType != GameDamageType.Internal_None)
                PreviousDamageType = CombatEffect.DamageType;
        }

        pgCombatModEx = new()
        {
            Description = description,
            PermanentEffects = PermanentEffects,
            DynamicEffects = DynamicEffects,
        };
    }

    private static float GetValueAndRemove(List<PgCombatEffectEx> effects, CombatKeywordEx keyword, bool asProbability = false)
    {
        if (effects.Find(item => item.Keyword == keyword) is not PgCombatEffectEx effectWithKeyword)
            return float.NaN;

        float Value = asProbability
                        ? effectWithKeyword.Data.Value / 100
                        : effectWithKeyword.Data.Value;
        effects.Remove(effectWithKeyword);

        return Value;
    }

    private static void GetTargets(List<PgCombatEffectEx> effects, int index, List<AbilityKeyword> abilityList, out CombatTarget target, out CombatTarget otherTarget, out bool isEveryOtherUse)
    {
        target = CombatTarget.Internal_None;
        otherTarget = CombatTarget.Internal_None;
        isEveryOtherUse = false;

        if (0 <= index && index < effects.Count)
        {
            PgCombatEffectEx NextCombatEffect = effects[index];
            if (NextCombatEffect.Keyword == CombatKeywordEx.ApplyToSelf)
                target = CombatTarget.Self;
            else if (NextCombatEffect.Keyword == CombatKeywordEx.ApplyToAllies)
                target = CombatTarget.Allies;
            else if (NextCombatEffect.Keyword == CombatKeywordEx.ApplyToPet)
                target = SelectPetType(abilityList);
            else if (NextCombatEffect.Keyword == CombatKeywordEx.ApplyToPetOfTarget)
                target = CombatTarget.PetOfTarget;
            else if (NextCombatEffect.Keyword == CombatKeywordEx.ApplyToSelfAndPet)
            {
                target = CombatTarget.Self;
                otherTarget = SelectPetType(abilityList);
            }
            else if (NextCombatEffect.Keyword == CombatKeywordEx.ApplyToSelfAndAllies)
            {
                target = CombatTarget.Self;
                otherTarget = CombatTarget.Allies;
            }
            else if (NextCombatEffect.Keyword == CombatKeywordEx.EveryOtherUse)
                isEveryOtherUse = true;
        }
    }

    private static void GetDamageCategory(List<PgCombatEffectEx> effects, int index, out GameDamageCategory damageCategory)
    {
        damageCategory = GameDamageCategory.Internal_None;

        if (0 <= index && index < effects.Count)
        {
            PgCombatEffectEx NextCombatEffect = effects[index];
            if (NextCombatEffect.Keyword == CombatKeywordEx.ApplyToIndirect)
                damageCategory = GameDamageCategory.Indirect;
        }
    }

    private void BuildMatchingModEffect_003(string description, PgEffect effect, bool isGolemMinion, List<AbilityKeyword> abilityList, PgCombatEffectCollectionEx dynamicCombatEffectList, PgCombatEffectCollectionEx staticCombatEffectList, List<AbilityKeyword> targetAbilityList, out PgCombatModEx pgCombatModEx, bool disallowPrevioustarget = false, int ignoreModifierIndex = -1)
    {
        PgCombatEffectCollectionEx combatEffectList = new();
        combatEffectList.AddRange(dynamicCombatEffectList);
        combatEffectList.AddRange(staticCombatEffectList);

        // Concatenate static & dynamic to dynamic
        BuildMatchingModEffect_001(description, effect, isGolemMinion, abilityList, combatEffectList, new PgCombatEffectCollectionEx(), targetAbilityList, out pgCombatModEx, disallowPrevioustarget, ignoreModifierIndex);
    }

    private void BuildMatchingModEffect_004(string description, PgEffect effect, List<AbilityKeyword> abilityList, PgCombatEffectCollectionEx dynamicCombatEffectList, PgCombatEffectCollectionEx staticCombatEffectList, List<AbilityKeyword> targetAbilityList, out PgCombatModEx pgCombatModEx, int ignoreModifierIndex = -1)
    {
        float DurationInSeconds = float.NaN;
        float RecurringDelay = float.NaN;
        CombatKeywordEx Keyword = CombatKeywordEx.Internal_None;
        CombatTarget Target = CombatTarget.Internal_None;

        for (int i = 0; i < staticCombatEffectList.Count; i++)
        {
            PgCombatEffectEx CombatEffect = staticCombatEffectList[i];
            StringToEnumConversion<CombatKeywordEx>.SetCustomParsedEnum(CombatEffect.Keyword);

            if (CombatEffect.Keyword == CombatKeywordEx.EffectDuration)
            {
                float? RawValue = CombatEffect.Data.RawValue;
                Debug.Assert(RawValue != null);
                DurationInSeconds = RawValue!.Value;

                if (Keyword == CombatKeywordEx.Internal_None)
                    Keyword = CombatKeywordEx.GiveBuff;

                if (Target == CombatTarget.Internal_None && targetAbilityList.Contains(AbilityKeyword.StabledPet))
                {
                    Target = CombatTarget.AnimalHandlingPet;
                    targetAbilityList.Remove(AbilityKeyword.StabledPet);
                }

                staticCombatEffectList.RemoveAt(i);
                break;
            }
            else if (CombatEffect.Keyword == CombatKeywordEx.EffectDurationInMinutes)
            {
                float? RawValue = CombatEffect.Data.RawValue;
                Debug.Assert(RawValue != null);
                DurationInSeconds = RawValue!.Value * 60;
                Keyword = CombatKeywordEx.GiveBuff;

                staticCombatEffectList.RemoveAt(i);
                break;
            }
            else if (CombatEffect.Keyword == CombatKeywordEx.RecurringEffect)
            {
                float? RawValue = CombatEffect.Data.RawValue;
                Debug.Assert(RawValue != null);
                RecurringDelay = RawValue!.Value;

                if (Keyword == CombatKeywordEx.Internal_None)
                    Keyword = CombatKeywordEx.GiveBuff;

                staticCombatEffectList.RemoveAt(i);
                break;
            }
            else if (CombatEffect.Keyword == CombatKeywordEx.NextUse)
            {
                DurationInSeconds = (effect.Duration == -2) ? 30 : throw new InvalidOperationException("Unknown effect duration");
                Keyword = CombatKeywordEx.GiveBuffOneUse;

                staticCombatEffectList.RemoveAt(i);
                break;
            }
            else if (CombatEffect.Keyword == CombatKeywordEx.NextAttack)
            {
                DurationInSeconds = (effect.Duration == -2) ? 30 : throw new InvalidOperationException("Unknown effect duration");
                Keyword = CombatKeywordEx.GiveBuffOneAttack;

                staticCombatEffectList.RemoveAt(i);
                break;
            }
            else if (CombatEffect.Keyword == CombatKeywordEx.NextRageAttack)
            {
                DurationInSeconds = (effect.Duration == -2) ? 30 : throw new InvalidOperationException("Unknown effect duration");
                Keyword = CombatKeywordEx.GiveBuffOneRageAttack;

                staticCombatEffectList.RemoveAt(i);
                break;
            }
            else if (CombatEffect.Keyword == CombatKeywordEx.NextHit)
            {
                DurationInSeconds = (effect.Duration == -2) ? 30 : throw new InvalidOperationException("Unknown effect duration");
                Keyword = CombatKeywordEx.GiveBuffOneHit;

                staticCombatEffectList.RemoveAt(i);
                break;
            }
            /*
            else if (CombatEffect.Keyword == CombatKeywordEx.NextEvade)
            {
                DurationInSeconds = (effect.Duration == -2) ? 30 : throw new InvalidOperationException("Unknown effect duration");
                Keyword = CombatKeywordEx.GiveBuffOneEvade;

                staticCombatEffectList.RemoveAt(i);
                break;
            }*/
            else if (CombatEffect.Keyword == CombatKeywordEx.IfTargetDies)
            {
                Keyword = CombatKeywordEx.GiveBuffOneUse;
            }
            else if (CombatEffect.Keyword == CombatKeywordEx.WhilePlayingSong)
            {
                Keyword = CombatEffect.Keyword;

                staticCombatEffectList.RemoveAt(i);
                break;
            }
            else if (CombatEffect.Keyword == CombatKeywordEx.GenerateTaunt)
            {
                DurationInSeconds = (effect.Duration == -2) ? 30 : throw new InvalidOperationException("Unknown effect duration");
                Keyword = CombatKeywordEx.GiveBuff;
                Target = CombatTarget.Self;
                break;
            }
            else if (CombatEffect.Keyword == CombatKeywordEx.ApplyToPet)
            {
                DurationInSeconds = (effect.Duration == -2) ? 30 : throw new InvalidOperationException("Unknown effect duration");
                Keyword = CombatKeywordEx.GiveBuff;
                Target = CombatTarget.AnimalHandlingPet;

                staticCombatEffectList.RemoveAt(i);
                break;
            }
            else if (CombatEffect.Keyword == CombatKeywordEx.ApplyToSelf)
            {
                Target = CombatTarget.Self;
                staticCombatEffectList.RemoveAt(i);
                i--;
            }
            else if (CombatEffect.Keyword == CombatKeywordEx.ApplyToAllies)
            {
                Target = CombatTarget.Allies;
                staticCombatEffectList.RemoveAt(i);
                i--;
            }
            else if (CombatEffect.Keyword == CombatKeywordEx.ApplyToPetOfTarget)
            {
                Target = CombatTarget.PetOfTarget;
                staticCombatEffectList.RemoveAt(i);
                i--;
            }
        }

        Debug.Assert(Keyword != CombatKeywordEx.Internal_None);
        Debug.Assert(!float.IsNaN(DurationInSeconds) || !float.IsNaN(RecurringDelay) || Keyword == CombatKeywordEx.WhilePlayingSong);

        StringToEnumConversion<CombatKeywordEx>.SetCustomParsedEnum(Keyword);

        // Inverse static & dynamic
        BuildMatchingModEffect_001(description, effect, isGolemMinion: false, targetAbilityList, staticCombatEffectList, new(), targetAbilityList, out pgCombatModEx);
        BuildMatchingModEffect_001(description, effect, isGolemMinion: false, abilityList, new(), dynamicCombatEffectList, targetAbilityList, out PgCombatModEx pgOtherCombatModEx, ignoreModifierIndex: ignoreModifierIndex);

        PgCombatModEffectEx pgApplyBuff = new()
        {
            Keyword = Keyword,
            AbilityList = new List<AbilityKeyword>(abilityList),
            Data = PgNumericValueEx.Empty,
            DurationInSeconds = DurationInSeconds,
            RecurringDelay = RecurringDelay,
            Target = Target,
        };

        pgCombatModEx.DynamicEffects.Insert(0, pgApplyBuff);

        for (int i = 0; i < pgOtherCombatModEx.DynamicEffects.Count; i++)
        {
            PgCombatModEffectEx OtherEffect = pgOtherCombatModEx.DynamicEffects[i];
            pgCombatModEx.DynamicEffects.Insert(i, OtherEffect);
        }
    }

    private void BuildMatchingModEffect_006(string description, bool isGolemMinion, List<AbilityKeyword> abilityList, PgCombatEffectCollectionEx dynamicCombatEffectList, PgCombatEffectCollectionEx staticCombatEffectList, List<AbilityKeyword> targetAbilityList, out PgCombatModEx pgCombatModEx, int ignoreModifierIndex = -1)
    {
        // Inverse static & dynamic
        BuildMatchingModEffect_005(description, isGolemMinion, abilityList, staticCombatEffectList, dynamicCombatEffectList, targetAbilityList, out pgCombatModEx, ignoreModifierIndex);
    }

    private void BuildMatchingModEffect_005(string description, bool isGolemMinion, List<AbilityKeyword> abilityList, PgCombatEffectCollectionEx dynamicCombatEffectList, PgCombatEffectCollectionEx staticCombatEffectList, List<AbilityKeyword> targetAbilityList, out PgCombatModEx pgCombatModEx, int ignoreModifierIndex = -1)
    {
        float DelayInSeconds = GetValueAndRemove(dynamicCombatEffectList, CombatKeywordEx.EffectDelay);
        float DurationInSeconds = GetValueAndRemove(dynamicCombatEffectList, CombatKeywordEx.EffectDuration);
        float RecurringDelay = GetValueAndRemove(dynamicCombatEffectList, CombatKeywordEx.RecurringEffect);
        float DurationOverTime = GetValueAndRemove(dynamicCombatEffectList, CombatKeywordEx.EffectOverTime);

        CombatTarget Target = CombatTarget.Internal_None;

        if (isGolemMinion)
        {
            Target = CombatTarget.BattleChemistryGolem;
        }
        else if (abilityList.Count >= 1)
        {
            switch (abilityList[0])
            {
                case AbilityKeyword.StabledPet:
                    Target = CombatTarget.AnimalHandlingPet;
                    break;
                case AbilityKeyword.ColdSphere:
                    Target = CombatTarget.IceMagicColdSphere;
                    break;
                case AbilityKeyword.SummonDeer:
                    Target = CombatTarget.SummonedDeer;
                    break;
                case AbilityKeyword.SummonedFireWall:
                    Target = CombatTarget.FireMagicFirewall;
                    break;
                case AbilityKeyword.SummonedSpider:
                    Target = CombatTarget.SummonedSpider;
                    break;
                case AbilityKeyword.TrickFox:
                    Target = CombatTarget.SummonedTrickFox;
                    break;
                case AbilityKeyword.SummonedTornado:
                    Target = CombatTarget.WeatherWitchingTornado;
                    break;
                case AbilityKeyword.HealingSanctuary:
                    Target = CombatTarget.DruidHealingSanctuary;
                    break;
                case AbilityKeyword.PowerGlyph:
                    Target = CombatTarget.SpiritFoxPowerGlyph;
                    break;
                case AbilityKeyword.FaeConduit:
                    Target = CombatTarget.FairyFaeConduit;
                    break;
                case AbilityKeyword.SummonZombie:
                    Target = CombatTarget.NecromancyZombie;
                    break;
                case AbilityKeyword.SummonSkeleton:
                    Target = CombatTarget.NecromancySkeleton;
                    break;
                case AbilityKeyword.SummonSkeletonSwordsman:
                    Target = CombatTarget.NecromancySkeletonSwordsman;
                    break;
                case AbilityKeyword.SummonSkeletonArcherOrMage:
                    Target = CombatTarget.NecromancySkeletonArcherOrMage;
                    break;
                case AbilityKeyword.Minigolem:
                    Target = CombatTarget.BattleChemistryGolem;
                    break;
                case AbilityKeyword.ConfusingDouble:
                    Target = CombatTarget.GiantBatDouble;
                    break;
                case AbilityKeyword.StunTrap:
                    Target = CombatTarget.WardenStunTrap;
                    break;
            }
        }
        else
            Target = CombatTarget.Self;

        Debug.Assert(Target != CombatTarget.Internal_None);

        PgCombatConditionCollectionEx ConditionList = new();
        List<AbilityKeyword> ConditionAbilityList = new();
        foreach (PgCombatEffectEx CombatEffect in dynamicCombatEffectList)
            if (KeywordToCondition.TryGetValue(CombatEffect.Keyword, out CombatCondition NewCondition))
            {
                Debug.Assert(ConditionList.Count == 0);
                Debug.Assert(NewCondition != CombatCondition.Internal_None);
                ConditionList.Add(NewCondition);

                if (NewCondition == CombatCondition.WhilePlayingSong ||
                    NewCondition == CombatCondition.TargetOfAbility)
                {
                    ConditionAbilityList = targetAbilityList;
                }
            }

        if (Target == CombatTarget.AnimalHandlingPet && ConditionList.Count == 0)
        {
            if (targetAbilityList.Count > 0 && targetAbilityList.TrueForAll(keyword => keyword == AbilityKeyword.SicEm ||
                                                                                       keyword == AbilityKeyword.CleverTrick ||
                                                                                       keyword == AbilityKeyword.GetItOffMe ||
                                                                                       keyword == AbilityKeyword.UnnaturalWrath ||
                                                                                       keyword == AbilityKeyword.BasicAttack))
            {
                ConditionList.Add(CombatCondition.PetAttackType);
                ConditionAbilityList = targetAbilityList;
            }
        }

        List<PgPermanentModEffectEx> PermanentEffects = new();
        for (int i = 0; i < dynamicCombatEffectList.Count; i++)
        {
            PgCombatEffectEx CombatEffect = dynamicCombatEffectList[i];
            CombatKeywordEx CombatKeyword = CombatEffect.Keyword;
            StringToEnumConversion<CombatKeywordEx>.SetCustomParsedEnum(CombatKeyword);

            bool CanApplyModifier = ignoreModifierIndex < 0 ||
                                    ((i != (ignoreModifierIndex % 1000)) &&
                                     (ignoreModifierIndex < 1000 || (i != ((ignoreModifierIndex / 1000) % 1000))));

            if (CombatKeyword == CombatKeywordEx.ApplyWithChance ||
                CombatKeyword == CombatKeywordEx.ApplyToSelf ||
                CombatKeyword == CombatKeywordEx.ApplyToAllies ||
                CombatKeyword == CombatKeywordEx.ApplyToSelfAndAllies ||
                CombatKeyword == CombatKeywordEx.ApplyToPet ||
                CombatKeyword == CombatKeywordEx.ApplyToSelfAndPet ||
                CombatKeyword == CombatKeywordEx.ApplyToPetOfTarget ||
                CombatKeyword == CombatKeywordEx.TargetRange ||
                CombatKeyword == CombatKeywordEx.EffectDuration ||
                CombatKeyword == CombatKeywordEx.EffectDurationInMinutes ||
                CombatKeyword == CombatKeywordEx.EffectOverTime ||
                CombatKeyword == CombatKeywordEx.RecurringEffect ||
                CombatKeyword == CombatKeywordEx.EffectEverySecond ||
                CombatKeyword == CombatKeywordEx.EffectDelay ||
                CombatKeyword == CombatKeywordEx.RandomDamage ||
                CombatKeyword == CombatKeywordEx.ApplyToIndirect ||
                CombatKeyword == CombatKeywordEx.EveryOtherUse)
            {
                continue;
            }

            if (KeywordToCondition.TryGetValue(CombatKeyword, out CombatCondition NewCondition))
            {
                continue;
            }

            PgNumericValueEx pgNumericValueEx = new()
            {
                Value = CombatEffect.Data.RawValue.HasValue ? CombatEffect.Data.RawValue.Value : float.NaN,
                IsPercent = CombatEffect.Data.RawIsPercent.HasValue ? CombatEffect.Data.RawIsPercent.Value : false,
            };

            if (OverTimeEffects.TryGetValue(CombatKeyword, out CombatKeywordEx CombatKeywordOverTime))
            {
                if (float.IsNaN(DurationInSeconds) && !float.IsNaN(DurationOverTime))
                {
                    DurationInSeconds = DurationOverTime;
                    CombatKeyword = CombatKeywordOverTime;
                }
            }

            PgPermanentModEffectEx pgPermanentModEffectEx = new()
            {
                Keyword = CombatKeyword,
                Data = pgNumericValueEx,
                DamageType = CombatEffect.DamageType,
                DelayInSeconds = DelayInSeconds,
                DurationInSeconds = CanApplyModifier ? DurationInSeconds : float.NaN,
                RecurringDelay = RecurringDelay,
                Target = Target,
                ConditionList = CanApplyModifier ? ConditionList : new(),
                ConditionAbilityList = ConditionAbilityList,
            };

            PermanentEffects.Add(pgPermanentModEffectEx);
        }

        pgCombatModEx = new()
        {
            Description = description,
            PermanentEffects = PermanentEffects,
            DynamicEffects = new(),
        };
    }

    private void BuildMatchingModEffect_007(string description, PgEffect effect, List<AbilityKeyword> abilityList, PgCombatEffectCollectionEx dynamicCombatEffectList, PgCombatEffectCollectionEx staticCombatEffectList, List<AbilityKeyword> targetAbilityList, out PgCombatModEx pgCombatModEx)
    {
        PgCombatEffectCollectionEx combatEffectList = new();
        combatEffectList.AddRange(dynamicCombatEffectList);
        combatEffectList.AddRange(staticCombatEffectList);

        // Concatenate static & dynamic to dynamic
        BuildMatchingModEffect_004(description, effect, abilityList, new PgCombatEffectCollectionEx(), combatEffectList, targetAbilityList, out pgCombatModEx);
    }

    private void BuildMatchingModEffect_008(string description, PgEffect effect, List<AbilityKeyword> abilityList, PgCombatEffectCollectionEx dynamicCombatEffectList, PgCombatEffectCollectionEx staticCombatEffectList, List<AbilityKeyword> targetAbilityList, List<int> pick1, List<int> pick2, bool inverseTargets, out PgCombatModEx pgCombatModEx)
    {
        PgCombatEffectCollectionEx combatEffectList = new();
        combatEffectList.AddRange(dynamicCombatEffectList);
        combatEffectList.AddRange(staticCombatEffectList);

        PgCombatEffectCollectionEx combatEffectList1 = new();
        for (int i = 0; i < pick1.Count; i++)
            combatEffectList1.Add(combatEffectList[pick1[i]]);
        PgCombatEffectCollectionEx combatEffectList2 = new();
        for (int i = 0; i < pick2.Count; i++)
            combatEffectList2.Add(combatEffectList[pick2[i]]);

        BuildMatchingModEffect_001(description, effect, isGolemMinion: false, abilityList, combatEffectList1, new(), abilityList, out pgCombatModEx);

        PgCombatModEx pgOtherCombatModEx;
        if (inverseTargets)
            BuildMatchingModEffect_001(description, effect, isGolemMinion: false, targetAbilityList, combatEffectList2, new(), abilityList, out pgOtherCombatModEx);
        else
            BuildMatchingModEffect_001(description, effect, isGolemMinion: false, abilityList, combatEffectList2, new(), targetAbilityList, out pgOtherCombatModEx);

        for (int i = 0; i < pgOtherCombatModEx.DynamicEffects.Count; i++)
        {
            PgCombatModEffectEx OtherEffect = pgOtherCombatModEx.DynamicEffects[i];
            pgCombatModEx.DynamicEffects.Add(OtherEffect);
        }
    }

    private void BuildMatchingModEffect_009(string description, PgCombatEffectCollectionEx dynamicCombatEffectList, PgCombatEffectCollectionEx staticCombatEffectList, out PgCombatModEx pgCombatModEx)
    {
        PgCombatEffectCollectionEx combatEffectList = new();
        combatEffectList.AddRange(dynamicCombatEffectList);
        combatEffectList.AddRange(staticCombatEffectList);

        float RandomChance = GetValueAndRemove(combatEffectList, CombatKeywordEx.ApplyWithChance, asProbability: true);

        PgCombatConditionCollectionEx ConditionList = new();
        foreach (PgCombatEffectEx CombatEffect in combatEffectList)
            if (KeywordToCondition.TryGetValue(CombatEffect.Keyword, out CombatCondition NewCondition))
            {
                Debug.Assert(NewCondition != CombatCondition.Internal_None);
                ConditionList.Add(NewCondition);
            }

        List<PgPermanentModEffectEx> PermanentEffects = new();
        for (int i = 0; i < combatEffectList.Count; i++)
        {
            PgCombatEffectEx CombatEffect = combatEffectList[i];
            CombatKeywordEx CombatKeyword = CombatEffect.Keyword;
            StringToEnumConversion<CombatKeywordEx>.SetCustomParsedEnum(CombatKeyword);

            if (CombatKeyword == CombatKeywordEx.ApplyWithChance ||
                CombatKeyword == CombatKeywordEx.ApplyToSelf ||
                CombatKeyword == CombatKeywordEx.ApplyToAllies ||
                CombatKeyword == CombatKeywordEx.ApplyToSelfAndAllies ||
                CombatKeyword == CombatKeywordEx.ApplyToPet ||
                CombatKeyword == CombatKeywordEx.ApplyToSelfAndPet ||
                CombatKeyword == CombatKeywordEx.ApplyToPetOfTarget ||
                CombatKeyword == CombatKeywordEx.TargetRange ||
                CombatKeyword == CombatKeywordEx.EffectDuration ||
                CombatKeyword == CombatKeywordEx.EffectDurationInMinutes ||
                CombatKeyword == CombatKeywordEx.EffectOverTime ||
                CombatKeyword == CombatKeywordEx.RecurringEffect ||
                CombatKeyword == CombatKeywordEx.EffectEverySecond ||
                CombatKeyword == CombatKeywordEx.EffectDelay ||
                CombatKeyword == CombatKeywordEx.RandomDamage ||
                CombatKeyword == CombatKeywordEx.ApplyToIndirect ||
                CombatKeyword == CombatKeywordEx.EveryOtherUse)
            {
                continue;
            }

            if (KeywordToCondition.TryGetValue(CombatKeyword, out CombatCondition NewCondition))
            {
                continue;
            }

            PgNumericValueEx pgNumericValueEx = new()
            {
                Value = CombatEffect.Data.RawValue.HasValue ? CombatEffect.Data.RawValue.Value : float.NaN,
                IsPercent = CombatEffect.Data.RawIsPercent.HasValue ? CombatEffect.Data.RawIsPercent.Value : false,
            };

            PgPermanentModEffectEx pgPermanentModEffectEx = new()
            {
                Keyword = CombatKeyword,
                Data = pgNumericValueEx,
                DamageType = CombatEffect.DamageType,
                RandomChance = RandomChance,
                Target = CombatTarget.Self,
                ConditionList = ConditionList,
            };

            PermanentEffects.Add(pgPermanentModEffectEx);
        }

        pgCombatModEx = new()
        {
            Description = description,
            PermanentEffects = PermanentEffects,
            DynamicEffects = new(),
        };
    }

    private static CombatTarget SelectPetType(List<AbilityKeyword> abilityList)
    {
        bool IsAnimalHandlingPet = abilityList.TrueForAll(Keyword => AnimalHandlingKeywordList.Contains(Keyword));
        bool IsNecromancyPet = abilityList.TrueForAll(Keyword => NecromancyKeywordList.Contains(Keyword));

        Debug.Assert(!IsAnimalHandlingPet || !IsNecromancyPet);

        if (IsAnimalHandlingPet && !IsNecromancyPet)
            return CombatTarget.AnimalHandlingPet;
        else if (!IsAnimalHandlingPet && IsNecromancyPet)
            return CombatTarget.NecromancyPet;
        else
            return CombatTarget.AnyPet;
    }
}
