using System.Collections.Generic;
using System.Diagnostics;

namespace NetTools
{
    public abstract class DatabaseOperation
    {
        public static readonly string VersionParameterName = "dbavn"; // Database Application Version Number
        public static string VersionParameter { private get; set; }

        public DatabaseOperation(string name, string scriptName, IDictionary<string, string> parameters, CompletionEventHandler callback)
        {
            Name = name;
            ScriptName = scriptName;
            Parameters = parameters;
            Callback = callback;
        }

        public string Name { get; private set; }
        public string ScriptName { get; private set; }
        public IDictionary<string, string> Parameters { get; private set; }
        public CompletionEventHandler Callback { get; private set; }

        public virtual string RequestString(string requestScriptPath)
        {
            IDictionary<string, string> RequestParameters = GetRequestParameters();

            string ParameterString = "";
            foreach (KeyValuePair<string, string> Entry in RequestParameters)
            {
                if (ParameterString.Length == 0)
                    ParameterString += "?";
                else
                    ParameterString += "&";
                ParameterString += $"{Entry.Key}={Entry.Value}";
            }

            string Request = $"{requestScriptPath}{ScriptName}{ParameterString}";

            return Request;
        }

        public abstract string TypeName { get; }

        public virtual void DebugStart()
        {
            string Line = $"{TypeName} {Name}, script={ScriptName}";
            IDictionary<string, string> RequestParameters = GetRequestParameters();

            foreach (KeyValuePair<string, string> Entry in RequestParameters)
                Line += $", {Entry.Key}={Entry.Value}";

            Debug.WriteLine(Line);
        }

        protected IDictionary<string, string> GetRequestParameters()
        {
            IDictionary<string, string> RequestParameters = new Dictionary<string, string>(Parameters);
            if (VersionParameter != null && !RequestParameters.ContainsKey(VersionParameterName))
                RequestParameters.Add(VersionParameterName, VersionParameter);

            return RequestParameters;
        }
    }
}
