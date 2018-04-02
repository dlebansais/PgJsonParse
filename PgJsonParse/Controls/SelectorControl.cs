using Converters;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CustomControls
{
    public class SelectorControl : Grid
    {
        #region Custom properties and events
        #region Selector
        /// <summary>
        ///     Identifies the <see cref="Selector"/> dependency property.
        /// </summary>
        /// <returns>
        ///     The identifier for the <see cref="Selector"/> dependency property.
        /// </returns>
        public static readonly DependencyProperty SelectorProperty = DependencyProperty.Register("Selector", typeof(string), typeof(SelectorControl), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnSelectorPropertyChanged)));

        /// <summary>
        ///     Gets or sets the scroll viewer property to bind on.
        /// </summary>
        [Bindable(true)]
        public string Selector
        {
            get { return (string)GetValue(SelectorProperty); }
            set { SetValue(SelectorProperty, value); }
        }

        private static void OnSelectorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SelectorControl ctrl = (SelectorControl)d;
            ctrl.OnSelectorPropertyChanged(e);
        }

        private void OnSelectorPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (IsBindingPossible(false))
                SetBinding();
        }
        #endregion
        #endregion

        #region Initialization
        public SelectorControl()
        {
            IsBindingSet = false;
            SizeChanged += OnSizeChanged;
            Loaded += OnLoaded;
        }
        #endregion

        #region Implementation
        private double SelectedWidth;
        private double SelectedHeight;

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            //Debug.WriteLine((Selector == null ? "*" : Selector) + ": Size changed, Width: " + Width + ", Height: " + Height + ", ActualWidth: " + ActualWidth + ", ActualHeight: " + ActualHeight);

            if (!double.IsNaN(ActualWidth) && ActualWidth > SelectedWidth)
                SelectedWidth = ActualWidth;

            if (!double.IsNaN(ActualHeight) && ActualHeight > SelectedHeight)
                SelectedHeight = ActualHeight;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            //Debug.WriteLine((Selector == null ? "*" : Selector) + ": Loaded, Width: " + Width + ", Height: " + Height + ", ActualWidth: " + ActualWidth + ", ActualHeight: " + ActualHeight);

            if (double.IsNaN(Width) && SelectedWidth > 0)
                Width = SelectedWidth;

            if (double.IsNaN(Height) && SelectedHeight > 0)
                Height = SelectedHeight;

            if (IsBindingPossible(true))
                SetBinding();
        }

        private bool IsBindingPossible(bool DisplayDiagnostic)
        {
            if (DataContext == null)
            {
                //if (DisplayDiagnostic)
                //    Debug.WriteLine("************* " + (Selector == null ? "*" : Selector) + ": Stop because DataContext == null");
                return false;
            }

            if (Children.Count == 0)
            {
                //if (DisplayDiagnostic)
                //    Debug.WriteLine("************* " + (Selector == null ? "*" : Selector) + ": Stop because Items.Count == 0");
                return false;
            }

            return true;
        }

        private void SetBinding()
        {
            if (IsBindingSet)
                return;

            foreach (object Item in Children)
            {
                SelectorItem AsSelectorItem = Item as SelectorItem;
                Binding NewBinding = new Binding(Selector) { Source = DataContext, Mode = BindingMode.OneWay, Converter = new ObjectToIndexConverter(), ConverterParameter = AsSelectorItem.Value };
                AsSelectorItem.SetBinding(VisibilityProperty, NewBinding);
            }

            IsBindingSet = true;
            //Debug.WriteLine("************* " + (Selector == null ? "*" : Selector) + ": Binding set");
        }

        private bool IsBindingSet;
        #endregion
    }
}
