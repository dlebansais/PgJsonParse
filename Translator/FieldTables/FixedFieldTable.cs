namespace Translator;

using System;
using System.Collections.Generic;
using System.Diagnostics;

public class FixedFieldTable : FieldTable
{
    public static FieldTable Empty { get; } = new FixedFieldTable();

    private FixedFieldTable()
        : base()
    {
    }

    public FixedFieldTable(Dictionary<string, Type> table)
        : base(table)
    {
    }

    public override bool ContainsKey(string key, out Type type)
    {
        if (!Table.ContainsKey(key))
        {
            type = typeof(object);
            return false;
        }
        else
        {
            type = Table[key];

            if (!UsedKeyList.Contains(key))
                UsedKeyList.Add(key);

            return true;
        }
    }

    public override bool VerifyTableCompletion(Type baseType)
    {
        bool Result = true;

        foreach (KeyValuePair<string, Type> Entry in Table)
        {
            string Key = Entry.Key;
            if (!UsedKeyList.Contains(Key))
                Result &= Program.ReportFailure($"Entry {Key} for type {baseType} was not used during parsing");
        }

        return Result;
    }

    private List<string> UsedKeyList = new List<string>();
}
