using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;

namespace PgJsonParse
{
    public class GameVersionInfo : INotifyPropertyChanged
    {
        #region Init
        public GameVersionInfo(int Version, DownloadState FileDownloadState, DownloadState IconDownloadState)
        {
            this.Version = Version;
            _FileDownloadState = FileDownloadState;
            _IconDownloadState = IconDownloadState;
        }
        #endregion

        #region Properties
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

        public bool DownloadFiles(List<string> FileList, string DestinationFolder, out string ExceptionMessage)
        {
            FileDownloadState = DownloadState.Downloading;
            IsFileDownloadCancelled = false;
            FileDownloadProgress = 0;

            bool Success = false;

            try
            {
                Success = ExecuteDownloadFiles(FileList, DestinationFolder);
                ExceptionMessage = null;
            }
            catch (Exception e)
            {
                ExceptionMessage = e.Message;
                FileDownloadState = DownloadState.FailedToDownload;
            }

            if (Success)
                FileDownloadState = DownloadState.Downloaded;

            return Success;
        }

        public void CancelDownloadFiles()
        {
            IsFileDownloadCancelled = true;
        }

        private bool ExecuteDownloadFiles(List<string> FileList, string DestinationFolder)
        {
            Stopwatch Watch = new Stopwatch();
            Watch.Start();

            int ProgressIndex = 0;
            foreach (string FileName in FileList)
            {
                if (!DownloadOneFile("data", FileName, "json", DestinationFolder))
                    return false;

                FileDownloadProgress = (ProgressIndex * 100.0) / FileList.Count;
                ProgressIndex++;
            }

            Tools.MinimalSleep(Watch);

            return true;
        }

        private bool DownloadOneFile(string SourceLocation, string FileName, string Extension, string DestinationFolder)
        {
            if (IsFileDownloadCancelled)
            {
                FileDownloadState = DownloadState.DownloadCanceled;
                return false;
            }

            bool Success = false;

            HttpWebRequest Request = WebRequest.Create("http://cdn.projectgorgon.com/v" + Version + "/" + SourceLocation + "/" + FileName + "." + Extension) as HttpWebRequest;
            using (WebResponse Response = Request.GetResponse())
            {
                using (Stream ResponseStream = Response.GetResponseStream())
                {
                    using (StreamReader Reader = new StreamReader(ResponseStream, Encoding.ASCII))
                    {
                        string Content = Reader.ReadToEnd();
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
                }
            }

            if (!Success)
                FileDownloadState = DownloadState.FailedToDownload;

            return Success;
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

        public bool DownloadIcons(ref int LoadedIconCount, List<int> IconList, string DestinationFolder, out string ExceptionMessage)
        {
            IconDownloadState = DownloadState.Downloading;
            IsIconDownloadCancelled = false;

            bool Success = false;

            try
            {
                Success = DownloadIconsNow(ref LoadedIconCount, IconList, DestinationFolder);
                ExceptionMessage = null;
            }
            catch (Exception e)
            {
                ExceptionMessage = e.Message;
                IconDownloadState = DownloadState.FailedToDownload;
            }

            if (Success)
                IconDownloadState = DownloadState.Downloaded;

            return Success;
        }

        public void CancelDownloadIcons()
        {
            IsIconDownloadCancelled = true;
        }

        private bool DownloadIconsNow(ref int LoadedIconCount, List<int> IconList, string DestinationFolder)
        {
            Stopwatch Watch = new Stopwatch();
            Watch.Start();

            List<int> DownloadedList = new List<int>();
            int ProgressIndex = 0;
            foreach (int IconId in IconList)
            {
                if (!DownloadIconNow("icons", "icon_" + IconId.ToString(), "png", DestinationFolder))
                {
                    foreach (int DownloadedIconId in DownloadedList)
                        if (IconList.Remove(DownloadedIconId))
                            LoadedIconCount++;

                    return false;
                }

                IconDownloadProgress = ((LoadedIconCount + ProgressIndex) * 100.0) / (LoadedIconCount + IconList.Count);

                DownloadedList.Add(IconId);
                ProgressIndex++;
            }

            Tools.MinimalSleep(Watch);

            return true;
        }

        private bool DownloadIconNow(string SourceLocation, string IconName, string Extension, string DestinationFolder)
        {
            if (IsIconDownloadCancelled)
            {
                IconDownloadState = DownloadState.DownloadCanceled;
                return false;
            }

            bool Success = false;

            HttpWebRequest Request = WebRequest.Create("http://cdn.projectgorgon.com/v" + Version + "/" + SourceLocation + "/" + IconName + "." + Extension) as HttpWebRequest;
            using (WebResponse Response = Request.GetResponse())
            {
                using (Stream ResponseStream = Response.GetResponseStream())
                {
                    using (BinaryReader Reader = new BinaryReader(ResponseStream))
                    {
                        if (!Directory.Exists(DestinationFolder))
                            Directory.CreateDirectory(DestinationFolder);

                        string IconPath = Path.Combine(DestinationFolder, IconName + "." + Extension);

                        using (FileStream fs = new FileStream(IconPath, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            ResponseStream.CopyTo(fs);
                            Success = true;
                        }
                    }
                }
            }

            if (!Success)
                IconDownloadState = DownloadState.FailedToDownload;

            return Success;
        }

        private bool IsIconDownloadCancelled;
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
