using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgMiscSource : PgGenericSource<PgMiscSource>, IPgMiscSource
    {
        public PgMiscSource(byte[] data, ref int offset)
            : base(data, offset)
        {
        }
        
        protected override PgMiscSource CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgMiscSource CreateNew(byte[] data, ref int offset)
        {
            return new PgMiscSource(data, ref offset);
        }

        public override void Init(IGenericPgObject Parent)
        {
        }

        public override string Key { get { return null; } }
        protected override List<string> FieldTableOrder { get { return null; } }
        protected override Dictionary<string, FieldParser> FieldTable { get { return null; } }
        public override string SortingName { get { return null; } }
    }
}
