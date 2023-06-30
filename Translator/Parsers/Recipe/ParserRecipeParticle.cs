namespace Translator;

using System;
using System.Collections.Generic;
using PgJsonReader;
using PgObjects;

public class ParserRecipeParticle : Parser
{
    public override object CreateItem()
    {
        return new PgRecipeParticle();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgRecipeParticle AsPgParticle)
            return Program.ReportFailure("Unexpected failure");

        return FinishItem(AsPgParticle, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
    }

    private bool FinishItem(PgRecipeParticle item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "ParticleName":
                    Result = StringToEnumConversion<RecipeParticle>.SetEnum((RecipeParticle valueEnum) => item.Particle = valueEnum, Value);
                    break;
                case "PrimaryColor":
                    Result = SetColorProperty((uint valueColor) => item.RawPrimaryColor = valueColor, Value);
                    break;
                case "SecondaryColor":
                    Result = SetColorProperty((uint valueColor) => item.RawSecondaryColor = valueColor, Value);
                    break;
                case "LightColor":
                    Result = SetColorProperty((uint valueColor) => item.RawLightColor = valueColor, Value);
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
}
