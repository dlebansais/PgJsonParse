namespace Selectors
{
    using System.Windows.Controls;
    using System.Windows;
    using PgBuilder;

    public class OtherEffectTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement Element = (FrameworkElement)container;
            DataTemplate Result;

            if (item is OtherEffectSimple)
                Result = (DataTemplate)Element.FindResource("OtherEffectSimpleTemplate");
            else if (item is OtherEffectDoT)
                Result = (DataTemplate)Element.FindResource("OtherEffectDoTTemplate");
            else
                Result = base.SelectTemplate(item, container);

            return Result;
        }
    }
}
