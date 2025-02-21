namespace Translator;

using System.Collections.Generic;
using PgJsonReader;
using PgObjects;

public class ParserDroppedAppearance : Parser
{
    public override object CreateItem()
    {
        return new PgDroppedAppearance();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgDroppedAppearance AsPgDroppedAppearance)
            return Program.ReportFailure("Unexpected failure");

        return FinishItem(AsPgDroppedAppearance, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
    }

    private bool FinishItem(PgDroppedAppearance item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "Appearance":
                    Result = StringToEnumConversion<ItemDroppedAppearance>.SetEnum((ItemDroppedAppearance valueEnum) => item.Appearance = valueEnum, Value);
                    break;
                case "Skin":
                    Result = StringToEnumConversion<AppearanceSkin>.SetEnum((AppearanceSkin valueEnum) => item.Skin = valueEnum, Value);
                    break;
                case "Cork":
                    Result = StringToEnumConversion<AppearanceSkin>.SetEnum((AppearanceSkin valueEnum) => item.Cork = valueEnum, Value);
                    break;
                case "Food":
                    Result = StringToEnumConversion<AppearanceSkin>.SetEnum((AppearanceSkin valueEnum) => item.Food = valueEnum, Value);
                    break;
                case "Plate":
                    Result = StringToEnumConversion<AppearanceSkin>.SetEnum((AppearanceSkin valueEnum) => item.Plate = valueEnum, Value);
                    break;
                case "Scale":
                    Result = SetFloatProperty((float valueFloat) => item.RawScale = valueFloat, Value);
                    break;
                case "Color":
                    Result = SetColorProperty((uint valueColor) => item.RawColor = valueColor, Value);
                    break;
                case "SkinColor":
                    Result = SetColorProperty((uint valueColor) => item.RawSkinColor = valueColor, Value);
                    break;
                default:
                    Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}' not handled");
                    break;
            }

            if (!Result)
                break;
        }

        if (!contentTable.ContainsKey("Appearance"))
            Result = Program.ReportFailure(parsedFile, parsedKey, "DroppedAppearance has no appearance");

        return Result;
    }
}
