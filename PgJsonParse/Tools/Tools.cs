using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PgJsonParse
{
    public class Tools
    {
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);

        public static ImageSource IconFileToImageSource(string IconFile)
        {
            if (!File.Exists(IconFile))
                return null;

            Bitmap bmp = new Bitmap(IconFile);
            var handle = bmp.GetHbitmap();
            try
            {
                return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally { DeleteObject(handle); }
        }

        public static void MinimalSleep(Stopwatch Watch)
        {
            MinimalSleep(Watch, TimeSpan.FromSeconds(4));
        }

        public static void MinimalSleep(Stopwatch Watch, TimeSpan MinimumTime)
        {
            TimeSpan Remaining = MinimumTime - Watch.Elapsed;
            if (Remaining > TimeSpan.Zero)
                Thread.Sleep(Remaining);
        }
    }
}
