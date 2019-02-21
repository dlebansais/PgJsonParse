using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AbilityRequirementNotInHotspot : AbilityRequirement, IPgAbilityRequirementNotInHotspot
    {
        public AbilityRequirementNotInHotspot(string name)
        {
            Name = name;
        }

        public override OtherRequirementType Type { get { return OtherRequirementType.IsNotInHotspot; } }
        public string Name { get; private set; }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(Type) } },
            { "Name", new FieldParser() {
                Type = FieldType.String,
                GetString  = () => Name } },
        }; } }

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, Name);

                return Result;
            }
        }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            SerializeJsonObjectInternalProlog(data, ref offset, StoredStringtable, StoredStringListTable);
            int BaseOffset = offset;

            AddString(Name, data, ref offset, BaseOffset, 0, StoredStringtable);

            FinishSerializing(data, ref offset, BaseOffset, 4, StoredStringtable, null, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
