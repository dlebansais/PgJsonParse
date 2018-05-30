using System;
using System.Windows.Media.Imaging;

namespace Presentation
{
    public class FileBitmapImage
    {
        public FileBitmapImage(string fileName)
        {
            try
            {
                BitmapImage = new BitmapImage(new Uri(fileName));
            }
            catch
            {
            }
        }

        public BitmapImage BitmapImage { get; private set; }
    }
}
