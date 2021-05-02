namespace PgJsonReader
{
    using System;

    public class DefaultStringHandler : IStringHandler
    {
        public string ReadString(string input)
        {
            return input;
        }

        public string WriteString(string input)
        {
            return input;
        }
    }
}
