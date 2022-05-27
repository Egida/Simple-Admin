using System.Threading;

namespace stub
{
    internal class MutexHandler
    {
        const string appName = "MC-DEBUG";
        static bool createdNew;
        private static Mutex mutex = null;

        public static void createMutex()
        {
            mutex = new Mutex(true, appName, out createdNew);

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
