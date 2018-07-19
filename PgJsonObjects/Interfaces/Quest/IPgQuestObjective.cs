using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IPgQuestObjective
    {
        string Description { get; }
        IList<IBackLinkable> GetLinkBack();

        void CopyFieldTableOrder(string key, List<string> fieldTableOrder);
        bool Connect(ParseErrorInfo ErrorInfo, IBackLinkable Parent, Dictionary<Type, Dictionary<string, IJsonKey>> AllTables);
    }
}
