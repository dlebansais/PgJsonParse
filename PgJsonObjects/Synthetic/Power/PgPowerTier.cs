﻿namespace PgJsonObjects
{
    public class PgPowerTier : GenericPgObject, IPgPowerTier
    {
        public PgPowerTier(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public PowerEffectCollection EffectList { get { return GetObjectList(0, ref _EffectList, PowerEffectCollection.CreateItem, () => new PowerEffectCollection()); } } private PowerEffectCollection _EffectList;
    }
}
