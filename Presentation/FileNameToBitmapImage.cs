using Presentation;
using System;
using System.Windows.Media.Imaging;

namespace Converters
{
    public class FileNameToBitmapImage
    {
        public static BitmapImage Convert(string FilePath)
        {
            Uri uri;
            BitmapImage img;

            try
            {
                uri = FileTools.GetFileUri(FilePath);
                img = new BitmapImage(uri);
                return img;
            }
            catch
            {
                return null;
            }
        }
    }
}
