namespace Translator;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using PgObjects;

public class CombatParser
{
    #region Data Analysis
    private bool WriteFile = true;
    private bool CompareTable = false;

    public void AnalyzeCachedData(List<ItemSlot> validSlotList, List<object> objectList, Dictionary<string, PgModEffect> existingPowerKeyToCompleteEffectTable, Dictionary<string, PgModEffect> existingEffectKeyToCompleteEffectTable)
    {
        Dictionary<string, PgSkill> SkillTable = new();
        foreach (object Item in objectList)
            if (Item is PgSkill AsSkill)
                SkillTable.Add(AsSkill.Key, AsSkill);

        List<PgSkill> SkillList = new List<PgSkill>();

        foreach (object Item in objectList)
            switch (Item)
            {
                case PgAbility AsAbility:
                    AbilityObjectKeyList.Add(AsAbility.Key);
                    break;
                case PgEffect AsEffect:
                    EffectObjectKeyList.Add(AsEffect.Key);
                    break;
                case PgPower AsPower:
                    PowerObjectKeyList.Add(AsPower.Key);
                    break;
                case PgSkill AsSkill:
                    if (Generate.IsCombatSkill(AsSkill, SkillTable))
                        SkillList.Add(AsSkill);
                    break;
            }

        InitValidAbilityList(SkillTable);
        FilterValidPowers(validSlotList, SkillList, out _, out List<PgPower> PowerSimpleEffectList);
        FilterValidEffects(out Dictionary<string, Dictionary<string, List<PgEffect>>> AllEffectTable);
        FindAbilitiesWithMatchingEffect();
        FindPowersWithMatchingEffect(AllEffectTable, PowerSimpleEffectList, out Dictionary<PgPower, List<PgEffect>> PowerToEffectTable, out List<PgPower> UnmatchedPowerList, out List<PgEffect> UnmatchedEffectList, out Dictionary<PgPower, List<PgEffect>> CandidateEffectTable);
        GetAbilityNames(SkillList, out List<string> AbilityNameList, out Dictionary<string, List<AbilityKeyword>> NameToKeyword);

        List<string[]> StringKeyTable = new List<string[]>();
        List<PgModEffect[]> AnalyzedPowerKeyToCompleteEffectTable = new List<PgModEffect[]>();
        AnalyzeMatchingPowersAndEffects(AbilityNameList, NameToKeyword, PowerToEffectTable, StringKeyTable, AnalyzedPowerKeyToCompleteEffectTable);
        AnalyzeRemainingPowers(AbilityNameList, NameToKeyword, UnmatchedPowerList, UnmatchedEffectList, CandidateEffectTable, StringKeyTable, AnalyzedPowerKeyToCompleteEffectTable);
        AnalyzeRemainingEffects(AbilityNameList, NameToKeyword, UnmatchedEffectList, AnalyzedPowerKeyToCompleteEffectTable, out List<string> EffectKeyList);

        StringToEnumConversion<GameCombatSkill>.SetCustomParsedEnum(GameCombatSkill.SpiritFox);
        CheckAllSentencesUsed();

        if (WriteFile)
        {
            WritePowerKeyToCompleteEffectFile("PowerKeyToCompleteEffect.cs", StringKeyTable, AnalyzedPowerKeyToCompleteEffectTable, EffectKeyList);

            for (int i = 0; i < StringKeyTable.Count + EffectKeyList.Count; i++)
            {
                string[] StringKeyArray;

                if (i < StringKeyTable.Count)
                    StringKeyArray = StringKeyTable[i];
                else
                    StringKeyArray = new string[] { EffectKeyList[i - StringKeyTable.Count] };

                PgModEffect[] ModEffectArray = AnalyzedPowerKeyToCompleteEffectTable[i];

                Debug.Assert(StringKeyArray.Length == ModEffectArray.Length);

                for (int j = 0; j < StringKeyArray.Length; j++)
                {
                    string StringKey = StringKeyArray[j];
                    PgModEffect ModEffect = ModEffectArray[j];
                    ModEffect.Key = StringKey;

                    if (ModEffect.SecondaryModEffect != null)
                    {
                        ModEffect.SecondaryModEffect.Key = StringKey + "_secondary";
                        WriteModEffect(ModEffect.SecondaryModEffect);
                    }

                    WriteModEffect(ModEffect);
                }
            }
        }

        if (CompareTable)
            CompareWithPowerKeyToCompleteEffectTable(StringKeyTable, AnalyzedPowerKeyToCompleteEffectTable, EffectKeyList, existingPowerKeyToCompleteEffectTable, existingEffectKeyToCompleteEffectTable);
    }

    public static string FromSkillKey(string? key)
    {
        if (key == null || key == string.Empty)
            return string.Empty;
        else
            return key.Substring(1);
    }

    private void InitValidAbilityList(Dictionary<string, PgSkill> skillTable)
    {
        ValidAbilityList = new List<PgAbility>();

        foreach (string Key in AbilityObjectKeyList)
        {
            PgAbility Ability = (PgAbility)ParsingContext.ObjectKeyTable[typeof(PgAbility)][Key].Item;
            string Skill_Key = FromSkillKey(Ability.Skill_Key ?? throw new NullReferenceException());
            PgSkill AbilitySkill = (PgSkill)(Skill_Key.Length == 0 ? PgSkill.Unknown : (Skill_Key == "AnySkill" ? PgSkill.AnySkill : ParsingContext.ObjectKeyTable[typeof(PgSkill)][Skill_Key].Item));

            if (!Generate.IsCombatSkill(AbilitySkill, skillTable) && AbilitySkill.Key != "Crossbow")
                continue;

            ValidAbilityList.Add(Ability);
        }
    }

    private void FilterValidPowers(List<ItemSlot> validSlotList, List<PgSkill> skillList, out List<PgPower> powerAttributeList, out List<PgPower> powerSimpleEffectList)
    {
        powerAttributeList = new List<PgPower>();
        powerSimpleEffectList = new List<PgPower>();

        int TotalTierCount = 0;

        foreach (string PowerKey in PowerObjectKeyList)
        {
            PgPower Power = (PgPower)ParsingContext.ObjectKeyTable[typeof(PgPower)][PowerKey].Item;

            FilterValidPowers(validSlotList, skillList, powerAttributeList, powerSimpleEffectList, Power, ref TotalTierCount);
        }

        Debug.WriteLine($"{powerSimpleEffectList.Count + powerAttributeList.Count} powers, {powerAttributeList.Count} with attribute, {powerSimpleEffectList.Count} with description, Total: {TotalTierCount} mods");
    }

    private void FilterValidPowers(List<ItemSlot> validSlotList, List<PgSkill> skillList, List<PgPower> powerAttributeList, List<PgPower> powerSimpleEffectList, PgPower power, ref int totalTierCount)
    {
        PgPowerTierCollection TierList = power.TierList;

        Debug.Assert(TierList.Count > 0);

        int AttributeCount = 0;
        int SimpleCount = 0;

        foreach (PgPowerTier PowerTier in TierList)
            CheckAttributeOrSimple(PowerTier, ref AttributeCount, ref SimpleCount);

        Debug.Assert(AttributeCount == 0 || SimpleCount == 0);

        if (!IsSlotCompatible(validSlotList, power))
            return;

        string Skill_Key = FromSkillKey(power.Skill_Key);

        if (Skill_Key == "SpiritFox")
        {
        }

        Debug.Assert(Skill_Key != null &&
                     (Skill_Key.Length == 0 ||
                     IsSkillInList(skillList, Skill_Key) ||
                     Skill_Key == "Gourmand" ||
                     Skill_Key == PgSkill.AnySkill.Key ||
                     Skill_Key == "Endurance" ||
                     Skill_Key == "ArmorPatching" ||
                     Skill_Key == "ShamanicInfusion"));

        if (Skill_Key != null && (Skill_Key.Length == 0 || Skill_Key == "Gourmand"))
            return;
        if (power.IsUnavailable)
            return;

        if (AttributeCount > 0)
        {
            if (powerAttributeList.Contains(power))
                return;

            powerAttributeList.Add(power);
        }
        else
        {
            if (powerSimpleEffectList.Contains(power))
                return;

            powerSimpleEffectList.Add(power);
        }

        totalTierCount += TierList.Count;
    }

    private static bool IsSkillInList(List<PgSkill> skillList, string skillKey)
    {
        foreach (PgSkill Item in skillList)
            if (Item.Key == skillKey)
                return true;

        return false;
    }

    private void CheckAttributeOrSimple(PgPowerTier powerTier, ref int attributeCount, ref int simpleCount)
    {
        IList<PgPowerEffect> ItemEffectList = powerTier.EffectList;

        int EffectListCount = ItemEffectList.Count;
        for (int i = 0; i < EffectListCount; i++)
        {
            PgPowerEffect ItemEffect = ItemEffectList[i];

            Debug.Assert((ItemEffect is PgPowerEffectAttribute) || (ItemEffect is PgPowerEffectSimple));

            if (ItemEffect is PgPowerEffectAttribute)
                attributeCount++;

            if (ItemEffect is PgPowerEffectSimple)
                simpleCount++;
        }
    }

    private bool IsSlotCompatible(List<ItemSlot> validSlotList, PgPower power)
    {
        foreach (ItemSlot SlotItem in power.SlotList)
            if (validSlotList.Contains(SlotItem))
                return true;

        return false;
    }

    private void FilterValidEffects(out Dictionary<string, Dictionary<string, List<PgEffect>>> allEffectTable)
    {
        allEffectTable = new Dictionary<string, Dictionary<string, List<PgEffect>>>();

        foreach (string EffectKey in EffectObjectKeyList)
        {
            PgEffect Item = (PgEffect)ParsingContext.ObjectKeyTable[typeof(PgEffect)][EffectKey].Item;

            if (Item.Key.StartsWith("60006"))
            {
            }

            if (Item.Key.Length <= 5 || Item.Key[Item.Key.Length - 3] != '0')
                continue;

            Debug.Assert(Item.AbilityKeywordList.Count > 0);

            bool IsIntSuffix = int.TryParse(Item.Key.Substring(Item.Key.Length - 3), out int TierIndex);
            Debug.Assert(IsIntSuffix);
            Debug.Assert(TierIndex >= 1 && TierIndex < 30);

            string Subkey = Item.Key.Substring(0, Item.Key.Length - 3);
            string PowerKey = Subkey.Substring(Subkey.Length - 3);

            if (!allEffectTable.ContainsKey(PowerKey))
                allEffectTable.Add(PowerKey, new Dictionary<string, List<PgEffect>>());
            Dictionary<string, List<PgEffect>> SameKeyTable = allEffectTable[PowerKey];

            if (!SameKeyTable.ContainsKey(Subkey))
                SameKeyTable.Add(Subkey, new List<PgEffect>());
            SameKeyTable[Subkey].Add(Item);
        }
    }

    private void FindAbilitiesWithMatchingEffect()
    {
        List<PgEffect> EffectList = new List<PgEffect>();
        foreach (KeyValuePair<string, ParsingContext> Entry in ParsingContext.ObjectKeyTable[typeof(PgEffect)])
            if (Entry.Value.Item is PgEffect AsEffect && AsEffect.Name.Length > 0)
                EffectList.Add(AsEffect);

        foreach (PgAbility Ability in ValidAbilityList)
            FindAbilityWithMatchingEffect(Ability, EffectList);
    }

    private void FindAbilityWithMatchingEffect(PgAbility ability, List<PgEffect> effectList)
    {
        int IconId = ability.ObjectIconId;
        string AbilityName = ability.DigitStrippedName;

        int FirstMatchingIndex = -1;
        for (int i = 0; i < effectList.Count; i++)
        {
            PgEffect Effect = effectList[i];
            string EffectName = HackedEffectName(AbilityName, Effect.Name);

            if (EffectName == AbilityName && Effect.IconId == IconId)
            {
                FirstMatchingIndex = i;
                break;
            }
        }

        if (FirstMatchingIndex >= 0)
        {
            int LastMatchingIndex = FirstMatchingIndex;

            for (int i = FirstMatchingIndex + 1; i < effectList.Count; i++)
            {
                PgEffect Effect = effectList[i];
                string EffectName = HackedEffectName(AbilityName, Effect.Name);

                if (EffectName == effectList[FirstMatchingIndex].Name && Effect.IconId == effectList[FirstMatchingIndex].IconId)
                    LastMatchingIndex++;
                else
                    break;
            }

            int SameNameCount = 0;
            int ThisAbilityIndex = 0;
            foreach (PgAbility Item in ValidAbilityList)
                if (Item.DigitStrippedName == ability.DigitStrippedName)
                {
                    if (Item == ability)
                        ThisAbilityIndex = SameNameCount;

                    SameNameCount++;
                }

            PgEffect MatchingEffect;

            if (LastMatchingIndex <= FirstMatchingIndex + SameNameCount)
                MatchingEffect = effectList[FirstMatchingIndex];
            else
                MatchingEffect = effectList[FirstMatchingIndex + ThisAbilityIndex];

            AddAssociatedEffect(ability, CombatKeyword.Internal_None, MatchingEffect);
        }

        for (int i = 0; i < effectList.Count; i++)
        {
            PgEffect Effect = effectList[i];
            string EffectName = HackedEffectName(AbilityName, Effect.Name);

            if (EffectName.Length + 1 > AbilityName.Length && EffectName.StartsWith(AbilityName + " "))
            {
                string Suffix = EffectName.Substring(AbilityName.Length).Trim();

                switch (Suffix)
                {
                    case "Speed":
                    case "Sprint":
                    case "Movement":
                        AddAssociatedEffect(ability, CombatKeyword.AddSprintSpeed, Effect);
                        break;

                    case "Damage":
                    case "Up":
                    case "Buff":
                        AddAssociatedEffect(ability, CombatKeyword.DamageBoost, Effect);
                        break;

                    case "Heal":
                    case "Health":
                    case "Boost":
                        AddAssociatedEffect(ability, CombatKeyword.RestoreHealth, Effect);
                        break;

                    case "Armor":
                        AddAssociatedEffect(ability, CombatKeyword.RestoreArmor, Effect);
                        break;

                    case "Power":
                        AddAssociatedEffect(ability, CombatKeyword.RestorePower, Effect);
                        break;

                    case "Regeneration":
                    case "Medicine":
                        AddAssociatedEffect(ability, CombatKeyword.EffectDuration, Effect);
                        break;

                    case "Aggro":
                    case "Bonus Aggro":
                    case "NOW":
                        AddAssociatedEffect(ability, CombatKeyword.AddTaunt, Effect);
                        break;

                    case "Fire":
                        AddAssociatedEffect(ability, CombatKeyword.ChangeDamageType, Effect);
                        break;

                    case "Accuracy":
                        AddAssociatedEffect(ability, CombatKeyword.AddAccuracy, Effect);
                        break;

                    case "Mitigation":
                        AddAssociatedEffect(ability, CombatKeyword.AddMitigation, Effect);
                        break;

                    case "Knockback":
                        AddAssociatedEffect(ability, CombatKeyword.ReflectKnockbackOnFirstMelee, Effect);
                        break;

                    case "Tagged":
                        break;

                    case "Invincibility":
                        AddAssociatedEffect(ability, CombatKeyword.ImmunityDirect, Effect);
                        StringToEnumConversion<CombatKeyword>.SetCustomParsedEnum(CombatKeyword.ImmunityDirect);
                        break;

                    case "Resistance":
                        AddAssociatedEffect(ability, CombatKeyword.AddDamageResistance, Effect);
                        break;

                    default:
                        Debug.WriteLine($"Additional Effect '{Effect.Name}' for ability '{ability.Name}'");
                        break;
                }
            }
        }
    }

    private static string HackedEffectName(string abilityName, string effectName)
    {
        if (abilityName == "Reconstruct" && effectName.StartsWith("Reconstruction"))
            return effectName.Replace("Reconstruct", "Reconstruction");
        else if (effectName == "Suppress PvP")
            return "** Suppress PvP";
        else if (effectName == "Delerium Tremens")
            return "** Delerium Tremens";
        else
            return effectName;
    }

    private void AddAssociatedEffect(PgAbility ability, CombatKeyword keyword, PgEffect effect)
    {
        if (!ability.AssociatedEffectKeyTable.ContainsKey(keyword))
            ability.AssociatedEffectKeyTable.Add(keyword, new List<int>());

        List<int> KeyList = ability.AssociatedEffectKeyTable[keyword];
        int KeyInt = int.Parse(effect.Key);

        if (!KeyList.Contains(KeyInt))
            KeyList.Add(KeyInt);
    }

    private void FindPowersWithMatchingEffect(Dictionary<string, Dictionary<string, List<PgEffect>>> allEffectTable, List<PgPower> powerSimpleEffectList, out Dictionary<PgPower, List<PgEffect>> powerToEffectTable, out List<PgPower> unmatchedPowerList, out List<PgEffect> unmatchedEffectList, out Dictionary<PgPower, List<PgEffect>> candidateEffectTable)
    {
        powerToEffectTable = new Dictionary<PgPower, List<PgEffect>>();
        unmatchedPowerList = new List<PgPower>();
        unmatchedEffectList = new List<PgEffect>();
        candidateEffectTable = new Dictionary<PgPower, List<PgEffect>>();

        foreach (string EffectKey in EffectObjectKeyList)
        {
            PgEffect Effect = (PgEffect)ParsingContext.ObjectKeyTable[typeof(PgEffect)][EffectKey].Item;
            unmatchedEffectList.Add(Effect);
        }

        foreach (PgPower Item in powerSimpleEffectList)
            if (FindPowersWithMatchingEffect(allEffectTable, Item, out List<PgEffect>? MatchingEffectList, out List<PgEffect>? CandidateSingleEffectList))
            {
                foreach (PgEffect Effect in MatchingEffectList!)
                    unmatchedEffectList.Remove(Effect);

                powerToEffectTable.Add(Item, MatchingEffectList);
            }
            else
            {
                unmatchedPowerList.Add(Item);
                if (CandidateSingleEffectList != null && CandidateSingleEffectList.Count > 0)
                    candidateEffectTable.Add(Item, CandidateSingleEffectList);
            }
    }

    private bool FindPowersWithMatchingEffect(Dictionary<string, Dictionary<string, List<PgEffect>>> allEffectTable, PgPower power, out List<PgEffect>? matchingEffectList, out List<PgEffect>? candidateSingleEffectList)
    {
        if (FindPowersWithMatchingEffectAllTiers(allEffectTable, power, out matchingEffectList))
        {
            candidateSingleEffectList = null;
            return true;
        }

        candidateSingleEffectList = FindMatchingEffectOneTier(power);
        return false;
    }

    private bool FindPowersWithMatchingEffectAllTiers(Dictionary<string, Dictionary<string, List<PgEffect>>> allEffectTable, PgPower power, out List<PgEffect>? matchingEffectList)
    {
        matchingEffectList = null;

        string Key = power.Key;
        Debug.Assert(Key.Length >= 3);

        if (Key == "24154")
        {
        }

        string PowerKey = Key.Substring(Key.Length - 3);
        if (!allEffectTable.ContainsKey(PowerKey))
            return false;

        Dictionary<string, List<PgEffect>> SameKeyTable = allEffectTable[PowerKey];
        PgPowerTierCollection TierList = power.TierList;
        List<string> MatchingKeyList = new List<string>();
        List<string> OneToOneMatchingKeyList = new List<string>();

        foreach (KeyValuePair<string, List<PgEffect>> Entry in SameKeyTable)
        {
            List<PgEffect> EffectList = Entry.Value;
            if (TierList.Count != EffectList.Count)
                continue;

            if (HasCommonIcon(power, EffectList, out bool IsOneToOne))
            {
                MatchingKeyList.Add(Entry.Key);

                if (IsOneToOne)
                    OneToOneMatchingKeyList.Add(Entry.Key);
            }
        }

        if (MatchingKeyList.Count == 0)
            return false;

        if (OneToOneMatchingKeyList.Count > 1)
        {
            PgPowerTier LastTier = power.TierList[power.TierList.Count - 1];
            PgPowerEffect FirstEffect = LastTier.EffectList[0];

            if (FirstEffect is PgPowerEffectAttribute AsAttribute && AsAttribute.Attribute_Key != null)
                Debug.WriteLine($"Possible mod bug: key={AsAttribute.Attribute_Key}");
            else if (FirstEffect is PgPowerEffectSimple AsSimple)
                Debug.WriteLine($"Possible mod bug: {AsSimple.Description}");
            return false;
        }

        int MaxEffectSameTier = 0;
        foreach (PgPowerTier PowerTier in TierList)
        {
            IList<PgPowerEffect> EffectList = PowerTier.EffectList;
            if (MaxEffectSameTier < EffectList.Count)
                MaxEffectSameTier = EffectList.Count;
        }

        if (MaxEffectSameTier > 1)
            return false;

        if (OneToOneMatchingKeyList.Count == 0)
        {
            if (MatchingKeyList.Count > 1)
            {
                int MatchingKeyIndex;

                for (MatchingKeyIndex = 0; MatchingKeyIndex < MatchingKeyList.Count; MatchingKeyIndex++)
                {
                    PgPowerTier LastTier = power.TierList[power.TierList.Count - 1];
                    PgPowerEffect FirstEffect = LastTier.EffectList[0];
                    string FirstEffectString = FirstEffect.Description;

                    List<PgEffect> CandidateEffectList = SameKeyTable[MatchingKeyList[MatchingKeyIndex]];
                    PgEffect LastEffect = CandidateEffectList[CandidateEffectList.Count - 1];
                    string LastEffectString = LastEffect.Description;

                    if (FirstEffectString.Contains(LastEffectString))
                    {
                        matchingEffectList = CandidateEffectList;
                        break;
                    }
                }

                if (MatchingKeyIndex >= MatchingKeyList.Count)
                {
                    Debug.WriteLine($"Possible mod bug ({OneToOneMatchingKeyList.Count}): {power}");
                    matchingEffectList = SameKeyTable[MatchingKeyList[MatchingKeyList.Count - 1]];
                }
            }
            else
                matchingEffectList = SameKeyTable[MatchingKeyList[0]];
        }
        else
            matchingEffectList = SameKeyTable[OneToOneMatchingKeyList[0]];

        return true;
    }

    private List<PgEffect> FindMatchingEffectOneTier(PgPower power)
    {
        if (power.Key == "23205")
        {
        }

        List<string> MatchingKeyList = new List<string>();

        foreach (string EffectKey in EffectObjectKeyList)
        {
            PgEffect Item = (PgEffect)ParsingContext.ObjectKeyTable[typeof(PgEffect)][EffectKey].Item;

            string Key = Item.Key;

            if (Key.Length > 6)
                continue;

            if (Item.AbilityKeywordList.Count != 1)
                continue;

            if (Item.DisplayMode != EffectDisplayMode.AbilityModifier)
                continue;

            if (!Item.KeywordList.Contains(EffectKeyword.Innate))
                continue;

            if (Item.Description.StartsWith("DamageType:"))
                continue;

            AbilityKeyword EffectAbilityKeyword = Item.AbilityKeywordList[0];
            if (GenericAbilityList.Contains(EffectAbilityKeyword) || BuffOrPetAbilityList.Contains(EffectAbilityKeyword))
                continue;

            List<PgEffect> TierList = new List<PgEffect>() { Item };
            if (HasCommonIcon(power, TierList, out bool IsOneToOne))
                MatchingKeyList.Add(Key);
        }

        List<PgEffect> Result = new List<PgEffect>();
        foreach (string Key in MatchingKeyList)
        {
            PgEffect CandidateEffect = (PgEffect)ParsingContext.ObjectKeyTable[typeof(PgEffect)][Key].Item;
            Result.Add(CandidateEffect);
        }

        return Result;
    }

