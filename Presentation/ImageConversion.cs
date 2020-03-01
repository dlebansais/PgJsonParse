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
        private static extern bool DeleteObject([In] IntPtr hObject);

        public static ImageSource IconFileToImageSource(string iconFile)
        {
            if (!File.Exists(iconFile))
                return null;

            using Bitmap Bitmap = new Bitmap(iconFile);
            IntPtr Handle = Bitmap.GetHbitmap();
            try
            {
                return Imaging.CreateBitmapSourceFromHBitmap(Handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally { DeleteObject(Handle); }
        }

        public static void UpdateWindowIconUsingFile(Window window, string fileName)
        {
            window.Icon = IconFileToImageSource(fileName);
        }
    }
}
#endif
