namespace Translator
{
    using PgJsonObjects;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class EnumStringMap
    {
        public static readonly Dictionary<AbilityItemKeyword, string> AbilityItemKeywordTable = new Dictionary<AbilityItemKeyword, string>()
        {
            { AbilityItemKeyword.form_Deer, "form:Deer" },
            { AbilityItemKeyword.form_PotbellyPig, "form:PotbellyPig" },
            { AbilityItemKeyword.form_Cow, "form:Cow" },
            { AbilityItemKeyword.form_Spider, "form:Spider" },
            { AbilityItemKeyword.form_GiantBat, "form:GiantBat" },
            { AbilityItemKeyword.form_Rabbit, "form:Rabbit" },
        };

        public static readonly Dictionary<EffectStackingType, string> StackingTypeTable = new Dictionary<EffectStackingType, string>()
        {
            { EffectStackingType.LamiasGaze, "Lamia's Gaze" },
            { EffectStackingType.One, "1" },
        };

        public static readonly Dictionary<EffectKeyword, string> EffectKeywordTable = new Dictionary<EffectKeyword, string>()
        {
            { EffectKeyword.Hyphen, "-" },
        };

        public static readonly Dictionary<EffectParticle, string> EffectParticleTable = new Dictionary<EffectParticle, string>()
        {
            { EffectParticle.OnFireGreen, "OnFire-Green"},
        };

        public static Dictionary<Type, IDictionary> Tables = new Dictionary<Type, IDictionary>()
        {
            {  typeof(AbilityItemKeyword), AbilityItemKeywordTable },
            {  typeof(EffectStackingType), StackingTypeTable },
            {  typeof(EffectKeyword), EffectKeywordTable },
            {  typeof(EffectParticle), EffectParticleTable },
        };
    }
}
