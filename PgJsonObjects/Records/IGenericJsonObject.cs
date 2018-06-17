using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IGenericJsonObject : IObjectContentGenerator, IBackLinkable, IJsonKey, IIndexableObject
    {
        void CheckUnparsedFields(ParseErrorInfo errorInfo);
        bool Connect(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables);
        void SetIndirectProperties(Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables, ParseErrorInfo ErrorInfo);
        void SortLinkBack();
    }
}
