using System.Collections.Generic;

namespace PgObjects
{
    public class PgNpcServiceBarter : PgNpcService
    {
        public Favor Favor { get; set; }
        public List<Favor> AdditionalUnlockList { get; set; } = new List<Favor>();
    }
}
