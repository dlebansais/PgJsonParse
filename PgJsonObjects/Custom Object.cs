using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class CustomObject : GenericJsonObject<CustomObject>
    {
        #region Indirect Properties
        protected override string SortingName { get { return null; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable
        {
            get
            {
                if (_FieldTable == null)
                {
                    _FieldTable = new Dictionary<string, FieldParser>();

                    foreach (string FieldKey in FieldTableOrder)
                    {
                        if (FieldSetTableInt.ContainsKey(FieldKey))
                            _FieldTable.Add(FieldKey, new FieldParser() { Type = FieldType.Integer, GetInteger = () => { return FieldSetTableInt[FieldKey]; } });
                        else if (FieldSetTableString.ContainsKey(FieldKey))
                            _FieldTable.Add(FieldKey, new FieldParser() { Type = FieldType.String, GetString = () => { return FieldSetTableString[FieldKey]; } });
                        else if (FieldSetTableObject.ContainsKey(FieldKey))
                            _FieldTable.Add(FieldKey, new FieldParser() { Type = FieldType.Object, GetObject = () => { return FieldSetTableObject[FieldKey]; } });
                    }
                }

                return _FieldTable;
            }
        }
        private Dictionary<string, FieldParser> _FieldTable;

        private Dictionary<string, int> FieldSetTableInt = new Dictionary<string, int>();
        private Dictionary<string, string> FieldSetTableString = new Dictionary<string, string>();
        private Dictionary<string, IGenericJsonObject> FieldSetTableObject = new Dictionary<string, IGenericJsonObject>();

        public void SetCustomKey(string Key)
        {
            base.InitializeKey(Key, 0, null, null);
        }

        public void SetFieldValue(string FieldKey, int FieldValue)
        {
            FieldSetTableInt.Add(FieldKey, FieldValue);
            FieldTableOrder.Add(FieldKey);
        }

        public void SetFieldValue(string FieldKey, string FieldValue)
        {
            FieldSetTableString.Add(FieldKey, FieldValue);
            FieldTableOrder.Add(FieldKey);
        }

        public void SetFieldValue(string FieldKey, IGenericJsonObject FieldValue)
        {
            FieldSetTableObject.Add(FieldKey, FieldValue);
            FieldTableOrder.Add(FieldKey);
        }
        #endregion

        #region Indexing
        public override string TextContent { get { return ""; } }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            return false;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return ""; } }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
        }
        #endregion
    }
}
