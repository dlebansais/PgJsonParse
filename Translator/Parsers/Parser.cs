namespace Translator
{
    using PgJsonReader;
    using System;
    using System.Collections.Generic;

    public abstract class Parser
    {
        public abstract object CreateItem();

        public virtual bool FinishItem(ref object item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> ContentTypeTable, List<object> itemCollection, Json.Token LastItemType, string parsedFile, string parsedKey)
        {
            return true; //TODO: abstract
        }

        public static bool SetBoolProperty(Action<bool> setter, object value)
        {
            if (value is bool ValueBool)
            {
                setter(ValueBool);
                return true;
            }
            else
                return Program.ReportFailure($"Value {value} was expected to be a bool");
        }

        public static bool SetIntProperty(Action<int> setter, object value)
        {
            if (value is int ValueInt)
            {
                setter(ValueInt);
                return true;
            }
            else if (value is string ValueString && int.TryParse(ValueString, out int ValueIntFromString))
            {
                setter(ValueIntFromString);
                return true;
            }
            else
                return Program.ReportFailure($"Value {value} was expected to be an int");
        }

        public static bool SetFloatProperty(Action<float> setter, object value)
        {
            if (value is float ValueFloat)
            {
                setter(ValueFloat);
                return true;
            }
            else if (value is int ValueInt)
            {
                setter((float)ValueInt);
                return true;
            }
            else if (value is string ValueString && Tools.TryParseSingle(ValueString, out float ValueFloatFromString))
            {
                setter(ValueFloatFromString);
                return true;
            }
            else
                return Program.ReportFailure($"Value {value} was expected to be a float");
        }

        public static bool SetStringProperty(Action<string> setter, object value)
        {
            if (value is string ValueString)
            {
                setter(ValueString);
                return true;
            }
            else
                return Program.ReportFailure($"Value {value} was expected to be a string");
        }

        public static bool SetTimeProperty(Func<TimeSpan?> getter, Action<TimeSpan> setter, int multiplier, object value)
        {
            if (value is int ValueInt)
            {
                TimeSpan ValueTimeSpan = TimeSpan.FromMinutes(ValueInt * multiplier);

                TimeSpan? OldValue = getter();
                if (!OldValue.HasValue)
                    OldValue = TimeSpan.Zero;

                setter(OldValue.Value + ValueTimeSpan);
                return true;
            }
            else
                return Program.ReportFailure($"Value {value} was expected to be an int");
        }
    }
}
