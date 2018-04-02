using System;
using System.Windows;
using System.Windows.Controls;

namespace Presentation
{
    public class ComboBoxTemplateSelector : ContentControl
    {
        public DataTemplate SelectedItemTemplate { get; set; }
        public DataTemplate DropdownItemsTemplate { get; set; }

        public ComboBoxTemplateSelector()
        {
            Initialized += OnInitialized;
        }

        private void OnInitialized(object sender, EventArgs e)
        {
            ContentTemplate = DropdownItemsTemplate;
        }
    }
}
