namespace Pipeline.Test;

using System.Collections.Generic;
using Preprocessor;

public partial class PreprocessorTest
{
    private static List<JsonFile> JsonFileList = new()
    {
        new JsonFile("abilities",
                     true,
                     Preprocessor.PreprocessDictionary<AbilityDictionary>,
                     Fixer.NoFix,
                     Preprocessor.SaveSerializedDictionary<AbilityDictionary, Ability>,
                     Preprocessor.InsertIntDictionary<AbilityDictionary, Ability>,
                     new()
                     {
                         Preprocessor.AdditionalIntInserter<int, int, Ability, Ammo>,
                         Preprocessor.AdditionalIntInserter<int, int, Ability, ConditionalKeyword>,
                         Preprocessor.AdditionalIntInserter<int, int, Ability, Cost>,
                         Preprocessor.AdditionalIntInserter<int, int, Ability, PvEAbility>,
                         Preprocessor.AdditionalIntInserter<int, int, Ability, AbilityParticle>,
                         Preprocessor.AdditionalIntInserter<int, int, Ability, Requirement>,
                         Preprocessor.AdditionalIntInserter<int, int, PvEAbility, DoT>,
                         Preprocessor.AdditionalIntInserter<int, int, PvEAbility, SpecialValue>,
                     },
                     Fixer.NoFix,
                     Preprocessor.SelectIntDictionary<AbilityDictionary, Ability>,
                     new()
                     {
                         Preprocessor.AdditionalIntSelector<int, int, Ability, Ammo>,
                         Preprocessor.AdditionalIntSelector<int, int, Ability, ConditionalKeyword>,
                         Preprocessor.AdditionalIntSelector<int, int, Ability, Cost>,
                         Preprocessor.AdditionalIntSelector<int, int, Ability, PvEAbility>,
                         Preprocessor.AdditionalIntSelector<int, int, Ability, AbilityParticle>,
                         Preprocessor.AdditionalIntSelector<int, int, Ability, Requirement>,
                         Preprocessor.AdditionalIntSelector<int, int, PvEAbility, DoT>,
                         Preprocessor.AdditionalIntSelector<int, int, PvEAbility, SpecialValue>,
                     }),
        new JsonFile("abilitydynamicdots",
                     true,
                     Preprocessor.PreprocessArray<AbilityDynamicDotArray>,
                     Fixer.NoFix,
                     Preprocessor.SaveSerializedList<AbilityDynamicDotArray, AbilityDynamicDot>,
                     Preprocessor.InsertArray<AbilityDynamicDotArray, AbilityDynamicDot>,
                     new()
                     {
                     },
                     Fixer.NoFix,
                     Preprocessor.SelectArray<AbilityDynamicDotArray, AbilityDynamicDot>,
                     new()
                     {
                     }),
        new JsonFile("abilitydynamicspecialvalues",
                     true,
                     Preprocessor.PreprocessArray<AbilityDynamicSpecialValueArray>,
                     Fixer.NoFix,
                     Preprocessor.SaveSerializedList<AbilityDynamicSpecialValueArray, AbilityDynamicSpecialValue>,
                     Preprocessor.InsertArray<AbilityDynamicSpecialValueArray, AbilityDynamicSpecialValue>,
                     new()
                     {
                     },
                     Fixer.NoFix,
                     Preprocessor.SelectArray<AbilityDynamicSpecialValueArray, AbilityDynamicSpecialValue>,
                     new()
                     {
                     }),
        new JsonFile("abilitykeywords",
                     true,
                     Preprocessor.PreprocessArray<AbilityKeywordArray>,
                     Fixer.NoFix,
                     Preprocessor.SaveSerializedList<AbilityKeywordArray, AbilityKeyword>,
                     Preprocessor.InsertArray<AbilityKeywordArray, AbilityKeyword>,
                     new()
                     {
                     },
                     Fixer.NoFix,
                     Preprocessor.SelectArray<AbilityKeywordArray, AbilityKeyword>,
                     new()
                     {
                     }),
        new JsonFile("advancementtables",
                     true,
                     Preprocessor.PreprocessDictionary<AdvancementTableDictionary>,
                     Fixer.NoFix,
                     Preprocessor.SaveSerializedDictionary<AdvancementTableDictionary, AdvancementTable>,
                     Preprocessor.InsertIntDictionary<AdvancementTableDictionary, AdvancementTable>,
                     new()
                     {
                         Preprocessor.AdditionalIntInserter<int, int, AdvancementTable, Advancement>,
                         Preprocessor.AdditionalIntInserter<int, int, Advancement, AdvancementEffectAttribute>,
                     },
                     Fixer.NoFix,
                     Preprocessor.SelectIntDictionary<AdvancementTableDictionary, AdvancementTable>,
                     new()
                     {
                         Preprocessor.AdditionalIntSelector<int, int, AdvancementTable, Advancement>,
                         Preprocessor.AdditionalIntSelector<int, int, Advancement, AdvancementEffectAttribute>,
                     }),
        new JsonFile("ai",
                     true,
                     Preprocessor.PreprocessDictionary<AIDictionary>,
                     Fixer.NoFix,
                     Preprocessor.SaveSerializedDictionary<AIDictionary, AI>,
                     Preprocessor.InsertStringDictionary<AIDictionary, AI>,
                     new()
                     {
                         Preprocessor.AdditionalIntInserter<string, int, AI, AIAbility>,
                     },
                     Fixer.NoFix,
                     Preprocessor.SelectStringDictionary<AIDictionary, AI>,
                     new()
                     {
                         (IFreeSql fsql, List<object> contents, bool allowEmptyArray) => Preprocessor.AdditionalIntSelector<string, int, AI, AIAbility>(fsql, contents, allowEmptyArray: true),
                     }),
        new JsonFile("areas",
                     true,
                     Preprocessor.PreprocessDictionary<AreaDictionary>, Fixer.NoFix,
                     Preprocessor.SaveSerializedDictionary<AreaDictionary, Area>,
                     Preprocessor.InsertStringDictionary<AreaDictionary, Area>,
                     new()
                     {
                     },
                     Fixer.NoFix,
                     Preprocessor.SelectStringDictionary<AreaDictionary, Area>,
                     new()
                     {
                     }),
        new JsonFile("attributes",
                     true,
                     Preprocessor.PreprocessDictionary<AttributeDictionary>,
                     Fixer.FixAttributes,
                     Preprocessor.SaveSerializedDictionary<AttributeDictionary, Attribute>,
                     Preprocessor.InsertStringDictionary<AttributeDictionary, Attribute>,
                     new()
                     {
                     },
                     Fixer.NoFix,
                     Preprocessor.SelectStringDictionary<AttributeDictionary, Attribute>,
                     new()
                     {
                     }),
        new JsonFile("directedgoals",
                     true,
                     Preprocessor.PreprocessDictionary<DirectedGoalDictionary>,
                     Fixer.NoFix,
                     Preprocessor.SaveSerializedDictionary<DirectedGoalDictionary, DirectedGoal>,
                     Preprocessor.InsertIntDictionary<DirectedGoalDictionary, DirectedGoal>,
                     new()
                     {
                     },
                     Fixer.NoFix,
                     Preprocessor.SelectIntDictionary<DirectedGoalDictionary, DirectedGoal>,
                     new()
                     {
                     }),
        new JsonFile("effects",
                     true,
                     Preprocessor.PreprocessDictionary<EffectDictionary>,
                     Fixer.NoFix,
                     Preprocessor.SaveSerializedDictionary<EffectDictionary, Effect>,
                     Preprocessor.InsertIntDictionary<EffectDictionary, Effect>,
                     new()
                     {
                         Preprocessor.AdditionalIntInserter<int, int, Effect, EffectParticle>,
                     },
                     Fixer.NoFix,
                     Preprocessor.SelectIntDictionary<EffectDictionary, Effect>,
                     new()
                     {
                         Preprocessor.AdditionalIntSelector<int, int, Effect, EffectParticle>,
                     }),
        new JsonFile("items",
                     true,
                     Preprocessor.PreprocessDictionary<ItemDictionary>,
                     Fixer.NoFix,
                     Preprocessor.SaveSerializedDictionary<ItemDictionary, Item>,
                     Preprocessor.InsertIntDictionary<ItemDictionary, Item>,
                     new()
                     {
                         Preprocessor.AdditionalIntInserter<int, int, Item, Behavior>,
                         Preprocessor.AdditionalIntInserter<int, int, Item, DroppedAppearance>,
                         Preprocessor.AdditionalIntInserter<int, int, Item, ItemEffect>,
                         Preprocessor.AdditionalIntInserter<int, int, Item, KeywordValues>,
                         Preprocessor.AdditionalIntInserter<int, int, Item, StockDye>,
                     },
                     Fixer.NoFix,
                     Preprocessor.SelectIntDictionary<ItemDictionary, Item>,
                     new()
                     {
                         Preprocessor.AdditionalIntSelector<int, int, Item, Behavior>,
                         Preprocessor.AdditionalIntSelector<int, int, Item, DroppedAppearance>,
                         Preprocessor.AdditionalIntSelector<int, int, Item, ItemEffect>,
                         Preprocessor.AdditionalIntSelector<int, int, Item, KeywordValues>,
                         Preprocessor.AdditionalIntSelector<int, int, Item, StockDye>,
                     }),
        new JsonFile("itemuses",
                     true,
                     Preprocessor.PreprocessDictionary<ItemUseDictionary>,
                     Fixer.NoFix,
                     Preprocessor.SaveSerializedDictionary<ItemUseDictionary, ItemUse>,
                     Preprocessor.InsertIntDictionary<ItemUseDictionary, ItemUse>,
                     new()
                     {
                     },
                     Fixer.NoFix,
                     Preprocessor.SelectIntDictionary<ItemUseDictionary, ItemUse>,
                     new()
                     {
                     }),
        new JsonFile("lorebookinfo",
                     true,
                     Preprocessor.PreprocessSingle<LoreBookInfo>,
                     Fixer.FixLoreBookInfo,
                     Preprocessor.SaveSerializedSingle<LoreBookInfo>,
                     Preprocessor.InsertSingle<LoreBookInfo>,
                     new()
                     {
                         Preprocessor.AdditionalIntInserter<int, string, LoreBookInfo, LoreBookCategory>,
                     },
                     Fixer.NoFix,
                     Preprocessor.SelectSingle<LoreBookInfo>,
                     new()
                     {
                     }),
        new JsonFile("lorebooks",
                     true,
                     Preprocessor.PreprocessDictionary<LoreBookDictionary>,
                     Fixer.NoFix,
                     Preprocessor.SaveSerializedDictionary<LoreBookDictionary, LoreBook>,
                     Preprocessor.InsertIntDictionary<LoreBookDictionary, LoreBook>,
                     new()
                     {
                     },
                     Fixer.NoFix,
                     Preprocessor.SelectIntDictionary<LoreBookDictionary, LoreBook>,
                     new()
                     {
                     }),
        new JsonFile("npcs",
                     true,
                     Preprocessor.PreprocessDictionary<NpcDictionary>,
                     Fixer.NoFix,
                     Preprocessor.SaveSerializedDictionary<NpcDictionary, Npc>,
                     Preprocessor.InsertStringDictionary<NpcDictionary, Npc>,
                     new()
                     {
                     },
                     Fixer.NoFix,
                     Preprocessor.SelectStringDictionary<NpcDictionary, Npc>,
                     new()
                     {
                     }),
        new JsonFile("playertitles",
                     true,
                     Preprocessor.PreprocessDictionary<PlayerTitleDictionary>,
                     Fixer.NoFix,
                     Preprocessor.SaveSerializedDictionary<PlayerTitleDictionary, PlayerTitle>,
                     Preprocessor.InsertIntDictionary<PlayerTitleDictionary, PlayerTitle>,
                     new()
                     {
                     },
                     Fixer.NoFix,
                     Preprocessor.SelectIntDictionary<PlayerTitleDictionary, PlayerTitle>,
                     new()
                     {
                     }),
        new JsonFile("quests",
                     true,
                     Preprocessor.PreprocessDictionary<QuestDictionary>,
                     Fixer.NoFix,
                     Preprocessor.SaveSerializedDictionary<QuestDictionary, Quest>,
                     Preprocessor.InsertIntDictionary<QuestDictionary, Quest>,
                     new()
                     {
                     },
                     Fixer.NoFix,
                     Preprocessor.SelectIntDictionary<QuestDictionary, Quest>,
                     new()
                     {
                     }),
        new JsonFile("recipes",
                     true,
                     Preprocessor.PreprocessDictionary<RecipeDictionary>,
                     Fixer.FixRecipes,
                     Preprocessor.SaveSerializedDictionary<RecipeDictionary, Recipe>,
                     Preprocessor.InsertIntDictionary<RecipeDictionary, Recipe>,
                     new()
                     {
                     },
                     Fixer.NoFix,
                     Preprocessor.SelectIntDictionary<RecipeDictionary, Recipe>,
                     new()
                     {
                     }),
        new JsonFile("skills",
                     true,
                     Preprocessor.PreprocessDictionary<SkillDictionary>,
                     Fixer.FixSkills,
                     Preprocessor.SaveSerializedDictionary<SkillDictionary, Skill>,
                     Preprocessor.InsertStringDictionary<SkillDictionary, Skill>,
                     new()
                     {
                     },
                     Fixer.NoFix,
                     Preprocessor.SelectStringDictionary<SkillDictionary, Skill>,
                     new()
                     {
                     }),
        new JsonFile("sources_abilities",
                     true,
                     Preprocessor.PreprocessDictionary<SourceAbilityDictionary>,
                     Fixer.FixSourceAbilities,
                     Preprocessor.SaveSerializedDictionary<SourceAbilityDictionary, SourceAbility>,
                     Preprocessor.InsertIntDictionary<SourceAbilityDictionary, SourceAbility>,
                     new()
                     {
                     },
                     Fixer.NoFix,
                     Preprocessor.SelectIntDictionary<SourceAbilityDictionary, SourceAbility>,
                     new()
                     {
                     }),
        new JsonFile("sources_items",
                     true,
                     Preprocessor.PreprocessDictionary<SourceItemDictionary>,
                     Fixer.NoFix,
                     Preprocessor.SaveSerializedDictionary<SourceItemDictionary, SourceItem>,
                     Preprocessor.InsertIntDictionary<SourceItemDictionary, SourceItem>,
                     new()
                     {
                     },
                     Fixer.NoFix,
                     Preprocessor.SelectIntDictionary<SourceItemDictionary, SourceItem>,
                     new()
                     {
                     }),
        new JsonFile("sources_recipes",
                     true,
                     Preprocessor.PreprocessDictionary<SourceRecipeDictionary>,
                     Fixer.FixSourceRecipes,
                     Preprocessor.SaveSerializedDictionary<SourceRecipeDictionary, SourceRecipe>,
                     Preprocessor.InsertIntDictionary<SourceRecipeDictionary, SourceRecipe>,
                     new()
                     {
                     },
                     Fixer.NoFix,
                     Preprocessor.SelectIntDictionary<SourceRecipeDictionary, SourceRecipe>,
                     new()
                     {
                     }),
        new JsonFile("storagevaults",
                     true,
                     Preprocessor.PreprocessDictionary<StorageVaultDictionary>,
                     Fixer.FixStorageVaults,
                     Preprocessor.SaveSerializedDictionary<StorageVaultDictionary, StorageVault>,
                     Preprocessor.InsertStringDictionary<StorageVaultDictionary, StorageVault>,
                     new()
                     {
                     },
                     Fixer.NoFix,
                     Preprocessor.SelectStringDictionary<StorageVaultDictionary, StorageVault>,
                     new()
                     {
                     }),
        new JsonFile("tsysclientinfo",
                     true,
                     Preprocessor.PreprocessDictionary<PowerDictionary>,
                     Fixer.FixPowers,
                     Preprocessor.SaveSerializedDictionary<PowerDictionary, Power>,
                     Preprocessor.InsertIntDictionary<PowerDictionary, Power>,
                     new()
                     {
                     },
                     Fixer.NoFix,
                     Preprocessor.SelectIntDictionary<PowerDictionary, Power>,
                     new()
                     {
                     }),
        new JsonFile("tsysprofiles",
                     true,
                     Preprocessor.PreprocessDictionary<ProfileDictionary>,
                     Fixer.NoFix,
                     Preprocessor.SaveSerializedDictionary<ProfileDictionary, Profile>,
                     Preprocessor.InsertStringDictionary<ProfileDictionary, Profile>,
                     new()
                     {
                     },
                     Fixer.NoFix,
                     Preprocessor.SelectStringDictionary<ProfileDictionary, Profile>,
                     new()
                     {
                     }),
        new JsonFile("xptables",
                     true,
                     Preprocessor.PreprocessDictionary<XpTableDictionary>,
                     Fixer.FixXpTables,
                     Preprocessor.SaveSerializedDictionary<XpTableDictionary, XpTable>,
                     Preprocessor.InsertIntDictionary<XpTableDictionary, XpTable>,
                     new()
                     {
                     },
                     Fixer.NoFix,
                     Preprocessor.SelectIntDictionary<XpTableDictionary, XpTable>,
                     new()
                     {
                     }),
    };
}
