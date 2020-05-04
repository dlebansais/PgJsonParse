﻿namespace PgBuilder
{
    using Microsoft.Win32;
    using PgJsonObjects;
    using PgJsonReader;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Threading;

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region Constants
        const int AnalyzedVersion = 334;
        #endregion

        #region Init
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            LoadCachedData(out int Version);
            FillSkillList();
            FillGearSlotList();
            FillAbilitySlotList();
            InitPowerKeyToCompleteEffectTable();

            if (Version > AnalyzedVersion)
            {
                Parser Parser = new Parser();

                List<ItemSlot> ValidSlotList = new List<ItemSlot>();
                foreach (GearSlot Item in GearSlotList)
                    ValidSlotList.Add(Item.Slot);

                Parser.AnalyzeCachedData(Version, ValidSlotList, SkillList, PowerKeyToCompleteEffectTable);
            }

            LoadSettings();

            Closed += OnClosed;
        }

        private void InitPowerKeyToCompleteEffectTable()
        {
            List<CombatKeyword> StaticKeywordList = new List<CombatKeyword>();
            IObjectDefinition PowerDefinition = ObjectList.Definitions[typeof(PgJsonObjects.Power)];
            List<string> StaticKeywordKeyList = new List<string>();

            foreach (KeyValuePair<string, List<AbilityKeyword>> Entry in PowerKeyToCompleteEffect.AbilityList)
            {
                string Key = Entry.Key;

                string EffectKey = PowerKeyToCompleteEffect.EffectKey[Key];
                List<AbilityKeyword> AbilityList = PowerKeyToCompleteEffect.AbilityList[Key];
                List<CombatEffect> StaticCombatEffectList = PowerKeyToCompleteEffect.StaticCombatEffectList[Key];
                List<CombatEffect> DynamicCombatEffectList = PowerKeyToCompleteEffect.DynamicCombatEffectList[Key];
                List<AbilityKeyword> TargetAbilityList = PowerKeyToCompleteEffect.TargetAbilityList[Key];

                ModEffect ModEffect = new ModEffect(EffectKey , AbilityList, StaticCombatEffectList, DynamicCombatEffectList, TargetAbilityList);
                PowerKeyToCompleteEffectTable.Add(Key, ModEffect);

                bool HasKeyword = false;
                foreach (CombatEffect Item in StaticCombatEffectList)
                    if (Item.Keyword == CombatKeyword.AddRage)
                        HasKeyword = true;

                if (HasKeyword)
                {
                    string PowerKey = Key.Substring(0, Key.LastIndexOf('_'));
                    if (!StaticKeywordKeyList.Contains(PowerKey))
                        StaticKeywordKeyList.Add(PowerKey);
                }
            }

            foreach (string PowerKey in StaticKeywordKeyList)
            {
                IPgPower Power = PowerDefinition.ObjectTable[PowerKey] as IPgPower;
                IList<IPgPowerTier> TierEffectList = Power.TierEffectList;
                IPgPowerTier PowerTier = TierEffectList[TierEffectList.Count - 1];
                IList<IPgPowerEffect> EffectList = PowerTier.EffectList;
                IPgPowerSimpleEffect SimpleEffect = (IPgPowerSimpleEffect)EffectList[0];
                string Description = SimpleEffect.Description;

                Debug.WriteLine(Description);
            }
        }

        private Dictionary<string, ModEffect> PowerKeyToCompleteEffectTable = new Dictionary<string, ModEffect>();
        #endregion

        #region Data Load
        public void LoadCachedData(out int version)
        {
            string UserRootFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string ApplicationFolder = Path.Combine(UserRootFolder, "PgJsonParse");
            string VersionCacheFolder = Path.Combine(ApplicationFolder, "Versions");

            version = 0;
            string[] VersionFolders = Directory.GetDirectories(VersionCacheFolder);
            foreach (string Item in VersionFolders)
                if (int.TryParse(Path.GetFileName(Item), out int VersionValue) && version < VersionValue)
                    version = VersionValue;

            string VersionFolder = Path.Combine(VersionCacheFolder, version.ToString());
            string IconCacheFolder = Path.Combine(ApplicationFolder, "Shared Icons");
            IconFolder = IconCacheFolder;

            LoadCachedData(VersionFolder, IconCacheFolder);
        }

        public void LoadCachedData(string versionFolder, string iconFolder)
        {
            try
            {
                string CacheFileName = Path.Combine(versionFolder, "cache.pg");
                if (File.Exists(CacheFileName))
                {
                    byte[] Data = LoadBinaryFile(CacheFileName);
                    DeserializeAll(versionFolder, iconFolder, Data);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        private void DeserializeAll(string versionFolder, string iconFolder, byte[] data)
        {
            SerializableJsonObject.ResetSerializedObjectTable();
            GenericPgObject.ResetCreatedObjectTable();
            byte[] CurrentOffset = new byte[4];

            List<IObjectDefinition> DefinitionList = new List<IObjectDefinition>();
            foreach (KeyValuePair<Type, IObjectDefinition> Entry in ObjectList.Definitions)
                DefinitionList.Add(Entry.Value);

            for (int ProgressIndex = 0; ProgressIndex < DefinitionList.Count; ProgressIndex++)
            {
                DeserializeAll0(data, CurrentOffset, DefinitionList, ProgressIndex);
            }

            DeserializeAll2(versionFolder, iconFolder, data);
        }

        private bool DeserializeAll0(byte[] data, byte[] currentOffset, List<IObjectDefinition> definitionList, int progressIndex)
        {
            try
            {
                IObjectDefinition Definition = definitionList[progressIndex];
                int Offset = BitConverter.ToInt32(currentOffset, 0);

                Definition.JsonObjectList.Clear();

                IMainPgObjectCollection PgObjectList = Definition.PgObjectList;
                PgObjectList.Clear();

                int Count = BitConverter.ToInt32(data, Offset);
                Offset += 4;

                int ObjectOffset = Offset;

                for (int i = 0; i < Count; i++)
                {
                    Offset = BitConverter.ToInt32(data, ObjectOffset + i * 4);

                    IMainPgObject Item = GenericPgObject.CreateMainObject(Definition.CreateNewObject, data, ref Offset);
                    PgObjectList.Add(Item);
                }

                Offset = BitConverter.ToInt32(data, ObjectOffset + Count * 4);
                Array.Copy(BitConverter.GetBytes(Offset), 0, currentOffset, 0, 4);

                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        private void DeserializeAll2(string versionFolder, string iconFolder, byte[] data)
        {
            foreach (KeyValuePair<Type, IObjectDefinition> Entry in ObjectList.Definitions)
            {
                IObjectDefinition Definition = Entry.Value;
                Dictionary<string, IJsonKey> ObjectTable = Definition.ObjectTable;
                IMainPgObjectCollection PgObjectList = Definition.PgObjectList;

                if (ObjectTable.Count == 0)
                    foreach (IJsonKey Item in PgObjectList)
                        ObjectTable.Add(Item.Key, Item);
            }

            Dictionary<string, IJsonKey> PowerTable = ObjectList.Definitions[typeof(PgJsonObjects.Power)].ObjectTable;
            Dictionary<string, IJsonKey> AttributeTable = ObjectList.Definitions[typeof(PgJsonObjects.Attribute)].ObjectTable;

            foreach (KeyValuePair<string, IJsonKey> Entry in PowerTable)
            {
                IPgPower Power = (IPgPower)Entry.Value;
                Power.InitTierList(AttributeTable);
            }
        }

        private static byte[] LoadBinaryFile(string fileName)
        {
            using FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            using BinaryReader br = new BinaryReader(fs);
            byte[] Result = br.ReadBytes((int)fs.Length);
            return Result;
        }

        public static string IconFolder { get; private set; }
        #endregion

        #region Properties
        public List<IPgSkill> SkillList { get; } = new List<IPgSkill>();
        public int SelectedSkill1 { get; private set; } = -1;
        public int SelectedSkill2 { get; private set; } = -1;
        public List<GearSlot> GearSlotList { get; } = new List<GearSlot>();
        public ObservableCollection<AbilitySlot> AbilitySlot1List { get; } = new ObservableCollection<AbilitySlot>();
        public ObservableCollection<AbilitySlot> AbilitySlot2List { get; } = new ObservableCollection<AbilitySlot>();
        public string LastBuildFile { get; private set; }
        public bool IsLargeView { get; private set; } = true;
        public bool IsSmallView { get { return !IsLargeView; } }

        public string TitleText
        {
            get
            {
                string Result = "Project: Gorgon - Builder";
                if (LastBuildFile != null)
                    Result += $" - {LastBuildFile}";

                return Result;
            }
        }
        #endregion

        #region Implementation
        private void FillSkillList()
        {
            IObjectDefinition PowerDefinition = ObjectList.Definitions[typeof(PgJsonObjects.Power)];
            IList<IPgPower> PowerList = (IList<IPgPower>)PowerDefinition.VerifiedObjectList;
            
            SkillList.Clear();

            foreach (IPgPower PowerItem in PowerList)
            {
                if (PowerItem.IsUnavailable)
                    continue;

                IList<IPgPowerTier> TierEffectList = PowerItem.TierEffectList;
                if (TierEffectList.Count == 0)
                    continue;

                int CombatGearSlotCount = 0;
                foreach (ItemSlot GearSlot in PowerItem.SlotList)
                    switch (GearSlot)
                    {
                        case ItemSlot.Internal_None:
                        case ItemSlot.None:
                            break;

                        default:
                            CombatGearSlotCount++;
                            break;
                    }

                if (CombatGearSlotCount == 0)
                    continue;

                if (PowerItem.RawSkill == PowerSkill.Internal_None ||
                    PowerItem.RawSkill == PowerSkill.AnySkill ||
                    PowerItem.RawSkill == PowerSkill.ArmorPatching ||
                    PowerItem.RawSkill == PowerSkill.Endurance ||
                    PowerItem.RawSkill == PowerSkill.ShamanicInfusion)
                    continue;

                if (!SkillList.Contains(PowerItem.Skill))
                    SkillList.Add(PowerItem.Skill);
            }

            SkillList.Sort(SortByName);
        }

        private static int SortByName(IPgSkill skill1, IPgSkill skill2)
        {
            return string.Compare(skill1.Name, skill2.Name, StringComparison.InvariantCulture);
        }

        private void FillGearSlotList()
        {
            GearSlotList.Add(new GearSlot("Main Hand", ItemSlot.MainHand));
            GearSlotList.Add(new GearSlot("Off Hand", ItemSlot.OffHand));
            GearSlotList.Add(new GearSlot("Head", ItemSlot.Head));
            GearSlotList.Add(new GearSlot("Chest", ItemSlot.Chest));
            GearSlotList.Add(new GearSlot("Legs", ItemSlot.Legs));
            GearSlotList.Add(new GearSlot("Hands", ItemSlot.Hands));
            GearSlotList.Add(new GearSlot("Feet", ItemSlot.Feet));
            GearSlotList.Add(new GearSlot("Neck", ItemSlot.Necklace));
            GearSlotList.Add(new GearSlot("Ring", ItemSlot.Ring));
            GearSlotList.Add(new GearSlot("Racial", ItemSlot.Racial));
            GearSlotList.Add(new GearSlot("Waist", ItemSlot.Waist));
        }

        private void ResetGearSlots()
        {
            IPgSkill Skill1 = SelectedSkill1 >= 0 ? SkillList[SelectedSkill1] : null;
            IPgSkill Skill2 = SelectedSkill2 >= 0 ? SkillList[SelectedSkill2] : null;

            foreach (GearSlot Item in GearSlotList)
                Item.Reset(Skill1, Skill2);
        }

        private void FillAbilitySlotList()
        {
            for (int Index = 0; Index < 6; Index++)
            {
                AbilitySlot Slot1 = new AbilitySlot();
                Slot1.AbilityMenuClicked += OnAbilityMenuClicked;
                Slot1.AbilityMenuCleared += OnAbilityMenuCleared;

                AbilitySlot Slot2 = new AbilitySlot();
                Slot2.AbilityMenuClicked += OnAbilityMenuClicked;
                Slot2.AbilityMenuCleared += OnAbilityMenuCleared;

                AbilitySlot1List.Add(Slot1);
                AbilitySlot2List.Add(Slot2);
            }
        }

        private void ResetAbilitySlots(ICollection<AbilitySlot> abilitySlotList)
        {
            foreach (AbilitySlot Item in abilitySlotList)
                Item.Reset();
        }

        private void UpdateAbilityCompatibilityList(IPgSkill skill, ICollection<AbilitySlot> abilitySlotList, List<AbilityTierList> compatibleAbilityList)
        {
            compatibleAbilityList.Clear();

            IObjectDefinition AbilityDefinition = ObjectList.Definitions[typeof(Ability)];
            IList<IPgAbility> AbilityList = (IList<IPgAbility>)AbilityDefinition.VerifiedObjectList;

            foreach (IPgAbility Item in AbilityList)
                if (AbilityBelongToSkill(skill, Item))
                    AddAbilityTier(compatibleAbilityList, Item);

            if (skill.ParentSkill != null)
            {
                foreach (IPgAbility Item in AbilityList)
                    if (AbilityBelongToSkill(skill.ParentSkill, Item))
                        AddAbilityTier(compatibleAbilityList, Item);
            }

            foreach (AbilitySlot Item in abilitySlotList)
                Item.SetCompatibleAbilityList(compatibleAbilityList);
        }

        private void FillEmptyAbilitySlots(IPgSkill skill, ICollection<AbilitySlot> abilitySlotList, List<AbilityTierList> compatibleAbilityList)
        {
            List<string> FilledSlotList = new List<string>();
            foreach (AbilitySlot Item in abilitySlotList)
                if (!Item.IsEmpty)
                    FilledSlotList.Add(AbilitySlot.CuteDigitStrippedName(Item.Ability));

            foreach (AbilitySlot Item in abilitySlotList)
                if (Item.IsEmpty)
                {
                    if (FillEmptyAbilitySlot(compatibleAbilityList, skill, Item, FilledSlotList))
                        FilledSlotList.Add(AbilitySlot.CuteDigitStrippedName(Item.Ability));
                }
        }

        private static bool AbilityBelongToSkill(IPgSkill skill, IPgAbility item)
        {
            if (item.Skill != skill)
                return false;

            if (item.KeywordList.Contains(AbilityKeyword.Lint_NotLearnable) && item.Name != "Sword Slash")
                return false;

            return true;
        }

        private static void AddAbilityTier(List<AbilityTierList> compatibleAbilityList, IPgAbility ability)
        {
            foreach (AbilityTierList Item in compatibleAbilityList)
                if (AbilitySlot.CuteDigitStrippedName(Item.Source) == AbilitySlot.CuteDigitStrippedName(ability))
                {
                    Item.Add(ability);
                    return;
                }

            compatibleAbilityList.Add(new AbilityTierList(ability));
        }

        private bool FillEmptyAbilitySlot(List<AbilityTierList> compatibleAbilityList, IPgSkill skill, AbilitySlot abilitySlot, List<string> filledSlotList)
        {
            foreach (AbilityTierList Item in compatibleAbilityList)
                if (!filledSlotList.Contains(AbilitySlot.CuteDigitStrippedName(Item.Source)))
                {
                    abilitySlot.SetAbility(Item, IconFolder);
                    return true;
                }

            return false;
        }

        private void SelectAbilityByName(AbilitySlot slot, string selectedName, List<AbilityTierList> compatibleAbilityList)
        {
            foreach (AbilityTierList TierListItem in compatibleAbilityList)
                foreach (IPgAbility AbilityItem in TierListItem)
                    if (AbilityItem.Name == selectedName)
                    {
                        slot.SetAbility(TierListItem, AbilityItem, IconFolder);
                        return;
                    }

            slot.Reset();
        }

        private void SelectAbilityByKey(AbilitySlot slot, string selectedKey, List<AbilityTierList> compatibleAbilityList)
        {
            foreach (AbilityTierList TierListItem in compatibleAbilityList)
                foreach (IPgAbility AbilityItem in TierListItem)
                    if (AbilityItem.Key == selectedKey)
                    {
                        slot.SetAbility(TierListItem, AbilityItem, IconFolder);
                        return;
                    }

            slot.Reset();
        }

        private readonly List<AbilityTierList> CompatibleAbility1List = new List<AbilityTierList>();
        private readonly List<AbilityTierList> CompatibleAbility2List = new List<AbilityTierList>();
        #endregion

        #region Events
        private void OnClosed(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void OnSkillSelectionChanged1(object sender, SelectionChangedEventArgs e)
        {
            ComboBox Control = (ComboBox) sender;
            OnSkillSelectionChanged1(Control.SelectedIndex);
        }

        private void OnSkillSelectionChanged1(int index)
        {
            if (SelectedSkill1 == index)
                return;

            SelectedSkill1 = index;
            NotifyPropertyChanged(nameof(SelectedSkill1));

            ResetAbilitySlots(AbilitySlot1List);
            UpdateAbilityCompatibilityList(SkillList[SelectedSkill1], AbilitySlot1List, CompatibleAbility1List);
            FillEmptyAbilitySlots(SkillList[SelectedSkill1], AbilitySlot1List, CompatibleAbility1List);
            ResetGearSlots();

            CommandManager.InvalidateRequerySuggested();
            RecalculateMods();
        }

        private void OnSkillSelectionChanged2(object sender, SelectionChangedEventArgs e)
        {
            ComboBox Control = (ComboBox)sender;
            OnSkillSelectionChanged2(Control.SelectedIndex);
        }

        private void OnSkillSelectionChanged2(int index)
        {
            if (SelectedSkill2 == index)
                return;

            SelectedSkill2 = index;
            NotifyPropertyChanged(nameof(SelectedSkill2));

            ResetAbilitySlots(AbilitySlot2List);
            UpdateAbilityCompatibilityList(SkillList[SelectedSkill2], AbilitySlot2List, CompatibleAbility2List);
            FillEmptyAbilitySlots(SkillList[SelectedSkill2], AbilitySlot2List, CompatibleAbility2List);
            ResetGearSlots();

            CommandManager.InvalidateRequerySuggested();
            RecalculateMods();
        }

        private void OnAbilityMenuClicked(object sender, RoutedEventArgs e)
        {
            MenuItem SubmenuItem = (MenuItem)sender;
            MenuItem MenuItem = (MenuItem)SubmenuItem.Parent;
            ContextMenu Menu = (ContextMenu)MenuItem.Parent;
            AbilitySlot Slot = (AbilitySlot)Menu.DataContext;
            string SelectedName = (string)SubmenuItem.Header;

            if (AbilitySlot1List.Contains(Slot))
                SelectAbilityByName(Slot, SelectedName, CompatibleAbility1List);
            else if (AbilitySlot2List.Contains(Slot))
                SelectAbilityByName(Slot, SelectedName, CompatibleAbility2List);

            RecalculateMods();
        }

        private void OnAbilityMenuCleared(object sender, RoutedEventArgs e)
        {
            MenuItem Menu = (MenuItem)e.OriginalSource;
            AbilitySlot Slot = (AbilitySlot)Menu.DataContext;

            OnClearAbility(Slot);
        }

        private void OnClearAbility(AbilitySlot slot)
        {
            slot.Reset();
            RecalculateMods();
        }

        private void OnSelectAbilities1(List<string> abilityTable)
        {
            int Index;

            for (Index = 0; Index < 6 && Index < abilityTable.Count; Index++)
            {
                AbilitySlot Slot = AbilitySlot1List[Index];
                string SelectedKey = abilityTable[Index];
                SelectAbilityByKey(Slot, SelectedKey, CompatibleAbility1List);
            }

            for (; Index < 6; Index++)
            {
                AbilitySlot Slot = AbilitySlot1List[Index];
                OnClearAbility(Slot);
            }

            RecalculateMods();
        }

        private void OnSelectAbilities2(List<string> abilityTable)
        {
            int Index;

            for (Index = 0; Index < 6 && Index < abilityTable.Count; Index++)
            {
                AbilitySlot Slot = AbilitySlot2List[Index];
                string SelectedKey = abilityTable[Index];
                SelectAbilityByKey(Slot, SelectedKey, CompatibleAbility2List);
            }

            for (; Index < 6; Index++)
            {
                AbilitySlot Slot = AbilitySlot2List[Index];
                OnClearAbility(Slot);
            }

            RecalculateMods();
        }

        private void OnItemSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox Control = (ComboBox)sender;
            GearSlot Slot = (GearSlot) Control.DataContext;

            if (Slot.SelectedItemIndex != Control.SelectedIndex)
            {
                Slot.SetSelectedItem(Control.SelectedIndex);
                RecalculateMods();
            }
        }

        private void OnSelectGear(string gearSlotName, string itemKey)
        {
            foreach (GearSlot Item in GearSlotList)
                if (Item.Name == gearSlotName)
                {
                    Item.SetSelectedItem(itemKey);
                    RecalculateMods();
                    break;
                }
        }

        private void OnPowerSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox Control = (ComboBox)sender;
            if (Control.Items.Count > 0)
            {
                Mod Mod = (Mod)Control.DataContext;

                if (Mod.SelectedPowerIndex != Control.SelectedIndex)
                {
                    Mod.SetSelectedPower(Control.SelectedIndex);
                    RecalculateMods();
                }
            }
        }

        private void CanAddMod(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = SelectedSkill1 >= 0 || SelectedSkill2 >= 0;
        }

        private void OnAddMod(object sender, ExecutedRoutedEventArgs e)
        {
            Button Control = (Button)e.OriginalSource;
            GearSlot Slot = (GearSlot)Control.DataContext;
            Slot.AddMod();
        }

        private void OnIncrementTier(object sender, ExecutedRoutedEventArgs e)
        {
            Button Control = (Button)e.OriginalSource;
            Mod Mod = (Mod)Control.DataContext;
            Mod.IncrementTier();
            RecalculateMods();
        }

        private void OnDecrementTier(object sender, ExecutedRoutedEventArgs e)
        {
            Button Control = (Button)e.OriginalSource;
            Mod Mod = (Mod)Control.DataContext;
            Mod.DecrementTier();
            RecalculateMods();
        }

        private void OnMoveDown(object sender, ExecutedRoutedEventArgs e)
        {
            Button Control = (Button)e.OriginalSource;
            Mod Mod = (Mod)Control.DataContext;
            Mod.MoveDown();
        }

        private void OnMoveUp(object sender, ExecutedRoutedEventArgs e)
        {
            Button Control = (Button)e.OriginalSource;
            Mod Mod = (Mod)Control.DataContext;
            Mod.MoveUp();
        }

        private void OnSelectGearMods(string gearSlotName, List<KeyValuePair<string, int>> modList)
        {
            foreach (GearSlot Item in GearSlotList)
                if (Item.Name == gearSlotName)
                {
                    OnSelectGearMods(Item, modList);
                    break;
                }

            RecalculateMods();
        }

        private void OnSelectGearMods(GearSlot slot, List<KeyValuePair<string, int>> modList)
        {
            slot.ResetMods();

            foreach (KeyValuePair<string, int> Item in modList)
                slot.AddMod(Item.Key, Item.Value);
        }

        private void OnLoad(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog Dlg = new OpenFileDialog();
            Dlg.FileName = LastBuildFile;
            Dlg.Filter = "Build File (*.txt)|*.txt";

            bool? Result = Dlg.ShowDialog(this);
            if (Result.Value && !string.IsNullOrEmpty(Dlg.FileName))
                OnLoad(Dlg.FileName);
        }

        private void OnLoad(string filePath)
        {
            if (!LoadBuild(filePath, out IPgSkill Skill1, out IPgSkill Skill2, out List<string>[] AbilityTable, out Dictionary<string, string> GearItemTable, out Dictionary<string, List<KeyValuePair<string, int>>> GearModTable))
            { 
                MessageBox.Show("Invalid file format", "Error");
                return;
            }

            Debug.Assert(AbilityTable.Length == 2);
            Debug.Assert(SkillList.Contains(Skill1));
            Debug.Assert(SkillList.Contains(Skill2));

            LastBuildFile = filePath;
            NotifyPropertyChanged(nameof(LastBuildFile));
            NotifyPropertyChanged(nameof(TitleText));

            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<int>(OnSkillSelectionChanged1), SkillList.IndexOf(Skill1));
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<int>(OnSkillSelectionChanged2), SkillList.IndexOf(Skill2));
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<List<string>>(OnSelectAbilities1), AbilityTable[0]);
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<List<string>>(OnSelectAbilities2), AbilityTable[1]);
            foreach (KeyValuePair<string, string> Entry in GearItemTable)
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<string, string>(OnSelectGear), Entry.Key, Entry.Value);
            foreach (KeyValuePair<string, List<KeyValuePair<string, int>>> Entry in GearModTable)
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<string, List<KeyValuePair<string, int>>>(OnSelectGearMods), Entry.Key, Entry.Value);

            RecalculateMods();
        }

        private void OnSave(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog Dlg = new SaveFileDialog();
            Dlg.FileName = LastBuildFile;
            Dlg.Filter = "Build File (*.txt)|*.txt";

            bool? Result = Dlg.ShowDialog(this);
            if (Result.Value && !string.IsNullOrEmpty(Dlg.FileName))
                SaveBuild(Dlg.FileName);
        }

        private void OnClearItem(object sender, ExecutedRoutedEventArgs e)
        {
            Button Control = (Button) e.OriginalSource;
            GearSlot Slot = (GearSlot) Control.DataContext;

            Slot.ResetItem();
        }

        private void OnClearMod(object sender, ExecutedRoutedEventArgs e)
        {
            Button Control = (Button)e.OriginalSource;
            Mod Mod = (Mod)Control.DataContext;
            GearSlot Slot = Mod.ParentSlot;

            Slot.RemoveMod(Mod);
        }

        private void OnLargeViewChecked(object sender, RoutedEventArgs e)
        {
            SetLargeView(true);
        }

        private void OnSmallViewChecked(object sender, RoutedEventArgs e)
        {
            SetLargeView(false);
        }

        private void SetLargeView(bool value)
        {
            if (IsLargeView == value)
                return;

            IsLargeView = value;
            NotifyPropertyChanged(nameof(IsLargeView));
            NotifyPropertyChanged(nameof(IsSmallView));

            ReloadAbilitySlotList(AbilitySlot1List);
            ReloadAbilitySlotList(AbilitySlot2List);
        }

        private void ReloadAbilitySlotList(Collection<AbilitySlot> list)
        {
            List<AbilitySlot> Copy = new List<AbilitySlot>(list);

            list.Clear();
            foreach (AbilitySlot Item in Copy)
                list.Add(Item);
        }
        #endregion

        #region Load
        private bool LoadBuild(string fileName, out IPgSkill skill1, out IPgSkill skill2, out List<string>[] abilityTable, out Dictionary<string, string> gearItemTable, out Dictionary<string, List<KeyValuePair<string, int>>> gearModTable)
        {
            using FileStream Stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            return LoadBuild(Stream, out skill1, out skill2, out abilityTable, out gearItemTable, out gearModTable);
        }

        private bool LoadBuild(FileStream stream, out IPgSkill skill1, out IPgSkill skill2, out List<string>[] abilityTable, out Dictionary<string, string> gearItemTable, out Dictionary<string, List<KeyValuePair<string, int>>> gearModTable)
        {
            skill1 = null;
            skill2 = null;
            abilityTable = new List<string>[2]
            {
                new List<string>(),
                new List<string>(),
            };
            gearItemTable = new Dictionary<string, string>();
            gearModTable = new Dictionary<string, List<KeyValuePair<string, int>>>();

            JsonTextReader Reader = new JsonTextReader(stream);
            
            Reader.Read();
            if (!(Reader.CurrentValue is string BuildKey && BuildKey  == "Build"))
                return false;

            Reader.Read();
            if (Reader.CurrentToken != Json.Token.ObjectStart)
                return false;

            if (!LoadBuildSkills(Reader, out skill1, out skill2))
                return false;

            Reader.Read();

            int SkillIndex = 0;
            int AbilityIndex = 0;
            while (LoadBuildAbility(Reader, ref SkillIndex, ref AbilityIndex, out string AbilityName))
            {
                if (SkillIndex < 0 || SkillIndex >= abilityTable.Length)
                    return false;

                abilityTable[SkillIndex].Add(AbilityName);
                Reader.Read();
            }

            while (LoadBuildGear(Reader, out string SlotName, out string ItemName, out List<KeyValuePair<string, int>> ModTable))
            {
                if (gearItemTable.ContainsKey(SlotName) || gearModTable.ContainsKey(SlotName))
                    return false;

                gearItemTable.Add(SlotName, ItemName);
                gearModTable.Add(SlotName, ModTable);

                Reader.Read();
            }

            if (Reader.CurrentToken != Json.Token.ObjectEnd)
                return false;

            return true;
        }

        private bool LoadBuildSkills(JsonTextReader reader, out IPgSkill skill1, out IPgSkill skill2)
        {
            skill1 = null;
            skill2 = null;

            reader.Read();
            if (!(reader.CurrentValue is string SkillKey1 && SkillKey1 == "Skill1"))
                return false;

            reader.Read();
            if (reader.CurrentToken != Json.Token.String || !(reader.CurrentValue is string SkillValue1))
                return false;

            reader.Read();
            if (!(reader.CurrentValue is string SkillKey2 && SkillKey2 == "Skill2"))
                return false;

            reader.Read();
            if (reader.CurrentToken != Json.Token.String || !(reader.CurrentValue is string SkillValue2))
                return false;

            if (!TryParseSkill(SkillValue1, out skill1) || !TryParseSkill(SkillValue2, out skill2))
                return false;

            return true;
        }

        private bool TryParseSkill(string skillName, out IPgSkill skill)
        {
            skill = null;

            foreach (IPgSkill Item in SkillList)
                if (Item.Name == skillName)
                {
                    skill = Item;
                    return true;
                }

            return false;
        }

        private bool LoadBuildAbility(JsonTextReader reader, ref int skillIndex, ref int abilityIndex, out string abilityName)
        {
            abilityName = string.Empty;

            if (!(reader.CurrentValue is string Key))
                return false;

            string[] Split = Key.Split('_');
            if (Split.Length != 3)
                return false;

            string KeyName = Split[0];
            if (KeyName != "Ability")
                return false;

            if (!int.TryParse(Split[1], out int SkillIndexValue))
                return false;

            if (SkillIndexValue != skillIndex)
            {
                if (SkillIndexValue != skillIndex + 1)
                    return false;

                skillIndex++;
                abilityIndex = 0;
            }

            if (!int.TryParse(Split[2], out int AbilityIndexValue))
                return false;

            if (abilityIndex != AbilityIndexValue)
                return false;

            abilityIndex++;

            reader.Read();
            if (reader.CurrentToken != Json.Token.String || !(reader.CurrentValue is string AbilityValue))
                return false;

            abilityName = AbilityValue;

            return true;
        }

        private bool LoadBuildGear(JsonTextReader reader, out string slotName, out string itemName, out List<KeyValuePair<string, int>> modTable)
        {
            slotName = string.Empty;
            itemName = string.Empty;
            modTable = new List<KeyValuePair<string, int>>();

            if (!(reader.CurrentValue is string SlotKey))
                return false;

            slotName = SlotKey;

            reader.Read();
            if (reader.CurrentToken != Json.Token.ObjectStart)
                return false;

            reader.Read();
            if (reader.CurrentValue is string GearKey1 && GearKey1 == "Item")
            {
                reader.Read();
                if (reader.CurrentToken != Json.Token.String || !(reader.CurrentValue is string ItemValue))
                    return false;

                itemName = ItemValue;

                reader.Read();
            }

            int PowerIndex = 0;
            int TierIndex = 0;
            while (LoadBuildPower(reader, ref PowerIndex, ref TierIndex, out string PowerKey, out int TierLevel))
            {
                modTable.Add(new KeyValuePair<string, int>(PowerKey, TierLevel));
                reader.Read();
            }

            if (reader.CurrentToken != Json.Token.ObjectEnd)
                return false;

            return true;
        }

        private bool LoadBuildPower(JsonTextReader reader, ref int powerIndex, ref int tierIndex, out string powerKey, out int tierLevel)
        {
            powerKey = string.Empty;
            tierLevel = -1;

            if (!(reader.CurrentValue is string Key1))
                return false;

            string[] Split1 = Key1.Split('_');
            if (Split1.Length != 2)
                return false;

            string KeyName1 = Split1[0];

            if (KeyName1 != "Power")
                return false;

            if (!int.TryParse(Split1[1], out int PowerIndexValue))
                return false;

            if (powerIndex != PowerIndexValue)
                return false;

            reader.Read();
            if (reader.CurrentToken != Json.Token.String || !(reader.CurrentValue is string PowerValue))
                return false;

            powerKey = PowerValue;
            powerIndex++;

            reader.Read();

            if (!(reader.CurrentValue is string Key2))
                return false;

            string[] Split2 = Key2.Split('_');
            if (Split2.Length != 2)
                return false;

            string KeyName2 = Split2[0];

            if (KeyName2 != "PowerTier")
                return false;

            if (!int.TryParse(Split2[1], out int TierIndexValue))
                return false;

            if (tierIndex != TierIndexValue)
                return false;

            if (tierIndex >= powerIndex)
                return false;

            reader.Read();
            if (reader.CurrentToken != Json.Token.Integer)
                return false;

            tierLevel = (int)reader.CurrentValue;
            tierIndex++;

            return true;
        }
        #endregion

        #region Save
        private void SaveBuild(string filePath)
        {
            using FileStream Stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            SaveBuild(Stream);

            LastBuildFile = filePath;
            NotifyPropertyChanged(nameof(LastBuildFile));
            NotifyPropertyChanged(nameof(TitleText));
        }

        private void SaveBuild(FileStream stream)
        {
            JsonTextWriter Writer = new JsonTextWriter(true);
            Writer.ObjectKey("Build");
            Writer.ObjectStart();

            SaveBuildSkills(Writer);

            for (int i = 0; i < AbilitySlot1List.Count; i++)
            {
                AbilitySlot Item = AbilitySlot1List[i];
                SaveBuildAbilitySlot(Writer, 0, i, Item);
            }

            for (int i = 0; i < AbilitySlot2List.Count; i++)
            {
                AbilitySlot Item = AbilitySlot2List[i];
                SaveBuildAbilitySlot(Writer, 1, i, Item);
            }

            foreach (GearSlot Item in GearSlotList)
                SaveBuildGearSlot(Writer, Item);

            Writer.ObjectEnd();
            Writer.Flush(stream);
        }

        private void SaveBuildSkills(JsonTextWriter writer)
        {
            string SkillName1 = SelectedSkill1 >= 0 ? SkillList[SelectedSkill1].Name : string.Empty;
            string SkillName2 = SelectedSkill2 >= 0 ? SkillList[SelectedSkill2].Name : string.Empty;

            writer.ObjectKey("Skill1");
            writer.Value(SkillName1);

            writer.ObjectKey("Skill2");
            writer.Value(SkillName2);
        }

        private void SaveBuildAbilitySlot(JsonTextWriter writer, int skillIndex, int abilityIndex, AbilitySlot slot)
        {
            string Key = $"Ability_{skillIndex}_{abilityIndex}";
            string Value = slot.IsEmpty ? string.Empty : slot.Ability.Key;

            writer.ObjectKey(Key);
            writer.Value(Value);
        }

        private void SaveBuildGearSlot(JsonTextWriter writer, GearSlot slot)
        {
            writer.ObjectKey(slot.Name);
            writer.ObjectStart();

            if (slot.SelectedItemIndex >= 0 && slot.SelectedItemIndex < slot.ItemList.Count)
            {
                writer.ObjectKey("Item");
                writer.Value(slot.ItemList[slot.SelectedItemIndex].ItemKey);
            }

            int Index, PowerIndex;
            for (Index = 0, PowerIndex = 0; Index < slot.ModList.Count; Index++)
            {
                Mod Mod = slot.ModList[Index];
                SaveBuildMod(writer, ref PowerIndex, Mod);
            }

            writer.ObjectEnd();
        }

        private void SaveBuildMod(JsonTextWriter writer, ref int powerIndex, Mod mod)
        {
            if (mod.SelectedPowerIndex < 0 || mod.SelectedPowerIndex >= mod.AvailablePowerList.Count)
                return;

            Power Power = mod.AvailablePowerList[mod.SelectedPowerIndex];
            SaveBuilPower(writer, ref powerIndex, Power);
        }

        private void SaveBuilPower(JsonTextWriter writer, ref int powerIndex, Power power)
        {
            if (power.SelectedTier < 0 || power.SelectedTier >= power.Source.CombinedTierList.Count)
                return;

            string KeyPower = $"Power_{powerIndex}";
            string ValuePower = power.Source.Key;
            string KeyTier = $"PowerTier_{powerIndex}";
            int ValueTier = power.SelectedTier;

            writer.ObjectKey(KeyPower);
            writer.Value(ValuePower);
            writer.ObjectKey(KeyTier);
            writer.Value(ValueTier);

            powerIndex++;
        }
        #endregion

        #region Settings
        private const string LastBuildFileValueName = "LastBuildFile";

        private void LoadSettings()
        {
            try
            {
                RegistryKey Key = Registry.CurrentUser.OpenSubKey(@"Software", true);
                Key = Key.CreateSubKey("Project Gorgon Tools");
                RegistryKey SettingKey = Key.CreateSubKey("PgBuilder");

                string Value;

                Value = SettingKey?.GetValue(LastBuildFileValueName) as string;
                if (Value != null)
                    if (File.Exists(Value))
                        OnLoad(Value);
            }
            catch
            {
            }
        }

        private void SaveSettings()
        {
            try
            {
                RegistryKey Key = Registry.CurrentUser.OpenSubKey(@"Software", true);
                Key = Key.CreateSubKey("Project Gorgon Tools");
                RegistryKey SettingKey = Key.CreateSubKey("PgBuilder");

                SettingKey?.SetValue(LastBuildFileValueName, LastBuildFile, RegistryValueKind.String);
            }
            catch
            {
            }
        }
        #endregion

        #region Recalculate Mods
        private void RecalculateMods()
        {
            if (RecalculateOperation == null || RecalculateOperation.Status == DispatcherOperationStatus.Completed)
            {
                Debug.WriteLine("Scheduling recalculate");
                RecalculateOperation = Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new Action(RecalculateModsNow));
            }
            else
                Debug.WriteLine("Skipping recalculate");
        }

        private void RecalculateModsNow()
        {
            Debug.WriteLine("Starting recalculate");
            ResetAllMods();
            RecalculateAllMods();
            Debug.WriteLine("Done with recalculate");
        }

        private void ResetAllMods()
        {
            foreach (AbilitySlot Item in AbilitySlot1List)
                Item.ResetMods();

            foreach (AbilitySlot Item in AbilitySlot2List)
                Item.ResetMods();
        }

        private void RecalculateAllMods()
        {
            foreach (GearSlot Item in GearSlotList)
                RecalculateSlotMods(Item);
        }

        private void RecalculateSlotMods(GearSlot slot)
        {
            if (slot.SelectedItemIndex >= 0)
            {
                ItemInfo Item = slot.ItemList[slot.SelectedItemIndex];
                RecalculateItemMods(Item);
            }

            foreach (Mod Item in slot.ModList)
                RecalculateSelectedMods(Item);
        }

        private void RecalculateItemMods(ItemInfo gearItem)
        {
            foreach (IPgItemEffect Item in gearItem.Item.EffectDescriptionList)
                RecalculateEffectMod(Item);
        }

        private void RecalculateSelectedMods(Mod mod)
        {
            IPgPowerTier Tier = mod.SelectedTier;
            if (Tier == null)
                return;

            foreach (IPgPowerEffect Item in Tier.EffectList)
                RecalculateEffectMod(mod.SelectedPower, mod.SelectedPower.SelectedTier, Item);
        }

        private void RecalculateEffectMod(IPgItemEffect effect)
        {
            switch (effect)
            {
                case IPgItemAttributeLink AsItemAttributeLink:
                    RecalculateAttributeMods(AsItemAttributeLink.Link, AsItemAttributeLink.AttributeEffect);
                    break;

                case IPgItemSimpleEffect AsItemSimpleEffect:
                    RecalculateSimpleEffectMods(AsItemSimpleEffect);
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }
        }

        private void RecalculateEffectMod(Power power, int tier, IPgPowerEffect effect)
        {
            switch (effect)
            {
                case IPgPowerAttributeLink AsPowerAttributeLink:
                    RecalculateAttributeMods(AsPowerAttributeLink.AttributeLink, AsPowerAttributeLink.AttributeEffect);
                    break;

                case IPgPowerSimpleEffect AsPowerSimpleEffect:
                    RecalculateSimpleEffectMods(power, tier);
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }
        }

        private void RecalculateAttributeMods(IPgAttribute attribute, float attributeEffect)
        {
            foreach (AbilitySlot Item in AbilitySlot1List)
                Item.RecalculateMods(attribute.Key, attributeEffect);

            foreach (AbilitySlot Item in AbilitySlot2List)
                Item.RecalculateMods(attribute.Key, attributeEffect);
        }

        private void RecalculateSimpleEffectMods(IPgItemSimpleEffect itemSimpleEffect)
        {
            Debug.WriteLine($"Ignoring item effect: {itemSimpleEffect.Description}");
        }

        private void RecalculateSimpleEffectMods(Power power, int tier)
        {
            string Key = Parser.PowerEffectPairKey(power.Source, tier);

            if (!PowerKeyToCompleteEffectTable.ContainsKey(Key))
            {
                Debug.WriteLine($"Key '{Key}' not found");
                return;
            }

            ModEffect ModEffect = PowerKeyToCompleteEffectTable[Key];

            foreach (AbilitySlot Item in AbilitySlot1List)
                Item.AddEffect(ModEffect);

            foreach (AbilitySlot Item in AbilitySlot2List)
                Item.AddEffect(ModEffect);
        }

        private DispatcherOperation RecalculateOperation = null;
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
