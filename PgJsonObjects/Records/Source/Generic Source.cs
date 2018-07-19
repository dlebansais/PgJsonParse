using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public abstract class GenericSource<T> : GenericJsonObject<T>, IPgGenericSource, ISerializableJsonObject
        where T : class
    {
        public virtual void Init(IGenericPgObject Parent)
        {
        }

        public override string SortingName { get { return null; } }
        protected override string FieldTableName { get { return null; } }
        protected override Dictionary<string, FieldParser> FieldTable { get { return null; } }
        public override string TextContent { get { return null; } }
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, IBackLinkable Parent, Dictionary<Type, Dictionary<string, IJsonKey>> AllTables)
        {
            return false;
        }

        protected abstract int Type { get; }

        #region Serializing
        protected void SerializeJsonObjectInternalProlog(byte[] data, ref int offset)
        {
            int BaseOffset = offset;

            AddInt(Type, data, ref offset, BaseOffset, 0);
        }

        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            throw new InvalidOperationException();
        }
        #endregion
    }
}
