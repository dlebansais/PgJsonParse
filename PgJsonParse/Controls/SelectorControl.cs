using Converters;
using PgJsonParse;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

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
        }
        #endregion

        #region Implementation
        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            //Debug.WriteLine((Selector == null ? "*" : Selector) + ": Size changed, Width: " + Width + ", Height: " + Height + ", ActualWidth: " + ActualWidth + ", ActualHeight: " + ActualHeight);

            if (double.IsNaN(Width) && !double.IsNaN(ActualWidth) && ActualWidth > 0)
                Width = ActualWidth;

            if (double.IsNaN(Height) && !double.IsNaN(ActualHeight) && ActualHeight > 0)
                Height = ActualHeight;

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
