using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgPowerSimpleEffect : GenericPgObject<PgPowerSimpleEffect>, IPgPowerSimpleEffect
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

        public string Description { get { return GetString(0); } }
        public List<int> IconIdList { get { return GetIntList(4, ref _IconIdList); } } private List<int> _IconIdList;
    }
}
