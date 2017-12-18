using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public static class ObjectList
    {
        public static Dictionary<Type, IObjectDefinition> Definitions { get; private set; } = new Dictionary<Type, IObjectDefinition>()
        {
            { typeof(Ability), new ObjectDefinition<Ability>("abilities") },
            { typeof(AbilitySource), new ObjectDefinition<AbilitySource>("sources_abilities") },
            { typeof(AdvancementTable), new ObjectDefinition<AdvancementTable>("advancementtables") },
            { typeof(PgJsonObjects.Attribute), new ObjectDefinition<PgJsonObjects.Attribute>("attributes") },
            { typeof(DirectedGoal), new ObjectDefinition<DirectedGoal>("directedgoals") },
            { typeof(GameArea), new ObjectDefinition<GameArea>("areas") },
            { typeof(GameNpc), new ObjectDefinition<GameNpc>("npcs") },
            { typeof(StorageVault), new ObjectDefinition<StorageVault>("storagevaults") },
            { typeof(Effect), new ObjectDefinition<Effect>("effects") },
            { typeof(Item), new ObjectDefinition<Item>("items") },
            { typeof(ItemUses), new ObjectDefinition<ItemUses>("itemuses") },
            { typeof(Quest), new ObjectDefinition<Quest>("quests") },
            { typeof(Recipe), new ObjectDefinition<Recipe>("recipes") },
            { typeof(RecipeSource), new ObjectDefinition<RecipeSource>("sources_recipes") },
            { typeof(Skill), new ObjectDefinition<Skill>("skills") },
            //{ typeof(PgJsonObjects.String), new ObjectDefinition<PgJsonObjects.String>("strings") },
            { typeof(Power), new ObjectDefinition<Power>("tsysclientinfo") },
            { typeof(XpTable), new ObjectDefinition<XpTable>("xptables") },
        };
    }
}
