#if CSHTML5
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Presentation
{
    public class CloseButton : Button
    {
        public CloseButton()
        {
            Visibility = Visibility.Collapsed;
        }
    }
}
#else
using System.Windows.Controls;
using System.Windows.Input;

namespace Presentation
{
    public class CloseButton : Button
    {
        public CloseButton()
        {
            Command = ApplicationCommands.Close;
        }
    }
}
#endif
