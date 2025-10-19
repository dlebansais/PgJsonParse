namespace TranslatorCombatParserEx;

using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using PgObjects;
using Translator;

internal partial class CombatParserEx
{
    public void Analyze()
    {
        CountMods();
        FindAbilitiesWithMatchingEffect();
        FindPowersWithMatchingEffect();
        GetAbilityNames();
        AnalyzeMods();

        VerifyConsistency();
    }

    public void CountMods()
    {
        int TotalTierCount = 0;

        FilterValidPowers(ref TotalTierCount);
        AddItemPowers();
        FilterValidEffects();

        Debug.WriteLine($"{allEffectTable.Count} effects, {PowerSimpleEffectList.Count + PowerAttributeList.Count} powers, {PowerAttributeList.Count} with attribute, {PowerSimpleEffectList.Count} with description, Total: {TotalTierCount} mods");
    }

    private void FilterValidPowers(ref int totalTierCount)
    {
        foreach (string PowerKey in PowerObjectKeyList)
        {
            PgPower Power = PowerFromKey(PowerKey);
            FilterValidPowers(Power, ref totalTierCount);
        }
    }

    private void FilterValidPowers(PgPower power, ref int totalTierCount)
    {
        PgPowerTierCollection TierList = power.TierList;

        Debug.Assert(TierList.Count > 0);

        int AttributeCount = 0;
        int SimpleCount = 0;

        foreach (PgPowerTier PowerTier in TierList)
            CheckAttributeOrSimple(PowerTier, ref AttributeCount, ref SimpleCount);

        Debug.Assert(AttributeCount == 0 || SimpleCount == 0);

        if (!IsSlotCompatible(ValidSlotList, power))
            return;

        string Skill_Key = FromSkillKey(power.Skill_Key);

        Debug.Assert(Skill_Key != null &&
                     (Skill_Key.Length == 0 ||
                     IsSkillInList(SkillList, Skill_Key) ||
                     Skill_Key == "Gourmand" ||
                     Skill_Key == PgSkill.AnySkill.Key ||
                     Skill_Key == "Endurance" ||
                     Skill_Key == "ArmorPatching" ||
                     Skill_Key == "Bladesmithing" ||
                     Skill_Key == "ShamanicInfusion"), $"Unexpected skill: {Skill_Key}");

        if (Skill_Key != null && (Skill_Key.Length == 0 || Skill_Key == "Gourmand"))
            return;
        if (power.IsUnavailable)
            return;

        if (AttributeCount > 0)
        {
            if (PowerAttributeList.Contains(power))
                return;

            PowerAttributeList.Add(power);
        }
        else
        {
            if (PowerSimpleEffectList.Contains(power))
                return;

            PowerSimpleEffectList.Add(power);
        }

        totalTierCount += TierList.Count;
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

    private void AddItemPowers()
    {
        Dictionary<PgItem, List<PgItemEffectSimple>> ItemList = new();

        foreach (string ItemKey in ItemObjectKeyList)
        {
            PgItem Item = ItemFromKey(ItemKey);
            if (ValidSlotList.Contains(Item.EquipSlot))
            {
                List<PgItemEffectSimple> ItemEffects = new();
                foreach (var Effect in Item.EffectDescriptionList)
                    if (Effect is PgItemEffectSimple AsSimpleEffect)
                        ItemEffects.Add(AsSimpleEffect);

                if (ItemEffects.Count > 0)
                {
                    ItemList.Add(Item, ItemEffects);
                    int Index = 0;
                    foreach (PgItemEffectSimple SimpleEffect in ItemEffects)
                    {
                        PgPowerEffectSimple NewPowerEffect = new();
                        NewPowerEffect.Description = SimpleEffect.Description;
                        PgPowerTier NewTier = new();
                        NewTier.EffectList.Add(NewPowerEffect);
                        PgPower NewPower = new();
                        NewPower.Key = $"Item_{Item.Key}_{Index}";
                        NewPower.SlotList.Add(Item.EquipSlot);
                        NewPower.TierList.Add(NewTier);

                        PowerSimpleEffectList.Insert(0, NewPower);
                        Index++;
                    }
                }
            }
        }
    }

    private void FilterValidEffects()
    {
        foreach (string EffectKey in EffectObjectKeyList)
        {
            PgEffect Effect = EffectFromKey(EffectKey);
            unmatchedEffectList.Add(Effect);

            if (Effect.Key.Length <= 5 || Effect.Key[Effect.Key.Length - 3] != '0')
                continue;

            Debug.Assert(Effect.AbilityKeywordList.Count > 0);

            bool IsIntSuffix = int.TryParse(Effect.Key.Substring(Effect.Key.Length - 3), out int TierIndex);
            Debug.Assert(IsIntSuffix);
            Debug.Assert(TierIndex >= 1 && TierIndex < 30);

            string Subkey = Effect.Key.Substring(0, Effect.Key.Length - 3);
            string PowerKey = Subkey.Substring(Subkey.Length - 3);

            Dictionary<string, List<PgEffect>> SameKeyTable;
            if (allEffectTable.ContainsKey(PowerKey))
                SameKeyTable = allEffectTable[PowerKey];
            else
            {
                SameKeyTable = new();
                allEffectTable.Add(PowerKey, SameKeyTable);
            }

            List<PgEffect> SameKeyEffect;
            if (SameKeyTable.ContainsKey(Subkey))
                SameKeyEffect = SameKeyTable[Subkey];
            else
            {
                SameKeyEffect = new();
                SameKeyTable.Add(Subkey, SameKeyEffect);
            }

            SameKeyEffect.Add(Effect);
        }
    }

    private void FindAbilitiesWithMatchingEffect()
    {
        List<PgEffect> EffectList = new List<PgEffect>();
        foreach (string EffectKey in EffectObjectKeyList)
        {
            PgEffect Effect = EffectFromKey(EffectKey);
            if (Effect.Name.Length > 0)
                EffectList.Add(Effect);
        }

        foreach (PgAbility Ability in CombatAbilityList)
        {
            Ability.AssociatedEffectKeyTable.Clear(); // TODO: remove me when cleaning up
            FindAbilityWithMatchingEffect(Ability, EffectList);
        }
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
            foreach (PgAbility Item in CombatAbilityList)
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

                    case "Crushing Boost":
                        AddAssociatedEffect(ability, CombatKeyword.CrushingBoost, Effect);
                        StringToEnumConversion<CombatKeyword>.SetCustomParsedEnum(CombatKeyword.CrushingBoost);
                        break;

                    case "Sprint Boost":
                        AddAssociatedEffect(ability, CombatKeyword.SprintBoost, Effect);
                        StringToEnumConversion<CombatKeyword>.SetCustomParsedEnum(CombatKeyword.SprintBoost);
                        break;

                    case "Slashing Boost":
                        AddAssociatedEffect(ability, CombatKeyword.SlashingBoost, Effect);
                        StringToEnumConversion<CombatKeyword>.SetCustomParsedEnum(CombatKeyword.SlashingBoost);
                        break;

                    case "Healing Augment":
                        AddAssociatedEffect(ability, CombatKeyword.HealingAugment, Effect);
                        StringToEnumConversion<CombatKeyword>.SetCustomParsedEnum(CombatKeyword.HealingAugment);
                        break;

                    case "Armor Regeneration":
                        AddAssociatedEffect(ability, CombatKeyword.ArmorRegenerationAugment, Effect);
                        StringToEnumConversion<CombatKeyword>.SetCustomParsedEnum(CombatKeyword.ArmorRegenerationAugment);
                        break;

                    default:
                        Debug.WriteLine($"NEW: Additional Effect '{Effect.Name}' for ability '{ability.Name}'");
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
        List<int> KeyList;

        if (ability.AssociatedEffectKeyTable.ContainsKey(keyword))
            KeyList = ability.AssociatedEffectKeyTable[keyword];
        else
        {
            KeyList = new();
            ability.AssociatedEffectKeyTable.Add(keyword, KeyList);
        }

        int KeyInt = int.Parse(effect.Key);

        Debug.Assert(!KeyList.Contains(KeyInt));
        KeyList.Add(KeyInt);
    }

    private void FindPowersWithMatchingEffect()
    {
        foreach (PgPower Item in PowerSimpleEffectList)
            if (FindPowersWithMatchingEffect(Item, out List<PgEffect>? MatchingEffectList, out List<PgEffect>? CandidateSingleEffectList))
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

    private bool FindPowersWithMatchingEffect(PgPower power, out List<PgEffect>? matchingEffectList, out List<PgEffect>? candidateSingleEffectList)
    {
        if (FindPowersWithMatchingEffectAllTiers(power, out matchingEffectList))
        {
            candidateSingleEffectList = null;
            return true;
        }

        candidateSingleEffectList = FindMatchingEffectOneTier(power);
        return false;
    }

    private bool FindPowersWithMatchingEffectAllTiers(PgPower power, out List<PgEffect>? matchingEffectList)
    {
        matchingEffectList = null;

        string Key = power.Key;
        Debug.Assert(Key.Length >= 3);

        string PowerKey = Key.Substring(Key.Length - 3);
        if (!allEffectTable.TryGetValue(PowerKey, out Dictionary<string, List<PgEffect>> SameKeyTable))
            return false;

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
            {
                // Hack to avoid mixing shield and bard mods.
                if (EffectItem.Description.Contains("Bardic Blast"))
                    continue;

                foreach (AbilityKeyword Keyword in EffectItem.AbilityKeywordList)
                    if (!AbilityKeywordList.Contains(Keyword))
                        AbilityKeywordList.Add(Keyword);
            }
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

        if ((AbilityKeywordList.Contains(AbilityKeyword.Knife) || AbilityKeywordList.Contains(AbilityKeyword.KnifeCut)) && Skill_Key == "Knife")
            EffectIconList.Add(108);

        if (AbilityKeywordList.Contains(AbilityKeyword.Druid) && Skill_Key == "Druid")
            EffectIconList.Add(108);

        if (AbilityKeywordList.Contains(AbilityKeyword.HammerAttack) && Skill_Key == "Hammer")
            EffectIconList.Add(108);

        if (AbilityKeywordList.Contains(AbilityKeyword.Melee))
        {
            EffectIconList.Add(108);

            if (PowerIconList.Count == 0)
                PowerIconList.Add(108);
        }

        foreach (PgAbility AbilityItem in CombatAbilityList)
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
                    KeywordMatch = true;
                }
            }

            AbilityPetType PetType = AbilityItem.PetTypeTagRequirement;
            if (PetTypeToKeywordTable.ContainsKey(PetType) && AbilityKeywordList.Contains(PetTypeToKeywordTable[PetType]))
            {
                if (!PetTypeToKeywordTableUsed.Contains(PetType))
                    PetTypeToKeywordTableUsed.Add(PetType);
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

    private List<PgEffect> FindMatchingEffectOneTier(PgPower power)
    {
        List<string> MatchingKeyList = new List<string>();

        foreach (string EffectKey in EffectObjectKeyList)
        {
            PgEffect Item = EffectFromKey(EffectKey);

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

            if (GenericAbilityList.Contains(EffectAbilityKeyword))
            {
                if (!GenericAbilityListUsed.Contains(EffectAbilityKeyword))
                    GenericAbilityListUsed.Add(EffectAbilityKeyword);
                continue;
            }

            List<PgEffect> TierList = new List<PgEffect>() { Item };
            if (HasCommonIcon(power, TierList, out bool IsOneToOne))
                MatchingKeyList.Add(Key);
        }

        List<PgEffect> Result = new List<PgEffect>();
        foreach (string Key in MatchingKeyList)
        {
            PgEffect CandidateEffect = EffectFromKey(Key);
            Result.Add(CandidateEffect);
        }

        return Result;
    }

    private void GetAbilityNames()
    {
        Dictionary<AbilityKeyword, string> KeywordToName = new Dictionary<AbilityKeyword, string>();

        foreach (PgAbility Item in CombatAbilityList)
        {
            if (Item.Name.StartsWith("Ilth Hale"))
            {
            }

            Debug.Assert(Item.Skill_Key is not null);
            string Skill_Key = FromSkillKey(Item.Skill_Key);
            PgSkill AbilitySkill = AbilitySkillFromKey(Skill_Key);

            if (!IsSkillInList(SkillList, AbilitySkill.Key))
                continue;

            string Name = AbilityBaseName(Item);

            if (Name.Length > 0 && Name[0] == '(')
                continue;
            if (Name.EndsWith(" (Energy Bow)"))
                continue;
            if (Name.EndsWith(" (Orc)"))
                continue;
            if (Name.EndsWith(" 3B"))
                continue;
            if (Name.EndsWith(" 3+"))
                continue;
            if (Name.EndsWith(" #"))
                continue;
            if (Name.EndsWith(" (Purple)"))
                continue;
            if (Name == "Cold Protection" ||
                Name == "Webspin")
                continue;

            if (KnownBaseAbilityNameTable.ContainsKey(Name))
            {
                if (!KnownBaseAbilityNameTableUsed.Contains(Name))
                    KnownBaseAbilityNameTableUsed.Add(Name);
                Name = KnownBaseAbilityNameTable[Name];
            }

            if (!abilityNameList.Contains(Name))
            {
                List<AbilityKeyword> KeywordList = new List<AbilityKeyword>(Item.KeywordList);

                if (KeywordList.Count > 0)
                {
                    foreach (AbilityKeyword Keyword in KeywordIgnoreList)
                        if (KeywordList.Contains(Keyword))
                        {
                            if (!KeywordIgnoreListUsed.Contains(Keyword))
                                KeywordIgnoreListUsed.Add(Keyword);
                            KeywordList.Remove(Keyword);
                        }

                    if (KeywordList.Count == 0)
                    {
                        switch (Name)
                        {
                            case "Unquenchable Fury":
                                break;
                            default:
                                Debug.WriteLine($"{Name} has no keyword.");
                                break;
                        }
                    }
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
                            AbilityKeyword SpecificKeyword = AbilityKeyword.Internal_None;
                            foreach (AbilityKeyword Keyword in KeywordList)
                                if (AbilitySpecificKeywordList.Contains(Keyword))
                                {
                                    if (!AbilitySpecificKeywordListUsed.Contains(Keyword))
                                        AbilitySpecificKeywordListUsed.Add(Keyword);
                                    SpecificKeyword = Keyword;
                                    FinalMatchingKeyword = Keyword;
                                    break;
                                }

                            if (SpecificKeyword == AbilityKeyword.Internal_None)
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
                        }

                        if (KeywordToName.ContainsKey(FinalMatchingKeyword))
                        {
                            if (!Name.EndsWith(" (Orc)"))
                                Debug.WriteLine($"Keyword {FinalMatchingKeyword} for {Name} is already used by {KeywordToName[FinalMatchingKeyword]}.");
                        }
                        else
                        {
                            KeywordToName.Add(FinalMatchingKeyword, Name);

                            if (Item.Skill_Key == "SAnimalHandling")
                                AnimalHandlingKeywordList.Add(FinalMatchingKeyword);
                            if (Item.Skill_Key == "SNecromancy")
                                NecromancyKeywordList.Add(FinalMatchingKeyword);
                        }
                    }
                }

                abilityNameList.Add(Name);
            }
        }

        AddAbilityToNameList("Werewolf Claw");
        AddAbilityToNameList("Armor Wave");
        AddAbilityToNameList("Health Wave");
        AddAbilityToNameList("Power Wave");
        AddAbilityToNameList("Fire Walls'");
        AddAbilityToNameList("your Fire Wall");
        AddAbilityToNameList("Summoned Skeletal Swordsmen");
        AddAbilityToNameListAndTable(KeywordToName, AbilityKeyword.GolemTauntingPunch, "Taunting Punch");
        AddAbilityToNameListAndTable(KeywordToName, AbilityKeyword.PoisonBombToss, "Poison Bomb");
        AddAbilityToNameListAndTable(KeywordToName, AbilityKeyword.GolemSelfDestruct, "Self Destruct");
        AddAbilityToNameListAndTable(KeywordToName, AbilityKeyword.GolemRageAcidToss, "Rage Acid Toss");
        AddAbilityToNameListAndTable(KeywordToName, AbilityKeyword.GolemDoomAdmixture, "Doom Admixture");
        AddAbilityToNameListAndTable(KeywordToName, AbilityKeyword.GolemRageMist, "Rage Mist");
        AddAbilityToNameListAndTable(KeywordToName, AbilityKeyword.GolemInvigoratingMist, "Invigorating Mist");
        AddAbilityToNameListAndTable(KeywordToName, AbilityKeyword.GolemSelfSacrifice, "Self Sacrifice");
        AddAbilityToNameListAndTable(KeywordToName, AbilityKeyword.GolemFireBalm, "Fire Balm");
        AddAbilityToNameListAndTable(KeywordToName, AbilityKeyword.SummonedFireWall, "Fire Walls");
        AddAbilityToNameListAndTable(KeywordToName, AbilityKeyword.SummonSkeleton, "Summoned Skeletons");
        AddAbilityToNameListAndTable(KeywordToName, AbilityKeyword.SummonSkeletonArcherOrMage, "Summoned Skeletal Archers and Mages");

        foreach (KeyValuePair<AbilityKeyword, string> Entry in KeywordToName)
        {
            Debug.Assert(abilityNameList.Contains(Entry.Value));
            nameToKeyword.Add(Entry.Value, new List<AbilityKeyword>() { Entry.Key });
        }

        nameToKeyword.Add("Fire Walls'", new List<AbilityKeyword>() { AbilityKeyword.SummonedFireWall });
        nameToKeyword.Add("your Fire Wall", new List<AbilityKeyword>() { AbilityKeyword.SummonedFireWall });
        nameToKeyword.Add("Summoned Skeletal Swordsmen", new List<AbilityKeyword>() { AbilityKeyword.SummonSkeletonSwordsman });

        foreach (KeyValuePair<string, List<AbilityKeyword>> Entry in WideAbilityTable)
        {
            string Pattern = Entry.Key;
            List<AbilityKeyword> KeywordList = Entry.Value;

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
        if (IsBloodMistBurstAbility(ability.Name))
            return "Blood Mist Burst";

        string Result = ability.Name;
        bool HasDigit = false;

        while (Result.Length > 0)
        {
            char c = Result[Result.Length - 1];
            if (char.IsDigit(c))
                HasDigit = true;
            else if (c != '#' && (c != '-' || !HasDigit))
                break;

            Result = Result.Substring(0, Result.Length - 1);
        }

        Result = Result.Trim();

        return Result;
    }

    private static bool IsBloodMistBurstAbility(string name)
    {
        string pattern = @"^Blood Mist (\d+) Burst$";
        Match match = Regex.Match(name, pattern);
        return match.Success;
    }

    private void AddAbilityToNameList(string abilityName)
    {
        Debug.Assert(!abilityNameList.Contains(abilityName));

        abilityNameList.Add(abilityName);
    }

    private void AddAbilityToNameListAndTable(Dictionary<AbilityKeyword, string> keywordToName, AbilityKeyword keyword, string abilityName)
    {
        Debug.Assert(!abilityNameList.Contains(abilityName));

        abilityNameList.Add(abilityName);
        keywordToName.Add(keyword, abilityName);
    }

    private List<PgPower> PowerAttributeList = new();
    private List<PgPower> PowerSimpleEffectList = new();
    private Dictionary<string, Dictionary<string, List<PgEffect>>> allEffectTable = new();
    private Dictionary<PgPower, List<PgEffect>> powerToEffectTable = new();
    private List<PgPower> unmatchedPowerList = new();
    private List<PgEffect> unmatchedEffectList = new();
    private Dictionary<PgPower, List<PgEffect>> candidateEffectTable = new();
    private List<string> abilityNameList = new(); // TODO: check used
    private Dictionary<string, List<AbilityKeyword>> nameToKeyword = new(); // TODO: check used
    private static readonly List<AbilityKeyword> AnimalHandlingKeywordList = new();
    private static readonly List<AbilityKeyword> NecromancyKeywordList = new();
}
