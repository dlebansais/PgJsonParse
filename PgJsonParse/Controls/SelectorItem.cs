using System.Windows;
using System.Windows.Controls;

namespace CustomControls
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
