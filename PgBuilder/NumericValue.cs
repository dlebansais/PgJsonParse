namespace PgBuilder
{
    using System.Globalization;

    public class NumericValue
    {
        public NumericValue()
        {
            Value = double.NaN;
            IsPercent = false;
        }

        private NumericValue(double value, bool isPercent)
        {
            Value = value;
            IsPercent = isPercent;
        }

        public double Value { get; set; }
        public bool IsPercent { get; set; }
        public bool IsValueSet { get { return !double.IsNaN(Value); } }

        public static NumericValue FromDouble(double value)
        {
            return new NumericValue(value, false);
        }

        public static NumericValue FromDoublePercent(double value)
        {
            return new NumericValue(value, true);
        }

        public static NumericValue Parse(string text)
        {
            if (text.EndsWith("%"))
                return FromDoublePercent(double.Parse(text.Substring(0, text.Length - 1), NumberStyles.Float, CultureInfo.InvariantCulture));
            else
                return FromDouble(double.Parse(text, NumberStyles.Float, CultureInfo.InvariantCulture));
        }

        public void ChangeSign()
        {
            Value = -Value;
        }

        public static int BackwardIndex(string text, int startIndex)
        {
            int Index = startIndex;

            if (Index > 0 && text[Index - 1] == '%')
                Index--;

            int IndexPercentCharacter = Index;

            while (Index > 0 && char.IsDigit(text[Index - 1]))
                Index--;

            if (Index == IndexPercentCharacter || Index == startIndex)
                return -1;

            while (Index > 0 && (char.IsDigit(text[Index - 1]) || text[Index - 1] == '.' || text[Index - 1] == '-' || text[Index - 1] == '+'))
                Index--;

            return Index;
        }

        public override string ToString()
        {
            if (IsValueSet)
                if (IsPercent)
                    return $"{Value.ToString(CultureInfo.InvariantCulture)}%";
                else
                    return Value.ToString(CultureInfo.InvariantCulture);
            else
                return string.Empty;
        }
    }
}
