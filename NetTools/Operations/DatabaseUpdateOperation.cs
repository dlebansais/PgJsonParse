using System.Collections.Generic;

namespace NetTools
{
    public class DatabaseUpdateOperation : DatabaseOperation
    {
        public DatabaseUpdateOperation(string name, string scriptName, IDictionary<string, string> parameters, CompletionEventHandler callback)
            : base(name, scriptName, parameters, callback)
        {
        }

        public override string TypeName { get { return "Update"; } }
    }
}
