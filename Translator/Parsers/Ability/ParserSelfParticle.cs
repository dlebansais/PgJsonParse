namespace Translator;

using System;
using System.Collections.Generic;
using PgJsonReader;
using PgObjects;

public class ParserSelfParticle : Parser
{
    public override object CreateItem()
    {
        return new PgSelfParticle();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgSelfParticle AsPgParticle)
            return Program.ReportFailure("Unexpected failure");

        return FinishItem(AsPgParticle, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
    }

    private bool FinishItem(PgSelfParticle item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "AoEColor":
                    Result = ParseColor(item, Value, (uint valueColor) => item.RawAoEColor = valueColor, parsedFile, parsedKey);
                    break;
                case "AoERange":
                    Result = SetIntProperty((int valueInt) => item.RawAoERange = valueInt, Value);
                    break;
                case "ParticleName":
                    Result = StringToEnumConversion<SelfParticle>.SetEnum((SelfParticle valueEnum) => item.Particle = valueEnum, Value);
                    break;
                case "PrimaryColor":
                    Result = ParseColor(item, Value, (uint valueColor) => item.RawPrimaryColor = valueColor, parsedFile, parsedKey);
                    break;
                case "SecondaryColor":
                    Result = ParseColor(item, Value, (uint valueColor) => item.RawSecondaryColor = valueColor, parsedFile, parsedKey);
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
        }

        return Result;
    }

    private bool ParseColor(PgSelfParticle item, object value, Action<uint> setter, string parsedFile, string parsedKey)
    {
        if (value is not string AsString)
            return Program.ReportFailure($"Value '{value}' was expected to be a string");

        if (!Tools.TryParseColor(AsString, out uint Color))
            return Program.ReportFailure($"Failed to parse '{AsString}' as a color");

        setter(Color);

        return true;
    }
}
