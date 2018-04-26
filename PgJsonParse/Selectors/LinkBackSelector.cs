using PgJsonObjects;
using Presentation;
using System;
using System.Collections.Generic;
using System.Windows;

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
                DataTemplate Result = TryFindResource(element, "LinkBackAbilityTemplate") as DataTemplate;
                return Result;
            }

            else if (ItemKeyValue.Key == typeof(DirectedGoal))
            {
                DataTemplate Result = TryFindResource(element, "LinkBackDirectedGoalTemplate") as DataTemplate;
                return Result;
            }

            else if (ItemKeyValue.Key == typeof(GameNpc))
            {
                DataTemplate Result = TryFindResource(element, "LinkBackGameNpcTemplate") as DataTemplate;
                return Result;
            }

            else if (ItemKeyValue.Key == typeof(StorageVault))
            {
                DataTemplate Result = TryFindResource(element, "LinkBackStorageVaultTemplate") as DataTemplate;
                return Result;
            }

            else if (ItemKeyValue.Key == typeof(Effect))
            {
                DataTemplate Result = TryFindResource(element, "LinkBackEffectTemplate") as DataTemplate;
                return Result;
            }

            else if (ItemKeyValue.Key == typeof(Item))
            {
                DataTemplate Result = TryFindResource(element, "LinkBackItemTemplate") as DataTemplate;
                return Result;
            }

            else if (ItemKeyValue.Key == typeof(Quest))
            {
                DataTemplate Result = TryFindResource(element, "LinkBackQuestTemplate") as DataTemplate;
                return Result;
            }

            else if (ItemKeyValue.Key == typeof(Recipe))
            {
                DataTemplate Result = TryFindResource(element, "LinkBackRecipeTemplate") as DataTemplate;
                return Result;
            }

            else if (ItemKeyValue.Key == typeof(Skill))
            {
                DataTemplate Result = TryFindResource(element, "LinkBackSkillTemplate") as DataTemplate;
                return Result;
            }

            else if (ItemKeyValue.Key == typeof(Power))
            {
                DataTemplate Result = TryFindResource(element, "LinkBackPowerTemplate") as DataTemplate;
                return Result;
            }

            else
                return base.SelectTemplate(item, container);
        }
    }
}
