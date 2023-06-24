namespace Preprocessor;

using System.Collections.Generic;

internal class Fixer
{
    public static void FixAbilities(object objectCollection)
    {
        FixAbilities((AbilityDictionary)objectCollection);
    }

    private static void FixAbilities(AbilityDictionary dictionary)
    {
    }

    public static void FixAdvancementTables(object objectCollection)
    {
        FixAdvancementTables((AdvancementTableDictionary)objectCollection);
    }

    private static void FixAdvancementTables(AdvancementTableDictionary dictionary)
    {
    }

    public static void FixAI(object objectCollection)
    {
        FixAI((AIDictionary)objectCollection);
    }

    private static void FixAI(AIDictionary dictionary)
    {
    }

    public static void FixAreas(object objectCollection)
    {
        FixAreas((AreaDictionary)objectCollection);
    }

    private static void FixAreas(AreaDictionary dictionary)
    {
    }

    public static void FixAttributes(object objectCollection)
    {
        FixAttributes((AttributeDictionary)objectCollection);
    }

    private static void FixAttributes(AttributeDictionary dictionary)
    {
        AddNonClientAttributes(dictionary);
    }

    private static void AddNonClientAttributes(AttributeDictionary dictionary)
    {
        List<string> AttributeKeyList = new()
        {
            "COCKATRICEDEBUFF_COST_DELTA",
            "LAMIADEBUFF_COST_DELTA",
            "MONSTER_MATCH_OWNER_SPEED",
            "ARMOR_MITIGATION_RATIO",
            "SHOW_CLEANLINESS_INDICATORS",
            "SHOW_COMMUNITY_INDICATORS",
            "SHOW_PEACEABLENESS_INDICATORS",
            "SHOW_FAIRYENERGY_INDICATORS",
            "BOOST_ANIMALPETHEAL_SENDER",
            "MONSTER_COMBAT_XP_VALUE",
            "MOD_VAULT_SIZE",
            "MENTAL_DEFENSE_RATING",
        };

        foreach (string Key in AttributeKeyList)
            dictionary.Add(Key, new Attribute() { DisplayRule = "Never", DisplayType = "AsBuffDelta" });
    }

    public static void FixDirectedGoals(object objectCollection)
    {
        FixDirectedGoals((DirectedGoalDictionary)objectCollection);
    }

    private static void FixDirectedGoals(DirectedGoalDictionary dictionary)
    {
    }

    public static void FixEffects(object objectCollection)
    {
        FixEffects((EffectDictionary)objectCollection);
    }

    private static void FixEffects(EffectDictionary dictionary)
    {
    }

    public static void FixItems(object objectCollection)
    {
        FixItems((ItemDictionary)objectCollection);
    }

    private static void FixItems(ItemDictionary dictionary)
    {
    }

    public static void FixItemUses(object objectCollection)
    {
        FixItemUses((ItemUseDictionary)objectCollection);
    }

    private static void FixItemUses(ItemUseDictionary dictionary)
    {
    }

    public static void FixLoreBookInfo(object item)
    {
        FixItemUses((LoreBookInfo)item);
    }

    private static void FixItemUses(LoreBookInfo item)
    {
    }

    public static void FixLoreBooks(object objectCollection)
    {
        FixLoreBooks((LoreBookDictionary)objectCollection);
    }

    private static void FixLoreBooks(LoreBookDictionary dictionary)
    {
    }

    public static void FixNpcs(object objectCollection)
    {
        FixNpcs((NpcDictionary)objectCollection);
    }

    private static void FixNpcs(NpcDictionary dictionary)
    {
    }

    public static void FixPlayerTitles(object objectCollection)
    {
        FixPlayerTitles((PlayerTitleDictionary)objectCollection);
    }

    private static void FixPlayerTitles(PlayerTitleDictionary dictionary)
    {
    }

    public static void FixQuests(object objectCollection)
    {
        FixQuests((QuestDictionary)objectCollection);
    }

    private static void FixQuests(QuestDictionary dictionary)
    {
    }

    public static void FixRecipes(object objectCollection)
    {
        FixRecipes((RecipeDictionary)objectCollection);
    }

    private static void FixRecipes(RecipeDictionary dictionary)
    {
    }

    public static void FixSkills(object objectCollection)
    {
        FixSkills((SkillDictionary)objectCollection);
    }

    private static void FixSkills(SkillDictionary dictionary)
    {
    }

    public static void FixSourceAbilities(object objectCollection)
    {
        FixSourceAbilities((SourceAbilityDictionary)objectCollection);
    }

    private static void FixSourceAbilities(SourceAbilityDictionary dictionary)
    {
    }

    public static void FixSourceRecipes(object objectCollection)
    {
        FixSourceRecipes((SourceRecipeDictionary)objectCollection);
    }

    private static void FixSourceRecipes(SourceRecipeDictionary dictionary)
    {
    }

    public static void FixStorageVaults(object objectCollection)
    {
        FixStorageVaults((StorageVaultDictionary)objectCollection);
    }

    private static void FixStorageVaults(StorageVaultDictionary dictionary)
    {
    }

    public static void FixPowers(object objectCollection)
    {
        FixPowers((PowerDictionary)objectCollection);
    }

    private static void FixPowers(PowerDictionary dictionary)
    {
    }

    public static void FixXpTables(object objectCollection)
    {
        FixXpTables((XpTableDictionary)objectCollection);
    }

    private static void FixXpTables(XpTableDictionary dictionary)
    {
    }
}
