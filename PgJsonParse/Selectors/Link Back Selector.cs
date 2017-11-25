using PgJsonObjects;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace PgJsonParse
{
    public class LinkBackSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;
            KeyValuePair<Type, List<GenericJsonObject>> ItemKeyValue = (KeyValuePair<Type, List<GenericJsonObject>>)item;

            if (ItemKeyValue.Key == typeof(Ability))
            {
                DataTemplate Result = element.TryFindResource("LinkBackAbilityTemplate") as DataTemplate;
                return Result;
            }

            else if (ItemKeyValue.Key == typeof(DirectedGoal))
            {
                DataTemplate Result = element.TryFindResource("LinkBackDirectedGoalTemplate") as DataTemplate;
                return Result;
            }

            else if (ItemKeyValue.Key == typeof(GameNpc))
            {
                DataTemplate Result = element.TryFindResource("LinkBackGameNpcTemplate") as DataTemplate;
                return Result;
            }

            else if (ItemKeyValue.Key == typeof(StorageVault))
            {
                DataTemplate Result = element.TryFindResource("LinkBackStorageVaultTemplate") as DataTemplate;
                return Result;
            }

            else if (ItemKeyValue.Key == typeof(Effect))
            {
                DataTemplate Result = element.TryFindResource("LinkBackEffectTemplate") as DataTemplate;
                return Result;
            }

            else if (ItemKeyValue.Key == typeof(Item))
            {
                DataTemplate Result = element.TryFindResource("LinkBackItemTemplate") as DataTemplate;
                return Result;
            }

            else if (ItemKeyValue.Key == typeof(Quest))
            {
                DataTemplate Result = element.TryFindResource("LinkBackQuestTemplate") as DataTemplate;
                return Result;
            }

            else if (ItemKeyValue.Key == typeof(Recipe))
            {
                DataTemplate Result = element.TryFindResource("LinkBackRecipeTemplate") as DataTemplate;
                return Result;
            }

            else if (ItemKeyValue.Key == typeof(Skill))
            {
                DataTemplate Result = element.TryFindResource("LinkBackSkillTemplate") as DataTemplate;
                return Result;
            }

            else if (ItemKeyValue.Key == typeof(Power))
            {
                DataTemplate Result = element.TryFindResource("LinkBackPowerTemplate") as DataTemplate;
                return Result;
            }

            else
                return null;
        }
    }
}
