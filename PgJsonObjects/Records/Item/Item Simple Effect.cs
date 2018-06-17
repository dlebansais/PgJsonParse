namespace PgJsonObjects
{
    public class ItemSimpleEffect : ItemEffect, IPgItemSimpleEffect
    {
        public ItemSimpleEffect(string Description)
        {
            this.Description = Description;
        }

        public string Description { get; private set; }

        public override string AsEffectString()
        {
            return Description;
        }

        public override bool Equals(object obj)
        {
            ItemSimpleEffect AsItemSimpleEffect;
            if ((AsItemSimpleEffect = obj as ItemSimpleEffect) != null)
                return AsItemSimpleEffect.Description == Description;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return Description.GetHashCode();
        }
    }
}
