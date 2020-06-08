namespace Translator
{
    using PgJsonReader;
    using System.Collections.Generic;

    public delegate bool QuestRequirementHandler(ref object item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, List<string> knownFieldList, List<string> usedFieldList, string parsedFile, string parsedKey);
}
