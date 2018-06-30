using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestObjectiveKill : GenericPgObject<PgQuestObjectiveKill>, IPgQuestObjectiveKill
    {
        public PgQuestObjectiveKill(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveKill CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestObjectiveKill CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestObjectiveKill(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public string AbilityKeyword { get { return GetString(4); } }
        public QuestObjectiveKillTarget Target { get { return GetEnum<QuestObjectiveKillTarget>(8); } }
        public EffectKeyword EffectRequirement { get { return GetEnum<EffectKeyword>(10); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
        protected override List<string> FieldTableOrder { get { return FieldTableOrder; } }
    }
}
