using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IConnectableObject
    {
        bool Connect(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IJsonKey>> AllTables);
        void SetIndirectProperties(Dictionary<Type, Dictionary<string, IJsonKey>> AllTables, ParseErrorInfo ErrorInfo);
    }
}
