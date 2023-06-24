namespace Translator;

using System.Collections.Generic;
using PgJsonReader;
using PgObjects;

public class ParserPowerTier : Parser
{
    public override object CreateItem()
    {
        return new PgPowerTier();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgPowerTier AsPgPowerTier)
            return Program.ReportFailure("Unexpected failure");

        return FinishItem(AsPgPowerTier, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
    }

    private bool FinishItem(PgPowerTier item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "EffectDescs":
                    Result = ParseEffectDescriptionList(item.EffectList, Value, parsedFile, parsedKey);
                    break;
                case "SkillLevelPrereq":
                    Result = SetIntProperty((int valueInt) => item.RawSkillLevelPrereq = valueInt, Value);
                    break;
                case "MinLevel":
                    Result = SetIntProperty((int valueInt) => item.RawMinLevel = valueInt, Value);
                    break;
                case "MaxLevel":
                    Result = SetIntProperty((int valueInt) => item.RawMaxLevel = valueInt, Value);
                    break;
                case "MinRarity":
                    Result = ParseKeywordAsMinRarity(item, Value, parsedFile, parsedKey);
                    break;
                default:
                    Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}' not handled");
                    break;
            }

            if (!Result)
                break;
        }

        if (Result)
        {
            if (!item.RawSkillLevelPrereq.HasValue)
                Result = Program.ReportFailure(parsedFile, parsedKey, $"Power has no skill level requirement");
        }

        return Result;
    }

    public static bool ParseEffectDescriptionList(PgPowerEffectCollection effectDescriptionList, object value, string parsedFile, string parsedKey)
    {
        if (!(value is List<object> ObjectList))
            return Program.ReportFailure(parsedFile, parsedKey, $"Value '{value}' was expected to be a list");

        foreach (object Item in ObjectList)
        {
            if (!(Item is string EffectDescription))
                return Program.ReportFailure(parsedFile, parsedKey, $"Value '{Item}' was expected to be a string");

            if (!ParseEffectDescription(effectDescriptionList, EffectDescription, parsedFile, parsedKey))
                return false;
        }

        return true;
    }

    private static bool ParseEffectDescription(PgPowerEffectCollection effectDescriptionList, string effectDescription, string parsedFile, string parsedKey)
    {
        PgPowerEffect PowerEffect;
        if (effectDescription.StartsWith("{") && effectDescription.EndsWith("}"))
        {
            string EffectString = effectDescription.Substring(1, effectDescription.Length - 2);
            if (!ParseItemEffectAttribute(EffectString, parsedFile, parsedKey, out PowerEffect))
                return Program.ReportFailure(parsedFile, parsedKey, $"Invalid attribute format '{EffectString}'");
        }
        else if (effectDescription.Contains("{") || effectDescription.Contains("}"))
            return Program.ReportFailure(parsedFile, parsedKey, $"Invalid attribute format '{effectDescription}'");
        else
        {
            if (!ParseItemEffectSimple(effectDescription, parsedFile, parsedKey, out PowerEffect))
                return Program.ReportFailure(parsedFile, parsedKey, $"Invalid attribute format '{effectDescription}'");
        }

        ParsingContext.AddSuplementaryObject(PowerEffect);
        effectDescriptionList.Add(PowerEffect);
        return true;
    }

    private static bool ParseItemEffectAttribute(string effectString, string parsedFile, string parsedKey, out PgPowerEffect powerEffect)
    {
        powerEffect = null!;

        string[] Split = effectString.Split('{');
        if (Split.Length != 2 && Split.Length != 3)
            return false;

        string AttributeName = Split[0];
        string AttributeEffect = Split[1];

        if (!AttributeName.EndsWith("}"))
            return false;

        AttributeName = AttributeName.Substring(0, AttributeName.Length - 1);
        if (AttributeName.Contains("{") || AttributeName.Contains("}"))
            return false;

        if (AttributeName.Length == 0 || AttributeEffect.Length == 0)
            return false;

        PgAttribute ParsedAttribute = null!;
        if (!Inserter<PgAttribute>.SetItemByKey((PgAttribute valueAttribute) => ParsedAttribute = valueAttribute, AttributeName))
            return false;

        if (Split.Length == 3)
        {
            if (!AttributeEffect.EndsWith("}"))
                return false;

            AttributeEffect = AttributeEffect.Substring(0, AttributeEffect.Length - 1);
        }

        if (!Tools.TryParseFloat(AttributeEffect, out float ParsedEffect, out FloatFormat ParsedEffectFormat))
            return false;

        if (ParsedEffectFormat != FloatFormat.Standard)
            return false;

        PgPowerEffectAttribute NewPowerEffectAttribute;

        if (Split.Length == 3)
        {
            string AttributeSkill = Split[2];

            PgSkill ParsedSkill = null!;

            if (AttributeSkill == "AnySkill")
                ParsedSkill = PgSkill.AnySkill;
            else if (!Inserter<PgSkill>.SetItemByKey((PgSkill valueSkill) => ParsedSkill = valueSkill, AttributeSkill))
                return false;

            NewPowerEffectAttribute = new PgPowerEffectAttribute() { AttributeEffect = ParsedEffect, AttributeEffectFormat = ParsedEffectFormat, Skill_Key = PgObject.GetItemKey(ParsedSkill) };
        }
        else
            NewPowerEffectAttribute = new PgPowerEffectAttribute() { AttributeEffect = ParsedEffect, AttributeEffectFormat = ParsedEffectFormat };

        NewPowerEffectAttribute.SetAttribute(ParsedAttribute);
        powerEffect = NewPowerEffectAttribute;
        return true;
    }

    private static bool ParseItemEffectSimple(string effectString, string parsedFile, string parsedKey, out PgPowerEffect powerEffect)
    {
        string Description = effectString.Trim();

        List<int> IconIdList = new List<int>();
        string IconIdPattern = "<icon=";

        for (; ;)
        {
            if (IconIdList.Count > 0 && Description.Contains(IconIdPattern))
                Description = Description.Trim();

            if (Description.Length < IconIdPattern.Length)
                break;

            if (!Description.StartsWith(IconIdPattern))
                break;

            int EndIndex = Description.IndexOf('>');
            if (EndIndex < IconIdPattern.Length + 1)
                break;

            string IdString = Description.Substring(IconIdPattern.Length, EndIndex - IconIdPattern.Length);

            int Id;
            if (!int.TryParse(IdString, out Id))
                break;

            if (parsedKey == "24154" && Id == 3553)
                Id = 3547; // Fix combo rip+bat stability -> bat stability icon.

            if (!IconIdList.Contains(Id))
                IconIdList.Add(Id);

            Description = Description.Substring(EndIndex + 1);
        }

        if (IsBuggedDescription(Description, IconIdList, out PgPowerEffect FixedEffect))
            powerEffect = FixedEffect;
        else
            powerEffect = new PgPowerEffectSimple() { Description = Description, IconIdList = IconIdList };

        return true;
    }

    private static bool IsBuggedDescription(string description, List<int> iconIdList, out PgPowerEffect fixedEffect)
    {
        if (IsBuggedDescription(description, iconIdList, "Tough Hoof deals ", " Trauma damage to the target each time they attack and damage you (within 8 seconds)", "BOOST_ABILITYDOT_TOUGHHOOF", out fixedEffect))
            return true;

        return false;
    }

    private static bool IsBuggedDescription(string description, List<int> iconIdList, string startPattern, string endPattern, string attributeKey, out PgPowerEffect fixedEffect)
    {
        int StartIndex = description.IndexOf(startPattern);
        int EndIndex = description.LastIndexOf(endPattern);
        PgAttribute ParsedAttribute = null!;

        if (StartIndex == 0 &&
            EndIndex > startPattern.Length &&
            Tools.TryParseFloat(description.Substring(startPattern.Length, EndIndex - startPattern.Length), out float ParsedEffect, out FloatFormat ParsedEffectFormat) &&
            Inserter<PgAttribute>.SetItemByKey((PgAttribute valueAttribute) => ParsedAttribute = valueAttribute, attributeKey))
        {
            PgPowerEffectAttribute NewPowerEffectAttribute = new PgPowerEffectAttribute() { Description = description, IconIdList = iconIdList, AttributeEffect = ParsedEffect, AttributeEffectFormat = ParsedEffectFormat };
            NewPowerEffectAttribute.SetAttribute(ParsedAttribute);
            fixedEffect = NewPowerEffectAttribute;
            return true;
        }
        else
        {
            fixedEffect = null!;
            return false;
        }
    }

    private bool ParseKeywordAsMinRarity(PgPowerTier item, object value, string parsedFile, string parsedKey)
    {
        if (!(value is string ValueKeyword))
            return Program.ReportFailure(parsedFile, parsedKey, $"Value '{value}' was expected to be a string");

        if (ValueKeyword == "Uncommon")
            item.MinRarity = RecipeItemKey.MinRarity_Uncommon;
        else if (ValueKeyword == "Rare")
            item.MinRarity = RecipeItemKey.MinRarity_Rare;
        else if (ValueKeyword == "Exceptional")
            item.MinRarity = RecipeItemKey.MinRarity_Exceptional;
        else if (ValueKeyword == "Epic")
            item.MinRarity = RecipeItemKey.MinRarity_Epic;
        else
            return Program.ReportFailure(parsedFile, parsedKey, $"Invalid minimum rarity '{value}'");

        StringToEnumConversion<RecipeItemKey>.SetCustomParsedEnum(item.MinRarity);
        return true;
    }
}
