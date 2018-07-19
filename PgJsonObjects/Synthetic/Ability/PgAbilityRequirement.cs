using System.Collections.Generic;

namespace PgJsonObjects
{
    public abstract class PgAbilityRequirement<TPg> : GenericPgObject<TPg>, IPgAbilityRequirement
        where TPg : IDeserializablePgObject
    {
        public PgAbilityRequirement(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public abstract OtherRequirementType Type { get; }

        public virtual IList<IBackLinkable> GetLinkBack()
        {
            return null;
        }

        #region Indirect Properties
        public override string SortingName { get { return null; } }
        #endregion

    }
}
