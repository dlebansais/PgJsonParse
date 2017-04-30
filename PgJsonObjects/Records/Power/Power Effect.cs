namespace PgJsonObjects
{
    public abstract class PowerEffect
    {
        public abstract string AsEffectString();

        public static bool TryParse(string Effect, ParseErrorInfo ErrorInfo, out PowerEffect Result)
        {
            Result = null;

            if (Effect == null)
                return false;

            if (Effect.StartsWith("{") && Effect.EndsWith("}"))
                return TryParseAttributeLink(Effect.Substring(1, Effect.Length - 2), ErrorInfo, out Result);
            else
                return TryParseSimple(Effect, out Result);
        }

        private static bool TryParseAttributeLink(string Effect, ParseErrorInfo ErrorInfo, out PowerEffect Result)
        {
            Result = null;

            string[] Split = Effect.Split('{');
            if (Split.Length != 2 && Split.Length != 3)
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

            if (Split.Length == 3)
            {
                if (!AttributeEffect.EndsWith("}"))
                    return false;

                AttributeEffect = AttributeEffect.Substring(0, AttributeEffect.Length - 1);
            }

            float ParsedEffect;
            FloatFormat ParsedEffectFormat;
            if (!Tools.TryParseFloat(AttributeEffect, out ParsedEffect, out ParsedEffectFormat))
                return false;

            if (Split.Length == 3)
            {
                string AttributeSkill = Split[2];
                PowerSkill ConvertedPowerSkill;
                if (StringToEnumConversion<PowerSkill>.TryParse(AttributeSkill, out ConvertedPowerSkill, ErrorInfo))
                {
                    Result = new PowerAttributeLink(AttributeName, ParsedEffect, ParsedEffectFormat, ConvertedPowerSkill);
                    return true;
                }
                else
                {
                    Result = new PowerAttributeLink(AttributeName, ParsedEffect, ParsedEffectFormat);
                    return true;
                }
            }
            else
            {
                Result = new PowerAttributeLink(AttributeName, ParsedEffect, ParsedEffectFormat);
                return true;
            }
        }

        private static bool TryParseSimple(string Effect, out PowerEffect Result)
        {
            Result = null;

            if (Effect.Contains("{") || Effect.Contains("}"))
                return false;

            Result = new PowerSimpleEffect(Effect);
            return true;
        }
    }
}
