using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgItemSimpleEffect : PgItemEffect<PgItemSimpleEffect>, IPgItemSimpleEffect
    {
        public PgItemSimpleEffect(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgItemSimpleEffect CreateItem(byte[] data, ref int offset)
        {
            return new PgItemSimpleEffect(data, ref offset);
        }

        public override string Key { get { return null; } }
        public string Description { get { return GetString(4); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
        protected override List<string> FieldTableOrder { get { return FieldTableOrder; } }
    }
}
