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
            return CreateNew(data, ref offset);
        }

        public static PgItemSimpleEffect CreateNew(byte[] data, ref int offset)
        {
            return new PgItemSimpleEffect(data, ref offset);
        }

        public override string Key { get { return null; } }
        public string Description { get { return GetString(4); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(8, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser>(); } }

        public override string AsEffectString()
        {
            return Description;
        }

        public override string SortingName { get { return Description; } }

        public override IList<IBackLinkable> GetLinkBack()
        {
            return null;
        }
    }
}
