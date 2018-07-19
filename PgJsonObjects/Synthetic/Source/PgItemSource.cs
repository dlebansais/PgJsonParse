using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgItemSource : PgGenericSource<PgItemSource>, IPgItemSource
    {
        public PgItemSource(byte[] data, ref int offset)
            : base(data, offset)
        {
        }
        
        protected override PgItemSource CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgItemSource CreateNew(byte[] data, ref int offset)
        {
            return new PgItemSource(data, ref offset);
        }

        public override void Init(IGenericPgObject Parent)
        {
            Parent.AddLinkBack(Item);
        }

        public IPgItem Item { get { return GetObject(PropertiesOffset + 0, ref _Item, PgItem.CreateNew); } } private IPgItem _Item;

        public override string Key { get { return null; } }
        protected override List<string> FieldTableOrder { get { return null; } }
        protected override Dictionary<string, FieldParser> FieldTable { get { return null; } }
        public override string SortingName { get { return null; } }
    }
}
