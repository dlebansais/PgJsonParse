#if CSHTML5
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Presentation
{
    public class ComboBox : Windows.UI.Xaml.Controls.ComboBox
    {
        #region Custom properties and events
        #region ControlSelectedIndex
        /// <summary>
        ///     Identifies the <see cref="ControlSelectedIndex"/> dependency property.
        /// </summary>
        /// <returns>
        ///     The identifier for the <see cref="ControlSelectedIndex"/> dependency property.
        /// </returns>
        public static readonly DependencyProperty ControlSelectedIndexProperty = DependencyProperty.Register("ControlSelectedIndex", typeof(int), typeof(ComboBox), new PropertyMetadata(-1, new PropertyChangedCallback(OnControlSelectedIndexPropertyChanged)));

        /// <summary>
        ///     Gets or sets the ControlSelectedIndex property.
        /// </summary>
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
            {
                SelectedIndex = ControlSelectedIndex;
                SetSelectedItem(SelectedIndex);
            }
        }
        #endregion
        #endregion

        public ComboBox()
        {
            Loaded += OnLoaded;
            SelectionChanged += OnSelectionChanged;
        }

        public bool ShowSelectText { get { return false; } }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (ComboBoxTable.ContainsKey(this))
                return;

            DependencyObject Parent = this;
            while (Parent != null)
            {
                RootControl AsRootControl;
                if ((AsRootControl = Parent as RootControl) != null)
                {
                    ComboBoxTable.Add(this, AsRootControl);
                    break;
                }

                Parent = VisualTreeHelper.GetParent(Parent);
            }

            DropDownToggle = PresentationEnvironment.EnumChildren(this, "DropDownToggle") as ToggleButton;

            Binding DropDownBinding = new Binding("DropDownToggle.IsChecked");
            DropDownBinding.RelativeSource = new RelativeSource(RelativeSourceMode.Self);
            SetBinding(IsDropDownOpenProperty, DropDownBinding);
        }

        protected override void OnDropDownOpened(RoutedEventArgs e)
        {
            base.OnDropDownOpened(e);
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ControlSelectedIndex != SelectedIndex)
            {
                SetValue(ControlSelectedIndexProperty, SelectedIndex);
                SetSelectedItem(SelectedIndex);
            }
        }

        private void SetSelectedItem(int index)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                ISelectorSelection AsComboBoxSelection;
                if ((AsComboBoxSelection = Items[i] as ISelectorSelection) != null)
                    AsComboBoxSelection.IsSelected = (i == index);
            }
        }

        public ToggleButton DropDownToggle { get; private set; }

        private static Dictionary<ComboBox, RootControl> ComboBoxTable { get; } = new Dictionary<ComboBox, RootControl>();
        public static List<ComboBox> LoadedComboBoxList(RootControl root)
        {
            List<ComboBox> Result = new List<ComboBox>();
            foreach (KeyValuePair<ComboBox, RootControl> Entry in ComboBoxTable)
                if (Entry.Value == root)
                    Result.Add(Entry.Key);

            return Result;
        }
    }
}
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
            ComboBox Control = (ComboBox)d;
            Control.OnControlSelectedIndexPropertyChanged();
        }

        private void OnControlSelectedIndexPropertyChanged()
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