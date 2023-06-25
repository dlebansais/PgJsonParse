namespace Translator;

using System.Collections.Generic;
using PgJsonReader;
using PgObjects;

public class ParserSelfPreEffect : Parser
{
    public override object CreateItem()
    {
        return new PgSelfPreEffect();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgSelfPreEffect AsSelfPreEffect)
            return Program.ReportFailure("Unexpected failure");

        if (FinishItem(ref AsSelfPreEffect, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey))
        {
            item = AsSelfPreEffect;
            return true;
        }

        return false;
    }

    private bool FinishItem(ref PgSelfPreEffect item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;
        PreEffect Enhancement = PreEffect.Internal_None;
        PreEffect Name = PreEffect.Internal_None;
        int? EffectValue = null;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "Enhancement":
                    Result = StringToEnumConversion<PreEffect>.SetEnum((PreEffect valueEnum) => Enhancement = valueEnum, Value);
                    break;
                case "Name":
                    Result = StringToEnumConversion<PreEffect>.SetEnum((PreEffect valueEnum) => Name = valueEnum, Value);
                    break;
                case "Value":
                    Result = SetIntProperty((int valueInt) => EffectValue = valueInt, Value);
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
            if (Enhancement != PreEffect.Internal_None)
                item = new PgSelfPreEffectEnhanceZombie() { Value = Enhancement };
            else if (EffectValue is not null)
                item = new PgSelfPreEffectConfigGalvanize() { RawValue = EffectValue };
            else
                item = new PgSelfPreEffectSimple() { Value = Name };
        }

        return Result;
    }
}
