using PgJsonReader;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public struct FieldParser
    {
        public FieldType Type;

        public Action<object, ParseErrorInfo> ParseUnknown;
        public Action<int, ParseErrorInfo> ParseInteger;
        public Action<bool, ParseErrorInfo> ParseBool;
        public Action<float, ParseErrorInfo> ParseFloat;
        public Action<string, ParseErrorInfo> ParseString;
        public Action<JsonObject, ParseErrorInfo> ParseObject;
        public Action<int, ParseErrorInfo> ParseSimpleIntegerArray;
        public Func<int, ParseErrorInfo, bool> ParseIntegerArray;
        public Action<string, ParseErrorInfo> ParseSimpleStringArray;
        public Func<string, ParseErrorInfo, bool> ParseStringArray;
        public Action<JsonObject, ParseErrorInfo> ParseObjectArray;
        public Action SetArrayIsEmpty;

        public Action<string, JsonGenerator> CustomGenerator;

        public Func<object> GetUnknown;
        public Func<int?> GetInteger;
        public Func<bool?> GetBool;
        public Func<double?> GetFloat;
        public Func<string> GetString;
        public Func<IObjectContentGenerator> GetObject;
        public Func<List<int>> GetIntegerArray;
        public Func<List<string>> GetStringArray;
        public Func<IPgCollection> GetObjectArray;
        public Func<bool> GetArrayIsEmpty;

        public bool SimplifyArray;
        public Action SetArrayIsSimple;
        public Func<bool> GetArrayIsSimple;

        public Action SetArrayIsNested;
        public Func<bool> GetArrayIsNested;
    }
}
