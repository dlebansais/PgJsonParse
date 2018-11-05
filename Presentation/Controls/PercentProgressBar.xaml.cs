#if CSHTML5
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Presentation
{
    public partial class PercentProgressBar : UserControl, INotifyPropertyChanged
    {
        // List of all existing controls of this type.
        public static List<PercentProgressBar> ControlList { get; } = new List<PercentProgressBar>();

        // Use NaN to make sure that the control is updated as soon as a correct value is entered.
        private const double DefaultValue = double.NaN;

        #region Init
        public PercentProgressBar()
        {
            ControlList.Add(this);
            InitializeComponent();

            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            NotifyPropertyChanged(nameof(Value));
        }
        #endregion

        #region Custom properties and events
        #region Value
        /// <summary>
        ///     Identifies the <see cref="Value"/> dependency property.
        /// </summary>
        /// <returns>
        ///     The identifier for the <see cref="Value"/> dependency property.
        /// </returns>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double), typeof(PercentProgressBar), new PropertyMetadata(DefaultValue, OnValueChanged));

        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PercentProgressBar ctrl = (PercentProgressBar)d;
            ctrl.OnValueChanged(e);
        }

        private void OnValueChanged(DependencyPropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(nameof(Value));
        }
        #endregion
        #region Dock
        /// <summary>
        ///     Identifies the <see cref="Dock"/> dependency property.
        /// </summary>
        /// <returns>
        ///     The identifier for the <see cref="Dock"/> dependency property.
        /// </returns>
        public static readonly DependencyProperty DockProperty = DependencyProperty.Register("Dock", typeof(Dock), typeof(PercentProgressBar), new PropertyMetadata(Dock.Left));

        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        public Dock Dock
        {
            get { return (Dock)GetValue(DockProperty); }
            set { SetValue(DockProperty, value); }
        }
        #endregion
        #endregion

        #region Implementation of INotifyPropertyChanged
        /// <summary>
        ///     Implements the PropertyChanged event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        internal void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "Default parameter is mandatory with [CallerMemberName]")]
        internal void NotifyThisPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
#else
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace Presentation
{
    public partial class PercentProgressBar : UserControl, INotifyPropertyChanged
    {
        // List of all existing controls of this type.
        public static List<PercentProgressBar> ControlList { get; } = new List<PercentProgressBar>();

        // Use NaN to make sure that the control is updated as soon as a correct value is entered.
        private const double DefaultValue = double.NaN;

        #region Init
        public PercentProgressBar()
        {
            InitializeComponent();

            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            ControlList.Add(this);
            NotifyPropertyChanged(nameof(Value));
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            ControlList.Remove(this);
        }
        #endregion

        #region Custom properties and events
        #region Value
        /// <summary>
        ///     Identifies the <see cref="Value"/> dependency property.
        /// </summary>
        /// <returns>
        ///     The identifier for the <see cref="Value"/> dependency property.
        /// </returns>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double), typeof(PercentProgressBar), new FrameworkPropertyMetadata(DefaultValue, OnValueChanged));

        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        [Bindable(true)]
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PercentProgressBar ctrl = (PercentProgressBar)d;
            ctrl.OnValueChanged(e);
        }

        private void OnValueChanged(DependencyPropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(nameof(Value));
        }
        #endregion
        #region Dock
        /// <summary>
        ///     Identifies the <see cref="Dock"/> dependency property.
        /// </summary>
        /// <returns>
        ///     The identifier for the <see cref="Dock"/> dependency property.
        /// </returns>
        public static readonly DependencyProperty DockProperty = DependencyProperty.Register("Dock", typeof(Dock), typeof(PercentProgressBar), new FrameworkPropertyMetadata(Dock.Left));

        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        public Dock Dock
        {
            get { return (Dock)GetValue(DockProperty); }
            set { SetValue(DockProperty, value); }
        }
        #endregion
        #endregion

        #region Implementation of INotifyPropertyChanged
        /// <summary>
        ///     Implements the PropertyChanged event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        internal void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "Default parameter is mandatory with [CallerMemberName]")]
        internal void NotifyThisPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
#endif
