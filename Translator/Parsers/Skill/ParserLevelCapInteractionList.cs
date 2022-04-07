namespace Translator
{
    using System.Collections.Generic;
    using PgJsonReader;
    using PgObjects;

    public class ParserLevelCapInteractionList : Parser
    {
        public override object CreateItem()
        {
            return new PgLevelCapInteractionList();
        }

        public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
        {
            if (item is not PgLevelCapInteractionList AsPgLevelCapInteractionList)
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgLevelCapInteractionList, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgLevelCapInteractionList item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
        {
            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                if (!(Value is int EntryLevel))
                    return Program.ReportFailure($"Invalid level cap interaction '{Value}'");

                if (!ParseInteraction(item, Key, EntryLevel, parsedFile, parsedKey))
                    return false;
            }

            return true;
        }

        private bool ParseInteraction(PgLevelCapInteractionList item, string interaction, int level, string parsedFile, string parsedKey)
        {
            if (interaction.Length > 0 && char.IsDigit(interaction[interaction.Length - 1]))
            {
                int FirstDigitIndex = interaction.Length - 1;
                while (FirstDigitIndex > 0 && char.IsDigit(interaction[FirstDigitIndex - 1]))
                    FirstDigitIndex--;

                if (FirstDigitIndex > 0 && interaction[FirstDigitIndex - 1] != '_' && interaction[FirstDigitIndex - 1] != ' ')
                    interaction = interaction.Substring(0, FirstDigitIndex) + "_" + interaction.Substring(FirstDigitIndex);
            }

            string[] Split = interaction.Split('_');

            if (Split.Length < 3 || Split[0] != "LevelCap")
                return Program.ReportFailure($"Invalid level cap interaction '{interaction}'");

            string MergedSkill = string.Empty;
            int i;
            for (i = 1; i + 1 < Split.Length; i++)
            {
                if (MergedSkill.Length > 0)
                    MergedSkill += "_";
                MergedSkill += Split[i];
            }

            if (MergedSkill == "Dance")
                MergedSkill = "Performance_Dance";

            PgSkill ParsedSkill = null!;
            if (!ParserSkill.Parse((PgSkill valueSkill) => ParsedSkill = valueSkill, MergedSkill, parsedFile, parsedKey))
                return false;

            int OtherLevel;
            if (!int.TryParse(Split[i], out OtherLevel) || OtherLevel <= 0)
                return Program.ReportFailure($"Invalid level cap interaction '{interaction}'");

            if (OtherLevel != level + 10 && OtherLevel != level + 5)
                return Program.ReportFailure("Inconsistent interaction level cap");

            PgLevelCapInteraction NewInteraction = new PgLevelCapInteraction();
            NewInteraction.RawLevel = level;
            NewInteraction.RawRangeUnlock = OtherLevel - level;
            NewInteraction.Skill_Key = ParsedSkill.Key;

            ParsingContext.AddSuplementaryObject(NewInteraction);
            item.List.Add(NewInteraction);
            return true;
        }
    }
}
