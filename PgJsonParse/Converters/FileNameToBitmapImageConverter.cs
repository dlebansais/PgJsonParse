using PgJsonParse;
using Presentation;
using System.IO;
using System.Windows.Data;

namespace Converters
{
    [ValueConversion(typeof(string), typeof(object))]
    public class FileNameToBitmapImageConverter : GenericValueConverter
    {
        protected override object Convert(object value, object parameter)
        {
            string FileName;

            if ((FileName = value as string) != null)
            {
                App CurrentApp = App.Current as App;
                foreach (RootControl w in CurrentApp.Windows)
                {
                    MainWindow AsMainWindow;
                    if ((AsMainWindow = w as MainWindow) != null)
                    {
                        GameVersionInfo SelectedVersion = AsMainWindow.LoadedVersion;
                        string IconCacheFolder = AsMainWindow.IconCacheFolder;
                        FileName = Path.GetFileNameWithoutExtension(FileName) + ".png";
                        string FilePath = Path.Combine(IconCacheFolder, FileName);

                        return FileNameToBitmapImage.Convert(FilePath);
                    }
                }
            }

            return null;
        }
    }
}
