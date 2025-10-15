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
            if (ItemPower.Key == "2060")
            {
            }

            if (!KnownCombatPowers.KnownUnmatchedPowers.ContainsKey(ItemPower.Key))
            {
                AnalyzeRemainingPowers(ItemPower, out string[] stringKeyArray, out PgModEffectCollectionEx ModEffectArray);

                stringKeyTable.Add(stringKeyArray);
                analyzedPowerKeyToCompleteEffectTable.Add(ModEffectArray);
            }
        }
    }

    private void AnalyzeRemainingPowers(PgPower itemPower, out string[] stringKeyArray, out PgModEffectCollectionEx modEffectArray)
    {
        PgPowerTierCollection TierList = itemPower.TierList;
        string Skill_Key = itemPower.Skill_Key ?? string.Empty;

        Debug.Assert(TierList.Count > 0);

        int ValidationIndex = 0;
        if (TierList.Count >= 2)
            ValidationIndex = 1;
        if (TierList.Count >= 4)
            ValidationIndex = 2;

        List<CombatKeywordEx>[] PowerTierKeywordListArray = new List<CombatKeywordEx>[TierList.Count];
        stringKeyArray = new string[TierList.Count];
        modEffectArray = new PgModEffectCollectionEx(Skill_Key, TierList.Count);

        int LastTierIndex = TierList.Count - 1;
        PgPowerTier LastTier = TierList[LastTierIndex];

        List<PgEffect>? CandidateEffectList = candidateEffectTable.ContainsKey(itemPower) ? candidateEffectTable[itemPower] : null;
        AnalyzeRemainingPowers(LastTier, unmatchedEffectList, CandidateEffectList, out List<CombatKeywordEx> ExtractedPowerTierKeywordList, out PgModEffectEx ExtractedModEffect, true);
        PowerTierKeywordListArray[LastTierIndex] = ExtractedPowerTierKeywordList;
        modEffectArray.Items[LastTierIndex] = ExtractedModEffect;

        for (int i = 0; i + 1 < TierList.Count; i++)
        {
            AnalyzeRemainingPowers(TierList[i], null, CandidateEffectList, out List<CombatKeywordEx> ComparedPowerTierKeywordList, out PgModEffectEx ParsedModEffect, false);

            bool AllowIncomplete = i < ValidationIndex;

            if (!IsSameCombatKeywordList(ExtractedPowerTierKeywordList, ComparedPowerTierKeywordList, AllowIncomplete))
            {
                Debug.WriteLine($"Mismatching power at tier #{i}");
                return;
            }

            PowerTierKeywordListArray[i] = ComparedPowerTierKeywordList;
            modEffectArray.Items[i] = ParsedModEffect;
        }

        for (int i = 0; i < TierList.Count; i++)
        {
            string Key = PowerEffectPairKey(itemPower, i);
            stringKeyArray[i] = Key;
        }
    }

    private bool AnalyzeRemainingPowers(PgPowerTier powerTier, List<PgEffect>? unmatchedEffectList, List<PgEffect>? candidateEffectList, out List<CombatKeywordEx> extractedPowerTierKeywordList, out PgModEffectEx modEffect, bool displayAnalysisResult)
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
        modText = modText.Replace("All Sword abilities apply Flaming Sword damage for 10 seconds.", "");

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
}
