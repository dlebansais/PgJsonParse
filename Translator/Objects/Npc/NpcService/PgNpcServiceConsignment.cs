using System.Collections.Generic;

namespace PgObjects
{
    public class PgNpcServiceConsignment : PgNpcService
    {
        public Favor Favor { get; set; }
        public List<ItemKeyword> ItemTypeList { get; set; } = new List<ItemKeyword>();
        public List<Favor> UnlockList { get; set; } = new List<Favor>();
    }
}
