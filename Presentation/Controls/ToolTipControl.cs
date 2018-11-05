#if CSHTML5
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Presentation
{
    public class ToolTipControl : UserControl
    {
        #region Custom properties and events
        #region ToolTip
        /// <summary>
        ///     Identifies the <see cref="ToolTip"/> dependency property.
        /// </summary>
        /// <returns>
        ///     The identifier for the <see cref="ToolTip"/> dependency property.
        /// </returns>
        public static readonly DependencyProperty ToolTipProperty = DependencyProperty.Register("ToolTip", typeof(object), typeof(ToolTipControl), new PropertyMetadata(null));

        /// <summary>
        ///     Gets or sets the ToolTip property.
        /// </summary>
        public object ToolTip
        {
            get { return GetValue(ToolTipProperty); }
            set { SetValue(ToolTipProperty, value); }
        }
        #endregion
        #endregion
    }
}
#else
using System.Windows.Controls;

namespace Presentation
{
    public class ToolTipControl : UserControl
    {
    }
}
#endif
