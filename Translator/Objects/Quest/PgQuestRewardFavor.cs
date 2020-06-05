﻿namespace PgJsonObjects
{
    public class PgQuestRewardFavor
    {
        public int Favor { get { return RawFavor.HasValue ? RawFavor.Value : 0; } }
        public int? RawFavor { get; set; }
        public string RawNpcName { get; set; } = string.Empty;
    }
}