using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAI : MainPgObject<PgAI>, IPgAI
    {
        public PgAI(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 22;
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
        public float? RawMinDelayBetweenAbilities { get { return (float?)GetDouble(12); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(16, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public bool? RawIsMelee { get { return GetBool(20, 0); } }
        public bool? RawIsUncontrolledPet { get { return GetBool(20, 2); } }
        public bool? RawIsStationary { get { return GetBool(20, 4); } }
        public bool? RawIsServerDriven { get { return GetBool(20, 6); } }
        public bool? RawUseAbilitiesWithoutEnemyTarget { get { return GetBool(20, 8); } }
        public bool? RawSwimming { get { return GetBool(20, 10); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Abilities", new FieldParser() {
                Type = FieldType.Object,
                GetObject = () => Abilities as IObjectContentGenerator } },
            { "Melee", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawIsMelee } },
            { "Comment", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Comment } },
            { "Stationary", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawIsStationary } },
            { "UncontrolledPet", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawIsUncontrolledPet } },
            { "ServerDriven", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawIsServerDriven } },
            { "MinDelayBetweenAbilities", new FieldParser() {
                Type = FieldType.Float,
                GetFloat = () => RawMinDelayBetweenAbilities } },
            { "UseAbilitiesWithoutEnemyTarget", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawUseAbilitiesWithoutEnemyTarget } },
            { "Swimming", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawSwimming} },
        }; } }

        #region Indirect Properties
        public override string SortingName { get { return null; } }
        #endregion
    }
}
