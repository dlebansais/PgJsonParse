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

        public static Dictionary<Type, IDictionary> Tables = new Dictionary<Type, IDictionary>()
        {
            {  typeof(AbilityItemKeyword), AbilityItemKeywordTable },
        };
    }
}
