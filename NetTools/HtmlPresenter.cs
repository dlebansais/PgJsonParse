using Windows.UI.Xaml;

namespace NetTools
{
#if USE_RESTRICTED_FEATURES
    public class HtmlPresenter : CSHTML5.Native.Html.Controls.HtmlPresenter
#else
    public class HtmlPresenter : Windows.UI.Xaml.FrameworkElement
#endif
    {
        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(string), typeof(HtmlPresenter), new PropertyMetadata(null, OnContentChanged));

        public string Content
        {
#if USE_RESTRICTED_FEATURES
            get { return Html; }
#else
            get; set;
#endif
        }

        private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            HtmlPresenter Ctrl = (HtmlPresenter)d;
            string Content = (string)e.NewValue;

#if USE_RESTRICTED_FEATURES
            Ctrl.Html = Content;
#endif
        }
    }
}
