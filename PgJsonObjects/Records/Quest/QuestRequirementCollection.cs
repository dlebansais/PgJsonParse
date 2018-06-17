using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestRequirementCollection : List<QuestRequirement>, ISerializableJsonObjectCollection
    {
        public ISerializableJsonObject GetAt(int index)
        {
            return this[index] as ISerializableJsonObject;
        }

        public static IGenericPgObject CreateItem(byte[] data, int offset)
        {
            OtherRequirementType OtherRequirementType = (OtherRequirementType)BitConverter.ToInt32(data, offset);
            offset += 4;

            switch (OtherRequirementType)
            {
                case OtherRequirementType.Or:
                    return new PgOrQuestRequirement(data, offset);

                case OtherRequirementType.QuestCompleted:
                    return new PgQuestCompletedQuestRequirement(data, offset);

                case OtherRequirementType.GuildQuestCompleted:
                    return new PgGuildQuestCompletedQuestRequirement(data, offset);

                case OtherRequirementType.MinFavorLevel:
                    return new PgMinFavorLevelQuestRequirement(data, offset);

                case OtherRequirementType.MinSkillLevel:
                    return new PgMinSkillLevelQuestRequirement(data, offset);

                case OtherRequirementType.RuntimeBehaviorRuleSet:
                    return new PgRunTimeBehaviorRuleSetQuestRequirement(data, offset);

                case OtherRequirementType.HasEffectKeyword:
                    return new PgHasEffectKeywordQuestRequirement(data, offset);

                case OtherRequirementType.IsLongtimeAnimal:
                    return new PgIsLongTimeAnimalQuestRequirement(data, offset);

                case OtherRequirementType.InteractionFlagSet:
                    return new PgInteractionFlagSetQuestRequirement(data, offset);

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
