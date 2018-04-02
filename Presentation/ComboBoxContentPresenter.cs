using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Presentation
{
    public class ComboBoxContentPresenter : ContentControl
    {
        public ComboBoxContentPresenter()
        {
            Initialized += OnInitialized;
        }

        public DataTemplate SelectionTemplate { get; set; }
        public DataTemplate SelectedItemTemplate { get; set; }

        void OnInitialized(object sender, EventArgs e)
        {
            Binding ContentBinding = new Binding("SelectionBoxItem");
            ContentBinding.RelativeSource = RelativeSource.TemplatedParent;
            SetBinding(ContentProperty, ContentBinding);

            ContentTemplate = SelectedItemTemplate;
        }
    }
}
