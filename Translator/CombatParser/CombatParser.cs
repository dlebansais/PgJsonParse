namespace Translator;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using PgObjects;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Encodings.Web;
using System.Linq;

public partial class CombatParser
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
            WritePowerCSV("combateffects.csv", StringKeyTable, AnalyzedPowerKeyToCompleteEffectTable, objectList);
            WritePowerEffectJson("combateffects.json", StringKeyTable, AnalyzedPowerKeyToCompleteEffectTable);
            WriteBuffEffectJson("buffeffects.json", StringKeyTable.Count, AnalyzedPowerKeyToCompleteEffectTable, EffectKeyList);
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

        if (Key == "7303")
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
        if (power.Key == "13003")
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

                    if (!PowerIconList.Contains(IconId))
                        PowerIconList.Add(IconId);
                }
            }
        }

        List<AbilityKeyword> AbilityKeywordList = new List<AbilityKeyword>();

        // Hack for this specific power
        if (effectList.Count > 0 && effectList[0].Name.StartsWith("TSys_HammerCoreBoostsLookAtMyHammer_"))
            AbilityKeywordList.Add(AbilityKeyword.LookAtMyHammer);
        else
        {
            foreach (PgEffect EffectItem in effectList)
                foreach (AbilityKeyword Keyword in EffectItem.AbilityKeywordList)
                    if (!AbilityKeywordList.Contains(Keyword))
                        AbilityKeywordList.Add(Keyword);
        }

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

            if (AbilityItem.KeywordList.Count > 0)
            {
                foreach (AbilityKeyword Keyword in AbilityItem.KeywordList)
                    if (AbilityKeywordList.Contains(Keyword))
                    {
                        KeywordMatch = true;
                        break;
                    }
            }
            else
            {
                AbilityKeyword TooltipsExtraKeyword = AbilityItem.ExtraKeywordsForTooltips;
                if (TooltipsExtraKeyword != AbilityKeyword.Internal_None && AbilityKeywordList.Contains(TooltipsExtraKeyword))
                {
                    if (TooltipsExtraKeyword == AbilityKeyword.Minigolem)
                    {
                    }

                    KeywordMatch = true;
                }
            }

            Dictionary<AbilityPetType, AbilityKeyword> PetTypeToKeywordTable = new()
            {
                { AbilityPetType.SummonedSpider, AbilityKeyword.SummonedSpider },
                { AbilityPetType.StabledPet, AbilityKeyword.StabledPet },
                { AbilityPetType.SummonedColdSphere, AbilityKeyword.SummonedColdSphere },
                { AbilityPetType.StunTrap, AbilityKeyword.StunTrap },
                { AbilityPetType.PowerGlyph, AbilityKeyword.PowerGlyph },
                { AbilityPetType.SummonedTornado, AbilityKeyword.SummonedTornado },
            };

            AbilityPetType PetType = AbilityItem.PetTypeTagRequirement;
            if (PetTypeToKeywordTable.ContainsKey(PetType) && AbilityKeywordList.Contains(PetTypeToKeywordTable[PetType]))
            {
                if (PetType == AbilityPetType.StabledPet)
                {
                }

                KeywordMatch = true;
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
            case CombatKeyword.Fear:
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
            case CombatKeyword.IncreaseHealEfficiency:
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

        List<EffectVerificationEntry> VerificationTable = null!;

        if (expectTableWithEntries)
        {
            if (!EffectVerification.EffectVerificationTable.ContainsKey(combatKeyword))
            {
                Debug.WriteLine($"ERROR Combat Keyword {combatKeyword}: no entries?");
                EffectVerification.EffectVerificationTable.Add(combatKeyword, new List<EffectVerificationEntry>());
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

                        foreach (EffectVerificationEntry Entry in VerificationTable)
                            if (Label == Entry.Prefix && Suffix == Entry.Suffix)
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
                                Debug.WriteLine($"new EffectVerificationEntry() {{ Prefix = \"{Label}\", Suffix = \"{Suffix}\" }},");
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
    private void WritePowerCSV(string fileName, List<string[]> stringKeyTable, List<PgModEffect[]> powerKeyToCompleteEffectTable, List<object> objectList)
    {
        List<int> PowerList = new();
        Dictionary<int, Dictionary<int, string>> PowerTiersTable = new();

        for (int i = 0; i < stringKeyTable.Count; i++)
        {
            string[] StringKeyArray;
            StringKeyArray = stringKeyTable[i];
            PgModEffect[] ModEffectArray = powerKeyToCompleteEffectTable[i];

            Debug.Assert(StringKeyArray.Length == ModEffectArray.Length);

            for (int j = 0; j < StringKeyArray.Length; j++)
            {
                string StringKey = StringKeyArray[j];
                PgModEffect PgModEffect = ModEffectArray[j];

                if (PgModEffect is not null)
                {
                    string[] Splitted = StringKey.Split('_');
                    Debug.Assert(Splitted.Length == 2);
                    int TierPowerId = int.Parse(Splitted[0]);

                    if (!PowerList.Contains(TierPowerId))
                        PowerList.Add(TierPowerId);
                }
            }
        }

        Dictionary<int, PgPower> PowerKeyTable = new();

        foreach (object Item in objectList)
            if (Item is PgPower AsPower)
            {
                int Key = int.Parse(AsPower.Key);
                PowerKeyTable.Add(Key, AsPower);
            }

        PowerList.Sort((int id1, int id2) => SortPowerBySkillAndSlot(id1, id2, PowerKeyTable));

        foreach (int PowerId in PowerList)
            PowerTiersTable.Add(PowerId, new Dictionary<int, string>());

        for (int i = 0; i < stringKeyTable.Count; i++)
        {
            string[] StringKeyArray;
            StringKeyArray = stringKeyTable[i];
            PgModEffect[] ModEffectArray = powerKeyToCompleteEffectTable[i];

            for (int j = 0; j < StringKeyArray.Length; j++)
            {
                string StringKey = StringKeyArray[j];
                PgModEffect PgModEffect = ModEffectArray[j];

                if (PgModEffect is not null)
                {
                    string[] Splitted = StringKey.Split('_');
                    Debug.Assert(Splitted.Length == 2);
                    int TierPowerId = int.Parse(Splitted[0]);

                    int Tier = int.Parse(Splitted[1]);
                    string Description = PgModEffect.Description;
                    Description = Description.Replace("\n", " ");
                    Description = Description.Replace(";", ",");

                    string Slots = string.Empty;
                    PgPower Power = PowerKeyTable[TierPowerId];

                    foreach (ItemSlot Slot in Power.SlotList)
                    {
                        if (Slots != string.Empty)
                            Slots += ", ";

                        Slots += Slot.ToString();
                    }

                    PowerTiersTable[TierPowerId].Add(Tier, $"{Description};{Slots}");
                }
            }
        }

        List<string> LineList = new()
        {
            "Verified;Power ID;Tier;Description"
        };

        for (int i = 0; i < PowerList.Count; i++)
        {
            LineList.Add(string.Empty);

            foreach (KeyValuePair<int, string> Entry in PowerTiersTable[PowerList[i]])
            {
                string Line = $";{PowerList[i]};{Entry.Key};{Entry.Value}";
                LineList.Add(Line);
            }
        }

        using FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
        using StreamWriter sw = new StreamWriter(fs);

        foreach (string Line in LineList)
            sw.WriteLine(Line);
    }

    private static int SortPowerBySkillAndSlot(int id1, int id2, Dictionary<int, PgPower> powerKeyTable)
    {
        PgPower Power1 = powerKeyTable[id1];
        PgPower Power2 = powerKeyTable[id2];

        if (Power1.Skill_Key != Power2.Skill_Key)
            return string.Compare(Power1.Skill_Key, Power2.Skill_Key);

        List<int> SlotList1 = new();
        foreach (ItemSlot Slot in Power1.SlotList)
            SlotList1.Add((int)Slot);
        SlotList1.Sort();

        List<int> SlotList2 = new();
        foreach (ItemSlot Slot in Power2.SlotList)
            SlotList2.Add((int)Slot);
        SlotList2.Sort();

        for (int i = 0; i < SlotList1.Count && i < SlotList2.Count; i++)
            if (SlotList1[i] != SlotList2[i])
                return SlotList1[i] - SlotList2[i];

        if (SlotList1.Count != SlotList2.Count)
            return SlotList1.Count - SlotList2.Count;

        return 0;
    }

    private void WritePowerEffectJson(string fileName, List<string[]> stringKeyTable, List<PgModEffect[]> powerKeyToCompleteEffectTable)
    {
        Dictionary<int, PowerToEffect> PowerToEffectTable = new();

        for (int i = 0; i < stringKeyTable.Count; i++)
        {
            string[] StringKeyArray;
            StringKeyArray = stringKeyTable[i];
            PgModEffect[] ModEffectArray = powerKeyToCompleteEffectTable[i];

            Debug.Assert(StringKeyArray.Length == ModEffectArray.Length);

            int PowerId = -1;
            List<PowerTierToEffect> Tiers = new();

            for (int j = 0; j < StringKeyArray.Length; j++)
            {
                string StringKey = StringKeyArray[j];
                PgModEffect PgModEffect = ModEffectArray[j];

                if (PgModEffect is not null)
                {
                    string[] Splitted = StringKey.Split('_');
                    Debug.Assert(Splitted.Length == 2);
                    int TierPowerId = int.Parse(Splitted[0]);

                    if (PowerId < 0)
                        PowerId = TierPowerId;
                    else
                        Debug.Assert(PowerId == TierPowerId);

                    int Tier = int.Parse(Splitted[1]);

                    PowerTierToEffect? NewPowerTierToEffect = ToPowerTierEffect(PgModEffect, hasTier: true, Tier);

                    if (NewPowerTierToEffect is not null)
                    {
                        if (PgModEffect.SecondaryModEffect is not null)
                        {
                            Debug.Assert(PgModEffect.SecondaryModEffect.EffectKey == string.Empty);
                            Debug.Assert(PgModEffect.SecondaryModEffect.Description == string.Empty);

                            PowerTierToEffect? SecondaryPowerTierToEffect = ToPowerTierEffect(PgModEffect.SecondaryModEffect, hasTier: false, 0);
                            Debug.Assert(SecondaryPowerTierToEffect is not null);

                            if (SecondaryPowerTierToEffect is not null)
                                NewPowerTierToEffect.Xtra = SecondaryPowerTierToEffect;
                        }

                        Tiers.Add(NewPowerTierToEffect);
                    }
                }
            }

            if (Tiers.Count > 0)
            {
                Debug.Assert(PowerId > 0);

                PowerToEffect NewPowerToEffect = new() { Tiers = Tiers.ToArray() };

                if (HardcodedEffectAllTiersTable.ContainsKey(PowerId))
                {
                    foreach (PowerTierToEffect PowerTierToEffect in NewPowerToEffect.Tiers)
                        PowerTierToEffect.AdditionalEffects = HardcodedEffectAllTiersTable[PowerId];
                }
                else if (HardcodedEffectTable.ContainsKey(PowerId))
                {
                    for (int j = 0; j < NewPowerToEffect.Tiers.Length && j < HardcodedEffectTable[PowerId].Count; j++)
                        NewPowerToEffect.Tiers[j].AdditionalEffects = HardcodedEffectTable[PowerId][j + 1];
                }

                PowerToEffectTable.Add(PowerId, NewPowerToEffect);
            }
        }

        JsonSerializerOptions WriteOptions = new();
        WriteOptions.WriteIndented = true;
        WriteOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        WriteOptions.NumberHandling = JsonNumberHandling.Strict;
        WriteOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;

        string Content = JsonSerializer.Serialize(PowerToEffectTable, WriteOptions);
        using FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
        using StreamWriter sw = new StreamWriter(fs);

        sw.Write(Content);
    }

    private static Dictionary<int, Dictionary<int, AdditionalEffect[]>> HardcodedEffectTable = new()
    {
        { 12317, new Dictionary<int, AdditionalEffect[]>()
            {
                { 1, new AdditionalEffect[1] { new AdditionalEffect() { AbilityTrigger = AbilityKeyword.CleverTrick.ToString(), Effect = 20451, Target = "Pet" } } },
                { 2, new AdditionalEffect[1] { new AdditionalEffect() { AbilityTrigger = AbilityKeyword.CleverTrick.ToString(), Effect = 20452, Target = "Pet" } } },
                { 3, new AdditionalEffect[1] { new AdditionalEffect() { AbilityTrigger = AbilityKeyword.CleverTrick.ToString(), Effect = 20453, Target = "Pet" } } },
                { 4, new AdditionalEffect[1] { new AdditionalEffect() { AbilityTrigger = AbilityKeyword.CleverTrick.ToString(), Effect = 20454, Target = "Pet" } } },
                { 5, new AdditionalEffect[1] { new AdditionalEffect() { AbilityTrigger = AbilityKeyword.CleverTrick.ToString(), Effect = 20455, Target = "Pet" } } },
                { 6, new AdditionalEffect[1] { new AdditionalEffect() { AbilityTrigger = AbilityKeyword.CleverTrick.ToString(), Effect = 20456, Target = "Pet" } } },
                { 7, new AdditionalEffect[1] { new AdditionalEffect() { AbilityTrigger = AbilityKeyword.CleverTrick.ToString(), Effect = 20457, Target = "Pet" } } },
                { 8, new AdditionalEffect[1] { new AdditionalEffect() { AbilityTrigger = AbilityKeyword.CleverTrick.ToString(), Effect = 20458, Target = "Pet" } } },
            }
        },
        { 10451, new Dictionary<int, AdditionalEffect[]>()
            {
                { 1, new AdditionalEffect[1] { new AdditionalEffect() { AbilityTrigger = AbilityKeyword.BowBash.ToString(), Effect = 14589, Target = "Self" } } },
                { 2, new AdditionalEffect[1] { new AdditionalEffect() { AbilityTrigger = AbilityKeyword.BowBash.ToString(), Effect = 14590, Target = "Self" } } },
                { 3, new AdditionalEffect[1] { new AdditionalEffect() { AbilityTrigger = AbilityKeyword.BowBash.ToString(), Effect = 14591, Target = "Self" } } },
                { 4, new AdditionalEffect[1] { new AdditionalEffect() { AbilityTrigger = AbilityKeyword.BowBash.ToString(), Effect = 14592, Target = "Self" } } },
                { 5, new AdditionalEffect[1] { new AdditionalEffect() { AbilityTrigger = AbilityKeyword.BowBash.ToString(), Effect = 14593, Target = "Self" } } },
                { 6, new AdditionalEffect[1] { new AdditionalEffect() { AbilityTrigger = AbilityKeyword.BowBash.ToString(), Effect = 14594, Target = "Self" } } },
                { 7, new AdditionalEffect[1] { new AdditionalEffect() { AbilityTrigger = AbilityKeyword.BowBash.ToString(), Effect = 14595, Target = "Self" } } },
                { 8, new AdditionalEffect[1] { new AdditionalEffect() { AbilityTrigger = AbilityKeyword.BowBash.ToString(), Effect = 14596, Target = "Self" } } },
                { 9, new AdditionalEffect[1] { new AdditionalEffect() { AbilityTrigger = AbilityKeyword.BowBash.ToString(), Effect = 14597, Target = "Self" } } },
                { 10, new AdditionalEffect[1] { new AdditionalEffect() { AbilityTrigger = AbilityKeyword.BowBash.ToString(), Effect = 14598, Target = "Self" } } },
            }
        },
    };

    private void WriteBuffEffectJson(string fileName, int stringKeyTableCount, List<PgModEffect[]> powerKeyToCompleteEffectTable, List<string> effectKeyList)
    {
        Dictionary<int, PowerTierToEffect> PowerToEffectTable = new();

        for (int i = 0; i < effectKeyList.Count; i++)
        {
            string[] StringKeyArray = new string[] { effectKeyList[i] };
            PgModEffect[] ModEffectArray = powerKeyToCompleteEffectTable[stringKeyTableCount + i];

            Debug.Assert(StringKeyArray.Length == 1);
            Debug.Assert(ModEffectArray.Length == 1);

            string StringKey = StringKeyArray[0];
            PgModEffect PgModEffect = ModEffectArray[0];

            if (PgModEffect is not null)
            {
                Debug.Assert(PgModEffect.SecondaryModEffect is null);

                int PowerId = int.Parse(StringKey);

                PowerTierToEffect? NewPowerTierToEffect = ToPowerTierEffect(PgModEffect, hasTier: false, 0);

                if (NewPowerTierToEffect is not null)
                    PowerToEffectTable.Add(PowerId, NewPowerTierToEffect);
            }
        }

        JsonSerializerOptions WriteOptions = new();
        WriteOptions.WriteIndented = true;
        WriteOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        WriteOptions.NumberHandling = JsonNumberHandling.Strict;
        WriteOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;

        string Content = JsonSerializer.Serialize(PowerToEffectTable, WriteOptions);
        using FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
        using StreamWriter sw = new StreamWriter(fs);

        sw.Write(Content);
    }

    private static PowerTierToEffect? ToPowerTierEffect(PgModEffect modEffect, bool hasTier, int tier)
    {
        PowerTierToEffect NewPowerTierToEffect = new();
        bool WriteBuff = false;

        if (hasTier && modEffect.EffectKey != string.Empty)
        {
            WriteBuff = true;

            int EffectId = int.Parse(modEffect.EffectKey);
            NewPowerTierToEffect.Effect = EffectId;
        }

        if (modEffect.Description != string.Empty)
        {
            WriteBuff = true;

            NewPowerTierToEffect.Description = modEffect.Description;
        }

        if (modEffect.AbilityList.Count > 0)
        {
            WriteBuff = true;

            List<string> AbilityKeywords = new();
            foreach (AbilityKeyword Keyword in modEffect.AbilityList)
                AbilityKeywords.Add(Keyword.ToString());

            NewPowerTierToEffect.AbilityKeywords = AbilityKeywords.ToArray();
        }

        if (modEffect.StaticCombatEffectList.Count > 0)
        {
            WriteBuff = true;

            List<PowerTierCombatEffect> CombatEffects = new();
            foreach (PgCombatEffect CombatEffect in modEffect.StaticCombatEffectList)
            {
                PowerTierCombatEffect NewPowerTierCombatEffect = ToPowerTierCombatEffect(CombatEffect);
                CombatEffects.Add(NewPowerTierCombatEffect);
            }

            NewPowerTierToEffect.StaticCombatEffects = CombatEffects.ToArray();
        }

        if (modEffect.DynamicCombatEffectList.Count > 0)
        {
            WriteBuff = true;

            List<PowerTierCombatEffect> CombatEffects = new();
            foreach (PgCombatEffect CombatEffect in modEffect.DynamicCombatEffectList)
            {
                PowerTierCombatEffect NewPowerTierCombatEffect = ToPowerTierCombatEffect(CombatEffect);
                CombatEffects.Add(NewPowerTierCombatEffect);
            }

            NewPowerTierToEffect.DynamicCombatEffects = CombatEffects.ToArray();
        }

        if (modEffect.TargetAbilityList.Count > 0)
        {
            WriteBuff = true;

            List<string> TargetAbilityKeywords = new();
            foreach (AbilityKeyword Keyword in modEffect.TargetAbilityList)
                TargetAbilityKeywords.Add(Keyword.ToString());

            NewPowerTierToEffect.TargetAbilityKeywords = TargetAbilityKeywords.ToArray();
        }

        if (WriteBuff)
        {
            if (hasTier)
                NewPowerTierToEffect.Tier = tier;

            return NewPowerTierToEffect;
        }
        else
            return null;
    }

    private static PowerTierCombatEffect ToPowerTierCombatEffect(PgCombatEffect CombatEffect)
    {
        PowerTierCombatEffect NewPowerTierCombatEffect = new();

        if (CombatEffect.Keyword != CombatKeyword.Internal_None)
            NewPowerTierCombatEffect.Keyword = CombatEffect.Keyword.ToString();

        if (CombatEffect.DamageType != GameDamageType.Internal_None)
        {
            GameDamageType DamageType = CombatEffect.DamageType;
            List<string> TextList = new();

            foreach (GameDamageType EnumValue in typeof(GameDamageType).GetEnumValues())
                if (EnumValue != GameDamageType.Internal_None && DamageType.HasFlag(EnumValue))
                    TextList.Add(EnumValue.ToString());

            if (TextList.Count > 1)
            {
            }

            NewPowerTierCombatEffect.DamageTypes = TextList.ToArray();
        }

        if (CombatEffect.CombatSkill != GameCombatSkill.Internal_None)
            NewPowerTierCombatEffect.CombatSkill = CombatEffect.CombatSkill.ToString();

        if (CombatEffect.Data is not null && CombatEffect.Data.RawValue is not null)
        {
            PowerTierNumericValue NewPowerTierNumericValue = new();
            NewPowerTierNumericValue.Value = (decimal)CombatEffect.Data.RawValue;
            NewPowerTierNumericValue.IsPercent = CombatEffect.Data.RawIsPercent;

            NewPowerTierCombatEffect.Data = NewPowerTierNumericValue;
        }

        return NewPowerTierCombatEffect;
    }

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

            if (Entry.Key.Key == "7301")
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
        bool IsGolemAbility = ModText.StartsWith("Your golem minion's");

        HackModAndEffectText(ref ModText, ref EffectText);

        bool IsMod = false;
        if (EffectText == ModText)
            IsMod = true;

        AnalyzeText(abilityNameList, nameToKeyword, ModText, true, IsGolemAbility, out List <AbilityKeyword> ModAbilityList, out PgCombatEffectCollection ModCombatList, out List<AbilityKeyword> ModTargetAbilityList);
        AnalyzeText(abilityNameList, nameToKeyword, EffectText, IsMod, IsGolemAbility, out List <AbilityKeyword> EffectAbilityList, out PgCombatEffectCollection EffectCombatList, out List<AbilityKeyword> EffectTargetAbilityList);

        // Make mitigation more accurate
        for (int i = 0; i < ModCombatList.Count && i < EffectCombatList.Count; i++)
            if (ModCombatList[i].Keyword == CombatKeyword.AddMitigation && EffectCombatList[i].Keyword == CombatKeyword.AddMitigationDirect &&
                ModCombatList[i].Data.RawValue.HasValue && EffectCombatList[i].Data.RawValue.HasValue && ModCombatList[i].Data.Value == EffectCombatList[i].Data.Value &&
                ModCombatList[i].Data.RawIsPercent.HasValue && EffectCombatList[i].Data.RawIsPercent.HasValue && ModCombatList[i].Data.IsPercent == EffectCombatList[i].Data.IsPercent)
                ModCombatList[i].Keyword = CombatKeyword.AddMitigationDirect;

        // Hack for Animal Handling
        if ((ModAbilityList.Contains(AbilityKeyword.SicEm) || ModAbilityList.Contains(AbilityKeyword.CleverTrick)) &&
            EffectAbilityList.Count == 0 && ModTargetAbilityList.Count == 0 &&
            (EffectTargetAbilityList.Contains(AbilityKeyword.SicEm) || EffectTargetAbilityList.Contains(AbilityKeyword.CleverTrick)))
        {
            foreach (AbilityKeyword Keyword in ModAbilityList)
                ModTargetAbilityList.Add(Keyword);

            ModAbilityList.Clear();
        }

        // Hack for Animal Handling
        if (effect.AbilityKeywordList.Count == 1 && effect.AbilityKeywordList[0] == AbilityKeyword.StabledPet &&
            EffectAbilityList.Count == 0 &&
            EffectTargetAbilityList.Count == 0 &&
            ModAbilityList.Count == 0)
        {
            ModAbilityList.Add(AbilityKeyword.StabledPet);
        }

        // Hack for Look At My Hammer
        if (EffectTargetAbilityList.Count == 1 && EffectTargetAbilityList[0] == AbilityKeyword.LookAtMyHammer &&
            EffectCombatList.Count == 2 && EffectCombatList[0].Keyword == CombatKeyword.RestorePower && EffectCombatList[1].Keyword == CombatKeyword.EffectDuration &&
            ModCombatList.Count == 1 && ModCombatList[0].Keyword == CombatKeyword.RestorePower)
        {
            ModCombatList.Add(new PgCombatEffect() { Keyword = CombatKeyword.EffectDuration, Data = new PgNumericValue() { RawValue = EffectCombatList[1].Data.RawValue, RawIsPercent = EffectCombatList[1].Data.RawIsPercent } });
        }

        // Hack for Parry
        if (ModAbilityList.Count == 1 && ModAbilityList[0] == AbilityKeyword.OnlyParry &&
            EffectCombatList.Count == 1 && EffectCombatList[0].Keyword == CombatKeyword.WithinDistance &&
            ModCombatList.Count == 2 && ModCombatList[0].Keyword == CombatKeyword.WithinDistance)
        {
            PgCombatEffect CombatEffect = ModCombatList[1];
            ModCombatList.RemoveAt(1);
            ModCombatList.Insert(0, CombatEffect);
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
                    case CombatKeyword.AddRage:
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
            Description = powerSimpleEffect.Description,
            AbilityList = ModAbilityList,
            StaticCombatEffectList = StaticCombatEffectList,
            DynamicCombatEffectList = DynamicCombatEffectList,
            TargetAbilityList = ModTargetAbilityList,
        };

        // Such ugly hacks...
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
        if (effectText.EndsWith("for 20 seconds or until you teleport"))
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
        RemoveDecorativeText(ref modText, "damage (such as via Insect Egg implantation),", out _, ref IndexFound);
        RemoveDecorativeText(ref modText, "Your golem minion's", out _, ref IndexFound);

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

        //BasicTextReplace(ref modText, ref effectText, "Sic Em", "Sic 'Em");
        BasicTextReplace(ref modText, ref effectText, "physical (slashing, piercing, and crushing)", "Crushing, Slashing, or Piercing");
        BasicTextReplace(ref modText, ref effectText, "Animal Handling pets' healing abilities", "Pet Healing");
        BasicTextReplace(ref modText, ref effectText, "Animal Handling pets' basic attacks", "Pet base attack");
        BasicTextReplace(ref modText, ref effectText, "pets' basic attacks", "Pet base attack");
        BasicTextReplace(ref modText, ref effectText, "Pet basic attack", "Pet base attack");
        BasicTextReplace(ref modText, ref effectText, "roots or slows", "slow or root");
        BasicTextReplace(ref modText, ref effectText, "Incubated Spiders' Rage attacks", "Incubated Spiders Rage attacks");

        BasicTextReplace(ref modText, ref effectText, "Animal Handling pets'", "Animal Handling pets uuuuuuuuuuuuuuuuuuuuunused");
        BasicTextReplace(ref modText, ref effectText, "damage-over-time effects (if any)", "Damage over Time");
        BasicTextReplace(ref modText, ref effectText, "Fire damage no longer dispels Ice Armor", "Fire damage no longer dispels");
        BasicTextReplace(ref modText, ref effectText, "Fire damage no longer dispels your Ice Armor", "Fire damage no longer dispels");
        BasicTextReplace(ref modText, ref effectText, "Trick Foxes", "Trick Fox");
        //BasicTextReplace(ref modText, ref effectText, "Bun-Fu Blitz", "Bun-Fu Kick");
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

    private void AnalyzeText(List<string> abilityNameList, Dictionary<string, List<AbilityKeyword>> nameToKeyword, string text, bool isMod, bool isGolemAbility, out List<AbilityKeyword> extractedAbilityList, out PgCombatEffectCollection extractedCombatEffectList, out List<AbilityKeyword> extractedTargetAbilityList)
    {
        RemoveDecorationText(ref text);
        SimplifyGrammar(ref text);
        SimplifyRandom(ref text);

        int RemoveCount;

        if (isMod)
            ExtractAbilityList(abilityNameList, nameToKeyword, true, false, ref text, out extractedAbilityList, out RemoveCount);
        else
        {
            extractedAbilityList = new List<AbilityKeyword>();
            RemoveCount = 0;
        }

        ExtractAbilityList(abilityNameList, nameToKeyword, false, isGolemAbility, ref text, out extractedTargetAbilityList, out int TargetRemoveCount);

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

        // Hack for pet effect
        if (extractedCombatEffectList.Count > 1 && extractedCombatEffectList[0].Keyword == CombatKeyword.PetImmolation)
        {
            for (int j = 1; j < extractedCombatEffectList.Count; j++)
                if (extractedCombatEffectList[j].Keyword == CombatKeyword.DamageBoost)
                {
                    extractedCombatEffectList.Insert(j + 1, new PgCombatEffect() { Keyword = CombatKeyword.ApplyToPet, Data = new PgNumericValue() { } });
                    break;
                }
        }

        // Hack for Flashing Strike
        if (extractedAbilityList.Contains(AbilityKeyword.FlashingStrike))
        {
            bool IsTargetUndead = false;
            foreach (PgCombatEffect CombatEffect in extractedCombatEffectList)
                if (CombatEffect.Keyword == CombatKeyword.TargetUndead)
                    IsTargetUndead = true;

            if (IsTargetUndead)
            {
                foreach (PgCombatEffect CombatEffect in extractedCombatEffectList)
                    if (CombatEffect.Keyword == CombatKeyword.DamageBoost && CombatEffect.DamageType == GameDamageType.Internal_None)
                        CombatEffect.DamageType = GameDamageType.Nature;
            }
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
        RemoveDecorativeText(ref text, "damage (such as via Insect Egg implantation),", out _, ref IndexFound);
        RemoveDecorativeText(ref text, "Your golem minion's", out _, ref IndexFound);
        ReplaceCaseInsensitive(ref text, " (or armor if health is full)", "/Armor");
    }

    private void ExtractAbilityList(List<string> abilityNameList, Dictionary<string, List<AbilityKeyword>> nameToKeyword, bool limitParsing, bool isGolemAbility, ref string text, out List<AbilityKeyword> extractedAbilityList, out int removeCount)
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

        // Hack for golem abilities
        if (isGolemAbility)
        {
            if (ExtractedTable.Count == 1)
            {
                if (ExtractedTable.Values.Contains("Taunting Punch") ||
                    ExtractedTable.Values.Contains("Poison Bomb") ||
                    ExtractedTable.Values.Contains("Self Destruct") ||
                    ExtractedTable.Values.Contains("Doom Admixture") ||
                    ExtractedTable.Values.Contains("Healing Mist") ||
                    ExtractedTable.Values.Contains("Invigorating Mist") ||
                    ExtractedTable.Values.Contains("Rage Mist") ||
                    ExtractedTable.Values.Contains("Rage Acid Toss") ||
                    ExtractedTable.Values.Contains("Self Sacrifice") ||
                    ExtractedTable.Values.Contains("Healing Injection") ||
                    ExtractedTable.Values.Contains("Fire Balm"))
                    ExtractedTable.Clear();
            }
            else if (ExtractedTable.Count == 2)
            {
                if (ExtractedTable.Values.Contains("Rage Mist") && ExtractedTable.Values.Contains("Self Sacrifice"))
                    ExtractedTable.Clear();
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
        //ReplaceCaseInsensitive(ref text, "anf  ", "and ");
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
                else if (extractedCombatEffectList[i].DamageType == GameDamageType.Internal_None)
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

        if (text.Contains("Egg implantation"))
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
        if (sentence.Format == "Mitigate %f damage from #D attacks")
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

    private void CreateDamageTypePermutations(Dictionary<int, string> FoundTextMap, List<int> FoundTextKey, int startIndex, string previousPermutation, Dictionary<string, int> PossibleTextMap, int Value)
    {
        if (startIndex + 2 < FoundTextMap.Count)
        {
            for (int i = startIndex; i < FoundTextMap.Count; i++)
            {
                string NewPermutation = startIndex > 0 ? $"{previousPermutation}, {FoundTextMap[FoundTextKey[i]]}" : FoundTextMap[FoundTextKey[i]];
                CreateDamageTypePermutations(FoundTextMap, FoundTextKey, startIndex + 1, NewPermutation, PossibleTextMap, Value);
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
                    }
        }
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

            if (ItemPower.Key == "7301")
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

        AnalyzeText(abilityNameList, nameToKeyword, ModText, true, false, out List<AbilityKeyword> ModAbilityList, out PgCombatEffectCollection ModCombatList, out List<AbilityKeyword> ModTargetAbilityList);

        List<PgCombatEffectCollection> EffectCombatList = new List<PgCombatEffectCollection>();
        if (candidateEffectList != null)
        {
            foreach (PgEffect CandidateEffect in candidateEffectList)
            {
                string EffectText = CandidateEffect.Description;
                HackModText(ref EffectText);

                AnalyzeText(abilityNameList, nameToKeyword, EffectText, false, false, out _, out PgCombatEffectCollection EffectCombatCollection, out _);
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
            Description = powerSimpleEffect.Description,
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
                PgModEffect NewModEffect = new PgModEffect() { EffectKey = Item.Key, Description = Item.Description, AbilityList = ExtractedAbilityList, StaticCombatEffectList = ExtractedCombatEffectList };
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

        ExtractAbilityList(abilityNameList, nameToKeyword, false, false, ref Text, out extractedTargetAbilityList, out int TargetRemoveCount);

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
                case CombatKeyword.AddSprintPowerCost:
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
            CombatKeyword.AddSprintPowerCost,
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
}
