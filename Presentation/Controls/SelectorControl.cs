#if CSHTML5
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace Presentation
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
        public static readonly DependencyProperty SelectorProperty = DependencyProperty.Register("Selector", typeof(string), typeof(SelectorControl), new PropertyMetadata(null));

        /// <summary>
        ///     Gets or sets the scroll viewer property to bind on.
        /// </summary>
        public string Selector
        {
            get { return (string)GetValue(SelectorProperty); }
            set { SetValue(SelectorProperty, value); }
        }
        #endregion
        #region FreezeWidth
        /// <summary>
        ///     Identifies the <see cref="FreezeWidth"/> dependency property.
        /// </summary>
        /// <returns>
        ///     The identifier for the <see cref="FreezeWidth"/> dependency property.
        /// </returns>
        public static readonly DependencyProperty FreezeWidthProperty = DependencyProperty.Register("FreezeWidth", typeof(bool), typeof(SelectorControl), new PropertyMetadata(false));

        /// <summary>
        ///     Gets or sets the scroll viewer property to bind on.
        /// </summary>
        public bool FreezeWidth
        {
            get { return (bool)GetValue(FreezeWidthProperty); }
            set { SetValue(FreezeWidthProperty, value); }
        }
        #endregion
        #region FreezeHeight
        /// <summary>
        ///     Identifies the <see cref="FreezeHeight"/> dependency property.
        /// </summary>
        /// <returns>
        ///     The identifier for the <see cref="FreezeHeight"/> dependency property.
        /// </returns>
        public static readonly DependencyProperty FreezeHeightProperty = DependencyProperty.Register("FreezeHeight", typeof(bool), typeof(SelectorControl), new PropertyMetadata(true));

        /// <summary>
        ///     Gets or sets the scroll viewer property to bind on.
        /// </summary>
        public bool FreezeHeight
        {
            get { return (bool)GetValue(FreezeHeightProperty); }
            set { SetValue(FreezeHeightProperty, value); }
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

            if (double.IsNaN(Width) && SelectedWidth > 0 && FreezeWidth)
            {
                Width = SelectedWidth;
                //Debug.WriteLine("Width set to " + SelectedWidth);
            }

            if (double.IsNaN(Height) && SelectedHeight > 0 && FreezeHeight)
            {
                Height = SelectedHeight;
                //Debug.WriteLine("Height set to " + SelectedHeight);
            }

            if (IsBindingPossible())
                SetBinding();
        }

        private bool IsBindingPossible()
        {
            if (DataContext == null)
            {
                //Debug.WriteLine("************* " + (Selector == null ? "*" : Selector) + ": Stop because DataContext == null");
                return false;
            }

            if (Children.Count == 0)
            {
                //Debug.WriteLine("************* " + (Selector == null ? "*" : Selector) + ": Stop because Items.Count == 0");
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
#else
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;

namespace Presentation
{
    public class SelectorControl : Grid
    {
#region Initialization
        public SelectorControl()
        {
            IsBindingSet = false;
            SizeChanged += OnSizeChanged;
            Loaded += OnLoaded;
            DataContextChanged += OnDataContextChanged;
        }
#endregion

#region Properties
        public string Selector { get; set; }
#endregion

#region Size
        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            //Debug.WriteLine((Selector == null ? "*" : Selector) + ": Size changed, Width: " + Width + ", Height: " + Height + ", ActualWidth: " + ActualWidth + ", ActualHeight: " + ActualHeight);

            if (!double.IsNaN(ActualWidth) && ActualWidth > SelectedWidth)
                SelectedWidth = ActualWidth;

            if (!double.IsNaN(ActualHeight) && ActualHeight > SelectedHeight)
                SelectedHeight = ActualHeight;

            IsSizeSet = true;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            //Debug.WriteLine((Selector == null ? "*" : Selector) + ": Loaded, Width: " + Width + ", Height: " + Height + ", ActualWidth: " + ActualWidth + ", ActualHeight: " + ActualHeight);

            if (double.IsNaN(Width) && SelectedWidth > 0)
                Width = SelectedWidth;

            if (double.IsNaN(Height) && SelectedHeight > 0)
                Height = SelectedHeight;

            if (IsBindingPossible())
                SetBinding();
            else
                TryBindingAgainLater();
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsSizeSet && IsBindingPossible())
                SetBinding();
        }

        private double SelectedWidth;
        private double SelectedHeight;
#endregion

#region Binding
        private bool IsBindingPossible()
        {
            if (IsBindingSet)
                return true;

            if (DataContext == null)
            {
                //Debug.WriteLine("************* " + (Selector == null ? "*" : Selector) + ": Stop because DataContext == null");
                return false;
            }

            if (DataContext as string == "")
            {
                //Debug.WriteLine("************* " + (Selector == null ? "*" : Selector) + ": Stop because DataContext is empty");
                return false;
            }

            if (Children.Count == 0)
            {
                //Debug.WriteLine("************* " + (Selector == null ? "*" : Selector) + ": Stop because Items.Count == 0");
                return false;
            }

            return true;
        }

        private void TryBindingAgainLater()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new Action(() => BindAgain()));
        }

        private void BindAgain()
        {
            if (IsBindingPossible())
                SetBinding();
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
            //Debug.WriteLine("************* " + (Selector == null ? "*" : Selector) + ": Binding set on Source=" + DataContext.ToString());
        }

        private bool IsSizeSet;
        private bool IsBindingSet;
#endregion
    }
}
#endif
