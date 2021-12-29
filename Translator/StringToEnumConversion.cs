namespace Translator
{
    using PgObjects;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    public static class StringToEnumConversion
    {
        public static Dictionary<Type, bool[]> KnownParsedEnumtable { get; } = new Dictionary<Type, bool[]>();
        public static Dictionary<Type, List<string>> MissingEnumTable { get; } = new Dictionary<Type, List<string>>();

        public static bool FinalizeParsing()
        {
            if (!FinalizeMissing())
                return false;

            if (!FinalizeKnownParsed())
                return false;

            return true;
        }

        private static bool FinalizeKnownParsed()
        {
            if (KnownParsedEnumtable.Count != TextMaps.TotalEnumTypes)
            {
                List<string> NameList = new List<string>();
                foreach (KeyValuePair<Type, bool[]> Entry in KnownParsedEnumtable)
                {
                    string Name = Entry.Key.Name;
                    NameList.Add(Name);
                }

                NameList.Sort();

                foreach (string Name in NameList)
                    Debug.WriteLine(Name);

                return Program.ReportFailure("Some enum types are not used anymore");
            }

            string Result = string.Empty;

            foreach (KeyValuePair<Type, bool[]> Entry in KnownParsedEnumtable)
                FinalizeKnownParsed(Entry.Key, Entry.Value, ref Result);

            if (Result.Length > 0)
                return Program.ReportFailure($"The following enums are not used.\n\n{Result}");

            return true;
        }

        private static void FinalizeKnownParsed(Type type, bool[] usedValues, ref string text)
        {
            Debug.Assert(type.IsEnum);

            string[] EnumNames = type.GetEnumNames();
            Debug.Assert(EnumNames.Length == usedValues.Length);

            string EnumText = string.Empty;
            for (int i = 1; i < usedValues.Length; i++)
                if (!usedValues[i])
                    EnumText += $"    {EnumNames[i]}\n";

            if (EnumText.Length > 0)
            {
                if (text.Length > 0)
                    text += "\n";

                text += $"  {type.Name}:\n{EnumText}";
            }
        }

        private static bool FinalizeMissing()
        {
            string Result = string.Empty;

            foreach (KeyValuePair<Type, List<string>> Entry in MissingEnumTable)
                FinalizeMissing(Entry.Key, Entry.Value, ref Result);

            if (Result.Length > 0)
                return Program.ReportFailure($"The following enums are missing.\n\n{Result}");

            return true;
        }

        private static void FinalizeMissing(Type type, List<string> missingValues, ref string text)
        {
            Debug.Assert(type.IsEnum);

            string EnumText = string.Empty;
            for (int i = 0; i < missingValues.Count; i++)
            {
                string EnumName = missingValues[i];
                EnumText += $"    {EnumName},\n";
            }

            if (EnumText.Length > 0)
            {
                if (text.Length > 0)
                    text += "\n";

                text += $"  {type.Name}:\n{EnumText}";
            }
        }
    }
}
