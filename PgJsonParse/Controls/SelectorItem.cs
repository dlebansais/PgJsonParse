using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace CustomControls
{
    public class SelectorItem : TabItem
    {
        #region Custom properties and events
        #region Value
        /// <summary>
        ///     Identifies the <see cref="Value"/> dependency property.
        /// </summary>
        /// <returns>
        ///     The identifier for the <see cref="Value"/> dependency property.
        /// </returns>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(bool), typeof(SelectorItem), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnValuePropertyChanged)));

        /// <summary>
        ///     Gets or sets the scroll viewer property to bind on.
        /// </summary>
        [Bindable(true)]
        public bool Value
        {
            get { return (bool)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SelectorItem ctrl = (SelectorItem)d;
            ctrl.OnValuePropertyChanged(e);
        }

        private void OnValuePropertyChanged(DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion
        #endregion

        #region Init
        public SelectorItem()
            : base()
        {
        }
        #endregion
    }
}
