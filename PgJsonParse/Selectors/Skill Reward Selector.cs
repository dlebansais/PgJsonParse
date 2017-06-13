using PgJsonObjects;
using System.Windows;
using System.Windows.Controls;

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
                DataTemplate Result = element.TryFindResource("SkillRewardAbilityTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsSkillRewardBonusLevel = item as SkillRewardBonusLevel) != null)
            {
                DataTemplate Result = element.TryFindResource("SkillRewardBonusLevelTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsSkillRewardMisc = item as SkillRewardMisc) != null)
            {
                DataTemplate Result = element.TryFindResource("SkillRewardMiscTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsSkillRewardRecipe = item as SkillRewardRecipe) != null)
            {
                DataTemplate Result = element.TryFindResource("SkillRewardRecipeTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsSkillRewardUnlock = item as SkillRewardUnlock) != null)
            {
                DataTemplate Result = element.TryFindResource("SkillRewardUnlockTemplate") as DataTemplate;
                return Result;
            }

            else
                return null;
        }
    }
}
