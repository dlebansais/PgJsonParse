﻿using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilityRequirementIsVegetarian: GenericPgObject<PgAbilityRequirementIsVegetarian>, IPgAbilityRequirementIsVegetarian
    {
        public PgAbilityRequirementIsVegetarian(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementIsVegetarian CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityRequirementIsVegetarian CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityRequirementIsVegetarian(data, ref offset);
        }

        public override string Key { get { return GetString(4); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
        protected override List<string> FieldTableOrder { get { return FieldTableOrder; } }
    }
}
