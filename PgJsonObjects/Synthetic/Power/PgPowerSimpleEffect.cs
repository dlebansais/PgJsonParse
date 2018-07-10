using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgPowerSimpleEffect : PgPowerEffect<PgPowerSimpleEffect>, IPgPowerSimpleEffect
    {
        public PgPowerSimpleEffect(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgPowerSimpleEffect CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgPowerSimpleEffect CreateNew(byte[] data, ref int offset)
        {
            return new PgPowerSimpleEffect(data, ref offset);
        }

        public override string Key { get { return GetString(4); } }
        public string Description { get { return GetString(8); } }
        public List<int> IconIdList { get { return GetIntList(12, ref _IconIdList); } } private List<int> _IconIdList;
        protected override List<string> FieldTableOrder { get { return GetStringList(16, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser>(); } }
    }
}
