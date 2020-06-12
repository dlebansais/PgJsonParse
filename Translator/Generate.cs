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
            FilePath = Path.Combine(FilePath, "Tables");

            RootFolder = FilePath;

            Write(objectList);

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

            string TypeName = type.Name;
            Debug.Assert(TypeName.StartsWith("Pg"));
            string ClassName = $"{TypeName.Substring(2)}Objects";

            string FilePath = Path.Combine(RootFolder, $"{ClassName}.cs");

            FileStream Stream = new FileStream(FilePath, FileMode.Create, FileAccess.Write);
            writer = new StreamWriter(Stream);

            writer.WriteLine("namespace PgObjects");
            writer.WriteLine("{");
            writer.WriteLine("    using System;");
            writer.WriteLine("    using System.Collections.Generic;");
            writer.WriteLine("");
            writer.WriteLine($"    public class {ClassName}");
            writer.WriteLine("    {");

            StreamTable.Add(type, Stream);
            WriterTable.Add(type, writer);
        }

        private static void WriteItem(List<object> objectList, int objectIndex)
        {
            object Item = objectList[objectIndex];
            Type Type = Item.GetType();

            AddTypeFile(Type, out StreamWriter Writer);

            PropertyInfo[] Properties = Type.GetProperties();
            string ObjectTypeName = SimpleTypeName(Type);
            string ObjectName = ToObjectName(objectIndex);

            string ItemHeader = $"        public static {ObjectTypeName} {ObjectName} {{ get; }} = new {ObjectTypeName}()";
            Writer.WriteLine(ItemHeader);
            Writer.WriteLine("        {");

            string Line = string.Empty;
            foreach (PropertyInfo Property in Properties)
            {
                if (!Property.CanWrite)
                    continue;

                if (Line.Length > 0)
                    Line += ", ";

                string PropertyName = Property.Name;
                Type PropertyType = Property.PropertyType;
                object PropertyValue = Property.GetValue(Item);
                string ValueString = GetValueString(PropertyType, PropertyValue, objectList);

                Line += $"{PropertyName} = {ValueString}";
            }

            if (Line.Length > 0)
                Writer.WriteLine($"            {Line}");

            Writer.WriteLine("        };");
        }

        private static string GetValueString(Type type, object value, List<object> objectList)
        {
            if (type == typeof(bool?))
                return GetBoolValueString(value);
            else if (type == typeof(int?) || type == typeof(int))
                return GetIntValueString(value);
            else if (type == typeof(uint?))
                return GetUIntValueString(value);
            else if (type == typeof(float?))
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
            {
                return null;
            }
        }

        private static string ToObjectName(int index)
        {
            return $"Object_{index}";
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

            int ItemIndex = objectList.IndexOf(value);
            if (ItemIndex >= 0)
            {
                string ItemName = ToObjectName(ItemIndex);
                return ItemName;
            }
            else if (type == typeof(PgSkill))
            {
                Debug.Assert(value == PgSkill.Unknown || value == PgSkill.AnySkill);

                if (value == PgSkill.Unknown)
                    return "PgSkill.Unknown";
                else
                    return "PgSkill.AnySkill";
            }
            else if (value is PgNpcLocation AsLocation)
                return GetLocationValueString(type, AsLocation, objectList);
            else if (value is IDictionary AsDictionary)
                return GetDictionaryValueString(type, AsDictionary, objectList);
            else if (type.Name.StartsWith("List"))
                return GetListValueString(type, value as IList, objectList);
            else if (value is ICollection AsCollection)
                return GetCollectionValueString(type, AsCollection, objectList);
            else
            {
                return null;
            }
        }

        private static string GetLocationValueString(Type type, PgNpcLocation location, List<object> objectList)
        {
            string NpcAreaString = GetEnumValueString(typeof(MapAreaName), location.NpcArea);
            string NpcIdString = GetStringValueString(location.NpcId);
            string NpcString = GetReferenceValueString(typeof(PgNpc), location.Npc, objectList);
            string NpcEnumString = GetEnumValueString(typeof(SpecialNpc), location.NpcEnum);
            string NpcNameString = GetStringValueString(location.NpcName);

            return $"new PgNpcLocation() {{ NpcArea = {NpcAreaString}, NpcId = {NpcIdString}, Npc = {NpcString}, NpcEnum = {NpcEnumString}, NpcName = {NpcNameString} }}";
        }

        private static string GetDictionaryValueString(Type type, IDictionary dictionary, List<object> objectList)
        {
            Type[] GenericArguments = type.GetGenericArguments();
            Debug.Assert(GenericArguments.Length == 2);
            Type KeyType = GenericArguments[0];
            string KeyTypeString = SimpleTypeName(KeyType);
            Type ValueType = GenericArguments[1];
            string ValueTypeString = SimpleTypeName(ValueType);

            if (KeyTypeString == "Int32")
                KeyTypeString = "int";

            string Result = $"new Dictionary<{KeyTypeString}, {ValueTypeString}>()";

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

            return type.Name;
        }
    }
}
