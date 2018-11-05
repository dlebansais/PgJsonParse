using System.Collections.Generic;

namespace NetTools
{
    public class DatabaseEncryptOperation : DatabaseOperation
    {
        public DatabaseEncryptOperation(string name, string scriptName, string constantName, string constantValue, string parameterName, string parameterValue, CompletionEventHandler callback)
            : base(name, scriptName, new Dictionary<string, string>() { { constantName, constantValue }, { parameterName, parameterValue } }, callback)
        {
        }

        public override string TypeName { get { return "Encrypt"; } }
    }
}
