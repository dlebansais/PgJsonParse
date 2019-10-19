using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestRequirementCollection : List<IPgQuestRequirement>, IPgQuestRequirementCollection
    {
        public static IPgQuestRequirement CreateItem(byte[] data, ref int offset)
        {
            OtherRequirementType OtherRequirementType = (OtherRequirementType)BitConverter.ToInt32(data, offset);

            switch (OtherRequirementType)
            {
                case OtherRequirementType.Or:
                    return new PgOrQuestRequirement(data, ref offset);

                case OtherRequirementType.QuestCompleted:
                    return new PgQuestCompletedQuestRequirement(data, ref offset);

                case OtherRequirementType.GuildQuestCompleted:
                    return new PgGuildQuestCompletedQuestRequirement(data, ref offset);

                case OtherRequirementType.MinFavorLevel:
                    return new PgMinFavorLevelQuestRequirement(data, ref offset);

                case OtherRequirementType.MinSkillLevel:
                    return new PgMinSkillLevelQuestRequirement(data, ref offset);

                case OtherRequirementType.RuntimeBehaviorRuleSet:
                    return new PgRunTimeBehaviorRuleSetQuestRequirement(data, ref offset);

                case OtherRequirementType.HasEffectKeyword:
                    return new PgHasEffectKeywordQuestRequirement(data, ref offset);

                case OtherRequirementType.IsLongtimeAnimal:
                    return new PgIsLongTimeAnimalQuestRequirement(data, ref offset);

                case OtherRequirementType.InteractionFlagSet:
                    return new PgInteractionFlagSetQuestRequirement(data, ref offset);

                case OtherRequirementType.HangOutCompleted:
                    return new PgHangOutCompletedQuestRequirement(data, ref offset);

                case OtherRequirementType.IsWarden:
                    return new PgIsWardenQuestRequirement(data, ref offset);

                case OtherRequirementType.AreaEventOn:
                    return new PgAreaEventOnQuestRequirement(data, ref offset);

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
