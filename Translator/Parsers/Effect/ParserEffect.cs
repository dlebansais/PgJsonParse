namespace Translator;

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
                    Result = ParseEffectParticle(item, Value, parsedFile, parsedKey);
                    break;
                case "StackingType":
                    Result = StringToEnumConversion<EffectStackingType>.SetEnum((EffectStackingType valueEnum) => item.StackingType = valueEnum, Value);
                    break;
                case "StackingPriority":
                    Result = SetIntProperty((int valueInt) => item.RawStackingPriority = valueInt, Value);
                    break;
                case "Duration":
                    ParseDuration(item, Value, parsedFile, parsedKey);
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

    private bool ParseDuration(PgEffect item, object value, string parsedFile, string parsedKey)
    {
        int ValueInt;

        if (value is int AsIntDirect)
            ValueInt = AsIntDirect;
        else if (value is string AsString && int.TryParse(AsString, out int AsInt))
            ValueInt = AsInt;
        else
            return Program.ReportFailure(parsedFile, parsedKey, $"Value {value} was expected to be an int");

        return SetIntProperty((int valueInt) => item.RawDuration = valueInt, ValueInt);
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

    private bool ParseEffectParticle(PgEffect item, object value, string parsedFile, string parsedKey)
    {
        if (ParseEffectParticleString(value, parsedFile, parsedKey, out PgEffectParticle Particle))
        {
            item.Particle = Particle;
            return true;
        }
        else
            return false;
    }

    private bool ParseEffectParticleString(object value, string parsedFile, string parsedKey, out PgEffectParticle effectParticle)
    {
        effectParticle = null!;
        EffectParticle Particle;
        int? AoERange = null;
        uint? AoEColor = null;

        if (!(value is string ValueString))
            return Program.ReportFailure($"Value '{value}' was expected to be a string");

        int StartIndex = ValueString.IndexOf('(');
        if (StartIndex >= 0)
        {
            if (StartIndex > 0 && ValueString.EndsWith(")"))
            {
                string ParticleString = ValueString.Substring(0, StartIndex);
                if (!StringToEnumConversion<EffectParticle>.TryParse(ParticleString, out Particle))
                    return false;

                string ColorStrings = ValueString.Substring(StartIndex + 1, ValueString.Length - 2 - StartIndex);
                string[] Split = ColorStrings.Split(';');

                for (int i = 0; i < Split.Length; i++)
                {
                    string RangeOrColorString = Split[i];

                    if (RangeOrColorString.StartsWith("AoeColor="))
                    {
                        string ColorString = RangeOrColorString.Substring(9);
                        if (ColorString.StartsWith("#"))
                            ColorString = ColorString.Substring(1);

                        if (!Tools.TryParseColor(ColorString, out uint AoEColorInt))
                            return Program.ReportFailure($"failed to parse effect particle '{ValueString}' bad aoe color");

                        AoEColor = AoEColorInt;
                    }
                    else if (RangeOrColorString.StartsWith("AoeRange="))
                    {
                        string ColorString = RangeOrColorString.Substring(9);

                        if (!int.TryParse(ColorString, out int AoERangeInt))
                            return Program.ReportFailure($"failed to parse effect particle '{ValueString}' bad aoe range");

                        AoERange = AoERangeInt;
                    }
                    else
                        return Program.ReportFailure($"failed to parse effect particle '{ValueString}' bad syntax");
                }
            }
            else
                return Program.ReportFailure($"failed to parse effect particle '{ValueString}'");
        }
        else if (!StringToEnumConversion<EffectParticle>.TryParse(ValueString, out Particle))
            return false;

        effectParticle = new PgEffectParticle() { Particle = Particle, RawAoERange = AoERange, RawAoEColor = AoEColor };

        return true;
    }
}
