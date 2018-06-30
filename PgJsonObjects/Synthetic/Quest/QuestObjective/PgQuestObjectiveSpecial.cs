﻿using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestObjectiveSpecial : GenericPgObject<PgQuestObjectiveSpecial>, IPgQuestObjectiveSpecial
    {
        public PgQuestObjectiveSpecial(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveSpecial CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestObjectiveSpecial CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestObjectiveSpecial(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public int MinAmount { get { return RawMinAmount.HasValue ? RawMinAmount.Value : 0; } }
        public int? RawMinAmount { get { return GetInt(4); } }
        public int MaxAmount { get { return RawMaxAmount.HasValue ? RawMaxAmount.Value : 0; } }
        public int? RawMaxAmount { get { return GetInt(8); } }
        public string StringParam { get { return GetString(12); } }
        public string InteractionTarget { get { return GetString(16); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
        protected override List<string> FieldTableOrder { get { return FieldTableOrder; } }
    }
}
