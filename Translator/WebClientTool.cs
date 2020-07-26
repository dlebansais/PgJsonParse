namespace Translator
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public static class WebClientTool
    {
        #region Download Text
        public delegate void DownloadTextResultHandler(bool isFound, string content);

        public static void DownloadText(string address, Stopwatch watch, DownloadTextResultHandler callback, out bool isFound)
        {
            Task<Tuple<bool, string>> DownloadTask = new Task<Tuple<bool, string>>(() => { return ExecuteDownloadText(address, watch); });
            DownloadTask.Start();

            OnCheckDownload(DownloadTask, callback, out isFound);
        }

        private static Tuple<bool, string> ExecuteDownloadText(string address, Stopwatch watch)
        {
            try
            {
                HttpClient Request = new HttpClient();
                Task<HttpResponseMessage> Message = Request.GetAsync(address);
                Message.Wait();
                HttpResponseMessage Response = Message.Result;
                if (Response.StatusCode != HttpStatusCode.OK)
                    return new Tuple<bool, string>(false, null);

                HttpContent ResponseContent = Response.Content;

                Task<Stream> ReadTask = ResponseContent.ReadAsStreamAsync();
                ReadTask.Wait();
                using Stream ResponseStream = ReadTask.Result;

                //using Stream ResponseStream = Response.GetResponseStream();
                Encoding Encoding = Encoding.ASCII;
                /*
                string[] ContentTypeSplit = Response.ContentType.ToUpperInvariant().Split(';');
                foreach (string s in ContentTypeSplit)
                {
                    string Chunk = s.Trim();
                    if (Chunk.StartsWith("CHARSET", StringComparison.InvariantCulture))
                    {
                        string[] ChunkSplit = Chunk.Split('=');
                        if (ChunkSplit.Length == 2)
                            if (ChunkSplit[0] == "CHARSET")
                                if (ChunkSplit[1] == "UTF8" || ChunkSplit[1] == "UTF-8")
                                {
                                    Encoding = Encoding.UTF8;
                                    break;
                                }
                    }
                }
                */

                using StreamReader Reader = new StreamReader(ResponseStream, Encoding);
                string Content = Reader.ReadToEnd();

                bool IsReadToEnd = Reader.EndOfStream;
                if (IsReadToEnd)
                {
                    if (watch != null)
                        MinimalSleep(watch);

                    return new Tuple<bool, string>(true, Content);
                }
                else
                    return new Tuple<bool, string>(true, null);
            }
            catch (Exception)
            {
                return new Tuple<bool, string>(false, null);
            }
        }

        private static void OnCheckDownload(Task<Tuple<bool, string>> DownloadTask, DownloadTextResultHandler callback, out bool isFound)
        {
            isFound = false;

            for (; ; )
            {
                if (DownloadTask.IsCompleted)
                {
                    Tuple<bool, string> Result = DownloadTask.Result;
                    callback(Result.Item1, Result.Item2);
                    isFound = Result.Item1;
                    break;
                }

                Thread.Sleep(1000);
            }
        }

        public static void MinimalSleep(Stopwatch Watch)
        {
            MinimalSleep(Watch, TimeSpan.FromSeconds(4));
        }

        public static void MinimalSleep(Stopwatch Watch, TimeSpan MinimumTime)
        {
            TimeSpan Remaining = MinimumTime - Watch.Elapsed;
            if (Remaining > TimeSpan.Zero)
                Thread.Sleep(Remaining);
        }
        #endregion

        #region Download Binary
        public delegate void DownloadDataResultHandler(byte[] data, Exception downloadException);

        public static void DownloadDataToFile(string address, DownloadDataResultHandler callback)
        {
            Task<Tuple<byte[], Exception>> DownloadTask = new Task<Tuple<byte[], Exception>>(() => { return ExecuteDownloadData(address); });
            DownloadTask.Start();

            OnCheckDownload(DownloadTask, callback);
        }

        private static Tuple<byte[], Exception> ExecuteDownloadData(string address)
        {
            try
            {
                HttpWebRequest Request = WebRequest.Create(new Uri(address)) as HttpWebRequest;
                using WebResponse Response = Request.GetResponse();
                using Stream ResponseStream = Response.GetResponseStream();
                using BinaryReader Reader = new BinaryReader(ResponseStream);
                byte[] Content = Reader.ReadBytes((int)Response.ContentLength);
                return new Tuple<byte[], Exception>(Content, null);
            }
            catch (Exception e)
            {
                return new Tuple<byte[], Exception>(null, e);
            }
        }

        private static void OnCheckDownload(Task<Tuple<byte[], Exception>> DownloadTask, DownloadDataResultHandler callback)
        {
            for (; ; )
            {
                if (DownloadTask.IsCompleted)
                {
                    Tuple<byte[], Exception> Result = DownloadTask.Result;
                    callback(Result.Item1, Result.Item2);
                    break;
                }

                Thread.Sleep(1000);
            }
        }
        #endregion

        #region Explorer
        public static void OpenFileExplorer(string folder)
        {
            using Process Explorer = new Process();
            Explorer.StartInfo.FileName = "explorer.exe";
            Explorer.StartInfo.Arguments = folder;
            Explorer.StartInfo.UseShellExecute = true;

            Explorer.Start();
        }
        #endregion
    }
}
