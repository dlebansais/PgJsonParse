namespace Translator
{
    using PgJsonObjects;
    using PgJsonReader;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.CompilerServices;

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

            return 0;
        }

        public static string VersionPath { get; set; }
        public static string LastParsedFile { get; set; }
        public static string LastParsedKey { get; set; }

        private static bool ReportFailure(string text, [CallerLineNumber] int callingFileLineNumber = 0)
        {
            Debug.WriteLine($"Error in '{LastParsedKey}' of {LastParsedFile}.json");
            Debug.WriteLine($"{text} (Line: {callingFileLineNumber})");
            return false;
        }

        private static bool ParseFile(string fileName, Type itemType, FileType fileType)
        {
            LastParsedFile = fileName;

            string FullPath = $"{VersionPath}\\{fileName}.json";
            using FileStream Stream = new FileStream(FullPath, FileMode.Open, FileAccess.Read);
            JsonTextReader Reader = new JsonTextReader(Stream);

            if (!FieldTables.GetTable(itemType, out Dictionary<string, Type> MainItemTable))
                return ReportFailure($"Table doesn't contain type {itemType}");

            if (!ParseFile(Reader, MainItemTable, fileType))
                return false;

            return true;
        }

        private static bool ParseFile(JsonTextReader reader, Dictionary<string, Type> rootItemTable, FileType fileType)
        {
            reader.Read();

            switch (fileType)
            {
                case FileType.EmbeddedObjects:
                    return ParseFileEmbeddedObjects(reader, rootItemTable);

                case FileType.KeylessArray:
                    return ParseFileKeylessArray(reader, rootItemTable);

                case FileType.KeyedArray:
                    return ParseFileKeyedArray(reader, rootItemTable);

                default:
                    return ReportFailure("Unsupported file format");
            }
        }

        private static bool ParseFileEmbeddedObjects(JsonTextReader reader, Dictionary<string, Type> rootItemTable)
        {
            if (reader.CurrentToken != Json.Token.ObjectStart)
                return ReportFailure("First token must open an object");

            reader.Read();

            while (reader.CurrentToken != Json.Token.ObjectEnd)
            {
                if (!ParseRootObject(reader, rootItemTable))
                    return false;
            }

            reader.Read();

            if (reader.CurrentToken != Json.Token.EndOfFile)
                return ReportFailure("Unexpected content after the last object");

            return true;
        }

        private static bool ParseFileKeylessArray(JsonTextReader reader, Dictionary<string, Type> rootItemTable)
        {
            if (reader.CurrentToken != Json.Token.ArrayStart)
                return ReportFailure("First token must open an array");

            reader.Read();

            while (reader.CurrentToken != Json.Token.ArrayEnd)
            {
                if (!ParseObject(reader, rootItemTable))
                    return false;
            }

            reader.Read();

            if (reader.CurrentToken != Json.Token.EndOfFile)
                return ReportFailure("Unexpected content after the last object");

            return true;
        }

        private static bool ParseFileKeyedArray(JsonTextReader reader, Dictionary<string, Type> rootItemTable)
        {
            if (reader.CurrentToken != Json.Token.ObjectStart)
                return ReportFailure("First token must open an object");

            reader.Read();

            while (reader.CurrentToken != Json.Token.ObjectEnd)
            {
                if (!ParseFileKeyedArrayItem(reader, rootItemTable))
                    return false;
            }

            reader.Read();

            if (reader.CurrentToken != Json.Token.EndOfFile)
                return ReportFailure("Unexpected content after the last array");

            return true;
        }

        private static bool ParseFileKeyedArrayItem(JsonTextReader reader, Dictionary<string, Type> rootItemTable)
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

            while (reader.CurrentToken != Json.Token.ArrayEnd)
            {
                if (!ParseObject(reader, rootItemTable))
                    return false;
            }

            reader.Read();

            return true;
        }

        private static bool ParseRootObject(JsonTextReader reader, Dictionary<string, Type> rootItemTable)
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

            if (!ParseObject(reader, rootItemTable))
                return false;

            return true;
        }

        private static bool ParseObject(JsonTextReader reader, Dictionary<string, Type> fieldTable)
        {
            if (reader.CurrentToken != Json.Token.ObjectStart)
                return ReportFailure("Object expected");

            reader.Read();

            while (reader.CurrentToken != Json.Token.ObjectEnd)
                if (!ParseField(reader, fieldTable))
                    return false;

            reader.Read();

            return true;
        }

        private static bool ParseField(JsonTextReader reader, Dictionary<string, Type> fieldTable)
        {
            if (reader.CurrentToken != Json.Token.ObjectKey)
                return ReportFailure("Key expected");

            if (reader.CurrentValue is string FieldName)
            {
                if (!fieldTable.ContainsKey(FieldName))
                    if (fieldTable.Count != 1 || !fieldTable.ContainsKey(string.Empty))
                        return ReportFailure($"Missing key: {FieldName}");
                    else
                        FieldName = string.Empty;
            }
            else
                return ReportFailure("Unexpected failure");

            Type FieldType = fieldTable[FieldName];

            reader.Read();

            if (!ParseFieldContent(reader, FieldType, FieldName))
                return false;

            return true;
        }

        private static bool ParseFieldContent(JsonTextReader reader, Type fieldType, string fieldName)
        {
            switch (reader.CurrentToken)
            {
                case Json.Token.Null:
                    reader.Read();
                    break;

                case Json.Token.Boolean:
                    if (fieldType != typeof(bool))
                        return ReportFailure($"{fieldType} expected for {fieldName} but file contains a boolean");

                    reader.Read();
                    break;

                case Json.Token.Integer:
                    if (fieldType != typeof(int) && fieldType != typeof(float) && fieldType != typeof(StringOrInteger))
                        return ReportFailure($"{fieldType} expected for {fieldName} but file contains an int");

                    reader.Read();
                    break;

                case Json.Token.Float:
                    if (fieldType != typeof(float))
                        return ReportFailure($"{fieldType} expected for {fieldName} but file contains an float");

                    reader.Read();
                    break;

                case Json.Token.String:
                    if (fieldType != typeof(string) && fieldType != typeof(StringOrInteger))
                    {
                        string StringValue = (string)reader.CurrentValue;
                        if (fieldType != typeof(int) || !int.TryParse(StringValue, out _))
                            return ReportFailure($"{fieldType} expected for {fieldName} but file contains a string");
                    }

                    reader.Read();
                    break;

                case Json.Token.ArrayStart:
                    if (!fieldType.IsArray)
                        return ReportFailure($"{fieldType} expected for {fieldName} but file contains an array");

                    Type ElementType = fieldType.GetElementType();

                    reader.Read();

                    if (reader.CurrentToken == Json.Token.ArrayStart)
                    {
                        if (!ParseNestedArray(reader, ElementType, fieldName))
                            return false;
                    }
                    else
                    {
                        if (!ParseArray(reader, ElementType, fieldName))
                            return false;
                    }

                    break;
                case Json.Token.ObjectStart:
                    if (fieldType.IsArray)
                        fieldType = fieldType.GetElementType();

                    if (!FieldTables.GetTable(fieldType, out Dictionary<string, Type> NestedTable))
                        return ReportFailure($"Table doesn't contain type {fieldType} expected for {fieldName}");

                    if (!ParseObject(reader, NestedTable))
                        return false;
                    break;
            }

            return true;
        }

        private static bool ParseArray(JsonTextReader reader, Type elementType, string fieldName)
        {
            while (reader.CurrentToken != Json.Token.ArrayEnd)
                if (!ParseFieldContent(reader, elementType, fieldName))
                    return false;

            reader.Read();

            return true;
        }

        private static bool ParseNestedArray(JsonTextReader reader, Type elementType, string fieldName)
        {
            reader.Read();

            if (!ParseArray(reader, elementType, fieldName))
                return false;

            if (reader.CurrentToken != Json.Token.ArrayEnd)
                return false;

            reader.Read();

            return true;
        }
    }
}
