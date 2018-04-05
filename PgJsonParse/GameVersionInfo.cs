using Presentation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
#if CSHARP_XAML_FOR_HTML5
using Windows.UI.Xaml;
#else
using System.Windows;
#endif
using Tools;

namespace PgJsonParse
{
    public class GameVersionInfo : INotifyPropertyChanged
    {
        #region Init
        public GameVersionInfo(FrameworkElement owner, int version, DownloadState fileDownloadState, DownloadState iconDownloadState)
        {
            Owner = owner;
            Version = version;
            _FileDownloadState = fileDownloadState;
            _IconDownloadState = iconDownloadState;
        }
        #endregion

        #region Properties
        public FrameworkElement Owner { get; private set; }
        public int Version { get; private set; }

        public DownloadState GlobalDownloadState
        {
            get
            {
                if (FileDownloadState == DownloadState.FailedToDownload || IconDownloadState == DownloadState.FailedToDownload)
                    return DownloadState.FailedToDownload;
                else if (FileDownloadState == DownloadState.DownloadCanceled || IconDownloadState == DownloadState.DownloadCanceled)
                    return DownloadState.DownloadCanceled;
                else if (FileDownloadState == DownloadState.Downloading || IconDownloadState == DownloadState.Downloading)
                    return DownloadState.Downloading;
                else if (FileDownloadState == DownloadState.NotDownloaded || IconDownloadState == DownloadState.NotDownloaded)
                    return DownloadState.NotDownloaded;
                else
                    return DownloadState.Downloaded;
            }
        }
        #endregion

        #region Files
        public DownloadState FileDownloadState
        {
            get { return _FileDownloadState; }
            private set
            {
                if (_FileDownloadState != value)
                {
                    _FileDownloadState = value;
                    NotifyThisPropertyChanged();
                    NotifyPropertyChanged(nameof(GlobalDownloadState));
                }
            }
        }
        private DownloadState _FileDownloadState;

        public double FileDownloadProgress
        {
            get { return _FileDownloadProgress; }
            private set
            {
                if (_FileDownloadProgress != value)
                {
                    _FileDownloadProgress = value;
                    NotifyThisPropertyChanged();
                }
            }
        }
        private double _FileDownloadProgress;

        public void DownloadFiles(RootControl control, string DestinationFolder, List<string> FileList, Action<bool, string> callback)
        {
            DownloadFiles0(control, FileList, DestinationFolder, callback);
        }

        private void DownloadFiles0(RootControl control, List<string> FileList, string DestinationFolder, Action<bool, string> callback)
        {
            FileDownloadState = DownloadState.Downloading;
            IsFileDownloadCancelled = false;
            FileDownloadProgress = 0;

            Stopwatch Watch = new Stopwatch();
            Watch.Start();

            if (FileList.Count > 0)
                DownloadFiles1(control, FileList, DestinationFolder, Watch, 0, callback);
            else
                DownloadFiles3(true, null, Watch, callback);
        }

        private void DownloadFiles1(RootControl control, List<string> FileList, string DestinationFolder, Stopwatch Watch, int ProgressIndex, Action<bool, string> callback)
        {
            string FileName = FileList[ProgressIndex];
            DownloadOneFile(control, "data", FileName, "json", DestinationFolder, new Action<bool, Exception>((bool Success, Exception DownloadException) => { DownloadFiles2(Success, DownloadException, control, FileList, DestinationFolder, Watch, ProgressIndex, callback); } ));
        }

        private void DownloadFiles2(bool Success, Exception DownloadException, RootControl control, List<string> FileList, string DestinationFolder, Stopwatch Watch, int ProgressIndex, Action<bool, string> callback)
        {
            if (Success && DownloadException == null)
            {
                FileDownloadProgress = (ProgressIndex * 100.0) / FileList.Count;
                ProgressIndex++;

                NotifyProgressChanged();

                if (ProgressIndex < FileList.Count)
                    DownloadFiles1(control, FileList, DestinationFolder, Watch, ProgressIndex, callback);
                else
                    DownloadFiles3(true, null, Watch, callback);
            }
            else
                DownloadFiles3(Success, DownloadException, Watch, callback);
        }

