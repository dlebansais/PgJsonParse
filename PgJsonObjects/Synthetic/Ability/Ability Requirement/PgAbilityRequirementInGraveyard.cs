﻿using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilityRequirementInGraveyard: GenericPgObject<PgAbilityRequirementInGraveyard>, IPgAbilityRequirementInGraveyard
    {
        public PgAbilityRequirementInGraveyard(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementInGraveyard CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityRequirementInGraveyard CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityRequirementInGraveyard(data, ref offset);
        }

        public override string Key { get { return GetString(4); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
        protected override List<string> FieldTableOrder { get { return FieldTableOrder; } }
    }
}
