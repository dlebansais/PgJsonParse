#if CSHTML5
using System;
using System.Collections.Generic;
using System.IO;

namespace Presentation
{
    public static class Persistent
    {
        #region Init
        static Persistent()
        {
            InitSettings();
        }
        #endregion

        #region Settings
        private static void InitSettings()
        {
            SettingTable.Clear();

            string Content = FileTools.LoadTextFile(SettingFileName, FileMode.OpenOrCreate);
            if (Content == null)
                Content = "";

            string[] SplitContent = Content.Split('\n');
            foreach (string Line in SplitContent)
            {
                string[] SplitLine = Line.Split(new string[] { KeyValueSeparator }, StringSplitOptions.None);
                if (SplitLine.Length == 2)
                {
                    string Key = SplitLine[0].Trim();
                    string Value = SplitLine[1].Trim();
                    if (Key.Length > 0)
                    {
                        bool AsBool;
                        int AsInt;

                        if (bool.TryParse(Value, out AsBool))
                            SettingTable.Add(Key, new bool?(AsBool));
                        else if (int.TryParse(Value, out AsInt))
                            SettingTable.Add(Key, new int?(AsInt));
                        else
                            SettingTable.Add(Key, Value);
                    }
                }
            }
        }

        private static Dictionary<string, object> SettingTable { get; } = new Dictionary<string, object>();

        private static object GetSettingKey(string ValueName)
        {
            try
            {
                if (SettingTable.ContainsKey(ValueName))
                    return SettingTable[ValueName];
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }

        private static void SetSettingKey(string ValueName, object Value)
        {
            try
            {
                if (SettingTable.ContainsKey(ValueName))
                    SettingTable[ValueName] = Value;
                else
                    SettingTable.Add(ValueName, Value);

                if (IsAutoCommitSettings)
                    CommitSettings();
            }
            catch
            {
            }
        }

        private static void DeleteSetting(string ValueName)
        {
            try
            {
                if (SettingTable.ContainsKey(ValueName))
                    SettingTable.Remove(ValueName);

                if (IsAutoCommitSettings)
                    CommitSettings();
            }
            catch
            {
            }
        }

        public static bool IsAutoCommitSettings { get { return true; } }

        public static bool GetSettingBool(string ValueName, bool Default)
        {
            bool? Value = GetSettingKey(ValueName) as bool?;
            return Value.HasValue ? Value.Value : Default;
        }

        public static void SetSettingBool(string ValueName, bool Value)
        {
            SetSettingKey(ValueName, new bool?(Value));
        }

        public static int GetSettingInt(string ValueName, int Default)
        {
            int? Value = GetSettingKey(ValueName) as int?;
            return Value.HasValue ? Value.Value : Default;
        }

        public static int GetSettingEnum(string ValueName, int Default, int Max)
        {
            int? Value = GetSettingKey(ValueName) as int?;
            return Value.HasValue ? (Value.Value >= 0 && Value.Value <= Max ? Value.Value : 0) : Default;
        }

        public static void SetSettingInt(string ValueName, int Value)
        {
            SetSettingKey(ValueName, new int?(Value));
        }

        public static string GetSettingString(string ValueName, string Default)
        {
            string Value = GetSettingKey(ValueName) as string;
            return Value != null ? Value : Default;
        }

        public static void SetSettingString(string ValueName, string Value)
        {
            if (Value == null)
                DeleteSetting(ValueName);
            else
                SetSettingKey(ValueName, Value);
        }

        public static void CommitSettings()
        {
            string Content = "";

            foreach (KeyValuePair<string, object> Entry in SettingTable)
                if (Entry.Value != null)
                {
                    string Line = Entry.Key + KeyValueSeparator + Entry.Value.ToString() + "\n";
                    Content += Line;
                }

            FileTools.CommitTextFile(SettingFileName, Content);
        }

        private static readonly string SettingFileName = "persistentsettings.txt";
        private static readonly string KeyValueSeparator = " !$===@* ";
        #endregion
    }
}
#else
using Microsoft.Win32;
using System;

namespace Presentation
{
    public static class Persistent
    {
        #region Init
        static Persistent()
        {
            InitSettings();
        }
        #endregion

        #region Settings
        private static void InitSettings()
        {
            try
            {
                RegistryKey Key = Registry.CurrentUser.OpenSubKey(@"Software", true);
                Key = Key.CreateSubKey("Project Gorgon Tools");
                SettingKey = Key.CreateSubKey("PgJsonParse");
            }
            catch
            {
            }
        }

        private static object GetSettingKey(string ValueName)
        {
            try
            {
                return SettingKey?.GetValue(ValueName);
            }
            catch
            {
                return null;
            }
        }

        private static void SetSettingKey(string ValueName, object Value, RegistryValueKind Kind)
        {
            try
            {
                SettingKey?.SetValue(ValueName, Value, Kind);
            }
            catch
            {
            }
        }

        private static void DeleteSetting(string ValueName)
        {
            try
            {
                SettingKey?.DeleteValue(ValueName, false);
            }
            catch
            {
            }
        }

        public static bool IsAutoCommitSettings { get { return true; } }

        public static bool GetSettingBool(string ValueName, bool Default)
        {
            int? Value = GetSettingKey(ValueName) as int?;
            return Value.HasValue ? (Value.Value != 0) : Default;
        }

        public static void SetSettingBool(string ValueName, bool Value)
        {
            SetSettingKey(ValueName, Value ? 1 : 0, RegistryValueKind.DWord);
        }

        public static int GetSettingInt(string ValueName, int Default)
        {
            int? Value = GetSettingKey(ValueName) as int?;
            return Value.HasValue ? Value.Value : Default;
        }

        public static int GetSettingEnum(string ValueName, int Default, int Max)
        {
            int? Value = GetSettingKey(ValueName) as int?;
            return Value.HasValue ? (Value.Value >= 0 && Value.Value <= Max ? Value.Value : 0) : Default;
        }

        public static void SetSettingInt(string ValueName, int Value)
        {
            SetSettingKey(ValueName, Value, RegistryValueKind.DWord);
        }

        public static string GetSettingString(string ValueName, string Default)
        {
            string Value = GetSettingKey(ValueName) as string;
            return Value != null ? Value : Default;
        }

        public static void SetSettingString(string ValueName, string Value)
        {
            if (Value == null)
                DeleteSetting(ValueName);
            else
                SetSettingKey(ValueName, Value, RegistryValueKind.String);
        }

        public static void CommitSettings()
        {
        }

        private static RegistryKey SettingKey = null;
        #endregion
    }
}
#endif