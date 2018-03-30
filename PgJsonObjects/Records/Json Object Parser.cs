using PgJsonReader;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public static class JsonObjectParser<T> where T : GenericJsonObject<T>, new()
    {
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
