using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

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

        public ComboBox()
        {
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            DropDownToggle = PresentationEnvironment.EnumChildren(this, "DropDownToggle") as ToggleButton;

            Binding DropDownBinding = new Binding();
            DropDownBinding.Path = new PropertyPath("DropDownToggle.IsChecked");
            DropDownBinding.RelativeSource = RelativeSource.Self;
            SetBinding(IsDropDownOpenProperty, DropDownBinding);
        }

        public ToggleButton DropDownToggle { get; private set; }
    }
}
