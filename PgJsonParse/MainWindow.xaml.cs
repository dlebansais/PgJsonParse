using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using PgJsonObjects;
using System.Globalization;
using System.Collections;
using System.Windows.Navigation;
using System.Reflection;
using System.Windows.Documents;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls.Primitives;

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
            InitOperations();
            InitCache();
            InitLoad();
            InitBuildPlaner();
            InitGearPlaner();
            InitSearch();
            InitCruncher();
        }
        #endregion

        #region Properties
        public ObservableCollection<PgJsonObjects.Ability> AbilityList { get; private set; }
        private Dictionary<string, PgJsonObjects.Ability> AbilityTable;
        public ObservableCollection<PgJsonObjects.AbilitySource> AbilitySourceList { get; private set; }
        private Dictionary<string, PgJsonObjects.AbilitySource> AbilitySourceTable;
        public ObservableCollection<PgJsonObjects.AdvancementTable> AdvancementTableList { get; private set; }
        private Dictionary<string, PgJsonObjects.AdvancementTable> AdvancementTableTable;
        public ObservableCollection<PgJsonObjects.Quest> QuestList { get; private set; }
        private Dictionary<string, PgJsonObjects.Quest> QuestTable;
        public ObservableCollection<PgJsonObjects.DirectedGoal> DirectedGoalList { get; private set; }
        private Dictionary<string, PgJsonObjects.DirectedGoal> DirectedGoalTable;
        public ObservableCollection<PgJsonObjects.XpTable> XpTableList { get; private set; }
        private Dictionary<string, PgJsonObjects.XpTable> XpTableTable;
        public ObservableCollection<PgJsonObjects.Attribute> AttributeList { get; private set; }
        private Dictionary<string, PgJsonObjects.Attribute> AttributeTable;
        public ObservableCollection<PgJsonObjects.Item> ItemList { get; private set; }
        private Dictionary<string, PgJsonObjects.Item> ItemTable;
        public ObservableCollection<PgJsonObjects.Effect> EffectList { get; private set; }
        private Dictionary<string, PgJsonObjects.Effect> EffectTable;
        public ObservableCollection<PgJsonObjects.Recipe> RecipeList { get; private set; }
        private Dictionary<string, PgJsonObjects.Recipe> RecipeTable;
        public ObservableCollection<PgJsonObjects.RecipeSource> RecipeSourceList { get; private set; }
        private Dictionary<string, PgJsonObjects.RecipeSource> RecipeSourceTable;
        public ObservableCollection<PgJsonObjects.Skill> SkillList { get; private set; }
        private Dictionary<string, PgJsonObjects.Skill> SkillTable;
        public ObservableCollection<PgJsonObjects.Power> PowerList { get; private set; }
        private Dictionary<string, PgJsonObjects.Power> PowerTable;
        public static string ApplicationFolder { get; private set; }
        public static string VersionCacheFolder { get; private set; }
        public static string CurrentVersionCacheFolder { get; private set; }
        public static string IconFile { get; private set; }
        #endregion

        #region Startup Page
        public ProgramState ProgramState
        {
            get { return _ProgramState; }
            set
            {
                if (_ProgramState != value)
                {
                    _ProgramState = value;
                    NotifyThisPropertyChanged();
                    NotifyPropertyChanged("IsAtStartup");
                    NotifyPropertyChanged("IsLocating");
                    NotifyPropertyChanged("HasDownloadStarted");
                    NotifyPropertyChanged("IsDownloading");
                    NotifyPropertyChanged("IsParsing");
                    NotifyPropertyChanged("IsJsonLoading");
                    NotifyPropertyChanged("IsLoadingIcons");
                    NotifyPropertyChanged("IsJsonLoaded");
                }
            }
        }
        private ProgramState _ProgramState;

        public bool IsAtStartup { get { return ProgramState == ProgramState.StartupScreen; } }
        public bool IsLocating { get { return ProgramState == ProgramState.LocatingLastVersion; } }
        public bool HasDownloadStarted { get { return ProgramState >= ProgramState.LocatingLastVersion; } }
        public bool IsDownloading { get { return ProgramState == ProgramState.Downloading; } }
        public bool IsParsing { get { return ProgramState == ProgramState.Parsing; } }
        public bool IsJsonLoading { get { return ProgramState >= ProgramState.Parsing && ProgramState < ProgramState.Ready; } }
        public bool IsLoadingIcons { get { return ProgramState == ProgramState.LoadingIcons; } }
        public bool IsJsonLoaded { get { return ProgramState == ProgramState.Ready; } }

        public bool LocateLastestVersion
        {
            get { return _LocateLastestVersion; }
            set
            {
                if (_LocateLastestVersion != value)
                {
                    _LocateLastestVersion = value;
                    NotifyThisPropertyChanged();
                }
            }
        }
        public bool _LocateLastestVersion;

        public bool UseSpecificVersion
        {
            get { return _UseSpecificVersion; }
            set
            {
                if (_UseSpecificVersion != value)
                {
                    _UseSpecificVersion = value;
                    NotifyThisPropertyChanged();
                }
            }
        }
        public bool _UseSpecificVersion;

        public int SelectedVersionIndex
        {
            get { return _SelectedVersionIndex; }
            set
            {
                if (_SelectedVersionIndex != value)
                {
                    _SelectedVersionIndex = value;
                    NotifyThisPropertyChanged();
                }
            }
        }
        public int _SelectedVersionIndex;

        public bool IsVersionAvailableInCache
        {
            get { return _IsVersionAvailableInCache; }
            set
            {
                if (_IsVersionAvailableInCache != value)
                {
                    _IsVersionAvailableInCache = value;
                    NotifyThisPropertyChanged();
                }
            }
        }
        public bool _IsVersionAvailableInCache;

        public ObservableCollection<int> VersionList { get; private set; }

        private void InitStartupPage()
        {
            _ProgramState = ProgramState.StartupScreen;
            _IsVersionAvailableInCache = false;
            VersionList = new ObservableCollection<int>();

            ApplicationFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PgJsonParse");

            if (!Directory.Exists(ApplicationFolder))
                Directory.CreateDirectory(ApplicationFolder);

            VersionCacheFolder = Path.Combine(ApplicationFolder, "Versions");

            if (!Directory.Exists(VersionCacheFolder))
                Directory.CreateDirectory(VersionCacheFolder);

            IconFile = Path.Combine(ApplicationFolder, "mainicon.png");

            InitVersionList();

            if (IsVersionAvailableInCache)
            {
                _LocateLastestVersion = false;
                _UseSpecificVersion = true;
                _SelectedVersionIndex = VersionList.Count - 1;
            }
            else
            {
                _LocateLastestVersion = true;
                _UseSpecificVersion = false;
                _SelectedVersionIndex = -1;
            }
        }

        private void InitVersionList()
        {
            string[] SubFolders = Directory.GetDirectories(VersionCacheFolder);
            if (SubFolders != null)
            {
                foreach (string SubFolder in SubFolders)
                {
                    string FolderName = Path.GetFileName(SubFolder);

                    int Version;
                    if (int.TryParse(FolderName, out Version) && Version > 0)
                        VersionList.Add(Version);
                }
            }

            IsVersionAvailableInCache = (VersionList.Count > 0);
        }

        void ClearVersionList()
        {
            if (MessageBox.Show("You are going to clear the local, downloaded version of previous versions of the game files. Some of them may not be available anymore. Continue?", "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Warning) != MessageBoxResult.OK)
                return;

            VersionList.Clear();
            IsVersionAvailableInCache = false;
            LocateLastestVersion = true;
            UseSpecificVersion = false;

            string[] SubFolders = Directory.GetDirectories(VersionCacheFolder);
            if (SubFolders != null)
            {
                foreach (string SubFolder in SubFolders)
                    Directory.Delete(SubFolder, true);
            }
        }

        void StartApplication()
        {
            StartOperation();

            if (LocateLastestVersion)
                StartCacheThread();
            else
            {
                int Version = VersionList[SelectedVersionIndex];
                DownloadedVersion = Version;
                LoadedVersion = Version;
                CreateLoadThread();
            }
        }
        #endregion

        #region Operations
        private void InitOperations()
        {
            _IsOperationStarted = false;
            _IsOperationCanceled = false;
            _DownloadProgress = 0;
            _FileLoadProgress = 0;
            _IconLoadProgress = 0;
        }

        public bool IsOperationStarted
        {
            get { return _IsOperationStarted; }
            set
            {
                if (_IsOperationStarted != value)
                {
                    _IsOperationStarted = value;
                    NotifyThisPropertyChanged();
                }
            }
        }
        public bool _IsOperationStarted;

        public bool IsOperationCanceled
        {
            get { return _IsOperationCanceled; }
            set
            {
                if (_IsOperationCanceled != value)
                {
                    _IsOperationCanceled = value;
                    NotifyThisPropertyChanged();
                }
            }
        }
        public bool _IsOperationCanceled;

        public double DownloadProgress
        {
            get { return _DownloadProgress; }
            set
            {
                if (_DownloadProgress != value)
                {
                    _DownloadProgress = value;
                    NotifyThisPropertyChanged();
                }
            }
        }
        public double _DownloadProgress;

        public double FileLoadProgress
        {
            get { return _FileLoadProgress; }
            set
            {
                if (_FileLoadProgress != value)
                {
                    _FileLoadProgress = value;
                    NotifyThisPropertyChanged();
                }
            }
        }
        public double _FileLoadProgress;

        public double IconLoadProgress
        {
            get { return _IconLoadProgress; }
            set
            {
                if (_IconLoadProgress != value)
                {
                    _IconLoadProgress = value;
                    NotifyThisPropertyChanged();
                }
            }
        }
        public double _IconLoadProgress;

        private void StartOperation()
        {
            DownloadProgress = 0;
            FileLoadProgress = 0;
            IconLoadProgress = 0;
            IsOperationStarted = true;
            IsOperationCanceled = false;
        }

        private void StopOperation()
        {
            IsOperationStarted = false;
            IsOperationCanceled = false;
        }

        private void CancelOperation()
        {
            IsOperationStarted = false;
            IsOperationCanceled = true;
            ProgramState = ProgramState.StartupScreen;
        }
        #endregion

        #region Cache
        private static readonly Dictionary<Type, string> JsonFileTable = new Dictionary<Type,string>()
        {
            { typeof(PgJsonObjects.Ability), "abilities" },
            { typeof(PgJsonObjects.AbilitySource), "sources_abilities" },
            { typeof(PgJsonObjects.AdvancementTable), "advancementtables" },
            { typeof(PgJsonObjects.Attribute), "attributes" },
            { typeof(PgJsonObjects.DirectedGoal), "directedgoals" },
            { typeof(PgJsonObjects.Effect), "effects" },
            { typeof(PgJsonObjects.Item), "items" },
            { typeof(PgJsonObjects.Quest), "quests" },
            { typeof(PgJsonObjects.Recipe), "recipes" },
            { typeof(PgJsonObjects.RecipeSource), "sources_recipes" },
            { typeof(PgJsonObjects.Skill), "skills" },
            //{ typeof(PgJsonObjects.String), "strings" },
            { typeof(PgJsonObjects.Power), "tsysclientinfo" },
            { typeof(PgJsonObjects.XpTable), "xptables" },
        };
        private static readonly Dictionary<ItemSlot, string> IconFileTable = new Dictionary<ItemSlot, string>()
        {
            { ItemSlot.Head, "icon_16002" },
            { ItemSlot.Chest, "icon_12006" },
            { ItemSlot.Legs, "icon_11011" },
            { ItemSlot.Hands, "icon_13005" },
            { ItemSlot.Feet, "icon_10015" },
            { ItemSlot.Ring, "icon_17002" },
            { ItemSlot.Necklace, "icon_17001" },
            { ItemSlot.MainHand, "icon_15001" },
            { ItemSlot.OffHand, "icon_5382" },
        };

        public int DownloadedVersion
        {
            get { return _DownloadedVersion; }
            set
            {
                if (_DownloadedVersion != value)
                {
                    _DownloadedVersion = value;
                    NotifyThisPropertyChanged();
                }
            }
        }
        public int _DownloadedVersion;

        private void InitCache()
        {
            if (File.Exists(IconFile))
            {
                Bitmap bmp = new Bitmap(IconFile);
                Icon = ImageSourceForBitmap(bmp);
            }
        }

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);

        public ImageSource ImageSourceForBitmap(Bitmap bmp)
        {
            var handle = bmp.GetHbitmap();
            try
            {
                return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally { DeleteObject(handle); }
        }

        private void StartCacheThread()
        {
            Thread CacheThread = new Thread(new ThreadStart(ExecuteCacheThread));
            CacheThread.Start();
        }

        private void ExecuteCacheThread()
        {
            ProgramState = ProgramState.LocatingLastVersion;
            CheckVersionOnServer(false);
        }

        private void CheckVersionOnServer(bool TopVersionFound)
        {
            Stopwatch CheckWatch = new Stopwatch();
            CheckWatch.Start();

            int Version;
            if (VersionCheckedOnServer(out Version))
            {
                TimeSpan Remaining = TimeSpan.FromSeconds(4) - CheckWatch.Elapsed;
                if (Remaining.TotalMilliseconds > 0)
                    Thread.Sleep(Remaining);

                if (IsOperationCanceled)
                {
                    ProgramState = ProgramState.StartupScreen;
                    return;
                }

                DownloadedVersion = Version;
                CurrentVersionCacheFolder = Path.Combine(VersionCacheFolder, DownloadedVersion.ToString());
                UpdateCache();
            }
            else
            {
                MessageBox.Show("Unable to connect to the game server");
                CancelOperation();
            }
        }

        private bool VersionCheckedOnServer(out int Version)
        {
            Version = 0;

            bool Success = false;

            HttpWebRequest Request = HttpWebRequest.Create("http://client.projectgorgon.com/fileversion.txt") as HttpWebRequest;
            using (WebResponse Response = Request.GetResponse())
            {
                using (Stream ResponseStream = Response.GetResponseStream())
                {
                    using (StreamReader Reader = new StreamReader(ResponseStream, Encoding.ASCII))
                    {
                        string Content = Reader.ReadToEnd();
                        Success = int.TryParse(Content, out Version);
                    }
                }
            }

            return Success;
        }

        private void UpdateCache()
        {
            ProgramState = ProgramState.Downloading;

            int Index = 0;
            int Total = JsonFileTable.Count + IconFileTable.Count;
            foreach (KeyValuePair<Type, string> Entry in JsonFileTable)
            {
                UpdateJsonFileInCache(Entry.Key, Entry.Value, Index++, Total);

                if (IsOperationCanceled)
                {
                    ProgramState = ProgramState.StartupScreen;
                    return;
                }
            }

            foreach (KeyValuePair<ItemSlot, string> Entry in IconFileTable)
            {
                UpdateIconFileInCache(Entry.Key, Entry.Value, Index++, Total);

                if (IsOperationCanceled)
                {
                    ProgramState = ProgramState.StartupScreen;
                    return;
                }
            }

            LoadedVersion = DownloadedVersion;

            CreateLoadThread();
        }

        private void UpdateJsonFileInCache(Type TypeIndex, string FileName, int Index, int Total)
        {
            try
            {
                if (!UpdateTextCacheFile(DownloadedVersion, "data", FileName, "json"))
                {
                    MessageBox.Show("Failed to download JSON file " + FileName + " for version " + DownloadedVersion);
                    CancelOperation();
                }
                else
                {
                    DownloadProgress = ((double)(Index + 1)) / (double)Total;

                    string IndexFilePath = Path.Combine(CurrentVersionCacheFolder, JsonFileTable[TypeIndex] + "-index.txt");
                    if (File.Exists(IndexFilePath))
                        File.Delete(IndexFilePath);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                CancelOperation();
            }
        }

        private void UpdateIconFileInCache(ItemSlot TypeIndex, string FileName, int Index, int Total)
        {
            try
            {
                if (!UpdateBinaryCacheFile(DownloadedVersion, "icons", FileName, "png"))
                {
                    MessageBox.Show("Failed to download icon file " + FileName + " for version " + DownloadedVersion);
                    CancelOperation();
                }
                else
                    DownloadProgress = ((double)(Index + 1)) / (double)Total;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                CancelOperation();
            }
        }

        private bool UpdateTextCacheFile(int Version, string Location, string FileName, string Extension)
        {
            bool Success = false;

            HttpWebRequest Request = HttpWebRequest.Create("http://cdn.projectgorgon.com/v" + Version + "/" + Location + "/" + FileName + "." + Extension) as HttpWebRequest;
            using (WebResponse Response = Request.GetResponse())
            {
                using (Stream ResponseStream = Response.GetResponseStream())
                {
                    using (StreamReader Reader = new StreamReader(ResponseStream, Encoding.ASCII))
                    {
                        string Result = Reader.ReadToEnd();
                        if (Result != null && Result.Length >= 256)
                        {
                            Success = true;

                            string FolderPath = Path.Combine(VersionCacheFolder, Version.ToString());
                            if (!Directory.Exists(FolderPath))
                                Directory.CreateDirectory(FolderPath);

                            string FilePath = Path.Combine(FolderPath, FileName + "." + Extension);

                            using (FileStream fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                            {
                                using (StreamWriter sw = new StreamWriter(fs, Encoding.ASCII))
                                {
                                    sw.Write(Result);
                                }
                            }
                        }
                    }
                }
            }

            return Success;
        }

        private bool UpdateBinaryCacheFile(int Version, string Location, string FileName, string Extension)
        {
            bool Success = false;

            HttpWebRequest Request = HttpWebRequest.Create("http://cdn.projectgorgon.com/v" + Version + "/" + Location + "/" + FileName + "." + Extension) as HttpWebRequest;
            using (WebResponse Response = Request.GetResponse())
            {
                using (Stream ResponseStream = Response.GetResponseStream())
                {
                    using (BinaryReader Reader = new BinaryReader(ResponseStream))
                    {
                        string FolderPath = Path.Combine(VersionCacheFolder, Version.ToString());
                        if (!Directory.Exists(FolderPath))
                            Directory.CreateDirectory(FolderPath);

                        string FilePath = Path.Combine(FolderPath, FileName + "." + Extension);

                        using (FileStream fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            ResponseStream.CopyTo(fs);
                            Success = true;
                        }
                    }
                }
            }

            return Success;
        }
        #endregion

        #region Loading
        private void InitLoad()
        {
            AbilityList = new ObservableCollection<PgJsonObjects.Ability>();
            AbilityTable = new Dictionary<string, PgJsonObjects.Ability>();
            AbilitySourceList = new ObservableCollection<PgJsonObjects.AbilitySource>();
            AbilitySourceTable = new Dictionary<string, PgJsonObjects.AbilitySource>();
            AdvancementTableList = new ObservableCollection<PgJsonObjects.AdvancementTable>();
            AdvancementTableTable = new Dictionary<string, PgJsonObjects.AdvancementTable>();
            DirectedGoalList = new ObservableCollection<PgJsonObjects.DirectedGoal>();
            DirectedGoalTable = new Dictionary<string, PgJsonObjects.DirectedGoal>();
            QuestList = new ObservableCollection<PgJsonObjects.Quest>();
            QuestTable = new Dictionary<string, PgJsonObjects.Quest>();
            XpTableList = new ObservableCollection<PgJsonObjects.XpTable>();
            XpTableTable = new Dictionary<string, PgJsonObjects.XpTable>();
            EffectList = new ObservableCollection<PgJsonObjects.Effect>();
            EffectTable = new Dictionary<string, PgJsonObjects.Effect>();
            AttributeList = new ObservableCollection<PgJsonObjects.Attribute>();
            AttributeTable = new Dictionary<string, PgJsonObjects.Attribute>();
            RecipeList = new ObservableCollection<PgJsonObjects.Recipe>();
            RecipeTable = new Dictionary<string, PgJsonObjects.Recipe>();
            RecipeSourceList = new ObservableCollection<PgJsonObjects.RecipeSource>();
            RecipeSourceTable = new Dictionary<string, PgJsonObjects.RecipeSource>();
            ItemList = new ObservableCollection<PgJsonObjects.Item>();
            ItemTable = new Dictionary<string, PgJsonObjects.Item>();
            PowerList = new ObservableCollection<PgJsonObjects.Power>();
            PowerTable = new Dictionary<string, PgJsonObjects.Power>();
            SkillList = new ObservableCollection<PgJsonObjects.Skill>();
            SkillTable = new Dictionary<string, PgJsonObjects.Skill>();
        }

        public int LoadedVersion
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
        public int _LoadedVersion;

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

        private void CreateLoadThread()
        {
            Thread LoadThread = new Thread(new ThreadStart(ExecuteLoadThread));
            LoadThread.Start();
        }

        private void ExecuteLoadThread()
        {
            ProgramState = ProgramState.Parsing;
            DownloadProgress = 1.0;

            CurrentVersionCacheFolder = Path.Combine(VersionCacheFolder, LoadedVersion.ToString());

            ParseErrorInfo ErrorInfo = new ParseErrorInfo();

            int Index = 0;
            int Total = JsonFileTable.Count;

            foreach (KeyValuePair<Type, string> Entry in JsonFileTable)
            {
                LoadNextFile(Entry.Key, Entry.Value, ErrorInfo, Index++, Total);

                if (IsOperationCanceled)
                {
                    ProgramState = ProgramState.StartupScreen;
                    return;
                }
            }

            ConnectTables(ErrorInfo);
        }

        private void LoadNextFile(Type TypeIndex, string FileName, ParseErrorInfo ErrorInfo, int Index, int Total)
        {
            try
            {
                string FilePath = Path.Combine(CurrentVersionCacheFolder, FileName + ".json");

                if (TypeIndex == typeof(PgJsonObjects.Ability))
                {
                    Parser<PgJsonObjects.Ability> AbilityParser = new Parser<PgJsonObjects.Ability>();
                    AbilityParser.LoadRaw(FilePath, AbilityList, ErrorInfo);

                    AbilityTable.Clear();
                    foreach (Ability Item in AbilityList)
                        AbilityTable.Add(Item.Key, Item);
                }

                else if (TypeIndex == typeof(PgJsonObjects.AbilitySource))
                {
                    Parser<PgJsonObjects.AbilitySource> AbilitySourceParser = new Parser<PgJsonObjects.AbilitySource>();
                    AbilitySourceParser.LoadRaw(FilePath, AbilitySourceList, ErrorInfo);

                    AbilitySourceTable.Clear();
                    foreach (AbilitySource Item in AbilitySourceList)
                        AbilitySourceTable.Add(Item.Key, Item);
                }

                else if (TypeIndex == typeof(PgJsonObjects.AdvancementTable))
                {
                    Parser<PgJsonObjects.AdvancementTable> AdvancementTableParser = new Parser<PgJsonObjects.AdvancementTable>();
                    AdvancementTableParser.LoadRaw(FilePath, AdvancementTableList, ErrorInfo);

                    AdvancementTableTable.Clear();
                    foreach (AdvancementTable Item in AdvancementTableList)
                        AdvancementTableTable.Add(Item.Key, Item);
                }

                else if (TypeIndex == typeof(PgJsonObjects.Attribute))
                {
                    Parser<PgJsonObjects.Attribute> AttributeParser = new Parser<PgJsonObjects.Attribute>();
                    AttributeParser.LoadRaw(FilePath, AttributeList, ErrorInfo);

                    AttributeTable.Clear();
                    foreach (PgJsonObjects.Attribute Item in AttributeList)
                        AttributeTable.Add(Item.Key, Item);
                }

                else if (TypeIndex == typeof(PgJsonObjects.DirectedGoal))
                {
                    Parser<PgJsonObjects.DirectedGoal> DirectedGoalParser = new Parser<PgJsonObjects.DirectedGoal>();
                    DirectedGoalParser.LoadRaw(FilePath, DirectedGoalList, ErrorInfo);

                    DirectedGoalTable.Clear();
                    foreach (DirectedGoal Item in DirectedGoalList)
                        DirectedGoalTable.Add(Item.Key, Item);
                }

                else if (TypeIndex == typeof(PgJsonObjects.Effect))
                {
                    Parser<PgJsonObjects.Effect> EffectParser = new Parser<PgJsonObjects.Effect>();
                    EffectParser.LoadRaw(FilePath, EffectList, ErrorInfo);

                    EffectTable.Clear();
                    foreach (PgJsonObjects.Effect Item in EffectList)
                        EffectTable.Add(Item.Key, Item);
                }

                else if (TypeIndex == typeof(PgJsonObjects.Item))
                {
                    Parser<PgJsonObjects.Item> ItemParser = new Parser<PgJsonObjects.Item>();
                    ItemParser.LoadRaw(FilePath, ItemList, ErrorInfo);

                    ItemTable.Clear();
                    foreach (PgJsonObjects.Item Item in ItemList)
                        ItemTable.Add(Item.Key, Item);
                }

                else if (TypeIndex == typeof(PgJsonObjects.Quest))
                {
                    Parser<PgJsonObjects.Quest> QuestParser = new Parser<PgJsonObjects.Quest>();
                    QuestParser.LoadRaw(FilePath, QuestList, ErrorInfo);

                    QuestTable.Clear();
                    foreach (PgJsonObjects.Quest Item in QuestList)
                        QuestTable.Add(Item.Key, Item);
                }

                else if (TypeIndex == typeof(PgJsonObjects.Recipe))
                {
                    Parser<PgJsonObjects.Recipe> RecipeParser = new Parser<PgJsonObjects.Recipe>();
                    RecipeParser.LoadRaw(FilePath, RecipeList, ErrorInfo);

                    RecipeTable.Clear();
                    foreach (Recipe Item in RecipeList)
                        RecipeTable.Add(Item.Key, Item);
                }

                else if (TypeIndex == typeof(PgJsonObjects.RecipeSource))
                {
                    Parser<PgJsonObjects.RecipeSource> RecipeSourceParser = new Parser<PgJsonObjects.RecipeSource>();
                    RecipeSourceParser.LoadRaw(FilePath, RecipeSourceList, ErrorInfo);

                    RecipeSourceTable.Clear();
                    foreach (RecipeSource Item in RecipeSourceList)
                        RecipeSourceTable.Add(Item.Key, Item);
                }

                else if (TypeIndex == typeof(PgJsonObjects.Skill))
                {
                    Parser<PgJsonObjects.Skill> SkillParser = new Parser<PgJsonObjects.Skill>();
                    SkillParser.LoadRaw(FilePath, SkillList, ErrorInfo);

                    SkillTable.Clear();
                    foreach (PgJsonObjects.Skill Item in SkillList)
                        SkillTable.Add(Item.Key, Item);
                }

                else if (TypeIndex == typeof(PgJsonObjects.Power))
                {
                    Parser<PgJsonObjects.Power> PowerParser = new Parser<PgJsonObjects.Power>();
                    PowerParser.LoadRaw(FilePath, PowerList, ErrorInfo);

                    PowerTable.Clear();
                    foreach (PgJsonObjects.Power Item in PowerList)
                        PowerTable.Add(Item.Key, Item);
                }

                else if (TypeIndex == typeof(PgJsonObjects.XpTable))
                {
                    Parser<PgJsonObjects.XpTable> XpTableParser = new Parser<PgJsonObjects.XpTable>();
                    XpTableParser.LoadRaw(FilePath, XpTableList, ErrorInfo);

                    XpTableTable.Clear();
                    foreach (XpTable Item in XpTableList)
                        XpTableTable.Add(Item.Key, Item);
                }

                FileLoadProgress = ((double)(Index + 1)) / (double)Total;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                CancelOperation();
            }
        }

        private void ConnectTables(ParseErrorInfo ErrorInfo)
        {
            ProgramState = ProgramState.ConnectingTables;

            foreach (PgJsonObjects.AdvancementTable Item in AdvancementTableList)
                Item.Connect(ErrorInfo, null, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable);

            foreach (PgJsonObjects.Ability Item in AbilityList)
                Item.Connect(ErrorInfo, null, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable);

            foreach (PgJsonObjects.AbilitySource Item in AbilitySourceList)
                Item.Connect(ErrorInfo, null, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable);

            foreach (PgJsonObjects.Attribute Item in AttributeList)
                Item.Connect(ErrorInfo, null, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable);

            foreach (PgJsonObjects.DirectedGoal Item in DirectedGoalList)
                Item.Connect(ErrorInfo, null, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable);

            foreach (PgJsonObjects.Effect Item in EffectList)
                Item.Connect(ErrorInfo, null, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable);

            foreach (PgJsonObjects.Item Item in ItemList)
                Item.Connect(ErrorInfo, null, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable);

            foreach (PgJsonObjects.Power Item in PowerList)
                Item.Connect(ErrorInfo, null, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable);

            foreach (PgJsonObjects.Quest Item in QuestList)
                Item.Connect(ErrorInfo, null, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable);

            foreach (PgJsonObjects.Recipe Item in RecipeList)
                Item.Connect(ErrorInfo, null, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable);

            foreach (PgJsonObjects.RecipeSource Item in RecipeSourceList)
                Item.Connect(ErrorInfo, null, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable);

            foreach (PgJsonObjects.Skill Item in SkillList)
                Item.Connect(ErrorInfo, null, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable);

            foreach (PgJsonObjects.XpTable Item in XpTableList)
                Item.Connect(ErrorInfo, null, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable);

            bool Continue;
            do
            {
                Continue = false;
                foreach (PgJsonObjects.Recipe Item in RecipeList)
                    Item.MeasurePerfectCottonRatio(ref Continue);
            }
            while (Continue);

            foreach (PgJsonObjects.AdvancementTable Item in AdvancementTableList)
                Item.SetIndirectProperties(AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable, ErrorInfo);

            foreach (PgJsonObjects.Ability Item in AbilityList)
                Item.SetIndirectProperties(AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable, ErrorInfo);

            foreach (PgJsonObjects.AbilitySource Item in AbilitySourceList)
                Item.SetIndirectProperties(AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable, ErrorInfo);

            foreach (PgJsonObjects.Attribute Item in AttributeList)
                Item.SetIndirectProperties(AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable, ErrorInfo);

            foreach (PgJsonObjects.DirectedGoal Item in DirectedGoalList)
                Item.SetIndirectProperties(AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable, ErrorInfo);

            foreach (PgJsonObjects.Effect Item in EffectList)
                Item.SetIndirectProperties(AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable, ErrorInfo);

            foreach (PgJsonObjects.Item Item in ItemList)
                Item.SetIndirectProperties(AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable, ErrorInfo);

            foreach (PgJsonObjects.Power Item in PowerList)
                Item.SetIndirectProperties(AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable, ErrorInfo);

            foreach (PgJsonObjects.Quest Item in QuestList)
                Item.SetIndirectProperties(AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable, ErrorInfo);

            foreach (PgJsonObjects.Recipe Item in RecipeList)
                Item.SetIndirectProperties(AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable, ErrorInfo);

            foreach (PgJsonObjects.RecipeSource Item in RecipeSourceList)
                Item.SetIndirectProperties(AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable, ErrorInfo);

            foreach (PgJsonObjects.Skill Item in SkillList)
                Item.SetIndirectProperties(AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable, ErrorInfo);

            foreach (PgJsonObjects.XpTable Item in XpTableList)
                Item.SetIndirectProperties(AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable, ErrorInfo);



            foreach (PgJsonObjects.AdvancementTable Item in AdvancementTableList)
                Item.SortLinkBack();

            foreach (PgJsonObjects.Ability Item in AbilityList)
                Item.SortLinkBack();

            foreach (PgJsonObjects.AbilitySource Item in AbilitySourceList)
                Item.SortLinkBack();

            foreach (PgJsonObjects.Attribute Item in AttributeList)
                Item.SortLinkBack();

            foreach (PgJsonObjects.DirectedGoal Item in DirectedGoalList)
                Item.SortLinkBack();

            foreach (PgJsonObjects.Effect Item in EffectList)
                Item.SortLinkBack();

            foreach (PgJsonObjects.Item Item in ItemList)
                Item.SortLinkBack();

            foreach (PgJsonObjects.Power Item in PowerList)
                Item.SortLinkBack();

            foreach (PgJsonObjects.Quest Item in QuestList)
                Item.SortLinkBack();

            foreach (PgJsonObjects.Recipe Item in RecipeList)
                Item.SortLinkBack();

            foreach (PgJsonObjects.RecipeSource Item in RecipeSourceList)
                Item.SortLinkBack();

            foreach (PgJsonObjects.Skill Item in SkillList)
                Item.SortLinkBack();

            foreach (PgJsonObjects.XpTable Item in XpTableList)
                Item.SortLinkBack();

            CreateIndexes(ErrorInfo);
        }

        private void CreateIndexes(ParseErrorInfo ErrorInfo)
        {
            ProgramState = ProgramState.CreatingIndex;

            foreach (KeyValuePair<Type, string> Entry in JsonFileTable)
            {
                CreateNextIndex(Entry.Key, Entry.Value, ErrorInfo);

                if (IsOperationCanceled)
                {
                    ProgramState = ProgramState.StartupScreen;
                    return;
                }
            }

            CreateMushroomIndex(ErrorInfo);
            LoadIcons(ErrorInfo);
        }

        private void CreateNextIndex(Type TypeIndex, string FileName, ParseErrorInfo ErrorInfo)
        { 
            try
            {
                string IndexFilePath = Path.Combine(CurrentVersionCacheFolder, FileName + "-index.txt");

                if (TypeIndex == typeof(PgJsonObjects.Ability))
                {
                    Parser<PgJsonObjects.Ability> AbilityParser = new Parser<PgJsonObjects.Ability>();
                    AbilityParser.CreateIndex(IndexFilePath, AbilityTable);
                }

                else if (TypeIndex == typeof(PgJsonObjects.AdvancementTable))
                {
                    Parser<PgJsonObjects.AdvancementTable> AdvancementTableParser = new Parser<PgJsonObjects.AdvancementTable>();
                    AdvancementTableParser.CreateIndex(IndexFilePath, AdvancementTableTable);
                }

                else if (TypeIndex == typeof(PgJsonObjects.Attribute))
                {
                    Parser<PgJsonObjects.Attribute> AttributeParser = new Parser<PgJsonObjects.Attribute>();
                    AttributeParser.CreateIndex(IndexFilePath, AttributeTable);
                }

                else if (TypeIndex == typeof(PgJsonObjects.DirectedGoal))
                {
                    Parser<PgJsonObjects.DirectedGoal> DirectedGoalParser = new Parser<PgJsonObjects.DirectedGoal>();
                    DirectedGoalParser.CreateIndex(IndexFilePath, DirectedGoalTable);
                }

                else if (TypeIndex == typeof(PgJsonObjects.Effect))
                {
                    Parser<PgJsonObjects.Effect> EffectParser = new Parser<PgJsonObjects.Effect>();
                    EffectParser.CreateIndex(IndexFilePath, EffectTable);
                }

                else if (TypeIndex == typeof(PgJsonObjects.Item))
                {
                    Parser<PgJsonObjects.Item> ItemParser = new Parser<PgJsonObjects.Item>();
                    ItemParser.CreateIndex(IndexFilePath, ItemTable);
                }

                else if (TypeIndex == typeof(PgJsonObjects.Quest))
                {
                    Parser<PgJsonObjects.Quest> QuestParser = new Parser<PgJsonObjects.Quest>();
                    QuestParser.CreateIndex(IndexFilePath, QuestTable);
                }

                else if (TypeIndex == typeof(PgJsonObjects.Recipe))
                {
                    Parser<PgJsonObjects.Recipe> RecipeParser = new Parser<PgJsonObjects.Recipe>();
                    RecipeParser.CreateIndex(IndexFilePath, RecipeTable);
                }

                else if (TypeIndex == typeof(PgJsonObjects.Skill))
                {
                    Parser<PgJsonObjects.Skill> SkillParser = new Parser<PgJsonObjects.Skill>();
                    SkillParser.CreateIndex(IndexFilePath, SkillTable);
                }

                else if (TypeIndex == typeof(PgJsonObjects.Power))
                {
                    Parser<PgJsonObjects.Power> PowerParser = new Parser<PgJsonObjects.Power>();
                    PowerParser.CreateIndex(IndexFilePath, PowerTable);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                CancelOperation();
            }
        }

        private void CreateMushroomIndex(ParseErrorInfo ErrorInfo)
        {
            string MushroomNameFile = Path.Combine(ApplicationFolder, "Mushrooms.txt");

            List<string> MushroomNameList = new List<string>();
            foreach (KeyValuePair<string, Item> Entry in ItemTable)
            {
                Item Item = Entry.Value;
                if (Item.KeywordTable.ContainsKey(ItemKeyword.RawMushroom))
                    MushroomNameList.Add(Item.Name);
            }

            try
            {
                using (FileStream fs = new FileStream(MushroomNameFile, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    using (StreamWriter sw = new StreamWriter(fs, Encoding.ASCII))
                    {
                        foreach (string MushroomName in MushroomNameList)
                            sw.WriteLine(MushroomName);
                    }
                }
            }
            catch
            {
            }
        }

        private void LoadIcons(ParseErrorInfo ErrorInfo)
        {
            List<int> MissingIconList = new List<int>();
            foreach (int IconId in ErrorInfo.IconList)
            {
                string FilePath = Path.Combine(CurrentVersionCacheFolder, "icon_" + IconId + ".png");
                if (!File.Exists(FilePath))
                    MissingIconList.Add(IconId);
            }

            if (MissingIconList.Count > 0)
            {
                if (MessageBox.Show("There are " + MissingIconList.Count + " icon(s) not downloaded yet, would you like to get them now?", "Loading", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                    MissingIconList.Clear();
            }

            ProgramState = ProgramState.LoadingIcons;
            LoadNextIcon(MissingIconList, 0, MissingIconList.Count, ErrorInfo);
        }

        private void LoadNextIcon(List<int> MissingIconList, int LoadedCount, int MaxCount, ParseErrorInfo ErrorInfo)
        {
            if (MissingIconList.Count > 0)
                Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new LoadNextIconHandler(OnLoadNextIcon), MissingIconList, LoadedCount, MaxCount, ErrorInfo);
            else
                Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new CompleteLoadingHandler(OnCompleteLoading), ErrorInfo);
        }

        private delegate void LoadNextIconHandler(List<int> MissingIconList, int LoadedCount, int MaxCount, ParseErrorInfo ErrorInfo);
        private void OnLoadNextIcon(List<int> MissingIconList, int LoadedCount, int MaxCount, ParseErrorInfo ErrorInfo)
        {
            if (IsOperationCanceled)
            {
                ProgramState = ProgramState.StartupScreen;
                return;
            }

            IconLoadProgress = ((double)LoadedCount) / (double)MaxCount;

            List<int> LoadedIconList = new List<int>();

            for (int i = 0; i < 10 && MissingIconList.Count > 0; i++)
            {
                int IconId = MissingIconList[0];

                LoadedIconList.Add(IconId);
                MissingIconList.RemoveAt(0);
            }

            foreach (int IconId in LoadedIconList)
            {
                LoadedCount++;

                string FilePath = Path.Combine(CurrentVersionCacheFolder, "icon_" + IconId + ".png");
                if (!UpdateBinaryCacheFile(LoadedVersion, "icons", "icon_" + IconId, "png"))
                    break;

                if (IconId == 5624)
                {
                    File.Copy(FilePath, IconFile, true);
                }
            }

            LoadNextIcon(MissingIconList, LoadedCount, MaxCount, ErrorInfo);
        }

        private delegate void CompleteLoadingHandler(ParseErrorInfo ErrorInfo);
        private void OnCompleteLoading(ParseErrorInfo ErrorInfo)
        {
            string Warnings = ErrorInfo.GetWarnings();
            if (Warnings.Length > 0)
            {
                Debug.Print(Warnings);
                WarningText = Warnings;
            }
            else
                WarningText = "Files loaded and parsed, no warnings.";

            StopOperation();
            ProgramState = ProgramState.Ready;
            RefreshCombatSkillList();
            RefreshWeightProfileList();
            RefreshGearPlaner();
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
                        case ItemSlot.Special1:
                        case ItemSlot.Special2:
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

            foreach (SlotPlaner PlanerItem in SlotPlanerList)
                PlanerItem.RefreshCombatSkillList(PowerList, AttributeTable, SelectAsFirst, MaxLevelFirstSkill, SelectAsSecond, MaxLevelSecondSkill, DefaultMaxLevel);
        }

        private void RefreshWeightProfileList()
        {
            WeightProfileList.Clear();

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
                    ItemSlot ParsedSlot;
                    if (StringToEnumConversion<ItemSlot>.TryParse(FieldName, out ParsedSlot, null))
                    {
                        if (PowerTable.ContainsKey(FieldValue))
                        {
                            Power SlotPower = PowerTable[FieldValue];

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
                            Item SlotItem = ItemTable[FieldValue];

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
        public bool IncludeEffect { get; set; }
        public bool IncludeItem { get; set; }
        public bool IncludeQuest { get; set; }
        public bool IncludeRecipe { get; set; }
        public bool IncludeSkill { get; set; }
        public bool IncludePower { get; set; }
        
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
            IncludeEffect = true;
            IncludeItem = true;
            IncludeQuest = true;
            IncludeRecipe = true;
            IncludeSkill = true;
            IncludePower = true;
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
            foreach (KeyValuePair<Type, string> Entry in JsonFileTable)
                PerformSearch(TermList, Entry, SearchMode);
        }

        private void PerformSearch(List<string> TermList, KeyValuePair<Type, string> Entry, SearchModes SearchMode)
        {
            try
            {
                string IndexFilePath = Path.Combine(CurrentVersionCacheFolder, Entry.Value + "-index.txt");
                if (!File.Exists(IndexFilePath))
                    return;

                using (FileStream fs = new FileStream(IndexFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                    {
                        string TextContent = sr.ReadToEnd();

                        Type TypeIndex = Entry.Key;

                        if (TypeIndex == typeof(PgJsonObjects.Ability) && IncludeAbility)
                            PerformSearch(TermList, AbilityTable, TextContent, SearchMode);

                        else if (TypeIndex == typeof(PgJsonObjects.DirectedGoal) && IncludeDirectedGoal)
                            PerformSearch(TermList, DirectedGoalTable, TextContent, SearchMode);

                        else if (TypeIndex == typeof(PgJsonObjects.Effect) && IncludeEffect)
                            PerformSearch(TermList, EffectTable, TextContent, SearchMode);

                        else if (TypeIndex == typeof(PgJsonObjects.Item) && IncludeItem)
                            PerformSearch(TermList, ItemTable, TextContent, SearchMode);

                        else if (TypeIndex == typeof(PgJsonObjects.Quest) && IncludeQuest)
                            PerformSearch(TermList, QuestTable, TextContent, SearchMode);

                        else if (TypeIndex == typeof(PgJsonObjects.Recipe) && IncludeRecipe)
                            PerformSearch(TermList, RecipeTable, TextContent, SearchMode);

                        else if (TypeIndex == typeof(PgJsonObjects.Skill) && IncludeSkill)
                            PerformSearch(TermList, SkillTable, TextContent, SearchMode);

                        else if (TypeIndex == typeof(PgJsonObjects.Power) && IncludePower)
                            PerformSearch(TermList, PowerTable, TextContent, SearchMode);
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

            List<Skill> CombatSkillList = new List<Skill>();
            foreach (Skill SkillItem in SkillList)
                if (IsCombatSkill(SkillItem))
                        CombatSkillList.Add(SkillItem);

            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new CrunchSkillsHandler(OnCrunchSkills), CombatSkillList, 0, 0);
        }

        private delegate void CrunchSkillsHandler(List<Skill> CombatSkillList, int i, int j);
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
                Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new CrunchSkillsHandler(OnCrunchSkills), CombatSkillList, i, j + 1);
            else if (i + 1 < CombatSkillList.Count)
                Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new CrunchSkillsHandler(OnCrunchSkills), CombatSkillList, i + 1, 0);
            else
                IsCrunching = false;
        }

        private bool IsCombatSkill(Skill SkillItem)
        {
            if (!SkillItem.Combat)
                return false;

            if (SkillItem.XpTable.InternalName != "TypicalCombatSkill" && SkillItem.XpTable.InternalName != "TypicalCombatSkillExt")
                return false;

            int AbilityCount = 0;
            foreach (Ability Item in AbilityList)
                if (SkillItem.CombatSkill != PowerSkill.Internal_None && Item.Skill == SkillItem.CombatSkill)
                    AbilityCount++;

            if (AbilityCount < 6)
                return false;

            //Debug.Print(SkillItem.ToString() + ": " + SkillItem.XpTable + ", " + AbilityCount);

            return true;
        }

        private bool IsValidCombination(Skill PrimarySkill, Skill SecondarySkill)
        {
            if (PrimarySkill.CompatibleCombatSkillList.Contains(SecondarySkill.CombatSkill) && SecondarySkill.CompatibleCombatSkillList.Contains(PrimarySkill.CombatSkill))
                return true;

            //Debug.Print("Incompatible skills: " + PrimarySkill.Key + " and " + SecondarySkill.Key);

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
                    Debug.Print(Ability.SpecialInfo);

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
        private void OnClearVersionList(object sender, ExecutedRoutedEventArgs e)
        {
            ClearVersionList();
        }

        private void OnStartApplication(object sender, ExecutedRoutedEventArgs e)
        {
            StartApplication();
        }

        private void OnLocateLatestVersion(object sender, RoutedEventArgs e)
        {
            LocateLastestVersion = true;
            UseSpecificVersion = false;
        }

        private void OnUseSpecificVersion(object sender, RoutedEventArgs e)
        {
            LocateLastestVersion = false;
            UseSpecificVersion = true;
        }

        private void OnCancel(object sender, ExecutedRoutedEventArgs e)
        {
            CancelOperation();
        }

        private void OnRefreshBuildPlaner(object sender, SelectionChangedEventArgs e)
        {
            RefreshBuildPlaner();
        }

        private void OnAddPower(object sender, ExecutedRoutedEventArgs e)
        {
            FrameworkElement SourceControl = e.OriginalSource as FrameworkElement;
            SlotPlaner SenderSlot = SourceControl.DataContext as SlotPlaner;
            AddPower(SenderSlot);
        }

        private void OnRemovePower(object sender, ExecutedRoutedEventArgs e)
        {
            FrameworkElement SourceControl = e.OriginalSource as FrameworkElement;
            SlotPlaner SenderSlot = SourceControl.DataContext as SlotPlaner;
            RemovePower(SenderSlot);
        }

        private void OnLoadBuild(object sender, ExecutedRoutedEventArgs e)
        {
            LoadBuild();
        }

        private void OnSaveBuild(object sender, ExecutedRoutedEventArgs e)
        {
            SaveBuild();
        }

        private void OnCopyBuild(object sender, ExecutedRoutedEventArgs e)
        {
            CopyBuild();
        }

        private void OnCrunch(object sender, ExecutedRoutedEventArgs e)
        {
            Crunch();
        }

        private void OnOpenProfileFolder(object sender, ExecutedRoutedEventArgs e)
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

        private void OnBackward(object sender, ExecutedRoutedEventArgs e)
        {
            OnBackward();
        }

        private void OnForward(object sender, ExecutedRoutedEventArgs e)
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

        private ToggleButton XpTableButton;
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
