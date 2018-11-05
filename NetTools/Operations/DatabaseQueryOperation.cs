using System;
using System.Collections.Generic;

namespace NetTools
{
    public class DatabaseQueryOperation : DatabaseOperation
    {
        public DatabaseQueryOperation(string name, string scriptName, IDictionary<string, string> parameters, CompletionEventHandler callback)
            : base(name, scriptName, parameters, callback)
        {
        }

        public override string TypeName { get { return "Query"; } }
    }
}
