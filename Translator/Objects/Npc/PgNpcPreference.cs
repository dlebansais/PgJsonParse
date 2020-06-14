namespace PgObjects
{
    using System.Collections.Generic;

    public class PgNpcPreference
    {
        public List<ItemKeyword> ItemKeywordList { get; set; } = new List<ItemKeyword>();
        public int MinValueRequirement { get { return RawMinValueRequirement.HasValue ? RawMinValueRequirement.Value : 0; } }
        public int? RawMinValueRequirement { get; set; }
        public PgSkill SkillRequirement { get; set; }
        public ItemSlot SlotRequirement { get; set; }
        public RecipeItemKey MinRarityRequirement { get; set; }
        public RecipeItemKey RarityRequirement { get; set; }
        public float Preference { get { return RawPreference.HasValue ? RawPreference.Value : 0; } }
        public float? RawPreference { get; set; }
    }
}
