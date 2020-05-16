namespace Selectors
{
    using System.Windows.Controls;
    using System.Windows;
    using PgBuilder;
    using System.Windows.Media;
    using PgJsonObjects;
    using System.Diagnostics;

    public class SkillTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement Element = (FrameworkElement)container;
            IPgSkill Skill = (IPgSkill)item;

            bool IsComboBoxItem = GetComboBoxItemParent(Element, out ComboBoxItem Parent);

            DataTemplate Result;
            if (Skill.ParentSkill != null && IsComboBoxItem)
                Result = (DataTemplate)Element.FindResource("SubskillTemplate");
            else
                Result = (DataTemplate)Element.FindResource("SkillTemplate");

            return Result;
        }

        public bool GetComboBoxItemParent(FrameworkElement element, out ComboBoxItem parent)
        {
            if (element is ComboBoxItem ComboBoxItemParent)
            {
                parent = ComboBoxItemParent;
                return true;
            }
            else if (element is ComboBox ComboBoxParent)
            {
                parent = null;
                return false;
            }
            else if (VisualTreeHelper.GetParent(element) is FrameworkElement ParentElement)
                return GetComboBoxItemParent(ParentElement, out parent);
            else
            {
                parent = null;
                return false;
            }
        }
    }
}
