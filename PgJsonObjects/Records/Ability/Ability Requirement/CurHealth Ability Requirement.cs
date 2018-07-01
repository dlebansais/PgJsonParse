﻿using System.Collections.Generic;

namespace PgJsonObjects
{
    public class CurHealthAbilityRequirement : AbilityRequirement, IPgAbilityRequirementCurHealth
    {
        public CurHealthAbilityRequirement(double? RawHealth)
        {
            this.RawHealth = RawHealth;
        }

        public double Health { get { return RawHealth.HasValue ? RawHealth.Value : 0; } }
        public double? RawHealth { get; private set; }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Health", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => Health } },
        }; } }

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, "CurHealth");

                return Result;
            }
        }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            AddInt((int?)OtherRequirementType, data, ref offset, BaseOffset, 0);
            AddString(Key, data, ref offset, BaseOffset, 4, StoredStringtable);
            AddDouble(RawHealth, data, ref offset, BaseOffset, 8);
            AddStringList(FieldTableOrder, data, ref offset, BaseOffset, 12, StoredStringListTable);

            FinishSerializing(data, ref offset, BaseOffset, 16, StoredStringtable, null, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
