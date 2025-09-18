namespace TranslatorCombatParserEx;

using System.Collections.Generic;
using PgObjects;

internal partial class CombatParserEx
{
    private Dictionary<string, PgCombatModCollectionEx> KnownPowers = new()
    {
        { "10003", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Blitz Shot and Aimed Shot heal you for 5 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BlitzShot, AbilityKeyword.AimedShot },
                            Data = new PgNumericValueEx() { Value = +5F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Blitz Shot and Aimed Shot heal you for 8 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BlitzShot, AbilityKeyword.AimedShot },
                            Data = new PgNumericValueEx() { Value = +8F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Blitz Shot and Aimed Shot heal you for 11 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BlitzShot, AbilityKeyword.AimedShot },
                            Data = new PgNumericValueEx() { Value = +11F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Blitz Shot and Aimed Shot heal you for 14 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BlitzShot, AbilityKeyword.AimedShot },
                            Data = new PgNumericValueEx() { Value = +14F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Blitz Shot and Aimed Shot heal you for 18 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BlitzShot, AbilityKeyword.AimedShot },
                            Data = new PgNumericValueEx() { Value = +18F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Blitz Shot and Aimed Shot heal you for 21 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BlitzShot, AbilityKeyword.AimedShot },
                            Data = new PgNumericValueEx() { Value = +21F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Blitz Shot and Aimed Shot heal you for 24 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BlitzShot, AbilityKeyword.AimedShot },
                            Data = new PgNumericValueEx() { Value = +24F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Blitz Shot and Aimed Shot heal you for 28 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BlitzShot, AbilityKeyword.AimedShot },
                            Data = new PgNumericValueEx() { Value = +28F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Blitz Shot and Aimed Shot heal you for 32 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BlitzShot, AbilityKeyword.AimedShot },
                            Data = new PgNumericValueEx() { Value = +32F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Blitz Shot and Aimed Shot heal you for 36 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BlitzShot, AbilityKeyword.AimedShot },
                            Data = new PgNumericValueEx() { Value = +36F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Blitz Shot and Aimed Shot heal you for 40 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BlitzShot, AbilityKeyword.AimedShot },
                            Data = new PgNumericValueEx() { Value = +40F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Blitz Shot and Aimed Shot heal you for 44 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BlitzShot, AbilityKeyword.AimedShot },
                            Data = new PgNumericValueEx() { Value = +44F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
            }
        },
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
        { "10082", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Multishot restores 12 Health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MultiShot },
                            Data = new PgNumericValueEx() { Value = +12F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Multishot restores 24 Health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MultiShot },
                            Data = new PgNumericValueEx() { Value = +24F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Multishot restores 36 Health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MultiShot },
                            Data = new PgNumericValueEx() { Value = +36F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Multishot restores 48 Health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MultiShot },
                            Data = new PgNumericValueEx() { Value = +48F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Multishot restores 60 Health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MultiShot },
                            Data = new PgNumericValueEx() { Value = +60F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Multishot restores 72 Health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MultiShot },
                            Data = new PgNumericValueEx() { Value = +72F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Multishot restores 87 Health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MultiShot },
                            Data = new PgNumericValueEx() { Value = +87F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Multishot restores 102 Health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MultiShot },
                            Data = new PgNumericValueEx() { Value = +102F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Multishot restores 117 Health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MultiShot },
                            Data = new PgNumericValueEx() { Value = +117F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Multishot restores 132 Health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MultiShot },
                            Data = new PgNumericValueEx() { Value = +132F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Multishot restores 147 Health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MultiShot },
                            Data = new PgNumericValueEx() { Value = +147F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Multishot restores 162 Health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.MultiShot },
                            Data = new PgNumericValueEx() { Value = +162F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
            }
        },
        { "10124", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Long Shot restores 10 health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LongShot },
                            Data = new PgNumericValueEx() { Value = +10F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Long Shot restores 14 health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LongShot },
                            Data = new PgNumericValueEx() { Value = +14F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Long Shot restores 18 health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LongShot },
                            Data = new PgNumericValueEx() { Value = +18F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Long Shot restores 22 health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LongShot },
                            Data = new PgNumericValueEx() { Value = +22F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Long Shot restores 26 health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LongShot },
                            Data = new PgNumericValueEx() { Value = +26F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Long Shot restores 30 health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LongShot },
                            Data = new PgNumericValueEx() { Value = +30F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Long Shot restores 34 health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LongShot },
                            Data = new PgNumericValueEx() { Value = +34F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Long Shot restores 38 health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LongShot },
                            Data = new PgNumericValueEx() { Value = +38F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Long Shot restores 42 health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LongShot },
                            Data = new PgNumericValueEx() { Value = +42F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Long Shot restores 46 health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LongShot },
                            Data = new PgNumericValueEx() { Value = +46F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Long Shot restores 50 health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LongShot },
                            Data = new PgNumericValueEx() { Value = +50F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Long Shot restores 54 health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LongShot },
                            Data = new PgNumericValueEx() { Value = +54F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Long Shot restores 58 health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LongShot },
                            Data = new PgNumericValueEx() { Value = +58F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Long Shot restores 62 health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LongShot },
                            Data = new PgNumericValueEx() { Value = +62F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Long Shot restores 66 health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LongShot },
                            Data = new PgNumericValueEx() { Value = +66F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Long Shot restores 70 health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LongShot },
                            Data = new PgNumericValueEx() { Value = +70F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Long Shot restores 74 health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LongShot },
                            Data = new PgNumericValueEx() { Value = +74F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Long Shot restores 78 health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LongShot },
                            Data = new PgNumericValueEx() { Value = +78F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Long Shot restores 82 health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LongShot },
                            Data = new PgNumericValueEx() { Value = +82F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Long Shot restores 86 health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LongShot },
                            Data = new PgNumericValueEx() { Value = +86F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Long Shot restores 90 health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LongShot },
                            Data = new PgNumericValueEx() { Value = +90F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Long Shot restores 94 health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LongShot },
                            Data = new PgNumericValueEx() { Value = +94F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Long Shot restores 98 health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LongShot },
                            Data = new PgNumericValueEx() { Value = +98F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Long Shot restores 102 health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LongShot },
                            Data = new PgNumericValueEx() { Value = +102F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Long Shot restores 106 health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.LongShot },
                            Data = new PgNumericValueEx() { Value = +106F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
            }
        },
        { "10452", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Bow Bash heals you for 1 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BowBash },
                            Data = new PgNumericValueEx() { Value = +1F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bow Bash heals you for 2 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BowBash },
                            Data = new PgNumericValueEx() { Value = +2F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bow Bash heals you for 3 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BowBash },
                            Data = new PgNumericValueEx() { Value = +3F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bow Bash heals you for 4 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BowBash },
                            Data = new PgNumericValueEx() { Value = +4F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bow Bash heals you for 5 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BowBash },
                            Data = new PgNumericValueEx() { Value = +5F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bow Bash heals you for 6 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BowBash },
                            Data = new PgNumericValueEx() { Value = +6F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bow Bash heals you for 7 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BowBash },
                            Data = new PgNumericValueEx() { Value = +7F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bow Bash heals you for 8 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BowBash },
                            Data = new PgNumericValueEx() { Value = +8F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bow Bash heals you for 9 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BowBash },
                            Data = new PgNumericValueEx() { Value = +9F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bow Bash heals you for 10 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BowBash },
                            Data = new PgNumericValueEx() { Value = +10F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bow Bash heals you for 11 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BowBash },
                            Data = new PgNumericValueEx() { Value = +11F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bow Bash heals you for 12 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BowBash },
                            Data = new PgNumericValueEx() { Value = +12F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bow Bash heals you for 13 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BowBash },
                            Data = new PgNumericValueEx() { Value = +13F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bow Bash heals you for 14 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BowBash },
                            Data = new PgNumericValueEx() { Value = +14F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bow Bash heals you for 15 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BowBash },
                            Data = new PgNumericValueEx() { Value = +15F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bow Bash heals you for 16 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BowBash },
                            Data = new PgNumericValueEx() { Value = +16F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bow Bash heals you for 17 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BowBash },
                            Data = new PgNumericValueEx() { Value = +17F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bow Bash heals you for 18 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BowBash },
                            Data = new PgNumericValueEx() { Value = +18F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bow Bash heals you for 19 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BowBash },
                            Data = new PgNumericValueEx() { Value = +19F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bow Bash heals you for 20 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BowBash },
                            Data = new PgNumericValueEx() { Value = +20F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bow Bash heals you for 21 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BowBash },
                            Data = new PgNumericValueEx() { Value = +21F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bow Bash heals you for 22 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BowBash },
                            Data = new PgNumericValueEx() { Value = +22F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bow Bash heals you for 23 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BowBash },
                            Data = new PgNumericValueEx() { Value = +23F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bow Bash heals you for 24 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BowBash },
                            Data = new PgNumericValueEx() { Value = +24F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bow Bash heals you for 25 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BowBash },
                            Data = new PgNumericValueEx() { Value = +25F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
            }
        },
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
        { "11501", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Take the Lead heals you for 6 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.TakeTheLead },
                            Data = new PgNumericValueEx() { Value = +6F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Take the Lead heals you for 12 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.TakeTheLead },
                            Data = new PgNumericValueEx() { Value = +12F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Take the Lead heals you for 18 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.TakeTheLead },
                            Data = new PgNumericValueEx() { Value = +18F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Take the Lead heals you for 24 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.TakeTheLead },
                            Data = new PgNumericValueEx() { Value = +24F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Take the Lead heals you for 30 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.TakeTheLead },
                            Data = new PgNumericValueEx() { Value = +30F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Take the Lead heals you for 36 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.TakeTheLead },
                            Data = new PgNumericValueEx() { Value = +36F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Take the Lead heals you for 42 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.TakeTheLead },
                            Data = new PgNumericValueEx() { Value = +42F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Take the Lead heals you for 48 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.TakeTheLead },
                            Data = new PgNumericValueEx() { Value = +48F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Take the Lead heals you for 54 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.TakeTheLead },
                            Data = new PgNumericValueEx() { Value = +54F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Take the Lead heals you for 60 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.TakeTheLead },
                            Data = new PgNumericValueEx() { Value = +60F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Take the Lead heals you for 66 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.TakeTheLead },
                            Data = new PgNumericValueEx() { Value = +66F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Take the Lead heals you for 72 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.TakeTheLead },
                            Data = new PgNumericValueEx() { Value = +72F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Take the Lead heals you for 78 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.TakeTheLead },
                            Data = new PgNumericValueEx() { Value = +78F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Take the Lead heals you for 84 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.TakeTheLead },
                            Data = new PgNumericValueEx() { Value = +84F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Take the Lead heals you for 90 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.TakeTheLead },
                            Data = new PgNumericValueEx() { Value = +90F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Take the Lead heals you for 96 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.TakeTheLead },
                            Data = new PgNumericValueEx() { Value = +96F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Take the Lead heals you for 102 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.TakeTheLead },
                            Data = new PgNumericValueEx() { Value = +102F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Take the Lead heals you for 108 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.TakeTheLead },
                            Data = new PgNumericValueEx() { Value = +108F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Take the Lead heals you for 114 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.TakeTheLead },
                            Data = new PgNumericValueEx() { Value = +114F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Take the Lead heals you for 120 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.TakeTheLead },
                            Data = new PgNumericValueEx() { Value = +120F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Take the Lead heals you for 126 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.TakeTheLead },
                            Data = new PgNumericValueEx() { Value = +126F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Take the Lead heals you for 132 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.TakeTheLead },
                            Data = new PgNumericValueEx() { Value = +132F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Take the Lead heals you for 138 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.TakeTheLead },
                            Data = new PgNumericValueEx() { Value = +138F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Take the Lead heals you for 144 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.TakeTheLead },
                            Data = new PgNumericValueEx() { Value = +144F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Take the Lead heals you for 150 Health after a 15 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.TakeTheLead },
                            Data = new PgNumericValueEx() { Value = +150F },
                            DelayInSeconds = 15,
                            Target = CombatTarget.Self,
                        },
                    },
                },
            }
        },
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
        { "12104", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Using Unnatural Wrath on your pet heals you for 8 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.UnnaturalWrath },
                            Data = new PgNumericValueEx() { Value = +8F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Using Unnatural Wrath on your pet heals you for 10 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.UnnaturalWrath },
                            Data = new PgNumericValueEx() { Value = +10F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Using Unnatural Wrath on your pet heals you for 12 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.UnnaturalWrath },
                            Data = new PgNumericValueEx() { Value = +12F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Using Unnatural Wrath on your pet heals you for 14 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.UnnaturalWrath },
                            Data = new PgNumericValueEx() { Value = +14F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Using Unnatural Wrath on your pet heals you for 16 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.UnnaturalWrath },
                            Data = new PgNumericValueEx() { Value = +16F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Using Unnatural Wrath on your pet heals you for 18 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.UnnaturalWrath },
                            Data = new PgNumericValueEx() { Value = +18F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Using Unnatural Wrath on your pet heals you for 20 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.UnnaturalWrath },
                            Data = new PgNumericValueEx() { Value = +20F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Using Unnatural Wrath on your pet heals you for 22 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.UnnaturalWrath },
                            Data = new PgNumericValueEx() { Value = +22F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Using Unnatural Wrath on your pet heals you for 24 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.UnnaturalWrath },
                            Data = new PgNumericValueEx() { Value = +24F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Using Unnatural Wrath on your pet heals you for 26 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.UnnaturalWrath },
                            Data = new PgNumericValueEx() { Value = +26F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Using Unnatural Wrath on your pet heals you for 28 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.UnnaturalWrath },
                            Data = new PgNumericValueEx() { Value = +28F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Using Unnatural Wrath on your pet heals you for 30 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.UnnaturalWrath },
                            Data = new PgNumericValueEx() { Value = +30F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Using Unnatural Wrath on your pet heals you for 32 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.UnnaturalWrath },
                            Data = new PgNumericValueEx() { Value = +32F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Using Unnatural Wrath on your pet heals you for 34 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.UnnaturalWrath },
                            Data = new PgNumericValueEx() { Value = +34F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Using Unnatural Wrath on your pet heals you for 36 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.UnnaturalWrath },
                            Data = new PgNumericValueEx() { Value = +36F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Using Unnatural Wrath on your pet heals you for 38 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.UnnaturalWrath },
                            Data = new PgNumericValueEx() { Value = +38F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Using Unnatural Wrath on your pet heals you for 40 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.UnnaturalWrath },
                            Data = new PgNumericValueEx() { Value = +40F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Using Unnatural Wrath on your pet heals you for 42 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.UnnaturalWrath },
                            Data = new PgNumericValueEx() { Value = +42F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Using Unnatural Wrath on your pet heals you for 44 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.UnnaturalWrath },
                            Data = new PgNumericValueEx() { Value = +44F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Using Unnatural Wrath on your pet heals you for 46 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.UnnaturalWrath },
                            Data = new PgNumericValueEx() { Value = +46F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Using Unnatural Wrath on your pet heals you for 48 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.UnnaturalWrath },
                            Data = new PgNumericValueEx() { Value = +48F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Using Unnatural Wrath on your pet heals you for 50 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.UnnaturalWrath },
                            Data = new PgNumericValueEx() { Value = +50F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Using Unnatural Wrath on your pet heals you for 52 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.UnnaturalWrath },
                            Data = new PgNumericValueEx() { Value = +52F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Using Unnatural Wrath on your pet heals you for 54 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.UnnaturalWrath },
                            Data = new PgNumericValueEx() { Value = +54F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Using Unnatural Wrath on your pet heals you for 56 Health (or Armor if Health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.UnnaturalWrath },
                            Data = new PgNumericValueEx() { Value = +56F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
            }
        },
        { "1251", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Flashing Strike heals you for 3 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FlashingStrike },
                            Data = new PgNumericValueEx() { Value = +3F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Flashing Strike heals you for 6 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FlashingStrike },
                            Data = new PgNumericValueEx() { Value = +6F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Flashing Strike heals you for 9 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FlashingStrike },
                            Data = new PgNumericValueEx() { Value = +9F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Flashing Strike heals you for 12 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FlashingStrike },
                            Data = new PgNumericValueEx() { Value = +12F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Flashing Strike heals you for 15 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FlashingStrike },
                            Data = new PgNumericValueEx() { Value = +15F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Flashing Strike heals you for 18 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FlashingStrike },
                            Data = new PgNumericValueEx() { Value = +18F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Flashing Strike heals you for 21 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FlashingStrike },
                            Data = new PgNumericValueEx() { Value = +21F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Flashing Strike heals you for 24 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FlashingStrike },
                            Data = new PgNumericValueEx() { Value = +24F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Flashing Strike heals you for 27 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FlashingStrike },
                            Data = new PgNumericValueEx() { Value = +27F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Flashing Strike heals you for 30 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FlashingStrike },
                            Data = new PgNumericValueEx() { Value = +30F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Flashing Strike heals you for 33 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FlashingStrike },
                            Data = new PgNumericValueEx() { Value = +33F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Flashing Strike heals you for 36 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FlashingStrike },
                            Data = new PgNumericValueEx() { Value = +36F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Flashing Strike heals you for 40 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FlashingStrike },
                            Data = new PgNumericValueEx() { Value = +40F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Flashing Strike heals you for 45 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FlashingStrike },
                            Data = new PgNumericValueEx() { Value = +45F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Flashing Strike heals you for 50 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FlashingStrike },
                            Data = new PgNumericValueEx() { Value = +50F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Flashing Strike heals you for 55 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FlashingStrike },
                            Data = new PgNumericValueEx() { Value = +55F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Flashing Strike heals you for 60 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FlashingStrike },
                            Data = new PgNumericValueEx() { Value = +60F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Flashing Strike heals you for 65 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FlashingStrike },
                            Data = new PgNumericValueEx() { Value = +65F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Flashing Strike heals you for 70 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FlashingStrike },
                            Data = new PgNumericValueEx() { Value = +70F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Flashing Strike heals you for 75 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FlashingStrike },
                            Data = new PgNumericValueEx() { Value = +75F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Flashing Strike heals you for 80 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FlashingStrike },
                            Data = new PgNumericValueEx() { Value = +80F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Flashing Strike heals you for 85 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FlashingStrike },
                            Data = new PgNumericValueEx() { Value = +85F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Flashing Strike heals you for 90 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FlashingStrike },
                            Data = new PgNumericValueEx() { Value = +90F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Flashing Strike heals you for 95 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FlashingStrike },
                            Data = new PgNumericValueEx() { Value = +95F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Flashing Strike heals you for 100 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FlashingStrike },
                            Data = new PgNumericValueEx() { Value = +100F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
            }
        },
        { "1301", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Heart Piercer heals you for 6 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +6F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heart Piercer heals you for 8 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +8F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heart Piercer heals you for 10 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +10F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heart Piercer heals you for 12 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +12F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heart Piercer heals you for 14 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +14F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heart Piercer heals you for 16 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +16F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heart Piercer heals you for 18 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +18F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heart Piercer heals you for 20 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +20F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heart Piercer heals you for 22 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +22F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heart Piercer heals you for 24 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +24F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heart Piercer heals you for 26 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +26F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heart Piercer heals you for 28 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +28F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heart Piercer heals you for 31 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +31F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heart Piercer heals you for 34 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +34F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heart Piercer heals you for 37 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +37F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heart Piercer heals you for 40 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +40F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heart Piercer heals you for 43 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +43F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heart Piercer heals you for 46 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +46F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heart Piercer heals you for 49 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +49F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heart Piercer heals you for 52 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +52F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heart Piercer heals you for 55 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +55F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heart Piercer heals you for 58 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +58F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heart Piercer heals you for 61 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +61F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heart Piercer heals you for 64 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +64F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heart Piercer heals you for 67 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +67F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
            }
        },
        { "1351", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Precision Pierce and Heart Piercer restore 3 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PrecisionPierce, AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +3F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Precision Pierce and Heart Piercer restore 4 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PrecisionPierce, AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +4F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Precision Pierce and Heart Piercer restore 5 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PrecisionPierce, AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +5F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Precision Pierce and Heart Piercer restore 6 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PrecisionPierce, AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +6F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Precision Pierce and Heart Piercer restore 7 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PrecisionPierce, AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +7F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Precision Pierce and Heart Piercer restore 8 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PrecisionPierce, AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +8F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Precision Pierce and Heart Piercer restore 9 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PrecisionPierce, AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +9F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Precision Pierce and Heart Piercer restore 10 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PrecisionPierce, AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +10F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Precision Pierce and Heart Piercer restore 11 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PrecisionPierce, AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +11F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Precision Pierce and Heart Piercer restore 12 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PrecisionPierce, AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +12F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Precision Pierce and Heart Piercer restore 13 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PrecisionPierce, AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +13F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Precision Pierce and Heart Piercer restore 14 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PrecisionPierce, AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +14F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Precision Pierce and Heart Piercer restore 15 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PrecisionPierce, AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +15F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Precision Pierce and Heart Piercer restore 16 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PrecisionPierce, AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +16F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Precision Pierce and Heart Piercer restore 17 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PrecisionPierce, AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +17F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Precision Pierce and Heart Piercer restore 18 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PrecisionPierce, AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +18F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Precision Pierce and Heart Piercer restore 19 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PrecisionPierce, AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +19F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Precision Pierce and Heart Piercer restore 20 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PrecisionPierce, AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +20F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Precision Pierce and Heart Piercer restore 21 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PrecisionPierce, AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +21F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Precision Pierce and Heart Piercer restore 22 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PrecisionPierce, AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +22F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Precision Pierce and Heart Piercer restore 23 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PrecisionPierce, AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +23F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Precision Pierce and Heart Piercer restore 24 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PrecisionPierce, AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +24F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Precision Pierce and Heart Piercer restore 25 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PrecisionPierce, AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +25F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Precision Pierce and Heart Piercer restore 26 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PrecisionPierce, AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +26F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Precision Pierce and Heart Piercer restore 27 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.PrecisionPierce, AbilityKeyword.HeartPiercer },
                            Data = new PgNumericValueEx() { Value = +27F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
            }
        },
        { "15014", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Ice Spear heals you for 4 health after a 7-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceSpear },
                            Data = new PgNumericValueEx() { Value = +4F },
                            DelayInSeconds = 7,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Spear heals you for 8 health after a 7-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceSpear },
                            Data = new PgNumericValueEx() { Value = +8F },
                            DelayInSeconds = 7,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Spear heals you for 12 health after a 7-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceSpear },
                            Data = new PgNumericValueEx() { Value = +12F },
                            DelayInSeconds = 7,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Spear heals you for 16 health after a 7-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceSpear },
                            Data = new PgNumericValueEx() { Value = +16F },
                            DelayInSeconds = 7,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Spear heals you for 20 health after a 7-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceSpear },
                            Data = new PgNumericValueEx() { Value = +20F },
                            DelayInSeconds = 7,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Spear heals you for 24 health after a 7-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceSpear },
                            Data = new PgNumericValueEx() { Value = +24F },
                            DelayInSeconds = 7,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Spear heals you for 28 health after a 7-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceSpear },
                            Data = new PgNumericValueEx() { Value = +28F },
                            DelayInSeconds = 7,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Spear heals you for 32 health after a 7-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceSpear },
                            Data = new PgNumericValueEx() { Value = +32F },
                            DelayInSeconds = 7,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Spear heals you for 36 health after a 7-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceSpear },
                            Data = new PgNumericValueEx() { Value = +36F },
                            DelayInSeconds = 7,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Spear heals you for 40 health after a 7-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceSpear },
                            Data = new PgNumericValueEx() { Value = +40F },
                            DelayInSeconds = 7,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Spear heals you for 44 health after a 7-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceSpear },
                            Data = new PgNumericValueEx() { Value = +44F },
                            DelayInSeconds = 7,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Spear heals you for 48 health after a 7-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceSpear },
                            Data = new PgNumericValueEx() { Value = +48F },
                            DelayInSeconds = 7,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Spear heals you for 52 health after a 7-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceSpear },
                            Data = new PgNumericValueEx() { Value = +52F },
                            DelayInSeconds = 7,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Spear heals you for 56 health after a 7-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceSpear },
                            Data = new PgNumericValueEx() { Value = +56F },
                            DelayInSeconds = 7,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Spear heals you for 60 health after a 7-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceSpear },
                            Data = new PgNumericValueEx() { Value = +60F },
                            DelayInSeconds = 7,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Spear heals you for 64 health after a 7-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceSpear },
                            Data = new PgNumericValueEx() { Value = +64F },
                            DelayInSeconds = 7,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Spear heals you for 68 health after a 7-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceSpear },
                            Data = new PgNumericValueEx() { Value = +68F },
                            DelayInSeconds = 7,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Spear heals you for 72 health after a 7-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceSpear },
                            Data = new PgNumericValueEx() { Value = +72F },
                            DelayInSeconds = 7,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Spear heals you for 76 health after a 7-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceSpear },
                            Data = new PgNumericValueEx() { Value = +76F },
                            DelayInSeconds = 7,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Spear heals you for 80 health after a 7-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceSpear },
                            Data = new PgNumericValueEx() { Value = +80F },
                            DelayInSeconds = 7,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Spear heals you for 84 health after a 7-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceSpear },
                            Data = new PgNumericValueEx() { Value = +84F },
                            DelayInSeconds = 7,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Spear heals you for 88 health after a 7-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceSpear },
                            Data = new PgNumericValueEx() { Value = +88F },
                            DelayInSeconds = 7,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Spear heals you for 92 health after a 7-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceSpear },
                            Data = new PgNumericValueEx() { Value = +92F },
                            DelayInSeconds = 7,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Spear heals you for 96 health after a 7-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceSpear },
                            Data = new PgNumericValueEx() { Value = +96F },
                            DelayInSeconds = 7,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Ice Spear heals you for 100 health after a 7-second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceSpear },
                            Data = new PgNumericValueEx() { Value = +100F },
                            DelayInSeconds = 7,
                            Target = CombatTarget.Self,
                        },
                    },
                },
            }
        },
        { "15052", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "You regain 8 Health when using Ice Nova or Shardblast",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceNova, AbilityKeyword.Shardblast },
                            Data = new PgNumericValueEx() { Value = +8F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 10 Health when using Ice Nova or Shardblast",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceNova, AbilityKeyword.Shardblast },
                            Data = new PgNumericValueEx() { Value = +10F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 12 Health when using Ice Nova or Shardblast",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceNova, AbilityKeyword.Shardblast },
                            Data = new PgNumericValueEx() { Value = +12F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 14 Health when using Ice Nova or Shardblast",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceNova, AbilityKeyword.Shardblast },
                            Data = new PgNumericValueEx() { Value = +14F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 16 Health when using Ice Nova or Shardblast",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceNova, AbilityKeyword.Shardblast },
                            Data = new PgNumericValueEx() { Value = +16F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 18 Health when using Ice Nova or Shardblast",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceNova, AbilityKeyword.Shardblast },
                            Data = new PgNumericValueEx() { Value = +18F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 20 Health when using Ice Nova or Shardblast",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceNova, AbilityKeyword.Shardblast },
                            Data = new PgNumericValueEx() { Value = +20F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 22 Health when using Ice Nova or Shardblast",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceNova, AbilityKeyword.Shardblast },
                            Data = new PgNumericValueEx() { Value = +22F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 24 Health when using Ice Nova or Shardblast",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceNova, AbilityKeyword.Shardblast },
                            Data = new PgNumericValueEx() { Value = +24F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 26 Health when using Ice Nova or Shardblast",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceNova, AbilityKeyword.Shardblast },
                            Data = new PgNumericValueEx() { Value = +26F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 28 Health when using Ice Nova or Shardblast",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceNova, AbilityKeyword.Shardblast },
                            Data = new PgNumericValueEx() { Value = +28F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 30 Health when using Ice Nova or Shardblast",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceNova, AbilityKeyword.Shardblast },
                            Data = new PgNumericValueEx() { Value = +30F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 32 Health when using Ice Nova or Shardblast",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceNova, AbilityKeyword.Shardblast },
                            Data = new PgNumericValueEx() { Value = +32F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 34 Health when using Ice Nova or Shardblast",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceNova, AbilityKeyword.Shardblast },
                            Data = new PgNumericValueEx() { Value = +34F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 37 Health when using Ice Nova or Shardblast",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceNova, AbilityKeyword.Shardblast },
                            Data = new PgNumericValueEx() { Value = +37F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 40 Health when using Ice Nova or Shardblast",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceNova, AbilityKeyword.Shardblast },
                            Data = new PgNumericValueEx() { Value = +40F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 42 Health when using Ice Nova or Shardblast",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceNova, AbilityKeyword.Shardblast },
                            Data = new PgNumericValueEx() { Value = +42F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 45 Health when using Ice Nova or Shardblast",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceNova, AbilityKeyword.Shardblast },
                            Data = new PgNumericValueEx() { Value = +45F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 48 Health when using Ice Nova or Shardblast",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceNova, AbilityKeyword.Shardblast },
                            Data = new PgNumericValueEx() { Value = +48F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 51 Health when using Ice Nova or Shardblast",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceNova, AbilityKeyword.Shardblast },
                            Data = new PgNumericValueEx() { Value = +51F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 54 Health when using Ice Nova or Shardblast",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceNova, AbilityKeyword.Shardblast },
                            Data = new PgNumericValueEx() { Value = +54F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 57 Health when using Ice Nova or Shardblast",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceNova, AbilityKeyword.Shardblast },
                            Data = new PgNumericValueEx() { Value = +57F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 60 Health when using Ice Nova or Shardblast",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceNova, AbilityKeyword.Shardblast },
                            Data = new PgNumericValueEx() { Value = +60F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 63 Health when using Ice Nova or Shardblast",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceNova, AbilityKeyword.Shardblast },
                            Data = new PgNumericValueEx() { Value = +63F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 66 Health when using Ice Nova or Shardblast",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.IceNova, AbilityKeyword.Shardblast },
                            Data = new PgNumericValueEx() { Value = +66F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
            }
        },
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
        { "16011", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Opening Thrust heals you for 2 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.OpeningThrust },
                            Data = new PgNumericValueEx() { Value = +2F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Opening Thrust heals you for 4 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.OpeningThrust },
                            Data = new PgNumericValueEx() { Value = +4F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Opening Thrust heals you for 6 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.OpeningThrust },
                            Data = new PgNumericValueEx() { Value = +6F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Opening Thrust heals you for 8 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.OpeningThrust },
                            Data = new PgNumericValueEx() { Value = +8F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Opening Thrust heals you for 10 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.OpeningThrust },
                            Data = new PgNumericValueEx() { Value = +10F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Opening Thrust heals you for 12 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.OpeningThrust },
                            Data = new PgNumericValueEx() { Value = +12F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Opening Thrust heals you for 13 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.OpeningThrust },
                            Data = new PgNumericValueEx() { Value = +13F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Opening Thrust heals you for 14 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.OpeningThrust },
                            Data = new PgNumericValueEx() { Value = +14F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Opening Thrust heals you for 16 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.OpeningThrust },
                            Data = new PgNumericValueEx() { Value = +16F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Opening Thrust heals you for 18 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.OpeningThrust },
                            Data = new PgNumericValueEx() { Value = +18F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Opening Thrust heals you for 20 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.OpeningThrust },
                            Data = new PgNumericValueEx() { Value = +20F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Opening Thrust heals you for 22 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.OpeningThrust },
                            Data = new PgNumericValueEx() { Value = +22F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
            }
        },
        { "2052", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "You regain 3 Health when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +3F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 5 Health when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +5F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 7 Health when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +7F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 8 Health when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +8F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 9 Health when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +9F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 10 Health when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +10F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 12 Health when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +12F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 14 Health when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +14F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 16 Health when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +16F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 19 Health when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +19F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 22 Health when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +22F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 24 Health when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +24F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 25 Health when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +25F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 27 Health when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +27F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 29 Health when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +29F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 31 Health when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +31F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 33 Health when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +33F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 35 Health when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +35F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 37 Health when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +37F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 39 Health when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +39F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 41 Health when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +41F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 43 Health when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +43F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 45 Health when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +45F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 47 Health when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +47F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "You regain 49 Health when using Ring of Fire, Defensive Burst, or Defensive Chill",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.RingOfFire, AbilityKeyword.DefensiveBurst, AbilityKeyword.DefensiveChill },
                            Data = new PgNumericValueEx() { Value = +49F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
            }
        },
        { "21062", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Cuteness Overload heals you for 8 health and increases your movement speed by +1 for 8 seconds",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cuteness Overload heals you for 16 health and increases your movement speed by +2 for 8 seconds",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CutenessOverload },
                            Data = new PgNumericValueEx() { Value = +16F },
                            Target = CombatTarget.Self,
                        },
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.AddSprintSpeed,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CutenessOverload },
                            Data = new PgNumericValueEx() { Value = +2F },
                            DurationInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cuteness Overload heals you for 24 health and increases your movement speed by +3 for 8 seconds",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CutenessOverload },
                            Data = new PgNumericValueEx() { Value = +24F },
                            Target = CombatTarget.Self,
                        },
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.AddSprintSpeed,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CutenessOverload },
                            Data = new PgNumericValueEx() { Value = +3F },
                            DurationInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cuteness Overload heals you for 32 health and increases your movement speed by +4 for 8 seconds",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CutenessOverload },
                            Data = new PgNumericValueEx() { Value = +32F },
                            Target = CombatTarget.Self,
                        },
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.AddSprintSpeed,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CutenessOverload },
                            Data = new PgNumericValueEx() { Value = +4F },
                            DurationInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cuteness Overload heals you for 40 health and increases your movement speed by +5 for 8 seconds",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CutenessOverload },
                            Data = new PgNumericValueEx() { Value = +40F },
                            Target = CombatTarget.Self,
                        },
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.AddSprintSpeed,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CutenessOverload },
                            Data = new PgNumericValueEx() { Value = +5F },
                            DurationInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cuteness Overload heals you for 48 health and increases your movement speed by +6 for 8 seconds",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CutenessOverload },
                            Data = new PgNumericValueEx() { Value = +48F },
                            Target = CombatTarget.Self,
                        },
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.AddSprintSpeed,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CutenessOverload },
                            Data = new PgNumericValueEx() { Value = +6F },
                            DurationInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cuteness Overload heals you for 60 health and increases your movement speed by +6 for 8 seconds",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CutenessOverload },
                            Data = new PgNumericValueEx() { Value = +60F },
                            Target = CombatTarget.Self,
                        },
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.AddSprintSpeed,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CutenessOverload },
                            Data = new PgNumericValueEx() { Value = +6F },
                            DurationInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cuteness Overload heals you for 72 health and increases your movement speed by +6 for 8 seconds",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CutenessOverload },
                            Data = new PgNumericValueEx() { Value = +72F },
                            Target = CombatTarget.Self,
                        },
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.AddSprintSpeed,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CutenessOverload },
                            Data = new PgNumericValueEx() { Value = +6F },
                            DurationInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cuteness Overload heals you for 84 health and increases your movement speed by +6 for 8 seconds",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CutenessOverload },
                            Data = new PgNumericValueEx() { Value = +84F },
                            Target = CombatTarget.Self,
                        },
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.AddSprintSpeed,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CutenessOverload },
                            Data = new PgNumericValueEx() { Value = +6F },
                            DurationInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Cuteness Overload heals you for 96 health and increases your movement speed by +6 for 8 seconds",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CutenessOverload },
                            Data = new PgNumericValueEx() { Value = +96F },
                            Target = CombatTarget.Self,
                        },
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.AddSprintSpeed,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.CutenessOverload },
                            Data = new PgNumericValueEx() { Value = +6F },
                            DurationInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
            }
        },
        { "21251", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Bounding Escape heals you for 5 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BoundingEscape },
                            Data = new PgNumericValueEx() { Value = +5F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bounding Escape heals you for 10 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BoundingEscape },
                            Data = new PgNumericValueEx() { Value = +10F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bounding Escape heals you for 15 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BoundingEscape },
                            Data = new PgNumericValueEx() { Value = +15F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bounding Escape heals you for 20 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BoundingEscape },
                            Data = new PgNumericValueEx() { Value = +20F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bounding Escape heals you for 25 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BoundingEscape },
                            Data = new PgNumericValueEx() { Value = +25F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bounding Escape heals you for 30 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BoundingEscape },
                            Data = new PgNumericValueEx() { Value = +30F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bounding Escape heals you for 35 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BoundingEscape },
                            Data = new PgNumericValueEx() { Value = +35F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bounding Escape heals you for 40 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BoundingEscape },
                            Data = new PgNumericValueEx() { Value = +40F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bounding Escape heals you for 45 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BoundingEscape },
                            Data = new PgNumericValueEx() { Value = +45F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bounding Escape heals you for 50 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BoundingEscape },
                            Data = new PgNumericValueEx() { Value = +50F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bounding Escape heals you for 55 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BoundingEscape },
                            Data = new PgNumericValueEx() { Value = +55F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bounding Escape heals you for 60 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BoundingEscape },
                            Data = new PgNumericValueEx() { Value = +60F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bounding Escape heals you for 63 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BoundingEscape },
                            Data = new PgNumericValueEx() { Value = +63F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bounding Escape heals you for 66 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BoundingEscape },
                            Data = new PgNumericValueEx() { Value = +66F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bounding Escape heals you for 69 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BoundingEscape },
                            Data = new PgNumericValueEx() { Value = +69F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bounding Escape heals you for 72 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BoundingEscape },
                            Data = new PgNumericValueEx() { Value = +72F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bounding Escape heals you for 75 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BoundingEscape },
                            Data = new PgNumericValueEx() { Value = +75F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bounding Escape heals you for 78 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BoundingEscape },
                            Data = new PgNumericValueEx() { Value = +78F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bounding Escape heals you for 81 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BoundingEscape },
                            Data = new PgNumericValueEx() { Value = +81F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bounding Escape heals you for 84 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.BoundingEscape },
                            Data = new PgNumericValueEx() { Value = +84F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
            }
        },
        { "21303", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Antler Slash heals you for 1 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.AntlerSlash },
                            Data = new PgNumericValueEx() { Value = +1F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Antler Slash heals you for 2 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.AntlerSlash },
                            Data = new PgNumericValueEx() { Value = +2F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Antler Slash heals you for 3 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.AntlerSlash },
                            Data = new PgNumericValueEx() { Value = +3F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Antler Slash heals you for 4 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.AntlerSlash },
                            Data = new PgNumericValueEx() { Value = +4F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Antler Slash heals you for 5 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.AntlerSlash },
                            Data = new PgNumericValueEx() { Value = +5F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Antler Slash heals you for 6 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.AntlerSlash },
                            Data = new PgNumericValueEx() { Value = +6F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Antler Slash heals you for 7 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.AntlerSlash },
                            Data = new PgNumericValueEx() { Value = +7F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Antler Slash heals you for 8 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.AntlerSlash },
                            Data = new PgNumericValueEx() { Value = +8F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Antler Slash heals you for 9 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.AntlerSlash },
                            Data = new PgNumericValueEx() { Value = +9F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Antler Slash heals you for 10 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.AntlerSlash },
                            Data = new PgNumericValueEx() { Value = +10F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Antler Slash heals you for 11 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.AntlerSlash },
                            Data = new PgNumericValueEx() { Value = +11F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Antler Slash heals you for 12 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.AntlerSlash },
                            Data = new PgNumericValueEx() { Value = +12F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Antler Slash heals you for 13 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.AntlerSlash },
                            Data = new PgNumericValueEx() { Value = +13F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Antler Slash heals you for 14 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.AntlerSlash },
                            Data = new PgNumericValueEx() { Value = +14F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Antler Slash heals you for 15 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.AntlerSlash },
                            Data = new PgNumericValueEx() { Value = +15F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Antler Slash heals you for 16 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.AntlerSlash },
                            Data = new PgNumericValueEx() { Value = +16F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Antler Slash heals you for 17 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.AntlerSlash },
                            Data = new PgNumericValueEx() { Value = +17F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Antler Slash heals you for 18 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.AntlerSlash },
                            Data = new PgNumericValueEx() { Value = +18F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Antler Slash heals you for 19 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.AntlerSlash },
                            Data = new PgNumericValueEx() { Value = +19F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Antler Slash heals you for 20 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.AntlerSlash },
                            Data = new PgNumericValueEx() { Value = +20F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
            }
        },
        { "22301", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Harmlessness heals you for 10 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Harmlessness },
                            Data = new PgNumericValueEx() { Value = +10F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Harmlessness heals you for 15 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Harmlessness },
                            Data = new PgNumericValueEx() { Value = +15F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Harmlessness heals you for 20 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Harmlessness },
                            Data = new PgNumericValueEx() { Value = +20F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Harmlessness heals you for 25 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Harmlessness },
                            Data = new PgNumericValueEx() { Value = +25F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Harmlessness heals you for 30 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Harmlessness },
                            Data = new PgNumericValueEx() { Value = +30F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Harmlessness heals you for 35 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Harmlessness },
                            Data = new PgNumericValueEx() { Value = +35F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Harmlessness heals you for 40 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Harmlessness },
                            Data = new PgNumericValueEx() { Value = +40F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Harmlessness heals you for 45 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Harmlessness },
                            Data = new PgNumericValueEx() { Value = +45F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Harmlessness heals you for 50 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Harmlessness },
                            Data = new PgNumericValueEx() { Value = +50F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Harmlessness heals you for 55 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Harmlessness },
                            Data = new PgNumericValueEx() { Value = +55F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Harmlessness heals you for 60 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Harmlessness },
                            Data = new PgNumericValueEx() { Value = +60F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Harmlessness heals you for 65 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Harmlessness },
                            Data = new PgNumericValueEx() { Value = +65F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Harmlessness heals you for 69 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Harmlessness },
                            Data = new PgNumericValueEx() { Value = +69F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Harmlessness heals you for 73 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Harmlessness },
                            Data = new PgNumericValueEx() { Value = +73F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Harmlessness heals you for 77 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Harmlessness },
                            Data = new PgNumericValueEx() { Value = +77F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Harmlessness heals you for 81 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Harmlessness },
                            Data = new PgNumericValueEx() { Value = +81F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Harmlessness heals you for 85 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Harmlessness },
                            Data = new PgNumericValueEx() { Value = +85F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Harmlessness heals you for 89 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Harmlessness },
                            Data = new PgNumericValueEx() { Value = +89F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Harmlessness heals you for 93 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Harmlessness },
                            Data = new PgNumericValueEx() { Value = +93F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Harmlessness heals you for 97 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Harmlessness },
                            Data = new PgNumericValueEx() { Value = +97F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
            }
        },
        { "23023", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Inject Venom heals you for 2 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.InjectVenom },
                            Data = new PgNumericValueEx() { Value = +2F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Inject Venom heals you for 4 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.InjectVenom },
                            Data = new PgNumericValueEx() { Value = +4F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Inject Venom heals you for 6 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.InjectVenom },
                            Data = new PgNumericValueEx() { Value = +6F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Inject Venom heals you for 8 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.InjectVenom },
                            Data = new PgNumericValueEx() { Value = +8F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Inject Venom heals you for 10 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.InjectVenom },
                            Data = new PgNumericValueEx() { Value = +10F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Inject Venom heals you for 12 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.InjectVenom },
                            Data = new PgNumericValueEx() { Value = +12F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Inject Venom heals you for 14 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.InjectVenom },
                            Data = new PgNumericValueEx() { Value = +14F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Inject Venom heals you for 16 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.InjectVenom },
                            Data = new PgNumericValueEx() { Value = +16F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Inject Venom heals you for 19 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.InjectVenom },
                            Data = new PgNumericValueEx() { Value = +19F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Inject Venom heals you for 23 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.InjectVenom },
                            Data = new PgNumericValueEx() { Value = +23F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Inject Venom heals you for 25 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.InjectVenom },
                            Data = new PgNumericValueEx() { Value = +25F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Inject Venom heals you for 28 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.InjectVenom },
                            Data = new PgNumericValueEx() { Value = +28F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Inject Venom heals you for 31 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.InjectVenom },
                            Data = new PgNumericValueEx() { Value = +31F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Inject Venom heals you for 34 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.InjectVenom },
                            Data = new PgNumericValueEx() { Value = +34F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Inject Venom heals you for 37 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.InjectVenom },
                            Data = new PgNumericValueEx() { Value = +37F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Inject Venom heals you for 40 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.InjectVenom },
                            Data = new PgNumericValueEx() { Value = +40F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Inject Venom heals you for 43 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.InjectVenom },
                            Data = new PgNumericValueEx() { Value = +43F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Inject Venom heals you for 46 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.InjectVenom },
                            Data = new PgNumericValueEx() { Value = +46F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Inject Venom heals you for 49 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.InjectVenom },
                            Data = new PgNumericValueEx() { Value = +49F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Inject Venom heals you for 52 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.InjectVenom },
                            Data = new PgNumericValueEx() { Value = +52F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
            }
        },
        { "24241", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Confusing Double heals you for 18 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ConfusingDouble },
                            Data = new PgNumericValueEx() { Value = +18F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Confusing Double heals you for 25 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ConfusingDouble },
                            Data = new PgNumericValueEx() { Value = +25F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Confusing Double heals you for 32 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ConfusingDouble },
                            Data = new PgNumericValueEx() { Value = +32F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Confusing Double heals you for 39 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ConfusingDouble },
                            Data = new PgNumericValueEx() { Value = +39F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Confusing Double heals you for 46 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ConfusingDouble },
                            Data = new PgNumericValueEx() { Value = +46F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Confusing Double heals you for 53 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ConfusingDouble },
                            Data = new PgNumericValueEx() { Value = +53F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Confusing Double heals you for 60 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ConfusingDouble },
                            Data = new PgNumericValueEx() { Value = +60F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Confusing Double heals you for 67 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ConfusingDouble },
                            Data = new PgNumericValueEx() { Value = +67F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Confusing Double heals you for 74 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ConfusingDouble },
                            Data = new PgNumericValueEx() { Value = +74F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Confusing Double heals you for 81 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ConfusingDouble },
                            Data = new PgNumericValueEx() { Value = +81F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Confusing Double heals you for 88 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ConfusingDouble },
                            Data = new PgNumericValueEx() { Value = +88F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Confusing Double heals you for 95 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ConfusingDouble },
                            Data = new PgNumericValueEx() { Value = +95F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Confusing Double heals you for 102 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ConfusingDouble },
                            Data = new PgNumericValueEx() { Value = +102F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Confusing Double heals you for 109 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ConfusingDouble },
                            Data = new PgNumericValueEx() { Value = +109F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Confusing Double heals you for 116 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ConfusingDouble },
                            Data = new PgNumericValueEx() { Value = +116F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Confusing Double heals you for 123 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ConfusingDouble },
                            Data = new PgNumericValueEx() { Value = +123F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Confusing Double heals you for 130 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ConfusingDouble },
                            Data = new PgNumericValueEx() { Value = +130F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Confusing Double heals you for 137 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ConfusingDouble },
                            Data = new PgNumericValueEx() { Value = +137F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Confusing Double heals you for 144 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ConfusingDouble },
                            Data = new PgNumericValueEx() { Value = +144F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Confusing Double heals you for 151 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.ConfusingDouble },
                            Data = new PgNumericValueEx() { Value = +151F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
            }
        },
        { "3003", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Punch, Jab, and Infuriating Fist restore 3 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Punch, AbilityKeyword.Jab, AbilityKeyword.InfuriatingFist },
                            Data = new PgNumericValueEx() { Value = +3F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Punch, Jab, and Infuriating Fist restore 5 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Punch, AbilityKeyword.Jab, AbilityKeyword.InfuriatingFist },
                            Data = new PgNumericValueEx() { Value = +5F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Punch, Jab, and Infuriating Fist restore 7 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Punch, AbilityKeyword.Jab, AbilityKeyword.InfuriatingFist },
                            Data = new PgNumericValueEx() { Value = +7F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Punch, Jab, and Infuriating Fist restore 9 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Punch, AbilityKeyword.Jab, AbilityKeyword.InfuriatingFist },
                            Data = new PgNumericValueEx() { Value = +9F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Punch, Jab, and Infuriating Fist restore 12 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Punch, AbilityKeyword.Jab, AbilityKeyword.InfuriatingFist },
                            Data = new PgNumericValueEx() { Value = +12F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Punch, Jab, and Infuriating Fist restore 14 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Punch, AbilityKeyword.Jab, AbilityKeyword.InfuriatingFist },
                            Data = new PgNumericValueEx() { Value = +14F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Punch, Jab, and Infuriating Fist restore 16 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Punch, AbilityKeyword.Jab, AbilityKeyword.InfuriatingFist },
                            Data = new PgNumericValueEx() { Value = +16F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Punch, Jab, and Infuriating Fist restore 18 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Punch, AbilityKeyword.Jab, AbilityKeyword.InfuriatingFist },
                            Data = new PgNumericValueEx() { Value = +18F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Punch, Jab, and Infuriating Fist restore 20 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Punch, AbilityKeyword.Jab, AbilityKeyword.InfuriatingFist },
                            Data = new PgNumericValueEx() { Value = +20F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Punch, Jab, and Infuriating Fist restore 22 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Punch, AbilityKeyword.Jab, AbilityKeyword.InfuriatingFist },
                            Data = new PgNumericValueEx() { Value = +22F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Punch, Jab, and Infuriating Fist restore 24 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Punch, AbilityKeyword.Jab, AbilityKeyword.InfuriatingFist },
                            Data = new PgNumericValueEx() { Value = +24F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Punch, Jab, and Infuriating Fist restore 26 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Punch, AbilityKeyword.Jab, AbilityKeyword.InfuriatingFist },
                            Data = new PgNumericValueEx() { Value = +26F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
            }
        },
        { "3135", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Bodyslam heals you for 10 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bodyslam },
                            Data = new PgNumericValueEx() { Value = +10F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bodyslam heals you for 18 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bodyslam },
                            Data = new PgNumericValueEx() { Value = +18F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bodyslam heals you for 26 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bodyslam },
                            Data = new PgNumericValueEx() { Value = +26F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bodyslam heals you for 34 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bodyslam },
                            Data = new PgNumericValueEx() { Value = +34F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bodyslam heals you for 42 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bodyslam },
                            Data = new PgNumericValueEx() { Value = +42F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bodyslam heals you for 50 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bodyslam },
                            Data = new PgNumericValueEx() { Value = +50F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bodyslam heals you for 58 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bodyslam },
                            Data = new PgNumericValueEx() { Value = +58F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bodyslam heals you for 66 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bodyslam },
                            Data = new PgNumericValueEx() { Value = +66F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bodyslam heals you for 74 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bodyslam },
                            Data = new PgNumericValueEx() { Value = +74F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bodyslam heals you for 82 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bodyslam },
                            Data = new PgNumericValueEx() { Value = +82F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bodyslam heals you for 90 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bodyslam },
                            Data = new PgNumericValueEx() { Value = +90F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bodyslam heals you for 98 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bodyslam },
                            Data = new PgNumericValueEx() { Value = +98F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bodyslam heals you for 106 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bodyslam },
                            Data = new PgNumericValueEx() { Value = +106F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bodyslam heals you for 114 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bodyslam },
                            Data = new PgNumericValueEx() { Value = +114F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bodyslam heals you for 122 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bodyslam },
                            Data = new PgNumericValueEx() { Value = +122F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bodyslam heals you for 130 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bodyslam },
                            Data = new PgNumericValueEx() { Value = +130F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bodyslam heals you for 138 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bodyslam },
                            Data = new PgNumericValueEx() { Value = +138F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bodyslam heals you for 146 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bodyslam },
                            Data = new PgNumericValueEx() { Value = +146F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bodyslam heals you for 154 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bodyslam },
                            Data = new PgNumericValueEx() { Value = +154F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bodyslam heals you for 162 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bodyslam },
                            Data = new PgNumericValueEx() { Value = +162F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bodyslam heals you for 170 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bodyslam },
                            Data = new PgNumericValueEx() { Value = +170F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bodyslam heals you for 178 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bodyslam },
                            Data = new PgNumericValueEx() { Value = +178F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bodyslam heals you for 186 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bodyslam },
                            Data = new PgNumericValueEx() { Value = +186F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bodyslam heals you for 194 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bodyslam },
                            Data = new PgNumericValueEx() { Value = +194F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bodyslam heals you for 202 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bodyslam },
                            Data = new PgNumericValueEx() { Value = +202F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
            }
        },
        { "4003", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Psychoanalyze restores 10 Health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Psychoanalyze },
                            Data = new PgNumericValueEx() { Value = +10F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Psychoanalyze restores 17 Health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Psychoanalyze },
                            Data = new PgNumericValueEx() { Value = +17F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Psychoanalyze restores 24 Health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Psychoanalyze },
                            Data = new PgNumericValueEx() { Value = +24F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Psychoanalyze restores 31 Health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Psychoanalyze },
                            Data = new PgNumericValueEx() { Value = +31F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Psychoanalyze restores 38 Health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Psychoanalyze },
                            Data = new PgNumericValueEx() { Value = +38F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Psychoanalyze restores 45 Health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Psychoanalyze },
                            Data = new PgNumericValueEx() { Value = +45F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Psychoanalyze restores 52 Health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Psychoanalyze },
                            Data = new PgNumericValueEx() { Value = +52F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Psychoanalyze restores 59 Health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Psychoanalyze },
                            Data = new PgNumericValueEx() { Value = +59F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Psychoanalyze restores 66 Health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Psychoanalyze },
                            Data = new PgNumericValueEx() { Value = +66F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Psychoanalyze restores 73 Health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Psychoanalyze },
                            Data = new PgNumericValueEx() { Value = +73F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Psychoanalyze restores 80 Health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Psychoanalyze },
                            Data = new PgNumericValueEx() { Value = +80F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Psychoanalyze restores 87 Health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Psychoanalyze },
                            Data = new PgNumericValueEx() { Value = +87F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Psychoanalyze restores 94 Health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Psychoanalyze },
                            Data = new PgNumericValueEx() { Value = +94F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Psychoanalyze restores 101 Health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Psychoanalyze },
                            Data = new PgNumericValueEx() { Value = +101F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Psychoanalyze restores 108 Health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Psychoanalyze },
                            Data = new PgNumericValueEx() { Value = +108F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Psychoanalyze restores 115 Health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Psychoanalyze },
                            Data = new PgNumericValueEx() { Value = +115F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Psychoanalyze restores 122 Health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Psychoanalyze },
                            Data = new PgNumericValueEx() { Value = +122F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Psychoanalyze restores 129 Health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Psychoanalyze },
                            Data = new PgNumericValueEx() { Value = +129F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Psychoanalyze restores 136 Health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Psychoanalyze },
                            Data = new PgNumericValueEx() { Value = +136F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Psychoanalyze restores 143 Health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Psychoanalyze },
                            Data = new PgNumericValueEx() { Value = +143F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Psychoanalyze restores 150 Health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Psychoanalyze },
                            Data = new PgNumericValueEx() { Value = +150F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Psychoanalyze restores 157 Health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Psychoanalyze },
                            Data = new PgNumericValueEx() { Value = +157F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Psychoanalyze restores 164 Health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Psychoanalyze },
                            Data = new PgNumericValueEx() { Value = +164F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Psychoanalyze restores 171 Health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Psychoanalyze },
                            Data = new PgNumericValueEx() { Value = +171F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Psychoanalyze restores 178 Health to you after an 8 second delay",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Psychoanalyze },
                            Data = new PgNumericValueEx() { Value = +178F },
                            DelayInSeconds = 8,
                            Target = CombatTarget.Self,
                        },
                    },
                },
            }
        },
        { "4112", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Fast Talk heals you for 10 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FastTalk },
                            Data = new PgNumericValueEx() { Value = +10F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fast Talk heals you for 11 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FastTalk },
                            Data = new PgNumericValueEx() { Value = +11F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fast Talk heals you for 12 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FastTalk },
                            Data = new PgNumericValueEx() { Value = +12F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fast Talk heals you for 13 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FastTalk },
                            Data = new PgNumericValueEx() { Value = +13F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fast Talk heals you for 14 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FastTalk },
                            Data = new PgNumericValueEx() { Value = +14F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fast Talk heals you for 15 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FastTalk },
                            Data = new PgNumericValueEx() { Value = +15F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fast Talk heals you for 17 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FastTalk },
                            Data = new PgNumericValueEx() { Value = +17F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fast Talk heals you for 19 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FastTalk },
                            Data = new PgNumericValueEx() { Value = +19F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fast Talk heals you for 21 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FastTalk },
                            Data = new PgNumericValueEx() { Value = +21F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fast Talk heals you for 23 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FastTalk },
                            Data = new PgNumericValueEx() { Value = +23F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fast Talk heals you for 25 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FastTalk },
                            Data = new PgNumericValueEx() { Value = +25F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fast Talk heals you for 27 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FastTalk },
                            Data = new PgNumericValueEx() { Value = +27F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fast Talk heals you for 29 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FastTalk },
                            Data = new PgNumericValueEx() { Value = +29F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fast Talk heals you for 32 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FastTalk },
                            Data = new PgNumericValueEx() { Value = +32F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fast Talk heals you for 35 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FastTalk },
                            Data = new PgNumericValueEx() { Value = +35F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fast Talk heals you for 37 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FastTalk },
                            Data = new PgNumericValueEx() { Value = +37F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fast Talk heals you for 40 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FastTalk },
                            Data = new PgNumericValueEx() { Value = +40F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fast Talk heals you for 43 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FastTalk },
                            Data = new PgNumericValueEx() { Value = +43F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fast Talk heals you for 46 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FastTalk },
                            Data = new PgNumericValueEx() { Value = +46F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fast Talk heals you for 49 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FastTalk },
                            Data = new PgNumericValueEx() { Value = +49F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fast Talk heals you for 52 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FastTalk },
                            Data = new PgNumericValueEx() { Value = +52F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fast Talk heals you for 55 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FastTalk },
                            Data = new PgNumericValueEx() { Value = +55F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fast Talk heals you for 58 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FastTalk },
                            Data = new PgNumericValueEx() { Value = +58F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fast Talk heals you for 61 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FastTalk },
                            Data = new PgNumericValueEx() { Value = +61F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Fast Talk heals you for 64 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.FastTalk },
                            Data = new PgNumericValueEx() { Value = +64F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
            }
        },
        { "5008", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Pin heals you for 10 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.StaffPin },
                            Data = new PgNumericValueEx() { Value = +10F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pin heals you for 15 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.StaffPin },
                            Data = new PgNumericValueEx() { Value = +15F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pin heals you for 20 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.StaffPin },
                            Data = new PgNumericValueEx() { Value = +20F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pin heals you for 25 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.StaffPin },
                            Data = new PgNumericValueEx() { Value = +25F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pin heals you for 30 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.StaffPin },
                            Data = new PgNumericValueEx() { Value = +30F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pin heals you for 35 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.StaffPin },
                            Data = new PgNumericValueEx() { Value = +35F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pin heals you for 40 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.StaffPin },
                            Data = new PgNumericValueEx() { Value = +40F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pin heals you for 45 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.StaffPin },
                            Data = new PgNumericValueEx() { Value = +45F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pin heals you for 50 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.StaffPin },
                            Data = new PgNumericValueEx() { Value = +50F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pin heals you for 55 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.StaffPin },
                            Data = new PgNumericValueEx() { Value = +55F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pin heals you for 60 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.StaffPin },
                            Data = new PgNumericValueEx() { Value = +60F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Pin heals you for 65 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.StaffPin },
                            Data = new PgNumericValueEx() { Value = +65F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
            }
        },
        { "5253", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Heed The Stick heals you for 5 health (or armor if health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeedTheStick },
                            Data = new PgNumericValueEx() { Value = +5F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heed The Stick heals you for 6 health (or armor if health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeedTheStick },
                            Data = new PgNumericValueEx() { Value = +6F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heed The Stick heals you for 7 health (or armor if health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeedTheStick },
                            Data = new PgNumericValueEx() { Value = +7F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heed The Stick heals you for 8 health (or armor if health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeedTheStick },
                            Data = new PgNumericValueEx() { Value = +8F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heed The Stick heals you for 9 health (or armor if health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeedTheStick },
                            Data = new PgNumericValueEx() { Value = +9F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heed The Stick heals you for 10 health (or armor if health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeedTheStick },
                            Data = new PgNumericValueEx() { Value = +10F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heed The Stick heals you for 11 health (or armor if health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeedTheStick },
                            Data = new PgNumericValueEx() { Value = +11F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heed The Stick heals you for 12 health (or armor if health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeedTheStick },
                            Data = new PgNumericValueEx() { Value = +12F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heed The Stick heals you for 14 health (or armor if health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeedTheStick },
                            Data = new PgNumericValueEx() { Value = +14F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heed The Stick heals you for 16 health (or armor if health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeedTheStick },
                            Data = new PgNumericValueEx() { Value = +16F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heed The Stick heals you for 18 health (or armor if health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeedTheStick },
                            Data = new PgNumericValueEx() { Value = +18F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heed The Stick heals you for 20 health (or armor if health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeedTheStick },
                            Data = new PgNumericValueEx() { Value = +20F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heed The Stick heals you for 22 health (or armor if health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeedTheStick },
                            Data = new PgNumericValueEx() { Value = +22F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heed The Stick heals you for 24 health (or armor if health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeedTheStick },
                            Data = new PgNumericValueEx() { Value = +24F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heed The Stick heals you for 26 health (or armor if health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeedTheStick },
                            Data = new PgNumericValueEx() { Value = +26F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heed The Stick heals you for 28 health (or armor if health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeedTheStick },
                            Data = new PgNumericValueEx() { Value = +28F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heed The Stick heals you for 30 health (or armor if health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeedTheStick },
                            Data = new PgNumericValueEx() { Value = +30F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heed The Stick heals you for 32 health (or armor if health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeedTheStick },
                            Data = new PgNumericValueEx() { Value = +32F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heed The Stick heals you for 34 health (or armor if health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeedTheStick },
                            Data = new PgNumericValueEx() { Value = +34F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heed The Stick heals you for 36 health (or armor if health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeedTheStick },
                            Data = new PgNumericValueEx() { Value = +36F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heed The Stick heals you for 38 health (or armor if health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeedTheStick },
                            Data = new PgNumericValueEx() { Value = +38F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heed The Stick heals you for 40 health (or armor if health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeedTheStick },
                            Data = new PgNumericValueEx() { Value = +40F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heed The Stick heals you for 42 health (or armor if health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeedTheStick },
                            Data = new PgNumericValueEx() { Value = +42F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heed The Stick heals you for 44 health (or armor if health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeedTheStick },
                            Data = new PgNumericValueEx() { Value = +44F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Heed The Stick heals you for 46 health (or armor if health is full)",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealthOrArmor,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.HeedTheStick },
                            Data = new PgNumericValueEx() { Value = +46F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
            }
        },
        { "5434", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Suppress heals you for 15 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Suppress },
                            Data = new PgNumericValueEx() { Value = +15F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Suppress heals you for 17 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Suppress },
                            Data = new PgNumericValueEx() { Value = +17F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Suppress heals you for 19 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Suppress },
                            Data = new PgNumericValueEx() { Value = +19F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Suppress heals you for 20 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Suppress },
                            Data = new PgNumericValueEx() { Value = +20F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Suppress heals you for 22 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Suppress },
                            Data = new PgNumericValueEx() { Value = +22F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Suppress heals you for 24 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Suppress },
                            Data = new PgNumericValueEx() { Value = +24F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Suppress heals you for 26 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Suppress },
                            Data = new PgNumericValueEx() { Value = +26F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Suppress heals you for 28 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Suppress },
                            Data = new PgNumericValueEx() { Value = +28F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Suppress heals you for 30 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Suppress },
                            Data = new PgNumericValueEx() { Value = +30F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Suppress heals you for 32 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Suppress },
                            Data = new PgNumericValueEx() { Value = +32F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Suppress heals you for 34 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Suppress },
                            Data = new PgNumericValueEx() { Value = +34F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Suppress heals you for 36 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Suppress },
                            Data = new PgNumericValueEx() { Value = +36F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Suppress heals you for 38 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Suppress },
                            Data = new PgNumericValueEx() { Value = +38F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Suppress heals you for 40 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Suppress },
                            Data = new PgNumericValueEx() { Value = +40F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Suppress heals you for 42 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Suppress },
                            Data = new PgNumericValueEx() { Value = +42F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Suppress heals you for 45 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Suppress },
                            Data = new PgNumericValueEx() { Value = +45F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Suppress heals you for 48 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Suppress },
                            Data = new PgNumericValueEx() { Value = +48F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Suppress heals you for 51 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Suppress },
                            Data = new PgNumericValueEx() { Value = +51F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Suppress heals you for 54 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Suppress },
                            Data = new PgNumericValueEx() { Value = +54F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Suppress heals you for 57 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Suppress },
                            Data = new PgNumericValueEx() { Value = +57F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Suppress heals you for 60 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Suppress },
                            Data = new PgNumericValueEx() { Value = +60F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Suppress heals you for 63 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Suppress },
                            Data = new PgNumericValueEx() { Value = +63F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Suppress heals you for 66 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Suppress },
                            Data = new PgNumericValueEx() { Value = +66F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Suppress heals you for 69 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Suppress },
                            Data = new PgNumericValueEx() { Value = +69F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Suppress heals you for 72 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Suppress },
                            Data = new PgNumericValueEx() { Value = +72F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
            }
        },
        { "6033", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Bite restores 5 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bite },
                            Data = new PgNumericValueEx() { Value = +5F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bite restores 6 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bite },
                            Data = new PgNumericValueEx() { Value = +6F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bite restores 8 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bite },
                            Data = new PgNumericValueEx() { Value = +8F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bite restores 9 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bite },
                            Data = new PgNumericValueEx() { Value = +9F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bite restores 11 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bite },
                            Data = new PgNumericValueEx() { Value = +11F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bite restores 12 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bite },
                            Data = new PgNumericValueEx() { Value = +12F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bite restores 14 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bite },
                            Data = new PgNumericValueEx() { Value = +14F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bite restores 15 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bite },
                            Data = new PgNumericValueEx() { Value = +15F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bite restores 17 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bite },
                            Data = new PgNumericValueEx() { Value = +17F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bite restores 18 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bite },
                            Data = new PgNumericValueEx() { Value = +18F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bite restores 20 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bite },
                            Data = new PgNumericValueEx() { Value = +20F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bite restores 22 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bite },
                            Data = new PgNumericValueEx() { Value = +22F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bite restores 24 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bite },
                            Data = new PgNumericValueEx() { Value = +24F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bite restores 26 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bite },
                            Data = new PgNumericValueEx() { Value = +26F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bite restores 28 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bite },
                            Data = new PgNumericValueEx() { Value = +28F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bite restores 30 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bite },
                            Data = new PgNumericValueEx() { Value = +30F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bite restores 32 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bite },
                            Data = new PgNumericValueEx() { Value = +32F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bite restores 34 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bite },
                            Data = new PgNumericValueEx() { Value = +34F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bite restores 36 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bite },
                            Data = new PgNumericValueEx() { Value = +36F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bite restores 38 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bite },
                            Data = new PgNumericValueEx() { Value = +38F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bite restores 40 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bite },
                            Data = new PgNumericValueEx() { Value = +40F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bite restores 42 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bite },
                            Data = new PgNumericValueEx() { Value = +42F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bite restores 44 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bite },
                            Data = new PgNumericValueEx() { Value = +44F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bite restores 46 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bite },
                            Data = new PgNumericValueEx() { Value = +46F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Bite restores 48 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Bite },
                            Data = new PgNumericValueEx() { Value = +48F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
            }
        },
        { "6151", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "See Red heals you for 8 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SeeRed },
                            Data = new PgNumericValueEx() { Value = +8F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "See Red heals you for 10 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SeeRed },
                            Data = new PgNumericValueEx() { Value = +10F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "See Red heals you for 12 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SeeRed },
                            Data = new PgNumericValueEx() { Value = +12F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "See Red heals you for 14 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SeeRed },
                            Data = new PgNumericValueEx() { Value = +14F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "See Red heals you for 16 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SeeRed },
                            Data = new PgNumericValueEx() { Value = +16F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "See Red heals you for 18 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SeeRed },
                            Data = new PgNumericValueEx() { Value = +18F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "See Red heals you for 20 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SeeRed },
                            Data = new PgNumericValueEx() { Value = +20F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "See Red heals you for 22 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SeeRed },
                            Data = new PgNumericValueEx() { Value = +22F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "See Red heals you for 24 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SeeRed },
                            Data = new PgNumericValueEx() { Value = +24F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "See Red heals you for 26 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SeeRed },
                            Data = new PgNumericValueEx() { Value = +26F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "See Red heals you for 28 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SeeRed },
                            Data = new PgNumericValueEx() { Value = +28F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "See Red heals you for 30 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SeeRed },
                            Data = new PgNumericValueEx() { Value = +30F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "See Red heals you for 32 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SeeRed },
                            Data = new PgNumericValueEx() { Value = +32F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "See Red heals you for 34 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SeeRed },
                            Data = new PgNumericValueEx() { Value = +34F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "See Red heals you for 36 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SeeRed },
                            Data = new PgNumericValueEx() { Value = +36F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "See Red heals you for 38 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SeeRed },
                            Data = new PgNumericValueEx() { Value = +38F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "See Red heals you for 40 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SeeRed },
                            Data = new PgNumericValueEx() { Value = +40F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "See Red heals you for 42 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SeeRed },
                            Data = new PgNumericValueEx() { Value = +42F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "See Red heals you for 44 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SeeRed },
                            Data = new PgNumericValueEx() { Value = +44F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "See Red heals you for 46 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SeeRed },
                            Data = new PgNumericValueEx() { Value = +46F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "See Red heals you for 48 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SeeRed },
                            Data = new PgNumericValueEx() { Value = +48F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "See Red heals you for 50 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SeeRed },
                            Data = new PgNumericValueEx() { Value = +50F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "See Red heals you for 52 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SeeRed },
                            Data = new PgNumericValueEx() { Value = +52F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "See Red heals you for 54 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SeeRed },
                            Data = new PgNumericValueEx() { Value = +54F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "See Red heals you for 56 health",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.SeeRed },
                            Data = new PgNumericValueEx() { Value = +56F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
            }
        },
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
        { "9754", new PgCombatModCollectionEx()
            {
                new PgCombatModEx()
                {
                    Description = "Electrify restores 8 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify },
                            Data = new PgNumericValueEx() { Value = +8F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify restores 11 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify },
                            Data = new PgNumericValueEx() { Value = +11F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify restores 14 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify },
                            Data = new PgNumericValueEx() { Value = +14F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify restores 17 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify },
                            Data = new PgNumericValueEx() { Value = +17F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify restores 20 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify },
                            Data = new PgNumericValueEx() { Value = +20F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify restores 24 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify },
                            Data = new PgNumericValueEx() { Value = +24F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify restores 28 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify },
                            Data = new PgNumericValueEx() { Value = +28F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify restores 32 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify },
                            Data = new PgNumericValueEx() { Value = +32F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify restores 36 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify },
                            Data = new PgNumericValueEx() { Value = +36F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify restores 40 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify },
                            Data = new PgNumericValueEx() { Value = +40F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify restores 44 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify },
                            Data = new PgNumericValueEx() { Value = +44F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify restores 48 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify },
                            Data = new PgNumericValueEx() { Value = +48F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify restores 52 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify },
                            Data = new PgNumericValueEx() { Value = +52F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify restores 56 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify },
                            Data = new PgNumericValueEx() { Value = +56F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify restores 60 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify },
                            Data = new PgNumericValueEx() { Value = +60F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify restores 64 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify },
                            Data = new PgNumericValueEx() { Value = +64F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify restores 68 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify },
                            Data = new PgNumericValueEx() { Value = +68F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify restores 72 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify },
                            Data = new PgNumericValueEx() { Value = +72F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify restores 76 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify },
                            Data = new PgNumericValueEx() { Value = +76F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify restores 80 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify },
                            Data = new PgNumericValueEx() { Value = +80F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify restores 84 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify },
                            Data = new PgNumericValueEx() { Value = +84F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify restores 88 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify },
                            Data = new PgNumericValueEx() { Value = +88F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify restores 92 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify },
                            Data = new PgNumericValueEx() { Value = +92F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify restores 96 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify },
                            Data = new PgNumericValueEx() { Value = +96F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
                new PgCombatModEx()
                {
                    Description = "Electrify restores 100 Health to you",
                    StaticEffects = new List<PgCombatModEffectEx>()
                    {
                    },
                    DynamicEffects = new List<PgCombatModEffectEx>()
                    {
                        new PgCombatModEffectEx()
                        {
                            Keyword = CombatKeywordEx.RestoreHealth,
                            AbilityList = new List<AbilityKeyword>() { AbilityKeyword.Electrify },
                            Data = new PgNumericValueEx() { Value = +100F },
                            Target = CombatTarget.Self,
                        },
                    },
                },
            }
        },
    };
}
