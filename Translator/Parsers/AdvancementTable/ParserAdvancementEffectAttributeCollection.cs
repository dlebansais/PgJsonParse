namespace Translator;

using System.Collections.Generic;
using PgJsonReader;
using PgObjects;

public class ParserAdvancementEffectAttributeCollection : Parser
{
    public override object CreateItem()
    {
        return new PgAdvancementEffectAttributeCollection();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgAdvancementEffectAttributeCollection AsPgAdvancementEffectAttributeCollection)
            return Program.ReportFailure("Unexpected failure");

        return FinishItem(AsPgAdvancementEffectAttributeCollection, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
    }

    private bool FinishItem(PgAdvancementEffectAttributeCollection item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            Result = int.TryParse(Key, out int Level);
            if (Result)
            {
                PgAdvancement? Advancement = null;
                Result = Inserter<PgAdvancement>.SetItemProperty((PgAdvancement valueAdvancement) => Advancement = valueAdvancement, Value);
                if (Result && Advancement is not null)
                    item.SetLevelAndAdvancement(Level, Advancement);
            }
            else
                Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}' not handled");

            if (!Result)
                break;
        }

        return Result;
    }
}
