namespace PgJsonObjects
{
    public class PgAI : MainPgObject<PgAI>, IPgAI
    {
        public PgAI(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 14;
            SerializableJsonObject.AlignSerializedLength(ref offset);
        }

        protected override PgAI CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAI CreateNew(byte[] data, ref int offset)
        {
            return new PgAI(data, ref offset);
        }

        public IPgAIAbilitySet Abilities { get { return GetObject(0, ref _Abilities, PgAIAbilitySet.CreateNew); } } private IPgAIAbilitySet _Abilities;
        public string Comment { get { return GetString(4); } }
        public float? RawMinDelayBetweenAbilities { get { return (float)GetDouble(8); } }
        public bool? RawIsMelee { get { return GetBool(12, 0); } }
        public bool? RawIsUncontrolledPet { get { return GetBool(12, 2); } }
        public bool? RawIsStationary { get { return GetBool(12, 4); } }
        public bool? RawIsServerDriven { get { return GetBool(12, 6); } }
    }
}
