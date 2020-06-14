namespace PgObjects
{
    public class PgNpc
    {
        public string Key { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public MapAreaName AreaName { get; set; }
        public string AreaFriendlyName { get; set; } = string.Empty;
        public PgNpcPreferenceCollection PreferenceList { get; set; } = new PgNpcPreferenceCollection();
    }
}
