using PgJsonObjects;
using Presentation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
#if CSHARP_XAML_FOR_HTML5
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
#else
using System.Windows;
using System.Windows.Controls.Primitives;
#endif

namespace PgJsonParse
{
    public partial class PrologueWindow : RootControl, INotifyPropertyChanged
    {
        public const double PARSER_VERSION = 332;
        public const int PG_CACHE_VERSION = 332;
        public const int PG_CACHE_SIZE = 42589244;

        #region Init
        public PrologueWindow()
            : base(RootControlMode.CustomShape)
        {
            try
            {
                InitializeComponent();
                DataContext = this;

                CheckOldDlls();
                InitSettings();
                InitStatus();
                InitVersionCheck();
                InitParserCheck();
                InitVersionCache();
                InitParsing();
                InitIcons();

                Loaded += OnLoaded;

                if (CheckLastVersionOnStartup)
                    OnCheckVersion();

                else if (CheckNewParserOnStartup)
                    OnCheckParser(() => { });
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\r\n" + e.StackTrace);
            }
        }

        private void CheckOldDlls()
        {
            string AssemblyLocation = System.Reflection.Assembly.GetCallingAssembly().Location;
            if (AssemblyLocation != null)
            {
                string RootPath = Path.GetDirectoryName(AssemblyLocation);

                if ((File.Exists(Path.Combine(RootPath, "PgJsonObjects.dll")) && !File.Exists(Path.Combine(RootPath, "PgJsonObjects.dll"))) ||
                    (File.Exists(Path.Combine(RootPath, "PgJsonReader.dll")) && !File.Exists(Path.Combine(RootPath, "PgJsonReader.dll"))) ||
                    (File.Exists(Path.Combine(RootPath, "Presentation.dll")) && !File.Exists(Path.Combine(RootPath, "Presentation.dll"))))
                {
                    MessageBox.Show($"Please delete all .dll files in folder '{RootPath}', they are no longer needed.");
                    Close();
                    return;
                }
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
        public string OptimizedVersion { get { return PG_CACHE_VERSION.ToString(); } }

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
                Debug.Assert(IsGlobalInteractionEnabled);

                if (_ShowDeleteButton != value)
                {
                    _ShowDeleteButton = value;
                    NotifyThisPropertyChanged();
                    Persistent.SetSettingBool(nameof(ShowDeleteButton), _ShowDeleteButton);
                }
            }
        }
        private bool _ShowDeleteButton;

        public bool ShareIconFiles
        {
            get { return _ShareIconFiles; }
            set
            {
                Debug.Assert(IsGlobalInteractionEnabled);

                if (_ShareIconFiles != value)
                {
                    _ShareIconFiles = value;
                    NotifyThisPropertyChanged();
                    NotifyPropertyChanged(nameof(CanDeleteSharedIcons));
                    Persistent.SetSettingBool(nameof(ShareIconFiles), _ShareIconFiles);
                }
            }
        }
        private bool _ShareIconFiles;

        public bool CanDeleteSharedIcons
        {
            get { return ShareIconFiles && IsGlobalInteractionEnabled; }
        }

        public bool CheckLastVersionOnStartup
        {
            get { return _CheckLastVersionOnStartup; }
            set
            {
                Debug.Assert(IsGlobalInteractionEnabled);

                if (_CheckLastVersionOnStartup != value)
                {
                    _CheckLastVersionOnStartup = value;
                    NotifyThisPropertyChanged();
                    Persistent.SetSettingBool(nameof(CheckLastVersionOnStartup), _CheckLastVersionOnStartup);
                }
            }
        }
        private bool _CheckLastVersionOnStartup;

        public bool DownloadNewVersionsAutomatically
        {
            get { return _DownloadNewVersionsAutomatically; }
            set
            {
                Debug.Assert(IsGlobalInteractionEnabled);

                if (_DownloadNewVersionsAutomatically != value)
                {
                    _DownloadNewVersionsAutomatically = value;
                    NotifyThisPropertyChanged();
                    Persistent.SetSettingBool(nameof(DownloadNewVersionsAutomatically), _DownloadNewVersionsAutomatically);
                }
            }
        }
        private bool _DownloadNewVersionsAutomatically;

        public int DefaultSelectedVersion
        {
            get { return _DefaultSelectedVersion; }
            private set
            {
                if (_DefaultSelectedVersion != value)
                {
                    _DefaultSelectedVersion = value;
                    NotifyThisPropertyChanged();
                    Persistent.SetSettingInt(nameof(DefaultSelectedVersion), _DefaultSelectedVersion);
                }
            }
        }
        private int _DefaultSelectedVersion;

        public bool KeepRecentVersions
        {
            get { return _KeepRecentVersions; }
            set
            {
                Debug.Assert(IsGlobalInteractionEnabled);

                if (_KeepRecentVersions != value)
                {
                    _KeepRecentVersions = value;
                    NotifyThisPropertyChanged();
                    Persistent.SetSettingBool(nameof(KeepRecentVersions), _KeepRecentVersions);
                }
            }
        }
        private bool _KeepRecentVersions;

        public bool StartAutomatically
        {
            get { return _StartAutomatically; }
            set
            {
                Debug.Assert(IsGlobalInteractionEnabled);

                if (_StartAutomatically != value)
                {
                    _StartAutomatically = value;
                    NotifyThisPropertyChanged();
                    Persistent.SetSettingBool(nameof(StartAutomatically), _StartAutomatically);
                }
            }
        }
        private bool _StartAutomatically;

        public int LastSelectedSetting
        {
            get { return _LastSelectedSetting; }
            set
            {
                if (_LastSelectedSetting != value)
                {
                    _LastSelectedSetting = value;
                    NotifyThisPropertyChanged();
                    Persistent.SetSettingInt(nameof(LastSelectedSetting), _LastSelectedSetting);
                }
            }
        }
        private int _LastSelectedSetting;

