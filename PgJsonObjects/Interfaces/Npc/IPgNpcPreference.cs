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
        IPgSkill SkillRequirement { get; }
        ItemSlot SlotRequirement { get; }
        RecipeItemKey RarityRequirement { get; }
        RecipeItemKey MinRarityRequirement { get; }

        ICollection<Gift> ItemFavorList { get; }
        void InitFavorList(Dictionary<string, IJsonKey> ItemTable);
    }
}
