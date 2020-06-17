namespace Translator
{
    using PgObjects;
    using PgJsonReader;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Reflection;

    public class Program
    {
        static int Main(string[] args)
        {
            int Version = 335;
            VersionPath = $@"C:\Users\DLB\AppData\Roaming\PgJsonParse\Versions\{Version}";

            if (!ParseFile("abilities", typeof(PgAbility), FileType.EmbeddedObjects))
                return -1;

            if (!ParseFile("advancementtables", typeof(PgAdvancementTable), FileType.EmbeddedObjects))
                return -1;

            if (!ParseFile("ai", typeof(PgAI), FileType.EmbeddedObjects))
                return -1;

            if (!ParseFile("areas", typeof(PgArea), FileType.EmbeddedObjects))
                return -1;

            if (!ParseFile("attributes", typeof(PgAttribute), FileType.EmbeddedObjects))
                return -1;

            if (!ParseFile("directedgoals", typeof(PgDirectedGoal), FileType.KeylessArray))
                return -1;

            if (!ParseFile("effects", typeof(PgEffect), FileType.EmbeddedObjects))
                return -1;

            if (!ParseFile("items", typeof(PgItem), FileType.EmbeddedObjects))
                return -1;

            if (!ParseFile("itemuses", typeof(PgItemUse), FileType.EmbeddedObjects))
                return -1;

            if (!ParseFile("lorebookinfo", typeof(PgLoreBookInfo), FileType.EmbeddedObjects))
                return -1;

            if (!ParseFile("lorebooks", typeof(PgLoreBook), FileType.EmbeddedObjects))
                return -1;

            if (!ParseFile("npcs", typeof(PgNpc), FileType.EmbeddedObjects))
                return -1;

            if (!ParseFile("playertitles", typeof(PgPlayerTitle), FileType.EmbeddedObjects))
                return -1;

            if (!ParseFile("tsysclientinfo", typeof(PgPower), FileType.EmbeddedObjects))
                return -1;

            if (!ParseFile("quests", typeof(PgQuest), FileType.EmbeddedObjects))
                return -1;

            if (!ParseFile("recipes", typeof(PgRecipe), FileType.EmbeddedObjects))
                return -1;

            if (!ParseFile("skills", typeof(PgSkill), FileType.EmbeddedObjects))
                return -1;

            if (!ParseFile("sources_abilities", typeof(PgSource), FileType.KeyedArray))
                return -1;

            if (!ParseFile("sources_recipes", typeof(PgSource), FileType.KeyedArray))
                return -1;

            if (!ParseFile("storagevaults", typeof(PgStorageVault), FileType.EmbeddedObjects))
                return -1;

            if (!ParseFile("xptables", typeof(PgXpTable), FileType.EmbeddedObjects))
                return -1;

            LastParsedFile = string.Empty;
            LastParsedKey = string.Empty;

            if (!FieldTableStore.VerifyTablesCompletion())
                return -1;

            if (!ParsingContext.FinalizeParsing())
                return -1;

            bool FinalizingResult = true;
            FinalizingResult &= ParserAbilityRequirement.FinalizeParsing();
            FinalizingResult &= ParserQuestObjective.FinalizeParsing();
            FinalizingResult &= ParserQuestObjectiveRequirement.FinalizeParsing();
            FinalizingResult &= ParserQuestRequirement.FinalizeParsing();
            FinalizingResult &= ParserQuestReward.FinalizeParsing();
            FinalizingResult &= ParserStorageRequirement.FinalizeParsing();

            if (!FinalizingResult)
                return -1;

            ParserAbility.UpdateIconsAndNames();
            ParserAttribute.UpdateIconsAndNames();
            ParserEffect.UpdateIconsAndNames();
            ParserPower.UpdateIconsAndNames();
            ParserQuest.UpdateIconsAndNames();

            ParserSkill.FillAssociationTables();

            List<object> ObjectList = ParsingContext.GetParsedObjectList();
            Generate.Write(Version, ObjectList);

            return 0;
        }

        public static string VersionPath { get; set; }
        public static string LastParsedFile { get; set; }
        public static string LastParsedKey { get; set; }

        public static bool ReportFailure(string text, [CallerLineNumber] int callingFileLineNumber = 0)
        {
            WriteFailureLine(LastParsedFile, LastParsedKey, text, ErrorControl.Normal, callingFileLineNumber);
            return false;
        }

        public static bool ReportFailure(string text, ErrorControl errorControl, [CallerLineNumber] int callingFileLineNumber = 0)
        {
            WriteFailureLine(LastParsedFile, LastParsedKey, text, errorControl, callingFileLineNumber);
            return false;
        }

        public static bool ReportFailure(string parsedFile, string parsedKey, string text, [CallerLineNumber] int callingFileLineNumber = 0)
        {
            WriteFailureLine(parsedFile, parsedKey, text, ErrorControl.Normal, callingFileLineNumber);
            return false;
        }

        public static bool ReportFailure(string parsedFile, string parsedKey, string text, ErrorControl errorControl, [CallerLineNumber] int callingFileLineNumber = 0)
        {
            WriteFailureLine(parsedFile, parsedKey, text, errorControl, callingFileLineNumber);
            return false;
        }

        public static void WriteFailureLine(string parsedFile, string parsedKey, string text, ErrorControl errorControl, int callingFileLineNumber)
        {
            if (errorControl != ErrorControl.Normal)
                return;

            if (parsedFile.Length > 0 && parsedKey.Length > 0)
                Debug.WriteLine($"Error in '{parsedKey}' of {parsedFile}.json");
            else
                Debug.WriteLine($"Error");

            Debug.WriteLine($"{text} (Line: {callingFileLineNumber})");
        }

        private static bool ParseFile(string fileName, Type itemType, FileType fileType)
        {
            LastParsedFile = fileName;

            string FullPath = $"{VersionPath}\\{fileName}.json";
            using FileStream Stream = new FileStream(FullPath, FileMode.Open, FileAccess.Read);
            JsonTextReader Reader = new JsonTextReader(Stream);

            if (!FieldTableStore.GetTable(itemType, out FieldTable MainItemTable))
                return ReportFailure($"Table doesn't contain type {itemType}");

            if (!ParseFile(Reader, itemType, MainItemTable, fileType))
                return false;

            return true;
        }

        private static bool ParseFile(JsonTextReader reader, Type itemType, FieldTable rootItemTable, FileType fileType)
        {
            reader.Read();

            switch (fileType)
            {
                case FileType.EmbeddedObjects:
                    return ParseFileEmbeddedObjects(reader, itemType, rootItemTable);

                case FileType.KeylessArray:
                    return ParseFileKeylessArray(reader, itemType, rootItemTable);

                case FileType.KeyedArray:
                    return ParseFileKeyedArray(reader, itemType, rootItemTable);

                default:
                    return ReportFailure("Unsupported file format");
            }
        }

        private static bool ParseFileEmbeddedObjects(JsonTextReader reader, Type itemType, FieldTable rootItemTable)
        {
            if (reader.CurrentToken != Json.Token.ObjectStart)
                return ReportFailure("First token must open an object");

            reader.Read();

            while (reader.CurrentToken != Json.Token.ObjectEnd)
            {
                if (!ParseRootObject(reader, itemType, rootItemTable))
                    return false;
            }

            reader.Read();

            if (reader.CurrentToken != Json.Token.EndOfFile)
                return ReportFailure("Unexpected content after the last object");

            return true;
        }

        private static bool ParseFileKeylessArray(JsonTextReader reader, Type itemType, FieldTable rootItemTable)
        {
            if (reader.CurrentToken != Json.Token.ArrayStart)
                return ReportFailure("First token must open an array");

            reader.Read();

            while (reader.CurrentToken != Json.Token.ArrayEnd)
            {
                if (!MainParser.GetParsingContext(itemType, rootItemTable, out ParsingContext Context))
                    return false;

                if (!ParseObject(reader, Context))
                    return false;

                if (!Context.RecordContext())
                    return false;
            }

            reader.Read();

            if (reader.CurrentToken != Json.Token.EndOfFile)
                return ReportFailure("Unexpected content after the last object");

            return true;
        }

        private static bool ParseFileKeyedArray(JsonTextReader reader, Type itemType, FieldTable rootItemTable)
        {
            if (reader.CurrentToken != Json.Token.ObjectStart)
                return ReportFailure("First token must open an object");

            reader.Read();

            while (reader.CurrentToken != Json.Token.ObjectEnd)
            {
                if (!ParseFileKeyedArrayItem(reader, itemType, rootItemTable))
                    return false;
            }

            reader.Read();

            if (reader.CurrentToken != Json.Token.EndOfFile)
                return ReportFailure("Unexpected content after the last array");

            return true;
        }

        private static bool ParseFileKeyedArrayItem(JsonTextReader reader, Type itemType, FieldTable rootItemTable)
        {
            if (reader.CurrentToken != Json.Token.ObjectKey)
                return ReportFailure("First token in the array must be a key");

            if (reader.CurrentValue is string Key)
            {
                LastParsedKey = Key;
                reader.Read();
            }
            else
                return ReportFailure("Unexpected failure");

            if (reader.CurrentToken != Json.Token.ArrayStart)
                return ReportFailure("File item must be an array of objects");

            reader.Read();

            int KeyIndex = 0;
            while (reader.CurrentToken != Json.Token.ArrayEnd)
            {
                string ObjectKey = $"{Key}__{KeyIndex}";
                KeyIndex++;

                if (!MainParser.GetParsingContext(itemType, rootItemTable, ObjectKey, out ParsingContext Context))
                    return false;

                if (!ParseObject(reader, Context))
                    return false;

                if (!Context.RecordContext())
                    return false;
            }

            reader.Read();

            return true;
        }

        private static bool ParseRootObject(JsonTextReader reader, Type itemType, FieldTable rootItemTable)
        {
            if (reader.CurrentToken != Json.Token.ObjectKey)
                return ReportFailure("Object must have a key");

            if (reader.CurrentValue is string Key)
            {
                LastParsedKey = Key;
                reader.Read();
            }
            else
                return ReportFailure("Unexpected failure");

            if (!MainParser.GetParsingContext(itemType, rootItemTable, Key, out ParsingContext Context))
                return false;

            if (!ParseObject(reader, Context))
                return false;

            SetItemKey(Context.Item, Key);

            if (!Context.RecordContext())
                return false;

            return true;
        }

        private static bool ParseObject(JsonTextReader reader, ParsingContext context)
        {
            if (reader.CurrentToken != Json.Token.ObjectStart)
                return ReportFailure("Object expected");

            reader.Read();

            while (reader.CurrentToken != Json.Token.ObjectEnd)
                if (!ParseField(reader, context))
                    return false;

            reader.Read();

            return true;
        }

        private static bool ParseField(JsonTextReader reader, ParsingContext context)
        {
            if (reader.CurrentToken != Json.Token.ObjectKey)
                return ReportFailure("Key expected");

            Type FieldType;

            if (reader.CurrentValue is string FieldName)
            {
                if (!context.FieldTable.ContainsKey(FieldName, out FieldType))
                    return ReportFailure($"Missing key: {FieldName}");
            }
            else
                return ReportFailure("Unexpected failure");

            reader.Read();

            if (!ParseFieldContent(reader, FieldType, FieldName, context))
                return false;

            return true;
        }

        private static bool ParseFieldContent(JsonTextReader reader, Type fieldType, string fieldName, ParsingContext context)
        {
            switch (reader.CurrentToken)
            {
                case Json.Token.Null:
                    if (!context.SetContent(fieldName, null, reader.CurrentToken))
                        return false;

                    reader.Read();
                    break;

                case Json.Token.Boolean:
                    if (fieldType != typeof(bool))
                        return ReportFailure($"{fieldType} expected for {fieldName} but file contains a boolean");

                    if (!context.SetContent(fieldName, reader.CurrentValue, reader.CurrentToken))
                        return false;

                    reader.Read();
                    break;

                case Json.Token.Integer:
                    if (fieldType != typeof(int) && fieldType != typeof(float) && fieldType != typeof(StringOrInteger))
                        return ReportFailure($"{fieldType} expected for {fieldName} but file contains an int");

                    if (!context.SetContent(fieldName, reader.CurrentValue, reader.CurrentToken))
                        return false;

                    reader.Read();
                    break;

                case Json.Token.Float:
                    if (fieldType != typeof(float))
                        return ReportFailure($"{fieldType} expected for {fieldName} but file contains an float");

                    if (!context.SetContent(fieldName, reader.CurrentValue, reader.CurrentToken))
                        return false;

                    reader.Read();
                    break;

                case Json.Token.String:
                    if (fieldType != typeof(string) && fieldType != typeof(StringOrInteger))
                    {
                        string StringValue = (string)reader.CurrentValue;
                        if (fieldType != typeof(int) || !int.TryParse(StringValue, out _))
                            return ReportFailure($"{fieldType} expected for {fieldName} but file contains a string");
                    }

                    if (!context.SetContent(fieldName, reader.CurrentValue, reader.CurrentToken))
                        return false;

                    reader.Read();
                    break;

                case Json.Token.ArrayStart:
                    if (!fieldType.IsArray)
                        return ReportFailure($"{fieldType} expected for {fieldName} but file contains an array");

                    Type ElementType = fieldType.GetElementType();

                    FieldTable ArrayItemTable;

                    if (ElementType == typeof(bool) || ElementType == typeof(int) || ElementType == typeof(float) || ElementType == typeof(string))
                        ArrayItemTable = null;
                    else if (!FieldTableStore.GetTable(ElementType, out ArrayItemTable))
                        return ReportFailure($"Table doesn't contain type {fieldType} expected for {fieldName}");

                    if (!MainParser.GetParsingContext(ElementType, ArrayItemTable, out ParsingContext ArrayItemContext))
                        return false;

                    ArrayItemContext.StartArray();

                    reader.Read();

                    if (reader.CurrentToken == Json.Token.ArrayStart)
                    {
                        if (!ParseNestedArray(reader, ElementType, ArrayItemContext))
                            return false;
                    }
                    else
                    {
                        if (!ParseArray(reader, ElementType, ArrayItemContext))
                            return false;
                    }

                    if (!context.FinishArray(fieldName, ArrayItemContext))
                        return false;

                    break;
                case Json.Token.ObjectStart:
                    if (fieldType.IsArray)
                        fieldType = fieldType.GetElementType();

                    if (!FieldTableStore.GetTable(fieldType, out FieldTable NestedTable))
                        return ReportFailure($"Table doesn't contain type {fieldType} expected for {fieldName}");

                    if (!MainParser.GetParsingContext(fieldType, NestedTable, out ParsingContext NestedContext))
                        return false;

                    if (!ParseObject(reader, NestedContext))
                        return false;

                    if (!NestedContext.RecordContext())
                        return false;

                    if (!context.SetContent(fieldName, NestedContext, Json.Token.Null))
                        return false;
                    break;
            }

            return true;
        }

        private static bool ParseArray(JsonTextReader reader, Type elementType, ParsingContext context)
        {
            while (reader.CurrentToken != Json.Token.ArrayEnd)
            {
                if (!ParseFieldContent(reader, elementType, string.Empty, context))
                    return false;

                if (!context.FinishArrayItem())
                    return false;
            }

            reader.Read();

            return true;
        }

        private static bool ParseNestedArray(JsonTextReader reader, Type elementType, ParsingContext context)
        {
            reader.Read();

            if (!ParseArray(reader, elementType, context))
                return false;

            if (reader.CurrentToken != Json.Token.ArrayEnd)
                return false;

            reader.Read();

            return true;
        }

        private static void SetItemKey(object item, string key)
        {
            Debug.Assert(item != null);

            PropertyInfo Property = item.GetType().GetProperty("Key");
            Debug.Assert(Property != null);
            Debug.Assert(Property.PropertyType == typeof(string));

            Property.SetValue(item, key);
        }
    }
}
