namespace PgObjects
{
    using System.Collections.Generic;

    public class PgAttribute
    {
        public string Key { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public List<int> IconIdList { get; set; } = new List<int>();
        public string Tooltip { get; set; } = string.Empty;
        public DisplayType DisplayType { get; set; }
        public bool IsHidden { get { return RawIsHidden.HasValue && RawIsHidden.Value; } }
        public bool? RawIsHidden { get; set; }
        public DisplayRule DisplayRule { get; set; }
        public float DefaultValue { get { return RawDefaultValue.HasValue ? RawDefaultValue.Value : 0; } }
        public float? RawDefaultValue { get; set; }
    }
}
