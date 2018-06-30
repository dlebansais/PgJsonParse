﻿using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgPotionServerInfoEffect : GenericPgObject<PgPotionServerInfoEffect>, IPgServerInfoEffect, IPgPotionServerInfoEffect
    {
        public PgPotionServerInfoEffect(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgPotionServerInfoEffect CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgPotionServerInfoEffect CreateNew(byte[] data, ref int offset)
        {
            return new PgPotionServerInfoEffect(data, ref offset);
        }

        public override string Key { get { return null; } }
        public string EffectString { get { return GetString(4); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
        protected override List<string> FieldTableOrder { get { return FieldTableOrder; } }
    }
}
