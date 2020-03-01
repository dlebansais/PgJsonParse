using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class ParseErrorInfo
    {
        public ParseErrorInfo()
        {
            InvalidObjectFormatList = new List<string>();
            MissingEnumTable = new Dictionary<string, List<string>>();
            InvalidStringTable = new Dictionary<string, List<string>>();
            DuplicateStringTable = new Dictionary<string, List<string>>();
            MissingKeyList = new List<string>();
            UnparsedSpecialInfoList = new List<string>();
            IconList = new List<int>();
            UnparsedFieldList = new List<string>();
            MissingFieldList = new List<string>();
        }

        private List<string> InvalidObjectFormatList;
        private Dictionary<string, List<string>> MissingEnumTable;
        private Dictionary<string, List<string>> InvalidStringTable;
        private Dictionary<string, List<string>> DuplicateStringTable;
        private List<string> MissingKeyList;
        private List<string> UnparsedSpecialInfoList;
        public List<int> IconList { get; private set; }
        private List<string> UnparsedFieldList;
        private List<string> MissingFieldList;
        public bool IgnoreUnparsedEnums { get; set; } = false;

        public void AddInvalidObjectFormat(string ObjectFormatName)
        {
            if (!InvalidObjectFormatList.Contains(ObjectFormatName))
                InvalidObjectFormatList.Add(ObjectFormatName);
        }

        public void AddMissingEnum(string EnumTypeName, string EnumName)
        {
            if (!MissingEnumTable.ContainsKey(EnumTypeName))
                MissingEnumTable.Add(EnumTypeName, new List<string>());

            List<string> MissingEnumList = MissingEnumTable[EnumTypeName];

            if (!MissingEnumList.Contains(EnumName))
                MissingEnumList.Add(EnumName);
        }

        public void AddInvalidString(string StringFormatName, string EnumName)
        {
            if (!InvalidStringTable.ContainsKey(StringFormatName))
                InvalidStringTable.Add(StringFormatName, new List<string>());

            List<string> InvalidStringList = InvalidStringTable[StringFormatName];

            if (!InvalidStringList.Contains(EnumName))
                InvalidStringList.Add(EnumName);
        }

        public void AddDuplicateString(string StringFormatName, string EnumName)
        {
            if (!DuplicateStringTable.ContainsKey(StringFormatName))
                DuplicateStringTable.Add(StringFormatName, new List<string>());

            List<string> DuplicateStringList = DuplicateStringTable[StringFormatName];

            if (!DuplicateStringList.Contains(EnumName))
                DuplicateStringList.Add(EnumName);
        }

        public void AddMissingKey(string KeyName)
        {
            if (!MissingKeyList.Contains(KeyName))
                MissingKeyList.Add(KeyName);
        }

        public void AddUnparsedSpecialInfo(string SpecialInfo)
        {
            if (!UnparsedSpecialInfoList.Contains(SpecialInfo))
                UnparsedSpecialInfoList.Add(SpecialInfo);
        }

        public void AddUnparsedField(string Field)
        {
            if (!UnparsedFieldList.Contains(Field))
                UnparsedFieldList.Add(Field);
        }

        public void AddMissingField(string Field)
        {
            if (!MissingFieldList.Contains(Field))
                MissingFieldList.Add(Field);
        }

        public void LookForIconId(string s)
        {
            if (s == null)
                return;

            if (s.Contains("Ice Spear Power Cost"))
                if (s == null)
                    return;

            string Pattern = "<icon=";

            int IndexStart = s.IndexOf(Pattern);
            if (IndexStart < 0)
                return;

            IndexStart += Pattern.Length;
            int IndexEnd = s.IndexOf(">", IndexStart);
            if (IndexEnd <= IndexStart)
                return;

            string IconIdString = s.Substring(IndexStart, IndexEnd - IndexStart);
            ParseIconId(IconIdString);
        }

        public void ParseIconId(string IconIdString)
        {
            int IconId;
            if (!int.TryParse(IconIdString, out IconId) || IconId <= 0)
                return;

            AddIconId(IconId);
        }

        public void AddIconId(int IconId)
        {
            if (!IconList.Contains(IconId))
                IconList.Add(IconId);
        }

        public string GetWarnings()
        {
            string Result = "";

            if (InvalidObjectFormatList.Count > 0)
            {
                string InvalidObjectFormatString = "";

                foreach (string InvalidObjectFormat in InvalidObjectFormatList)
                    InvalidObjectFormatString += InvalidObjectFormat + "\r\n";

                Result += "Invalid object formats:\r\n" + InvalidObjectFormatString + "\r\n";
            }

            if (MissingEnumTable.Count > 0)
            {
                string MissingEnumString = "";
                foreach (KeyValuePair<string, List<string>> Entry in MissingEnumTable)
                {
                    string EnumType = Entry.Key;
                    List<string> ValueList = Entry.Value;

                    if (MissingEnumString.Length > 0)
                        MissingEnumString += "\r\n";

                    MissingEnumString += "  " + Entry.Key + ":" + "\r\n";
                    foreach (string s in Entry.Value)
                        MissingEnumString += "    " + s + "\r\n";
                }

                Result += "Missing enums:\r\n" + MissingEnumString + "\r\n";
            }

            if (InvalidStringTable.Count > 0)
            {
                string InvalidStringString = "";
                foreach (KeyValuePair<string, List<string>> Entry in InvalidStringTable)
                {
                    string EnumType = Entry.Key;
                    List<string> ValueList = Entry.Value;

                    if (InvalidStringString.Length > 0)
                        InvalidStringString += "\r\n";

                    InvalidStringString += "  " + Entry.Key + ":" + "\r\n";
                    foreach (string s in Entry.Value)
                        InvalidStringString += "    " + s + "\r\n";
                }

                Result += "Invalid strings:\r\n" + InvalidStringString + "\r\n";
            }

            if (DuplicateStringTable.Count > 0)
            {
                string DuplicateStringString = "";
                foreach (KeyValuePair<string, List<string>> Entry in DuplicateStringTable)
                {
                    string EnumType = Entry.Key;
                    List<string> ValueList = Entry.Value;

                    if (DuplicateStringString.Length > 0)
                        DuplicateStringString += "\r\n";

                    DuplicateStringString += "  " + Entry.Key + ":" + "\r\n";
                    foreach (string s in Entry.Value)
                        DuplicateStringString += "    " + s + "\r\n";
                }

                Result += "Duplicate strings:\r\n" + DuplicateStringString + "\r\n";
            }

            if (MissingKeyList.Count > 0)
            {
                string MissingKeyString = "";

                foreach (string MissingKey in MissingKeyList)
                    MissingKeyString += MissingKey + "\r\n";

                Result += "Missing keys:\r\n" + MissingKeyString + "\r\n";
            }

            if (UnparsedSpecialInfoList.Count > 0)
            {
                string UnparsedSpecialInfo = "";

                foreach (string SpecialInfo in UnparsedSpecialInfoList)
                    UnparsedSpecialInfo += SpecialInfo + "\r\n";

                Result += "Unparsed special info:\r\n" + UnparsedSpecialInfo + "\r\n";
            }

            if (UnparsedFieldList.Count > 0)
            {
                string UnparsedField = "";

                foreach (string Field in UnparsedFieldList)
                    UnparsedField += Field + "\r\n";

                Result += "Unparsed field info:\r\n" + UnparsedField + "\r\n";
            }

            if (MissingFieldList.Count > 0)
            {
                string MissingField = "";

                foreach (string Field in MissingFieldList)
                    MissingField += Field + "\r\n";

                Result += "Missing field info:\r\n" + MissingField + "\r\n";
            }

            if (!IgnoreUnparsedEnums)
            {
                string AllNotEnumerated = "";
                foreach (KeyValuePair<Type, bool[]> Entry in StringToEnumConversion.KnownParsedEnumtable)
                {
                    string NotEnumerated = "";
                    string[] EnumNames = Enum.GetNames(Entry.Key);
                    for (int i = 1; i < Entry.Value.Length; i++)
                        if (!Entry.Value[i])
                        {
                            if (Entry.Key == typeof(ItemKeyword) && i == (int)ItemKeyword.Any)
                                continue;
                            if (Entry.Key == typeof(RecipeItemKey) && (i == (int)RecipeItemKey.Rarity_Common || i == (int)RecipeItemKey.MinRarity_Rare))
                                continue;
                            if (Entry.Key == typeof(RecipeEffect) && (i >= (int)RecipeEffect.ExtractTSysPower))
                                continue;

                            if (NotEnumerated.Length > 0)
                                NotEnumerated += "\n";
                            NotEnumerated += "\t\t" + EnumNames[i];
                        }

                    if (NotEnumerated.Length > 0)
                    {
                        if (AllNotEnumerated.Length == 0)
                            AllNotEnumerated += "The following enums have not been found:\n";

                        AllNotEnumerated += "\t" + Entry.Key.Name + "\n" + NotEnumerated + "\n";
                    }
                }

                Result += AllNotEnumerated;
            }

            return Result;
        }
    }
}
