namespace Preprocessor;

using System.Collections.Generic;
using System.Linq;

internal class Fixer
{
    public static void NoFix(object objectCollection)
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

    public static void FixQuests(object objectCollection)
    {
        FixQuests((QuestDictionary)objectCollection);
    }

    private static void FixQuests(QuestDictionary dictionary)
    {
        foreach (var Entry in dictionary)
            if (Entry.Value.Objectives is QuestObjective[] Objectives)
                foreach (QuestObjective Objective in Objectives)
                    FixQuestObjective(Objective);
    }

    private static void FixQuestObjective(QuestObjective objective)
    {
        if (objective.Type is string TypeString && TypeString == "Kill" && objective.AbilityKeyword is not null)
        {
            List<Requirement> NewRequirements = new();

            if (objective.Requirements is not null)
                NewRequirements.AddRange(objective.Requirements);

            NewRequirements.Add(new Requirement() { T = "UseAbility", AbilityKeyword = objective.AbilityKeyword });

            objective.Requirements = NewRequirements.ToArray();
            objective.AbilityKeyword = null;
        }
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
