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
                    NotifyPropertyChanged(nameof(IsDownloadSuccessful));
                    NotifyPropertyChanged(nameof(IsDownloadFailed));
                    NotifyPropertyChanged(nameof(GlobalDownloadState));
                }
            }
        }
        public bool IsDownloadSuccessful { get { return FileDownloadState == DownloadState.Downloaded; } }
        public bool IsDownloadFailed { get { return FileDownloadState > DownloadState.Downloading && FileDownloadState < DownloadState.Downloaded; } }
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

        public delegate void DownloadFilesResultHandler(bool success, string exceptionMessage);

        public void DownloadFiles(IDispatcherSource dispatcherSource, string destinationFolder, IList<string> fileList, DownloadFilesResultHandler callback)
        {
            DownloadFiles0(dispatcherSource, fileList, destinationFolder, callback);
        }

        public void CancelDownloadFiles()
        {
            IsFileDownloadCancelled = true;
        }

        private void DownloadFiles0(IDispatcherSource dispatcherSource, IList<string> fileList, string destinationFolder, DownloadFilesResultHandler callback)
        {
            FileDownloadState = DownloadState.Downloading;
            IsFileDownloadCancelled = false;
            FileDownloadProgress = 0;

            Stopwatch Watch = new Stopwatch();
            Watch.Start();

            DownloadFiles1(dispatcherSource, fileList, destinationFolder, Watch, 0, callback);
        }

        private void DownloadFiles1(IDispatcherSource dispatcherSource, IList<string> fileList, string destinationFolder, Stopwatch watch, int progressIndex, DownloadFilesResultHandler callback)
        {
            if (progressIndex < fileList.Count)
            {
                string FileName = fileList[progressIndex];
                DownloadOneFile(dispatcherSource, "data", FileName, "json", destinationFolder, 
                                (bool success, Exception downloadException) => { DownloadFiles2(success, downloadException, dispatcherSource, fileList, destinationFolder, watch, progressIndex, callback); });
            }
            else
                DownloadFiles3(success: true, downloadException: null, watch, callback);
        }

        private void DownloadFiles2(bool success, Exception downloadException, IDispatcherSource dispatcherSource, IList<string> fileList, string destinationFolder, Stopwatch watch, int progressIndex, DownloadFilesResultHandler callback)
        {
            if (success && downloadException == null)
            {
                FileDownloadProgress = (progressIndex * 100.0) / fileList.Count;
                NotifyProgressChanged();

                DownloadFiles1(dispatcherSource, fileList, destinationFolder, watch, progressIndex + 1, callback);
            }
            else
                DownloadFiles3(success, downloadException, watch, callback);
        }

        private void DownloadFiles3(bool success, Exception downloadException, Stopwatch watch, DownloadFilesResultHandler callback)
        {
            string ExceptionMessage = null;

            if (success && downloadException == null)
                UserUI.MinimalSleep(watch);

            else if (downloadException != null)
            {
                Debug.Assert(!success);

                ExceptionMessage = downloadException.Message;
                FileDownloadState = DownloadState.FailedToDownload;
            }

            DownloadFiles4(success, ExceptionMessage, callback);
        }

        private void DownloadFiles4(bool success, string exceptionMessage, DownloadFilesResultHandler callback)
        {
            if (success)
                FileDownloadState = DownloadState.Downloaded;

            Debug.Assert((success && IsDownloadSuccessful) || (!success && IsDownloadFailed));

            callback(success, exceptionMessage);
        }

        private delegate void DownloadFilesCompletedHandler(bool success, Exception downloadException);

        private void DownloadOneFile(IDispatcherSource dispatcherSource, string sourceLocation, string fileName, string extension, string destinationFolder, DownloadFilesCompletedHandler callback)
        {
            DownloadOneFile0(dispatcherSource, sourceLocation, fileName, extension, destinationFolder, callback);
        }

        private void DownloadOneFile0(IDispatcherSource dispatcherSource, string sourceLocation, string fileName, string extension, string destinationFolder, DownloadFilesCompletedHandler callback)
        {
            if (IsFileDownloadCancelled)
            {
                FileDownloadState = DownloadState.DownloadCanceled;
                callback(success: false, downloadException: null);
                return;
            }

            string RequestUri = $"http://cdn.projectgorgon.com/v{Version}/{sourceLocation}/{fileName}.{extension}";
            WebClientTool.DownloadText(dispatcherSource, RequestUri, null,
                                       (string content, Exception downloadException) => { DownloadOneFile1(content, downloadException, fileName, extension, destinationFolder, callback); });
        }

        private void DownloadOneFile1(string content, Exception downloadException, string fileName, string extension, string destinationFolder, DownloadFilesCompletedHandler callback)
        {
            bool Success = false;

            if (downloadException == null)
            {
                try
                {
                    if (content != null && content.Length >= 256)
                    {
                        string FilePath = Path.Combine(destinationFolder, fileName + "." + extension);
                        FileTools.CommitTextFile(FilePath, content);
                        Success = true;
                    }
                }
                catch (Exception e)
                {
                    downloadException = e;
                }

                if (!Success)
                    FileDownloadState = DownloadState.FailedToDownload;
            }

            callback(Success, downloadException);
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
                    NotifyPropertyChanged(nameof(IsIconDownloadSuccessful));
                    NotifyPropertyChanged(nameof(IsIconDownloadFailed));
                    NotifyPropertyChanged(nameof(GlobalDownloadState));
                }
            }
        }
        public bool IsIconDownloadSuccessful { get { return IconDownloadState == DownloadState.Downloaded; } }
        public bool IsIconDownloadFailed { get { return IconDownloadState > DownloadState.Downloading && IconDownloadState < DownloadState.Downloaded; } }
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

        public void SetIconsDownloaded(bool isDownloaded)
        {
            IconDownloadState = isDownloaded ? DownloadState.Downloaded : DownloadState.NotDownloaded;
        }

        public delegate void DownloadIconsResultHandler(bool success, string exceptionMessage, int downloadedCount);

        public void DownloadIcons(IDispatcherSource dispatcherSource, int loadedIconCount, IList<int> iconList, string destinationFolder, DownloadIconsResultHandler callback)
        {
            IconDownloadState = DownloadState.Downloading;
            IsIconDownloadCancelled = false;

            DownloadIcons0(dispatcherSource, loadedIconCount, iconList, destinationFolder, callback);
        }

        public void CancelDownloadIcons()
        {
            IsIconDownloadCancelled = true;
        }

        private void DownloadIcons0(IDispatcherSource dispatcherSource, int loadedIconCount, IList<int> iconList, string destinationFolder, DownloadIconsResultHandler callback)
        {
            DownloadIcons1(dispatcherSource, loadedIconCount, iconList, destinationFolder, 
                           (bool success, Exception downloadException, int downloadedCount) => { DownloadIcons4(success, downloadException, downloadedCount, iconList, destinationFolder, callback); });
        }

        private delegate void DownloadIconsCompletedHandler(bool success, Exception downloadException, int downloadedCount);

        private void DownloadIcons1(IDispatcherSource dispatcherSource, int loadedIconCount, IList<int> iconList, string destinationFolder, DownloadIconsCompletedHandler callback)
        {
            Stopwatch Watch = new Stopwatch();
            Watch.Start();

            DownloadIcons2(dispatcherSource, loadedIconCount, iconList, destinationFolder, 0, callback, Watch);
        }

        private void DownloadIcons2(IDispatcherSource dispatcherSource, int loadedIconCount, IList<int> iconList, string destinationFolder, int progressIndex, DownloadIconsCompletedHandler callback, Stopwatch watch)
        {
            if (progressIndex < iconList.Count)
            {
                int IconId = iconList[progressIndex];
                DownloadOneIcon0(dispatcherSource, "icons", "icon_" + IconId.ToString(), "png", destinationFolder,
                                 (bool success, Exception downloadException) => { DownloadIcons3(success, downloadException, dispatcherSource, iconList, destinationFolder, callback, loadedIconCount, progressIndex, watch, IconId); });
            }
            else
            {
                if (iconList.Count > 0)
                    UserUI.MinimalSleep(watch);

                callback(success: true, downloadException: null, loadedIconCount + progressIndex);
            }
        }

        private void DownloadIcons3(bool success, Exception downloadException, IDispatcherSource dispatcherSource, IList<int> iconList, string destinationFolder, DownloadIconsCompletedHandler callback, int loadedIconCount, int progressIndex, Stopwatch watch, int iconId)
        {
            if (success)
            {
                IconDownloadProgress = ((loadedIconCount + progressIndex) * 100.0) / (loadedIconCount + iconList.Count);

                NotifyProgressChanged();

                DownloadIcons2(dispatcherSource, loadedIconCount, iconList, destinationFolder, progressIndex + 1, callback, watch);
            }
            else
                callback(false, downloadException, loadedIconCount + progressIndex);
        }

        private void DownloadIcons4(bool success, Exception downloadException, int downloadedCount, IList<int> iconList, string destinationFolder, DownloadIconsResultHandler callback)
        {
            if (success)
            {
                IconDownloadState = DownloadState.Downloaded;
                callback(success, exceptionMessage: null, downloadedCount);
            }

            else
            {
                Debug.Assert(IsIconDownloadFailed);

                if (downloadException != null)
                    callback(success, downloadException.Message, downloadedCount);

                else
                    callback(success, exceptionMessage: null, downloadedCount);
            }
        }

        private delegate void DownloadOneIconResultHandler(bool success, Exception downloadException);

        private void DownloadOneIcon0(IDispatcherSource dispatcherSource, string SourceLocation, string IconName, string Extension, string destinationFolder, DownloadOneIconResultHandler callback)
        {
            if (IsIconDownloadCancelled)
            {
                IconDownloadState = DownloadState.DownloadCanceled;
                callback(success: false, downloadException: null);
            }
            else
            {
                string RequestUri = $"http://cdn.projectgorgon.com/v{Version}/{SourceLocation}/{IconName}.{Extension}";
                string IconPath = Path.Combine(destinationFolder, IconName + "." + Extension);
                WebClientTool.DownloadDataToFile(dispatcherSource, RequestUri, 
                                                 (byte[] data, Exception downloadException) => { DownloadOneIcon1(data, downloadException, IconPath, 256, callback); });
            }
        }

        private void DownloadOneIcon1(byte[] data, Exception downloadException, string iconPath, int minLength, DownloadOneIconResultHandler callback)
        {
            if (data == null || data.Length < minLength || downloadException != null)
            {
                IconDownloadState = DownloadState.FailedToDownload;
                callback(success: false, downloadException);
            }

            bool success = false;

            try
            {
                string DestinationFolder = Path.GetDirectoryName(iconPath);
                FileTools.CreateDirectory(DestinationFolder);

                FileTools.CommitBinaryFile(iconPath, data);
                success = true;
            }
            catch (Exception e)
            {
                IconDownloadState = DownloadState.FailedToDownload;
                downloadException = e;
            }

            callback(success, downloadException);
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