    private bool HasCommonIcon(PgPower power, List<PgEffect> effectList, out bool isOneToOne)
    {
        List<int> PowerIconList = new List<int>();
        List<int> EffectIconList = new List<int>();

        foreach (PgPowerTier Item in power.TierList)
        {
            foreach (PgPowerEffect PowerEffectItem in Item.EffectList)
            {
                Debug.Assert(PowerEffectItem is PgPowerEffectSimple);
                PgPowerEffectSimple SimpleEffect = (PgPowerEffectSimple)PowerEffectItem;

                foreach (int Id in SimpleEffect.IconIdList)
                {
                    int IconId = Id;
                    if (power.Key == "23205" && Id == 3402)
                        IconId = 3042;

                    if (!PowerIconList.Contains(IconId))
                        PowerIconList.Add(IconId);
                }
            }
        }

        List<AbilityKeyword> AbilityKeywordList = new List<AbilityKeyword>();
        foreach (PgEffect EffectItem in effectList)
            foreach (AbilityKeyword Keyword in EffectItem.AbilityKeywordList)
                if (!AbilityKeywordList.Contains(Keyword))
                    AbilityKeywordList.Add(Keyword);

        if (AbilityKeywordList.Contains(AbilityKeyword.NiceAttack) ||
            AbilityKeywordList.Contains(AbilityKeyword.CoreAttack) ||
            AbilityKeywordList.Contains(AbilityKeyword.BasicAttack) ||
            AbilityKeywordList.Contains(AbilityKeyword.MajorHeal) ||
            AbilityKeywordList.Contains(AbilityKeyword.MinorHealTargeted) ||
            AbilityKeywordList.Contains(AbilityKeyword.SignatureDebuff) ||
            AbilityKeywordList.Contains(AbilityKeyword.SignatureSupport))
            EffectIconList.Add(108);

        string Skill_Key = FromSkillKey(power.Skill_Key);

        if (AbilityKeywordList.Contains(AbilityKeyword.Sword) && Skill_Key == "Sword")
            EffectIconList.Add(108);

        if (AbilityKeywordList.Contains(AbilityKeyword.FireSpell) && Skill_Key == "FireMagic")
            EffectIconList.Add(108);

        if (AbilityKeywordList.Contains(AbilityKeyword.Unarmed) && Skill_Key == "Unarmed")
            EffectIconList.Add(108);

        if (AbilityKeywordList.Contains(AbilityKeyword.Knife) && Skill_Key == "Knife")
            EffectIconList.Add(108);

        if (AbilityKeywordList.Contains(AbilityKeyword.Melee))
        {
            EffectIconList.Add(108);

            if (PowerIconList.Count == 0)
                PowerIconList.Add(108);
        }

        foreach (PgAbility AbilityItem in ValidAbilityList)
        {
            bool KeywordMatch = false;

            foreach (AbilityKeyword Keyword in AbilityItem.KeywordList)
                if (AbilityKeywordList.Contains(Keyword))
                {
                    KeywordMatch = true;
                    break;
                }

            if (KeywordMatch)
            {
                if (!EffectIconList.Contains(AbilityItem.IconId))
                    EffectIconList.Add(AbilityItem.IconId);
            }
        }

        isOneToOne = PowerIconList.Count == 1 && EffectIconList.Count == 1;

        foreach (int IconId in PowerIconList)
            if (EffectIconList.Contains(IconId))
                return true;

        return false;
    }

    public static IList<PgSpecialValue> GetSpecialValueList(PgAbility ability)
    {
        IList<PgSpecialValue> Result = new List<PgSpecialValue>(ability.PvE.SpecialValueList);

        bool IsChangedToArmor = false;
        foreach (PgSpecialValue Item in Result)
            if (Item.Suffix == "of the Health damage done (up to the max)")
            {
                if (IsChangedToArmor)
                    Item.Suffix = "of the Armor damage done (up to the max)";
                else
                    IsChangedToArmor = true;
            }

        return Result;
    }

    private List<AbilityKeyword> KeywordIgnoreList = new List<AbilityKeyword>()
    {
        AbilityKeyword.Lint_NotLearnable,
        AbilityKeyword.Lint_HarmlessWithDamageBoosts,
        AbilityKeyword.Attack,
        AbilityKeyword.BasicAttack,
        AbilityKeyword.NiceAttack,
        AbilityKeyword.CoreAttack,
        AbilityKeyword.EpicAttack,
        AbilityKeyword.CombatRefresh,
        AbilityKeyword.SignatureDebuff,
        AbilityKeyword.SignatureSupport,
        AbilityKeyword.MajorHeal,
        AbilityKeyword.MinorHeal,
        AbilityKeyword.MinorHealTargeted,
        AbilityKeyword.Burst,
        AbilityKeyword.SurvivalUtility,
        AbilityKeyword.FistAttack,
        AbilityKeyword.FireMagicAttack,
        AbilityKeyword.Melee,
        AbilityKeyword.Ranged,
        AbilityKeyword.Kick,
        AbilityKeyword.BodyPartAttack,
        AbilityKeyword.BodypartAttack,
        AbilityKeyword.BarrageOnly,
        AbilityKeyword.SerpentStrike,
        AbilityKeyword.HipSlam,
        AbilityKeyword.FireSpell,
        AbilityKeyword.FireBurst,
        AbilityKeyword.SelfImmolation,
        AbilityKeyword.PsychologyAttack,
        AbilityKeyword.PsychologyHeal,
        AbilityKeyword.PhrenologyCriticals,
        AbilityKeyword.AnatomyCriticals,
        AbilityKeyword.WerewolfAttack,
        AbilityKeyword.DeerAttack,
        AbilityKeyword.CowAttack,
        AbilityKeyword.StaffAttack,
        AbilityKeyword.Mutation,
        AbilityKeyword.BattleChemistryAttack,
        AbilityKeyword.SummonSkeletonArcherOrMage,
        AbilityKeyword.SummonSkeletonArcherOrSwordsman,
        AbilityKeyword.SummonSkeletonSwordsmanOrMage,
        AbilityKeyword.SpiderAttack,
        AbilityKeyword.HammerNonBasic,
        AbilityKeyword.DruidHeal,
        AbilityKeyword.IceMagicSingleTarget,
        AbilityKeyword.SummonedColdSphere,
        AbilityKeyword.KnifeCut,
        AbilityKeyword.KnifeNonCut,
        AbilityKeyword.BardBlast,
        AbilityKeyword.SummonedFireWall,
        AbilityKeyword.PigAttack,
        AbilityKeyword.ChemistryBomb,
        AbilityKeyword.Bomb,
        AbilityKeyword.SummonSkeleton,
        AbilityKeyword.PriestAttack,
        AbilityKeyword.HeavyArchery,
        AbilityKeyword.Unarmed,
        AbilityKeyword.FireMagic,
        AbilityKeyword.Psychology,
        AbilityKeyword.Werewolf,
        AbilityKeyword.Deer,
        AbilityKeyword.Cow,
        AbilityKeyword.BattleChemistry,
        AbilityKeyword.Pig,
        AbilityKeyword.Staff,
        AbilityKeyword.Necromancy,
        AbilityKeyword.Spider,
        AbilityKeyword.Shield,
        AbilityKeyword.Hammer,
        AbilityKeyword.Druid,
        AbilityKeyword.IceMagic,
        AbilityKeyword.BardSong,
        AbilityKeyword.Bard,
        AbilityKeyword.Knife,
        AbilityKeyword.Rabbit,
        AbilityKeyword.Priest,
        AbilityKeyword.Archery,
    };

    private Dictionary<string, string> KnownBaseAbilityNameTable = new Dictionary<string, string>()
    {
        { "Boiling Veins", "Molten Veins" },
        { "Super Warmthball", "Super Fireball" },
        { "Heat Breath", "Fire Breath" },
        { "Chillball", "Frostball" },
        { "Warmthball", "Fireball" },
        { "Flare Fireball", "Fireball" },
        { "Call Living Stabled Pet", "Call Stabled Pet" },
        { "Raise Skeletal Ratkin Mage", "Raise Skeletal Battle Mage" },
        { "Slicing Ice", "Slice" },
        { "Pouncing Rend", "Pouncing Rake" },
        { "Pinning Slash", "Pin" },
        { "Free-Summon Skeletal Archer", "Raise Skeletal Archer" },
        { "Rotflesh", "Rotskin" },
    };

    private Dictionary<string, List<AbilityKeyword>> WideAbilityTable = new Dictionary<string, List<AbilityKeyword>>()
    {
        { "Nice Attack", new List<AbilityKeyword>() { AbilityKeyword.NiceAttack } },
        { "Core Attack", new List<AbilityKeyword>() { AbilityKeyword.CoreAttack } },
        { "Epic Attack", new List<AbilityKeyword>() { AbilityKeyword.EpicAttack } },
        { "Basic Attack", new List<AbilityKeyword>() { AbilityKeyword.BasicAttack } },
        { "Nice and Epic Attack", new List<AbilityKeyword>() { AbilityKeyword.NiceAttack, AbilityKeyword.EpicAttack } },
        { "Nice Attack and Epic Attack", new List<AbilityKeyword>() { AbilityKeyword.NiceAttack, AbilityKeyword.EpicAttack } },
        { "Core and Nice Attack", new List<AbilityKeyword>() { AbilityKeyword.CoreAttack, AbilityKeyword.NiceAttack } },
        { "Basic, Core, and Nice Attack", new List<AbilityKeyword>() { AbilityKeyword.BasicAttack, AbilityKeyword.CoreAttack, AbilityKeyword.NiceAttack } },
        { "Signature Support", new List<AbilityKeyword>() { AbilityKeyword.SignatureSupport } },
        { "Signature Debuff", new List<AbilityKeyword>() { AbilityKeyword.SignatureDebuff } },
        { "Major Healing", new List<AbilityKeyword>() { AbilityKeyword.MajorHeal } },
        { "Minor Healing", new List<AbilityKeyword>() { AbilityKeyword.MinorHeal } },
        { "Minor Heal", new List<AbilityKeyword>() { AbilityKeyword.MinorHeal } },
        { "First Aid", new List<AbilityKeyword>() { AbilityKeyword.FirstAid } },
        { "Crossbow", new List<AbilityKeyword>() { AbilityKeyword.Crossbow } },
        { "Ranged Attack", new List<AbilityKeyword>() { AbilityKeyword.Ranged } },
        { "All Sword", new List<AbilityKeyword>() { AbilityKeyword.Sword } },
        { "All Fire spell", new List<AbilityKeyword>() { AbilityKeyword.FireSpell } },
        { "All Fire Magic attack", new List<AbilityKeyword>() { AbilityKeyword.FireMagicAttack } },
        { "Unarmed attack", new List<AbilityKeyword>() { AbilityKeyword.Unarmed } },
        { "Kick attack", new List<AbilityKeyword>() { AbilityKeyword.Kick } },
        { "Any Kick ability", new List<AbilityKeyword>() { AbilityKeyword.Kick } },
        { "All kicks", new List<AbilityKeyword>() { AbilityKeyword.Kick } },
        { "Bomb attack", new List<AbilityKeyword>() { AbilityKeyword.Bomb } },
        { "Psi Health Wave, Armor Wave, and Power Wave", new List<AbilityKeyword>() { AbilityKeyword.PsiHealthWave, AbilityKeyword.PsiArmorWave, AbilityKeyword.PsiPowerWave } },
        { "All Psi Wave Ability", new List<AbilityKeyword>() { AbilityKeyword.PsiWave } },
        { "All types of shield Bash", new List<AbilityKeyword>() { AbilityKeyword.Bash } },
        { "All Shield Bash ability", new List<AbilityKeyword>() { AbilityKeyword.Bash } },
        { "All Shield ability", new List<AbilityKeyword>() { AbilityKeyword.Shield } },
        { "Hammer attack", new List<AbilityKeyword>() { AbilityKeyword.HammerAttack } },
        { "All Druid ability", new List<AbilityKeyword>() { AbilityKeyword.Druid } },
        { "All Staff attack", new List<AbilityKeyword>() { AbilityKeyword.StaffAttack } },
        { "All Ice Magic ability that hit multiple", new List<AbilityKeyword>() { AbilityKeyword.IceMagicAoE } },
        { "All Ice Magic attack that hit a single", new List<AbilityKeyword>() { AbilityKeyword.IceMagicSingleTarget } },
        { "All Ice Magic ability", new List<AbilityKeyword>() { AbilityKeyword.IceMagic } },
        { "Knife ability with 'Cut'", new List<AbilityKeyword>() { AbilityKeyword.KnifeCut } },
        { "All Knife ability WITHOUT 'Cut'", new List<AbilityKeyword>() { AbilityKeyword.KnifeNonCut } },
        { "All Knife Fighting attack", new List<AbilityKeyword>() { AbilityKeyword.Knife } },
        { "Bard Songs", new List<AbilityKeyword>() { AbilityKeyword.BardSong } },
        { "All Major Healing ability targeting you", new List<AbilityKeyword>() { AbilityKeyword.MajorHeal } },
        { "All Bun-Fu moves", new List<AbilityKeyword>() { AbilityKeyword.Rabbit } },
        { "Survival Utility", new List<AbilityKeyword>() { AbilityKeyword.SurvivalUtility } },
        { "Survival Utility and Major Heal", new List<AbilityKeyword>() { AbilityKeyword.SurvivalUtility, AbilityKeyword.MajorHeal } },
        { "Knee Spikes Mutation", new List<AbilityKeyword>() { AbilityKeyword.Mutation_KneeSpikes } },
        { "Extra Skin mutation", new List<AbilityKeyword>() { AbilityKeyword.Mutation_ExtraSkin } },
        { "Extra Heart mutation", new List<AbilityKeyword>() { AbilityKeyword.Mutation_ExtraHeart } },
        { "Extra Heart and Stretchy Spine mutation", new List<AbilityKeyword>() { AbilityKeyword.Mutation_ExtraHeart, AbilityKeyword.Mutation_StretchySpine } },
        { "Stretchy Spine mutation", new List<AbilityKeyword>() { AbilityKeyword.Mutation_StretchySpine } },
        { "Animal Handling pets", new List<AbilityKeyword>() { AbilityKeyword.StabledPet } },
        { "Allies' Combat Refreshes", new List<AbilityKeyword>() { AbilityKeyword.CombatRefresh } },
        { "Raised Zombies", new List<AbilityKeyword>() { AbilityKeyword.SummonZombie } },
        { "Major Heal", new List<AbilityKeyword>() { AbilityKeyword.MajorHeal } },
        { "Summoned Deer", new List<AbilityKeyword>() { AbilityKeyword.SummonDeer } },
        { "Incubated Spiders", new List<AbilityKeyword>() { AbilityKeyword.SummonedSpider } },
        { "Minor Healing (Targeted)", new List<AbilityKeyword>() { AbilityKeyword.MinorHealTargeted } },
        { "All Mentalism and Psychology attack", new List<AbilityKeyword>() { AbilityKeyword.MentalismAttack, AbilityKeyword.PsychologyAttack } },
        { "All non-basic attack", new List<AbilityKeyword>() { Internal_NonBasic } },
    };

    private List<AbilityKeyword> GenericAbilityList = new List<AbilityKeyword>()
    {
        AbilityKeyword.NiceAttack,
        AbilityKeyword.CoreAttack,
        AbilityKeyword.EpicAttack,
        AbilityKeyword.BasicAttack,
        AbilityKeyword.SignatureSupport,
        AbilityKeyword.SignatureDebuff,
        AbilityKeyword.MajorHeal,
        AbilityKeyword.MinorHeal,
        AbilityKeyword.FirstAid,
        AbilityKeyword.Crossbow,
        AbilityKeyword.Ranged,
        AbilityKeyword.Sword,
        AbilityKeyword.FireSpell,
        AbilityKeyword.FireMagicAttack,
        AbilityKeyword.Unarmed,
        AbilityKeyword.Kick,
        AbilityKeyword.Bomb,
        AbilityKeyword.PsiWave,
        AbilityKeyword.PsiHealthWave,
        AbilityKeyword.PsiArmorWave,
        AbilityKeyword.PsiPowerWave,
        AbilityKeyword.Bash,
        AbilityKeyword.HammerAttack,
        AbilityKeyword.Druid,
        AbilityKeyword.StaffAttack,
        AbilityKeyword.IceMagicAoE,
        AbilityKeyword.IceMagicSingleTarget,
        AbilityKeyword.IceMagic,
        AbilityKeyword.KnifeCut,
        AbilityKeyword.KnifeNonCut,
        AbilityKeyword.Knife,
        AbilityKeyword.BardSong,
        AbilityKeyword.SurvivalUtility,
        AbilityKeyword.MajorHeal,
        AbilityKeyword.Rabbit,
        AbilityKeyword.Mutation_KneeSpikes,
        AbilityKeyword.Mutation_ExtraSkin,
        AbilityKeyword.Mutation_ExtraHeart,
        AbilityKeyword.Mutation_StretchySpine,
        AbilityKeyword.StabledPet,
        AbilityKeyword.CombatRefresh,
        AbilityKeyword.SummonZombie,
        AbilityKeyword.Shield,
        AbilityKeyword.SummonDeer,
        AbilityKeyword.SummonedSpider,
        AbilityKeyword.MinorHealTargeted,
        AbilityKeyword.MentalismAttack,
        AbilityKeyword.PsychologyAttack,
        Internal_NonBasic,
    };

    private List<AbilityKeyword> BuffOrPetAbilityList = new List<AbilityKeyword>()
    {
        AbilityKeyword.SummonDeer,
        AbilityKeyword.SummonedColdSphere,
        AbilityKeyword.SummonedFireWall,
        AbilityKeyword.SummonSkeleton,
        AbilityKeyword.SummonSkeletonArcher,
        AbilityKeyword.SummonSkeletonArcherOrMage,
        AbilityKeyword.SummonSkeletonArcherOrSwordsman,
        AbilityKeyword.SummonSkeletonMage,
        AbilityKeyword.SummonSkeletonSwordsman,
        AbilityKeyword.SummonSkeletonSwordsmanOrMage,
        AbilityKeyword.SummonZombie,
        AbilityKeyword.WebTrap,
        AbilityKeyword.ConfusingDouble,
        AbilityKeyword.StunTrap,
        AbilityKeyword.SicEm,
        AbilityKeyword.CleverTrick,
        AbilityKeyword.FillWithBile,
        AbilityKeyword.IceArmor,
    };

    private void GetAbilityNames(List<PgSkill> skillList, out List<string> abilityNameList, out Dictionary<string, List<AbilityKeyword>> nameToKeyword)
    {
        Dictionary<AbilityKeyword, string> KeywordToName = new Dictionary<AbilityKeyword, string>();

        abilityNameList = new List<string>();

        foreach (PgAbility Item in ValidAbilityList)
        {
            string Skill_Key = FromSkillKey(Item.Skill_Key ?? throw new NullReferenceException());
            PgSkill AbilitySkill = (PgSkill)(Skill_Key.Length == 0 ? PgSkill.Unknown : (Skill_Key == "AnySkill" ? PgSkill.AnySkill : ParsingContext.ObjectKeyTable[typeof(PgSkill)][Skill_Key].Item));

            if (!IsSkillInList(skillList, AbilitySkill.Key))
                continue;

            string Name = AbilityBaseName(Item);

            if (Name.Length > 0 && Name[0] == '(')
                continue;
            if (Name.EndsWith(" (Energy Bow)"))
                continue;
            if (Name.EndsWith(" 3B"))
                continue;
            if (Name.EndsWith(" 3+"))
                continue;
            if (Name.EndsWith(" #"))
                continue;
            if (Name.EndsWith(" (Purple)"))
                continue;
            if (Name == "Cold Protection")
                continue;

            if (KnownBaseAbilityNameTable.ContainsKey(Name))
                Name = KnownBaseAbilityNameTable[Name];

            if (!abilityNameList.Contains(Name))
            {
                List<AbilityKeyword> KeywordList = new List<AbilityKeyword>(Item.KeywordList);

                if (KeywordList.Count > 0)
                {
                    foreach (AbilityKeyword Keyword in KeywordIgnoreList)
                        if (KeywordList.Contains(Keyword))
                            KeywordList.Remove(Keyword);

                    if (KeywordList.Count == 0)
                        Debug.WriteLine($"{Name} has no keyword.");
                    else
                    {
                        if (KeywordList.Count > 1)
                        {
                            AbilityKeyword MatchingKeyword = AbilityKeyword.Internal_None;
                            foreach (AbilityKeyword Keyword in KeywordList)
                                if (TextMaps.AbilityKeywordTextMap[Keyword] == Name)
                                {
                                    MatchingKeyword = Keyword;
                                    break;
                                }

                            if (MatchingKeyword != AbilityKeyword.Internal_None)
                                KeywordList = new List<AbilityKeyword>() { MatchingKeyword };
                        }

                        AbilityKeyword FinalMatchingKeyword = KeywordList[0];

                        if (KeywordList.Count > 1)
                        {
                            string KeywordListString = string.Empty;

                            foreach (AbilityKeyword Keyword in KeywordList)
                            {
                                if (KeywordListString.Length > 0)
                                    KeywordListString += ", ";

                                KeywordListString += Keyword.ToString();
                            }

                            if (!Name.EndsWith(" (Orc)"))
                            {
                                if (FinalMatchingKeyword == AbilityKeyword.WeatherWitching && KeywordList.Contains(AbilityKeyword.SummonedTornado))
                                    FinalMatchingKeyword = AbilityKeyword.SummonedTornado;

                                Debug.WriteLine($"{Name} has more than one keyword: {KeywordListString}. Selected: {FinalMatchingKeyword}");
                            }
                        }

                        if (KeywordToName.ContainsKey(FinalMatchingKeyword))
                        {
                            if (!Name.EndsWith(" (Orc)"))
                                Debug.WriteLine($"Keyword {FinalMatchingKeyword} for {Name} is already used by {KeywordToName[FinalMatchingKeyword]}.");
                        }
                        else
                            KeywordToName.Add(FinalMatchingKeyword, Name);
                    }
                }

                abilityNameList.Add(Name);
            }
        }

        AddAbilityToNameList(abilityNameList, "Werewolf Claw");
        AddAbilityToNameList(abilityNameList, "Armor Wave");
        AddAbilityToNameList(abilityNameList, "Health Wave");
        AddAbilityToNameList(abilityNameList, "Power Wave");
        AddAbilityToNameList(abilityNameList, "Fire Walls'");
        AddAbilityToNameList(abilityNameList, "Summoned Skeletal Swordsmen");
        AddAbilityToNameListAndTable(abilityNameList, KeywordToName, AbilityKeyword.GolemTauntingPunch, "Taunting Punch");
        AddAbilityToNameListAndTable(abilityNameList, KeywordToName, AbilityKeyword.PoisonBombToss, "Poison Bomb");
        AddAbilityToNameListAndTable(abilityNameList, KeywordToName, AbilityKeyword.GolemSelfDestruct, "Self Destruct");
        AddAbilityToNameListAndTable(abilityNameList, KeywordToName, AbilityKeyword.GolemRageAcidToss, "Rage Acid Toss");
        AddAbilityToNameListAndTable(abilityNameList, KeywordToName, AbilityKeyword.GolemDoomAdmixture, "Doom Admixture");
        AddAbilityToNameListAndTable(abilityNameList, KeywordToName, AbilityKeyword.GolemRageMist, "Rage Mist");
        AddAbilityToNameListAndTable(abilityNameList, KeywordToName, AbilityKeyword.GolemInvigoratingMist, "Invigorating Mist");
        AddAbilityToNameListAndTable(abilityNameList, KeywordToName, AbilityKeyword.GolemSelfSacrifice, "Self Sacrifice");
        AddAbilityToNameListAndTable(abilityNameList, KeywordToName, AbilityKeyword.GolemFireBalm, "Fire Balm");
        AddAbilityToNameListAndTable(abilityNameList, KeywordToName, AbilityKeyword.SummonedFireWall, "Fire Walls");
        AddAbilityToNameListAndTable(abilityNameList, KeywordToName, AbilityKeyword.SummonSkeleton, "Summoned Skeletons");
        AddAbilityToNameListAndTable(abilityNameList, KeywordToName, AbilityKeyword.SummonSkeletonArcherOrMage, "Summoned Skeletal Archers and Mages");

        nameToKeyword = new Dictionary<string, List<AbilityKeyword>>();
        foreach (KeyValuePair<AbilityKeyword, string> Entry in KeywordToName)
        {
            Debug.Assert(abilityNameList.Contains(Entry.Value));
            nameToKeyword.Add(Entry.Value, new List<AbilityKeyword>() { Entry.Key });
        }

        nameToKeyword.Add("Fire Walls'", new List<AbilityKeyword>() { AbilityKeyword.SummonedFireWall });
        nameToKeyword.Add("Summoned Skeletal Swordsmen", new List<AbilityKeyword>() { AbilityKeyword.SummonSkeletonSwordsman });

        foreach (AbilityKeyword Keyword in GenericAbilityList)
        {
            bool IsInTable = false;
            foreach (KeyValuePair<string, List<AbilityKeyword>> Entry in WideAbilityTable)
                if (Entry.Value.Contains(Keyword))
                {
                    IsInTable = true;
                    break;
                }

            Debug.Assert(IsInTable);
        }

        foreach (KeyValuePair<string, List<AbilityKeyword>> Entry in WideAbilityTable)
            foreach (AbilityKeyword Keyword in Entry.Value)
                Debug.Assert(GenericAbilityList.Contains(Keyword));

        foreach (KeyValuePair<string, List<AbilityKeyword>> Entry in WideAbilityTable)
        {
            string Pattern = Entry.Key;
            List<AbilityKeyword> KeywordList = Entry.Value;

            foreach (AbilityKeyword Keyword in KeywordList)
                Debug.Assert(GenericAbilityList.Contains(Keyword));

            if (!Pattern.EndsWith(" ability"))
            {
                string PatternWithAbility = Pattern + " ability";

                Debug.Assert(!nameToKeyword.ContainsKey(PatternWithAbility));
                nameToKeyword.Add(PatternWithAbility, KeywordList);
                abilityNameList.Add(PatternWithAbility);
            }

            Debug.Assert(!nameToKeyword.ContainsKey(Pattern));
            nameToKeyword.Add(Pattern, KeywordList);
            abilityNameList.Add(Pattern);
        }

        abilityNameList.Sort();

        for (int i = 0; i < abilityNameList.Count; i++)
        {
            string SmallName = abilityNameList[i];

            for (int j = i + 1; j < abilityNameList.Count; j++)
            {
                string LargeName = abilityNameList[j];

                if (LargeName.Contains(SmallName))
                {
                    abilityNameList.RemoveAt(j);
                    abilityNameList.RemoveAt(i);
                    abilityNameList.Insert(i, LargeName);
                    abilityNameList.Insert(i + 1, SmallName);
                    i--;
                    break;
                }
            }
        }
    }

