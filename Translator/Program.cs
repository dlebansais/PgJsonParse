namespace Translator;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using PgJsonReader;
using PgObjects;

public class Program
{
    public static int Main(string[] args)
    {
        return ParseCurated(403);
    }

    private static int ParseCurated(int Version)
    {
        Debug.WriteLine($"Parsing version {Version}");

        VersionPath = $@"C:\Users\DLB\AppData\Roaming\PgJsonParse\Versions\{Version}";

        if (!ParseFile(Version, "abilities", typeof(PgAbility)))
            return -1;

        if (!ParseFile(Version, "advancementtables", typeof(PgAdvancementTable)))
            return -1;

        if (!ParseFile(Version, "ai", typeof(PgAI)))
            return -1;

        if (!ParseFile(Version, "areas", typeof(PgArea)))
            return -1;

        if (!ParseFile(Version, "attributes", typeof(PgAttribute)))
            return -1;

        if (!ParseFile(Version, "directedgoals", typeof(PgDirectedGoal)))
            return -1;

        if (!ParseFile(Version, "effects", typeof(PgEffect)))
            return -1;

        if (!ParseFile(Version, "items", typeof(PgItem)))
            return -1;

        if (!ParseFile(Version, "itemuses", typeof(PgItemUse)))
            return -1;

        if (!ParseFile(Version, "lorebookinfo", typeof(PgLoreBookInfo)))
            return -1;

        if (!ParseFile(Version, "lorebooks", typeof(PgLoreBook)))
            return -1;

        if (!ParseFile(Version, "npcs", typeof(PgNpc)))
            return -1;

        if (!ParseFile(Version, "playertitles", typeof(PgPlayerTitle)))
            return -1;

        if (!ParseFile(Version, "tsysclientinfo", typeof(PgPower)))
            return -1;

        if (!ParseFile(Version, "tsysprofiles", typeof(PgProfile)))
            return -1;

        if (!ParseFile(Version, "quests", typeof(PgQuest)))
            return -1;

        if (!ParseFile(Version, "recipes", typeof(PgRecipe)))
            return -1;

        if (!ParseFile(Version, "skills", typeof(PgSkill)))
            return -1;

        if (!ParseFile(Version, "sources_abilities", typeof(PgSourceEntriesAbility)))
            return -1;

        if (!ParseFile(Version, "sources_recipes", typeof(PgSourceEntriesRecipe)))
            return -1;

        if (!ParseFile(Version, "storagevaults", typeof(PgStorageVault)))
            return -1;

        if (!ParseFile(Version, "xptables", typeof(PgXpTable)))
            return -1;

        LastParsedFile = string.Empty;
        LastParsedKey = string.Empty;

        if (!FieldTableStore.VerifyTablesCompletion())
            return -1;

        Debug.WriteLine("Finalizing...");

        if (!ParsingContext.FinalizeParsing())
            return -1;

        bool FinalizingResult = true;
        FinalizingResult &= ParserAbilityRequirement.FinalizeParsing();
        FinalizingResult &= ParserQuestObjective.FinalizeParsing();
        FinalizingResult &= ParserQuestObjectiveRequirement.FinalizeParsing();
        FinalizingResult &= ParserQuestRequirement.FinalizeParsing();
        FinalizingResult &= ParserQuestReward.FinalizeParsing();
        FinalizingResult &= ParserQuestFailEffect.FinalizeParsing();
        FinalizingResult &= ParserStorageRequirement.FinalizeParsing();
        FinalizingResult &= ParserNpcService.FinalizeParsing();

        FinalizingResult &= ParserAbility.UpdateSource();
        FinalizingResult &= ParserRecipe.UpdateSource();

        Debug.WriteLine("Updating icons and names...");

        ParserAbility.UpdateIconsAndNames();
        ParserAttribute.UpdateIconsAndNames();
        ParserEffect.UpdateIconsAndNames();
        ParserPower.UpdateIconsAndNames();
        ParserProfile.UpdateIconsAndNames();
        ParserQuest.UpdateIconsAndNames();
        ParserSkill.UpdateIconsAndNames();

        ParserSkill.FillAssociationTables();

        ListUniqueQuests();

        List<object> ObjectList = ParsingContext.GetParsedObjectList();

        FindNpcBarters(ObjectList);
        FindNpcSales(ObjectList);
        FindNpcSources(ObjectList);
        FindLinks(ObjectList);

        Debug.WriteLine("Running combat parser...");

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

        Debug.WriteLine("Exporting tables...");

        ObjectList = ParsingContext.GetParsedObjectList();
        Generate.Write(Version, ObjectList);
        DownloadNewIcons(Version);

        return 0;
    }

    public static string VersionPath { get; set; } = string.Empty;
    public static string LastParsedFile { get; set; } = string.Empty;
    public static string LastParsedKey { get; set; } = string.Empty;

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

