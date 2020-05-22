﻿using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public static class ObjectList
    {
        public static Dictionary<Type, IObjectDefinition> Definitions { get; private set; } = new Dictionary<Type, IObjectDefinition>()
        {
            { typeof(Ability), new ObjectDefinition<Ability, PgAbility, IPgAbility>("abilities", 0, PgAbility.CreateNew, false, false, true, true) },
            { typeof(AdvancementTable), new ObjectDefinition<AdvancementTable, PgAdvancementTable, IPgAdvancementTable>("advancementtables", 0, PgAdvancementTable.CreateNew, false, false, true, true) },
            { typeof(AI), new ObjectDefinition<AI, PgAI, IPgAI>("ai", 305, PgAI.CreateNew, false, false, true, true) },
            { typeof(GameArea), new ObjectDefinition<GameArea, PgGameArea, IPgGameArea>("areas", 0, PgGameArea.CreateNew, false, false, true, true) },
            { typeof(PgJsonObjects.Attribute), new ObjectDefinition<PgJsonObjects.Attribute, PgAttribute, IPgAttribute>("attributes", 0, PgAttribute.CreateNew, false, false, true, true) },
            { typeof(DirectedGoal), new ObjectDefinition<DirectedGoal, PgDirectedGoal, IPgDirectedGoal>("directedgoals", 0, PgDirectedGoal.CreateNew, true, false, true, true) },
            { typeof(Effect), new ObjectDefinition<Effect, PgEffect, IPgEffect>("effects", 0, PgEffect.CreateNew, false, false, true, true) },
            { typeof(Item), new ObjectDefinition<Item, PgItem, IPgItem>("items", 0, PgItem.CreateNew, false, false, true, true) },
            { typeof(LoreBookInfo), new ObjectDefinition<LoreBookInfo, PgLoreBookInfo, IPgLoreBookInfo>("lorebookinfo", 293, PgLoreBookInfo.CreateNew, false, false, true, true) },
            { typeof(LoreBook), new ObjectDefinition<LoreBook, PgLoreBook, IPgLoreBook>("lorebooks", 293, PgLoreBook.CreateNew, false, false, true, true) },
            { typeof(GameNpc), new ObjectDefinition<GameNpc, PgGameNpc, IPgGameNpc>("npcs", 0, PgGameNpc.CreateNew, false, false, false, true) },
            { typeof(PlayerTitle), new ObjectDefinition<PlayerTitle, PgPlayerTitle, IPgPlayerTitle>("playertitles", 305, PgPlayerTitle.CreateNew, false, false, true, true) },
            { typeof(Quest), new ObjectDefinition<Quest, PgQuest, IPgQuest>("quests", 0, PgQuest.CreateNew, false, false, true, true) },
            { typeof(Recipe), new ObjectDefinition<Recipe, PgRecipe, IPgRecipe>("recipes", 0, PgRecipe.CreateNew, false, false, true, true) },
            { typeof(Skill), new ObjectDefinition<Skill, PgSkill, IPgSkill>("skills", 0, PgSkill.CreateNew, false, false, true, true) },
            { typeof(AbilitySource), new ObjectDefinition<AbilitySource, PgAbilitySource, IPgAbilitySource>("sources_abilities", 0, PgAbilitySource.CreateNew, false, true, true, true) },
            { typeof(RecipeSource), new ObjectDefinition<RecipeSource, PgRecipeSource, IPgRecipeSource>("sources_recipes", 0, PgRecipeSource.CreateNew, false, true, true, true) },
            { typeof(Power), new ObjectDefinition<Power, PgPower, IPgPower>("tsysclientinfo", 0, PgPower.CreateNew, false, false, true, true) },
            { typeof(XpTable), new ObjectDefinition<XpTable, PgXpTable, IPgXpTable>("xptables", 0, PgXpTable.CreateNew, false, false, true, true) },
            { typeof(StorageVault), new ObjectDefinition<StorageVault, PgStorageVault, IPgStorageVault>("storagevaults", 0, PgStorageVault.CreateNew, false, false, true, true) },
            { typeof(ItemUses), new ObjectDefinition<ItemUses, PgItemUses, IPgItemUses>("itemuses", 0, PgItemUses.CreateNew, false, false, false, true) },
        };

        public static void DeserializeObjects(byte[] data, byte[] currentOffset, List<IObjectDefinition> definitionList, int progressIndex)
        {
            IObjectDefinition Definition = definitionList[progressIndex];
            int Offset = BitConverter.ToInt32(currentOffset, 0);

            Definition.JsonObjectList.Clear();

            IMainPgObjectCollection PgObjectList = Definition.PgObjectList;
            PgObjectList.Clear();

            int Count = BitConverter.ToInt32(data, Offset);
            Offset += 4;

            int ObjectOffset = Offset;

            for (int i = 0; i < Count; i++)
            {
                Offset = BitConverter.ToInt32(data, ObjectOffset + i * 4);

                IMainPgObject Item = GenericPgObject.CreateMainObject(Definition.CreateNewObject, data, ref Offset);
                PgObjectList.Add(Item);
            }

            Offset = BitConverter.ToInt32(data, ObjectOffset + Count * 4);
            Array.Copy(BitConverter.GetBytes(Offset), 0, currentOffset, 0, 4);
        }

        public static void FinalizeDeserializingObjects()
        {
            foreach (KeyValuePair<Type, IObjectDefinition> Entry in Definitions)
            {
                IObjectDefinition definition = Entry.Value;
                IMainPgObjectCollection PgObjectList = definition.PgObjectList;

                foreach (IGenericPgObject Item in PgObjectList)
                    if (Item is IBackLinkable AsLinkBack)
                        AsLinkBack.SortLinkBack();
            }

            foreach (KeyValuePair<Type, IObjectDefinition> Entry in Definitions)
            {
                IObjectDefinition Definition = Entry.Value;
                Dictionary<string, IJsonKey> ObjectTable = Definition.ObjectTable;
                IMainPgObjectCollection PgObjectList = Definition.PgObjectList;

                if (ObjectTable.Count == 0)
                    foreach (IJsonKey Item in PgObjectList)
                        ObjectTable.Add(Item.Key, Item);
            }

            Dictionary<string, IJsonKey> NpcTable = Definitions[typeof(GameNpc)].ObjectTable;
            Dictionary<string, IJsonKey> ItemTable = Definitions[typeof(Item)].ObjectTable;
            Dictionary<string, IJsonKey> PowerTable = Definitions[typeof(PgJsonObjects.Power)].ObjectTable;
            Dictionary<string, IJsonKey> AttributeTable = Definitions[typeof(PgJsonObjects.Attribute)].ObjectTable;

            foreach (KeyValuePair<string, IJsonKey> Entry in NpcTable)
            {
                IPgGameNpc Npc = Entry.Value as IPgGameNpc;
                foreach (IPgNpcPreference NpcPreference in Npc.PreferenceList)
                    NpcPreference.InitFavorList(ItemTable);
            }

            foreach (KeyValuePair<string, IJsonKey> Entry in PowerTable)
            {
                IPgPower Power = (IPgPower)Entry.Value;
                Power.InitTierList(AttributeTable);
            }
        }
    }
}
