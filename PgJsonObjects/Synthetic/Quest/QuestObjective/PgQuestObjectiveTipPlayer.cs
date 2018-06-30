using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestObjectiveTipPlayer : GenericPgObject<PgQuestObjectiveTipPlayer>, IPgQuestObjectiveTipPlayer
    {
        public PgQuestObjectiveTipPlayer(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveTipPlayer CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestObjectiveTipPlayer CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestObjectiveTipPlayer(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public int MinAmount { get { return RawMinAmount.HasValue ? RawMinAmount.Value : 0; } }
        public int? RawMinAmount { get { return GetInt(4); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
        protected override List<string> FieldTableOrder { get { return FieldTableOrder; } }
    }
}
