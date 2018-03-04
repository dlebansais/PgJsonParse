using Microsoft.Win32;
using System;
using System.Windows;
using Taskbar;

namespace PgJsonParse
{
    public partial class App : Application
    {
        #region Init
        static App()
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

        private static RegistryKey SettingKey = null;
        #endregion

        #region Progress
        public static void SetState(Window window, TaskbarStates taskbarState)
        {
            window.Dispatcher.BeginInvoke(new Action(() => OnSetState(window, taskbarState)));
        }

        public static void OnSetState(Window window, TaskbarStates taskbarState)
        {
            TaskbarProgress.SetState(window, taskbarState);
        }

        public static void SetValue(Window window, double progressValue, double progressMax)
        {
            window.Dispatcher.BeginInvoke(new Action(() => OnSetValue(window, progressValue, progressMax)));
        }

        public static void OnSetValue(Window window, double progressValue, double progressMax)
        {
            TaskbarProgress.SetValue(window, progressValue, progressMax);
        }
        #endregion
    }
}
