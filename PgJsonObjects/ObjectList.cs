using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public static class ObjectList
    {
        public static Dictionary<Type, IObjectDefinition> Definitions { get; private set; } = new Dictionary<Type, IObjectDefinition>()
        {
            { typeof(Ability), new ObjectDefinition<Ability>("abilities", 0) },
            { typeof(AbilitySource), new ObjectDefinition<AbilitySource>("sources_abilities", 0) },
            { typeof(AdvancementTable), new ObjectDefinition<AdvancementTable>("advancementtables", 0) },
            { typeof(PgJsonObjects.Attribute), new ObjectDefinition<PgJsonObjects.Attribute>("attributes", 0) },
            { typeof(DirectedGoal), new ObjectDefinition<DirectedGoal>("directedgoals", 0) },
            { typeof(GameArea), new ObjectDefinition<GameArea>("areas", 0) },
            { typeof(GameNpc), new ObjectDefinition<GameNpc>("npcs", 0) },
            { typeof(StorageVault), new ObjectDefinition<StorageVault>("storagevaults", 0) },
            { typeof(Effect), new ObjectDefinition<Effect>("effects", 0) },
            { typeof(Item), new ObjectDefinition<Item>("items", 0) },
            { typeof(ItemUses), new ObjectDefinition<ItemUses>("itemuses", 0) },
            { typeof(Quest), new ObjectDefinition<Quest>("quests", 0) },
            { typeof(Recipe), new ObjectDefinition<Recipe>("recipes", 0) },
            { typeof(RecipeSource), new ObjectDefinition<RecipeSource>("sources_recipes", 0) },
            { typeof(Skill), new ObjectDefinition<Skill>("skills", 0) },
            //{ typeof(PgJsonObjects.String), new ObjectDefinition<PgJsonObjects.String>("strings") },
            { typeof(Power), new ObjectDefinition<Power>("tsysclientinfo", 0) },
            { typeof(XpTable), new ObjectDefinition<XpTable>("xptables", 0) },
            { typeof(LoreBook), new ObjectDefinition<LoreBook>("lorebooks", 293) },
            { typeof(LoreBookInfo), new ObjectDefinition<LoreBookInfo>("lorebookinfo", 293) },
        };
    }
}
