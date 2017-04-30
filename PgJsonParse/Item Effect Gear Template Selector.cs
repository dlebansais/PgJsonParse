using PgJsonObjects;
using System.Windows;
using System.Windows.Controls;

namespace PgJsonParse
{
    public class ItemEffectGearTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;

            ItemAttributeLink AsItemAttributeLink;
            ItemSimpleEffect AsItemSimpleEffect;

            if ((AsItemAttributeLink = item as ItemAttributeLink) != null)
            {
                DataTemplate Result = element.TryFindResource("AttributeLinkGearTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsItemSimpleEffect = item as ItemSimpleEffect) != null)
            {
                DataTemplate Result = element.TryFindResource("SimpleEffectGearTemplate") as DataTemplate;
                return Result;
            }

            else
                return null;
        }
    }
}
