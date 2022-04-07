namespace PgObjects
{
    public class PgAdvancementEffectAttribute
    {
        public string? Attribute_Key { get; set; }
        public float Value { get { return RawValue.HasValue ? RawValue.Value : 0; } }
        public float? RawValue { get; set; }
    }
}
