namespace PgBuilder
{
    using PgJsonObjects;

    public class Mod
    {
        public Mod(IPgPower power)
        {
            Power = power;
        }

        public IPgPower Power { get; }
    }
}
