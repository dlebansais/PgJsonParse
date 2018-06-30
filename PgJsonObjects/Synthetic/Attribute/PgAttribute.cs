using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAttribute : MainPgObject<PgAttribute>, IPgAttribute
    {
        public PgAttribute(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 30;
            SerializableJsonObject.AlignSerializedLength(ref offset);
        }

        protected override PgAttribute CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAttribute CreateNew(byte[] data, ref int offset)
        {
            PgAttribute Result = new PgAttribute(data, ref offset);
            string Label = Result.Label;
            List<int> IconIdList = Result.IconIdList;
            return Result;
        }

        public override string Key { get { return GetString(0); } }
        public string Label { get { return GetString(4); } }
        public List<int> IconIdList { get { return GetIntList(8, ref _IconIdList); } } private List<int> _IconIdList;
        public string Tooltip { get { return GetString(12); } }
        public double DefaultValue { get { return RawDefaultValue.HasValue ? RawDefaultValue.Value : 0; } }
        public double? RawDefaultValue { get { return GetDouble(16); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(20, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public DisplayType DisplayType { get { return GetEnum<DisplayType>(24); } }
        public DisplayRule DisplayRule { get { return GetEnum<DisplayRule>(26); } }
        public bool IsHidden { get { return RawIsHidden.HasValue && RawIsHidden.Value; } }
        public bool? RawIsHidden { get { return GetBool(28, 0); } }

        public bool IsLabelWithPercent
        {
            get
            {
                return Label.EndsWith("%");
            }
        }

        public string LabelRippedOfPercent
        {
            get
            {
                string Result = IsLabelWithPercent ? Label.Substring(0, Label.Length - 1) : Label;
                return Result.Trim();
            }
        }

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
    }
}
