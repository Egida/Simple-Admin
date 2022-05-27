using System.IO;

namespace stub
{
    internal class DriveGet
    {
        public static string getdrives()
        {
            DriveInfo[] alldrives = DriveInfo.GetDrives();

            foreach (DriveInfo d in alldrives)
            {
                string path = d.Name + @"\Windows";

                if (Directory.Exists(path))
                {
                    return d.Name;
                }
            }
            return "";
        }
    }
}
