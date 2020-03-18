namespace Selectors
{
    using System.Windows.Controls;
    using System.Windows;
    using PgJsonObjects;

    public class ItemEffectTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement Element = (FrameworkElement)container;
            DataTemplate Result;

            if (item is IPgItemSimpleEffect AsItemSimpleEffect)
                Result = (DataTemplate)Element.FindResource("ItemSimpleEffectTemplate");
            else if (item is IPgItemAttributeLink AsItemAttributeLink)
                Result = (DataTemplate)Element.FindResource("ItemAttributeLinkTemplate");
            else
                Result = base.SelectTemplate(item, container);

            return Result;
        }
    }
}
