using Microsoft.Win32;
using PgJsonReader;

namespace PgBuilder
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls;
    using System.Windows.Input;
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
            int Version = 333;
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
            IObjectDefinition PowerDefinition = ObjectList.Definitions[typeof(PgJsonObjects.Power)];
            IList<IPgPower> PowerList = (IList<IPgPower>)PowerDefinition.VerifiedObjectList;
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

        private void ResetGearSlots()
        {
            IPgSkill Skill1 = SelectedSkill1 >= 0 ? Skill1List[SelectedSkill1] : null;
            IPgSkill Skill2 = SelectedSkill2 >= 0 ? Skill2List[SelectedSkill2] : null;

            foreach (GearSlot Item in GearSlotList)
                Item.Reset(Skill1, Skill2);
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
                ResetGearSlots();

                CommandManager.InvalidateRequerySuggested();
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
                ResetGearSlots();

                CommandManager.InvalidateRequerySuggested();
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

        private void OnItemSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox Control = (ComboBox)sender;
            GearSlot Slot = (GearSlot) Control.DataContext;

            if (Slot.SelectedItem != Control.SelectedIndex)
                Slot.SetSelectedItem(Control.SelectedIndex);
        }

        private void OnPowerSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox Control = (ComboBox)sender;
            if (Control.Items.Count > 0)
            {
                Mod Mod = (Mod)Control.DataContext;

                if (Mod.SelectedPower != Control.SelectedIndex)
                    Mod.SetSelectedPower(Control.SelectedIndex);
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
        }

        private void OnDecrementTier(object sender, ExecutedRoutedEventArgs e)
        {
            Button Control = (Button)e.OriginalSource;
            Mod Mod = (Mod)Control.DataContext;
            Mod.DecrementTier();
        }

        private void OnLoad(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog Dlg = new OpenFileDialog();
            Dlg.Filter = "Build File (*.txt)|*.txt";

            bool? Result = Dlg.ShowDialog(this);
            if (Result.Value && !string.IsNullOrEmpty(Dlg.FileName))
                if (!LoadBuild(Dlg.FileName))
                    MessageBox.Show("Invalid file format", "Error");
        }

        private void OnSave(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog Dlg = new SaveFileDialog();
            Dlg.Filter = "Build File (*.txt)|*.txt";

            bool? Result = Dlg.ShowDialog(this);
            if (Result.Value && !string.IsNullOrEmpty(Dlg.FileName))
                SaveBuild(Dlg.FileName);
        }
        #endregion

        #region Load
        private bool LoadBuild(string fileName)
        {
            using (FileStream Stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                return LoadBuild(Stream);
            }
        }

        private bool LoadBuild(FileStream stream)
        {
            JsonTextReader Reader = new JsonTextReader(stream);
            
            Reader.Read();
            if (!(Reader.CurrentValue is string BuildKey && BuildKey  == "Build"))
                return false;

            Reader.Read();
            if (Reader.CurrentToken != Json.Token.ObjectStart)
                return false;

            if (!LoadBuildSkills(Reader, out string SkillName1, out string SkillName2))
                return false;

            Reader.Read();

            while (LoadBuildAbility(Reader, out int SkillIndex, out int AbilityIndex))
                Reader.Read();

            while (LoadBuildGear(Reader, out string SlotName, out string ItemName))
                Reader.Read();

            if (Reader.CurrentToken != Json.Token.ObjectEnd)
                return false;

            return true;
        }

        private bool LoadBuildSkills(JsonTextReader reader, out string skillName1, out string skillName2)
        {
            skillName1 = string.Empty;
            skillName2 = string.Empty;

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

            skillName1 = SkillValue1;
            skillName2 = SkillValue2;

            return true;
        }

        private bool LoadBuildAbility(JsonTextReader reader, out int skillIndex, out int abilityIndex)
        {
            skillIndex = -1;
            abilityIndex = -1;

            if (!(reader.CurrentValue is string Key))
                return false;

            string[] Split = Key.Split('_');
            if (Split.Length != 3)
                return false;

            string KeyName = Split[0];
            if (KeyName != "Ability")
                return false;

            if (!int.TryParse(Split[1], out skillIndex))
                return false;

            if (skillIndex < 0 || skillIndex >= 2)
                return false;

            if (!int.TryParse(Split[2], out abilityIndex))
                return false;

            if (skillIndex == 0)
                if (abilityIndex < 0 || abilityIndex >= AbilitySlot1List.Count)
                    return false;

            if (skillIndex == 1)
                if (abilityIndex < 0 || abilityIndex >= AbilitySlot2List.Count)
                    return false;

            reader.Read();
            if (reader.CurrentToken != Json.Token.String || !(reader.CurrentValue is string AbilityValue))
                return false;

            return true;
        }

        private bool LoadBuildGear(JsonTextReader reader, out string slotName, out string itemName)
        {
            slotName = string.Empty;
            itemName = string.Empty;

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

            while (LoadBuildPower(reader, out int PowerIndex, out int TierIndex, out string PowerKey, out int TierLevel))
                reader.Read();

            if (reader.CurrentToken != Json.Token.ObjectEnd)
                return false;

            return true;
        }

        private bool LoadBuildPower(JsonTextReader reader, out int powerIndex, out int tierIndex, out string powerKey, out int tierLevel)
        {
            powerIndex = -1;
            tierIndex = -1;
            powerKey = string.Empty;
            tierLevel = -1;

            if (!(reader.CurrentValue is string Key))
                return false;

            string[] Split = Key.Split('_');
            if (Split.Length != 2)
                return false;

            string KeyName = Split[0];

            if (KeyName == "Power")
            {
                if (!int.TryParse(Split[1], out powerIndex))
                    return false;

                reader.Read();
                if (reader.CurrentToken != Json.Token.String || !(reader.CurrentValue is string PowerValue))
                    return false;

                powerKey = PowerValue;
            }
            else if (KeyName == "PowerTier")
            {
                if (!int.TryParse(Split[1], out tierIndex))
                    return false;

                reader.Read();
                if (reader.CurrentToken != Json.Token.Integer)
                    return false;

                tierLevel = (int)reader.CurrentValue;
            }
            else
                return false;

            return true;
        }
        #endregion

        #region Save
        private void SaveBuild(string fileName)
        {
            using (FileStream Stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                SaveBuild(Stream);
            }
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
            string SkillName1 = SelectedSkill1 >= 0 ? Skill1List[SelectedSkill1].Name : string.Empty;
            string SkillName2 = SelectedSkill2 >= 0 ? Skill2List[SelectedSkill2].Name : string.Empty;

            writer.ObjectKey("Skill1");
            writer.Value(SkillName1);

            writer.ObjectKey("Skill2");
            writer.Value(SkillName2);
        }

        private void SaveBuildAbilitySlot(JsonTextWriter writer, int skillIndex, int abilityIndex, AbilitySlot slot)
        {
            if (slot.IsEmpty)
                return;

            IPgAbility Ability = slot.Ability;
            string Key = $"Ability_{skillIndex}_{abilityIndex}";
            string Value = Ability.Key;

            writer.ObjectKey(Key);
            writer.Value(Value);
        }

        private void SaveBuildGearSlot(JsonTextWriter writer, GearSlot slot)
        {
            writer.ObjectKey(slot.Name);
            writer.ObjectStart();

            if (slot.SelectedItem >= 0 && slot.SelectedItem < slot.ItemList.Count)
            {
                writer.ObjectKey("Item");
                writer.Value(slot.ItemList[slot.SelectedItem].Key);
            }

            for (int i = 0; i < slot.ModList.Count; i++)
            {
                Mod Mod = slot.ModList[i];
                SaveBuildMod(writer, i, Mod);
            }

            writer.ObjectEnd();
        }

        private void SaveBuildMod(JsonTextWriter writer, int index, Mod mod)
        {
            if (mod.SelectedPower < 0 || mod.SelectedPower >= mod.AvailablePowerList.Count)
                return;

            Power Power = mod.AvailablePowerList[mod.SelectedPower];
            SaveBuilPower(writer, index, Power);
        }

        private void SaveBuilPower(JsonTextWriter writer, int index, Power power)
        {
            if (power.SelectedTier < 0 || power.SelectedTier >= power.Source.CombinedTierList.Count)
                return;

            string KeyPower = $"Power_{index}";
            string ValuePower = power.Source.Key;
            string KeyTier = $"PowerTier_{index}";
            int ValueTier = power.SelectedTier;

            writer.ObjectKey(KeyPower);
            writer.Value(ValuePower);
            writer.ObjectKey(KeyTier);
            writer.Value(ValueTier);
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
