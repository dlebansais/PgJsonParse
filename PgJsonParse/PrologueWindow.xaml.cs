using PgJsonObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace PgJsonParse
{
    public partial class PrologueWindow : Window, INotifyPropertyChanged
    {
        #region Init
        public PrologueWindow()
        {
            try
            {
                InitializeComponent();
                DataContext = this;

                InitSettings();
                InitStatus();
                InitVersionCheck();
                InitParserCheck();
                InitVersionCache();
                InitParsing();
                InitIcons();

                if (CheckLastVersionOnStartup)
                    Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new CheckVersionHandler(OnCheckVersion));

                else if (CheckNewParserOnStartup)
                    Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new CheckParserHandler(OnCheckParser));

                Loaded += OnLoaded;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\r\n" + e.StackTrace);
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            ApplicationCommand.SubscribeToGlobalCommand("CloseCommand", OnClose);
            ApplicationCommand.SubscribeToGlobalCommand("CheckVersionCommand", OnCheckVersion);
            ApplicationCommand.SubscribeToGlobalCommand("SelectVersionCommand", OnSelectVersion);
            ApplicationCommand.SubscribeToGlobalCommand("DownloadVersionCommand", OnDownloadVersion);
            ApplicationCommand.SubscribeToGlobalCommand("CancelDownloadVersionCommand", OnCancelDownloadVersion);
            ApplicationCommand.SubscribeToGlobalCommand("DownloadIconsCommand", OnDownloadIcons);
            ApplicationCommand.SubscribeToGlobalCommand("CancelDownloadIconsCommand", OnCancelDownloadIcons);
            ApplicationCommand.SubscribeToGlobalCommand("StartCommand", OnStart);
            ApplicationCommand.SubscribeToGlobalCommand("CancelStartCommand", OnCancelStart);
            ApplicationCommand.SubscribeToGlobalCommand("DeleteVersionCommand", OnDeleteVersion);
            ApplicationCommand.SubscribeToGlobalCommand("DeleteIconsCommand", OnDeleteIcons);
        }
        #endregion

        #region Properties
        public string ApplicationFolder { get; private set; }
        public string VersionCacheFolder { get; private set; }
        public string IconCacheFolder { get; private set; }

        public bool IsGlobalInteractionEnabled
        {
            get { return _IsGlobalInteractionEnabled; }
            private set
            {
                if (_IsGlobalInteractionEnabled != value)
                {
                    _IsGlobalInteractionEnabled = value;
                    NotifyThisPropertyChanged();
                }
            }
        }
        private bool _IsGlobalInteractionEnabled;

        public bool IsIconStateUpdated
        {
            get { return _IsIconStateUpdated; }
            private set
            {
                if (_IsIconStateUpdated != value)
                {
                    _IsIconStateUpdated = value;
                    NotifyThisPropertyChanged();
                }
            }
        }
        private bool _IsIconStateUpdated;

        public bool ShowDeleteButton
        {
            get { return _ShowDeleteButton; }
            set
            {
                if (_ShowDeleteButton != value)
                {
                    _ShowDeleteButton = value;
                    NotifyThisPropertyChanged();
                }
            }
        }
        private bool _ShowDeleteButton;

        public bool CheckLastVersionOnStartup { get; set; }
        public bool DownloadNewVersionsAutomatically { get; set; }
        public int DefaultSelectedVersion { get; set; }
        public bool ShareIconFiles { get; set; }
        public bool KeepRecentVersions { get; set; }
        public bool StartAutomatically { get; set; }
        public int LastSelectedSetting { get; set; }

        public bool CheckNewParserOnStartup
        {
            get { return _CheckNewParserOnStartup; }
            set
            {
                if (_CheckNewParserOnStartup != value)
                {
                    _CheckNewParserOnStartup = value;
                    NotifyThisPropertyChanged();

                    if (StatusMessage == null && value == true && !IsParserUpdateChecked)
                        Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new CheckParserHandler(OnCheckParser));
                }
            }
        }
        private bool _CheckNewParserOnStartup;

        public bool AskToIgnoreMissingIcons
        {
            get { return MissingIconsAction == MissingIconsAction.Ask; }
            set { SetMissingIconsAction(MissingIconsAction.Ask); }
        }
        public bool DownloadMissingIcons
        {
            get { return MissingIconsAction == MissingIconsAction.Download; }
            set { SetMissingIconsAction(MissingIconsAction.Download); }
        }
        public bool IgnoreMissingIcons
        {
            get { return MissingIconsAction == MissingIconsAction.Ignore; }
            set { SetMissingIconsAction(MissingIconsAction.Ignore); }
        }
        private MissingIconsAction MissingIconsAction;

        private void SetMissingIconsAction(MissingIconsAction Action)
        {
            if (MissingIconsAction != Action)
            {
                MissingIconsAction = Action;
                NotifyPropertyChanged(nameof(AskToIgnoreMissingIcons));
                NotifyPropertyChanged(nameof(DownloadMissingIcons));
                NotifyPropertyChanged(nameof(IgnoreMissingIcons));
            }
        }
        #endregion

        #region Settings
        private void InitSettings()
        {
            ApplicationFolder = InitFolder(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PgJsonParse"));
            VersionCacheFolder = Path.Combine(ApplicationFolder, "Versions");
            IconCacheFolder = InitFolder(Path.Combine(ApplicationFolder, "Shared Icons"));
            _IsIconStateUpdated = false;

            CheckLastVersionOnStartup = App.GetSettingBool(nameof(CheckLastVersionOnStartup), true);
            _CheckNewParserOnStartup = App.GetSettingBool(nameof(CheckNewParserOnStartup), true);
            DownloadNewVersionsAutomatically = App.GetSettingBool(nameof(DownloadNewVersionsAutomatically), false);
            DefaultSelectedVersion = App.GetSettingInt(nameof(DefaultSelectedVersion), 0);
            ShareIconFiles = App.GetSettingBool(nameof(ShareIconFiles), true);
            MissingIconsAction = (MissingIconsAction)App.GetSettingEnum(nameof(MissingIconsAction), (int)MissingIconsAction.Ask, (int)MissingIconsAction.Ignore);
            KeepRecentVersions = App.GetSettingBool(nameof(KeepRecentVersions), true);
            ShowDeleteButton = App.GetSettingBool(nameof(ShowDeleteButton), true);
            StartAutomatically = App.GetSettingBool(nameof(StartAutomatically), false);
            LastSelectedSetting = App.GetSettingInt(nameof(LastSelectedSetting), 0);
            LastIconId = App.GetSettingString(nameof(LastIconId), null);
        }

        private string InitFolder(string FolderName)
        {
            if (!Directory.Exists(FolderName))
                Directory.CreateDirectory(FolderName);

            return FolderName;
        }

        private void SaveSettings()
        {
            if (CachedVersionIndex >= 0 && CachedVersionIndex < VersionList.Count)
                DefaultSelectedVersion = VersionList[CachedVersionIndex].Version;
            else
                DefaultSelectedVersion = 0;

            App.SetSettingBool(nameof(CheckLastVersionOnStartup), CheckLastVersionOnStartup);
            App.SetSettingBool(nameof(CheckNewParserOnStartup), CheckNewParserOnStartup);
            App.SetSettingBool(nameof(DownloadNewVersionsAutomatically), DownloadNewVersionsAutomatically);
            App.SetSettingInt(nameof(DefaultSelectedVersion), DefaultSelectedVersion);
            App.SetSettingBool(nameof(ShareIconFiles), ShareIconFiles);
            App.SetSettingInt(nameof(MissingIconsAction), (int)MissingIconsAction);
            App.SetSettingBool(nameof(KeepRecentVersions), KeepRecentVersions);
            App.SetSettingBool(nameof(ShowDeleteButton), ShowDeleteButton);
            App.SetSettingBool(nameof(StartAutomatically), StartAutomatically);
            App.SetSettingInt(nameof(LastSelectedSetting), LastSelectedSetting);
            App.SetSettingString(nameof(LastIconId), LastIconId);
        }

        private string LastIconId;
        #endregion

        #region Status
        private void InitStatus()
        {
            _StatusMessage = null;
            _LastExceptionMessage = null;
        }

        public string StatusMessage
        {
            get { return _StatusMessage; }
            private set
            {
                if (_StatusMessage != value)
                {
                    _StatusMessage = value;
                    NotifyThisPropertyChanged();
                }
            }
        }
        private string _StatusMessage;

        public string LastExceptionMessage
        {
            get { return _LastExceptionMessage; }
            private set
            {
                if (_LastExceptionMessage != value)
                {
                    _LastExceptionMessage = value;
                    NotifyThisPropertyChanged();
                }
            }
        }
        private string _LastExceptionMessage;
        #endregion

        #region Version Check
        private void InitVersionCheck()
        {
            if (CheckLastVersionOnStartup)
            {
                IsGlobalInteractionEnabled = false;
                _VersionCheckState = VersionCheckState.Checking;
            }
            else
            {
                IsGlobalInteractionEnabled = true;
                _VersionCheckState = VersionCheckState.Unchecked;
            }

            _LatestVersion = null;
        }

        public VersionCheckState VersionCheckState
        {
            get { return _VersionCheckState; }
            private set
            {
                if (_VersionCheckState != value)
                {
                    _VersionCheckState = value;
                    NotifyThisPropertyChanged();
                    NotifyPropertyChanged(nameof(IsLatestVersionSelectable));
                }
            }
        }
        private VersionCheckState _VersionCheckState;

        public bool IsLatestVersionSelectable
        {
            get
            {
                if (VersionCheckState != VersionCheckState.Known)
                    return false;

                if (LatestVersion == null)
                    return false;

                GameVersionInfo VersionInfo = SelectedVersion;
                if (VersionInfo == null)
                    return false;

                if (VersionInfo.Version.ToString() == LatestVersion)
                    return false;

                return true;
            }
        }

        public string LatestVersion
        {
            get { return _LatestVersion; }
            private set
            {
                if (_LatestVersion != value)
                {
                    _LatestVersion = value;
                    NotifyThisPropertyChanged();
                    NotifyPropertyChanged(nameof(IsLatestVersionSelectable));
                }
            }
        }
        private string _LatestVersion;

        private delegate void CheckVersionHandler();
        private async void OnCheckVersion()
        {
            IsGlobalInteractionEnabled = false;
            VersionCheckState = VersionCheckState.Checking;

            int Version = await Task.Run(() => { return ExecuteCheckVersion(); });
            if (Version > 0)
                ExecuteCompleteCheckVersion(Version);
            else
            {
                VersionCheckState = VersionCheckState.CheckFailed;
                IsGlobalInteractionEnabled = true;
            }
        }

        private int ExecuteCheckVersion()
        {
            int Version;

            Stopwatch Watch = new Stopwatch();
            Watch.Start();

            try
            {
                HttpWebRequest Request = WebRequest.Create("http://client.projectgorgon.com/fileversion.txt") as HttpWebRequest;
                using (WebResponse Response = Request.GetResponse())
                {
                    using (Stream ResponseStream = Response.GetResponseStream())
                    {
                        using (StreamReader Reader = new StreamReader(ResponseStream, Encoding.ASCII))
                        {
                            string Content = Reader.ReadToEnd();
                            if (int.TryParse(Content, out Version))
                            {
                                StatusMessage = null;
                                LastExceptionMessage = null;
                                Tools.MinimalSleep(Watch);
                            }
                            else
                                throw new Exception(Request.RequestUri + " is invalid.");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                StatusMessage = "Unable to connect to the game server.";
                LastExceptionMessage = e.Message;
                Version = 0;
            }

            return Version;
        }

        private async void ExecuteCompleteCheckVersion(int Version)
        {
            LatestVersion = Version.ToString();
            VersionCheckState = VersionCheckState.Known;

            int i;
            for (i = 0; i < VersionList.Count; i++)
            {
                GameVersionInfo Item = VersionList[i];
                if (Item.Version == Version)
                {
                    if (CachedVersionIndex < 0)
                        CachedVersionIndex = i;
                    break;
                }
            }

            if (i >= VersionList.Count)
            {
                DownloadState IconDownloadState = IsLastIconLoadedForVersion(Version) ? DownloadState.Downloaded : DownloadState.NotDownloaded;
                GameVersionInfo NewVersion = new GameVersionInfo(Version, DownloadState.NotDownloaded, IconDownloadState);

                for (i = 0; i < VersionList.Count; i++)
                    if (Version > VersionList[i].Version)
                        break;

                VersionList.Insert(i, NewVersion);
                if (CachedVersionIndex < 0)
                    CachedVersionIndex = i;

                if (CheckNewParserOnStartup)
                    await Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new CheckParserHandler(OnCheckParser));

                if (DownloadNewVersionsAutomatically)
                {
                    CachedVersionIndex = VersionList.IndexOf(NewVersion);
                    await Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new DownloadVersionHandler(OnDownloadVersion), NewVersion);
                    return;
                }
            }
            else
            {
                if (CheckNewParserOnStartup)
                    await Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new CheckParserHandler(OnCheckParser));

                GameVersionInfo VersionInfo = SelectedVersion;

                if (StartAutomatically)
                {
                    await Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new StartHandler(OnStart), VersionInfo);
                    return;
                }
            }

            IsGlobalInteractionEnabled = true;
        }

        private void OnSelectVersion()
        {
            IsGlobalInteractionEnabled = false;

            for (int i = 0; i < VersionList.Count; i++)
            {
                GameVersionInfo VersionInfo = VersionList[i];
                if (VersionInfo.Version.ToString() == LatestVersion)
                    CachedVersionIndex = i;
            }

            IsGlobalInteractionEnabled = true;
        }
        #endregion

        #region Parser Check
        public const double PARSER_VERSION = 298.1;

        private void InitParserCheck()
        {
            _IsNewParserAvailable = false;
            IsParserUpdateChecked = false;
        }

        public bool IsNewParserAvailable
        {
            get { return _IsNewParserAvailable; }
            private set
            {
                if (_IsNewParserAvailable != value)
                {
                    _IsNewParserAvailable = value;
                    NotifyThisPropertyChanged();
                }
            }
        }
        private bool _IsNewParserAvailable;

        public string ReleasePageAddress { get { return "https://github.com/dlebansais/PgJsonParse/releases"; } }

        private void OnCheckNewParser()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new CheckParserHandler(OnCheckParser));
        }

        private delegate void CheckParserHandler();
        private async void OnCheckParser()
        {
            IsParserUpdateChecked = true;

            IsNewParserAvailable = await Task.Run(() => { return ExecuteCheckParser(); });
        }

        private bool ExecuteCheckParser()
        {
            bool FoundUpdate = false;

            Stopwatch Watch = new Stopwatch();
            Watch.Start();

            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                HttpWebRequest Request = WebRequest.Create(ReleasePageAddress) as HttpWebRequest;
                using (WebResponse Response = Request.GetResponse())
                {
                    using (Stream ResponseStream = Response.GetResponseStream())
                    {
                        using (StreamReader Reader = new StreamReader(ResponseStream, Encoding.ASCII))
                        {
                            string Content = Reader.ReadToEnd();

                            string Pattern = @"<a href=""/dlebansais/PgJsonParse/releases/tag/";
                            int Index = Content.IndexOf(Pattern);
                            if (Index >= 0)
                            {
                                string ParserTagVersion = Content.Substring(Index + Pattern.Length, 20);
                                int EndIndex = ParserTagVersion.IndexOf('"');
                                if (EndIndex > 0)
                                {
                                    ParserTagVersion = ParserTagVersion.Substring(0, EndIndex);
                                    if (ParserTagVersion.ToLower().StartsWith("v"))
                                        ParserTagVersion = ParserTagVersion.Substring(1);

                                    double ReleasedParserVersion;
                                    if (double.TryParse(ParserTagVersion, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture.NumberFormat, out ReleasedParserVersion) && ReleasedParserVersion > PARSER_VERSION)
                                        FoundUpdate = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
            }

            return FoundUpdate;
        }

        private bool IsParserUpdateChecked;
        #endregion

        #region Version Cached
        private void InitVersionCache()
        {
            _CachedVersionIndex = -1;

            UpdateCachedVersion();
        }

        public ObservableCollection<GameVersionInfo> VersionList { get; private set; } = new ObservableCollection<GameVersionInfo>();

        public GameVersionInfo SelectedVersion { get { return CachedVersionIndex >= 0 && CachedVersionIndex < VersionList.Count ? VersionList[CachedVersionIndex] : null; } }
        public int CachedVersionIndex
        {
            get { return _CachedVersionIndex; }
            set
            {
                if (_CachedVersionIndex != value)
                {
                    _CachedVersionIndex = value;
                    NotifyThisPropertyChanged();
                    NotifyPropertyChanged(nameof(SelectedVersion));
                    NotifyPropertyChanged(nameof(IsLatestVersionSelectable));
                }
            }
        }
        private int _CachedVersionIndex;

        private void UpdateCachedVersion()
        {
            VersionList.Clear();

            try
            {
                string[] SubFolders = Directory.GetDirectories(VersionCacheFolder);
                if (SubFolders != null)
                {
                    GameVersionInfo DefaultVersion = null;
                    foreach (string SubFolder in SubFolders)
                    {
                        string FolderName = Path.GetFileName(SubFolder);

                        int Version;
                        if (int.TryParse(FolderName, out Version) && Version > 0)
                        {
                            string[] Files = Directory.GetFiles(SubFolder, "*.json");

                            int DowloadableCount = 0;
                            foreach (KeyValuePair<Type, IObjectDefinition> Entry in ObjectList.Definitions)
                                if (Entry.Value.MinVersion >= Version)
                                    DowloadableCount++;

                            DownloadState FileDownloadState = Files.Length >= DowloadableCount ? DownloadState.Downloaded : DownloadState.NotDownloaded;
                            DownloadState IconDownloadState = IsLastIconLoadedForVersion(Version) ? DownloadState.Downloaded : DownloadState.NotDownloaded;

                            GameVersionInfo NewVersion = new GameVersionInfo(Version, FileDownloadState, IconDownloadState);

                            int i;
                            for (i = 0; i < VersionList.Count; i++)
                                if (Version > VersionList[i].Version)
                                    break;

                            VersionList.Insert(i, NewVersion);

                            if (Version == DefaultSelectedVersion)
                                DefaultVersion = NewVersion;
                        }
                    }

                    if (DefaultVersion != null)
                        CachedVersionIndex = VersionList.IndexOf(DefaultVersion);
                    else if (VersionList.Count > 0)
                        CachedVersionIndex = 0;
                    else
                        CachedVersionIndex = -1;

                    if (SelectedVersion != null && SelectedVersion.IconDownloadState == DownloadState.Downloaded)
                        IsIconStateUpdated = true;
                }
                else
                    CachedVersionIndex = -1;
            }
            catch (Exception e)
            {
                StatusMessage = "Error looking for downloaded files.";
                LastExceptionMessage = e.Message;
                CachedVersionIndex = -1;
            }
        }

        private delegate void DownloadVersionHandler(GameVersionInfo VersionInfo);
        private async void OnDownloadVersion(GameVersionInfo VersionInfo)
        {
            IsGlobalInteractionEnabled = false;
            App.SetState(this, TaskbarProgress.TaskbarStates.Normal);

            VersionInfo.ProgressChanged += OnFileDownloadProgressChanged;
            bool Success = await Task.Run(() => { return ExecuteDownloadVersion(VersionInfo); });
            VersionInfo.ProgressChanged -= OnFileDownloadProgressChanged;

            if (Success)
            {
                App.SetState(this, TaskbarProgress.TaskbarStates.NoProgress);

                if (StartAutomatically)
                {
                    await Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new StartHandler(OnStart), VersionInfo);
                    return;
                }
            }
            else
            {
                App.SetState(this, TaskbarProgress.TaskbarStates.Error);
                StatusMessage = "Unable to download version files.";
            }

            IsGlobalInteractionEnabled = true;
        }

        public bool ExecuteDownloadVersion(GameVersionInfo VersionInfo)
        {
            string VersionFolder = InitFolder(Path.Combine(VersionCacheFolder, VersionInfo.Version.ToString()));

            List<string> JsonFileList = new List<string>();
            foreach (KeyValuePair<Type, IObjectDefinition> Entry in ObjectList.Definitions)
                if (Entry.Value.MinVersion <= VersionInfo.Version)
                    JsonFileList.Add(Entry.Value.JsonFileName);

            string LastExceptionMessage;
            bool Success = VersionInfo.DownloadFiles(JsonFileList, VersionFolder, out LastExceptionMessage);
            this.LastExceptionMessage = LastExceptionMessage;

            return Success;
        }

        private void OnCancelDownloadVersion(GameVersionInfo VersionInfo)
        {
            VersionInfo.CancelDownloadFiles();
        }

        private void OnDeleteVersion(GameVersionInfo VersionInfo)
        {
            int OldCachedVersionIndex = CachedVersionIndex;

            if (VersionList.Count == 1)
                if (MessageBox.Show("This will delete the last downloaded version, are you sure?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes)
                    return;

            try
            {
                string VersionFolder = Path.Combine(VersionCacheFolder, VersionInfo.Version.ToString());
                Directory.Delete(VersionFolder, true);

                VersionList.Remove(VersionInfo);
                if (OldCachedVersionIndex < VersionList.Count)
                    CachedVersionIndex = OldCachedVersionIndex;
                else if (VersionList.Count > 0)
                    CachedVersionIndex = VersionList.Count - 1;
                else
                    CachedVersionIndex = -1;
            }
            catch (Exception e)
            {
                StatusMessage = "Error deleting files.";
                LastExceptionMessage = e.Message;
            }
        }
        #endregion

        #region Parsing
        private void InitParsing()
        {
            _ParseProgress = 0;
        }

        public bool IsParsing
        {
            get { return _IsParsing; }
            private set
            {
                if (_IsParsing != value)
                {
                    _IsParsing = value;
                    NotifyThisPropertyChanged();
                    NotifyPropertyChanged(nameof(SelectedVersion));
                    NotifyPropertyChanged(nameof(IsLatestVersionSelectable));
                }
            }
        }
        private bool _IsParsing;

        public bool IsParsingCancelable
        {
            get { return _IsParsingCancelable; }
            private set
            {
                if (_IsParsingCancelable != value)
                {
                    _IsParsingCancelable = value;
                    NotifyThisPropertyChanged();
                    NotifyPropertyChanged(nameof(SelectedVersion));
                    NotifyPropertyChanged(nameof(IsLatestVersionSelectable));
                }
            }
        }
        private bool _IsParsingCancelable;

        public double ParseProgress
        {
            get { return _ParseProgress; }
            private set
            {
                if (_ParseProgress != value)
                {
                    _ParseProgress = value;
                    NotifyThisPropertyChanged();
                }
            }
        }
        private double _ParseProgress;

        public MainWindow Dlg
        {
            get { return _Dlg; }
            private set
            {
                if (_Dlg != value)
                {
                    _Dlg = value;
                    NotifyThisPropertyChanged();
                }
            }
        }
        private MainWindow _Dlg;

        private delegate void StartHandler(GameVersionInfo VersionInfo);
        private async void OnStart(GameVersionInfo VersionInfo)
        {
            IsGlobalInteractionEnabled = false;
            //Dlg = new MainWindow(ApplicationFolder);
            Dlg = new MainWindow();
            IsParsing = true;
            IsParsingCancelable = true;
            ParseCancellationTokenSource = new CancellationTokenSource();
            ParseProgress = 0;
            ParseErrorInfo ErrorInfo = new ParseErrorInfo();

            App.SetState(this, TaskbarProgress.TaskbarStates.Normal);
            bool Success = await Task.Run(() => { return ExecuteParse(VersionInfo, ErrorInfo); });
            App.SetState(this, TaskbarProgress.TaskbarStates.NoProgress);

            if (!Success)
            {
                IsParsing = false;
                IsParsingCancelable = false;
                Dlg.Close();
                Dlg = null;
                IsGlobalInteractionEnabled = true;
                return;
            }

            LoadedIconCount = 0;
            MissingIconList = new List<int>();
            foreach (KeyValuePair<int, bool> Entry in IconTable)
                if (Entry.Value == true)
                    LoadedIconCount++;
                else
                    MissingIconList.Add(Entry.Key);

            IsIconStateUpdated = true;
            bool IsMissingIconListNotEmpty = MissingIconList.Count > 0;

            if (IsMissingIconListNotEmpty)
            {
                if (ShareIconFiles)
                {
                    foreach (GameVersionInfo Item in VersionList)
                        Item.SetIconsDownloaded(false);
                }
                else
                    VersionInfo.SetIconsDownloaded(false);

                LastIconId = "icon_" + MissingIconList[MissingIconList.Count - 1];

                switch (MissingIconsAction)
                {
                    default:
                    case MissingIconsAction.Ask:
                        if (MessageBox.Show("There are " + MissingIconList.Count + " icon(s) not downloaded yet, would you like to get them now?", "Starting", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            FollowStartWithDownloadIcons(VersionInfo);
                            return;
                        }
                        break;

                    case MissingIconsAction.Download:
                        FollowStartWithDownloadIcons(VersionInfo);
                        return;

                    case MissingIconsAction.Ignore:
                        break;
                }
            }

            if (!KeepRecentVersions)
            {
                try
                {
                    List<GameVersionInfo> ToRemove = new List<GameVersionInfo>();
                    foreach (GameVersionInfo Item in VersionList)
                        if (Item.Version < VersionInfo.Version)
                            ToRemove.Add(Item);

                    foreach (GameVersionInfo Item in ToRemove)
                    {
                        string VersionFolder = Path.Combine(VersionCacheFolder, Item.Version.ToString());
                        Directory.Delete(VersionFolder, true);

                        VersionList.Remove(Item);
                    }

                    CachedVersionIndex = VersionList.IndexOf(VersionInfo);
                }
                catch (Exception e)
                {
                    StatusMessage = "Error deleting files.";
                    LastExceptionMessage = e.Message;
                }
            }

            string Warnings = ErrorInfo.GetWarnings();
            if (Warnings.Length > 0)
            {
                Debug.Print(Warnings);
                Dlg.WarningText = Warnings;
            }
            else
                Dlg.WarningText = "Files loaded and parsed, no warnings.";

            Dlg.LoadedVersion = VersionInfo;
            Dlg.ApplicationFolder = ApplicationFolder;
            Dlg.IconCacheFolder = IconCacheFolder;
            Dlg.CurrentVersionCacheFolder = InitFolder(Path.Combine(VersionCacheFolder, VersionInfo.Version.ToString()));
            Dlg.IconFile = Path.Combine(ApplicationFolder, "mainicon.png");
            Dlg.FavorIconFile = Path.Combine(ApplicationFolder, "favoricon.png");

            Dlg.Show();
            Close();
            Dlg.StartApplication();

            IsGlobalInteractionEnabled = true;
        }

        public bool ExecuteParse(GameVersionInfo VersionInfo, ParseErrorInfo ErrorInfo)
        {
            string VersionFolder = Path.Combine(VersionCacheFolder, VersionInfo.Version.ToString());
            string IconFolder = ShareIconFiles ? IconCacheFolder : VersionFolder;

            IconTable.Clear();

            int ProgressIndex = 0;
            foreach (KeyValuePair<Type, IObjectDefinition> Entry in ObjectList.Definitions)
            {
                if (Entry.Value.MinVersion <= VersionInfo.Version)
                {
                    if (!LoadNextFile(Entry.Value, VersionFolder, ErrorInfo))
                        return false;

                    if (ParseCancellationTokenSource.IsCancellationRequested)
                        return false;
                }

                ParseProgress = (ProgressIndex * 100.0) / ObjectList.Definitions.Count;
                ProgressIndex++;

                App.SetValue(this, ParseProgress, 100.0);
            }

            return ConnectTables(VersionFolder, IconFolder, ErrorInfo);
        }

        private bool LoadNextFile(IObjectDefinition Definition, string VersionFolder, ParseErrorInfo ErrorInfo)
        {
            try
            {
                string FilePath = Path.Combine(VersionFolder, Definition.JsonFileName + ".json");

                IParser FileParser = Definition.FileParser;
                IList ObjectList = Definition.ObjectList;
                Dictionary<string, IGenericJsonObject> ObjectTable = Definition.ObjectTable;
                FileParser.LoadRaw(FilePath, ObjectList, ErrorInfo);

                ObjectTable.Clear();
                foreach (IGenericJsonObject Item in ObjectList)
                    ObjectTable.Add(Item.Key, Item);
            }
            catch (Exception e)
            {
                StatusMessage = "File parsing error.";
                LastExceptionMessage = e.Message;
                return false;
            }

            return true;
        }

        private bool ConnectTables(string VersionFolder, string IconFolder, ParseErrorInfo ErrorInfo)
        {
            Dictionary<Type, IList> AllLists = new Dictionary<Type, IList>();
            Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables = new Dictionary<Type, Dictionary<string, IGenericJsonObject>>();
            foreach (KeyValuePair<Type, IObjectDefinition> Entry in ObjectList.Definitions)
            {
                IObjectDefinition Definition = Entry.Value;
                AllLists.Add(Entry.Key, Definition.ObjectList);
                AllTables.Add(Entry.Key, Definition.ObjectTable);
            }

            foreach (KeyValuePair<Type, IObjectDefinition> Entry in ObjectList.Definitions)
            {
                IObjectDefinition Definition = Entry.Value;
                foreach (IGenericJsonObject Item in Definition.ObjectList)
                    Item.Connect(ErrorInfo, null, AllTables);

                if (ParseCancellationTokenSource.IsCancellationRequested)
                    return false;
            }

            IObjectDefinition RecipeDefinition = ObjectList.Definitions[typeof(Recipe)];
            bool Continue;
            do
            {
                Continue = false;
                foreach (Recipe Item in RecipeDefinition.ObjectList)
                    Item.MeasurePerfectCottonRatio(ref Continue);
            }
            while (Continue);

            foreach (KeyValuePair<Type, IObjectDefinition> Entry in ObjectList.Definitions)
            {
                IObjectDefinition Definition = Entry.Value;
                foreach (IGenericJsonObject Item in Definition.ObjectList)
                    Item.SetIndirectProperties(AllTables, ErrorInfo);
            }

            foreach (KeyValuePair<Type, IObjectDefinition> Entry in ObjectList.Definitions)
            {
                IObjectDefinition Definition = Entry.Value;
                foreach (IGenericJsonObject Item in Definition.ObjectList)
                    Item.SortLinkBack();
            }

            if (ParseCancellationTokenSource.IsCancellationRequested)
                return false;

            return CreateIndexes(VersionFolder, IconFolder, ErrorInfo);
        }

        private bool CreateIndexes(string VersionFolder, string IconFolder, ParseErrorInfo ErrorInfo)
        {
            foreach (KeyValuePair<Type, IObjectDefinition> Entry in ObjectList.Definitions)
            {
                if (!CreateNextIndex(Entry.Value, VersionFolder, ErrorInfo))
                    return false;

                if (ParseCancellationTokenSource.IsCancellationRequested)
                    return false;
            }

            IsParsingCancelable = false;
            CreateMushroomIndex(ErrorInfo);

            return EnumerateMissingIcons(IconFolder, ErrorInfo);
        }

        private bool CreateNextIndex(IObjectDefinition Definition, string VersionFolder, ParseErrorInfo ErrorInfo)
        {
            try
            {
                string IndexFilePath = Path.Combine(VersionFolder, Definition.JsonFileName + "-index.txt");
                IParser FileParser = Definition.FileParser;
                Dictionary<string, IGenericJsonObject> ObjectTable = Definition.ObjectTable;
                FileParser.CreateIndex(IndexFilePath, ObjectTable);
            }
            catch (Exception e)
            {
                StatusMessage = "Index creation error.";
                LastExceptionMessage = e.Message;
                return false;
            }

            return true;
        }

        private void CreateMushroomIndex(ParseErrorInfo ErrorInfo)
        {
            string MushroomNameFile = Path.Combine(ApplicationFolder, "Mushrooms.txt");

            List<string> MushroomNameList = new List<string>();
            IObjectDefinition ItemDefinition = ObjectList.Definitions[typeof(Item)];
            Dictionary<string, IGenericJsonObject> ItemTable = ItemDefinition.ObjectTable;

            foreach (KeyValuePair<string, IGenericJsonObject> Entry in ItemTable)
            {
                Item Item = Entry.Value as Item;
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
            catch (Exception e)
            {
                StatusMessage = "Mushroom index file creation error.";
                LastExceptionMessage = e.Message;
            }
        }

        private bool EnumerateMissingIcons(string IconFolder, ParseErrorInfo ErrorInfo)
        {
            foreach (int IconId in ErrorInfo.IconList)
            {
                string FilePath = Path.Combine(IconFolder, "icon_" + IconId + ".png");
                IconTable.Add(IconId, File.Exists(FilePath));
            }

            return true;
        }

        private void CancelParse()
        {
            ParseCancellationTokenSource.Cancel();
        }

        private async void FollowStartWithDownloadIcons(GameVersionInfo VersionInfo)
        {
            IsParsing = false;
            IsParsingCancelable = false;
            Dlg.Close();
            Dlg = null;
            await Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new DownloadIconsHandler(OnDownloadIcons), VersionInfo);
        }

        private CancellationTokenSource ParseCancellationTokenSource;
        #endregion

        #region Icons
        private void InitIcons()
        {
            LoadedIconCount = 0;
            MissingIconList = new List<int>();
            IconTable = new Dictionary<int, bool>();
            UpdateWindowIcon();
        }

        private void UpdateWindowIcon()
        {
            Icon = Tools.IconFileToImageSource(Path.Combine(ApplicationFolder, "mainicon.png"));
        }

        private bool IsLastIconLoadedForVersion(int Version)
        {
            if (LastIconId == null)
                return false;

            string IconFolder = ShareIconFiles ? IconCacheFolder : Path.Combine(VersionCacheFolder, Version.ToString());
            string LastIconFile = Path.Combine(IconFolder, LastIconId + ".png");
            bool FileExists = File.Exists(LastIconFile);

            return FileExists;
        }

        private delegate void DownloadIconsHandler(GameVersionInfo VersionInfo);
        private async void OnDownloadIcons(GameVersionInfo VersionInfo)
        {
            IsGlobalInteractionEnabled = false;
            IsIconStateUpdated = true;
            App.SetState(this, TaskbarProgress.TaskbarStates.Normal);

            VersionInfo.ProgressChanged += OnIconDownloadProgressChanged;
            bool Success = await Task.Run(() => { return ExecuteDownloadIcons(VersionInfo); });
            VersionInfo.ProgressChanged -= OnIconDownloadProgressChanged;

            if (Success)
            {
                App.SetState(this, TaskbarProgress.TaskbarStates.NoProgress);

                string IconFolder = ShareIconFiles ? IconCacheFolder : Path.Combine(VersionCacheFolder, VersionInfo.Version.ToString());
                string IconFile = Path.Combine(ApplicationFolder, "mainicon.png");
                string SourceIconFile = Path.Combine(IconFolder, "icon_5624.png");
                string FavorIconFile = Path.Combine(ApplicationFolder, "favoricon.png");
                string SourceFavorIconFile = Path.Combine(IconFolder, "icon_102.png");

                if (File.Exists(SourceIconFile))
                    File.Copy(SourceIconFile, IconFile, true);
                if (File.Exists(SourceFavorIconFile))
                    File.Copy(SourceFavorIconFile, FavorIconFile, true);

                if (StartAutomatically)
                    await Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new StartHandler(OnStart), VersionInfo);
            }
            else
            {
                App.SetState(this, TaskbarProgress.TaskbarStates.Error);
                StatusMessage = "Failed to download icons.";
            }

            UpdateWindowIcon();

            IsGlobalInteractionEnabled = true;
        }

        private bool ExecuteDownloadIcons(GameVersionInfo VersionInfo)
        {
            string DestinationFolder = ShareIconFiles ? IconCacheFolder : Path.Combine(VersionCacheFolder, VersionInfo.Version.ToString());

            int CurrentLoadedIconCount = LoadedIconCount;
            string LastExceptionMessage;

            bool Success = VersionInfo.DownloadIcons(ref CurrentLoadedIconCount, MissingIconList, DestinationFolder, out LastExceptionMessage);

            this.LastExceptionMessage = LastExceptionMessage;
            LoadedIconCount = CurrentLoadedIconCount;

            return Success;
        }

        private void OnCancelDownloadIcons(GameVersionInfo VersionInfo)
        {
            VersionInfo.CancelDownloadIcons();
        }

        private delegate void IconSharingChangedHandler();
        private void OnIconSharingChanged()
        {
            bool IsSharedIconsDownloaded = false;
            bool IsNonSharedIconsDownloaded = true;

            foreach (GameVersionInfo Item in VersionList)
            {
                bool IconsDownloaded = IsLastIconLoadedForVersion(Item.Version);
                Item.SetIconsDownloaded(IconsDownloaded);

                if (ShareIconFiles && IconsDownloaded)
                    IsSharedIconsDownloaded = true;
                else if (!ShareIconFiles && !IconsDownloaded)
                    IsNonSharedIconsDownloaded = false;
            }

            if (IsSharedIconsDownloaded)
                IsIconStateUpdated = true;
            else if (!IsNonSharedIconsDownloaded)
                IsIconStateUpdated = false;
        }

        private void OnDeleteIcons()
        {
            if (!ShareIconFiles)
                return;

            if (MessageBox.Show("This will delete all shared icons, are you sure?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes)
                return;

            string IconFolder = IconCacheFolder;

            try
            {
                if (Directory.Exists(IconCacheFolder))
                    Directory.Delete(IconCacheFolder, true);
            }
            catch (Exception e)
            {
                StatusMessage = "Error deleting files.";
                LastExceptionMessage = e.Message;
            }

            LoadedIconCount = 0;
            LastIconId = null;
            IsIconStateUpdated = false;

            Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new IconSharingChangedHandler(OnIconSharingChanged));
        }

        private int LoadedIconCount;
        private List<int> MissingIconList;
        private Dictionary<int, bool> IconTable;
        #endregion

        #region Events
        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private async void OnCheckVersion(object sender, EventArgs e)
        {
            App.SetState(this, TaskbarProgress.TaskbarStates.NoProgress);
            await Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new CheckVersionHandler(OnCheckVersion));
        }

        private void OnSelectVersion(object sender, EventArgs e)
        {
            App.SetState(this, TaskbarProgress.TaskbarStates.NoProgress);
            OnSelectVersion();
        }

        private GameVersionInfo VersionInfoFromControl(EventArgs e)
        {
            GameVersionInfo VersionInfo = (e as ExecutedEventArgs).Parameter as GameVersionInfo;
            return VersionInfo;
        }

        private async void OnDownloadVersion(object sender, EventArgs e)
        {
            App.SetState(this, TaskbarProgress.TaskbarStates.NoProgress);
            await Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new DownloadVersionHandler(OnDownloadVersion), VersionInfoFromControl(e));
        }

        private void OnCancelDownloadVersion(object sender, EventArgs e)
        {
            OnCancelDownloadVersion(VersionInfoFromControl(e));
        }

        private void OnDeleteVersion(object sender, EventArgs e)
        {
            App.SetState(this, TaskbarProgress.TaskbarStates.NoProgress);
            OnDeleteVersion(VersionInfoFromControl(e));
        }

        private void OnDeleteIcons(object sender, EventArgs e)
        {
            App.SetState(this, TaskbarProgress.TaskbarStates.NoProgress);
            OnDeleteIcons();
        }

        private async void OnDownloadIcons(object sender, EventArgs e)
        {
            App.SetState(this, TaskbarProgress.TaskbarStates.NoProgress);
            await Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new DownloadIconsHandler(OnDownloadIcons), VersionInfoFromControl(e));
        }

        private void OnCancelDownloadIcons(object sender, EventArgs e)
        {
            OnCancelDownloadIcons(VersionInfoFromControl(e));
        }

        private async void OnStart(object sender, EventArgs e)
        {
            App.SetState(this, TaskbarProgress.TaskbarStates.NoProgress);
            await Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new StartHandler(OnStart), VersionInfoFromControl(e));
        }

        private void OnCancelStart(object sender, EventArgs e)
        {
            CancelParse();
        }

        private void SetOpenPopupButton(ToggleButton Button)
        {
            OpenPopupButton = Button;
        }

        private void ClearOpenPopupButton()
        {
            OpenPopupButton = null;
        }

        private void OnToggleButtonChecked(object sender, RoutedEventArgs e)
        {
            SetOpenPopupButton(sender as ToggleButton);
        }

        private void OnToggleButtonUnchecked(object sender, RoutedEventArgs e)
        {
            ClearOpenPopupButton();
        }

        private void OnPopupSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClosePopups();
        }

        private void OnPopupMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ClosePopups();
        }

        private void OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(ReleasePageAddress);
        }

        private void OnFileDownloadProgressChanged(object sender, EventArgs e)
        {
            GameVersionInfo VersionInfo = sender as GameVersionInfo;
            App.SetValue(this, VersionInfo.FileDownloadProgress, 100.0);
        }

        private void OnIconDownloadProgressChanged(object sender, EventArgs e)
        {
            GameVersionInfo VersionInfo = sender as GameVersionInfo;
            App.SetValue(this, VersionInfo.IconDownloadProgress, 100.0);
        }

        private void OnPopupClosed(object sender, EventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                Mouse.AddMouseUpHandler(this, OnMouseUp);
                Mouse.Capture(this);
            }
            else
                Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new ClosePopupsHandler(ClosePopups));
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.RemoveMouseUpHandler(this, OnMouseUp);
            Mouse.Capture(null);
            Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new ClosePopupsHandler(ClosePopups));

            e.Handled = false;
        }

        public delegate void ClosePopupsHandler();
        public void ClosePopups()
        {
            if (OpenPopupButton != null && OpenPopupButton.IsChecked.HasValue && OpenPopupButton.IsChecked.Value == true)
            {
                OpenPopupButton.IsChecked = false;
                ClearOpenPopupButton();
            }
        }

        private void OnIconSharingChanged(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new IconSharingChangedHandler(OnIconSharingChanged));
        }

        private void OnClose(object sender, EventArgs e)
        {
            App.SetState(this, TaskbarProgress.TaskbarStates.NoProgress);
            Close();
        }

        private void OnWindowClosed(object sender, EventArgs e)
        {
            if (Dlg != null && !Dlg.IsVisible)
                Dlg.Close();

            SaveSettings();
        }

        private ToggleButton OpenPopupButton;
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
