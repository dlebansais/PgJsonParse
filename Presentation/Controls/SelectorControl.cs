using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
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
            if (IsBindingPossible())
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

        private bool IsBindingSet;
        #endregion
    }
}
