namespace PgObjects
{
    using System.Collections.Generic;

    public class PgNpc : PgObject
    {
        public string Name { get; set; } = string.Empty;
        public MapAreaName AreaName { get; set; }
        public string AreaFriendlyName { get; set; } = string.Empty;
        public PgNpcPreferenceCollection PreferenceList { get; set; } = new PgNpcPreferenceCollection();
        public string WikiAddress { get; set; } = string.Empty;
        public PgItemCollection SaleList { get; set; } = new PgItemCollection();
        public PgNpcBarterCollection BarterList { get; set; } = new PgNpcBarterCollection();
        public PgSourceCollection SourceAbilityList { get; set; } = new PgSourceCollection();
        public PgSourceCollection SourceRecipeList { get; set; } = new PgSourceCollection();
        public string Description { get; set; } = string.Empty;
        public List<Favor> ItemGiftList { get; set; } = new List<Favor>();
        public float PositionX { get { return RawPositionX.HasValue ? RawPositionX.Value : 0; } }
        public float? RawPositionX { get; set; }
        public float PositionY { get { return RawPositionY.HasValue ? RawPositionY.Value : 0; } }
        public float? RawPositionY { get; set; }
        public float PositionZ { get { return RawPositionZ.HasValue ? RawPositionZ.Value : 0; } }
        public float? RawPositionZ { get; set; }
        public PgNpcServiceCollection ServiceList { get; set; } = new PgNpcServiceCollection();

        public int IconId { get; set; }

        public override int ObjectIconId { get { return IconId; } }
        public override string ObjectName { get { return Name; } }
        public override string ToString() { return Name; }
    }
}
