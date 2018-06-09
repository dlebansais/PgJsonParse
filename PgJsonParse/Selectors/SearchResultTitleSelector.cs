using PgJsonObjects;
using Presentation;
#if CSHARP_XAML_FOR_HTML5
using Windows.UI.Xaml;
#else
using System.Windows;
#endif

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
            PlayerTitle AsPlayerTitle;
            LoreBook AsLoreBook;

            if ((AsAbility = item as Ability) != null)
            {
                DataTemplate Result = FindTemplate(element, "SearchResultAbilityTitleTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsDirectedGoal = item as DirectedGoal) != null)
            {
                DataTemplate Result = FindTemplate(element, "SearchResultDirectedGoalTitleTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsGameNpc = item as GameNpc) != null)
            {
                DataTemplate Result = FindTemplate(element, "SearchResultGameNpcTitleTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsStorageVault = item as StorageVault) != null)
            {
                DataTemplate Result = FindTemplate(element, "SearchResultStorageVaultTitleTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsEffect = item as Effect) != null)
            {
                DataTemplate Result = FindTemplate(element, "SearchResultEffectTitleTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsItem = item as Item) != null)
            {
                DataTemplate Result = FindTemplate(element, "SearchResultItemTitleTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsQuest = item as Quest) != null)
            {
                DataTemplate Result = FindTemplate(element, "SearchResultQuestTitleTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsRecipe = item as Recipe) != null)
            {
                DataTemplate Result = FindTemplate(element, "SearchResultRecipeTitleTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsSkill = item as Skill) != null)
            {
                DataTemplate Result = FindTemplate(element, "SearchResultSkillTitleTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsPower = item as Power) != null)
            {
                DataTemplate Result = FindTemplate(element, "SearchResultPowerTitleTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsPlayerTitle = item as PlayerTitle) != null)
            {
                DataTemplate Result = FindTemplate(element, "SearchResultPlayerTitleTitleTemplate") as DataTemplate;
                return Result;
            }

            else if ((AsLoreBook = item as LoreBook) != null)
            {
                DataTemplate Result = FindTemplate(element, "SearchResultLoreBookTitleTemplate") as DataTemplate;
                return Result;
            }

            else
                return base.SelectTemplate(item, container);
        }
    }
}
