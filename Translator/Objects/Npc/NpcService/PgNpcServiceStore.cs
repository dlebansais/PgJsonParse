using System.Collections.Generic;

namespace PgObjects
{
    public class PgNpcServiceStore : PgNpcService
    {
        public Favor Favor { get; set; }
        public List<Favor> CapIncreaseList { get; set; } = new List<Favor>();
    }
}
