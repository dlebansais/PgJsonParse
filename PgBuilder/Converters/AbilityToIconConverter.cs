namespace Converters
{
    using System.Windows.Data;
    using System.Windows.Media;
    using System.IO;
    using Presentation;
    using PgBuilder;
    using PgJsonObjects;

    [ValueConversion(typeof(IPgAbility), typeof(ImageSource))]
    public class AbilityToIconConverter : GenericValueConverter
    {
        protected override object Convert(object value, object parameter)
        {
            IPgAbility AbilityValue = (IPgAbility) value;
            if (AbilityValue != null)
            {
                int IconId = AbilityValue.IconId;
                string IconFile = Path.Combine(MainWindow.IconFolder, $"icon_{IconId}.png");
                ImageSource Source = ImageConversion.IconFileToImageSource(IconFile);

                return Source;
            }
            else
                return null;
        }
    }
}
