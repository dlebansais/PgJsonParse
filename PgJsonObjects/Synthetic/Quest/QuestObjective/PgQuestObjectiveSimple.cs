using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestObjectiveSimple : GenericPgObject<PgQuestObjectiveSimple>, IPgQuestObjectiveSimple
    {
        public PgQuestObjectiveSimple(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveSimple CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestObjectiveSimple CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestObjectiveSimple(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(4, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
    }
}
