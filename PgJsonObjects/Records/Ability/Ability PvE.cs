using System;
using System.Collections.Generic;

namespace PgJsonParse
{
    public class AbilityPvE : GenericJsonObject<RawAbilityPvE>
    {
        #region Properties
        public string Key { get; private set; }
        public int? Damage { get; set; }
        public int? Range { get; set; }
        public int? PowerCost { get; set; }
        public string[] AttributesThatDeltaDamage { get; set; }
        public string[] AttributesThatModDamage { get; set; }
        public string[] AttributesThatModBaseDamage { get; set; }
        public int? ResetTime { get; set; }
        public string Skill { get; set; }
        public string Target { get; set; }
        public string TargetParticle { get; set; }
        #endregion

        #region Client Interface
        public override void Init(KeyValuePair<string, RawAbilityPvE> Entry, ParseErrorInfo ErrorInfo)
        {
            RawAbilityPvE Raw = Entry.Value;
            Key = Entry.Key;

        }

        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.CloseObject();
        }
        #endregion
    }
}