        public bool CheckNewParserOnStartup
        {
            get { return _CheckNewParserOnStartup; }
            set
            {
                Debug.Assert(IsGlobalInteractionEnabled);

                if (_CheckNewParserOnStartup != value)
                {
                    _CheckNewParserOnStartup = value;
                    NotifyThisPropertyChanged();
                    Persistent.SetSettingBool(nameof(CheckNewParserOnStartup), _CheckNewParserOnStartup);

                    if (StatusMessage == null && value == true && !IsParserUpdateChecked)
                        OnCheckParser(() => { });
                }
            }
        }
        private bool _CheckNewParserOnStartup;

        public bool OptimizeLoad
        {
            get { return _OptimizeLoad; }
            set
            {
                if (_OptimizeLoad != value)
                {
                    _OptimizeLoad = value;
                    NotifyThisPropertyChanged();
                    Persistent.SetSettingBool(nameof(OptimizeLoad), _OptimizeLoad);
                }
            }
        }
        private bool _OptimizeLoad;

        public MissingIconsAction MissingIconsAction
        {
            get { return _MissingIconsAction; }
            set
            {
                if (_MissingIconsAction != value)
                {
                    Debug.Assert(IsGlobalInteractionEnabled);

                    _MissingIconsAction = value;
                    Persistent.SetSettingInt(nameof(MissingIconsAction), (int)_MissingIconsAction);
                }
            }
        }
        private MissingIconsAction _MissingIconsAction;

        public bool AskToIgnoreMissingIcons
        {
            get { return MissingIconsAction == MissingIconsAction.Ask; }
        }
        public bool DownloadMissingIcons
        {
            get { return MissingIconsAction == MissingIconsAction.Download; }
        }
        public bool IgnoreMissingIcons
        {
            get { return MissingIconsAction == MissingIconsAction.Ignore; }
        }

        private void OnSetAskToIgnoreMissingIcons(object sender, RoutedEventArgs e)
        {
            MissingIconsAction = MissingIconsAction.Ask;
        }

        private void OnSetDownloadMissingIcons(object sender, RoutedEventArgs e)
        {
            MissingIconsAction = MissingIconsAction.Download;
        }

        private void OnSetIgnoreMissingIcons(object sender, RoutedEventArgs e)
        {
            MissingIconsAction = MissingIconsAction.Ignore;
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
            _CheckLastVersionOnStartup = Persistent.GetSettingBool(nameof(CheckLastVersionOnStartup), true);
            _CheckNewParserOnStartup = Persistent.GetSettingBool(nameof(CheckNewParserOnStartup), true);
            _OptimizeLoad = Persistent.GetSettingBool(nameof(OptimizeLoad), true);
            _DownloadNewVersionsAutomatically = Persistent.GetSettingBool(nameof(DownloadNewVersionsAutomatically), false);
            _DefaultSelectedVersion = Persistent.GetSettingInt(nameof(DefaultSelectedVersion), 0);
            _ShareIconFiles = Persistent.GetSettingBool(nameof(ShareIconFiles), true);
            _MissingIconsAction = (MissingIconsAction)Persistent.GetSettingEnum(nameof(MissingIconsAction), (int)MissingIconsAction.Ask, (int)MissingIconsAction.Ignore);
            _KeepRecentVersions = Persistent.GetSettingBool(nameof(KeepRecentVersions), true);
            _ShowDeleteButton = Persistent.GetSettingBool(nameof(ShowDeleteButton), true);
            _StartAutomatically = Persistent.GetSettingBool(nameof(StartAutomatically), false);
            _LastSelectedSetting = Persistent.GetSettingInt(nameof(LastSelectedSetting), 0);
            _LastIconId = Persistent.GetSettingString(nameof(LastIconId), null);
        }

        private string InitFolder(string folderName)
        {
            FileTools.CreateDirectory(folderName);

            return folderName;
        }

        private void SaveSettings()
        {
            if (CachedVersionIndex >= 0 && CachedVersionIndex < VersionList.Count)
                DefaultSelectedVersion = VersionList[CachedVersionIndex].Version;
            else
                DefaultSelectedVersion = 0;

            Persistent.CommitSettings();
        }

        private string LastIconId
        {
            get { return _LastIconId; }
            set
            {
                if (_LastIconId != value)
                {
                    _LastIconId = value;
                    Persistent.SetSettingString(nameof(LastIconId), _LastIconId);
                }
            }
        }
        private string _LastIconId;
        #endregion

