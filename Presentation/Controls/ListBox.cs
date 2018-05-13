using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Presentation
{
    public class ListBox : System.Windows.Controls.ListBox
    {
        #region Custom properties and events
        #region ControlSelectedIndex
        /// <summary>
        ///     Identifies the <see cref="ControlSelectedIndex"/> dependency property.
        /// </summary>
        /// <returns>
        ///     The identifier for the <see cref="ControlSelectedIndex"/> dependency property.
        /// </returns>
        public static readonly DependencyProperty ControlSelectedIndexProperty = DependencyProperty.Register("ControlSelectedIndex", typeof(int), typeof(ListBox), new FrameworkPropertyMetadata(-1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnControlSelectedIndexPropertyChanged)));

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
            ListBox ctrl = (ListBox)d;
            ctrl.OnControlSelectedIndexPropertyChanged(e);
        }

        private void OnControlSelectedIndexPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (SelectedIndex != ControlSelectedIndex)
                SelectedIndex = ControlSelectedIndex;
        }
        #endregion
        #endregion

        public const int MaxCount = int.MaxValue;

        public ListBox()
        {
            SetValue(System.Windows.Controls.ScrollViewer.HorizontalScrollBarVisibilityProperty, System.Windows.Controls.ScrollBarVisibility.Disabled);
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);

            if (ControlSelectedIndex != SelectedIndex)
                SetValue(ControlSelectedIndexProperty, SelectedIndex);
        }
    }
}