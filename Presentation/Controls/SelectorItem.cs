#if CSHTML5
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Presentation
{
    public class SelectorItem : ContentControl
    {
        #region Custom properties and events
        #region Value
        /// <summary>
        ///     Identifies the <see cref="Value"/> dependency property.
        /// </summary>
        /// <returns>
        ///     The identifier for the <see cref="Value"/> dependency property.
        /// </returns>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(object), typeof(SelectorItem), new PropertyMetadata(null));

        /// <summary>
        ///     Gets or sets the Value property.
        /// </summary>
        public object Value
        {
            get { return GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        #endregion
        #endregion
    }
}
#else
using System.Windows;
using System.Windows.Controls;

namespace Presentation
{
    public class SelectorItem : ContentControl
    {
        #region Custom properties and events
        #region Value
        /// <summary>
        ///     Identifies the <see cref="Value"/> dependency property.
        /// </summary>
        /// <returns>
        ///     The identifier for the <see cref="Value"/> dependency property.
        /// </returns>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(object), typeof(SelectorItem), new FrameworkPropertyMetadata(null));

        /// <summary>
        ///     Gets or sets the Value property.
        /// </summary>
        public object Value
        {
            get { return GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        #endregion
        #endregion
    }
}
#endif
