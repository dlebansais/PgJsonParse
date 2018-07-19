namespace PgJsonObjects
{
    public class Gift
    {
        public Gift(ItemKeyword Keyword, IPgItem Item, double Value)
        {
            this.Keyword = Keyword;
            this.Item = Item;
            this.Value = Value;

            if (Value < 0)
                IsHated = true;
        }

        public bool IsHated { get; private set; }
        public ItemKeyword Keyword { get; private set; }
        public IPgItem Item { get; private set; }
        public double Value { get; private set; }

        public void SetHated()
        {
            IsHated = true;
            Value = -Value;
        }
    }
}
