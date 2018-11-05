#if CSHTML5
using System.Collections.Generic;
using Windows.UI.Xaml;

namespace Presentation
{
    public class ComboBoxTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SelectedItemTemplate { get; set; }
        public DataTemplate DropdownItemsTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            ISelectorSelection AsComboBoxSelection;
            bool IsSelected = false;

            if ((AsComboBoxSelection = item as ISelectorSelection) != null)
                if (AsComboBoxSelection.Owner != null)
                {
                    List<ComboBox> Candidates = ComboBox.LoadedComboBoxList(AsComboBoxSelection.Owner);
                    foreach (ComboBox Candidate in Candidates)
                        if (Candidate.SelectedItem == item)
                        {
                            IsSelected = true;
                            break;
                        }
                }

            if (ReturnedTemplate == null)
                ReturnedTemplate = IsSelected ? SelectedItemTemplate : DropdownItemsTemplate;

            return ReturnedTemplate;
        }

        DataTemplate ReturnedTemplate;
    }
}
#else
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
#endif
