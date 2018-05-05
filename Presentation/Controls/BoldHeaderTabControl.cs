using System.Windows;
using System.Windows.Controls;

namespace Presentation
{
    public class BoldHeaderTabControl : TabControl
    {
        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);

            foreach (TabItem Item in Items)
                SetHeaderFontWeight(Item, Item == SelectedItem ? FontWeights.Bold : FontWeights.Normal);
        }

        private void SetHeaderFontWeight(TabItem item, FontWeight fontWeight)
        {
            Control AsControl;
            if ((AsControl = item.Header as Control) != null)
                if (AsControl.FontWeight != fontWeight)
                    AsControl.FontWeight = fontWeight;
        }
    }
}
