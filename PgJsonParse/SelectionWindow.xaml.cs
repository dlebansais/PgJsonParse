using Presentation;
using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows;

namespace PgJsonParse
{
    public partial class SelectionWindow : RootControl, INotifyPropertyChanged
    {
        #region Init
        public SelectionWindow(double width, double height)
            : base(RootControlMode.CustomShape)
        {
            InitializeComponent();
            DataContext = this;

            FrameworkElement RootContent = Content as FrameworkElement;
            RootContent.Width = width;
            RootContent.Height = height;

            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            SubscribeToCommand("BuildPlanerCommand", OnBuildPlaner);
            SubscribeToCommand("SearchCommand", OnSearch);
            SubscribeToCommand("HomeCommand", OnHome);
        }

        public void StartApplication()
        {
        }
        #endregion

        #region Properties
        public string ApplicationFolder { get; set; }
        public string VersionCacheFolder { get; set; }
        public string IconCacheFolder { get; set; }
        public string CurrentVersionCacheFolder { get; set; }
        public string IconFile { get; set; }
        public string FavorIconFile { get; set; }

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

        #region Events
        private void OnBuildPlaner(object sender, EventArgs e)
        {
            Dlg = new MainWindow();
            App.SetMainWindow(Dlg);

            Dlg.LoadedVersion = LoadedVersion;
            Dlg.ApplicationFolder = ApplicationFolder;
            Dlg.IconCacheFolder = IconCacheFolder;
            Dlg.CurrentVersionCacheFolder = CurrentVersionCacheFolder;
            Dlg.IconFile = IconFile;
            Dlg.FavorIconFile = FavorIconFile;

            SwitchTo(Dlg, () => Dlg.StartApplication());
        }

        private void OnSearch(object sender, EventArgs e)
        {
            Dlg = new MainWindow();
            App.SetMainWindow(Dlg);

            Dlg.LoadedVersion = LoadedVersion;
            Dlg.ApplicationFolder = ApplicationFolder;
            Dlg.IconCacheFolder = IconCacheFolder;
            Dlg.CurrentVersionCacheFolder = CurrentVersionCacheFolder;
            Dlg.IconFile = IconFile;
            Dlg.FavorIconFile = FavorIconFile;

            SwitchTo(Dlg, () => Dlg.StartApplication());
        }

        private void OnHome(object sender, EventArgs e)
        {
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
