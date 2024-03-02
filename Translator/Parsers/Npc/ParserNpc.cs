namespace Translator;

using System.Collections.Generic;
using PgJsonReader;
using PgObjects;

public class ParserNpc : Parser
{
    public override object CreateItem()
    {
        return new PgNpc();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgNpc AsPgNpc)
            return Program.ReportFailure("Unexpected failure");

        return FinishItem(AsPgNpc, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
    }

    private bool FinishItem(PgNpc item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
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
                case "AreaName":
                    Result = StringToEnumConversion<MapAreaName>.SetEnum((MapAreaName valueEnum) => item.AreaName = valueEnum, Value);
                    break;
                case "AreaFriendlyName":
                    Result = SetStringProperty((string valueString) => item.AreaFriendlyName = valueString, Value);
                    break;
                case "Preferences":
                    Result = Inserter<PgNpcPreference>.AddKeylessArray(item.PreferenceList, Value);
                    break;
                case "Description":
                    Result = SetStringProperty((string valueString) => item.Description = valueString, Value);
                    break;
                case "ItemGifts":
                    Result = StringToEnumConversion<Favor>.TryParseList(Value, item.ItemGiftList);
                    break;
                case "PositionX":
                    Result = SetFloatProperty((float valueFloat) => item.RawPositionX = valueFloat, Value);
                    break;
                case "PositionY":
                    Result = SetFloatProperty((float valueFloat) => item.RawPositionY = valueFloat, Value);
                    break;
                case "PositionZ":
                    Result = SetFloatProperty((float valueFloat) => item.RawPositionZ = valueFloat, Value);
                    break;
                case "Services":
                    Result = Inserter<PgNpcService>.AddKeylessArray(item.ServiceList, Value);
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
            if (item.AreaFriendlyName == null)
                return Program.ReportFailure(parsedFile, parsedKey, "No area friendly name");

            item.IconId = PgObject.NpcIconId;
        }

        return Result;
    }
}
