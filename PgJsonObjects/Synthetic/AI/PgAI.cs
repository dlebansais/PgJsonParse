namespace PgJsonObjects
{
    public class PgAI : MainPgObject<PgAI>, IPgAI
    {
        public PgAI(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgAI CreateItem(byte[] data, int offset)
        {
            return new PgAI(data, offset);
        }

        public AIAbilitySet Abilities { get { return GetObject(0, ref _Abilities); } } private AIAbilitySet _Abilities;
        public string Comment { get { return GetString(4); } }
        public float? RawMinDelayBetweenAbilities { get { return (float)GetDouble(8); } }
        public bool? RawIsMelee { get { return GetBool(12, 0); } }
        public bool? RawIsUncontrolledPet { get { return GetBool(12, 2); } }
        public bool? RawIsStationary { get { return GetBool(12, 4); } }
        public bool? RawIsServerDriven { get { return GetBool(12, 6); } }
    }
}
