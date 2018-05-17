using PgJsonObjects;
using Presentation;
#if CSHARP_XAML_FOR_HTML5
using Windows.UI.Xaml;
#else
using System.Windows;
#endif

namespace PgJsonParse
{
    public class SkillRewardSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;

            SkillRewardAbility AsSkillRewardAbility;
            SkillRewardBonusLevel AsSkillRewardBonusLevel;
            SkillRewardMisc AsSkillRewardMisc;
            SkillRewardRecipe AsSkillRewardRecipe;
            SkillRewardUnlock AsSkillRewardUnlock;

            if ((AsSkillRewardAbility = item as SkillRewardAbility) != null)
            {
                DataTemplate Result = FindTemplate(element, "SkillRewardAbilityTemplate");
                return Result;
            }

            else if ((AsSkillRewardBonusLevel = item as SkillRewardBonusLevel) != null)
            {
                DataTemplate Result = FindTemplate(element, "SkillRewardBonusLevelTemplate");
                return Result;
            }

            else if ((AsSkillRewardMisc = item as SkillRewardMisc) != null)
            {
                DataTemplate Result = FindTemplate(element, "SkillRewardMiscTemplate");
                return Result;
            }

            else if ((AsSkillRewardRecipe = item as SkillRewardRecipe) != null)
            {
                DataTemplate Result = FindTemplate(element, "SkillRewardRecipeTemplate");
                return Result;
            }

            else if ((AsSkillRewardUnlock = item as SkillRewardUnlock) != null)
            {
                DataTemplate Result = FindTemplate(element, "SkillRewardUnlockTemplate");
                return Result;
            }

            else
                return base.SelectTemplate(item, container);
        }
    }
}
