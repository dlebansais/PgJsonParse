using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Presentation
{
    public class WebClientTool
    {
        #region Download Text
        public delegate void DownloadTextResultHandler(string content, Exception downloadException);

        public static void DownloadText(IDispatcherSource dispatcherSource, string address, Stopwatch watch, DownloadTextResultHandler callback)
        {
            Task<Tuple<string,Exception>> DownloadTask = new Task<Tuple<string, Exception>>(() => { return ExecuteDownloadText(address, watch); });
            DownloadTask.Start();

            PollDownload(DownloadTask, callback);
        }

        private static Tuple<string, Exception> ExecuteDownloadText(string address, Stopwatch watch)
        {
            try
            {
                HttpWebRequest Request = WebRequest.Create(address) as HttpWebRequest;
                using (WebResponse Response = Request.GetResponse())
                {
                    using (Stream ResponseStream = Response.GetResponseStream())
                    {
                        Encoding Encoding = Encoding.ASCII;
                        string[] ContentTypeSplit = Response.ContentType.ToLower().Split(';');
                        foreach (string s in ContentTypeSplit)
                        {
                            string Chunk = s.Trim();
                            if (Chunk.StartsWith("charset"))
                            {
                                string[] ChunkSplit = Chunk.Split('=');
                                if (ChunkSplit.Length == 2)
                                    if (ChunkSplit[0] == "charset")
                                        if (ChunkSplit[1] == "utf8" || ChunkSplit[1] == "utf-8")
                                        {
                                            Encoding = Encoding.UTF8;
                                            break;
                                        }
                            }
                        }

                        using (StreamReader Reader = new StreamReader(ResponseStream, Encoding))
                        {
                            string Content = Reader.ReadToEnd();

                            bool IsReadToEnd = Reader.EndOfStream;
                            if (IsReadToEnd)
                            {
                                if (watch != null)
                                    UserUI.MinimalSleep(watch);

                                return new Tuple<string, Exception>(Content, null);
                            }
                            else
                                return new Tuple<string, Exception>(null, null);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return new Tuple<string, Exception>(null, e);
            }
        }

        private static void PollDownload(Task<Tuple<string, Exception>> DownloadTask, DownloadTextResultHandler callback)
        {
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new Action<Task<Tuple<string, Exception>>, DownloadTextResultHandler>(OnCheckDownload), DownloadTask, callback);
        }

        private static void OnCheckDownload(Task<Tuple<string, Exception>> DownloadTask, DownloadTextResultHandler callback)
        {
            if (DownloadTask.IsCompleted)
            {
                Tuple<string, Exception> Result = DownloadTask.Result;
                callback(Result.Item1, Result.Item2);
            }
            else
                PollDownload(DownloadTask, callback);
        }
        #endregion

        #region Download Binary
        public delegate void DownloadDataResultHandler(byte[] data, Exception downloadException);

        public static void DownloadDataToFile(IDispatcherSource dispatcherSource, string address, DownloadDataResultHandler callback)
        {
            Task<Tuple<byte[], Exception>> DownloadTask = new Task<Tuple<byte[], Exception>>(() => { return ExecuteDownloadData(address); });
            DownloadTask.Start();

            PollDownload(DownloadTask, callback);
        }

        private static Tuple<byte[], Exception> ExecuteDownloadData(string address)
        {
            try
            {
                HttpWebRequest Request = WebRequest.Create(address) as HttpWebRequest;
                using (WebResponse Response = Request.GetResponse())
                {
                    using (Stream ResponseStream = Response.GetResponseStream())
                    {
                        using (BinaryReader Reader = new BinaryReader(ResponseStream))
                        {
                            byte[] Content = Reader.ReadBytes((int)Response.ContentLength);
                            return new Tuple<byte[], Exception>(Content, null);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return new Tuple<byte[], Exception>(null, e);
            }
        }

        private static void PollDownload(Task<Tuple<byte[], Exception>> DownloadTask, DownloadDataResultHandler callback)
        {
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new Action<Task<Tuple<byte[], Exception>>, DownloadDataResultHandler>(OnCheckDownload), DownloadTask, callback);
        }

        private static void OnCheckDownload(Task<Tuple<byte[], Exception>> DownloadTask, DownloadDataResultHandler callback)
        {
            if (DownloadTask.IsCompleted)
            {
                Tuple<byte[], Exception> Result = DownloadTask.Result;
                callback(Result.Item1, Result.Item2);
            }
            else
                PollDownload(DownloadTask, callback);
        }
        #endregion

        #region Explorer
        public static void OpenFileExplorer(string folder)
        {
            Process Explorer = new Process();
            Explorer.StartInfo.FileName = "explorer.exe";
            Explorer.StartInfo.Arguments = folder;
            Explorer.StartInfo.UseShellExecute = true;

            Explorer.Start();
        }
        #endregion
    }
}