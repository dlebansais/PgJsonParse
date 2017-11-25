using PgJsonObjects;
using System.Windows;
using System.Windows.Controls;

namespace PgJsonParse
{
    public class SearchResultTitleSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;

            Ability AsAbility;
            DirectedGoal AsDirectedGoal;
            GameNpc AsGameNpc;
            StorageVault AsStorageVault;
            Effect AsEffect;
            Item AsItem;
            Quest AsQuest;
            Recipe AsRecipe;
            Skill AsSkill;
            Power AsPower;

            if ((AsAbility = item as Ability) != null)
            {
                DataTemplate Result = element.TryFindResource("SearchResultAbilityTitleTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsDirectedGoal = item as DirectedGoal) != null)
            {
                DataTemplate Result = element.TryFindResource("SearchResultDirectedGoalTitleTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsGameNpc = item as GameNpc) != null)
            {
                DataTemplate Result = element.TryFindResource("SearchResultGameNpcTitleTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsStorageVault = item as StorageVault) != null)
            {
                DataTemplate Result = element.TryFindResource("SearchResultStorageVaultTitleTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsEffect = item as Effect) != null)
            {
                DataTemplate Result = element.TryFindResource("SearchResultEffectTitleTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsItem = item as Item) != null)
            {
                DataTemplate Result = element.TryFindResource("SearchResultItemTitleTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsQuest = item as Quest) != null)
            {
                DataTemplate Result = element.TryFindResource("SearchResultQuestTitleTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsRecipe = item as Recipe) != null)
            {
                DataTemplate Result = element.TryFindResource("SearchResultRecipeTitleTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsSkill = item as Skill) != null)
            {
                DataTemplate Result = element.TryFindResource("SearchResultSkillTitleTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsPower = item as Power) != null)
            {
                DataTemplate Result = element.TryFindResource("SearchResultPowerTitleTemplate") as DataTemplate;
                return Result;
            }

            else
                return null;
        }
    }
}
