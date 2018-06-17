using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AppearanceAbilityRequirement : AbilityRequirement, IPgAbilityRequirementAppearance
    {
        public AppearanceAbilityRequirement(List<string> RawAppearanceList, ParseErrorInfo ErrorInfo)
        {
            List<string> MixedList = new List<string>();
            MixedList.AddRange(RawAppearanceList);

            foreach (string Item in MixedList)
            {
                Appearance ParsedAppearance;
                StringToEnumConversion<Appearance>.TryParse(Item, out ParsedAppearance, ErrorInfo);
                AppearanceList.Add(ParsedAppearance);
            }
        }

        public List<Appearance> AppearanceList { get; } = new List<Appearance>();

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType.Appearance) } },
            { "Appearance", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = () => StringToEnumConversion<Appearance>.ToStringList(AppearanceList) } },
        }; } }

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                foreach (Appearance Item in AppearanceList)
                    AddWithFieldSeparator(ref Result, TextMaps.AppearanceTextMap[Item]);

                return Result;
            }
        }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, IList> StoredEnumListTable = new Dictionary<int, IList>();

            AddInt((int?)OtherRequirementType, data, ref offset, BaseOffset, 0);
            AddEnumList(AppearanceList, data, ref offset, BaseOffset, 4, StoredEnumListTable);

            FinishSerializing(data, ref offset, BaseOffset, 8, null, null, null, StoredEnumListTable, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
