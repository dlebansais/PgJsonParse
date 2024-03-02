using System.Collections.Generic;

namespace PgObjects
{
    public class PgNpcServiceInstallAugments : PgNpcService
    {
        public Favor Favor { get; set; }
        public PgNpcLevelRangeCollection LevelRangeList { get; set; } = new PgNpcLevelRangeCollection();
    }
}
