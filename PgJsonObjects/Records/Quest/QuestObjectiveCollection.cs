using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestObjectiveCollection : List<QuestObjective>, ISerializableJsonObjectCollection
    {
        public ISerializableJsonObject GetAt(int index)
        {
            return this[index] as ISerializableJsonObject;
        }

        public static IGenericPgObject CreateItem(byte[] data, int offset)
        {
            QuestObjectiveType QuestObjectiveType = (QuestObjectiveType)BitConverter.ToInt32(data, offset);
            offset += 4;

            switch (QuestObjectiveType)
            {
                case QuestObjectiveType.Kill:
                case QuestObjectiveType.KillElite:
                    return new PgQuestObjectiveKill(data, offset);

                case QuestObjectiveType.TipPlayer:
                    return new PgQuestObjectiveTipPlayer(data, offset);

                case QuestObjectiveType.Special:
                    return new PgQuestObjectiveSpecial(data, offset);

                case QuestObjectiveType.MultipleInteractionFlags:
                    return new PgQuestObjectiveMultipleInteractionFlags(data, offset);

                case QuestObjectiveType.Deliver:
                    return new PgQuestObjectiveDeliver(data, offset);

                case QuestObjectiveType.Collect:
                case QuestObjectiveType.Have:
                case QuestObjectiveType.Harvest:
                case QuestObjectiveType.UseItem:
                    return new PgQuestObjectiveItem(data, offset);

                case QuestObjectiveType.GuildGiveItem:
                    return new PgQuestObjectiveGuildGiveItem(data, offset);

                case QuestObjectiveType.InteractionFlag:
                    return new PgQuestObjectiveInteractionFlag(data, offset);

                case QuestObjectiveType.GiveGift:
                    return new PgQuestObjectiveGiveGift(data, offset);

                case QuestObjectiveType.UseRecipe:
                    return new PgQuestObjectiveUseRecipe(data, offset);

                case QuestObjectiveType.BeAttacked:
                case QuestObjectiveType.Bury:
                    return new PgQuestObjectiveAnatomy(data, offset);

                case QuestObjectiveType.UseAbility:
                    return new PgQuestObjectiveUseAbility(data, offset);

                case QuestObjectiveType.Loot:
                    return new PgQuestObjectiveLoot(data, offset);

                case QuestObjectiveType.Scripted:
                case QuestObjectiveType.SayInChat:
                case QuestObjectiveType.UniqueSpecial:
                case QuestObjectiveType.GuildKill:
                case QuestObjectiveType.DruidKill:
                case QuestObjectiveType.DruidScripted:
                    return new PgQuestObjectiveSimple(data, offset);

                case QuestObjectiveType.ScriptedReceiveItem:
                    return new PgQuestObjectiveScriptedReceiveItem(data, offset);

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
