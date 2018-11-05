#if CSHTML5
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Presentation
{
    public class WebClientTool
    {
        #region Download Text
        public delegate void DownloadTextResultHandler(string content, Exception downloadException);

        public static void DownloadText(IDispatcherSource dispatcherSource, string address, Stopwatch watch, DownloadTextResultHandler callback)
        {
            Download(address, "text_proxy", OnDownloadTextCompleted, DownloadTimerTickText, new Action<string, Exception>((string data, Exception exception) => ReturnDownloadedText(data, exception, callback)));
        }

        private static void OnDownloadTextCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            OnDownloadCompleted(e, OnDownloadTextCompleted);
        }

        private static void DownloadTimerTickText(object sender, object e)
        {
            DownloadTimerTick(OnDownloadTextCompleted);
        }

        private static void ReturnDownloadedText(string data, Exception exception, DownloadTextResultHandler callback)
        {
            callback(data, exception);
        }
        #endregion

        #region Download Binary
        public delegate void DownloadDataResultHandler(byte[] data, Exception downloadException);

        public static void DownloadDataToFile(IDispatcherSource dispatcherSource, string address, DownloadDataResultHandler callback)
        {
            Download(address, "data_proxy", OnDownloadDataCompleted, DownloadTimerTickData, new Action<string, Exception>((string data, Exception exception) => SaveDownloadedData(data, exception, callback)));
        }

        private static void OnDownloadDataCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            OnDownloadCompleted(e, OnDownloadDataCompleted);
        }

        private static void DownloadTimerTickData(object sender, object e)
        {
            DownloadTimerTick(OnDownloadDataCompleted);
        }

        private static void SaveDownloadedData(string data, Exception exception, DownloadDataResultHandler callback)
        {
            if (data != null && exception == null)
            {
                try
                {
                    byte[] bytes = Convert.FromBase64String(data);
                    callback(bytes, exception);
                }
                catch (Exception e)
                {
                    callback(null, e);
                }
            }
            else
                callback(null, exception);
        }
        #endregion

        #region Explorer
        public static void OpenFileExplorer(string folder)
        {
        }
        #endregion

        #region Implementation
        private static void Download(string address, string ProxyName, DownloadStringCompletedEventHandler handlerAddEvent, EventHandler<object> handlerTick, Action<string, Exception> callback)
        {
            string ProxyAddress = "http://www.easly.org/support/" + ProxyName + ".php? addr=" + address;

            DownloadClient = new WebClient();
            AddDownloadedEvent(handlerAddEvent);

            DownloadCallback = callback;

            DownloadTimer = new DispatcherTimer();
            DownloadTimer.Interval = TimeSpan.FromSeconds(1);
            DownloadTimer.Tick += handlerTick;
            MaxTicks = 30;

            DownloadTask = DownloadClient.DownloadStringTaskAsync(ProxyAddress);
            DownloadTimer.Start();
        }

        private static void AddDownloadedEvent(DownloadStringCompletedEventHandler handler)
        {
            DownloadClient.DownloadStringCompleted += handler;
        }

        private static void RemoveDownloadedEvent(DownloadStringCompletedEventHandler handler)
        {
            if (DownloadClient != null)
            {
                DownloadClient.DownloadStringCompleted -= handler;
                DownloadClient = null;
            }
        }

        private static void DownloadTimerTick(DownloadStringCompletedEventHandler handlerRemoveEvent)
        {
            if (MaxTicks <= 0 || (DownloadTask != null && DownloadTask.IsCompleted))
            {
                if (DownloadTimer != null)
                {
                    DownloadTimer.Stop();
                    DownloadTimer = null;
                }

                RemoveDownloadedEvent(handlerRemoveEvent);

                if (DownloadTask.IsCompleted)
                {
                    string Content = DownloadTask.Result;
                    DownloadTask = null;
                    DownloadCallback?.Invoke(Content, null);
                }
                else
                {
                    DownloadTask = null;
                    DownloadCallback?.Invoke(null, null);
                }
            }
            else
                MaxTicks--;
        }

        private static void OnDownloadCompleted(DownloadStringCompletedEventArgs e, DownloadStringCompletedEventHandler handlerRemoveEvent)
        {
            if (DownloadTimer != null)
            {
                DownloadTimer.Stop();
                DownloadTimer = null;
            }

            RemoveDownloadedEvent(handlerRemoveEvent);

            DownloadTask = null;

            string Content = e.Result;
            DownloadCallback?.Invoke(Content, null);
        }

        private delegate object ConvertDownloadedContentHandler(string content);

        private static WebClient DownloadClient;
        private static Action<string, Exception> DownloadCallback;
        private static DispatcherTimer DownloadTimer;
        private static Task<string> DownloadTask;
        private static int MaxTicks;
        #endregion
    }
}
#else
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
#endif
