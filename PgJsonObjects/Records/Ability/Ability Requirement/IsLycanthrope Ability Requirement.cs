﻿using System.Collections.Generic;

namespace PgJsonObjects
{
    public class IsLycanthropeAbilityRequirement : AbilityRequirement, IPgAbilityRequirementIsLycanthrope
    {
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType.IsLycanthrope) } },
        }; } }

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, "Is Lycanthrope");

                return Result;
            }
        }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            AddInt((int?)OtherRequirementType, data, ref offset, BaseOffset, 0);

            FinishSerializing(data, ref offset, BaseOffset, 4, null, null, null, null, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
