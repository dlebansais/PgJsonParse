﻿namespace PgObjects
{
    public class EffectVerificationEntry
    {
        public required string Prefix { get; init; }
        public required string Suffix { get; init; }
        public bool AllowRecurrence { get; init; }
        public bool TargetElite { get; init; }
        public bool AllowDuration { get; init; }
    }
}
