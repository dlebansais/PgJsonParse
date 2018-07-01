using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgPowerTier : GenericPgObject<PgPowerTier>, IPgPowerTier
    {
        public PgPowerTier(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgPowerTier CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgPowerTier CreateNew(byte[] data, ref int offset)
        {
            return new PgPowerTier(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public PowerEffectCollection EffectList { get { return GetObjectList(4, ref _EffectList, PowerEffectCollection.CreateItem, () => new PowerEffectCollection()); } } private PowerEffectCollection _EffectList;
        public int SkillLevelPrereq { get { return RawSkillLevelPrereq.HasValue ? RawSkillLevelPrereq.Value : 0; } }
        public int? RawSkillLevelPrereq { get { return GetInt(8); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(12, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "EffectDescs", new FieldParser() {
                Type = FieldType.StringArray,
                GetStringArray = GetEffectDescs } },
            { "SkillLevelPrereq", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawSkillLevelPrereq } },
        }; } }

        private List<string> GetEffectDescs()
        {
            List<string> Result = new List<string>();

            foreach (PowerEffect Effect in EffectList)
                Result.Add(Effect.AsEffectString());

            return Result;
        }
    }
}