        WriteFailureLine(parsedFile, parsedKey, text, callingFileLineNumber);
    }

    public static void WriteFailureLine(string parsedFile, string parsedKey, string text, int callingFileLineNumber)
    {
        if (parsedFile.Length > 0 && parsedKey.Length > 0)
            Debug.WriteLine($"Error in '{parsedKey}' of {parsedFile}.json");
        else
            Debug.WriteLine($"Error");

        Debug.WriteLine($"{text} (Line: {callingFileLineNumber})");
    }

    private static bool ParseFile(int version, string fileName, Type itemType)
    {
        LastParsedFile = fileName;

        string FullPath = $"{VersionPath}\\Curated\\{fileName}.json";

        if (!File.Exists(FullPath))
            return false;

        using FileStream Stream = new FileStream(FullPath, FileMode.Open, FileAccess.Read);
        JsonTextReader Reader = new JsonTextReader(Stream);

        if (!FieldTableStore.GetTable(itemType, out FieldTable MainItemTable))
            return ReportFailure($"Table doesn't contain type {itemType}");

        Debug.WriteLine($"Parsing {fileName}...");

        if (!ParseFileEmbeddedObjects(Reader, itemType, MainItemTable))
            return false;

        return true;
    }

    private static bool ParseFileEmbeddedObjects(JsonTextReader reader, Type itemType, FieldTable rootItemTable)
    {
        reader.Read();

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
            FieldTable FieldTable = context.FieldTable;
            if (!FieldTable.ContainsKey(FieldName, out FieldType))
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
                if (fieldType != typeof(string) && fieldType != typeof(StringOrInteger) && fieldType != typeof(StringOrStringArray))
                {
                    string StringValue = (string)reader.CurrentValue!;
                    if (fieldType != typeof(int) || !int.TryParse(StringValue, out _))
                        return ReportFailure($"{fieldType} expected for {fieldName} but file contains a string");
                }

                if (!context.SetContent(fieldName, reader.CurrentValue, reader.CurrentToken))
                    return false;

                reader.Read();
                break;

            case Json.Token.ArrayStart:
                if (!fieldType.IsArray && fieldType != typeof(StringOrStringArray))
                    return ReportFailure($"{fieldType} expected for {fieldName} but file contains an array");

                Type ElementType = fieldType == typeof(StringOrStringArray) ? typeof(string) : fieldType.GetElementType();

                FieldTable ArrayItemTable;

                if (ElementType == typeof(bool) || ElementType == typeof(int) || ElementType == typeof(float) || ElementType == typeof(string))
                    ArrayItemTable = null!;
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

        Type ItemType = item?.GetType()!;
        PropertyInfo Property = ItemType.GetProperty("Key")!;
        Debug.Assert(Property != null && Property.PropertyType == typeof(string));

        Property?.SetValue(item, key);
    }

    private static void FindLinks(List<object> objectList)
    {
        Dictionary<string, PgObject> ObjectTable = new();

        foreach (object Item in objectList)
            if (Item is PgObject Reference)
            {
                string ItemKey = PgObject.GetItemKey(Reference);

                Debug.Assert(!ObjectTable.ContainsKey(ItemKey));

                if (!ObjectTable.ContainsKey(ItemKey))
                    ObjectTable.Add(ItemKey, Reference);
            }

        foreach (KeyValuePair<string, PgObject> Entry in ObjectTable)
        {
            PgObject Reference = Entry.Value;
            string LinkKey = PgObject.GetItemKey(Reference);

            FindLinks(LinkKey, Reference, ObjectTable);
        }
    }

    private static void FindLinks(string linkKey, object nestedItem, Dictionary<string, PgObject> objectTable)
    {
        PropertyInfo[] Properties = nestedItem.GetType().GetProperties();

        foreach (PropertyInfo Property in Properties)
        {
            if (!Property.CanWrite)
                continue;

            if (Property.Name == "AssociationListAbility" || Property.Name == "AssociationTablePower")
                continue;

            Type PropertyType = Property.PropertyType;

            if ((PropertyType == typeof(string) && !Property.Name.EndsWith("_Key")) || PropertyType.IsEnum || Generate.IsTypeIgnoredForIndex(PropertyType))
                continue;

            if (PropertyType == typeof(string) && Property.Name.EndsWith("_Key"))
            {
                string? ReferenceKey = Property.GetValue(nestedItem) as string;
                if (ReferenceKey is not null)
                    AddLinkKey(linkKey, ReferenceKey, objectTable);
            }
            else if (PropertyType == typeof(string[]) && Property.Name.EndsWith("_Keys"))
            {
                string[]? ReferenceKeys = Property.GetValue(nestedItem) as string[];
                if (ReferenceKeys is not null)
                {
                    foreach (string ReferenceKey in ReferenceKeys)
                        AddLinkKey(linkKey, ReferenceKey, objectTable);
                }
            }
            else if (PropertyType.GetInterface(typeof(IDictionary).Name) != null)
            {
                IDictionary ObjectDictionary = (IDictionary)Property.GetValue(nestedItem);
                Type DictionaryType = PropertyType.IsGenericType ? PropertyType : PropertyType.BaseType;

                Debug.Assert(DictionaryType.IsGenericType);
                Type[] GenericArguments = DictionaryType.GetGenericArguments();
                Debug.Assert(GenericArguments.Length == 2);
                Type ItemType = GenericArguments[0]!;
                if (ItemType != null)
                {
                    if (ItemType == typeof(List<float>) || ItemType.IsEnum || Generate.IsTypeIgnoredForIndex(ItemType))
                    {
                    }
                    else if (ItemType == typeof(string))
                    {
                        foreach (string ReferenceKey in ObjectDictionary.Keys)
                            AddLinkKey(linkKey, ReferenceKey, objectTable);
                    }
                    else if (ItemType.Name.StartsWith("Pg"))
                    {
                        foreach (object Key in ObjectDictionary.Keys)
                        {
                            object? Reference = ObjectDictionary[Key];

                            if (Reference is PgObject AsLinkable)
                                AddLinkKey(linkKey, AsLinkable.Key, objectTable);
                            else if (Reference is not null)
                                FindLinks(linkKey, Reference, objectTable);
                        }
                    }
                    else
                    {
                    }
                }
            }
            else if (PropertyType.GetInterface(typeof(ICollection).Name) != null)
            {
                ICollection ObjectCollection = (ICollection)Property.GetValue(nestedItem);
                Type CollectionType = PropertyType.IsGenericType ? PropertyType : PropertyType.BaseType;

                Debug.Assert(CollectionType.IsGenericType);
                Type[] GenericArguments = CollectionType.GetGenericArguments();
                Debug.Assert(GenericArguments.Length == 1);
                Type ItemType = GenericArguments[0]!;
                if (ItemType != null)
                {
                    if (ItemType.IsEnum || Generate.IsTypeIgnoredForIndex(ItemType))
                    {
                    }
                    else if (ItemType == typeof(string))
                    {
                        foreach (string ReferenceKey in ObjectCollection)
                            AddLinkKey(linkKey, ReferenceKey, objectTable);
                    }
                    else if (ItemType.Name.StartsWith("Pg"))
                    {
                        foreach (object Reference in ObjectCollection)
                        {
                            if (Reference is PgObject AsLinkable)
                                AddLinkKey(linkKey, AsLinkable.Key, objectTable);
                            else
                                FindLinks(linkKey, Reference, objectTable);
                        }
                    }
                    else
                    {
                    }
                }
            }
            else if (PropertyType.Name.StartsWith("Pg"))
            {
                object Reference = Property.GetValue(nestedItem);
                if (Reference != null)
                    FindLinks(linkKey, Reference, objectTable);
            }
            else
            {
            }
        }
    }

    private static void AddLinkKey(string linkKey, string referenceKey, Dictionary<string, PgObject> objectTable)
    {
        if (referenceKey.Length > 0 && referenceKey != "AnySkill")
        {
            //Debug.WriteLine(referenceKey);
            if (objectTable.ContainsKey(referenceKey))
            {
                PgObject Reference = objectTable[referenceKey];
                string ItemKey = PgObject.GetItemKey(Reference);

                if (ItemKey != linkKey && !Reference.LinkList.Contains(linkKey))
                    Reference.LinkList.Add(linkKey);
            }
        }
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
        WebClientTool.DownloadText(Address, null, (bool isFound, string? content) => FindSales(isFound, Address, content, false, npc, itemList), ignoreCache: false, out bool IsFound);

        if (!IsFound)
        {
            Address = $"http://wiki.projectgorgon.com/wiki/{NpcName}";
            WebClientTool.DownloadText(Address, null, (bool isFound, string? content) => FindSales(isFound, Address, content, true, npc, itemList), ignoreCache: false, out IsFound);
        }

        if (!IsFound && npc.ObjectName != "Chest")
            Debug.WriteLine($"{npc} NOT FOUND in wiki");
    }

    private static void FindSales(bool isFound, string address, string? content, bool saleSectionOnly, PgNpc npc, List<PgItem> itemList)
    {
        if (content == null || !isFound)
            return;

        if (!FindSaleSection(content, saleSectionOnly, out string SaleSection))
            return;

        int StartIndex = 0;
        List<PgItem> SaleList = new();

        for (; ;)
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
                PgItem? ItemMatch = null;

                foreach (PgItem Item in itemList)
                    if (Item.ObjectName == ItemName)
                    {
                        ItemMatch = Item;
                        break;
                    }

                if (ItemMatch != null)
                {
                    SaleList.Add(ItemMatch);
                    npc.SaleList.Add(ItemMatch.Key);
                }
                else if (!ItemName.StartsWith("Title:"))
                    Debug.WriteLine($"{ItemName} NOT FOUND, parsed from: {Line}, Npc is {npc}");
            }
            else
                Debug.WriteLine($"'{Line}' not parsed");

            StartIndex = EndIndex;
        }

        if (SaleList.Count > 0)
        {
            npc.WikiAddress = address;

            WriteSaleLine($"\n{npc.ObjectName}: {SaleList.Count} item(s) sold");
            foreach (PgItem Item in SaleList)
                WriteSaleLine($"  {Item.ObjectName}");
        }
    }

    private static void WriteSaleLine(string text)
    {
        // Debug.WriteLine(text);
    }

    private static bool FindSaleSection(string content, bool saleSectionOnly, out string saleSection)
    {
        if (saleSectionOnly)
        {
            saleSection = null!;
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
        List<PgNpc> NpcList = new List<PgNpc>();

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
                case PgNpc AsNpc:
                    NpcList.Add(AsNpc);
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
                    AddNpcSource(Source, AsSourceGift.Npc, AbilityList, RecipeList, NpcList);
                    break;
                case PgSourceHangOut AsSourceHangOut:
                    AddNpcSource(Source, AsSourceHangOut.Npc, AbilityList, RecipeList, NpcList);
                    break;
                case PgSourceItem AsSourceItem:
                    break;
                case PgSourceLearnAbility AsSourceLearnAbility:
                    break;
                case PgSourceQuest AsSourceQuest:
                    break;
                case PgSourceTraining AsSourceTraining:
                    AddNpcSource(Source, AsSourceTraining.Npc, AbilityList, RecipeList, NpcList);
                    break;
            }
        }
    }

    private static void AddNpcSource(PgSource source, PgNpcLocation location, List<PgAbility> abilityList, List<PgRecipe> recipeList, List<PgNpc> npcList)
    {
        string? Npc_Key = location.Npc_Key;
        if (Npc_Key == null)
            return;

        PgNpc? Npc = null;
        foreach (PgNpc Item in npcList)
            if (PgObject.GetItemKey(Item) == Npc_Key)
            {
                Npc = Item;
                break;
            }

        if (Npc == null)
            return;

        bool IsFound = false;
        string SourceKey = source.SourceKey;

        if (source.IsAbility)
        {
            foreach (PgAbility Item in abilityList)
                if (Item.Key == SourceKey)
                {
                    Npc.SourceAbilityList.Add(source);
                    IsFound = true;
                    break;
                }
        }

        if (source.IsRecipe)
        {
            foreach (PgRecipe Item in recipeList)
                if (Item.Key == SourceKey)
                {
                    Npc.SourceRecipeList.Add(source);
                    IsFound = true;
                    break;
                }
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
        WebClientTool.DownloadText(Address, null, (bool isFound, string? content) => FindBarters(isFound, Address, content, npc, itemList), ignoreCache: false, out bool IsFound);

        if (!IsFound && npc.ObjectName != "Chest")
            Debug.WriteLine($"{npc} NOT FOUND in wiki");
    }

    private static void FindBarters(bool isFound, string address, string? content, PgNpc npc, List<PgItem> itemList)
    {
        if (content == null || !isFound)
            return;

        if (!FindBarterSection(content, out HtmlSection BarterSection))
            return;

        List<TagTableRow> RowList = new List<TagTableRow>();
        for (int i = 1; i < BarterSection.NestedTagList.Count; i++)
        {
            TagTableRow AsRow = (TagTableRow)BarterSection.NestedTagList[i];

            RowList.Add(AsRow);
        }

        TagTableCell? GiveCell = null;
        TagTableCell? ReceiveCell = null;
        string? ReceiveText = null;
        PgNpcBarter NewBarter = null!;
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
                    ReceiveCell = (TagTableCell)Row.NestedTagList[0];
                }
                else
                {
                    GiveCell = (TagTableCell)Row.NestedTagList[0];
                    ReceiveCell = null;
                }

                if (IsMultiBarter && NewBarter != null)
                {
                    Dictionary<string, int> GiveTable = NewBarter.GiveTable;
                    Dictionary<string, int> ReceiveTable = NewBarter.ReceiveTable;

                    if (SplitGive)
                    {
                        foreach (KeyValuePair<string, int> Entry in GiveTable)
                        {
                            PgNpcBarter Barter = new PgNpcBarter();
                            Barter.GiveTable.Add(Entry.Key, Entry.Value);

                            foreach (KeyValuePair<string, int> ReceiveEntry in ReceiveTable)
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
                        foreach (KeyValuePair<string, int> Entry in GiveTable)
                            NewBarter.GiveTable.Add(Entry.Key, Entry.Value);
                    }

                    if (ReceiveCell == null)
                    {
                        foreach (KeyValuePair<string, int> Entry in ReceiveTable)
                            NewBarter.ReceiveTable.Add(Entry.Key, Entry.Value);
                    }
                }
            }
            else
            {
                GiveCell = (TagTableCell)Row.NestedTagList[0];
                //Debug.Assert(GiveCell.NestedTagList.Count > 0); TODO find out why NestedTagList.Count == 0 for Kelim

                ReceiveCell = (TagTableCell)Row.NestedTagList[1];
                if (ReceiveCell.NestedTagList.Count == 0)
                {
                    ReceiveText = ReceiveCell.Content;
                    if (ReceiveText.EndsWith("\n"))
                        ReceiveText = ReceiveText.Substring(0, ReceiveText.Length - 1);

                    ReceiveCell = null;
                }

                GiveRowSpan = 1;
                ReceiveRowSpan = 1;

                if (NewBarter != null && NewBarter.GiveTable.Count > 0 && (NewBarter.ReceiveTable.Count > 0 || NewBarter.ReceiveText is not null))
                {
                    Dictionary<string, int> GiveTable = NewBarter.GiveTable;
                    Dictionary<string, int> ReceiveTable = NewBarter.ReceiveTable;

                    if (SplitGive)
                    {
                        foreach (KeyValuePair<string, int> Entry in GiveTable)
                        {
                            PgNpcBarter Barter = new PgNpcBarter();
                            Barter.GiveTable.Add(Entry.Key, Entry.Value);

                            foreach (KeyValuePair<string, int> ReceiveEntry in ReceiveTable)
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

                    TagSection GiveSectionItem = (TagSection)GiveCell.NestedTagList[TagIndex];
                    Debug.Assert(GiveSectionItem != null);

                    PgItem Item;
                    bool IsItemFound = GetItem(GiveSectionItem!, itemList, out Item);
                    Debug.Assert(IsItemFound);

                    int MinCount = 1;
                    int MaxCount = 0;
                    if (GiveCellContent.Length > TagIndex)
                        ContentToCount(GiveCellContent[TagIndex], out MinCount, out MaxCount);
                    if (MinCount == 0)
                        MinCount = 1;

                    NewBarter?.GiveTable.Add(Item.Key, MinCount + (MaxCount * 100000));
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

                    TagSection ReceiveSectionItem = (TagSection)ReceiveCell.NestedTagList[TagIndex];
                    Debug.Assert(ReceiveSectionItem != null);

                    PgItem Item;
                    GetItem(ReceiveSectionItem!, itemList, out Item);

                    if (Item != null)
                    {
                        int MinCount = 1;
                        int MaxCount = 0;
                        if (ReceiveCellContent.Length > TagIndex)
                            ContentToCount(ReceiveCellContent[TagIndex], out MinCount, out MaxCount);
                        if (MinCount == 0)
                            MinCount = 1;

                        NewBarter?.ReceiveTable.Add(Item.Key, MinCount + (MaxCount * 100000));
                    }
                    else
                        Debug.WriteLine($"Item NOT FOUND in wiki");
                }
            }
            else if (NewBarter != null && ReceiveText != null)
            {
                NewBarter.ReceiveText = ReceiveText;
            }
        }

        if (NewBarter != null && NewBarter.GiveTable.Count > 0 && (NewBarter.ReceiveTable.Count > 0 || NewBarter.ReceiveText is not null))
        {
            if (SplitGive)
            {
                Dictionary<string, int> GiveTable = NewBarter.GiveTable;
                Dictionary<string, int> ReceiveTable = NewBarter.ReceiveTable;

                foreach (KeyValuePair<string, int> Entry in GiveTable)
                {
                    PgNpcBarter Barter = new PgNpcBarter();
                    Barter.GiveTable.Add(Entry.Key, Entry.Value);

                    foreach (KeyValuePair<string, int> ReceiveEntry in ReceiveTable)
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

                foreach (KeyValuePair<string, int> Entry in Barter.GiveTable)
                {
                    if (GiveList.Length > 0)
                        GiveList += ", ";

                    foreach (PgItem Item in itemList)
                        if (Item.Key == Entry.Key)
                        {
                            GiveList += Item.ObjectName;
                            break;
                        }

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

                foreach (KeyValuePair<string, int> Entry in Barter.ReceiveTable)
                {
                    if (ReceiveList.Length > 0)
                        ReceiveList += ", ";

                    foreach (PgItem Item in itemList)
                        if (Item.Key == Entry.Key)
                        {
                            ReceiveList += Item.ObjectName;
                            break;
                        }

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
        // Debug.WriteLine(text);
    }

    private static bool GetItem(TagSection sectionTag, List<PgItem> itemList, out PgItem item)
    {
        item = null!;

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

    private static void ParseRowSpan(TagTableCell? cell, ref int value)
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
        barterSection = null!;
        string Pattern = "id=\"Bartering\">Bartering";

        int StartIndex = content.IndexOf(Pattern);
        if (StartIndex < 0)
            return false;

        barterSection = HtmlSection.FromPage(content, "Give", "Receive")!;

        if (barterSection == null)
        {
        }

        return barterSection != null;
    }

    public static string ToWikiNpcName(string name)
    {
        Dictionary<string, string> WikiNpcNameTable = new Dictionary<string, string>()
        {
            // { "*Flia", "Flia" },
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

    public static void DownloadNewIcons(int version)
    {
        string ApplicationDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string SharedIconsFolder = Path.Combine(ApplicationDataFolder, @"PgJsonParse\Shared Icons");

        if (!Directory.Exists(SharedIconsFolder))
            Directory.CreateDirectory(SharedIconsFolder);

        string[] FileNames = Directory.GetFiles(SharedIconsFolder);
        List<int> ExistingIconIdList = new();

        foreach (string FileName in FileNames)
        {
            if (FileName.EndsWith(".txt"))
            {
                string DownloadedString = Path.GetFileNameWithoutExtension(FileName);
                if (int.TryParse(DownloadedString, out int Value) && Value >= version)
                {
                    Debug.WriteLine($"Icons for version v{version} have already been downloaded.");
                    return;
                }
            }
            else
            {
                string ShortFileName = Path.GetFileName(FileName);

                if (ShortFileName.StartsWith("icon_") && ShortFileName.EndsWith(".png"))
                {
                    string IconIdString = ShortFileName.Substring(5, ShortFileName.Length - 9);
                    if (int.TryParse(IconIdString, out int IconId))
                        ExistingIconIdList.Add(IconId);
                }
            }
        }

        Debug.WriteLine($"Icons for version v{version} have not been downloaded, starting it.");

        List<int> IdToRemoveList = new();

        foreach (int IconId in ExistingIconIdList)
            if (!Parser.IconIdList.Contains(IconId) && !IdToRemoveList.Contains(IconId))
                IdToRemoveList.Add(IconId);

        foreach (int IconId in Parser.IconIdList)
            DownloadIcon(version, SharedIconsFolder, IconId);

        Debug.WriteLine($"Download done, deleting old ones.");

        foreach (int IconId in IdToRemoveList)
        {
            string FileName = @$"{SharedIconsFolder}\icon_{IconId}.png";

            try
            {
                File.Delete(FileName);
            }
            catch
            {
            }
        }

        foreach (string FileName in FileNames)
        {
            if (FileName.EndsWith(".txt"))
            {
                try
                {
                    File.Delete(FileName);
                }
                catch
                {
                }
            }
        }

        string VersionFile = Path.Combine(SharedIconsFolder, $"{version}.txt");
        FileStream Stream = new(VersionFile, FileMode.Create, FileAccess.Write);
        StreamWriter Writer = new StreamWriter(Stream);
        Writer.WriteLine($"Last download: {DateTime.UtcNow.ToString("g", CultureInfo.InvariantCulture)}");
    }

    public static void DownloadIcon(int version, string folder, int iconId)
    {
        if (iconId == 0)
            return;

        string SourceLocation = "icons";
        string IconFileName = $"icon_{iconId}.png";
        string RequestUri = $"http://cdn.projectgorgon.com/v{version}/{SourceLocation}/{IconFileName}";
        try
        {
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(new Uri(RequestUri));
            using WebResponse Response = Request.GetResponse();
            using Stream ResponseStream = Response.GetResponseStream();
            using BinaryReader Reader = new BinaryReader(ResponseStream);
            byte[] Content = Reader.ReadBytes((int)Response.ContentLength);

            string DestinationFile = Path.Combine(folder, IconFileName);
            using FileStream Stream = new FileStream(DestinationFile, FileMode.Create, FileAccess.Write);
            using BinaryWriter Writer = new BinaryWriter(Stream);
            Writer.Write(Content);
        }
        catch (Exception e)
        {
            Debug.WriteLine($"Failed to download {IconFileName}");
            Debug.WriteLine(e.Message);
        }
    }

    private static void ListUniqueQuests()
    {
        Dictionary<string, ParsingContext> QuestParsingTable = ParsingContext.ObjectKeyTable[typeof(PgQuest)];
        Dictionary<MapAreaName, string> QuestAreas = new()
        {
            { MapAreaName.Internal_None, "Other" },
            { MapAreaName.NewbieIsland, "Anagoge island" },
            { MapAreaName.Dungeons, "Dungeons" },
            { MapAreaName.Eltibule, "Eltibule" },
            { MapAreaName.FaeRealm, "Fae Realm" },
            { MapAreaName.GazlukCaves, "Gazluk Dungeons" },
            { MapAreaName.GazlukKeep, "Gazluk Keep" },
            { MapAreaName.Gazluk, "Gazluk Plateau" },
            { MapAreaName.Desert1, "Ilmari Desert" },
            { MapAreaName.Tomb1, "Khyrulek's Crypt" },
            { MapAreaName.KurMountains, "Kur Mountains" },
            { MapAreaName.MyconianCave, "Myconian Caverns" },
            { MapAreaName.Povus, "Povus" },
            { MapAreaName.Rahu, "Rahu" },
            { MapAreaName.RahuSewer, "Rahu Sewer" },
            //{ MapAreaName.RahuSewers, "Rahu Sewers" },
            { MapAreaName.Casino, "Red Wing Casino" },
            { MapAreaName.SacredGrotto, "Sacred Grotto" },
            { MapAreaName.Serbule, "Serbule" },
            { MapAreaName.Serbule2, "Serbule Hills" },
            { MapAreaName.SunVale, "Sun Vale" },
        };

        Dictionary<MapAreaName, List<PgQuest>> QuestAreaListedTable = new();

        foreach (KeyValuePair<MapAreaName, string> QuestAreaEntry in QuestAreas)
        {
            foreach (KeyValuePair<string, ParsingContext> Entry in QuestParsingTable)
            {
                PgQuest Quest = (PgQuest)Entry.Value.Item;

                MapAreaName DisplayedLocation = Quest.DisplayedLocation;
                if (DisplayedLocation == MapAreaName.NightmareCaves)
                {
                }

                if (!QuestAreaListedTable.ContainsKey(DisplayedLocation))
                    QuestAreaListedTable.Add(DisplayedLocation, new List<PgQuest>());

                if (Quest.KeywordList.Contains(QuestKeyword.EventQuest) ||
                    Quest.KeywordList.Contains(QuestKeyword.WorkOrder))
                    continue;

                if (Quest.IsGuildQuest)
                    continue;

                if (!Quest.Name.StartsWith("Angling:"))
                {
                    if (Quest.IsAutoWrapUp)
                        continue;
                }

                if (Quest.ReuseTime != TimeSpan.Zero)
                    continue;

                string QuestName = Quest.Name;

                if (QuestName == string.Empty ||
                    QuestName.StartsWith("Event: "))
                    continue;

                if (QuestName.StartsWith("Hunting: ") ||
                    QuestName.StartsWith("Wolf: "))
                {
                }

                if (Quest.QuestObjectiveList.Count == 1 && Quest.QuestObjectiveList[0] is PgQuestObjectiveScripted)
                {
                    if (QuestName.StartsWith("Visit"))
                        continue;
                    else
                    {
                    }
                }

                PgNpcLocation? QuestNpc = Quest.QuestNpc;

                if (QuestNpc is null)
                    QuestNpc = Quest.FavorNpc;

                if (QuestNpc is null)
                    QuestNpc = Quest.QuestCompleteNpc;

                if (QuestNpc is not null && DisplayedLocation == MapAreaName.Internal_None)
                    DisplayedLocation = QuestNpc.NpcArea;

                if (Quest.Name == "Orange Proof" || Quest.Name == "The Sewer Man" || Quest.Name == "Under The Hand Is An Orb")
                    DisplayedLocation = MapAreaName.Internal_None;

                if (DisplayedLocation == QuestAreaEntry.Key)
                    QuestAreaListedTable[DisplayedLocation].Add(Quest);
            }
        }

        foreach (KeyValuePair<MapAreaName, List<PgQuest>> Entry in QuestAreaListedTable)
        {
            MapAreaName Area = Entry.Key;

            if (!KnownUniqueQuests.ContainsKey(Area))
                Debug.WriteLine($"UNKNOWN AREA: {Area}");
            else
            {
                foreach (PgQuest Quest in Entry.Value)
                    if (!KnownUniqueQuests[Area].ContainsKey(Quest.Name))
                        Debug.WriteLine($"UNKNOWN QUEST: {Quest.Name}, Area: {Area}");
            }
        }
    }

    enum QuestSpecifics
    {
        None,
        Bugged,
        NotTracked,
        DruidOnly,
        LycanOnly,
        FaeOnly,
        Hunting,
    };

    private static Dictionary<MapAreaName, Dictionary<string, QuestSpecifics>> KnownUniqueQuests = new()
    {
        {
            MapAreaName.Internal_None, new Dictionary<string, QuestSpecifics>()
            {
                { "Get Jesina's Help", QuestSpecifics.None },
                { "If Trees Could Talk", QuestSpecifics.None },
                { "Learn Oritania's Secret Info", QuestSpecifics.None },
                { "Lucky Teeth", QuestSpecifics.None },
                { "Orange Proof", QuestSpecifics.None },
                { "The Sewer Man", QuestSpecifics.None },
                { "Under The Hand Is An Orb", QuestSpecifics.None },
            }
        },
        {
            MapAreaName.NewbieIsland, new Dictionary<string, QuestSpecifics>()
            {
                { "Cheer Up Riger", QuestSpecifics.None },
                { "Find an Amethyst", QuestSpecifics.None },
                { "Find Bones", QuestSpecifics.None },
                { "Find Spoons", QuestSpecifics.None },
                { "Obelisk Math", QuestSpecifics.None },
            }
        },
        {
            MapAreaName.CarpalTunnels, new Dictionary<string, QuestSpecifics>()
            {
                { "Mana Sponges", QuestSpecifics.None },
                { "Sludge!", QuestSpecifics.None },
            }
        },
        {
            MapAreaName.Dungeons, new Dictionary<string, QuestSpecifics>()
            {
                { "Broken Globe Thing", QuestSpecifics.None },
                { "Broken Playset", QuestSpecifics.None },
                { "Dice Bunnies", QuestSpecifics.None },
                { "Fake Bombs", QuestSpecifics.None },
                { "Find Dalvos's Machine Parts", QuestSpecifics.None },
                { "Into the Kur Tower", QuestSpecifics.None },
                { "Locked Bottled Ship", QuestSpecifics.None },
                { "Lost Penguins", QuestSpecifics.None },
                { "Regma's Raging Rum", QuestSpecifics.None },
                { "Rixie's Ring Challenge", QuestSpecifics.None },
                { "Rixie's Ring Challenge 2", QuestSpecifics.None },
                { "Stargazer Alignment", QuestSpecifics.None },
            }
        },
        {
            MapAreaName.Eltibule, new Dictionary<string, QuestSpecifics>()
            {
                { "An Experiment Involving Rats", QuestSpecifics.None },
                { "Angling: Poison Slug Infestation", QuestSpecifics.None },
                { "Angling: The Scary Pike", QuestSpecifics.None },
                { "Chasing Dalvos Through the Nexus", QuestSpecifics.None },
                { "Chasing Dalvos to Eltibule", QuestSpecifics.None },
                { "Cocktails for Thimble Pete", QuestSpecifics.None },
                { "Collect Giant Snail Shells", QuestSpecifics.None },
                { "Cotton Pickin'", QuestSpecifics.None },
                { "Cream for Potatoes", QuestSpecifics.None },
                { "Delivering Phlogiston to Makara", QuestSpecifics.None },
                { "Explosive Ink for Yasinda", QuestSpecifics.None },
                { "Feathers For Yetta", QuestSpecifics.None },
                { "Finding Yasinda's Daughter", QuestSpecifics.None },
                { "Flitterbies", QuestSpecifics.None },
                { "Flowers for Dye Practice", QuestSpecifics.None },
                { "Game Night Is Calling", QuestSpecifics.None },
                { "Get Some Cotton", QuestSpecifics.None },
                { "Gratflower Millet", QuestSpecifics.None },
                { "Groupie Killing", QuestSpecifics.None },
                { "Gruzark's Works", QuestSpecifics.None },
                { "Guavas of the Land", QuestSpecifics.None },
                { "Hogan's Request", QuestSpecifics.None },
                { "Hooks", QuestSpecifics.None },
                { "James' Birthday Adventure", QuestSpecifics.None },
                { "Kar-Get on Villas Wake", QuestSpecifics.None },
                { "Kill Fey Panthers", QuestSpecifics.None },
                { "Kill Giant Mantises", QuestSpecifics.None },
                { "Kill Giant Snails", QuestSpecifics.None },
                { "Kill Goblins", QuestSpecifics.None },
                { "Kill Panthers", QuestSpecifics.None },
                { "Kill Spiders", QuestSpecifics.None },
                { "Lydia's Ghost", QuestSpecifics.None },
                { "Mandrake Root", QuestSpecifics.None },
                { "Meet with Jesina and Sarina", QuestSpecifics.None },
                { "Milk!", QuestSpecifics.None },
                { "Muntok Peppercorns", QuestSpecifics.None },
                { "Mystery Rocks", QuestSpecifics.None },
                { "Perfectly Round Pebbles", QuestSpecifics.None },
                { "Phlogiston Basics", QuestSpecifics.None },
                { "Pick-Me-Up", QuestSpecifics.None },
                { "Potato Dinner", QuestSpecifics.None },
                { "Power Potions!", QuestSpecifics.None },
                { "Preventing a Predictable Problem", QuestSpecifics.None },
                { "Quarter Hoops!", QuestSpecifics.None },
                { "Revenge For Slavery, Pt. 1", QuestSpecifics.None },
                { "Revenge For Slavery, Pt. 2", QuestSpecifics.None },
                { "Revenge For Slavery, Pt. 3", QuestSpecifics.None },
                { "Sachets of Onion Powder", QuestSpecifics.None },
                { "Safe For Travel", QuestSpecifics.None },
                { "Saltpeter for Percy Evans", QuestSpecifics.None },
                { "Sarina's Lost Clothes", QuestSpecifics.None },
                { "Save Sarina", QuestSpecifics.None },
                { "Scrap Paintings", QuestSpecifics.None },
                { "Sie's Roadside Assistance", QuestSpecifics.None },
                { "Spider Legs For Yetta", QuestSpecifics.None },
                { "Spiders Too Near The Herd", QuestSpecifics.None },
                { "Stomachs!", QuestSpecifics.None },
                { "Strips", QuestSpecifics.None },
                { "Sturdy Phlogiston", QuestSpecifics.None },
                { "Sulfur For Yetta", QuestSpecifics.None },
                { "The Mauler", QuestSpecifics.None },
                { "The Perfect Cow Hide", QuestSpecifics.None },
                { "Urak Mandibles", QuestSpecifics.None },
            }
        },
        {
            MapAreaName.FaeRealm, new Dictionary<string, QuestSpecifics>()
            {
                { "Aquamarine Gems for Solgribue", QuestSpecifics.None },
                { "Bad Memory Fixer", QuestSpecifics.None },
                { "Bad Memory Potion", QuestSpecifics.None },
                { "Barghest Snouts", QuestSpecifics.None },
                { "Blinding Wasp Removal", QuestSpecifics.None },
                { "Carnivine Teeth", QuestSpecifics.None },
                { "Dangerous Weeds", QuestSpecifics.None },
                { "Elven Sketches", QuestSpecifics.FaeOnly },
                { "Explosive Mushrooms", QuestSpecifics.None },
                { "Fae Panther Hunt", QuestSpecifics.None },
                { "Fae Rhino Horns", QuestSpecifics.None },
                { "Fashion Kittens", QuestSpecifics.FaeOnly },
                { "Felmer's First Aid", QuestSpecifics.FaeOnly },
                { "Freshest Pears", QuestSpecifics.None },
                { "Freshest Pixie's Parasols", QuestSpecifics.None },
                { "Freshest Shimmerwing Wings", QuestSpecifics.None },
                { "Freshest Stingers", QuestSpecifics.None },
                { "Gigantic Barbecue", QuestSpecifics.None },
                { "Honey-Loving Bears", QuestSpecifics.None },
                { "Icebreeze Moth Wings", QuestSpecifics.None },
                { "Just a Hop Away", QuestSpecifics.None },
                { "Just Kill 'Em", QuestSpecifics.None },
                { "Maybe For the Best", QuestSpecifics.FaeOnly },
                { "Meet the Hive", QuestSpecifics.FaeOnly },
                { "Mortal Lemons", QuestSpecifics.None },
                { "Mushroom Samples", QuestSpecifics.None },
                { "Pegast's Armor Patching", QuestSpecifics.FaeOnly },
                { "Pile of Parasol Flakes", QuestSpecifics.FaeOnly },
                { "Pile of Parasols", QuestSpecifics.FaeOnly },
                { "Plant Duty", QuestSpecifics.None },
                { "Precious Buckets of Slime", QuestSpecifics.None },
                { "Re-Awakening", QuestSpecifics.FaeOnly },
                { "Refreshment Duty", QuestSpecifics.None },
                { "Snack From The Homeland", QuestSpecifics.None },
                { "Spiders On Fire", QuestSpecifics.None },
                { "Spriggan Weed Teeth", QuestSpecifics.None },
                { "Two Thousand Potatoes", QuestSpecifics.FaeOnly },
                { "Winter Troll Extermination", QuestSpecifics.None },
            }
        },
        {
            MapAreaName.GazlukKeep, new Dictionary<string, QuestSpecifics>()
            {
                { "Contacting Melandria's Brother", QuestSpecifics.None },
                { "Melandria's Little Helper", QuestSpecifics.None },
                { "Mint for Melandria", QuestSpecifics.None },
                { "Resisting Extreme Punishment", QuestSpecifics.None },
            }
        },
        {
            MapAreaName.Gazluk, new Dictionary<string, QuestSpecifics>()
            {
                { "Lumber for Campsite Repairs", QuestSpecifics.None },
                { "Sampling Local Cinnabar", QuestSpecifics.None },
            }
        },
        {
            MapAreaName.GoblinDungeon, new Dictionary<string, QuestSpecifics>()
            {
                { "Bandage The Soul", QuestSpecifics.None },
                { "Goblin Genocide", QuestSpecifics.None },
            }
        },
        {
            MapAreaName.HogansBasement, new Dictionary<string, QuestSpecifics>()
            {
                { "Convince Ultashk to Help", QuestSpecifics.None },
                { "Gorvessa's Cheesy Demand", QuestSpecifics.None },
                { "Gribburn Wants Eel Kabobs", QuestSpecifics.None },
                { "Gribburn's Hair Pin", QuestSpecifics.None },
                { "Malvol's Calligraphy Needs", QuestSpecifics.None },
                { "Slabs for Malvol", QuestSpecifics.None },
                { "The Sexy Panther", QuestSpecifics.None },
                { "Tongues for Shoes", QuestSpecifics.None },
            }
        },
        {
            MapAreaName.Desert1, new Dictionary<string, QuestSpecifics>()
            {
                { "A Nice Stool", QuestSpecifics.None },
                { "A Stencil to Make Runes", QuestSpecifics.None },
                { "Angling: Baby Slugs Need Help!", QuestSpecifics.None },
                { "Carnelians for Urzab", QuestSpecifics.None },
                { "Fresh Carrots", QuestSpecifics.None },
                { "Geometric Rune Charts", QuestSpecifics.None },
                { "Hairballs Are Edible?!", QuestSpecifics.None },
                { "Manticore Tails Needed!", QuestSpecifics.None },
                { "The Ink For Something Amazing", QuestSpecifics.None },
                { "The Weird Prism", QuestSpecifics.None },
            }
        },
        {
            MapAreaName.Tomb1, new Dictionary<string, QuestSpecifics>()
            {
                { "Fiery Secrets", QuestSpecifics.None },
                { "Fiery Secrets, Round 2", QuestSpecifics.NotTracked },
                { "Fiery Secrets, Round 3", QuestSpecifics.NotTracked },
            }
        },
        {
            MapAreaName.KurMountains, new Dictionary<string, QuestSpecifics>()
            {
                { "A Bench For Writing", QuestSpecifics.None },
                { "Angling: Frozen... For Now", QuestSpecifics.None },
                { "Anomalous Fire Sheep", QuestSpecifics.None },
                { "Arrows for the Occasion", QuestSpecifics.None },
                { "Bear Massacre", QuestSpecifics.None },
                { "Better Chairs", QuestSpecifics.None },
                { "Check In On Gribburn", QuestSpecifics.None },
                { "Check In On Malgath", QuestSpecifics.None },
                { "Check In On Poe", QuestSpecifics.None },
                { "Check In On Syndra", QuestSpecifics.None },
                { "Cheddar of Friendship", QuestSpecifics.None },
                { "Deer Meat", QuestSpecifics.None },
                { "Defeat Bleddyn", QuestSpecifics.LycanOnly },
                { "Disguising Fox Outpost", QuestSpecifics.None },
                { "Disguising Fox Outpost, pt 2", QuestSpecifics.None },
                { "Gurki Wants Silver", QuestSpecifics.None },
                { "Ice Plane Research", QuestSpecifics.None },
                { "Ink for the Inscription", QuestSpecifics.None },
                { "Keep the Kitchen Stocked", QuestSpecifics.None },
                { "Killing Kin", QuestSpecifics.None },
                { "Lapis for the Priest", QuestSpecifics.None },
                { "Lichens for Research", QuestSpecifics.None },
                { "Magic Teeth", QuestSpecifics.None },
                { "Return to the Sacred Grotto", QuestSpecifics.None },
                { "Saltpeter for Landri the Cold", QuestSpecifics.None },
                { "Scales of a Dragon!", QuestSpecifics.None },
                { "Silver Ore for Swords", QuestSpecifics.None },
                { "Smelly Gurki", QuestSpecifics.None },
                { "So... Cold...", QuestSpecifics.None },
                { "Stool Order", QuestSpecifics.None },
                { "The Dawn Flower", QuestSpecifics.None },
                { "The Lightning Connection", QuestSpecifics.None },
                { "The Sacred Grotto Password", QuestSpecifics.None },
                { "Thin The Food Supply", QuestSpecifics.None },
                { "Trophy Wolves", QuestSpecifics.None },
                { "Ukorga's Fur Order", QuestSpecifics.None },
                { "Ultimate Woolly Comfort", QuestSpecifics.None },
                { "Werewolves", QuestSpecifics.None },
                { "Wolfsbane Needed", QuestSpecifics.None },
                { "Wool Supply", QuestSpecifics.None },
                { "Wooly Needs", QuestSpecifics.None },
            }
        },
        {
            MapAreaName.KurTower, new Dictionary<string, QuestSpecifics>()
            {
                { "Better Skulls Needed", QuestSpecifics.None },
                { "Looking at Skulls", QuestSpecifics.None },
                { "Necromancy Goblets", QuestSpecifics.None },
            }
        },
        {
            MapAreaName.MyconianCave, new Dictionary<string, QuestSpecifics>()
            {
                { "Cross-Pollination", QuestSpecifics.None },
                { "Find Poe", QuestSpecifics.None },
                { "Fixing Way's Yo-Yo", QuestSpecifics.None },
                { "Gold Ore For Poe", QuestSpecifics.None },
                { "Greta Must Die", QuestSpecifics.None },
                { "Heartshrooms", QuestSpecifics.None },
                { "Inhibiting the Sporing", QuestSpecifics.None },
                { "Iocaine Samples", QuestSpecifics.None },
                { "The Secret Ingredient", QuestSpecifics.None },
                { "Too Many Tenders", QuestSpecifics.None },
                { "Way's Lost Yo-Yo", QuestSpecifics.None },
            }
        },
        {
            MapAreaName.NewPrestonbule, new Dictionary<string, QuestSpecifics>()
            {
                { "Barghest Hunt", QuestSpecifics.None },
                { "Black Foot Morels for Hemmit", QuestSpecifics.None },
                { "Blankets From Ghosts", QuestSpecifics.None },
                { "Cave Fishing", QuestSpecifics.None },
                { "Cave Mummies", QuestSpecifics.None },
                { "Confronting Tal-Saka", QuestSpecifics.None },
                { "Confronting Urzab", QuestSpecifics.None },
                { "Deadly Yellow Crystals", QuestSpecifics.None },
                { "Fake Cinnamon", QuestSpecifics.None },
                { "Ghost Puke", QuestSpecifics.None },
                { "Grisly Polar Bear Deaths", QuestSpecifics.None },
                { "Groxmax Mushrooms for Hemmit", QuestSpecifics.None },
                { "Ice Sludge", QuestSpecifics.None },
                { "Mummy Mastermind", QuestSpecifics.None },
                { "Only The Best Handles", QuestSpecifics.None },
                { "Proof of Rahu's Involvement", QuestSpecifics.None },
                { "Puke Sweetener", QuestSpecifics.None },
                { "Safer Crystal Transport", QuestSpecifics.None },
                { "Sentimental Jewelry", QuestSpecifics.None },
                { "Steel From Ghosts", QuestSpecifics.None },
                { "The Chain of Gasu'um's Necklace", QuestSpecifics.None },
                { "The Gem in Gasu'um's Necklace", QuestSpecifics.None },
                { "The Gem in Gasu'um's Necklace (round 2)", QuestSpecifics.None },
                { "The Mushroom Man's Secret", QuestSpecifics.None },
                { "True Orcish Ore", QuestSpecifics.None },
            }
        },
        {
            MapAreaName.Povus, new Dictionary<string, QuestSpecifics>()
            {
                { "A Kick In The Whiskers", QuestSpecifics.None },
                { "Angling: Ghost Bass", QuestSpecifics.None },
                { "Angling: Old Dreadtooth", QuestSpecifics.None },
                { "Angling: Too Many Frogs", QuestSpecifics.None },
                { "Auto-Clamp Safety Blanket", QuestSpecifics.None },
                { "Beautiful Beaker Bits", QuestSpecifics.None },
                { "Better Hammer Wraps", QuestSpecifics.None },
                { "Bones For Inspection", QuestSpecifics.None },
                { "Brukal Must Die", QuestSpecifics.None },
                { "Exotic Varieties of Fungus", QuestSpecifics.None },
                { "Forging Armor from Blood", QuestSpecifics.None },
                { "Fresh Tallow For The Bees", QuestSpecifics.None },
                { "Fresh-Picked Death", QuestSpecifics.None },
                { "Hammer Steel, Hammer Brain", QuestSpecifics.None },
                { "Hunting: Kuvou", QuestSpecifics.Hunting },
                { "In Memoriam", QuestSpecifics.None },
                { "Khopesh Parts", QuestSpecifics.None },
                { "Kimeta's Key", QuestSpecifics.None },
                { "No Ordinary Blacksmith's Challenge", QuestSpecifics.None },
                { "Norala's Armor", QuestSpecifics.None },
                { "Only the Freshest Grizlark", QuestSpecifics.None },
                { "Parts For An Auto-Clamp", QuestSpecifics.None },
                { "Povus Chapter, Reporting In", QuestSpecifics.None },
                { "Prettier Knife Coatings", QuestSpecifics.None },
                { "Repurposing Old Fishing Nets", QuestSpecifics.None },
                { "Secrets of Orcish Armor (part 1)", QuestSpecifics.None },
                { "Secrets of Orcish Armor (part 2)", QuestSpecifics.None },
                { "Squid-Beasts In the River", QuestSpecifics.None },
                { "Stephie's Gorget", QuestSpecifics.None },
                { "Tending To the Root-Tenders", QuestSpecifics.None },
                { "Time For a Smoke Break", QuestSpecifics.None },
                { "Way of the Warsmith", QuestSpecifics.None },
            }
        },
        {
            MapAreaName.Rahu, new Dictionary<string, QuestSpecifics>()
            {
                { "A Ratkin Talisman for Daniel", QuestSpecifics.None },
                { "A Secret Type of Skeleton", QuestSpecifics.None },
                { "A Substitute for the Horn Bow", QuestSpecifics.None },
                { "Amutasa Needs To Stop Working Faster", QuestSpecifics.None },
                { "Amutasa Wants To Work Faster", QuestSpecifics.None },
                { "Amutasa's Metal", QuestSpecifics.None },
                { "Angling: Toxic Blooms", QuestSpecifics.None },
                { "Answers for Shirogin", QuestSpecifics.None },
                { "Aquamarine Dye for Shirogin", QuestSpecifics.None },
                { "Bear Gallbladders for Ashk", QuestSpecifics.None },
                { "Care Package from Rahu", QuestSpecifics.None },
                { "Drumskins", QuestSpecifics.None },
                { "Escaped Snail Meat", QuestSpecifics.DruidOnly },
                { "Furnishing Nishika's Restaurant", QuestSpecifics.None },
                { "Grapefish for Snacks", QuestSpecifics.None },
                { "Killing Orcs For Old Times' Sake", QuestSpecifics.None },
                { "Lac for Amutasa", QuestSpecifics.None },
                { "Nishika's Meat Request", QuestSpecifics.None },
                { "Powering The Back-Scratcher", QuestSpecifics.None },
                { "Questions for Shirogin", QuestSpecifics.None },
                { "Rabbit's Feet for Ashk", QuestSpecifics.None },
                { "Rat Tails for Ashk", QuestSpecifics.None },
                { "Repairing the Horn Bow", QuestSpecifics.None },
                { "Restringing the Horn Bow", QuestSpecifics.None },
                { "Sewer Crystal Water Samples", QuestSpecifics.DruidOnly },
                { "Spice For The General's Dish", QuestSpecifics.None },
                { "Spiderwebs for Beautiful Clothes", QuestSpecifics.None },
                { "Telka's Tail for Furlak", QuestSpecifics.None },
                { "The Eye of Fate", QuestSpecifics.None },
                { "The Knife Backlog", QuestSpecifics.None },
                { "The Missing Gown Ingredient", QuestSpecifics.None },
                { "The Perfect Rice For Weddings", QuestSpecifics.None },
            }
        },
        {
            MapAreaName.RahuSewer, new Dictionary<string, QuestSpecifics>()
            {
                { "Bogaku's Lost Notebook", QuestSpecifics.None },
                { "Cheese Testing", QuestSpecifics.None },
                { "More Miner Tests", QuestSpecifics.None },
                { "Necromancy Target Practice", QuestSpecifics.None },
                { "Necro-Rats!", QuestSpecifics.None },
                { "Punt Some Loungers", QuestSpecifics.None },
                { "Ratkin Appetizers", QuestSpecifics.None },
                { "Ratkin Gaming Habits", QuestSpecifics.None },
                { "Ratkin Snails", QuestSpecifics.None },
                { "Rattus Root, The Book Of Knowledge", QuestSpecifics.None },
                { "Sewer Chicken", QuestSpecifics.None },
                { "Some Miner Tests", QuestSpecifics.None },
                { "Underground Trees", QuestSpecifics.None },
            }
        },
        {
            MapAreaName.Casino, new Dictionary<string, QuestSpecifics>()
            {
                { "A Message For Willem", QuestSpecifics.None },
                { "Bear Meat Swirlers", QuestSpecifics.None },
                { "Better Cheese Needed", QuestSpecifics.None },
                { "Better Lac Than Lac?", QuestSpecifics.None },
                { "Cheese For Reasons", QuestSpecifics.None },
                { "Cottage Pie", QuestSpecifics.None },
                { "Cotton For Dresses", QuestSpecifics.None },
                { "Darker Than Dark", QuestSpecifics.None },
                { "Dirty Snow Water", QuestSpecifics.None },
                { "Fae Felt", QuestSpecifics.None },
                { "Fiddler's Favorite Snack", QuestSpecifics.None },
                { "Fiddler's Hoof Scrubber", QuestSpecifics.None },
                { "Fiddler's New Comb", QuestSpecifics.None },
                { "Goblin Ale", QuestSpecifics.None },
                { "Hatred Oil", QuestSpecifics.None },
                { "Keeping Meat Cold", QuestSpecifics.None },
                { "Kib Wants Fire Resistance", QuestSpecifics.None },
                { "Killing Goblins in Mandibles' Name", QuestSpecifics.None },
                { "Letting Qatik Down Gently", QuestSpecifics.None },
                { "Lucky Rabbit Feet", QuestSpecifics.None },
                { "Metal for Repairs", QuestSpecifics.None },
                { "Moss Agates for the Garden", QuestSpecifics.Bugged },
                { "NEED POTION", QuestSpecifics.None },
                { "NEED STINGER", QuestSpecifics.None },
                { "NEW CLUB", QuestSpecifics.None },
                { "NEW STOMACH", QuestSpecifics.None },
                { "Paladium Potions", QuestSpecifics.None },
                { "Paralytic Chicken Meat", QuestSpecifics.None },
                { "Probing Tavilak's History", QuestSpecifics.None },
                { "Quartz Jewelry", QuestSpecifics.None },
                { "Specialty Bread Order", QuestSpecifics.None },
                { "Still Better Cheese", QuestSpecifics.None },
                { "Sulfur Bath", QuestSpecifics.None },
                { "The Natural Food Experience", QuestSpecifics.None },
                { "The Natural Food Experience, pt 2", QuestSpecifics.None },
                { "The Natural Food Experience, pt 3", QuestSpecifics.None },
            }
        },
        {
            MapAreaName.SacredGrotto, new Dictionary<string, QuestSpecifics>()
            {
                { "A Little Guidance Goes A Long Way", QuestSpecifics.None },
                { "A Weighty Repair", QuestSpecifics.None },
                { "Checking In On Lumpfuzz", QuestSpecifics.None },
                { "Checking In On Norbert", QuestSpecifics.None },
                { "Checking In On The Suspicious Cow", QuestSpecifics.None },
                { "Checking In On Trekker", QuestSpecifics.None },
                { "Even More Parts?!", QuestSpecifics.None },
                { "Fox Drawing Lessons", QuestSpecifics.None },
                { "Fox Sewing Lessons", QuestSpecifics.None },
                { "Gears and Screws For Warden Alerts", QuestSpecifics.None },
                { "Getting Warden Alerts Working", QuestSpecifics.None },
                { "Prop Gems", QuestSpecifics.None },
                { "Rubies for Rubi", QuestSpecifics.None },
            }
        },
        {
            MapAreaName.Serbule, new Dictionary<string, QuestSpecifics>()
            {
                { "A Soothing Balm", QuestSpecifics.None },
                { "Angling: Junk Removal", QuestSpecifics.None },
                { "Angling: Invasive Species", QuestSpecifics.None },
                { "Arrowhead Delivery", QuestSpecifics.None },
                { "Bacon for Joeh", QuestSpecifics.None },
                { "Bandage for Blanche", QuestSpecifics.None },
                { "Beneath the Red Crystal", QuestSpecifics.None },
                { "Blanche Needs Acid", QuestSpecifics.None },
                { "Blanche Wants a Shroom", QuestSpecifics.None },
                { "Blanche Wants A Spoon", QuestSpecifics.None },
                { "Bloody Bandages", QuestSpecifics.None },
                { "Bone Meal", QuestSpecifics.None },
                { "Brain Bug Infestation", QuestSpecifics.None },
                { "Brains For Drugs", QuestSpecifics.None },
                { "Cabbage Time", QuestSpecifics.None },
                { "Cheese Run", QuestSpecifics.None },
                { "Collect Apples", QuestSpecifics.None },
                { "Convince Leonard", QuestSpecifics.None },
                { "Cranium Cure: Brain Bug Lobes", QuestSpecifics.None },
                { "Cranium Cure: Willpower Gel", QuestSpecifics.None },
                { "Cranium Powder", QuestSpecifics.None },
                { "Defeat the Wolf Trial", QuestSpecifics.None },
                { "Dinner For Zeratak", QuestSpecifics.None },
                { "Dinosaur Scales For Therese", QuestSpecifics.None },
                { "Feathers For Elahil", QuestSpecifics.None },
                { "Find Gravestones", QuestSpecifics.NotTracked },
                { "Find Mushrooms", QuestSpecifics.None },
                { "Flia's Odd Mushroom Request", QuestSpecifics.None },
                { "Fresh Grass for Gisli", QuestSpecifics.None },
                { "Gather More Mushrooms", QuestSpecifics.None },
                { "Get 100 Fish", QuestSpecifics.None },
                { "Get a Pig Snout", QuestSpecifics.None },
                { "Get Cat Eyeballs for Joeh", QuestSpecifics.None },
                { "Get Crystal Samples", QuestSpecifics.None },
                { "Get Some Culture", QuestSpecifics.None },
                { "Giant Mushroom Sample", QuestSpecifics.None },
                { "Graveyard Cleanup", QuestSpecifics.None },
                { "Horseshoe Order", QuestSpecifics.None },
                { "Hulon's Bribe", QuestSpecifics.None },
                { "Hunt for Preservative Runes", QuestSpecifics.None },
                { "Ivory Horn", QuestSpecifics.None },
                { "Ivyn Made a Salmpo", QuestSpecifics.None },
                { "Ivyn Needs Butter", QuestSpecifics.None },
                { "Ivyn Needs Seedlings", QuestSpecifics.None },
                { "Ivyn's Demon Bean", QuestSpecifics.None },
                { "Kill Myconians", QuestSpecifics.None },
                { "Lapis Lazuli for Marna", QuestSpecifics.None },
                { "Lumber for the Forge", QuestSpecifics.None },
                { "Message for Therese", QuestSpecifics.None },
                { "Metal Slabs", QuestSpecifics.None },
                { "New Spiky Headgear", QuestSpecifics.None },
                { "Oak For A MasterPiece", QuestSpecifics.None },
                { "Oils for Arrows", QuestSpecifics.None },
                { "Old Fangsworth", QuestSpecifics.None },
                { "Oritania's Navel Ring", QuestSpecifics.None },
                { "Perfect Tiger Skin", QuestSpecifics.None },
                { "Peridots Needed", QuestSpecifics.None },
                { "Pork Party", QuestSpecifics.None },
                { "Rita Needs Slippers", QuestSpecifics.None },
                { "Rita's Curiosity", QuestSpecifics.None },
                { "Rita's Soap", QuestSpecifics.None },
                { "Sewer Rats", QuestSpecifics.None },
                { "Sir Coth's Obsession", QuestSpecifics.None },
                { "Strange Dirt", QuestSpecifics.None },
                { "Talk to Jack", QuestSpecifics.None },
                { "Talk to Someone Who Knows", QuestSpecifics.None },
                { "Talking Mantises", QuestSpecifics.None },
                { "The Bear in There", QuestSpecifics.None },
                { "The Beautiful Topaz", QuestSpecifics.None },
                { "The Cat's Glassy Stare", QuestSpecifics.None },
                { "The Cure For Cranium Blues", QuestSpecifics.None },
                { "The Fate of Commander Ferrows", QuestSpecifics.None },
                { "The Galvanizer", QuestSpecifics.None },
                { "The Golem's Gear", QuestSpecifics.None },
                { "The Mythical Heartshroom", QuestSpecifics.None },
                { "The Nature Of Mantis Love", QuestSpecifics.None },
                { "The Rat Tiara", QuestSpecifics.None },
                { "The Red Crystal", QuestSpecifics.NotTracked },
                { "The Ring's Requirement", QuestSpecifics.None },
                { "The Second Woo", QuestSpecifics.None },
                { "The Spies", QuestSpecifics.None },
                { "The Sweet Butter Secret", QuestSpecifics.None },
                { "The Trespassing Charlatan", QuestSpecifics.None },
                { "Their Hairy Legs", QuestSpecifics.None },
                { "Therese Wants Hash Browns", QuestSpecifics.None },
                { "Those Deer Aren't Gonna Kill Themselves", QuestSpecifics.None },
                { "Un-cow-ification", QuestSpecifics.None },
                { "Unlocking the Crystal Lattice", QuestSpecifics.None },
                { "Unusual Powder", QuestSpecifics.None },
                { "Ur-Bacon", QuestSpecifics.None },
                { "Velkort Desires Grapes", QuestSpecifics.None },
                { "Velkort Needs Spore Bombs", QuestSpecifics.None },
                { "Velkort Wants A Lobe", QuestSpecifics.None },
                { "Velkort Wants Acid Claws", QuestSpecifics.None },
                { "Wooing Zeratak", QuestSpecifics.None },
            }
        },
        {
            MapAreaName.SerbuleCrypt, new Dictionary<string, QuestSpecifics>()
            {
                { "Damned Dinosaurs", QuestSpecifics.None },
                { "Deer In The Crypt", QuestSpecifics.None },
                { "Ursula's Creepy Bear", QuestSpecifics.None },
            }
        },
        {
            MapAreaName.Serbule2, new Dictionary<string, QuestSpecifics>()
            {
                { "A Crop of Onions", QuestSpecifics.None },
                { "A Scray Stinger", QuestSpecifics.None },
                { "All The Fish You Could Want", QuestSpecifics.None },
                { "Angling: Scray Overpopulation", QuestSpecifics.None },
                { "Attacking Ranalon Farms", QuestSpecifics.None },
                { "Boar Tusks For Bardic Lore", QuestSpecifics.None },
                { "Breaded Perch with Onions", QuestSpecifics.None },
                { "Citrus For Tyler", QuestSpecifics.None },
                { "Clearing the Ranalon Temple", QuestSpecifics.None },
                { "Cleo's Lost Books", QuestSpecifics.None },
                { "Cold Iron Shackles", QuestSpecifics.None },
                { "Collecting Keg Orders", QuestSpecifics.None },
                { "Cress Paper", QuestSpecifics.None },
                { "Distracting the Ranalon", QuestSpecifics.None },
                { "Eggs for The Road", QuestSpecifics.None },
                { "Grapes for Sammie", QuestSpecifics.None },
                { "Guardian Lures", QuestSpecifics.None },
                { "Kill That Wooly Bastard", QuestSpecifics.None },
                { "'Leeka and the Blade Trials' Chapter 1", QuestSpecifics.None },
                { "'Leeka and the Blade Trials' Chapter 2", QuestSpecifics.None },
                { "'Leeka and the Blade Trials' Chapter 3", QuestSpecifics.None },
                { "'Leeka and the Blade Trials' Chapter 4", QuestSpecifics.None },
                { "'Leeka and the Blade Trials' Chapter 5", QuestSpecifics.None },
                { "'Leeka and the Blade Trials' Chapter 6", QuestSpecifics.None },
                { "Meditation Stool for Gershok", QuestSpecifics.None },
                { "Merriana's Second Request", QuestSpecifics.None },
                { "Merriana's Strange Request", QuestSpecifics.None },
                { "Mushrooms for Beer", QuestSpecifics.None },
                { "Paul's Beer Additive", QuestSpecifics.None },
                { "Paul's Lucky Buckle", QuestSpecifics.None },
                { "Paul's Mycena Weakness", QuestSpecifics.None },
                { "Potato Beer?", QuestSpecifics.None },
                { "Ranalan Pickles", QuestSpecifics.None },
                { "Rat Teeth for Gershok", QuestSpecifics.None },
                { "Real Catgut", QuestSpecifics.None },
                { "Repelling a Bear", QuestSpecifics.None },
                { "Rotten Beer Needs Rotten Fruit", QuestSpecifics.None },
                { "Rubbery Tongues", QuestSpecifics.None },
                { "Scary Sheep", QuestSpecifics.None },
                { "Serbule Hills Farm Samples", QuestSpecifics.None },
                { "Shield-Repair Metal", QuestSpecifics.None },
                { "Spore Bombs for Julius", QuestSpecifics.None },
                { "Stocking Up on Travel Meds", QuestSpecifics.None },
                { "The Borghild Lead", QuestSpecifics.None },
                { "The Grapefish Roundup", QuestSpecifics.None },
                { "The Grapefish Roundup, Part 2", QuestSpecifics.None },
                { "The Logging Camp Overseers", QuestSpecifics.None },
                { "The Lost Spade", QuestSpecifics.None },
                { "The Melted Shield", QuestSpecifics.None },
                { "The Sweater", QuestSpecifics.None },
                { "Watercress Salad", QuestSpecifics.None },
                { "Why Violet Glass?", QuestSpecifics.None },
                { "Windbiter!", QuestSpecifics.None },
            }
        },
        {
            MapAreaName.SnowbloodShadowGazlukShadowCaves, new Dictionary<string, QuestSpecifics>()
            {
                { "Dissolution of Cave Snails", QuestSpecifics.None },
                { "Inconsiderate Slime Organisms", QuestSpecifics.None },
                { "The Worst Slug", QuestSpecifics.None },
            }
        },
        {
            MapAreaName.SunVale, new Dictionary<string, QuestSpecifics>()
            {
                { "A Brighter Glow", QuestSpecifics.None },
                { "A Dark Surprise", QuestSpecifics.None },
                { "Analyzing Antler", QuestSpecifics.FaeOnly },
                { "Analyzing Chitin", QuestSpecifics.FaeOnly },
                { "Angling: Killer Starfish", QuestSpecifics.None },
                { "Angling: The Overgrown Clownfish", QuestSpecifics.None },
                { "Angling: Unnaturally Cold Waters", QuestSpecifics.None },
                { "Angling: Vicious Eels", QuestSpecifics.None },
                { "Apple Juice", QuestSpecifics.None },
                { "Bananas!", QuestSpecifics.None },
                { "Blasted Vines", QuestSpecifics.None },
                { "Camembert", QuestSpecifics.None },
                { "Cleanse Sun Vale", QuestSpecifics.FaeOnly },
                { "Collect Eels", QuestSpecifics.None },
                { "Crab Soup", QuestSpecifics.FaeOnly },
                { "Encroaching Winter Camps", QuestSpecifics.None },
                { "Expert's Advice", QuestSpecifics.None },
                { "Eyes On Stalks", QuestSpecifics.None },
                { "Fake Honey For a Fake Bee", QuestSpecifics.None },
                { "Fancier Knives", QuestSpecifics.None },
                { "Fresh Grass", QuestSpecifics.None },
                { "Fresh Troll Guts", QuestSpecifics.None },
                { "Giant Oyster Invasion", QuestSpecifics.None },
                { "Lemony Dye", QuestSpecifics.None },
                { "Lost Recipe Hunt", QuestSpecifics.None },
                { "Making Do With Crap Kits", QuestSpecifics.None },
                { "Maple Dowels Make Odd Fences", QuestSpecifics.None },
                { "More Cleansing", QuestSpecifics.FaeOnly },
                { "Powdered Mammal", QuestSpecifics.None },
                { "Powdery Secretions", QuestSpecifics.None },
                { "Preta Wants a Gem Crusher", QuestSpecifics.None },
                { "Rabid Deer", QuestSpecifics.None },
                { "Ready for Note-Taking", QuestSpecifics.None },
                { "Reasonably Evil Grass", QuestSpecifics.None },
                { "Shell Fragments", QuestSpecifics.FaeOnly },
                { "Sick Spiders", QuestSpecifics.FaeOnly },
                { "Simple Knives", QuestSpecifics.None },
                { "Slabs for Yogzi", QuestSpecifics.None },
                { "Snail Armor", QuestSpecifics.None },
                { "Tasty Perch", QuestSpecifics.None },
                { "The Beauty in Predation", QuestSpecifics.FaeOnly },
                { "The Beauty in Seeds", QuestSpecifics.FaeOnly },
                { "The Boreal Fist", QuestSpecifics.None },
                { "The Bridge Bully", QuestSpecifics.None },
                { "The Deadliest Mushroom", QuestSpecifics.None },
                { "The External Resource Coordinator", QuestSpecifics.None },
                { "The Warden Test", QuestSpecifics.None },
                { "Tongues of The Ranalan", QuestSpecifics.None },
                { "Trade-In For Nice Werewolf Barding", QuestSpecifics.None },
                { "Trade-In For Nice Werewolf Champron", QuestSpecifics.None },
                { "Vegetable Masterpiece", QuestSpecifics.None },
            }
        },
        {
            MapAreaName.WinterNexus, new Dictionary<string, QuestSpecifics>()
            {
                { "Corey Wants Ogre Stomachs", QuestSpecifics.None },
                { "Corey Wants Wings", QuestSpecifics.None },
                { "Free Tortured Fairies", QuestSpecifics.None },
                { "Garnets", QuestSpecifics.None },
                { "Murdering Gorgos", QuestSpecifics.None },
            }
        },
        {
            MapAreaName.KurCaves, new Dictionary<string, QuestSpecifics>()
            {
                { "Anti-Lycanthropy Potion", QuestSpecifics.None },
                { "Orange Juice for Malgath", QuestSpecifics.None },
                { "Worm Teeth", QuestSpecifics.None },
            }
        },
    };
}
