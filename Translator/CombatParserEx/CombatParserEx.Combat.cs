namespace TranslatorCombatParserEx;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using PgObjects;
using Translator;

internal partial class CombatParserEx
{
    public void AnalyzeMods()
    {
        AnalyzeMatchingPowersAndEffects(out List<string[]> StringKeyTable, out List<PgModEffectCollectionEx> AnalyzedPowerKeyToCompleteEffectTable);
    }

    private void AnalyzeMatchingPowersAndEffects(out List<string[]> stringKeyTable, out List<PgModEffectCollectionEx> powerKeyToCompleteEffectTable)
    {
        stringKeyTable = new();
        powerKeyToCompleteEffectTable = new();

        foreach (KeyValuePair<PgPower, List<PgEffect>> Entry in powerToEffectTable)
        {
            PgPower ItemPower = Entry.Key;
            List<PgEffect> ItemEffectList = Entry.Value;

            if (!KnownPowers.ContainsKey(ItemPower.Key))
            {
                if (AnalyzeMatchingPowersAndEffects(ItemPower, ItemEffectList, out string[] stringKeyArray, out PgModEffectCollectionEx ModEffectArray, out PgCombatModCollectionEx pgCombatModCollectionEx))
                {
                    stringKeyTable.Add(stringKeyArray);
                    powerKeyToCompleteEffectTable.Add(ModEffectArray);
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
            /*
            Debug.WriteLine(string.Empty);
            Debug.WriteLine("BAD TARGET!");
            Debug.WriteLine($"   Effect: {effect.Description}");
            Debug.WriteLine($"Parsed as: {ParsedEffectString}, Target: {ParsedEffectTargetAbilityList}");
            Debug.WriteLine($"    Power: {powerSimpleEffect.Description}");
            Debug.WriteLine($"Parsed as: {{{ParsedAbilityList}}} {ParsedPowerString}, Target: {ParsedModTargetAbilityList}");
            */
            return false;
        }

        bool IsContained = CombatEffectContains(ModCombatList, EffectCombatList, out PgCombatEffectCollectionEx DynamicCombatEffectList, out PgCombatEffectCollectionEx StaticCombatEffectList);
        if (!IsContained)
        {
            /*
            Debug.WriteLine(string.Empty);
            Debug.WriteLine("UNPARSED!");
            Debug.WriteLine($"   Effect: {effect.Description}");
            Debug.WriteLine($"Parsed as: {ParsedEffectString}, Target: {ParsedEffectTargetAbilityList}");
            Debug.WriteLine($"    Power: {powerSimpleEffect.Description}");
            Debug.WriteLine($"Parsed as: {{{ParsedAbilityList}}} {ParsedPowerString}, Target: {ParsedModTargetAbilityList}");
            */
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

        BuildModEffect(powerKey, effect, powerSimpleEffect.Description, ModAbilityList, DynamicCombatEffectList, StaticCombatEffectList, ModTargetAbilityList, out pgCombatModEx);

        return true;
    }

    private void AnalyzeText(string text, bool isMod, bool isGolemAbility, out List<AbilityKeyword> extractedAbilityList, out PgCombatEffectCollectionEx extractedCombatEffectList, out List<AbilityKeyword> extractedTargetAbilityList)
    {
        RemoveDecorationText(ref text);
        SimplifyGrammar(ref text);
        SimplifyRandom(ref text);

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
        ReplaceCaseInsensitive(ref text, " to you (or armor if health is full)", "/Armor");
        ReplaceCaseInsensitive(ref text, " (or armor if health is full)", "/Armor");
        ReplaceCaseInsensitive(ref text, " (or armor, if health is full)", "/Armor");
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
        return index == 0 || text[index - 1] == ' ' || text[index - 1] == ',';
    }

    private static bool IsEndingSentenceIndex(string text, int index)
    {
        return index + 1 >= text.Length || text[index] == ' ' || text[index] == ',' || text[index] == '.' || (index + 1 < text.Length && text[index] == '\'' && (text[index + 1] == 's' || text[index - 1] == 's'));
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

    private void ExtractAbilityList(bool limitParsing, bool isGolemAbility, ref string text, out List<AbilityKeyword> extractedAbilityList, out int removeCount)
    {
        extractedAbilityList = new List<AbilityKeyword>();
        removeCount = 0;

        Dictionary<int, string> ExtractedTable = new Dictionary<int, string>();

        foreach (string AbilityName in abilityNameList)
            if (nameToKeyword.ContainsKey(AbilityName))
            {
                if (AbilityName.StartsWith("Animal Handling pets"))
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
                        ExtractedTable.Add(NewIndex, AbilityName);
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

                KeyList.Add(BestString);

                ExtractedTable.Remove(BestIndex);

                LastBestIndex = BestIndex + BestString.Length;
            }

            int UnusedIndex = -1;
            foreach (string Key in KeyList)
            {
                bool IsRemoved;
                do
                {
                    RemoveDecorativeText(ref text, Key, "@", out IsRemoved, ref UnusedIndex);
                    if (IsRemoved)
                        removeCount++;
                }
                while (IsRemoved);

                extractedAbilityList.AddRange(nameToKeyword[Key]);
            }
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

    private void BuildModEffect(string powerKey, PgEffect effect, string description, List<AbilityKeyword> abilityList, PgCombatEffectCollectionEx dynamicCombatEffectList, PgCombatEffectCollectionEx staticCombatEffectList, List<AbilityKeyword> targetAbilityList, out PgCombatModEx pgCombatModEx)
    {
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
            case "1202":
            case "5122":
            case "14151":
            case "14152":
            case "14153":
            case "14154":
            case "14155":
            case "14156":

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
                BuildModEffect_002(description, abilityList, dynamicCombatEffectList, staticCombatEffectList, out pgCombatModEx);
                break;
            case "28184":
                BuildModEffect_001(description, abilityList, dynamicCombatEffectList, staticCombatEffectList, out pgCombatModEx);
                break;
            case "11502":
            case "2022":
            case "4201":
                BuildModEffect_003(description, abilityList, dynamicCombatEffectList, staticCombatEffectList, out pgCombatModEx);
                break;
            default:
                if (dynamicCombatEffectList.Exists(item => item.Keyword == CombatKeywordEx.RestoreHealth ||
                                                           item.Keyword == CombatKeywordEx.RestorePower ||
                                                           item.Keyword == CombatKeywordEx.RestoreArmor ||
                                                           item.Keyword == CombatKeywordEx.IncreaseMaxHealth ||
                                                           item.Keyword == CombatKeywordEx.IncreaseMaxArmor ||
                                                           item.Keyword == CombatKeywordEx.IncreaseMaxPower ||
                                                           item.Keyword == CombatKeywordEx.AddSprintSpeed ||
                                                           item.Keyword == CombatKeywordEx.RegenPercentageOfArmor ||
                                                           item.Keyword == CombatKeywordEx.IncreaseHealEfficiency ||
                                                           item.Keyword == CombatKeywordEx.RestoreHealthOrArmor) ||
                    staticCombatEffectList.Exists(item => item.Keyword == CombatKeywordEx.RestoreHealth ||
                                                          item.Keyword == CombatKeywordEx.RestorePower ||
                                                          item.Keyword == CombatKeywordEx.RestoreArmor ||
                                                          item.Keyword == CombatKeywordEx.IncreaseMaxHealth ||
                                                          item.Keyword == CombatKeywordEx.IncreaseMaxArmor ||
                                                          item.Keyword == CombatKeywordEx.IncreaseMaxPower ||
                                                          item.Keyword == CombatKeywordEx.AddSprintSpeed ||
                                                          item.Keyword == CombatKeywordEx.RegenPercentageOfArmor ||
                                                          item.Keyword == CombatKeywordEx.IncreaseHealEfficiency ||
                                                          item.Keyword == CombatKeywordEx.RestoreHealthOrArmor))
                {
                    BuildModEffect_002(description, abilityList, dynamicCombatEffectList, staticCombatEffectList, out pgCombatModEx);
                }
                else
                {
                    pgCombatModEx = new PgCombatModEx() { Description = description, PermanentEffects = new(), DynamicEffects = new() };
                }
                break;
            case "10401":
            case "11404":
            case "12122":
                BuildModEffect_004(description, effect, abilityList, dynamicCombatEffectList, staticCombatEffectList, targetAbilityList, out pgCombatModEx);
                break;
            case "12313":
                BuildModEffect_005(description, abilityList, dynamicCombatEffectList, staticCombatEffectList, out pgCombatModEx);
                break;
            case "12301":
            case "21201":
            case "28644":
            case "12302":
            case "12312":
            case "23302":
                BuildModEffect_006(description, abilityList, dynamicCombatEffectList, staticCombatEffectList, out pgCombatModEx);
                break;
            case "10509":
            case "13004":
            case "1303":
            case "16082":
            case "17022":
            case "20009":
            case "24062":
            case "24242":
            case "25223":
            case "4471":
            case "5203":
            case "5401":
            case "12121":
            case "14205":
            case "14353":
            case "14605":
            case "15102":
            case "15452":
            case "15454":
            case "157":
            case "16004":
            case "16009":
            case "16223":
            case "17063":
            case "18064":
            case "18065":
            case "2021":
            case "20302":
            case "21353":
            case "22401":
            case "24002":
            case "25105":
            case "26052":
            case "26094":
            case "26205":
            case "28141":
            case "28142":
            case "28143":
            case "28612":
            case "28686":
            case "4304":
            case "4502":
            case "5124":
            case "5152":
            case "6088":
            case "6306":
            case "7009":
            case "7202":
            case "7215":
            case "7306":
            case "7307":
            case "7308":
            case "7309":
            case "7310":
            case "7401":
            case "7431":
            case "8313":
            case "8318":
            case "9008":
            case "9084":
            case "9086":
            case "9881":
            case "9883":
            case "12092":
            case "159":
            case "16102":
            case "163":
            case "17023":
            case "17083":
            case "20066":
            case "23201":
            case "26053":
            case "26223":
            case "27033":
            case "3047":
            case "4004":
            case "7216":
            case "8022":
            case "9703":
            case "9752":
            case "13206":
            case "15107":
            case "18115":
            case "28221":
            case "8023":
            case "8353":
            case "7301":
            case "7472":
            case "8301":
            case "23301":
            case "2302":
            case "15505":
            case "27172":
            case "28201":
            case "28202":
            case "28203":
            case "27173":
            case "8302":
            case "8304":
                pgCombatModEx = new PgCombatModEx() { Description = description, PermanentEffects = new(), DynamicEffects = new() };
                break;
            case "XXX":
            case "14601":
            case "14602":
            case "14603":
            case "14354":
                pgCombatModEx = new PgCombatModEx() { Description = description, PermanentEffects = new(), DynamicEffects = new() };
                break;
        }
    }

    private void BuildModEffect_001(string description, List<AbilityKeyword> abilityList, PgCombatEffectCollectionEx dynamicCombatEffectList, PgCombatEffectCollectionEx staticCombatEffectList, out PgCombatModEx pgCombatModEx)
    {
        List<PgCombatEffectEx> AllEffects = new(dynamicCombatEffectList);
        AllEffects.AddRange(staticCombatEffectList);

        float RandomChance = GetValueAndRemove(AllEffects, CombatKeywordEx.ApplyWithChance, asProbability: true);
        float DelayInSeconds = GetValueAndRemove(AllEffects, CombatKeywordEx.EffectDelay);
        float DurationInSeconds = GetValueAndRemove(AllEffects, CombatKeywordEx.EffectDuration);

        List<PgCombatModEffectEx> DynamicEffects = new();
        for (int i = 0; i < AllEffects.Count; i++)
        {
            PgCombatEffectEx CombatEffect = AllEffects[i];

            if (CombatEffect.Keyword == CombatKeywordEx.ApplyWithChance ||
                CombatEffect.Keyword == CombatKeywordEx.ApplyToSelf ||
                CombatEffect.Keyword == CombatKeywordEx.ApplyToSelfAndAllies ||
                CombatEffect.Keyword == CombatKeywordEx.ApplyToPet ||
                CombatEffect.Keyword == CombatKeywordEx.ApplyToSelfAndPet ||
                CombatEffect.Keyword == CombatKeywordEx.EffectDuration ||
                CombatEffect.Keyword == CombatKeywordEx.EffectDelay)
            {
                continue;
            }

            bool CanHaveDuration = CombatEffect.Keyword == CombatKeywordEx.AddSprintSpeed ||
                                   CombatEffect.Keyword == CombatKeywordEx.IncreaseMaxHealth ||
                                   CombatEffect.Keyword == CombatKeywordEx.IncreaseMaxArmor ||
                                   CombatEffect.Keyword == CombatKeywordEx.IncreaseMaxPower;

            PgNumericValueEx pgNumericValueEx = new()
            {
                Value = CombatEffect.Data.RawValue.HasValue ? CombatEffect.Data.RawValue.Value : float.NaN,
                IsPercent = CombatEffect.Data.RawIsPercent.HasValue ? CombatEffect.Data.RawIsPercent.Value : false,
            };

            CombatTarget Target = CombatTarget.Internal_None;
            CombatTarget OtherTarget = CombatTarget.Internal_None;
            if (i + 1 < dynamicCombatEffectList.Count)
            {
                PgCombatEffectEx NextCombatEffect = dynamicCombatEffectList[i + 1];
                if (NextCombatEffect.Keyword == CombatKeywordEx.ApplyToSelf)
                    Target = CombatTarget.Self;
                else if (NextCombatEffect.Keyword == CombatKeywordEx.ApplyToPet)
                    Target = SelectPetType(abilityList);
                else if (NextCombatEffect.Keyword == CombatKeywordEx.ApplyToSelfAndPet)
                {
                    Target = CombatTarget.Self;
                    OtherTarget = SelectPetType(abilityList);
                }
                else if (NextCombatEffect.Keyword == CombatKeywordEx.ApplyToSelfAndAllies)
                {
                    Target = CombatTarget.Self;
                    OtherTarget = CombatTarget.Allies;
                }
            }

            PgCombatModEffectEx pgCombatModEffectEx = new()
            {
                Keyword = CombatEffect.Keyword,
                AbilityList = new List<AbilityKeyword>(abilityList),
                Data = pgNumericValueEx,
                DamageType = CombatEffect.DamageType,
                CombatSkill = CombatEffect.CombatSkill,
                RandomChance = RandomChance,
                DelayInSeconds = DelayInSeconds,
                DurationInSeconds = CanHaveDuration ? DurationInSeconds : float.NaN,
                Target = Target,
            };

            DynamicEffects.Add(pgCombatModEffectEx);

            if (OtherTarget != CombatTarget.Internal_None)
            {
                PgCombatModEffectEx pgOtherCombatModEffectEx = new()
                {
                    Keyword = CombatEffect.Keyword,
                    AbilityList = new List<AbilityKeyword>(abilityList),
                    Data = pgNumericValueEx,
                    DamageType = CombatEffect.DamageType,
                    CombatSkill = CombatEffect.CombatSkill,
                    RandomChance = RandomChance,
                    DelayInSeconds = DelayInSeconds,
                    DurationInSeconds = CanHaveDuration ? DurationInSeconds : float.NaN,
                    Target = OtherTarget,
                };

                DynamicEffects.Add(pgOtherCombatModEffectEx);
            }
        }

        pgCombatModEx = new()
        {
            Description = description,
            PermanentEffects = new(),
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

    private void BuildModEffect_002(string description, List<AbilityKeyword> abilityList, PgCombatEffectCollectionEx dynamicCombatEffectList, PgCombatEffectCollectionEx staticCcombatEffectList, out PgCombatModEx pgCombatModEx)
    {
        // Inverse static & dynamic
        BuildModEffect_001(description, abilityList, staticCcombatEffectList, dynamicCombatEffectList, out pgCombatModEx);
    }

    private void BuildModEffect_004(string description, PgEffect effect, List<AbilityKeyword> abilityList, PgCombatEffectCollectionEx dynamicCombatEffectList, PgCombatEffectCollectionEx staticCcombatEffectList, List<AbilityKeyword> targetAbilityList, out PgCombatModEx pgCombatModEx)
    {
        float DurationInSeconds = float.NaN;
        CombatKeywordEx Keyword = CombatKeywordEx.Internal_None;

        foreach (PgCombatEffectEx CombatEffect in staticCcombatEffectList)
        {
            if (CombatEffect.Keyword == CombatKeywordEx.EffectDuration)
            {
                float? RawValue = CombatEffect.Data.RawValue;
                Debug.Assert(RawValue != null);
                DurationInSeconds = RawValue!.Value;
                Keyword = CombatKeywordEx.GiveBuff;

                staticCcombatEffectList.Remove(CombatEffect);
                break;
            }
            else if (CombatEffect.Keyword == CombatKeywordEx.NextUse)
            {
                DurationInSeconds = (effect.Duration == -2) ? 30 : throw new InvalidOperationException("Unknown effect duration");
                Keyword = CombatKeywordEx.GiveBuffOneUse;

                staticCcombatEffectList.Remove(CombatEffect);
                break;
            }
        }

        Debug.Assert(!float.IsNaN(DurationInSeconds));
        Debug.Assert(Keyword != CombatKeywordEx.Internal_None);

        // Inverse static & dynamic
        BuildModEffect_001(description, targetAbilityList, staticCcombatEffectList, dynamicCombatEffectList, out pgCombatModEx);

        PgCombatModEffectEx pgApplyBuff = new()
        {
            Keyword = Keyword,
            AbilityList = new List<AbilityKeyword>(abilityList),
            Data = PgNumericValueEx.Empty,
            DurationInSeconds = DurationInSeconds,
        };

        pgCombatModEx.DynamicEffects.Insert(0, pgApplyBuff);
    }

    private void BuildModEffect_003(string description, List<AbilityKeyword> abilityList, PgCombatEffectCollectionEx dynamicCombatEffectList, PgCombatEffectCollectionEx staticCcombatEffectList, out PgCombatModEx pgCombatModEx)
    {
        PgCombatEffectCollectionEx combatEffectList = new();
        combatEffectList.AddRange(dynamicCombatEffectList);
        combatEffectList.AddRange(staticCcombatEffectList);

        // Concatenate static & dynamic to dynamic
        BuildModEffect_001(description, abilityList, combatEffectList, new PgCombatEffectCollectionEx(), out pgCombatModEx);
    }

    private void BuildModEffect_006(string description, List<AbilityKeyword> abilityList, PgCombatEffectCollectionEx dynamicCombatEffectList, PgCombatEffectCollectionEx staticCombatEffectList, out PgCombatModEx pgCombatModEx)
    {
        // Inverse static & dynamic
        BuildModEffect_005(description, abilityList, staticCombatEffectList, dynamicCombatEffectList, out pgCombatModEx);
    }

    private void BuildModEffect_005(string description, List<AbilityKeyword> abilityList, PgCombatEffectCollectionEx dynamicCombatEffectList, PgCombatEffectCollectionEx staticCombatEffectList, out PgCombatModEx pgCombatModEx)
    {
        Debug.Assert(abilityList.Count == 1);

        CombatTarget Target = CombatTarget.Internal_None;
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
        }

        Debug.Assert(Target != CombatTarget.Internal_None);

        List<PgPermanentModEffectEx> PermanentEffects = new();
        for (int i = 0; i < dynamicCombatEffectList.Count; i++)
        {
            PgCombatEffectEx CombatEffect = dynamicCombatEffectList[i];

            if (CombatEffect.Keyword == CombatKeywordEx.ApplyWithChance ||
                CombatEffect.Keyword == CombatKeywordEx.ApplyToSelf ||
                CombatEffect.Keyword == CombatKeywordEx.ApplyToSelfAndAllies ||
                CombatEffect.Keyword == CombatKeywordEx.ApplyToPet ||
                CombatEffect.Keyword == CombatKeywordEx.ApplyToSelfAndPet ||
                CombatEffect.Keyword == CombatKeywordEx.EffectDuration ||
                CombatEffect.Keyword == CombatKeywordEx.EffectDelay)
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
                Keyword = CombatEffect.Keyword,
                Data = pgNumericValueEx,
                Target = Target,
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

        if (IsAnimalHandlingPet && !IsNecromancyPet)
            return CombatTarget.AnimalHandlingPet;
        else if (!IsAnimalHandlingPet && IsNecromancyPet)
            return CombatTarget.NecromancyPet;
        else
            return CombatTarget.Internal_None;
    }
}
