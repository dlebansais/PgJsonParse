namespace PgObjects
{
    using System.Collections.Generic;

    public class PgAdvancementHint
    {
        public Dictionary<int, string> HintTable { get; set; } = new Dictionary<int, string>();
        public PgNpcLocationCollection NpcList { get; set; } = new();
    }
}
