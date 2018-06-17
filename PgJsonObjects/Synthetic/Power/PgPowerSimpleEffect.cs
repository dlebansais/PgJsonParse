using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgPowerSimpleEffect : GenericPgObject, IPgPowerSimpleEffect
    {
        public PgPowerSimpleEffect(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public override IGenericPgObject CreateItem(byte[] data, int offset)
        {
            return new PgPowerSimpleEffect(data, offset);
        }

        public string Description { get { return GetString(0); } }
        public List<int> IconIdList { get { return GetIntList(4, ref _IconIdList); } } private List<int> _IconIdList;
    }
}
