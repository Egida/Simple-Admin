using System;
using System.Diagnostics;

namespace stub
{
    internal class Commands
    {

        public static string Run(string cmdToRun)
        {
            try
            {
                string retString = "";

                Process processCmd = new Process();
                processCmd.StartInfo.FileName = "cmd.exe";
                processCmd.StartInfo.Arguments = "/c " + cmdToRun;
                processCmd.StartInfo.UseShellExecute = false;
                processCmd.StartInfo.CreateNoWindow = true;
                processCmd.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
                processCmd.StartInfo.RedirectStandardOutput = true;
                processCmd.StartInfo.RedirectStandardError = true;
                processCmd.Start();

                retString += processCmd.StandardOutput.ReadToEnd();
                retString += processCmd.StandardError.ReadToEnd();

                return retString;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString() + Environment.NewLine;
            }

        }
    }


}
