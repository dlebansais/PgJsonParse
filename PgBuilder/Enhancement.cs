namespace PgBuilder
{
    using PgJsonObjects;

    public class Enhancement
    {
        public Enhancement(DamageType damageType, string key)
        {
            DamageType = damageType;
            Key = key;
            PointCount = 0;
        }

        public DamageType DamageType { get; }
        public string Key { get; }
        public string Name { get { return DamageType.ToString(); } }
        public int PointCount { get; private set; }

        public void Increment()
        {
            PointCount++;
        }

        public void Decrement()
        {
            if (PointCount > 0)
                PointCount--;
        }

        public void SetPointCount(int value)
        {
            if (value >= 0)
                PointCount = value;
            else
                PointCount = 0;
        }
    }
}
