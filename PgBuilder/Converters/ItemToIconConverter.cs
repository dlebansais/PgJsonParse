namespace Converters
{
    using System.Windows.Data;
    using System.Windows.Media;
    using System.IO;
    using Presentation;
    using PgBuilder;
    using PgJsonObjects;

    [ValueConversion(typeof(IPgItem), typeof(ImageSource))]
    public class ItemToIconConverter : GenericValueConverter
    {
        protected override object Convert(object value, object parameter)
        {
            IPgItem ItemValue = (IPgItem) value;
            if (ItemValue != null)
            {
                int IconId = ItemValue.IconId;
                string IconFile = Path.Combine(App.IconFolder, $"icon_{IconId}.png");
                ImageSource Source = ImageConversion.IconFileToImageSource(IconFile);

                return Source;
            }
            else
                return null;
        }
    }
}
