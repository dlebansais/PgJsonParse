﻿using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public static class ObjectList
    {
        public static Dictionary<Type, IObjectDefinition> Definitions { get; private set; } = new Dictionary<Type, IObjectDefinition>()
        {
            { typeof(GameArea), new ObjectDefinition<GameArea, PgGameArea>("areas", 0, PgGameArea.CreateNew, false, true, false) },
            { typeof(DirectedGoal), new ObjectDefinition<DirectedGoal, PgDirectedGoal>("directedgoals", 0, PgDirectedGoal.CreateNew, false, true, false) },

            { typeof(Ability), new ObjectDefinition<Ability, PgAbility>("abilities", 0, PgAbility.CreateNew, false, true, false) },
            { typeof(AdvancementTable), new ObjectDefinition<AdvancementTable, PgAdvancementTable>("advancementtables", 0, PgAdvancementTable.CreateNew, false, true, false) },
            { typeof(AI), new ObjectDefinition<AI, PgAI>("ai", 305, PgAI.CreateNew, false, true, false) },

            //{ typeof(GameArea), new ObjectDefinition<GameArea, PgGameArea>("areas", 0, PgGameArea.CreateNew, false, true, false) },

            { typeof(PgJsonObjects.Attribute), new ObjectDefinition<PgJsonObjects.Attribute, PgAttribute>("attributes", 0, PgAttribute.CreateNew, false, true, false) },

            //{ typeof(DirectedGoal), new ObjectDefinition<DirectedGoal, PgDirectedGoal>("directedgoals", 0, PgDirectedGoal.CreateNew, false, true, false) },

            { typeof(Effect), new ObjectDefinition<Effect, PgEffect>("effects", 0, PgEffect.CreateNew, false, true, false) },
            { typeof(Item), new ObjectDefinition<Item, PgItem>("items", 0, PgItem.CreateNew, false, true, false) },
            { typeof(LoreBookInfo), new ObjectDefinition<LoreBookInfo, PgLoreBookInfo>("lorebookinfo", 293, PgLoreBookInfo.CreateNew, false, true, false) },
            { typeof(LoreBook), new ObjectDefinition<LoreBook, PgLoreBook>("lorebooks", 293, PgLoreBook.CreateNew, false, true, false) },
            { typeof(GameNpc), new ObjectDefinition<GameNpc, PgGameNpc>("npcs", 0, PgGameNpc.CreateNew, false, false, false) },
            { typeof(PlayerTitle), new ObjectDefinition<PlayerTitle, PgPlayerTitle>("playertitles", 305, PgPlayerTitle.CreateNew, false, true, false) },
            { typeof(Quest), new ObjectDefinition<Quest, PgQuest>("quests", 0, PgQuest.CreateNew, false, true, false) },
            { typeof(Recipe), new ObjectDefinition<Recipe, PgRecipe>("recipes", 0, PgRecipe.CreateNew, false, true, false) },
            { typeof(Skill), new ObjectDefinition<Skill, PgSkill>("skills", 0, PgSkill.CreateNew, false, true, false) },
            { typeof(AbilitySource), new ObjectDefinition<AbilitySource, PgAbilitySource>("sources_abilities", 0, PgAbilitySource.CreateNew, true, true, false) },
            { typeof(RecipeSource), new ObjectDefinition<RecipeSource, PgRecipeSource>("sources_recipes", 0, PgRecipeSource.CreateNew, true, true, false) },
            { typeof(Power), new ObjectDefinition<Power, PgPower>("tsysclientinfo", 0, PgPower.CreateNew, false, true, false) },
            { typeof(XpTable), new ObjectDefinition<XpTable, PgXpTable>("xptables", 0, PgXpTable.CreateNew, false, true, false) },
            { typeof(StorageVault), new ObjectDefinition<StorageVault, PgStorageVault>("storagevaults", 0, PgStorageVault.CreateNew, false, true, false) },
            { typeof(ItemUses), new ObjectDefinition<ItemUses, PgItemUses>("itemuses", 0, PgItemUses.CreateNew, false, false, false) },
        };
    }
}
