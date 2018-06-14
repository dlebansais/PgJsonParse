using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilityRequirementOr: GenericPgObject, IPgAbilityRequirementOr
    {
        public PgAbilityRequirementOr(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public List<AbilityRequirement> OrList { get { return GetObjectList(0, ref _OrList); } } private List<AbilityRequirement> _OrList;
        public string ErrorMsg { get { return GetString(4); } }
    }
}
