#if CSHTML5
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Presentation
{
    public class ComboBoxContentPresenter : ContentPresenter
    {
        public DataTemplate SelectionTemplate { get; set; }
        public DataTemplate SelectedItemTemplate { get; set; }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            if (ContentTemplate == null)
                ContentTemplate = SelectionTemplate;
            else if (oldContent == null || newContent == null)
                return;

            base.OnContentChanged(oldContent, newContent);
        }
    }
}
#else
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
#endif