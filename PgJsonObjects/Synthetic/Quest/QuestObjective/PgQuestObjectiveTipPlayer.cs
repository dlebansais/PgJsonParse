namespace PgJsonObjects
{
    public class PgQuestObjectiveTipPlayer : GenericPgObject, IPgQuestObjectiveTipPlayer
    {
        public PgQuestObjectiveTipPlayer(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public int MinAmount { get { return RawMinAmount.HasValue ? RawMinAmount.Value : 0; } }
        public int? RawMinAmount { get { return GetInt(0); } }
    }
}
