#if CSHTML5
#else
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
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
        [Bindable(true)]
        public bool UseNativeComboBox
        {
            get { return (bool)GetValue(UseNativeComboBoxProperty); }
            set { SetValue(UseNativeComboBoxProperty, value); }
        }
        #endregion
        #region ControlSelectedIndex
        /// <summary>
        ///     Identifies the <see cref="ControlSelectedIndex"/> dependency property.
        /// </summary>
        /// <returns>
        ///     The identifier for the <see cref="ControlSelectedIndex"/> dependency property.
        /// </returns>
        public static readonly DependencyProperty ControlSelectedIndexProperty = DependencyProperty.Register("ControlSelectedIndex", typeof(int), typeof(ComboBox), new FrameworkPropertyMetadata(-1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnControlSelectedIndexPropertyChanged)));

        /// <summary>
        ///     Gets or sets the ControlSelectedIndex property.
        /// </summary>
        [Bindable(true)]
        public int ControlSelectedIndex
        {
            get { return (int)GetValue(ControlSelectedIndexProperty); }
            set { SetValue(ControlSelectedIndexProperty, value); }
        }

        private static void OnControlSelectedIndexPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ComboBox ctrl = (ComboBox)d;
            ctrl.OnControlSelectedIndexPropertyChanged(e);
        }

        private void OnControlSelectedIndexPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (SelectedIndex != ControlSelectedIndex)
                SelectedIndex = ControlSelectedIndex;
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

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);

            if (ControlSelectedIndex != SelectedIndex)
                SetValue(ControlSelectedIndexProperty, SelectedIndex);
        }

        public ToggleButton DropDownToggle { get; private set; }
    }
}
#endif