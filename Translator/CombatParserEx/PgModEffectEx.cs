namespace PgObjects
{
    using System.Collections.Generic;

    public class PgModEffectEx
    {
        public string Key { get; set; } = string.Empty;
        public string EffectKey { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<AbilityKeyword> AbilityList { get; set; } = new List<AbilityKeyword>();
        public PgCombatEffectCollectionEx StaticCombatEffectList { get; set; } = new PgCombatEffectCollectionEx();
        public PgCombatEffectCollectionEx DynamicCombatEffectList { get; set; } = new PgCombatEffectCollectionEx();
        public List<AbilityKeyword> TargetAbilityList { get; set; } = new List<AbilityKeyword>();
        public PgModEffectEx? SecondaryModEffect { get; set; }

        public override string ToString()
        {
            string StaticResult = string.Empty;
            string DynamicResult = string.Empty;

            foreach (PgCombatEffectEx Item in StaticCombatEffectList)
            {
                if (StaticResult.Length > 0)
                    StaticResult += "; ";

                StaticResult += Item.ToString();
            }

            if (StaticResult.Length > 0)
                StaticResult = $"Static: {StaticResult}";

            foreach (PgCombatEffectEx Item in DynamicCombatEffectList)
            {
                if (DynamicResult.Length > 0)
                    DynamicResult += "; ";

                DynamicResult += Item.ToString();
            }

            if (DynamicResult.Length > 0)
            {
                if (StaticResult.Length > 0)
                    DynamicResult = $", Dynamic: {DynamicResult}";
                else
                    DynamicResult = $"Dynamic: {DynamicResult}";
            }

            string CombatResult = StaticResult + DynamicResult;

            string AffectedString = AbilityKeywordListToShortString(AbilityList);
            string TargetString = AbilityKeywordListToShortString(TargetAbilityList);

            return $"{CombatResult}, Affected: {AffectedString}, Target: {TargetString}";
        }

        public static string AbilityKeywordListToShortString(List<AbilityKeyword> list)
        {
            string Result = string.Empty;

            foreach (AbilityKeyword Keyword in list)
            {
                if (Result.Length > 0)
                    Result += ", ";

                Result += Keyword.ToString();
            }

            return Result;
        }
    }
}
