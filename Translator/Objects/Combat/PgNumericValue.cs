namespace PgObjects
{
    using System.Globalization;

    public class PgNumericValue
    {
        public float Value { get { return RawValue.HasValue ? RawValue.Value : 0; } }
        public float? RawValue { get; set; }
        public bool IsPercent { get { return RawIsPercent.HasValue && RawIsPercent.Value; } }
        public bool? RawIsPercent { get; set; }

        public override string ToString()
        {
            if (RawValue.HasValue)
            {
                string Text = Value.ToString(CultureInfo.InvariantCulture);

                if (Value > 0)
                    Text = $"+{Text}";
                if (IsPercent)
                    Text = $"{Text}%";

                return Text;
            }
            else
                return string.Empty;
        }
    }
}
