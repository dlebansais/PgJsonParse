﻿namespace PgJsonObjects
{
    public class PgQuestObjectiveTipPlayer : GenericPgObject<PgQuestObjectiveTipPlayer>, IPgQuestObjectiveTipPlayer
    {
        public PgQuestObjectiveTipPlayer(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgQuestObjectiveTipPlayer CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgQuestObjectiveTipPlayer CreateNew(byte[] data, ref int offset)
        {
            return new PgQuestObjectiveTipPlayer(data, ref offset);
        }

        public int MinAmount { get { return RawMinAmount.HasValue ? RawMinAmount.Value : 0; } }
        public int? RawMinAmount { get { return GetInt(0); } }
    }
}
