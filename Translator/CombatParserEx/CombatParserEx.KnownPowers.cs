namespace TranslatorCombatParserEx;

using System.Collections.Generic;
using PgObjects;

internal partial class CombatParserEx
{
    private Dictionary<string, PgCombatModCollectionEx> KnownPowers = new()
    {
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "1006", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "All Sword abilities have a 20% chance to restore 12 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Sword },
                            Data = new PgNumericValueEx() { Value = +12F },
                            RandomChance = 0.2F,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "All Sword abilities have a 20% chance to restore 16 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Sword },
                            Data = new PgNumericValueEx() { Value = +16F },
                            RandomChance = 0.2F,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "All Sword abilities have a 20% chance to restore 20 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Sword },
                            Data = new PgNumericValueEx() { Value = +20F },
                            RandomChance = 0.2F,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "All Sword abilities have a 20% chance to restore 24 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Sword },
                            Data = new PgNumericValueEx() { Value = +24F },
                            RandomChance = 0.2F,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "All Sword abilities have a 20% chance to restore 28 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Sword },
                            Data = new PgNumericValueEx() { Value = +28F },
                            RandomChance = 0.2F,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "All Sword abilities have a 22% chance to restore 32 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Sword },
                            Data = new PgNumericValueEx() { Value = +32F },
                            RandomChance = 0.22F,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "All Sword abilities have a 22% chance to restore 36 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Sword },
                            Data = new PgNumericValueEx() { Value = +36F },
                            RandomChance = 0.22F,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "All Sword abilities have a 22% chance to restore 40 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Sword },
                            Data = new PgNumericValueEx() { Value = +40F },
                            RandomChance = 0.22F,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "All Sword abilities have a 22% chance to restore 44 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Sword },
                            Data = new PgNumericValueEx() { Value = +44F },
                            RandomChance = 0.22F,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "All Sword abilities have a 22% chance to restore 48 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Sword },
                            Data = new PgNumericValueEx() { Value = +48F },
                            RandomChance = 0.22F,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "All Sword abilities have a 22% chance to restore 52 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Sword },
                            Data = new PgNumericValueEx() { Value = +52F },
                            RandomChance = 0.22F,
                            Target = CombatTarget.Self,
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "10404", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Snare Arrow restores 2 Health and 2 Armor to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +2F },
                        },
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +2F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Snare Arrow restores 5 Health and 5 Armor to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +5F },
                        },
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +5F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Snare Arrow restores 8 Health and 8 Armor to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +8F },
                        },
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +8F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Snare Arrow restores 11 Health and 11 Armor to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +11F },
                        },
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +11F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Snare Arrow restores 14 Health and 14 Armor to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +14F },
                        },
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +14F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Snare Arrow restores 17 Health and 17 Armor to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +17F },
                        },
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +17F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Snare Arrow restores 20 Health and 20 Armor to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +20F },
                        },
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +20F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Snare Arrow restores 23 Health and 23 Armor to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +23F },
                        },
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +23F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Snare Arrow restores 26 Health and 26 Armor to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +26F },
                        },
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +26F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Snare Arrow restores 29 Health and 29 Armor to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +29F },
                        },
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +29F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Snare Arrow restores 32 Health and 32 Armor to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +32F },
                        },
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +32F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Snare Arrow restores 35 Health and 35 Armor to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +35F },
                        },
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +35F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Snare Arrow restores 37 Health and 37 Armor to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +37F },
                        },
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +37F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Snare Arrow restores 39 Health and 39 Armor to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +39F },
                        },
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +39F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Snare Arrow restores 41 Health and 41 Armor to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +41F },
                        },
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +41F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Snare Arrow restores 43 Health and 43 Armor to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +43F },
                        },
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +43F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Snare Arrow restores 46 Health and 46 Armor to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +46F },
                        },
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +46F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Snare Arrow restores 49 Health and 49 Armor to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +49F },
                        },
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +49F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Snare Arrow restores 52 Health and 52 Armor to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +52F },
                        },
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +52F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Snare Arrow restores 55 Health and 55 Armor to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +55F },
                        },
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +55F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Snare Arrow restores 58 Health and 58 Armor to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +58F },
                        },
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +58F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Snare Arrow restores 61 Health and 61 Armor to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +61F },
                        },
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +61F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Snare Arrow restores 64 Health and 64 Armor to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +64F },
                        },
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +64F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Snare Arrow restores 67 Health and 67 Armor to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +67F },
                        },
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +67F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Snare Arrow restores 70 Health and 70 Armor to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +70F },
                        },
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SnareArrow },
                            Data = new PgNumericValueEx() { Value = +70F },
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "1042", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Parry restores 3 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Parry },
                            Data = new PgNumericValueEx() { Value = +3F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Parry restores 4 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Parry },
                            Data = new PgNumericValueEx() { Value = +4F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Parry restores 5 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Parry },
                            Data = new PgNumericValueEx() { Value = +5F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Parry restores 6 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Parry },
                            Data = new PgNumericValueEx() { Value = +6F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Parry restores 7 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Parry },
                            Data = new PgNumericValueEx() { Value = +7F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Parry restores 8 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Parry },
                            Data = new PgNumericValueEx() { Value = +8F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Parry restores 9 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Parry },
                            Data = new PgNumericValueEx() { Value = +9F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Parry restores 10 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Parry },
                            Data = new PgNumericValueEx() { Value = +10F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Parry restores 12 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Parry },
                            Data = new PgNumericValueEx() { Value = +12F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Parry restores 14 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Parry },
                            Data = new PgNumericValueEx() { Value = +14F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Parry restores 16 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Parry },
                            Data = new PgNumericValueEx() { Value = +16F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Parry restores 18 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Parry },
                            Data = new PgNumericValueEx() { Value = +18F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Parry restores 20 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Parry },
                            Data = new PgNumericValueEx() { Value = +20F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Parry restores 22 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Parry },
                            Data = new PgNumericValueEx() { Value = +22F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Parry restores 24 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Parry },
                            Data = new PgNumericValueEx() { Value = +24F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Parry restores 26 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Parry },
                            Data = new PgNumericValueEx() { Value = +26F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Parry restores 27 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Parry },
                            Data = new PgNumericValueEx() { Value = +27F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Parry restores 28 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Parry },
                            Data = new PgNumericValueEx() { Value = +28F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Parry restores 29 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Parry },
                            Data = new PgNumericValueEx() { Value = +29F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Parry restores 30 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Parry },
                            Data = new PgNumericValueEx() { Value = +30F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Parry restores 31 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Parry },
                            Data = new PgNumericValueEx() { Value = +31F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Parry restores 32 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Parry },
                            Data = new PgNumericValueEx() { Value = +32F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Parry restores 33 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Parry },
                            Data = new PgNumericValueEx() { Value = +33F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Parry restores 34 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Parry },
                            Data = new PgNumericValueEx() { Value = +34F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Parry restores 35 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Parry },
                            Data = new PgNumericValueEx() { Value = +35F },
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "10506", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow heals YOU for 5 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +5F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow heals YOU for 11 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +11F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow heals YOU for 17 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +17F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow heals YOU for 23 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +23F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow heals YOU for 29 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +29F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow heals YOU for 35 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +35F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow heals YOU for 41 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +41F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow heals YOU for 47 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +47F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow heals YOU for 53 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +53F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow heals YOU for 59 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +59F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow heals YOU for 65 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +65F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow heals YOU for 70 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +70F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow heals YOU for 75 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +75F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow heals YOU for 80 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +80F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow heals YOU for 85 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +85F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow heals YOU for 90 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +90F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow heals YOU for 95 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +95F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow heals YOU for 100 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +100F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow heals YOU for 105 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +105F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow heals YOU for 110 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +110F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow heals YOU for 115 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +115F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow heals YOU for 120 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +120F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow heals YOU for 125 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +125F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow heals YOU for 130 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +130F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow heals YOU for 135 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +135F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "10507", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow restores 5 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +5F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow restores 10 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +10F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow restores 15 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +15F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow restores 20 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +20F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow restores 25 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +25F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow restores 30 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +30F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow restores 35 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +35F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow restores 40 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +40F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow restores 45 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +45F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow restores 50 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +50F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow restores 55 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +55F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow restores 60 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +60F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow restores 65 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +65F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow restores 70 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +70F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow restores 75 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +75F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow restores 80 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +80F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow restores 85 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +85F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow restores 90 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +90F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow restores 95 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +95F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow restores 100 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +100F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow restores 105 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +105F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow restores 110 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +110F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow restores 115 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +115F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow restores 120 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +120F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Restorative Arrow restores 125 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RestorativeArrow },
                            Data = new PgNumericValueEx() { Value = +125F },
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "11603", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Finish It Restores 16 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FinishIt },
                            Data = new PgNumericValueEx() { Value = +16F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Finish It Restores 24 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FinishIt },
                            Data = new PgNumericValueEx() { Value = +24F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Finish It Restores 32 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FinishIt },
                            Data = new PgNumericValueEx() { Value = +32F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Finish It Restores 40 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FinishIt },
                            Data = new PgNumericValueEx() { Value = +40F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Finish It Restores 48 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FinishIt },
                            Data = new PgNumericValueEx() { Value = +48F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Finish It Restores 56 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FinishIt },
                            Data = new PgNumericValueEx() { Value = +56F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Finish It Restores 62 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FinishIt },
                            Data = new PgNumericValueEx() { Value = +62F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Finish It Restores 68 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FinishIt },
                            Data = new PgNumericValueEx() { Value = +68F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Finish It Restores 74 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FinishIt },
                            Data = new PgNumericValueEx() { Value = +74F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Finish It Restores 80 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FinishIt },
                            Data = new PgNumericValueEx() { Value = +80F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Finish It Restores 86 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FinishIt },
                            Data = new PgNumericValueEx() { Value = +86F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Finish It Restores 92 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FinishIt },
                            Data = new PgNumericValueEx() { Value = +92F },
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "12053", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Get It Off Me heals you for 25 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.GetItOffMe },
                            Data = new PgNumericValueEx() { Value = +25F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Get It Off Me heals you for 34 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.GetItOffMe },
                            Data = new PgNumericValueEx() { Value = +34F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Get It Off Me heals you for 43 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.GetItOffMe },
                            Data = new PgNumericValueEx() { Value = +43F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Get It Off Me heals you for 52 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.GetItOffMe },
                            Data = new PgNumericValueEx() { Value = +52F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Get It Off Me heals you for 61 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.GetItOffMe },
                            Data = new PgNumericValueEx() { Value = +61F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Get It Off Me heals you for 70 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.GetItOffMe },
                            Data = new PgNumericValueEx() { Value = +70F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Get It Off Me heals you for 79 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.GetItOffMe },
                            Data = new PgNumericValueEx() { Value = +79F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Get It Off Me heals you for 88 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.GetItOffMe },
                            Data = new PgNumericValueEx() { Value = +88F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Get It Off Me heals you for 97 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.GetItOffMe },
                            Data = new PgNumericValueEx() { Value = +97F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Get It Off Me heals you for 106 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.GetItOffMe },
                            Data = new PgNumericValueEx() { Value = +106F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Get It Off Me heals you for 115 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.GetItOffMe },
                            Data = new PgNumericValueEx() { Value = +115F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Get It Off Me heals you for 124 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.GetItOffMe },
                            Data = new PgNumericValueEx() { Value = +124F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Get It Off Me heals you for 133 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.GetItOffMe },
                            Data = new PgNumericValueEx() { Value = +133F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Get It Off Me heals you for 142 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.GetItOffMe },
                            Data = new PgNumericValueEx() { Value = +142F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Get It Off Me heals you for 151 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.GetItOffMe },
                            Data = new PgNumericValueEx() { Value = +151F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Get It Off Me heals you for 160 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.GetItOffMe },
                            Data = new PgNumericValueEx() { Value = +160F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Get It Off Me heals you for 169 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.GetItOffMe },
                            Data = new PgNumericValueEx() { Value = +169F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Get It Off Me heals you for 178 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.GetItOffMe },
                            Data = new PgNumericValueEx() { Value = +178F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Get It Off Me heals you for 187 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.GetItOffMe },
                            Data = new PgNumericValueEx() { Value = +187F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Get It Off Me heals you for 196 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.GetItOffMe },
                            Data = new PgNumericValueEx() { Value = +196F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Get It Off Me heals you for 205 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.GetItOffMe },
                            Data = new PgNumericValueEx() { Value = +205F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Get It Off Me heals you for 214 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.GetItOffMe },
                            Data = new PgNumericValueEx() { Value = +214F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Get It Off Me heals you for 223 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.GetItOffMe },
                            Data = new PgNumericValueEx() { Value = +223F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Get It Off Me heals you for 232 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.GetItOffMe },
                            Data = new PgNumericValueEx() { Value = +232F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Get It Off Me heals you for 241 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.GetItOffMe },
                            Data = new PgNumericValueEx() { Value = +241F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "12091", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Feed Pet restores 20 Health (or Armor if Health is full) to your pet after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FeedPet },
                            Data = new PgNumericValueEx() { Value = +20F },
                            DelayInSeconds = 20,
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Feed Pet restores 28 Health (or Armor if Health is full) to your pet after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FeedPet },
                            Data = new PgNumericValueEx() { Value = +28F },
                            DelayInSeconds = 20,
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Feed Pet restores 36 Health (or Armor if Health is full) to your pet after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FeedPet },
                            Data = new PgNumericValueEx() { Value = +36F },
                            DelayInSeconds = 20,
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Feed Pet restores 44 Health (or Armor if Health is full) to your pet after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FeedPet },
                            Data = new PgNumericValueEx() { Value = +44F },
                            DelayInSeconds = 20,
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Feed Pet restores 52 Health (or Armor if Health is full) to your pet after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FeedPet },
                            Data = new PgNumericValueEx() { Value = +52F },
                            DelayInSeconds = 20,
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Feed Pet restores 60 Health (or Armor if Health is full) to your pet after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FeedPet },
                            Data = new PgNumericValueEx() { Value = +60F },
                            DelayInSeconds = 20,
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Feed Pet restores 68 Health (or Armor if Health is full) to your pet after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FeedPet },
                            Data = new PgNumericValueEx() { Value = +68F },
                            DelayInSeconds = 20,
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Feed Pet restores 76 Health (or Armor if Health is full) to your pet after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FeedPet },
                            Data = new PgNumericValueEx() { Value = +76F },
                            DelayInSeconds = 20,
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Feed Pet restores 84 Health (or Armor if Health is full) to your pet after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FeedPet },
                            Data = new PgNumericValueEx() { Value = +84F },
                            DelayInSeconds = 20,
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Feed Pet restores 92 Health (or Armor if Health is full) to your pet after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FeedPet },
                            Data = new PgNumericValueEx() { Value = +92F },
                            DelayInSeconds = 20,
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Feed Pet restores 100 Health (or Armor if Health is full) to your pet after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FeedPet },
                            Data = new PgNumericValueEx() { Value = +100F },
                            DelayInSeconds = 20,
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Feed Pet restores 108 Health (or Armor if Health is full) to your pet after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FeedPet },
                            Data = new PgNumericValueEx() { Value = +108F },
                            DelayInSeconds = 20,
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Feed Pet restores 116 Health (or Armor if Health is full) to your pet after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FeedPet },
                            Data = new PgNumericValueEx() { Value = +116F },
                            DelayInSeconds = 20,
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Feed Pet restores 124 Health (or Armor if Health is full) to your pet after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FeedPet },
                            Data = new PgNumericValueEx() { Value = +124F },
                            DelayInSeconds = 20,
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Feed Pet restores 132 Health (or Armor if Health is full) to your pet after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FeedPet },
                            Data = new PgNumericValueEx() { Value = +132F },
                            DelayInSeconds = 20,
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Feed Pet restores 140 Health (or Armor if Health is full) to your pet after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FeedPet },
                            Data = new PgNumericValueEx() { Value = +140F },
                            DelayInSeconds = 20,
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Feed Pet restores 148 Health (or Armor if Health is full) to your pet after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FeedPet },
                            Data = new PgNumericValueEx() { Value = +148F },
                            DelayInSeconds = 20,
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Feed Pet restores 156 Health (or Armor if Health is full) to your pet after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FeedPet },
                            Data = new PgNumericValueEx() { Value = +156F },
                            DelayInSeconds = 20,
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Feed Pet restores 164 Health (or Armor if Health is full) to your pet after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FeedPet },
                            Data = new PgNumericValueEx() { Value = +164F },
                            DelayInSeconds = 20,
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Feed Pet restores 172 Health (or Armor if Health is full) to your pet after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FeedPet },
                            Data = new PgNumericValueEx() { Value = +172F },
                            DelayInSeconds = 20,
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Feed Pet restores 180 Health (or Armor if Health is full) to your pet after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FeedPet },
                            Data = new PgNumericValueEx() { Value = +180F },
                            DelayInSeconds = 20,
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Feed Pet restores 188 Health (or Armor if Health is full) to your pet after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FeedPet },
                            Data = new PgNumericValueEx() { Value = +188F },
                            DelayInSeconds = 20,
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Feed Pet restores 196 Health (or Armor if Health is full) to your pet after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FeedPet },
                            Data = new PgNumericValueEx() { Value = +196F },
                            DelayInSeconds = 20,
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Feed Pet restores 204 Health (or Armor if Health is full) to your pet after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FeedPet },
                            Data = new PgNumericValueEx() { Value = +204F },
                            DelayInSeconds = 20,
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Feed Pet restores 212 Health (or Armor if Health is full) to your pet after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FeedPet },
                            Data = new PgNumericValueEx() { Value = +212F },
                            DelayInSeconds = 20,
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "12102", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Wild Endurance heals your pet for 15 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.WildEndurance },
                            Data = new PgNumericValueEx() { Value = +15F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Wild Endurance heals your pet for 22 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.WildEndurance },
                            Data = new PgNumericValueEx() { Value = +22F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Wild Endurance heals your pet for 29 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.WildEndurance },
                            Data = new PgNumericValueEx() { Value = +29F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Wild Endurance heals your pet for 36 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.WildEndurance },
                            Data = new PgNumericValueEx() { Value = +36F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Wild Endurance heals your pet for 43 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.WildEndurance },
                            Data = new PgNumericValueEx() { Value = +43F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Wild Endurance heals your pet for 50 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.WildEndurance },
                            Data = new PgNumericValueEx() { Value = +50F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Wild Endurance heals your pet for 57 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.WildEndurance },
                            Data = new PgNumericValueEx() { Value = +57F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Wild Endurance heals your pet for 64 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.WildEndurance },
                            Data = new PgNumericValueEx() { Value = +64F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Wild Endurance heals your pet for 71 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.WildEndurance },
                            Data = new PgNumericValueEx() { Value = +71F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Wild Endurance heals your pet for 78 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.WildEndurance },
                            Data = new PgNumericValueEx() { Value = +78F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Wild Endurance heals your pet for 85 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.WildEndurance },
                            Data = new PgNumericValueEx() { Value = +85F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Wild Endurance heals your pet for 92 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.WildEndurance },
                            Data = new PgNumericValueEx() { Value = +92F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Wild Endurance heals your pet for 99 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.WildEndurance },
                            Data = new PgNumericValueEx() { Value = +99F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Wild Endurance heals your pet for 106 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.WildEndurance },
                            Data = new PgNumericValueEx() { Value = +106F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Wild Endurance heals your pet for 113 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.WildEndurance },
                            Data = new PgNumericValueEx() { Value = +113F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Wild Endurance heals your pet for 120 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.WildEndurance },
                            Data = new PgNumericValueEx() { Value = +120F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Wild Endurance heals your pet for 127 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.WildEndurance },
                            Data = new PgNumericValueEx() { Value = +127F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Wild Endurance heals your pet for 134 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.WildEndurance },
                            Data = new PgNumericValueEx() { Value = +134F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Wild Endurance heals your pet for 141 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.WildEndurance },
                            Data = new PgNumericValueEx() { Value = +141F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Wild Endurance heals your pet for 148 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.WildEndurance },
                            Data = new PgNumericValueEx() { Value = +148F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Wild Endurance heals your pet for 155 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.WildEndurance },
                            Data = new PgNumericValueEx() { Value = +155F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Wild Endurance heals your pet for 162 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.WildEndurance },
                            Data = new PgNumericValueEx() { Value = +162F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Wild Endurance heals your pet for 169 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.WildEndurance },
                            Data = new PgNumericValueEx() { Value = +169F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Wild Endurance heals your pet for 176 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.WildEndurance },
                            Data = new PgNumericValueEx() { Value = +176F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Wild Endurance heals your pet for 183 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.WildEndurance },
                            Data = new PgNumericValueEx() { Value = +183F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "12103", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Nimble Limbs heals your pet for 7 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.NimbleLimbs },
                            Data = new PgNumericValueEx() { Value = +7F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Nimble Limbs heals your pet for 13 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.NimbleLimbs },
                            Data = new PgNumericValueEx() { Value = +13F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Nimble Limbs heals your pet for 19 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.NimbleLimbs },
                            Data = new PgNumericValueEx() { Value = +19F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Nimble Limbs heals your pet for 25 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.NimbleLimbs },
                            Data = new PgNumericValueEx() { Value = +25F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Nimble Limbs heals your pet for 31 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.NimbleLimbs },
                            Data = new PgNumericValueEx() { Value = +31F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Nimble Limbs heals your pet for 37 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.NimbleLimbs },
                            Data = new PgNumericValueEx() { Value = +37F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Nimble Limbs heals your pet for 43 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.NimbleLimbs },
                            Data = new PgNumericValueEx() { Value = +43F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Nimble Limbs heals your pet for 49 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.NimbleLimbs },
                            Data = new PgNumericValueEx() { Value = +49F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Nimble Limbs heals your pet for 55 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.NimbleLimbs },
                            Data = new PgNumericValueEx() { Value = +55F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Nimble Limbs heals your pet for 61 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.NimbleLimbs },
                            Data = new PgNumericValueEx() { Value = +61F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Nimble Limbs heals your pet for 67 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.NimbleLimbs },
                            Data = new PgNumericValueEx() { Value = +67F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Nimble Limbs heals your pet for 73 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.NimbleLimbs },
                            Data = new PgNumericValueEx() { Value = +73F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Nimble Limbs heals your pet for 80 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.NimbleLimbs },
                            Data = new PgNumericValueEx() { Value = +80F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Nimble Limbs heals your pet for 87 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.NimbleLimbs },
                            Data = new PgNumericValueEx() { Value = +87F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Nimble Limbs heals your pet for 94 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.NimbleLimbs },
                            Data = new PgNumericValueEx() { Value = +94F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Nimble Limbs heals your pet for 101 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.NimbleLimbs },
                            Data = new PgNumericValueEx() { Value = +101F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Nimble Limbs heals your pet for 108 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.NimbleLimbs },
                            Data = new PgNumericValueEx() { Value = +108F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Nimble Limbs heals your pet for 115 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.NimbleLimbs },
                            Data = new PgNumericValueEx() { Value = +115F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Nimble Limbs heals your pet for 122 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.NimbleLimbs },
                            Data = new PgNumericValueEx() { Value = +122F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Nimble Limbs heals your pet for 129 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.NimbleLimbs },
                            Data = new PgNumericValueEx() { Value = +129F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Nimble Limbs heals your pet for 136 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.NimbleLimbs },
                            Data = new PgNumericValueEx() { Value = +136F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Nimble Limbs heals your pet for 143 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.NimbleLimbs },
                            Data = new PgNumericValueEx() { Value = +143F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Nimble Limbs heals your pet for 150 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.NimbleLimbs },
                            Data = new PgNumericValueEx() { Value = +150F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Nimble Limbs heals your pet for 157 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.NimbleLimbs },
                            Data = new PgNumericValueEx() { Value = +157F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Nimble Limbs heals your pet for 164 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.NimbleLimbs },
                            Data = new PgNumericValueEx() { Value = +164F },
                            Target = CombatTarget.AnimalHandlingPet,
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "14352", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Regrowth restores 15 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Regrowth },
                            Data = new PgNumericValueEx() { Value = +15F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Regrowth restores 24 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Regrowth },
                            Data = new PgNumericValueEx() { Value = +24F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Regrowth restores 33 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Regrowth },
                            Data = new PgNumericValueEx() { Value = +33F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Regrowth restores 42 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Regrowth },
                            Data = new PgNumericValueEx() { Value = +42F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Regrowth restores 51 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Regrowth },
                            Data = new PgNumericValueEx() { Value = +51F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Regrowth restores 60 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Regrowth },
                            Data = new PgNumericValueEx() { Value = +60F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Regrowth restores 69 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Regrowth },
                            Data = new PgNumericValueEx() { Value = +69F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Regrowth restores 78 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Regrowth },
                            Data = new PgNumericValueEx() { Value = +78F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Regrowth restores 87 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Regrowth },
                            Data = new PgNumericValueEx() { Value = +87F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Regrowth restores 96 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Regrowth },
                            Data = new PgNumericValueEx() { Value = +96F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Regrowth restores 105 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Regrowth },
                            Data = new PgNumericValueEx() { Value = +105F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Regrowth restores 114 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Regrowth },
                            Data = new PgNumericValueEx() { Value = +114F },
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "15106", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Ice Armor instantly restores 11 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceArmor },
                            Data = new PgNumericValueEx() { Value = +11F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Armor instantly restores 15 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceArmor },
                            Data = new PgNumericValueEx() { Value = +15F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Armor instantly restores 19 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceArmor },
                            Data = new PgNumericValueEx() { Value = +19F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Armor instantly restores 23 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceArmor },
                            Data = new PgNumericValueEx() { Value = +23F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Armor instantly restores 27 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceArmor },
                            Data = new PgNumericValueEx() { Value = +27F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Armor instantly restores 31 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceArmor },
                            Data = new PgNumericValueEx() { Value = +31F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Armor instantly restores 35 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceArmor },
                            Data = new PgNumericValueEx() { Value = +35F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Armor instantly restores 40 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceArmor },
                            Data = new PgNumericValueEx() { Value = +40F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Armor instantly restores 45 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceArmor },
                            Data = new PgNumericValueEx() { Value = +45F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Armor instantly restores 50 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceArmor },
                            Data = new PgNumericValueEx() { Value = +50F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Armor instantly restores 55 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceArmor },
                            Data = new PgNumericValueEx() { Value = +55F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Armor instantly restores 60 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceArmor },
                            Data = new PgNumericValueEx() { Value = +60F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Armor instantly restores 65 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceArmor },
                            Data = new PgNumericValueEx() { Value = +65F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Armor instantly restores 70 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceArmor },
                            Data = new PgNumericValueEx() { Value = +70F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Armor instantly restores 75 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceArmor },
                            Data = new PgNumericValueEx() { Value = +75F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Armor instantly restores 80 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceArmor },
                            Data = new PgNumericValueEx() { Value = +80F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Armor instantly restores 85 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceArmor },
                            Data = new PgNumericValueEx() { Value = +85F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Armor instantly restores 90 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceArmor },
                            Data = new PgNumericValueEx() { Value = +90F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Armor instantly restores 95 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceArmor },
                            Data = new PgNumericValueEx() { Value = +95F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Armor instantly restores 100 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceArmor },
                            Data = new PgNumericValueEx() { Value = +100F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Armor instantly restores 105 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceArmor },
                            Data = new PgNumericValueEx() { Value = +105F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Armor instantly restores 110 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceArmor },
                            Data = new PgNumericValueEx() { Value = +110F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Armor instantly restores 115 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceArmor },
                            Data = new PgNumericValueEx() { Value = +115F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Armor instantly restores 120 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceArmor },
                            Data = new PgNumericValueEx() { Value = +120F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Armor instantly restores 125 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceArmor },
                            Data = new PgNumericValueEx() { Value = +125F },
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "15303", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "You regain 15 Health when using Blizzard",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Blizzard },
                            Data = new PgNumericValueEx() { Value = +15F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 18 Health when using Blizzard",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Blizzard },
                            Data = new PgNumericValueEx() { Value = +18F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 21 Health when using Blizzard",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Blizzard },
                            Data = new PgNumericValueEx() { Value = +21F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 24 Health when using Blizzard",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Blizzard },
                            Data = new PgNumericValueEx() { Value = +24F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 27 Health when using Blizzard",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Blizzard },
                            Data = new PgNumericValueEx() { Value = +27F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 30 Health when using Blizzard",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Blizzard },
                            Data = new PgNumericValueEx() { Value = +30F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 33 Health when using Blizzard",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Blizzard },
                            Data = new PgNumericValueEx() { Value = +33F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 36 Health when using Blizzard",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Blizzard },
                            Data = new PgNumericValueEx() { Value = +36F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 39 Health when using Blizzard",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Blizzard },
                            Data = new PgNumericValueEx() { Value = +39F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 42 Health when using Blizzard",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Blizzard },
                            Data = new PgNumericValueEx() { Value = +42F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 45 Health when using Blizzard",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Blizzard },
                            Data = new PgNumericValueEx() { Value = +45F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 48 Health when using Blizzard",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Blizzard },
                            Data = new PgNumericValueEx() { Value = +48F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 52 Health when using Blizzard",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Blizzard },
                            Data = new PgNumericValueEx() { Value = +52F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 56 Health when using Blizzard",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Blizzard },
                            Data = new PgNumericValueEx() { Value = +56F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 60 Health when using Blizzard",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Blizzard },
                            Data = new PgNumericValueEx() { Value = +60F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 64 Health when using Blizzard",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Blizzard },
                            Data = new PgNumericValueEx() { Value = +64F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 68 Health when using Blizzard",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Blizzard },
                            Data = new PgNumericValueEx() { Value = +68F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 72 Health when using Blizzard",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Blizzard },
                            Data = new PgNumericValueEx() { Value = +72F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 76 Health when using Blizzard",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Blizzard },
                            Data = new PgNumericValueEx() { Value = +76F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 80 Health when using Blizzard",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Blizzard },
                            Data = new PgNumericValueEx() { Value = +80F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 84 Health when using Blizzard",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Blizzard },
                            Data = new PgNumericValueEx() { Value = +84F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 88 Health when using Blizzard",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Blizzard },
                            Data = new PgNumericValueEx() { Value = +88F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 92 Health when using Blizzard",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Blizzard },
                            Data = new PgNumericValueEx() { Value = +92F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 96 Health when using Blizzard",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Blizzard },
                            Data = new PgNumericValueEx() { Value = +96F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 100 Health when using Blizzard",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Blizzard },
                            Data = new PgNumericValueEx() { Value = +100F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "154", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Basic Attacks restore 5 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BasicAttack },
                            Data = new PgNumericValueEx() { Value = +5F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Basic Attacks restore 6 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BasicAttack },
                            Data = new PgNumericValueEx() { Value = +6F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Basic Attacks restore 7 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BasicAttack },
                            Data = new PgNumericValueEx() { Value = +7F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Basic Attacks restore 8 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BasicAttack },
                            Data = new PgNumericValueEx() { Value = +8F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Basic Attacks restore 9 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BasicAttack },
                            Data = new PgNumericValueEx() { Value = +9F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Basic Attacks restore 10 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BasicAttack },
                            Data = new PgNumericValueEx() { Value = +10F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Basic Attacks restore 11 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BasicAttack },
                            Data = new PgNumericValueEx() { Value = +11F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Basic Attacks restore 12 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BasicAttack },
                            Data = new PgNumericValueEx() { Value = +12F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Basic Attacks restore 13 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BasicAttack },
                            Data = new PgNumericValueEx() { Value = +13F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Basic Attacks restore 14 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BasicAttack },
                            Data = new PgNumericValueEx() { Value = +14F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Basic Attacks restore 15 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BasicAttack },
                            Data = new PgNumericValueEx() { Value = +15F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Basic Attacks restore 16 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BasicAttack },
                            Data = new PgNumericValueEx() { Value = +16F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Basic Attacks restore 17 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BasicAttack },
                            Data = new PgNumericValueEx() { Value = +17F },
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "15401", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 8 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +8F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 15 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +15F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 22 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +22F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 29 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +29F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 36 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +36F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 43 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +43F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 50 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +50F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 57 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +57F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 64 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +64F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 71 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +71F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 78 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +78F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 85 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +85F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 92 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +92F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 99 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +99F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 106 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +106F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 113 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +113F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 120 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +120F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 127 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +127F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 134 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +134F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 141 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +141F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 148 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +148F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 155 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +155F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 162 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +162F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 169 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +169F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 176 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +176F },
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "15403", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 8 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +8F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 15 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +15F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 22 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +22F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 29 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +29F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 36 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +36F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 43 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +43F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 50 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +50F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 57 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +57F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 64 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +64F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 71 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +71F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 78 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +78F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 85 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +85F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 92 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +92F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 99 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +99F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 106 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +106F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 113 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +113F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 120 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +120F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 127 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +127F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 134 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +134F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 141 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +141F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 148 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +148F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 155 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +155F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 162 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +162F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 169 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +169F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cryogenic Freeze restores 176 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CryogenicFreeze },
                            Data = new PgNumericValueEx() { Value = +176F },
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "16042", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Blur Cut restores 3 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BlurCut },
                            Data = new PgNumericValueEx() { Value = +3F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Blur Cut restores 4 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BlurCut },
                            Data = new PgNumericValueEx() { Value = +4F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Blur Cut restores 5 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BlurCut },
                            Data = new PgNumericValueEx() { Value = +5F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Blur Cut restores 6 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BlurCut },
                            Data = new PgNumericValueEx() { Value = +6F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Blur Cut restores 7 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BlurCut },
                            Data = new PgNumericValueEx() { Value = +7F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Blur Cut restores 8 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BlurCut },
                            Data = new PgNumericValueEx() { Value = +8F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Blur Cut restores 9 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BlurCut },
                            Data = new PgNumericValueEx() { Value = +9F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Blur Cut restores 11 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BlurCut },
                            Data = new PgNumericValueEx() { Value = +11F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Blur Cut restores 13 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BlurCut },
                            Data = new PgNumericValueEx() { Value = +13F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Blur Cut restores 15 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BlurCut },
                            Data = new PgNumericValueEx() { Value = +15F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Blur Cut restores 17 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BlurCut },
                            Data = new PgNumericValueEx() { Value = +17F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Blur Cut restores 19 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BlurCut },
                            Data = new PgNumericValueEx() { Value = +19F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Blur Cut restores 21 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BlurCut },
                            Data = new PgNumericValueEx() { Value = +21F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Blur Cut restores 23 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BlurCut },
                            Data = new PgNumericValueEx() { Value = +23F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Blur Cut restores 25 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BlurCut },
                            Data = new PgNumericValueEx() { Value = +25F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Blur Cut restores 27 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BlurCut },
                            Data = new PgNumericValueEx() { Value = +27F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Blur Cut restores 29 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BlurCut },
                            Data = new PgNumericValueEx() { Value = +29F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Blur Cut restores 31 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BlurCut },
                            Data = new PgNumericValueEx() { Value = +31F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Blur Cut restores 33 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BlurCut },
                            Data = new PgNumericValueEx() { Value = +33F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Blur Cut restores 35 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BlurCut },
                            Data = new PgNumericValueEx() { Value = +35F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Blur Cut restores 37 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BlurCut },
                            Data = new PgNumericValueEx() { Value = +37F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Blur Cut restores 39 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BlurCut },
                            Data = new PgNumericValueEx() { Value = +39F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Blur Cut restores 41 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BlurCut },
                            Data = new PgNumericValueEx() { Value = +41F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Blur Cut restores 43 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BlurCut },
                            Data = new PgNumericValueEx() { Value = +43F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Blur Cut restores 45 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BlurCut },
                            Data = new PgNumericValueEx() { Value = +45F },
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "16083", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Fending Blade restores 6 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FendingBlade },
                            Data = new PgNumericValueEx() { Value = +6F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fending Blade restores 8 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FendingBlade },
                            Data = new PgNumericValueEx() { Value = +8F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fending Blade restores 10 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FendingBlade },
                            Data = new PgNumericValueEx() { Value = +10F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fending Blade restores 12 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FendingBlade },
                            Data = new PgNumericValueEx() { Value = +12F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fending Blade restores 14 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FendingBlade },
                            Data = new PgNumericValueEx() { Value = +14F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fending Blade restores 16 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FendingBlade },
                            Data = new PgNumericValueEx() { Value = +16F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fending Blade restores 18 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FendingBlade },
                            Data = new PgNumericValueEx() { Value = +18F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fending Blade restores 20 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FendingBlade },
                            Data = new PgNumericValueEx() { Value = +20F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fending Blade restores 22 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FendingBlade },
                            Data = new PgNumericValueEx() { Value = +22F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fending Blade restores 24 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FendingBlade },
                            Data = new PgNumericValueEx() { Value = +24F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fending Blade restores 26 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FendingBlade },
                            Data = new PgNumericValueEx() { Value = +26F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fending Blade restores 28 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FendingBlade },
                            Data = new PgNumericValueEx() { Value = +28F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fending Blade restores 30 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FendingBlade },
                            Data = new PgNumericValueEx() { Value = +30F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fending Blade restores 32 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FendingBlade },
                            Data = new PgNumericValueEx() { Value = +32F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fending Blade restores 34 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FendingBlade },
                            Data = new PgNumericValueEx() { Value = +34F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fending Blade restores 36 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FendingBlade },
                            Data = new PgNumericValueEx() { Value = +36F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fending Blade restores 38 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FendingBlade },
                            Data = new PgNumericValueEx() { Value = +38F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fending Blade restores 40 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FendingBlade },
                            Data = new PgNumericValueEx() { Value = +40F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fending Blade restores 42 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FendingBlade },
                            Data = new PgNumericValueEx() { Value = +42F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fending Blade restores 44 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FendingBlade },
                            Data = new PgNumericValueEx() { Value = +44F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fending Blade restores 46 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FendingBlade },
                            Data = new PgNumericValueEx() { Value = +46F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fending Blade restores 48 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FendingBlade },
                            Data = new PgNumericValueEx() { Value = +48F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fending Blade restores 50 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FendingBlade },
                            Data = new PgNumericValueEx() { Value = +50F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fending Blade restores 52 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FendingBlade },
                            Data = new PgNumericValueEx() { Value = +52F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fending Blade restores 54 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FendingBlade },
                            Data = new PgNumericValueEx() { Value = +54F },
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "162", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Major Healing abilities have a 65% chance to restore an additional 24 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MajorHeal },
                            Data = new PgNumericValueEx() { Value = +24F },
                            RandomChance = 0.65F,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Major Healing abilities have a 65% chance to restore an additional 35 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MajorHeal },
                            Data = new PgNumericValueEx() { Value = +35F },
                            RandomChance = 0.65F,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Major Healing abilities have a 65% chance to restore an additional 46 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MajorHeal },
                            Data = new PgNumericValueEx() { Value = +46F },
                            RandomChance = 0.65F,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Major Healing abilities have a 65% chance to restore an additional 57 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MajorHeal },
                            Data = new PgNumericValueEx() { Value = +57F },
                            RandomChance = 0.65F,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Major Healing abilities have a 65% chance to restore an additional 68 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MajorHeal },
                            Data = new PgNumericValueEx() { Value = +68F },
                            RandomChance = 0.65F,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Major Healing abilities have a 65% chance to restore an additional 79 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MajorHeal },
                            Data = new PgNumericValueEx() { Value = +79F },
                            RandomChance = 0.65F,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Major Healing abilities have a 65% chance to restore an additional 90 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MajorHeal },
                            Data = new PgNumericValueEx() { Value = +90F },
                            RandomChance = 0.65F,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Major Healing abilities have a 65% chance to restore an additional 101 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MajorHeal },
                            Data = new PgNumericValueEx() { Value = +101F },
                            RandomChance = 0.65F,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Major Healing abilities have a 65% chance to restore an additional 110 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MajorHeal },
                            Data = new PgNumericValueEx() { Value = +110F },
                            RandomChance = 0.65F,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Major Healing abilities have a 65% chance to restore an additional 119 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MajorHeal },
                            Data = new PgNumericValueEx() { Value = +119F },
                            RandomChance = 0.65F,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Major Healing abilities have a 65% chance to restore an additional 128 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MajorHeal },
                            Data = new PgNumericValueEx() { Value = +128F },
                            RandomChance = 0.65F,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Major Healing abilities have a 65% chance to restore an additional 137 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MajorHeal },
                            Data = new PgNumericValueEx() { Value = +137F },
                            RandomChance = 0.65F,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Major Healing abilities have a 65% chance to restore an additional 145 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MajorHeal },
                            Data = new PgNumericValueEx() { Value = +145F },
                            RandomChance = 0.65F,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Major Healing abilities have a 65% chance to restore an additional 153 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MajorHeal },
                            Data = new PgNumericValueEx() { Value = +153F },
                            RandomChance = 0.65F,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Major Healing abilities have a 65% chance to restore an additional 161 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MajorHeal },
                            Data = new PgNumericValueEx() { Value = +161F },
                            RandomChance = 0.65F,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Major Healing abilities have a 65% chance to restore an additional 169 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MajorHeal },
                            Data = new PgNumericValueEx() { Value = +169F },
                            RandomChance = 0.65F,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Major Healing abilities have a 65% chance to restore an additional 177 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MajorHeal },
                            Data = new PgNumericValueEx() { Value = +177F },
                            RandomChance = 0.65F,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Major Healing abilities have a 65% chance to restore an additional 185 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MajorHeal },
                            Data = new PgNumericValueEx() { Value = +185F },
                            RandomChance = 0.65F,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Major Healing abilities have a 65% chance to restore an additional 193 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MajorHeal },
                            Data = new PgNumericValueEx() { Value = +193F },
                            RandomChance = 0.65F,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Major Healing abilities have a 65% chance to restore an additional 201 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MajorHeal },
                            Data = new PgNumericValueEx() { Value = +201F },
                            RandomChance = 0.65F,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Major Healing abilities have a 65% chance to restore an additional 209 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MajorHeal },
                            Data = new PgNumericValueEx() { Value = +209F },
                            RandomChance = 0.65F,
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "17222", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Rally restores 10 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Rally },
                            Data = new PgNumericValueEx() { Value = +10F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Rally restores 13 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Rally },
                            Data = new PgNumericValueEx() { Value = +13F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Rally restores 16 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Rally },
                            Data = new PgNumericValueEx() { Value = +16F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Rally restores 21 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Rally },
                            Data = new PgNumericValueEx() { Value = +21F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Rally restores 26 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Rally },
                            Data = new PgNumericValueEx() { Value = +26F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Rally restores 31 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Rally },
                            Data = new PgNumericValueEx() { Value = +31F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Rally restores 36 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Rally },
                            Data = new PgNumericValueEx() { Value = +36F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Rally restores 41 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Rally },
                            Data = new PgNumericValueEx() { Value = +41F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Rally restores 46 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Rally },
                            Data = new PgNumericValueEx() { Value = +46F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Rally restores 51 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Rally },
                            Data = new PgNumericValueEx() { Value = +51F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Rally restores 56 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Rally },
                            Data = new PgNumericValueEx() { Value = +56F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Rally restores 61 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Rally },
                            Data = new PgNumericValueEx() { Value = +61F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Rally restores 66 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Rally },
                            Data = new PgNumericValueEx() { Value = +66F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Rally restores 71 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Rally },
                            Data = new PgNumericValueEx() { Value = +71F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Rally restores 76 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Rally },
                            Data = new PgNumericValueEx() { Value = +76F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Rally restores 81 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Rally },
                            Data = new PgNumericValueEx() { Value = +81F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Rally restores 86 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Rally },
                            Data = new PgNumericValueEx() { Value = +86F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Rally restores 91 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Rally },
                            Data = new PgNumericValueEx() { Value = +91F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Rally restores 96 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Rally },
                            Data = new PgNumericValueEx() { Value = +96F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Rally restores 101 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Rally },
                            Data = new PgNumericValueEx() { Value = +101F },
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "20041", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Moo of Calm heals +5 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MooOfCalm },
                            Data = new PgNumericValueEx() { Value = +5F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Moo of Calm heals +10 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MooOfCalm },
                            Data = new PgNumericValueEx() { Value = +10F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Moo of Calm heals +15 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MooOfCalm },
                            Data = new PgNumericValueEx() { Value = +15F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Moo of Calm heals +20 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MooOfCalm },
                            Data = new PgNumericValueEx() { Value = +20F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Moo of Calm heals +25 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MooOfCalm },
                            Data = new PgNumericValueEx() { Value = +25F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Moo of Calm heals +30 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MooOfCalm },
                            Data = new PgNumericValueEx() { Value = +30F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Moo of Calm heals +35 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MooOfCalm },
                            Data = new PgNumericValueEx() { Value = +35F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Moo of Calm heals +40 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MooOfCalm },
                            Data = new PgNumericValueEx() { Value = +40F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Moo of Calm heals +45 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MooOfCalm },
                            Data = new PgNumericValueEx() { Value = +45F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Moo of Calm heals +50 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MooOfCalm },
                            Data = new PgNumericValueEx() { Value = +50F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Moo of Calm heals +55 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MooOfCalm },
                            Data = new PgNumericValueEx() { Value = +55F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Moo of Calm heals +60 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MooOfCalm },
                            Data = new PgNumericValueEx() { Value = +60F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Moo of Calm heals +65 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MooOfCalm },
                            Data = new PgNumericValueEx() { Value = +65F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Moo of Calm heals +70 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MooOfCalm },
                            Data = new PgNumericValueEx() { Value = +70F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Moo of Calm heals +75 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MooOfCalm },
                            Data = new PgNumericValueEx() { Value = +75F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Moo of Calm heals +80 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MooOfCalm },
                            Data = new PgNumericValueEx() { Value = +80F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Moo of Calm heals +85 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MooOfCalm },
                            Data = new PgNumericValueEx() { Value = +85F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Moo of Calm heals +90 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MooOfCalm },
                            Data = new PgNumericValueEx() { Value = +90F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Moo of Calm heals +95 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MooOfCalm },
                            Data = new PgNumericValueEx() { Value = +95F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Moo of Calm heals +100 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MooOfCalm },
                            Data = new PgNumericValueEx() { Value = +100F },
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "20042", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Moo of Calm restores +5 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MooOfCalm },
                            Data = new PgNumericValueEx() { Value = +5F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Moo of Calm restores +10 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MooOfCalm },
                            Data = new PgNumericValueEx() { Value = +10F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Moo of Calm restores +15 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MooOfCalm },
                            Data = new PgNumericValueEx() { Value = +15F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Moo of Calm restores +20 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MooOfCalm },
                            Data = new PgNumericValueEx() { Value = +20F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Moo of Calm restores +25 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MooOfCalm },
                            Data = new PgNumericValueEx() { Value = +25F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Moo of Calm restores +30 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MooOfCalm },
                            Data = new PgNumericValueEx() { Value = +30F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Moo of Calm restores +35 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MooOfCalm },
                            Data = new PgNumericValueEx() { Value = +35F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Moo of Calm restores +40 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MooOfCalm },
                            Data = new PgNumericValueEx() { Value = +40F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Moo of Calm restores +45 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MooOfCalm },
                            Data = new PgNumericValueEx() { Value = +45F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Moo of Calm restores +50 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MooOfCalm },
                            Data = new PgNumericValueEx() { Value = +50F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Moo of Calm restores +55 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MooOfCalm },
                            Data = new PgNumericValueEx() { Value = +55F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Moo of Calm restores +60 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MooOfCalm },
                            Data = new PgNumericValueEx() { Value = +60F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Moo of Calm restores +65 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MooOfCalm },
                            Data = new PgNumericValueEx() { Value = +65F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Moo of Calm restores +70 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MooOfCalm },
                            Data = new PgNumericValueEx() { Value = +70F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Moo of Calm restores +75 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MooOfCalm },
                            Data = new PgNumericValueEx() { Value = +75F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Moo of Calm restores +80 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MooOfCalm },
                            Data = new PgNumericValueEx() { Value = +80F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Moo of Calm restores +85 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MooOfCalm },
                            Data = new PgNumericValueEx() { Value = +85F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Moo of Calm restores +90 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MooOfCalm },
                            Data = new PgNumericValueEx() { Value = +90F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Moo of Calm restores +95 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MooOfCalm },
                            Data = new PgNumericValueEx() { Value = +95F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Moo of Calm restores +100 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MooOfCalm },
                            Data = new PgNumericValueEx() { Value = +100F },
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "2051", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "You regain 2 Power when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +2F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 4 Power when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +4F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 6 Power when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +6F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 8 Power when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +8F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 10 Power when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +10F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 12 Power when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +12F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 14 Power when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +14F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 16 Power when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +16F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 18 Power when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +18F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 20 Power when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +20F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 22 Power when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +22F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 24 Power when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +24F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 26 Power when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +26F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 28 Power when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +28F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 30 Power when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +30F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 32 Power when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +32F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 34 Power when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +34F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 36 Power when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +36F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 38 Power when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +38F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 40 Power when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +40F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 42 Power when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +42F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 44 Power when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +44F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 46 Power when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +46F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 48 Power when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +48F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 50 Power when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +50F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "2054", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Scintillating Flame restores 2 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ScintillatingFlame },
                            Data = new PgNumericValueEx() { Value = +2F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Scintillating Flame restores 4 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ScintillatingFlame },
                            Data = new PgNumericValueEx() { Value = +4F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Scintillating Flame restores 6 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ScintillatingFlame },
                            Data = new PgNumericValueEx() { Value = +6F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Scintillating Flame restores 8 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ScintillatingFlame },
                            Data = new PgNumericValueEx() { Value = +8F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Scintillating Flame restores 10 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ScintillatingFlame },
                            Data = new PgNumericValueEx() { Value = +10F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Scintillating Flame restores 12 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ScintillatingFlame },
                            Data = new PgNumericValueEx() { Value = +12F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Scintillating Flame restores 14 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ScintillatingFlame },
                            Data = new PgNumericValueEx() { Value = +14F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Scintillating Flame restores 16 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ScintillatingFlame },
                            Data = new PgNumericValueEx() { Value = +16F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Scintillating Flame restores 18 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ScintillatingFlame },
                            Data = new PgNumericValueEx() { Value = +18F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Scintillating Flame restores 20 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ScintillatingFlame },
                            Data = new PgNumericValueEx() { Value = +20F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Scintillating Flame restores 22 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ScintillatingFlame },
                            Data = new PgNumericValueEx() { Value = +22F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Scintillating Flame restores 25 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ScintillatingFlame },
                            Data = new PgNumericValueEx() { Value = +25F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Scintillating Flame restores 27 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ScintillatingFlame },
                            Data = new PgNumericValueEx() { Value = +27F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Scintillating Flame restores 29 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ScintillatingFlame },
                            Data = new PgNumericValueEx() { Value = +29F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Scintillating Flame restores 31 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ScintillatingFlame },
                            Data = new PgNumericValueEx() { Value = +31F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Scintillating Flame restores 33 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ScintillatingFlame },
                            Data = new PgNumericValueEx() { Value = +33F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Scintillating Flame restores 35 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ScintillatingFlame },
                            Data = new PgNumericValueEx() { Value = +35F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Scintillating Flame restores 37 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ScintillatingFlame },
                            Data = new PgNumericValueEx() { Value = +37F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Scintillating Flame restores 39 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ScintillatingFlame },
                            Data = new PgNumericValueEx() { Value = +39F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Scintillating Flame restores 41 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ScintillatingFlame },
                            Data = new PgNumericValueEx() { Value = +41F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Scintillating Flame restores 43 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ScintillatingFlame },
                            Data = new PgNumericValueEx() { Value = +43F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Scintillating Flame restores 45 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ScintillatingFlame },
                            Data = new PgNumericValueEx() { Value = +45F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Scintillating Flame restores 47 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ScintillatingFlame },
                            Data = new PgNumericValueEx() { Value = +47F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Scintillating Flame restores 49 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ScintillatingFlame },
                            Data = new PgNumericValueEx() { Value = +49F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Scintillating Flame restores 51 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ScintillatingFlame },
                            Data = new PgNumericValueEx() { Value = +51F },
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "21024", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Deer Bash heals 3 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.DeerBash },
                            Data = new PgNumericValueEx() { Value = +3F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Deer Bash heals 6 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.DeerBash },
                            Data = new PgNumericValueEx() { Value = +6F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Deer Bash heals 8 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.DeerBash },
                            Data = new PgNumericValueEx() { Value = +8F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Deer Bash heals 11 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.DeerBash },
                            Data = new PgNumericValueEx() { Value = +11F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Deer Bash heals 13 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.DeerBash },
                            Data = new PgNumericValueEx() { Value = +13F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Deer Bash heals 16 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.DeerBash },
                            Data = new PgNumericValueEx() { Value = +16F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Deer Bash heals 18 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.DeerBash },
                            Data = new PgNumericValueEx() { Value = +18F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Deer Bash heals 21 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.DeerBash },
                            Data = new PgNumericValueEx() { Value = +21F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Deer Bash heals 23 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.DeerBash },
                            Data = new PgNumericValueEx() { Value = +23F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Deer Bash heals 26 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.DeerBash },
                            Data = new PgNumericValueEx() { Value = +26F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Deer Bash heals 28 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.DeerBash },
                            Data = new PgNumericValueEx() { Value = +28F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Deer Bash heals 30 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.DeerBash },
                            Data = new PgNumericValueEx() { Value = +30F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Deer Bash heals 32 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.DeerBash },
                            Data = new PgNumericValueEx() { Value = +32F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Deer Bash heals 35 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.DeerBash },
                            Data = new PgNumericValueEx() { Value = +35F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Deer Bash heals 37 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.DeerBash },
                            Data = new PgNumericValueEx() { Value = +37F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Deer Bash heals 40 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.DeerBash },
                            Data = new PgNumericValueEx() { Value = +40F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Deer Bash heals 42 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.DeerBash },
                            Data = new PgNumericValueEx() { Value = +42F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Deer Bash heals 45 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.DeerBash },
                            Data = new PgNumericValueEx() { Value = +45F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Deer Bash heals 48 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.DeerBash },
                            Data = new PgNumericValueEx() { Value = +48F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Deer Bash heals 51 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.DeerBash },
                            Data = new PgNumericValueEx() { Value = +51F },
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "21043", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Doe Eyes restores 4 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.DoeEyes },
                            Data = new PgNumericValueEx() { Value = +4F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Doe Eyes restores 8 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.DoeEyes },
                            Data = new PgNumericValueEx() { Value = +8F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Doe Eyes restores 12 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.DoeEyes },
                            Data = new PgNumericValueEx() { Value = +12F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Doe Eyes restores 16 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.DoeEyes },
                            Data = new PgNumericValueEx() { Value = +16F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Doe Eyes restores 20 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.DoeEyes },
                            Data = new PgNumericValueEx() { Value = +20F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Doe Eyes restores 24 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.DoeEyes },
                            Data = new PgNumericValueEx() { Value = +24F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Doe Eyes restores 28 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.DoeEyes },
                            Data = new PgNumericValueEx() { Value = +28F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Doe Eyes restores 32 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.DoeEyes },
                            Data = new PgNumericValueEx() { Value = +32F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Doe Eyes restores 37 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.DoeEyes },
                            Data = new PgNumericValueEx() { Value = +37F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Doe Eyes restores 42 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.DoeEyes },
                            Data = new PgNumericValueEx() { Value = +42F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Doe Eyes restores 47 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.DoeEyes },
                            Data = new PgNumericValueEx() { Value = +47F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Doe Eyes restores 52 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.DoeEyes },
                            Data = new PgNumericValueEx() { Value = +52F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Doe Eyes restores 57 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.DoeEyes },
                            Data = new PgNumericValueEx() { Value = +57F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Doe Eyes restores 62 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.DoeEyes },
                            Data = new PgNumericValueEx() { Value = +62F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Doe Eyes restores 67 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.DoeEyes },
                            Data = new PgNumericValueEx() { Value = +67F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Doe Eyes restores 72 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.DoeEyes },
                            Data = new PgNumericValueEx() { Value = +72F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Doe Eyes restores 77 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.DoeEyes },
                            Data = new PgNumericValueEx() { Value = +77F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Doe Eyes restores 82 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.DoeEyes },
                            Data = new PgNumericValueEx() { Value = +82F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Doe Eyes restores 87 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.DoeEyes },
                            Data = new PgNumericValueEx() { Value = +87F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Doe Eyes restores 92 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.DoeEyes },
                            Data = new PgNumericValueEx() { Value = +92F },
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "2153", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Calefaction restores 14 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Calefaction },
                            Data = new PgNumericValueEx() { Value = +14F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Calefaction restores 16 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Calefaction },
                            Data = new PgNumericValueEx() { Value = +16F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Calefaction restores 18 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Calefaction },
                            Data = new PgNumericValueEx() { Value = +18F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Calefaction restores 20 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Calefaction },
                            Data = new PgNumericValueEx() { Value = +20F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Calefaction restores 22 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Calefaction },
                            Data = new PgNumericValueEx() { Value = +22F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Calefaction restores 24 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Calefaction },
                            Data = new PgNumericValueEx() { Value = +24F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Calefaction restores 26 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Calefaction },
                            Data = new PgNumericValueEx() { Value = +26F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Calefaction restores 28 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Calefaction },
                            Data = new PgNumericValueEx() { Value = +28F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Calefaction restores 30 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Calefaction },
                            Data = new PgNumericValueEx() { Value = +30F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Calefaction restores 32 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Calefaction },
                            Data = new PgNumericValueEx() { Value = +32F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Calefaction restores 34 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Calefaction },
                            Data = new PgNumericValueEx() { Value = +34F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Calefaction restores 36 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Calefaction },
                            Data = new PgNumericValueEx() { Value = +36F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Calefaction restores 38 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Calefaction },
                            Data = new PgNumericValueEx() { Value = +38F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Calefaction restores 40 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Calefaction },
                            Data = new PgNumericValueEx() { Value = +40F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Calefaction restores 42 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Calefaction },
                            Data = new PgNumericValueEx() { Value = +42F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Calefaction restores 45 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Calefaction },
                            Data = new PgNumericValueEx() { Value = +45F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Calefaction restores 48 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Calefaction },
                            Data = new PgNumericValueEx() { Value = +48F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Calefaction restores 51 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Calefaction },
                            Data = new PgNumericValueEx() { Value = +51F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Calefaction restores 54 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Calefaction },
                            Data = new PgNumericValueEx() { Value = +54F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Calefaction restores 57 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Calefaction },
                            Data = new PgNumericValueEx() { Value = +57F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Calefaction restores 60 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Calefaction },
                            Data = new PgNumericValueEx() { Value = +60F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Calefaction restores 63 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Calefaction },
                            Data = new PgNumericValueEx() { Value = +63F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Calefaction restores 66 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Calefaction },
                            Data = new PgNumericValueEx() { Value = +66F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Calefaction restores 69 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Calefaction },
                            Data = new PgNumericValueEx() { Value = +69F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Calefaction restores 72 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Calefaction },
                            Data = new PgNumericValueEx() { Value = +72F },
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "22004", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Pig Bite restores 1 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PigBite },
                            Data = new PgNumericValueEx() { Value = +1F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pig Bite restores 2 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PigBite },
                            Data = new PgNumericValueEx() { Value = +2F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pig Bite restores 3 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PigBite },
                            Data = new PgNumericValueEx() { Value = +3F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pig Bite restores 4 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PigBite },
                            Data = new PgNumericValueEx() { Value = +4F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pig Bite restores 5 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PigBite },
                            Data = new PgNumericValueEx() { Value = +5F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pig Bite restores 6 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PigBite },
                            Data = new PgNumericValueEx() { Value = +6F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pig Bite restores 7 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PigBite },
                            Data = new PgNumericValueEx() { Value = +7F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pig Bite restores 8 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PigBite },
                            Data = new PgNumericValueEx() { Value = +8F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pig Bite restores 9 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PigBite },
                            Data = new PgNumericValueEx() { Value = +9F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pig Bite restores 10 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PigBite },
                            Data = new PgNumericValueEx() { Value = +10F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pig Bite restores 11 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PigBite },
                            Data = new PgNumericValueEx() { Value = +11F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pig Bite restores 12 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PigBite },
                            Data = new PgNumericValueEx() { Value = +12F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pig Bite restores 13 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PigBite },
                            Data = new PgNumericValueEx() { Value = +13F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pig Bite restores 14 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PigBite },
                            Data = new PgNumericValueEx() { Value = +14F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pig Bite restores 15 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PigBite },
                            Data = new PgNumericValueEx() { Value = +15F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pig Bite restores 16 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PigBite },
                            Data = new PgNumericValueEx() { Value = +16F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pig Bite restores 17 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PigBite },
                            Data = new PgNumericValueEx() { Value = +17F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pig Bite restores 18 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PigBite },
                            Data = new PgNumericValueEx() { Value = +18F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pig Bite restores 19 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PigBite },
                            Data = new PgNumericValueEx() { Value = +19F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pig Bite restores 20 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PigBite },
                            Data = new PgNumericValueEx() { Value = +20F },
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "22083", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Strategic Chomp restores 3 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PigChomp },
                            Data = new PgNumericValueEx() { Value = +3F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Strategic Chomp restores 7 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PigChomp },
                            Data = new PgNumericValueEx() { Value = +7F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Strategic Chomp restores 11 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PigChomp },
                            Data = new PgNumericValueEx() { Value = +11F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Strategic Chomp restores 15 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PigChomp },
                            Data = new PgNumericValueEx() { Value = +15F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Strategic Chomp restores 19 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PigChomp },
                            Data = new PgNumericValueEx() { Value = +19F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Strategic Chomp restores 24 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PigChomp },
                            Data = new PgNumericValueEx() { Value = +24F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Strategic Chomp restores 29 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PigChomp },
                            Data = new PgNumericValueEx() { Value = +29F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Strategic Chomp restores 34 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PigChomp },
                            Data = new PgNumericValueEx() { Value = +34F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Strategic Chomp restores 39 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PigChomp },
                            Data = new PgNumericValueEx() { Value = +39F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Strategic Chomp restores 44 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PigChomp },
                            Data = new PgNumericValueEx() { Value = +44F },
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "23101", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Web Trap boosts your movement speed by 2 for 10 seconds",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.AddSprintSpeed,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.WebTrap },
                            Data = new PgNumericValueEx() { Value = +2F },
                            DurationInSeconds = 10,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Web Trap boosts your movement speed by 3 for 10 seconds",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.AddSprintSpeed,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.WebTrap },
                            Data = new PgNumericValueEx() { Value = +3F },
                            DurationInSeconds = 10,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Web Trap boosts your movement speed by 4 for 10 seconds",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.AddSprintSpeed,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.WebTrap },
                            Data = new PgNumericValueEx() { Value = +4F },
                            DurationInSeconds = 10,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Web Trap boosts your movement speed by 5 for 10 seconds",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.AddSprintSpeed,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.WebTrap },
                            Data = new PgNumericValueEx() { Value = +5F },
                            DurationInSeconds = 10,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Web Trap boosts your movement speed by 6 for 10 seconds",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.AddSprintSpeed,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.WebTrap },
                            Data = new PgNumericValueEx() { Value = +6F },
                            DurationInSeconds = 10,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Web Trap boosts your movement speed by 7 for 10 seconds",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.AddSprintSpeed,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.WebTrap },
                            Data = new PgNumericValueEx() { Value = +7F },
                            DurationInSeconds = 10,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Web Trap boosts your movement speed by 7.5 for 10 seconds",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.AddSprintSpeed,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.WebTrap },
                            Data = new PgNumericValueEx() { Value = +7.5F },
                            DurationInSeconds = 10,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Web Trap boosts your movement speed by 8 for 10 seconds",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.AddSprintSpeed,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.WebTrap },
                            Data = new PgNumericValueEx() { Value = +8F },
                            DurationInSeconds = 10,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Web Trap boosts your movement speed by 8.5 for 10 seconds",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.AddSprintSpeed,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.WebTrap },
                            Data = new PgNumericValueEx() { Value = +8.5F },
                            DurationInSeconds = 10,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Web Trap boosts your movement speed by 9 for 10 seconds",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.AddSprintSpeed,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.WebTrap },
                            Data = new PgNumericValueEx() { Value = +9F },
                            DurationInSeconds = 10,
                            Target = CombatTarget.Self,
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "23252", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Spider Bite and Infinite Legs restore 1 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SpiderBite, AbilityKeyword.InfiniteLegs },
                            Data = new PgNumericValueEx() { Value = +1F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Spider Bite and Infinite Legs restore 2 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SpiderBite, AbilityKeyword.InfiniteLegs },
                            Data = new PgNumericValueEx() { Value = +2F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Spider Bite and Infinite Legs restore 3 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SpiderBite, AbilityKeyword.InfiniteLegs },
                            Data = new PgNumericValueEx() { Value = +3F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Spider Bite and Infinite Legs restore 4 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SpiderBite, AbilityKeyword.InfiniteLegs },
                            Data = new PgNumericValueEx() { Value = +4F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Spider Bite and Infinite Legs restore 5 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SpiderBite, AbilityKeyword.InfiniteLegs },
                            Data = new PgNumericValueEx() { Value = +5F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Spider Bite and Infinite Legs restore 6 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SpiderBite, AbilityKeyword.InfiniteLegs },
                            Data = new PgNumericValueEx() { Value = +6F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Spider Bite and Infinite Legs restore 7 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SpiderBite, AbilityKeyword.InfiniteLegs },
                            Data = new PgNumericValueEx() { Value = +7F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Spider Bite and Infinite Legs restore 8 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SpiderBite, AbilityKeyword.InfiniteLegs },
                            Data = new PgNumericValueEx() { Value = +8F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Spider Bite and Infinite Legs restore 9 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SpiderBite, AbilityKeyword.InfiniteLegs },
                            Data = new PgNumericValueEx() { Value = +9F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Spider Bite and Infinite Legs restore 10 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SpiderBite, AbilityKeyword.InfiniteLegs },
                            Data = new PgNumericValueEx() { Value = +10F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Spider Bite and Infinite Legs restore 11 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SpiderBite, AbilityKeyword.InfiniteLegs },
                            Data = new PgNumericValueEx() { Value = +11F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Spider Bite and Infinite Legs restore 12 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SpiderBite, AbilityKeyword.InfiniteLegs },
                            Data = new PgNumericValueEx() { Value = +12F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Spider Bite and Infinite Legs restore 13 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SpiderBite, AbilityKeyword.InfiniteLegs },
                            Data = new PgNumericValueEx() { Value = +13F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Spider Bite and Infinite Legs restore 14 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SpiderBite, AbilityKeyword.InfiniteLegs },
                            Data = new PgNumericValueEx() { Value = +14F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Spider Bite and Infinite Legs restore 15 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SpiderBite, AbilityKeyword.InfiniteLegs },
                            Data = new PgNumericValueEx() { Value = +15F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Spider Bite and Infinite Legs restore 16 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SpiderBite, AbilityKeyword.InfiniteLegs },
                            Data = new PgNumericValueEx() { Value = +16F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Spider Bite and Infinite Legs restore 17 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SpiderBite, AbilityKeyword.InfiniteLegs },
                            Data = new PgNumericValueEx() { Value = +17F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Spider Bite and Infinite Legs restore 18 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SpiderBite, AbilityKeyword.InfiniteLegs },
                            Data = new PgNumericValueEx() { Value = +18F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Spider Bite and Infinite Legs restore 19 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SpiderBite, AbilityKeyword.InfiniteLegs },
                            Data = new PgNumericValueEx() { Value = +19F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Spider Bite and Infinite Legs restore 20 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SpiderBite, AbilityKeyword.InfiniteLegs },
                            Data = new PgNumericValueEx() { Value = +20F },
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "24151", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Bat Stability heals 10 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BatStability },
                            Data = new PgNumericValueEx() { Value = +10F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bat Stability heals 14 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BatStability },
                            Data = new PgNumericValueEx() { Value = +14F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bat Stability heals 18 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BatStability },
                            Data = new PgNumericValueEx() { Value = +18F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bat Stability heals 22 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BatStability },
                            Data = new PgNumericValueEx() { Value = +22F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bat Stability heals 26 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BatStability },
                            Data = new PgNumericValueEx() { Value = +26F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bat Stability heals 30 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BatStability },
                            Data = new PgNumericValueEx() { Value = +30F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bat Stability heals 34 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BatStability },
                            Data = new PgNumericValueEx() { Value = +34F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bat Stability heals 38 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BatStability },
                            Data = new PgNumericValueEx() { Value = +38F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bat Stability heals 43 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BatStability },
                            Data = new PgNumericValueEx() { Value = +43F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bat Stability heals 48 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BatStability },
                            Data = new PgNumericValueEx() { Value = +48F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bat Stability heals 53 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BatStability },
                            Data = new PgNumericValueEx() { Value = +53F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bat Stability heals 58 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BatStability },
                            Data = new PgNumericValueEx() { Value = +58F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bat Stability heals 63 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BatStability },
                            Data = new PgNumericValueEx() { Value = +63F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bat Stability heals 68 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BatStability },
                            Data = new PgNumericValueEx() { Value = +68F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bat Stability heals 73 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BatStability },
                            Data = new PgNumericValueEx() { Value = +73F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bat Stability heals 78 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BatStability },
                            Data = new PgNumericValueEx() { Value = +78F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bat Stability heals 83 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BatStability },
                            Data = new PgNumericValueEx() { Value = +83F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bat Stability heals 88 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BatStability },
                            Data = new PgNumericValueEx() { Value = +88F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bat Stability heals 93 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BatStability },
                            Data = new PgNumericValueEx() { Value = +93F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bat Stability heals 98 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BatStability },
                            Data = new PgNumericValueEx() { Value = +98F },
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "24244", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Confusing Double restores 20 Power after a 10 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ConfusingDouble },
                            Data = new PgNumericValueEx() { Value = +20F },
                            DelayInSeconds = 10,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Confusing Double restores 40 Power after a 10 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ConfusingDouble },
                            Data = new PgNumericValueEx() { Value = +40F },
                            DelayInSeconds = 10,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Confusing Double restores 60 Power after a 10 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ConfusingDouble },
                            Data = new PgNumericValueEx() { Value = +60F },
                            DelayInSeconds = 10,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Confusing Double restores 80 Power after a 10 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ConfusingDouble },
                            Data = new PgNumericValueEx() { Value = +80F },
                            DelayInSeconds = 10,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Confusing Double restores 100 Power after a 10 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ConfusingDouble },
                            Data = new PgNumericValueEx() { Value = +100F },
                            DelayInSeconds = 10,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Confusing Double restores 120 Power after a 10 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ConfusingDouble },
                            Data = new PgNumericValueEx() { Value = +120F },
                            DelayInSeconds = 10,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Confusing Double restores 140 Power after a 10 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ConfusingDouble },
                            Data = new PgNumericValueEx() { Value = +140F },
                            DelayInSeconds = 10,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Confusing Double restores 160 Power after a 10 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ConfusingDouble },
                            Data = new PgNumericValueEx() { Value = +160F },
                            DelayInSeconds = 10,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Confusing Double restores 180 Power after a 10 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ConfusingDouble },
                            Data = new PgNumericValueEx() { Value = +180F },
                            DelayInSeconds = 10,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Confusing Double restores 200 Power after a 10 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ConfusingDouble },
                            Data = new PgNumericValueEx() { Value = +200F },
                            DelayInSeconds = 10,
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "25131", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Play Dead restores 15 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PlayDead },
                            Data = new PgNumericValueEx() { Value = +15F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Play Dead restores 20 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PlayDead },
                            Data = new PgNumericValueEx() { Value = +20F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Play Dead restores 25 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PlayDead },
                            Data = new PgNumericValueEx() { Value = +25F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Play Dead restores 30 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PlayDead },
                            Data = new PgNumericValueEx() { Value = +30F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Play Dead restores 35 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PlayDead },
                            Data = new PgNumericValueEx() { Value = +35F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Play Dead restores 40 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PlayDead },
                            Data = new PgNumericValueEx() { Value = +40F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Play Dead restores 45 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PlayDead },
                            Data = new PgNumericValueEx() { Value = +45F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Play Dead restores 50 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PlayDead },
                            Data = new PgNumericValueEx() { Value = +50F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Play Dead restores 55 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PlayDead },
                            Data = new PgNumericValueEx() { Value = +55F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Play Dead restores 60 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PlayDead },
                            Data = new PgNumericValueEx() { Value = +60F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Play Dead restores 65 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PlayDead },
                            Data = new PgNumericValueEx() { Value = +65F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Play Dead restores 70 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PlayDead },
                            Data = new PgNumericValueEx() { Value = +70F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Play Dead restores 75 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PlayDead },
                            Data = new PgNumericValueEx() { Value = +75F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Play Dead restores 80 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PlayDead },
                            Data = new PgNumericValueEx() { Value = +80F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Play Dead restores 85 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PlayDead },
                            Data = new PgNumericValueEx() { Value = +85F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Play Dead restores 90 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PlayDead },
                            Data = new PgNumericValueEx() { Value = +90F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Play Dead restores 95 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PlayDead },
                            Data = new PgNumericValueEx() { Value = +95F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Play Dead restores 100 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PlayDead },
                            Data = new PgNumericValueEx() { Value = +100F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Play Dead restores 105 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PlayDead },
                            Data = new PgNumericValueEx() { Value = +105F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Play Dead restores 110 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PlayDead },
                            Data = new PgNumericValueEx() { Value = +110F },
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "25192", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Carrot Power restores 15 Health after an 8-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CarrotPower },
                            Data = new PgNumericValueEx() { Value = +15F },
                            DelayInSeconds = 8,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Carrot Power restores 30 Health after an 8-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CarrotPower },
                            Data = new PgNumericValueEx() { Value = +30F },
                            DelayInSeconds = 8,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Carrot Power restores 45 Health after an 8-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CarrotPower },
                            Data = new PgNumericValueEx() { Value = +45F },
                            DelayInSeconds = 8,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Carrot Power restores 60 Health after an 8-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CarrotPower },
                            Data = new PgNumericValueEx() { Value = +60F },
                            DelayInSeconds = 8,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Carrot Power restores 75 Health after an 8-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CarrotPower },
                            Data = new PgNumericValueEx() { Value = +75F },
                            DelayInSeconds = 8,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Carrot Power restores 90 Health after an 8-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CarrotPower },
                            Data = new PgNumericValueEx() { Value = +90F },
                            DelayInSeconds = 8,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Carrot Power restores 105 Health after an 8-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CarrotPower },
                            Data = new PgNumericValueEx() { Value = +105F },
                            DelayInSeconds = 8,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Carrot Power restores 120 Health after an 8-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CarrotPower },
                            Data = new PgNumericValueEx() { Value = +120F },
                            DelayInSeconds = 8,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Carrot Power restores 135 Health after an 8-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CarrotPower },
                            Data = new PgNumericValueEx() { Value = +135F },
                            DelayInSeconds = 8,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Carrot Power restores 150 Health after an 8-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CarrotPower },
                            Data = new PgNumericValueEx() { Value = +150F },
                            DelayInSeconds = 8,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Carrot Power restores 165 Health after an 8-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CarrotPower },
                            Data = new PgNumericValueEx() { Value = +165F },
                            DelayInSeconds = 8,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Carrot Power restores 180 Health after an 8-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CarrotPower },
                            Data = new PgNumericValueEx() { Value = +180F },
                            DelayInSeconds = 8,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Carrot Power restores 195 Health after an 8-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CarrotPower },
                            Data = new PgNumericValueEx() { Value = +195F },
                            DelayInSeconds = 8,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Carrot Power restores 210 Health after an 8-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CarrotPower },
                            Data = new PgNumericValueEx() { Value = +210F },
                            DelayInSeconds = 8,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Carrot Power restores 225 Health after an 8-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CarrotPower },
                            Data = new PgNumericValueEx() { Value = +225F },
                            DelayInSeconds = 8,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Carrot Power restores 240 Health after an 8-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CarrotPower },
                            Data = new PgNumericValueEx() { Value = +240F },
                            DelayInSeconds = 8,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Carrot Power restores 255 Health after an 8-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CarrotPower },
                            Data = new PgNumericValueEx() { Value = +255F },
                            DelayInSeconds = 8,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Carrot Power restores 270 Health after an 8-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CarrotPower },
                            Data = new PgNumericValueEx() { Value = +270F },
                            DelayInSeconds = 8,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Carrot Power restores 285 Health after an 8-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CarrotPower },
                            Data = new PgNumericValueEx() { Value = +285F },
                            DelayInSeconds = 8,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Carrot Power restores 300 Health after an 8-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CarrotPower },
                            Data = new PgNumericValueEx() { Value = +300F },
                            DelayInSeconds = 8,
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "257", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Crossbow abilities restore 15 health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Crossbow },
                            Data = new PgNumericValueEx() { Value = +15F },
                            DelayInSeconds = 15,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Crossbow abilities restore 30 health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Crossbow },
                            Data = new PgNumericValueEx() { Value = +30F },
                            DelayInSeconds = 15,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Crossbow abilities restore 45 health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Crossbow },
                            Data = new PgNumericValueEx() { Value = +45F },
                            DelayInSeconds = 15,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Crossbow abilities restore 60 health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Crossbow },
                            Data = new PgNumericValueEx() { Value = +60F },
                            DelayInSeconds = 15,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Crossbow abilities restore 75 health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Crossbow },
                            Data = new PgNumericValueEx() { Value = +75F },
                            DelayInSeconds = 15,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Crossbow abilities restore 90 health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Crossbow },
                            Data = new PgNumericValueEx() { Value = +90F },
                            DelayInSeconds = 15,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Crossbow abilities restore 105 health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Crossbow },
                            Data = new PgNumericValueEx() { Value = +105F },
                            DelayInSeconds = 15,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Crossbow abilities restore 120 health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Crossbow },
                            Data = new PgNumericValueEx() { Value = +120F },
                            DelayInSeconds = 15,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Crossbow abilities restore 135 health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Crossbow },
                            Data = new PgNumericValueEx() { Value = +135F },
                            DelayInSeconds = 15,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Crossbow abilities restore 150 health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Crossbow },
                            Data = new PgNumericValueEx() { Value = +150F },
                            DelayInSeconds = 15,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Crossbow abilities restore 165 health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Crossbow },
                            Data = new PgNumericValueEx() { Value = +165F },
                            DelayInSeconds = 15,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Crossbow abilities restore 180 health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Crossbow },
                            Data = new PgNumericValueEx() { Value = +180F },
                            DelayInSeconds = 15,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Crossbow abilities restore 195 health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Crossbow },
                            Data = new PgNumericValueEx() { Value = +195F },
                            DelayInSeconds = 15,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Crossbow abilities restore 210 health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Crossbow },
                            Data = new PgNumericValueEx() { Value = +210F },
                            DelayInSeconds = 15,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Crossbow abilities restore 225 health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Crossbow },
                            Data = new PgNumericValueEx() { Value = +225F },
                            DelayInSeconds = 15,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Crossbow abilities restore 240 health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Crossbow },
                            Data = new PgNumericValueEx() { Value = +240F },
                            DelayInSeconds = 15,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Crossbow abilities restore 255 health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Crossbow },
                            Data = new PgNumericValueEx() { Value = +255F },
                            DelayInSeconds = 15,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Crossbow abilities restore 270 health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Crossbow },
                            Data = new PgNumericValueEx() { Value = +270F },
                            DelayInSeconds = 15,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Crossbow abilities restore 285 health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Crossbow },
                            Data = new PgNumericValueEx() { Value = +285F },
                            DelayInSeconds = 15,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Crossbow abilities restore 300 health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Crossbow },
                            Data = new PgNumericValueEx() { Value = +300F },
                            DelayInSeconds = 15,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Crossbow abilities restore 315 health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Crossbow },
                            Data = new PgNumericValueEx() { Value = +315F },
                            DelayInSeconds = 15,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Crossbow abilities restore 330 health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Crossbow },
                            Data = new PgNumericValueEx() { Value = +330F },
                            DelayInSeconds = 15,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Crossbow abilities restore 345 health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Crossbow },
                            Data = new PgNumericValueEx() { Value = +345F },
                            DelayInSeconds = 15,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Crossbow abilities restore 360 health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Crossbow },
                            Data = new PgNumericValueEx() { Value = +360F },
                            DelayInSeconds = 15,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Crossbow abilities restore 375 health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Crossbow },
                            Data = new PgNumericValueEx() { Value = +375F },
                            DelayInSeconds = 15,
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "28184", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Power Glyph and Galvanize restore +3 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PowerGlyph, AbilityKeyword.Galvanize },
                            Data = new PgNumericValueEx() { Value = +3F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Power Glyph and Galvanize restore +6 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PowerGlyph, AbilityKeyword.Galvanize },
                            Data = new PgNumericValueEx() { Value = +6F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Power Glyph and Galvanize restore +9 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PowerGlyph, AbilityKeyword.Galvanize },
                            Data = new PgNumericValueEx() { Value = +9F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Power Glyph and Galvanize restore +12 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PowerGlyph, AbilityKeyword.Galvanize },
                            Data = new PgNumericValueEx() { Value = +12F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Power Glyph and Galvanize restore +15 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PowerGlyph, AbilityKeyword.Galvanize },
                            Data = new PgNumericValueEx() { Value = +15F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Power Glyph and Galvanize restore +18 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PowerGlyph, AbilityKeyword.Galvanize },
                            Data = new PgNumericValueEx() { Value = +18F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Power Glyph and Galvanize restore +21 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PowerGlyph, AbilityKeyword.Galvanize },
                            Data = new PgNumericValueEx() { Value = +21F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Power Glyph and Galvanize restore +24 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PowerGlyph, AbilityKeyword.Galvanize },
                            Data = new PgNumericValueEx() { Value = +24F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Power Glyph and Galvanize restore +27 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PowerGlyph, AbilityKeyword.Galvanize },
                            Data = new PgNumericValueEx() { Value = +27F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Power Glyph and Galvanize restore +31 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PowerGlyph, AbilityKeyword.Galvanize },
                            Data = new PgNumericValueEx() { Value = +31F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Power Glyph and Galvanize restore +35 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PowerGlyph, AbilityKeyword.Galvanize },
                            Data = new PgNumericValueEx() { Value = +35F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Power Glyph and Galvanize restore +39 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PowerGlyph, AbilityKeyword.Galvanize },
                            Data = new PgNumericValueEx() { Value = +39F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Power Glyph and Galvanize restore +43 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PowerGlyph, AbilityKeyword.Galvanize },
                            Data = new PgNumericValueEx() { Value = +43F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Power Glyph and Galvanize restore +47 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PowerGlyph, AbilityKeyword.Galvanize },
                            Data = new PgNumericValueEx() { Value = +47F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Power Glyph and Galvanize restore +51 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PowerGlyph, AbilityKeyword.Galvanize },
                            Data = new PgNumericValueEx() { Value = +51F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Power Glyph and Galvanize restore +55 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PowerGlyph, AbilityKeyword.Galvanize },
                            Data = new PgNumericValueEx() { Value = +55F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Power Glyph and Galvanize restore +59 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PowerGlyph, AbilityKeyword.Galvanize },
                            Data = new PgNumericValueEx() { Value = +59F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Power Glyph and Galvanize restore +63 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PowerGlyph, AbilityKeyword.Galvanize },
                            Data = new PgNumericValueEx() { Value = +63F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Power Glyph and Galvanize restore +67 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PowerGlyph, AbilityKeyword.Galvanize },
                            Data = new PgNumericValueEx() { Value = +67F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Power Glyph and Galvanize restore +71 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PowerGlyph, AbilityKeyword.Galvanize },
                            Data = new PgNumericValueEx() { Value = +71F },
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "3253", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Bruising Blow and Headbutt restore 2 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BruisingBlow, AbilityKeyword.Headbutt },
                            Data = new PgNumericValueEx() { Value = +2F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bruising Blow and Headbutt restore 3 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BruisingBlow, AbilityKeyword.Headbutt },
                            Data = new PgNumericValueEx() { Value = +3F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bruising Blow and Headbutt restore 5 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BruisingBlow, AbilityKeyword.Headbutt },
                            Data = new PgNumericValueEx() { Value = +5F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bruising Blow and Headbutt restore 6 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BruisingBlow, AbilityKeyword.Headbutt },
                            Data = new PgNumericValueEx() { Value = +6F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bruising Blow and Headbutt restore 8 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BruisingBlow, AbilityKeyword.Headbutt },
                            Data = new PgNumericValueEx() { Value = +8F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bruising Blow and Headbutt restore 9 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BruisingBlow, AbilityKeyword.Headbutt },
                            Data = new PgNumericValueEx() { Value = +9F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bruising Blow and Headbutt restore 11 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BruisingBlow, AbilityKeyword.Headbutt },
                            Data = new PgNumericValueEx() { Value = +11F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bruising Blow and Headbutt restore 13 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BruisingBlow, AbilityKeyword.Headbutt },
                            Data = new PgNumericValueEx() { Value = +13F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bruising Blow and Headbutt restore 15 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BruisingBlow, AbilityKeyword.Headbutt },
                            Data = new PgNumericValueEx() { Value = +15F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bruising Blow and Headbutt restore 17 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BruisingBlow, AbilityKeyword.Headbutt },
                            Data = new PgNumericValueEx() { Value = +17F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bruising Blow and Headbutt restore 18 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BruisingBlow, AbilityKeyword.Headbutt },
                            Data = new PgNumericValueEx() { Value = +18F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bruising Blow and Headbutt restore 20 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BruisingBlow, AbilityKeyword.Headbutt },
                            Data = new PgNumericValueEx() { Value = +20F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bruising Blow and Headbutt restore 22 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BruisingBlow, AbilityKeyword.Headbutt },
                            Data = new PgNumericValueEx() { Value = +22F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bruising Blow and Headbutt restore 24 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BruisingBlow, AbilityKeyword.Headbutt },
                            Data = new PgNumericValueEx() { Value = +24F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bruising Blow and Headbutt restore 26 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BruisingBlow, AbilityKeyword.Headbutt },
                            Data = new PgNumericValueEx() { Value = +26F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bruising Blow and Headbutt restore 28 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BruisingBlow, AbilityKeyword.Headbutt },
                            Data = new PgNumericValueEx() { Value = +28F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bruising Blow and Headbutt restore 30 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BruisingBlow, AbilityKeyword.Headbutt },
                            Data = new PgNumericValueEx() { Value = +30F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bruising Blow and Headbutt restore 32 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BruisingBlow, AbilityKeyword.Headbutt },
                            Data = new PgNumericValueEx() { Value = +32F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bruising Blow and Headbutt restore 34 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BruisingBlow, AbilityKeyword.Headbutt },
                            Data = new PgNumericValueEx() { Value = +34F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bruising Blow and Headbutt restore 36 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BruisingBlow, AbilityKeyword.Headbutt },
                            Data = new PgNumericValueEx() { Value = +36F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bruising Blow and Headbutt restore 38 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BruisingBlow, AbilityKeyword.Headbutt },
                            Data = new PgNumericValueEx() { Value = +38F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bruising Blow and Headbutt restore 40 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BruisingBlow, AbilityKeyword.Headbutt },
                            Data = new PgNumericValueEx() { Value = +40F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bruising Blow and Headbutt restore 42 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BruisingBlow, AbilityKeyword.Headbutt },
                            Data = new PgNumericValueEx() { Value = +42F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bruising Blow and Headbutt restore 44 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BruisingBlow, AbilityKeyword.Headbutt },
                            Data = new PgNumericValueEx() { Value = +44F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bruising Blow and Headbutt restore 46 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BruisingBlow, AbilityKeyword.Headbutt },
                            Data = new PgNumericValueEx() { Value = +46F },
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "4082", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Pep Talk restores 14 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PepTalk },
                            Data = new PgNumericValueEx() { Value = +14F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pep Talk restores 18 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PepTalk },
                            Data = new PgNumericValueEx() { Value = +18F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pep Talk restores 22 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PepTalk },
                            Data = new PgNumericValueEx() { Value = +22F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pep Talk restores 26 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PepTalk },
                            Data = new PgNumericValueEx() { Value = +26F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pep Talk restores 30 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PepTalk },
                            Data = new PgNumericValueEx() { Value = +30F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pep Talk restores 34 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PepTalk },
                            Data = new PgNumericValueEx() { Value = +34F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pep Talk restores 38 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PepTalk },
                            Data = new PgNumericValueEx() { Value = +38F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pep Talk restores 42 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PepTalk },
                            Data = new PgNumericValueEx() { Value = +42F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pep Talk restores 46 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PepTalk },
                            Data = new PgNumericValueEx() { Value = +46F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pep Talk restores 50 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PepTalk },
                            Data = new PgNumericValueEx() { Value = +50F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pep Talk restores 54 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PepTalk },
                            Data = new PgNumericValueEx() { Value = +54F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pep Talk restores 58 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PepTalk },
                            Data = new PgNumericValueEx() { Value = +58F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pep Talk restores 62 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PepTalk },
                            Data = new PgNumericValueEx() { Value = +62F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pep Talk restores 66 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PepTalk },
                            Data = new PgNumericValueEx() { Value = +66F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pep Talk restores 70 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PepTalk },
                            Data = new PgNumericValueEx() { Value = +70F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pep Talk restores 74 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PepTalk },
                            Data = new PgNumericValueEx() { Value = +74F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pep Talk restores 78 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PepTalk },
                            Data = new PgNumericValueEx() { Value = +78F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pep Talk restores 82 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PepTalk },
                            Data = new PgNumericValueEx() { Value = +82F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pep Talk restores 86 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PepTalk },
                            Data = new PgNumericValueEx() { Value = +86F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pep Talk restores 90 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PepTalk },
                            Data = new PgNumericValueEx() { Value = +90F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pep Talk restores 94 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PepTalk },
                            Data = new PgNumericValueEx() { Value = +94F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pep Talk restores 98 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PepTalk },
                            Data = new PgNumericValueEx() { Value = +98F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pep Talk restores 102 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PepTalk },
                            Data = new PgNumericValueEx() { Value = +102F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pep Talk restores 106 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PepTalk },
                            Data = new PgNumericValueEx() { Value = +106F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pep Talk restores 110 Power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PepTalk },
                            Data = new PgNumericValueEx() { Value = +110F },
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "4532", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Ridicule boosts movement speed by 1 for 6 seconds",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.AddSprintSpeed,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Ridicule },
                            Data = new PgNumericValueEx() { Value = +1F },
                            DurationInSeconds = 6,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ridicule boosts movement speed by 1.5 for 6 seconds",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.AddSprintSpeed,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Ridicule },
                            Data = new PgNumericValueEx() { Value = +1.5F },
                            DurationInSeconds = 6,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ridicule boosts movement speed by 2 for 6 seconds",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.AddSprintSpeed,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Ridicule },
                            Data = new PgNumericValueEx() { Value = +2F },
                            DurationInSeconds = 6,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ridicule boosts movement speed by 2.5 for 6 seconds",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.AddSprintSpeed,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Ridicule },
                            Data = new PgNumericValueEx() { Value = +2.5F },
                            DurationInSeconds = 6,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ridicule boosts movement speed by 3 for 6 seconds",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.AddSprintSpeed,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Ridicule },
                            Data = new PgNumericValueEx() { Value = +3F },
                            DurationInSeconds = 6,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ridicule boosts movement speed by 3.5 for 6 seconds",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.AddSprintSpeed,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Ridicule },
                            Data = new PgNumericValueEx() { Value = +3.5F },
                            DurationInSeconds = 6,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ridicule boosts movement speed by 4 for 6 seconds",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.AddSprintSpeed,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Ridicule },
                            Data = new PgNumericValueEx() { Value = +4F },
                            DurationInSeconds = 6,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ridicule boosts movement speed by 4.5 for 6 seconds",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.AddSprintSpeed,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Ridicule },
                            Data = new PgNumericValueEx() { Value = +4.5F },
                            DurationInSeconds = 6,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ridicule boosts movement speed by 5 for 6 seconds",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.AddSprintSpeed,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Ridicule },
                            Data = new PgNumericValueEx() { Value = +5F },
                            DurationInSeconds = 6,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ridicule boosts movement speed by 5.5 for 6 seconds",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.AddSprintSpeed,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Ridicule },
                            Data = new PgNumericValueEx() { Value = +5.5F },
                            DurationInSeconds = 6,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ridicule boosts movement speed by 6 for 6 seconds",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ridicule boosts movement speed by 6.5 for 6 seconds",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.AddSprintSpeed,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Ridicule },
                            Data = new PgNumericValueEx() { Value = +6.5F },
                            DurationInSeconds = 6,
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "6003", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Claw and Double Claw restore 2 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Claw, AbilityKeyword.DoubleClaw },
                            Data = new PgNumericValueEx() { Value = +2F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Claw and Double Claw restore 3 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Claw, AbilityKeyword.DoubleClaw },
                            Data = new PgNumericValueEx() { Value = +3F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Claw and Double Claw restore 4 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Claw, AbilityKeyword.DoubleClaw },
                            Data = new PgNumericValueEx() { Value = +4F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Claw and Double Claw restore 5 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Claw, AbilityKeyword.DoubleClaw },
                            Data = new PgNumericValueEx() { Value = +5F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Claw and Double Claw restore 6 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Claw, AbilityKeyword.DoubleClaw },
                            Data = new PgNumericValueEx() { Value = +6F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Claw and Double Claw restore 7 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Claw, AbilityKeyword.DoubleClaw },
                            Data = new PgNumericValueEx() { Value = +7F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Claw and Double Claw restore 8 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Claw, AbilityKeyword.DoubleClaw },
                            Data = new PgNumericValueEx() { Value = +8F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Claw and Double Claw restore 9 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Claw, AbilityKeyword.DoubleClaw },
                            Data = new PgNumericValueEx() { Value = +9F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Claw and Double Claw restore 10 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Claw, AbilityKeyword.DoubleClaw },
                            Data = new PgNumericValueEx() { Value = +10F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Claw and Double Claw restore 11 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Claw, AbilityKeyword.DoubleClaw },
                            Data = new PgNumericValueEx() { Value = +11F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Claw and Double Claw restore 12 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Claw, AbilityKeyword.DoubleClaw },
                            Data = new PgNumericValueEx() { Value = +12F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Claw and Double Claw restore 13 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Claw, AbilityKeyword.DoubleClaw },
                            Data = new PgNumericValueEx() { Value = +13F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Claw and Double Claw restore 14 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Claw, AbilityKeyword.DoubleClaw },
                            Data = new PgNumericValueEx() { Value = +14F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Claw and Double Claw restore 15 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Claw, AbilityKeyword.DoubleClaw },
                            Data = new PgNumericValueEx() { Value = +15F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Claw and Double Claw restore 16 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Claw, AbilityKeyword.DoubleClaw },
                            Data = new PgNumericValueEx() { Value = +16F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Claw and Double Claw restore 18 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Claw, AbilityKeyword.DoubleClaw },
                            Data = new PgNumericValueEx() { Value = +18F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Claw and Double Claw restore 19 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Claw, AbilityKeyword.DoubleClaw },
                            Data = new PgNumericValueEx() { Value = +19F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Claw and Double Claw restore 21 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Claw, AbilityKeyword.DoubleClaw },
                            Data = new PgNumericValueEx() { Value = +21F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Claw and Double Claw restore 22 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Claw, AbilityKeyword.DoubleClaw },
                            Data = new PgNumericValueEx() { Value = +22F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Claw and Double Claw restore 23 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Claw, AbilityKeyword.DoubleClaw },
                            Data = new PgNumericValueEx() { Value = +23F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Claw and Double Claw restore 24 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Claw, AbilityKeyword.DoubleClaw },
                            Data = new PgNumericValueEx() { Value = +24F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Claw and Double Claw restore 25 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Claw, AbilityKeyword.DoubleClaw },
                            Data = new PgNumericValueEx() { Value = +25F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Claw and Double Claw restore 26 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Claw, AbilityKeyword.DoubleClaw },
                            Data = new PgNumericValueEx() { Value = +26F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Claw and Double Claw restore 28 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Claw, AbilityKeyword.DoubleClaw },
                            Data = new PgNumericValueEx() { Value = +28F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Claw and Double Claw restore 30 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Claw, AbilityKeyword.DoubleClaw },
                            Data = new PgNumericValueEx() { Value = +30F },
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "7201", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Healing Mist heals +7 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +7F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist heals +12 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +12F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist heals +17 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +17F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist heals +22 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +22F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist heals +27 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +27F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist heals +32 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +32F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist heals +37 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +37F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist heals +42 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +42F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist heals +47 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +47F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist heals +52 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +52F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist heals +57 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +57F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist heals +62 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +62F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist heals +67 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +67F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist heals +72 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +72F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist heals +77 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +77F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist heals +82 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +82F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist heals +87 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +87F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist heals +92 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +92F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist heals +97 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +97F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist heals +102 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +102F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist heals +107 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +107F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist heals +112 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +112F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist heals +117 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +117F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist heals +122 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +122F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist heals +127 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +127F },
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "7205", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Healing Mist restores 6 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +6F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist restores 10 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +10F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist restores 14 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +14F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist restores 18 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +18F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist restores 22 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +22F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist restores 26 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +26F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist restores 30 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +30F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist restores 34 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +34F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist restores 38 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +38F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist restores 42 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +42F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist restores 46 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +46F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist restores 50 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +50F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist restores 54 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +54F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist restores 58 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +58F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist restores 62 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +62F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist restores 66 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +66F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist restores 70 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +70F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist restores 74 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +74F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist restores 78 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +78F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist restores 82 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +82F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist restores 86 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +86F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist restores 90 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +90F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist restores 94 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +94F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist restores 98 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +98F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Mist restores 102 power",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingMist },
                            Data = new PgNumericValueEx() { Value = +102F },
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "7206", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Healing Injection heals 15 Health after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingInjection },
                            Data = new PgNumericValueEx() { Value = +15F },
                            DelayInSeconds = 20,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Injection heals 20 Health after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingInjection },
                            Data = new PgNumericValueEx() { Value = +20F },
                            DelayInSeconds = 20,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Injection heals 25 Health after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingInjection },
                            Data = new PgNumericValueEx() { Value = +25F },
                            DelayInSeconds = 20,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Injection heals 30 Health after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingInjection },
                            Data = new PgNumericValueEx() { Value = +30F },
                            DelayInSeconds = 20,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Injection heals 35 Health after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingInjection },
                            Data = new PgNumericValueEx() { Value = +35F },
                            DelayInSeconds = 20,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Injection heals 40 Health after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingInjection },
                            Data = new PgNumericValueEx() { Value = +40F },
                            DelayInSeconds = 20,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Injection heals 45 Health after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingInjection },
                            Data = new PgNumericValueEx() { Value = +45F },
                            DelayInSeconds = 20,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Injection heals 50 Health after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingInjection },
                            Data = new PgNumericValueEx() { Value = +50F },
                            DelayInSeconds = 20,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Injection heals 55 Health after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingInjection },
                            Data = new PgNumericValueEx() { Value = +55F },
                            DelayInSeconds = 20,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Injection heals 60 Health after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingInjection },
                            Data = new PgNumericValueEx() { Value = +60F },
                            DelayInSeconds = 20,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Injection heals 65 Health after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingInjection },
                            Data = new PgNumericValueEx() { Value = +65F },
                            DelayInSeconds = 20,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Injection heals 70 Health after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingInjection },
                            Data = new PgNumericValueEx() { Value = +70F },
                            DelayInSeconds = 20,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Injection heals 75 Health after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingInjection },
                            Data = new PgNumericValueEx() { Value = +75F },
                            DelayInSeconds = 20,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Injection heals 80 Health after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingInjection },
                            Data = new PgNumericValueEx() { Value = +80F },
                            DelayInSeconds = 20,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Injection heals 85 Health after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingInjection },
                            Data = new PgNumericValueEx() { Value = +85F },
                            DelayInSeconds = 20,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Injection heals 90 Health after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingInjection },
                            Data = new PgNumericValueEx() { Value = +90F },
                            DelayInSeconds = 20,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Injection heals 95 Health after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingInjection },
                            Data = new PgNumericValueEx() { Value = +95F },
                            DelayInSeconds = 20,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Injection heals 100 Health after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingInjection },
                            Data = new PgNumericValueEx() { Value = +100F },
                            DelayInSeconds = 20,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Injection heals 105 Health after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingInjection },
                            Data = new PgNumericValueEx() { Value = +105F },
                            DelayInSeconds = 20,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Injection heals 110 Health after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingInjection },
                            Data = new PgNumericValueEx() { Value = +110F },
                            DelayInSeconds = 20,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Injection heals 115 Health after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingInjection },
                            Data = new PgNumericValueEx() { Value = +115F },
                            DelayInSeconds = 20,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Injection heals 120 Health after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingInjection },
                            Data = new PgNumericValueEx() { Value = +120F },
                            DelayInSeconds = 20,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Injection heals 125 Health after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingInjection },
                            Data = new PgNumericValueEx() { Value = +125F },
                            DelayInSeconds = 20,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Injection heals 130 Health after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingInjection },
                            Data = new PgNumericValueEx() { Value = +130F },
                            DelayInSeconds = 20,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Healing Injection heals 135 Health after a 20 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HealingInjection },
                            Data = new PgNumericValueEx() { Value = +135F },
                            DelayInSeconds = 20,
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "8005", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Life Steal restores 4 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LifeSteal },
                            Data = new PgNumericValueEx() { Value = +4F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Life Steal restores 6 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LifeSteal },
                            Data = new PgNumericValueEx() { Value = +6F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Life Steal restores 8 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LifeSteal },
                            Data = new PgNumericValueEx() { Value = +8F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Life Steal restores 10 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LifeSteal },
                            Data = new PgNumericValueEx() { Value = +10F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Life Steal restores 12 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LifeSteal },
                            Data = new PgNumericValueEx() { Value = +12F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Life Steal restores 14 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LifeSteal },
                            Data = new PgNumericValueEx() { Value = +14F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Life Steal restores 16 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LifeSteal },
                            Data = new PgNumericValueEx() { Value = +16F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Life Steal restores 18 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LifeSteal },
                            Data = new PgNumericValueEx() { Value = +18F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Life Steal restores 20 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LifeSteal },
                            Data = new PgNumericValueEx() { Value = +20F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Life Steal restores 22 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LifeSteal },
                            Data = new PgNumericValueEx() { Value = +22F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Life Steal restores 24 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LifeSteal },
                            Data = new PgNumericValueEx() { Value = +24F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Life Steal restores 26 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LifeSteal },
                            Data = new PgNumericValueEx() { Value = +26F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Life Steal restores 28 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LifeSteal },
                            Data = new PgNumericValueEx() { Value = +28F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Life Steal restores 30 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LifeSteal },
                            Data = new PgNumericValueEx() { Value = +30F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Life Steal restores 32 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LifeSteal },
                            Data = new PgNumericValueEx() { Value = +32F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Life Steal restores 34 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LifeSteal },
                            Data = new PgNumericValueEx() { Value = +34F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Life Steal restores 36 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LifeSteal },
                            Data = new PgNumericValueEx() { Value = +36F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Life Steal restores 38 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LifeSteal },
                            Data = new PgNumericValueEx() { Value = +38F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Life Steal restores 40 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LifeSteal },
                            Data = new PgNumericValueEx() { Value = +40F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Life Steal restores 42 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LifeSteal },
                            Data = new PgNumericValueEx() { Value = +42F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Life Steal restores 44 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LifeSteal },
                            Data = new PgNumericValueEx() { Value = +44F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Life Steal restores 46 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LifeSteal },
                            Data = new PgNumericValueEx() { Value = +46F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Life Steal restores 48 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LifeSteal },
                            Data = new PgNumericValueEx() { Value = +48F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Life Steal restores 50 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LifeSteal },
                            Data = new PgNumericValueEx() { Value = +50F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Life Steal restores 52 Health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LifeSteal },
                            Data = new PgNumericValueEx() { Value = +52F },
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "9005", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Electrify, System Shock, and Panic Charge restore 2 Health after a 7 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify, AbilityKeyword.SystemShock, AbilityKeyword.PanicCharge },
                            Data = new PgNumericValueEx() { Value = +2F },
                            DelayInSeconds = 7,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify, System Shock, and Panic Charge restore 4 Health after a 7 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify, AbilityKeyword.SystemShock, AbilityKeyword.PanicCharge },
                            Data = new PgNumericValueEx() { Value = +4F },
                            DelayInSeconds = 7,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify, System Shock, and Panic Charge restore 6 Health after a 7 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify, AbilityKeyword.SystemShock, AbilityKeyword.PanicCharge },
                            Data = new PgNumericValueEx() { Value = +6F },
                            DelayInSeconds = 7,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify, System Shock, and Panic Charge restore 8 Health after a 7 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify, AbilityKeyword.SystemShock, AbilityKeyword.PanicCharge },
                            Data = new PgNumericValueEx() { Value = +8F },
                            DelayInSeconds = 7,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify, System Shock, and Panic Charge restore 10 Health after a 7 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify, AbilityKeyword.SystemShock, AbilityKeyword.PanicCharge },
                            Data = new PgNumericValueEx() { Value = +10F },
                            DelayInSeconds = 7,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify, System Shock, and Panic Charge restore 12 Health after a 7 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify, AbilityKeyword.SystemShock, AbilityKeyword.PanicCharge },
                            Data = new PgNumericValueEx() { Value = +12F },
                            DelayInSeconds = 7,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify, System Shock, and Panic Charge restore 14 Health after a 7 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify, AbilityKeyword.SystemShock, AbilityKeyword.PanicCharge },
                            Data = new PgNumericValueEx() { Value = +14F },
                            DelayInSeconds = 7,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify, System Shock, and Panic Charge restore 17 Health after a 7 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify, AbilityKeyword.SystemShock, AbilityKeyword.PanicCharge },
                            Data = new PgNumericValueEx() { Value = +17F },
                            DelayInSeconds = 7,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify, System Shock, and Panic Charge restore 20 Health after a 7 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify, AbilityKeyword.SystemShock, AbilityKeyword.PanicCharge },
                            Data = new PgNumericValueEx() { Value = +20F },
                            DelayInSeconds = 7,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify, System Shock, and Panic Charge restore 23 Health after a 7 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify, AbilityKeyword.SystemShock, AbilityKeyword.PanicCharge },
                            Data = new PgNumericValueEx() { Value = +23F },
                            DelayInSeconds = 7,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify, System Shock, and Panic Charge restore 26 Health after a 7 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify, AbilityKeyword.SystemShock, AbilityKeyword.PanicCharge },
                            Data = new PgNumericValueEx() { Value = +26F },
                            DelayInSeconds = 7,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify, System Shock, and Panic Charge restore 29 Health after a 7 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify, AbilityKeyword.SystemShock, AbilityKeyword.PanicCharge },
                            Data = new PgNumericValueEx() { Value = +29F },
                            DelayInSeconds = 7,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify, System Shock, and Panic Charge restore 32 Health after a 7 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify, AbilityKeyword.SystemShock, AbilityKeyword.PanicCharge },
                            Data = new PgNumericValueEx() { Value = +32F },
                            DelayInSeconds = 7,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify, System Shock, and Panic Charge restore 35 Health after a 7 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify, AbilityKeyword.SystemShock, AbilityKeyword.PanicCharge },
                            Data = new PgNumericValueEx() { Value = +35F },
                            DelayInSeconds = 7,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify, System Shock, and Panic Charge restore 38 Health after a 7 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify, AbilityKeyword.SystemShock, AbilityKeyword.PanicCharge },
                            Data = new PgNumericValueEx() { Value = +38F },
                            DelayInSeconds = 7,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify, System Shock, and Panic Charge restore 41 Health after a 7 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify, AbilityKeyword.SystemShock, AbilityKeyword.PanicCharge },
                            Data = new PgNumericValueEx() { Value = +41F },
                            DelayInSeconds = 7,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify, System Shock, and Panic Charge restore 44 Health after a 7 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify, AbilityKeyword.SystemShock, AbilityKeyword.PanicCharge },
                            Data = new PgNumericValueEx() { Value = +44F },
                            DelayInSeconds = 7,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify, System Shock, and Panic Charge restore 47 Health after a 7 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify, AbilityKeyword.SystemShock, AbilityKeyword.PanicCharge },
                            Data = new PgNumericValueEx() { Value = +47F },
                            DelayInSeconds = 7,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify, System Shock, and Panic Charge restore 50 Health after a 7 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify, AbilityKeyword.SystemShock, AbilityKeyword.PanicCharge },
                            Data = new PgNumericValueEx() { Value = +50F },
                            DelayInSeconds = 7,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify, System Shock, and Panic Charge restore 53 Health after a 7 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify, AbilityKeyword.SystemShock, AbilityKeyword.PanicCharge },
                            Data = new PgNumericValueEx() { Value = +53F },
                            DelayInSeconds = 7,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify, System Shock, and Panic Charge restore 56 Health after a 7 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify, AbilityKeyword.SystemShock, AbilityKeyword.PanicCharge },
                            Data = new PgNumericValueEx() { Value = +56F },
                            DelayInSeconds = 7,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify, System Shock, and Panic Charge restore 59 Health after a 7 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify, AbilityKeyword.SystemShock, AbilityKeyword.PanicCharge },
                            Data = new PgNumericValueEx() { Value = +59F },
                            DelayInSeconds = 7,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify, System Shock, and Panic Charge restore 62 Health after a 7 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify, AbilityKeyword.SystemShock, AbilityKeyword.PanicCharge },
                            Data = new PgNumericValueEx() { Value = +62F },
                            DelayInSeconds = 7,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify, System Shock, and Panic Charge restore 65 Health after a 7 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify, AbilityKeyword.SystemShock, AbilityKeyword.PanicCharge },
                            Data = new PgNumericValueEx() { Value = +65F },
                            DelayInSeconds = 7,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify, System Shock, and Panic Charge restore 68 Health after a 7 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify, AbilityKeyword.SystemShock, AbilityKeyword.PanicCharge },
                            Data = new PgNumericValueEx() { Value = +68F },
                            DelayInSeconds = 7,
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "9085", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Reconstruct restores 6 Power to the target",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Reconstruct },
                            Data = new PgNumericValueEx() { Value = +6F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Reconstruct restores 11 Power to the target",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Reconstruct },
                            Data = new PgNumericValueEx() { Value = +11F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Reconstruct restores 16 Power to the target",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Reconstruct },
                            Data = new PgNumericValueEx() { Value = +16F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Reconstruct restores 21 Power to the target",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Reconstruct },
                            Data = new PgNumericValueEx() { Value = +21F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Reconstruct restores 26 Power to the target",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Reconstruct },
                            Data = new PgNumericValueEx() { Value = +26F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Reconstruct restores 31 Power to the target",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Reconstruct },
                            Data = new PgNumericValueEx() { Value = +31F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Reconstruct restores 36 Power to the target",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Reconstruct },
                            Data = new PgNumericValueEx() { Value = +36F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Reconstruct restores 41 Power to the target",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Reconstruct },
                            Data = new PgNumericValueEx() { Value = +41F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Reconstruct restores 46 Power to the target",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Reconstruct },
                            Data = new PgNumericValueEx() { Value = +46F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Reconstruct restores 51 Power to the target",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Reconstruct },
                            Data = new PgNumericValueEx() { Value = +51F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Reconstruct restores 56 Power to the target",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Reconstruct },
                            Data = new PgNumericValueEx() { Value = +56F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Reconstruct restores 61 Power to the target",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Reconstruct },
                            Data = new PgNumericValueEx() { Value = +61F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Reconstruct restores 66 Power to the target",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Reconstruct },
                            Data = new PgNumericValueEx() { Value = +66F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Reconstruct restores 71 Power to the target",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Reconstruct },
                            Data = new PgNumericValueEx() { Value = +71F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Reconstruct restores 76 Power to the target",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Reconstruct },
                            Data = new PgNumericValueEx() { Value = +76F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Reconstruct restores 81 Power to the target",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Reconstruct },
                            Data = new PgNumericValueEx() { Value = +81F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Reconstruct restores 86 Power to the target",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Reconstruct },
                            Data = new PgNumericValueEx() { Value = +86F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Reconstruct restores 91 Power to the target",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Reconstruct },
                            Data = new PgNumericValueEx() { Value = +91F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Reconstruct restores 96 Power to the target",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Reconstruct },
                            Data = new PgNumericValueEx() { Value = +96F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Reconstruct restores 101 Power to the target",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Reconstruct },
                            Data = new PgNumericValueEx() { Value = +101F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Reconstruct restores 106 Power to the target",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Reconstruct },
                            Data = new PgNumericValueEx() { Value = +106F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Reconstruct restores 111 Power to the target",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Reconstruct },
                            Data = new PgNumericValueEx() { Value = +111F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Reconstruct restores 116 Power to the target",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Reconstruct },
                            Data = new PgNumericValueEx() { Value = +116F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Reconstruct restores 121 Power to the target",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Reconstruct },
                            Data = new PgNumericValueEx() { Value = +121F },
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Reconstruct restores 126 Power to the target",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestorePower,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Reconstruct },
                            Data = new PgNumericValueEx() { Value = +126F },
                        },
                    },
                },
            }
        },
        /*
         *
         *
         *
         *
         *
         *
         *
         *
         *
         *
         */
        { "9303", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Psi Health Wave and Psi Armor Wave instantly heal you for 10 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PsiHealthWave, AbilityKeyword.PsiArmorWave },
                            Data = new PgNumericValueEx() { Value = +10F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Psi Health Wave and Psi Armor Wave instantly heal you for 15 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PsiHealthWave, AbilityKeyword.PsiArmorWave },
                            Data = new PgNumericValueEx() { Value = +15F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Psi Health Wave and Psi Armor Wave instantly heal you for 20 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PsiHealthWave, AbilityKeyword.PsiArmorWave },
                            Data = new PgNumericValueEx() { Value = +20F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Psi Health Wave and Psi Armor Wave instantly heal you for 25 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PsiHealthWave, AbilityKeyword.PsiArmorWave },
                            Data = new PgNumericValueEx() { Value = +25F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Psi Health Wave and Psi Armor Wave instantly heal you for 30 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PsiHealthWave, AbilityKeyword.PsiArmorWave },
                            Data = new PgNumericValueEx() { Value = +30F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Psi Health Wave and Psi Armor Wave instantly heal you for 35 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PsiHealthWave, AbilityKeyword.PsiArmorWave },
                            Data = new PgNumericValueEx() { Value = +35F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Psi Health Wave and Psi Armor Wave instantly heal you for 41 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PsiHealthWave, AbilityKeyword.PsiArmorWave },
                            Data = new PgNumericValueEx() { Value = +41F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Psi Health Wave and Psi Armor Wave instantly heal you for 47 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PsiHealthWave, AbilityKeyword.PsiArmorWave },
                            Data = new PgNumericValueEx() { Value = +47F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Psi Health Wave and Psi Armor Wave instantly heal you for 53 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PsiHealthWave, AbilityKeyword.PsiArmorWave },
                            Data = new PgNumericValueEx() { Value = +53F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Psi Health Wave and Psi Armor Wave instantly heal you for 59 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PsiHealthWave, AbilityKeyword.PsiArmorWave },
                            Data = new PgNumericValueEx() { Value = +59F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Psi Health Wave and Psi Armor Wave instantly heal you for 65 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PsiHealthWave, AbilityKeyword.PsiArmorWave },
                            Data = new PgNumericValueEx() { Value = +65F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Psi Health Wave and Psi Armor Wave instantly heal you for 71 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PsiHealthWave, AbilityKeyword.PsiArmorWave },
                            Data = new PgNumericValueEx() { Value = +71F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
            }
        },
    };
}
