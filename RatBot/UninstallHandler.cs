using System;
using System.IO;

namespace stub
{
    internal class UninstallHandler
    {
        public static void uninstall(string cmd)
        {
            if (cmd.Contains("uninstall stop"))
            {
                try
                {
                    Microsoft.Win32.RegistryKey regKey =
                        Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                    regKey.DeleteValue("Service", true);
                    regKey.Close();
                }
                catch { }
                finally
                {
                    string letter = DriveGet.getdrives();
                    string user = Environment.UserName;
                    string connection_path = letter + @"Users\" + user + @"\AppData\Local\Temp\log.txt";
                    string path = letter + @"Users\" + user + @"\AppData\Local\Temp\update.txt";

                    if (File.Exists(connection_path))
                    {
                        File.Delete(connection_path);
                    }
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    System.Environment.Exit(0);
                }
            }
            else if (cmd.Contains("uninstall"))
            {
                try
                {
                    Microsoft.Win32.RegistryKey regKey =
                        Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                    regKey.DeleteValue("Service", true);
                    regKey.Close();
                }
                catch { }
            }
        }
    }
}
