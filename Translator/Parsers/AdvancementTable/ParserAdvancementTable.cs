namespace Translator;

using System.Collections.Generic;
using PgJsonReader;
using PgObjects;

public class ParserAdvancementTable : Parser
{
    public override object CreateItem()
    {
        return new PgAdvancementTable();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgAdvancementTable AsPgAdvancementTable)
            return Program.ReportFailure("Unexpected failure");

        AsPgAdvancementTable.Key = objectKey;

        return FinishItem(AsPgAdvancementTable, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
    }

    private bool FinishItem(PgAdvancementTable item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;
        PgAdvancementEffectAttributeCollection? Advancement = null;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "Levels":
                    Result = Inserter<PgAdvancementEffectAttributeCollection>.SetItemProperty((PgAdvancementEffectAttributeCollection valueAdvancement) => Advancement = valueAdvancement, Value);
                    break;
                case "Name":
                    Result = SetStringProperty((string valueString) => item.InternalName = valueString, Value);
                    break;
                default:
                    Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}' not handled");
                    break;
            }

            if (!Result)
                break;
        }

        if (Advancement is not null)
        {
            Dictionary<int, PgAdvancement> LevelAdvancementTable = Advancement.GetLevelAdvancementTable();

            foreach (KeyValuePair<int, PgAdvancement> Entry in LevelAdvancementTable)
                item.LevelTable.Add(Entry.Key, Entry.Value);
        }

        return true;
    }
}
