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
        List<PgAdvancement> Advancements = new();

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "Levels":
                    Result = Inserter<PgAdvancement>.AddKeylessArray(Advancements, Value);
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

        if (Advancements is not null)
        {
            foreach (PgAdvancement Advancement in Advancements)
                item.LevelTable.Add(Advancement.GetLevel(), Advancement);
        }

        return true;
    }
}
