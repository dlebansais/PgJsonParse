namespace PgJsonObjects
{
    public abstract class ItemEffect
    {
        public abstract string AsEffectString();

        public static bool TryParse(string Effect, out ItemEffect Result)
        {
            Result = null;

            if (Effect == null)
                return false;

            if (Effect.StartsWith("{") && Effect.EndsWith("}"))
                return TryParseAttributeLink(Effect.Substring(1, Effect.Length - 2), out Result);
            else
                return TryParseSimple(Effect, out Result);
        }

        private static bool TryParseAttributeLink(string Effect, out ItemEffect Result)
        {
            Result = null;

            string[] Split = Effect.Split('{');
            if (Split.Length != 2)
                return false;

            string AttributeName = Split[0];
            string AttributeEffect = Split[1];

            if (!AttributeName.EndsWith("}"))
                return false;

            AttributeName = AttributeName.Substring(0, AttributeName.Length - 1);
            if (AttributeName.Contains("{") || AttributeName.Contains("}"))
                return false;

            if (AttributeName.Length == 0 || AttributeEffect.Length == 0)
                return false;

            float ParsedEffect;
            FloatFormat ParsedEffectFormat;
            if (!Tools.TryParseFloat(AttributeEffect, out ParsedEffect, out ParsedEffectFormat))
                return false;

            if (ParsedEffectFormat != FloatFormat.Standard)
                return false;

            Result = new ItemAttributeLink(AttributeName, ParsedEffect, ParsedEffectFormat);
            return true;
        }

        private static bool TryParseSimple(string Effect, out ItemEffect Result)
        {
            Result = null;

            if (Effect.Contains("{") || Effect.Contains("}"))
                return false;

            Result = new ItemSimpleEffect(Effect);
            return true;
        }
    }
}
