namespace PgObjects
{
    using System.Collections.Generic;

    public class PgAttribute : PgObject
    {
        public static PgAttribute COCKATRICEDEBUFF_COST_DELTA { get; } = new PgAttribute() { Key = "COCKATRICEDEBUFF_COST_DELTA" };
        public static PgAttribute LAMIADEBUFF_COST_DELTA { get; } = new PgAttribute() { Key = "LAMIADEBUFF_COST_DELTA" };
        public static PgAttribute MONSTER_MATCH_OWNER_SPEED { get; } = new PgAttribute() { Key = "MONSTER_MATCH_OWNER_SPEED" };
        public static PgAttribute ARMOR_MITIGATION_RATIO { get; } = new PgAttribute() { Key = "ARMOR_MITIGATION_RATIO" };
        public static PgAttribute SHOW_CLEANLINESS_INDICATORS { get; } = new PgAttribute() { Key = "SHOW_CLEANLINESS_INDICATORS" };
        public static PgAttribute SHOW_COMMUNITY_INDICATORS { get; } = new PgAttribute() { Key = "SHOW_COMMUNITY_INDICATORS" };
        public static PgAttribute SHOW_PEACEABLENESS_INDICATORS { get; } = new PgAttribute() { Key = "SHOW_PEACEABLENESS_INDICATORS" };
        public static PgAttribute SHOW_FAIRYENERGY_INDICATORS { get; } = new PgAttribute() { Key = "SHOW_FAIRYENERGY_INDICATORS" };
        public static PgAttribute MONSTER_COMBAT_XP_VALUE { get; } = new PgAttribute() { Key = "MONSTER_COMBAT_XP_VALUE" };

        public string Key { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public List<int> IconIdList { get; set; } = new List<int>();
        public string Tooltip { get; set; } = string.Empty;
        public DisplayType DisplayType { get; set; }
        public bool IsHidden { get { return RawIsHidden.HasValue && RawIsHidden.Value; } }
        public bool? RawIsHidden { get; set; }
        public DisplayRule DisplayRule { get; set; }
        public float DefaultValue { get { return RawDefaultValue.HasValue ? RawDefaultValue.Value : 0; } }
        public float? RawDefaultValue { get; set; }

        public int IconId { get; set; }
        public string ValidLabel { get; set; }

        public override int ObjectIconId { get { return IconId; } }
        public override string ObjectName { get { return ValidLabel; } }
        public override string ToString() { return ValidLabel; }
    }
}
