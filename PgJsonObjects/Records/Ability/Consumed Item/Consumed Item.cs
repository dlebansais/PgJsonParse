﻿namespace PgJsonObjects
{
    public abstract class ConsumedItem
    {
        public ConsumedItem(int? RawCount, double? RawConsumedChance, double? RawChanceToStickInCorpse)
        {
            this.RawCount = (RawCount.HasValue && RawCount.Value > 1 ? RawCount : null);
            this.RawConsumedChance = RawConsumedChance;
            this.RawChanceToStickInCorpse = RawChanceToStickInCorpse;
        }

        public int? RawCount { get; private set; }
        public double? RawConsumedChance { get; private set; }
        public double? RawChanceToStickInCorpse { get; private set; }
        public int Count { get { return RawCount.HasValue ? RawCount.Value : 0; } }
        public double ConsumedChance { get { return RawConsumedChance.HasValue ? RawConsumedChance.Value : 0; } }
        public double ChanceToStickInCorpse { get { return RawChanceToStickInCorpse.HasValue ? RawChanceToStickInCorpse.Value : 0; } }

        public abstract string Name { get; }

        public virtual string TextContent
        {
            get
            {
                string Result = Name;

                if (RawConsumedChance.HasValue)
                    Result += ", Can Be Consumed";

                if (RawConsumedChance.HasValue)
                    Result += ", Can Stick Corpse";

                return Result;
            }
        }
    }
}