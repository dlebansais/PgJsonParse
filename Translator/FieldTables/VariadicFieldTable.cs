namespace Translator;

using System;
using System.Collections.Generic;
using System.Diagnostics;

public class VariadicFieldTable : FieldTable
{
    public VariadicFieldTable(Type type)
        : base(new Dictionary<string, Type>() { { string.Empty, type } })
    {
    }

    public override bool ContainsKey(string key, out Type type)
    {
        type = Table[string.Empty];
        IsTableUsed = true;
        return true;
    }

    public override bool VerifyTableCompletion(Type baseType)
    {
        if (!IsTableUsed)
            return Program.ReportFailure($"No entry for type {baseType} was not used during parsing");
        else
            return true;
    }

    private bool IsTableUsed = false;
}
