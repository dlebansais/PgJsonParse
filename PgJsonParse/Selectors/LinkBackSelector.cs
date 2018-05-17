using PgJsonObjects;
using Presentation;
using System;
using System.Collections.Generic;
#if CSHARP_XAML_FOR_HTML5
using Windows.UI.Xaml;
#else
using System.Windows;
#endif

namespace PgJsonParse
{
    public class LinkBackSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;
            if (!(item is KeyValuePair<Type, List<GenericJsonObject>>))
                return null;

            KeyValuePair<Type, List<GenericJsonObject>> ItemKeyValue = (KeyValuePair<Type, List<GenericJsonObject>>)item;

            if (ItemKeyValue.Key == typeof(Ability))
            {
                DataTemplate Result = FindTemplate(element, "LinkBackAbilityTemplate");
                return Result;
            }

            else if (ItemKeyValue.Key == typeof(DirectedGoal))
            {
                DataTemplate Result = FindTemplate(element, "LinkBackDirectedGoalTemplate");
                return Result;
            }

            else if (ItemKeyValue.Key == typeof(GameNpc))
            {
                DataTemplate Result = FindTemplate(element, "LinkBackGameNpcTemplate");
                return Result;
            }

            else if (ItemKeyValue.Key == typeof(StorageVault))
            {
                DataTemplate Result = FindTemplate(element, "LinkBackStorageVaultTemplate");
                return Result;
            }

            else if (ItemKeyValue.Key == typeof(Effect))
            {
                DataTemplate Result = FindTemplate(element, "LinkBackEffectTemplate");
                return Result;
            }

            else if (ItemKeyValue.Key == typeof(Item))
            {
                DataTemplate Result = FindTemplate(element, "LinkBackItemTemplate");
                return Result;
            }

            else if (ItemKeyValue.Key == typeof(Quest))
            {
                DataTemplate Result = FindTemplate(element, "LinkBackQuestTemplate");
                return Result;
            }

            else if (ItemKeyValue.Key == typeof(Recipe))
            {
                DataTemplate Result = FindTemplate(element, "LinkBackRecipeTemplate");
                return Result;
            }

            else if (ItemKeyValue.Key == typeof(Skill))
            {
                DataTemplate Result = FindTemplate(element, "LinkBackSkillTemplate");
                return Result;
            }

            else if (ItemKeyValue.Key == typeof(Power))
            {
                DataTemplate Result = FindTemplate(element, "LinkBackPowerTemplate");
                return Result;
            }

            else
                return base.SelectTemplate(item, container);
        }
    }
}
