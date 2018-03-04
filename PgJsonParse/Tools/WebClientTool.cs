#if USE_HTTPREQUEST
using System.IO;
using System.Net;
using System.Text;
#else
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
                    using (StreamReader Reader = new StreamReader(ResponseStream, Encoding.ASCII))
                    {
                        return Reader.ReadToEnd();
                    }
                }
            }
#else
            using (WebClient client = new WebClient())
            {
                return client.DownloadString(address);
            }
#endif
        }

        public static bool DownloadDataToFile(string address, string FileName, int minLength)
        {
#if USE_HTTPREQUEST
            HttpWebRequest Request = WebRequest.Create(address) as HttpWebRequest;
            using (WebResponse Response = Request.GetResponse())
            {
                using (Stream ResponseStream = Response.GetResponseStream())
                {
                    using (BinaryReader Reader = new BinaryReader(ResponseStream))
                    {
                        string DestinationFolder = Path.GetDirectoryName(FileName);
                        if (!Directory.Exists(DestinationFolder))
                            Directory.CreateDirectory(DestinationFolder);

                        using (FileStream fs = new FileStream(FileName, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            ResponseStream.CopyTo(fs);
                            if (fs.Length < minLength)
                                return false;
                        }
                    }
                }
            }

            return true;
#else
            using (WebClient client = new WebClient())
            {
                return client.DownloadString(address);
            }
#endif
        }
    }
}