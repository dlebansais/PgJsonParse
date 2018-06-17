using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IGenericJsonObject
    {
        string Key { get; }
        bool Connect(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables);
        void SetIndirectProperties(Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables, ParseErrorInfo ErrorInfo);
        void SortLinkBack();
        string TextContent { get; }
        void GenerateObjectContent(JsonGenerator Generator, bool openWithKey, bool openWithNullKey);
    }
}
