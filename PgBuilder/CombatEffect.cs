namespace PgBuilder
{
    using System.Collections.Generic;
    using System.Diagnostics;

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
        public static bool Contains(List<CombatEffect> list1, List<CombatEffect> list2, out List<CombatEffect> difference, out List<CombatEffect> union)
        {
            difference = new List<CombatEffect>();
            union = new List<CombatEffect>();

            List<CombatEffect> MatchList = new List<CombatEffect>();

            foreach (CombatEffect Item2 in list2)
            {
                bool IsContained = false;
                foreach (CombatEffect Item1 in list1)
                    if (Item1.Equals(Item2))
                    {
                        IsContained = true;
                        MatchList.Add(Item1);
                        break;
                    }

                if (!IsContained)
                    return false;
            }

            foreach (CombatEffect Item in list1)
                if (!MatchList.Contains(Item))
                    difference.Add(Item);
                else
                    break;

            foreach (CombatEffect Item in list1)
                if (!difference.Contains(Item))
                    union.Add(Item);

            return true;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is CombatEffect Other))
                return false;

            if (Keyword != Other.Keyword && Keyword != CombatKeyword.Again && Other.Keyword != CombatKeyword.Again)
            {
                if ((Keyword != CombatKeyword.AddVulnerability || Other.Keyword != CombatKeyword.AddDirectVulnerability) && (Keyword != CombatKeyword.AddDirectVulnerability || Other.Keyword != CombatKeyword.AddVulnerability))
                    return false;
            }

            if (Data1.IsValueSet != Other.Data1.IsValueSet || Data2.IsValueSet != Other.Data2.IsValueSet)
                return false;

            if (Data1.IsValueSet && Data1.Value != Other.Data1.Value)
                return false;

            if (Data2.IsValueSet && Data2.Value != Other.Data2.Value)
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool IsEqualStrict(List<CombatEffect> list1, List<CombatEffect> list2)
        {
            if (list1.Count != list2.Count)
                return false;

            for (int i = 0; i < list1.Count; i++)
                if (!IsEqualStrict(list1[i], list2[i]))
                    return false;

            return true;
        }

        public static bool IsEqualStrict(CombatEffect combatEffect1, CombatEffect combatEffect2)
        {
            if (combatEffect1.Keyword != combatEffect2.Keyword)
                return false;

            if (!NumericValue.IsEqualStrict(combatEffect1.Data1, combatEffect2.Data1))
                return false;

            if (!NumericValue.IsEqualStrict(combatEffect1.Data2, combatEffect2.Data2))
                return false;

            if (combatEffect1.DamageType != combatEffect2.DamageType)
                return false;

            if (combatEffect1.CombatSkill != combatEffect2.CombatSkill)
                return false;

            return true;
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
