namespace PgBuilder
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls;
    using PgJsonObjects;

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region Init
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            LoadCachedData();
            FillSkillList();
            FillGearSlotList();
            FillAbilitySlotList();
        }
        #endregion

        #region Data Load
        public void LoadCachedData()
        {
            int Version = 332;
            string UserRootFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string ApplicationFolder = Path.Combine(UserRootFolder, "PgJsonParse");
            string VersionCacheFolder = Path.Combine(ApplicationFolder, "Versions");
            string IconCacheFolder = Path.Combine(ApplicationFolder, "Shared Icons");
            string VersionFolder = Path.Combine(VersionCacheFolder, Version.ToString());
            IconFolder = IconCacheFolder;

            LoadCachedData(VersionFolder, IconFolder);
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

            Dictionary<string, IJsonKey> PowerTable = ObjectList.Definitions[typeof(Power)].ObjectTable;
            Dictionary<string, IJsonKey> AttributeTable = ObjectList.Definitions[typeof(PgJsonObjects.Attribute)].ObjectTable;

            foreach (KeyValuePair<string, IJsonKey> Entry in PowerTable)
            {
                IPgPower Power = (IPgPower)Entry.Value;
                Power.InitTierList(AttributeTable);
            }
        }

        private static byte[] LoadBinaryFile(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    byte[] Result = br.ReadBytes((int)fs.Length);
                    return Result;
                }
            }
        }

        private string IconFolder;
        #endregion

        #region Properties
        public List<IPgSkill> Skill1List { get; } = new List<IPgSkill>();
        public int SelectedSkill1 { get; private set; } = -1;
        public List<IPgSkill> Skill2List { get; } = new List<IPgSkill>();
        public int SelectedSkill2 { get; private set; } = -1;
        public List<GearSlot> GearSlotList { get; } = new List<GearSlot>();
        public List<AbilitySlot> AbilitySlot1List { get; } = new List<AbilitySlot>();
        public List<AbilitySlot> AbilitySlot2List { get; } = new List<AbilitySlot>();
        #endregion

        #region Implementation
        private void FillSkillList()
        {
            IObjectDefinition PowerDefinition = ObjectList.Definitions[typeof(Power)];
            IList<IPgPower> PowerList = (IList<IPgPower>)PowerDefinition.VerifedObjectList;
            List<IPgSkill> SkillList = new List<IPgSkill>();

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

            Skill1List.AddRange(SkillList);
            Skill2List.AddRange(SkillList);
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

        private void FillAbilitySlotList()
        {
            for (int Index = 0; Index < 6; Index++)
            {
                AbilitySlot1List.Add(new AbilitySlot());
                AbilitySlot2List.Add(new AbilitySlot());
            }
        }

        private void ResetAbilitySlots(List<AbilitySlot> abilitySlotList)
        {
            foreach (AbilitySlot Item in abilitySlotList)
                Item.Reset();
        }

        private void FillEmptyAbilitySlots(IPgSkill skill, List<AbilitySlot> abilitySlotList, List<AbilityTierList> compatibleAbilityList)
        {
            compatibleAbilityList.Clear();

            IObjectDefinition AbilityDefinition = ObjectList.Definitions[typeof(Ability)];
            IList<IPgAbility> AbilityList = (IList<IPgAbility>)AbilityDefinition.VerifedObjectList;

            foreach (IPgAbility Item in AbilityList)
                if (Item.Skill == skill && (!Item.KeywordList.Contains(AbilityKeyword.Lint_NotLearnable) || Item.Name == "Sword Slash"))
                    AddAbilityTier(compatibleAbilityList, Item);

            List<string> FilledSlotList = new List<string>();
            foreach (AbilitySlot Item in abilitySlotList)
                if (!Item.IsEmpty)
                    FilledSlotList.Add(AbilitySlot.CuteDigitStrippedName(Item.Ability));

            foreach (AbilitySlot Item in abilitySlotList)
                if (Item.IsEmpty)
                {
                    FillEmptyAbilitySlot(compatibleAbilityList, skill, Item, FilledSlotList);
                    FilledSlotList.Add(AbilitySlot.CuteDigitStrippedName(Item.Ability));
                }
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

        private void FillEmptyAbilitySlot(List<AbilityTierList> compatibleAbilityList, IPgSkill skill, AbilitySlot abilitySlot, List<string> filledSlotList)
        {
            foreach (AbilityTierList Item in compatibleAbilityList)
                if (!filledSlotList.Contains(AbilitySlot.CuteDigitStrippedName(Item.Source)))
                {
                    abilitySlot.SetAbility(Item, IconFolder);
                    return;
                }
        }

        private void DisplayAbilityChoiceMenu(FrameworkElement control, AbilitySlot slot, List<AbilityTierList> compatibleAbilityList)
        {
            if (control.ContextMenu == null)
                control.ContextMenu = new ContextMenu();
            ContextMenu Menu = control.ContextMenu;

            List<string> AbilityNameList = new List<string>();
            Menu.Items.Clear();

            foreach (AbilityTierList TierListItem in compatibleAbilityList)
            {
                if (AbilityNameList.Contains(TierListItem.Name))
                    continue;
                AbilityNameList.Add(TierListItem.Name);

                string Name = TierListItem.Name;
                MenuItem NewMenuItem = new MenuItem();
                NewMenuItem.Header = Name;
                NewMenuItem.IsChecked = TierListItem == slot.AbilityTierList;

                foreach (IPgAbility AbilityItem in TierListItem)
                {
                    MenuItem NewSubmenuItem = new MenuItem();
                    NewSubmenuItem.Header = AbilityItem.Name;
                    NewSubmenuItem.IsChecked = AbilityItem == slot.Ability;
                    NewSubmenuItem.Click += OnAbilityMenuClick;

                    NewMenuItem.Items.Add(NewSubmenuItem);
                }

                Menu.Items.Add(NewMenuItem);
            }
        }

        private void SelectAbility(AbilitySlot slot, string selectedName, List<AbilityTierList> compatibleAbilityList)
        {
            foreach (AbilityTierList TierListItem in compatibleAbilityList)
                foreach (IPgAbility AbilityItem in TierListItem)
                    if (AbilityItem.Name == selectedName)
                    {
                        slot.SetAbility(TierListItem, AbilityItem, IconFolder);
                        break;
                    }
        }

        private void CloseAbilityChoiceMenu(FrameworkElement control)
        {
            if (control.ContextMenu == null)
                return;
            ContextMenu Menu = control.ContextMenu;

            foreach (MenuItem MenuItem in Menu.Items)
                foreach (MenuItem SubmenuItem in MenuItem.Items)
                    SubmenuItem.Click -= OnAbilityMenuClick;
        }

        private readonly List<AbilityTierList> CompatibleAbility1List = new List<AbilityTierList>();
        private readonly List<AbilityTierList> CompatibleAbility2List = new List<AbilityTierList>();
        #endregion

        #region Events
        private void OnSkillSelectionChanged1(object sender, SelectionChangedEventArgs e)
        {
            ComboBox Control = (ComboBox) sender;
            if (SelectedSkill1 != Control.SelectedIndex)
            {
                SelectedSkill1 = Control.SelectedIndex;
                NotifyPropertyChanged(nameof(SelectedSkill1));

                ResetAbilitySlots(AbilitySlot1List);
                FillEmptyAbilitySlots(Skill1List[SelectedSkill1], AbilitySlot1List, CompatibleAbility1List);
            }
        }

        private void OnSkillSelectionChanged2(object sender, SelectionChangedEventArgs e)
        {
            ComboBox Control = (ComboBox)sender;
            if (SelectedSkill2 != Control.SelectedIndex)
            {
                SelectedSkill2 = Control.SelectedIndex;
                NotifyPropertyChanged(nameof(SelectedSkill2));

                ResetAbilitySlots(AbilitySlot2List);
                FillEmptyAbilitySlots(Skill2List[SelectedSkill2], AbilitySlot2List, CompatibleAbility2List);
            }
        }

        private void OnAbilityContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            FrameworkElement Control = (FrameworkElement)sender;
            AbilitySlot Slot = (AbilitySlot)Control.DataContext;
            IPgSkill Skill = Slot.Ability.Skill;

            if (SelectedSkill1 >= 0 && Skill == Skill1List[SelectedSkill1])
                DisplayAbilityChoiceMenu(Control, Slot, CompatibleAbility1List);
            else if (SelectedSkill2 >= 0 && Skill == Skill2List[SelectedSkill2])
                DisplayAbilityChoiceMenu(Control, Slot, CompatibleAbility2List);
        }

        private void OnAbilityMenuClick(object sender, RoutedEventArgs e)
        {
            MenuItem SubmenuItem = (MenuItem)sender;
            MenuItem MenuItem = (MenuItem)SubmenuItem.Parent;
            ContextMenu Menu = (ContextMenu)MenuItem.Parent;
            AbilitySlot Slot = (AbilitySlot)Menu.DataContext;
            IPgSkill Skill = Slot.Ability.Skill;
            string SelectedName = (string)SubmenuItem.Header;

            if (SelectedSkill1 >= 0 && Skill == Skill1List[SelectedSkill1])
                SelectAbility(Slot, SelectedName, CompatibleAbility1List);
            else if (SelectedSkill2 >= 0 && Skill == Skill2List[SelectedSkill2])
                SelectAbility(Slot, SelectedName, CompatibleAbility2List);
        }

        private void OnAbilityContextMenuClosing(object sender, ContextMenuEventArgs e)
        {
            FrameworkElement Control = (FrameworkElement)sender;
            CloseAbilityChoiceMenu(Control);
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
