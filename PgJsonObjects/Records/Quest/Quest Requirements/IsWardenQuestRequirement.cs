﻿using System.Collections.Generic;

namespace PgJsonObjects
{
    public class IsWardenQuestRequirement : QuestRequirement, IPgIsWardenQuestRequirement
    {
        public IsWardenQuestRequirement(OtherRequirementType OtherRequirementType)
            : base(OtherRequirementType)
        {
        }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType, null, OtherRequirementType.Internal_None) } },
        }; } }

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

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

            FinishSerializing(data, ref offset, BaseOffset, 0, StoredStringtable, null, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
