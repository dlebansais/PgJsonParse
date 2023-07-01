namespace Translator;

using System;
using System.Collections.Generic;
using PgObjects;

public static class MainParser
{
    public static Dictionary<Type, Parser> Parsers { get; } = new Dictionary<Type, Parser>()
    {
        { typeof(bool), new ParserNative() },
        { typeof(int), new ParserNative() },
        { typeof(float), new ParserNative() },
        { typeof(string), new ParserNative() },
        { typeof(PgAbility), new ParserAbility() },
        { typeof(PgAbilityAmmo), new ParserAbilityAmmo() },
        { typeof(PgAbilityPvX), new ParserAbilityPvX() },
        { typeof(PgSelfPreEffect), new ParserSelfPreEffect() },
        { typeof(PgAbilityRequirement), new ParserAbilityRequirement() },
        { typeof(PgSelfParticle), new ParserSelfParticle() },
        { typeof(PgSelfPreParticle), new ParserSelfPreParticle() },
        { typeof(PgTargetParticle), new ParserTargetParticle() },
        { typeof(PgDoT), new ParserDoT() },
        { typeof(PgSpecialValue), new ParserSpecialValue() },
        { typeof(PgAdvancementTable), new ParserAdvancementTable() },
        { typeof(PgAdvancement), new ParserAdvancement() },
        { typeof(PgAdvancementEffectAttribute), new ParserAdvancementEffectAttribute() },
        { typeof(PgAI), new ParserAI() },
        { typeof(PgAIAbilitySet), new ParserAIAbilitySet() },
        { typeof(PgAIAbility), new ParserAIAbility() },
        { typeof(PgArea), new ParserArea() },
        { typeof(PgAttribute), new ParserAttribute() },
        { typeof(PgDirectedGoal), new ParserDirectedGoal() },
        { typeof(PgEffect), new ParserEffect() },
        { typeof(PgEffectParticle), new ParserEffectParticle() },
        { typeof(PgItem), new ParserItem() },
        { typeof(PgItemSkillLink), new ParserItemSkillLink() },
        { typeof(PgItemBehavior), new ParserItemBehavior() },
        { typeof(PgDroppedAppearance), new ParserDroppedAppearance() },
        { typeof(PgItemKeywordValues), new ParserItemKeywordValues() },
        { typeof(PgItemEffect), new ParserItemEffect() },
        { typeof(PgItemUse), new ParserItemUse() },
        { typeof(PgStockDye), new ParserStockDye() },
        { typeof(PgLoreBookInfo), new ParserLoreBookInfo() },
        { typeof(PgLoreBookInfoCategory), new ParserLoreBookInfoCategory() },
        { typeof(PgLoreBook), new ParserLoreBook() },
        { typeof(PgNpc), new ParserNpc() },
        { typeof(PgNpcPreference), new ParserNpcPreference() },
        { typeof(PgPlayerTitle), new ParserPlayerTitle() },
        { typeof(PgPower), new ParserPower() },
        //{ typeof(PgPowerTierList), new ParserPowerTierList() },
        { typeof(PgPowerTier), new ParserPowerTier() },
        { typeof(PgPowerEffect), new ParserPowerEffect() },
        { typeof(PgQuest), new ParserQuest() },
        { typeof(PgQuestRequirement), new ParserQuestRequirement() },
        { typeof(PgQuestObjective), new ParserQuestObjective() },
        { typeof(PgQuestObjectiveRequirement), new ParserQuestObjectiveRequirement() },
        { typeof(PgQuestRewardWorkOrderCurrency), new ParserQuestRewardWorkOrderCurrency() },
        { typeof(PgQuestRewardItem), new ParserQuestRewardItem() },
        { typeof(PgQuestReward), new ParserQuestReward() },
        { typeof(PgQuestPreGiveEffect), new ParserQuestPreGiveEffect() },
        { typeof(PgQuestTime), new ParserQuestTime() },
        { typeof(PgRecipe), new ParserRecipe() },
        { typeof(PgRecipeItem), new ParserRecipeItem() },
        { typeof(PgRecipeCost), new ParserRecipeCost() },
        { typeof(PgRecipeParticle), new ParserRecipeParticle() },
        { typeof(PgRecipeResultEffect), new ParserRecipeResultEffect() },
        { typeof(PgSkill), new ParserSkill() },
        { typeof(PgLevelCapInteraction), new ParserLevelCapInteraction() },
        { typeof(PgAdvancementHint), new ParserAdvancementHint() },
        { typeof(PgReward), new ParserReward() },
        { typeof(PgReport), new ParserReport() },
        { typeof(PgSourceEntriesAbility), new ParserSourceEntriesAbility() },
        { typeof(PgSourceEntriesRecipe), new ParserSourceEntriesRecipe() },
        { typeof(PgSource), new ParserSource() },
        { typeof(PgStorageVault), new ParserStorageVault() },
        { typeof(PgStorageEventList), new ParserStorageEventLevel() },
        { typeof(PgStorageFavorLevel), new ParserStorageFavorLevel() },
        { typeof(PgStorageRequirement), new ParserStorageRequirement() },
        { typeof(PgXpTable), new ParserXpTable() },
    };

    public static bool GetParsingContext(Type type, FieldTable fieldTable, out ParsingContext context)
    {
        return GetParsingContext(type, fieldTable, string.Empty, out context);
    }

    public static bool GetParsingContext(Type type, FieldTable fieldTable, string objectKey, out ParsingContext context)
    {
        if (!Parsers.ContainsKey(type))
        {
            context = null!;
            return Program.ReportFailure($"Type {type} not found in parsers");
        }

        Parser Parser = Parsers[type];

        context = new ParsingContext(Parser, type, fieldTable, objectKey);
        return true;
    }
}
