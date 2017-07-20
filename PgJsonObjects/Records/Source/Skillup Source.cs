namespace PgJsonObjects
{
    public class SkillupSource : GenericSource
    {
        #region Init
        public SkillupSource(Skill Skill)
        {
            this.Skill = Skill;
        }
        #endregion

        #region Properties
        public Skill Skill { get; private set; }
        #endregion
    }
}
