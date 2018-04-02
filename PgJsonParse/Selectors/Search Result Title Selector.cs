using PgJsonObjects;
using Presentation;
using System.Windows;

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
            LoreBook AsLoreBook;

            if ((AsAbility = item as Ability) != null)
            {
                DataTemplate Result = TryFindResource(element, "SearchResultAbilityTitleTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsDirectedGoal = item as DirectedGoal) != null)
            {
                DataTemplate Result = TryFindResource(element, "SearchResultDirectedGoalTitleTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsGameNpc = item as GameNpc) != null)
            {
                DataTemplate Result = TryFindResource(element, "SearchResultGameNpcTitleTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsStorageVault = item as StorageVault) != null)
            {
                DataTemplate Result = TryFindResource(element, "SearchResultStorageVaultTitleTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsEffect = item as Effect) != null)
            {
                DataTemplate Result = TryFindResource(element, "SearchResultEffectTitleTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsItem = item as Item) != null)
            {
                DataTemplate Result = TryFindResource(element, "SearchResultItemTitleTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsQuest = item as Quest) != null)
            {
                DataTemplate Result = TryFindResource(element, "SearchResultQuestTitleTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsRecipe = item as Recipe) != null)
            {
                DataTemplate Result = TryFindResource(element, "SearchResultRecipeTitleTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsSkill = item as Skill) != null)
            {
                DataTemplate Result = TryFindResource(element, "SearchResultSkillTitleTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsPower = item as Power) != null)
            {
                DataTemplate Result = TryFindResource(element, "SearchResultPowerTitleTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsLoreBook = item as LoreBook) != null)
            {
                DataTemplate Result = TryFindResource(element, "SearchResultLoreBookTitleTemplate") as DataTemplate;
                return Result;
            }

            else
                return base.SelectTemplate(item, container);
        }
    }
}
