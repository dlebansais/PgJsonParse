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
    private void AnalyzeRemainingPowers(List<string[]> stringKeyTable, List<PgModEffectCollectionEx> analyzedPowerKeyToCompleteEffectTable)
    {
        foreach (PgPower ItemPower in unmatchedPowerList)
        {
            if (!KnownCombatPowers.KnownUnmatchedPowers.ContainsKey(ItemPower.Key))
            {
                AnalyzeRemainingPowers(ItemPower, out string[] stringKeyArray, out PgModEffectCollectionEx ModEffectArray, out PgCombatModCollectionEx pgCombatModCollectionEx);

                stringKeyTable.Add(stringKeyArray);
                analyzedPowerKeyToCompleteEffectTable.Add(ModEffectArray);
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
        AnalyzeRemainingPowers(LastTier, powerKey, unmatchedEffectList, CandidateEffectList, out List<CombatKeywordEx> ExtractedPowerTierKeywordList, out PgModEffectEx ExtractedModEffect, out PgCombatModEx pgCombatModEx, true);
        PowerTierKeywordListArray[LastTierIndex] = ExtractedPowerTierKeywordList;
        modEffectArray.Items[LastTierIndex] = ExtractedModEffect;
        pgCombatModCollectionEx.Insert(0, pgCombatModEx);

        for (int i = 0; i + 1 < TierList.Count; i++)
        {
            AnalyzeRemainingPowers(TierList[i], powerKey, null, CandidateEffectList, out List<CombatKeywordEx> ComparedPowerTierKeywordList, out PgModEffectEx ParsedModEffect, out pgCombatModEx, false);

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

        if (EffectCombatList.Count > 0 && CombatEffectContains(ModCombatList, EffectCombatList, out int CandidateIndex, out _, out _) && candidateEffectList != null)
        {
            PgEffect CandidateEffect = candidateEffectList[CandidateIndex];
            EffectKey = CandidateEffect.Key;

            if (unmatchedEffectList != null)
                unmatchedEffectList.Remove(CandidateEffect);
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

        return true;
    }

    private void HackModText(ref string modText)
    {
        modText = modText.Replace("Indirect Poison and Indirect Trauma damage", "Indirect Poison and Trauma damage");
        modText = modText.Replace("Indirect Nature and Indirect Electricity damage", "Indirect Nature and Electricity damage");
        modText = modText.Replace("Indirect Nature and Indirect Trauma damage", "Indirect Nature and Trauma damage");
        modText = modText.Replace("Physical and Cold damage", "Crushing, Slashing, Piercing, and Cold damage");
        modText = modText.Replace(", but the ability's range is reduced to 12m", ", but range is reduced 18 meter");
        modText = modText.Replace("When you teleport via Shadow Feint", "When you teleport");
        modText = modText.Replace("and Paradox Trot boosts Sprint Speed +1", string.Empty);
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
                        Debug.WriteLine("Multiple candidates found for a single matching effect");
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
                BuildUnmatchedMod_004(description, abilityList, modCombatList, targetAbilityList, out pgCombatModEx);
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
            case "Item_42095_0":
            case "Item_42091_0":
            case "Item_42087_0":
            case "Item_42085_0":
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
            case "10010":
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
                BuildUnmatchedMod_001(description, abilityList, modCombatList, targetAbilityList, out pgCombatModEx);
                break;

            default:
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

            if (CombatKeyword == CombatKeywordEx.Ignite)
            {
                IsFireDamage = true;
                continue;
            }

            GameDamageType DamageType = CombatEffect.DamageType;
            if (DamageType == GameDamageType.Internal_None && IsFireDamage)
                DamageType = GameDamageType.Fire;

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
                                   CombatKeyword == CombatKeywordEx.DamageBoostDoubleDirect ||
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
            bool CanHaveDelay = CombatKeyword != CombatKeywordEx.AddSprintSpeed;

            float EffectValue = CombatEffect.Data.RawValue.HasValue ? CombatEffect.Data.RawValue.Value : float.NaN;
            bool EffectIsPercent = CombatEffect.Data.RawIsPercent.HasValue ? CombatEffect.Data.RawIsPercent.Value : false;
            PgNumericValueEx pgNumericValueEx = new() { Value = EffectValue, IsPercent = EffectIsPercent };

            GetTargets(allEffects, i + 1, abilityList, out CombatTarget Target, out CombatTarget OtherTarget, out bool IsEveryOtherUse);
            if (Target == CombatTarget.Internal_None)
                GetTargets(allEffects, i - 1, abilityList, out Target, out OtherTarget, out IsEveryOtherUse);

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
                                 (CombatKeyword != CombatKeywordEx.AddMitigationPhysical) &&
                                 (CombatKeyword != CombatKeywordEx.AddMitigationElemental) &&
                                 (CombatKeyword != CombatKeywordEx.IncreaseMeleePowerCost);

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
                Target = CanHaveTarget && CanApplyModifier ? Target : CombatTarget.Internal_None,
                TargetRange = CanApplyModifier ? TargetRange : float.NaN,
                TargetAbilityList = TargetAbilityList,
                ConditionList = CanApplyModifier ? ConditionList : new(),
                ConditionAbilityList = CanApplyModifier ? ConditionAbilityList : new(),
                ConditionValue = CanApplyModifier ? ConditionValue : float.NaN,
                ConditionPercentage = CanApplyModifier ? ConditionPercentage : float.NaN,
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
        CombatTarget Target = CombatTarget.Internal_None;

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
            Keyword = CombatKeywordEx.GiveBuff,
            Data = PgNumericValueEx.Empty,
            RecurringDelay = RecurringDelay,
            Target = Target,
        };

        pgCombatModEx.DynamicEffects.Insert(0, pgCombatModEffectEx);
    }
}