        #region Status
        private void InitStatus()
        {
            _StatusMessage = null;
            _LastExceptionMessage = null;
            _IsGlobalInteractionEnabled = true;
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
            _VersionCheckState = CheckLastVersionOnStartup ? VersionCheckState.Checking : VersionCheckState.Unchecked;
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

        private void OnCheckVersion()
        {
            IsGlobalInteractionEnabled = false;
            VersionCheckState = VersionCheckState.Checking;

            OnCheckVersion0();
        }

        private void OnCheckVersion0()
        {
            Stopwatch Watch = new Stopwatch();
            Watch.Start();

            const string RequestUri = "http://client.projectgorgon.com/fileversion.txt";
            WebClientTool.DownloadText(this, RequestUri, Watch,
                                       (string content, Exception downloadException) => OnCheckVersion1(content, downloadException, RequestUri));
        }

        private void OnCheckVersion1(string content, Exception downloadException, string requestUri)
        {
            if (downloadException == null)
            {
                if (content != null && int.TryParse(content, out int Version) && Version > 0)
                {
                    StatusMessage = null;
                    LastExceptionMessage = null;
                    OnCheckVersion2(Version);
                    return;
                }
                else
                    LastExceptionMessage = requestUri + " is invalid.";
            }
            else
                LastExceptionMessage = downloadException.Message;

            StatusMessage = "Unable to connect to the game server.";

            VersionCheckState = VersionCheckState.CheckFailed;
            IsGlobalInteractionEnabled = true;
        }

        private void OnCheckVersion2(int version)
        {
            LatestVersion = version.ToString();
            VersionCheckState = VersionCheckState.Known;

            int i;
            for (i = 0; i < VersionList.Count; i++)
                if (VersionList[i].Version == version)
                {
                    if (CachedVersionIndex < 0)
                        CachedVersionIndex = i;
                    break;
                }

            if (i >= VersionList.Count)
            {
                GameVersionInfo NewVersion = AddCheckedVersion(version);

                if (CheckNewParserOnStartup)
                    OnCheckParser(() => OnCheckVersion3(NewVersion));
                else
                    OnCheckVersion3(NewVersion);
            }
            else
            {
                if (CheckNewParserOnStartup)
                    OnCheckParser(() => OnCheckVersion4());
                else
                    OnCheckVersion4();
            }
        }

        private void OnCheckVersion3(GameVersionInfo newVersion)
        {
            if (DownloadNewVersionsAutomatically)
            {
                CachedVersionIndex = VersionList.IndexOf(newVersion);
                OnDownloadVersion(newVersion);
            }
            else
                IsGlobalInteractionEnabled = true;
        }

        private void OnCheckVersion4()
        {
            GameVersionInfo VersionInfo = SelectedVersion;

            if (StartAutomatically)
                OnStart(VersionInfo);
            else
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
            CachedVersionIndex = -1;
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
            OnCheckParser(() => { });
        }

        private void OnCheckParser(Action callback)
        {
            OnCheckParser0(callback);
        }

        private void OnCheckParser0(Action callback)
        {
            IsParserUpdateChecked = true;

            NetTools.EnableSecurityProtocol(out object OldSecurityProtocol);
            WebClientTool.DownloadText(this, ReleasePageAddress, null,
                                       (string Content, Exception DownloadException) => OnCheckParser1(Content, DownloadException, callback, OldSecurityProtocol));
        }

        private void OnCheckParser1(string content, Exception downloadException, Action callback, object oldSecurityProtocol)
        {
            NetTools.RestoreSecurityProtocol(oldSecurityProtocol);

            if (downloadException == null)
            {
                bool FoundUpdate = false;

                if (content != null)
                {
                    const string Pattern = @"<a href=""/dlebansais/PgJsonParse/releases/tag/";
                    int Index = content.IndexOf(Pattern);
                    if (Index >= 0)
                    {
                        string ParserTagVersion = content.Substring(Index + Pattern.Length, 20);
                        int EndIndex = ParserTagVersion.IndexOf('"');
                        if (EndIndex > 0)
                        {
                            ParserTagVersion = ParserTagVersion.Substring(0, EndIndex);
                            if (ParserTagVersion.ToLower().StartsWith("v"))
                                ParserTagVersion = ParserTagVersion.Substring(1);

                            if (InvariantCulture.TryParseDouble(ParserTagVersion, out double ReleasedParserVersion) && ReleasedParserVersion > PARSER_VERSION)
                                FoundUpdate = true;
                        }
                    }
                }

                IsNewParserAvailable = FoundUpdate;
            }

            callback();
        }

        private bool IsParserUpdateChecked;
        #endregion

        #region Version Cache
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
                    NotifyPropertyChanged(nameof(SelectedVersionIsOptimized));
                }
            }
        }
        public bool SelectedVersionIsOptimized
        {
            get { return SelectedVersion != null && SelectedVersion.Version == PG_CACHE_VERSION; }
        }
        private int _CachedVersionIndex;

        private void UpdateCachedVersion()
        {
            VersionList.Clear();

            try
            {
                GameVersionInfo DefaultVersion = null;

                foreach (string SubFolder in FileTools.DirectoryFolders(VersionCacheFolder))
                {
                    string FolderName = Path.GetFileName(SubFolder);

                    int Version;
                    if (int.TryParse(FolderName, out Version) && Version > 0)
                    {
                        DownloadState FileDownloadState = DownloadState.Downloaded;
                        foreach (KeyValuePair<Type, IObjectDefinition> Entry in ObjectList.Definitions)
                            if (Entry.Value.MinVersion == 0 || Entry.Value.MinVersion <= Version)
                                if (!FileTools.FileExists(Path.Combine(SubFolder, Entry.Value.JsonFileName + ".json")))
                                {
                                    FileDownloadState = DownloadState.NotDownloaded;
                                    break;
                                }

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
            catch (Exception e)
            {
                StatusMessage = "Error looking for downloaded files.";
                LastExceptionMessage = e.Message;
                CachedVersionIndex = -1;
            }
        }

        private void OnDownloadVersion(GameVersionInfo versionInfo)
        {
            IsGlobalInteractionEnabled = false;
            SetTaskbarState(TaskbarStates.Normal);
            versionInfo.ProgressChanged += OnFileDownloadProgressChanged;

            OnDownloadVersion0(versionInfo);
        }

        private void OnDownloadVersion0(GameVersionInfo versionInfo)
        {
            string VersionFolder = InitFolder(Path.Combine(VersionCacheFolder, versionInfo.Version.ToString()));

            List<string> JsonFileList = new List<string>();
            foreach (KeyValuePair<Type, IObjectDefinition> Entry in ObjectList.Definitions)
                if (Entry.Value.MinVersion <= versionInfo.Version)
                    JsonFileList.Add(Entry.Value.JsonFileName);

            versionInfo.DownloadFiles(this, VersionFolder, JsonFileList,
                                      (bool success, string exceptionMessage) => OnDownloadVersion1(success, exceptionMessage, versionInfo));
        }

        private void OnDownloadVersion1(bool success, string exceptionMessage, GameVersionInfo versionInfo)
        {
            versionInfo.ProgressChanged -= OnFileDownloadProgressChanged;

            if (success)
            {
                SetTaskbarState(TaskbarStates.NoProgress);
                StatusMessage = null;
                LastExceptionMessage = null;

                if (StartAutomatically)
                    OnStart(versionInfo);
                else
                    IsGlobalInteractionEnabled = true;
            }
            else
            {
                SetTaskbarState(TaskbarStates.Error);
                StatusMessage = "Unable to download version files.";
                LastExceptionMessage = exceptionMessage;
                IsGlobalInteractionEnabled = true;
            }
        }

        private void OnCancelDownloadVersion(GameVersionInfo versionInfo)
        {
            versionInfo.CancelDownloadFiles();
        }

        private void OnDeleteVersion(GameVersionInfo versionInfo)
        {
            int OldCachedVersionIndex = CachedVersionIndex;

            if (VersionList.Count == 1)
                if (Confirmation.Show("This will delete the last downloaded version.", "Delete", true, ConfirmationType.Warning) != MessageBoxResult.OK)
                    return;

            try
            {
                string VersionFolder = Path.Combine(VersionCacheFolder, versionInfo.Version.ToString());
                FileTools.DeleteDirectory(VersionFolder);

                VersionList.Remove(versionInfo);
                if (OldCachedVersionIndex < VersionList.Count)
                    CachedVersionIndex = OldCachedVersionIndex;
                else if (VersionList.Count > 0)
                    CachedVersionIndex = VersionList.Count - 1;
                else
                {
                    CachedVersionIndex = -1;
                    if (CheckLastVersionOnStartup)
                        StartTask(OnCheckVersion);
                }
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
                    NotifyPropertyChanged(nameof(SelectedVersionIsOptimized));
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
                    NotifyPropertyChanged(nameof(SelectedVersionIsOptimized));
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
/*
        public SelectionWindow Dlg
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
        private SelectionWindow _Dlg;*/

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

        private void OnStart(GameVersionInfo versionInfo)
        {
            OnStart0(versionInfo);
        }

        private void OnStart0(GameVersionInfo versionInfo)
        {
            IsGlobalInteractionEnabled = false;

            FrameworkElement RootContent = Content as FrameworkElement;
            //Dlg = new SelectionWindow(RootContent.ActualWidth, RootContent.ActualHeight);
            Dlg = new MainWindow();
            IsParsing = true;
            IsParsingCancelable = true;
            ParseCancellation = new Cancellation();
            ParseProgress = 0;
            ParseErrorInfo ErrorInfo = new ParseErrorInfo();
            SetTaskbarState(TaskbarStates.Normal);
            App.SetMainWindow(Dlg);

            string VersionFolder = Path.Combine(VersionCacheFolder, versionInfo.Version.ToString());
            string IconFolder = ShareIconFiles ? IconCacheFolder : VersionFolder;

            if (!ObjectDefinition.UseJson && versionInfo.Version == PG_CACHE_VERSION && OptimizeLoad)
                if (LoadCachedData(versionInfo, ErrorInfo, VersionFolder, IconFolder))
                    return;

            OnStart1(false, versionInfo, ErrorInfo, VersionFolder, IconFolder);
        }

        private void OnStart1(bool success, GameVersionInfo versionInfo, ParseErrorInfo errorInfo, string versionFolder, string iconFolder)
        {
            if (success)
                OnStart3(true, versionInfo, errorInfo, versionFolder, iconFolder);

            else if (!ParseCancellation.IsCanceled)
            {
                List<KeyValuePair<Type, IObjectDefinition>> EntryList = new List<KeyValuePair<Type, IObjectDefinition>>();
                foreach (KeyValuePair<Type, IObjectDefinition> Entry in ObjectList.Definitions)
                    EntryList.Add(Entry);

                IconTable.Clear();
                int ProgressIndex = 0;

                StartTask(() => ExecuteParse0(versionInfo, errorInfo, versionFolder, EntryList, ProgressIndex),
                          (bool nextStepSuccess) => ExecuteParse1(nextStepSuccess, versionInfo, errorInfo, versionFolder, iconFolder, EntryList, ProgressIndex));
            }
            else
                OnStart3(false, versionInfo, errorInfo, versionFolder, iconFolder);
        }

        public bool LoadCachedData(GameVersionInfo versionInfo, ParseErrorInfo errorInfo, string versionFolder, string iconFolder)
        {
            try
            {
                string CacheFileName = Path.Combine(versionFolder, "cache.pg");
                if (FileTools.FileExists(CacheFileName))
                {
                    byte[] Data = FileTools.LoadBinaryFile(CacheFileName);
                    if (Data.Length == PG_CACHE_SIZE)
                    {
                        IconTable.Clear();
                        errorInfo.IgnoreUnparsedEnums = true;
                        DeserializeAll(versionInfo, errorInfo, versionFolder, iconFolder, Data, 
                                       (bool success, byte[] data) => OnStart1(success, versionInfo, errorInfo, versionFolder, iconFolder));
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return false;
        }

        public bool ExecuteParse0(GameVersionInfo versionInfo, ParseErrorInfo errorInfo, string versionFolder, List<KeyValuePair<Type, IObjectDefinition>> entryList, int progressIndex)
        {
            KeyValuePair<Type, IObjectDefinition> Entry = entryList[progressIndex];

            if (Entry.Value.MinVersion <= versionInfo.Version)
            {
                if (!LoadNextFile(Entry.Value, versionFolder, errorInfo))
                    return false;

                if (ParseCancellation.IsCanceled)
                    return false;
            }

            return true;
        }

        public void ExecuteParse1(bool success, GameVersionInfo versionInfo, ParseErrorInfo errorInfo, string versionFolder, string iconFolder, List<KeyValuePair<Type, IObjectDefinition>> entryList, int progressIndex)
        {
            if (success)
            {
                progressIndex++;

                ParseProgress = (progressIndex * 100.0) / ObjectList.Definitions.Count;
                SetTaskbarProgressValue(ParseProgress, 100.0);

                if (progressIndex < entryList.Count)
                {
                    StartTask(() => ExecuteParse0(versionInfo, errorInfo, versionFolder, entryList, progressIndex),
                                (bool nextStepSuccess) => ExecuteParse1(nextStepSuccess, versionInfo, errorInfo, versionFolder, iconFolder, entryList, progressIndex));
                    return;
                }
            }

            OnStart2(success, versionInfo, errorInfo, versionFolder, iconFolder);
        }

        private void OnStart2(bool success, GameVersionInfo versionInfo, ParseErrorInfo errorInfo, string versionFolder, string iconFolder)
        {
            if (success)
                success = ConnectTables(versionFolder, iconFolder, errorInfo);

            if (success)
                if (!ObjectDefinition.UseJson && versionInfo.Version == PG_CACHE_VERSION)
                {
                    int offset;

                    offset = 0;
                    SerializeAll(null, ref offset);

                    int length = offset;
                    byte[] Data = new byte[length];

                    offset = 0;
                    SerializeAll(Data, ref offset);

                    DeserializeAll(versionInfo, errorInfo, versionFolder, iconFolder, Data,
                                   (bool lastStepSuccess, byte[] data) => CompleteDeserializeCommit(lastStepSuccess, versionInfo, errorInfo, versionFolder, iconFolder, data));
                    return;
                }

            OnStart3(success, versionInfo, errorInfo, versionFolder, iconFolder);
        }

        private void OnStart3(bool success, GameVersionInfo versionInfo, ParseErrorInfo errorInfo, string versionFolder, string iconFolder)
        {
            SetTaskbarState(TaskbarStates.NoProgress);

            IsParsing = false;
            IsParsingCancelable = false;
            ParseCancellation = null;

            if (!success)
            {
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

            if (MissingIconList.Count > 0)
            {
                if (ShareIconFiles)
                {
                    foreach (GameVersionInfo Item in VersionList)
                        Item.SetIconsDownloaded(false);
                }
                else
                    versionInfo.SetIconsDownloaded(false);

                LastIconId = "icon_" + MissingIconList[MissingIconList.Count - 1];

                switch (MissingIconsAction)
                {
                    default:
                    case MissingIconsAction.Ask:
                        if (Confirmation.Show("There are " + MissingIconList.Count + " icon(s) not downloaded yet, would you like to get them now?", "Starting", true, ConfirmationType.Info) == MessageBoxResult.OK)
                        {
                            FollowStartWithDownloadIcons(versionInfo);
                            return;
                        }
                        break;

                    case MissingIconsAction.Download:
                        FollowStartWithDownloadIcons(versionInfo);
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
                        if (Item.Version < versionInfo.Version)
                            ToRemove.Add(Item);

                    foreach (GameVersionInfo Item in ToRemove)
                    {
                        string VersionFolder = Path.Combine(VersionCacheFolder, Item.Version.ToString());
                        FileTools.DeleteDirectory(VersionFolder);

                        VersionList.Remove(Item);
                    }

                    CachedVersionIndex = VersionList.IndexOf(versionInfo);
                }
                catch (Exception e)
                {
                    StatusMessage = "Error deleting files.";
                    LastExceptionMessage = e.Message;
                }
            }

            string Warnings = errorInfo.GetWarnings();
            if (Warnings.Length > 0)
            {
                Debug.WriteLine(Warnings);
                Dlg.WarningText = Warnings;
            }
            else
                Dlg.WarningText = "Files loaded and parsed, no warnings.";

            foreach (KeyValuePair<Type, IObjectDefinition> Entry in ObjectList.Definitions)
            {
                IObjectDefinition definition = Entry.Value;
                IVerifyer FileVerifyer = definition.FileVerifyer;

                string FilePath = Path.Combine(versionFolder, definition.JsonFileName + ".json");

                if (ObjectDefinition.Verify && !FileVerifyer.Verify(FilePath, definition.VerifedObjectList, definition.LoadAsArray, definition.LoadAsObject, definition.UseJavaFormat))
                    break;
            }

            Dlg.LoadedVersion = versionInfo;
            Dlg.ApplicationFolder = ApplicationFolder;
            Dlg.IconCacheFolder = IconCacheFolder;
            Dlg.CurrentVersionCacheFolder = InitFolder(Path.Combine(VersionCacheFolder, versionInfo.Version.ToString()));
            Dlg.IconFile = Path.Combine(ApplicationFolder, "mainicon.png");
            Dlg.FavorIconFile = Path.Combine(ApplicationFolder, "favoricon.png");

            SwitchTo(Dlg, () => Dlg.StartApplication());

            IsGlobalInteractionEnabled = true;
        }

        private void SerializeAll(byte[] data, ref int offset)
        {
            SerializableJsonObject.ResetSerializedObjectTable();

            foreach (KeyValuePair<Type, IObjectDefinition> Entry in ObjectList.Definitions)
            {
                IObjectDefinition definition = Entry.Value;
                IMainJsonObjectCollection JsonObjectList = definition.JsonObjectList;
                int Count = JsonObjectList.Count;

                if (data != null)
                {
                    byte[] valueData = BitConverter.GetBytes(Count);
                    Array.Copy(valueData, 0, data, offset, 4);

                    //Debug.WriteLine(Count.ToString());
                }

                offset += 4;

                int ObjectOffset = offset;
                offset += (Count + 1) * 4;

                for (int i = 0; i < Count; i++)
                {
                    IMainJsonObject Item = JsonObjectList[i] as IMainJsonObject;

                    Item.SerializeJsonMainObject(data, ref offset, ObjectOffset);
                    ObjectOffset += 4;
                }

                if (data != null)
                {
                    byte[] valueData = BitConverter.GetBytes(offset);
                    Array.Copy(valueData, 0, data, ObjectOffset, 4);
                }
            }
        }

        private void DeserializeAll(GameVersionInfo versionInfo, ParseErrorInfo errorInfo, string versionFolder, string iconFolder, byte[] data, Action<bool, byte[]> callback)
        {
            SerializableJsonObject.ResetSerializedObjectTable();
            GenericPgObject.ResetCreatedObjectTable();
            byte[] CurrentOffset = new byte[4];
            int progressIndex = 0;

            List<IObjectDefinition> DefinitionList = new List<IObjectDefinition>();
            foreach (KeyValuePair<Type, IObjectDefinition> Entry in ObjectList.Definitions)
                DefinitionList.Add(Entry.Value);

            StartTask(() => DeserializeAll0(data, CurrentOffset, DefinitionList, progressIndex),
                      (bool nextStepSuccess) => DeserializeAll1(nextStepSuccess, versionInfo, errorInfo, versionFolder, iconFolder, data, callback, CurrentOffset, DefinitionList, progressIndex));
        }

        private bool DeserializeAll0(byte[] data, byte[] currentOffset, List<IObjectDefinition> definitionList, int progressIndex)
        {
            if (ParseCancellation.IsCanceled)
                return false;

            try
            {
                IObjectDefinition definition = definitionList[progressIndex];
                int Offset = BitConverter.ToInt32(currentOffset, 0);

                definition.JsonObjectList.Clear();

                IMainPgObjectCollection PgObjectList = definition.PgObjectList;
                PgObjectList.Clear();

                int Count = BitConverter.ToInt32(data, Offset);
                Offset += 4;

                int ObjectOffset = Offset;
                IMainPgObject Item;

                for (int i = 0; i < Count; i++)
                {
                    Offset = BitConverter.ToInt32(data, ObjectOffset + i * 4);

                    Item = GenericPgObject.CreateMainObject(definition.CreateNewObject, data, ref Offset);
                    PgObjectList.Add(Item);
                }

                Offset = BitConverter.ToInt32(data, ObjectOffset + Count * 4);
                Array.Copy(BitConverter.GetBytes(Offset), 0, currentOffset, 0, 4);

                ParseProgress = (progressIndex * 100.0) / ObjectList.Definitions.Count;
                SetTaskbarProgressValue(ParseProgress, 100.0);

                return true;
            }
            catch (Exception e)
            {
                StatusMessage = "Fast load error.";
                LastExceptionMessage = e.Message;
                return false;
            }
        }

        private void DeserializeAll1(bool success, GameVersionInfo versionInfo, ParseErrorInfo errorInfo, string versionFolder, string iconFolder, byte[] data, Action<bool, byte[]> callback, byte[] currentOffset, List<IObjectDefinition> definitionList, int progressIndex)
        {
            if (success)
            {
                if (++progressIndex < definitionList.Count)
                    StartTask(() => DeserializeAll0(data, currentOffset, definitionList, progressIndex),
                                (bool nextStepSuccess) => DeserializeAll1(nextStepSuccess, versionInfo, errorInfo, versionFolder, iconFolder, data, callback, currentOffset, definitionList, progressIndex));
                else
                    DeserializeAll2(versionInfo, errorInfo, versionFolder, iconFolder, data, callback);
            }
            else
                callback(false, data);
        }

        private void DeserializeAll2(GameVersionInfo versionInfo, ParseErrorInfo errorInfo, string versionFolder, string iconFolder, byte[] data, Action<bool, byte[]> callback)
        {
            foreach (KeyValuePair<Type, IObjectDefinition> Entry in ObjectList.Definitions)
            {
                IObjectDefinition definition = Entry.Value;
                IMainPgObjectCollection PgObjectList = definition.PgObjectList;

                foreach (IGenericPgObject Item in PgObjectList)
                    if (Item is IBackLinkable AsLinkBack)
                        AsLinkBack.SortLinkBack();
            }

            foreach (KeyValuePair<Type, IObjectDefinition> Entry in ObjectList.Definitions)
            {
                IObjectDefinition Definition = Entry.Value;
                Dictionary<string, IJsonKey> ObjectTable = Definition.ObjectTable;
                IMainPgObjectCollection PgObjectList = Definition.PgObjectList;

                if (ObjectTable.Count == 0)
                    foreach (IJsonKey Item in PgObjectList)
                        ObjectTable.Add(Item.Key, Item);
            }

            Dictionary<string, IJsonKey> NpcTable = ObjectList.Definitions[typeof(GameNpc)].ObjectTable;
            Dictionary<string, IJsonKey> ItemTable = ObjectList.Definitions[typeof(Item)].ObjectTable;
            Dictionary<string, IJsonKey> PowerTable = ObjectList.Definitions[typeof(Power)].ObjectTable;
            Dictionary<string, IJsonKey> AttributeTable = ObjectList.Definitions[typeof(PgJsonObjects.Attribute)].ObjectTable;

            foreach (KeyValuePair<string, IJsonKey> Entry in NpcTable)
            {
                IPgGameNpc Npc = Entry.Value as IPgGameNpc;
                foreach (IPgNpcPreference NpcPreference in Npc.PreferenceList)
                    NpcPreference.InitFavorList(ItemTable);
            }

            foreach (KeyValuePair<string, IJsonKey> Entry in PowerTable)
            {
                IPgPower Power = Entry.Value as IPgPower;
                Power.InitTierList(AttributeTable);
            }

            callback(true, data);
        }

        private void CompleteDeserializeCommit(bool succcess, GameVersionInfo versionInfo, ParseErrorInfo errorInfo, string versionFolder, string iconFolder, byte[] data)
        {
            if (succcess)
                if (OptimizeLoad)
                {
                    string CacheFileName = Path.Combine(versionFolder, "cache.pg");
                    FileTools.CommitBinaryFile(CacheFileName, data);
                }

            OnStart3(succcess, versionInfo, errorInfo, versionFolder, iconFolder);
        }

        private bool LoadNextFile(IObjectDefinition definition, string versionFolder, ParseErrorInfo errorInfo)
        {
            try
            {
                string FilePath = Path.Combine(versionFolder, definition.JsonFileName + ".json");

                IParser FileParser = definition.FileParser;
                IMainJsonObjectCollection ObjectList = definition.JsonObjectList;
                Dictionary<string, IJsonKey> ObjectTable = definition.ObjectTable;
                if (!FileParser.LoadRaw(FilePath, ObjectList, definition.LoadAsArray, definition.LoadAsObject, errorInfo))
                    return false;

                ObjectTable.Clear();
                foreach (IJsonKey Item in ObjectList)
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

        private bool ConnectTables(string versionFolder, string iconFolder, ParseErrorInfo errorInfo)
        {
            Dictionary<Type, IMainJsonObjectCollection> AllLists = new Dictionary<Type, IMainJsonObjectCollection>();
            Dictionary<Type, Dictionary<string, IJsonKey>> AllTables = new Dictionary<Type, Dictionary<string, IJsonKey>>();
            foreach (KeyValuePair<Type, IObjectDefinition> Entry in ObjectList.Definitions)
            {
                IObjectDefinition definition = Entry.Value;
                AllLists.Add(Entry.Key, definition.JsonObjectList);
                AllTables.Add(Entry.Key, definition.ObjectTable);
            }

            foreach (KeyValuePair<Type, IObjectDefinition> Entry in ObjectList.Definitions)
            {
                IObjectDefinition definition = Entry.Value;
                foreach (IGenericJsonObject Item in definition.JsonObjectList)
                    Item.Connect(errorInfo, null, AllTables);

                if (ParseCancellation.IsCanceled)
                    return false;
            }

            IObjectDefinition RecipeDefinition = ObjectList.Definitions[typeof(Recipe)];
            bool Continue;
            do
            {
                Continue = false;
                foreach (Recipe Item in RecipeDefinition.JsonObjectList)
                    Item.MeasurePerfectCottonRatio(ref Continue);
            }
            while (Continue);

            foreach (KeyValuePair<Type, IObjectDefinition> Entry in ObjectList.Definitions)
            {
                IObjectDefinition definition = Entry.Value;
                foreach (IGenericJsonObject Item in definition.JsonObjectList)
                    Item.SetIndirectProperties(AllTables, errorInfo);
            }

            if (ParseCancellation.IsCanceled)
                return false;

            return CreateIndexes(versionFolder, iconFolder, errorInfo);
        }

        private bool CreateIndexes(string versionFolder, string iconFolder, ParseErrorInfo errorInfo)
        {
            foreach (KeyValuePair<Type, IObjectDefinition> Entry in ObjectList.Definitions)
            {
                if (!CreateNextIndex(Entry.Value, versionFolder, errorInfo))
                    return false;

                if (ParseCancellation.IsCanceled)
                    return false;
            }

            IsParsingCancelable = false;
            CreateMushroomIndex(errorInfo);

            return EnumerateMissingIcons(iconFolder, errorInfo);
        }

        private bool CreateNextIndex(IObjectDefinition definition, string versionFolder, ParseErrorInfo errorInfo)
        {
            try
            {
                string IndexFilePath = Path.Combine(versionFolder, definition.JsonFileName + "-index.txt");
                IParser FileParser = definition.FileParser;
                Dictionary<string, IJsonKey> ObjectTable = definition.ObjectTable;
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

        private void CreateMushroomIndex(ParseErrorInfo errorInfo)
        {
            string MushroomNameFile = Path.Combine(ApplicationFolder, "Mushrooms.txt");

            List<string> MushroomNameList = new List<string>();
            IObjectDefinition ItemDefinition = ObjectList.Definitions[typeof(Item)];
            Dictionary<string, IJsonKey> ItemTable = ItemDefinition.ObjectTable;

            foreach (KeyValuePair<string, IJsonKey> Entry in ItemTable)
            {
                Item Item = Entry.Value as Item;
                if (Item.KeywordTable.ContainsKey(ItemKeyword.RawMushroom))
                    MushroomNameList.Add(Item.Name);
            }

            try
            {
                string Content = "";
                foreach (string MushroomName in MushroomNameList)
                {
                    string Line = MushroomName + InvariantCulture.NewLine;
                    Content += Line;
                }

                FileTools.CommitTextFile(MushroomNameFile, Content);
            }
            catch (Exception e)
            {
                StatusMessage = "Mushroom index file creation error.";
                LastExceptionMessage = e.Message;
            }
        }

        private bool EnumerateMissingIcons(string iconFolder, ParseErrorInfo errorInfo)
        {
            foreach (int IconId in errorInfo.IconList)
            {
                string FilePath = Path.Combine(iconFolder, "icon_" + IconId + ".png");
                IconTable.Add(IconId, FileTools.FileExists(FilePath));
            }

            return true;
        }

        private void CancelParse()
        {
            ParseCancellation.Cancel();
        }

        private void FollowStartWithDownloadIcons(GameVersionInfo versionInfo)
        {
            Dlg.ControlClose();
            Dlg = null;

            OnDownloadIcons(versionInfo);
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

        private bool IsLastIconLoadedForVersion(int version)
        {
            if (LastIconId == null)
                return false;

            string IconFolder = ShareIconFiles ? IconCacheFolder : Path.Combine(VersionCacheFolder, version.ToString());
            string LastIconFile = Path.Combine(IconFolder, LastIconId + ".png");
            bool FileExists = FileTools.FileExists(LastIconFile);

            return FileExists;
        }

        private void OnDownloadIcons(GameVersionInfo versionInfo)
        {
            IsGlobalInteractionEnabled = false;
            versionInfo.ProgressChanged += OnIconDownloadProgressChanged;

            OnDownloadIcons0(versionInfo);
        }

        private void OnDownloadIcons0(GameVersionInfo versionInfo)
        {
            IsIconStateUpdated = true;
            SetTaskbarState(TaskbarStates.Normal);

            ExecuteDownloadIcons0(versionInfo, 
                                  (bool Success) => OnDownloadIcons1(Success, versionInfo));
        }

        private void OnDownloadIcons1(bool success, GameVersionInfo versionInfo)
        {
            versionInfo.ProgressChanged -= OnIconDownloadProgressChanged;

            if (success)
            {
                SetTaskbarState(TaskbarStates.NoProgress);

                string IconFolder = ShareIconFiles ? IconCacheFolder : Path.Combine(VersionCacheFolder, versionInfo.Version.ToString());
                string IconFile = Path.Combine(ApplicationFolder, "mainicon.png");
                string SourceIconFile = Path.Combine(IconFolder, "icon_5624.png");
                string FavorIconFile = Path.Combine(ApplicationFolder, "favoricon.png");
                string SourceFavorIconFile = Path.Combine(IconFolder, "icon_102.png");

                FileTools.CopyFile(SourceIconFile, IconFile);
                FileTools.CopyFile(SourceFavorIconFile, FavorIconFile);

                if (StartAutomatically)
                {
                    OnStart(versionInfo);
                    UpdateWindowIcon();
                    return;
                }
            }
            else
            {
                SetTaskbarState(TaskbarStates.Error);
                StatusMessage = "Failed to download icons.";
            }

            UpdateWindowIcon();

            IsGlobalInteractionEnabled = true;
        }

        private void ExecuteDownloadIcons0(GameVersionInfo versionInfo, Action<bool> callback)
        {
            string DestinationFolder = ShareIconFiles ? IconCacheFolder : Path.Combine(VersionCacheFolder, versionInfo.Version.ToString());
            versionInfo.DownloadIcons(this, LoadedIconCount, MissingIconList, DestinationFolder, 
                                      (bool Success, string ExceptionMessage, int NewLoadedIconCount) => ExecuteDownloadIcons1(Success, ExceptionMessage, NewLoadedIconCount, callback));
        }

        private void ExecuteDownloadIcons1(bool success, string exceptionMessage, int newLoadedIconCount, Action<bool> callback)
        {
            LastExceptionMessage = exceptionMessage;
            LoadedIconCount = newLoadedIconCount;

            callback(success);
        }

        private void OnCancelDownloadIcons(GameVersionInfo versionInfo)
        {
            versionInfo.CancelDownloadIcons();
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
                FileTools.DeleteDirectory(IconCacheFolder);
            }
            catch (Exception e)
            {
                StatusMessage = "Error deleting files.";
                LastExceptionMessage = e.Message;
            }

            LoadedIconCount = 0;
            LastIconId = null;
            IsIconStateUpdated = false;

            OnIconSharingChanged();
        }

        private int LoadedIconCount;
        private List<int> MissingIconList;
        private Dictionary<int, bool> IconTable;
        #endregion

        #region Events
        private void OnCheckVersion(object sender, EventArgs e)
        {
            SetTaskbarState(TaskbarStates.NoProgress);
            OnCheckVersion();
        }

        private void OnSelectVersion(object sender, EventArgs e)
        {
            SetTaskbarState(TaskbarStates.NoProgress);
            OnSelectVersion();
        }

        private GameVersionInfo VersionInfoFromControl(EventArgs e)
        {
            GameVersionInfo VersionInfo = (e as ExecutedEventArgs).Parameter as GameVersionInfo;
            return VersionInfo;
        }

        private void OnDownloadVersion(object sender, EventArgs e)
        {
            SetTaskbarState(TaskbarStates.NoProgress);
            GameVersionInfo Version = VersionInfoFromControl(e);
            OnDownloadVersion(Version);
        }

        private void OnCancelDownloadVersion(object sender, EventArgs e)
        {
            OnCancelDownloadVersion(VersionInfoFromControl(e));
        }

        private void OnDeleteVersion(object sender, EventArgs e)
        {
            SetTaskbarState(TaskbarStates.NoProgress);
            OnDeleteVersion(VersionInfoFromControl(e));
        }

        private void OnDeleteIcons(object sender, EventArgs e)
        {
            SetTaskbarState(TaskbarStates.NoProgress);
            OnDeleteIcons();
        }

        private void OnDownloadIcons(object sender, EventArgs e)
        {
            SetTaskbarState(TaskbarStates.NoProgress);
            OnDownloadIcons(VersionInfoFromControl(e));
        }

        private void OnCancelDownloadIcons(object sender, EventArgs e)
        {
            OnCancelDownloadIcons(VersionInfoFromControl(e));
        }

        private void OnStart(object sender, EventArgs e)
        {
            SetTaskbarState(TaskbarStates.NoProgress);
            OnStart(VersionInfoFromControl(e));
        }

        private void OnCancelStart(object sender, EventArgs e)
        {
            CancelParse();
        }

        private void OnComboBoxLoaded(object sender, RoutedEventArgs e)
        {
            Presentation.ComboBox ctrl = sender as Presentation.ComboBox;
            ToggleButton btn = ctrl.DropDownToggle;
            if (btn != null && !double.IsNaN(btn.ActualWidth) && btn.ActualWidth > 0)
            {
                FrameworkElement ctrlAnchor1;
                if ((ctrlAnchor1 = FindName("ctrlAnchor1") as FrameworkElement) != null)
                    ctrlAnchor1.Width = btn.ActualWidth;
                FrameworkElement ctrlAnchor2;
                if ((ctrlAnchor2 = FindName("ctrlAnchor2") as FrameworkElement) != null)
                    ctrlAnchor2.Width = btn.ActualWidth;
            }
        }

        private void OnRequestNavigate(object sender, RoutedEventArgs e)
        {
            NavigateTo(ReleasePageAddress);
        }

        private void OnFileDownloadProgressChanged(object sender, EventArgs e)
        {
            GameVersionInfo VersionInfo = sender as GameVersionInfo;
            SetTaskbarProgressValue(VersionInfo.FileDownloadProgress, 100.0);
        }

        private void OnIconDownloadProgressChanged(object sender, EventArgs e)
        {
            GameVersionInfo VersionInfo = sender as GameVersionInfo;
            SetTaskbarProgressValue(VersionInfo.IconDownloadProgress, 100.0);
        }

        private void OnIconSharingChanged(object sender, RoutedEventArgs e)
        {
            OnIconSharingChanged();
        }

        protected override void OnControlClosing(ref bool cancel)
        {
            SetTaskbarState(TaskbarStates.NoProgress);

            base.OnControlClosing(ref cancel);
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
