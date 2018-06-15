using System.Collections.Generic;

namespace PgJsonObjects
{
    public class SingleAppearanceAbilityRequirement : AbilityRequirement, IPgAbilityRequirementSingleAppearance
    {
        public SingleAppearanceAbilityRequirement(string RawAppearance, ParseErrorInfo ErrorInfo)
        {
            Appearance = StringToEnumConversion<Appearance>.Parse(RawAppearance, ErrorInfo);
        }

        public Appearance Appearance { get; private set; }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType.Appearance) } },
            { "Appearance", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<Appearance>.ToString(Appearance) } },
        }; } }

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                if (Appearance != Appearance.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.AppearanceTextMap[Appearance]);

                return Result;
            }
        }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;

            AddEnum(Appearance, data, ref offset, BaseOffset, 0);

            FinishSerializing(data, ref offset, BaseOffset, 2, null, null, null, null, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
