using System.Net;

namespace Presentation
{
    public class NetTools
    {
        public static void EnableTls12()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }
    }
}
