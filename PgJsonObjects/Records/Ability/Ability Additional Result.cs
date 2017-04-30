using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AbilityAdditionalResult
    {
        public AbilityAdditionalResult(AbilityEffect Effect, TimeSpan Duration)
        {
            this.Effect = Effect;
            this.Duration = Duration;
            Parameters = new List<AbilityEffectParameters>();
        }

        public AbilityEffect Effect { get; set; }
        public AbilityEffectTarget Target { get; set; }
        public double AoERange { get; set; }
        public TimeSpan Duration { get; set; }
        public List<AbilityEffectParameters> Parameters { get; private set; }
        public AbilityEffectConditional Conditional { get; set; }
        public double ConditionalValue { get; set; }
        public TimeSpan RandomDuration { get; set; }
        public AbilityAdditionalResult Reset { get; set; }
    }
}
