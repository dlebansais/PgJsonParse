using PgJsonObjects;
using Presentation;
#if CSHARP_XAML_FOR_HTML5
using Windows.UI.Xaml;
#else
using System.Windows;
#endif

namespace PgJsonParse
{
    public abstract class ItemEffectGearSelector : DataTemplateSelector
    {
        public abstract string Prefix { get; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;

            IPgItemAttributeLink AsItemAttributeLink;
            IPgItemSimpleEffect AsItemSimpleEffect;

            if ((AsItemAttributeLink = item as IPgItemAttributeLink) != null)
            {
                DataTemplate Result = FindTemplate(element, (Prefix ?? "") + "AttributeLinkGearTemplate");
                return Result;
            }

            else if ((AsItemSimpleEffect = item as IPgItemSimpleEffect) != null)
            {
                DataTemplate Result = FindTemplate(element, (Prefix ?? "") + "SimpleEffectGearTemplate");
                return Result;
            }

            else
                return base.SelectTemplate(item, container);
        }
    }
}