    public static string AbilityBaseName(PgAbility ability)
    {
        string Result = ability.Name;
        bool HasDigit = false;

        while (Result.Length > 0)
        {
            char c = Result[Result.Length - 1];
            if (char.IsDigit(c))
                HasDigit = true;
            else if (c == '#')
            {
            }
            else if (c == '-' && HasDigit)
            {
            }
            else
                break;

            Result = Result.Substring(0, Result.Length - 1);
        }

        Result = Result.Trim();

        return Result;
    }

    private void AddAbilityToNameList(List<string> abilityNameList, string abilityName)
    {
        Debug.Assert(!abilityNameList.Contains(abilityName));

        abilityNameList.Add(abilityName);
    }

    private void AddAbilityToNameListAndTable(List<string> abilityNameList, Dictionary<AbilityKeyword, string> keywordToName, AbilityKeyword keyword, string abilityName)
    {
        Debug.Assert(!abilityNameList.Contains(abilityName));

        abilityNameList.Add(abilityName);
        keywordToName.Add(keyword, abilityName);
    }

    private void VerifyStaticEffects(List<AbilityKeyword> modAbilityList, List<PgCombatEffect> staticCombatEffectList)
    {
        foreach (AbilityKeyword Keyword in modAbilityList)
            foreach (PgCombatEffect CombatEffect in staticCombatEffectList)
                VerifyStaticEffects(Keyword, staticCombatEffectList, CombatEffect);
    }

    private List<CombatKeyword> TodoKeywordList = new List<CombatKeyword>();

    private void VerifyStaticEffects(AbilityKeyword keyword, List<PgCombatEffect> combatEffectList, PgCombatEffect combatEffect)
    {
        switch (combatEffect.Keyword)
        {
            // Handled directly in display.
            case CombatKeyword.AddPowerCost:
            case CombatKeyword.AddResetTimer:
            case CombatKeyword.AddRange:
            case CombatKeyword.AddRage:
            case CombatKeyword.ZeroRage:
                break;

            // Handled in special values.
            case CombatKeyword.RestoreHealth:
            case CombatKeyword.RestorePower:
            case CombatKeyword.RestoreArmor:
            case CombatKeyword.RestoreHealthArmor:
            case CombatKeyword.RestoreHealthArmorPower:
            case CombatKeyword.AddMaxHealth:
            case CombatKeyword.DrainHealth:
            case CombatKeyword.DrainArmor:
            case CombatKeyword.DrainHealthMax:
            case CombatKeyword.DrainArmorMax:
            case CombatKeyword.DamageBoost:
            case CombatKeyword.DebuffMitigation:
            case CombatKeyword.AddSprintSpeed:
            case CombatKeyword.DealArmorDamage:
                VerifyStaticEffectKeyword(keyword, combatEffectList, combatEffect.Keyword, true);
                break;

            // Empty
            case CombatKeyword.AddChannelingTime:
            case CombatKeyword.TargetSubsequentAttacks:
            case CombatKeyword.TargetSubsequentRageAttacks:
            case CombatKeyword.EffectDuration:
            case CombatKeyword.AnotherTrap:
            case CombatKeyword.ChangeDamageType:
            case CombatKeyword.AddMitigation:
            case CombatKeyword.NextAttack:
            case CombatKeyword.DealDirectHealthDamage:
            case CombatKeyword.EffectDelay:
            case CombatKeyword.EffectRecurrence:
            case CombatKeyword.ActiveSkill:
            case CombatKeyword.AddEvasionMelee:
            case CombatKeyword.OnEvadeMelee:
            case CombatKeyword.OnEvade:
            case CombatKeyword.MitigateReflect:
            case CombatKeyword.ReflectRate:
            case CombatKeyword.MitigateReflectKick:
            case CombatKeyword.Combo7:
            case CombatKeyword.ComboFinalStepDamageAndStun:
            case CombatKeyword.TargetSelf:
            case CombatKeyword.StunIncorporeal:
            case CombatKeyword.AddTaunt:
            case CombatKeyword.EffectDurationMinute:
            case CombatKeyword.CombatRefreshRestoreHeatlth:
            case CombatKeyword.ResetOtherAbilityTimer:
            case CombatKeyword.AddMaxArmor:
            case CombatKeyword.DamageBoostAgainstSpecie:
            case CombatKeyword.DealIndirectDamage:
            case CombatKeyword.ZeroTaunt:
            case CombatKeyword.ThickArmor:
            case CombatKeyword.ReflectOnBurst:
            case CombatKeyword.AddMitigationIndirect:
            case CombatKeyword.ReflectOnAnyAttack:
            case CombatKeyword.MaxStack:
            case CombatKeyword.AddEvasionBurst:
            case CombatKeyword.AddChanceToIgnoreKnockback:
            case CombatKeyword.AddChanceToIgnoreStun:
            case CombatKeyword.AboveRage:
            case CombatKeyword.AddChanceToKnockdown:
            case CombatKeyword.ApplyWithChance:
            case CombatKeyword.RequireTwoKnives:
            case CombatKeyword.RequireNoAggro:
            case CombatKeyword.BaseDamageBoost:
            case CombatKeyword.DrainAsArmor:
            // case CombatKeyword.MaxOccurence:
            case CombatKeyword.ChanceToConsume:
            // case CombatKeyword.AddHealthRegen:
            //case CombatKeyword.Combo1:
            case CombatKeyword.ComboFinalStepBurst:
            case CombatKeyword.Combo2:
            case CombatKeyword.ComboFinalStepDamage:
            case CombatKeyword.Combo3:
            case CombatKeyword.Combo4:
            case CombatKeyword.Stun:
            case CombatKeyword.Combo5:
            case CombatKeyword.Combo6:
            case CombatKeyword.NotAttackedRecently:
            case CombatKeyword.AddPowerRegen:
            case CombatKeyword.AddPowerCostMax:
            case CombatKeyword.ZeroPowerCost:
            case CombatKeyword.AddIndirectVulnerability:
            case CombatKeyword.AddVulnerability:
            case CombatKeyword.ReflectKnockbackOnFirstMelee:
            case CombatKeyword.ReflectOnMelee:
            case CombatKeyword.ReflectOnRanged:
            case CombatKeyword.ReflectMeleeIndirectDamage:
            case CombatKeyword.AddCombatRefreshTimer:
            case CombatKeyword.WithinDistance:
            case CombatKeyword.Knockback:
            case CombatKeyword.DamageBoostToHealthAndArmor:
                VerifyStaticEffectKeyword(keyword, combatEffectList, combatEffect.Keyword, false);
                break;

            // Ignored
            case CombatKeyword.But:
            case CombatKeyword.TargetElite:
            case CombatKeyword.AnimalPetRageAttackBoost:
            case CombatKeyword.ApplyToAllies:
            case CombatKeyword.WhenTeleporting:
            case CombatKeyword.AddAccuracy:
            case CombatKeyword.TargetUndead:
                break;

            default:
                Debug.WriteLine($"Unexpected keyword to verify: {combatEffect.Keyword}");
                break;
        }
    }

    public static bool HasNonSpecialValueEffect(List<PgCombatEffect> combatEffectList, out bool hasRecurrence)
    {
        hasRecurrence = false;

        foreach (PgCombatEffect Item in combatEffectList)
        {
            if (Item.Keyword == CombatKeyword.ReflectOnAnyAttack || Item.Keyword == CombatKeyword.ApplyWithChance)
                return true;
            if (Item.Keyword == CombatKeyword.EffectRecurrence)
                hasRecurrence = true;
        }

        return false;
    }

    private void VerifyStaticEffectKeyword(AbilityKeyword keyword, List<PgCombatEffect> combatEffectList, CombatKeyword combatKeyword, bool expectTableWithEntries, bool showCandidates = false)
    {
        if (HasNonSpecialValueEffect(combatEffectList, out _))
            return;

        List<KeyValuePair<string, string>> VerificationTable = null!;

        if (expectTableWithEntries)
        {
            if (!EffectVerification.EffectVerificationTable.ContainsKey(combatKeyword))
            {
                Debug.WriteLine($"ERROR Combat Keyword {combatKeyword}: no entries?");
                EffectVerification.EffectVerificationTable.Add(combatKeyword, new List<KeyValuePair<string, string>>());
            }

            VerificationTable = EffectVerification.EffectVerificationTable[combatKeyword];
        }
        else
        {
            if (EffectVerification.EffectVerificationTable.ContainsKey(combatKeyword))
                Debug.WriteLine($"ERROR Combat Keyword {combatKeyword}: has entries?");
        }

        int UnverifiedCount = 0;

        foreach (PgAbility Ability in ValidAbilityList)
            if (Ability.KeywordList.Contains(keyword))
            {
                int VerificationCount = 0;

                if (expectTableWithEntries)
                {
                    IList<PgSpecialValue> SpecialValueList = GetSpecialValueList(Ability);
                    foreach (PgSpecialValue SpecialValue in SpecialValueList)
                    {
                        string Label = SpecialValue.Label;
                        string Suffix = SpecialValue.Suffix;

                        foreach (KeyValuePair<string, string> Entry in VerificationTable)
                            if (Label == Entry.Key && Suffix == Entry.Value)
                            {
                                VerificationCount++;
                                break;
                            }
                    }
                }

                if (VerificationCount != 1)
                    UnverifiedCount++;
            }

        if (UnverifiedCount > 0)
        {
            // Debug.WriteLine($"Combat Keyword {combatKeyword}: {UnverifiedCount} unverified abilities");
            if (!UnverifiedTable.ContainsKey(combatKeyword))
                UnverifiedTable.Add(combatKeyword, new List<KeyValuePair<string, string>>());
            List<KeyValuePair<string, string>> SpecificUnverifiedTable = UnverifiedTable[combatKeyword];

            foreach (PgAbility Ability in ValidAbilityList)
                if (Ability.KeywordList.Contains(keyword))
                {
                    int VerificationCount = 0;

                    IList<PgSpecialValue> SpecialValueList = GetSpecialValueList(Ability);
                    foreach (PgSpecialValue SpecialValue in SpecialValueList)
                    {
                        string Label = SpecialValue.Label;
                        string Suffix = SpecialValue.Suffix;

                        bool IsFound = false;
                        foreach (KeyValuePair<string, string> Entry in SpecificUnverifiedTable)
                            if (Entry.Key == Label && Entry.Value == Suffix)
                            {
                                IsFound = true;
                                break;
                            }

                        if (!IsFound)
                        {
                            SpecificUnverifiedTable.Add(new KeyValuePair<string, string>(Label, Suffix));

                            if (showCandidates)
                                Debug.WriteLine($"new KeyValuePair<string, string>(\"{Label}\", \"{Suffix}\"),");
                        }
                    }

                    if (VerificationCount != 1)
                        UnverifiedCount++;
                }
        }
    }

    public static Dictionary<CombatKeyword, List<KeyValuePair<string, string>>> UnverifiedTable { get; } = new Dictionary<CombatKeyword, List<KeyValuePair<string, string>>>()
    {
    };

    public static AbilityKeyword Internal_NonBasic { get; } = (AbilityKeyword)0xFFFF;
    private List<PgAbility> ValidAbilityList = null!;
    private List<string> AbilityObjectKeyList = new List<string>();
    private List<string> EffectObjectKeyList = new List<string>();
    private List<string> PowerObjectKeyList = new List<string>();
    #endregion

    #region Write File
    private void WritePowerKeyToCompleteEffectFile(string fileName, List<string[]> stringKeyTable, List<PgModEffect[]> powerKeyToCompleteEffectTable, List<string> effectKeyList)
    {
        using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
        {
            using (StreamWriter sw = new StreamWriter(fs))
            {
                Debug.Assert(stringKeyTable.Count + effectKeyList.Count == powerKeyToCompleteEffectTable.Count);

                sw.WriteLine("namespace PgBuilder");
                sw.WriteLine("{");
                sw.WriteLine("    using System.Collections.Generic;");
                sw.WriteLine("    using PgObjects;");
                sw.WriteLine(string.Empty);
                sw.WriteLine("    public static class PowerKeyToCompleteEffect");
                sw.WriteLine("    {");
                sw.WriteLine("        public static Dictionary<string, string> EffectKey { get; } = new Dictionary<string, string>()");
                sw.WriteLine("        {");

                for (int i = 0; i < stringKeyTable.Count + effectKeyList.Count; i++)
                {
                    string[] StringKeyArray;

                    if (i < stringKeyTable.Count)
                        StringKeyArray = stringKeyTable[i];
                    else
                        StringKeyArray = new string[] { effectKeyList[i - stringKeyTable.Count] };

                    PgModEffect[] ModEffectArray = powerKeyToCompleteEffectTable[i];

                    Debug.Assert(StringKeyArray.Length == ModEffectArray.Length);

                    for (int j = 0; j < StringKeyArray.Length; j++)
                    {
                        string StringKey = StringKeyArray[j];
                        PgModEffect PgModEffect = ModEffectArray[j];

                        if (PgModEffect is not null)
                            WritePowerKeyToCompleteEffectKeyLine(sw, StringKey, PgModEffect.EffectKey);
                    }
                }

                sw.WriteLine("        };");
                sw.WriteLine(string.Empty);
                sw.WriteLine("        public static Dictionary<string, List<AbilityKeyword>> AbilityList { get; } = new Dictionary<string, List<AbilityKeyword>>()");
                sw.WriteLine("        {");

                for (int i = 0; i < stringKeyTable.Count + effectKeyList.Count; i++)
                {
                    string[] StringKeyArray;

                    if (i < stringKeyTable.Count)
                        StringKeyArray = stringKeyTable[i];
                    else
                        StringKeyArray = new string[] { effectKeyList[i - stringKeyTable.Count] };

                    PgModEffect[] ModEffectArray = powerKeyToCompleteEffectTable[i];

                    Debug.Assert(StringKeyArray.Length == ModEffectArray.Length);

                    for (int j = 0; j < StringKeyArray.Length; j++)
                    {
                        string StringKey = StringKeyArray[j];
                        PgModEffect PgModEffect = ModEffectArray[j];

                        WritePowerKeyToCompleteEffectAbilityKeywordListLine(sw, StringKey, PgModEffect.AbilityList);
                    }
                }

                sw.WriteLine("        };");
                sw.WriteLine(string.Empty);
                sw.WriteLine("        public static Dictionary<string, List<CombatEffect>> StaticCombatEffectList { get; } = new Dictionary<string, List<CombatEffect>>()");
                sw.WriteLine("        {");

                for (int i = 0; i < stringKeyTable.Count + effectKeyList.Count; i++)
                {
                    string[] StringKeyArray;

                    if (i < stringKeyTable.Count)
                        StringKeyArray = stringKeyTable[i];
                    else
                        StringKeyArray = new string[] { effectKeyList[i - stringKeyTable.Count] };

                    PgModEffect[] ModEffectArray = powerKeyToCompleteEffectTable[i];

                    Debug.Assert(StringKeyArray.Length == ModEffectArray.Length);

                    for (int j = 0; j < StringKeyArray.Length; j++)
                    {
                        string StringKey = StringKeyArray[j];
                        PgModEffect PgModEffect = ModEffectArray[j];

                        WritePowerKeyToCompleteEffectCombatEffectListLine(sw, StringKey, PgModEffect.StaticCombatEffectList);
                    }
                }

                sw.WriteLine("        };");
                sw.WriteLine(string.Empty);
                sw.WriteLine("        public static Dictionary<string, List<CombatEffect>> DynamicCombatEffectList { get; } = new Dictionary<string, List<CombatEffect>>()");
                sw.WriteLine("        {");

                for (int i = 0; i < stringKeyTable.Count + effectKeyList.Count; i++)
                {
                    string[] StringKeyArray;

                    if (i < stringKeyTable.Count)
                        StringKeyArray = stringKeyTable[i];
                    else
                        StringKeyArray = new string[] { effectKeyList[i - stringKeyTable.Count] };

                    PgModEffect[] ModEffectArray = powerKeyToCompleteEffectTable[i];

                    Debug.Assert(StringKeyArray.Length == ModEffectArray.Length);

                    for (int j = 0; j < StringKeyArray.Length; j++)
                    {
                        string StringKey = StringKeyArray[j];
                        PgModEffect PgModEffect = ModEffectArray[j];

                        WritePowerKeyToCompleteEffectCombatEffectListLine(sw, StringKey, PgModEffect.DynamicCombatEffectList);
                    }
                }

                sw.WriteLine("        };");
                sw.WriteLine(string.Empty);
                sw.WriteLine("        public static Dictionary<string, List<AbilityKeyword>> TargetAbilityList { get; } = new Dictionary<string, List<AbilityKeyword>>()");
                sw.WriteLine("        {");

                for (int i = 0; i < stringKeyTable.Count + effectKeyList.Count; i++)
                {
                    string[] StringKeyArray;

                    if (i < stringKeyTable.Count)
                        StringKeyArray = stringKeyTable[i];
                    else
                        StringKeyArray = new string[] { effectKeyList[i - stringKeyTable.Count] };

                    PgModEffect[] ModEffectArray = powerKeyToCompleteEffectTable[i];

                    Debug.Assert(StringKeyArray.Length == ModEffectArray.Length);

                    for (int j = 0; j < StringKeyArray.Length; j++)
                    {
                        string StringKey = StringKeyArray[j];
                        PgModEffect PgModEffect = ModEffectArray[j];

                        WritePowerKeyToCompleteEffectAbilityKeywordListLine(sw, StringKey, PgModEffect.TargetAbilityList);
                    }
                }

                sw.WriteLine("        };");
                sw.WriteLine(string.Empty);
                sw.WriteLine("        public static List<string> EffectKeyList { get; } = new List<string>()");
                sw.WriteLine("        {");

                foreach (string EffectKey in effectKeyList)
                {
                    sw.WriteLine($"            \"{EffectKey}\",");
                }

                sw.WriteLine("        };");
                sw.WriteLine("    }");
                sw.WriteLine("}");
            }
        }
    }

    private void WritePowerKeyToCompleteEffectKeyLine(StreamWriter sw, string stringKey, string effectKey)
    {
        sw.WriteLine($"            {{ \"{stringKey}\", \"{effectKey}\" }},");
    }

    private void WritePowerKeyToCompleteEffectAbilityKeywordListLine(StreamWriter sw, string stringKey, List<AbilityKeyword> abilityList)
    {
        string AbilityKeywordListString = AbilityKeywordListToLongString(abilityList);

        sw.WriteLine($"            {{ \"{stringKey}\", new List<AbilityKeyword>() {AbilityKeywordListString} }},");
    }

    private void WritePowerKeyToCompleteEffectCombatEffectListLine(StreamWriter sw, string stringKey, List<PgCombatEffect> combatEffectList)
    {
        string CombatEffectListString = CombatEffectListToString(combatEffectList);

        sw.WriteLine($"            {{ \"{stringKey}\", new List<CombatEffect>() {CombatEffectListString} }},");
    }

    private void WriteModEffect(PgModEffect modEffect)
    {
        ParsingContext.AddSuplementaryObject(modEffect);

        foreach (PgCombatEffect CombatEffect in modEffect.StaticCombatEffectList)
            WriteCombatEffect(CombatEffect);

        foreach (PgCombatEffect CombatEffect in modEffect.DynamicCombatEffectList)
            WriteCombatEffect(CombatEffect);
    }

    private void WriteCombatEffect(PgCombatEffect combatEffect)
    {
        ParsingContext.AddSuplementaryObject(combatEffect);

        if (combatEffect.Keyword != CombatKeyword.Internal_None)
            StringToEnumConversion<CombatKeyword>.SetCustomParsedEnum(combatEffect.Keyword);

        if (combatEffect.Data != null)
            ParsingContext.AddSuplementaryObject(combatEffect.Data);

        if (combatEffect.DamageType != GameDamageType.Internal_None)
            StringToEnumConversion<GameDamageType>.SetCustomParsedEnum(combatEffect.DamageType);

        if (combatEffect.CombatSkill != GameCombatSkill.Internal_None)
            StringToEnumConversion<GameCombatSkill>.SetCustomParsedEnum(combatEffect.CombatSkill);
    }
    #endregion

    #region Data Analysis, Matching Powers and effects
    private void AnalyzeMatchingPowersAndEffects(List<string> abilityNameList, Dictionary<string, List<AbilityKeyword>> nameToKeyword, Dictionary<PgPower, List<PgEffect>> powerToEffectTable, List<string[]> stringKeyTable, List<PgModEffect[]> powerKeyToCompleteEffectTable)
    {
        int DebugIndex = 0;
        int SkipIndex = 0;

        foreach (KeyValuePair<PgPower, List<PgEffect>> Entry in powerToEffectTable)
        {
            DebugIndex++;

            if (SkipIndex > 0)
            {
                SkipIndex--;
                continue;
            }

            if (Entry.Key.Key == "28683")
            {
            }

            // Debug.WriteLine("");
            // Debug.WriteLine($"Debug Index: {DebugIndex - 1} / {powerToEffectTable.Count} (Matching)");
            PgPower ItemPower = Entry.Key;
            List<PgEffect> ItemEffectList = Entry.Value;

            AnalyzeMatchingPowersAndEffects(abilityNameList, nameToKeyword, ItemPower, ItemEffectList, out string[] stringKeyArray, out PgModEffect[] ModEffectArray);

            stringKeyTable.Add(stringKeyArray);
            powerKeyToCompleteEffectTable.Add(ModEffectArray);
        }

        // DisplayParsingResult(powerToEffectTable);
    }

