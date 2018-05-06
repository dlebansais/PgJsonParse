namespace Presentation
{
    public class ListBox : System.Windows.Controls.ListBox
    {
        public ListBox()
        {
            SetValue(System.Windows.Controls.ScrollViewer.HorizontalScrollBarVisibilityProperty, System.Windows.Controls.ScrollBarVisibility.Disabled);
        }
    }
}