using PgJsonObjects;
using Presentation;
using System.Windows;

namespace PgJsonParse
{
    public class SearchResultContentSelector : DataTemplateSelector
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
                DataTemplate Result = TryFindResource(element, "SearchResultAbilityContentTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsDirectedGoal = item as DirectedGoal) != null)
            {
                DataTemplate Result = TryFindResource(element, "SearchResultDirectedGoalContentTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsGameNpc = item as GameNpc) != null)
            {
                DataTemplate Result = TryFindResource(element, "SearchResultGameNpcContentTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsStorageVault = item as StorageVault) != null)
            {
                DataTemplate Result = TryFindResource(element, "SearchResultStorageVaultContentTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsEffect = item as Effect) != null)
            {
                DataTemplate Result = TryFindResource(element, "SearchResultEffectContentTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsItem = item as Item) != null)
            {
                DataTemplate Result = TryFindResource(element, "SearchResultItemContentTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsQuest = item as Quest) != null)
            {
                DataTemplate Result = TryFindResource(element, "SearchResultQuestContentTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsRecipe = item as Recipe) != null)
            {
                DataTemplate Result = TryFindResource(element, "SearchResultRecipeContentTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsSkill = item as Skill) != null)
            {
                DataTemplate Result = TryFindResource(element, "SearchResultSkillContentTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsPower = item as Power) != null)
            {
                DataTemplate Result = TryFindResource(element, "SearchResultPowerContentTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsLoreBook = item as LoreBook) != null)
            {
                DataTemplate Result = TryFindResource(element, "SearchResultLoreBookContentTemplate") as DataTemplate;
                return Result;
            }

            else
                return base.SelectTemplate(item, container);
        }
    }
}
