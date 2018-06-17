namespace PgJsonObjects
{
    public class PgQuestObjectiveTipPlayer : GenericPgObject<PgQuestObjectiveTipPlayer>, IPgQuestObjectiveTipPlayer
    {
        public PgQuestObjectiveTipPlayer(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveTipPlayer CreateItem(byte[] data, int offset)
        {
            return new PgQuestObjectiveTipPlayer(data, offset);
        }

        public int MinAmount { get { return RawMinAmount.HasValue ? RawMinAmount.Value : 0; } }
        public int? RawMinAmount { get { return GetInt(0); } }
    }
}
