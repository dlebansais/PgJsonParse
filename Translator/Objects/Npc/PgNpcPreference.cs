namespace PgObjects
{
    using System.Collections.Generic;
    using Translator;

    public class PgNpcPreference
    {
        public List<ItemKeyword> ItemKeywordList { get; set; } = new List<ItemKeyword>();
        public int MinValueRequirement { get { return RawMinValueRequirement.HasValue ? RawMinValueRequirement.Value : 0; } }
        public int? RawMinValueRequirement { get; set; }
        public string? SkillRequirement_Key { get; set; }
        public ItemSlot SlotRequirement { get; set; }
        public RecipeItemKey MinRarityRequirement { get; set; }
        public RecipeItemKey RarityRequirement { get; set; }
        public float Preference { get { return RawPreference.HasValue ? RawPreference.Value : 0; } }
        public float? RawPreference { get; set; }
        public Favor PreferenceFavor { get; set; }
        public Desire PreferenceDesire { get; set; }
        public string Name { get; set; } = string.Empty;

        private PgSkill? SkillRequirementRef;

        public void SetSkillRequirement(PgSkill skill)
        {
            SkillRequirement_Key = PgObject.GetItemKey(skill);
            SkillRequirementRef = skill;
        }

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

            if (SkillRequirement_Key != null && SkillRequirementRef != null)
                AddContent(ref Result, $"Skill: {SkillRequirementRef.ObjectName}");

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
