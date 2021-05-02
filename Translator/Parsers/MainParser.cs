namespace Translator
{
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
            { typeof(PgAbilityRequirement), new ParserAbilityRequirement() },
            { typeof(PgDoT), new ParserDoT() },
            { typeof(PgSpecialValue), new ParserSpecialValue() },
            { typeof(PgAdvancement), new ParserAdvancement() },
            { typeof(PgAdvancementTable), new ParserAdvancementTable() },
            { typeof(PgAI), new ParserAI() },
            { typeof(PgAIAbilitySet), new ParserAIAbilitySet() },
            { typeof(PgAIAbility), new ParserAIAbility() },
            { typeof(PgArea), new ParserArea() },
            { typeof(PgAttribute), new ParserAttribute() },
            { typeof(PgDirectedGoal), new ParserDirectedGoal() },
            { typeof(PgEffect), new ParserEffect() },
            { typeof(PgItem), new ParserItem() },
            { typeof(PgItemSkillLink), new ParserItemSkillLink() },
            { typeof(PgItemBehavior), new ParserItemBehavior() },
            { typeof(PgItemUse), new ParserItemUse() },
            { typeof(PgLoreBookInfo), new ParserLoreBookInfo() },
            { typeof(PgLoreBookInfoCategory), new ParserLoreBookInfoCategory() },
            { typeof(PgLoreBook), new ParserLoreBook() },
            { typeof(PgNpc), new ParserNpc() },
            { typeof(PgNpcPreference), new ParserNpcPreference() },
            { typeof(PgPlayerTitle), new ParserPlayerTitle() },
            { typeof(PgPower), new ParserPower() },
            { typeof(PgPowerTierList), new ParserPowerTierList() },
            { typeof(PgPowerTier), new ParserPowerTier() },
            { typeof(PgQuest), new ParserQuest() },
            { typeof(PgQuestRequirement), new ParserQuestRequirement() },
            { typeof(PgQuestObjective), new ParserQuestObjective() },
            { typeof(PgQuestObjectiveRequirement), new ParserQuestObjectiveRequirement() },
            { typeof(PgQuestRewardSkillXp), new ParserQuestRewardSkillXp() },
            { typeof(PgQuestRewardCurrency), new ParserQuestRewardCurrency() },
            { typeof(PgQuestRewardItem), new ParserQuestRewardItem() },
            { typeof(PgQuestReward), new ParserQuestReward() },
            { typeof(PgRecipe), new ParserRecipe() },
            { typeof(PgRecipeItem), new ParserRecipeItem() },
            { typeof(PgRecipeCost), new ParserRecipeCost() },
            { typeof(PgSkill), new ParserSkill() },
            { typeof(PgLevelCapInteractionList), new ParserLevelCapInteractionList() },
            { typeof(PgAdvancementHint), new ParserAdvancementHint() },
            { typeof(PgRewardList), new ParserRewardList() },
            { typeof(PgReward), new ParserReward() },
            { typeof(PgReportList), new ParserReportList() },
            { typeof(PgSource), new ParserSource() },
            { typeof(PgStorageVault), new ParserStorageVault() },
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
}
