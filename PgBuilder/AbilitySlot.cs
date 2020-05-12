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
    using System;
    using System.Globalization;

    public class AbilitySlot : INotifyPropertyChanged
    {
        #region Init
        static AbilitySlot()
        {
            Assembly CurrentAssembly = Assembly.GetExecutingAssembly();

            using Stream ResourceStream = CurrentAssembly.GetManifestResourceStream("PgBuilder.Resources.default.png");
            DefaultAbilityImageSource = ImageConversion.IconStreamToImageSource(ResourceStream);
        }

        public AbilitySlot()
        {
            ModifierList = new List<AbilityModifier>()
            {
                PowerCost,
                ResetTime,
                DelayLoopTime,
                Range,
                AoE,
                RageBoost,
                RageMultiplier,
                Accuracy,
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

        public AbilityModifier PowerCost { get; } = new AbilityModifier("PowerCost", (IPgAbility ability) => ability.PvE.PowerCost, DefaultDisplayHandler);
        public AbilityModifier ResetTime { get; } = new AbilityModifier("ResetTime", (IPgAbility ability) => ability.ResetTime, DefaultDisplayHandler);
        public AbilityModifier DelayLoopTime { get; } = new AbilityModifier("DelayLoopTime", (IPgAbility ability) => ability.DelayLoopTime, DefaultDisplayHandler);
        public AbilityModifier Range { get; } = new AbilityModifier("Range", (IPgAbility ability) => ability.PvE.AoE == 0 ? ability.PvE.Range : 0, RangeDisplayHandler);
        public AbilityModifier AoE { get; } = new AbilityModifier("AoE", (IPgAbility ability) => ability.PvE.AoE, DefaultDisplayHandler);
        public AbilityModifier RageBoost { get; } = new AbilityModifier("RageBoost", (IPgAbility ability) => ability.PvE.RageBoost, DefaultDisplayHandler);
        public AbilityModifierPercent RageMultiplier { get; } = new AbilityModifierPercent("RageMultiplier", 100, (IPgAbility ability) => ability.PvE.RageMultiplier * 100, DefaultDisplayHandler);
        public AbilityModifierPercent Accuracy { get; } = new AbilityModifierPercent("Accuracy", 0, (IPgAbility ability) => ability.PvE.Accuracy * 100, DefaultDisplayHandler);

        private List<AbilityModifier> ModifierList;
        public bool HasOtherEffects 
        { 
            get 
            {
                foreach (OtherEffect Item in OtherEffectList)
                    if (Item.IsDisplayed)
                        return true;

                return false;
            }
        }

        public static string DefaultDisplayHandler(double d)
        {
            return ((int)Math.Round(d)).ToString();
        }

        public static string RangeDisplayHandler(double d)
        {
            if (d > 4)
                return $"{Math.Round(d, 1).ToString(CultureInfo.InvariantCulture)} meters";
            else
                return "Melee";
        }

        public bool HasAbilityDamage { get { return HasAbilityNormalDamage || HasAbilityHealthDamage; } }
        public bool HasAbilityNormalDamage { get { return Ability != null && Ability.PvE.RawDamage != null; } }
        public bool HasAbilityHealthDamage { get { return Ability != null && Ability.PvE.RawHealthSpecificDamage != null; } }
        public int BaseValueDamage { get { return Ability != null ? (Ability.PvE.RawHealthSpecificDamage != null ? Ability.PvE.HealthSpecificDamage : Ability.PvE.Damage): 0; } }
        public int ModifiedAbilityDamage { get { return Ability != null ? App.CalculateDamage(BaseValueDamage, DeltaDamage, ModDamage, ModBaseDamage, BoostDamage, ModCriticalDamage) : 0; } }

        public string AbilityDamage { get { return Ability != null ? ModifiedAbilityDamage.ToString() : string.Empty; } }
        public bool? AbilityDamageModified { get { return Ability != null ? App.IntModifier(ModifiedAbilityDamage - BaseValueDamage) : null; } }
        private int DeltaDamage;
        private double ModDamage;
        private double ModBaseDamage;
        private double BoostDamage;
        private double ModCriticalDamage;

        public string AbilityDamageTypeString { get { return (Ability != null) ? TextMaps.DamageTypeTextMap[AbilityDamageType] : string.Empty; } }
        public DamageType AbilityDamageType { get { return ModifiedDamageType == DamageType.Internal_None ? Ability.DamageType : ModifiedDamageType; } }
        public bool AbilityDamageTypeModified { get { return ModifiedDamageType != DamageType.Internal_None; } }
        private DamageType ModifiedDamageType = DamageType.Internal_None;

        public bool HasAbilityDamageVulnerable { get { return Ability != null && Ability.PvE.RawExtraDamageIfTargetVulnerable.HasValue; } }
        public string AbilityDamageVulnerable { get { return Ability != null ? Ability.PvE.ExtraDamageIfTargetVulnerable.ToString() : string.Empty; } }
        public bool? AbilityDamageVulnerableModified { get { return null; } }

        public bool IsNiceAttack { get { return Ability != null && Ability.KeywordList.Contains(AbilityKeyword.NiceAttack); } }
        public bool IsCoreAttack { get { return Ability != null && Ability.KeywordList.Contains(AbilityKeyword.CoreAttack); } }
        public bool IsEpicAttack { get { return Ability != null && Ability.KeywordList.Contains(AbilityKeyword.EpicAttack); } }
        public bool IsSignatureDebuff { get { return Ability != null && Ability.KeywordList.Contains(AbilityKeyword.SignatureDebuff); } }
        public bool IsSignatureSupport { get { return Ability != null && Ability.KeywordList.Contains(AbilityKeyword.SignatureSupport); } }
        public bool IsMajorHeal { get { return Ability != null && Ability.KeywordList.Contains(AbilityKeyword.MajorHeal); } }
        public bool IsMinorHeal { get { return Ability != null && Ability.KeywordList.Contains(AbilityKeyword.MinorHeal); } }
        public bool IsSurvivalUtility { get { return Ability != null && Ability.KeywordList.Contains(AbilityKeyword.SurvivalUtility); } }

        public bool IsBasicAttack { get { return Ability != null && Ability.KeywordList.Contains(AbilityKeyword.BasicAttack); } }
        public string BasicAttackHealth { get { return $"+{App.DoubleToString(BasicAttackHealthModified)}"; } }
        public string BasicAttackArmor { get { return $"+{App.DoubleToString(BasicAttackArmorModified)}"; } }
        public string BasicAttackPower { get { return $"+{App.DoubleToString(BasicAttackPowerModified)}"; } }
        private double BasicAttackHealthModified;
        private double BasicAttackArmorModified;
        private double BasicAttackPowerModified;

        public bool IsDelayLoopOnlyUsedInCombat { get { return Ability != null && Ability.DelayLoopIsOnlyUsedInCombat; } }
        public bool IsConsumingAmmo { get { return Ability != null && Ability.AmmoDescription != null; } }
        public string AmmoName { get { return Ability != null ? Ability.AmmoDescription : string.Empty; } }

        public ObservableCollection<AbilitySlotSpecialValue> SpecialValueList { get; } = new ObservableCollection<AbilitySlotSpecialValue>();
        public ObservableCollection<string> SpecialEffectList { get; } = new ObservableCollection<string>();
        public ObservableCollection<OtherEffect> OtherEffectList { get; } = new ObservableCollection<OtherEffect>();

        public List<AbilityTierList> CompatibleAbilityList { get; private set; } = new List<AbilityTierList>();

        public ContextMenu AbilityContextMenu
        { 
            get 
            {
                ContextMenu Menu = new ContextMenu();

                if (CompatibleAbilityList.Count == 0)
                {
                    Menu.Visibility = Visibility.Collapsed;
                    return Menu;
                }

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
                    NewMenuItem.DataContext = this;

                    foreach (IPgAbility AbilityItem in TierListItem)
                    {
                        MenuItem NewSubmenuItem = new MenuItem();
                        NewSubmenuItem.Header = AbilityItem.Name;
                        NewSubmenuItem.IsChecked = AbilityItem == Ability;
                        NewSubmenuItem.Click += OnAbilityMenuClick;
                        NewSubmenuItem.DataContext = this;

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
                Menu.DataContext = this;

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
            UpdateSpecialEffects();
            UpdateOtherEffects();

            NotifyPropertiesChanged();
        }

        public void SetAbility(AbilityTierList abilityTierList, IPgAbility ability, string iconFolder)
        {
            Debug.Assert(abilityTierList.Contains(ability));

            AbilityTierList = abilityTierList;
            Ability = ability;
            ModifierList.ForEach((AbilityModifier modifier) => modifier.SetAbility(Ability));

            UpdateName();
            UpdateSource(iconFolder);
            UpdateSpecialEffects();
            UpdateOtherEffects();

            NotifyPropertiesChanged();
        }

        public void Reset()
        {
            AbilityTierList = null;
            Ability = null;
            ModifierList.ForEach((AbilityModifier modifier) => modifier.SetAbility(Ability));

            ResetName();
            ResetSource();
            ResetSpecialEffects();
            ResetOtherEffects();

            NotifyPropertiesChanged();
        }

        public void NotifyPropertiesChanged()
        {
            NotifyPropertyChanged(nameof(Ability));
            NotifyPropertyChanged(nameof(AbilityDescription));
            NotifyPropertyChanged(nameof(AbilityMinLevel));
            NotifyPropertyChanged(nameof(AbilityMinLevelModified));
            NotifyPropertyChanged(nameof(PowerCost));
            NotifyPropertyChanged(nameof(ResetTime));
            NotifyPropertyChanged(nameof(DelayLoopTime));
            NotifyPropertyChanged(nameof(IsDelayLoopOnlyUsedInCombat));
            NotifyPropertyChanged(nameof(Range));
            NotifyPropertyChanged(nameof(AoE));
            NotifyPropertyChanged(nameof(RageBoost));
            NotifyPropertyChanged(nameof(RageMultiplier));
            NotifyPropertyChanged(nameof(Accuracy));

            NotifyPropertyChanged(nameof(HasOtherEffects));

            NotifyPropertyChanged(nameof(HasAbilityDamage));
            NotifyPropertyChanged(nameof(HasAbilityNormalDamage));
            NotifyPropertyChanged(nameof(HasAbilityHealthDamage));
            NotifyPropertyChanged(nameof(ModifiedAbilityDamage));
            NotifyPropertyChanged(nameof(AbilityDamage));
            NotifyPropertyChanged(nameof(AbilityDamageModified));

            NotifyPropertyChanged(nameof(AbilityDamageType));
            NotifyPropertyChanged(nameof(AbilityDamageTypeString));
            NotifyPropertyChanged(nameof(AbilityDamageTypeModified));

            NotifyPropertyChanged(nameof(HasAbilityDamageVulnerable));
            NotifyPropertyChanged(nameof(AbilityDamageVulnerable));
            NotifyPropertyChanged(nameof(AbilityDamageVulnerableModified));

            NotifyPropertyChanged(nameof(IsNiceAttack));
            NotifyPropertyChanged(nameof(IsCoreAttack));
            NotifyPropertyChanged(nameof(IsEpicAttack));
            NotifyPropertyChanged(nameof(IsSignatureDebuff));
            NotifyPropertyChanged(nameof(IsSignatureSupport));
            NotifyPropertyChanged(nameof(IsMajorHeal));
            NotifyPropertyChanged(nameof(IsMinorHeal));
            NotifyPropertyChanged(nameof(IsSurvivalUtility));

            NotifyPropertyChanged(nameof(IsBasicAttack));
            NotifyPropertyChanged(nameof(BasicAttackHealth));
            NotifyPropertyChanged(nameof(BasicAttackArmor));
            NotifyPropertyChanged(nameof(BasicAttackPower));
            NotifyPropertyChanged(nameof(BasicAttackHealthModified));
            NotifyPropertyChanged(nameof(BasicAttackArmorModified));
            NotifyPropertyChanged(nameof(BasicAttackPowerModified));

            NotifyPropertyChanged(nameof(AbilityContextMenu));
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

        private void UpdateSpecialEffects()
        {
            SpecialValueList.Clear();

            IList<IPgSpecialValue> AbilitySpecialValueList = Parser.GetSpecialValueList(Ability);
            foreach (IPgSpecialValue Item in AbilitySpecialValueList)
            {
                string Label = Item.Label;
                string Suffix = Item.Suffix;
                double BaseValue = Item.Value;
                bool DisplayAsPercent = Item.DisplayAsPercent;
                bool SkipIfZero = Item.SkipIfZero;

                List<string> AttributesThatDeltaList = ToKeyList(Item.AttributesThatDeltaList);
                List<string> AttributesThatModList = ToKeyList(Item.AttributesThatModList);
                List<string> AttributesThatModBaseList = ToKeyList(Item.AttributesThatModBaseList);
                AbilitySlotSpecialValue NewSpecialValue = new AbilitySlotSpecialValue(Label, Suffix, BaseValue, DisplayAsPercent, SkipIfZero, AttributesThatDeltaList, AttributesThatModList, AttributesThatModBaseList);
                SpecialValueList.Add(NewSpecialValue);
            }

            SpecialEffectList.Clear();

            if (Ability.SpecialInfo != null)
                SpecialEffectList.Add($"Special:  {Ability.SpecialInfo}");

            if (Ability.CanBeOnSidebar && Ability.Skill != null)
            {
                string SkillName = Ability.Skill.Name;
                SpecialEffectList.Add($"Special:  This ability can optionally be placed on your side-bar instead of your primary ability bars, but you must have the {SkillName} skill active to use it.");
            }
        }

        private void UpdateOtherEffects()
        {
            BasicAttackPowerModified += Ability.CombatRefreshBaseAmount;

            IList<IPgDoT> DoTList = Ability.PvE.DoTList;
            foreach (IPgDoT Item in DoTList)
                OtherEffectList.Add(new OtherEffectDoT(Ability.Name, Item));
        }

        private List<string> ToKeyList(IPgAttributeCollection attributeCollection)
        {
            List<string> Result = new List<string>();

            foreach (IPgAttribute Item in attributeCollection)
                Result.Add(Item.Key);

            return Result;
        }

        private void ResetSpecialEffects()
        {
            SpecialValueList.Clear();
            SpecialEffectList.Clear();
        }

        private void ResetOtherEffects()
        {
            OtherEffectList.Clear();
        }
        #endregion

        #region Mods
        public void ResetMods()
        {
            ModifierList.ForEach((AbilityModifier modifier) => modifier.Reset());

            DeltaDamage = 0;
            ModDamage = 0;
            ModBaseDamage = 0;
            BoostDamage = 0;
            ModCriticalDamage = 1.0;
            BasicAttackHealthModified = 0;
            BasicAttackArmorModified = 0;
            BasicAttackPowerModified = 0;
            ModifiedDamageType = DamageType.Internal_None;

            OtherEffectList.Clear();

            if (Ability != null)
            {
                BasicAttackPowerModified += Ability.CombatRefreshBaseAmount;

                IList<IPgDoT> DoTList = Ability.PvE.DoTList;
                foreach (IPgDoT Item in DoTList)
                    OtherEffectList.Add(new OtherEffectDoT(Ability.Name, Item));
            }

            foreach (OtherEffect OtherEffect in OtherEffectList)
                OtherEffect.Reset();

            foreach (AbilitySlotSpecialValue SpecialValue in SpecialValueList)
                SpecialValue.Reset();
        }

        public void RecalculateMiscMods(int loreLevel)
        {
            IObjectDefinition SkillDefinition = ObjectList.Definitions[typeof(PgJsonObjects.Skill)];
            if (!SkillDefinition.ObjectTable.ContainsKey("Lore"))
                return;
            IPgSkill LoreSkill = SkillDefinition.ObjectTable["Lore"] as IPgSkill;
            IPgAdvancementTable LoreAdvancementTable = LoreSkill.AdvancementTable;

            foreach (KeyValuePair<int, IPgAdvancement> Entry in LoreAdvancementTable.LevelTable)
                if (Entry.Key <= loreLevel)
                {
                    IPgAdvancement Advancement = Entry.Value;

                    foreach (string Text in Advancement.DirectModList)
                        RecalculateMiscModDirect(Text);
                    foreach (string Text in Advancement.IndirectModList)
                        RecalculateMiscModIndirect(Text);
                }
        }

        public void RecalculateMiscModDirect(string text)
        {
            int EndDamageNameIndex = text.IndexOf(" Direct Damage");
            int DataIndex = text.LastIndexOf(" ");

            if (EndDamageNameIndex >= 0 && DataIndex > EndDamageNameIndex && text.EndsWith("%"))
            {
                if (ParseAttributeDamage(text.Substring(0, EndDamageNameIndex), out DamageType DamageType))
                    if (AbilityDamageType == DamageType)
                    {
                        string DataText = text.Substring(DataIndex + 1, text.Length - DataIndex - 2);
                        if (AbilityDamageType == DamageType && float.TryParse(DataText, NumberStyles.Float, CultureInfo.InvariantCulture, out float Value))
                        {
                            ModDamage += Value;
                        }
                    }
            }
        }

        public void RecalculateMiscModIndirect(string text)
        {
            int EndDamageNameIndex = text.IndexOf(" Indirect Damage");
            int DataIndex = text.LastIndexOf(" ");

            if (EndDamageNameIndex >= 0 && DataIndex > EndDamageNameIndex && text.EndsWith("%"))
            {
                if (ParseAttributeDamage(text.Substring(0, EndDamageNameIndex), out DamageType DamageType))
                {
                    string DataText = text.Substring(DataIndex + 1, text.Length - DataIndex - 2);
                    if (float.TryParse(DataText, NumberStyles.Float, CultureInfo.InvariantCulture, out float Value))
                    {
                        foreach (OtherEffect Item in OtherEffectList)
                            if (Item is OtherEffectDoT AsDot && AsDot.DamageType == DamageType)
                                AsDot.AddBoostMultiplier(0.01);
                    }
                }
            }
        }

        public void RecalculateSuitMods(int metalArmorCount, int leatherArmorCount, int clothArmorCount, int organicArmorCount, bool isFairyCharacter)
        {
            if (metalArmorCount >= 3)
            {
                if (isFairyCharacter)
                {
                    if (metalArmorCount == 3)
                        ApplyMetalSuitPenalty(0.1);
                    else if (metalArmorCount == 4)
                        ApplyMetalSuitPenalty(0.15);
                    else
                        ApplyMetalSuitPenalty(0.2);
                }
                else
                    ApplyMetalSuitBonus();
            }
            else if (leatherArmorCount >= 3)
                ApplyLeatherSuitBonus();
            else if (clothArmorCount >= 3)
                ApplyClothSuitBonus();
            else if (organicArmorCount >= 3)
                ApplyOrganicSuitBonus();
        }

        public void ApplyMetalSuitBonus()
        {
        }

        public void ApplyMetalSuitPenalty(double penalty)
        {
            ModDamage -= penalty;
        }

        public void ApplySuitEffect(string key)
        {
            IObjectDefinition EffectDefinition = ObjectList.Definitions[typeof(PgJsonObjects.Effect)];

            if (!EffectDefinition.ObjectTable.ContainsKey(key))
            {
                Debug.WriteLine("SUIT EFFECT NOT FOUND!");
                return;
            }

            IPgEffect SuitEffect = (IPgEffect)EffectDefinition.ObjectTable[key];
            OtherEffectList.Add(new OtherEffectSimple(SuitEffect));
        }

        public void ApplyLeatherSuitBonus()
        {
            if (Ability == null || !Ability.KeywordList.Contains(AbilityKeyword.BasicAttack))
                return;

            ApplySuitEffect("effect_13306");
        }

        public void ApplyClothSuitBonus()
        {
            if (Ability == null || !Ability.KeywordList.Contains(AbilityKeyword.BasicAttack))
                return;

            ApplySuitEffect("effect_13305");
        }

        public void ApplyOrganicSuitBonus()
        {
            if (Ability == null || !Ability.KeywordList.Contains(AbilityKeyword.BasicAttack))
                return;

            RecalculateDeltaResetTime(-3.0);
        }

        public bool RecalculateSpecificAttributes(string key, float attributeEffect)
        {
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
                case "ACCURACY_BOOST":
                    RecalculateDeltaAccuracy(attributeEffect * 100);
                    break;
                case "BOOST_UNIVERSAL_DIRECT":
                    RecalculateDeltaDamage(attributeEffect);
                    break;
                case "BOOST_UNIVERSAL_INDIRECT":
                    IndirectDeltaDamage(attributeEffect);
                    break;
                case "BOOST_UNIVERSAL":
                    RecalculateDeltaDamage(attributeEffect);
                    IndirectDeltaDamage(attributeEffect);
                    break;

                default:
                    return false;
            }

            return true;
        }

        public bool ParseAttributeDamage(string text, out DamageType damageType)
        {
            foreach (KeyValuePair<DamageType, string> Entry in TextMaps.DamageTypeTextMap)
                if (Entry.Value.ToUpperInvariant() == text.ToUpperInvariant())
                {
                    damageType = Entry.Key;
                    return true;
                }

            damageType = DamageType.Internal_None;
            return false;
        }

        public bool RecalculateGenericAttributes(string key, float attributeEffect)
        {
            DamageType DamageType;

            if (key.StartsWith("MOD_"))
            {
                if (key.EndsWith("_DIRECT"))
                {
                    if (ParseAttributeDamage(key.Substring(4, key.Length - 7 - 4), out DamageType))
                    {
                        if (DamageType == AbilityDamageType)
                            RecalculateModDamage(attributeEffect);
                    }
                }
                else if (key.EndsWith("_INDIRECT"))
                {
                    if (ParseAttributeDamage(key.Substring(4, key.Length - 9 - 4), out DamageType))
                    {
                        IndirectModDamage(attributeEffect, DamageType);
                    }
                }
                else if (ParseAttributeDamage(key.Substring(4, key.Length - 4), out DamageType))
                {
                    if (DamageType == AbilityDamageType)
                        RecalculateModDamage(attributeEffect);

                    IndirectModDamage(attributeEffect, DamageType);
                }
            }
            else if (key.StartsWith("BOOST_"))
            {
                if (key.EndsWith("_DIRECT"))
                {
                    if (ParseAttributeDamage(key.Substring(6, key.Length - 7 - 6), out DamageType))
                    {
                        DirectDeltaDamage(attributeEffect);
                    }
                }
                else if (key.EndsWith("_INDIRECT"))
                {
                    if (ParseAttributeDamage(key.Substring(6, key.Length - 9 - 6), out DamageType))
                    {
                        if (DamageType == AbilityDamageType)
                            IndirectDeltaDamage(attributeEffect);
                    }
                }
                else if (ParseAttributeDamage(key.Substring(6, key.Length - 6), out DamageType))
                {
                    if (DamageType == AbilityDamageType)
                    {
                        DirectDeltaDamage(attributeEffect);
                        IndirectDeltaDamage(attributeEffect);
                    }
                }
            }

            return false;
        }

        public void RecalculateMods(string key, float attributeEffect)
        {
            if (IsEmpty)
                return;

            if (RecalculateSpecificAttributes(key, attributeEffect))
                return;

            bool IsHandled = false;

            if (HasAttributeKey(Ability.AttributesThatModAmmoConsumeChanceList, key))
            {
                RecalculateModAmmoConsumeChance(attributeEffect);
                IsHandled = true;
            }

            if (HasAttributeKey(Ability.AttributesThatDeltaDelayLoopTimeList, key))
            {
                RecalculateDeltaDelayLoopTime(attributeEffect);
                IsHandled = true;
            }

            if (HasAttributeKey(Ability.AttributesThatDeltaPowerCostList, key))
            {
                RecalculateDeltaPowerCost(attributeEffect);
                IsHandled = true;
            }

            if (HasAttributeKey(Ability.AttributesThatDeltaResetTimeList, key))
            {
                RecalculateDeltaResetTime(attributeEffect);
                IsHandled = true;
            }

            if (HasAttributeKey(Ability.AttributesThatModPowerCostList, key))
            {
                RecalculateModPowerCost(attributeEffect);
                IsHandled = true;
            }

            if (HasAttributeKey(Ability.PvE.AttributesThatDeltaDamageList, key))
            {
                RecalculateDeltaDamage(attributeEffect);
                IsHandled = true;
            }

            if (HasAttributeKey(Ability.PvE.AttributesThatModDamageList, key))
            {
                RecalculateModDamage(attributeEffect);
                IsHandled = true;
            }

            if (HasAttributeKey(Ability.PvE.AttributesThatModBaseDamageList, key))
            {
                RecalculateModBaseDamage(attributeEffect);
                IsHandled = true;
            }

            if (HasAttributeKey(Ability.PvE.AttributesThatDeltaTauntList, key))
            {
                RecalculateDeltaTaunt(attributeEffect);
                IsHandled = true;
            }

            if (HasAttributeKey(Ability.PvE.AttributesThatModTauntList, key))
            {
                RecalculateModTaunt(attributeEffect);
                IsHandled = true;
            }

            if (HasAttributeKey(Ability.PvE.AttributesThatDeltaRageList, key))
            {
                RecalculateDeltaRage(attributeEffect);
                IsHandled = true;
            }

            if (HasAttributeKey(Ability.PvE.AttributesThatModRageList, key))
            {
                RecalculateModRage(attributeEffect * 100);
                IsHandled = true;
            }

            if (HasAttributeKey(Ability.PvE.AttributesThatDeltaRangeList, key))
            {
                RecalculateDeltaRange(attributeEffect);
                IsHandled = true;
            }

            if (HasAttributeKey(Ability.PvE.AttributesThatDeltaAccuracyList, key))
            {
                RecalculateDeltaAccuracy(attributeEffect);
                IsHandled = true;
            }

            if (HasAttributeKey(Ability.PvE.AttributesThatModCritDamageList, key))
            {
                RecalculateModCriticalDamage(attributeEffect);
                IsHandled = true;
            }

            foreach (AbilitySlotSpecialValue Item in SpecialValueList)
            {
                if (Item.AttributesThatDeltaList.Contains(key))
                {
                    Item.AddDelta(attributeEffect);
                    IsHandled = true;
                }
            }

            if (RecalculateGenericAttributes(key, attributeEffect))
            {
                if (IsHandled)
                    Debug.WriteLine("ATTRIBUTE HANDLED TWICE!");
            }
        }

        private bool HasAttributeKey(IPgAttributeCollection attributeList, string key)
        {
            foreach (IPgAttribute Item in attributeList)
                if (Item.Key == key)
                    return true;

            return false;
        }

        private void RecalculateModAmmoConsumeChance(double attributeEffect)
        {
        }

        private void RecalculateDeltaDelayLoopTime(double attributeEffect)
        {
            DelayLoopTime.AddValue(attributeEffect);
        }

        private void RecalculateDeltaPowerCost(double attributeEffect)
        {
            PowerCost.AddValue(attributeEffect);
        }

        private void RecalculateModPowerCost(double attributeEffect)
        {
            PowerCost.SetMultiplier(attributeEffect);
        }

        private void RecalculateDeltaResetTime(double attributeEffect)
        {
            ResetTime.AddValue(attributeEffect);
        }

        private void RecalculateDeltaDamage(double attributeEffect)
        {
            DeltaDamage += (int)attributeEffect;
        }

        private void IndirectDeltaDamage(double attributeEffect)
        {
            foreach (OtherEffect Item in OtherEffectList)
                if (Item is OtherEffectDoT AsDot)
                    AsDot.AddDelta(attributeEffect);
        }

        private void DirectDeltaDamage(double attributeEffect)
        {
            BoostDamage += attributeEffect;
        }

        private void IndirectModDamage(double attributeEffect, DamageType damagetype)
        {
            foreach (OtherEffect Item in OtherEffectList)
                if (Item is OtherEffectDoT AsDot && AsDot.DamageType == damagetype)
                    AsDot.AddMultiplier(attributeEffect);
        }

        private void RecalculateModDamage(double attributeEffect)
        {
            ModDamage += attributeEffect;
        }

        private void RecalculateModBaseDamage(double attributeEffect)
        {
            ModBaseDamage += attributeEffect;
        }

        private void RecalculateDeltaDamageLast(double attributeEffect)
        {
        }

        private void RecalculateModCriticalDamage(double attributeEffect)
        {
            ModCriticalDamage += attributeEffect;
        }

        private void RecalculateDeltaTaunt(double attributeEffect)
        {
        }

        private void RecalculateModTaunt(double attributeEffect)
        {
        }

        private void RecalculateDeltaRage(double attributeEffect)
        {
            RageBoost.AddValue(attributeEffect);
        }

        private void RecalculateModRage(double attributeEffect)
        {
            RageMultiplier.AddValue(attributeEffect);
        }

        private void RecalculateDeltaRange(double attributeEffect)
        {
            Range.AddValue(attributeEffect);
        }

        private void RecalculateDeltaAccuracy(double attributeEffect)
        {
            Accuracy.AddValue(attributeEffect);
        }

        public void AddEffect(ModEffect modEffect)
        {
            if (Ability == null)
                return;

            List<CombatEffect> StaticCombatEffectList = modEffect.StaticCombatEffectList;

            if (Ability.KeywordList.Contains(AbilityKeyword.BasicAttack))
            {
                foreach (CombatEffect Item in StaticCombatEffectList)
                    if (Item.Keyword == CombatKeyword.CombatRefreshRestoreHeatlth)
                    {
                        if (Item.Data.IsValueSet)
                            BasicAttackHealthModified += Item.Data.Value;
                        break;
                    }
            }

            bool IsAbilityModified = false;
            foreach (AbilityKeyword Keyword in modEffect.AbilityList)
                if (Ability.KeywordList.Contains(Keyword))
                {
                    IsAbilityModified = true;
                    break;
                }

            if (!IsAbilityModified)
                return;

            int DamageBoostCount = 0;
            foreach (CombatEffect Item in StaticCombatEffectList)
                if (Item.Keyword == CombatKeyword.DamageBoost)
                    DamageBoostCount++;

            bool IsFirstDamageBoost = true;
            foreach (CombatEffect Item in StaticCombatEffectList)
                AddEffect(modEffect, Item, DamageBoostCount, ref IsFirstDamageBoost);

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
            OtherEffectList.Add(new OtherEffectSimple(Effect));
        }

        public void RecalculateModEnd()
        {
            if (Ability != null && Ability.DamageType != DamageType.Fire && ModifiedDamageType == DamageType.Fire)
                RageMultiplier.SetMultiplier(2.0);
            else
                RageMultiplier.SetMultiplier(1.0);

            NotifyPropertiesChanged();
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

        public bool AddEffectToSpecialValueDelta(CombatKeyword combatKeyword, double deltaValue, bool hasRecurrence)
        {
            List<KeyValuePair<string, string>> VerificationTable;

            if (Parser.EffectVerificationTable.ContainsKey(combatKeyword))
                VerificationTable = Parser.EffectVerificationTable[combatKeyword];
            else
                VerificationTable = new List<KeyValuePair<string, string>>();

            bool IsFound = false;
            foreach (AbilitySlotSpecialValue SpecialValue in SpecialValueList)
            {
                string Label = SpecialValue.Label;
                string Suffix = SpecialValue.Suffix;

                foreach (KeyValuePair<string, string> Entry in VerificationTable)
                    if (Label == Entry.Key && Suffix == Entry.Value)
                    {
                        IsFound = true;

                        if (hasRecurrence && !(Suffix.Contains(" every ") || Suffix.Contains("per second")))
                            IsFound = false;

                        break;
                    }

                if (IsFound)
                {
                    SpecialValue.AddDelta(deltaValue);
                    break;
                }
            }

            //if (!IsFound && VerificationTable.Count > 0)
            //    Debug.WriteLine("Special value not found!");

            return IsFound;
        }

        private DamageType ToDamageType(GameDamageType type)
        {
            if (type.HasFlag(GameDamageType.Crushing))
                return DamageType.Crushing;
            else if (type.HasFlag(GameDamageType.Slashing))
                return DamageType.Slashing;
            else if (type.HasFlag(GameDamageType.Nature))
                return DamageType.Nature;
            else if (type.HasFlag(GameDamageType.Fire))
                return DamageType.Fire;
            else if (type.HasFlag(GameDamageType.Cold))
                return DamageType.Cold;
            else if (type.HasFlag(GameDamageType.Piercing))
                return DamageType.Piercing;
            else if (type.HasFlag(GameDamageType.Piercing))
                return DamageType.Piercing;
            else if (type.HasFlag(GameDamageType.Psychic))
                return DamageType.Psychic;
            else if (type.HasFlag(GameDamageType.Trauma))
                return DamageType.Trauma;
            else if (type.HasFlag(GameDamageType.Electricity))
                return DamageType.Electricity;
            else if (type.HasFlag(GameDamageType.Poison))
                return DamageType.Poison;
            else if (type.HasFlag(GameDamageType.Acid))
                return DamageType.Acid;
            else if (type.HasFlag(GameDamageType.Darkness))
                return DamageType.Darkness;
            else
                return DamageType.Internal_None;
        }

        public void AddEffect(ModEffect modEffect, CombatEffect combatEffect, int damageBoostCount, ref bool isFirstDamageBoost)
        {
            if (HasSituationalModifier(modEffect.StaticCombatEffectList))
                return;

            CombatKeyword Keyword = combatEffect.Keyword;
            bool HasRecurrence;

            switch (Keyword)
            {
                case CombatKeyword.AddPowerCost:
                    if (combatEffect.Data.IsValueSet)
                        RecalculateDeltaPowerCost(combatEffect.Data.Value);
                    break;

                case CombatKeyword.AddResetTimer:
                    if (combatEffect.Data.IsValueSet)
                        RecalculateDeltaResetTime(combatEffect.Data.Value);
                    break;

                case CombatKeyword.AddRange:
                    if (combatEffect.Data.IsValueSet)
                        RecalculateDeltaRange(combatEffect.Data.Value);
                    break;

                case CombatKeyword.AddRage:
                    if (combatEffect.Data.IsValueSet)
                        if (combatEffect.Data.IsPercent)
                            RecalculateModRage(combatEffect.Data.Value);
                        else
                            RecalculateDeltaRage(combatEffect.Data.Value);
                    break;

                case CombatKeyword.ZeroRage:
                    RageMultiplier.SetValueZero();
                    break;

                case CombatKeyword.AddAccuracy:
                    if (combatEffect.Data.IsValueSet)
                        RecalculateDeltaAccuracy(combatEffect.Data.Value);
                    break;

                case CombatKeyword.ChangeDamageType:
                    if (combatEffect.DamageType != GameDamageType.None)
                        ModifiedDamageType = ToDamageType(combatEffect.DamageType);
                    break;

                case CombatKeyword.AddChannelingTime:
                    if (combatEffect.Data.IsValueSet)
                        RecalculateDeltaDelayLoopTime(combatEffect.Data.Value);
                    break;

                case CombatKeyword.CombatRefreshRestoreHeatlth:
                    break;

                case CombatKeyword.RestoreHealth:
                case CombatKeyword.RestorePower:
                case CombatKeyword.RestoreArmor:
                case CombatKeyword.RestoreHealthArmor:
                case CombatKeyword.RestoreHealthArmorPower:
                case CombatKeyword.TargetSubsequentAttacks:
                case CombatKeyword.AnotherTrap:
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
                case CombatKeyword.AddSprintSpeed:
                case CombatKeyword.EffectDurationMinute:
                case CombatKeyword.AddMaxHealth:
                case CombatKeyword.ResetOtherAbilityTimer:
                case CombatKeyword.AddMaxArmor:
                case CombatKeyword.DamageBoostAgainstSpecie:
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
                case CombatKeyword.DrainHealth:
                case CombatKeyword.DrainArmor:
                case CombatKeyword.DrainHealthMax:
                case CombatKeyword.DrainArmorMax:
                case CombatKeyword.DrainAsArmor:
                case CombatKeyword.MaxOccurence:
                case CombatKeyword.ChanceToConsume:
                case CombatKeyword.AddHealthRegen:
                case CombatKeyword.Combo1:
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
                    if (combatEffect.Data.IsValueSet)
                        if (!Parser.HasNonSpecialValueEffect(modEffect.StaticCombatEffectList, out HasRecurrence))
                            AddEffectToSpecialValueDelta(Keyword, combatEffect.Data.Value, HasRecurrence);
                    break;

                case CombatKeyword.DamageBoost:
                case CombatKeyword.DealIndirectDamage:
                    if (combatEffect.Data.IsValueSet)
                    {
                        bool IsHandled = false;
                        if (Keyword == CombatKeyword.DamageBoost && !Parser.HasNonSpecialValueEffect(modEffect.StaticCombatEffectList, out HasRecurrence))
                            IsHandled  = AddEffectToSpecialValueDelta(Keyword, combatEffect.Data.Value, HasRecurrence);

                        if (!IsHandled)
                            if (modEffect.AbilityList.Count > 0)
                            {
                                bool IsConcerned = false;
                                foreach (AbilityKeyword Item in modEffect.AbilityList)
                                    if (Ability.KeywordList.Contains(Item))
                                    {
                                        IsConcerned = true;
                                        break;
                                    }

                                if (IsConcerned)
                                {
                                    if (damageBoostCount < 2 || !isFirstDamageBoost)
                                    {
                                        DamageType DamageType = ToDamageType(combatEffect.DamageType);
                                        bool RequireNoAggro = false;

                                        foreach (CombatEffect Item in modEffect.StaticCombatEffectList)
                                            if (Item.Keyword == CombatKeyword.RequireNoAggro)
                                                RequireNoAggro = true;

                                        foreach (OtherEffect Item in OtherEffectList)
                                            if (Item is OtherEffectDoT AsDot && AsDot.IsDisplayable)
                                            {
                                                if ((AsDot.DamageType == DamageType || DamageType == DamageType.Internal_None) && AsDot.RequireNoAggro == RequireNoAggro)
                                                {
                                                    if (combatEffect.Data.IsPercent)
                                                        AsDot.AddBoostMultiplier(combatEffect.Data.Value / 100.0);
                                                    else
                                                        AsDot.AddDelta(combatEffect.Data.Value);
                                                    IsHandled = true;
                                                    break;
                                                }
                                            }
                                    }

                                    if (!IsHandled)
                                    {
                                        if (combatEffect.Data.IsPercent)
                                            RecalculateModDamage(combatEffect.Data.Value / 100.0);
                                        else
                                            RecalculateDeltaDamage(combatEffect.Data.Value);
                                    }
                                }
                            }

                        isFirstDamageBoost = false;
                    }
                    break;

                default:
                    //Debug.WriteLine($"Unexpected keyword in mod to add: {Keyword}");
                    break;
            }
        }
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
