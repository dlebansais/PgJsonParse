using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgNpcPreference
    {
        List<ItemKeyword> ItemKeywordList { get; }
        List<string> RawKeywordList { get; }
        double Preference { get; }
        double? RawPreference { get; }
        int MinValueRequirement { get; }
        int? RawMinValueRequirement { get; }
        Skill SkillRequirement { get; }
        ItemSlot SlotRequirement { get; }
        RecipeItemKey RarityRequirement { get; }
        RecipeItemKey MinRarityRequirement { get; }
    }
}
