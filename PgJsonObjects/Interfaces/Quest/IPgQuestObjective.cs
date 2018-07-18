using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgQuestObjective
    {
        string Description { get; }
        Quest ParentQuest { get; }
        void CopyFieldTableOrder(string key, List<string> fieldTableOrder);
        bool Connect(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IJsonKey>> AllTables);
    }
}
