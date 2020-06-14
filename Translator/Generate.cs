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

            IndexTable.Clear();
            for (int ObjectIndex = 0; ObjectIndex < objectList.Count; ObjectIndex++)
                IndexTable.Add(objectList[ObjectIndex], ObjectIndex);

            WriteGroupings(objectList);
            Write(objectList);
            WriteKeys(objectList);
            WriteDictionaries(objectList);

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
                Writer.WriteLine($"            if (!Table.ContainsKey(key))");
                Writer.WriteLine($"            {{");
                Writer.WriteLine($"                switch (key)");
                Writer.WriteLine($"                {{");
            }

            int LastDisplay = 10;

            for (int ObjectIndex = 0; ObjectIndex < objectList.Count; ObjectIndex++)
            {
                WriteItemToDictionary(objectList, ObjectIndex);

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
                Writer.WriteLine($"                    default:");
                Writer.WriteLine($"                        return null;");
                Writer.WriteLine($"                }}");
                Writer.WriteLine($"            }}");
                Writer.WriteLine("");
                Writer.WriteLine($"            return Table[key];");
                Writer.WriteLine($"        }}");
            }
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
            string FilePath = Path.Combine(RootFolder, "Tables", $"{ClassName}.cs");

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
            Writer.WriteLine($"                        Table.Add(key, {LinkValue});");
            Writer.WriteLine($"                        break;");
        }

        private static void WriteGroupings(List<object> objectList)
        {
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

            WriteGroupingList(objectList, Writer, "CombatSkill", GetCombatSkillList);
            WriteGroupingDictionary(objectList, Writer, typeof(string), "CombatSubskill", GetCombatSubskillTable);
            WriteGroupingDictionary(objectList, Writer, typeof(ItemSlot), "ItemBySlot", GetItemBySlotTable);
            WriteGroupingDictionary(objectList, Writer, typeof(ItemSlot), "ShamanicInfusionPowerBySlot", GetShamanicInfusionPowerBySlotTable);
            WriteGroupingDictionary(objectList, Writer, typeof(ItemSlot), "EndurancePowerBySlot", GetEndurancePowerBySlotTable);
            WriteGroupingDictionary(objectList, Writer, typeof(ItemSlot), "ArmorPatchingPowerBySlot", GetArmorPatchingPowerBySlotTable);
            WriteGroupingDictionary(objectList, Writer, typeof(ItemSlot), "AnySkillPowerBySlot", GetAnySkillPowerBySlotTable);

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

        private static bool IsCombatSkill(PgSkill skill)
        {
            return (skill.IsCombatSkill || skill.ParentSkillList.Exists((PgSkill item) => IsCombatSkill(item))) && skill.AssociationTablePower.Count > 0;
        }

        private static bool IsCombatSubskill(PgSkill skill)
        {
            return skill.ParentSkillList.Exists((PgSkill item) => IsCombatSkill(item)) && skill.AssociationTablePower.Count > 0;
        }

        private static bool IsCombatParentSkill(string subskillKey)
        {
            PgSkill Subskill = (PgSkill)ParsingContext.ObjectKeyTable[typeof(PgSkill)][subskillKey].Item;
            return Subskill.ParentSkillList.Count > 0;
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

        private static int SortItemByName(PgItem item1, PgItem item2)
        {
            return string.Compare(item1.Name, item2.Name, StringComparison.InvariantCulture);
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

        private static ItemSlot SlotFromPower(PgPower item)
        {
            if (item.SlotList.Count == 0)
                return ItemSlot.Internal_None;
            else
                return item.SlotList[0];
        }

        private static bool IsGenericPowerForSlot(ItemSlot slot, PgPower power)
        {
            if (slot == ItemSlot.Internal_None)
                return false;

            if (power.Skill != PgSkill.AnySkill)
                return false;

            if (power.IsUnavailable)
                return false;

            if (power.PowerTierList.TierList.Count == 0)
                return false;

            return power.SlotList.Contains(slot);
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
