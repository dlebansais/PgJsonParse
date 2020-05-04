namespace PgBuilder
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;
    using System.Collections.ObjectModel;
    using PgJsonObjects;
    using Presentation;
    using System.Windows.Controls;
    using System.Windows;

    public class AbilitySlot : INotifyPropertyChanged
    {
        #region Init
        static AbilitySlot()
        {
            Assembly CurrentAssembly = Assembly.GetExecutingAssembly();
            string[] Names = CurrentAssembly.GetManifestResourceNames();

            using Stream ResourceStream = CurrentAssembly.GetManifestResourceStream("PgBuilder.Resources.default.png");
            DefaultAbilityImageSource = ImageConversion.IconStreamToImageSource(ResourceStream);
        }

        public AbilitySlot()
        {
            ModifierList = new List<AbilityModifier>()
            {
                PowerCost,
                ResetTime,
                Range,
                AoE,
                RageBoost,
                RageMultiplier,
            };

            AbilityTierList = null;
            Ability = null;
            AbilityName = null;
            Source = DefaultAbilityImageSource;

            ResetMods();
        }
        #endregion

        #region Properties
        public static ImageSource DefaultAbilityImageSource { get; }
        public AbilityTierList AbilityTierList { get; private set; }
        public IPgAbility Ability { get; private set; }
        public bool IsEmpty { get { return Ability == null; } }
        public string AbilityName { get; private set; }
        public ImageSource Source { get; private set; }
        public string AbilityDescription { get {return Ability?.Description; } }

        public string AbilityMinLevel { get { return Ability != null ? Ability.Level.ToString() : string.Empty; } }
        public bool? AbilityMinLevelModified { get { return null; } }

        public AbilityModifier PowerCost { get; } = new AbilityModifier((IPgAbility ability) => ability.PvE.PowerCost);
        public AbilityModifier ResetTime { get; } = new AbilityModifier((IPgAbility ability) => ability.ResetTime);
        public AbilityModifier Range { get; } = new AbilityModifier((IPgAbility ability) => ability.PvE.AoE == 0 ? ability.PvE.Range : 0);
        public AbilityModifier AoE { get; } = new AbilityModifier((IPgAbility ability) => ability.PvE.AoE);
        public AbilityModifier RageBoost { get; } = new AbilityModifier((IPgAbility ability) => ability.PvE.RageBoost);
        public AbilityModifierPercent RageMultiplier { get; } = new AbilityModifierPercent((IPgAbility ability) => ability.PvE.RageMultiplier * 100);
        private List<AbilityModifier> ModifierList;
        public bool HasOtherEffects { get { return OtherEffectList.Count > 0; } }

        public bool HasAbilityDamage{ get { return Ability != null && Ability.PvE.Damage != 0; } }
        public int ModifiedAbilityDamage { get { return Ability != null ? App.CalculateDamage(Ability.PvE.Damage, DeltaDamage, ModDamage, ModBaseDamage, ModCriticalDamage) : 0; } }
        public string AbilityDamage { get { return Ability != null ? ModifiedAbilityDamage.ToString() : string.Empty; } }
        public bool? AbilityDamageModified { get { return Ability != null ? App.IntModifier(ModifiedAbilityDamage - Ability.PvE.Damage) : null; } }
        private int DeltaDamage;
        private double ModDamage;
        private double ModBaseDamage;
        private double ModCriticalDamage;

        public string AbilityDamageType { get { return Ability != null ? Ability.DamageType.ToString() : string.Empty; } }
        public bool? AbilityDamageTypeModified { get { return null; } }
        
        public bool HasAbilityDamageVulnerable { get { return Ability != null && Ability.PvE.RawExtraDamageIfTargetVulnerable.HasValue; } }
        public string AbilityDamageVulnerable { get { return Ability != null ? Ability.PvE.ExtraDamageIfTargetVulnerable.ToString() : string.Empty; } }
        public bool? AbilityDamageVulnerableModified { get { return null; } }
        
        public string AbilityAccuracy { get { return Ability != null ? App.DoubleToString(Ability.PvE.Accuracy) : string.Empty; } }
        public bool? AbilityAccuracyModified { get { return null; } }

        public bool IsEpicAttack { get { return Ability != null && Ability.KeywordList.Contains(AbilityKeyword.EpicAttack); } }
        public string BasicAttackHealth { get { return $"+{App.DoubleToString(BasicAttackHealthModified)}"; } }
        public string BasicAttackArmor { get { return $"+{App.DoubleToString(BasicAttackArmorModified)}"; } }
        public string BasicAttackPower { get { return $"+{App.DoubleToString(BasicAttackPowerModified)}"; } }
        private double BasicAttackHealthModified;
        private double BasicAttackArmorModified;
        private double BasicAttackPowerModified;

        public bool IsBasicAttack { get { return Ability != null && Ability.KeywordList.Contains(AbilityKeyword.BasicAttack); } }

        public ObservableCollection<IPgEffect> OtherEffectList { get; } = new ObservableCollection<IPgEffect>();

        public List<AbilityTierList> CompatibleAbilityList { get; private set; }

        public ContextMenu AbilityContextMenu
        { 
            get 
            {
                ContextMenu Menu = new ContextMenu();

                List<string> AbilityNameList = new List<string>();
                Menu.Items.Clear();

                foreach (AbilityTierList TierListItem in CompatibleAbilityList)
                {
                    if (AbilityNameList.Contains(TierListItem.Name))
                        continue;
                    AbilityNameList.Add(TierListItem.Name);

                    string Name = TierListItem.Name;
                    MenuItem NewMenuItem = new MenuItem();
                    NewMenuItem.Header = Name;
                    NewMenuItem.IsChecked = TierListItem == AbilityTierList;

                    foreach (IPgAbility AbilityItem in TierListItem)
                    {
                        MenuItem NewSubmenuItem = new MenuItem();
                        NewSubmenuItem.Header = AbilityItem.Name;
                        NewSubmenuItem.IsChecked = AbilityItem == Ability;
                        NewSubmenuItem.Click += OnAbilityMenuClick;

                        NewMenuItem.Items.Add(NewSubmenuItem);
                    }

                    Menu.Items.Add(NewMenuItem);
                }

                Menu.Items.Add(new Separator());

                MenuItem MenuItemClear = new MenuItem();
                MenuItemClear.Header = "Clear";
                MenuItemClear.Click += OnClearAbility;
                MenuItemClear.DataContext = this;

                Menu.Items.Add(MenuItemClear);

                return Menu;
            }
        }

        private void OnAbilityMenuClick(object sender, RoutedEventArgs e)
        {
            AbilityMenuClicked?.Invoke(sender, e);
        }

        private void OnClearAbility(object sender, RoutedEventArgs e)
        {
            AbilityMenuCleared?.Invoke(sender, e);
        }

        public event RoutedEventHandler AbilityMenuClicked;
        public event RoutedEventHandler AbilityMenuCleared;
        #endregion

        #region Client Interface
        public void SetCompatibleAbilityList(List<AbilityTierList> compatibleAbilityList)
        {
            CompatibleAbilityList = compatibleAbilityList;
        }

        public void SetAbility(AbilityTierList abilityTierList, string iconFolder)
        {
            AbilityTierList = abilityTierList;
            Ability = abilityTierList.Source;
            ModifierList.ForEach((AbilityModifier modifier) => modifier.SetAbility(Ability));

            UpdateName();
            UpdateSource(iconFolder);
        }

        public void SetAbility(AbilityTierList abilityTierList, IPgAbility ability, string iconFolder)
        {
            Debug.Assert(abilityTierList.Contains(ability));

            AbilityTierList = abilityTierList;
            Ability = ability;
            ModifierList.ForEach((AbilityModifier modifier) => modifier.SetAbility(Ability));

            UpdateName();
            UpdateSource(iconFolder);
        }

        public void Reset()
        {
            AbilityTierList = null;
            Ability = null;
            ModifierList.ForEach((AbilityModifier modifier) => modifier.SetAbility(Ability));

            ResetName();
            ResetSource();
        }

        public static string CuteDigitStrippedName(IPgAbility ability)
        {
            string DigitStrippedName = ability.DigitStrippedName;
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
        #endregion

        #region Implementation
        private void UpdateName()
        {
            AbilityName = Ability.Name;
            NotifyPropertyChanged(nameof(AbilityName));
        }

        private void ResetName()
        {
            AbilityName = null;
            NotifyPropertyChanged(nameof(AbilityName));
        }

        private void UpdateSource(string iconFolder)
        {
            string IconFile = Path.Combine(iconFolder, $"icon_{Ability.IconId}.png");
            Source = ImageConversion.IconFileToImageSource(IconFile);
            NotifyPropertyChanged(nameof(Source));
        }

        private void ResetSource()
        {
            Source = DefaultAbilityImageSource;
            NotifyPropertyChanged(nameof(Source));
        }
        #endregion

        #region Mods
        public void ResetMods()
        {
            ModifierList.ForEach((AbilityModifier modifier) => modifier.Reset());

            DeltaDamage = 0; // Damage +X
            ModDamage = 1.0;   // Damage +(X*100)%
            ModBaseDamage = 1.0; // Base Damage +(X*100)%
            ModCriticalDamage = 1.0; // Critical Damage +(X*100)%
            BasicAttackHealthModified = 0;
            BasicAttackArmorModified = 0;
            BasicAttackPowerModified = 0;

            if (Ability != null)
                BasicAttackPowerModified += Ability.CombatRefreshBaseAmount;

            OtherEffectList.Clear();
            NotifyPropertyChanged(nameof(HasOtherEffects));
        }

        public void RecalculateMods(string key, float attributeEffect)
        {
            if (IsEmpty)
                return;

            if (Ability.Name == "Chill 6")
            {
            }

            switch (key)
            {
                case "COMBAT_REFRESH_HEALTH_DELTA":
                    BasicAttackHealthModified += attributeEffect;
                    break;
                case "COMBAT_REFRESH_ARMOR_DELTA":
                    BasicAttackArmorModified += attributeEffect;
                    break;
                case "COMBAT_REFRESH_POWER_DELTA":
                    BasicAttackPowerModified += attributeEffect;
                    break;
            }

            if (HasAttributeKey(Ability.AttributesThatModAmmoConsumeChanceList, key))
                RecalculateModAmmoConsumeChance(attributeEffect);

            if (HasAttributeKey(Ability.AttributesThatDeltaDelayLoopTimeList, key))
                RecalculateDeltaDelayLoopTime(attributeEffect);

            if (HasAttributeKey(Ability.AttributesThatDeltaPowerCostList, key))
                RecalculateDeltaPowerCost(attributeEffect);

            if (HasAttributeKey(Ability.AttributesThatDeltaResetTimeList, key))
                RecalculateDeltaResetTime(attributeEffect);

            if (HasAttributeKey(Ability.AttributesThatModPowerCostList, key))
                RecalculateModPowerCost(attributeEffect);

            if (HasAttributeKey(Ability.PvE.AttributesThatDeltaDamageList, key))
                RecalculateDeltaDamage(attributeEffect);

            if (HasAttributeKey(Ability.PvE.AttributesThatModDamageList, key))
                RecalculateModDamage(attributeEffect);

            if (HasAttributeKey(Ability.PvE.AttributesThatModBaseDamageList, key))
                RecalculateModBaseDamage(attributeEffect);

            if (HasAttributeKey(Ability.PvE.AttributesThatDeltaTauntList, key))
                RecalculateDeltaTaunt(attributeEffect);

            if (HasAttributeKey(Ability.PvE.AttributesThatModTauntList, key))
                RecalculateModTaunt(attributeEffect);

            if (HasAttributeKey(Ability.PvE.AttributesThatDeltaRageList, key))
                RecalculateDeltaRage(attributeEffect);

            if (HasAttributeKey(Ability.PvE.AttributesThatModRageList, key))
                RecalculateModRage(attributeEffect);

            if (HasAttributeKey(Ability.PvE.AttributesThatDeltaRangeList, key))
                RecalculateDeltaRange(attributeEffect);

            if (HasAttributeKey(Ability.PvE.AttributesThatDeltaAccuracyList, key))
                RecalculateDeltaAccuracy(attributeEffect);

            if (HasAttributeKey(Ability.PvE.AttributesThatModCritDamageList, key))
                RecalculateModCriticalDamage(attributeEffect);
        }

        private bool HasAttributeKey(IPgAttributeCollection attributeList, string key)
        {
            foreach (IPgAttribute Item in attributeList)
                if (Item.Key == key)
                    return true;

            return false;
        }

        private void RecalculateModAmmoConsumeChance(float attributeEffect)
        {
        }

        private void RecalculateDeltaDelayLoopTime(float attributeEffect)
        {
        }

        private void RecalculateDeltaPowerCost(float attributeEffect)
        {
        }

        private void RecalculateDeltaResetTime(float attributeEffect)
        {
        }

        private void RecalculateModPowerCost(float attributeEffect)
        {
        }

        private void RecalculateDeltaDamage(float attributeEffect)
        {
            DeltaDamage += (int)attributeEffect;
        }

        private void RecalculateModDamage(float attributeEffect)
        {
            ModDamage += attributeEffect;
        }

        private void RecalculateModBaseDamage(float attributeEffect)
        {
            ModBaseDamage += attributeEffect;
        }

        private void RecalculateDeltaTaunt(float attributeEffect)
        {
        }

        private void RecalculateModTaunt(float attributeEffect)
        {
        }

        private void RecalculateDeltaRage(float attributeEffect)
        {
        }

        private void RecalculateModRage(float attributeEffect)
        {
        }

        private void RecalculateDeltaRange(float attributeEffect)
        {
        }

        private void RecalculateDeltaDamageLast(float attributeEffect)
        {
        }

        private void RecalculateDeltaAccuracy(float attributeEffect)
        {
        }

        private void RecalculateModCriticalDamage(float attributeEffect)
        {
            ModCriticalDamage += attributeEffect;
        }

        public void AddEffect(ModEffect modEffect)
        {
            Debug.Assert(Ability != null);

            bool IsAbilityModified = false;
            foreach (AbilityKeyword Keyword in modEffect.AbilityList)
                if (Ability.KeywordList.Contains(Keyword))
                {
                    IsAbilityModified = true;
                    break;
                }

            if (!IsAbilityModified)
                return;

            List<CombatEffect> StaticCombatEffectList = modEffect.StaticCombatEffectList;
            foreach (CombatEffect Item in StaticCombatEffectList)
                AddEffect(modEffect, Item);

            string EffectKey = modEffect.EffectKey;

            if (EffectKey.Length == 0)
                return;

            IObjectDefinition EffectDefinition = ObjectList.Definitions[typeof(PgJsonObjects.Effect)];
            if (!EffectDefinition.ObjectTable.ContainsKey(EffectKey))
            {
                Debug.WriteLine($"Ignoring power effect: {EffectKey}");
                return;
            }

            IPgEffect Effect = (IPgEffect)EffectDefinition.ObjectTable[EffectKey];
            OtherEffectList.Add(Effect);

            NotifyPropertyChanged(nameof(HasOtherEffects));
        }

        public bool HasSituationalModifier(List<CombatEffect> combatEffectList)
        {
            foreach (CombatEffect Item in combatEffectList)
                switch (Item.Keyword)
                {
                    case CombatKeyword.ReflectOnBurst:
                        return true;
                    default:
                        break;
                }

            return false;
        }

        public void AddEffect(ModEffect modEffect, CombatEffect combatEffect)
        {
            if (HasSituationalModifier(modEffect.StaticCombatEffectList))
                return;

            switch (combatEffect.Keyword)
            {
                case CombatKeyword.AddPowerCost:
                    if (combatEffect.Data.IsValueSet)
                        PowerCost.AddValue(combatEffect.Data.Value);
                    break;

                case CombatKeyword.AddResetTimer:
                    if (combatEffect.Data.IsValueSet)
                        ResetTime.AddValue(combatEffect.Data.Value);
                    break;

                case CombatKeyword.AddRange:
                    if (combatEffect.Data.IsValueSet)
                        Range.AddValue(combatEffect.Data.Value);
                    break;

                case CombatKeyword.AddRage:
                    if (combatEffect.Data.IsValueSet)
                        if (combatEffect.Data.IsPercent)
                            RageMultiplier.AddValue(combatEffect.Data.Value);
                        else
                            RageBoost.AddValue(combatEffect.Data.Value);
                    break;

                case CombatKeyword.ZeroRage:
                    RageMultiplier.SetValueZero();
                    break;

                default:
                    break;
            }
        }

/*
DamageBoost
RestorePower
//AddPowerCost
RestoreHealthArmor
RestoreHealth
//AddResetTimer
//AddRange
TargetSubsequentAttacks
EffectDuration
AddChannelingTime
AnotherTrap
//AddRage
ChangeDamageType
RestoreArmor
AddMitigation
NextAttack
DealDirectHealthDamage
//ZeroRage
EffectDelay
Recurring
ActiveSkill
AddEvasionMelee
OnEvadeMelee
AddArmor
OnEvade
MitigateReflect
ReflectRate
MitigateReflectKick
AddTaunt
Combo7
ComboFinalStepDamageAndStun
TargetSelf
AddSprintSpeed
EffectRecurrence
EffectDurationMinute
AddMaxHealth
CombatRefreshRestoreHeatlth
RestoreHealthArmorPower
StunIncorporeal
ResetOtherAbilityTimer
DamageBoostAgainstSpecie
DealIndirectDamage
ZeroTaunt
ThickArmor
ReflectOnBurst
AddMitigationIndirect
ReflectOnAnyAttack
MaxStack
AddEvasionBurst
AddChanceToIgnoreKnockback
AddChanceToIgnoreStun
AboveRage
AddChanceToKnockdown
ApplyWithChance
RequireTwoKnives
RequireNoAggro
BaseDamageBoost
DrainHealth
DrainMax
DrainArmor
MaxOccurence
DrainAsArmor
ChanceToConsume
AddHealthRegen
Combo1
ComboFinalStepBurst
AddMaxArmor
Combo2
ComboFinalStepDamage
Combo3
Combo4
Stun
Combo5
Combo6
NotAttackedRecently
AddPowerRegen
AddPowerCostMax
ZeroPowerCost
AddChannelTime
AddIndirectVulnerability
AddVulnerability
ReflectKnockbackOnFirstMelee
ReflectOnMelee
ReflectOnRanged
ReflectMeleeIndirectDamage

 */
        #endregion

        #region Implementation of INotifyPropertyChanged
        /// <summary>
        /// Implements the PropertyChanged event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        internal void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "Default parameter is mandatory with [CallerMemberName]")]
        internal void NotifyThisPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
