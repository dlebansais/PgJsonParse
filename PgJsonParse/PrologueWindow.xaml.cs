using PgJsonObjects;
using Presentation;
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
using System.Windows.Media;
using Tools;
#if CSHARP_XAML_FOR_HTML5
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
#else
#endif

namespace PgJsonParse
{
    public partial class PrologueWindow : RootControl, INotifyPropertyChanged
    {
#region Init
        public PrologueWindow()
            : base(RootControlMode.CustomShape)
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
                    InvokeAndForget(new Action(() => OnCheckVersion()));

                else if (CheckNewParserOnStartup)
                    InvokeAndForget(new Action(() => OnCheckParser()));

                Loaded += OnLoaded;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\r\n" + e.StackTrace);
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            SubscribeToCommand("CheckVersionCommand", OnCheckVersion);
            SubscribeToCommand("SelectVersionCommand", OnSelectVersion);
            SubscribeToCommand("DownloadVersionCommand", OnDownloadVersion);
            SubscribeToCommand("CancelDownloadVersionCommand", OnCancelDownloadVersion);
            SubscribeToCommand("DownloadIconsCommand", OnDownloadIcons);
            SubscribeToCommand("CancelDownloadIconsCommand", OnCancelDownloadIcons);
            SubscribeToCommand("StartCommand", OnStart);
            SubscribeToCommand("CancelStartCommand", OnCancelStart);
            SubscribeToCommand("DeleteVersionCommand", OnDeleteVersion);
            SubscribeToCommand("DeleteIconsCommand", OnDeleteIcons);
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
                    NotifyPropertyChanged(nameof(CanDeleteSharedIcons));
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

        public bool ShareIconFiles
        {
            get { return _ShareIconFiles; }
            set
            {
                if (_ShareIconFiles != value)
                {
                    _ShareIconFiles = value;
                    NotifyThisPropertyChanged();
                    NotifyPropertyChanged(nameof(CanDeleteSharedIcons));
                }
            }
        }
        private bool _ShareIconFiles;

        public bool CanDeleteSharedIcons
        {
            get { return ShareIconFiles && IsGlobalInteractionEnabled; }
        }

        public bool CheckLastVersionOnStartup { get; set; }
        public bool DownloadNewVersionsAutomatically { get; set; }
        public int DefaultSelectedVersion { get; set; }
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
                        InvokeAndForget(new Action(() => OnCheckParser()));
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
            string UserRootFolder = InitFolder(PresentationEnvironment.UserRootFolder);
            ApplicationFolder = InitFolder(Path.Combine(UserRootFolder, "PgJsonParse"));
            VersionCacheFolder = InitFolder(Path.Combine(ApplicationFolder, "Versions"));
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

        private async void OnCheckVersion()
        {
            IsGlobalInteractionEnabled = false;
            VersionCheckState = VersionCheckState.Checking;

            int Version = await RunAsync(() => { return ExecuteCheckVersion(); });
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
                string RequestUri = "http://client.projectgorgon.com/fileversion.txt";
                string Content = WebClientTool.DownloadText(RequestUri);
                if (int.TryParse(Content, out Version))
                {
                    StatusMessage = null;
                    LastExceptionMessage = null;
                    UserUI.MinimalSleep(Watch);
                }
                else
                    throw new Exception(RequestUri + " is invalid.");
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
                GameVersionInfo NewVersion = AddCheckedVersion(Version);

                if (CheckNewParserOnStartup)
                    await InvokeAndWait(new Action(() => OnCheckParser()));

                if (DownloadNewVersionsAutomatically)
                {
                    CachedVersionIndex = VersionList.IndexOf(NewVersion);
                    await InvokeAndWait(new Action(() => OnDownloadVersion(NewVersion)));
                    return;
                }
            }
            else
            {
                if (CheckNewParserOnStartup)
                    await InvokeAndWait(new Action(() => OnCheckParser()));

                GameVersionInfo VersionInfo = SelectedVersion;

                if (StartAutomatically)
                {
                    await InvokeAndWait(new Action(() => OnStart(VersionInfo)));
                    return;
                }
            }

