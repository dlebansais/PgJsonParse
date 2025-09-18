namespace PgObjects;

using System.Globalization;

public class PgNumericValueEx
{
    public static PgNumericValueEx Empty { get; } = new PgNumericValueEx() { Value = float.NaN };

    public required float Value { get; init; }

    public bool IsPercent { get; init; }

    public override string ToString()
    {
        if (float.IsNaN(Value))
            return string.Empty;
        else
        {
            string Text = Value.ToString(CultureInfo.InvariantCulture);

            if (Value > 0)
                Text = $"+{Text}";
            if (IsPercent)
                Text = $"{Text}%";

            return Text;
        }
    }
}
