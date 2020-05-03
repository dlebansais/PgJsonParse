namespace PgBuilder
{
    using PgJsonObjects;
    using System.Collections.Generic;

    public class ModEffect
    {
        public ModEffect(string effectKey, List<AbilityKeyword> abilityList, List<CombatEffect> staticCombatEffectList, List<CombatEffect> dynamicCombatEffectList, List<AbilityKeyword> targetAbilityList)
        {
            EffectKey = effectKey;
            AbilityList = abilityList;
            StaticCombatEffectList = staticCombatEffectList;
            DynamicCombatEffectList = dynamicCombatEffectList;
            TargetAbilityList = targetAbilityList;
        }

        public string EffectKey { get; }
        public List<AbilityKeyword> AbilityList { get; }
        public List<CombatEffect> StaticCombatEffectList { get; }
        public List<CombatEffect> DynamicCombatEffectList { get; }
        public List<AbilityKeyword> TargetAbilityList { get; }

        public static bool IsSameAbilityKeywordList(List<AbilityKeyword> list1, List<AbilityKeyword> list2)
        {
            if (list1.Count != list2.Count)
                return false;

            for (int i = 0; i < list1.Count; i++)
                if (list1[i] != list2[i])
                    return false;

            return true;
        }

        public static bool IsEqualStrict(ModEffect modEffect1, ModEffect modEffect2)
        {
            if (!IsSameAbilityKeywordList(modEffect1.AbilityList, modEffect2.AbilityList))
                return false;

            if (!CombatEffect.IsEqualStrict(modEffect1.StaticCombatEffectList, modEffect2.StaticCombatEffectList))
                return false;

            if (!CombatEffect.IsEqualStrict(modEffect1.DynamicCombatEffectList, modEffect2.DynamicCombatEffectList))
                return false;

            if (!IsSameAbilityKeywordList(modEffect1.TargetAbilityList, modEffect2.TargetAbilityList))
                return false;

            return true;
        }
    }
}