        private void DownloadFiles3(bool Success, Exception DownloadException, Stopwatch Watch, Action<bool, string> callback)
        {
            string ExceptionMessage = null;

            if (Success && DownloadException == null)
                UserUI.MinimalSleep(Watch);

            else if (DownloadException != null)
            {
                Success = false;
                ExceptionMessage = DownloadException.Message;
                FileDownloadState = DownloadState.FailedToDownload;
            }

            DownloadFiles4(Success, ExceptionMessage, callback);
        }

        private void DownloadFiles4(bool Success, string ExceptionMessage, Action<bool, string> callback)
        {
            if (Success)
                FileDownloadState = DownloadState.Downloaded;

            callback(Success, ExceptionMessage);
        }

        public void CancelDownloadFiles()
        {
            IsFileDownloadCancelled = true;
        }

        private void DownloadOneFile(RootControl control, string SourceLocation, string FileName, string Extension, string DestinationFolder, Action<bool, Exception> callback)
        {
            DownloadOneFile0(control, SourceLocation, FileName, Extension, DestinationFolder, callback);
        }

        private void DownloadOneFile0(RootControl control, string SourceLocation, string FileName, string Extension, string DestinationFolder, Action<bool, Exception> callback)
        {
            if (IsFileDownloadCancelled)
            {
                FileDownloadState = DownloadState.DownloadCanceled;
                callback(false, null);
                return;
            }

            string RequestUri = "http://cdn.projectgorgon.com/v" + Version + "/" + SourceLocation + "/" + FileName + "." + Extension;
            WebClientTool.DownloadText(control, RequestUri, null,
                                       new Action<string, Exception>((string Content, Exception e) => { DownloadOneFile1(Content, e, FileName, Extension, DestinationFolder, callback); }));
        }