    public static string CombatEffectListToString(List<PgCombatEffect> combatEffectList)
    {
        string CombatEffectListString = string.Empty;

        for (int j = 0; j < combatEffectList.Count; j++)
        {
            PgCombatEffect CombatEffect = combatEffectList[j];

            if (CombatEffectListString.Length > 0)
                CombatEffectListString += ", ";

            CombatEffectListString += CombatEffectToString(CombatEffect);
        }

        if (CombatEffectListString.Length == 0)
            CombatEffectListString = "{ }";
        else
            CombatEffectListString = $"{{ {CombatEffectListString} }}";

        return CombatEffectListString;
    }

    public static string CombatEffectToString(PgCombatEffect combatEffect)
    {
        string CombatEffectString;

        if (!combatEffect.Data.RawValue.HasValue && combatEffect.DamageType == GameDamageType.Internal_None && combatEffect.CombatSkill == GameCombatSkill.Internal_None)
            CombatEffectString = $"new CombatEffect(CombatKeyword.{combatEffect.Keyword})";
        else
        {
            if (combatEffect.Data.RawValue.HasValue)
            {
                string CreateMethod = combatEffect.Data.IsPercent ? "FromDoublePercent" : "FromDouble";
                string NumericValueString = $"NumericValue.{CreateMethod}({combatEffect.Data.Value.ToString(CultureInfo.InvariantCulture)})";

                if (combatEffect.DamageType == GameDamageType.Internal_None && combatEffect.CombatSkill == GameCombatSkill.Internal_None)
                    CombatEffectString = $"new CombatEffect(CombatKeyword.{combatEffect.Keyword}, {NumericValueString})";
                else
                    CombatEffectString = $"new CombatEffect(CombatKeyword.{combatEffect.Keyword}, {NumericValueString}, (GameDamageType){(int)combatEffect.DamageType}, GameCombatSkill.{combatEffect.CombatSkill})";
            }
            else
                CombatEffectString = $"new CombatEffect(CombatKeyword.{combatEffect.Keyword}, new NumericValue(), (GameDamageType){(int)combatEffect.DamageType}, GameCombatSkill.{combatEffect.CombatSkill})";
        }

        return CombatEffectString;
    }

    private void CompareWithPowerKeyToCompleteEffectTable(List<string[]> stringKeyTable, List<PgModEffect[]> analyzedPowerKeyToCompleteEffectTable, List<string> effectKeyList, Dictionary<string, PgModEffect> existingPowerKeyToCompleteEffectTable, Dictionary<string, PgModEffect> existingEffectKeyToCompleteEffectTable)
    {
        Debug.Assert(stringKeyTable.Count + effectKeyList.Count == analyzedPowerKeyToCompleteEffectTable.Count);

        for (int i = 0; i < stringKeyTable.Count + effectKeyList.Count; i++)
        {
            string[] StringKeyArray;

            if (i < stringKeyTable.Count)
                StringKeyArray = stringKeyTable[i];
            else
                StringKeyArray = new string[] { effectKeyList[i - stringKeyTable.Count] };

            PgModEffect[] ModEffectArray = analyzedPowerKeyToCompleteEffectTable[i];

            Debug.Assert(StringKeyArray.Length == ModEffectArray.Length);

            for (int j = 0; j < StringKeyArray.Length; j++)
            {
                string StringKey = StringKeyArray[j];
                PgModEffect PgModEffect = ModEffectArray[j];

                if (i < stringKeyTable.Count)
                    CompareWithPowerKeyToCompleteEffectLine(i, StringKey, PgModEffect, existingPowerKeyToCompleteEffectTable);
                else
                    CompareWithPowerKeyToCompleteEffectLine(i - stringKeyTable.Count, StringKey, PgModEffect, existingEffectKeyToCompleteEffectTable);
            }
        }
    }

    private void CompareWithPowerKeyToCompleteEffectLine(int index, string stringKey, PgModEffect modEffect, Dictionary<string, PgModEffect> existingPowerKeyToCompleteEffectTable)
    {
        if (!existingPowerKeyToCompleteEffectTable.ContainsKey(stringKey))
        {
            Debug.WriteLine($"Index #{index}, Key missing: {stringKey}");
            return;
        }

        if (!IsModEffecsEqualStrict(existingPowerKeyToCompleteEffectTable[stringKey], modEffect))
        {
            Debug.WriteLine($"Index #{index}, Line mistmatch for key {stringKey}");
        }
    }

