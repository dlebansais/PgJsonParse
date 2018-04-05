using Presentation;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Tools
{
    public class WebClientTool
    {
        #region Download Text
        public static void DownloadText(RootControl control, string address, Stopwatch watch, Action<string, Exception> callback)
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

        private static void PollDownload(Task<Tuple<string, Exception>> DownloadTask, Action<string, Exception> callback)
        {
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new Action<Task<Tuple<string, Exception>>, Action<string, Exception>>(OnCheckDownload), DownloadTask, callback);
        }

        private static void OnCheckDownload(Task<Tuple<string, Exception>> DownloadTask, Action<string, Exception> callback)
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
        public static void DownloadDataToFile(RootControl control, string address, string FileName, int minLength, Action<bool, Exception> callback)
        {
            Task<Tuple<bool, Exception>> DownloadTask = new Task<Tuple<bool, Exception>>(() => { return ExecuteDownloadDataToFile(address, FileName, minLength); });
            DownloadTask.Start();

            PollDownload(DownloadTask, callback);
        }

        private static Tuple<bool, Exception> ExecuteDownloadDataToFile(string address, string FileName, int minLength)
        {
            try
            {
                HttpWebRequest Request = WebRequest.Create(address) as HttpWebRequest;
                using (WebResponse Response = Request.GetResponse())
                {
                    if (Response.ContentLength < minLength)
                        return new Tuple<bool, Exception>(false, null);

                    using (Stream ResponseStream = Response.GetResponseStream())
                    {
                        using (BinaryReader Reader = new BinaryReader(ResponseStream))
                        {
                            string DestinationFolder = Path.GetDirectoryName(FileName);
                            if (!Directory.Exists(DestinationFolder))
                                Directory.CreateDirectory(DestinationFolder);

                            using (FileStream fs = new FileStream(FileName, FileMode.Create, FileAccess.Write, FileShare.None))
                            {
                                byte[] Content = Reader.ReadBytes((int)Response.ContentLength);

                                if (Content.Length < Response.ContentLength)
                                    return new Tuple<bool, Exception>(false, null);

                                fs.Write(Content, 0, Content.Length);
                            }
                        }
                    }
                }

                return new Tuple<bool, Exception>(true, null);
            }
            catch (Exception e)
            {
                return new Tuple<bool, Exception>(false, e);
            }
        }

        private static void PollDownload(Task<Tuple<bool, Exception>> DownloadTask, Action<bool, Exception> callback)
        {
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new Action<Task<Tuple<bool, Exception>>, Action<bool, Exception>>(OnCheckDownload), DownloadTask, callback);
        }

        private static void OnCheckDownload(Task<Tuple<bool, Exception>> DownloadTask, Action<bool, Exception> callback)
        {
            if (DownloadTask.IsCompleted)
            {
                Tuple<bool, Exception> Result = DownloadTask.Result;
                callback(Result.Item1, Result.Item2);
            }
            else
                PollDownload(DownloadTask, callback);
        }
        #endregion
    }
}