        private void DownloadOneFile1(string Content, Exception ContentException, string FileName, string Extension, string DestinationFolder, Action<bool, Exception> callback)
        {
            bool Success = false;

            if (ContentException == null)
            {
                try
                {
                    if (Content != null && Content.Length >= 256)
                    {
                        string FilePath = Path.Combine(DestinationFolder, FileName + "." + Extension);

                        using (FileStream fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            using (StreamWriter sw = new StreamWriter(fs, Encoding.ASCII))
                            {
                                sw.Write(Content);
                                Success = true;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    ContentException = e;
                }

                if (!Success)
                    FileDownloadState = DownloadState.FailedToDownload;
            }

            callback(Success, ContentException);
        }

        private bool IsFileDownloadCancelled;
        #endregion

        #region Icons
        public DownloadState IconDownloadState
        {
            get { return _IconDownloadState; }
            private set
            {
                if (_IconDownloadState != value)
                {
                    _IconDownloadState = value;
                    NotifyThisPropertyChanged();
                    NotifyPropertyChanged(nameof(GlobalDownloadState));
                }
            }
        }
        private DownloadState _IconDownloadState;

        public double IconDownloadProgress
        {
            get { return _IconDownloadProgress; }
            private set
            {
                if (_IconDownloadProgress != value)
                {
                    _IconDownloadProgress = value;
                    NotifyThisPropertyChanged();
                }
            }
        }
        private double _IconDownloadProgress;

        public void SetIconsDownloaded(bool IsDownloaded)
        {
            IconDownloadState = IsDownloaded ? DownloadState.Downloaded : DownloadState.NotDownloaded;
        }

        public void DownloadIcons(RootControl control, int LoadedIconCount, List<int> IconList, string DestinationFolder, Action<bool, string, int> callback)
        {
            IconDownloadState = DownloadState.Downloading;
            IsIconDownloadCancelled = false;

            DownloadIcons0(control, LoadedIconCount, IconList, DestinationFolder, callback);
        }

        public void DownloadIcons0(RootControl control, int LoadedIconCount, List<int> IconList, string DestinationFolder, Action<bool, string, int> callback)
        {
            DownloadIconsNow(control, LoadedIconCount, IconList, DestinationFolder, new Action<bool, Exception, int>((bool Success, Exception DownloadException, int Count) => { DownloadIcons1(Success, DownloadException, Count, IconList, DestinationFolder, callback); }));
        }

        public void DownloadIcons1(bool Success, Exception DownloadException, int Count, List<int> IconList, string DestinationFolder, Action<bool, string, int> callback)
        {
            if (Success)
            {
                IconDownloadState = DownloadState.Downloaded;
                callback(true, null, Count);
            }

            else if (DownloadException != null)
                callback(false, DownloadException.Message, Count);

            else
                callback(false, null, Count);
        }

        public void CancelDownloadIcons()
        {
            IsIconDownloadCancelled = true;
        }

        private void DownloadIconsNow(RootControl control, int LoadedIconCount, List<int> IconList, string DestinationFolder, Action<bool, Exception, int> callback)
        {
            Stopwatch Watch = new Stopwatch();
            Watch.Start();

            if (IconList.Count > 0)
                DownloadIconsNow0(control, LoadedIconCount, IconList, DestinationFolder, 0, callback, Watch);
            else
                callback(true, null, LoadedIconCount);
        }

        private void DownloadIconsNow0(RootControl control, int LoadedIconCount, List<int> IconList, string DestinationFolder, int ProgressIndex, Action<bool, Exception, int> callback, Stopwatch Watch)
        {
            int IconId = IconList[ProgressIndex];
            DownloadOneIcon0(control, "icons", "icon_" + IconId.ToString(), "png", DestinationFolder, new Action<bool, Exception>((bool Success, Exception DownloadException) => { DownloadIconsNow1(Success, DownloadException, control, IconList, DestinationFolder, callback, LoadedIconCount, ProgressIndex, Watch, IconId); }));
        }

        private void DownloadIconsNow1(bool Success, Exception DownloadException, RootControl control, List<int> IconList, string DestinationFolder, Action<bool, Exception, int> callback, int LoadedIconCount, int ProgressIndex, Stopwatch Watch, int IconId)
        {
            if (Success)
            {
                IconDownloadProgress = ((LoadedIconCount + ProgressIndex) * 100.0) / (LoadedIconCount + IconList.Count);

                ProgressIndex++;

                NotifyProgressChanged();

                if (ProgressIndex < IconList.Count)
                    DownloadIconsNow0(control, LoadedIconCount, IconList, DestinationFolder, ProgressIndex, callback, Watch);
                else
                {
                    UserUI.MinimalSleep(Watch);
                    callback(true, null, LoadedIconCount + ProgressIndex);
                }
            }
            else
                callback(false, DownloadException, LoadedIconCount + ProgressIndex);
        }

        private void DownloadOneIcon0(RootControl control, string SourceLocation, string IconName, string Extension, string DestinationFolder, Action<bool, Exception> callback)
        {
            if (IsIconDownloadCancelled)
            {
                IconDownloadState = DownloadState.DownloadCanceled;
                callback(false, null);
            }
            else
            {
                string RequestUri = "http://cdn.projectgorgon.com/v" + Version + "/" + SourceLocation + "/" + IconName + "." + Extension;
                string IconPath = Path.Combine(DestinationFolder, IconName + "." + Extension);
                WebClientTool.DownloadDataToFile(control, RequestUri, IconPath, 256, new Action<bool, Exception>((bool Success, Exception DownloadException) => { DownloadOneIcon1(Success, DownloadException, callback); }));
            }
        }

        private void DownloadOneIcon1(bool Success, Exception DownloadException, Action<bool, Exception> callback)
        {
            if (!Success)
                IconDownloadState = DownloadState.FailedToDownload;

            callback(Success, DownloadException);
        }

        private bool IsIconDownloadCancelled;
        #endregion

        #region Events
        public event EventHandler ProgressChanged;

        private void NotifyProgressChanged()
        {
            ProgressChanged?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region Debugging
        public override string ToString()
        {
            return Version.ToString() + ", Files: " + FileDownloadState + ", Icons: " + IconDownloadState;
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
