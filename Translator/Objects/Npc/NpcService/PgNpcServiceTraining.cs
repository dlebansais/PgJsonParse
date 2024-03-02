namespace PgObjects
{
    using System.Collections.Generic;

    public class PgNpcServiceTraining : PgNpcService
    {
        public Favor Favor { get; set; }
        public List<string> Skill_Keys { get; set; } = new();
        public List<Favor> UnlockList { get; set; } = new List<Favor>();
    }
}
