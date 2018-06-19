namespace PgJsonObjects
{
    public class SkillupSource : GenericSource
    {
        #region Init
        public SkillupSource(IPgSkill Skill)
        {
            this.Skill = Skill;
        }
        #endregion

        #region Properties
        public IPgSkill Skill { get; private set; }
        #endregion
    }
}
