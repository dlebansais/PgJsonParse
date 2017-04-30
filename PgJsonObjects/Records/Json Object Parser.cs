using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public static class JsonObjectParser<T> where T : GenericJsonObject<T>, new()
    {
        public static void InitAsSubitem(string SubitemName, Dictionary<string, object> RawSubarray, out T Subitem, ParseErrorInfo ErrorInfo)
        {
            Subitem = new T();
            KeyValuePair<string, object> RawEntry = new KeyValuePair<string, object>(SubitemName, RawSubarray);
            Subitem.Init(RawEntry, ErrorInfo);
        }

        public static void InitAsSublist(ArrayList RawSubarray, out List<T> Sublist, ParseErrorInfo ErrorInfo)
        {
            Sublist = new List<T>();

            foreach (object RawSubitem in RawSubarray)
            {
                T NewT = new T();
                KeyValuePair<string, object> RawEntry = new KeyValuePair<string, object>(null, RawSubitem);
                NewT.Init(RawEntry, ErrorInfo);

                Sublist.Add(NewT);
            }
        }
    }
}
