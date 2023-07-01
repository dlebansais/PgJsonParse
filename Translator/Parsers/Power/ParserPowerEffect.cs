namespace Translator;

using System.Collections.Generic;
using PgJsonReader;
using PgObjects;

public class ParserPowerEffect : Parser
{
    public override object CreateItem()
    {
        return new PgPowerEffect();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgPowerEffect AsPgPowerEffect)
            return Program.ReportFailure("Unexpected failure");

        if (FinishItem(ref AsPgPowerEffect, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey))
        {
            item = AsPgPowerEffect;
            return true;
        }

        return false;
    }

    private bool FinishItem(ref PgPowerEffect powerEffect, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;
        string? Description = null;
        PgAttribute? ParsedAttribute = null;
        float? ParsedEffect = null;
        string? SkillKey = null;
        List<int> IconIdList = new();

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "Description":
                    Result = SetStringProperty((string valueString) => Description = valueString, Value);
                    break;
                case "AttributeName":
                    Result = Inserter<PgAttribute>.SetItemByKey((PgAttribute valueAttribute) => ParsedAttribute = valueAttribute, Value);
                    break;
                case "AttributeEffect":
                    Result = SetFloatProperty((float valueFloat) => ParsedEffect = valueFloat, Value);
                    break;
                case "AttributeSkill":
                    Result = Inserter<PgSkill>.SetItemByKey((PgSkill valueSkill) => SkillKey = PgObject.GetItemKey(valueSkill), Value);
                    break;
                case "IconIds":
                    ParseIcondIds(ref IconIdList, Value, parsedFile, parsedKey);
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
            if (ParsedAttribute is not null && ParsedEffect is not null)
            {
                powerEffect = new PgPowerEffectAttribute() { Attribute_Key = PgObject.GetItemKey(ParsedAttribute), AttributeEffect = ParsedEffect.Value, Skill_Key = SkillKey };
                if (Description is not null)
                {
                    powerEffect.Description = Description;
                    powerEffect.IconIdList = IconIdList;
                }
            }
            else if (Description is not null)
                powerEffect = new PgPowerEffectSimple() { Description = Description, IconIdList = IconIdList };
            else
                Result = Program.ReportFailure(parsedFile, parsedKey, "PowerEffect has no appearance");
        }

        return Result;
    }

    private bool ParseIcondIds(ref List<int> iconIdList, object value, string parsedFile, string parsedKey)
    {
        if (!(value is List<object> AsObjectList))
            return Program.ReportFailure(parsedFile, parsedKey, "Array of int expected for icon IDs");

        foreach (object ObjectItem in AsObjectList)
        {
            if (!(ObjectItem is int AsInt))
                return Program.ReportFailure(parsedFile, parsedKey, "Int expected for an icon ID");

            if (iconIdList.Contains(AsInt))
                return Program.ReportFailure(parsedFile, parsedKey, $"Duplicate icon ID {AsInt}");

            iconIdList.Add(AsInt);
        }

        return true;
    }
}
