using System.Threading;

namespace stub
{
    internal class MutexHandler
    {
        static string appName = "";
        static bool createdNew;
        private static Mutex mutex = null;

        public static void createMutex(string ID)
        {
            mutex = new Mutex(true, ID, out createdNew);

            appName = ID;
            if (!createdNew)
            {
                System.Environment.Exit(0);
            }
        }

        public static string appNameReturn()
        {
            return appName;
        }
    }
}
