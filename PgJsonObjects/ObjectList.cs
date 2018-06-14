using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public static class ObjectList
    {
        public static Dictionary<Type, IObjectDefinition> Definitions { get; private set; } = new Dictionary<Type, IObjectDefinition>()
        {
            { typeof(Ability), new ObjectDefinition<Ability>("abilities", 0, false, true, false) },
            { typeof(AdvancementTable), new ObjectDefinition<AdvancementTable>("advancementtables", 0, false, true, false) },
            { typeof(AI), new ObjectDefinition<AI>("ai", 305, false, true, false) },
            { typeof(GameArea), new ObjectDefinition<GameArea>("areas", 0, false, true, false) },
            { typeof(PgJsonObjects.Attribute), new ObjectDefinition<PgJsonObjects.Attribute>("attributes", 0, false, true, false) },
            { typeof(DirectedGoal), new ObjectDefinition<DirectedGoal>("directedgoals", 0, false, true, false) },
            { typeof(Effect), new ObjectDefinition<Effect>("effects", 0, false, true, false) },
            { typeof(Item), new ObjectDefinition<Item>("items", 0, false, true, false) },
            { typeof(LoreBookInfo), new ObjectDefinition<LoreBookInfo>("lorebookinfo", 293, false, true, false) },
            { typeof(LoreBook), new ObjectDefinition<LoreBook>("lorebooks", 293, false, true, false) },
            { typeof(GameNpc), new ObjectDefinition<GameNpc>("npcs", 0, false, false, false) },
            { typeof(PlayerTitle), new ObjectDefinition<PlayerTitle>("playertitles", 305, false, true, false) },
            { typeof(Quest), new ObjectDefinition<Quest>("quests", 0, false, true, false) },
            { typeof(Recipe), new ObjectDefinition<Recipe>("recipes", 0, false, true, false) },
            { typeof(Skill), new ObjectDefinition<Skill>("skills", 0, false, true, false) },
            { typeof(AbilitySource), new ObjectDefinition<AbilitySource>("sources_abilities", 0, true, true, false) },
            { typeof(RecipeSource), new ObjectDefinition<RecipeSource>("sources_recipes", 0, true, true, false) },
            //{ typeof(PgJsonObjects.String), new ObjectDefinition<PgJsonObjects.String>("strings", false, true, false) },
            { typeof(Power), new ObjectDefinition<Power>("tsysclientinfo", 0, false, true, false) },
            { typeof(XpTable), new ObjectDefinition<XpTable>("xptables", 0, false, true, false) },

            { typeof(StorageVault), new ObjectDefinition<StorageVault>("storagevaults", 0, false, true, false) },
            { typeof(ItemUses), new ObjectDefinition<ItemUses>("itemuses", 0, false, false, false) },
        };
    }
}
