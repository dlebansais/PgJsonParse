#if CSHTML5
using System;

namespace Presentation
{
    public class Popup : Windows.UI.Xaml.Controls.Primitives.Popup
    {
        public Popup()
        {
        }

        protected virtual void NotifyOpened()
        {
            Opened?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void NotifyClosed()
        {
            Closed?.Invoke(this, EventArgs.Empty);
        }

        public bool AllowsTransparency { get; set; }
        public bool Focusable { get; set; }
        public bool StaysOpen { get; set; }

        public event EventHandler Opened;
        public event EventHandler Closed;
    }
}
#else
namespace Presentation
{
    public class Popup : System.Windows.Controls.Primitives.Popup
    {
    }
}
#endif
