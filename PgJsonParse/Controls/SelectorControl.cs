using Converters;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace CustomControls
{
    public class SelectorControl : TabControl
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
            UpdateBindings();
        }
        #endregion
        #endregion

        #region Initialization
        public SelectorControl()
            : base()
        {
            BorderThickness = new Thickness();
            Background = Brushes.Transparent;
            Style NoHeaderStyle = new Style();
            Setter NoHeaderSetter = new Setter();
            NoHeaderSetter.Property = VisibilityProperty;
            NoHeaderSetter.Value = Visibility.Collapsed;
            NoHeaderStyle.TargetType = typeof(TabItem);
            NoHeaderStyle.Setters.Add(NoHeaderSetter);
            ItemContainerStyle = NoHeaderStyle;

            IsBindingDecided = false;
            IsBindingSet = false;
            DataContextChanged += OnDataContextChanged;
            LayoutUpdated += OnLayoutUpdated;
            Initialized += OnInitialized;
            SizeChanged += OnSizeChanged;
        }
        #endregion

        #region Implementation
        private void OnInitialized(object sender, EventArgs e)
        {
            //Debug.Print("Initialized");

            if (!IsBindingDecided)
                UpdateBindings();
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            //Debug.Print("Size changed for " + (Selector == null ? "" : Selector) + ", Width: " + Width + ", Height: " + Height + ", ActualWidth: " + ActualWidth + ", ActualHeight: " + ActualHeight);

            if (double.IsNaN(Width) && SelectedWidth < ActualWidth)
                SelectedWidth = ActualWidth;
            if (double.IsNaN(Height) && SelectedHeight < ActualHeight)
                SelectedHeight = ActualHeight;
        }

        /// <summary>
        ///     Binds the SelectedIndex properties to the new selected property.
        /// </summary>
        private void UpdateBindings()
        {
            if (DataContext == null)
            {
                //Debug.Print("Stop because DataContext == null");
                return;
            }
            if (Items.Count == 0)
            {
                //Debug.Print("Stop because Items.Count == 0");
                return;
            }

            IsBindingDecided = true;
            SelectedIndex = 0;

            //Debug.Print("Binding decided for " + Selector);
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //Debug.Print("DataContext changed");

            if (!IsBindingDecided)
                UpdateBindings();
        }

        private void OnLayoutUpdated(object sender, EventArgs e)
        {
            if (IsBindingDecided && !IsBindingSet)
            {
                if (SelectedIndex + 1 < Items.Count)
                {
                    SelectedIndex = SelectedIndex + 1;
                    return;
                }
                else
                {
                    SetBinding();
                    FreezeSize();
                }
            }
        }

        private void SetBinding()
        {
            if (Selector == null)
                BindingOperations.ClearBinding(this, SelectedIndexProperty);
            else
            {
                const string ErrorMessage = "A SelectorControl can only contain exactly two SelectorItem children, one with Value=True and the other with Value=Talse";
                bool[] ItemValues = new bool[2] { false, false };
                int Index = 0;

                foreach (object Item in Items)
                {
                    SelectorItem AsSelectorItem;
                    if ((AsSelectorItem = Item as SelectorItem) != null)
                        if (Index < ItemValues.Length)
                            ItemValues[Index++] = AsSelectorItem.Value;
                        else
                            throw new InvalidOperationException(ErrorMessage);
                    else
                        throw new InvalidOperationException(ErrorMessage);
                }

                if (Index != ItemValues.Length)
                    throw new InvalidOperationException(ErrorMessage);

                for (Index = 0; Index + 1 < ItemValues.Length; Index++)
                    if (ItemValues[Index] == ItemValues[Index + 1])
                        throw new InvalidOperationException(ErrorMessage);

                Binding NewBinding = new Binding(Selector) { Source = DataContext, Mode = BindingMode.OneWay, Converter = new ObjectToIndexConverter(), ConverterParameter = ItemValues };
                SetBinding(SelectedIndexProperty, NewBinding);
            }

            IsBindingSet = true;
            //Debug.Print("Binding set for " + Selector);
        }

        private void FreezeSize()
        {
            if (double.IsNaN(Width))
                Width = SelectedWidth;
            if (double.IsNaN(Height))
                Height = SelectedHeight;
        }

        private bool IsBindingDecided;
        private bool IsBindingSet;
        private double SelectedWidth;
        private double SelectedHeight;
        #endregion
    }
}
