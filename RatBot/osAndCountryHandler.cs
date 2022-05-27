using System.Globalization;
using System.Linq;
using System.Management;

namespace stub
{
    internal class osAndCountryHandler
    {
        static string country = RegionInfo.CurrentRegion.DisplayName;
        public static string osGrabber()
        {
            var os = (from x in new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem").Get().Cast<ManagementObject>()
                      select x.GetPropertyValue("Caption")).FirstOrDefault();

            if (os == null)
            {
                return "Unknown";
            }
            else
            {
                return os.ToString();
            }
        }

        public static string countryGrabber()
        {
            return country;
        }
    }
}
