namespace PgObjects
{
    using System.Collections.Generic;

    public class PgNpcPreference
    {
        public List<ItemKeyword> ItemKeywordList { get; set; } = new List<ItemKeyword>();
        public int MinValueRequirement { get { return RawMinValueRequirement.HasValue ? RawMinValueRequirement.Value : 0; } }
        public int? RawMinValueRequirement { get; set; }
        public PgSkill SkillRequirement { get; set; } = null!;
        public ItemSlot SlotRequirement { get; set; }
        public RecipeItemKey MinRarityRequirement { get; set; }
        public RecipeItemKey RarityRequirement { get; set; }
        public float Preference { get { return RawPreference.HasValue ? RawPreference.Value : 0; } }
        public float? RawPreference { get; set; }

        public Dictionary<PgItem, int> ItemValueTable { get; set; } = new Dictionary<PgItem, int>();

        public override string ToString()
        {
            string Result = string.Empty;

            string KeywordString = string.Empty;
            foreach (ItemKeyword Keyword in ItemKeywordList)
            {
                if (KeywordString.Length > 0)
                    KeywordString += ", ";

                KeywordString += Keyword.ToString();
            }

            AddContent(ref Result, KeywordString);

            if (RawMinValueRequirement.HasValue)
                AddContent(ref Result, $"MinValue: {RawMinValueRequirement.Value}");

            if (SkillRequirement != null)
                AddContent(ref Result, $"Skill: {SkillRequirement.Name}");

            if (SlotRequirement != ItemSlot.Internal_None)
                AddContent(ref Result, $"Slot: {SlotRequirement}");

            if (MinRarityRequirement != RecipeItemKey.Internal_None)
                AddContent(ref Result, $"MinRarity: {MinRarityRequirement}");

            if (RarityRequirement != RecipeItemKey.Internal_None)
                AddContent(ref Result, $"Rarity: {RarityRequirement}");

            if (RawPreference.HasValue)
                AddContent(ref Result, $": {RawPreference.Value}");

            return Result;
        }

        private void AddContent(ref string text, string content)
        {
            if (text.Length > 0)
                text += "; ";

            text += content;
        }
    }
}
