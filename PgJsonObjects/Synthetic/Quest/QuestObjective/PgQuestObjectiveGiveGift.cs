using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestObjectiveGiveGift : GenericPgObject<PgQuestObjectiveGiveGift>, IPgQuestObjectiveGiveGift
    {
        public PgQuestObjectiveGiveGift(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveGiveGift CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestObjectiveGiveGift CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestObjectiveGiveGift(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public float MinFavorReceived { get { return RawMinFavorReceived.HasValue ? RawMinFavorReceived.Value : 0; } }
        public float? RawMinFavorReceived { get { return (float)GetDouble(4); } }
        public float MaxFavorReceived { get { return RawMaxFavorReceived.HasValue ? RawMaxFavorReceived.Value : 0; } }
        public float? RawMaxFavorReceived { get { return (float)GetDouble(8); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(12, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
    }
}
