namespace Translator
{
    using System.Collections.Generic;
    using PgJsonReader;

    public delegate bool VariadicObjectHandler(ref object? item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey);
}
