namespace PgJsonObjects
{
    public class PgQuestObjectiveGiveGift : GenericPgObject<PgQuestObjectiveGiveGift>, IPgQuestObjectiveGiveGift
    {
        public PgQuestObjectiveGiveGift(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveGiveGift CreateItem(byte[] data, int offset)
        {
            return new PgQuestObjectiveGiveGift(data, offset);
        }

        public float MinFavorReceived { get { return RawMinFavorReceived.HasValue ? RawMinFavorReceived.Value : 0; } }
        public float? RawMinFavorReceived { get { return (float)GetDouble(0); } }
        public float MaxFavorReceived { get { return RawMaxFavorReceived.HasValue ? RawMaxFavorReceived.Value : 0; } }
        public float? RawMaxFavorReceived { get { return (float)GetDouble(4); } }
    }
}
