using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestRewardCurrency : GenericPgObject<PgQuestRewardCurrency>, IPgQuestRewardCurrency
    {
        public PgQuestRewardCurrency(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestRewardCurrency CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestRewardCurrency CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestRewardCurrency(data, ref offset);
        }

        public override string Key { get { return null; } }
        public int Amount { get { return RawAmount.HasValue ? RawAmount.Value : 0; } }
        public int? RawAmount { get { return GetInt(0); } }
        public Currency Currency { get { return GetEnum<Currency>(4); } }

        protected override List<string> FieldTableOrder { get { return GetStringList(8, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser>(); } }

        public override string SortingName { get { return null; } }
    }
}
