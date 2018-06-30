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
/*            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType.Appearance) } },*/
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
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, IList> StoredEnumListTable = new Dictionary<int, IList>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            AddInt((int?)OtherRequirementType, data, ref offset, BaseOffset, 0);
            AddString(Key, data, ref offset, BaseOffset, 4, StoredStringtable);
            AddEnumList(AppearanceList, data, ref offset, BaseOffset, 8, StoredEnumListTable);
            AddStringList(FieldTableOrder, data, ref offset, BaseOffset, 12, StoredStringListTable);

            FinishSerializing(data, ref offset, BaseOffset, 16, StoredStringtable, null, null, StoredEnumListTable, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
