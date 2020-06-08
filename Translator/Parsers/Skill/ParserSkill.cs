namespace Translator
{
    using PgJsonObjects;
    using System;

    public class ParserSkill : Parser
    {
        public override object CreateItem()
        {
            return new PgSkill();
        }

        public static bool Parse(Action<PgSkill> setter, object value, string parsedFile, string parsedKey, ErrorControl errorControl = ErrorControl.Normal)
        {
            if (!(value is string ValueKey))
                return Program.ReportFailure(parsedFile, parsedKey, $"Value '{value}' was expected to be a string");

            if (ValueKey == "Unknown")
            {
                setter(PgSkill.Unknown);
                return true;
            }
            else if (ValueKey == "AnySkill")
            {
                setter(PgSkill.AnySkill);
                return true;
            }
            else
                return Inserter<PgSkill>.SetItemByKey(setter, value, errorControl);
        }
    }
}
