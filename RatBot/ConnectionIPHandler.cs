using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stub
{
    internal class ConnectionIPHandler
    {
        static string GlobalIP;
        static public void IPSet(string IP)
        {
            GlobalIP = IP;
        }

        static public string GetIP()
        {
            return GlobalIP;
        }
    }
}
