namespace Translator
{
    using System.Collections.Generic;
    using System.Diagnostics;

    public static class Finalizer<T>
    {
        public static bool FinalizeParsing(Dictionary<T, VariadicObjectHandler> handlerTable, Dictionary<T, List<string>> handledTable, Dictionary<T, List<string>> knownFieldTable)
        {
            List<T> NeverHandledKeyList = new List<T>();
            Dictionary<T, List<string>> NeverHandledFieldList = new Dictionary<T, List<string>>();

            foreach (KeyValuePair<T, VariadicObjectHandler> Entry in handlerTable)
            {
                T Key = Entry.Key;

                if (!handledTable.ContainsKey(Key))
                    NeverHandledKeyList.Add(Key);
                else
                {
                    List<string> ReportedFieldList = handledTable[Key];
                    List<string> ExpectedFieldList = knownFieldTable[Key];

                    foreach (string FieldName in ExpectedFieldList)
                        if (!ReportedFieldList.Contains(FieldName))
                        {
                            if (!NeverHandledFieldList.ContainsKey(Key))
                                NeverHandledFieldList.Add(Key, new List<string>());
                            if (!NeverHandledFieldList[Key].Contains(FieldName))
                                NeverHandledFieldList[Key].Add(FieldName);
                        }
                }
            }

            if (NeverHandledKeyList.Count > 0)
            {
                Debug.WriteLine($"The following keys for type {typeof(T)} were not handled:");

                foreach (T Item in NeverHandledKeyList)
                    Debug.WriteLine(Item.ToString());

                return false;
            }

            if (NeverHandledFieldList.Count > 0)
            {
                Debug.WriteLine($"The following fields for type {typeof(T)} were expected but never handled:");

                foreach (KeyValuePair<T, List<string>> Entry in NeverHandledFieldList)
                    foreach (string FieldName in Entry.Value)
                        Debug.WriteLine($"{Entry.Key}: {FieldName}");

                return false;
            }

            return true;
        }
    }
}
