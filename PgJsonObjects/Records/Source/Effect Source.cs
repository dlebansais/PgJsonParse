namespace PgJsonObjects
{
    public class EffectSource : GenericSource
    {
        #region Init
        public EffectSource(IPgEffect Effect)
        {
            this.Effect = Effect;
        }
        #endregion

        #region Properties
        public IPgEffect Effect { get; private set; }
        #endregion
    }
}
