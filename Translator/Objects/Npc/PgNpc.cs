namespace PgJsonObjects
{
    public class PgNpc
    {
        public string Name { get; set; } = string.Empty;
        public string AreaFriendlyName { get; set; } = string.Empty;
        public PgNpcPreferenceCollection PreferenceList { get; set; } = new PgNpcPreferenceCollection();
        public PgNpcPreferenceCollection LikeList { get; set; } = new PgNpcPreferenceCollection();
        public PgNpcPreferenceCollection HateList { get; set; } = new PgNpcPreferenceCollection();
        public MapAreaName AreaName { get; set; }
    }
}
