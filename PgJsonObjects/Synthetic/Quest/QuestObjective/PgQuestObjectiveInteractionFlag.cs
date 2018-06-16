﻿namespace PgJsonObjects
{
    public class PgQuestObjectiveInteractionFlag : GenericPgObject, IPgQuestObjectiveInteractionFlag
    {
        public PgQuestObjectiveInteractionFlag(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public string InteractionFlag { get { return GetString(0); } }
        public string InteractionTarget { get { return GetString(4); } }
    }
}