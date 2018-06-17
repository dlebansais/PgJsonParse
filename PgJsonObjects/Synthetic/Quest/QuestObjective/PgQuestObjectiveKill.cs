namespace PgJsonObjects
{
    public class PgQuestObjectiveKill : GenericPgObject<PgQuestObjectiveKill>, IPgQuestObjectiveKill
    {
        public PgQuestObjectiveKill(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveKill CreateItem(byte[] data, int offset)
        {
            return new PgQuestObjectiveKill(data, offset);
        }

        public string AbilityKeyword { get { return GetString(0); } }
        public QuestObjectiveKillTarget Target { get { return GetEnum<QuestObjectiveKillTarget>(4); } }
        public EffectKeyword EffectRequirement { get { return GetEnum<EffectKeyword>(6); } }
    }
}
