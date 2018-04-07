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
using System.Runtime.CompilerServices;
using System.Text;
using Tools;
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
                                       (string Content, Exception DownloadException) => OnCheckVersion1(Content, DownloadException, RequestUri));
        }

        private void OnCheckVersion1(string Content, Exception DownloadException, string RequestUri)
        {
            if (DownloadException == null)
            {
                if (Content != null && int.TryParse(Content, out int Version) && Version > 0)
                {
                    StatusMessage = null;
                    LastExceptionMessage = null;
                    OnCheckVersion2(Version);
                    return;
                }
                else
                    LastExceptionMessage = RequestUri + " is invalid.";
            }
            else
                LastExceptionMessage = DownloadException.Message;

            StatusMessage = "Unable to connect to the game server.";

            VersionCheckState = VersionCheckState.CheckFailed;
            IsGlobalInteractionEnabled = true;
        }

        private void OnCheckVersion2(int Version)
        {
            LatestVersion = Version.ToString();
            VersionCheckState = VersionCheckState.Known;

            int i;
            for (i = 0; i < VersionList.Count; i++)
                if (VersionList[i].Version == Version)
                {
                    if (CachedVersionIndex < 0)
                        CachedVersionIndex = i;
                    break;
                }

            if (i >= VersionList.Count)
            {
                GameVersionInfo NewVersion = AddCheckedVersion(Version);

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

        private void OnCheckVersion3(GameVersionInfo NewVersion)
        {
            if (DownloadNewVersionsAutomatically)
            {
                CachedVersionIndex = VersionList.IndexOf(NewVersion);
                OnDownloadVersion(NewVersion);
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
        public const double PARSER_VERSION = 301;

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

        private void OnCheckParser1(string Content, Exception DownloadException, Action callback, object oldSecurityProtocol)
        {
            NetTools.RestoreSecurityProtocol(oldSecurityProtocol);

            if (DownloadException == null)
            {
                bool FoundUpdate = false;

                if (Content != null)
                {
                    const string Pattern = @"<a href=""/dlebansais/PgJsonParse/releases/tag/";
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

        private void OnDownloadVersion(GameVersionInfo VersionInfo)
        {
            IsGlobalInteractionEnabled = false;
            SetTaskbarState(TaskbarStates.Normal);
            VersionInfo.ProgressChanged += OnFileDownloadProgressChanged;

            OnDownloadVersion0(VersionInfo);
        }

        private void OnDownloadVersion0(GameVersionInfo VersionInfo)
        {
            string VersionFolder = InitFolder(Path.Combine(VersionCacheFolder, VersionInfo.Version.ToString()));

            List<string> JsonFileList = new List<string>();
            foreach (KeyValuePair<Type, IObjectDefinition> Entry in ObjectList.Definitions)
                if (Entry.Value.MinVersion <= VersionInfo.Version)
                    JsonFileList.Add(Entry.Value.JsonFileName);

            VersionInfo.DownloadFiles(this, VersionFolder, JsonFileList,
                                      (bool success, string exceptionMessage) => OnDownloadVersion1(success, exceptionMessage, VersionInfo));
        }

        private void OnDownloadVersion1(bool success, string exceptionMessage, GameVersionInfo VersionInfo)
        {
            VersionInfo.ProgressChanged -= OnFileDownloadProgressChanged;

            if (success)
            {
                SetTaskbarState(TaskbarStates.NoProgress);
                StatusMessage = null;
                LastExceptionMessage = null;

                if (StartAutomatically)
                    OnStart(VersionInfo);
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

        private void OnStart(GameVersionInfo VersionInfo)
        {
            OnStart0(VersionInfo);
        }

        private void OnStart0(GameVersionInfo VersionInfo)
        {
            IsGlobalInteractionEnabled = false;

            Dlg = new MainWindow();
            IsParsing = true;
            IsParsingCancelable = true;
            ParseCancellation = new Cancellation();
            ParseProgress = 0;
            ParseErrorInfo ErrorInfo = new ParseErrorInfo();
            SetTaskbarState(TaskbarStates.Normal);

            string VersionFolder = Path.Combine(VersionCacheFolder, VersionInfo.Version.ToString());
            string IconFolder = ShareIconFiles ? IconCacheFolder : VersionFolder;

            StartTask(() => { return ExecuteParse(VersionInfo, ErrorInfo, VersionFolder, IconFolder); },
                      (bool Success) => OnStart1(Success, VersionInfo, ErrorInfo));
        }

        private void OnStart1(bool Success, GameVersionInfo VersionInfo, ParseErrorInfo ErrorInfo)
        {
            SetTaskbarState(TaskbarStates.NoProgress);

            IsParsing = false;
            IsParsingCancelable = false;
            ParseCancellation = null;

            if (!Success)
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
                        if (Directory.Exists(VersionFolder))
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

            SwitchTo(Dlg, () => Dlg.StartApplication());

            IsGlobalInteractionEnabled = true;
        }

        public bool ExecuteParse(GameVersionInfo VersionInfo, ParseErrorInfo ErrorInfo, string VersionFolder, string IconFolder)
        {
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

                SetTaskbarProgressValue(ParseProgress, 100.0);
            }

            bool Success = ConnectTables(VersionFolder, IconFolder, ErrorInfo);

            return Success;
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

        private void FollowStartWithDownloadIcons(GameVersionInfo VersionInfo)
        {
            Dlg.ControlClose();
            Dlg = null;

            OnDownloadIcons(VersionInfo);
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

        private void OnDownloadIcons(GameVersionInfo VersionInfo)
        {
            IsGlobalInteractionEnabled = false;
            VersionInfo.ProgressChanged += OnIconDownloadProgressChanged;

            OnDownloadIcons0(VersionInfo);
        }

        private void OnDownloadIcons0(GameVersionInfo VersionInfo)
        {
            IsIconStateUpdated = true;
            SetTaskbarState(TaskbarStates.Normal);

            ExecuteDownloadIcons0(VersionInfo, 
                                  (bool Success) => OnDownloadIcons1(Success, VersionInfo));
        }

        private void OnDownloadIcons1(bool Success, GameVersionInfo VersionInfo)
        {
            VersionInfo.ProgressChanged -= OnIconDownloadProgressChanged;

            if (Success)
            {
                SetTaskbarState(TaskbarStates.NoProgress);

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
                {
                    OnStart(VersionInfo);
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

        private void ExecuteDownloadIcons0(GameVersionInfo VersionInfo, Action<bool> callback)
        {
            string DestinationFolder = ShareIconFiles ? IconCacheFolder : Path.Combine(VersionCacheFolder, VersionInfo.Version.ToString());
            VersionInfo.DownloadIcons(this, LoadedIconCount, MissingIconList, DestinationFolder, 
                                      (bool Success, string ExceptionMessage, int NewLoadedIconCount) => ExecuteDownloadIcons1(Success, ExceptionMessage, NewLoadedIconCount, callback));
        }

        private void ExecuteDownloadIcons1(bool success, string exceptionMessage, int newLoadedIconCount, Action<bool> callback)
        {
            LastExceptionMessage = exceptionMessage;
            LoadedIconCount = newLoadedIconCount;

            callback(success);
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

        private void OnPopupClosed(object sender, EventArgs e)
        {
            PopupHandler.OnPopupClosed(this);
        }

        private void OnIconSharingChanged(object sender, RoutedEventArgs e)
        {
            OnIconSharingChanged();
        }

        protected override void OnControlClosing(ref bool Cancel)
        {
            SetTaskbarState(TaskbarStates.NoProgress);

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
