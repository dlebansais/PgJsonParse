namespace PgJsonObjects
{
    public class EffectSource : GenericSource
    {
        #region Init
        public EffectSource(Effect Effect)
        {
            this.Effect = Effect;
        }
        #endregion

        #region Properties
        public Effect Effect { get; private set; }
        #endregion
    }
}
