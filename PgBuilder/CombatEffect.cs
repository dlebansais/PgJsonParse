namespace PgBuilder
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;

    public class CombatEffect
    {
        #region Init
        public CombatEffect()
        {
            Keyword = CombatKeyword.None;
            Data1 = new NumericValue();
            Data2 = new NumericValue();
            DamageType = GameDamageType.None;
            CombatSkill = GameCombatSkill.None;
        }

        public CombatEffect(CombatKeyword keyword)
        {
            Debug.Assert(keyword != CombatKeyword.None);

            Keyword = keyword;
            Data1 = new NumericValue();
            Data2 = new NumericValue();
            DamageType = GameDamageType.None;
            CombatSkill = GameCombatSkill.None;
        }

        public CombatEffect(CombatKeyword keyword, NumericValue data)
        {
            Debug.Assert(keyword != CombatKeyword.None);
            Debug.Assert(data.IsValueSet);

            Keyword = keyword;
            Data1 = data;
            Data2 = new NumericValue();
            DamageType = GameDamageType.None;
            CombatSkill = GameCombatSkill.None;
        }

        public CombatEffect(CombatKeyword keyword, NumericValue data1, NumericValue data2, GameDamageType damageType, GameCombatSkill combatSkill)
        {
            Debug.Assert(keyword != CombatKeyword.None);
            Debug.Assert((!data1.IsValueSet && !data2.IsValueSet) || data1.IsValueSet);

            Keyword = keyword;
            Data1 = data1;
            Data2 = data2;
            DamageType = damageType;
            CombatSkill = combatSkill;
        }
        #endregion

        #region Properties
        public CombatKeyword Keyword { get; }
        public NumericValue Data { get { return Data1; } }
        public NumericValue Data1 { get; }
        public NumericValue Data2 { get; }
        public GameDamageType DamageType { get; }
        public GameCombatSkill CombatSkill { get; }
        #endregion

        #region Client Interface
        public static bool Contains(List<CombatEffect> list1, List<CombatEffect> list2)
        {
            foreach (CombatEffect Item2 in list2)
            {
                bool IsContained = false;
                foreach (CombatEffect Item1 in list1)
                    if (Item1.Equals(Item2))
                    {
                        IsContained = true;
                        break;
                    }

                if (!IsContained)
                    return false;
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is CombatEffect Other)
                if (Keyword == Other.Keyword || (Keyword == CombatKeyword.AddVulnerability && Other.Keyword == CombatKeyword.AddDirectVulnerability) || (Keyword == CombatKeyword.AddDirectVulnerability && Other.Keyword == CombatKeyword.AddVulnerability))
                {
                    if (Data1.IsValueSet == Other.Data1.IsValueSet && Data2.IsValueSet == Other.Data2.IsValueSet)
                        return true;
                }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion

        #region Debugging
        public override string ToString()
        {
            string Data1String = Data1.IsValueSet ? $": {Data1}" : string.Empty;
            string Data2String = Data2.IsValueSet ? $", {Data2}" : string.Empty;

            string DamageTypeString = string.Empty;

            if (DamageType != GameDamageType.None)
            {
                for (int i = 0; i + 1 < sizeof(int) * 8; i++)
                {
                    GameDamageType Value = (GameDamageType)(1 << i);

                    if (DamageType.HasFlag(Value))
                    {
                        if (DamageTypeString.Length > 0)
                            DamageTypeString += ", ";

                        DamageTypeString += Value.ToString();
                    }
                }

                DamageTypeString = $" ({DamageTypeString})";
            }

            string CombatSkillString = CombatSkill == GameCombatSkill.None ? string.Empty : $" ({CombatSkill})";

            return $"{Keyword}{Data1String}{Data2String}{DamageTypeString}{CombatSkillString}";
        }
        #endregion
    }
}
