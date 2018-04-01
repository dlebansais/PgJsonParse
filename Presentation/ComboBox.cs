using System.Windows;

namespace Presentation
{
    public class ComboBox : System.Windows.Controls.ComboBox
    {
        #region Custom properties and events
        #region UseNativeComboBox
        /// <summary>
        ///     Identifies the <see cref="UseNativeComboBox"/> dependency property.
        /// </summary>
        /// <returns>
        ///     The identifier for the <see cref="UseNativeComboBox"/> dependency property.
        /// </returns>
        public static readonly DependencyProperty UseNativeComboBoxProperty = DependencyProperty.Register("UseNativeComboBox", typeof(bool), typeof(ComboBox), new FrameworkPropertyMetadata(false));

        /// <summary>
        ///     Gets or sets the UseNativeComboBox property.
        /// </summary>
        public bool UseNativeComboBox
        {
            get { return (bool)GetValue(UseNativeComboBoxProperty); }
            set { SetValue(UseNativeComboBoxProperty, value); }
        }
        #endregion
        #endregion
    }
}
