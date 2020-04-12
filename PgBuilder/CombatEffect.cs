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
            Data1 = double.NaN;
            Data2 = double.NaN;
            DamageType = GameDamageType.None;
        }

        public CombatEffect(CombatKeyword keyword)
        {
            Debug.Assert(keyword != CombatKeyword.None);

            Keyword = keyword;
            Data1 = double.NaN;
            Data2 = double.NaN;
            DamageType = GameDamageType.None;
        }

        public CombatEffect(CombatKeyword keyword, double data)
        {
            Debug.Assert(keyword != CombatKeyword.None);
            Debug.Assert(!double.IsNaN(data));

            Keyword = keyword;
            Data1 = data;
            Data2 = double.NaN;
            DamageType = GameDamageType.None;
        }

        public CombatEffect(CombatKeyword keyword, double data1, double data2, GameDamageType damageType)
        {
            Debug.Assert(keyword != CombatKeyword.None);
            Debug.Assert((double.IsNaN(data1) && double.IsNaN(data2)) || !double.IsNaN(data1));

            Keyword = keyword;
            Data1 = data1;
            Data2 = data2;
            DamageType = damageType;
        }
        #endregion

        #region Properties
        public CombatKeyword Keyword { get; }
        public double Data { get { return Data1; } }
        public double Data1 { get; }
        public double Data2 { get; }
        public GameDamageType DamageType { get; }
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
                if (Keyword == Other.Keyword)
                    if (double.IsNaN(Data1) == double.IsNaN(Other.Data1) && double.IsNaN(Data2) == double.IsNaN(Other.Data2))
                        return true;

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
            if (double.IsNaN(Data1))
                return $"{Keyword}";
            else
            {
                string Data1String = Data1.ToString(CultureInfo.InvariantCulture);
                string Data2String = double.IsNaN(Data2) ? string.Empty : $", {Data2.ToString(CultureInfo.InvariantCulture)}";
                string DamageTypeString = DamageType == GameDamageType.None ? string.Empty : $" ({DamageType})";

                return $"{Keyword}: {Data1String}{Data2String}{DamageTypeString}";
            }
        }
        #endregion
    }
}
