namespace PgObjects
{
    public class PgNpc
    {
        public string Name { get; set; } = string.Empty;
        public MapAreaName AreaName { get; set; }
        public string AreaFriendlyName { get; set; } = string.Empty;
        public PgNpcPreferenceCollection PreferenceList { get; } = new PgNpcPreferenceCollection();
    }
}
