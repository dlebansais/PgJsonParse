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

        public override string Key { get { return GetString(0); } }
        public IPgAIAbilitySet Abilities { get { return GetObject(4, ref _Abilities, PgAIAbilitySet.CreateNew); } } private IPgAIAbilitySet _Abilities;
        public string Comment { get { return GetString(8); } }
        public float? RawMinDelayBetweenAbilities { get { return (float)GetDouble(12); } }
        public bool? RawIsMelee { get { return GetBool(16, 0); } }
        public bool? RawIsUncontrolledPet { get { return GetBool(16, 2); } }
        public bool? RawIsStationary { get { return GetBool(16, 4); } }
        public bool? RawIsServerDriven { get { return GetBool(16, 6); } }
    }
}
