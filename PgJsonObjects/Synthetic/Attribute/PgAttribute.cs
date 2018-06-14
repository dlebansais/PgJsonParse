using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAttribute : GenericPgObject, IPgAttribute
    {
        public PgAttribute(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public string Label { get { return GetString(0); } }
        public List<int> IconIdList { get { return GetIntList(4, ref _IconIdList); } } private List<int> _IconIdList;
        public string Tooltip { get { return GetString(8); } }
        public double DefaultValue { get { return RawDefaultValue.HasValue ? RawDefaultValue.Value : 0; } }
        public double? RawDefaultValue { get { return GetDouble(12); } }
        public DisplayType DisplayType { get { return GetEnum<DisplayType>(16); } }
        public DisplayRule DisplayRule { get { return GetEnum<DisplayRule>(18); } }
        public bool IsHidden { get { return RawIsHidden.HasValue && RawIsHidden.Value; } }
        public bool? RawIsHidden { get { return GetBool(0, 0); } }
    }
}
