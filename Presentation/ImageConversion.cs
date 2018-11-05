#if CSHTML5
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Presentation
{
    public static class ImageConversion
    {
        public static ImageSource IconFileToImageSource(string IconFile)
        {
            return null;
        }

        public static void UpdateWindowIconUsingFile(Page window, string FileName)
        {
        }
    }
}
#else
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Presentation
{
    public static class ImageConversion
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

        public static void UpdateWindowIconUsingFile(Window window, string FileName)
        {
            window.Icon = IconFileToImageSource(FileName);
        }
    }
}
#endif
