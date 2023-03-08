namespace PgObjects
{
    public class PgAbilityRequirementWeather : PgAbilityRequirement
    {
        public int BoolValues { get; set; }
        public const int ClearSkyNotNull = 1 << 0;
        public const int ClearSkyIsTrue = 1 << 1;
        public bool ClearSky { get { return (BoolValues & (ClearSkyNotNull + ClearSkyIsTrue)) == (ClearSkyNotNull + ClearSkyIsTrue); } }
        public bool? RawClearSky { get { return ((BoolValues & ClearSkyNotNull) != 0) ? (BoolValues & ClearSkyIsTrue) != 0 : null; } }
        public void SetClearSky(bool value) { BoolValues |= (BoolValues & ~(ClearSkyNotNull + ClearSkyIsTrue)) | ((value ? ClearSkyIsTrue : 0) + ClearSkyNotNull); }
    }
}
