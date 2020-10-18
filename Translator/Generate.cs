namespace Translator
{
    using PgObjects;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Reflection;

    public class Generate
    {
        public const char FieldSeparator = '§';
        public const char ObjectKeyStart = '|';
        public const char ObjectKeyEnd = '‖';

        private static Dictionary<object, int> IndexTable = new Dictionary<object, int>();

        public static void Write(int version, List<object> objectList)
        {
            string FilePath = Environment.CurrentDirectory;

            for(;;)
            {
                string Folder = Path.GetFileName(FilePath);

                if (Folder == "Debug" || Folder == "Release" || Folder == "x64" || Folder == "bin")
                    FilePath = Path.GetDirectoryName(FilePath);
                else
                    break;
            }

            FilePath = Path.GetDirectoryName(FilePath);
            FilePath = Path.GetDirectoryName(FilePath);
            FilePath = Path.Combine(FilePath, "PgObjects");
            FilePath = Path.Combine(FilePath, $"v{version}");

            RootFolder = FilePath;

            if (!Directory.Exists(RootFolder))
            {
                Directory.CreateDirectory(RootFolder);
                Directory.CreateDirectory(Path.Combine(RootFolder, "Objects"));
                Directory.CreateDirectory(Path.Combine(RootFolder, "Properties"));
                Directory.CreateDirectory(Path.Combine(RootFolder, "Tables"));
            }

            IndexTable.Clear();
            for (int ObjectIndex = 0; ObjectIndex < objectList.Count; ObjectIndex++)
                IndexTable.Add(objectList[ObjectIndex], ObjectIndex);

            WriteGroupings(version, objectList);
            Write(objectList);
            WriteKeys(objectList);
            WriteDictionaries(objectList);
            WriteIndexes(objectList);

            foreach (KeyValuePair<Type, StreamWriter> Entry in WriterTable)
            {
                using StreamWriter Writer = Entry.Value;
                Writer.WriteLine("    }");
                Writer.WriteLine("}");
            }

            foreach (KeyValuePair<Type, FileStream> Entry in StreamTable)
            {
                using FileStream Stream = Entry.Value;
            }
        }

        private static void Write(List<object> objectList)
        {
            int LastDisplay = 10;

            for (int ObjectIndex = 0; ObjectIndex < objectList.Count; ObjectIndex++)
            {
                WriteItem(objectList, ObjectIndex);

                int Percent = (int)((((float)ObjectIndex) / objectList.Count) * 100);

                if (Percent >= LastDisplay)
                {
                    Debug.WriteLine($"{Percent}%");
                    LastDisplay += 10;
                }
            }

            Debug.WriteLine($"100%");
        }

        private static void WriteKeys(List<object> objectList)
        {
            foreach (KeyValuePair<Type, StreamWriter> Entry in WriterTable)
            {
                StreamWriter Writer = Entry.Value;

                Writer.WriteLine("");
                Writer.WriteLine("        public static List<string> Keys { get { return new List<string>() {");
            }

            for (int ObjectIndex = 0; ObjectIndex < objectList.Count; ObjectIndex++)
                WriteItemToKeys(objectList, ObjectIndex);

            foreach (KeyValuePair<Type, StreamWriter> Entry in WriterTable)
            {
                StreamWriter Writer = Entry.Value;
                Writer.WriteLine("                                         }; } }");
            }
        }

        private static void WriteDictionaries(List<object> objectList)
        {
            foreach (KeyValuePair<Type, StreamWriter> Entry in WriterTable)
            {
                StreamWriter Writer = Entry.Value;
                string TypeName = Entry.Key.Name;

                Writer.WriteLine("");
                Writer.WriteLine($"        private static Dictionary<string, {TypeName}> Table = new Dictionary<string, {TypeName}>();");
                Writer.WriteLine("");
                Writer.WriteLine($"        public static {TypeName} Get(string key)");
                Writer.WriteLine($"        {{");
                Writer.WriteLine($"            if (Table.ContainsKey(key))");
                Writer.WriteLine($"                return Table[key];");
                Writer.WriteLine($"            else");
                Writer.WriteLine($"                return Get_(Table, key);");
                Writer.WriteLine($"        }}");
            }

            Dictionary<Type, Dictionary<char, IDictionary>> RecursiveTable = new Dictionary<Type, Dictionary<char, IDictionary>>();

            foreach (KeyValuePair<Type, StreamWriter> Entry in WriterTable)
                RecursiveTable.Add(Entry.Key, new Dictionary<char, IDictionary>());

            for (int ObjectIndex = 0; ObjectIndex < objectList.Count; ObjectIndex++)
                FillRecursiveTable(objectList, ObjectIndex, RecursiveTable);

            foreach (KeyValuePair<Type, StreamWriter> Entry in WriterTable)
                WriteRecursiveTable(Entry.Value, Entry.Key.Name, "_", 0, RecursiveTable[Entry.Key]);
        }

        private static void FillRecursiveTable(List<object> objectList, int objectIndex, Dictionary<Type, Dictionary<char, IDictionary>> recursiveTable)
        {
            object Item = objectList[objectIndex];
            Type Type = Item.GetType();

            PropertyInfo Property = Type.GetProperty("Key");
            if (Property == null || Property.PropertyType != typeof(string))
                return;

            string Key = Property.GetValue(Item) as string;
            Debug.Assert(Key != null);

            FillRecursiveTable(Key, objectIndex, recursiveTable[Type], 0);
        }

        private static void FillRecursiveTable(string key, int objectIndex, Dictionary<char, IDictionary> table, int rank)
        {
            char c = key[rank];

            if (!table.ContainsKey(c))
                table.Add(c, new Dictionary<char, IDictionary>());

            if (rank + 1 >= key.Length)
                table[c].Add('\0', new Dictionary<int, int>() { { 0, objectIndex } });
            else
                FillRecursiveTable(key, objectIndex, (Dictionary<char, IDictionary>)table[c], rank + 1);
        }

        private static void WriteRecursiveTable(StreamWriter writer, string typeName, string prefix, int rank, Dictionary<char, IDictionary> table)
        {
            writer.WriteLine("");
            writer.WriteLine($"        private static {typeName} Get{prefix}(Dictionary<string, {typeName}> table, string key)");
            writer.WriteLine($"        {{");

            int CaseCount = table.Count;

            if (table.ContainsKey('\0'))
            {
                int ObjectIndex = ((Dictionary<int, int>)table['\0'])[0];
                string LinkValue = ToObjectName(ObjectIndex);

                if (CaseCount > 1)
                {
                    writer.WriteLine($"            if (key.Length == {rank})");
                    writer.WriteLine($"            {{");
                    writer.WriteLine($"                table.Add(key, {LinkValue});");
                    writer.WriteLine($"                return {LinkValue};");
                    writer.WriteLine($"            }}");
                }
                else
                {
                    writer.WriteLine($"            table.Add(key, {LinkValue});");
                    writer.WriteLine($"            return {LinkValue};");
                }

                CaseCount--;
            }

            writer.WriteLine("");

            if (CaseCount > 1)
            {
                writer.WriteLine($"            switch (key[{rank}])");
                writer.WriteLine($"            {{");

                foreach (KeyValuePair<char, IDictionary> Entry in table)
                {
                    char c = Entry.Key;

                    if (c != '\0')
                    {
                        writer.WriteLine($"                case '{c}':");
                        writer.WriteLine($"                    return Get{prefix}{ToValidChar(c)}(table, key);");
                    }
                }

                writer.WriteLine($"                default:");
                writer.WriteLine($"                    return null;");
                writer.WriteLine($"            }}");
            }
            else if (CaseCount == 1)
            {
                foreach (KeyValuePair<char, IDictionary> Entry in table)
                {
                    char c = Entry.Key;

                    if (c != '\0')
                    {
                        writer.WriteLine($"            return Get{prefix}{ToValidChar(c)}(table, key);");
                    }
                }
            }
            else if (table.Count == 0)
                writer.WriteLine($"            return null;");

            writer.WriteLine($"        }}");

            foreach (KeyValuePair<char, IDictionary> Entry in table)
            {
                char c = Entry.Key;

                if (c != '\0')
                {
                    Dictionary<char, IDictionary> NextTable = (Dictionary<char, IDictionary>)Entry.Value;
                    WriteRecursiveTable(writer, typeName, $"{prefix}{ToValidChar(c)}", rank + 1, NextTable);
                }
            }
        }

        private static char ToValidChar(char c)
        {
            if (c == '*')
                return '_';
            else
                return c;
        }

        private static void WriteIndexes(List<object> objectList)
        {
            foreach (KeyValuePair<Type, StreamWriter> Entry in WriterTable)
            {
                StreamWriter Writer = Entry.Value;

                string TypeName = Entry.Key.Name;
                Writer.WriteLine("");
                Writer.WriteLine($"        public static string Index = @\"");
            }

            int LastDisplay = 10;
            Dictionary<Type, List<string>> TypeIndexTable = new Dictionary<Type, List<string>>();

            for (int ObjectIndex = 0; ObjectIndex < objectList.Count; ObjectIndex++)
            {
                GetItemIndexContent(objectList, ObjectIndex, TypeIndexTable);

                int Percent = (int)((((float)ObjectIndex) / objectList.Count) * 100);

                if (Percent >= LastDisplay)
                {
                    Debug.WriteLine($"{Percent}%");
                    LastDisplay += 10;
                }
            }

            Debug.WriteLine($"100%");

            foreach (KeyValuePair<Type, StreamWriter> Entry in WriterTable)
            {
                StreamWriter Writer = Entry.Value;
                Writer.WriteLine($"        \";");
            }

            WriteSearchIndex(TypeIndexTable);
            WriteSearchIndexResources(TypeIndexTable);
        }

        private static void WriteSearchIndex(Dictionary<Type, List<string>> typeIndexTable)
        {
            string FilePath = Path.Combine(RootFolder, "SearchIndex.cs");

            using FileStream Stream = new FileStream(FilePath, FileMode.Create, FileAccess.Write);
            using StreamWriter Writer = new StreamWriter(Stream);

            Writer.WriteLine("namespace PgObjects");
            Writer.WriteLine("{");
            Writer.WriteLine("    using System;");
            Writer.WriteLine("    using System.Collections.Generic;");
            Writer.WriteLine("    using PgObjects.Properties;");
            Writer.WriteLine("");
            Writer.WriteLine($"    public static class SearchIndex");
            Writer.WriteLine("    {");
            Writer.WriteLine($"        public const char FieldSeparator = '{FieldSeparator}';");
            Writer.WriteLine($"        public const char ObjectKeyStart = '{ObjectKeyStart}';");
            Writer.WriteLine($"        public const char ObjectKeyEnd = '{ObjectKeyEnd}';");
            Writer.WriteLine("");
            Writer.WriteLine("        public static Dictionary<Func<string, PgObject>, string> Table { get; } = new Dictionary<Func<string, PgObject>, string>()");
            Writer.WriteLine("        {");

            foreach (KeyValuePair<Type, List<string>> Entry in typeIndexTable)
            {
                if (Entry.Key.BaseType != typeof(PgObject))
                    continue;

                string ClassPrefix = ToClassName(Entry.Key);
                Writer.WriteLine($"            {{ (string key) => {ClassPrefix}.Get(key), Indexes.{ClassPrefix} }},");
            }

            Writer.WriteLine("        };");
            Writer.WriteLine("    }");
            Writer.WriteLine("}");
        }

        private static void WriteSearchIndexResources(Dictionary<Type, List<string>> typeIndexTable)
        {
            string PropertiesRootFolder = Path.Combine(RootFolder, "Properties");
            if (!Directory.Exists(PropertiesRootFolder))
                Directory.CreateDirectory(PropertiesRootFolder);

            string FilePath = Path.Combine(PropertiesRootFolder, "Indexes.resx");

            using FileStream Stream = new FileStream(FilePath, FileMode.Create, FileAccess.Write);
            using StreamWriter Writer = new StreamWriter(Stream);

            Writer.WriteLine(@"<?xml version=""1.0"" encoding=""utf-8""?>
<root>
  <xsd:schema id=""root"" xmlns="""" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:msdata=""urn:schemas-microsoft-com:xml-msdata"">
    <xsd:import namespace=""http://www.w3.org/XML/1998/namespace"" />
    <xsd:element name=""root"" msdata:IsDataSet=""true"">
      <xsd:complexType>
        <xsd:choice maxOccurs=""unbounded"">
          <xsd:element name=""metadata"">
            <xsd:complexType>
              <xsd:sequence>
                <xsd:element name=""value"" type=""xsd:string"" minOccurs=""0"" />
              </xsd:sequence>
              <xsd:attribute name=""name"" use=""required"" type=""xsd:string"" />
              <xsd:attribute name=""type"" type=""xsd:string"" />
              <xsd:attribute name=""mimetype"" type=""xsd:string"" />
              <xsd:attribute ref=""xml:space"" />
            </xsd:complexType>
          </xsd:element>
          <xsd:element name=""assembly"">
            <xsd:complexType>
              <xsd:attribute name=""alias"" type=""xsd:string"" />
              <xsd:attribute name=""name"" type=""xsd:string"" />
            </xsd:complexType>
          </xsd:element>
          <xsd:element name=""data"">
            <xsd:complexType>
              <xsd:sequence>
                <xsd:element name=""value"" type=""xsd:string"" minOccurs=""0"" msdata:Ordinal=""1"" />
                <xsd:element name=""comment"" type=""xsd:string"" minOccurs=""0"" msdata:Ordinal=""2"" />
              </xsd:sequence>
              <xsd:attribute name=""name"" type=""xsd:string"" use=""required"" msdata:Ordinal=""1"" />
              <xsd:attribute name=""type"" type=""xsd:string"" msdata:Ordinal=""3"" />
              <xsd:attribute name=""mimetype"" type=""xsd:string"" msdata:Ordinal=""4"" />
              <xsd:attribute ref=""xml:space"" />
            </xsd:complexType>
          </xsd:element>
          <xsd:element name=""resheader"">
            <xsd:complexType>
              <xsd:sequence>
                <xsd:element name=""value"" type=""xsd:string"" minOccurs=""0"" msdata:Ordinal=""1"" />
              </xsd:sequence>
              <xsd:attribute name=""name"" type=""xsd:string"" use=""required"" />
            </xsd:complexType>
          </xsd:element>
        </xsd:choice>
      </xsd:complexType>
    </xsd:element>
  </xsd:schema>
  <resheader name=""resmimetype"">
    <value>text/microsoft-resx</value>
  </resheader>
  <resheader name=""version"">
    <value>2.0</value>
  </resheader>
  <resheader name=""reader"">
    <value>System.Resources.ResXResourceReader, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>
  </resheader>
  <resheader name=""writer"">
    <value>System.Resources.ResXResourceWriter, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>
  </resheader>");

            foreach (KeyValuePair<Type, List<string>> Entry in typeIndexTable)
            {
                if (Entry.Key.BaseType != typeof(PgObject))
                    continue;

                string ClassPrefix = ToClassName(Entry.Key);
                Writer.WriteLine($"  <data name=\"{ClassPrefix}\" xml:space=\"preserve\">");
                Writer.WriteLine($"    <value>");

                foreach (string Line in Entry.Value)
                    Writer.WriteLine($"{Line}\r\n");

                Writer.WriteLine($"</value>");
                Writer.WriteLine($"  </data>");
            }

            Writer.WriteLine("</root>");
        }

        private static Dictionary<Type, FileStream> StreamTable = new Dictionary<Type, FileStream>();
        private static Dictionary<Type, StreamWriter> WriterTable = new Dictionary<Type, StreamWriter>();
        private static string RootFolder;

        private static void AddTypeFile(Type type, out StreamWriter writer)
        {
            if (WriterTable.ContainsKey(type))
            {
                writer = WriterTable[type];
                writer.WriteLine("");
                return;
            }

            string ClassName = ToClassName(type);
            string FileFolderPath = Path.Combine(RootFolder, "Tables");
            string FilePath = Path.Combine(FileFolderPath, $"{ClassName}.cs");

            FileStream Stream = new FileStream(FilePath, FileMode.Create, FileAccess.Write);
            writer = new StreamWriter(Stream);

            writer.WriteLine("namespace PgObjects");
            writer.WriteLine("{");
            writer.WriteLine("    using System;");
            writer.WriteLine("    using System.Collections.Generic;");
            writer.WriteLine("");
            writer.WriteLine($"    public static class {ClassName}");
            writer.WriteLine("    {");

            StreamTable.Add(type, Stream);
            WriterTable.Add(type, writer);
        }

        private static void WriteItemProlog(StreamWriter writer, string objectTypeName, string objectName)
        {
            writer.WriteLine($"        public static {objectTypeName} {objectName}");
            writer.WriteLine($"        {{");
            writer.WriteLine($"            get");
            writer.WriteLine($"            {{");
            writer.WriteLine($"                if (_{objectName} == null)");
            writer.WriteLine($"                {{");
            writer.WriteLine($"                    _{objectName} = new {objectTypeName}();");
            writer.WriteLine("");
        }

        private static void WriteItemEpilog(StreamWriter writer, string objectTypeName, string objectName)
        {
            writer.WriteLine($"                }}");
            writer.WriteLine("");
            writer.WriteLine($"                return _{objectName};");
            writer.WriteLine($"            }}");
            writer.WriteLine($"        }}");
            writer.WriteLine($"        private static {objectTypeName} _{objectName};");
        }

        private static void WriteItem(List<object> objectList, int objectIndex)
        {
            object Item = objectList[objectIndex];
            Type Type = Item.GetType();

            if (Item is PgModEffect AsModEffect)
            {
            }

            if (Item is PgCombatEffect AsCombatEffect)
            {
            }

            AddTypeFile(Type, out StreamWriter Writer);

            PropertyInfo[] Properties = Type.GetProperties();
            string ObjectTypeName = SimpleTypeName(Type);
            string ObjectName = ToObjectName(objectIndex);

            WriteItemProlog(Writer, ObjectTypeName, ObjectName);

            foreach (PropertyInfo Property in Properties)
            {
                if (!Property.CanWrite)
                    continue;

                string PropertyName = Property.Name;
                Type PropertyType = Property.PropertyType;
                object PropertyValue = Property.GetValue(Item);
                string ValueString = GetValueString(PropertyType, PropertyValue, objectList);

                Writer.WriteLine($"                    _{ObjectName}.{PropertyName} = {ValueString};");
            }

            WriteItemEpilog(Writer, ObjectTypeName, ObjectName);
        }

        private static void WriteItemToKeys(List<object> objectList, int objectIndex)
        {
            object Item = objectList[objectIndex];
            Type Type = Item.GetType();

            PropertyInfo Property = Type.GetProperty("Key");
            if (Property == null || Property.PropertyType != typeof(string))
                return;

            string Key = Property.GetValue(Item) as string;
            Debug.Assert(Key != null);

            string KeyValue = GetStringValueString(Key);
            StreamWriter Writer = WriterTable[Type];

            Writer.WriteLine($"                                             {KeyValue}, ");
        }

        private static void WriteItemToDictionary(List<object> objectList, int objectIndex)
        {
            object Item = objectList[objectIndex];
            Type Type = Item.GetType();

            PropertyInfo Property = Type.GetProperty("Key");
            if (Property == null || Property.PropertyType != typeof(string))
                return;

            string Key = Property.GetValue(Item) as string;
            Debug.Assert(Key != null);

            string KeyValue = GetStringValueString(Key);
            string LinkValue = ToObjectName(objectIndex);

            Debug.Assert(WriterTable.ContainsKey(Type));
            StreamWriter Writer = WriterTable[Type];

            Writer.WriteLine($"                    case {KeyValue}:");
            Writer.WriteLine($"                        Add_{LinkValue}(Table, key);");
            Writer.WriteLine($"                        break;");
        }

        private static void WriteItemAdder(List<object> objectList, int objectIndex)
        {
            object Item = objectList[objectIndex];
            Type Type = Item.GetType();

            PropertyInfo Property = Type.GetProperty("Key");
            if (Property == null || Property.PropertyType != typeof(string))
                return;

            string Key = Property.GetValue(Item) as string;
            Debug.Assert(Key != null);

            string LinkValue = ToObjectName(objectIndex);

            Debug.Assert(WriterTable.ContainsKey(Type));
            StreamWriter Writer = WriterTable[Type];
            string TypeName = Type.Name;

            Writer.WriteLine("");
            Writer.WriteLine($"        public static void Add_{LinkValue}(Dictionary<string, {TypeName}> table, string key)");
            Writer.WriteLine("        {");
            Writer.WriteLine($"            table.Add(key, {LinkValue});");
            Writer.WriteLine("        }");
        }

        private static void GetItemIndexContent(List<object> objectList, int objectIndex, Dictionary<Type, List<string>> typeIndexTable)
        {
            object Item = objectList[objectIndex];
            Type Type = Item.GetType();

            if (Type.GetProperty("Key") == null)
                return;

            if (!typeIndexTable.ContainsKey(Type))
                typeIndexTable.Add(Type, new List<string>());

            if (GetObjectIndexContent(Item, Type, 2, out string Key, out string Content))
            {
                Debug.Assert(Key != null);

                typeIndexTable[Type].Add($"{Content}{ObjectKeyStart}{Key}{ObjectKeyEnd}");
            }
        }

        private static bool GetObjectIndexContent(object item, Type type, int recursion, out string key, out string content)
        {
            key = null;
            content = string.Empty;

            if (recursion == 0)
                return false;

            PropertyInfo[] Properties = type.GetProperties();
            foreach (PropertyInfo Property in Properties)
            {
                if (!Property.CanWrite)
                    continue;
                if (Property.Name == "AssociationListAbility" || Property.Name == "AssociationTablePower")
                    continue;

                Type PropertyType = Property.PropertyType;
                if (PropertyType == typeof(string))
                {
                    string StringValue = Property.GetValue(item) as string;
                    Debug.Assert(StringValue != null);

                    if (Property.Name == "Key")
                        key = StringValue;
                    else if (Property.Name == "SourceKey")
                    {
                    }
                    else
                        content += GeStringIndexContent(StringValue);
                }
                else if (PropertyType.BaseType == typeof(PgObject))
                {
                    PgObject ObjectValue = Property.GetValue(item) as PgObject;
                    if (ObjectValue != null)
                        content += GetPgObjectIndexContent(ObjectValue);
                }
                else if (PropertyType.IsEnum)
                {
                    content += GetEnumIndexContent(PropertyType, Property.GetValue(item));
                }
                else if (IsTypeIgnoredForIndex(PropertyType))
                {
                }
                else if (PropertyType.GetInterface(typeof(IDictionary).Name) != null)
                {
                }
                else if (PropertyType.GetInterface(typeof(ICollection).Name) != null)
                {
                    ICollection ObjectCollection = Property.GetValue(item) as ICollection;
                    Type CollectionType = PropertyType.IsGenericType ? PropertyType : PropertyType.BaseType;

                    Debug.Assert(CollectionType.IsGenericType);
                    Type[] GenericArguments = CollectionType.GetGenericArguments();
                    Debug.Assert(GenericArguments.Length == 1);
                    Type ItemType = GenericArguments[0];
                    Debug.Assert(ItemType != null);

                    if (ItemType.BaseType == typeof(PgObject))
                    {
                        foreach (PgObject ObjectValue in ObjectCollection)
                        {
                            Debug.Assert(ObjectValue != null);
                            content += GetPgObjectIndexContent(ObjectValue);
                        }
                    }
                    else if (ItemType.IsEnum)
                    {
                        foreach (object EnumValue in ObjectCollection)
                            content += GetEnumIndexContent(ItemType, EnumValue);
                    }
                    else if (ItemType == typeof(string))
                    {
                        foreach (string StringValue in ObjectCollection)
                            content += GeStringIndexContent(StringValue);
                    }
                    else if (ItemType.Name.StartsWith("Pg"))
                    {
                        foreach (object ItemValue in ObjectCollection)
                            content += GetReferenceIndexContent(ItemValue.GetType(), ItemValue, recursion);
                    }
                    else if (IsTypeIgnoredForIndex(ItemType))
                    {
                    }
                    else
                    {
                    }
                }
                else if (PropertyType.Name.StartsWith("Pg"))
                {
                    content += GetReferenceIndexContent(PropertyType, Property.GetValue(item), recursion);
                }
                else
                {
                }
            }

            return content.Length > 0;
        }

        public static bool IsTypeIgnoredForIndex(Type type)
        {
            return (type == typeof(int) | type == typeof(int?) ||
                    type == typeof(uint) | type == typeof(uint?) ||
                    type == typeof(float) | type == typeof(float?) ||
                    type == typeof(bool) | type == typeof(bool?) ||
                    type == typeof(TimeSpan) | type == typeof(TimeSpan?));
        }

        private static string GeStringIndexContent(string value)
        {
            value = value.Replace("\n", "\\n");
            value = value.Replace("&", "&#38;");
            value = value.Replace("<", "&lt;");
            value = value.Replace(">", "&gt;");

            return $"{value}{FieldSeparator}";
        }

        private static string GetPgObjectIndexContent(PgObject item)
        {
            string ItemName = item.ToString();
            return GeStringIndexContent(ItemName);
        }

        private static string GetReferenceIndexContent(Type referenceType, object value, int recursion)
        {
            if (value != null && GetObjectIndexContent(value, referenceType, recursion - 1, out string _, out string ValueContent))
                return ValueContent;
            else
                return string.Empty;
        }

        private static string GetEnumIndexContent(Type enumType, object value)
        {
            Debug.Assert(StringToEnumConversion.KnownParsedEnumtable.ContainsKey(enumType));

            string EnumTextMapName = $"{enumType.Name}TextMap";
            PropertyInfo EnumTextMapProperty = typeof(TextMaps).GetProperty(EnumTextMapName);
            if (EnumTextMapProperty != null)
            {
                IDictionary EnumTextMap = EnumTextMapProperty.GetValue(null) as IDictionary;
                Debug.Assert(EnumTextMap != null);
                string EnumText = EnumTextMap[value] as string;
                if (EnumText != null)
                    return GeStringIndexContent(EnumText);
                else
                    return string.Empty;
            }
            else
                return string.Empty;
        }

        private static void WriteGroupings(int version, List<object> objectList)
        {
            if (!Directory.Exists(RootFolder))
                Directory.CreateDirectory(RootFolder);

            string FilePath = Path.Combine(RootFolder, "Grouping.cs");

            using FileStream Stream = new FileStream(FilePath, FileMode.Create, FileAccess.Write);
            using StreamWriter Writer = new StreamWriter(Stream);

            Writer.WriteLine("namespace PgObjects");
            Writer.WriteLine("{");
            Writer.WriteLine("    using System;");
            Writer.WriteLine("    using System.Collections.Generic;");
            Writer.WriteLine("");
            Writer.WriteLine($"    public static class Groups");
            Writer.WriteLine("    {");
            Writer.WriteLine($"        public static string Version {{ get; }} = \"v{version}\";");
            Writer.WriteLine("");

            WriteGroupingList(objectList, Writer, "CombatSkill", GetCombatSkillList);
            WriteGroupingDictionary(objectList, Writer, typeof(string), "CombatSubskill", GetCombatSubskillTable);
            WriteGroupingDictionary(objectList, Writer, typeof(ItemSlot), "ItemBySlot", GetItemBySlotTable);
            WriteGroupingDictionary(objectList, Writer, typeof(ItemSlot), "ShamanicInfusionPowerBySlot", GetShamanicInfusionPowerBySlotTable);
            WriteGroupingDictionary(objectList, Writer, typeof(ItemSlot), "EndurancePowerBySlot", GetEndurancePowerBySlotTable);
            WriteGroupingDictionary(objectList, Writer, typeof(ItemSlot), "ArmorPatchingPowerBySlot", GetArmorPatchingPowerBySlotTable);
            WriteGroupingDictionary(objectList, Writer, typeof(ItemSlot), "AnySkillPowerBySlot", GetAnySkillPowerBySlotTable);
            WriteGroupingDictionary(objectList, Writer, typeof(ItemKeyword), "ItemByKeyword", GetItemByKeywordTable);
            WriteGroupingDictionary(objectList, Writer, typeof(RecipeKeyword), "RecipeByKeyword", GetRecipeByKeywordTable);
            WriteGroupingDictionary(objectList, Writer, typeof(EffectKeyword), "EffectByKeyword", GetEffectByKeywordTable);
            WriteGroupingDictionary(objectList, Writer, typeof(AbilityKeyword), "AbilityByKeyword", GetAbilityByKeywordTable);
            WriteGroupingDictionary(objectList, Writer, typeof(InteractionFlag), "QuestByInteractionFlag", GetQuestByInteractionFlagTable);
            WriteGroupingDictionary(objectList, Writer, typeof(RecipeItemKey), "ItemByRecipeKey", GetItemByRecipeKeyTable);
            WriteGroupingList(objectList, Writer, "Buff", GetBuffList);

            Writer.WriteLine("    }");
            Writer.WriteLine("}");
        }

        private static List<string> GetCombatSkillList(List<object> objectList)
        {
            List<string> KeyList = new List<string>();

            foreach (object Item in objectList)
                if (Item is PgSkill AsSkill && IsCombatSkill(AsSkill))
                    KeyList.Add(AsSkill.Key);

            return KeyList;
        }

        private static List<string> GetBuffList(List<object> objectList)
        {
            List<string> KeyList = new List<string>();

            foreach (object Item in objectList)
                if (Item is PgModEffect ModEffect && ModEffect.EffectKey.Length > 0 && ModEffect.EffectKey == ModEffect.Key && ParsingContext.ObjectKeyTable[typeof(PgEffect)].ContainsKey(ModEffect.EffectKey))
                    KeyList.Add(ModEffect.EffectKey);

            return KeyList;
        }

        private static Dictionary<object, List<string>> GetCombatSubskillTable(List<object> objectList)
        {
            List<PgSkill> SubskillList = new List<PgSkill>();
            List<PgSkill> CombatSkillList = new List<PgSkill>();
            Dictionary<object, List<string>> KeyTable = new Dictionary<object, List<string>>();

            foreach (object Item in objectList)
                if (Item is PgSkill AsSkill)
                {
                    if (IsCombatSkill(AsSkill))
                        CombatSkillList.Add(AsSkill);

                    if (IsCombatSubskill(AsSkill))
                    {
                        SubskillList.Add(AsSkill);
                        KeyTable.Add(AsSkill.Key, new List<string>());
                    }
                }

            foreach (PgSkill Subskill in SubskillList)
                foreach (PgSkill Skill in CombatSkillList)
                    if (Subskill.ParentSkillList.Contains(Skill))
                        KeyTable[Subskill.Key].Add(Skill.Key);

            return KeyTable;
        }

        public static bool IsCombatSkill(PgSkill skill)
        {
            return (skill.IsCombatSkill || skill.ParentSkillList.Exists((PgSkill item) => IsCombatSkill(item))) && skill.AssociationTablePower.Count > 0;
        }

        private static bool IsCombatSubskill(PgSkill skill)
        {
            return skill.ParentSkillList.Exists((PgSkill item) => IsCombatSkill(item)) && skill.AssociationTablePower.Count > 0;
        }

        private static Dictionary<object, List<string>> GetItemBySlotTable(List<object> objectList)
        {
            Dictionary<ItemSlot, List<PgItem>> ItemTable = new Dictionary<ItemSlot, List<PgItem>>();
            Dictionary<object, List<string>> KeyTable = new Dictionary<object, List<string>>();

            foreach (object Item in objectList)
                if (Item is PgItem AsItem)
                {
                    ItemSlot Slot = AsItem.EquipSlot;

                    if (Slot != ItemSlot.Internal_None)
                    {
                        if (Slot == ItemSlot.OffHandShield)
                            Slot = ItemSlot.OffHand;

                        if (!ItemTable.ContainsKey(Slot))
                        {
                            ItemTable.Add(Slot, new List<PgItem>());
                            KeyTable.Add(Slot, new List<string>());
                        }

                        ItemTable[Slot].Add(AsItem);
                    }
                }

            foreach (KeyValuePair<ItemSlot, List<PgItem>> Entry in ItemTable)
            {
                ItemSlot Slot = Entry.Key;

                Entry.Value.Sort(SortItemByName);

                foreach (PgItem Item in Entry.Value)
                    KeyTable[Slot].Add(Item.Key);
            }

            return KeyTable;
        }

        private static Dictionary<object, List<string>> GetItemByKeywordTable(List<object> objectList)
        {
            Dictionary<ItemKeyword, List<PgItem>> ItemTable = new Dictionary<ItemKeyword, List<PgItem>>();
            Dictionary<object, List<string>> KeyTable = new Dictionary<object, List<string>>();

            foreach (object Item in objectList)
                if (Item is PgItem AsItem)
                {
                    foreach (KeyValuePair<ItemKeyword, List<float>> Entry in AsItem.KeywordTable)
                    {
                        ItemKeyword Keyword = Entry.Key;

                        if (!ItemTable.ContainsKey(Keyword))
                        {
                            ItemTable.Add(Keyword, new List<PgItem>());
                            KeyTable.Add(Keyword, new List<string>());
                        }

                        ItemTable[Keyword].Add(AsItem);
                    }
                }

            foreach (KeyValuePair<ItemKeyword, List<PgItem>> Entry in ItemTable)
            {
                ItemKeyword Keyword = Entry.Key;

                Entry.Value.Sort(SortItemByName);

                foreach (PgItem Item in Entry.Value)
                    KeyTable[Keyword].Add(Item.Key);
            }

            return KeyTable;
        }

        private static Dictionary<object, List<string>> GetItemByRecipeKeyTable(List<object> objectList)
        {
            Dictionary<RecipeItemKey, List<PgItem>> ItemTable = new Dictionary<RecipeItemKey, List<PgItem>>();
            Dictionary<object, List<string>> KeyTable = new Dictionary<object, List<string>>();

            foreach (object Item in objectList)
                if (Item is PgItem AsItem)
                {
                    List<RecipeItemKey> RecipeItemKeyList = new List<RecipeItemKey>(AsItem.RecipeItemKeyList);

                    ItemSlot Slot = AsItem.EquipSlot;
                    if (Slot != ItemSlot.Internal_None)
                    {
                        if (Slot == ItemSlot.OffHandShield)
                            Slot = ItemSlot.OffHand;

                        RecipeItemKey SlotRecipeKey = RecipeItemKey.Internal_None;

                        switch (Slot)
                        {
                            case ItemSlot.MainHand:
                                SlotRecipeKey = RecipeItemKey.EquipmentSlot_MainHand;
                                break;
                            case ItemSlot.OffHand:
                                SlotRecipeKey = RecipeItemKey.EquipmentSlot_OffHand;
                                break;
                            case ItemSlot.Hands:
                                SlotRecipeKey = RecipeItemKey.EquipmentSlot_Hands;
                                break;
                            case ItemSlot.Chest:
                                SlotRecipeKey = RecipeItemKey.EquipmentSlot_Chest;
                                break;
                            case ItemSlot.Legs:
                                SlotRecipeKey = RecipeItemKey.EquipmentSlot_Legs;
                                break;
                            case ItemSlot.Head:
                                SlotRecipeKey = RecipeItemKey.EquipmentSlot_Head;
                                break;
                            case ItemSlot.Feet:
                                SlotRecipeKey = RecipeItemKey.EquipmentSlot_Feet;
                                break;
                            case ItemSlot.Ring:
                                SlotRecipeKey = RecipeItemKey.EquipmentSlot_Ring;
                                break;
                            case ItemSlot.Necklace:
                                SlotRecipeKey = RecipeItemKey.EquipmentSlot_Necklace;
                                break;
                        }

                        RecipeItemKeyList.Add(SlotRecipeKey);
                    }

                    foreach (RecipeItemKey Keyword in RecipeItemKeyList)
                    {
                        bool Ignore = false;

                        switch (Keyword)
                        {
                            case RecipeItemKey.Rarity_Common:
                            case RecipeItemKey.Rarity_Uncommon:
                            case RecipeItemKey.Rarity_Rare:
                            case RecipeItemKey.Rarity_Exceptional:
                            case RecipeItemKey.MinRarity_Uncommon:
                            case RecipeItemKey.MinRarity_Rare:
                            case RecipeItemKey.MinRarity_Exceptional:
                            case RecipeItemKey.MinRarity_Epic:
                            case RecipeItemKey.MinTSysPrereq_0:
                            case RecipeItemKey.MinTSysPrereq_31:
                            case RecipeItemKey.MinTSysPrereq_61:
                            case RecipeItemKey.MaxTSysPrereq_30:
                            case RecipeItemKey.MaxTSysPrereq_60:
                            case RecipeItemKey.MaxTSysPrereq_90:
                                Ignore = true;
                                break;
                        }

                        if (Ignore)
                            continue;

                        if (!ItemTable.ContainsKey(Keyword))
                        {
                            ItemTable.Add(Keyword, new List<PgItem>());
                            KeyTable.Add(Keyword, new List<string>());
                        }

                        ItemTable[Keyword].Add(AsItem);
                    }
                }

            foreach (KeyValuePair<RecipeItemKey, List<PgItem>> Entry in ItemTable)
            {
                RecipeItemKey Keyword = Entry.Key;

                Entry.Value.Sort(SortItemByName);

                foreach (PgItem Item in Entry.Value)
                    KeyTable[Keyword].Add(Item.Key);
            }

            return KeyTable;
        }

        private static int SortItemByName(PgItem item1, PgItem item2)
        {
            return string.Compare(item1.Name, item2.Name, StringComparison.InvariantCulture);
        }

        private static Dictionary<object, List<string>> GetRecipeByKeywordTable(List<object> objectList)
        {
            Dictionary<RecipeKeyword, List<PgRecipe>> RecipeTable = new Dictionary<RecipeKeyword, List<PgRecipe>>();
            Dictionary<object, List<string>> KeyTable = new Dictionary<object, List<string>>();

            foreach (object Recipe in objectList)
                if (Recipe is PgRecipe AsRecipe)
                {
                    foreach (RecipeKeyword Keyword in AsRecipe.KeywordList)
                    {
                        if (!RecipeTable.ContainsKey(Keyword))
                        {
                            RecipeTable.Add(Keyword, new List<PgRecipe>());
                            KeyTable.Add(Keyword, new List<string>());
                        }

                        RecipeTable[Keyword].Add(AsRecipe);
                    }
                }

            foreach (KeyValuePair<RecipeKeyword, List<PgRecipe>> Entry in RecipeTable)
            {
                RecipeKeyword Keyword = Entry.Key;

                Entry.Value.Sort(SortRecipeByName);

                foreach (PgRecipe Recipe in Entry.Value)
                    KeyTable[Keyword].Add(Recipe.Key);
            }

            return KeyTable;
        }

        private static int SortRecipeByName(PgRecipe recipe1, PgRecipe recipe2)
        {
            return string.Compare(recipe1.Name, recipe2.Name, StringComparison.InvariantCulture);
        }

        private static Dictionary<object, List<string>> GetEffectByKeywordTable(List<object> objectList)
        {
            Dictionary<EffectKeyword, List<PgEffect>> EffectTable = new Dictionary<EffectKeyword, List<PgEffect>>();
            Dictionary<object, List<string>> KeyTable = new Dictionary<object, List<string>>();

            foreach (object Effect in objectList)
                if (Effect is PgEffect AsEffect)
                {
                    foreach (EffectKeyword Keyword in AsEffect.KeywordList)
                    {
                        if (!EffectTable.ContainsKey(Keyword))
                        {
                            EffectTable.Add(Keyword, new List<PgEffect>());
                            KeyTable.Add(Keyword, new List<string>());
                        }

                        EffectTable[Keyword].Add(AsEffect);
                    }
                }

            foreach (KeyValuePair<EffectKeyword, List<PgEffect>> Entry in EffectTable)
            {
                EffectKeyword Keyword = Entry.Key;

                Entry.Value.Sort(SortEffectByName);

                foreach (PgEffect Effect in Entry.Value)
                    KeyTable[Keyword].Add(Effect.Key);
            }

            return KeyTable;
        }

        private static int SortEffectByName(PgEffect effect1, PgEffect effect2)
        {
            return string.Compare(effect1.Name, effect2.Name, StringComparison.InvariantCulture);
        }

        private static Dictionary<object, List<string>> GetAbilityByKeywordTable(List<object> objectList)
        {
            Dictionary<AbilityKeyword, List<PgAbility>> AbilityTable = new Dictionary<AbilityKeyword, List<PgAbility>>();
            Dictionary<object, List<string>> KeyTable = new Dictionary<object, List<string>>();

            foreach (object Ability in objectList)
                if (Ability is PgAbility AsAbility)
                {
                    foreach (AbilityKeyword Keyword in AsAbility.KeywordList)
                    {
                        if (!AbilityTable.ContainsKey(Keyword))
                        {
                            AbilityTable.Add(Keyword, new List<PgAbility>());
                            KeyTable.Add(Keyword, new List<string>());
                        }

                        AbilityTable[Keyword].Add(AsAbility);
                    }
                }

            foreach (KeyValuePair<AbilityKeyword, List<PgAbility>> Entry in AbilityTable)
            {
                AbilityKeyword Keyword = Entry.Key;

                Entry.Value.Sort(SortAbilityByName);

                foreach (PgAbility Ability in Entry.Value)
                    KeyTable[Keyword].Add(Ability.Key);
            }

            return KeyTable;
        }

        private static int SortAbilityByName(PgAbility ability1, PgAbility ability2)
        {
            return string.Compare(ability1.Name, ability2.Name, StringComparison.InvariantCulture);
        }

        private static Dictionary<object, List<string>> GetQuestByInteractionFlagTable(List<object> objectList)
        {
            Dictionary<InteractionFlag, List<PgQuest>> QuestTable = new Dictionary<InteractionFlag, List<PgQuest>>();
            Dictionary<object, List<string>> KeyTable = new Dictionary<object, List<string>>();

            foreach (object Quest in objectList)
                if (Quest is PgQuest AsQuest)
                {
                    foreach (PgQuestReward Reward in AsQuest.QuestRewardList)
                        if (Reward is PgQuestRewardInteractionFlag AsSetInteractionFlag)
                        {
                            InteractionFlag Keyword = AsSetInteractionFlag.InteractionFlag;

                            if (!QuestTable.ContainsKey(Keyword))
                            {
                                QuestTable.Add(Keyword, new List<PgQuest>());
                                KeyTable.Add(Keyword, new List<string>());
                            }

                            QuestTable[Keyword].Add(AsQuest);
                        }
                }

            foreach (KeyValuePair<InteractionFlag, List<PgQuest>> Entry in QuestTable)
            {
                InteractionFlag Keyword = Entry.Key;

                Entry.Value.Sort(SortQuestByName);

                foreach (PgQuest Quest in Entry.Value)
                    KeyTable[Keyword].Add(Quest.Key);
            }

            return KeyTable;
        }

        private static int SortQuestByName(PgQuest quest1, PgQuest quest2)
        {
            return string.Compare(quest1.Name, quest2.Name, StringComparison.InvariantCulture);
        }

        private static Dictionary<object, List<string>> GetShamanicInfusionPowerBySlotTable(List<object> objectList)
        {
            return GetPowerBySlotTable("ShamanicInfusion", objectList);
        }

        private static Dictionary<object, List<string>> GetEndurancePowerBySlotTable(List<object> objectList)
        {
            return GetPowerBySlotTable("Endurance", objectList);
        }

        private static Dictionary<object, List<string>> GetArmorPatchingPowerBySlotTable(List<object> objectList)
        {
            return GetPowerBySlotTable("ArmorPatching", objectList);
        }

        private static Dictionary<object, List<string>> GetAnySkillPowerBySlotTable(List<object> objectList)
        {
            return GetPowerBySlotTable("AnySkill", objectList);
        }

        private static Dictionary<object, List<string>> GetPowerBySlotTable(string skillKey, List<object> objectList)
        {
            Dictionary<object, List<string>> KeyTable = new Dictionary<object, List<string>>();

            foreach (object Item in objectList)
                if (Item is PgPower AsPower)
                    if (AsPower.Skill.Key == skillKey || (AsPower.Skill == PgSkill.AnySkill && skillKey == "AnySkill"))
                    {
                        foreach (ItemSlot Slot in AsPower.SlotList)
                        {
                            ItemSlot SlotKey = Slot;

                            if (SlotKey != ItemSlot.Internal_None)
                            {
                                if (SlotKey == ItemSlot.OffHandShield)
                                    SlotKey = ItemSlot.OffHand;

                                if (!KeyTable.ContainsKey(SlotKey))
                                    KeyTable.Add(SlotKey, new List<string>());

                                KeyTable[SlotKey].Add(AsPower.Key);
                            }
                        }
                    }

            return KeyTable;
        }

        private static void WriteGroupingList(List<object> objectList, StreamWriter writer, string groupingName, Func<List<object>, List<string>> getterList)
        {
            writer.WriteLine($"        public static List<string> {groupingName}List = new List<string>()");
            writer.WriteLine($"        {{");

            List<string> KeyList = getterList(objectList);

            foreach (string Key in KeyList)
            {
                string KeyValue = GetStringValueString(Key);
                writer.WriteLine($"            {KeyValue},");
            }

            writer.WriteLine($"        }};");
        }

        private static void WriteGroupingDictionary(List<object> objectList, StreamWriter writer, Type keyType, string groupingName, Func<List<object>, Dictionary<object, List<string>>> getterTable)
        {
            string KeyTypeName = SimpleTypeName(keyType);

            writer.WriteLine($"        public static Dictionary<{KeyTypeName}, List<string>> {groupingName}List = new Dictionary<{KeyTypeName}, List<string>>()");
            writer.WriteLine($"        {{");

            Dictionary<object, List<string>> KeyTable = getterTable(objectList);

            foreach (KeyValuePair<object, List<string>> Entry in KeyTable)
            {
                string KeyString = GetValueString(keyType, Entry.Key, objectList);

                string ValueListString = string.Empty;

                foreach (string ValueKey in Entry.Value)
                {
                    if (ValueListString.Length > 0)
                        ValueListString += ", ";

                    string ValueKeyString = GetStringValueString(ValueKey);
                    ValueListString += ValueKeyString;
                }

                writer.WriteLine($"            {{ {KeyString}, new List<string>() {{ {ValueListString} }} }},");
            }

            writer.WriteLine($"        }};");
        }

        private static string GetValueString(Type type, object value, List<object> objectList)
        {
            if (type == typeof(bool?) || type == typeof(bool))
                return GetBoolValueString(value);
            else if (type == typeof(int?) || type == typeof(int))
                return GetIntValueString(value);
            else if (type == typeof(uint?) || type == typeof(uint))
                return GetUIntValueString(value);
            else if (type == typeof(float?) || type == typeof(float))
                return GetFloatValueString(value);
            else if (type == typeof(TimeSpan?))
                return GetTimeSpanValueString(value);
            else if (type == typeof(string))
                return GetStringValueString(value);
            else if (type.IsEnum)
                return GetEnumValueString(type, value);
            else if (type.IsClass)
                return GetReferenceValueString(type, value, objectList);
            else
                throw new ArgumentException();
        }

        private static string ToObjectName(int index)
        {
            return $"Object_{index}";
        }

        private static string ToClassName(Type type)
        {
            string TypeName = type.Name;
            Debug.Assert(TypeName.StartsWith("Pg"));
            string ClassName = $"{TypeName.Substring(2)}Objects";

            return ClassName;
        }

        private static string ToObjectName(Type type, int index)
        {
            string ClassPrefix = ToClassName(type);
            string ObjectName = ToObjectName(index);

            return $"{ClassPrefix}.{ObjectName}";
        }

        private static string GetBoolValueString(object value)
        {
            bool? BoolValue = (bool?)value;
            if (BoolValue.HasValue)
                return BoolValue.Value.ToString().ToLower();
            else
                return "null";
        }

        private static string GetIntValueString(object value)
        {
            int? IntValue = (int?)value;
            if (IntValue.HasValue)
                return IntValue.Value.ToString();
            else
                return "null";
        }

        private static string GetUIntValueString(object value)
        {
            uint? UIntValue = (uint?)value;
            if (UIntValue.HasValue)
                return UIntValue.Value.ToString();
            else
                return "null";
        }

        private static string GetFloatValueString(object value)
        {
            float? FloatValue = (float?)value;
            if (FloatValue.HasValue)
            {
                string FloatValueString = FloatValue.Value.ToString(CultureInfo.InvariantCulture);

                if (FloatValueString.Contains(".") || FloatValueString.Contains("E"))
                    FloatValueString += "F";

                return FloatValueString;
            }
            else
                return "null";
        }

        private static string GetTimeSpanValueString(object value)
        {
            TimeSpan? TimeSpanValue = (TimeSpan?)value;
            if (TimeSpanValue.HasValue)
                return $"TimeSpan.FromTicks({TimeSpanValue.Value.Ticks})";
            else
                return "null";
        }

        private static string GetStringValueString(object value)
        {
            string StringValue = value as string;
            Debug.Assert(StringValue != null);

            StringValue = StringValue.Replace("\n", "\\n");
            StringValue = StringValue.Replace("\"", "\\\"");

            return $"\"{StringValue}\"";
        }

        private static string GetEnumValueString(Type type, object value)
        {
            string EnumTypeString = SimpleTypeName(type);
            string EnumString = type.GetEnumName(value);

            if (EnumString == null)
                return $"({EnumTypeString}){(int)value}";
            else
                return $"{EnumTypeString}.{EnumString}";
        }

        private static string GetReferenceValueString(Type type, object value, List<object> objectList)
        {
            if (value == null)
                return "null";

            if (IndexTable.ContainsKey(value))
                return ToObjectName(value.GetType(), IndexTable[value]);
            else if (type == typeof(PgSkill))
            {
                Debug.Assert(value == PgSkill.Unknown || value == PgSkill.AnySkill);

                if (value == PgSkill.Unknown)
                    return "PgSkill.Unknown";
                else
                    return "PgSkill.AnySkill";
            }
            else if (value is IDictionary AsDictionary)
                return GetDictionaryValueString(type, AsDictionary, objectList);
            else if (type.Name.StartsWith("List"))
                return GetListValueString(type, value as IList, objectList);
            else if (value is ICollection AsCollection)
                return GetCollectionValueString(type, AsCollection, objectList);
            else
                throw new ArgumentException();
        }

        private static string GetDictionaryValueString(Type type, IDictionary dictionary, List<object> objectList)
        {
            string Result;

            Type[] GenericArguments = type.GetGenericArguments();
            if (GenericArguments.Length == 0)
                Result = $"new {type.Name}()";
            else
            {
                Debug.Assert(GenericArguments.Length == 2);
                Type KeyType = GenericArguments[0];
                string KeyTypeString = SimpleTypeName(KeyType);
                Type ValueType = GenericArguments[1];
                string ValueTypeString = SimpleTypeName(ValueType);

                if (KeyTypeString == "Int32")
                    KeyTypeString = "int";

                Result = $"new Dictionary<{KeyTypeString}, {ValueTypeString}>()";
            }

            string DictionaryContentString = string.Empty;
            ICollection DictionaryKeys = dictionary.Keys;

            foreach (object DictionaryKey in DictionaryKeys)
            {
                if (DictionaryContentString.Length > 0)
                    DictionaryContentString += ", ";

                object DictionaryValue = dictionary[DictionaryKey];

                string KeyString = GetValueString(DictionaryKey.GetType(), DictionaryKey, objectList);
                string ValueString = GetValueString(DictionaryValue.GetType(), DictionaryValue, objectList);

                DictionaryContentString += $"{{ {KeyString}, {ValueString} }}";
            }

            Result += $" {{ {DictionaryContentString } }}";

            return Result;
        }

        private static string GetListValueString(Type type, IList list, List<object> objectList)
        {
            Type[] GenericArguments = type.GetGenericArguments();
            Debug.Assert(GenericArguments.Length == 1);
            Type ItemType = GenericArguments[0];
            string ItemTypeString = SimpleTypeName(ItemType);

            string Result = $"new List<{ItemTypeString}>()";

            string ListContentString = string.Empty;

            foreach (object Item in list)
            {
                if (ListContentString.Length > 0)
                    ListContentString += ", ";

                ListContentString += GetValueString(Item.GetType(), Item, objectList);
            }

            Result += $" {{ {ListContentString} }}";

            return Result;
        }

        private static string GetCollectionValueString(Type type, ICollection collection, List<object> objectList)
        {
            string CollectionTypeString = SimpleTypeName(type);

            string Result = $"new {CollectionTypeString}()";

            string CollectionContentString = string.Empty;

            foreach (object Item in collection)
            {
                if (CollectionContentString.Length > 0)
                    CollectionContentString += ", ";

                CollectionContentString += GetValueString(Item.GetType(), Item, objectList);
            }

            Result += $" {{ {CollectionContentString} }}";

            return Result;
        }

        private static string SimpleTypeName(Type type)
        {
            if (type == typeof(Int32) || type == typeof(int))
                return "int";
            else if (type == typeof(string))
                return "string";
            else if (type.Name.StartsWith("List"))
            {
                Type[] GenericArguments = type.GetGenericArguments();
                Debug.Assert(GenericArguments.Length == 1);
                Type ItemType = GenericArguments[0];
                string ItemTypeString = SimpleTypeName(ItemType);
                return $"List<{ItemTypeString}>";
            }

            return type.Name;
        }
    }
}
