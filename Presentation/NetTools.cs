using System;
using System.Net;

namespace Presentation
{
    public class NetTools
    {
        public static void EnableSecurityProtocol(out object oldSecurityProtocol)
        {
            oldSecurityProtocol = new Tuple<bool, SecurityProtocolType>(ServicePointManager.Expect100Continue, ServicePointManager.SecurityProtocol);

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        public static void RestoreSecurityProtocol(object oldSecurityProtocol)
        {
            if (oldSecurityProtocol is Tuple<bool, SecurityProtocolType> RestoredValues)
            {
                ServicePointManager.Expect100Continue = RestoredValues.Item1;
                ServicePointManager.SecurityProtocol = RestoredValues.Item2;
            }
        }
    }
}
