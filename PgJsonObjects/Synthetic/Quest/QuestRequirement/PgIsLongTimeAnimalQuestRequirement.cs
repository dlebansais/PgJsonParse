using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgIsLongTimeAnimalQuestRequirement : GenericPgObject<PgIsLongTimeAnimalQuestRequirement>, IPgIsLongTimeAnimalQuestRequirement
    {
        public PgIsLongTimeAnimalQuestRequirement(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgIsLongTimeAnimalQuestRequirement CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgIsLongTimeAnimalQuestRequirement CreateNew(byte[] data, ref int offset)
        {
            return new PgIsLongTimeAnimalQuestRequirement(data, ref offset);
        }

        public override string Key { get { return GetString(4); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(8, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
    }
}
