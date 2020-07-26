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
    using System.Collections;
    using System.ComponentModel;

    public class Program
    {
        static int Main(string[] args)
        {
            int Version = 336;
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

            FinalizingResult &= ParserAbility.UpdateSource();
            FinalizingResult &= ParserRecipe.UpdateSource();

            ParserAbility.UpdateIconsAndNames();
            ParserAttribute.UpdateIconsAndNames();
            ParserEffect.UpdateIconsAndNames();
            ParserPower.UpdateIconsAndNames();
            ParserQuest.UpdateIconsAndNames();
            ParserSkill.UpdateIconsAndNames();

            ParserSkill.FillAssociationTables();

            List<object> ObjectList = ParsingContext.GetParsedObjectList();

            FindNpcSales(ObjectList);
            FindLinks(ObjectList);

            CombatParser CombatParser = new CombatParser();
            List<ItemSlot> ValidSlotList = new List<ItemSlot>()
            {
                ItemSlot.MainHand,
                ItemSlot.OffHand,
                ItemSlot.Head,
                ItemSlot.Chest,
                ItemSlot.Legs,
                ItemSlot.Feet,
                ItemSlot.Hands,
                ItemSlot.Necklace,
                ItemSlot.Ring,
                ItemSlot.Racial,
                ItemSlot.Waist,
            };

            Dictionary<string, PgModEffect> PowerKeyToCompleteEffectTable = new Dictionary<string, PgModEffect>();
            Dictionary<string, PgModEffect> EffectKeyToCompleteEffectTable = new Dictionary<string, PgModEffect>();
            CombatParser.AnalyzeCachedData(ValidSlotList, ObjectList, PowerKeyToCompleteEffectTable, EffectKeyToCompleteEffectTable);

            FinalizingResult &= StringToEnumConversion.FinalizeParsing();

            if (!FinalizingResult)
                return -1;

            ObjectList = ParsingContext.GetParsedObjectList();
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

        private static void FindLinks(List<object> objectList)
        {
            foreach (object Item in objectList)
                if (Item is PgObject AsLinkable)
                {
                    string ItemKey = GetItemKey(AsLinkable);
                    FindLinks(ItemKey, AsLinkable);
                }
        }

        private static void FindLinks(string itemKey, object nestedItem)
        {
            PropertyInfo[] Properties = nestedItem.GetType().GetProperties();

            foreach (PropertyInfo Property in Properties)
            {
                if (!Property.CanWrite)
                    continue;

                if (Property.Name == "AssociationListAbility" || Property.Name == "AssociationTablePower")
                    continue;

                Type PropertyType = Property.PropertyType;

                if (PropertyType == typeof(string) || PropertyType.IsEnum || Generate.IsTypeIgnoredForIndex(PropertyType))
                    continue;

                if (PropertyType.BaseType == typeof(PgObject))
                {
                    PgObject Reference = (PgObject)Property.GetValue(nestedItem);
                    AddLinkKey(itemKey, Reference);
                }
                else if (PropertyType.GetInterface(typeof(IDictionary).Name) != null)
                {
                    IDictionary ObjectDictionary = Property.GetValue(nestedItem) as IDictionary;
                    Type DictionaryType = PropertyType.IsGenericType ? PropertyType : PropertyType.BaseType;

                    Debug.Assert(DictionaryType.IsGenericType);
                    Type[] GenericArguments = DictionaryType.GetGenericArguments();
                    Debug.Assert(GenericArguments.Length == 2);
                    Type ItemType = GenericArguments[0];
                    Debug.Assert(ItemType != null);

                    if (ItemType == typeof(string) || ItemType == typeof(List<float>) || ItemType.IsEnum || Generate.IsTypeIgnoredForIndex(ItemType))
                    {
                    }
                    else if (ItemType.BaseType == typeof(PgObject))
                    {
                        foreach (PgObject Reference in ObjectDictionary.Keys)
                        {
                            Debug.Assert(Reference != null);
                            AddLinkKey(itemKey, Reference);
                        }
                    }
                    else if (ItemType.Name.StartsWith("Pg"))
                    {
                        foreach (object Key in ObjectDictionary.Keys)
                        {
                            object Reference = ObjectDictionary[Key];

                            Debug.Assert(Reference != null);

                            if (Reference is PgObject AsLinkable)
                                AddLinkKey(itemKey, AsLinkable);
                            else
                                FindLinks(itemKey, Reference);
                        }
                    }
                    else
                    {
                    }
                }
                else if (PropertyType.GetInterface(typeof(ICollection).Name) != null)
                {
                    ICollection ObjectCollection = Property.GetValue(nestedItem) as ICollection;
                    Type CollectionType = PropertyType.IsGenericType ? PropertyType : PropertyType.BaseType;

                    Debug.Assert(CollectionType.IsGenericType);
                    Type[] GenericArguments = CollectionType.GetGenericArguments();
                    Debug.Assert(GenericArguments.Length == 1);
                    Type ItemType = GenericArguments[0];
                    Debug.Assert(ItemType != null);

                    if (ItemType == typeof(string) || ItemType.IsEnum || Generate.IsTypeIgnoredForIndex(ItemType))
                    {
                    }
                    else if (ItemType.BaseType == typeof(PgObject))
                    {
                        foreach (PgObject Reference in ObjectCollection)
                        {
                            Debug.Assert(Reference != null);
                            AddLinkKey(itemKey, Reference);
                        }
                    }
                    else if (ItemType.Name.StartsWith("Pg"))
                    {
                        foreach (object Reference in ObjectCollection)
                        {
                            Debug.Assert(Reference != null);

                            if (Reference is PgObject AsLinkable)
                                AddLinkKey(itemKey, AsLinkable);
                            else
                                FindLinks(itemKey, Reference);
                        }
                    }
                    else
                    {
                    }
                }
                else if (PropertyType.Name.StartsWith("Pg"))
                {
                    object Reference = Property.GetValue(nestedItem);
                    if (Reference != null)
                        FindLinks(itemKey, Reference);
                }
                else
                {
                }
            }
        }

        private static string GetItemKey(PgObject item)
        {
            string Prefix = null;

            switch (item)
            {
                case PgAbility AsAbility:
                    Prefix = "A";
                    break;

                case PgAttribute AsAttribute:
                    Prefix = "T";
                    break;

                case PgDirectedGoal AsDirectedGoal:
                    Prefix = "D";
                    break;

                case PgEffect AsEffect:
                    Prefix = "E";
                    break;

                case PgItem AsItem:
                    Prefix = "I";
                    break;

                case PgLoreBook AsLoreBook:
                    Prefix = "L";
                    break;

                case PgNpc AsNpc:
                    Prefix = "N";
                    break;

                case PgPlayerTitle AsPlayerTitle:
                    Prefix = "P";
                    break;

                case PgPower AsPower:
                    Prefix = "W";
                    break;

                case PgQuest AsQuest:
                    Prefix = "Q";
                    break;

                case PgRecipe AsRecipe:
                    Prefix = "R";
                    break;

                case PgSkill AsSkill:
                    Prefix = "S";
                    break;

                case PgStorageVault AsStorageVault:
                    Prefix = "V";
                    break;
            }

            Debug.Assert(Prefix != null);

            PropertyInfo KeyProperty = item.GetType().GetProperty("Key");
            string Key = (string)KeyProperty.GetValue(item);

            Debug.Assert(Key.Length > 0);

            return $"{Prefix}{Key}";
        }

        private static void AddLinkKey(string itemKey, PgObject reference)
        {
            if (reference == null || reference == PgSkill.Unknown || reference == PgSkill.AnySkill)
                return;

            if (!reference.LinkList.Contains(itemKey))
                reference.LinkList.Add(itemKey);
        }

        private static void FindNpcSales(List<object> objectList)
        {
            List<PgItem> ItemList = new List<PgItem>();
            foreach (object Item in objectList)
                if (Item is PgItem AsItem)
                    ItemList.Add(AsItem);

            foreach (object Item in objectList)
                if (Item is PgNpc AsNpc)
                    FindNpcSales(AsNpc, ItemList);
        }

        private static void FindNpcSales(PgNpc npc, List<PgItem> itemList)
        {
            string Address = $"http://wiki.projectgorgon.com/wiki/{npc.ObjectName}/Items_sold";
            WebClientTool.DownloadText(Address, null, (bool isFound, string content) => FindSales(isFound, Address, content, false, npc, itemList), out bool IsFound);

            if (!IsFound)
            {
                Address = $"http://wiki.projectgorgon.com/wiki/{npc.ObjectName}";
                WebClientTool.DownloadText(Address, null, (bool isFound, string content) => FindSales(isFound, Address, content, true, npc, itemList), out IsFound);
            }
        }

        private static void FindSales(bool isFound, string address, string content, bool saleSectionOnly, PgNpc npc, List<PgItem> itemList)
        {
            if (content == null || !isFound)
                return;

            if (!FindSaleSection(content, saleSectionOnly, out string SaleSection))
                return;

            int StartIndex = 0;

            for (;;)
            {
                string Pattern = "<div style=\"display:table-cell; vertical-align:middle\">";
                StartIndex = SaleSection.IndexOf(Pattern, StartIndex);
                if (StartIndex < 0)
                    break;

                int EndIndex = SaleSection.IndexOf("</div>", StartIndex);
                if (EndIndex <= StartIndex)
                    break;

                StartIndex += Pattern.Length;
                string Line = SaleSection.Substring(StartIndex, EndIndex - StartIndex);

                string ItemName = string.Empty;

                if (Line.StartsWith("<a "))
                {
                    int StartNameIndex = Line.IndexOf(">");
                    if (StartNameIndex > 0)
                    {
                        int EndNameIndex = Line.IndexOf("<", StartNameIndex);
                        ItemName = Line.Substring(StartNameIndex + 1, EndNameIndex - StartNameIndex - 1);
                    }
                }

                if (ItemName.Length > 0)
                {
                    PgItem ItemMatch = null;

                    foreach (PgItem Item in itemList)
                        if (Item.ObjectName == ItemName)
                        {
                            ItemMatch = Item;
                            break;
                        }

                    if (ItemMatch != null)
                        npc.SaleList.Add(ItemMatch);
                    else if (!ItemName.StartsWith("Title:"))
                        Debug.WriteLine($"{ItemName} NOT FOUND, parsed from: {Line}");
                }
                else
                    Debug.WriteLine($"'{Line}' not parsed");

                StartIndex = EndIndex;
            }

            if (npc.SaleList.Count > 0)
            {
                npc.WikiAddress = address;
                Debug.WriteLine($"{npc.ObjectName}: {npc.SaleList.Count} item(s) sold");
            }
        }

        private static bool FindSaleSection(string content, bool saleSectionOnly, out string saleSection)
        {
            if (saleSectionOnly)
            {
                saleSection = null;
                string Pattern = "id=\"Sells\">Sells";

                int StartIndex = content.IndexOf(Pattern);
                if (StartIndex < 0)
                    return false;

                int EndIndex = content.IndexOf("<h2>", StartIndex);
                if (EndIndex <= StartIndex)
                    return false;

                StartIndex += Pattern.Length;

                saleSection = content.Substring(StartIndex, EndIndex - StartIndex);
            }
            else
                saleSection = content;

            return true;
        }
    }
}
