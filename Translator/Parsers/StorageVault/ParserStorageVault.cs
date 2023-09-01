namespace Translator;

using System.Collections.Generic;
using PgJsonReader;
using PgObjects;

public class ParserStorageVault : Parser
{
    public override object CreateItem()
    {
        return new PgStorageVault();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgStorageVault AsPgStorageVault)
            return Program.ReportFailure("Unexpected failure");

        return FinishItem(AsPgStorageVault, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
    }

    private bool FinishItem(PgStorageVault item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "ID":
                    Result = SetIntProperty((int valueInt) => item.RawId = valueInt, Value);
                    break;
                case "NpcFriendlyName":
                    Result = SetStringProperty((string valueString) => item.NpcFriendlyName = valueString, Value);
                    break;
                case "AreaName":
                    Result = StringToEnumConversion<MapAreaName>.SetEnum((MapAreaName valueEnum) => item.Area = valueEnum, Value);
                    break;
                case "NumberOfSlots":
                    Result = SetIntProperty((int valueInt) => item.RawNumSlots = valueInt, Value);
                    break;
                case "NumberOfSlotsScriptAtomic":
                    Result = SetStringProperty((string valueString) => item.NumberOfSlotsScriptAtomic = valueString, Value);
                    break;
                case "NumberOfSlotsScriptAtomicMaxValue":
                    Result = SetIntProperty((int valueInt) => item.RawNumberOfSlotsScriptAtomicMaxValue = valueInt, Value);
                    break;
                case "NumberOfSlotsScriptAtomicMinValue":
                    Result = SetIntProperty((int valueInt) => item.RawNumberOfSlotsScriptAtomicMinValue = valueInt, Value);
                    break;
                case "HasAssociatedNpc":
                    Result = SetBoolProperty((bool valueBool) => item.RawHasAssociatedNpc = valueBool, Value);
                    break;
                case "Levels":
                    Result = Inserter<PgStorageFavorLevel>.SetItemProperty((PgStorageFavorLevel valueStorageFavorLevel) => item.Levels = valueStorageFavorLevel, Value);
                    break;
                case "Requirements":
                    Result = Inserter<PgStorageRequirement>.AddKeylessArray(item.RequirementList, Value);
                    break;
                case "RequirementDescription":
                    Result = SetStringProperty((string valueString) => item.RequirementDescription = valueString, Value);
                    break;
                case "Grouping":
                    Result = StringToEnumConversion<MapAreaName>.SetEnum((MapAreaName valueEnum) => item.Grouping = valueEnum, Value);
                    break;
                case "RequiredItemKeywords":
                    Result = StringToEnumConversion<ItemKeyword>.TryParseList(Value, item.RequiredItemKeywordList);
                    break;
                case "SlotAttribute":
                    Result = Inserter<PgAttribute>.SetItemByKey((PgAttribute valueAttribute) => item.SlotAttribute_Key = PgObject.GetItemKey(valueAttribute), Value);
                    break;
                case "EventLevels":
                    Result = Inserter<PgStorageEventList>.SetItemProperty((PgStorageEventList valueStorageEventLevel) => item.EventLevels = valueStorageEventLevel, Value);
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
            if (!item.RawHasAssociatedNpc.HasValue || item.HasAssociatedNpc)
                Inserter<PgQuest>.SetNpc((PgNpcLocation npcLocation) => item.AssociatedNpc = npcLocation, item.Key, parsedFile, parsedKey, ErrorControl.IgnoreIfNotFound);
        }

        return Result;
    }
}
