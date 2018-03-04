using Microsoft.Win32;
using PgJsonObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;
using Tools;

namespace PgJsonParse
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region Init
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            InitStartupPage();
            InitCache();
            InitBuildPlaner();
            InitGearPlaner();
            InitSearch();
            InitCruncher();

            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            ApplicationCommand.SubscribeToGlobalCommand("AddPowerCommand", OnAddPower);
            ApplicationCommand.SubscribeToGlobalCommand("RemovePowerCommand", OnRemovePower);
            ApplicationCommand.SubscribeToGlobalCommand("LoadBuildCommand", OnLoadBuild);
            ApplicationCommand.SubscribeToGlobalCommand("SaveBuildCommand", OnSaveBuild);
            ApplicationCommand.SubscribeToGlobalCommand("CopyBuildCommand", OnCopyBuild);
            ApplicationCommand.SubscribeToGlobalCommand("CrunchCommand", OnCrunch);
            ApplicationCommand.SubscribeToGlobalCommand("OpenProfileFolderCommand", OnOpenProfileFolder);
            ApplicationCommand.SubscribeToGlobalCommand("BackwardCommand", OnBackward);
            ApplicationCommand.SubscribeToGlobalCommand("ForwardCommand", OnForward);
        }
        #endregion

        #region Properties
        public string ApplicationFolder { get; set; }
        public string VersionCacheFolder { get; set; }
        public string IconCacheFolder { get; set; }
        public string CurrentVersionCacheFolder { get; set; }
        public string IconFile { get; set; }
        public string FavorIconFile { get; set; }
        public ImageSource FavorIcon { get; private set; }

        public GameVersionInfo LoadedVersion
        {
            get { return _LoadedVersion; }
            set
            {
                if (_LoadedVersion != value)
                {
                    _LoadedVersion = value;
                    NotifyThisPropertyChanged();
                }
            }
        }
        public GameVersionInfo _LoadedVersion;

        public string WarningText
        {
            get { return _WarningText; }
            set
            {
                if (_WarningText != value)
                {
                    _WarningText = value;
                    NotifyThisPropertyChanged();
                }
            }
        }
        public string _WarningText;
        #endregion

        #region Startup Page
        private void InitStartupPage()
        {
            ApplicationFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PgJsonParse");

            if (!Directory.Exists(ApplicationFolder))
                Directory.CreateDirectory(ApplicationFolder);

            VersionCacheFolder = Path.Combine(ApplicationFolder, "Versions");

            if (!Directory.Exists(VersionCacheFolder))
                Directory.CreateDirectory(VersionCacheFolder);

            IconFile = Path.Combine(ApplicationFolder, "mainicon.png");
            FavorIconFile = Path.Combine(ApplicationFolder, "favoricon.png");

            TaskbarShortcut.UpdateTaskbarShortcut(IconFile, "mainicon.p", "mainicon.i");
        }

        public void StartApplication()
        {
            RefreshCombatSkillList();
            RefreshWeightProfileList();
            RefreshGearPlaner();
        }
        #endregion

        #region Cache
        private static readonly Dictionary<ItemSlot, string> IconFileTable = new Dictionary<ItemSlot, string>()
        {
            { ItemSlot.Head, "icon_16010" },
            { ItemSlot.Chest, "icon_12110" },
            { ItemSlot.Legs, "icon_11110" },
            { ItemSlot.Hands, "icon_13110" },
            { ItemSlot.Feet, "icon_10017" },
            { ItemSlot.Ring, "icon_17202" },
            { ItemSlot.Necklace, "icon_17101" },
            { ItemSlot.MainHand, "icon_15014" },
            { ItemSlot.OffHand, "icon_5382" },
        };

        private void InitCache()
        {
            ImageConversion.UpdateWindowIconUsingFile(this, IconFile);
            FavorIcon = ImageConversion.IconFileToImageSource(FavorIconFile);
        }
        #endregion

        #region Build Planer
        public const int DefaultMaxLevel = 70;

        private void InitBuildPlaner()
        {
            CombatSkillList = new ObservableCollection<PowerSkill>();
            MaxLevelFirstSkill = DefaultMaxLevel;
            MaxLevelSecondSkill = DefaultMaxLevel;
            SelectedFirstSkill = -1;
            SelectedSecondSkill = -1;
            SlotPlanerList = new List<SlotPlaner>();

            HeadSlotPlaner = new SlotPlaner(ItemSlot.Head, IconFileTable);
            SlotPlanerList.Add(HeadSlotPlaner);

            ChestSlotPlaner = new SlotPlaner(ItemSlot.Chest, IconFileTable);
            SlotPlanerList.Add(ChestSlotPlaner);

            LegSlotPlaner = new SlotPlaner(ItemSlot.Legs, IconFileTable);
            SlotPlanerList.Add(LegSlotPlaner);

            HandSlotPlaner = new SlotPlaner(ItemSlot.Hands, IconFileTable);
            SlotPlanerList.Add(HandSlotPlaner);

            FeetSlotPlaner = new SlotPlaner(ItemSlot.Feet, IconFileTable);
            SlotPlanerList.Add(FeetSlotPlaner);

            RingSlotPlaner = new SlotPlaner(ItemSlot.Ring, IconFileTable);
            SlotPlanerList.Add(RingSlotPlaner);

            NeckSlotPlaner = new SlotPlaner(ItemSlot.Necklace, IconFileTable);
            SlotPlanerList.Add(NeckSlotPlaner);

            MainHandSlotPlaner = new SlotPlaner(ItemSlot.MainHand, IconFileTable);
            SlotPlanerList.Add(MainHandSlotPlaner);

            OffHandSlotPlaner = new SlotPlaner(ItemSlot.OffHand, IconFileTable);
            SlotPlanerList.Add(OffHandSlotPlaner);

            WeightProfileList = new ObservableCollection<WeightProfile>();
            WeightProfileIndex = -1;
            IgnoreUnobtainable = true;
            IgnoreNoAttribute = true;
        }

        public ObservableCollection<PowerSkill> CombatSkillList { get; private set; }

        public int MaxLevelFirstSkill
        {
            get { return _MaxLevelFirstSkill; }
            set
            {
                if (_MaxLevelFirstSkill != value)
                {
                    _MaxLevelFirstSkill = value;
                    NotifyThisPropertyChanged();

                    RefreshBuildPlaner();
                }
            }
        }
        private int _MaxLevelFirstSkill;

        public int MaxLevelSecondSkill
        {
            get { return _MaxLevelSecondSkill; }
            set
            {
                if (_MaxLevelSecondSkill != value)
                {
                    _MaxLevelSecondSkill = value;
                    NotifyThisPropertyChanged();

                    RefreshBuildPlaner();
                }
            }
        }
        private int _MaxLevelSecondSkill;

        public int SelectedFirstSkill { get; set; }
        public int SelectedSecondSkill { get; set; }
        public SlotPlaner HeadSlotPlaner { get; private set; }
        public SlotPlaner ChestSlotPlaner { get; private set; }
        public SlotPlaner HandSlotPlaner { get; private set; }
        public SlotPlaner LegSlotPlaner { get; private set; }
        public SlotPlaner FeetSlotPlaner { get; private set; }
        public SlotPlaner MainHandSlotPlaner { get; private set; }
        public SlotPlaner OffHandSlotPlaner { get; private set; }
        public SlotPlaner RingSlotPlaner { get; private set; }
        public SlotPlaner NeckSlotPlaner { get; private set; }
        public ObservableCollection<WeightProfile> WeightProfileList { get; private set; }
        public int WeightProfileIndex { get; set; }
        public bool IgnoreUnobtainable { get; set; }
        public bool IgnoreNoAttribute { get; set; }

        private void RefreshCombatSkillList()
        {
            PowerSkill SelectAsFirst, SelectAsSecond;

            if (SelectedFirstSkill >= 0 && SelectedFirstSkill < CombatSkillList.Count)
                SelectAsFirst = CombatSkillList[SelectedFirstSkill];
            else
                SelectAsFirst = PowerSkill.Internal_None;

            if (SelectedSecondSkill >= 0 && SelectedSecondSkill < CombatSkillList.Count)
                SelectAsSecond = CombatSkillList[SelectedSecondSkill];
            else
                SelectAsSecond = PowerSkill.Internal_None;

            List<PowerSkill> NewCombatSkillList = new List<PowerSkill>();

            IObjectDefinition PowerDefinition = ObjectList.Definitions[typeof(Power)];
            IList PowerList = PowerDefinition.ObjectList;

            foreach (Power PowerItem in PowerList)
            {
                if (PowerItem.IsUnavailable)
                    continue;

                if (PowerItem.TierEffectTable.Count == 0)
                    continue;

                int CombatSlotCount = 0;
                foreach (ItemSlot Slot in PowerItem.SlotList)
                    switch (Slot)
                    {
                        case ItemSlot.Internal_None:
                        case ItemSlot.None:
                        //case ItemSlot.Special1:
                        //case ItemSlot.Special2:
                            break;

                        default:
                            CombatSlotCount++;
                            break;
                    }

                if (CombatSlotCount == 0)
                    continue;

                if (PowerItem.Skill == PowerSkill.Internal_None ||
                    PowerItem.Skill == PowerSkill.AnySkill ||
                    PowerItem.Skill == PowerSkill.Endurance ||
                    PowerItem.Skill == PowerSkill.ShamanicInfusion)
                    continue;

                if (NewCombatSkillList.Contains(PowerItem.Skill))
                    continue;

                if (PowerItem.Skill == PowerSkill.Carpentry)
                    NewCombatSkillList.Add(PowerItem.Skill);
                else
                    NewCombatSkillList.Add(PowerItem.Skill);
            }

            NewCombatSkillList.Sort(SortSkillByName);

            CombatSkillList.Clear();
            foreach (PowerSkill Item in NewCombatSkillList)
                CombatSkillList.Add(Item);

            if (CombatSkillList.Contains(SelectAsFirst))
                SelectedFirstSkill = CombatSkillList.IndexOf(SelectAsFirst);
            else
                SelectedFirstSkill = -1;

            if (CombatSkillList.Contains(SelectAsSecond))
                SelectedSecondSkill = CombatSkillList.IndexOf(SelectAsSecond);
            else
                SelectedSecondSkill = -1;

            RefreshBuildPlaner();
        }

        private int SortSkillByName(PowerSkill skill1, PowerSkill skill2)
        {
            string s1 = skill1.ToString();
            string s2 = skill2.ToString();
            return string.Compare(s1, s2);
        }
        
        private void RefreshBuildPlaner()
        {
            if (SlotPlanerList == null)
                return;

            PowerSkill SelectAsFirst, SelectAsSecond;

            if (SelectedFirstSkill >= 0 && SelectedFirstSkill < CombatSkillList.Count)
                SelectAsFirst = CombatSkillList[SelectedFirstSkill];
            else
                SelectAsFirst = PowerSkill.Internal_None;

            if (SelectedSecondSkill >= 0 && SelectedSecondSkill < CombatSkillList.Count)
                SelectAsSecond = CombatSkillList[SelectedSecondSkill];
            else
                SelectAsSecond = PowerSkill.Internal_None;

            if (SelectAsFirst == SelectAsSecond)
            {
                SelectedFirstSkill = -1;
                SelectedSecondSkill = -1;
                NotifyPropertyChanged("SelectedFirstSkill");
                NotifyPropertyChanged("SelectedSecondSkill");
                SelectAsFirst = PowerSkill.Internal_None;
                SelectAsSecond = PowerSkill.Internal_None;
            }

            IObjectDefinition PowerDefinition = ObjectList.Definitions[typeof(Power)];
            IList<Power> PowerList = PowerDefinition.ObjectList as IList<Power>;
            IObjectDefinition AttributeDefinition = ObjectList.Definitions[typeof(PgJsonObjects.Attribute)];
            Dictionary<string, IGenericJsonObject> AttributeTable = AttributeDefinition.ObjectTable;

            foreach (SlotPlaner PlanerItem in SlotPlanerList)
                PlanerItem.RefreshCombatSkillList(PowerList, AttributeTable, SelectAsFirst, MaxLevelFirstSkill, SelectAsSecond, MaxLevelSecondSkill, DefaultMaxLevel);
        }

        private void RefreshWeightProfileList()
        {
            WeightProfileList.Clear();

            IObjectDefinition AttributeDefinition = ObjectList.Definitions[typeof(PgJsonObjects.Attribute)];
            IList<PgJsonObjects.Attribute> AttributeList = AttributeDefinition.ObjectList as IList<PgJsonObjects.Attribute>;

            if (!File.Exists(DefaultProfileName))
            {
                try
                {
                    using (FileStream fs = new FileStream(DefaultProfileName, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        using (StreamWriter sw = new StreamWriter(fs, Encoding.ASCII))
                        {
                            foreach (PgJsonObjects.Attribute Attribute in AttributeList)
                            {
                                sw.WriteLine(Attribute.Label + " " + "=" + " " + "0");
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }

            if (!File.Exists(BestArmorProfileName))
            {
                try
                {
                    using (FileStream fs = new FileStream(BestArmorProfileName, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        using (StreamWriter sw = new StreamWriter(fs, Encoding.ASCII))
                        {
                            foreach (PgJsonObjects.Attribute Attribute in AttributeList)
                            {
                                if (Attribute.Key == "MAX_ARMOR")
                                    sw.WriteLine(Attribute.Label + " " + "=" + " " + "1.0");
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }

            foreach (string FileName in Directory.GetFiles(ProfileFolder, "*.txt"))
            {
                if (FileName == DefaultProfileName)
                    continue;

                try
                {
                    WeightProfile NewProfile = null;

                    using (FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        using (StreamReader sr = new StreamReader(fs, Encoding.ASCII))
                        {
                            for (;;)
                            {
                                string Line = sr.ReadLine();
                                if (Line == null)
                                    break;

                                if (Line.Length == 0)
                                    continue;

                                string[] Split = Line.Split('=');
                                if (Split.Length != 2)
                                    continue;

                                string AttributeLabel = Split[0].Trim();
                                string AttributeWeightString = Split[1].Trim();
                                if (AttributeLabel.Length == 0 || AttributeWeightString.Length == 0)
                                    continue;

                                float AttributeWeight;
                                if (!float.TryParse(AttributeWeightString, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture.NumberFormat, out AttributeWeight))
                                    continue;

                                if (AttributeWeight <= 0)
                                    continue;

                                foreach (PgJsonObjects.Attribute Attribute in AttributeList)
                                    if (Attribute.Label == AttributeLabel)
                                    {
                                        if (NewProfile == null)
                                            NewProfile = new WeightProfile(Path.GetFileNameWithoutExtension(FileName));

                                        NewProfile.AddAttributeWeight(Attribute, AttributeWeight);
                                        break;
                                    }
                            }
                        }
                    }

                    if (NewProfile != null)
                        WeightProfileList.Add(NewProfile);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        private void RefreshGearPlaner()
        {
            WeightProfile SelectedProfile = (WeightProfileIndex >= 0 && WeightProfileIndex < WeightProfileList.Count) ? WeightProfileList[WeightProfileIndex] : null;
            ItemAttributeLink.SetSelectedProfile(SelectedProfile);

            IObjectDefinition AttributeDefinition = ObjectList.Definitions[typeof(PgJsonObjects.Attribute)];
            IList<PgJsonObjects.Attribute> AttributeList = AttributeDefinition.ObjectList as IList<PgJsonObjects.Attribute>;
            IObjectDefinition ItemDefinition = ObjectList.Definitions[typeof(Item)];
            IList<Item> ItemList = ItemDefinition.ObjectList as IList<Item>;

            foreach (SlotPlaner PlanerItem in SlotPlanerList)
                PlanerItem.RefreshGearList(ItemList, AttributeList, SelectedProfile, IgnoreUnobtainable, IgnoreNoAttribute);
        }

        private void AddPower(SlotPlaner SenderSlot)
        {
            SenderSlot.AddPower1();
            SenderSlot.AddPower2();
            SenderSlot.AddPower3();
            SenderSlot.AddPower4();
            SenderSlot.AddPower5();
        }

        private void RemovePower(SlotPlaner SenderSlot)
        {
            SenderSlot.RemovePower1();
            SenderSlot.RemovePower2();
            SenderSlot.RemovePower3();
            SenderSlot.RemovePower4();
            SenderSlot.RemovePower5();
        }

        private void LoadBuild()
        {
            OpenFileDialog Dlg = new OpenFileDialog();
            Dlg.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            bool? Result = Dlg.ShowDialog();

            if (Result.HasValue && Result.Value == true)
            {
                using (FileStream fs = new FileStream(Dlg.FileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (StreamReader sr = new StreamReader(fs, Encoding.ASCII))
                    {
                        LoadBuild(sr);
                    }
                }
            }
        }

        private void LoadBuild(StreamReader Reader)
        {
            while (!Reader.EndOfStream)
            {
                string Line = Reader.ReadLine();
                if (Line == null)
                    break;

                Line = Line.Trim();

                if (Line.Length > 0 && Line[0] == ';')
                    continue;

                string[] Split = Line.Split('=');
                if (Split.Length != 2)
                    continue;

                string FieldName = Split[0];
                string FieldValue = Split[1];

                if (FieldName == "FirstSkill")
                {
                    int MaxLevelIndexStart = FieldValue.IndexOf(" (");
                    if (MaxLevelIndexStart > 0)
                    {
                        int MaxLevelIndexEnd = FieldValue.IndexOf(")", MaxLevelIndexStart);
                        if (MaxLevelIndexEnd > MaxLevelIndexStart + 2)
                        {
                            string MaxLevelValueString = FieldValue.Substring(MaxLevelIndexStart + 2, MaxLevelIndexEnd - MaxLevelIndexStart - 2);

                            int MaxLevelValue;
                            if (int.TryParse(MaxLevelValueString, out MaxLevelValue))
                            {
                                MaxLevelFirstSkill = MaxLevelValue;
                            }
                        }

                        FieldValue = FieldValue.Substring(0, MaxLevelIndexStart);
                    }

                    PowerSkill ConvertedPowerSkill;
                    if (StringToEnumConversion<PowerSkill>.TryParse(FieldValue, out ConvertedPowerSkill, null))
                    {
                        int SkillIndex = CombatSkillList.IndexOf(ConvertedPowerSkill);
                        if (SkillIndex >= 0)
                        {
                            SelectedFirstSkill = SkillIndex;
                            RefreshBuildPlaner();
                            NotifyPropertyChanged("SelectedFirstSkill");
                        }
                    }
                }

                else if (FieldName == "SecondSkill")
                {
                    int MaxLevelIndexStart = FieldValue.IndexOf(" (");
                    if (MaxLevelIndexStart > 0)
                    {
                        int MaxLevelIndexEnd = FieldValue.IndexOf(")", MaxLevelIndexStart);
                        if (MaxLevelIndexEnd > MaxLevelIndexStart + 2)
                        {
                            string MaxLevelValueString = FieldValue.Substring(MaxLevelIndexStart + 2, MaxLevelIndexEnd - MaxLevelIndexStart - 2);

                            int MaxLevelValue;
                            if (int.TryParse(MaxLevelValueString, out MaxLevelValue))
                            {
                                MaxLevelSecondSkill = MaxLevelValue;
                            }
                        }

                        FieldValue = FieldValue.Substring(0, MaxLevelIndexStart);
                    }

                    PowerSkill ConvertedPowerSkill;
                    if (StringToEnumConversion<PowerSkill>.TryParse(FieldValue, out ConvertedPowerSkill, null))
                    {
                        int SkillIndex = CombatSkillList.IndexOf(ConvertedPowerSkill);
                        if (SkillIndex >= 0)
                        {
                            SelectedSecondSkill = SkillIndex;
                            RefreshBuildPlaner();
                            NotifyPropertyChanged("SelectedSecondSkill");
                        }
                    }
                }

                else if (FieldName == "GearProfile")
                {
                    int GearProfileIndex = -1;
                    for (int i = 0; i < WeightProfileList.Count; i++)
                        if (WeightProfileList[i].Name == FieldValue)
                        {
                            GearProfileIndex = i;
                            break;
                        }

                    if (GearProfileIndex >= 0)
                        WeightProfileIndex = GearProfileIndex;
                    else
                        WeightProfileIndex = -1;
                    NotifyPropertyChanged("WeightProfileIndex");
                }

                else
                {
                    IObjectDefinition PowerDefinition = ObjectList.Definitions[typeof(Power)];
                    Dictionary<string, IGenericJsonObject> PowerTable = PowerDefinition.ObjectTable;
                    IObjectDefinition ItemDefinition = ObjectList.Definitions[typeof(Item)];
                    Dictionary<string, IGenericJsonObject> ItemTable = ItemDefinition.ObjectTable;

                    ItemSlot ParsedSlot;
                    if (StringToEnumConversion<ItemSlot>.TryParse(FieldName, out ParsedSlot, null))
                    {
                        if (PowerTable.ContainsKey(FieldValue))
                        {
                            Power SlotPower = PowerTable[FieldValue] as Power;

                            foreach (SlotPlaner Planer in SlotPlanerList)
                                if (Planer.Slot == ParsedSlot)
                                {
                                    foreach (PlanerSlotPower PlanerSlot in Planer.AvailablePowerList1)
                                        if (PlanerSlot.Reference == SlotPower)
                                        {
                                            Planer.SelectPower1(PlanerSlot);
                                            break;
                                        }

                                    foreach (PlanerSlotPower PlanerSlot in Planer.AvailablePowerList2)
                                        if (PlanerSlot.Reference == SlotPower)
                                        {
                                            Planer.SelectPower2(PlanerSlot);
                                            break;
                                        }

                                    foreach (PlanerSlotPower PlanerSlot in Planer.AvailablePowerList3)
                                        if (PlanerSlot.Reference == SlotPower)
                                        {
                                            Planer.SelectPower3(PlanerSlot);
                                            break;
                                        }

                                    foreach (PlanerSlotPower PlanerSlot in Planer.AvailablePowerList4)
                                        if (PlanerSlot.Reference == SlotPower)
                                        {
                                            Planer.SelectPower4(PlanerSlot);
                                            break;
                                        }

                                    foreach (PlanerSlotPower PlanerSlot in Planer.AvailablePowerList5)
                                        if (PlanerSlot.Reference == SlotPower)
                                        {
                                            Planer.SelectPower5(PlanerSlot);
                                            break;
                                        }

                                    break;
                                }
                        }
                        else if (ItemTable.ContainsKey(FieldValue))
                        {
                            Item SlotItem = ItemTable[FieldValue] as Item;

                            foreach (SlotPlaner Planer in SlotPlanerList)
                                if (Planer.Slot == ParsedSlot)
                                {
                                    Planer.SelectGear(SlotItem);
                                    break;
                                }
                        }
                    }
                }
            }
        }

        private void SaveBuild()
        {
            SaveFileDialog Dlg = new SaveFileDialog();
            Dlg.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            bool? Result = Dlg.ShowDialog();

            if (Result.HasValue && Result.Value == true)
            {
                using (FileStream fs = new FileStream(Dlg.FileName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    using (StreamWriter sw = new StreamWriter(fs, Encoding.ASCII))
                    {
                        SaveBuild(sw);
                    }
                }
            }
        }

        private void SaveBuild(StreamWriter sw)
        {
            if (SelectedFirstSkill >= 0)
            {
                string FirstSkillName = CombatSkillList[SelectedFirstSkill].ToString();
                sw.WriteLine("FirstSkill" + "=" + FirstSkillName + " (" + MaxLevelFirstSkill + ")");
            }

            if (SelectedSecondSkill >= 0)
            {
                string SecondSkillName = CombatSkillList[SelectedSecondSkill].ToString();
                sw.WriteLine("SecondSkill" + "=" + SecondSkillName + " (" + MaxLevelSecondSkill + ")");
            }

            if (WeightProfileIndex >= 0)
            {
                string WeightProfileName = WeightProfileList[WeightProfileIndex].Name;
                sw.WriteLine("GearProfile" + "=" + WeightProfileName);
            }

            foreach (SlotPlaner Planer in SlotPlanerList)
            {
                string SlotName = Planer.Slot.ToString();

                foreach (PlanerSlotPower PlanerSlot in Planer.SelectedPowerList1)
                {
                    Power Reference = PlanerSlot.Reference;
                    string Key = Reference.Key;

                    sw.WriteLine(SlotName + "=" + Key);
                }

                foreach (PlanerSlotPower PlanerSlot in Planer.SelectedPowerList2)
                {
                    Power Reference = PlanerSlot.Reference;
                    string Key = Reference.Key;

                    sw.WriteLine(SlotName + "=" + Key);
                }

                foreach (PlanerSlotPower PlanerSlot in Planer.SelectedPowerList3)
                {
                    Power Reference = PlanerSlot.Reference;
                    string Key = Reference.Key;

                    sw.WriteLine(SlotName + "=" + Key);
                }

                foreach (PlanerSlotPower PlanerSlot in Planer.SelectedPowerList4)
                {
                    Power Reference = PlanerSlot.Reference;
                    string Key = Reference.Key;

                    sw.WriteLine(SlotName + "=" + Key);
                }

                foreach (PlanerSlotPower PlanerSlot in Planer.SelectedPowerList5)
                {
                    Power Reference = PlanerSlot.Reference;
                    string Key = Reference.Key;

                    sw.WriteLine(SlotName + "=" + Key);
                }

                if (Planer.SelectedGearIndex >= 0 && Planer.SelectedGearIndex < Planer.SortedGearList.Count)
                {
                    Item PlanerItem = Planer.SortedGearList[Planer.SelectedGearIndex];
                    string Key = PlanerItem.Key;

                    sw.WriteLine(SlotName + "=" + Key);
                }
            }
        }

        private void CopyBuild()
        {
            string Text = "";

            foreach (SlotPlaner Planer in SlotPlanerList)
            {
                string SlotName = Planer.Slot.ToString();
                string GearName = "";

                if (Planer.SelectedGearIndex >= 0 && Planer.SelectedGearIndex < Planer.SortedGearList.Count)
                {
                    Item PlanerItem = Planer.SortedGearList[Planer.SelectedGearIndex];
                    GearName = " " + PlanerItem.Name;
                }

                if (Text.Length > 0)
                    Text += "\r\n";

                Text += SlotName + ":" + GearName + "\r\n";

                foreach (PlanerSlotPower PlanerSlot in Planer.SelectedPowerList1)
                {
                    Power Reference = PlanerSlot.Reference;
                    string Key = Reference.Key;

                    Text += PlanerSlot.Name + "\r\n";
                }

                foreach (PlanerSlotPower PlanerSlot in Planer.SelectedPowerList2)
                {
                    Power Reference = PlanerSlot.Reference;
                    string Key = Reference.Key;

                    Text += PlanerSlot.Name + "\r\n";
                }

                foreach (PlanerSlotPower PlanerSlot in Planer.SelectedPowerList3)
                {
                    Power Reference = PlanerSlot.Reference;
                    string Key = Reference.Key;

                    Text += PlanerSlot.Name + "\r\n";
                }

                foreach (PlanerSlotPower PlanerSlot in Planer.SelectedPowerList4)
                {
                    Power Reference = PlanerSlot.Reference;
                    string Key = Reference.Key;

                    Text += PlanerSlot.Name + "\r\n";
                }

                foreach (PlanerSlotPower PlanerSlot in Planer.SelectedPowerList5)
                {
                    Power Reference = PlanerSlot.Reference;
                    string Key = Reference.Key;

                    Text += PlanerSlot.Name + "\r\n";
                }
            }

            Clipboard.SetText(Text);

            MessageBox.Show("The clipboard now contains a text description of this build.", "Build Planner", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void OnProfileSelected(object sender, SelectionChangedEventArgs e)
        {
            RefreshGearPlaner();
        }

        private void OnIgnoreUnobtainableCheckChanged(object sender, RoutedEventArgs e)
        {
            RefreshGearPlaner();
        }

        private void OnIgnoreNoAttributeCheckChanged(object sender, RoutedEventArgs e)
        {
            RefreshGearPlaner();
        }

        private List<SlotPlaner> SlotPlanerList;
        #endregion

        #region Gear Planer
        private void InitGearPlaner()
        {
            ProfileFolder = Path.Combine(ApplicationFolder, "Profiles");

            if (!Directory.Exists(ProfileFolder))
                Directory.CreateDirectory(ProfileFolder);

            DefaultProfileName = Path.Combine(ProfileFolder, "default.txt");
            BestArmorProfileName = Path.Combine(ProfileFolder, "Best Armor.txt");
        }

        private string ProfileFolder;
        private string DefaultProfileName;
        private string BestArmorProfileName;
        #endregion

        #region Search
        public string SearchTerms { get; set; }
        public bool MustMatchCase { get; set; }
        public bool MustMatchWholeWord { get; set; }
        public bool IncludeAbility { get; set; }
        public bool IncludeDirectedGoal { get; set; }
        public bool IncludeGameNpc { get; set; }
        public bool IncludeStorageVault { get; set; }
        public bool IncludeEffect { get; set; }
        public bool IncludeItem { get; set; }
        public bool IncludeQuest { get; set; }
        public bool IncludeRecipe { get; set; }
        public bool IncludeSkill { get; set; }
        public bool IncludePower { get; set; }
        public bool IncludeLoreBook { get; set; }

        public ObservableCollection<object> SearchResult { get; private set; }

        public object SearchSelectedItem
        {
            get { return _SearchSelectedItem; }
            set
            {
                if (_SearchSelectedItem != value)
                {
                    _SearchSelectedItem = value;
                    NotifyThisPropertyChanged();

                    int Index = CurrentSearchItem != null ? SearchHistory.IndexOf(CurrentSearchItem) : -1;
                    if (Index >= 0 && Index + 1 < SearchHistory.Count)
                        SearchHistory.RemoveRange(Index + 1, SearchHistory.Count - Index - 1);

                    SearchHistory.Add(value);
                    CurrentSearchItem = value;
                    IsBackwardEnabled = true;
                    IsForwardEnabled = false;
                }
            }
        }
        private object _SearchSelectedItem;

        public object CurrentSearchItem
        {
            get { return _CurrentSearchItem; }
            set
            {
                if (_CurrentSearchItem != value)
                {
                    _CurrentSearchItem = value;
                    NotifyThisPropertyChanged();
                }
            }
        }
        private object _CurrentSearchItem;

        public bool IsBackwardEnabled
        {
            get { return _IsBackwardEnabled; }
            private set
            {
                if (_IsBackwardEnabled != value)
                {
                    _IsBackwardEnabled = value;
                    NotifyThisPropertyChanged();
                }
            }
        }
        public bool _IsBackwardEnabled;

        public bool IsForwardEnabled
        {
            get { return _IsForwardEnabled; }
            private set
            {
                if (_IsForwardEnabled != value)
                {
                    _IsForwardEnabled = value;
                    NotifyThisPropertyChanged();
                }
            }
        }
        public bool _IsForwardEnabled;

        private void InitSearch()
        {
            SearchTerms = "";
            MustMatchCase = false;
            MustMatchWholeWord = false;
            IncludeAbility = true;
            IncludeDirectedGoal = true;
            IncludeGameNpc = true;
            IncludeStorageVault = true;
            IncludeEffect = true;
            IncludeItem = true;
            IncludeQuest = true;
            IncludeRecipe = true;
            IncludeSkill = true;
            IncludePower = true;
            IncludeLoreBook = true;
            SearchResult = new ObservableCollection<object>();
            SearchHistory = new List<object>();
            _CurrentSearchItem = null;
            _IsBackwardEnabled = false;
            _IsForwardEnabled = false;
        }

        private void OnSearchTermsEntered()
        {
            PerformSearch();
        }

        private void OnSearchCheckChanged()
        {
            PerformSearch();
        }

        private void PerformSearch()
        {
            if (SearchTerms == null || SearchTerms.Trim().Length < 3)
                return;

            List<string> ValidTermList = new List<string>();
            string[] AndTerms = SearchTerms.Split(new string[] { " AND " }, StringSplitOptions.None);
            string[] OrTerms = SearchTerms.Split(new string[] { " OR " }, StringSplitOptions.None);
            SearchModes SearchMode;

            if (AndTerms.Length > 1 && OrTerms.Length == 1)
            {
                SearchMode = SearchModes.And;
                foreach (string Term in AndTerms)
                    if (Term.Trim().Length > 0)
                        ValidTermList.Add(Term.Trim());
            }

            else if (AndTerms.Length == 1 && OrTerms.Length > 1)
            {
                SearchMode = SearchModes.Or;
                foreach (string Term in OrTerms)
                    if (Term.Trim().Length > 0)
                        ValidTermList.Add(Term.Trim());
            }
            else
            {
                SearchMode = SearchModes.Neither;
                ValidTermList.Add(SearchTerms.Trim());
            }

            if (ValidTermList.Count > 0)
            {
                SearchResult.Clear();
                SearchHistory.Clear();
                CurrentSearchItem = null;
                IsBackwardEnabled = false;
                IsForwardEnabled = false;

                PerformSearch(ValidTermList, SearchMode);
            }
        }

        private void PerformSearch(List<string> TermList, SearchModes SearchMode)
        {
            foreach (KeyValuePair<Type, IObjectDefinition> Entry in ObjectList.Definitions)
            {
                Type EntryType = Entry.Key;

                if ((EntryType == typeof(Ability) && IncludeAbility) ||
                    (EntryType == typeof(DirectedGoal) && IncludeDirectedGoal) ||
                    (EntryType == typeof(GameNpc) && IncludeGameNpc) ||
                    (EntryType == typeof(StorageVault) && IncludeStorageVault) ||
                    (EntryType == typeof(Effect) && IncludeEffect) ||
                    (EntryType == typeof(Item) && IncludeItem) ||
                    (EntryType == typeof(Quest) && IncludeQuest) ||
                    (EntryType == typeof(Recipe) && IncludeRecipe) ||
                    (EntryType == typeof(Skill) && IncludeSkill) ||
                    (EntryType == typeof(Power) && IncludePower) ||
                    (EntryType == typeof(LoreBook) && IncludeLoreBook))
                    PerformSearch(TermList, Entry.Value, SearchMode);
            }
        }

        private void PerformSearch(List<string> TermList, IObjectDefinition Definition, SearchModes SearchMode)
        {
            try
            {
                string IndexFilePath = Path.Combine(CurrentVersionCacheFolder, Definition.JsonFileName + "-index.txt");
                if (!File.Exists(IndexFilePath))
                    return;

                using (FileStream fs = new FileStream(IndexFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                    {
                        string TextContent = sr.ReadToEnd();

                        Dictionary<string, IGenericJsonObject> ObjectTable = Definition.ObjectTable;
                        PerformSearch(TermList, ObjectTable, TextContent, SearchMode);
                    }
                }
            }
            catch
            {

            }
        }

        private void PerformSearch(List<string> TermList, IDictionary ObjectTable, string TextContent, SearchModes SearchMode)
        {
            List<GenericJsonObject> Result = new List<GenericJsonObject>();

            switch (SearchMode)
            {
                case SearchModes.And:
                    for (int i = 0; i < TermList.Count; i++)
                    {
                        List<GenericJsonObject> SingleTermResult = new List<GenericJsonObject>();
                        PerformSearch(TermList[i], ObjectTable, TextContent, SingleTermResult);

                        if (i == 0)
                        {
                            foreach (GenericJsonObject o in SingleTermResult)
                                Result.Add(o);
                        }
                        else
                        {
                            List<GenericJsonObject> ToRemove = new List<GenericJsonObject>();
                            foreach (GenericJsonObject o in Result)
                                if (!SingleTermResult.Contains(o))
                                    ToRemove.Add(o);

                            foreach (GenericJsonObject o in ToRemove)
                                Result.Remove(o);
                        }
                    }
                    break;

                case SearchModes.Or:
                    foreach (string Term in TermList)
                    {
                        List<GenericJsonObject> SingleTermResult = new List<GenericJsonObject>();
                        PerformSearch(Term, ObjectTable, TextContent, SingleTermResult);

                        foreach (GenericJsonObject o in SingleTermResult)
                            if (!Result.Contains(o))
                                Result.Add(o);
                    }
                    break;

                case SearchModes.Neither:
                    PerformSearch(TermList[0], ObjectTable, TextContent, Result);
                    break;
            }

            Result.Sort(GenericJsonObject.SortByName);

            foreach (object o in Result)
                SearchResult.Add(o);
        }

        private void PerformSearch(string Term, IDictionary ObjectTable, string TextContent, List<GenericJsonObject> SingleTermResult)
        {
            int MatchIndex = -1;
            StringComparison Comparison = MustMatchCase ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase;

            while ((MatchIndex = TextContent.IndexOf(Term, MatchIndex + 1, Comparison)) >= 0)
            {
                int KeyIndex = TextContent.IndexOf(JsonGenerator.ObjectSeparator, MatchIndex + Term.Length);
                if (KeyIndex < 0)
                    break;

                KeyIndex++;

                string Key = "";

                for (;;)
                {
                    if (KeyIndex >= TextContent.Length)
                        break;

                    char c = TextContent[KeyIndex++];
                    if (c == '\r' || c == '\n')
                        break;

                    Key += c;
                }

                for (;;)
                {
                    if (KeyIndex >= TextContent.Length)
                        break;

                    char c = TextContent[KeyIndex];
                    if (c != '\r' && c != '\n')
                        break;

                    KeyIndex++;
                }

                if (ObjectTable.Contains(Key))
                    SingleTermResult.Add((GenericJsonObject)ObjectTable[Key]);

                MatchIndex = KeyIndex;

                if (MatchIndex + Term.Length >= TextContent.Length)
                    break;

                MatchIndex--;
            }
        }

        private void OnBackward()
        {
            int Index = SearchHistory.IndexOf(CurrentSearchItem);
            if (Index > 0)
            {
                CurrentSearchItem = SearchHistory[Index - 1];

                if (Index == 1)
                    IsBackwardEnabled = false;
                IsForwardEnabled = true;
            }
        }

        private void OnForward()
        {
            int Index = SearchHistory.IndexOf(CurrentSearchItem);
            if (Index >= 0 && Index + 1 < SearchHistory.Count)
            {
                CurrentSearchItem = SearchHistory[Index + 1];

                if (Index + 1 == SearchHistory.Count - 1)
                    IsForwardEnabled= false;
                IsBackwardEnabled = true;
            }
        }

        private void OnRequestNavigate(object FromObject, string PropertyName)
        {
            object ToObject = null;

            Type ObjectType = FromObject.GetType();

            if (PropertyName== "hyperlink")
                ToObject = FromObject;
            else
            {
                PropertyInfo pi = ObjectType.GetProperty(PropertyName);
                if (pi != null)
                    ToObject = pi.GetValue(FromObject);
            }

            if (ToObject != null)
            {
                int Index = CurrentSearchItem != null ? SearchHistory.IndexOf(CurrentSearchItem) : -1;
                if (Index >= 0 && Index + 1 < SearchHistory.Count)
                    SearchHistory.RemoveRange(Index + 1, SearchHistory.Count - Index - 1);

                SearchHistory.Add(ToObject);
                CurrentSearchItem = ToObject;
                IsBackwardEnabled = true;
                IsForwardEnabled = false;
            }
        }

        private List<object> SearchHistory;
        #endregion

        #region Cruncher
        private void InitCruncher()
        {
            CruncherCategoryIndex = 0;
            CrunchSelectionList = new ObservableCollection<CrunchSelection>();
            _IsCrunching = false;
            _CrunchProgress = 0;
        }

        public int CruncherCategoryIndex { get; set; }
        public ObservableCollection<CrunchSelection> CrunchSelectionList { get; private set; }

        public bool IsCrunching
        {
            get { return _IsCrunching; }
            set
            {
                if (_IsCrunching != value)
                {
                    _IsCrunching = value;
                    NotifyThisPropertyChanged();
                }
            }
        }
        private bool _IsCrunching;

        public double CrunchProgress
        {
            get { return _CrunchProgress; }
            set
            {
                if (_CrunchProgress != value)
                {
                    _CrunchProgress = value;
                    NotifyThisPropertyChanged();
                }
            }
        }
        private double _CrunchProgress;

        private void Crunch()
        {
            IsCrunching = true;

            CrunchSelectionList.Clear();

            IObjectDefinition SkillDefinition = ObjectList.Definitions[typeof(Skill)];
            IList<Skill> SkillList = SkillDefinition.ObjectList as IList<Skill>;

            List<Skill> CombatSkillList = new List<Skill>();
            foreach (Skill SkillItem in SkillList)
                if (IsCombatSkill(SkillItem))
                        CombatSkillList.Add(SkillItem);

            Dispatcher.BeginInvoke(new Action(() => OnCrunchSkills(CombatSkillList, 0, 0)));
        }

        private void OnCrunchSkills(List<Skill> CombatSkillList, int i, int j)
        {
            CrunchProgress = ((double)(i * CombatSkillList.Count + j)) / ((double)(CombatSkillList.Count * CombatSkillList.Count));

            Skill PrimarySkill = CombatSkillList[i];
            Skill SecondarySkill = CombatSkillList[j];
            if (IsValidCombination(PrimarySkill, SecondarySkill))
            {
                CrunchSelection Selection;
                Crunch(PrimarySkill, SecondarySkill, out Selection);

                int k;
                for (k = 0; k < CrunchSelectionList.Count; k++)
                    if (CrunchSelectionList[k].BestDPS < Selection.BestDPS)
                        break;

                CrunchSelectionList.Insert(i, Selection);
            }

            if (j + 1 < CombatSkillList.Count)
                Dispatcher.BeginInvoke(new Action(() => OnCrunchSkills(CombatSkillList, i, j + 1)));
            else if (i + 1 < CombatSkillList.Count)
                Dispatcher.BeginInvoke(new Action(() => OnCrunchSkills(CombatSkillList, i + 1, 0)));
            else
                IsCrunching = false;
        }

        private bool IsCombatSkill(Skill SkillItem)
        {
            if (!SkillItem.Combat)
                return false;

            if (SkillItem.XpTable.InternalName != "TypicalCombatSkill" && SkillItem.XpTable.InternalName != "TypicalCombatSkillExt")
                return false;

            IObjectDefinition AbilityDefinition = ObjectList.Definitions[typeof(Ability)];
            IList<Ability> AbilityList = AbilityDefinition.ObjectList as IList<Ability>;

            int AbilityCount = 0;
            foreach (Ability Item in AbilityList)
                if (SkillItem.CombatSkill != PowerSkill.Internal_None && Item.Skill == SkillItem.CombatSkill)
                    AbilityCount++;

            if (AbilityCount < 6)
                return false;

            //Debug.WriteLine(SkillItem.ToString() + ": " + SkillItem.XpTable + ", " + AbilityCount);

            return true;
        }

        private bool IsValidCombination(Skill PrimarySkill, Skill SecondarySkill)
        {
            if (PrimarySkill.CompatibleCombatSkillList.Contains(SecondarySkill.CombatSkill) && SecondarySkill.CompatibleCombatSkillList.Contains(PrimarySkill.CombatSkill))
                return true;

            //Debug.WriteLine("Incompatible skills: " + PrimarySkill.Key + " and " + SecondarySkill.Key);

            return false;
        }

        private void Crunch(Skill PrimarySkill, Skill SecondarySkill, out CrunchSelection Selection)
        {
            List<Ability> PrimaryAbilityList = SelectAbilities(PrimarySkill);
            List<Ability> SecondaryAbilityList = SelectAbilities(SecondarySkill);

            for (;;)
            {
                int OldPrimaryAbilityCount = PrimaryAbilityList.Count;
                int OldSecondaryAbilityCount = SecondaryAbilityList.Count;

                List<Ability> SelectedAbilityList = new List<Ability>();

                for (int i = 0; i < 6; i++)
                    SelectedAbilityList.Add(PrimaryAbilityList[i]);
                for (int i = 0; i < 6; i++)
                    SelectedAbilityList.Add(SecondaryAbilityList[i]);

                CrunchSelection SequenceSelection;
                Crunch(PrimarySkill, SecondarySkill, SelectedAbilityList, out SequenceSelection);

                for (int i = 0; i < 6; i++)
                    if (!SequenceSelection.BestSequenceAbilityList.Contains(PrimaryAbilityList[i]))
                        if (PrimaryAbilityList.Count > 6)
                            PrimaryAbilityList.Remove(PrimaryAbilityList[i]);

                for (int i = 0; i < 6; i++)
                    if (!SequenceSelection.BestSequenceAbilityList.Contains(SecondaryAbilityList[i]))
                        if (SecondaryAbilityList.Count > 6)
                            SecondaryAbilityList.Remove(SecondaryAbilityList[i]);

                int NewPrimaryAbilityCount = PrimaryAbilityList.Count;
                int NewSecondaryAbilityCount = SecondaryAbilityList.Count;

                if (NewPrimaryAbilityCount == OldPrimaryAbilityCount && NewSecondaryAbilityCount == OldSecondaryAbilityCount)
                    break;
            }

            Selection = null;

            Sequence PrimarySequence = Sequence.CreateSeparate(6, PrimaryAbilityList.Count);
            while (!PrimarySequence.IsCompleted)
            {
                Sequence SecondarySequence = Sequence.CreateSeparate(6, SecondaryAbilityList.Count);
                while (!SecondarySequence.IsCompleted)
                {
                    List<Ability> SelectedAbilityList = new List<Ability>();
                    int[] PrimaryArray = PrimarySequence.Array;
                    int[] SecondaryArray = SecondarySequence.Array;

                    for (int i = 0; i < PrimaryArray.Length; i++)
                        SelectedAbilityList.Add(PrimaryAbilityList[PrimaryArray[i]]);
                    for (int i = 0; i < SecondaryArray.Length; i++)
                        SelectedAbilityList.Add(SecondaryAbilityList[SecondaryArray[i]]);

                    CrunchSelection SequenceSelection;
                    Crunch(PrimarySkill, SecondarySkill, SelectedAbilityList, out SequenceSelection);

                    if (Selection == null || Selection.BestDPS < SequenceSelection.BestDPS)
                        Selection = SequenceSelection;

                    SecondarySequence.NextSeparate();
                }

                PrimarySequence.NextSeparate();
            }
        }

        private void Crunch(Skill PrimarySkill, Skill SecondarySkill, List<Ability> SelectedAbilityList, out CrunchSelection Selection)
        {
            Dictionary<ItemSlot, List<Power>> Gear = SelectGear(PrimarySkill, SecondarySkill);

            List<CrunchTarget> TargetList = new List<CrunchTarget>();
            TargetList.Add(Target1);

            Selection = new CrunchSelection(PrimarySkill, SecondarySkill, SelectedAbilityList, Gear, TargetList);
            Selection.Crunch();
        }

        private List<Ability> SelectAbilities(Skill SelectedSkill)
        {
            List<Ability> LineAbilityList = new List<Ability>();
            int MaxLevel;
            if (CrunchSelection.MaxAttainableLevel.ContainsKey(SelectedSkill.CombatSkill))
                MaxLevel = CrunchSelection.MaxAttainableLevel[SelectedSkill.CombatSkill];
            else
                MaxLevel = int.MaxValue;

            Dictionary <Ability, List<Ability>> AbilitiesSortedByLineName = new Dictionary<Ability, List<Ability>>();
            IObjectDefinition AbilityDefinition = ObjectList.Definitions[typeof(Ability)];
            IList<Ability> AbilityList = AbilityDefinition.ObjectList as IList<Ability>;

            foreach (Ability AbilityItem in AbilityList)
            {
                if (!AbilityItem.IsAbilityValidForCrunch)
                    continue;

                if (AbilityItem.Level > MaxLevel)
                    continue;

                if ((AbilityItem.InternalName != null) && (AbilityItem.Skill == SelectedSkill.CombatSkill))
                {
                    List<Ability> SortedAbilityList;

                    Ability LineAbility = AbilityUpgradeOf(AbilityItem);

                    if (LineAbility == null || !AbilitiesSortedByLineName.ContainsKey(LineAbility))
                    {
                        SortedAbilityList = new List<Ability>();
                        AbilitiesSortedByLineName.Add(AbilityItem, SortedAbilityList);
                    }
                    else
                    {
                        SortedAbilityList = AbilitiesSortedByLineName[LineAbility];
                        Ability Prerequisite = AbilityPrerequisite(AbilityItem);

                        if (Prerequisite != null)
                            foreach (Ability SortedAbility in SortedAbilityList)
                                if (Prerequisite == SortedAbility)
                                {
                                    SortedAbilityList.Remove(SortedAbility);
                                    break;
                                }
                    }

                    SortedAbilityList.Add(AbilityItem);
                }
            }

            foreach (KeyValuePair<Ability, List<Ability>> Entry in AbilitiesSortedByLineName)
                LineAbilityList.AddRange(Entry.Value);

            foreach (Ability Ability in LineAbilityList)
                if (Ability.SpecialInfo != null && Ability.SpecialInfo.Length > 0 && Ability.AbilityAdditionalResultList.Count == 0)
                    Debug.WriteLine(Ability.SpecialInfo);

            return LineAbilityList;
        }

        private Ability AbilityUpgradeOf(Ability AbilityItem)
        {
            if (AbilityItem.UpgradeOf != null)
                return AbilityItem.UpgradeOf;

            return AbilityPrevious(AbilityItem);
        }

        private Ability AbilityPrerequisite(Ability AbilityItem)
        {
            if (AbilityItem.Prerequisite != null)
                return AbilityItem.Prerequisite;

            return AbilityPrevious(AbilityItem);
        }

        private Ability AbilityPrevious(Ability AbilityItem)
        {
            IObjectDefinition AbilityDefinition = ObjectList.Definitions[typeof(Ability)];
            IList<Ability> AbilityList = AbilityDefinition.ObjectList as IList<Ability>;

            if (AbilityItem.UpgradeOf != null)
                return AbilityItem.UpgradeOf;

            if (AbilityItem.Prerequisite != null)
                return AbilityItem.Prerequisite;

            if (AbilityItem.LineIndex <= 0)
                return null;

            foreach (Ability Item in AbilityList)
                if (Item.DigitStrippedName == AbilityItem.DigitStrippedName)
                    if (Item.LineIndex + 1 == AbilityItem.LineIndex)
                        return Item;

            foreach (Ability Item in AbilityList)
                if (Item.DigitStrippedName == AbilityItem.DigitStrippedName)
                    if (Item.LineIndex == -1)
                        return Item;

            if (AbilityItem.LineIndex == 1)
                return null;

            return null;
        }

        private Dictionary<ItemSlot, List<Power>> SelectGear(Skill PrimarySkill, Skill SecondarySkill)
        {
            Dictionary<ItemSlot, List<Power>> Gear = new Dictionary<ItemSlot, List<Power>>();
            IObjectDefinition PowerDefinition = ObjectList.Definitions[typeof(Power)];
            IList<Power> PowerList = PowerDefinition.ObjectList as IList<Power>;

            List<ItemSlot> SlotList = new List<ItemSlot>()
            {
                ItemSlot.Feet,
                ItemSlot.Head,
                ItemSlot.Chest,
                ItemSlot.Legs,
                ItemSlot.Necklace,
                ItemSlot.Ring,
                ItemSlot.Hands,
                ItemSlot.MainHand,
                ItemSlot.OffHand
            };

            foreach (ItemSlot Slot in SlotList)
            {
                List<Power> PrimaryPowerListForSlot = new List<Power>();
                List<Power> SecondaryPowerListForSlot = new List<Power>();

                foreach (Power PowerItem in PowerList)
                    if (PowerItem.IsValidForSlot(PrimarySkill.CombatSkill, Slot))
                        PrimaryPowerListForSlot.Add(PowerItem);
                    else if (PowerItem.IsValidForSlot(SecondarySkill.CombatSkill, Slot))
                        SecondaryPowerListForSlot.Add(PowerItem);

                List<Power> PowerListForSlot = new List<Power>();
                SelectGearForSlot(PrimaryPowerListForSlot, SecondaryPowerListForSlot, PowerListForSlot);

                Gear.Add(Slot, PowerListForSlot);
            }

            return Gear;
        }

        private void SelectGearForSlot(List<Power> PrimaryPowerListForSlot, List<Power> SecondaryPowerListForSlot, List<Power> PowerListForSlot)
        {
            int i = 0;
            List<Power> List1 = new List<Power>(PrimaryPowerListForSlot);
            List<Power> List2 = new List<Power>(PrimaryPowerListForSlot);
            int ModCount1 = 0;
            int ModCount2 = 0;

            for (i = 0; i < 7; i++)
            {
                Power SelectedPower;
                if (!SelectMod(List1, List2, ref ModCount1, ref ModCount2, out SelectedPower))
                    break;

                PowerListForSlot.Add(SelectedPower);
            }
        }

        private bool SelectMod(List<Power> List1, List<Power> List2, ref int ModCount1, ref int ModCount2, out Power SelectedPower)
        {
            if (List1.Count == 0 && List2.Count == 0)
            {
                SelectedPower = null;
                return false;
            }

            List<Power> SelectedList;

            if (ModCount1 + 1 < ModCount2 && List1.Count > 0)
            {
                SelectedList = List1;
                ModCount1++;
            }

            else if (ModCount2 + 1 < ModCount1 && List2.Count > 0)
            {
                SelectedList = List2;
                ModCount2++;
            }

            else if (List2.Count == 0)
            {
                SelectedList = List1;
                ModCount1++;
            }

            else if (List1.Count == 0)
            {
                SelectedList = List2;
                ModCount2++;
            }
            else
            {
                //TODO: select list randomly
                SelectedList = List1;
                ModCount1++;
            }

            //TODO: select index randomly
            int Index = 0;

            SelectedPower = SelectedList[Index];
            SelectedList.RemoveAt(Index);

            return true;
        }

        private CrunchTarget Target1 = new CrunchTarget() { MaxArmor = 100, MaxHealth = 1000, MaxRage = 100, Effective = new List<DamageType> { DamageType.Cold } };
        #endregion

        #region Events
        private void OnRefreshBuildPlaner(object sender, SelectionChangedEventArgs e)
        {
            RefreshBuildPlaner();
        }

        private void OnAddPower(object sender, EventArgs e)
        {
            SlotPlaner SenderSlot = (e as ExecutedEventArgs).Parameter as SlotPlaner;
            AddPower(SenderSlot);
        }

        private void OnRemovePower(object sender, EventArgs e)
        {
            SlotPlaner SenderSlot = (e as ExecutedEventArgs).Parameter as SlotPlaner;
            RemovePower(SenderSlot);
        }

        private void OnLoadBuild(object sender, EventArgs e)
        {
            LoadBuild();
        }

        private void OnSaveBuild(object sender, EventArgs e)
        {
            SaveBuild();
        }

        private void OnCopyBuild(object sender, EventArgs e)
        {
            CopyBuild();
        }

        private void OnCrunch(object sender, EventArgs e)
        {
            Crunch();
        }

        private void OnOpenProfileFolder(object sender, EventArgs e)
        {
            Process Explorer = new Process();
            Explorer.StartInfo.FileName = "explorer.exe";
            Explorer.StartInfo.Arguments = ProfileFolder;
            Explorer.StartInfo.UseShellExecute = true;

            Explorer.Start();
        }

        private void OnSearchTermsEntered(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                OnSearchTermsEntered();
        }

        private void OnSearchCheckChanged(object sender, RoutedEventArgs e)
        {
            OnSearchCheckChanged();
        }

        private void OnBackward(object sender, EventArgs e)
        {
            OnBackward();
        }

        private void OnForward(object sender, EventArgs e)
        {
            OnForward();
        }

        private void OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Hyperlink hyperlink = e.OriginalSource as Hyperlink;
            if (hyperlink != null && hyperlink.DataContext != null)
            {
                string[] Splitted = hyperlink.Name.Split('_');
                if (Splitted.Length == 3 && Splitted[0] == "hyperlink" && Splitted[2].Length > 0)
                    OnRequestNavigate(hyperlink.DataContext, Splitted[2]);
            }
        }

        private void OnXpTableButtonChecked(object sender, RoutedEventArgs e)
        {
            XpTableButton = sender as ToggleButton;
        }

        private void OnContentTemplateLostFocus(object sender, RoutedEventArgs e)
        {
            CloseXpTable();
        }

        public void CloseXpTable()
        {
            if (XpTableButton != null && XpTableButton.IsChecked.HasValue && XpTableButton.IsChecked.Value == true)
                XpTableButton.IsChecked = false;
        }

        private void OnGridViewColumnHeaderClick(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader Header = e.OriginalSource as GridViewColumnHeader;
            if (Header == null)
                return;

            string ColumnName = Header.Content as string;
            FrameworkElement Ctrl = sender as FrameworkElement;
            NpcPreference Favor = Ctrl.DataContext as NpcPreference;

            if (ColumnName == "Name")
                Favor.SortByName();
            else if (ColumnName == "Value")
                Favor.SortByValue();
        }

        private void OnFavorButtonClicked(object sender, RoutedEventArgs e)
        {
            ToggleButton NewFavorButton = sender as ToggleButton;
            if (FavorButton != null && FavorButton != NewFavorButton && FavorButton.IsChecked.HasValue && FavorButton.IsChecked.Value)
                FavorButton.IsChecked = false;

            FavorButton = NewFavorButton;
        }

        private void OnFavorPopupOpened(object sender, EventArgs e)
        {
        }

        private ToggleButton XpTableButton;
        private ToggleButton FavorButton;
        #endregion

        #region Implementation of INotifyPropertyChanged
        /// <summary>
        ///     Implements the PropertyChanged event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        internal void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "Default parameter is mandatory with [CallerMemberName]")]
        internal void NotifyThisPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
