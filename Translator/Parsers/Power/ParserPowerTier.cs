namespace Translator
{
    using PgObjects;
    using PgJsonReader;
    using System.Collections.Generic;

    public class ParserPowerTier : Parser
    {
        public override object CreateItem()
        {
            return new PgPowerTier();
        }

        public override bool FinishItem(ref object item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            if (!(item is PgPowerTier AsPgPowerTier))
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgPowerTier, contentTable, ContentTypeTable, itemCollection, LastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgPowerTier item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            bool Result = true;

            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                switch (Key)
                {
                    case "EffectDescs":
                        Result = ParseEffectDescriptionList(item.EffectList, Value, parsedFile, parsedKey);
                        break;
                    case "SkillLevelPrereq":
                        Result = SetIntProperty((int valueInt) => item.RawSkillLevelPrereq = valueInt, Value);
                        break;
                    default:
                        Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}' not handled");
                        break;
                }

                if (!Result)
                    break;
            }

            return Result;
        }

        public static bool ParseEffectDescriptionList(PgPowerEffectCollection effectDescriptionList, object value, string parsedFile, string parsedKey)
        {
            if (!(value is List<object> ObjectList))
                return Program.ReportFailure(parsedFile, parsedKey, $"Value '{value}' was expected to be a list");

            foreach (object Item in ObjectList)
            {
                if (!(Item is string EffectDescription))
                    return Program.ReportFailure(parsedFile, parsedKey, $"Value '{Item}' was expected to be a string");

                if (!ParseEffectDescription(effectDescriptionList, EffectDescription, parsedFile, parsedKey))
                    return false;
            }

            return true;
        }

        private static bool ParseEffectDescription(PgPowerEffectCollection effectDescriptionList, string effectDescription, string parsedFile, string parsedKey)
        {
            PgPowerEffect PowerEffect;
            if (effectDescription.StartsWith("{") && effectDescription.EndsWith("}"))
            {
                string EffectString = effectDescription.Substring(1, effectDescription.Length - 2);
                if (!ParseItemEffectAttribute(EffectString, parsedFile, parsedKey, out PowerEffect))
                    return Program.ReportFailure(parsedFile, parsedKey, $"Invalid attribute format '{EffectString}'");
            }
            else if (effectDescription.Contains("{") || effectDescription.Contains("}"))
                return Program.ReportFailure(parsedFile, parsedKey, $"Invalid attribute format '{effectDescription}'");
            else
                PowerEffect = new PgPowerEffectSimple() { Description = effectDescription };

            ParsingContext.AddSuplementaryObject(PowerEffect);
            effectDescriptionList.Add(PowerEffect);
            return true;
        }

        private static bool ParseItemEffectAttribute(string effectString, string parsedFile, string parsedKey, out PgPowerEffect itemEffect)
        {
            itemEffect = null;

            string[] Split = effectString.Split('{');
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

            PgAttribute ParsedAttribute = null;
            if (!Inserter<PgAttribute>.SetItemByKey((PgAttribute valueAttribute) => ParsedAttribute = valueAttribute, AttributeName))
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

            if (ParsedEffectFormat != FloatFormat.Standard)
                return false;

            if (Split.Length == 3)
            {
                string AttributeSkill = Split[2];

                PgSkill ParsedSkill = null;

                if (AttributeSkill == "AnySkill")
                    ParsedSkill = PgSkill.AnySkill;
                else if (!Inserter<PgSkill>.SetItemByKey((PgSkill valueSkill) => ParsedSkill = valueSkill, AttributeSkill))
                    return false;

                itemEffect = new PgPowerEffectAttribute() { Attribute = ParsedAttribute, AttributeEffect = ParsedEffect, AttributeEffectFormat = ParsedEffectFormat, Skill = ParsedSkill };
            }
            else
                itemEffect = new PgPowerEffectAttribute() { Attribute = ParsedAttribute, AttributeEffect = ParsedEffect, AttributeEffectFormat = ParsedEffectFormat };

            return true;
        }
    }
}
