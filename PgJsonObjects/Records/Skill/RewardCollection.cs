using System.Collections.Generic;

namespace PgJsonObjects
{
    public class RewardCollection : List<IPgReward>, IPgRewardCollection, ISerializableJsonObjectCollection
    {
    }
}
