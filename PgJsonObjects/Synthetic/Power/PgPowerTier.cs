using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgPowerTier : GenericPgObject, IPgPowerTier
    {
        public PgPowerTier(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public List<PowerEffect> EffectList { get { return GetObjectList(0, ref _EffectList); } } private List<PowerEffect> _EffectList;
    }
}
