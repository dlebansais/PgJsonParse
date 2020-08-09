namespace PgObjects
{
    public class PgNpc : PgObject
    {
        public string Key { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public MapAreaName AreaName { get; set; }
        public string AreaFriendlyName { get; set; } = string.Empty;
        public PgNpcPreferenceCollection PreferenceList { get; set; } = new PgNpcPreferenceCollection();
        public string WikiAddress { get; set; } = string.Empty;
        public PgItemCollection SaleList { get; set; } = new PgItemCollection();
        public PgNpcBarterCollection BarterList { get; set; } = new PgNpcBarterCollection();
        public PgSourceCollection SourceAbilityList { get; set; } = new PgSourceCollection();
        public PgSourceCollection SourceRecipeList { get; set; } = new PgSourceCollection();

        public int IconId { get; set; }

        public override int ObjectIconId { get { return IconId; } }
        public override string ObjectName { get { return Name; } }
        public override string ToString() { return Name; }
    }
}
