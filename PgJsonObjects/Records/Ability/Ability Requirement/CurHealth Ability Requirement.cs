using System.Collections.Generic;

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
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType.CurHealth) } },
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

            AddDouble(RawHealth, data, ref offset, BaseOffset, 0);

            FinishSerializing(data, ref offset, BaseOffset, 4, null, null, null, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
