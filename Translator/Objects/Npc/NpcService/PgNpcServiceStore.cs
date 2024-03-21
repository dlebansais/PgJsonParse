namespace PgObjects
{
    public class PgNpcServiceStore : PgNpcService
    {
        public Favor Favor { get; set; }
        public PgNpcCapIncreaseCollection CapIncreaseList { get; set; } = new();
    }
}
