using System;
using System.Windows.Media.Imaging;

namespace Presentation
{
    public class FileBitmapImage
    {
        public FileBitmapImage(string fileName)
        {
            BitmapImage = new BitmapImage(new Uri(fileName));
        }

        public BitmapImage BitmapImage { get; private set; }
    }
}
