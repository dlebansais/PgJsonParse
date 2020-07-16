namespace PgObjects
{
    public class PgCombatEffect
    {
        public CombatKeyword Keyword { get; set; }
        public PgNumericValue Data { get; set; }
        public GameDamageType DamageType { get; set; }
        public GameCombatSkill CombatSkill { get; set; }

        public override string ToString()
        {
            string DataString = Data.RawValue.HasValue ? $": {Data}" : string.Empty;

            string DamageTypeString = string.Empty;

            if (DamageType != GameDamageType.Internal_None)
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

            string CombatSkillString = CombatSkill == GameCombatSkill.Internal_None ? string.Empty : $" ({CombatSkill})";

            return $"{Keyword}{DataString}{DamageTypeString}{CombatSkillString}";
        }
    }
}
