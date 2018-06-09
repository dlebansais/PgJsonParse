using System;
using System.Diagnostics;
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
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public BitmapImage BitmapImage { get; private set; }
    }
}
