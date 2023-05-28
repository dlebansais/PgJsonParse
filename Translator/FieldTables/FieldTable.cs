namespace Translator;

using System;
using System.Collections.Generic;

public abstract class FieldTable
{
    protected FieldTable()
    {
    }

    public FieldTable(Dictionary<string, Type> table)
    {
        Table = table;
    }

    public Dictionary<string, Type> Table { get; } = new();

    public abstract bool ContainsKey(string key, out Type type);
    public abstract bool VerifyTableCompletion(Type baseType);
}
