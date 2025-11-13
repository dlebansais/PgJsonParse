namespace Preprocessor;

using System.Collections.Generic;
using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

public class NpcPreference : IHasKey<int>, IHasParentKey<string>
{
    public NpcPreference(RawNpcPreference rawNpcPreference)
    {
        Desire = rawNpcPreference.Desire;
        Favor = ParseFavor(rawNpcPreference.Favor, out IsFavorError);
        Name = rawNpcPreference.Name;
        ParseKeywords(rawNpcPreference.Keywords);
        PreferenceMultiplier = rawNpcPreference.Pref;
    }

    private const string ErrorText = "Error";
    private const string MinValueHeader = "MinValue:";
    private const string SkillHeader = "SkillPrereq:";
    private const string SlotHeader = "EquipmentSlot:";
    private const string MinRarityHeader = "MinRarity:";
    private const string RarityHeader = "Rarity:";

    private static string? ParseFavor(string? rawFavor, out bool isFavorError)
    {
        isFavorError = false;

        if (rawFavor is null)
            return null;

        if (rawFavor == ErrorText)
        {
            isFavorError = true;
            return null;
        }

        return rawFavor;
    }

    private void ParseKeywords(string[]? rawKeywords)
    {
        List<string> ParsedItemKeywords = new();
        if (rawKeywords is not null)
        {
            for (int i = 0; i < rawKeywords.Length; i++)
                ParseKeyword(i, rawKeywords[i], ParsedItemKeywords);

            if (ParsedItemKeywords.Count > 0)
                ItemKeywords = ParsedItemKeywords.ToArray();
        }
    }

    private void ParseKeyword(int index, string rawKeyword, List<string> itemKeywords)
    {
        if (rawKeyword.StartsWith(MinValueHeader))
        {
            HeaderTable.Add(index, MinValueHeader);
            MinValueRequirement = int.Parse(rawKeyword.Substring(MinValueHeader.Length).Trim());
        }
        else if (rawKeyword.StartsWith(SkillHeader))
        {
            HeaderTable.Add(index, SkillHeader);
            SkillRequirement = rawKeyword.Substring(SkillHeader.Length).Trim();
        }
        else if (rawKeyword.StartsWith(SlotHeader))
        {
            HeaderTable.Add(index, SlotHeader);
            SlotRequirement = rawKeyword.Substring(SlotHeader.Length).Trim();
        }
        else if (rawKeyword.StartsWith(MinRarityHeader))
        {
            HeaderTable.Add(index, MinRarityHeader);
            MinRarityRequirement = rawKeyword.Substring(MinRarityHeader.Length).Trim();
        }
        else if (rawKeyword.StartsWith(RarityHeader))
        {
            HeaderTable.Add(index, RarityHeader);
            RarityRequirement = rawKeyword.Substring(RarityHeader.Length).Trim();
        }
        else
        {
            string ItemKeyword = rawKeyword.Trim();
            if (ItemKeyword == "Crafted:y")
                ItemKeyword = "CraftedYes";

            HeaderTable.Add(index, string.Empty);
            itemKeywords.Add(ItemKeyword);
        }
    }

    [JsonIgnore]
    [Column(IsPrimary = true)]
    public int Key { get; set; }

    [JsonIgnore]
    public string ParentKey { get; set; } = string.Empty;

    [JsonIgnore]
    public string? ParentProperty { get; set; }

    public string? Desire { get; set; }

    public string? Favor { get; set; }

    [Column(MapType = typeof(string))]
    public string[]? ItemKeywords { get; set; }

    public string? MinRarityRequirement { get; set; }

    public int? MinValueRequirement { get; set; }

    public string? Name { get; set; }

    public decimal? PreferenceMultiplier { get; set; }

    public string? RarityRequirement { get; set; }

    public string? SkillRequirement { get; set; }

    public string? SlotRequirement { get; set; }

    public RawNpcPreference ToRawNpcPreference()
    {
        RawNpcPreference Result = new();

        Result.Desire = Desire;
        Result.Favor = ToRawFavor(Favor, IsFavorError);
        Result.Keywords = ToRawKeywords();
        Result.Name = Name;
        Result.Pref = PreferenceMultiplier;

        return Result;
    }

    private static string? ToRawFavor(string? favor, bool isFavorError)
    {
        if (favor is null)
            if (isFavorError)
                return ErrorText;
            else
                return null;
        else
            return favor;
    }

    private string[]? ToRawKeywords()
    {
        if (HeaderTable.Count == 0)
            return null;

        string[] Result = new string[HeaderTable.Count];
        int ItemKeywordIndex = 0;

        for (int i = 0; i < Result.Length; i++)
            Result[i] = ToRawKeyword(i, ref ItemKeywordIndex);

        return Result;
    }

    private string ToRawKeyword(int index, ref int itemKeywordIndex)
    {
        string Result = string.Empty;

        switch (HeaderTable[index])
        {
            case MinValueHeader:
                Result = $"{MinValueHeader}{MinValueRequirement}";
                break;
            case SkillHeader:
                Result = $"{SkillHeader}{SkillRequirement}";
                break;
            case SlotHeader:
                Result = $"{SlotHeader}{SlotRequirement}";
                break;
            case MinRarityHeader:
                Result = $"{MinRarityHeader}{MinRarityRequirement}";
                break;
            case RarityHeader:
                Result = $"{RarityHeader}{RarityRequirement}";
                break;
            default:
                string ItemKeyword = ItemKeywords![itemKeywordIndex++];
                if (ItemKeyword == "CraftedYes")
                    ItemKeyword = "Crafted:y";

                Result = ItemKeyword;
                break;
        }

        return Result;
    }

    private Dictionary<int, string> HeaderTable = new Dictionary<int, string>();
    private bool IsFavorError;
}
