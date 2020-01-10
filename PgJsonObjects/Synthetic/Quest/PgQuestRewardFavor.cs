using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestRewardFavor : GenericPgObject<PgQuestRewardFavor>, IPgQuestRewardFavor
    {
        public PgQuestRewardFavor(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestRewardFavor CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestRewardFavor CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestRewardFavor(data, ref offset);
        }

        public override string Key { get { return null; } }
        public string RawNpcName { get { return GetString(0); } }
        public int Favor { get { return RawFavor.HasValue ? RawFavor.Value : 0; } }
        public int? RawFavor { get { return GetInt(4); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(8, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser>(); } }

        public override string SortingName { get { return null; } }
    }
}
