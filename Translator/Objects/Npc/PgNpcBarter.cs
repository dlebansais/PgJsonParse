namespace PgObjects
{
    using System.Collections.Generic;

    public class PgNpcBarter
    {
        public Dictionary<string, int> GiveTable { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, int> ReceiveTable { get; set; } = new Dictionary<string, int>();
    }
}
