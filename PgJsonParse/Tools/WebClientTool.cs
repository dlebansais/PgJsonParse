#if USE_HTTPREQUEST
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
#else
using System.IO;
using System.Net;
#endif

namespace Tools
{
    public class WebClientTool
    {
        public static string DownloadText(string address)
        {
#if USE_HTTPREQUEST
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

                        try
                        {
                            bool IsReadToEnd = Reader.EndOfStream;
                            if (IsReadToEnd)
                                return Content;

                            return null;
                        }
                        catch
                        {
                            throw;
                        }
                    }
                }
            }
#else
            WebClient client = new WebClient();
            return client.DownloadString(address);
#endif
        }

        public static bool DownloadDataToFile(string address, string FileName, int minLength)
        {
#if USE_HTTPREQUEST
            HttpWebRequest Request = WebRequest.Create(address) as HttpWebRequest;
            using (WebResponse Response = Request.GetResponse())
            {
                if (Response.ContentLength < minLength)
                    return false;

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
                                return false;

                            fs.Write(Content, 0, Content.Length);
                        }
                    }
                }
            }

            return true;
#else
            WebClient client = new WebClient();
            string Content = client.DownloadString(address);
            if (Content.Length < minLength)
                return false;

            using (FileStream fs = new FileStream(FileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write(Content);
                }
            }
            return true;
#endif
        }
    }
}