namespace PgObjects
{
    using System.Collections.Generic;

    public class PgSkillAdvancementRewardAbility : PgSkillAdvancement
    {
        public List<string> Ability_Keys { get; set; } = new();
    }
}
