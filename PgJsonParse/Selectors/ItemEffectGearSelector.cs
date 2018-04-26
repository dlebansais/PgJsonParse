using PgJsonObjects;
using Presentation;
using System.Windows;

namespace PgJsonParse
{
    public class ItemEffectGearSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;

            ItemAttributeLink AsItemAttributeLink;
            ItemSimpleEffect AsItemSimpleEffect;

            if ((AsItemAttributeLink = item as ItemAttributeLink) != null)
            {
                DataTemplate Result = TryFindResource(element, "AttributeLinkGearTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsItemSimpleEffect = item as ItemSimpleEffect) != null)
            {
                DataTemplate Result = TryFindResource(element, "SimpleEffectGearTemplate") as DataTemplate;
                return Result;
            }

            else
                return base.SelectTemplate(item, container);
        }
    }
}
