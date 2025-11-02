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
    public static bool DebugMode = true;

    private void AnalyzeRemainingPowers(List<string[]> stringKeyTable, List<PgModEffectCollectionEx> analyzedPowerKeyToCompleteEffectTable)
    {
        foreach (PgPower ItemPower in unmatchedPowerList)
        {
            if (DebugMode)
            {
                if (!KnownCombatPowers.KnownUnmatchedPowers.ContainsKey(ItemPower.Key))
                {
                    if (AnalyzeRemainingPowers(ItemPower, out string[] stringKeyArray, out PgModEffectCollectionEx ModEffectArray, out PgCombatModCollectionEx pgCombatModCollectionEx))
                    {
                        stringKeyTable.Add(stringKeyArray);
                        analyzedPowerKeyToCompleteEffectTable.Add(ModEffectArray);
                        pgCombatModCollectionEx.Display(ItemPower.Key);
                    }
                }
            }
            else if (AnalyzeRemainingPowers(ItemPower, out string[] stringKeyArray, out PgModEffectCollectionEx ModEffectArray, out PgCombatModCollectionEx pgCombatModCollectionEx))
            {
                stringKeyTable.Add(stringKeyArray);
                analyzedPowerKeyToCompleteEffectTable.Add(ModEffectArray);

                foreach (PgCombatModEx pgCombatModEx in pgCombatModCollectionEx)
                {
                    foreach (PgPermanentModEffectEx pgPermanentModEffectEx in pgCombatModEx.PermanentEffects)
                        StringToEnumConversion<CombatKeywordEx>.SetCustomParsedEnum(pgPermanentModEffectEx.Keyword);
                    foreach (PgCombatModEffectEx pgCombatModEffectEx in pgCombatModEx.DynamicEffects)
                        StringToEnumConversion<CombatKeywordEx>.SetCustomParsedEnum(pgCombatModEffectEx.Keyword);
                }

                if (!KnownCombatPowers.KnownUnmatchedPowers.ContainsKey(ItemPower.Key))
                    pgCombatModCollectionEx.Display(ItemPower.Key);
            }
        }
    }

    private bool AnalyzeRemainingPowers(PgPower itemPower, out string[] stringKeyArray, out PgModEffectCollectionEx modEffectArray, out PgCombatModCollectionEx pgCombatModCollectionEx)
    {
        PgPowerTierCollection TierList = itemPower.TierList;
        string Skill_Key = itemPower.Skill_Key ?? string.Empty;
        string powerKey = itemPower.Key;

        Debug.Assert(TierList.Count > 0);

        int ValidationIndex = 0;
        if (TierList.Count >= 2)
            ValidationIndex = 1;
        if (TierList.Count >= 4)
            ValidationIndex = 2;

        List<CombatKeywordEx>[] PowerTierKeywordListArray = new List<CombatKeywordEx>[TierList.Count];
        stringKeyArray = new string[TierList.Count];
        modEffectArray = new PgModEffectCollectionEx(Skill_Key, TierList.Count);
        pgCombatModCollectionEx = new();

        int LastTierIndex = TierList.Count - 1;
        PgPowerTier LastTier = TierList[LastTierIndex];

        List<PgEffect>? CandidateEffectList = candidateEffectTable.ContainsKey(itemPower) ? candidateEffectTable[itemPower] : null;
        if (!AnalyzeRemainingPowers(LastTier, powerKey, unmatchedEffectList, CandidateEffectList, out List<CombatKeywordEx> ExtractedPowerTierKeywordList, out PgModEffectEx ExtractedModEffect, out PgCombatModEx pgCombatModEx, true))
            return false;

        PowerTierKeywordListArray[LastTierIndex] = ExtractedPowerTierKeywordList;
        modEffectArray.Items[LastTierIndex] = ExtractedModEffect;
        pgCombatModCollectionEx.Insert(0, pgCombatModEx);

        for (int i = 0; i + 1 < TierList.Count; i++)
        {
            if (!AnalyzeRemainingPowers(TierList[i], powerKey, null, CandidateEffectList, out List<CombatKeywordEx> ComparedPowerTierKeywordList, out PgModEffectEx ParsedModEffect, out pgCombatModEx, false))
                return false;

            bool AllowIncomplete = powerKey == "10402"
                ? i < 10
                : i < ValidationIndex;

            if (!IsSameCombatKeywordList(ExtractedPowerTierKeywordList, ComparedPowerTierKeywordList, AllowIncomplete))
            {
                Debug.WriteLine($"Mismatched power {powerKey} at tier #{i}.");
                return false;
            }

            PowerTierKeywordListArray[i] = ComparedPowerTierKeywordList;
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

    private bool AnalyzeRemainingPowers(PgPowerTier powerTier, string powerKey, List<PgEffect>? unmatchedEffectList, List<PgEffect>? candidateEffectList, out List<CombatKeywordEx> extractedPowerTierKeywordList, out PgModEffectEx modEffect, out PgCombatModEx pgCombatModEx, bool displayAnalysisResult)
    {
        extractedPowerTierKeywordList = new();
        modEffect = null!;
        pgCombatModEx = null!;

        IList<PgPowerEffect> EffectList = powerTier.EffectList;
        Debug.Assert(EffectList.Count == 1);
        PgPowerEffectSimple powerSimpleEffect = (PgPowerEffectSimple)EffectList[0];

        string ModText = powerSimpleEffect.Description;

        HackModText(ref ModText);

        AnalyzeText(ModText, true, false, out List<AbilityKeyword> ModAbilityList, out PgCombatEffectCollectionEx ModCombatList, out List<AbilityKeyword> ModTargetAbilityList);

        List<PgCombatEffectCollectionEx> EffectCombatList = new List<PgCombatEffectCollectionEx>();
        if (candidateEffectList != null)
        {
            foreach (PgEffect CandidateEffect in candidateEffectList)
            {
                string EffectText = CandidateEffect.Description;
                HackModText(ref EffectText);

                AnalyzeText(EffectText, false, false, out _, out PgCombatEffectCollectionEx EffectCombatCollection, out _);
                EffectCombatList.Add(EffectCombatCollection);
            }
        }
        else
            EffectCombatList = new List<PgCombatEffectCollectionEx>();

        string EffectKey = string.Empty;

        if (unmatchedEffectList != null)
        {
            if (EffectCombatList.Count > 0 && CombatEffectContains(ModCombatList, EffectCombatList, out int CandidateIndex, out _, out _) && candidateEffectList != null)
            {
                PgEffect CandidateEffect = candidateEffectList[CandidateIndex];
                EffectKey = CandidateEffect.Key;

                unmatchedEffectList.Remove(CandidateEffect);
            }
        }

        /*
        // Hack for Heart Thorn
        if (ModAbilityList.Count == 1 && ModAbilityList[0] == AbilityKeyword.HeartThorn &&
            ModCombatList.Count >= 1 && ModCombatList[0].Keyword == CombatKeywordEx.DealArmorDamage && ModCombatList[0].DamageType == GameDamageType.Internal_None &&
            ModText.Contains("coats the target in acid"))
        {
            ModCombatList[0].DamageType = GameDamageType.Acid;
        }

        // Hack for Poisoner's Cut
        if (ModAbilityList.Count == 1 && ModAbilityList[0] == AbilityKeyword.PoisonersCut &&
            ModCombatList.Count >= 1 && ModCombatList[0].Keyword == CombatKeyword.DirectOnlyDamageBoost && ModCombatList[0].DamageType == GameDamageType.Poison)
        {
            ModCombatList.Add(new PgCombatEffect() { Keyword = CombatKeyword.EffectDuration, Data = new PgNumericValue() { RawValue = 8 } });
        }

        if (ModText.Contains("leave a Lasting Mark in the target"))
        {
            ModCombatList.Clear();
            ModTargetAbilityList.Clear();
        }
        */

        if (EffectCombatList.Count == 0 && ModCombatList.Count == 0)
            return false;

        string ParsedAbilityList = AbilityKeywordListToShortString(ModAbilityList);
        string ParsedPowerString = CombatEffectListToString(ModCombatList, out extractedPowerTierKeywordList);
        string ParsedModTargetAbilityList = AbilityKeywordListToShortString(ModTargetAbilityList);

        if (displayAnalysisResult)
        {
            /*
            Debug.WriteLine("");
            Debug.WriteLine($"    Power: {powerSimpleEffect.Description}");
            Debug.WriteLine($"Parsed as: {{{ParsedAbilityList}}} {ParsedPowerString}, Target: {ParsedModTargetAbilityList}");
            */
        }

        modEffect = new PgModEffectEx()
        {
            EffectKey = EffectKey,
            Description = powerSimpleEffect.Description,
            AbilityList = ModAbilityList,
            StaticCombatEffectList = ModCombatList,
            TargetAbilityList = ModTargetAbilityList,
        };

        /*

        // Such an ugly hack...
        if (powerSimpleEffect.Description.EndsWith(", and Paradox Trot boosts Sprint Speed +1"))
        {
            PgModEffect SecondaryModEffect = new PgModEffect()
            {
                EffectKey = string.Empty,
                AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ParadoxTrot },
                StaticCombatEffectList = new PgCombatEffectCollection() { new PgCombatEffect() { Keyword = CombatKeyword.AddSprintSpeed, Data = new PgNumericValue() { RawValue = 1 } } },
                DynamicCombatEffectList = new PgCombatEffectCollection(),
                TargetAbilityList = new List<AbilityKeyword>(),
            };

            modEffect.SecondaryModEffect = SecondaryModEffect;
        }
        else if (modEffect.AbilityList.Count == 1 && modEffect.AbilityList[0] == AbilityKeyword.SongOfDiscord &&
                 modEffect.TargetAbilityList.Count == 1 && modEffect.TargetAbilityList[0] == AbilityKeyword.SongOfResurgence &&
                 modEffect.DynamicCombatEffectList.Count == 0 &&
                 modEffect.StaticCombatEffectList.Count == 5 && modEffect.StaticCombatEffectList[0].Keyword == CombatKeyword.ReflectOnAnyAttack &&
                 powerSimpleEffect.Description.Contains("and Song of Resurgence Healing"))
        {
            PgModEffect SecondaryModEffect = new PgModEffect()
            {
                EffectKey = string.Empty,
                AbilityList = new List<AbilityKeyword>() { modEffect.TargetAbilityList[0] },
                StaticCombatEffectList = new PgCombatEffectCollection(),
                DynamicCombatEffectList = new PgCombatEffectCollection(),
                TargetAbilityList = new List<AbilityKeyword>() { modEffect.TargetAbilityList[0] },
            };

            modEffect.TargetAbilityList.RemoveAt(0);
            modEffect.SecondaryModEffect = SecondaryModEffect;

            while (modEffect.StaticCombatEffectList.Count > 2)
            {
                SecondaryModEffect.DynamicCombatEffectList.Insert(0, modEffect.StaticCombatEffectList[modEffect.StaticCombatEffectList.Count - 1]);
                modEffect.StaticCombatEffectList.RemoveAt(modEffect.StaticCombatEffectList.Count - 1);
            }
        }
        else
        {
            if (ModAbilityList.Count > 0 && ModTargetAbilityList.Count > 0)
            {
                AbilityKeyword Ability = ModAbilityList[0];
                AbilityKeyword TargetAbility = ModTargetAbilityList[0];

                if (Ability != TargetAbility || ModTargetAbilityList.Count > 1)
                {
                    if (Ability == AbilityKeyword.Melee || Ability == AbilityKeyword.FillWithBile || Ability == AbilityKeyword.Deluge)
                    {
                    }

                    if (Ability != AbilityKeyword.Melee && Ability != AbilityKeyword.FillWithBile && Ability != AbilityKeyword.Deluge && ModCombatList.Count >= 2)
                    {
                        List<AbilityKeyword> TargetAbilityList = new(ModTargetAbilityList);

                        PgCombatEffectCollection FirstCombatEffects = new();
                        PgCombatEffectCollection SecondCombatEffects = new();
                        int CombatIndex = 0;
                        int Dividing = 1;

                        if (Ability == AbilityKeyword.EclipseOfShadows)
                            Dividing = ModCombatList.Count - 1;

                        for (; CombatIndex < Dividing && CombatIndex < ModCombatList.Count; CombatIndex++)
                            FirstCombatEffects.Add(ModCombatList[CombatIndex]);
                        for (; CombatIndex < ModCombatList.Count; CombatIndex++)
                            SecondCombatEffects.Add(ModCombatList[CombatIndex]);

                        PgCombatEffect FirstCombatEffect = FirstCombatEffects[0];
                        PgCombatEffect SecondCombatEffect = SecondCombatEffects[0];

                        if (FirstCombatEffect.Keyword != CombatKeyword.ReflectOnAnyAttack &&
                            FirstCombatEffect.Keyword != CombatKeyword.ApplyWithChance &&
                            FirstCombatEffect.Keyword != CombatKeyword.EffectRecurrence &&
                            SecondCombatEffect.Keyword != CombatKeyword.ResetOtherAbilityTimer && SecondCombatEffect.Keyword != CombatKeyword.EffectDuration && SecondCombatEffect.Keyword != CombatKeyword.WithinDistance)
                        {
                            if (SecondCombatEffect.Keyword == CombatKeyword.AddResetTimer)
                            {
                                modEffect.StaticCombatEffectList.Remove(SecondCombatEffect);
                                modEffect.DynamicCombatEffectList.Add(SecondCombatEffect);
                            }
                            else
                            {
                                PgModEffect SecondaryModEffect = new PgModEffect()
                                {
                                    EffectKey = string.Empty,
                                    AbilityList = TargetAbilityList,
                                    StaticCombatEffectList = SecondCombatEffects,
                                    DynamicCombatEffectList = new PgCombatEffectCollection(),
                                    TargetAbilityList = new List<AbilityKeyword>(),
                                };

                                foreach (var CombatEffect in SecondCombatEffects)
                                    modEffect.StaticCombatEffectList.Remove(CombatEffect);

                                modEffect.SecondaryModEffect = SecondaryModEffect;
                                modEffect.TargetAbilityList.Clear();
                            }
                        }
                    }
                }
            }
        }

        VerifyStaticEffects(ModAbilityList, ModCombatList);
        */

        BuildModEffect(powerKey, powerSimpleEffect.Description, ModAbilityList, ModCombatList, ModTargetAbilityList, out pgCombatModEx);

        if (pgCombatModEx.DynamicEffects.Count > 0 || pgCombatModEx.PermanentEffects.Count > 0)
            return true;
        else
            return false;
    }

    private void HackModText(ref string modText)
    {
        modText = modText.Replace("Indirect Poison and Indirect Trauma damage", "Indirect Poison and Trauma damage");
        modText = modText.Replace("Indirect Nature and Indirect Electricity damage", "Indirect Nature and Electricity damage");
        modText = modText.Replace("Indirect Nature and Indirect Trauma damage", "Indirect Nature and Trauma damage");
        modText = modText.Replace("Physical and Cold damage", "Crushing, Slashing, Piercing, and Cold damage");
        modText = modText.Replace(", but the ability's range is reduced to 12m", ", but range is reduced 18 meter");
        modText = modText.Replace("When you teleport via Shadow Feint", "When you teleport");
        //modText = modText.Replace("and Paradox Trot boosts Sprint Speed +1", string.Empty);
        modText = modText.Replace("Direct Electricity Damage, Direct Fire Damage, and Direct Cold Damage", "Direct Electricity, Fire, and Cold Damage");
        modText = modText.Replace("All Sword abilities apply Flaming Sword damage", "");

        if (!modText.Contains("But I Love You"))
            ReplaceCaseInsensitive(ref modText, " but ", " b*u*t ");
    }

    public static bool CombatEffectContains(List<PgCombatEffectEx> list, List<PgCombatEffectCollectionEx> candidateList, out int candidateIndex, out PgCombatEffectCollectionEx difference, out PgCombatEffectCollectionEx union)
    {
        Debug.Assert(candidateList.Count > 0);

        List<bool> ContainList = new List<bool>();
        List<PgCombatEffectCollectionEx> DifferenceList = new();
        List<PgCombatEffectCollectionEx> UnionList = new();
        List<int> UnionCountList = new List<int>();
        candidateIndex = -1;

        for (int i = 0; i < candidateList.Count; i++)
        {
            bool ContainValue = CombatEffectContains(list, candidateList[i], out PgCombatEffectCollectionEx DifferenceValue, out PgCombatEffectCollectionEx UnionValue);

            ContainList.Add(ContainValue);
            DifferenceList.Add(DifferenceValue);
            UnionList.Add(UnionValue);
            int UnionCount = UnionValue.Count;
            UnionCountList.Add(UnionCount);

            if (ContainValue)
            {
                if (candidateIndex < 0 && UnionValue.Count > 0)
                    candidateIndex = i;
                else if (candidateIndex >= 0)
                {
                    if (UnionCount > UnionCountList[candidateIndex])
                        candidateIndex = i;
                    else if (UnionCount == UnionCountList[candidateIndex])
                    {
                        // Debug.WriteLine("Multiple candidates found for a single matching effect");
                    }
                }
            }
        }

        if (candidateIndex >= 0)
        {
            difference = DifferenceList[candidateIndex];
            union = UnionList[candidateIndex];
            return true;
        }
        else
        {
            difference = new();
            union = new();
            return false;
        }
    }

    private void BuildModEffect(string powerKey, string description, List<AbilityKeyword> abilityList, PgCombatEffectCollectionEx modCombatList, List<AbilityKeyword> targetAbilityList, out PgCombatModEx pgCombatModEx)
    {
        PgCombatModEx pgExtraCombatModEx;

        switch (powerKey)
        {
            case "1204":
            case "13017":
            case "13153":
            case "13202":
            case "13252":
            case "21356":
            case "28082":
            case "28122":
                BuildUnmatchedMod_001(description, abilityList, modCombatList, targetAbilityList, out pgCombatModEx, ignoreModifierIndex: 0);
                break;
            case "11014":
            case "11015":
            case "20403":
                BuildUnmatchedMod_001(description, abilityList, modCombatList, targetAbilityList, out pgCombatModEx, ignoreModifierIndex: 1);
                break;

            case "Item_46059_0":
            case "Item_46052_0":
            case "Item_46051_0":
            case "Item_46036_0":
                BuildUnmatchedMod_001(description, abilityList, new() { modCombatList[0] }, targetAbilityList, out pgCombatModEx);
                BuildUnmatchedMod_001(description, new() { AbilityKeyword.Sword }, new() { new() { Keyword = CombatKeywordEx.FlamingSwordDamageOverTime, Data = new() }, modCombatList[1] }, targetAbilityList, out pgExtraCombatModEx);
                pgCombatModEx.DynamicEffects.AddRange(pgExtraCombatModEx.DynamicEffects);
                break;
            case "10315":
                BuildUnmatchedMod_001(description, abilityList, new() { modCombatList[3], modCombatList[0], modCombatList[1], modCombatList[2] }, targetAbilityList, out pgCombatModEx, ignoreModifierIndex: 0);
                break;
            case "17046":
                BuildUnmatchedMod_001(description, abilityList, new() { modCombatList[1], modCombatList[2], modCombatList[3] }, targetAbilityList, out pgCombatModEx);
                break;
            case "18133":
                BuildUnmatchedMod_001(description, abilityList, new() { modCombatList[0], modCombatList[2], modCombatList[1] }, targetAbilityList, out pgCombatModEx);
                break;
            case "23254":
                BuildUnmatchedMod_001(description, abilityList, modCombatList, abilityList, out pgCombatModEx);
                break;
            case "Item_42390_0":
            case "Item_42389_0":
            case "Item_42388_0":
            case "Item_42387_0":
            case "Item_42386_0":
            case "Item_42385_0":
            case "Item_42384_0":
            case "Item_41270_0":
            case "Item_41269_0":
            case "Item_41268_0":
            case "Item_41267_0":
            case "Item_41266_0":
            case "Item_41265_0":
            case "Item_41264_0":
            case "Item_40410_0":
            case "Item_40409_0":
            case "Item_40408_0":
            case "Item_40407_0":
            case "Item_40406_0":
            case "Item_40405_0":
            case "Item_40404_0":
            case "11403":
            case "11604":
                BuildUnmatchedMod_004(description, new(), modCombatList, targetAbilityList, out pgCombatModEx);
                break;
            case "27153":
            case "27154":
            case "27155":
            case "27156":
                BuildUnmatchedMod_001(description, new(), modCombatList, abilityList, out pgCombatModEx);
                break;
            case "17104":
            case "28241":
            case "3009":
                BuildUnmatchedMod_004(description, abilityList, modCombatList, targetAbilityList, out pgCombatModEx);
                break;
            case "17284":
                BuildUnmatchedMod_004(description, abilityList, modCombatList, targetAbilityList, out pgCombatModEx);
                pgCombatModEx.DynamicEffects[1].AbilityList.Clear();
                break;
            case "11051":
                BuildUnmatchedMod_001(description, targetAbilityList, new() { modCombatList[0], modCombatList[1] }, abilityList, out pgCombatModEx);
                BuildUnmatchedMod_004(description, new(), new() { modCombatList[0], modCombatList[2], modCombatList[3], modCombatList[4] }, abilityList, out pgExtraCombatModEx);
                pgCombatModEx.DynamicEffects.AddRange(pgExtraCombatModEx.DynamicEffects);
                break;
            case "156":
                BuildUnmatchedMod_001(description, abilityList, new() { modCombatList[0] }, new(), out pgCombatModEx);
                BuildUnmatchedMod_004(description, abilityList, new() { modCombatList[1], modCombatList[2] }, new(), out pgExtraCombatModEx);
                pgCombatModEx.DynamicEffects.AddRange(pgExtraCombatModEx.DynamicEffects);
                break;
            case "10010":
                BuildUnmatchedMod_001(description, abilityList, modCombatList, targetAbilityList, out pgCombatModEx);
                pgCombatModEx.DynamicEffects.Insert(0, pgCombatModEx.DynamicEffects[1]);
                pgCombatModEx.DynamicEffects.RemoveAt(2);
                pgCombatModEx.DynamicEffects[0].ConditionList.Clear();
                break;
            case "17205":
                BuildUnmatchedMod_001(description, abilityList, new() { modCombatList[0] }, new(), out pgCombatModEx);
                BuildUnmatchedMod_001(description, abilityList, new() { modCombatList[1] }, targetAbilityList, out pgExtraCombatModEx);
                pgCombatModEx.DynamicEffects.AddRange(pgExtraCombatModEx.DynamicEffects);
                break;
            case "11102":
                BuildUnmatchedMod_001(description, abilityList, new() { modCombatList[0], modCombatList[1], modCombatList[2], modCombatList[3] }, targetAbilityList, out pgCombatModEx);
                BuildUnmatchedMod_001(description, abilityList, new() { modCombatList[0], modCombatList[4], modCombatList[5] }, targetAbilityList, out pgExtraCombatModEx);
                pgCombatModEx.DynamicEffects.AddRange(pgExtraCombatModEx.DynamicEffects);
                break;
            case "11354":
                BuildUnmatchedMod_001(description, abilityList, new() { modCombatList[0], modCombatList[1] }, targetAbilityList, out pgCombatModEx);
                BuildUnmatchedMod_001(description, abilityList, new() { modCombatList[0], modCombatList[1], modCombatList[3] }, targetAbilityList, out pgExtraCombatModEx);
                pgCombatModEx.DynamicEffects.AddRange(pgExtraCombatModEx.DynamicEffects);
                break;
            case "21042":
                BuildUnmatchedMod_001(description, abilityList, new() { modCombatList[0], modCombatList[1] }, targetAbilityList, out pgCombatModEx);
                BuildUnmatchedMod_001(description, abilityList, new() { new PgCombatEffectEx() { Keyword = modCombatList[2].Keyword, DamageType = modCombatList[0].DamageType, Data = modCombatList[2].Data }, modCombatList[1], modCombatList[3] }, targetAbilityList, out pgExtraCombatModEx);
                pgCombatModEx.DynamicEffects.AddRange(pgExtraCombatModEx.DynamicEffects);
                break;
            case "18044":
            case "18045":
                BuildUnmatchedMod_001(description, abilityList, new() { modCombatList[0], modCombatList[1] }, targetAbilityList, out pgCombatModEx);
                BuildUnmatchedMod_001(description, abilityList, new() { modCombatList[2], modCombatList[3], modCombatList[4] }, targetAbilityList, out pgExtraCombatModEx);
                pgCombatModEx.DynamicEffects.AddRange(pgExtraCombatModEx.DynamicEffects);
                break;
            case "27152":
                BuildUnmatchedMod_001(description, abilityList, new() { modCombatList[0], modCombatList[1] }, targetAbilityList, out pgCombatModEx);
                BuildUnmatchedMod_001(description, abilityList, new() { modCombatList[2], modCombatList[3], modCombatList[4] }, targetAbilityList, out pgExtraCombatModEx, ignoreModifierIndex: 2);
                pgCombatModEx.DynamicEffects.AddRange(pgExtraCombatModEx.DynamicEffects);
                break;
            case "11401":
            case "17024":
                BuildUnmatchedMod_001(description, abilityList, new() { modCombatList[0] }, new(), out pgCombatModEx);
                BuildUnmatchedMod_001(description, targetAbilityList, new() { modCombatList[1] }, new(), out pgExtraCombatModEx);
                pgCombatModEx.DynamicEffects.AddRange(pgExtraCombatModEx.DynamicEffects);
                break;
            case "14252":
            case "14253":
            case "29946":
                BuildUnmatchedMod_001(description, abilityList, new() { modCombatList[0], modCombatList[1] }, new(), out pgCombatModEx);
                BuildUnmatchedMod_001(description, abilityList, new() { modCombatList[2] }, new(), out pgExtraCombatModEx);
                pgCombatModEx.DynamicEffects.AddRange(pgExtraCombatModEx.DynamicEffects);
                break;
            case "18074":
                BuildUnmatchedMod_001(description, abilityList, new() { modCombatList[0], modCombatList[1] }, new(), out pgCombatModEx);
                BuildUnmatchedMod_001(description, abilityList, new() { modCombatList[2] }, targetAbilityList, out pgExtraCombatModEx);
                pgCombatModEx.DynamicEffects.AddRange(pgExtraCombatModEx.DynamicEffects);
                break;
            case "28163":
                BuildUnmatchedMod_001(description, abilityList, new() { modCombatList[0], modCombatList[1] }, new(), out pgCombatModEx);
                BuildUnmatchedMod_001(description, targetAbilityList, new() { modCombatList[2] }, new(), out pgExtraCombatModEx);
                pgCombatModEx.DynamicEffects.AddRange(pgExtraCombatModEx.DynamicEffects);
                break;
            case "11553":
            case "15101":
            case "21153":
            case "25303":
            case "27052":
                BuildUnmatchedMod_001(description, abilityList, new() { modCombatList[0] }, new(), out pgCombatModEx);
                BuildUnmatchedMod_001(description, abilityList, new() { modCombatList[1], modCombatList[2] }, new(), out pgExtraCombatModEx);
                pgCombatModEx.DynamicEffects.AddRange(pgExtraCombatModEx.DynamicEffects);
                break;
            case "18037":
            case "18112":
            case "18113":
                BuildUnmatchedMod_001(description, abilityList, new() { modCombatList[0] }, new(), out pgCombatModEx);
                BuildUnmatchedMod_001(description, new(), new() { modCombatList[1], modCombatList[2] }, targetAbilityList, out pgExtraCombatModEx);
                pgCombatModEx.DynamicEffects.AddRange(pgExtraCombatModEx.DynamicEffects);
                break;
            case "11651":
                BuildUnmatchedMod_001(description, abilityList, new() { modCombatList[0], modCombatList[1] }, new(), out pgCombatModEx);
                BuildUnmatchedMod_001(description, abilityList, new() { modCombatList[2], modCombatList[3] }, new(), out pgExtraCombatModEx);
                pgCombatModEx.DynamicEffects.AddRange(pgExtraCombatModEx.DynamicEffects);
                break;
            case "16143":
                BuildUnmatchedMod_001(description, abilityList, new() { modCombatList[0] }, new(), out pgCombatModEx);
                BuildUnmatchedMod_001(description, abilityList, new() { modCombatList[1], modCombatList[2], modCombatList[3] }, new(), out pgExtraCombatModEx);
                pgCombatModEx.DynamicEffects.AddRange(pgExtraCombatModEx.DynamicEffects);
                break;
            case "17045":
                BuildUnmatchedMod_001(description, new(), new() { modCombatList[0], modCombatList[1], modCombatList[3] }, abilityList, out pgCombatModEx);
                BuildUnmatchedMod_001(description, new(), new() { modCombatList[0], modCombatList[2], modCombatList[3] }, targetAbilityList, out pgExtraCombatModEx);
                pgCombatModEx.DynamicEffects.AddRange(pgExtraCombatModEx.DynamicEffects);
                break;
            case "18132":
                BuildUnmatchedMod_001(description, abilityList, new() { modCombatList[0], modCombatList[1], modCombatList[2] }, new(), out pgCombatModEx);
                BuildUnmatchedMod_001(description, abilityList, new() { modCombatList[0], modCombatList[3], modCombatList[4] }, new(), out pgExtraCombatModEx);
                pgCombatModEx.DynamicEffects.AddRange(pgExtraCombatModEx.DynamicEffects);
                break;
            case "20018":
                BuildUnmatchedMod_001(description, abilityList, new() { modCombatList[0], modCombatList[3] }, new(), out pgCombatModEx);
                BuildUnmatchedMod_001(description, abilityList, new() { modCombatList[1], modCombatList[2], modCombatList[3] }, new(), out pgExtraCombatModEx);
                pgCombatModEx.DynamicEffects.AddRange(pgExtraCombatModEx.DynamicEffects);
                break;
            case "20201":
                BuildUnmatchedMod_001(description, abilityList, new() { modCombatList[0], modCombatList[1], modCombatList[2], modCombatList[3] }, new(), out pgCombatModEx);
                BuildUnmatchedMod_001(description, abilityList, new() { modCombatList[0], modCombatList[4], modCombatList[5] }, new(), out pgExtraCombatModEx);
                pgCombatModEx.DynamicEffects.AddRange(pgExtraCombatModEx.DynamicEffects);
                break;
            case "3008":
                BuildUnmatchedMod_001(description, new(), new() { modCombatList[0], modCombatList[1] }, new(), out pgCombatModEx);
                BuildUnmatchedMod_001(description, new(), new() { modCombatList[0], modCombatList[2], modCombatList[3] }, abilityList, out pgExtraCombatModEx);
                pgCombatModEx.DynamicEffects.AddRange(pgExtraCombatModEx.DynamicEffects);
                break;
            case "2205":
                BuildUnmatchedMod_001(description, new(), new() { modCombatList[0], modCombatList[1], modCombatList[2], modCombatList[3] }, abilityList, out pgCombatModEx);
                BuildUnmatchedMod_001(description, targetAbilityList, new() { modCombatList[4] }, new(), out pgExtraCombatModEx);
                pgCombatModEx.DynamicEffects.AddRange(pgExtraCombatModEx.DynamicEffects);
                break;
            case "25302":
                BuildUnmatchedMod_001(description, abilityList, new() { modCombatList[0] }, new(), out pgCombatModEx);
                BuildUnmatchedMod_004(description, targetAbilityList, new() { modCombatList[1], modCombatList[2] }, abilityList, out pgExtraCombatModEx);
                pgCombatModEx.DynamicEffects.AddRange(pgExtraCombatModEx.DynamicEffects);
                break;
            case "28623":
                BuildUnmatchedMod_001(description, abilityList, new() { modCombatList[0], modCombatList[1] }, new(), out pgCombatModEx);
                BuildUnmatchedMod_001(description, targetAbilityList, new() { modCombatList[2] }, new(), out pgExtraCombatModEx);
                pgCombatModEx.DynamicEffects.AddRange(pgExtraCombatModEx.DynamicEffects);
                break;
            case "Item_55613_0":
            case "Item_55619_0":
            case "Item_55620_0":
            case "Item_55621_0":
            case "Item_55622_0":
            case "Item_55623_0":
            case "Item_55624_0":
            case "Item_55625_0":
            case "Item_55631_0":
            case "Item_55632_0":
            case "Item_55633_0":
            case "Item_55634_0":
            case "Item_55635_0":
            case "Item_55524_0":
            case "Item_55509_0":
            case "Item_54628_0":
            case "Item_54627_0":
            case "Item_54626_0":
            case "Item_54625_0":
            case "Item_54624_0":
            case "Item_54623_0":
            case "Item_54622_0":
            case "Item_54621_0":
            case "Item_54620_0":
            case "Item_54619_0":
            case "Item_54618_0":
            case "Item_54617_0":
            case "Item_54616_0":
            case "Item_54302_0":
            case "Item_54301_0":
            case "Item_54008_0":
            case "Item_54006_0":
            case "Item_54005_0":
            case "Item_54004_0":
            case "Item_52611_0":
            case "Item_54007_0":
            case "Item_54003_0":
            case "Item_54002_0":
            case "Item_54001_0":
            case "Item_52038_0":
            case "Item_52030_0":
            case "Item_52028_0":
            case "Item_51029_0":
            case "Item_49070_0":
            case "Item_49069_0":
            case "Item_49068_0":
            case "Item_49067_0":
            case "Item_49066_0":
            case "Item_49065_0":
            case "Item_49064_0":
            case "Item_49063_0":
            case "Item_49062_0":
            case "Item_48182_0":
            case "Item_48181_0":
            case "Item_48101_0":
            case "Item_48100_0":
            case "Item_48099_0":
            case "Item_48098_0":
            case "Item_48097_0":
            case "Item_48096_0":
            case "Item_48095_0":
            case "Item_48094_0":
            case "Item_48093_0":
            case "Item_48091_0":
            case "Item_48090_0":
            case "Item_48089_0":
            case "Item_48088_0":
            case "Item_48087_0":
            case "Item_48086_0":
            case "Item_48085_0":
            case "Item_48084_0":
            case "Item_48083_0":
            case "Item_47068_0":
            case "Item_47052_0":
            case "Item_47051_0":
            case "Item_47042_0":
            case "Item_47040_0":
            case "Item_46736_0":
            case "Item_46735_0":
            case "Item_46734_0":
            case "Item_46723_0":
            case "Item_46521_0":
            case "Item_46055_0":
            case "Item_46045_0":
            case "Item_46026_0":
            case "Item_46014_0":
            case "Item_46013_1":
            case "Item_46013_0":
            case "Item_46012_1":
            case "Item_46012_0":
            case "Item_45721_0":
            case "Item_45641_0":
            case "Item_45637_0":
            case "Item_45533_0":
            case "Item_44551_0":
            case "Item_44310_0":
            case "Item_44309_0":
            case "Item_43119_0":
            case "Item_43118_0":
            case "Item_42470_0":
            case "Item_42469_0":
            case "Item_42468_0":
            case "Item_42467_0":
            case "Item_42466_0":
            case "Item_42450_0":
            case "Item_42449_0":
            case "Item_42448_0":
            case "Item_42447_0":
            case "Item_42446_0":
            case "Item_42360_0":
            case "Item_42359_0":
            case "Item_42358_0":
            case "Item_42357_0":
            case "Item_42356_0":
            case "Item_42355_0":
            case "Item_42354_0":
            case "Item_42353_0":
            case "Item_42352_0":
            case "Item_42351_0":
            case "Item_42094_0":
            case "Item_42091_0":
            case "Item_42087_0":
            case "Item_42058_0":
            case "Item_42057_0":
            case "Item_41310_0":
            case "Item_41309_0":
            case "Item_40312_0":
            case "Item_40311_0":
            case "10004":
            case "10005":
            case "10007":
            case "10008":
            case "10042":
            case "1005":
            case "1007":
            case "10123":
            case "10201":
            case "10202":
            case "1021":
            case "1025":
            case "1029":
            case "10301":
            case "10304":
            case "10305":
            case "10308":
            case "10310":
            case "10311":
            case "10312":
            case "10402":
            case "Item_42095_0":
            case "Item_42085_0":
            case "1041":
            case "1043":
            case "10455":
            case "10502":
            case "10554":
            case "1066":
            case "1085":
            case "11012":
            case "11013":
            case "11103":
            case "11105":
            case "11201":
            case "11253":
            case "11351":
            case "11352":
            case "11402":
            case "11451":
            case "11453":
            case "11454":
            case "11471":
            case "11472":
            case "11552":
            case "11602":
            case "11405":
            case "11704":
            case "12334":
            case "1253":
            case "13005":
            case "13007":
            case "13011":
            case "13052":
            case "13058":
            case "13101":
            case "13102":
            case "13203":
            case "13401":
            case "13402":
            case "1353":
            case "14004":
            case "14006":
            case "14007":
            case "14012":
            case "14013":
            case "14015":
            case "14016":
            case "13006":
            case "1402":
            case "14052":
            case "14102":
            case "14103":
            case "14157":
            case "14159":
            case "14203":
            case "14204":
            case "14251":
            case "14254":
            case "14255":
            case "14355":
            case "14501":
            case "14502":
            case "14504":
            case "1452":
            case "1453":
            case "1454":
            case "14552":
            case "14604":
            case "15051":
            case "15053":
            case "15151":
            case "15154":
            case "15251":
            case "15302":
            case "15304":
            case "15354":
            case "15453":
            case "16005":
            case "16007":
            case "11705":
            case "16010":
            case "16041":
            case "16061":
            case "16064":
            case "16065":
            case "16081":
            case "16122":
            case "16141":
            case "16144":
            case "16162":
            case "16163":
            case "16181":
            case "16183":
            case "17003":
            case "17062":
            case "17102":
            case "17103":
            case "17142":
            case "17161":
            case "17202":
            case "17204":
            case "17263":
            case "17281":
            case "17282":
            case "17283":
            case "18002":
            case "18003":
            case "18021":
            case "18022":
            case "18046":
            case "18053":
            case "18073":
            case "18084":
            case "18093":
            case "18111":
            case "18116":
            case "18121":
            case "18134":
            case "20008":
            case "20010":
            case "20014":
            case "20015":
            case "20017":
            case "2004":
            case "20063":
            case "20064":
            case "20104":
            case "2012":
            case "2024":
            case "20351":
            case "20356":
            case "20402":
            case "2053":
            case "2060":
            case "20103":
            case "21003":
            case "21006":
            case "2104":
            case "21066":
            case "21084":
            case "21101":
            case "21102":
            case "21152":
            case "21304":
            case "21354":
            case "21355":
            case "21358":
            case "2152":
            case "2154":
            case "22079":
            case "22080":
            case "22086":
            case "22090":
            case "22202":
            case "22352":
            case "23024":
            case "23025":
            case "2304":
            case "23102":
            case "23103":
            case "23251":
            case "23402":
            case "23452":
            case "23503":
            case "23505":
            case "23551":
            case "23602":
            case "24004":
            case "24034":
            case "24091":
            case "24092":
            case "24093":
            case "24096":
            case "24123":
            case "24124":
            case "24153":
            case "24183":
            case "24184":
            case "24213":
            case "24214":
            case "24283":
            case "24284":
            case "24304":
            case "24305":
            case "25024":
            case "25133":
            case "25135":
            case "25195":
            case "25221":
            case "25305":
            case "25351":
            case "25353":
            case "26006":
            case "26054":
            case "26153":
            case "26173":
            case "26204":
            case "26222":
            case "26243":
            case "27012":
            case "27013":
            case "27031":
            case "27032":
            case "27053":
            case "27056":
            case "27072":
            case "27074":
            case "27092":
            case "27093":
            case "27094":
            case "27112":
            case "27113":
            case "27114":
            case "27115":
            case "27133":
            case "27151":
            case "27192":
            case "28066":
            case "28083":
            case "28084":
            case "28102":
            case "28106":
            case "28123":
            case "28125":
            case "28183":
            case "28185":
            case "28622":
            case "28624":
            case "28663":
            case "28664":
            case "28665":
            case "28681":
            case "28682":
            case "28701":
            case "28702":
            case "28703":
            case "28704":
            case "28742":
            case "28743":
            case "28744":
            case "28761":
            case "28763":
            case "28781":
            case "28786":
            case "28787":
            case "28801":
            case "28802":
            case "28803":
            case "28804":
            case "3002":
                BuildUnmatchedMod_001(description, abilityList, modCombatList, targetAbilityList, out pgCombatModEx);
                break;

            default:
                //pgCombatModEx = new() { Description = description, DynamicEffects = new(), PermanentEffects = new() };
                BuildUnmatchedMod_001(description, abilityList, modCombatList, targetAbilityList, out pgCombatModEx);
                break;
            case "Other":
                BuildUnmatchedMod_001(description, abilityList, modCombatList, targetAbilityList, out pgCombatModEx);
                break;
        }
    }

    private void BuildUnmatchedMod_001(string description, List<AbilityKeyword> abilityList, PgCombatEffectCollectionEx allEffects, List<AbilityKeyword> targetAbilityList, out PgCombatModEx pgCombatModEx, int ignoreModifierIndex = -1)
    {
        List<PgPermanentModEffectEx> PermanentEffects = new();
        List<PgCombatModEffectEx> DynamicEffects = new();

        float RandomChance = GetValueAndRemove(allEffects, CombatKeywordEx.ApplyWithChance, asProbability: true);
        float DelayInSeconds = GetValueAndRemove(allEffects, CombatKeywordEx.EffectDelay);
        float DurationInSeconds = GetValueAndRemove(allEffects, CombatKeywordEx.EffectDuration);
        float DurationInMinutes = GetValueAndRemove(allEffects, CombatKeywordEx.EffectDurationInMinutes);
        if (float.IsNaN(DurationInSeconds) && !float.IsNaN(DurationInMinutes))
            DurationInSeconds = DurationInMinutes * 60;
        float DurationOverTime = GetValueAndRemove(allEffects, CombatKeywordEx.EffectOverTime);
        bool IsPerSecond = allEffects.Exists(Item => Item.Keyword == CombatKeywordEx.EffectEverySecond);
        float RecurringDelay = GetValueAndRemove(allEffects, CombatKeywordEx.RecurringEffect);
        float TargetRange = GetValueAndRemove(allEffects, CombatKeywordEx.TargetRange);

        PgCombatConditionCollectionEx ConditionList = new();
        List<AbilityKeyword> ConditionAbilityList = new();
        float ConditionValue = float.NaN;
        float ConditionPercentage = float.NaN;
        int ConditionIndex = -1;
        GameDamageType ConditionDamageType = GameDamageType.Internal_None;
        GameCombatSkill ConditionSkill = GameCombatSkill.Internal_None;
        for (int i = 0; i < allEffects.Count; i++)
        {
            PgCombatEffectEx CombatEffect = allEffects[i];
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

                if (NewCondition == CombatCondition.MinimumDistance || NewCondition == CombatCondition.NotAttackedInThePast)
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

                if (NewCondition == CombatCondition.ActiveSkill ||
                    NewCondition == CombatCondition.WhileChanneling ||
                    NewCondition == CombatCondition.UsingCombatSkill)
                {
                    ConditionSkill = CombatEffect.CombatSkill;
                }
            }
        }

        bool IsFireDamage = false;
        bool IsTargetAbilityListUsed = false;

        for (int i = 0; i < allEffects.Count; i++)
        {
            PgCombatEffectEx CombatEffect = allEffects[i];
            CombatKeywordEx CombatKeyword = CombatEffect.Keyword;
            bool CanApplyModifier = ignoreModifierIndex < 0 ||
                                    ((i != (ignoreModifierIndex % 1000)) &&
                                     (ignoreModifierIndex < 1000 || (i != ((ignoreModifierIndex / 1000) % 1000))));

            if (CombatKeyword == CombatKeywordEx.ApplyWithChance ||
                CombatKeyword == CombatKeywordEx.ApplyToSelf ||
                CombatKeyword == CombatKeywordEx.ApplyToAllies ||
                CombatKeyword == CombatKeywordEx.ApplyToSelfAndAllies ||
                CombatKeyword == CombatKeywordEx.ApplyToPet ||
                CombatKeyword == CombatKeywordEx.ApplyToCharmedPet ||
                CombatKeyword == CombatKeywordEx.ApplyToSelfAndPet ||
                CombatKeyword == CombatKeywordEx.ApplyToPetOfTarget ||
                CombatKeyword == CombatKeywordEx.ApplyToConjuredAndPet ||
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

            if (CombatKeyword == CombatKeywordEx.Ignite)
            {
                IsFireDamage = true;
                continue;
            }

            GameDamageType DamageType = CombatEffect.DamageType;
            if (DamageType == GameDamageType.Internal_None && IsFireDamage)
                DamageType = GameDamageType.Fire;

            if (ConditionList.Exists(condition => condition == CombatCondition.SpecificDirectDamageType) && CombatKeyword == CombatKeywordEx.DamageBoost)
            {
                Debug.Assert(CombatEffect.DamageType == GameDamageType.Internal_None);
                DamageType = ConditionDamageType;
            }

            bool CanHaveSpecialDuration = false;
            if (OverTimeEffects.TryGetValue(CombatKeyword, out CombatKeywordEx CombatKeywordOverTime))
            {
                if (float.IsNaN(DurationInSeconds) && !float.IsNaN(DurationOverTime))
                {
                    DurationInSeconds = DurationOverTime;
                    CombatKeyword = CombatKeywordOverTime;
                }
                else if (CombatKeywordOverTime == CombatKeywordEx.RestoreArmorOverTime && !float.IsNaN(DurationInSeconds) && float.IsNaN(DurationOverTime))
                {
                    CanHaveSpecialDuration = true;
                }
                else if (IsPerSecond)
                {
                    CombatKeyword = CombatKeywordOverTime;
                }
            }

            bool CanHaveDuration = CanHaveSpecialDuration  ||
                                   CombatKeyword == CombatKeywordEx.AddSprintSpeed ||
                                   CombatKeyword == CombatKeywordEx.AddFlySpeed ||
                                   CombatKeyword == CombatKeywordEx.AddSwimSpeed ||
                                   CombatKeyword == CombatKeywordEx.AddOutOfCombatSpeed ||
                                   CombatKeyword == CombatKeywordEx.RestoreHealth ||
                                   CombatKeyword == CombatKeywordEx.RestoreHealthOverTime ||
                                   CombatKeyword == CombatKeywordEx.RestorePowerOverTime ||
                                   CombatKeyword == CombatKeywordEx.RestoreArmorOverTime ||
                                   CombatKeyword == CombatKeywordEx.RestoreHealthOrArmorOverTime ||
                                   CombatKeyword == CombatKeywordEx.DamageOverTimeBoost ||
                                   CombatKeyword == CombatKeywordEx.HealthDamageOverTimeBoost ||
                                   CombatKeyword == CombatKeywordEx.ArmorDamageOverTimeBoost ||
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
                                   CombatKeyword == CombatKeywordEx.Stun ||
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
                                   CombatKeyword == CombatKeywordEx.DrainHealthOverTime ||
                                   CombatKeyword == CombatKeywordEx.GiveBuff;
            bool CanHaveDelay = CombatKeyword != CombatKeywordEx.AddSprintSpeed;

            float EffectValue = CombatEffect.Data.RawValue.HasValue ? CombatEffect.Data.RawValue.Value : float.NaN;
            bool EffectIsPercent = CombatEffect.Data.RawIsPercent.HasValue ? CombatEffect.Data.RawIsPercent.Value : false;
            PgNumericValueEx pgNumericValueEx = new() { Value = EffectValue, IsPercent = EffectIsPercent };

            bool IsEveryOtherUse = false;
            GetTargets(allEffects, i + 1, abilityList, out CombatTarget Target, out CombatTarget OtherTarget, ref IsEveryOtherUse);
            if (Target == CombatTarget.Internal_None)
                GetTargets(allEffects, i - 1, abilityList, out Target, out OtherTarget, ref IsEveryOtherUse);

            bool CanHaveTarget = (CombatKeyword != CombatKeywordEx.IncreaseCurrentRefreshTime || Target != CombatTarget.Self) &&
                                 (CombatKeyword != CombatKeywordEx.ResetRefreshTime || Target != CombatTarget.Self) &&
                                 (CombatKeyword != CombatKeywordEx.ZeroPowerCost) &&
                                 (CombatKeyword != CombatKeywordEx.DamageBoost) &&
                                 (CombatKeyword != CombatKeywordEx.DealArmorDamage) &&
                                 (CombatKeyword != CombatKeywordEx.DealHealthDamage) &&
                                 (CombatKeyword != CombatKeywordEx.DealHealthAndArmorDamage) &&
                                 (CombatKeyword != CombatKeywordEx.IncreasePowerCost) &&
                                 (CombatKeyword != CombatKeywordEx.AddOutOfCombatSpeed) &&
                                 (CombatKeyword != CombatKeywordEx.NextAttackMiss) &&
                                 (CombatKeyword != CombatKeywordEx.AddMitigation) &&
                                 (CombatKeyword != CombatKeywordEx.AddMitigationDirect) &&
                                 (CombatKeyword != CombatKeywordEx.AddMitigationIndirect) &&
                                 (CombatKeyword != CombatKeywordEx.AddMitigationBurst) &&
                                 //(CombatKeyword != CombatKeywordEx.AddMitigationPhysical) &&
                                 (CombatKeyword != CombatKeywordEx.AddMitigationElemental) &&
                                 (CombatKeyword != CombatKeywordEx.IncreaseMeleePowerCost);

            List<AbilityKeyword> TargetAbilityList = new();
            if (Target == CombatTarget.Internal_None &&
                (CombatKeyword == CombatKeywordEx.IncreaseCurrentRefreshTime ||
                 CombatKeyword == CombatKeywordEx.ResetRefreshTime ||
                 CombatKeyword == CombatKeywordEx.ZeroPowerCost ||
                 CombatKeyword == CombatKeywordEx.GrantCriticalChance ||
                 CombatKeyword == CombatKeywordEx.NoYellForHelp ||
                 CombatKeyword == CombatKeywordEx.ChangeAbilityType ||
                 CombatKeyword == CombatKeywordEx.IncreasePowerCost ||
                 CombatKeyword == CombatKeywordEx.IncreaseMeleePowerCost))
            {
                if (targetAbilityList.Count > 0)
                {
                    TargetAbilityList = targetAbilityList;
                    IsTargetAbilityListUsed = true;
                }
                /*else if (abilityList.Count > 0)
                {
                    Debug.Assert(abilityList.Count == 1);
                    TargetAbilityList = new() { abilityList[0] };
                }*/
            }
            else if (Target == CombatTarget.Internal_None &&
                     (CombatKeyword == CombatKeywordEx.ApplyOtherMods ||
                      CombatKeyword == CombatKeywordEx.DamageBoost ||
                      CombatKeyword == CombatKeywordEx.DealArmorDamage ||
                      CombatKeyword == CombatKeywordEx.DealHealthDamage ||
                      CombatKeyword == CombatKeywordEx.DealHealthAndArmorDamage) &&
                     targetAbilityList.Count > 0 &&
                     !IsTargetAbilityListUsed)
            {
                TargetAbilityList = targetAbilityList;
            }
            else if (Target == CombatTarget.Self && CombatKeyword == CombatKeywordEx.TurnMitigationToDamage)
            {
                TargetAbilityList = targetAbilityList;
            }

            PgCombatModEffectEx pgCombatModEffectEx = new()
            {
                Keyword = CombatKeyword,
                AbilityList = new List<AbilityKeyword>(abilityList),
                Data = pgNumericValueEx,
                DamageType = DamageType,
                CombatSkill = ConditionSkill != GameCombatSkill.Internal_None ? ConditionSkill : CombatEffect.CombatSkill,
                RandomChance = CanApplyModifier ? RandomChance : float.NaN,
                DelayInSeconds = CanHaveDelay && CanApplyModifier ? DelayInSeconds : float.NaN,
                DurationInSeconds = CanHaveDuration && CanApplyModifier ? DurationInSeconds : float.NaN,
                RecurringDelay = CanApplyModifier ? RecurringDelay : float.NaN,
                Target = CanHaveTarget && CanApplyModifier ? Target : CombatTarget.Internal_None,
                TargetRange = CanApplyModifier ? TargetRange : float.NaN,
                TargetAbilityList = TargetAbilityList,
                ConditionList = CanApplyModifier ? new(ConditionList) : new(),
                ConditionAbilityList = CanApplyModifier ? ConditionAbilityList : new(),
                ConditionValue = CanApplyModifier ? ConditionValue : float.NaN,
                ConditionPercentage = CanApplyModifier ? ConditionPercentage : float.NaN,
                IsEveryOtherUse = IsEveryOtherUse,
            };

            DynamicEffects.Add(pgCombatModEffectEx);
        }

        pgCombatModEx = new()
        {
            Description = description,
            PermanentEffects = PermanentEffects,
            DynamicEffects = DynamicEffects,
        };
    }

    private void BuildUnmatchedMod_004(string description, List<AbilityKeyword> abilityList, PgCombatEffectCollectionEx allEffects, List<AbilityKeyword> targetAbilityList, out PgCombatModEx pgCombatModEx)
    {
        float RecurringDelay = float.NaN;
        CombatKeywordEx Keyword = CombatKeywordEx.GiveBuff;
        CombatTarget Target = CombatTarget.Internal_None;

        PgCombatConditionCollectionEx ConditionList = new();
        List<AbilityKeyword> ConditionAbilityList = new();
        float ConditionValue = float.NaN;
        float ConditionPercentage = float.NaN;
        int ConditionIndex = -1;
        GameDamageType ConditionDamageType = GameDamageType.Internal_None;
        GameCombatSkill ConditionSkill = GameCombatSkill.Internal_None;
        for (int i = 0; i < allEffects.Count; i++)
        {
            PgCombatEffectEx CombatEffect = allEffects[i];
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

                if (NewCondition == CombatCondition.ActiveSkill ||
                    NewCondition == CombatCondition.WhileChanneling ||
                    NewCondition == CombatCondition.UsingCombatSkill)
                {
                    ConditionSkill = CombatEffect.CombatSkill;
                }

                allEffects.RemoveAt(i);
                i--;
            }
        }

        for (int i = 0; i < allEffects.Count; i++)
        {
            PgCombatEffectEx CombatEffect = allEffects[i];

            if (CombatEffect.Keyword == CombatKeywordEx.RecurringEffect)
            {
                float? RawValue = CombatEffect.Data.RawValue;
                Debug.Assert(RawValue != null);
                RecurringDelay = RawValue!.Value;
                allEffects.RemoveAt(i);
                i--;
            }
            else if (CombatEffect.Keyword == CombatKeywordEx.NextAttack)
            {
                Keyword = CombatKeywordEx.GiveBuffOneAttack;

                allEffects.RemoveAt(i);
                i--;
            }
            else if (CombatEffect.Keyword == CombatKeywordEx.NextHit)
            {
                Keyword = CombatKeywordEx.GiveBuffOneHit;

                allEffects.RemoveAt(i);
                i--;
            }
            else if (CombatEffect.Keyword == CombatKeywordEx.ApplyToSelf)
            {
                Target = CombatTarget.Self;
                allEffects.RemoveAt(i);
                i--;
            }
        }

        BuildUnmatchedMod_001(description, abilityList, allEffects, targetAbilityList, out pgCombatModEx);

        PgCombatModEffectEx pgCombatModEffectEx = new()
        {
            Keyword = Keyword,
            AbilityList = new List<AbilityKeyword>(abilityList),
            Data = PgNumericValueEx.Empty,
            CombatSkill = ConditionSkill,
            RecurringDelay = RecurringDelay,
            Target = Target,
            ConditionList = new(ConditionList),
            ConditionAbilityList = ConditionAbilityList,
            ConditionValue = ConditionValue,
            ConditionPercentage = ConditionPercentage,
        };

        pgCombatModEx.DynamicEffects.Insert(0, pgCombatModEffectEx);
    }

    private void BuildUnmatchedMod_006(string description, List<AbilityKeyword> abilityList, PgCombatEffectCollectionEx allEffectList, List<AbilityKeyword> targetAbilityList, out PgCombatModEx pgCombatModEx, int ignoreModifierIndex = -1)
    {
        float DelayInSeconds = GetValueAndRemove(allEffectList, CombatKeywordEx.EffectDelay);
        float DurationInSeconds = GetValueAndRemove(allEffectList, CombatKeywordEx.EffectDuration);
        float RecurringDelay = GetValueAndRemove(allEffectList, CombatKeywordEx.RecurringEffect);
        float DurationOverTime = GetValueAndRemove(allEffectList, CombatKeywordEx.EffectOverTime);

        CombatTarget Target = CombatTarget.Internal_None;

        if (abilityList.Count >= 1)
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
        foreach (PgCombatEffectEx CombatEffect in allEffectList)
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
        for (int i = 0; i < allEffectList.Count; i++)
        {
            PgCombatEffectEx CombatEffect = allEffectList[i];
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
}
