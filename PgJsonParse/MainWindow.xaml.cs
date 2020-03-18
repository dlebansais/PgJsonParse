using PgJsonObjects;
using Presentation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
#if CSHARP_XAML_FOR_HTML5
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
#else
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;
#endif

namespace PgJsonParse
{
    public partial class MainWindow : RootControl, INotifyPropertyChanged
    {
        #region Init
        public MainWindow()
            : base(RootControlMode.ResizedWithCaption)
        {
            InitializeComponent();
            DataContext = this;

            SetTitle();
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
            SubscribeToCommand("AddPowerCommand", OnAddPower);
            SubscribeToCommand("RemovePowerCommand", OnRemovePower);
            SubscribeToCommand("LoadBuildCommand", OnLoadBuild);
            SubscribeToCommand("SaveBuildCommand", OnSaveBuild);
            SubscribeToCommand("CopyBuildCommand", OnCopyBuild);
            SubscribeToCommand("CrunchCommand", OnCrunch);
            SubscribeToCommand("OpenProfileFolderCommand", OnOpenProfileFolder);
            SubscribeToCommand("CreateFromSkillCommand", OnCreateFromSkill);
            SubscribeToCommand("BackwardCommand", OnBackward);
            SubscribeToCommand("ForwardCommand", OnForward);
            SubscribeToCommand("GoToCommand", OnGoTo);
            SubscribeToCommand("LevelUpCommand", OnLevelUp);
            SubscribeToCommand("LevelDownCommand", OnLevelDown);
        }

        private void SetTitle()
        {
            string ParserVersion = PrologueWindow.PARSER_VERSION.ToString(System.Globalization.CultureInfo.InvariantCulture);
            Title = $"Project Gorgon - Json Parser v{ParserVersion}";

            if (LoadedVersion != null)
                Title += $" - File version: {LoadedVersion.Version}";
        }
        #endregion

        #region Properties
        public string ApplicationFolder { get; set; }
        public string VersionCacheFolder { get; set; }
        public string IconCacheFolder { get; set; }
        public string CurrentVersionCacheFolder { get; set; }
        public string IconFile { get; set; }
        public string FavorIconFile { get; set; }

