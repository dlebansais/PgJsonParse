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
