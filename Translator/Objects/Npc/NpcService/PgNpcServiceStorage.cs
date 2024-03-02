using System.Collections.Generic;

namespace PgObjects
{
    public class PgNpcServiceStorage : PgNpcService
    {
        public Favor Favor { get; set; }
        public List<string> ItemDescriptionList { get; set; } = new List<string>();
        public List<Favor> SpaceIncreaseList { get; set; } = new List<Favor>();
    }
}
