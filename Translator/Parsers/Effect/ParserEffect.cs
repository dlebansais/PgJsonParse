﻿namespace Translator;

using System.Collections.Generic;
using System.Diagnostics;
using PgJsonReader;
using PgObjects;

public class ParserEffect : Parser
{
    public override object CreateItem()
    {
        return new PgEffect();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgEffect AsPgEffect)
            return Program.ReportFailure("Unexpected failure");

        return FinishItem(AsPgEffect, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
    }

    private bool FinishItem(PgEffect item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "Name":
                    Result = SetStringProperty((string valueString) => item.Name = valueString, Value);
                    break;
                case "Description":
                    Result = SetStringProperty((string valueString) => item.Description = valueString, Value);
                    break;
                case "IconId":
                    Result = SetIconIdProperty((int valueInt) => item.RawIconId = valueInt, Value);
                    break;
                case "DisplayMode":
                    Result = StringToEnumConversion<EffectDisplayMode>.SetEnum((EffectDisplayMode valueEnum) => item.DisplayMode = valueEnum, Value);
                    break;
                case "SpewText":
                    Result = SetStringProperty((string valueString) => item.SpewText = valueString, Value);
                    break;
                case "Particle":
                    Result = Inserter<PgEffectParticle>.SetItemProperty((PgEffectParticle valueEffectParticle) => item.Particle = valueEffectParticle, Value);
                    break;
                case "StackingType":
                    Result = StringToEnumConversion<EffectStackingType>.SetEnum((EffectStackingType valueEnum) => item.StackingType = valueEnum, Value);
                    break;
                case "StackingPriority":
                    Result = SetIntProperty((int valueInt) => item.RawStackingPriority = valueInt, Value);
                    break;
                case "Duration":
                    Result = SetIntProperty((int valueInt) => item.RawDuration = valueInt, Value);
                    break;
                case "Keywords":
                    Result = StringToEnumConversion<EffectKeyword>.TryParseList(Value, item.KeywordList);
                    break;
                case "AbilityKeywords":
                    Result = StringToEnumConversion<AbilityKeyword>.TryParseList(Value, item.AbilityKeywordList);
                    break;
                default:
                    Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}' not handled");
                    break;
            }

            if (!Result)
                break;
        }

        return Result;
    }

    public static void UpdateIconsAndNames()
    {
        Dictionary<string, ParsingContext> EffectParsingTable = ParsingContext.ObjectKeyTable[typeof(PgEffect)];
        foreach (KeyValuePair<string, ParsingContext> Entry in EffectParsingTable)
        {
            PgEffect Effect = (PgEffect)Entry.Value.Item;

            if (Effect.IconId != 0)
                Effect.FriendlyIconId = Effect.IconId;
            else
                Effect.FriendlyIconId = PgObject.EffectIconId;

            if (Effect.Name.Length > 0)
                Effect.FriendlyName = Effect.Name;
            else
                Effect.FriendlyName = "(no name)";

            Debug.Assert(Effect.ObjectIconId != 0);
            Debug.Assert(Effect.ObjectName.Length > 0);
        }
    }
}
