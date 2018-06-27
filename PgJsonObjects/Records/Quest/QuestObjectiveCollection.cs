using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestObjectiveCollection : List<IGenericPgObject>, ISerializableJsonObjectCollection
    {
        /*public ISerializableJsonObject GetAt(int index)
        {
            return this[index] as ISerializableJsonObject;
        }*/

        public static IGenericPgObject CreateItem(byte[] data, ref int offset)
        {
            QuestObjectiveType QuestObjectiveType = (QuestObjectiveType)BitConverter.ToInt32(data, offset);
            offset += 4;

            switch (QuestObjectiveType)
            {
                case QuestObjectiveType.Kill:
                case QuestObjectiveType.KillElite:
                    return new PgQuestObjectiveKill(data, ref offset);

                case QuestObjectiveType.TipPlayer:
                    return new PgQuestObjectiveTipPlayer(data, ref offset);

                case QuestObjectiveType.Special:
                    return new PgQuestObjectiveSpecial(data, ref offset);

                case QuestObjectiveType.MultipleInteractionFlags:
                    return new PgQuestObjectiveMultipleInteractionFlags(data, ref offset);

                case QuestObjectiveType.Deliver:
                    return new PgQuestObjectiveDeliver(data, ref offset);

                case QuestObjectiveType.Collect:
                case QuestObjectiveType.Have:
                case QuestObjectiveType.Harvest:
                case QuestObjectiveType.UseItem:
                    return new PgQuestObjectiveItem(data, ref offset);

                case QuestObjectiveType.GuildGiveItem:
                    return new PgQuestObjectiveGuildGiveItem(data, ref offset);

                case QuestObjectiveType.InteractionFlag:
                    return new PgQuestObjectiveInteractionFlag(data, ref offset);

                case QuestObjectiveType.GiveGift:
                    return new PgQuestObjectiveGiveGift(data, ref offset);

                case QuestObjectiveType.UseRecipe:
                    return new PgQuestObjectiveUseRecipe(data, ref offset);

                case QuestObjectiveType.BeAttacked:
                case QuestObjectiveType.Bury:
                    return new PgQuestObjectiveAnatomy(data, ref offset);

                case QuestObjectiveType.UseAbility:
                    return new PgQuestObjectiveUseAbility(data, ref offset);

                case QuestObjectiveType.Loot:
                    return new PgQuestObjectiveLoot(data, ref offset);

                case QuestObjectiveType.Scripted:
                case QuestObjectiveType.SayInChat:
                case QuestObjectiveType.UniqueSpecial:
                case QuestObjectiveType.GuildKill:
                case QuestObjectiveType.DruidKill:
                case QuestObjectiveType.DruidScripted:
                    return new PgQuestObjectiveSimple(data, ref offset);

                case QuestObjectiveType.ScriptedReceiveItem:
                    return new PgQuestObjectiveScriptedReceiveItem(data, ref offset);

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
