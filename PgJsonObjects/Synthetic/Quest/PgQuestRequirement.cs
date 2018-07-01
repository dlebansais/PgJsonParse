using System.Collections.Generic;

namespace PgJsonObjects
{
    public abstract class PgQuestRequirement<TPg> : GenericPgObject<TPg>
        where TPg : IDeserializablePgObject
    {
        public const int PropertiesOffset = 12;

        public PgQuestRequirement(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public OtherRequirementType OtherRequirementType { get { return GetEnum<OtherRequirementType>(0); } }
        public override string Key { get { return GetString(4); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(8, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
    }
}
