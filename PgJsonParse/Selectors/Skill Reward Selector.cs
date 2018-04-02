using PgJsonObjects;
using Presentation;
using System.Windows;

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
                DataTemplate Result = TryFindResource(element, "SkillRewardAbilityTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsSkillRewardBonusLevel = item as SkillRewardBonusLevel) != null)
            {
                DataTemplate Result = TryFindResource(element, "SkillRewardBonusLevelTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsSkillRewardMisc = item as SkillRewardMisc) != null)
            {
                DataTemplate Result = TryFindResource(element, "SkillRewardMiscTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsSkillRewardRecipe = item as SkillRewardRecipe) != null)
            {
                DataTemplate Result = TryFindResource(element, "SkillRewardRecipeTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsSkillRewardUnlock = item as SkillRewardUnlock) != null)
            {
                DataTemplate Result = TryFindResource(element, "SkillRewardUnlockTemplate") as DataTemplate;
                return Result;
            }

            else
                return base.SelectTemplate(item, container);
        }
    }
}
