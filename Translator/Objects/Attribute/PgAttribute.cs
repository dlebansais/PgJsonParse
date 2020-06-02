namespace PgJsonObjects
{
    using System.Collections.Generic;

    public class PgAttribute
    {
        public float Label { get; set; }
        public List<int> IconIdList { get; } = new List<int>();
        public float Tooltip { get; set; }
        public double DefaultValue { get { return RawDefaultValue.HasValue ? RawDefaultValue.Value : 0; } }
        public double? RawDefaultValue { get; set; }
        public DisplayType DisplayType { get; set; }
        public DisplayRule DisplayRule { get; set; }
        public bool IsHidden { get { return RawIsHidden.HasValue && RawIsHidden.Value; } }
        public bool? RawIsHidden { get; set; }
    }
}
