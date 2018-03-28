using System.Windows;
using System.Windows.Controls;

namespace CustomControls
{
    public class BoldHeaderTabControl : TabControl
    {
        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);

            foreach (TabItem Item in Items)
                SetHeaderFontWeight(Item, Item == SelectedItem ? FontWeights.Bold : FontWeights.Normal);
        }

        private void SetHeaderFontWeight(TabItem Item, FontWeight FontWeight)
        {
            Control AsControl;
            if ((AsControl = Item.Header as Control) != null)
                if (AsControl.FontWeight != FontWeight)
                    AsControl.FontWeight = FontWeight;
        }
    }
}
