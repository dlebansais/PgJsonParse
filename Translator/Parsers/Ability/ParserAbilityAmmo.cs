namespace Translator
{
    using PgJsonObjects;
    using PgJsonReader;
    using System.Collections.Generic;

    public class ParserAbilityAmmo : Parser
    {
        public override object CreateItem()
        {
            return new PgAbilityAmmo();
        }

        public override bool FinishItem(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            if (!(item is PgAbilityAmmo AsPgAbilityAmmo))
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgAbilityAmmo, contentTable, ContentTypeTable, itemCollection, LastItemType, parsedFile, parsedKey);
        }

        public bool FinishItem(PgAbilityAmmo item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            bool Result = true;

            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                switch (Key)
                {
                    case "ItemKeyword":
                        Result = Inserter<ItemKeyword>.SetEnum((ItemKeyword valueEnum) => item.ItemKeyword = valueEnum, Value);
                        break;
                    case "Count":
                        Result = SetIntProperty((int valueInt) => item.RawCount = valueInt, Value);
                        break;
                    default:
                        Result = Program.ReportFailure("Key not handled");
                        break;
                }

                if (!Result)
                    break;
            }

            return Result;
        }
    }
}
