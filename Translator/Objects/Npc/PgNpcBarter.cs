namespace PgObjects
{
    using System.Collections.Generic;

    public class PgNpcBarter
    {
        public Dictionary<PgItem, int> GiveTable { get; set; } = new Dictionary<PgItem, int>();
        public Dictionary<PgItem, int> ReceiveTable { get; set; } = new Dictionary<PgItem, int>();
    }
}
