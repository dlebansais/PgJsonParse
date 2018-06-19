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

        public string AbilityKeyword { get { return GetString(0); } }
        public QuestObjectiveKillTarget Target { get { return GetEnum<QuestObjectiveKillTarget>(4); } }
        public EffectKeyword EffectRequirement { get { return GetEnum<EffectKeyword>(6); } }
    }
}