        public GameVersionInfo LoadedVersion
        {
            get { return _LoadedVersion; }
            set
            {
                if (_LoadedVersion != value)
                {
                    _LoadedVersion = value;
                    SetTitle();
                    NotifyThisPropertyChanged();
                    NotifyPropertyChanged(nameof(Title));
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
            ApplicationFolder = Path.Combine(PresentationEnvironment.UserRootFolder, "PgJsonParse");
            FileTools.CreateDirectory(ApplicationFolder);

            VersionCacheFolder = Path.Combine(ApplicationFolder, "Versions");
            FileTools.CreateDirectory(VersionCacheFolder);

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
            { ItemSlot.Waist, "icon_20001" },
        };

        private void InitCache()
        {
            ImageConversion.UpdateWindowIconUsingFile(this, IconFile);
            StaticContainer Container = GetResourceByName("imgFavorIcon") as StaticContainer;
            Container.Item = ImageConversion.IconFileToImageSource(FavorIconFile);
        }
        #endregion

        #region Build Planer
        public const int DefaultMaxLevel = 80;

        private void InitBuildPlaner()
        {
            CombatSkillList = new ObservableCollection<PowerSkill>();
            MaxLevelFirstSkill = DefaultMaxLevel;
            MaxLevelSecondSkill = DefaultMaxLevel;
            SelectedFirstSkill = -1;
            SelectedSecondSkill = -1;
            SlotPlanerList = new List<SlotPlaner>();

            HeadSlotPlaner = CreateSlotPlaner(ItemSlot.Head);
            ChestSlotPlaner = CreateSlotPlaner(ItemSlot.Chest);
            LegSlotPlaner = CreateSlotPlaner(ItemSlot.Legs);
            HandSlotPlaner = CreateSlotPlaner(ItemSlot.Hands);
            FeetSlotPlaner = CreateSlotPlaner(ItemSlot.Feet);
            RingSlotPlaner = CreateSlotPlaner(ItemSlot.Ring);
            NeckSlotPlaner = CreateSlotPlaner(ItemSlot.Necklace);
            MainHandSlotPlaner = CreateSlotPlaner(ItemSlot.MainHand);
            OffHandSlotPlaner = CreateSlotPlaner(ItemSlot.OffHand);
            WaistSlotPlaner = CreateSlotPlaner(ItemSlot.Waist);

            WeightProfileList = new ObservableCollection<WeightProfile>();
            WeightProfileIndex = -1;
            IgnoreUnobtainable = true;
            IgnoreNoAttribute = true;
        }

        private SlotPlaner CreateSlotPlaner(ItemSlot slot)
        {
            SlotPlaner Result = new SlotPlaner(this, slot, IconFileTable, GetFirstSelectedSkill, GetSecondSelectedSkill);
            SlotPlanerList.Add(Result);

            return Result;
        }

        private int GetFirstSelectedSkill()
        {
            return SelectedFirstSkill;
        }

        private int GetSecondSelectedSkill()
        {
            return SelectedSecondSkill;
        }

        public FrameworkElement FirstSkill { get { return comboFirstSkill; } }
        public FrameworkElement SecondSkill { get { return comboSecondSkill; } }
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
        public SlotPlaner WaistSlotPlaner { get; private set; }
        public ObservableCollection<WeightProfile> WeightProfileList { get; private set; }
        public int WeightProfileIndex { get; set; }
        public bool IgnoreUnobtainable { get; set; }
        public bool IgnoreNoAttribute { get; set; }
        public bool IsSkillSelected { get { return SelectedFirstSkill >= 0 || SelectedSecondSkill >= 0; } }

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
            IList<IPgPower> PowerList = PowerDefinition.VerifiedObjectList as IList<IPgPower>;

            foreach (IPgPower PowerItem in PowerList)
            {
                if (PowerItem.IsUnavailable)
                    continue;

                IList<IPgPowerTier> TierEffectList = PowerItem.TierEffectList;
                if (TierEffectList.Count == 0)
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

                if (PowerItem.RawSkill == PowerSkill.Internal_None ||
                    PowerItem.RawSkill == PowerSkill.AnySkill ||
                    PowerItem.RawSkill == PowerSkill.ArmorPatching ||
                    PowerItem.RawSkill == PowerSkill.Endurance ||
                    PowerItem.RawSkill == PowerSkill.ShamanicInfusion)
                    continue;

                if (NewCombatSkillList.Contains(PowerItem.RawSkill))
                    continue;

                NewCombatSkillList.Add(PowerItem.RawSkill);
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
            IsBuildPlanerRefreshed = true;

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
                NotifyPropertyChanged(nameof(SelectedFirstSkill));
                NotifyPropertyChanged(nameof(SelectedSecondSkill));
                SelectAsFirst = PowerSkill.Internal_None;
                SelectAsSecond = PowerSkill.Internal_None;
            }

            IObjectDefinition PowerDefinition = ObjectList.Definitions[typeof(Power)];
            IList<IPgPower> PowerList = PowerDefinition.VerifiedObjectList as IList<IPgPower>;
            IObjectDefinition AttributeDefinition = ObjectList.Definitions[typeof(PgJsonObjects.Attribute)];
            Dictionary<string, IJsonKey> AttributeTable = AttributeDefinition.ObjectTable;
            IObjectDefinition SkillDefinition = ObjectList.Definitions[typeof(Skill)];
            IList<IPgSkill> SkillList = SkillDefinition.VerifiedObjectList as IList<IPgSkill>;

            PowerSkill FirstSkillParent = SelectAsFirst;
            PowerSkill SecondSkillParent = SelectAsSecond;

            foreach (IPgSkill Item in SkillList)
            {
                if (Item.CombatSkill == SelectAsFirst && Item.ParentSkill != null)
                    FirstSkillParent = Item.ParentSkill.CombatSkill;
                if (Item.CombatSkill == SelectAsSecond && Item.ParentSkill != null)
                    SecondSkillParent = Item.ParentSkill.CombatSkill;
            }

            foreach (SlotPlaner PlanerItem in SlotPlanerList)
                PlanerItem.RefreshCombatSkillList(PowerList, AttributeTable, SelectAsFirst, FirstSkillParent, MaxLevelFirstSkill, SelectAsSecond, SecondSkillParent, MaxLevelSecondSkill, DefaultMaxLevel);

            NotifyPropertyChanged(nameof(IsSkillSelected));
        }

        private void RefreshWeightProfileList()
        {
            WeightProfileList.Clear();

            IObjectDefinition AttributeDefinition = ObjectList.Definitions[typeof(PgJsonObjects.Attribute)];
            IList<IPgAttribute> AttributeList = AttributeDefinition.VerifiedObjectList as IList<IPgAttribute>;

            if (!FileTools.FileExists(DefaultProfileName))
            {
                string AllContent = "";
                foreach (IPgAttribute Attribute in AttributeList)
                {
                    string Line = Attribute.Label + " " + "=" + " " + "0" + InvariantCulture.NewLine;
                    AllContent += Line;
                }

                FileTools.CommitTextFile(DefaultProfileName, AllContent);
            }

            if (!FileTools.FileExists(BestArmorProfileName))
            {
                try
                {
                    string ArmorContent = "";
                    foreach (IPgAttribute Attribute in AttributeList)
                    {
                        if (Attribute.Key == "MAX_ARMOR")
                        {
                            string Line = Attribute.Label + " " + "=" + " " + "1.0" + InvariantCulture.NewLine;
                            ArmorContent += Line;
                        }
                    }

                    FileTools.CommitTextFile(BestArmorProfileName, ArmorContent);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }

            foreach (string FileName in FileTools.DirectoryFiles(ProfileFolder, "txt"))
            {
                if (FileName == DefaultProfileName)
                    continue;

                try
                {
                    WeightProfile NewProfile = null;

                    string Content = FileTools.LoadTextFile(FileName, FileMode.Open);
                    string[] Lines = Content.Split(new string[] { InvariantCulture.NewLine }, StringSplitOptions.None);

                    foreach (string Line in Lines)
                    {
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
                        if (!InvariantCulture.TryParseSingle(AttributeWeightString, out AttributeWeight))
                            continue;

                        if (AttributeWeight <= 0)
                            continue;

                        foreach (IPgAttribute Attribute in AttributeList)
                            if (Attribute.Label == AttributeLabel)
                            {
                                if (NewProfile == null)
                                    NewProfile = new WeightProfile(Path.GetFileNameWithoutExtension(FileName));

                                NewProfile.AddAttributeWeight(Attribute, AttributeWeight);
                                break;
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
            IList<IPgAttribute> AttributeList = AttributeDefinition.VerifiedObjectList as IList<IPgAttribute>;
            IObjectDefinition ItemDefinition = ObjectList.Definitions[typeof(Item)];
            IList<IPgItem> ItemList = ItemDefinition.VerifiedObjectList as IList<IPgItem>;

            foreach (SlotPlaner PlanerItem in SlotPlanerList)
                PlanerItem.RefreshGearList(ItemList, AttributeList, SelectedProfile, IgnoreUnobtainable, IgnoreNoAttribute);
        }

        private void AddPower(SlotPlaner senderSlot)
        {
            senderSlot.AddPower1();
            senderSlot.AddPower2();
            senderSlot.AddPower3();
            senderSlot.AddPower4();
            senderSlot.AddPower5();
        }

        private void RemovePower(SlotPlaner senderSlot)
        {
            senderSlot.RemovePower1();
            senderSlot.RemovePower2();
            senderSlot.RemovePower3();
            senderSlot.RemovePower4();
            senderSlot.RemovePower5();
        }

        private void LoadBuild()
        {
            string LoadedBuildFileName = BuildFileName;
            if (FileTools.OpenTextFile(ref LoadedBuildFileName, out string Content))
            {
                BuildFileName = LoadedBuildFileName;

                UpdateSelections(Content);
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => LoadBuild(Content)));
            }
        }

        private void UpdateSelections(string content)
        {
            string[] Lines = content.Split(new string[] { InvariantCulture.NewLine }, StringSplitOptions.None);

            foreach (string s in Lines)
            {
                string Line = s.Trim();

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
                            NotifyPropertyChanged(nameof(SelectedFirstSkill));
                            NotifyPropertyChanged(nameof(IsSkillSelected));
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
                            NotifyPropertyChanged(nameof(SelectedSecondSkill));
                            NotifyPropertyChanged(nameof(IsSkillSelected));
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
                    NotifyPropertyChanged(nameof(WeightProfileIndex));
                }
            }
        }

        private void LoadBuild(string content)
        {
            string[] Lines = content.Split(new string[] { InvariantCulture.NewLine }, StringSplitOptions.None);
            bool PowerSelected = false;

            foreach (string s in Lines)
            {
                string Line = s.Trim();

                if (Line.Length > 0 && Line[0] == ';')
                    continue;

                string[] Split = Line.Split('=');
                if (Split.Length != 2)
                    continue;

                string FieldName = Split[0];
                string FieldValue = Split[1];

                IObjectDefinition PowerDefinition = ObjectList.Definitions[typeof(Power)];
                Dictionary<string, IJsonKey> PowerTable = PowerDefinition.ObjectTable;
                IObjectDefinition ItemDefinition = ObjectList.Definitions[typeof(Item)];
                Dictionary<string, IJsonKey> ItemTable = ItemDefinition.ObjectTable;

                ItemSlot ParsedSlot;
                if (StringToEnumConversion<ItemSlot>.TryParse(FieldName, out ParsedSlot, null))
                {
                    if (PowerTable.ContainsKey(FieldValue))
                    {
                        IPgPower SlotPower = PowerTable[FieldValue] as IPgPower;

                        foreach (SlotPlaner Planer in SlotPlanerList)
                            if (Planer.Slot == ParsedSlot)
                            {
                                foreach (PlanerSlotPower PlanerSlot in Planer.AvailablePowerList1)
                                    if (PlanerSlot.Reference == SlotPower)
                                    {
                                        Planer.SelectPower1(PlanerSlot);
                                        PowerSelected = true;
                                        break;
                                    }

                                foreach (PlanerSlotPower PlanerSlot in Planer.AvailablePowerList2)
                                    if (PlanerSlot.Reference == SlotPower)
                                    {
                                        Planer.SelectPower2(PlanerSlot);
                                        PowerSelected = true;
                                        break;
                                    }

                                foreach (PlanerSlotPower PlanerSlot in Planer.AvailablePowerList3)
                                    if (PlanerSlot.Reference == SlotPower)
                                    {
                                        Planer.SelectPower3(PlanerSlot);
                                        PowerSelected = true;
                                        break;
                                    }

                                foreach (PlanerSlotPower PlanerSlot in Planer.AvailablePowerList4)
                                    if (PlanerSlot.Reference == SlotPower)
                                    {
                                        Planer.SelectPower4(PlanerSlot);
                                        PowerSelected = true;
                                        break;
                                    }

                                foreach (PlanerSlotPower PlanerSlot in Planer.AvailablePowerList5)
                                    if (PlanerSlot.Reference == SlotPower)
                                    {
                                        Planer.SelectPower5(PlanerSlot);
                                        PowerSelected = true;
                                        break;
                                    }

                                break;
                            }
                    }
                    else if (ItemTable.ContainsKey(FieldValue))
                    {
                        IPgItem SlotItem = ItemTable[FieldValue] as IPgItem;

                        foreach (SlotPlaner Planer in SlotPlanerList)
                            if (Planer.Slot == ParsedSlot)
                            {
                                Planer.SelectGear(SlotItem);
                                break;
                            }
                    }
                }
            }

            if (!PowerSelected)
                MessageBox.Show("You might have to restart the program for the gear to be properly selected");
        }

        private void SaveBuild()
        {
            using (StringWriter sw = new StringWriter())
            {
                SaveBuild(sw);

                string LoadedBuildFileName = BuildFileName;
                FileTools.SaveTextFile(ref LoadedBuildFileName, sw.ToString());
                BuildFileName = LoadedBuildFileName;
            }
        }

        private void SaveBuild(TextWriter tw)
        {
            if (SelectedFirstSkill >= 0)
            {
                string FirstSkillName = CombatSkillList[SelectedFirstSkill].ToString();
                string Line = "FirstSkill" + "=" + FirstSkillName + " (" + MaxLevelFirstSkill + ")" + InvariantCulture.NewLine;
                tw.Write(Line);
            }

            if (SelectedSecondSkill >= 0)
            {
                string SecondSkillName = CombatSkillList[SelectedSecondSkill].ToString();
                string Line = "SecondSkill" + "=" + SecondSkillName + " (" + MaxLevelSecondSkill + ")" + InvariantCulture.NewLine;
                tw.Write(Line);
            }

            if (WeightProfileIndex >= 0)
            {
                string WeightProfileName = WeightProfileList[WeightProfileIndex].Name;
                string Line = "GearProfile" + "=" + WeightProfileName + InvariantCulture.NewLine;
                tw.Write(Line);
            }

            foreach (SlotPlaner Planer in SlotPlanerList)
            {
                string SlotName = Planer.Slot.ToString();

                foreach (PlanerSlotPower PlanerSlot in Planer.SelectedPowerList1)
                {
                    IPgPower Reference = PlanerSlot.Reference;
                    string Key = Reference.Key;
                    string Line = SlotName + "=" + Key + InvariantCulture.NewLine;
                    tw.Write(Line);
                }

                foreach (PlanerSlotPower PlanerSlot in Planer.SelectedPowerList2)
                {
                    IPgPower Reference = PlanerSlot.Reference;
                    string Key = Reference.Key;
                    string Line = SlotName + "=" + Key + InvariantCulture.NewLine;
                    tw.Write(Line);
                }

                foreach (PlanerSlotPower PlanerSlot in Planer.SelectedPowerList3)
                {
                    IPgPower Reference = PlanerSlot.Reference;
                    string Key = Reference.Key;
                    string Line = SlotName + "=" + Key + InvariantCulture.NewLine;
                    tw.Write(Line);
                }

                foreach (PlanerSlotPower PlanerSlot in Planer.SelectedPowerList4)
                {
                    IPgPower Reference = PlanerSlot.Reference;
                    string Key = Reference.Key;
                    string Line = SlotName + "=" + Key + InvariantCulture.NewLine;
                    tw.Write(Line);
                }

                foreach (PlanerSlotPower PlanerSlot in Planer.SelectedPowerList5)
                {
                    IPgPower Reference = PlanerSlot.Reference;
                    string Key = Reference.Key;
                    string Line = SlotName + "=" + Key + InvariantCulture.NewLine;
                    tw.Write(Line);
                }

                if (Planer.SelectedGearIndex >= 0 && Planer.SelectedGearIndex < Planer.SortedGearList.Count)
                {
                    IPgItem PlanerItem = Planer.SortedGearList[Planer.SelectedGearIndex];
                    string Key = PlanerItem.Key;
                    string Line = SlotName + "=" + Key + InvariantCulture.NewLine;
                    tw.Write(Line);
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
                    IPgItem PlanerItem = Planer.SortedGearList[Planer.SelectedGearIndex];
                    GearName = " " + PlanerItem.Name;
                }

                if (Text.Length > 0)
                    Text += "\r\n";

                Text += SlotName + ":" + GearName + "\r\n";

                foreach (PlanerSlotPower PlanerSlot in Planer.SelectedPowerList1)
                {
                    IPgPower Reference = PlanerSlot.Reference;
                    string Key = Reference.Key;

                    Text += PlanerSlot.Name + "\r\n";
                }

                foreach (PlanerSlotPower PlanerSlot in Planer.SelectedPowerList2)
                {
                    IPgPower Reference = PlanerSlot.Reference;
                    string Key = Reference.Key;

                    Text += PlanerSlot.Name + "\r\n";
                }

                foreach (PlanerSlotPower PlanerSlot in Planer.SelectedPowerList3)
                {
                    IPgPower Reference = PlanerSlot.Reference;
                    string Key = Reference.Key;

                    Text += PlanerSlot.Name + "\r\n";
                }

                foreach (PlanerSlotPower PlanerSlot in Planer.SelectedPowerList4)
                {
                    IPgPower Reference = PlanerSlot.Reference;
                    string Key = Reference.Key;

                    Text += PlanerSlot.Name + "\r\n";
                }

                foreach (PlanerSlotPower PlanerSlot in Planer.SelectedPowerList5)
                {
                    IPgPower Reference = PlanerSlot.Reference;
                    string Key = Reference.Key;

                    Text += PlanerSlot.Name + "\r\n";
                }
            }

            Clipboard.SetText(Text);

            Confirmation.Show("The clipboard now contains a text description of this build.", "Build Planner", false, ConfirmationType.Info);
        }

        private void CreateFromSkill()
        {
            using (StringWriter sw = new StringWriter())
            {
                if (CreateFromSkill(sw))
                {
                    string ComboFileName = "";

                    if (SelectedFirstSkill >= 0 && SelectedFirstSkill < CombatSkillList.Count)
                    {
                        if (ComboFileName.Length > 0)
                            ComboFileName += "-";
                        ComboFileName += StringToEnumConversion<PowerSkill>.ToString(CombatSkillList[SelectedFirstSkill], null, PowerSkill.Internal_None);
                    }

                    if (SelectedSecondSkill >= 0 && SelectedSecondSkill < CombatSkillList.Count)
                    {
                        if (ComboFileName.Length > 0)
                            ComboFileName += "-";
                        ComboFileName += StringToEnumConversion<PowerSkill>.ToString(CombatSkillList[SelectedSecondSkill], null, PowerSkill.Internal_None);
                    }

                    ComboFileName = Path.Combine(ProfileFolder, ComboFileName + ".txt");

                    if (FileTools.SaveTextFile(ref ComboFileName, sw.ToString()))
                        RefreshWeightProfileList();
                }
            }
        }

        private bool CreateFromSkill(StringWriter sw)
        {
            List<IPgAttribute> AttributeList = new List<IPgAttribute>();

            if (SelectedFirstSkill >= 0 && SelectedFirstSkill < CombatSkillList.Count)
                FillMatchingAttributes(AttributeList, CombatSkillList[SelectedFirstSkill]);
            if (SelectedSecondSkill >= 0 && SelectedSecondSkill < CombatSkillList.Count)
                FillMatchingAttributes(AttributeList, CombatSkillList[SelectedSecondSkill]);

            if (AttributeList.Count == 0)
                return false;

            foreach (IPgAttribute Attribute in AttributeList)
            {
                string Line = Attribute.Label + " " + "=" + " " + "1.0" + InvariantCulture.NewLine;
                sw.WriteLine(Line);
            }

            return true;
        }

        private void FillMatchingAttributes(List<IPgAttribute> AttributeList, PowerSkill SelectedSkill)
        {
            IObjectDefinition AttributeDefinition = ObjectList.Definitions[typeof(PgJsonObjects.Attribute)];
            Dictionary<string, IJsonKey> AttributeTable = AttributeDefinition.ObjectTable;
            IObjectDefinition AbilityDefinition = ObjectList.Definitions[typeof(Ability)];
            Dictionary<string, IJsonKey> AbilityTable = AbilityDefinition.ObjectTable;

            string SkillName = StringToEnumConversion<PowerSkill>.ToString(SelectedSkill, null, PowerSkill.Internal_None).ToUpper();
            string CostMod = SkillName + "_COST_MOD";
            string BoostMod = "BOOST_SKILL_" + SkillName;
            string DmgMod = "MOD_SKILL_" + SkillName;

            foreach (KeyValuePair<string, IJsonKey> AttributeEntry in AttributeTable)
            {
                IPgAttribute Attribute = AttributeEntry.Value as IPgAttribute;
                if (Attribute.Label == null)
                    continue;

                bool IsMatchingAttribute = false;

                if (Attribute.IconIdList.Count > 0)
                {
                    foreach (KeyValuePair<string, IJsonKey> AbilityEntry in AbilityTable)
                    {
                        IPgAbility Ability = AbilityEntry.Value as IPgAbility;

                        if (Ability.RawSkill == SelectedSkill && Attribute.IconIdList.Contains(Ability.IconId))
                        {
                            IsMatchingAttribute = true;
                            break;
                        }
                    }
                }

                if (!IsMatchingAttribute && AttributeEntry.Key == CostMod)
                    IsMatchingAttribute = true;

                if (!IsMatchingAttribute && AttributeEntry.Key.StartsWith(BoostMod))
                    IsMatchingAttribute = true;

                if (!IsMatchingAttribute && AttributeEntry.Key.StartsWith(DmgMod))
                    IsMatchingAttribute = true;

                if (IsMatchingAttribute)
                    AttributeList.Add(Attribute);
            }
        }

        private void OnProfileSelected(object sender, SelectionChangedEventArgs e)
        {
            if (IsGearPlanerRefreshing) // Prevents recursion
                return;

            IsGearPlanerRefreshing = true;
            IsGearPlanerRefreshed = false;
            Dispatcher.BeginInvoke(new Action(() => ExecuteRefreshGearPlaner()));
        }

        private void OnIgnoreUnobtainableCheckChanged(object sender, RoutedEventArgs e)
        {
            if (IsGearPlanerRefreshing) // Prevents recursion
                return;

            IsGearPlanerRefreshing = true;
            IsGearPlanerRefreshed = false;
            Dispatcher.BeginInvoke(new Action(() => ExecuteRefreshGearPlaner()));
        }

        private void OnIgnoreNoAttributeCheckChanged(object sender, RoutedEventArgs e)
        {
            if (IsGearPlanerRefreshing) // Prevents recursion
                return;

            IsGearPlanerRefreshing = true;
            IsGearPlanerRefreshed = false;
            Dispatcher.BeginInvoke(new Action(() => ExecuteRefreshGearPlaner()));
        }

        private void ExecuteRefreshGearPlaner()
        {
            try
            {
                if (!IsGearPlanerRefreshed) // Prevents calling it multiple times
                    RefreshGearPlaner();
            }
            finally
            {
                IsGearPlanerRefreshing = false;
            }
        }

        private List<SlotPlaner> SlotPlanerList;

        public string BuildFileName
        {
            get { return _BuildFileName; }
            set
            {
                if (_BuildFileName != value)
                {
                    _BuildFileName = value;
                    NotifyThisPropertyChanged();
                }
            }
        }
        public string _BuildFileName;
        #endregion

        #region Gear Planer
        private void InitGearPlaner()
        {
            ProfileFolder = Path.Combine(ApplicationFolder, "Profiles");
            FileTools.CreateDirectory(ProfileFolder);

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
        public bool IncludePlayerTitle { get; set; }
        public bool IncludeLoreBook { get; set; }
        public bool IncludeAdvancement { get; set; }

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
            IncludePlayerTitle = true;
            IncludeLoreBook = true;
            IncludeAdvancement = true;
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

        private void PerformSearch(List<string> termList, SearchModes searchMode)
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
                    (EntryType == typeof(PlayerTitle) && IncludePlayerTitle) ||
                    (EntryType == typeof(AdvancementTable) && IncludeAdvancement) ||
                    (EntryType == typeof(LoreBook) && IncludeLoreBook))
                    PerformSearch(termList, Entry.Value, searchMode);
            }
        }

        private void PerformSearch(List<string> termList, IObjectDefinition definition, SearchModes searchMode)
        {
            string IndexFilePath = Path.Combine(CurrentVersionCacheFolder, definition.JsonFileName + "-index.txt");
            if (!FileTools.FileExists(IndexFilePath))
                return;

            string TextContent = FileTools.LoadTextFile(IndexFilePath, FileMode.OpenOrCreate);
            if (!string.IsNullOrEmpty(TextContent))
            {
                Dictionary<string, IJsonKey> ObjectTable = definition.ObjectTable;
                PerformSearch(termList, ObjectTable, TextContent, searchMode);
            }
        }

        private void PerformSearch(List<string> termList, Dictionary<string, IJsonKey> objectTable, string textContent, SearchModes searchMode)
        {
            List<IBackLinkable> Result = new List<IBackLinkable>();

            switch (searchMode)
            {
                case SearchModes.And:
                    for (int i = 0; i < termList.Count; i++)
                    {
                        List<IBackLinkable> SingleTermResult = new List<IBackLinkable>();
                        PerformSearch(termList[i], objectTable, textContent, SingleTermResult);

                        if (i == 0)
                        {
                            foreach (IBackLinkable o in SingleTermResult)
                                Result.Add(o);
                        }
                        else
                        {
                            List<IBackLinkable> ToRemove = new List<IBackLinkable>();
                            foreach (IBackLinkable o in Result)
                                if (!SingleTermResult.Contains(o))
                                    ToRemove.Add(o);

                            foreach (IBackLinkable o in ToRemove)
                                Result.Remove(o);
                        }
                    }
                    break;

                case SearchModes.Or:
                    foreach (string Term in termList)
                    {
                        List<IBackLinkable> SingleTermResult = new List<IBackLinkable>();
                        PerformSearch(Term, objectTable, textContent, SingleTermResult);

                        foreach (IBackLinkable o in SingleTermResult)
                            if (!Result.Contains(o))
                                Result.Add(o);
                    }
                    break;

                case SearchModes.Neither:
                    PerformSearch(termList[0], objectTable, textContent, Result);
                    break;
            }

            Result.Sort(GenericJsonObject.SortByName);

            foreach (object o in Result)
                SearchResult.Add(o);
        }

        private void PerformSearch(string term, Dictionary<string, IJsonKey> objectTable, string textContent, List<IBackLinkable> SingleTermResult)
        {
            int MatchIndex = -1;
            StringComparison Comparison = MustMatchCase ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase;

            while ((MatchIndex = textContent.IndexOf(term, MatchIndex + 1, Comparison)) >= 0)
            {
                int StartKeyIndex = textContent.IndexOf(JsonGenerator.ObjectKeyStart, MatchIndex + term.Length);
                if (StartKeyIndex < 0)
                    break;

                int EndKeyIndex = textContent.IndexOf(JsonGenerator.ObjectKeyEnd, MatchIndex + term.Length);
                if (EndKeyIndex < StartKeyIndex)
                    break;

                int KeyIndex = StartKeyIndex + 1;

                string Key = "";

                for (;;)
                {
                    if (KeyIndex >= textContent.Length || Key.Length > 30)
                        break;

                    char c = textContent[KeyIndex++];
                    if (c == JsonGenerator.ObjectKeyEnd)
                        break;

                    Key += c;
                }

                for (;;)
                {
                    if (KeyIndex >= textContent.Length)
                        break;

                    char c = textContent[KeyIndex];
                    if (c != '\r' && c != '\n')
                        break;

                    KeyIndex++;
                }

                if (objectTable.ContainsKey(Key))
                {
                    IBackLinkable AsBackLinkable = objectTable[Key] as IBackLinkable;
                    if (AsBackLinkable != null)
                        SingleTermResult.Add(AsBackLinkable);
                    else
                        AsBackLinkable = null;
                }

                MatchIndex = KeyIndex;

                if (MatchIndex + term.Length >= textContent.Length)
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

        private void OnRequestNavigate(object fromObject, string propertyName)
        {
            object ToObject = null;

            Type ObjectType = fromObject.GetType();

            if (propertyName== "hyperlink")
                ToObject = fromObject;
            else
            {
                PropertyInfo pi = ObjectType.GetProperty(propertyName);
                if (pi != null)
                    ToObject = pi.GetValue(fromObject);
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
            IList<IPgSkill> SkillList = SkillDefinition.VerifiedObjectList as IList<IPgSkill>;

            List<Skill> CombatSkillList = new List<Skill>();
            foreach (Skill SkillItem in SkillList)
                if (IsCombatSkill(SkillItem))
                    CombatSkillList.Add(SkillItem);

            Dispatcher.BeginInvoke(new Action(() => OnCrunchSkills(CombatSkillList, 0, 0)));
        }

        private void OnCrunchSkills(List<Skill> combatSkillList, int i, int j)
        {
            CrunchProgress = ((double)(i * combatSkillList.Count + j)) / ((double)(combatSkillList.Count * combatSkillList.Count));

            Skill PrimarySkill = combatSkillList[i];
            Skill SecondarySkill = combatSkillList[j];
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

            if (j + 1 < combatSkillList.Count)
                Dispatcher.BeginInvoke(new Action(() => OnCrunchSkills(combatSkillList, i, j + 1)));
            else if (i + 1 < combatSkillList.Count)
                Dispatcher.BeginInvoke(new Action(() => OnCrunchSkills(combatSkillList, i + 1, 0)));
            else
                IsCrunching = false;
        }

        private bool IsCombatSkill(Skill skillItem)
        {
            if (!skillItem.Combat)
                return false;

            if (skillItem.XpTable.InternalName != "TypicalCombatSkill" && skillItem.XpTable.InternalName != "TypicalCombatSkillExt")
                return false;

            IObjectDefinition AbilityDefinition = ObjectList.Definitions[typeof(Ability)];
            IList<IPgAbility> AbilityList = AbilityDefinition.VerifiedObjectList as IList<IPgAbility>;

            int AbilityCount = 0;
            foreach (Ability Item in AbilityList)
                if (skillItem.CombatSkill != PowerSkill.Internal_None && Item.Skill.CombatSkill == skillItem.CombatSkill)
                    AbilityCount++;

            if (AbilityCount < 6)
                return false;

            //Debug.WriteLine(skillItem.ToString() + ": " + skillItem.XpTable + ", " + AbilityCount);

            return true;
        }

        private bool IsValidCombination(Skill primarySkill, Skill secondarySkill)
        {
            if (primarySkill.CompatibleCombatSkillList.Contains(secondarySkill.CombatSkill) && secondarySkill.CompatibleCombatSkillList.Contains(primarySkill.CombatSkill))
                return true;

            //Debug.WriteLine("Incompatible skills: " + primarySkill.Key + " and " + secondarySkill.Key);

            return false;
        }

        private void Crunch(Skill primarySkill, Skill secondarySkill, out CrunchSelection selection)
        {
            List<Ability> PrimaryAbilityList = SelectAbilities(primarySkill);
            List<Ability> SecondaryAbilityList = SelectAbilities(secondarySkill);

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
                Crunch(primarySkill, secondarySkill, SelectedAbilityList, out SequenceSelection);

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

            selection = null;

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
                    Crunch(primarySkill, secondarySkill, SelectedAbilityList, out SequenceSelection);

                    if (selection == null || selection.BestDPS < SequenceSelection.BestDPS)
                        selection = SequenceSelection;

                    SecondarySequence.NextSeparate();
                }

                PrimarySequence.NextSeparate();
            }
        }

        private void Crunch(Skill primarySkill, Skill secondarySkill, List<Ability> selectedAbilityList, out CrunchSelection selection)
        {
            Dictionary<ItemSlot, List<Power>> Gear = SelectGear(primarySkill, secondarySkill);

            List<CrunchTarget> TargetList = new List<CrunchTarget>();
            TargetList.Add(Target1);

            selection = new CrunchSelection(primarySkill, secondarySkill, selectedAbilityList, Gear, TargetList);
            selection.Crunch();
        }

        private List<Ability> SelectAbilities(Skill selectedSkill)
        {
            List<Ability> LineAbilityList = new List<Ability>();
            int MaxLevel;
            if (CrunchSelection.MaxAttainableLevel.ContainsKey(selectedSkill.CombatSkill))
                MaxLevel = CrunchSelection.MaxAttainableLevel[selectedSkill.CombatSkill];
            else
                MaxLevel = int.MaxValue;

            Dictionary<Ability, List<Ability>> AbilitiesSortedByLineName = new Dictionary<Ability, List<Ability>>();
            IObjectDefinition AbilityDefinition = ObjectList.Definitions[typeof(Ability)];
            IList<IPgAbility> AbilityList = AbilityDefinition.VerifiedObjectList as IList<IPgAbility>;

            foreach (Ability AbilityItem in AbilityList)
            {
                if (!AbilityItem.IsAbilityValidForCrunch)
                    continue;

                if (AbilityItem.Level > MaxLevel)
                    continue;

                if ((AbilityItem.InternalName != null) && (AbilityItem.Skill.CombatSkill == selectedSkill.CombatSkill))
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

        private Ability AbilityUpgradeOf(Ability abilityItem)
        {
            if (abilityItem.UpgradeOf != null)
                return abilityItem.UpgradeOf as Ability;

            return AbilityPrevious(abilityItem);
        }

        private Ability AbilityPrerequisite(Ability abilityItem)
        {
            if (abilityItem.Prerequisite != null)
                return abilityItem.Prerequisite as Ability;

            return AbilityPrevious(abilityItem);
        }

        private Ability AbilityPrevious(Ability abilityItem)
        {
            IObjectDefinition AbilityDefinition = ObjectList.Definitions[typeof(Ability)];
            IList<IPgAbility> AbilityList = AbilityDefinition.VerifiedObjectList as IList<IPgAbility>;

            if (abilityItem.UpgradeOf != null)
                return abilityItem.UpgradeOf as Ability;

            if (abilityItem.Prerequisite != null)
                return abilityItem.Prerequisite as Ability;

            if (abilityItem.LineIndex <= 0)
                return null;

            foreach (Ability Item in AbilityList)
                if (Item.DigitStrippedName == abilityItem.DigitStrippedName)
                    if (Item.LineIndex + 1 == abilityItem.LineIndex)
                        return Item;

            foreach (Ability Item in AbilityList)
                if (Item.DigitStrippedName == abilityItem.DigitStrippedName)
                    if (Item.LineIndex == -1)
                        return Item;

            if (abilityItem.LineIndex == 1)
                return null;

            return null;
        }

        private Dictionary<ItemSlot, List<Power>> SelectGear(Skill primarySkill, Skill secondarySkill)
        {
            Dictionary<ItemSlot, List<Power>> Gear = new Dictionary<ItemSlot, List<Power>>();
            IObjectDefinition PowerDefinition = ObjectList.Definitions[typeof(Power)];
            IList<IPgPower> PowerList = PowerDefinition.VerifiedObjectList as IList<IPgPower>;

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
                ItemSlot.OffHand,
                ItemSlot.Waist,
            };

            foreach (ItemSlot Slot in SlotList)
            {
                List<Power> PrimaryPowerListForSlot = new List<Power>();
                List<Power> SecondaryPowerListForSlot = new List<Power>();
                PowerSkill FirstSkill = primarySkill.CombatSkill;
                PowerSkill SecondSkill = secondarySkill.CombatSkill;
                PowerSkill FirstSkillParent = primarySkill.ParentSkill != null ? primarySkill.ParentSkill.CombatSkill : FirstSkill;
                PowerSkill SecondSkillParent = secondarySkill.ParentSkill != null ? secondarySkill.ParentSkill.CombatSkill : SecondSkill;

                foreach (Power PowerItem in PowerList)
                    if (Power.IsValidForSlot(PowerItem, FirstSkill, FirstSkillParent, Slot))
                        PrimaryPowerListForSlot.Add(PowerItem);
                    else if (Power.IsValidForSlot(PowerItem, SecondSkill, SecondSkillParent, Slot))
                        SecondaryPowerListForSlot.Add(PowerItem);

                List<Power> PowerListForSlot = new List<Power>();
                SelectGearForSlot(PrimaryPowerListForSlot, SecondaryPowerListForSlot, PowerListForSlot);

                Gear.Add(Slot, PowerListForSlot);
            }

            return Gear;
        }

        private void SelectGearForSlot(List<Power> primaryPowerListForSlot, List<Power> secondaryPowerListForSlot, List<Power> powerListForSlot)
        {
            int i = 0;
            List<Power> List1 = new List<Power>(primaryPowerListForSlot);
            List<Power> List2 = new List<Power>(primaryPowerListForSlot);
            int ModCount1 = 0;
            int ModCount2 = 0;

            for (i = 0; i < 7; i++)
            {
                Power SelectedPower;
                if (!SelectMod(List1, List2, ref ModCount1, ref ModCount2, out SelectedPower))
                    break;

                powerListForSlot.Add(SelectedPower);
            }
        }

        private bool SelectMod(List<Power> list1, List<Power> list2, ref int modCount1, ref int modCount2, out Power selectedPower)
        {
            if (list1.Count == 0 && list2.Count == 0)
            {
                selectedPower = null;
                return false;
            }

            List<Power> SelectedList;

            if (modCount1 + 1 < modCount2 && list1.Count > 0)
            {
                SelectedList = list1;
                modCount1++;
            }

            else if (modCount2 + 1 < modCount1 && list2.Count > 0)
            {
                SelectedList = list2;
                modCount2++;
            }

            else if (list2.Count == 0)
            {
                SelectedList = list1;
                modCount1++;
            }

            else if (list1.Count == 0)
            {
                SelectedList = list2;
                modCount2++;
            }
            else
            {
                //TODO: select list randomly
                SelectedList = list1;
                modCount1++;
            }

            //TODO: select index randomly
            int Index = 0;

            selectedPower = SelectedList[Index];
            SelectedList.RemoveAt(Index);

            return true;
        }

        private CrunchTarget Target1 = new CrunchTarget() { MaxArmor = 100, MaxHealth = 1000, MaxRage = 100, Effective = new List<DamageType> { DamageType.Cold } };
        #endregion

        #region Events
        private bool IsBuildPlanerRefreshing;
        private bool IsBuildPlanerRefreshed;
        private bool IsGearPlanerRefreshing;
        private bool IsGearPlanerRefreshed;

        private void OnRefreshBuildPlaner(object sender, SelectionChangedEventArgs e)
        {
            if (IsBuildPlanerRefreshing) // Prevents recursion
                return;

            IsBuildPlanerRefreshing = true;
            IsBuildPlanerRefreshed = false;
            Dispatcher.BeginInvoke(new Action(() => ExecuteRefreshBuildPlaner()));
        }

        private void ExecuteRefreshBuildPlaner()
        {
            try
            {
                if (!IsBuildPlanerRefreshed) // Prevents calling it multiple times
                    RefreshBuildPlaner();
            }
            finally
            {
                IsBuildPlanerRefreshing = false;
            }
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
            WebClientTool.OpenFileExplorer(ProfileFolder);
        }

        private void OnCreateFromSkill(object sender, EventArgs e)
        {
            CreateFromSkill();
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

        private void OnGoTo(object sender, EventArgs e)
        {
            OnSearchTermsEntered();
        }

        private void OnLevelUp(object sender, EventArgs e)
        {
            OnLevelChange(+1);
        }

        private void OnLevelDown(object sender, EventArgs e)
        {
            OnLevelChange(-1);
        }

        private void OnLevelChange(int change)
        {
            if (CurrentSearchItem is IPgAdvancementTable CurrentAdvancementTable)
                CurrentAdvancementTable.OnLevelChange(change);
        }

        private void OnRequestNavigate(object sender, RoutedEventArgs e)
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

        private void OnTabControlLoaded(object sender, RoutedEventArgs e)
        {
            TabControl ctrl = sender as TabControl;
            Dispatcher.BeginInvoke(new Action(() => { ctrl.SelectedIndex = 0; }));
        }

#if CSHARP_XAML_FOR_HTML5
        private void OnSearchTermsEntered(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
                OnSearchTermsEntered();
        }
#else
        private void OnSearchTermsEntered(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                OnSearchTermsEntered();
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
        #endif

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
