﻿using PgJsonReader;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public static class JsonObjectParser<T> where T : GenericJsonObject<T>, new()
    {
        public static T Parse(string SubitemName, JsonObject value, ParseErrorInfo errorInfo)
        {
            T Parsed = new T();
            KeyValuePair<string, IJsonValue> RawEntry = new KeyValuePair<string, IJsonValue>(SubitemName, value);
            Parsed.Init(RawEntry, errorInfo);

            if (Parsed is ISpecificRecord AsRefinable)
                Parsed = AsRefinable.ToSpecific(errorInfo) as T;

            return Parsed;
        }

        public static void ParseList(string SubitemName, JsonObject value, ICollection<T> list, ParseErrorInfo errorInfo)
        {
            T Parsed = Parse(SubitemName, value, errorInfo);
            if (Parsed != null)
                list.Add(Parsed);
        }

        public static void InitAsSubitem(string SubitemName, JsonObject RawSubarray, out T Subitem, ParseErrorInfo ErrorInfo)
        {
            Subitem = new T();
            KeyValuePair<string, IJsonValue> RawEntry = new KeyValuePair<string, IJsonValue>(SubitemName, RawSubarray);
            Subitem.Init(RawEntry, ErrorInfo);
        }

        public static void InitAsSublist(JsonArray RawSubarray, out List<T> Sublist, ParseErrorInfo ErrorInfo)
        {
            Sublist = new List<T>();

            foreach (IJsonValue RawSubitem in RawSubarray)
            {
                T NewT = new T();
                KeyValuePair<string, IJsonValue> RawEntry = new KeyValuePair<string, IJsonValue>(null, RawSubitem);
                NewT.Init(RawEntry, ErrorInfo);

                Sublist.Add(NewT);
            }
        }
    }
}