    public static bool IsModEffecsEqualStrict(PgModEffect modEffect1, PgModEffect modEffect2)
    {
        if (modEffect1.EffectKey != modEffect2.EffectKey)
            return false;

        if (!IsSameAbilityKeywordList(modEffect1.AbilityList, modEffect2.AbilityList))
            return false;

        if (!IsCombatEffectEqualStrict(modEffect1.StaticCombatEffectList, modEffect2.StaticCombatEffectList))
            return false;

        if (!IsCombatEffectEqualStrict(modEffect1.DynamicCombatEffectList, modEffect2.DynamicCombatEffectList))
            return false;

        if (!IsSameAbilityKeywordList(modEffect1.TargetAbilityList, modEffect2.TargetAbilityList))
            return false;

        return true;
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

    public static bool IsSameCombatKeywordList(List<CombatKeyword> list1, List<CombatKeyword> list2)
    {
        if (list1.Count != list2.Count)
            return false;

        for (int i = 0; i < list1.Count; i++)
            if (list1[i] != list2[i])
                return false;

        return true;
    }

    public static bool IsCombatEffectEqualStrict(List<PgCombatEffect> list1, List<PgCombatEffect> list2)
    {
        if (list1.Count != list2.Count)
            return false;

        for (int i = 0; i < list1.Count; i++)
            if (!IsCombatEffectEqualStrict(list1[i], list2[i]))
                return false;

        return true;
    }

    public static bool IsCombatEffectEqualStrict(PgCombatEffect combatEffect1, PgCombatEffect combatEffect2)
    {
        if (combatEffect1.Keyword != combatEffect2.Keyword)
            return false;

        if (!IsNumericValueEqualStrict(combatEffect1.Data, combatEffect2.Data))
            return false;

        if (combatEffect1.DamageType != combatEffect2.DamageType)
            return false;

        if (combatEffect1.CombatSkill != combatEffect2.CombatSkill)
            return false;

        return true;
    }

    public static bool IsNumericValueEqualStrict(PgNumericValue v1, PgNumericValue v2)
    {
        if (v1.RawValue.HasValue != v2.RawValue.HasValue)
            return false;

        if (v1.RawValue.HasValue)
        {
            if (v1.IsPercent != v2.IsPercent)
                return false;

            if (v1.Value != v2.Value)
                return false;
        }

        return true;
    }

    private void CheckAllSentencesUsed()
    {
        foreach (Sentence Item in SentenceList)
            if (!Item.IsUsed)
                Debug.WriteLine($"Sentence '{Item.Format}' not used");
    }

    public static string PowerEffectPairKey(PgPower power, int tierIndex)
    {
        string PowerTierString = (tierIndex + 1).ToString("D03");
        string Key = $"{power.Key}_{PowerTierString}";

        return Key;
    }

    private void DisplayParsingResult(Dictionary<PgPower, List<PgEffect>> powerToEffectTable)
    {
        foreach (KeyValuePair<PgPower, List<PgEffect>> Entry in powerToEffectTable)
        {
            PgPowerTierCollection TierList = Entry.Key.TierList;
            PgPowerTier LastTier = TierList[TierList.Count - 1];

            foreach (PgPowerEffect ItemEffect in LastTier.EffectList)
            {
                if (ItemEffect is PgPowerEffectSimple AsSimpleEffect)
                {
                    List<PgEffect> EffectList = Entry.Value;
                    PgEffect Effect = EffectList[EffectList.Count - 1];

                    Debug.WriteLine(string.Empty);
                    Debug.WriteLine(AsSimpleEffect.Description);
                    Debug.WriteLine(Effect.Description);
                    break;
                }
            }
        }
    }

    private void AnalyzeMatchingPowersAndEffects(List<string> abilityNameList, Dictionary<string, List<AbilityKeyword>> nameToKeyword, PgPower itemPower, List<PgEffect> itemEffectList, out string[] stringKeyArray, out PgModEffect[] modEffectArray)
    {
        PgPowerTierCollection TierList = itemPower.TierList;

        Debug.Assert(TierList.Count == itemEffectList.Count);
        Debug.Assert(TierList.Count > 0);

        int ValidationIndex = 0;
        if (TierList.Count >= 2)
            ValidationIndex = 1;
        if (TierList.Count >= 4)
            ValidationIndex = 2;

        List<CombatKeyword>[] PowerTierKeywordListArray = new List<CombatKeyword>[TierList.Count];
        List<CombatKeyword>[] EffectKeywordListArray = new List<CombatKeyword>[TierList.Count];
        stringKeyArray = new string[TierList.Count];
        modEffectArray = new PgModEffect[TierList.Count];

        int LastTierIndex = TierList.Count - 1;
        PgPowerTier LastTier = TierList[LastTierIndex];

        AnalyzeMatchingPowersAndEffects(abilityNameList, nameToKeyword, LastTier, itemEffectList[LastTierIndex], out List<CombatKeyword> ExtractedPowerTierKeywordList, out List<CombatKeyword> ExtractedEffectKeywordList, out PgModEffect ExtractedModEffect, true);
        PowerTierKeywordListArray[LastTierIndex] = ExtractedPowerTierKeywordList;
        EffectKeywordListArray[LastTierIndex] = ExtractedEffectKeywordList;
        modEffectArray[LastTierIndex] = ExtractedModEffect;

        for (int i = 0; i + 1 < TierList.Count; i++)
        {
            AnalyzeMatchingPowersAndEffects(abilityNameList, nameToKeyword, TierList[i], itemEffectList[i], out List<CombatKeyword> ComparedPowerTierKeywordList, out List<CombatKeyword> ComparedEffectKeywordList, out PgModEffect ParsedModEffect, false);

            bool AllowIncomplete = i < ValidationIndex;

            if (!IsSameCombatKeywordList(ExtractedPowerTierKeywordList, ComparedPowerTierKeywordList, AllowIncomplete))
            {
                Debug.WriteLine($"Mismatching power at tier #{i}");
                return;
            }

            if (!IsSameCombatKeywordList(ExtractedEffectKeywordList, ComparedEffectKeywordList, AllowIncomplete))
            {
                Debug.WriteLine($"Mismatching effect at tier #{i}");
                return;
            }

            PowerTierKeywordListArray[i] = ComparedPowerTierKeywordList;
            EffectKeywordListArray[i] = ComparedEffectKeywordList;
            modEffectArray[i] = ParsedModEffect;
        }

        for (int i = 0; i < TierList.Count; i++)
        {
            string Key = PowerEffectPairKey(itemPower, i);
            stringKeyArray[i] = Key;
        }
    }

    private bool IsSameCombatKeywordList(List<CombatKeyword> list1, List<CombatKeyword> list2, bool allowIncomplete)
    {
        if (list2.Count == list1.Count + 1 && list2[0] == CombatKeyword.ApplyWithChance)
            return true;

        if ((!allowIncomplete && list1.Count != list2.Count) || (allowIncomplete && list1.Count < list2.Count))
            return false;

        Debug.Assert(list1.Count >= list2.Count);

        List<CombatKeyword> MissingKeywordList = new List<CombatKeyword>();
        for (int i = 0; i < list2.Count; i++)
            if (!list1.Contains(list2[i]))
                MissingKeywordList.Add(list2[i]);

        if (MissingKeywordList.Count == 0)
            return true;

        if (MissingKeywordList.Count == 1 && MissingKeywordList[0] == CombatKeyword.ApplyWithChance && list1.Contains(CombatKeyword.DamageBoost) && list1.Count == list2.Count)
            return true;

        return false;
    }

    private bool AnalyzeMatchingPowersAndEffects(List<string> abilityNameList, Dictionary<string, List<AbilityKeyword>> nameToKeyword, PgPowerTier powerTier, PgEffect effect, out List<CombatKeyword> extractedPowerTierKeywordList, out List<CombatKeyword> extractedEffectKeywordList, out PgModEffect modEffect, bool displayAnalysisResult)
    {
        modEffect = null!;

        IList<PgPowerEffect> EffectList = powerTier.EffectList;
        Debug.Assert(EffectList.Count == 1);
        PgPowerEffectSimple powerSimpleEffect = (PgPowerEffectSimple)EffectList[0];

        string ModText = powerSimpleEffect.Description;
        string EffectText = effect.Description;

        HackModAndEffectText(ref ModText, ref EffectText);

        bool IsMod = false;
        if (EffectText == ModText)
            IsMod = true;

        AnalyzeText(abilityNameList, nameToKeyword, ModText, true, out List<AbilityKeyword> ModAbilityList, out PgCombatEffectCollection ModCombatList, out List<AbilityKeyword> ModTargetAbilityList);
        AnalyzeText(abilityNameList, nameToKeyword, EffectText, IsMod, out List<AbilityKeyword> EffectAbilityList, out PgCombatEffectCollection EffectCombatList, out List<AbilityKeyword> EffectTargetAbilityList);

        // Hack for Animal Handling
        if ((ModAbilityList.Contains(AbilityKeyword.SicEm) || ModAbilityList.Contains(AbilityKeyword.CleverTrick)) &&
            EffectAbilityList.Count == 0 &&
            (EffectTargetAbilityList.Contains(AbilityKeyword.SicEm) || EffectTargetAbilityList.Contains(AbilityKeyword.CleverTrick)))
        {
            EffectTargetAbilityList.Clear();
        }

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

        bool IsContained = CombatEffectContains(ModCombatList, EffectCombatList, out PgCombatEffectCollection StaticCombatEffectList, out PgCombatEffectCollection DynamicCombatEffectList);
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

        if (IsMod && StaticCombatEffectList.Count == 0)
        {
            bool HasNonDisplayedEffect = false;
            foreach (PgCombatEffect Item in DynamicCombatEffectList)
                switch (Item.Keyword)
                {
                    case CombatKeyword.DamageBoost:
                        break;

                    default:
                        HasNonDisplayedEffect = true;
                        break;
                }

            if (!HasNonDisplayedEffect)
            {
                StaticCombatEffectList = DynamicCombatEffectList;
                DynamicCombatEffectList = new PgCombatEffectCollection();

                if (displayAnalysisResult)
                {
                    /*Debug.WriteLine("");
                    Debug.WriteLine($"   Effect: {effect.Desc}");
                    Debug.WriteLine($"Parsed as: {ParsedEffectString}, Target: {ParsedEffectTargetAbilityList}");
                    Debug.WriteLine($"    Power: {powerSimpleEffect.Description}");
                    Debug.WriteLine($"Parsed as: {{{ParsedAbilityList}}} {ParsedPowerString}, Target: {ParsedModTargetAbilityList}");*/
                }
            }
        }

        if (displayAnalysisResult)
        {
            /*Debug.WriteLine("");
            Debug.WriteLine($"   Effect: {effect.Desc}");
            Debug.WriteLine($"Parsed as: {ParsedEffectString}, Target: {ParsedEffectTargetAbilityList}");
            Debug.WriteLine($"    Power: {powerSimpleEffect.Description}");
            Debug.WriteLine($"Parsed as: {{{ParsedAbilityList}}} {ParsedPowerString}, Target: {ParsedModTargetAbilityList}");*/
        }

        modEffect = new PgModEffect()
        {
            EffectKey = effect.Key,
            AbilityList = ModAbilityList,
            StaticCombatEffectList = StaticCombatEffectList,
            DynamicCombatEffectList = DynamicCombatEffectList,
            TargetAbilityList = ModTargetAbilityList,
        };

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

        VerifyStaticEffects(ModAbilityList, StaticCombatEffectList);

        return true;
    }

    public static bool CombatEffectEquals(PgCombatEffect effect1, PgCombatEffect effect2)
    {
        if (effect1.Keyword != effect2.Keyword && effect1.Keyword != CombatKeyword.Again && effect2.Keyword != CombatKeyword.Again)
        {
            if (effect1.Keyword != CombatKeyword.AddVulnerability && effect2.Keyword != CombatKeyword.AddVulnerability)
                return false;
        }

        if (effect1.Data.RawValue.HasValue != effect2.Data.RawValue.HasValue && effect2.Data.RawValue.HasValue)
            return false;

        if (effect2.Data.RawValue.HasValue && effect1.Data.Value != effect2.Data.Value)
            return false;

        return true;
    }

    public static bool CombatEffectContains(List<PgCombatEffect> list, List<PgCombatEffectCollection> candidateList, out int candidateIndex, out PgCombatEffectCollection difference, out PgCombatEffectCollection union)
    {
        Debug.Assert(candidateList.Count > 0);

        List<bool> ContainList = new List<bool>();
        List<PgCombatEffectCollection> DifferenceList = new List<PgCombatEffectCollection>();
        List<PgCombatEffectCollection> UnionList = new List<PgCombatEffectCollection>();
        List<int> UnionCountList = new List<int>();
        candidateIndex = -1;

        for (int i = 0; i < candidateList.Count; i++)
        {
            bool ContainValue = CombatEffectContains(list, candidateList[i], out PgCombatEffectCollection DifferenceValue, out PgCombatEffectCollection UnionValue);

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
            difference = new PgCombatEffectCollection();
            union = new PgCombatEffectCollection();
            return false;
        }
    }

    public static bool CombatEffectContains(List<PgCombatEffect> list1, List<PgCombatEffect> list2, out PgCombatEffectCollection difference, out PgCombatEffectCollection union)
    {
        difference = new PgCombatEffectCollection();
        union = new PgCombatEffectCollection();

        List<PgCombatEffect> MatchList = new List<PgCombatEffect>();
        List<PgCombatEffect> NoMatchList = new List<PgCombatEffect>();

        foreach (PgCombatEffect Item2 in list2)
        {
            bool IsContained = false;
            foreach (PgCombatEffect Item1 in list1)
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

        foreach (PgCombatEffect Item in list1)
            if (AddToDifference)
                if (!MatchList.Contains(Item))
                {
                    if (Item.Keyword != CombatKeyword.But)
                        difference.Add(Item);
                }
                else
                    AddToDifference = false;
            else if (Item.Keyword == CombatKeyword.But)
            {
                AddToDifference = true;

                int ItemIndex = list1.IndexOf(Item);
                if (ItemIndex >= 2)
                    if (list1[ItemIndex - 1].Keyword == CombatKeyword.DamageBoost)
                        difference.Add(list1[ItemIndex - 1]);
            }
            else if (!MatchList.Contains(Item))
                NoMatchList.Add(Item);

        if (NoMatchList.Count == 1 && difference.Count == 0)
        {
            PgCombatEffect NoMatch = NoMatchList[0];
            switch (NoMatch.Keyword)
            {
                case CombatKeyword.AddTaunt:
                case CombatKeyword.AddRage:
                case CombatKeyword.DebuffMitigation:
                    difference.Add(NoMatch);
                    break;
            }
        }

        foreach (PgCombatEffect Item in list1)
            if (!difference.Contains(Item) && Item.Keyword != CombatKeyword.But)
                union.Add(Item);

        return true;
    }

    private void HackModAndEffectText(ref string modText, ref string effectText)
    {
        if (modText == "Doe Eyes restores 3 power" && effectText == "Restores 3 Armor")
            effectText = "Restores 3 Power";
        else if (effectText.EndsWith("for 20 seconds or until you teleport"))
            effectText = effectText.Substring(0, effectText.Length - 22 - 15) + effectText.Substring(effectText.Length - 22);
        else if (effectText.EndsWith("for 20 seconds or until you Feint"))
            effectText = effectText.Substring(0, effectText.Length - 19 - 15) + effectText.Substring(effectText.Length - 19);
        else if (effectText.StartsWith("Increases target's Max Rage by") && effectText.EndsWith("for 60 seconds"))
            effectText = effectText.Substring(0, effectText.Length - 15);
        else if (effectText.StartsWith("Indirect Poison +") && effectText.EndsWith(" for 5 seconds"))
            effectText = effectText.Substring(0, effectText.Length - 14) + " per tick";
        else if (effectText.StartsWith("Buffs targets' direct") && effectText.EndsWith("for 30 seconds every 5 seconds"))
            effectText = effectText.Substring(0, effectText.Length - 15) + " (stacking up to 6 times)";
        else if (modText.EndsWith("% damage to health and armor") && effectText.EndsWith("% Armor damage"))
            effectText = effectText.Substring(0, effectText.Length - 14) + "% Health and Armor damage";
        else if (modText.StartsWith("Astral Strike's "))
            modText = "Astral Strike" + modText.Substring(15);
        else if (modText.StartsWith("Pixie Flare's "))
            modText = "Pixie Flare" + modText.Substring(13);
        else if (effectText.StartsWith("Kick damage ") && modText.Contains("all kicks"))
            effectText = "All kicks damage " + effectText.Substring(12);
        else if (modText.StartsWith("While Blur Step is active, "))
            modText = modText.Substring(27) + " while Blur Step is active";
        else if (effectText.StartsWith("Universal Direct Elite Mitigation ") && effectText.EndsWith(" for 15 seconds"))
            effectText = effectText.Replace("for 15 seconds", "for 10 seconds");
        
        /*if (modText.StartsWith("When you trigger Cloud Trick, "))
            modText = modText.Replace("When you trigger Cloud Trick", "When you trigger Teleport");*/
        if (effectText.StartsWith("When you trigger Cloud Trick, "))
            effectText = effectText.Replace("When you trigger Cloud Trick", "When you trigger Teleport");

        int IndexFound = 0;
        RemoveDecorativeText(ref modText, "have less than a third of their Armor", "have less than 33% of their Armor", out _, ref IndexFound);
        RemoveDecorativeText(ref modText, "have less than a third of their Max Rage", "have less than 33% of their Max Rage", out _, ref IndexFound);
        RemoveDecorativeText(ref modText, "has less than a third of their Max Rage", "have less than 33% of their Max Rage", out _, ref IndexFound);
        RemoveDecorativeText(ref effectText, "have less than a third of their Armor", "have less than 33% of their Armor", out _, ref IndexFound);
        RemoveDecorativeText(ref effectText, "have less than a third of their Max Rage", "have less than 33% of their Max Rage", out _, ref IndexFound);
        RemoveDecorativeText(ref effectText, "has less than a third of their Max Rage", "have less than 33% of their Max Rage", out _, ref IndexFound);
        RemoveDecorativeText(ref modText, "you take half damage from", "you take 50% damage from", out _, ref IndexFound);
        RemoveDecorativeText(ref modText, "and Paradox Trot boosts Sprint Speed +1", out _, ref IndexFound);
        RemoveDecorativeText(ref modText, "(so it can be used again more quickly)", out _, ref IndexFound);
        RemoveDecorativeText(ref modText, "(both direct and indirect)", out _, ref IndexFound);

        int NegateIndex = modText.IndexOf(". (You can negate the latent psychic damage by using");
        if (NegateIndex >= 0)
            modText = modText.Substring(0, NegateIndex);

        if (effectText.StartsWith("DamageType:"))
        {
            int EndDamageNameIndex = effectText.IndexOf(";");
            if (EndDamageNameIndex > 11)
            {
                string DamageTypeName = effectText.Substring(11, EndDamageNameIndex - 11);
                effectText = $"Deals {DamageTypeName} damage and " + effectText.Substring(EndDamageNameIndex + 1).Trim();
            }
        }

        BasicTextReplace(ref modText, ref effectText, "Sic Em", "Sic 'Em");
        BasicTextReplace(ref modText, ref effectText, "physical (slashing, piercing, and crushing)", "Crushing, Slashing, or Piercing");
        BasicTextReplace(ref modText, ref effectText, "Animal Handling pets' healing abilities", "Pet Healing");
        BasicTextReplace(ref modText, ref effectText, "Pet basic attack", "Pet attack");

        // BasicTextReplace(ref modText, ref effectText, "Animal Handling pets'", "Animal Handling pets uuuuuuuuuuuuuuuuuuuuunused");
        BasicTextReplace(ref modText, ref effectText, "damage-over-time effects (if any)", "Damage over Time");
        BasicTextReplace(ref modText, ref effectText, "Fire damage no longer dispels Ice Armor", "Fire damage no longer dispels");
        BasicTextReplace(ref modText, ref effectText, "Fire damage no longer dispels your Ice Armor", "Fire damage no longer dispels");
        BasicTextReplace(ref modText, ref effectText, "Trick Foxes", "Trick Fox");
        BasicTextReplace(ref modText, ref effectText, "Bun-Fu Blitz", "Bun-Fu Kick");
        BasicTextReplace(ref modText, ref effectText, "after using Doe Eyes", string.Empty);

        if (!modText.Contains("But I Love You"))
            ReplaceCaseInsensitive(ref modText, " but ", " b*u*t ");
    }

    private void BasicTextReplace(ref string modText, ref string effectText, string oldText, string newText)
    {
        modText = modText.Replace(oldText, newText);
        effectText = effectText.Replace(oldText, newText);
    }

    private string AbilityKeywordListToLongString(List<AbilityKeyword> list)
    {
        string Result = string.Empty;

        foreach (AbilityKeyword Keyword in list)
        {
            if (Result.Length > 0)
                Result += ", ";

            if (Keyword == Internal_NonBasic)
                Result += "Parser.Internal_NonBasic";
            else
                Result += $"AbilityKeyword.{Keyword}";
        }

        if (Result.Length == 0)
            Result = "{ }";
        else
            Result = $"{{ {Result} }}";

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

    private string CombatEffectListToString(List<PgCombatEffect> list, out List<CombatKeyword> combatKeywordList)
    {
        string Result = string.Empty;
        combatKeywordList = new List<CombatKeyword>();

        foreach (PgCombatEffect Item in list)
        {
            if (Result.Length > 0)
                Result += ", ";

            Result += Item.ToString();
            combatKeywordList.Add(Item.Keyword);
        }

        return Result;
    }

    private void AnalyzeText(List<string> abilityNameList, Dictionary<string, List<AbilityKeyword>> nameToKeyword, string text, bool isMod, out List<AbilityKeyword> extractedAbilityList, out PgCombatEffectCollection extractedCombatEffectList, out List<AbilityKeyword> extractedTargetAbilityList)
    {
        RemoveDecorationText(ref text);
        SimplifyGrammar(ref text);
        SimplifyRandom(ref text);

        int RemoveCount;

        if (isMod)
            ExtractAbilityList(abilityNameList, nameToKeyword, true, ref text, out extractedAbilityList, out RemoveCount);
        else
        {
            extractedAbilityList = new List<AbilityKeyword>();
            RemoveCount = 0;
        }

        ExtractAbilityList(abilityNameList, nameToKeyword, false, ref text, out extractedTargetAbilityList, out int TargetRemoveCount);

        ExtractAttributesFull(text, extractedAbilityList, out extractedCombatEffectList);

        if (RemoveCount > extractedAbilityList.Count && extractedAbilityList.Count > 0 && extractedTargetAbilityList.Count == 0)
            if (extractedAbilityList[extractedAbilityList.Count - 1] != AbilityKeyword.Chill)
                extractedTargetAbilityList.Add(extractedAbilityList[extractedAbilityList.Count - 1]);

        // Hack for single abilities
        if (extractedAbilityList.Count == 1)
        {
            if (extractedAbilityList[0] == AbilityKeyword.Parry)
                extractedAbilityList[0] = AbilityKeyword.OnlyParry;
            else if (extractedAbilityList[0] == AbilityKeyword.Barrage)
                extractedAbilityList[0] = AbilityKeyword.BarrageOnly;
        }

        // Hack for major heals
        foreach (PgCombatEffect Item in extractedCombatEffectList)
            if (Item.Keyword == CombatKeyword.TargetAbilityBoost)
            {
                if (extractedTargetAbilityList.Count == 1 && extractedTargetAbilityList[0] == AbilityKeyword.MajorHeal)
                    Item.Keyword = CombatKeyword.RestoreHealth;
            }

        // Hack for multiple evasion
        float? EvasionValue = null;
        bool? EvasionIsPercent = null;
        foreach (PgCombatEffect Item in extractedCombatEffectList)
            if (Item.Keyword == CombatKeyword.AddEvasionBurst || Item.Keyword == CombatKeyword.AddEvasionProjectile || Item.Keyword == CombatKeyword.AddEvasionMelee)
                if (Item.Data.RawValue.HasValue && !EvasionValue.HasValue)
                {
                    EvasionValue = Item.Data.RawValue.Value;
                    EvasionIsPercent = Item.Data.RawIsPercent;
                }
                else if (!Item.Data.RawValue.HasValue && EvasionValue.HasValue)
                {
                    Item.Data.RawValue = EvasionValue.Value;
                    Item.Data.RawIsPercent = EvasionIsPercent;
                }

        bool IsRandom = false;
        int i = 0;
        while (i < extractedCombatEffectList.Count)
        {
            PgCombatEffect Item = extractedCombatEffectList[i];
            if (Item.Keyword == CombatKeyword.RandomDamage)
                if (!IsRandom)
                    IsRandom = true;
                else
                {
                    extractedCombatEffectList.RemoveAt(i);
                    continue;
                }

            i++;
        }
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
        ReplaceCaseInsensitive(ref text, " (or armor if health is full)", "/Armor");
    }

    private void ExtractAbilityList(List<string> abilityNameList, Dictionary<string, List<AbilityKeyword>> nameToKeyword, bool limitParsing, ref string text, out List<AbilityKeyword> extractedAbilityList, out int removeCount)
    {
        extractedAbilityList = new List<AbilityKeyword>();
        removeCount = 0;

        Dictionary<int, string> ExtractedTable = new Dictionary<int, string>();

        foreach (string AbilityName in abilityNameList)
            if (nameToKeyword.ContainsKey(AbilityName))
            {
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

    private void SimplifyGrammar(ref string text)
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
        ReplaceCaseInsensitive(ref text, "anf  ", "and ");
    }

    private void ReplaceCaseInsensitive(ref string text, string searchPattern, string replacementPattern)
    {
        string LowerText = text.ToLowerInvariant();
        int Index;

        while ((Index = LowerText.IndexOf(searchPattern)) >= 0)
        {
            text = text.Substring(0, Index) + replacementPattern + text.Substring(Index + searchPattern.Length);
            LowerText = text.ToLowerInvariant();
        }
    }

    private void SimplifyRandom(ref string text)
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

    private bool RemoveWideAbilityReferences(ref string text, List<AbilityKeyword> modifiedAbilityKeywordList, string pattern, List<AbilityKeyword> abilityKeywordList, out int indexFound)
    {
        string PatternWithAbility = pattern + " ability";
        string InputText = text;
        indexFound = text.Length;

        RemoveDecorativeText(ref InputText, PatternWithAbility, "@", out bool IsRemovedAbility, ref indexFound);
        RemoveDecorativeText(ref InputText, pattern, "@", out bool IsRemoved, ref indexFound);

        if (IsRemovedAbility || IsRemoved)
        {
            if (modifiedAbilityKeywordList.Count > 0)
            {
                string KeywordListString = string.Empty;

                foreach (AbilityKeyword Keyword in modifiedAbilityKeywordList)
                {
                    if (KeywordListString.Length > 0)
                        KeywordListString += ", ";

                    KeywordListString += Keyword.ToString();
                }

                Debug.WriteLine($"Ability already removed, for keywords {KeywordListString}");
            }
            else
            {
                text = InputText;
                modifiedAbilityKeywordList.AddRange(abilityKeywordList);
                return true;
            }
        }

        return false;
    }

    private void RemoveDecorativeText(ref string text, string pattern, out bool isRemoved, ref int indexFound)
    {
        RemoveDecorativeText(ref text, pattern, string.Empty, out isRemoved, ref indexFound);
    }

    private void RemoveDecorativeText(ref string text, string pattern, string replacement, out bool isRemoved, ref int indexFound)
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

    private bool IsStartingSentenceIndex(string text, int index)
    {
        return index == 0 || text[index - 1] == ' ' || text[index - 1] == ',';
    }

    private bool IsEndingSentenceIndex(string text, int index)
    {
        return index + 1 >= text.Length || text[index] == ' ' || text[index] == ',' || text[index] == '.' || (index + 1 < text.Length && text[index] == '\'' && text[index + 1] == 's');
    }

    private void ExtractAttributesFull(string text, List<AbilityKeyword> extractedAbilityList, out PgCombatEffectCollection extractedCombatEffectList)
    {
        List<CombatKeyword> SkippedKeywordList = new List<CombatKeyword>();
        ExtractAttributes(text, SkippedKeywordList, extractedAbilityList, out extractedCombatEffectList);
    }

    private void ExtractAttributes(string text, List<CombatKeyword> skippedKeywordList, List<AbilityKeyword> extractedAbilityList, out PgCombatEffectCollection extractedCombatEffectList)
    {
        extractedCombatEffectList = new PgCombatEffectCollection();
        List<CombatKeyword> SkippedKeywordListCopy = new List<CombatKeyword>(skippedKeywordList);

        bool HasUntiltrigger = false;
        bool HasDuration = false;

        bool IsAttributeExtracted;
        do
        {
            IsAttributeExtracted = ExtractKnownAttribute(SkippedKeywordListCopy, ref text, out List<PgCombatEffect> CombatEffectList);

            if (IsAttributeExtracted)
            {
                extractedCombatEffectList.AddRange(CombatEffectList);
                foreach (PgCombatEffect Item in CombatEffectList)
                {
                    SkippedKeywordListCopy.Add(Item.Keyword);

                    if (Item.Keyword == CombatKeyword.UntilTrigger)
                        HasUntiltrigger = true;
                    if (Item.Keyword == CombatKeyword.EffectDuration)
                        HasDuration = true;
                }
            }
        }
        while (IsAttributeExtracted);

        // Hack for ShadowFeint.
        if (extractedAbilityList.Count == 1 && extractedAbilityList[0] == AbilityKeyword.ShadowFeint && HasUntiltrigger && !HasDuration)
            extractedCombatEffectList.Add(new PgCombatEffect() { Keyword = CombatKeyword.EffectDuration, Data = NumericValueFromDouble(20) });

        // Hack for Aimed Shot.
        if (extractedAbilityList.Count == 1 && extractedAbilityList[0] == AbilityKeyword.AimedShot && extractedCombatEffectList.Count > 0 && extractedCombatEffectList[0].Keyword == CombatKeyword.DealDirectHealthDamage)
            extractedCombatEffectList[0] = new PgCombatEffect() { Keyword = CombatKeyword.DamageBoost, Data = extractedCombatEffectList[0].Data, DamageType = GameDamageType.Trauma, CombatSkill = GameCombatSkill.Internal_None };

        // Hack for Privacy Field
        if (extractedAbilityList.Count == 1 && extractedAbilityList[0] == AbilityKeyword.PrivacyField)
        {
            bool HasReflect = false;
            foreach (PgCombatEffect Item in extractedCombatEffectList)
                if (Item.Keyword == CombatKeyword.ReflectOnMelee)
                    HasReflect = true;

            if (!HasReflect)
                extractedCombatEffectList.Add(new PgCombatEffect() { Keyword = CombatKeyword.ReflectOnMelee, Data = new PgNumericValue() { RawIsPercent = false } });
        }

        // Hack for CloudTrick.
        if (extractedAbilityList.Count >= 1 && extractedAbilityList[0] == AbilityKeyword.CloudTrick)
            extractedCombatEffectList.Add(new PgCombatEffect() { Keyword = CombatKeyword.WhenTeleporting, Data = new PgNumericValue() });

        CombatKeyword MitigationKeyword = CombatKeyword.Internal_None;
        GameDamageType MitigationDamageType = GameDamageType.Internal_None;

        for (int i = 0; i < extractedCombatEffectList.Count; i++)
        {
            PgCombatEffect CombatEffect = extractedCombatEffectList[i];
            if (CombatEffect.Keyword == CombatKeyword.AddMitigationPhysical && CombatEffect.DamageType == GameDamageType.Internal_None)
                extractedCombatEffectList[i] = new PgCombatEffect() { Keyword = CombatKeyword.AddMitigation, Data = CombatEffect.Data, DamageType = GameDamageType.Crushing | GameDamageType.Slashing | GameDamageType.Piercing, CombatSkill = CombatEffect.CombatSkill };

            bool IsMitigationKeyword = extractedCombatEffectList[i].Keyword == CombatKeyword.AddMitigation ||
                extractedCombatEffectList[i].Keyword == CombatKeyword.AddMitigationPhysical ||
                extractedCombatEffectList[i].Keyword == CombatKeyword.AddMitigationIndirect ||
                extractedCombatEffectList[i].Keyword == CombatKeyword.AddMitigationDirect;

            if (IsMitigationKeyword)
            {
                if (MitigationKeyword == CombatKeyword.Internal_None)
                {
                    MitigationKeyword = extractedCombatEffectList[i].Keyword;
                    MitigationDamageType = extractedCombatEffectList[i].DamageType;
                }
                else
                {
                    extractedCombatEffectList[i].Keyword = MitigationKeyword;
                    extractedCombatEffectList[i].DamageType = MitigationDamageType;
                }
            }
        }

        // Merge duplicate effects
        bool IsMerged;
        do
        {
            IsMerged = false;

            for (int i = 0; i + 1 < extractedCombatEffectList.Count; i++)
            {
                if (extractedCombatEffectList[i].Keyword == extractedCombatEffectList[i + 1].Keyword && extractedCombatEffectList[i].CombatSkill == extractedCombatEffectList[i + 1].CombatSkill)
                {
                    if ((extractedCombatEffectList[i].DamageType & extractedCombatEffectList[i + 1].DamageType) == 0)
                    {
                        if (extractedCombatEffectList[i].Data.RawValue is null && extractedCombatEffectList[i + 1].Data.RawValue is not null)
                        {
                            extractedCombatEffectList[i + 1].DamageType |= extractedCombatEffectList[i].DamageType;
                            extractedCombatEffectList.RemoveAt(i);
                            IsMerged = true;
                            break;
                        }
                    }
                }

                if (extractedCombatEffectList[i].Keyword == CombatKeyword.TargetSubsequentRageAttacks && extractedCombatEffectList[i + 1].Keyword == CombatKeyword.DamageBoost)
                {
                    if (extractedCombatEffectList[i].Data.RawValue is not null && extractedCombatEffectList[i + 1].Data.RawValue is null)
                    {
                        extractedCombatEffectList[i + 1].Data.RawValue = extractedCombatEffectList[i].Data.RawValue;
                        extractedCombatEffectList[i].Data.RawValue = null;
                        IsMerged = true;
                        break;
                    }
                }
            }
        }
        while (IsMerged);
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

    private bool ExtractKnownAttribute(List<CombatKeyword> skippedKeywordList, ref string text, out List<PgCombatEffect> extractedCombatEffectList)
    {
        List<CombatKeyword> ExtractedKeywordList = new List<CombatKeyword>();
        PgNumericValue Data1 = new PgNumericValue();
        GameDamageType DamageType = GameDamageType.Internal_None;
        GameCombatSkill CombatSkill = GameCombatSkill.Internal_None;
        int ParsedIndex = -1;
        string ModifiedText = text;
        Sentence? SelectedSentence = null;

        if (text.Contains("When you trigger"))
        {
        }

        foreach (Sentence Item in SentenceList)
            ExtractSentence(Item, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref DamageType, ref CombatSkill, ref ParsedIndex, ref SelectedSentence);

        extractedCombatEffectList = new List<PgCombatEffect>();

        if (ExtractedKeywordList.Count > 0)
        {
            text = ModifiedText;
            bool IsDataSaved = false;

            foreach (CombatKeyword Item in ExtractedKeywordList)
            {
                if (Item == CombatKeyword.Ignore)
                    continue;

                PgCombatEffect ExtractedCombatEffect;
                if (!IsDataSaved)
                {
                    if (Item == CombatKeyword.But)
                        ExtractedCombatEffect = new PgCombatEffect() { Keyword = Item, Data = new PgNumericValue() { RawIsPercent = false } };
                    else
                    {
                        ExtractedCombatEffect = new PgCombatEffect() { Keyword = Item, Data = Data1, DamageType = DamageType, CombatSkill = CombatSkill };
                        IsDataSaved = true;
                    }
                }
                else
                    ExtractedCombatEffect = new PgCombatEffect() { Keyword = Item, Data = new PgNumericValue() { RawIsPercent = false } };

                extractedCombatEffectList.Add(ExtractedCombatEffect);
            }

            Debug.Assert(SelectedSentence != null);
            SelectedSentence?.SetUsed();

            return true;
        }
        else
            return false;
    }

    private void ExtractSentence(Sentence sentence, List<CombatKeyword> skippedKeywordList, string text, ref string modifiedText, List<CombatKeyword> extractedKeywordList, ref PgNumericValue data1, ref GameDamageType damageType, ref GameCombatSkill combatSkill, ref int parsedIndex, ref Sentence? selectedSentence)
    {
        if (sentence.Format == "Restore %f health (or Armor)")
        {
        }

        string NewText = text;
        List<CombatKeyword> NewExtractedKeywordList = new List<CombatKeyword>();
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

    private bool ExtractNewSentence(Sentence sentence, List<CombatKeyword> skippedKeywordList, ref string text, List<CombatKeyword> extractedKeywordList, ref PgNumericValue data1, ref GameDamageType damageType, ref GameCombatSkill combatSkill, ref int parsedIndex)
    {
        string format = sentence.Format;
        List<CombatKeyword> associatedKeywordList = sentence.AssociatedKeywordList;
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
            int PatternIndex = FindPattern(LowerText, LowerFormat, 0, out GameDamageType ParsedDamageType, out GameCombatSkill ParsedCombatSkill, out int FormatLength);
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

    private bool ParseFormat(int startIndex, string beforePattern, string afterPattern, string lowerText, List<CombatKeyword> associatedKeywordList, SignInterpretation signInterpretation, ref string text, List<CombatKeyword> extractedKeywordList, ref PgNumericValue data1, ref GameDamageType damageType, ref GameCombatSkill combatSkill, out int parsedLength)
    {
        int PatternIndex;
        int AfterPatternIndex;
        GameDamageType DamageTypeBefore;
        GameCombatSkill CombatSkillBefore;
        int PatternLengthBefore;
        GameDamageType DamageTypeAfter;
        GameCombatSkill CombatSkillAfter;
        int PatternLengthAfter;

        if (beforePattern.Length > 0)
        {
            PatternIndex = FindPattern(lowerText, beforePattern, startIndex, out DamageTypeBefore, out CombatSkillBefore, out PatternLengthBefore);
            DamageTypeAfter = GameDamageType.Internal_None;
            CombatSkillAfter = GameCombatSkill.Internal_None;
            PatternLengthAfter = 0;
        }
        else
        {
            DamageTypeBefore = GameDamageType.Internal_None;
            CombatSkillBefore = GameCombatSkill.Internal_None;
            PatternLengthBefore = 0;

            AfterPatternIndex = FindPattern(lowerText, afterPattern, startIndex, out DamageTypeAfter, out CombatSkillAfter, out PatternLengthAfter);
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

        AfterPatternIndex = FindPattern(lowerText, afterPattern, EndDataIndex, out DamageTypeAfter, out CombatSkillAfter, out PatternLengthAfter);
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

    public static PgNumericValue NumericValueParse(string text)
    {
        if (text.EndsWith("%"))
            return NumericValueFromDoublePercent(float.Parse(text.Substring(0, text.Length - 1), NumberStyles.Float, CultureInfo.InvariantCulture));
        else
            return NumericValueFromDouble(float.Parse(text, NumberStyles.Float, CultureInfo.InvariantCulture));
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

    private int FindPattern(string text, string pattern, int startIndex, out GameDamageType damageType, out GameCombatSkill combatSkill, out int patternLength)
    {
        damageType = GameDamageType.Internal_None;
        combatSkill = GameCombatSkill.Internal_None;

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
            int Value;

            switch (FoundTextMap.Count)
            {
                case 3:
                    Value = 0;
                    for (int i = 0; i < 3; i++)
                        Value |= FoundTextKey[i];

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
                    Value = 0;
                    for (int i = 0; i < 2; i++)
                        Value |= FoundTextKey[i];

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

                default:
                case 1:
                    Value = FoundTextKey[0];
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
    #endregion

    #region Data Analysis, Remaining Powers
    private void AnalyzeRemainingPowers(List<string> abilityNameList, Dictionary<string, List<AbilityKeyword>> nameToKeyword, List<PgPower> powerSimpleEffectList, List<PgEffect> unmatchedEffectList, Dictionary<PgPower, List<PgEffect>> candidateEffectTable, List<string[]> stringKeyTable, List<PgModEffect[]> powerKeyToCompleteEffectTable)
    {
        int DebugIndex = 0;
        int SkipIndex = 0;

        foreach (PgPower ItemPower in powerSimpleEffectList)
        {
            DebugIndex++;

            if (SkipIndex > 0)
            {
                SkipIndex--;
                continue;
            }

            if (ItemPower.Key == "23205")
            {
            }

            // Debug.WriteLine("");
            // Debug.WriteLine($"Debug Index: {DebugIndex - 1} / {powerSimpleEffectList.Count} (Remaining)");
            AnalyzeRemainingPowers(abilityNameList, nameToKeyword, ItemPower, unmatchedEffectList, candidateEffectTable, out string[] stringKeyArray, out PgModEffect[] ModEffectArray);

            stringKeyTable.Add(stringKeyArray);
            powerKeyToCompleteEffectTable.Add(ModEffectArray);
        }
    }

    private void AnalyzeRemainingPowers(List<string> abilityNameList, Dictionary<string, List<AbilityKeyword>> nameToKeyword, PgPower itemPower, List<PgEffect> unmatchedEffectList, Dictionary<PgPower, List<PgEffect>> candidateEffectTable, out string[] stringKeyArray, out PgModEffect[] modEffectArray)
    {
        PgPowerTierCollection TierList = itemPower.TierList;

        Debug.Assert(TierList.Count > 0);

        int ValidationIndex = 0;
        if (TierList.Count >= 2)
            ValidationIndex = 1;
        if (TierList.Count >= 4)
            ValidationIndex = 2;

        List<CombatKeyword>[] PowerTierKeywordListArray = new List<CombatKeyword>[TierList.Count];
        stringKeyArray = new string[TierList.Count];
        modEffectArray = new PgModEffect[TierList.Count];

        int LastTierIndex = TierList.Count - 1;
        PgPowerTier LastTier = TierList[LastTierIndex];

        List<PgEffect>? CandidateEffectList = candidateEffectTable.ContainsKey(itemPower) ? candidateEffectTable[itemPower] : null;
        AnalyzeRemainingPowers(abilityNameList, nameToKeyword, LastTier, unmatchedEffectList, CandidateEffectList, out List<CombatKeyword> ExtractedPowerTierKeywordList, out PgModEffect ExtractedModEffect, true);
        PowerTierKeywordListArray[LastTierIndex] = ExtractedPowerTierKeywordList;
        modEffectArray[LastTierIndex] = ExtractedModEffect;

        if (ExtractedModEffect.AbilityList.Count > 0 && ExtractedModEffect.TargetAbilityList.Count > 0)
        {
            // Debug.WriteLine($"{ExtractedModEffect}");
        }

        for (int i = 0; i + 1 < TierList.Count; i++)
        {
            AnalyzeRemainingPowers(abilityNameList, nameToKeyword, TierList[i], null, CandidateEffectList, out List<CombatKeyword> ComparedPowerTierKeywordList, out PgModEffect ParsedModEffect, false);

            bool AllowIncomplete = i < ValidationIndex;

            if (!IsSameCombatKeywordList(ExtractedPowerTierKeywordList, ComparedPowerTierKeywordList, AllowIncomplete))
            {
                Debug.WriteLine($"Mismatching power at tier #{i}");
                return;
            }

            PowerTierKeywordListArray[i] = ComparedPowerTierKeywordList;
            modEffectArray[i] = ParsedModEffect;
        }

        for (int i = 0; i < TierList.Count; i++)
        {
            string Key = PowerEffectPairKey(itemPower, i);
            stringKeyArray[i] = Key;
        }
    }

    private bool AnalyzeRemainingPowers(List<string> abilityNameList, Dictionary<string, List<AbilityKeyword>> nameToKeyword, PgPowerTier powerTier, List<PgEffect>? unmatchedEffectList, List<PgEffect>? candidateEffectList, out List<CombatKeyword> extractedPowerTierKeywordList, out PgModEffect modEffect, bool displayAnalysisResult)
    {
        IList<PgPowerEffect> EffectList = powerTier.EffectList;
        Debug.Assert(EffectList.Count == 1);
        PgPowerEffectSimple powerSimpleEffect = (PgPowerEffectSimple)EffectList[0];

        string ModText = powerSimpleEffect.Description;

        HackModText(ref ModText);

        AnalyzeText(abilityNameList, nameToKeyword, ModText, true, out List<AbilityKeyword> ModAbilityList, out PgCombatEffectCollection ModCombatList, out List<AbilityKeyword> ModTargetAbilityList);

        List<PgCombatEffectCollection> EffectCombatList = new List<PgCombatEffectCollection>();
        if (candidateEffectList != null)
        {
            foreach (PgEffect CandidateEffect in candidateEffectList)
            {
                string EffectText = CandidateEffect.Description;
                HackModText(ref EffectText);

                AnalyzeText(abilityNameList, nameToKeyword, EffectText, false, out _, out PgCombatEffectCollection EffectCombatCollection, out _);
                EffectCombatList.Add(EffectCombatCollection);
            }
        }
        else
            EffectCombatList = new List<PgCombatEffectCollection>();

        string EffectKey = string.Empty;

        if (EffectCombatList.Count > 0 && CombatEffectContains(ModCombatList, EffectCombatList, out int CandidateIndex, out _, out _) && candidateEffectList != null)
        {
            PgEffect CandidateEffect = candidateEffectList[CandidateIndex];
            EffectKey = CandidateEffect.Key;

            if (unmatchedEffectList != null)
                unmatchedEffectList.Remove(CandidateEffect);
        }

        string ParsedAbilityList = AbilityKeywordListToShortString(ModAbilityList);
        string ParsedPowerString = CombatEffectListToString(ModCombatList, out extractedPowerTierKeywordList);
        string ParsedModTargetAbilityList = AbilityKeywordListToShortString(ModTargetAbilityList);

        if (displayAnalysisResult)
        {
            /*Debug.WriteLine("");
            Debug.WriteLine($"    Power: {powerSimpleEffect.Description}");
            Debug.WriteLine($"Parsed as: {{{ParsedAbilityList}}} {ParsedPowerString}, Target: {ParsedModTargetAbilityList}");*/
        }

        modEffect = new PgModEffect()
        {
            EffectKey = EffectKey,
            AbilityList = ModAbilityList,
            StaticCombatEffectList = ModCombatList,
            TargetAbilityList = ModTargetAbilityList,
        };

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
        else
        {
            if (ModAbilityList.Count > 0 && ModTargetAbilityList.Count > 0)
            {
                AbilityKeyword Ability = ModAbilityList[0];
                AbilityKeyword TargetAbility = ModTargetAbilityList[0];

                if (Ability != TargetAbility)
                {
                    if (ModCombatList.Count >= 2)
                    {
                        PgCombatEffect FirstCombatEffect = ModCombatList[0];
                        PgCombatEffect SecondCombatEffect = ModCombatList[1];

                        if (FirstCombatEffect.Keyword != CombatKeyword.ReflectOnAnyAttack && FirstCombatEffect.Keyword != CombatKeyword.ApplyWithChance && SecondCombatEffect.Keyword != CombatKeyword.ResetOtherAbilityTimer && SecondCombatEffect.Keyword != CombatKeyword.EffectDuration)
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
                                    AbilityList = new List<AbilityKeyword>() { TargetAbility },
                                    StaticCombatEffectList = new PgCombatEffectCollection() { SecondCombatEffect },
                                    DynamicCombatEffectList = new PgCombatEffectCollection(),
                                    TargetAbilityList = new List<AbilityKeyword>(),
                                };

                                modEffect.StaticCombatEffectList.Remove(SecondCombatEffect);
                                modEffect.SecondaryModEffect = SecondaryModEffect;
                                modEffect.TargetAbilityList.Clear();
                            }
                        }
                    }
                }
            }
        }

        VerifyStaticEffects(ModAbilityList, ModCombatList);

        return true;
    }

    private void HackModText(ref string modText)
    {
        modText = modText.Replace("Indirect Poison and Indirect Trauma damage", "Indirect Poison and Trauma damage");
        modText = modText.Replace("Indirect Nature and Indirect Electricity damage", "Indirect Nature and Electricity damage");
        modText = modText.Replace(", but the ability's range is reduced to 12m", ", but range is reduced 18 meter");
        modText = modText.Replace("When you teleport via Shadow Feint", "When you teleport");
        modText = modText.Replace("and Paradox Trot boosts Sprint Speed +1", string.Empty);

        if (!modText.Contains("But I Love You"))
            ReplaceCaseInsensitive(ref modText, " but ", " b*u*t ");
    }
    #endregion

    #region Data Analysis, Remaining Effects
    private void AnalyzeRemainingEffects(List<string> abilityNameList, Dictionary<string, List<AbilityKeyword>> nameToKeyword, List<PgEffect> unmatchedEffectList, List<PgModEffect[]> powerKeyToCompleteEffectTable, out List<string> effectKeyList)
    {
        effectKeyList = new List<string>();

        foreach (PgEffect Item in unmatchedEffectList)
        {
            if (!IsLongDurationBuff(Item))
                continue;

            string Description = Item.Description.Replace("\n", "\n-> ");
            AnalyzeRemainingEffects(abilityNameList, nameToKeyword, Item, Description, out List<AbilityKeyword> ExtractedAbilityList, out PgCombatEffectCollection ExtractedCombatEffectList, out bool isSelected);

            if (isSelected)
            {
                PgModEffect NewModEffect = new PgModEffect() { EffectKey = Item.Key, AbilityList = ExtractedAbilityList, StaticCombatEffectList = ExtractedCombatEffectList };
                powerKeyToCompleteEffectTable.Add(new PgModEffect[] { NewModEffect });
                effectKeyList.Add(Item.Key);

                string ParsedEffectString = CombatEffectListToString(ExtractedCombatEffectList, out _);
                string ParsedEffectAbilityList = AbilityKeywordListToShortString(ExtractedAbilityList);

                string BuffKeywordString = string.Empty;
                foreach (EffectKeyword Keyword in Item.KeywordList)
                    if (Keyword != EffectKeyword.Buff)
                    {
                        if (BuffKeywordString.Length > 0)
                            BuffKeywordString += ", ";

                        BuffKeywordString += Keyword.ToString();
                    }

                /*
                Debug.WriteLine("");
                Debug.WriteLine($"   Effect: {Description}");
                Debug.WriteLine($"Parsed as: {ParsedEffectString}, Target: {ParsedEffectAbilityList}");
                Debug.WriteLine($"           {BuffKeywordString}");*/
            }
        }
    }

    private bool IsLongDurationBuff(PgEffect effect)
    {
        if (effect.Duration <= 30)
            return false;

        if (!effect.KeywordList.Contains(EffectKeyword.Buff))
            return false;

        if (effect.DisplayMode != EffectDisplayMode.Effect)
            return false;

        return true;
    }

    private void AnalyzeRemainingEffects(List<string> abilityNameList, Dictionary<string, List<AbilityKeyword>> nameToKeyword, PgEffect effect, string description, out List<AbilityKeyword> extractedTargetAbilityList, out PgCombatEffectCollection extractedCombatEffectList, out bool isSelected)
    {
        string ModText = string.Empty;
        string EffectText = effect.Description;

        HackModAndEffectText(ref ModText, ref EffectText);

        string Text = EffectText;

        if (Text == "Poison attacks deal +5 damage")
        {
        }

        RemoveDecorationText(ref Text);
        SimplifyGrammar(ref Text);
        SimplifyRandom(ref Text);

        ExtractAbilityList(abilityNameList, nameToKeyword, false, ref Text, out extractedTargetAbilityList, out int TargetRemoveCount);

        List<AbilityKeyword> ExtractedAbilityList = new List<AbilityKeyword>();
        ExtractAttributesFull(Text, ExtractedAbilityList, out extractedCombatEffectList);

        isSelected = false;
        bool IsEffectBoost = false;
        bool IsEffectDebuff = false;

        foreach (PgCombatEffect Item in extractedCombatEffectList)
        {
            CombatKeyword Keyword = Item.Keyword;

            switch (Keyword)
            {
                case CombatKeyword.DamageBoost:
                case CombatKeyword.BaseDamageBoost:
                case CombatKeyword.DealIndirectDamage:
                case CombatKeyword.ComboFinalStepDamage:
                case CombatKeyword.DealArmorDamage:
                case CombatKeyword.AddTaunt:
                case CombatKeyword.AddAccuracy:
                case CombatKeyword.AddMeleeAccuracy:
                case CombatKeyword.AddBurstAccuracy:
                    isSelected = true;
                    IsEffectBoost = true;
                    break;
                case CombatKeyword.AddPowerCost:
                    isSelected = true;
                    if (Item.Data.RawValue.HasValue && Item.Data.Value > 0)
                        IsEffectDebuff = true;
                    break;
            }
        }

        if (!isSelected)
            return;

        if (IsEffectBoost)
        {
            if (!IsGoodCombatEffect(description, extractedCombatEffectList))
            {
                isSelected = false;
            }
        }
        else if (IsEffectDebuff)
        {
            isSelected = false;
        }
    }

    private bool IsGoodCombatEffect(string description, List<PgCombatEffect> combatEffectList)
    {
        bool IsGood = false;
        bool IsBad = false;
        bool IsIgnored = false;
        List<PgCombatEffect> EffectiveCombatEffectList = new List<PgCombatEffect>(combatEffectList);
        List<CombatKeyword> IgnoreKeywordList = new List<CombatKeyword>()
        {
            CombatKeyword.ApplyWithChance,
            CombatKeyword.AddSprintSpeed,
            CombatKeyword.EffectDuration,
            CombatKeyword.EffectDurationMinute,
            CombatKeyword.AddMitigation,
            CombatKeyword.RestoreHealth,
            CombatKeyword.AddPowerCost,
            CombatKeyword.RandomDamage,
            CombatKeyword.WithinDistance,
            CombatKeyword.ComboFinalStepBurst,
            CombatKeyword.AboveRage,
            CombatKeyword.TargetFishAndSnail,
            CombatKeyword.ApplyToCrits,
            CombatKeyword.AddEvasionMelee,
            CombatKeyword.MeleeAttack,
        };

        bool IsRemoved;
        do
        {
            IsRemoved = false;

            foreach (PgCombatEffect Item in EffectiveCombatEffectList)
                if (IgnoreKeywordList.Contains(Item.Keyword))
                {
                    EffectiveCombatEffectList.Remove(Item);
                    Debug.Assert(EffectiveCombatEffectList.Count > 0);
                    IsRemoved = true;
                    break;
                }
        }
        while (IsRemoved);

        int DamageKeywordCount = 0;
        foreach (PgCombatEffect Item in EffectiveCombatEffectList)
        {
            CombatKeyword Keyword = Item.Keyword;

            switch (Keyword)
            {
                case CombatKeyword.ReflectOnAnyAttack:
                case CombatKeyword.NextAttack:
                case CombatKeyword.EffectRecurrence:
                case CombatKeyword.TargetSelf:
                case CombatKeyword.ReflectOnMelee:
                    IsIgnored = true;
                    break;
                case CombatKeyword.DamageBoost:
                case CombatKeyword.BaseDamageBoost:
                case CombatKeyword.DealIndirectDamage:
                case CombatKeyword.ComboFinalStepDamage:
                case CombatKeyword.DealArmorDamage:
                case CombatKeyword.AddTaunt:
                case CombatKeyword.AddAccuracy:
                case CombatKeyword.AddMeleeAccuracy:
                case CombatKeyword.AddBurstAccuracy:
                    DamageKeywordCount++;
                    break;
            }
        }

        if (IsIgnored)
            return false;

        if (IsGood && IsBad)
        {
            string ParsedEffectString0 = CombatEffectListToString(combatEffectList, out _);
            Debug.WriteLine($"Text: {description}");
            Debug.WriteLine($"WARNING both good and bad effect: {ParsedEffectString0}");
            return false;
        }

        if (IsGood)
            return true;

        if (IsBad)
            return false;

        if (DamageKeywordCount > 0 && EffectiveCombatEffectList.Count == DamageKeywordCount)
        {
            foreach (PgCombatEffect Item in EffectiveCombatEffectList)
            {
                PgNumericValue Data = Item.Data;
                if (Data.RawValue.HasValue)
                {
                    if (Data.Value <= 0)
                        return false;
                }
            }

            return true;
        }

        string ParsedEffectString1 = CombatEffectListToString(combatEffectList, out _);
        Debug.WriteLine($"Text: {description}");
        Debug.WriteLine($"WARNING: unknown effect: {ParsedEffectString1}");
        return false;
    }
    #endregion

    #region Tables
    public static readonly Dictionary<int, string> DamageTypeTextMap = new Dictionary<int, string>()
    {
        { (int)GameDamageType.Internal_None, string.Empty },
        { (int)GameDamageType.Crushing, "Crushing" },
        { (int)GameDamageType.Slashing, "Slashing" },
        { (int)GameDamageType.Nature, "Nature" },
        { (int)GameDamageType.Fire, "Fire" },
        { (int)GameDamageType.Cold, "Cold" },
        { (int)GameDamageType.Piercing, "Piercing" },
        { (int)GameDamageType.Psychic, "Psychic" },
        { (int)GameDamageType.Trauma, "Trauma" },
        { (int)GameDamageType.Electricity, "Electricity" },
        { (int)GameDamageType.Poison, "Poison" },
        { (int)GameDamageType.Acid, "Acid" },
        { (int)GameDamageType.Darkness, "Darkness" },
    };

    public static readonly Dictionary<int, string> SkillTextMap = new Dictionary<int, string>()
    {
        { (int)GameCombatSkill.Internal_None, string.Empty },
        { (int)GameCombatSkill.Sword, "Sword" },
        { (int)GameCombatSkill.FireMagic, "Fire Magic" },
        { (int)GameCombatSkill.Unarmed, "Unarmed" },
        { (int)GameCombatSkill.Psychology, "Psychology" },
        { (int)GameCombatSkill.Staff, "Staff" },
        { (int)GameCombatSkill.Mentalism, "Mentalism" },
        { (int)GameCombatSkill.Archery, "Archery" },
        { (int)GameCombatSkill.Shield, "Shield" },
        { (int)GameCombatSkill.AnimalHandling, "Animal Handling" },
        { (int)GameCombatSkill.Knife, "Knife" },
        { (int)GameCombatSkill.Cow, "Cow" },
        { (int)GameCombatSkill.Deer, "Deer" },
        { (int)GameCombatSkill.Pig, "Pig" },
        { (int)GameCombatSkill.Spider, "Spider" },
        { (int)GameCombatSkill.Werewolf, "Werewolf" },
        { (int)GameCombatSkill.BattleChemistry, "Battle Chemistry" },
        { (int)GameCombatSkill.Necromancy, "Necromancy" },
        { (int)GameCombatSkill.Hammer, "Hammer" },
        { (int)GameCombatSkill.Druid, "Druid" },
        { (int)GameCombatSkill.IceMagic, "Ice Magic" },
        { (int)GameCombatSkill.GiantBat, "Giant Bat" },

        // { (int)GameCombatSkill.Axe, "Axe" },
        { (int)GameCombatSkill.Bard, "Bard" },
        { (int)GameCombatSkill.Rabbit, "Rabbit" },
        { (int)GameCombatSkill.Priest, "Priest" },
        { (int)GameCombatSkill.Warden, "Warden" },

        // { (int)GameCombatSkill.FairyMagic, "Fairy Magic" },
        { (int)GameCombatSkill.Lycanthropy, "Lycanthropy" },
        { (int)GameCombatSkill.SpiritFox, "Spirit Fox" },
    };

    private static List<Sentence> SentenceList = new List<Sentence>()
    {
        new Sentence("Place an extra trap", CombatKeyword.AnotherTrap),
        new Sentence("Target's Critical Hit Chance reduced by %f", CombatKeyword.ReduceCriticalChance),
        new Sentence("Chance to critically-hit is reduced by %f", CombatKeyword.ReduceCriticalChance),
        new Sentence("Mend a broken bone", CombatKeyword.MendBrokenBone),
        new Sentence("Turn while leaping", CombatKeyword.FreeMovementLeaping),
        new Sentence("Free-form movement while leaping", CombatKeyword.FreeMovementLeaping),
        new Sentence("Repeated castings on same target", CombatKeyword.RepeatedCasting),
        new Sentence("Exhilarates on the same target", CombatKeyword.RepeatedCasting),
        new Sentence("Boost Jump Height", CombatKeyword.BoostJumpHeight),
        new Sentence("Shuffling their hatred", CombatKeyword.ShuffleTaunt),
        new Sentence("Target ignore you", CombatKeyword.Ignored),
        new Sentence("Cause the target to ignore you", CombatKeyword.Ignored),
        new Sentence("Uniformly diminishes all targets' entire aggro lists by %f", CombatKeyword.ChangeTaunt),
        new Sentence("This absorbed damage is added to your next @ attack at a %f rate", CombatKeyword.ReturnDamage),
        new Sentence("This absorbed damage is added to your next @", CombatKeyword.ReturnDamage),
        new Sentence("Boost your next attack %f", new List<CombatKeyword>() { CombatKeyword.DamageBoost, CombatKeyword.NextAttack }),
        new Sentence("Future @ attack damage %f", new List<CombatKeyword>() { CombatKeyword.DamageBoost, CombatKeyword.NextAttack }),
        new Sentence("Boost the damage of @ by %f", new List<CombatKeyword>() { CombatKeyword.DamageBoost }),
        new Sentence("Boost the damage of future @ attack by %f", new List<CombatKeyword>() { CombatKeyword.DamageBoost, CombatKeyword.NextAttack }),
        new Sentence("Until %f damage is mitigated", CombatKeyword.MitigationLimit),
        new Sentence("Until it has absorbed a total of %f damage", CombatKeyword.MitigationLimit),
        new Sentence("Until it has absorbed %f total damage", CombatKeyword.MitigationLimit),
        new Sentence("Up to a maximum of %f total mitigated damage", CombatKeyword.MitigationLimit),
        new Sentence("(Randomly determined for each attack)", CombatKeyword.RandomDamage),
        new Sentence("(Randomly determined)", CombatKeyword.RandomDamage),
        new Sentence("(Random)", CombatKeyword.RandomDamage),
        new Sentence("If it is a #D attack", CombatKeyword.IfDamageType),
        new Sentence("If it deal #D damage", CombatKeyword.IfDamageType),
        new Sentence("Gain %f Direct", CombatKeyword.DamageBoost),
        new Sentence("#D damage for your next attack", new List<CombatKeyword>() { CombatKeyword.IfDamageType, CombatKeyword.NextAttack }),
        //new Sentence("Briefly terrifies the target", CombatKeyword.Fear),
        new Sentence("Cause all sentient targets to flee in terror", CombatKeyword.FearSentient),
        new Sentence("Trigger the target's Vulnerability", CombatKeyword.SetVulnerable),
        new Sentence("To Arthropods", CombatKeyword.TargetAnatomyArthropods),
        new Sentence("To Undead", CombatKeyword.TargetUndead),
        new Sentence("On an undead target", CombatKeyword.TargetUndead),
        new Sentence("Vs Undead", CombatKeyword.TargetUndead),
        new Sentence("To Aberration", CombatKeyword.TargetAnatomyAbberation),
        new Sentence("Vs Elite", CombatKeyword.TargetElite),
        new Sentence("Vs Fish-anatomy creatures", CombatKeyword.TargetFishAndSnail),
        new Sentence("To non-Elite target", CombatKeyword.TargetNotElite),
        new Sentence("To non-Elite enemies", CombatKeyword.TargetNotElite),
        new Sentence("To Vulnerable target", CombatKeyword.TargetVulnerable),
        new Sentence("To sentient creatures", CombatKeyword.TargetSentient),
        new Sentence("If the target is Vulnerable", CombatKeyword.TargetVulnerable),
        new Sentence("If target is Vulnerable", CombatKeyword.TargetVulnerable),
        new Sentence("Is used on a Vulnerable target", CombatKeyword.TargetVulnerable),
        new Sentence("Both you and your pet", CombatKeyword.ApplyToPetAndMaster),
        new Sentence("Your pet's", CombatKeyword.ApplyToPet),
        new Sentence("Your pet gain", CombatKeyword.ApplyToPet),
        new Sentence("Give your pet", CombatKeyword.ApplyToPet),
        new Sentence("Grant your pet", CombatKeyword.ApplyToPet),
        new Sentence("To your pet", CombatKeyword.ApplyToPet),
        new Sentence("To your undead", CombatKeyword.ApplyToPet),
        new Sentence("To the same target", CombatKeyword.SameTarget),
        new Sentence("To you and your allies", CombatKeyword.ApplyToAllies),
        new Sentence("Affects caster as well as allies", CombatKeyword.ApplyToAllies),
        new Sentence("To YOU", CombatKeyword.TargetSelf),
        new Sentence("If target is covered", CombatKeyword.TargetUnderEffect),
        new Sentence("To targets that are covered", CombatKeyword.TargetUnderEffect),
        new Sentence("To targets that are Knock Down", CombatKeyword.TargetKnockedDown),
        new Sentence("Deal #D damage", CombatKeyword.ChangeDamageType),
        new Sentence("Instead of #D", CombatKeyword.Ignore),
        new Sentence("Next attack", CombatKeyword.NextAttack),
        new Sentence("Next ability", CombatKeyword.NextAttack),
        new Sentence("For one attack", CombatKeyword.NextAttack),
        new Sentence("Target's next Rage Attack", CombatKeyword.TargetNextRageAttack),
        new Sentence("The next time they use a Rage attack", CombatKeyword.TargetNextRageAttack),
        new Sentence("%f of target's attack miss and have no effect", CombatKeyword.AddAccuracy, SignInterpretation.Opposite),
        new Sentence("%f of their attack miss and have no effect", CombatKeyword.AddAccuracy, SignInterpretation.Opposite),
        new Sentence("Target's attack", CombatKeyword.TargetSubsequentAttacks),
        new Sentence("Target's rage attack", CombatKeyword.TargetSubsequentRageAttacks),
        new Sentence("Ignore Knockback effect", CombatKeyword.IgnoreKnockback),
        new Sentence("Immunity to Knockback", CombatKeyword.IgnoreKnockback),
        new Sentence("Grant all targets immunity to Knockback", CombatKeyword.IgnoreKnockback),
        new Sentence("Grant Knockback Immunity", CombatKeyword.IgnoreKnockback),
        new Sentence("Immune to Knockback effects", CombatKeyword.IgnoreKnockback),
        new Sentence("Target take %f indirect #D damage", CombatKeyword.AddIndirectVulnerability),
        new Sentence("Indirect #D damage is %f per tick", CombatKeyword.AddIndirectVulnerability),
        new Sentence("Cause the target to take %f damage from indirect #D", CombatKeyword.AddIndirectVulnerability),
        new Sentence("Cause the target to take %f more damage from #D", CombatKeyword.AddVulnerability),
        new Sentence("Target is %f more vulnerable to #D damage", CombatKeyword.AddVulnerability),
        new Sentence("Target %f more vulnerable to #D damage", CombatKeyword.AddVulnerability),
        new Sentence("Make the target %f more vulnerable to #D", CombatKeyword.AddVulnerability),
        new Sentence("Make the target %f more vulnerable to direct #D", CombatKeyword.AddVulnerability),
        new Sentence("Cause the target to become %f more vulnerable to #D attack", CombatKeyword.AddVulnerability),
        // new Sentence("Increase the damage target take from #D by %f", CombatKeyword.AddVulnerability),
        new Sentence("Target take %f more damage from #D", CombatKeyword.AddVulnerability),
        new Sentence("Target take %f #D damage", CombatKeyword.AddVulnerability),
        // new Sentence("Take %f direct #D damage", CombatKeyword.AddDirectVulnerability),
        new Sentence("Take %f damage from #D attack", CombatKeyword.AddVulnerability),
        new Sentence("#D Vulnerability %f", CombatKeyword.AddVulnerability),
        new Sentence("%f more damage from any #D attack", CombatKeyword.AddVulnerability),
        new Sentence("Target's #D Vulnerability", CombatKeyword.AddVulnerability),
        new Sentence("Target's vulnerability to #D %f", CombatKeyword.AddVulnerability),

        // new Sentence("Take %f damage from both #D", CombatKeyword.AddVulnerability),
        //new Sentence("Target take %f #D damage from future attack", CombatKeyword.AddVulnerability),
        new Sentence("B*u*t take %f damage from any #D attack", new List<CombatKeyword>() { CombatKeyword.But, CombatKeyword.AddVulnerability }),
        new Sentence("#D Vulnerability +infinity", CombatKeyword.DestroyedByDamageType),
        new Sentence("Instantly destroyed by ANY #D Damage", CombatKeyword.DestroyedByDamageType),
        new Sentence("Targets suffer %f damage from other #D attack", CombatKeyword.AddVulnerability),
        new Sentence("Cause the target to suffer %f #D damage", CombatKeyword.AddVulnerability),
        new Sentence("Cause target to be %f more vulnerable to #D damage", CombatKeyword.AddVulnerability),
        new Sentence("Deal %f damage to Health and Armor", CombatKeyword.DamageBoostToHealthAndArmor),
        new Sentence("Increase the damage of all targets' attack %f", new List<CombatKeyword>() { CombatKeyword.DamageBoost, CombatKeyword.ApplyToAllies }),
        new Sentence("%f damage from all attack", new List<CombatKeyword>() { CombatKeyword.DamageBoost, CombatKeyword.ApplyToAllies }),
        new Sentence("Turning half of that into Trauma damage", CombatKeyword.ExtraTraumaDamage),
        new Sentence("#D attack Deal %f damage", CombatKeyword.DamageBoost),
        new Sentence("Rage attack deal %f damage", CombatKeyword.AnimalPetRageAttackBoost),
        new Sentence("Pet's Rage Attack Damage %f", CombatKeyword.AnimalPetRageAttackBoost),
        new Sentence("Rage Attack Damage %f", CombatKeyword.AnimalPetRageAttackBoost),
        new Sentence("Deal up to %f damage", CombatKeyword.DamageBoost),
        //new Sentence("#D damage and #D damage", CombatKeyword.DamageBoost),
        new Sentence("Add up to %f extra damage", CombatKeyword.DamageBoost),
        new Sentence("Deal %f Armor damage", CombatKeyword.DealArmorDamage),
        new Sentence("Deal %f immediate #D damage", CombatKeyword.DamageBoost),
        new Sentence("Deal %f #D damage", CombatKeyword.DamageBoost),
        //new Sentence("Gain %f direct #S Damage", CombatKeyword.DamageBoost),
        new Sentence("Critical hit deal %f damage", new List<CombatKeyword>() { CombatKeyword.DamageBoost, CombatKeyword.ApplyToCrits }),
        new Sentence("Deal %f damage", CombatKeyword.DamageBoost),
        new Sentence("Deal %f more damage", CombatKeyword.DamageBoost),
        new Sentence("Plus %f more damage", CombatKeyword.DamageBoost),
        new Sentence("Pet take %f #D damage", CombatKeyword.PetImmolation),
        new Sentence("Pet bleed for %f #D damage", CombatKeyword.PetImmolation),
        new Sentence("Cause your pet to bleed for %f #D damage", CombatKeyword.PetImmolation),
        new Sentence("(Debuff cannot stack with itself)", CombatKeyword.NonStackingDebuff),
        new Sentence("(This effect does not stack with itself.)", CombatKeyword.NonStackingDebuff),
        //new Sentence("(This buff does not stack with itself)", CombatKeyword.NonStackingDebuff),
        new Sentence("(Stacking up to %f times)", CombatKeyword.StackingDebuffLimit),
        new Sentence("(This effect does not stack with itself)", CombatKeyword.NonStackingDebuff),
//        new Sentence("Combo: Deer Bash+Any Melee+Any Melee+Deer Kick:", CombatKeyword.Combo1),
        new Sentence("Combo: Gripjaw+Any Spider+Any Spider+Inject Venom:", CombatKeyword.Combo2),
        new Sentence("Combo: Rip+Any Melee+Any Giant Bat Attack+Tear:", CombatKeyword.Combo3),
        new Sentence("Combo: Screech+Any Giant Bat Attack+Any Melee+Virulent Bite:", CombatKeyword.Combo4),
        new Sentence("Combo: Rip+Any Melee+Any Melee+Bat Stability:", CombatKeyword.Combo5),
        new Sentence("Combo: Sonic Burst+Any Giant Bat Attack+Any Ranged Attack+Any Ranged Attack:", CombatKeyword.Combo6),
        new Sentence("Combo: Suppress+Any Melee+Any Melee+Headcracker:", CombatKeyword.Combo7),
        new Sentence("Final step hit all enemies within %f meter", CombatKeyword.ComboFinalStepBurst),
        new Sentence("Final step hit all targets within %f meter", CombatKeyword.ComboFinalStepBurst),
        new Sentence("Final step deal %f damage", CombatKeyword.ComboFinalStepDamage),
        new Sentence("Final step stun the target while dealing %f damage", CombatKeyword.ComboFinalStepDamageAndStun),
        new Sentence("Whenever you take damage from an enemy", CombatKeyword.ReflectOnAnyAttack),
        new Sentence("If you are using the #S skill", CombatKeyword.ActiveSkill),
        new Sentence("While the #S skill is active", CombatKeyword.ActiveSkill),
        new Sentence("While #S skill is active", CombatKeyword.ActiveSkill),
        new Sentence("While #S skill active", CombatKeyword.ActiveSkill),
        new Sentence("(If #S skill is active)", CombatKeyword.ActiveSkill),
        new Sentence("Gain %f #S Skill Base Damage", CombatKeyword.BaseDamageBoost),
        new Sentence("You have not been attacked in the past %f second", CombatKeyword.NotAttackedRecently),

        // new Sentence("If you have less than half of your Health remaining", CombatKeyword.LessThanHalfMaxHealth),
        new Sentence("Combat Refresh restore %f health", CombatKeyword.CombatRefreshRestoreHeatlth),
        new Sentence("Healing from Combat Refreshes %f", CombatKeyword.CombatRefreshRestoreHeatlth),
        new Sentence("Boost the target's #D damage-over-time by %f per tick", CombatKeyword.DealIndirectDamage),
        new Sentence("Take %f damage from #D", CombatKeyword.AddVulnerability),
        new Sentence("Grant you %f #D Vulnerability", CombatKeyword.AddVulnerability),

        // new Sentence("%f Direct Damage Vulnerability", CombatKeyword.AddDirectVulnerability),
        new Sentence("Boost your #S damage %f", CombatKeyword.DamageBoost),
        new Sentence("Boost your @ damage %f", CombatKeyword.DamageBoost),
        new Sentence("Boost your #D attack damage %f", CombatKeyword.DamageBoost),
        new Sentence("Boost #D attack damage %f", CombatKeyword.DamageBoost),
        new Sentence("Boost #D attack %f", CombatKeyword.DamageBoost),
        new Sentence("Boost the damage of your @ by %f", CombatKeyword.DamageBoost),
        new Sentence("Boost the damage of your @ %f", CombatKeyword.DamageBoost),
        new Sentence("Boost the damage from @ %f", CombatKeyword.DamageBoost),
        new Sentence("Boost the damage of all your attack %f", CombatKeyword.DamageBoost),
        // new Sentence("Boost damage from @ %f", CombatKeyword.DamageBoost),
        new Sentence("Boost #D damage %f", CombatKeyword.DamageBoost),
        new Sentence("Increase the damage of your next attack by %f", new List<CombatKeyword>() { CombatKeyword.DamageBoost, CombatKeyword.NextAttack }),
        new Sentence("Reduce the damage of the target's next attack by %f", new List<CombatKeyword>() { CombatKeyword.DamageBoost, CombatKeyword.NextAttack }, SignInterpretation.AlwaysNegative),
        new Sentence("Their Rage attack are further reduced by %f", new List<CombatKeyword>() { CombatKeyword.TargetSubsequentRageAttacks, CombatKeyword.DamageBoost }, SignInterpretation.AlwaysNegative),
        new Sentence("Increase the damage of your @ by %f", CombatKeyword.DamageBoost),
        new Sentence("#S Skill Base Damage %f", CombatKeyword.BaseDamageBoost),
        new Sentence("#S Base Damage by %f", CombatKeyword.BaseDamageBoost),
        new Sentence("#S Base Damage %f", CombatKeyword.BaseDamageBoost),

        // new Sentence("Your #S Base Damage is %f", CombatKeyword.BaseDamageBoost),
        new Sentence("Your #S Base Damage increase %f", CombatKeyword.BaseDamageBoost),
        new Sentence("#S Base Attack Damage %f", CombatKeyword.BaseDamageBoost),
        new Sentence("Direct #D Damage %f", CombatKeyword.DamageBoost),
        new Sentence("Universal Indirect Damage %f", CombatKeyword.DealIndirectDamage),
        new Sentence("Boost targets' indirect damage %f", CombatKeyword.DealIndirectDamage),
        new Sentence("Boost your direct and indirect #D damage %f", CombatKeyword.DamageBoost),
        new Sentence("Indirect #D Damage %f", CombatKeyword.DealIndirectDamage),
        new Sentence("Indirect #D %f per tick", CombatKeyword.DealIndirectDamage),
        new Sentence("%f indirect damage per tick", CombatKeyword.DealIndirectDamage),
        new Sentence("Direct and Indirect #D Damage %f", CombatKeyword.DamageBoost),
        new Sentence("Damage over Time %f per tick", new List<CombatKeyword>() { CombatKeyword.DamageBoost, CombatKeyword.DamageOverTime }),
        new Sentence("Damage over Time deal %f damage per tick", new List<CombatKeyword>() { CombatKeyword.DamageBoost, CombatKeyword.DamageOverTime }),
        new Sentence("The #D Damage is boosted %f", CombatKeyword.DamageBoost),
        new Sentence("Damage is boosted %f", CombatKeyword.DamageBoost),
        new Sentence("Boost the damage of @ %f", CombatKeyword.DamageBoost),
        new Sentence("Reap %f of the Health damage to you as healing", CombatKeyword.DrainHealth),
        new Sentence("Reap %f of the Armor damage done", CombatKeyword.DrainArmor),
        new Sentence("Reap %f health", CombatKeyword.DrainHealth),
        new Sentence("Melee Attackers suffer %f indirect #D damage", CombatKeyword.ReflectMeleeIndirectDamage),
        new Sentence("(up to a max of %f)", CombatKeyword.DrainArmorMax),
        new Sentence("The reap cap is %f", CombatKeyword.DrainHealthMax),
        new Sentence("Deal %f direct damage", CombatKeyword.DamageBoost),
        new Sentence("%f direct health damage", CombatKeyword.DealDirectHealthDamage),
        new Sentence("+Up to %f extra damage", new List<CombatKeyword>() { CombatKeyword.DamageBoost, CombatKeyword.RandomDamage }),
        new Sentence("%f random damage", new List<CombatKeyword>() { CombatKeyword.DamageBoost, CombatKeyword.RandomDamage }),
        new Sentence("All attack deal %f damage", CombatKeyword.DamageBoost),
        new Sentence("Melee attack deal %f damage", new List<CombatKeyword>() { CombatKeyword.DamageBoost, CombatKeyword.MeleeAttack }),
        new Sentence("Deal %f indirect #D damage", CombatKeyword.DealIndirectDamage),
        new Sentence("Dealing %f damage", CombatKeyword.DamageBoost),
        new Sentence("Cause %f damage", CombatKeyword.DamageBoost),
        new Sentence("Your #D attack deal %f direct damage", CombatKeyword.DamageBoost),
        new Sentence("Over %f second", CombatKeyword.EffectDuration),

        // new Sentence("Within %f second", CombatKeyword.EffectDuration),
        new Sentence("Lasts %f second", CombatKeyword.EffectDuration),
        new Sentence("For %f second after using ", CombatKeyword.EffectDuration),
        new Sentence("For %f second", CombatKeyword.EffectDuration),
        new Sentence("(%f second)", CombatKeyword.EffectDuration),
        new Sentence("For %f minute", CombatKeyword.EffectDurationMinute),
        new Sentence("After a %f second delay", CombatKeyword.EffectDelay),
        new Sentence("After an %f second delay", CombatKeyword.EffectDelay),
        new Sentence("After %f second", CombatKeyword.EffectDelay),
        new Sentence("Every %f second", CombatKeyword.EffectRecurrence),
        new Sentence("Every other second", CombatKeyword.EffectRecurrence),
        new Sentence("Every few second", CombatKeyword.EffectRecurrence),
        new Sentence("With each heal", CombatKeyword.EffectRecurrence),
        new Sentence("Remove (up to) %f more Rage", CombatKeyword.AddRage, SignInterpretation.Opposite),
        new Sentence("Reduce Rage by %f", CombatKeyword.AddRage, SignInterpretation.Opposite),
        new Sentence("Reduce %f more Rage", CombatKeyword.AddRage, SignInterpretation.Opposite),
        new Sentence("Reduce the target's Rage by %f", CombatKeyword.AddRage, SignInterpretation.AlwaysNegative),
        new Sentence("Reduce target's Rage by %f", CombatKeyword.AddRage, SignInterpretation.AlwaysNegative),
        new Sentence("reduce targets' Rage by %f", CombatKeyword.AddRage, SignInterpretation.AlwaysNegative),
        new Sentence("%f Rage reduction", CombatKeyword.AddRage, SignInterpretation.AlwaysNegative),
        new Sentence("Generate %f Rage", CombatKeyword.AddRage),

        // new Sentence("Lower Rage by %f", CombatKeyword.AddRage, SignInterpretation.Opposite),
        new Sentence("Remove %f Rage", CombatKeyword.AddRage, SignInterpretation.Opposite),
        new Sentence("Deplete %f Rage", CombatKeyword.AddRage, SignInterpretation.Opposite),
        new Sentence("Generate no Rage", CombatKeyword.ZeroRage),
        new Sentence("Raise the target's Max Rage by %f", CombatKeyword.IncreaseMaxRage),
        new Sentence("Raise target's Max Rage by %f", CombatKeyword.IncreaseMaxRage),
        new Sentence("Increase target's Max Rage by%f", CombatKeyword.IncreaseMaxRage),
        new Sentence("Generate no Taunt", CombatKeyword.ZeroTaunt),
        new Sentence("Power Cost %f", CombatKeyword.AddPowerCost),
        new Sentence("Power Cost is %f", CombatKeyword.AddPowerCost),
        new Sentence("Reduce the Power cost of your @ %f", CombatKeyword.AddPowerCost),
        new Sentence("Reduce Power cost of your next @ by %f", new List<CombatKeyword>() { CombatKeyword.AddPowerCost, CombatKeyword.NextUse }),
        new Sentence("Reduce the Power cost of your next @ by %f", new List<CombatKeyword>() { CombatKeyword.AddPowerCost, CombatKeyword.NextUse }),
        new Sentence("Reduce the Power cost of @ %f", CombatKeyword.AddPowerCost),
        new Sentence("In-Combat Armor Regeneration %f", CombatKeyword.AddArmorRegen),
        // new Sentence("Armor Regeneration (in-combat) %f", CombatKeyword.AddArmorRegen),
        new Sentence("%f Armor Regeneration", CombatKeyword.AddArmorRegen),
        new Sentence("Recover %f Armor every five second", CombatKeyword.AddArmorRegen),
        new Sentence("Recover %f Armor", CombatKeyword.RestoreArmor),
        new Sentence("Power Regeneration is %f", CombatKeyword.AddPowerRegen),
        new Sentence("Cost %f Power", CombatKeyword.AddPowerCost),
        new Sentence("Regain %f Power", CombatKeyword.AddPowerRegen),
        new Sentence("The maximum Power restored by @ increase %f", CombatKeyword.AddPowerCostMax),
        new Sentence("Max Armor %f", CombatKeyword.AddMaxArmor),

        // new Sentence("Gain %f Armor", CombatKeyword.AddMaxArmor),
        new Sentence("Increase your Max Health by %f", CombatKeyword.AddMaxHealth),
        new Sentence("Increase your Max Armor by %f", CombatKeyword.AddMaxArmor),
        new Sentence("Reuse Time %f second", CombatKeyword.AddResetTimer),
        new Sentence("Reuse Time is %f second faster", CombatKeyword.AddResetTimer, SignInterpretation.AlwaysNegative),
        new Sentence("Reuse Time is %f second sooner", CombatKeyword.AddResetTimer, SignInterpretation.AlwaysNegative),
        new Sentence("Reuse Time is %f second", CombatKeyword.AddResetTimer),
        new Sentence("Reuse Time is %f sec", CombatKeyword.AddResetTimer),
        new Sentence("Hasten current reuse time of @ by %f second", CombatKeyword.AddResetTimer, SignInterpretation.AlwaysNegative),
        new Sentence("Hasten the current reuse time of @ by %f second", CombatKeyword.AddResetTimer, SignInterpretation.AlwaysNegative),
        new Sentence("Hasten the current reset time of @ by %f second", CombatKeyword.AddResetTimer, SignInterpretation.AlwaysNegative),
        new Sentence("Hasten the remaining reset time of @ by %f second", CombatKeyword.AddResetTimer, SignInterpretation.AlwaysNegative),
        new Sentence("Hasten your current Combat Refresh delay by %f second", CombatKeyword.AddCombatRefreshTimer, SignInterpretation.AlwaysNegative),
        new Sentence("Hasten your current Combat Refresh time by %f second", CombatKeyword.AddCombatRefreshTimer, SignInterpretation.AlwaysNegative),
        new Sentence("Shorten the remaining reset time of @ by %f second", CombatKeyword.AddResetTimer, SignInterpretation.AlwaysNegative),
        new Sentence("Shorten the current reuse time of @ by %f second", CombatKeyword.AddResetTimer, SignInterpretation.AlwaysNegative),
        new Sentence("Reuse time of @ is hastened by %f second", CombatKeyword.AddResetTimer, SignInterpretation.AlwaysNegative),
        new Sentence("Reuse time on @ is hastened %f second", CombatKeyword.AddResetTimer, SignInterpretation.AlwaysNegative),
        new Sentence("@ reuse time is hastened %f second", CombatKeyword.AddResetTimer, SignInterpretation.AlwaysNegative),
        new Sentence("Its reuse time is increased %f second", CombatKeyword.AddResetTimer),
        new Sentence("Reset time of @ is increased %f second", CombatKeyword.AddCombatRefreshTimer),
        new Sentence("Reduce the taunt of all your attack by %f", CombatKeyword.AddTaunt, SignInterpretation.Opposite),
        new Sentence("Taunt %f", CombatKeyword.AddTaunt),
        new Sentence("Taunted %f", CombatKeyword.AddTaunt),
        new Sentence("Taunt as if they did %f more damage", CombatKeyword.AddTaunt),
        new Sentence("Taunt as if they did %f damage", CombatKeyword.AddTaunt),
        new Sentence("Taunt their opponents %f less", CombatKeyword.AddTaunt, SignInterpretation.Opposite),
        new Sentence("Taunt of all your attack %f", CombatKeyword.AddTaunt),
        new Sentence("Your Taunt is %f", CombatKeyword.AddTaunt),
        new Sentence("%f Taunt", CombatKeyword.AddTaunt),
        new Sentence("When you have %f or less of your Armor left", CombatKeyword.BelowArmor),
        //new Sentence("Have less than %f of their Armor", CombatKeyword.BelowArmor),
        new Sentence("Have less than %f of their Max Rage", CombatKeyword.BelowMaxRage),
        new Sentence("Restore %f Health, Armor, and Power", CombatKeyword.RestoreHealthArmorPower),
        new Sentence("%f Health/Armor healing", CombatKeyword.RestoreHealthArmor),
        new Sentence("Heal you for %f Health/Armor", new List<CombatKeyword>() { CombatKeyword.RestoreHealthArmor, CombatKeyword.TargetSelf }),
        new Sentence("Heal you for %f Health", new List<CombatKeyword>() { CombatKeyword.RestoreHealth, CombatKeyword.TargetSelf }),
        new Sentence("Heal your pet for %f Health/Armor", CombatKeyword.RestoreHealthArmorToPet),
        new Sentence("You regain %f Health", new List<CombatKeyword>() { CombatKeyword.RestoreHealth, CombatKeyword.TargetSelf }),
        new Sentence("Restore %f Health/Armor to your pet", CombatKeyword.RestoreHealthArmorToPet),
        new Sentence("Restore %f Health/Armor", CombatKeyword.RestoreHealthArmor),

        // new Sentence("Restore %f of your Max Health", CombatKeyword.RestoreMaxHealth),
        new Sentence("Restore %f health (or Armor)", CombatKeyword.RestoreHealthArmor),
        new Sentence("Restore %f health", CombatKeyword.RestoreHealth),
        new Sentence("Boost the healing of your @ %f", CombatKeyword.RestoreHealth),

        // new Sentence("Heal you for %f of your Max Health", CombatKeyword.RestoreMaxHealth),
        new Sentence("Heal all targets for %f health", CombatKeyword.RestoreHealth),
        new Sentence("Heal %f armor", CombatKeyword.RestoreArmor),
        new Sentence("Restore %f armor", CombatKeyword.RestoreArmor),
        new Sentence("Restore %f Power", CombatKeyword.RestorePower),
        //new Sentence("Recover Power when", CombatKeyword.RestorePower),
        new Sentence("Recover %f health", CombatKeyword.RestoreHealth),
        new Sentence("Recover %f power", CombatKeyword.RestorePower),
        new Sentence("Restoration %f", CombatKeyword.RestoreHealth),
        new Sentence("You regain %f power", CombatKeyword.RestorePower),
        new Sentence("Cost no Power to cast", CombatKeyword.ZeroPowerCost),
        new Sentence("Take %f second to channel", CombatKeyword.AddChannelingTime),
        // new Sentence("Boost the healing from your @ %f", CombatKeyword.TargetAbilityBoost),
        // new Sentence("Heal you for %f armor", CombatKeyword.RestoreArmor),
        new Sentence("Heal %f health", CombatKeyword.RestoreHealth),
        new Sentence("Healing %f", CombatKeyword.RestoreHealth),
        new Sentence("Heal you %f", CombatKeyword.RestoreHealth),
        new Sentence("Heal %f", CombatKeyword.RestoreHealth),
        new Sentence("Restore %f Body Heat", CombatKeyword.RestoreBodyHeat),
        new Sentence("Sprint Speed increase by %f", CombatKeyword.AddSprintSpeed),
        new Sentence("%f Sprint Speed", CombatKeyword.AddSprintSpeed),
        new Sentence("Max Health %f", CombatKeyword.AddMaxHealth),
        new Sentence("Max Health is %f", CombatKeyword.AddMaxHealth),
        new Sentence("Max Health by %f", CombatKeyword.AddMaxHealth),
        new Sentence("%f Max Health", CombatKeyword.AddMaxHealth),
        new Sentence("%f Max Armor", CombatKeyword.AddMaxArmor),
        new Sentence("Have %f Armor", CombatKeyword.AddMaxArmor),
        new Sentence("Attack Range is %f", CombatKeyword.AddRange),
        new Sentence("Range is %f meter", CombatKeyword.AddRange),
        new Sentence("Range is increased %f meter", CombatKeyword.AddRange),
        new Sentence("Range is reduced %f meter", CombatKeyword.AddRange, SignInterpretation.AlwaysNegative),
        new Sentence("Stun you", CombatKeyword.SelfStun),
        new Sentence("Complete stun immunity", CombatKeyword.StunImmunity),
        new Sentence("Grant immunity to new stun", CombatKeyword.StunImmunity),
        new Sentence("Grant them immunity to stun", CombatKeyword.StunImmunity),
        new Sentence("Grant immunity to new slow and root", CombatKeyword.SlowRootImmunity),
        new Sentence("Grant them immunity to Slow and Root", CombatKeyword.SlowRootImmunity),
        new Sentence("Dispel stun", CombatKeyword.RemoveStun),
        new Sentence("Dispel any Stun", CombatKeyword.RemoveStun),
        new Sentence("Can be used while stunned. Doing so remove the stun effect from you.", CombatKeyword.RemoveStun),
        new Sentence("Dispel slow and root", CombatKeyword.RemoveSlowRoot),
        new Sentence("Dispel any Slow or Root", CombatKeyword.RemoveSlowRoot),
        new Sentence("Target is prone to random self-stuns", CombatKeyword.Concussion),
        new Sentence("Stun targets", CombatKeyword.Stun),
        new Sentence("Stun incorporeal enemies", CombatKeyword.StunIncorporeal),
        new Sentence("Stun", CombatKeyword.Stun),
        new Sentence("Targets are Knock back", CombatKeyword.Knockback),
        new Sentence("Knock back targets", CombatKeyword.Knockback),
        new Sentence("Knock all targets back", CombatKeyword.Knockback),
        new Sentence("Knock them back", CombatKeyword.Knockback),
        new Sentence("Knock the target backward", CombatKeyword.Knockback),
        new Sentence("Knock the enemy backward", CombatKeyword.Knockback),
        new Sentence("Knock the target back", CombatKeyword.Knockback),
        new Sentence("Knock target backward", CombatKeyword.Knockback),
        new Sentence("Knock targets backward", CombatKeyword.Knockback),
        //new Sentence("Knock them backward", CombatKeyword.Knockback),
        new Sentence("Reset the time on", CombatKeyword.ResetOtherAbilityTimer),
        new Sentence("Deal %f total damage against Demons", CombatKeyword.DamageBoostAgainstSpecie),
        new Sentence("Boost targets' mitigation %f", CombatKeyword.AddMitigation),
        new Sentence("#D mitigation %f", CombatKeyword.AddMitigation),
        // new Sentence("Vulnerability to Elite #D damage %f", new List<CombatKeyword>() { CombatKeyword.AddMitigation, CombatKeyword.TargetElite }, SignInterpretation.Opposite),
        // new Sentence("Mitigate %f of physical damage from Elite", new List<CombatKeyword>() { CombatKeyword.AddMitigationPhysical, CombatKeyword.TargetElite }),

        // new Sentence("%f #D Damage Reduction", CombatKeyword.AddMitigation),
        new Sentence("Mitigate %f #D Damage", CombatKeyword.AddMitigation),

        // new Sentence("%f Direct Damage Reduction", CombatKeyword.AddMitigationDirect),
        new Sentence("Grant %f Universal #D Mitigation", CombatKeyword.AddMitigation),
        new Sentence("Universal Damage Mitigation %f", CombatKeyword.AddMitigation),
        new Sentence("Reduce the damage you take from #D attack by %f", CombatKeyword.AddMitigation),
        new Sentence("Reduce the damage of the next attack that hit the target by %f", new List<CombatKeyword>() { CombatKeyword.AddMitigation, CombatKeyword.NextAttack }),
        new Sentence("Take %f less damage from all attack", CombatKeyword.AddMitigation),
        new Sentence("Target take %f less damage from attack", CombatKeyword.AddMitigation),
        new Sentence("Target take %f less damage from #D attack", CombatKeyword.AddMitigation),
        new Sentence("Target to take %f less damage from attack", CombatKeyword.AddMitigation),
        new Sentence("Target to take %f less damage from #D attack", CombatKeyword.AddMitigation),
        new Sentence("Mitigate %f of all #D damage", CombatKeyword.AddMitigation),
        new Sentence("Up to %f direct damage mitigation", new List<CombatKeyword>() { CombatKeyword.VariableMitigation, CombatKeyword.ApplyToPet }),
        new Sentence("#D Mitigation vs Elites %f", new List<CombatKeyword>() { CombatKeyword.AddMitigation, CombatKeyword.TargetElite }),

        // new Sentence("Mitigation vs Elites %f", new List<CombatKeyword>() { CombatKeyword.AddMitigation, CombatKeyword.TargetElite }),
        new Sentence("Mitigation vs all attack by Elites %f", new List<CombatKeyword>() { CombatKeyword.AddMitigation, CombatKeyword.TargetElite }),
        new Sentence("Against Elite enemies, mitigate %f", new List<CombatKeyword>() { CombatKeyword.AddMitigation, CombatKeyword.TargetElite }),
        new Sentence("Mitigation vs physical damage %f", CombatKeyword.AddMitigationPhysical),
        new Sentence("Elite Physical Damage Mitigation %f", new List<CombatKeyword>() { CombatKeyword.AddMitigationPhysical, CombatKeyword.TargetElite }),
        new Sentence("Physical Damage Mitigation %f", CombatKeyword.AddMitigationPhysical),
        new Sentence("%f absorption of any physical damage", CombatKeyword.AddMitigationPhysical),
        new Sentence("Any internal (#D) attack that hit you are reduced by %f", CombatKeyword.AddMitigationInternal),
        new Sentence("Any physical (#D) attack that hit you are reduced by %f", CombatKeyword.AddMitigationInternal),
        new Sentence("Universal Indirect Mitigation %f", CombatKeyword.AddMitigationIndirect),
        new Sentence("Mitigate all damage over time by %f per tick", CombatKeyword.AddMitigationIndirect),
        new Sentence("when armor is empty, up to %f when armor is full", new List<CombatKeyword>() { CombatKeyword.VariableMitigation, CombatKeyword.ApplyToPet }),
        new Sentence("Universal Direct Elite Mitigation %f", new List<CombatKeyword>() { CombatKeyword.AddMitigationDirect, CombatKeyword.TargetElite }),
        new Sentence("%f Mitigation from all Elite attack", new List<CombatKeyword>() { CombatKeyword.AddMitigationDirect, CombatKeyword.TargetElite }),
        new Sentence("Stacks up to %f times", CombatKeyword.MaxStack),
        new Sentence("Stacks up to %fx", CombatKeyword.MaxStack),
        new Sentence("Max of %f stacks", CombatKeyword.MaxStack),
        new Sentence("To all allies", CombatKeyword.ApplyToAllies),
        new Sentence("All allies gain", CombatKeyword.ApplyToAllies),
        new Sentence("Grant all allies", CombatKeyword.ApplyToAllies),
        new Sentence("And your allies' attack", CombatKeyword.ApplyToAllies),
        new Sentence("Burst Evasion and Projectile evasion %f", new List<CombatKeyword>() { CombatKeyword.AddEvasionBurst, CombatKeyword.AddEvasionProjectile }),
        new Sentence("%f chance to avoid being hit by burst attack", CombatKeyword.AddEvasionBurst),
        new Sentence("%f evasion of burst attack", CombatKeyword.AddEvasionBurst),
        new Sentence("Burst Evasion %f", CombatKeyword.AddEvasionBurst),
        new Sentence("%f Burst Evasion", CombatKeyword.AddEvasionBurst),
        new Sentence("Boost Burst Evasion by %f", CombatKeyword.AddEvasionBurst),
        new Sentence("Projectile Evasion %f", CombatKeyword.AddEvasionProjectile),
        new Sentence("%f Projectile Evasion", CombatKeyword.AddEvasionProjectile),
        new Sentence("Melee Evasion %f", CombatKeyword.AddEvasionMelee),
        new Sentence("%f Melee Evasion", CombatKeyword.AddEvasionMelee),
        // new Sentence("A barrier that mitigates %f damage from #D attacks", CombatKeyword.AddMitigation),
        new Sentence("%f mitigation of all physical attack", CombatKeyword.AddMitigationPhysical),
        new Sentence("%f mitigation of any physical damage", CombatKeyword.AddMitigationPhysical),
        new Sentence("%f mitigation against physical attack", CombatKeyword.AddMitigationPhysical),
        new Sentence("%f mitigation from direct attack", CombatKeyword.AddMitigationDirect),
        new Sentence("%f mitigation vs direct attack", CombatKeyword.AddMitigationDirect),
        new Sentence("%f mitigation against all attack", CombatKeyword.AddMitigation),
        new Sentence("%f Mitigation from all attack", CombatKeyword.AddMitigation),
        new Sentence("%f Mitigation from attack", CombatKeyword.AddMitigation),
        new Sentence("%f mitigation vs #D", CombatKeyword.AddMitigation),
        // new Sentence("Increase your Mitigation vs #D attack %f", CombatKeyword.AddMitigation),
        new Sentence("%f mitigation from #D attack", CombatKeyword.AddMitigation),
        new Sentence("Direct and Indirect #D mitigation %f", CombatKeyword.AddMitigation),
        new Sentence("Indirect #D mitigation %f", CombatKeyword.AddMitigationIndirect),
        new Sentence("Direct #D mitigation %f", CombatKeyword.AddMitigation),
        new Sentence("%f direct damage mitigation", CombatKeyword.AddMitigationDirect),
        new Sentence("Boost your direct damage mitigation %f", CombatKeyword.AddMitigationDirect),
        new Sentence("Cause all targets to suffer %f damage from direct #D attack", CombatKeyword.AddMitigation, SignInterpretation.Opposite),
        new Sentence("Debuff the target so that it take %f damage from future #D attack", CombatKeyword.AddMitigation, SignInterpretation.Opposite),
        new Sentence("You take %f damage from #D attack", CombatKeyword.AddMitigation, SignInterpretation.Opposite),
        new Sentence("%f #D mitigation", CombatKeyword.AddMitigation),
        new Sentence("Increase your #D Mitigation %f", CombatKeyword.AddMitigation),
        new Sentence("%f Damage Mitigation", CombatKeyword.AddMitigation),
        new Sentence("%f Direct Mitigation", CombatKeyword.AddMitigationDirect),
        new Sentence("Mitigate %f of all physical damage", CombatKeyword.AddMitigationPhysical),
        new Sentence("Mitigate %f physical damage", CombatKeyword.AddMitigationPhysical),
        new Sentence("Debuff their mitigation %f", CombatKeyword.DebuffMitigation, SignInterpretation.AlwaysNegative),
        new Sentence("%f Cold Protection (Direct and Indirect)", CombatKeyword.AddProtectionCold),
        new Sentence("%f Direct and Indirect Cold Protection", CombatKeyword.AddProtectionCold),
        new Sentence("Remove ongoing #D effects (up to %f dmg/sec)", CombatKeyword.RemoveEffects),
        new Sentence("Chance to Ignore Knockbacks %f", CombatKeyword.AddChanceToIgnoreKnockback),
        new Sentence("%f chance to ignore Stun", CombatKeyword.AddChanceToIgnoreStun),
        new Sentence("Chance to ignore Stun %f", CombatKeyword.AddChanceToIgnoreStun),
        new Sentence("Targets whose Rage meter are at least %f full", CombatKeyword.AboveRage),
        new Sentence("Targets whose Rage meter is at least %f full", CombatKeyword.AboveRage),
        new Sentence("If target's Rage is at least %f full", CombatKeyword.AboveRage),
        new Sentence("If target's Rage meter is at least %f full", CombatKeyword.AboveRage),
        new Sentence("%f chance to Knock Down", CombatKeyword.AddChanceToKnockdown),
        new Sentence("There's a %f chance", CombatKeyword.ApplyWithChance),
        new Sentence("%f chance to", CombatKeyword.ApplyWithChance),
        new Sentence("%f of your attack", CombatKeyword.ApplyWithChance),
        new Sentence("When wielding two knives", CombatKeyword.RequireTwoKnives),
        new Sentence("If the target is not focused on you", CombatKeyword.RequireNoAggro),
        new Sentence("If they are not focused on you", CombatKeyword.RequireNoAggro),
        new Sentence("If target is not focused on you", CombatKeyword.RequireNoAggro),
        new Sentence("The first melee attacker is knock away", CombatKeyword.ReflectKnockbackOnFirstMelee),
        new Sentence("First attacker is knock back", CombatKeyword.ReflectKnockbackOnFirstMelee),
        //new Sentence("When a melee attack deal damage to you", CombatKeyword.ReflectOnMelee),
        //new Sentence("Melee attackers deal damage to you", CombatKeyword.ReflectOnMelee),

        // new Sentence("Melee damagers take", CombatKeyword.ReflectOnMelee),
        new Sentence("Deal its damage when you are hit by burst attack", CombatKeyword.ReflectOnBurst),
        new Sentence("Deal its damage when you are hit by ranged attack", CombatKeyword.ReflectOnRanged),
        new Sentence("Chance to consume grass is %f", CombatKeyword.ChanceToConsume),

        // new Sentence("You regenerate %f Health per tick", CombatKeyword.AddHealthRegen),
        new Sentence("Have %f health", CombatKeyword.AddMaxHealth),
        new Sentence("Per second", CombatKeyword.EffectRecurrence),
        new Sentence("Steal %f health", CombatKeyword.DrainHealth),
        new Sentence("Steal %f more health", CombatKeyword.DrainHealth),
        new Sentence("Chance to consume carrot is %f", CombatKeyword.ChanceToConsume),
        new Sentence("Lower target's aggro toward you by %f", CombatKeyword.AddTaunt, SignInterpretation.Opposite),
        new Sentence("Until you trigger the teleport", CombatKeyword.UntilTrigger),
        new Sentence("Until you Feint", CombatKeyword.UntilTrigger),
        new Sentence("Boost your movement speed by %f", CombatKeyword.AddSprintSpeed),
        new Sentence("Boost movement speed by %f", CombatKeyword.AddSprintSpeed),
        new Sentence("Increase your movement speed by %f", CombatKeyword.AddSprintSpeed),
        new Sentence("Out of Combat Sprint Speed %f", CombatKeyword.AddOutOfCombatSpeed),
        new Sentence("%f Out of Combat Sprint Speed", CombatKeyword.AddOutOfCombatSpeed),
        new Sentence("Your Out of Combat Sprint speed %f", CombatKeyword.AddOutOfCombatSpeed),
        new Sentence("Your Out of Combat Sprint speed by %f", CombatKeyword.AddOutOfCombatSpeed),
        new Sentence("Speed is %f", CombatKeyword.AddSprintSpeed),
        new Sentence("Movement speed %f", CombatKeyword.AddSprintSpeed),
        new Sentence("Sprint speed %f", CombatKeyword.AddSprintSpeed),
        new Sentence("Sprint speed by %f", CombatKeyword.AddSprintSpeed),
        new Sentence("%f Movement Speed", CombatKeyword.AddSprintSpeed),
        new Sentence("Fly speed is boosted %f", CombatKeyword.AddFlySpeed),
        new Sentence("Fly Speed %f", CombatKeyword.AddFlySpeed),
        new Sentence("Swim Speed %f", CombatKeyword.AddSwimSpeed),
        new Sentence("Slow target's movement by %f", CombatKeyword.Slow),
        new Sentence("Slow target's movement speed by %f", CombatKeyword.Slow),
        new Sentence("Give you %f Accuracy", CombatKeyword.AddAccuracy),
        new Sentence("Melee Accuracy %f", CombatKeyword.AddMeleeAccuracy),
        new Sentence("Burst Accuracy %f", CombatKeyword.AddBurstAccuracy),
        new Sentence("You gain Accuracy %f with melee attack", CombatKeyword.AddMeleeAccuracy),
        new Sentence("Accuracy %f", CombatKeyword.AddAccuracy),
        new Sentence("%f Accuracy", CombatKeyword.AddAccuracy),
        new Sentence("Boost melee evasion %f", CombatKeyword.AddEvasionMelee),
        new Sentence("Melee Evasion is %f", CombatKeyword.AddEvasionMelee),
        new Sentence("Lower targets' Evasion by %f", CombatKeyword.RemoveEvasion),
        new Sentence("%f more chance of missing", CombatKeyword.AddAccuracy, SignInterpretation.Opposite),
        new Sentence("%f Miss Chance", CombatKeyword.AddAccuracy, SignInterpretation.Opposite),
        new Sentence("%f Physical Damage Reflection", CombatKeyword.AddPhysicalReflection),
        new Sentence("%f resistance to #D damage", CombatKeyword.AddDamageResistance),
        new Sentence("#D Resistance %f", CombatKeyword.AddDamageResistance),
        new Sentence("You are %f resistant to #D damage", CombatKeyword.AddDamageResistance),
        new Sentence("Within %f meter", CombatKeyword.WithinDistance),
        new Sentence("%f resistance to Elemental damage (Fire, Cold, Electricity)", CombatKeyword.AddElementalDamageResistance),
        new Sentence("Worth %f more XP", CombatKeyword.IncreaseXPGain),
        new Sentence("%f Earned Combat XP", CombatKeyword.IncreaseXPGain),
        new Sentence("Slain within %f second", CombatKeyword.MaxKillTime),
        new Sentence("Buff targets' direct #D damage %f", CombatKeyword.DamageBoost),
        new Sentence("%f Direct Damage", CombatKeyword.DamageBoost),
        new Sentence("%f #D damage", CombatKeyword.DamageBoost),
        new Sentence("#D damage %f", CombatKeyword.DamageBoost),
        new Sentence("%f health damage", CombatKeyword.DealDirectHealthDamage),
        new Sentence("%f #D health damage", CombatKeyword.DealDirectHealthDamage),

        // new Sentence("%f Health and Armor damage", CombatKeyword.DamageBoostToHealthAndArmor),
        new Sentence("%f damage", CombatKeyword.DamageBoost),
        new Sentence("Damage %f", CombatKeyword.DamageBoost),
        new Sentence("%f armor damage", CombatKeyword.DealArmorDamage),
        new Sentence("%f @", CombatKeyword.TargetAbilityBoost),
        new Sentence("@ boost %f", CombatKeyword.TargetAbilityBoost),
        new Sentence("Boost your @ %f", CombatKeyword.TargetAbilityBoost),
        new Sentence("Until you are attacked", CombatKeyword.UntilAttacked),
        new Sentence("(for all targets)", CombatKeyword.ApplyToAllies),
        new Sentence("Target does not yell for help because of this attack", CombatKeyword.NoYellForHelp),
        new Sentence("Doesn't cause the target to yell for help", CombatKeyword.NoYellForHelp),
        new Sentence("This attack does not cause the target to shout for help", CombatKeyword.NoYellForHelp),
        new Sentence("Does not cause the target to shout for help", CombatKeyword.NoYellForHelp),
        new Sentence("If it is a Werewolf ability", CombatKeyword.IfWerewolf),
        new Sentence("To the kicker", CombatKeyword.ToKickerTarget),
        new Sentence("Cause kicks", CombatKeyword.ToKickerTarget),
        new Sentence("Randomly repair broken bones twice as often", CombatKeyword.RepairBrokenBone),
        new Sentence("And %f armor", CombatKeyword.RestoreArmor),
        new Sentence("And %f power", CombatKeyword.RestorePower),
        new Sentence("Restore %f", CombatKeyword.RestoreHealth),
        //new Sentence("%f Enthusiasm", CombatKeyword.AddEnthusiasm),
        new Sentence("%f Death Avoidance", CombatKeyword.AddDeathAvoidance),
        new Sentence("Target suffer a second blast of #D damage", CombatKeyword.SecondBlast),
        new Sentence("Target take a second full blast of delayed #D damage", CombatKeyword.SecondBlast),
        new Sentence("#D damage no longer dispel", CombatKeyword.NoDispel),
        new Sentence("Ignore mitigation from armor", CombatKeyword.IgnoreArmor),
        new Sentence("%f Body Heat", CombatKeyword.RestoreBodyHeat),
        new Sentence("Damage is %f", CombatKeyword.DamageBoost),
        new Sentence("#S damage %f", CombatKeyword.DamageBoost),
        new Sentence("Deal double damage", CombatKeyword.DamageBoostDouble),
        new Sentence("Channeling time is %f second", CombatKeyword.AddChannelingTime),
        new Sentence("Summons figment", CombatKeyword.AnotherTrap),
        new Sentence("Reduce it by %f more", CombatKeyword.Again, SignInterpretation.AlwaysNegative),
        new Sentence("Any time you Evade a Melee attack", CombatKeyword.OnEvadeMelee),
        new Sentence("Any time you Evade an attack", CombatKeyword.OnEvade),
        new Sentence("%f of all #D damage you take is mitigated and added to the damage done by your next Kick", CombatKeyword.MitigateReflectKick),
        new Sentence("%f of all #D damage you take is mitigated and added to the damage done by your next", CombatKeyword.MitigateReflect),
        new Sentence("At a %f rate", CombatKeyword.ReflectRate),
        new Sentence("Damage type become #D", CombatKeyword.ChangeDamageType),
        new Sentence("Damage type is #D instead", CombatKeyword.ChangeDamageType),
        new Sentence("Damage become #D", CombatKeyword.ChangeDamageType),
        new Sentence("Damage type is changed to #D", CombatKeyword.ChangeDamageType),
        new Sentence("Cause targets to lose %f Rage", CombatKeyword.AddRage, SignInterpretation.AlwaysNegative),
        new Sentence("You mitigate 1 point of attack damage for every 20 Armor you have remaining", CombatKeyword.ThickArmor),
        new Sentence("When you are hit by a monster's Rage Attack", CombatKeyword.ReflectOnBurst),
        new Sentence("When you are hit", CombatKeyword.ReflectOnAnyAttack),
        new Sentence("Any attackers that hit you", CombatKeyword.ReflectOnAnyAttack),

        // new Sentence("Each time they attack and damage you", CombatKeyword.ReflectOnAnyAttack),
        new Sentence("Returning it to you as armor", CombatKeyword.DrainAsArmor),
        new Sentence("When you teleport", CombatKeyword.WhenTeleporting),
        new Sentence("When you trigger teleport", CombatKeyword.WhenTeleporting),
        new Sentence("b*u*t", CombatKeyword.But),
        new Sentence("However,", CombatKeyword.But),
    };
    #endregion
}
