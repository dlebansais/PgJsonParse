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
    using System.Text;

    public class Program
    {
        static int Main(string[] args)
        {
            const string RequestUri = "http://client.projectgorgon.com/fileversion.txt";
            Stopwatch Watch = new Stopwatch();
            string VersionContent = string.Empty;
            WebClientTool.DownloadText(RequestUri, Watch, (bool isFound, string content) => VersionContent = content, out bool IsFound);

            if (!int.TryParse(VersionContent, out int Version))
            {
                Debug.WriteLine($"Unable to parse {VersionContent} as a version number");
                return -1;
            }

            VersionPath = $@"C:\Users\DLB\AppData\Roaming\PgJsonParse\Versions\{Version}";

            if (!Directory.Exists(VersionPath))
            {
                Debug.WriteLine($"{Version} is a new version");
                Directory.CreateDirectory(VersionPath);
            }

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

            //AddHardCodedAttribute(PgAttribute.COCKATRICEDEBUFF_COST_DELTA);
            //AddHardCodedAttribute(PgAttribute.LAMIADEBUFF_COST_DELTA);
            //AddHardCodedAttribute(PgAttribute.MONSTER_MATCH_OWNER_SPEED);
            //AddHardCodedAttribute(PgAttribute.ARMOR_MITIGATION_RATIO);
            //AddHardCodedAttribute(PgAttribute.SHOW_CLEANLINESS_INDICATORS);
            //AddHardCodedAttribute(PgAttribute.SHOW_COMMUNITY_INDICATORS);
            //AddHardCodedAttribute(PgAttribute.SHOW_PEACEABLENESS_INDICATORS);
            //AddHardCodedAttribute(PgAttribute.SHOW_FAIRYENERGY_INDICATORS);
            //AddHardCodedAttribute(PgAttribute.MONSTER_COMBAT_XP_VALUE);
            
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

            FindNpcBarters(ObjectList);
            FindNpcSales(ObjectList);
            FindNpcSources(ObjectList);
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

        private static void AddHardCodedAttribute(PgAttribute staticAttribute)
        {
            Dictionary<string, ParsingContext> AttributeContextTable = ParsingContext.ObjectKeyTable[typeof(PgAttribute)];
            ParsingContext Context = new ParsingContext(MainParser.Parsers[typeof(PgAttribute)], typeof(PgAttribute), staticAttribute, FieldTableStore.Tables[typeof(PgAttribute)], staticAttribute.Key);

            AttributeContextTable.Add(staticAttribute.Key, Context);
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

            if (!File.Exists(FullPath))
            {
                string RequestUri = $"http://client.projectgorgon.com/{fileName}.json";
                Stopwatch Watch = new Stopwatch();
                string FileContent = string.Empty;
                WebClientTool.DownloadText(RequestUri, Watch, (bool isFound, string content) => FileContent = content, out bool IsFound);

                if (IsFound)
                {
                    using FileStream WriteStream = new FileStream(FullPath, FileMode.Create, FileAccess.Write);
                    using StreamWriter Writer = new StreamWriter(WriteStream, Encoding.UTF8);
                    Writer.Write(FileContent);
                }
            }


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
                if (reader.CurrentToken == Json.Token.ArrayStart)
                {
                    reader.Read();

                    if (!ParseArray(reader, elementType, context))
                        return false;

                    if (reader.CurrentToken != Json.Token.ArrayEnd)
                        return false;
                }
                else if (!ParseFieldContent(reader, elementType, string.Empty, context))
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
            if (IsNpcIgnoredInWiki(npc.ObjectName))
                return;

            string NpcName = ToWikiNpcName(npc.ObjectName);

            string Address = $"http://wiki.projectgorgon.com/wiki/{NpcName}/Items_sold";
            WebClientTool.DownloadText(Address, null, (bool isFound, string content) => FindSales(isFound, Address, content, false, npc, itemList), out bool IsFound);

            if (!IsFound)
            {
                Address = $"http://wiki.projectgorgon.com/wiki/{NpcName}";
                WebClientTool.DownloadText(Address, null, (bool isFound, string content) => FindSales(isFound, Address, content, true, npc, itemList), out IsFound);
            }

            if (!IsFound)
                Debug.WriteLine($"{npc} NOT FOUND in wiki");
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
                        Debug.WriteLine($"{ItemName} NOT FOUND, parsed from: {Line}, Npc is {npc}");
                }
                else
                    Debug.WriteLine($"'{Line}' not parsed");

                StartIndex = EndIndex;
            }

            if (npc.SaleList.Count > 0)
            {
                npc.WikiAddress = address;

                WriteSaleLine($"\n{npc.ObjectName}: {npc.SaleList.Count} item(s) sold");
                foreach (PgItem Item in npc.SaleList)
                    WriteSaleLine($"  {Item.ObjectName}");
            }
        }

        private static void WriteSaleLine(string text)
        {
            //Debug.WriteLine(text);
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

        private static void FindNpcSources(List<object> objectList)
        {
            List<PgSource> SourceList = new List<PgSource>();
            List<PgAbility> AbilityList = new List<PgAbility>();
            List<PgRecipe> RecipeList = new List<PgRecipe>();

            foreach (object Item in objectList)
                switch (Item)
                {
                    case PgSource AsSource:
                        SourceList.Add(AsSource);
                        break;
                    case PgAbility AsAbility:
                        AbilityList.Add(AsAbility);
                        break;
                    case PgRecipe AsRecipe:
                        RecipeList.Add(AsRecipe);
                        break;
                }

            foreach (PgSource Source in SourceList)
            {
                switch (Source)
                {
                    case PgSourceAutomaticFromSkill AsSourceAutomaticFromSkill:
                        break;
                    case PgSourceEffect AsSourceEffect:
                        break;
                    case PgSourceGift AsSourceGift:
                        AddNpcSource(Source, AsSourceGift.Npc, AbilityList, RecipeList);
                        break;
                    case PgSourceHangOut AsSourceHangOut:
                        AddNpcSource(Source, AsSourceHangOut.Npc, AbilityList, RecipeList);
                        break;
                    case PgSourceItem AsSourceItem:
                        break;
                    case PgSourceLearnAbility AsSourceLearnAbility:
                        break;
                    case PgSourceQuest AsSourceQuest:
                        break;
                    case PgSourceRecipe AsSourceRecipe:
                        break;
                    case PgSourceTraining AsSourceTraining:
                        AddNpcSource(Source, AsSourceTraining.Npc, AbilityList, RecipeList);
                        break;
                }
            }
        }

        private static void AddNpcSource(PgSource source, PgNpcLocation location, List<PgAbility> abilityList, List<PgRecipe> recipeList)
        {
            PgNpc Npc = location.Npc;
            if (Npc == null)
                return;

            bool IsFound = false;
            string SourceKey = source.SourceKey;

            foreach (PgAbility Item in abilityList)
                if (Item.Key == SourceKey)
                {
                    Npc.SourceAbilityList.Add(source);
                    IsFound = true;
                    break;
                }

            foreach (PgRecipe Item in recipeList)
                if (Item.Key == SourceKey)
                {
                    Npc.SourceRecipeList.Add(source);
                    IsFound = true;
                    break;
                }

            Debug.Assert(IsFound);
        }

        private static void FindNpcBarters(List<object> objectList)
        {
            List<PgItem> ItemList = new List<PgItem>();
            foreach (object Item in objectList)
                if (Item is PgItem AsItem)
                    ItemList.Add(AsItem);

            foreach (object Item in objectList)
                if (Item is PgNpc AsNpc)
                    FindNpcBarters(AsNpc, ItemList);
        }

        private static void FindNpcBarters(PgNpc npc, List<PgItem> itemList)
        {
            if (IsNpcIgnoredInWiki(npc.ObjectName))
                return;

            string NpcName = ToWikiNpcName(npc.ObjectName);

            string Address = $"http://wiki.projectgorgon.com/wiki/{NpcName}";
            WebClientTool.DownloadText(Address, null, (bool isFound, string content) => FindBarters(isFound, Address, content, npc, itemList), out bool IsFound);

            if (!IsFound)
                Debug.WriteLine($"{npc} NOT FOUND in wiki");
        }

        private static void FindBarters(bool isFound, string address, string content, PgNpc npc, List<PgItem> itemList)
        {
            if (content == null || !isFound)
                return;

            if (!FindBarterSection(content, out HtmlSection BarterSection))
                return;

            List<TagTableRow> RowList = new List<TagTableRow>();
            for (int i = 1; i < BarterSection.NestedTagList.Count; i++)
            {
                TagTableRow AsRow = BarterSection.NestedTagList[i] as TagTableRow;
                Debug.Assert(AsRow != null);

                RowList.Add(AsRow);
            }

            TagTableCell GiveCell = null;
            TagTableCell ReceiveCell = null;
            PgNpcBarter NewBarter = null;
            int GiveRowSpan = 1;
            int ReceiveRowSpan = 1;
            bool IsMultiBarter = IsNpcMultiBarter(npc.Name);

            if (npc.Name.StartsWith("Puffy Ben"))
            {
            }

            bool SplitGive = false;

            foreach (TagTableRow Row in RowList)
            {
                Debug.Assert(Row.NestedTagList.Count >= 1 && Row.NestedTagList.Count <= 2);

                if (Row.NestedTagList.Count == 1)
                {
                    Debug.Assert(NewBarter != null);

                    ParseRowSpan(GiveCell, ref GiveRowSpan);
                    ParseRowSpan(ReceiveCell, ref ReceiveRowSpan);
                    Debug.Assert(GiveRowSpan > 1 || ReceiveRowSpan > 1);

                    if (GiveRowSpan > 1)
                    {
                        GiveCell = null;
                        ReceiveCell = Row.NestedTagList[0] as TagTableCell;
                    }
                    else
                    {
                        GiveCell = Row.NestedTagList[0] as TagTableCell;
                        ReceiveCell = null;
                    }

                    if (IsMultiBarter)
                    {
                        Dictionary<PgItem, int> GiveTable = NewBarter.GiveTable;
                        Dictionary<PgItem, int> ReceiveTable = NewBarter.ReceiveTable;

                        if (SplitGive)
                        {
                            foreach (KeyValuePair<PgItem, int> Entry in GiveTable)
                            {
                                PgNpcBarter Barter = new PgNpcBarter();
                                Barter.GiveTable.Add(Entry.Key, Entry.Value);

                                foreach (KeyValuePair<PgItem, int> ReceiveEntry in ReceiveTable)
                                    Barter.ReceiveTable.Add(ReceiveEntry.Key, ReceiveEntry.Value);

                                ParsingContext.AddSuplementaryObject(Barter);
                                npc.BarterList.Add(Barter);
                            }
                        }
                        else
                        {
                            ParsingContext.AddSuplementaryObject(NewBarter);
                            npc.BarterList.Add(NewBarter);
                        }

                        NewBarter = new PgNpcBarter();

                        if (GiveCell == null)
                        {
                            foreach (KeyValuePair<PgItem, int> Entry in GiveTable)
                                NewBarter.GiveTable.Add(Entry.Key, Entry.Value);
                        }

                        if (ReceiveCell == null)
                        {
                            foreach (KeyValuePair<PgItem, int> Entry in ReceiveTable)
                                NewBarter.ReceiveTable.Add(Entry.Key, Entry.Value);
                        }
                    }
                }
                else
                {
                    GiveCell = Row.NestedTagList[0] as TagTableCell;
                    Debug.Assert(GiveCell != null);
                    Debug.Assert(GiveCell.NestedTagList.Count > 0);

                    ReceiveCell = Row.NestedTagList[1] as TagTableCell;
                    Debug.Assert(ReceiveCell != null);
                    Debug.Assert(ReceiveCell.NestedTagList.Count > 0);

                    GiveRowSpan = 1;
                    ReceiveRowSpan = 1;

                    if (NewBarter != null && NewBarter.GiveTable.Count > 0 && NewBarter.ReceiveTable.Count > 0)
                    {
                        Dictionary<PgItem, int> GiveTable = NewBarter.GiveTable;
                        Dictionary<PgItem, int> ReceiveTable = NewBarter.ReceiveTable;

                        if (SplitGive)
                        {
                            foreach (KeyValuePair<PgItem, int> Entry in GiveTable)
                            {
                                PgNpcBarter Barter = new PgNpcBarter();
                                Barter.GiveTable.Add(Entry.Key, Entry.Value);

                                foreach (KeyValuePair<PgItem, int> ReceiveEntry in ReceiveTable)
                                    Barter.ReceiveTable.Add(ReceiveEntry.Key, ReceiveEntry.Value);

                                ParsingContext.AddSuplementaryObject(Barter);
                                npc.BarterList.Add(Barter);
                            }
                        }
                        else
                        {
                            ParsingContext.AddSuplementaryObject(NewBarter);
                            npc.BarterList.Add(NewBarter);
                        }
                    }

                    NewBarter = new PgNpcBarter();
                }

                if (GiveCell != null)
                {
                    SplitGive = false;

                    string[] GiveCellContent = GiveCell.Content.Trim().Split('|');

                    for (int TagIndex = 0; TagIndex < GiveCell.NestedTagList.Count; TagIndex++)
                    {
                        if (GiveCell.NestedTagList[TagIndex] is TagBreak)
                        {
                            if (GiveCellContent.Length > TagIndex && GiveCellContent[TagIndex] == "OR")
                                SplitGive = true;
                            continue;
                        }

                        TagSection GiveSectionItem = GiveCell.NestedTagList[TagIndex] as TagSection;
                        Debug.Assert(GiveSectionItem != null);

                        PgItem Item;
                        GetItem(GiveSectionItem, itemList, out Item);
                        Debug.Assert(Item != null);

                        int MinCount = 1;
                        int MaxCount = 0;
                        if (GiveCellContent.Length > TagIndex)
                            ContentToCount(GiveCellContent[TagIndex], out MinCount, out MaxCount);
                        if (MinCount == 0)
                            MinCount = 1;

                        NewBarter.GiveTable.Add(Item, MinCount + MaxCount * 100000);
                    }
                }

                if (ReceiveCell != null)
                {
                    string[] ReceiveCellContent = ReceiveCell.Content.Trim().Split('|');

                    for (int TagIndex = 0; TagIndex < ReceiveCell.NestedTagList.Count; TagIndex++)
                    {
                        if (ReceiveCell.NestedTagList[TagIndex] is TagSpan)
                            continue;
                        if (ReceiveCell.NestedTagList[TagIndex] is TagSuperscript)
                            continue;

                        TagSection ReceiveSectionItem = ReceiveCell.NestedTagList[TagIndex] as TagSection;
                        Debug.Assert(ReceiveSectionItem != null);

                        PgItem Item;
                        GetItem(ReceiveSectionItem, itemList, out Item);

                        if (Item != null)
                        {
                            int MinCount = 1;
                            int MaxCount = 0;
                            if (ReceiveCellContent.Length > TagIndex)
                                ContentToCount(ReceiveCellContent[TagIndex], out MinCount, out MaxCount);
                            if (MinCount == 0)
                                MinCount = 1;

                            NewBarter.ReceiveTable.Add(Item, MinCount + MaxCount * 100000);
                        }
                        else
                            Debug.WriteLine($"Item NOT FOUND in wiki");
                    }
                }
            }

            if (NewBarter != null && NewBarter.GiveTable.Count > 0 && NewBarter.ReceiveTable.Count > 0)
            {
                if (SplitGive)
                {
                    Dictionary<PgItem, int> GiveTable = NewBarter.GiveTable;
                    Dictionary<PgItem, int> ReceiveTable = NewBarter.ReceiveTable;

                    foreach (KeyValuePair<PgItem, int> Entry in GiveTable)
                    {
                        PgNpcBarter Barter = new PgNpcBarter();
                        Barter.GiveTable.Add(Entry.Key, Entry.Value);

                        foreach (KeyValuePair<PgItem, int> ReceiveEntry in ReceiveTable)
                            Barter.ReceiveTable.Add(ReceiveEntry.Key, ReceiveEntry.Value);

                        ParsingContext.AddSuplementaryObject(Barter);
                        npc.BarterList.Add(Barter);
                    }
                }
                else
                {
                    ParsingContext.AddSuplementaryObject(NewBarter);
                    npc.BarterList.Add(NewBarter);
                }
            }

            if (npc.BarterList.Count > 0)
            {
                npc.WikiAddress = address;
                WriteBarterLine($"\n{npc.ObjectName}: {npc.BarterList.Count} item(s) bartered");

                foreach (PgNpcBarter Barter in npc.BarterList)
                {
                    string GiveList = string.Empty;

                    foreach (KeyValuePair<PgItem, int> Entry in Barter.GiveTable)
                    {
                        if (GiveList.Length > 0)
                            GiveList += ", ";

                        GiveList += Entry.Key.ObjectName;
                        if (Entry.Value > 1)
                        {
                            int MinCount = Entry.Value % 100000;
                            int MaxCount = Entry.Value / 100000;

                            if (MaxCount > 0)
                                GiveList += $" (x{MinCount}-{MaxCount})";
                            else
                                GiveList += $" (x{MinCount})";
                        }
                    }

                    string ReceiveList = string.Empty;

                    foreach (KeyValuePair<PgItem, int> Entry in Barter.ReceiveTable)
                    {
                        if (ReceiveList.Length > 0)
                            ReceiveList += ", ";

                        ReceiveList += Entry.Key.ObjectName;
                        if (Entry.Value > 1)
                        {
                            int MinCount = Entry.Value % 100000;
                            int MaxCount = Entry.Value / 100000;

                            if (MaxCount > 0)
                                ReceiveList += $" (x{MinCount}-{MaxCount})";
                            else
                                ReceiveList += $" (x{MinCount})";
                        }
                    }

                    WriteBarterLine($"  {GiveList} --> {ReceiveList}");
                }
            }
        }

        private static void WriteBarterLine(string text)
        {
            //Debug.WriteLine(text);
        }

        private static bool GetItem(TagSection sectionTag, List<PgItem> itemList, out PgItem item)
        {
            item = null;

            foreach (Tag NestedTag in sectionTag.NestedTagList)
            {
                switch (NestedTag)
                {
                    case TagSection AsNestedSectionTag:
                        if (GetItem(AsNestedSectionTag, itemList, out item))
                            return true;
                        break;
                    case TagHyperlink AsHyperlink:
                        foreach (PgItem CandidateItem in itemList)
                            if (CandidateItem.ObjectName == AsHyperlink.Content)
                            {
                                item = CandidateItem;
                                break;
                            }

                        return item != null;
                }
            }

            return false;
        }

        private static void ParseRowSpan(TagTableCell cell, ref int value)
        {
            if (cell == null)
                return;

            string Parameters = cell.Parameters;
            int StartIndex = Parameters.IndexOf("rowspan=\"");

            if (StartIndex < 0)
                return;

            StartIndex += 9;
            int EndIndex = Parameters.IndexOf("\"", StartIndex);
            if (EndIndex <= StartIndex)
                return;

            string RowSpan = Parameters.Substring(StartIndex, EndIndex - StartIndex);
            int.TryParse(RowSpan, out value);
        }

        private static bool FindBarterSection(string content, out HtmlSection barterSection)
        {
            barterSection = null;
            string Pattern = "id=\"Bartering\">Bartering";

            int StartIndex = content.IndexOf(Pattern);
            if (StartIndex < 0)
                return false;

            barterSection = HtmlSection.FromPage(content, "Give", "Receive");

            if (barterSection == null)
            {
            }

            return barterSection != null;
        }

        public static string ToWikiNpcName(string name)
        {
            Dictionary<string, string> WikiNpcNameTable = new Dictionary<string, string>()
            {
                //{ "*Flia", "Flia" },
                { "Hulon the Hoarder", "Hulon" },
            };

            if (WikiNpcNameTable.ContainsKey(name))
                return WikiNpcNameTable[name];
            else
                return name;
        }

        public static void ContentToCount(string content, out int minCount, out int maxCount)
        {
            minCount = 1;
            maxCount = 0;

            content = content.Replace("&#160;", " ");
            content = content.Trim();

            if (!content.StartsWith("x"))
                return;

            content = content.Substring(1).Trim();

            string[] Splitted = content.Split('-');

            Debug.Assert(Splitted.Length >= 1 && Splitted.Length <= 2);

            if (Splitted.Length == 1)
            {
                if (int.TryParse(Splitted[0].Trim(), out int Count))
                {
                    minCount = Count;
                    maxCount = 0;
                }
            }
            else
            {
                int.TryParse(Splitted[0].Trim(), out minCount);
                int.TryParse(Splitted[1].Trim(), out maxCount);
            }
        }

        public static bool IsNpcIgnoredInWiki(string name)
        {
            List<string> IgnoredNpcNameTable = new List<string>()
            {
                "Werewolf Altar",
                "Temporary Spider",
                "Footlocker",
                "Placeholder",
                "Akhisa's Messenger",
                "Gilded Chest",
                "Ivyn's Chest",
                "Warden Storage",
                "Guild Roster",
                "Au Shin",
                "Riger's Chest",
                "Food Work Orders",
                "Storage Chest",
                "Sturdy Chest",
                "Treasure Chest",
                "*Flia",
            };

            return IgnoredNpcNameTable.Contains(name);
        }

        public static bool IsNpcMultiBarter(string name)
        {
            List<string> IsNpcMultiBarterTable = new List<string>()
            {
                "Midge the Apothecary",
                "The Sand Seer",
                "Gretchen Salas",
                "Orran",
                "Bogaku",
                "Ukorga",
                "Helena Veilmoor",
                "Brianna Willer",
                "Mox Warcut",
                "Puffy Ben",
                "Dorimir Fangblade",
                "Torgan",
                "Urglemarg",
                "Yagreet",
                "Zeratak",
            };

            return IsNpcMultiBarterTable.Contains(name);
        }
    }
}
