namespace Preprocessor;

using System.Collections.Generic;

internal class NpcPreference
{
    public NpcPreference(RawNpcPreference rawNpcPreference)
    {
        Desire = rawNpcPreference.Desire;
        Favor = ParseFavor(rawNpcPreference.Favor, out IsFavorError);
        ParseKeywords(rawNpcPreference.Keywords);
        PreferenceMultiplier = rawNpcPreference.Pref;
    }

    private const string ErrorText = "Error";
    private const string MinValueHeader = "MinValue:";
    private const string SkillHeader = "SkillPrereq:";
    private const string SlotHeader = "EquipmentSlot:";
    private const string MinRarityHeader = "MinRarity:";
    private const string RarityHeader = "Rarity:";

    private static string? ParseFavor(string? content, out bool isFavorError)
    {
        isFavorError = false;

        if (content == null)
            return null;

        if (content == ErrorText)
        {
            isFavorError = true;
            return null;
        }

        return content;
    }

    private void ParseKeywords(string[]? content)
    {
        if (content is not null)
            for (int i = 0; i < content.Length; i++)
                ParseKeyword(i, content[i]);
    }

    private void ParseKeyword(int index, string content)
    {
        if (content.StartsWith(MinValueHeader))
        {
            HeaderTable.Add(index, MinValueHeader);
            MinValueRequirement = int.Parse(content.Substring(MinValueHeader.Length).Trim());
        }
        else if (content.StartsWith(SkillHeader))
        {
            HeaderTable.Add(index, SkillHeader);
            SkillRequirement = content.Substring(SkillHeader.Length).Trim();
        }
        else if (content.StartsWith(SlotHeader))
        {
            HeaderTable.Add(index, SlotHeader);
            SlotRequirement = content.Substring(SlotHeader.Length).Trim();
        }
        else if (content.StartsWith(MinRarityHeader))
        {
            HeaderTable.Add(index, MinRarityHeader);
            MinRarityRequirement = content.Substring(MinRarityHeader.Length).Trim();
        }
        else if (content.StartsWith(RarityHeader))
        {
            HeaderTable.Add(index, RarityHeader);
            RarityRequirement = content.Substring(RarityHeader.Length).Trim();
        }
        else
        {
            if (ItemKeywords is null)
                ItemKeywords = new List<string>();

            string ItemKeyword = content.Trim();
            if (ItemKeyword == "Crafted:y")
                ItemKeyword = "CraftedYes";

            HeaderTable.Add(index, string.Empty);
            ItemKeywords.Add(ItemKeyword);
        }
    }

    public string? Desire { get; set; }
    public string? Favor { get; set; }
    public List<string>? ItemKeywords { get; set; }
    public string? MinRarityRequirement { get; set; }
    public int? MinValueRequirement { get; set; }
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
        switch (HeaderTable[index])
        {
            case MinValueHeader:
                return $"{MinValueHeader}{MinValueRequirement}";
            case SkillHeader:
                return $"{SkillHeader}{SkillRequirement}";
            case SlotHeader:
                return $"{SlotHeader}{SlotRequirement}";
            case MinRarityHeader:
                return $"{MinRarityHeader}{MinRarityRequirement}";
            case RarityHeader:
                return $"{RarityHeader}{RarityRequirement}";
            default:
                string? ItemKeyword = ItemKeywords?[itemKeywordIndex++];
                if (ItemKeyword == "CraftedYes")
                    ItemKeyword = "Crafted:y";

                return $"{ItemKeyword}";
        }
    }

    private Dictionary<int, string> HeaderTable = new Dictionary<int, string>();
    private bool IsFavorError;
}
