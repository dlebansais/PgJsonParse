namespace PgJsonObjects
{
    public class PgQuestObjectiveSpecial : GenericPgObject, IPgQuestObjectiveSpecial
    {
        public PgQuestObjectiveSpecial(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public override IGenericPgObject CreateItem(byte[] data, int offset)
        {
            return new PgQuestObjectiveSpecial(data, offset);
        }

        public int MinAmount { get { return RawMinAmount.HasValue ? RawMinAmount.Value : 0; } }
        public int? RawMinAmount { get { return GetInt(0); } }
        public int MaxAmount { get { return RawMaxAmount.HasValue ? RawMaxAmount.Value : 0; } }
        public int? RawMaxAmount { get { return GetInt(4); } }
        public string StringParam { get { return GetString(8); } }
        public string InteractionTarget { get { return GetString(12); } }
    }
}
