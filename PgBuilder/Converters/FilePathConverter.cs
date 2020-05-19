namespace Converters
{
    using Presentation;
    using System.IO;
    using System.Windows.Data;

    [ValueConversion(typeof(string), typeof(string))]
    public class FilePathConverter : GenericValueConverter
    {
        protected override object Convert(object value, object parameter)
        {
            string FilePathValue = (string)value;
            return Path.GetFileNameWithoutExtension(FilePathValue);
        }
    }
}