            IsGlobalInteractionEnabled = true;
        }

        private GameVersionInfo AddCheckedVersion(int Version)
        {
            DownloadState IconDownloadState = IsLastIconLoadedForVersion(Version) ? DownloadState.Downloaded : DownloadState.NotDownloaded;
            GameVersionInfo NewVersion = new GameVersionInfo(this, Version, DownloadState.NotDownloaded, IconDownloadState);

            int i;
            for (i = 0; i < VersionList.Count; i++)
                if (Version > VersionList[i].Version)
                    break;

            VersionList.Insert(i, NewVersion);
            if (CachedVersionIndex < 0)
                CachedVersionIndex = i;

            return NewVersion;
        }

        private void OnSelectVersion()
        {
            IsGlobalInteractionEnabled = false;

            int i;
            for (i = 0; i < VersionList.Count; i++)
            {
                GameVersionInfo VersionInfo = VersionList[i];
                if (VersionInfo.Version.ToString() == LatestVersion)
                {
                    CachedVersionIndex = i;
                    break;
                }
            }

            if (i >= VersionList.Count)
            {
                int LatestVersionInt;
                if (int.TryParse(LatestVersion, out LatestVersionInt))
                    AddCheckedVersion(LatestVersionInt);
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
            InvokeAndForget(new Action(() => OnCheckParser()));
        }

        private async void OnCheckParser()
        {
            IsParserUpdateChecked = true;
            IsNewParserAvailable = await RunAsync(() => { return ExecuteCheckParser(); });
        }

        private bool ExecuteCheckParser()
        {
            bool FoundUpdate = false;

            Stopwatch Watch = new Stopwatch();
            Watch.Start();

            try
            {
                NetTools.EnableTls12();

                string Content = WebClientTool.DownloadText(ReleasePageAddress);
                string Pattern = @"<a href=""/dlebansais/PgJsonParse/releases/tag/";
                int Index = Content == null ? -1 : Content.IndexOf(Pattern);
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
                        if (double.TryParse(ParserTagVersion, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out ReleasedParserVersion) && ReleasedParserVersion > PARSER_VERSION)
                            FoundUpdate = true;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
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
                                if (Entry.Value.MinVersion == 0 || Entry.Value.MinVersion >= Version)
                                    DowloadableCount++;

                            DownloadState FileDownloadState = Files.Length >= DowloadableCount ? DownloadState.Downloaded : DownloadState.NotDownloaded;
                            DownloadState IconDownloadState = IsLastIconLoadedForVersion(Version) ? DownloadState.Downloaded : DownloadState.NotDownloaded;

                            GameVersionInfo NewVersion = new GameVersionInfo(this, Version, FileDownloadState, IconDownloadState);

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

        private async void OnDownloadVersion(GameVersionInfo VersionInfo)
        {
            IsGlobalInteractionEnabled = false;
            App.SetState(this, TaskbarStates.Normal);

            VersionInfo.ProgressChanged += OnFileDownloadProgressChanged;
            bool Success = await RunAsync(() => { return ExecuteDownloadVersion(VersionInfo); });
            VersionInfo.ProgressChanged -= OnFileDownloadProgressChanged;

            if (Success)
            {
                App.SetState(this, TaskbarStates.NoProgress);

                if (StartAutomatically)
                {
                    await InvokeAndWait(new Action(() => OnStart(VersionInfo)));
                    return;
                }
            }
            else
            {
                App.SetState(this, TaskbarStates.Error);
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
                if (Confirmation.Show("This will delete the last downloaded version.", "Delete", true, ConfirmationType.Warning) != MessageBoxResult.OK)
                    return;

            try
            {
                string VersionFolder = Path.Combine(VersionCacheFolder, VersionInfo.Version.ToString());
                if (Directory.Exists(VersionFolder))
                    FolderTools.DeleteDirectory(VersionFolder, true);

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

        private async void OnStart(GameVersionInfo VersionInfo)
        {
            IsGlobalInteractionEnabled = false;
            Dlg = new MainWindow();
            IsParsing = true;
            IsParsingCancelable = true;
            ParseCancellation = new Cancellation();
            ParseProgress = 0;
            ParseErrorInfo ErrorInfo = new ParseErrorInfo();

            App.SetState(this, TaskbarStates.Normal);
            bool Success = await RunAsync(() => { return ExecuteParse(VersionInfo, ErrorInfo); });
            App.SetState(this, TaskbarStates.NoProgress);

            if (!Success)
            {
                IsParsing = false;
                IsParsingCancelable = false;
                Dlg.ControlClose();
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
                        if (Confirmation.Show("There are " + MissingIconList.Count + " icon(s) not downloaded yet, would you like to get them now?", "Starting", true, ConfirmationType.Info) == MessageBoxResult.OK)
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
                        FolderTools.DeleteDirectory(VersionFolder, true);

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
                Debug.WriteLine(Warnings);
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

            SwitchTo(Dlg, new Action(() => Dlg.StartApplication()));

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

                    if (ParseCancellation.IsCanceled)
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

                if (ParseCancellation.IsCanceled)
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

            if (ParseCancellation.IsCanceled)
                return false;

            return CreateIndexes(VersionFolder, IconFolder, ErrorInfo);
        }

        private bool CreateIndexes(string VersionFolder, string IconFolder, ParseErrorInfo ErrorInfo)
        {
            foreach (KeyValuePair<Type, IObjectDefinition> Entry in ObjectList.Definitions)
            {
                if (!CreateNextIndex(Entry.Value, VersionFolder, ErrorInfo))
                    return false;

                if (ParseCancellation.IsCanceled)
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
                        {
                            string Line = MushroomName + PgJsonObjects.Tools.NewLine;
                            sw.Write(Line);
                        }
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
            ParseCancellation.Cancel();
        }

        private async void FollowStartWithDownloadIcons(GameVersionInfo VersionInfo)
        {
            IsParsing = false;
            IsParsingCancelable = false;
            Dlg.ControlClose();
            Dlg = null;

            await InvokeAndWait(new Action(() => OnDownloadIcons(VersionInfo)));
        }

        private Cancellation ParseCancellation;
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
            ImageConversion.UpdateWindowIconUsingFile(this, Path.Combine(ApplicationFolder, "mainicon.png"));
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

        private async void OnDownloadIcons(GameVersionInfo VersionInfo)
        {
            IsGlobalInteractionEnabled = false;
            IsIconStateUpdated = true;
            App.SetState(this, TaskbarStates.Normal);

            VersionInfo.ProgressChanged += OnIconDownloadProgressChanged;
            bool Success = await RunAsync(() => { return ExecuteDownloadIcons(VersionInfo); });
            VersionInfo.ProgressChanged -= OnIconDownloadProgressChanged;

            if (Success)
            {
                App.SetState(this, TaskbarStates.NoProgress);

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
                    await InvokeAndWait(new Action(() => OnStart(VersionInfo)));
            }
            else
            {
                App.SetState(this, TaskbarStates.Error);
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

            if (Confirmation.Show("This will delete all shared icons.", "Delete", true, ConfirmationType.Warning) != MessageBoxResult.OK)
                return;

            string IconFolder = IconCacheFolder;

            try
            {
                if (Directory.Exists(IconCacheFolder))
                    FolderTools.DeleteDirectory(IconCacheFolder, true);
            }
            catch (Exception e)
            {
                StatusMessage = "Error deleting files.";
                LastExceptionMessage = e.Message;
            }

            LoadedIconCount = 0;
            LastIconId = null;
            IsIconStateUpdated = false;

            InvokeAndForget(new Action(OnIconSharingChanged));
        }

        private int LoadedIconCount;
        private List<int> MissingIconList;
        private Dictionary<int, bool> IconTable;
#endregion

#region Events
        private async void OnCheckVersion(object sender, EventArgs e)
        {
            App.SetState(this, TaskbarStates.NoProgress);
            await InvokeAndWait(new Action(() => OnCheckVersion()));
        }

        private void OnSelectVersion(object sender, EventArgs e)
        {
            App.SetState(this, TaskbarStates.NoProgress);
            OnSelectVersion();
        }

        private GameVersionInfo VersionInfoFromControl(EventArgs e)
        {
            GameVersionInfo VersionInfo = (e as ExecutedEventArgs).Parameter as GameVersionInfo;
            return VersionInfo;
        }

        private async void OnDownloadVersion(object sender, EventArgs e)
        {
            App.SetState(this, TaskbarStates.NoProgress);
            await InvokeAndWait(new Action(() => OnDownloadVersion(VersionInfoFromControl(e))));
        }

        private void OnCancelDownloadVersion(object sender, EventArgs e)
        {
            OnCancelDownloadVersion(VersionInfoFromControl(e));
        }

        private void OnDeleteVersion(object sender, EventArgs e)
        {
            App.SetState(this, TaskbarStates.NoProgress);
            OnDeleteVersion(VersionInfoFromControl(e));
        }

        private void OnDeleteIcons(object sender, EventArgs e)
        {
            App.SetState(this, TaskbarStates.NoProgress);
            OnDeleteIcons();
        }

        private async void OnDownloadIcons(object sender, EventArgs e)
        {
            App.SetState(this, TaskbarStates.NoProgress);
            await InvokeAndWait(new Action(() => OnDownloadIcons(VersionInfoFromControl(e))));
        }

        private void OnCancelDownloadIcons(object sender, EventArgs e)
        {
            OnCancelDownloadIcons(VersionInfoFromControl(e));
        }

        private async void OnStart(object sender, EventArgs e)
        {
            App.SetState(this, TaskbarStates.NoProgress);
            await InvokeAndWait(new Action(() => OnStart(VersionInfoFromControl(e))));
        }

        private void OnCancelStart(object sender, EventArgs e)
        {
            CancelParse();
        }
        
        /*
        private void OnToggleButtonChecked(object sender, RoutedEventArgs e)
        {
            PopupHandler.SetOpenPopupButton(sender as ToggleButton);
        }*/

        private void OnToggleButtonUnchecked(object sender, RoutedEventArgs e)
        {
            PopupHandler.ClearOpenPopupButton();
        }

        private void OnPopupSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PopupHandler.OnPopupSelectionChanged();
        }

        /*
        private void OnPopupMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PopupHandler.OnPopupMouseLeftButtonUp();
        }*/

        private void OnComboBoxLoaded(object sender, RoutedEventArgs e)
        {
            Presentation.ComboBox ctrl = sender as Presentation.ComboBox;
            ToggleButton btn = ctrl.DropDownToggle;
            if (btn != null && !double.IsNaN(btn.ActualWidth) && btn.ActualWidth > 0)
                ctrlDockingSpace.Width = btn.ActualWidth;
        }

        private void OnRequestNavigate(object sender, RoutedEventArgs e)
        {
            NavigateTo(ReleasePageAddress);
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
            PopupHandler.OnPopupClosed(this);
        }

        private void OnIconSharingChanged(object sender, RoutedEventArgs e)
        {
            InvokeAndForget(new Action(OnIconSharingChanged));
        }

        protected override void OnControlClosing(ref bool Cancel)
        {
            App.SetState(this, TaskbarStates.NoProgress);

            base.OnControlClosing(ref Cancel);
        }

        protected override void OnControlClosed()
        {
            if (Dlg != null && !Dlg.IsControlVisible)
                Dlg.ControlClose();

            SaveSettings();

            base.OnControlClosed();
        }
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
